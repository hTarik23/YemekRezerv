$(document).on("click", ".deletemusteri", async function () {
    let musteriId = $(this).closest("tr").attr("id");
    if (musteriId > 0) {
        var requestData = { MusterId: parseInt(musteriId) };
            $.ajax({
                type: 'POST',
                url: '/Musteri/MusteriSil?musterId=' + musteriId,
                contentType: 'application/json',
                success: function (response) {
                    onSuccess(response);
                    if (response.isSuccess) {
                        document.getElementById(MusterId).remove();
                    }
                },
                error: function (response) {
                    messageAlertWithError();
                }
            });
    }
});

$(document).on("click", ".deleterestorant", async function () {
    let Restorantd = $(this).closest("tr").attr("id");
    if (Restorantd > 0) {
        var requestData = { Restorantd: parseInt(Restorantd) };
        $.ajax({
            type: 'POST',
            url: '/Restorant/RestorantSil?Restorantd=' + Restorantd,
            contentType: 'application/json',
            success: function (response) {
                onSuccess(response);
                if (response.isSuccess) {
                    document.getElementById(MusterId).remove();
                }
            },
            error: function (response) {
                messageAlertWithError();
            }
        });
    }
});

var dateInput = document.getElementById("dateInput");

dateInput.addEventListener("change", function () {
    var restaurantIdSelect = document.getElementById("RestaurantId");
    var timeInput = document.getElementById("timeInput");
    var sender = parseInt(restaurantIdSelect.value); // restaurantIdSelect öğesinin değerini alıyoruz
    fetch(`/Randevu/RandevuSaatleri?restorantId=${sender}`)
        .then(response => response.json())
        .then(data => {
            timeInput.innerHTML = '';

            if (data.length > 0) {
                data.forEach(function (time) {
                    var option = document.createElement("option");
                    option.value = time;
                    option.text = time;
                    timeInput.appendChild(option);
                });
            } else {
                var option = document.createElement("option");
                option.text = "No available times.";
                timeInput.appendChild(option);
            }
        })
        .catch(error => {
            console.error("Error fetching available times:", error);
        });
});
