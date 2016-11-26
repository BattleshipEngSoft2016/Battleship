$('#game-on').find('#1').on('click', function () {
    $('#game-on').find('.lyt-div-default').removeClass('active');
    $('#game-on').find('.lyt-div-default').removeClass('not-active');
    $('#game-on').find('.lyt-div-default').addClass('not-active');
    $('#game-on').find('.lyt-search-player').removeClass('not-active');
    $('#game-on').find('.lyt-search-player').addClass('active');
});

$('#game-on').find('#2').on('click', function () {
    $('#game-on').find('.lyt-div-default').removeClass('active');
    $('#game-on').find('.lyt-div-default').removeClass('not-active');
    $('#game-on').find('.lyt-div-default').addClass('not-active');
    $('#game-on').find('.lyt-found-player').removeClass('not-active');
    $('#game-on').find('.lyt-found-player').addClass('active');
});

$('#game-on').find('#3').on('click', function () {
    $('#game-on').find('.lyt-div-default').removeClass('active');
    $('#game-on').find('.lyt-div-default').removeClass('not-active');
    $('#game-on').find('.lyt-div-default').addClass('not-active');
    $('#game-on').find('.lyt-chose-skin').removeClass('not-active');
    $('#game-on').find('.lyt-chose-skin').addClass('active');
});

$('#game-on').find('#4').on('click', function () {
    $('#game-on').find('.lyt-div-default').removeClass('active');
    $('#game-on').find('.lyt-div-default').removeClass('not-active');
    $('#game-on').find('.lyt-div-default').addClass('not-active');
    $('#game-on').find('.lyt-position-boats').removeClass('not-active');
    $('#game-on').find('.lyt-position-boats').addClass('active');
});

$('#game-on').find('.js-confirm-skin').on('click', function () {
    $('#game-on').find('.lyt-div-default').removeClass('active');
    $('#game-on').find('.lyt-div-default').removeClass('not-active');
    $('#game-on').find('.lyt-div-default').addClass('not-active');
    $('#game-on').find('.lyt-position-boats').removeClass('not-active');
    $('#game-on').find('.lyt-position-boats').addClass('active');
});

$('#home').find('.js-btn-play').on('click', function () {
    var levels = $('#home').find('.lyt-levels');
    var back = $('#home').find('.lyt-main');
    levels.removeClass('not-active');
    levels.addClass('active');
    back.addClass('active');
});
