$(document).ready(function () {
    function Contains(text_one, text_two) {
        if (text_one.indexOf(text_two) != -1) {
            return true;
        }
    }
    $(".table-search").addClass('hidden');
    $(".table .Search").addClass('hidden');
    $("#Search").keyup(function () {
        var searchText = $("#Search").val().toLowerCase();
        
        $(".Search").each(function () {
            if (!Contains($(this).text().toLowerCase(), searchText)) {
                $(this).addClass('hidden');
            }
            else {
                $(this).removeClass('hidden');  
            }
            if (searchText == "") {
                $(".table .Search").addClass('hidden');
                $(".table-search").addClass('hidden');
            }
            else {
                $(".table-search").removeClass('hidden');
            }
        });
        
    });
    
});