// Variables
var playerFleet, cpuFleet;
var attemptedHits = [];
var jsonShips = [];
var count = 0;
var tamGrid = 10;
var url = "";
var ws = null;
var lDoisJogadores = "false";
var lVez = false;
var lastShoot;
var nivelId = 0;
var ships = [];
var skins = {};
var tempo;
var tab;
function nivelGrid(nivelId, tamGrid, qtdPortaAvioes, qtdDestroiers, qtdEncouracados, qtdCruzadores, qtdSubmarinos) {
    nivelId = nivelId;
    tamGrid = tamGrid;

    for (var i = 0; i < qtdPortaAvioes; i++) {
        ships.push({ "name": "porta_avioes", "length": 5 });
    }

    for (var i = 0; i < qtdDestroiers; i++) {
        ships.push({ "name": "destroier", "length": 4 });
    }

    for (var i = 0; i < qtdEncouracados; i++) {
        ships.push({ "name": "encouraçado", "length": 3 });
    }

    for (var i = 0; i < qtdCruzadores; i++) {
        ships.push({ "name": "cruzador", "length": 2 });
    }

    for (var i = 0; i < qtdSubmarinos; i++) {
        ships.push({ "name": "submarino", "length": 1 });
    }

}

function atualizaSkin(skins) {
    skins = skins;
}

function iniciaSocket(cNome, idNivel, idUser, tabuleiro) {

    tab = tabuleiro;
    url = 'ws://localhost:26532/WebSocketsServer2.ashx?chatName=' + cNome + '&NivelId=' + idNivel + '&Id=' + idUser;
    ws = new WebSocket(url);

    ws.onerror = function (e) {
        alert('Problemas com a conexão: ' + e.message);
    };

    ws.onopen = function () {
        //conversation.innerHTML = 'Client connected <br/>';
    };

    ws.onmessage = function (e) {

        var oMessage = JSON.parse(e.data);
        var tipo = oMessage.TipoMensagem;
        if (tipo == 6) {

            $('#game-on').find('.lyt-search-player').removeClass('active');
            $('#game-on').find('.lyt-search-player').addClass('not-active');
            $('#game-on').find('.lyt-position-boats').removeClass('not-active');
            $('#game-on').find('.lyt-position-boats').addClass('active');

            //var m = '{"TipoMensagem":1,"Objeto":[{"TipoBarco":"submarino","Coordenadas":["c10"],"IdBarco":5}]}';
            ws.send(tab);
        } else if (tipo == 14) {
            lVez = true;
            alert('É a sua vez, você tem 15 segundos');
            var t = '{"TipoMensagem" : 3, "Objeto":"Tempo esgotado"}';
            tempo = setTimeout(function () { alert("Tempo esgotado!"); lVez = false; ws.send(t); }, 15000)
        } else if (tipo == 1) {
            alert(oMessage.Mensagem);
            $('#game-on').find('#' + lastShoot).addClass('hitLi');
        } else if (tipo == 2) {
            alert(oMessage.Mensagem);
            $('#game-on').find('#' + lastShoot).addClass('missLi');
        } else {
            alert(oMessage.Mensagem);
        }

    };

    ws.onclose = function () {
        alert('Jogo finalizado!');
    };
}
// Object Constructors
function Fleet(name) {
    this.name = name;
    this.shipDetails = ships;
    this.numOfShips = this.shipDetails.length;
    this.ships = [];
    this.currentShipSize = 0;
    this.currentShip = 0;
    this.initShips = function () {
        for (var i = 0; i < this.numOfShips; i++) {
            this.ships[i] = new Ship(this.shipDetails[i].name);
            this.ships[i].length = this.shipDetails[i].length;
        }
    };
    this.removeShip = function (pos) {
        this.numOfShips--;
        $(".text").text(output.sunk(this.name, this.ships[pos].name));
        if (this == playerFleet) bot.sizeOfShipSunk = this.ships[pos].length;
        this.ships.splice(pos, 1);
        if (this.ships.length == 0) {
            $(".text").text(output.lost(this.name));
        }
        return true;
    };
}

