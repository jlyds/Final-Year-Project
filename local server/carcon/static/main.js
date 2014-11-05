$().ready(function() {

  // Auto reload image:
  var imgUrl = $('#tumbnail')[0].src;
  setInterval(function() {
    $('#tumbnail')[0].src = imgUrl + '?t=' + new Date().getTime();
  }, 500);

  // Control button:
  $('#controller button').click(function(e) {
    $.get($(this).data('url'));
  });
});


