$('#home').find('.js-btn-play').on('click', function() {
  var levels = $('#home').find('.lyt-levels');
  var back = $('#home').find('.lyt-main');
  levels.removeClass('not-active');
  levels.addClass('active');
  back.addClass('active');
});