function Ship(name) {
    this.name = name;
    this.length = 0;
    this.hitPoints = [];
    this.populateHorzHits = function (start) {
        for (var i = 0; i < this.length; i++, start++) {
            this.hitPoints[i] = start;
        }
    };
    this.populateVertHits = function (start) {
        for (var i = 0; i < this.length; i++, start += tamGrid) { //teste
            this.hitPoints[i] = start;
        }
    };
    this.checkLocation = function (loc) {
        for (var i = 0; i < this.length; i++) {
            if (this.hitPoints[i] == loc) return true;
        }
        return false;
    };
}

// Console obj
var output = {
    "welcome": " > Bem vindo ao WarShip.  Use o menu acima para começar.",
    "not": " > Essa opção não está disponível.",
    "self": " > Use o mouse para clicar nos botões Horizontal e Vertical para colocar seus navios no grid ao lado.",
    "overlap": " > Você não pode sobrepor navios.  Por favor, tente novamente.",
    "start": " > Clique no botão 'Pronto pra guerra' para começar.  Boa sorte!",
    "youwin": " > Você ganhou! Parabéns ;)",
    "youlose": " > Você perdeu todos os seus navios! Game Over.",
    placed: function (name) { return " > Seu " + name + "  colocado."; },
    hit: function (name, type) { return " > Seu " + name + " foi atingido." },
    miss: function (name) { return " > Seu " + name + " foi perdido!" },
    sunk: function (user, type) { return " > O navio " + type + " do adversário foi afundado!" },
    lost: function (name) { return " > " + name + " Você perdeu todos os seus navios!!  Game Over." },
};

//  Create the games grids and layout
$(document).ready(function () {
    for (var i = 1; i <= (tamGrid * tamGrid) ; i++) {
        // The number and letter designators
        $(".grid").append("<li class='points offset1 " + i + "'></li>");

        if (i < tamGrid + 1) {
            $(".top").prepend("<span class='aTops'>" + Math.abs(i - (tamGrid + 1)) + "</span>");
            $(".bottom").prepend("<span class='aTops'>" + Math.abs(i - (tamGrid + 1)) + "</span>");
        }
        if (i == tamGrid + 1) {
            $(".top").prepend("<span class='aTops hidezero'></span>");
            $(".bottom").prepend("<span class='aTops hidezero'></span>");
        }
        if (i > (tamGrid * (tamGrid - 1))) {
            $(".top").append("<span class='aLeft'>" +
								String.fromCharCode(97 + (i - ((tamGrid * (tamGrid - 1)) + 1))).toUpperCase() + "</span>");
            $(".bottom").append("<span class='aLeft'>" +
								String.fromCharCode(97 + (i - ((tamGrid * (tamGrid - 1)) + 1))).toUpperCase() + "</span>");
        }
    }

    var ul = $('.grid')[0];
    ul.style.width = tamGrid == 10 ? "300px" : "446px"; // width in PIXELS
    ul.style.height = tamGrid == 10 ? "300px" : "446px"; // width in PIXELS
    $('.grid').css('cssText', 'background-image: url(' + skins.ImagemCoordenada + ');  !important;');


    var lis = $(".bottom > .grid > li");
    var j = 0;
    var aux = 0;
    for (i = 0; i < lis.length; i++) {
        if (j < tamGrid) {
            lis[i].id = String.fromCharCode(97 + aux) + (j + 1);
            j++
        }
        else {
            j = 0;
            aux++;
            lis[i].id = String.fromCharCode(97 + aux) + (j + 1);
            j++;
        }
    }

    var lis = $(".top > .grid > li");
    var j = 0;
    var aux = 0;
    for (i = 0; i < lis.length; i++) {
        if (j < tamGrid) {
            lis[i].id = String.fromCharCode(97 + aux) + (j + 1);
            lis[i].addEventListener("click", hit, false);
            j++
        }
        else {
            j = 0;
            aux++;
            lis[i].id = String.fromCharCode(97 + aux) + (j + 1);
            lis[i].addEventListener("click", hit, false);
            j++;
        }
    }

    $(".text").text(output.welcome);
})

