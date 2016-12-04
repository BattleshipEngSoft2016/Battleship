$('#home').find('.js-btn-play').on('click', function() {
  var levels = $('#home').find('.lyt-levels');
  var back = $('#home').find('.lyt-main');
  levels.removeClass('not-active');
  levels.addClass('active');
  back.addClass('active');
});

$('#home').find('.js-btn-shop').on('click', function() {
  var levels = $('#home').find('.lyt-shops');
  var back = $('#home').find('.lyt-main');
  levels.removeClass('not-active');
  levels.addClass('active');
  back.addClass('active');
});

$('#home').find('.btn-close').on('click', function() {
  var divContent = $(this).closest('.sh-div-default');
  var back = $('#home').find('.lyt-main');
  divContent.removeClass('active');
  divContent.addClass('not-active');
  if($(this).closest('div').attr('class').indexOf('modal-loja') == -1) back.removeClass('active');
});

$('#home').find('.pirata').on('click', function() {
  var modal = $('#home').find('.modal-pirates');
  modal.removeClass('not-active');
  modal.addClass('active');
});

$('#home').find('.espacial').on('click', function() {
  var modal = $('#home').find('.modal-space');
  modal.removeClass('not-active');
  modal.addClass('active');
});

$('#home').find('.coca').on('click', function() {
  var modal = $('#home').find('.modal-coca');
  modal.removeClass('not-active');
  modal.addClass('active');
});

$('#home').find('.sushi').on('click', function() {
  var modal = $('#home').find('.modal-sushi');
  modal.removeClass('not-active');
  modal.addClass('active');
});