var hit = function hit() {
    if (!($('#game-on').find('#' + this.id).hasClass('hitLi') || $('#game-on').find('#' + this.id).hasClass('missLi'))) {
        if (lVez) {
            lVez = false;
            clearTimeout(tempo);
            var t = '{"TipoMensagem" : 2, "Objeto":["' + this.id + '"]}';
            lastShoot = this.id;
            ws.send(t);
        } else { alert('Aguarde a sua vez!'); }
    }
}

// Start the game setup
$(document).ready(function () {

    $(".self").off("click").on("click", function () {
        $(".text").text(output.self);
        selfSetup(playerFleet);
    });
    $(".random").off("click").on("click", function () {
        playerFleet = new Fleet("Player 1");
        playerFleet.initShips();
        randomSetup(playerFleet);
    });

});

function selfSetup() {
    $(".self").addClass("horz").removeClass("self").text("Horizontal");
    $(".random").addClass("vert").removeClass("random").text("Vertical");

    // initialize the fleet
    playerFleet = new Fleet("Player 1");
    playerFleet.initShips();
    // light up the players ship board for placement
    placeShip(playerFleet.ships[playerFleet.currentShip], playerFleet);
}

function randomSetup(fleet) {
    // Decide if the ship will be placed vertically or horizontally 
    // if 0 then ship will be places horizontally if 1 vertically
    // setShip(location, ship, "vert", fleet, "self");
    if (fleet.currentShip >= fleet.numOfShips) return; // regard against undefined length

    var orien = Math.floor((Math.random() * tamGrid) + 1);
    var length = fleet.ships[fleet.currentShip].length;

    if (orien <= (tamGrid / 2)) {
        // create a random number betwee 1 and 6 // alterado
        var shipOffset = (tamGrid + 1) - fleet.ships[fleet.currentShip].length;
        //var horiz = Math.floor((Math.random() * shipOffset) + 1);		
        //var vert = Math.floor(Math.random() * (tamGrid-1));

        var horiz = Math.floor((Math.random() * shipOffset) + 1);
        var vert = Math.floor((Math.random() * shipOffset) + 1);

        //var randNum = parseInt(String(vert) + String(horiz));
        var randNum = vert * tamGrid + horiz;

        if (fleet == cpuFleet) checkOverlap(randNum, length, "horz", fleet);
        else setShip(randNum, fleet.ships[fleet.currentShip], "horz", fleet, "random");
    } else {
        var shipOffset = (tamGrid * (tamGrid + 1)) - (fleet.ships[fleet.currentShip].length * tamGrid);
        var randNum = Math.floor((Math.random() * shipOffset) + 1);

        if (fleet == cpuFleet) checkOverlap(randNum, length, "vert", fleet);
        else setShip(randNum, fleet.ships[fleet.currentShip], "vert", fleet, "random");
    }
}

function createCpuFleet() {
    // create a random ship placement for the cpu's fleet
    cpuFleet = new Fleet("CPU");
    cpuFleet.initShips();
    randomSetup(cpuFleet);
}


function placeShip(ship, fleet) {
    // check orientation of ship and highlight accordingly
    var orientation = "horz";
    $(".vert").off("click").on("click", function () {
        orientation = "vert";
    });
    $(".horz").off("click").on("click", function () {
        orientation = "horz";
    });
    // when the user enters the grid have the ships lenght highlighted with the
    // ships length.
    $(".bottom").find(".points").off("mouseenter").on("mouseenter", function () {
        var num = $(this).attr('class').slice(15);
        //
        if (orientation == "horz") displayShipHorz(parseInt(num), ship, this, fleet);
        else displayShipVert(parseInt(num), ship, this, fleet);
    });
}


function displayShipHorz(location, ship, point, fleet) {
    var endPoint = location + ship.length - 2;
    if (!(endPoint % tamGrid >= 0 && endPoint % tamGrid < ship.length - 1)) {
        for (var i = location; i < (location + ship.length) ; i++) {
            $(".bottom ." + i).addClass("highlight");
        }
        $(point).off("click").on("click", function () {
            setShip(location, ship, "horz", fleet, "self");
        });
    }
    $(point).off("mouseleave").on("mouseleave", function () {
        removeShipHorz(location, ship.length);
    });
}

function displayShipVert(location, ship, point, fleet) {
    var endPoint = (ship.length * tamGrid) - tamGrid;
    var inc = 0;
    if (location + endPoint <= (tamGrid * tamGrid)) {
        for (var i = location; i < (location + ship.length) ; i++) {
            $(".bottom ." + (location + inc)).addClass("highlight");
            inc = inc + tamGrid;
        }
        $(point).off("click").on("click", function () {
            setShip(location, ship, "vert", fleet, "self");
        });
    }
    $(point).off("mouseleave").on("mouseleave", function () {
        removeShipVert(location, ship.length);

    });
}

function removeShipHorz(location, length) {
    for (var i = location; i < location + length; i++) {
        $(".bottom ." + i).removeClass("highlight");
    }
}

function removeShipVert(location, length) {
    var inc = 0;
    for (var i = location; i < location + length; i++) {
        $(".bottom ." + (location + inc)).removeClass("highlight");
        inc = inc + tamGrid;
    }
}

function setShip(location, ship, orientation, genericFleet, type) {
    if (!(checkOverlap(location, ship.length, orientation, genericFleet))) {
        if (orientation == "horz") {
            genericFleet.ships[genericFleet.currentShip].populateHorzHits(location);
            $(".text").text(output.placed(genericFleet.ships[genericFleet.currentShip].name + " foi"));
            var j = 1;
            var fleet = { TipoBarco: "", Coordenadas: [], IdBarco: count };
            for (var i = location; i < (location + ship.length) ; i++) {
                fleet.TipoBarco = genericFleet.ships[genericFleet.currentShip].name;
                fleet.Coordenadas.push($(".bottom ." + i)[0].id);
                var urlNav = "";
                var barco = genericFleet.ships[genericFleet.currentShip].name;
                switch (barco) {
                    case "porta_avioes":
                        urlNav = skins.ImagemNav01;
                        break;
                    case "destroier":
                        urlNav = skins.ImagemNav02;
                        break;
                    case "encouraçado":
                        urlNav = skins.ImagemNav03;
                        break;
                    case "cruzador":
                        urlNav = skins.ImagemNav04;
                        break;
                    case "submarino":
                        urlNav = skins.ImagemNav05;
                        break;
                }

                $(".bottom ." + i).addClass(genericFleet.ships[genericFleet.currentShip].name);
                $(".bottom ." + i).addClass(genericFleet.ships[genericFleet.currentShip].name + j);
                $(".bottom ." + i).addClass("horz-ship");
                $(".bottom ." + i).children().removeClass("hole");
                $('.' + genericFleet.ships[genericFleet.currentShip].name + j).css('cssText', 'background-image: url(' + urlNav + ');  !important;');
                j++;
            }
            if (++genericFleet.currentShip == genericFleet.numOfShips) {
                $(".text").text(output.placed("ships have"));
                $(".bottom").find(".points").off("mouseenter");
                // clear the call stack
                setTimeout(createCpuFleet, 100);
            } else {
                if (type == "random") randomSetup(genericFleet);
                else placeShip(genericFleet.ships[genericFleet.currentShip], genericFleet);
            }
            jsonShips.push(fleet);
        } else {
            var inc = 0;
            genericFleet.ships[genericFleet.currentShip].populateVertHits(location);
            $(".text").text(output.placed(genericFleet.ships[genericFleet.currentShip].name + " foi"));
            var j = 1;
            var fleet = { TipoBarco: "", Coordenadas: [], IdBarco: count };
            for (var i = location; i < (location + ship.length) ; i++) {
                fleet.TipoBarco = genericFleet.ships[genericFleet.currentShip].name;
                fleet.Coordenadas.push($(".bottom ." + (location + inc))[0].id);
                $(".bottom ." + (location + inc)).addClass(genericFleet.ships[genericFleet.currentShip].name);
                $(".bottom ." + (location + inc)).addClass(genericFleet.ships[genericFleet.currentShip].name + j);
                $(".bottom ." + (location + inc)).addClass("vert-ship");
                if (i == location) $(".bottom ." + i).addClass("border-nav");
                $(".bottom ." + (location + inc)).children().removeClass("hole");
                j++;
                inc = inc + tamGrid;
            }
            if (++genericFleet.currentShip == genericFleet.numOfShips) {
                $(".text").text(output.placed("navio "));
                $(".bottom").find(".points").off("mouseenter");
                // clear the call stack
                setTimeout(createCpuFleet, 100);
            } else {
                if (type == "random") randomSetup(genericFleet);
                else placeShip(genericFleet.ships[genericFleet.currentShip], genericFleet);
            }
            jsonShips.push(fleet);
        }
    } else {
        if (type == "random") randomSetup(genericFleet);
        else $(".text").text(output.overlap);
    }
} // end of setShip()

function checkOverlap(location, length, orientation, genFleet) {
    var loc = location;
    if (orientation == "horz") {
        var end = location + length;
        for (; location < end; location++) {
            for (var i = 0; i < genFleet.currentShip; i++) {
                if (genFleet.ships[i].checkLocation(location)) {
                    if (genFleet == cpuFleet) randomSetup(genFleet);
                    else return true;
                }
            } // end of for loop
        } // end of for loop
    } else {
        var end = location + (tamGrid * length);
        for (; location < end; location += tamGrid) {
            for (var i = 0; i < genFleet.currentShip; i++) {
                if (genFleet.ships[i].checkLocation(location)) {
                    if (genFleet == cpuFleet) randomSetup(genFleet);
                    else return true;
                }
            }
        }
    } // end of if/else 
    if (genFleet == cpuFleet && genFleet.currentShip < genFleet.numOfShips) {
        if (orientation == "horz") genFleet.ships[genFleet.currentShip++].populateHorzHits(loc);
        else genFleet.ships[genFleet.currentShip++].populateVertHits(loc);
        if (genFleet.currentShip == genFleet.numOfShips) {
            // clear the call stack
            setTimeout(startGame, 500);
        } else randomSetup(genFleet);
    }
    return false;
} // end of checkOverlap()


function startGame() {

    $(".topPanel").css({ "display": "block" });
    document.getElementsByClassName('layout')[0].innerHTML = " <div class='buttons wartime' style='width:240px;'>Pronto pra guerra!</div>";

    $(".wartime").off("click").on("click", function () {
        var skinId = $('.selectpicker').val();
        var nivelId = 1;
        var dados = JSON.stringify(jsonShips);

        var data = "{";
        data += "'skinId':'" + skinId + "',";
        data += "'nivelId':'" + nivelId + "',";
        data += "'dados':'" + dados + "'";
        data += "}";

        $.ajax({
            type: "POST",
            data: data,
            url: "http://localhost:26352/GameOn/Cadastrar",
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",

            success: function (output) {
                // window.location = "../Game/Index";
                window.location = "http://localhost:26532/Game/Index"
            }
        });

    });

    $(".text").text(output.start);
}
