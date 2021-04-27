
$().ready(function () {

    $.ajaxSetup({ cache: false })

    $('#btnUpload').click(function () {

        if ($('#form1').valid()) {

            $('#prijavaPredana').hide();
            $('#exampleModalCenter').modal();
        }
        else {
            var validator = $("#form1").validate();
            validator.focusInvalid();
        }
    }
    );

    jQuery.validator.addMethod("isDate", function (value, element) {
        var dateRegex = /^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-.\/])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$/;
        return this.optional(element) || dateRegex.test(value);
    }, "Upišite ispravan datum");

    jQuery.validator.addMethod("isPositiveNumber", function (value, element) {
        var dateRegex = /^[1-9]\d*$/;
        return this.optional(element) || dateRegex.test(value);
    }, "Upišite pozitivan broj");

    jQuery.validator.addMethod("maxFilesLenValid", function (value, element) {
        var filesSize = 0;
        var maxFilesLen = 5242880;

        for (var i = 0; i < element.files.length; i++) {
            filesSize += element.files[i].size;
        }

        return filesSize <= maxFilesLen;
    }, "Maximalna dozvoljena veličina datoteka za presliku isprave je 5 MB");

    jQuery.validator.addMethod("isSifraKandidataValid", function (value, element) {
        var sifraKandidataRegex = /^[A-Za-zšđčćžŠĐČĆŽ]{2,3}[0-9]{2}[A-Za-zšđčćžŠĐČĆŽ]{3}$/;
        return sifraKandidataRegex.test(value);
    }, "Neispravana šifra kandidata");

    //jQuery.validator.addMethod("isIspitPolazemGreaterThenOne", function (value, element) {
    //    return $("#IspitPolazem").val() > 1;
    //}, "");

    $(function () {
        $("#form1").validate({
            rules: {
                "Ime": "required",
                "Prezime": "required",
                "OIB": {
                    required: true,
                    rangelength: [11, 11]
                },
                "DatumRodjenja": {
                    required: true,
                    isDate: true
                },
                "MjestoRodjenja": "required",
                "DrzavaRodjenja": "required",

                "Ulica": "required",
                "KucniBroj": "required",
                "Grad": "required",

                "BrojTelefona": "required",
                "Email": {
                    required: true,
                    email: true
                },
                "StrucnaSprema": "required",
                "IspitPolazem": {
                    required: true,
                    isPositiveNumber: true
                },
                "SifraKandidata": {
                    required: true,
                    isSifraKandidataValid: true
                },
                "PreslikeOSpremi": {
                    maxFilesLenValid: true,
                    required: function () {
                         return $("#IspitPolazem").val() == 1;
                    }
                },
                "DokaziOUplacenojNaknadi": {
                    required: true,
                    maxFilesLenValid: true
                },
                "Izjave": {
                    required: true,
                    maxFilesLenValid: true
                },
                "Suglasnost": {
                    required: true,
                    maxFilesLenValid: true
                },
                "OstalaDokumentacija": {
                    maxFilesLenValid: true
                }
            },
            messages: {
                "Ime": "Ime je obavezan podatak",
                "Prezime": "Prezime je obavezan podatak",
                "OIB": {
                    required: "OIB je obavezan podatak",
                    rangelength: "Unesite ispravan OIB."
                },
                "DatumRodjenja": {
                    required: "Upišite datum rođenja",
                    isDate: "Upišite ispravan datum"
                },
                "MjestoRodjenja": "Mjesto rođenja je obavezan podatak",
                "DrzavaRodjenja": "Država rođenja je obavezan podatak",

                "Ulica": "Ulica je obavezan podatak",
                "KucniBroj": "Kućni broj je obavezan podatak",
                "Grad": "Grad je obavezan podatak",

                "BrojTelefona": "Broj telefona je obavezan podatak",
                "Email": {
                    required: "Upišite email",
                    email: "Unesite ispravan email."
                },
                "StrucnaSprema": "Stručna sprema je obavezna podatak",
                "IspitPolazem": {
                    required: "Upišite broj pristupa ovom ispitu",
                    isPositiveNumber: "Upišite pozitivan broj"
                },
                "SifraKandidata": {
                    required: "Šifra kandidata je obavezan podatak",
                    isSifraKandidataValid: "Neispravana šifra kandidata"
                },
                "PreslikeOSpremi": {
                    required: "Priložite preslike o spremi",
                    maxFilesLenValid: "Maximalna dozvoljena veličina datoteka za presliku isprave je 5 MB"
                },
                "DokaziOUplacenojNaknadi": {
                    required: "Priložite dokaze o uplaćenoj naknadi",
                    maxFilesLenValid: "Maximalna dozvoljena veličina datoteka za dokaz o uplaćenoj naknadi je 5 MB"
                },
                "Izjave": {
                    required: "Priložite izjavu o ispunjavanju uvjeta primjerenosti",
                    maxFilesLenValid: "Maximalna dozvoljena veličina datoteka za dokaz o uplaćenoj naknadi je 5 MB"
                },
                "Suglasnost": {
                    required: "Priložite suglasnost za objavu osobnih podataka u registru ovlaštenih zastupnika u osiguranju, brokera u osiguranju i/ili reosiguranju i sporednih posrednika ",
                    maxFilesLenValid: "Maximalna dozvoljena veličina datoteka za dokaz o uplaćenoj naknadi je 5 MB"
                },
                "OstalaDokumentacija": {
                    maxFilesLenValid: "Maximalna dozvoljena veličina datoteka za ostalu dokumetaciju je 5 MB"
                }
            }
        });
    });
});


var fileDownloadCheckTimer;
function blockUIForDownload() {
    fileDownloadCheckTimer = window.setInterval(function () {
        if ($.cookie('onLinePrijaveDistributeriDownloadPDF')) {
            finishDownload();
        }
    }, 1000);
}

function finishDownload() {
    window.clearInterval(fileDownloadCheckTimer);

    //document.cookie = 'onLinePrijaveDistributeriDownloadPDF' + '=;expires=Thu, 01 Jan 1970 00:00:01 GMT;path=/';
    //onLinePrijaveDistributeriDownloadPDF =; expires = Tue, 26 May 2020 11: 44: 38 GMT; path = /
    setCookie('onLinePrijaveDistributeriDownloadPDF', 'null', -1);

    $('#generiranjePotvrde').modal('hide');

    // clear input files
    $("#PreslikeOSpremi").val(null);
    $("#DokaziOUplacenojNaknadi").val(null);
    $("#Izjave").val(null);
    $("#Suglasnost").val(null);
    $("#OstalaDokumentacija").val(null);

    $('#prijavaPredana').fadeIn();
}

// Cookie functions from w3schools
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

$("#DatumRodjenja").datepicker({
    closeText: 'Zatvori',
    prevText: 'Prethodni mjesec',
    nextText: 'Slijedeći mjesec',
    currentText: 'Danas',
    monthNames: ['Siječanj', 'Veljača', 'Ožujak', 'Travanj', 'Svibanj', 'Lipanj',
        'Srpanj', 'Kolovoz', 'Rujan', 'Listopad', 'Studeni', 'Prosinac'],
    monthNamesShort: ['Sij.', 'Velj.', 'Ožu.', 'Tra.', 'Svi.', 'Lip.',
        'Srp.', 'Kol.', 'Ruj.', 'Lis.', 'Stu.', 'Pro.'],
    dayNames: ['Nedjelja', 'Ponedjeljak', 'Utorak', 'Srijeda', 'Četvrtak', 'Petak', 'Subota'],
    dayNamesShort: ['Ned.', 'Pon.', 'Uto.', 'Sri.', 'Čet.', 'Pet.', 'Sub.'],
    dayNamesMin: ['N', 'P', 'U', 'S', 'Č', 'P', 'S'],
    weekHeader: 'Tjedan',
    dateFormat: 'dd.mm.yy',
    firstDay: 1,
    isRTL: false,
    showMonthAfterYear: false,
    yearSuffix: '',
    maxDate: 0
});
$.datepicker.setDefaults($.datepicker.regional['hr']);
$("#DatumRodjenja").datepicker("setDate", new Date());

function UploadFiles(registrationDate) {

    var preslikaIspraveFiles = document.getElementById('PreslikeOSpremi').files;
    var dokaziOUplacenojNaknadi = document.getElementById('DokaziOUplacenojNaknadi').files;
    var izjave = document.getElementById('Izjave').files;
    var suglasnost = document.getElementById('Suglasnost').files;
    var ostalaDokumentacija = document.getElementById('OstalaDokumentacija').files;

    var totalByteLength = 0;    

    for (var i = 0, l = preslikaIspraveFiles.length; i < l; i++) {
        totalByteLength += preslikaIspraveFiles[i].size;
    }

    for (var i = 0, l = dokaziOUplacenojNaknadi.length; i < l; i++) {
        totalByteLength += dokaziOUplacenojNaknadi[i].size;
    }

    for (var i = 0, l = izjave.length; i < l; i++) {
        totalByteLength += izjave[i].size;
    }

    for (var i = 0, l = suglasnost.length; i < l; i++) {
        totalByteLength += suglasnost[i].size;
    }

    for (var i = 0, l = ostalaDokumentacija.length; i < l; i++) {
        totalByteLength += ostalaDokumentacija[i].size;
    }

    $('#total').val(totalByteLength / 1024 / (chunkSizeInMB * 1024));
    $('#counter').val(0);
    $('#filesUploaded').val(0);

    $('#filesToUpload').val(preslikaIspraveFiles.length +
                                dokaziOUplacenojNaknadi.length +
                                izjave.length +
                                suglasnost.length +
                                ostalaDokumentacija.length);

    $('.progress-bar').css('width', '0%')
        .attr('aria-valuenow', 0);
    $("#percentageText").text("0%");

    $('#exampleModal').modal();

    for (var i = 0; i < preslikaIspraveFiles.length; i++) {
        UploadFile(preslikaIspraveFiles[i], 'PreslikaIsprave', i + 1, registrationDate);
    }

    for (var i = 0; i < dokaziOUplacenojNaknadi.length; i++) {
        UploadFile(dokaziOUplacenojNaknadi[i], 'DokaziOUplacenojNaknadi', i + 1, registrationDate);
    }

    for (var i = 0; i < izjave.length; i++) {
        UploadFile(izjave[i], 'Izjave', i + 1, registrationDate);
    }

    for (var i = 0; i < suglasnost.length; i++) {
        UploadFile(suglasnost[i], 'Suglasnost', i + 1, registrationDate);
    }

    for (var i = 0; i < ostalaDokumentacija.length; i++) {
        UploadFile(ostalaDokumentacija[i], 'OstalaDokumentacija', i + 1, registrationDate);
    }
}

function UploadFileChunk(FileChunks, FileName, CurrentPart, TotalPart) {
    var formData = new FormData();
    formData.append('file', FileChunks[CurrentPart - 1], FileName);

    jQuery(function ($) {
        $.ajax({
            type: "POST",
            url: 'UploadFiles',
            contentType: false,
            processData: false,
            data: formData,
            xhr: function () {
                var xhr = new window.XMLHttpRequest();
                xhr.upload.addEventListener("progress", function (e, data) {
                }, false);
                return xhr;
            },
            success: function (data, textStatus, jqXHR) {
                if (data.status == true) {
                    if (TotalPart == CurrentPart) {
                        console.log("whole file uploaded successfully");

                        var filesUploaded = parseInt($('#filesUploaded').val()) + 1;
                        $('#filesUploaded').val(filesUploaded);

                        if ($('#filesUploaded').val() == $('#filesToUpload').val()) {
                            $('.progress-bar').css('width', '100%')
                                .attr('aria-valuenow', percentage);
                            $("#percentageText").text("100%");

                            $('#exampleModal').modal('hide');

                             // snimanje u bazu
                            SaveDataToDatabase();

                            $('#pozdravniDialog').modal();
                        }
                    }   
                    else {
                        var count = parseInt($('#counter').val()) + 1;
                        $('#counter').val(count);

                        console.log("success:", $('#counter').val(), $('#total').val());

                        var percentage = Math.floor($('#counter').val() / $('#total').val() * 100);

                        $('.progress-bar').css('width', percentage + '%')
                            .attr('aria-valuenow', percentage);
                        $("#percentageText").text(Math.round(percentage) + "%");

                        UploadFileChunk(FileChunks, FileName, CurrentPart + 1, TotalPart);

                        console.log("success:", percentage);
                    }
                }
                else {
                    console.log("failed to upload file part no: " + CurrentPart);
                    JL("Distributeri_UploadFileChunk").fatal("failed to upload file part no: " + CurrentPart);
                    alert('"failed to upload file part no: " + CurrentPart');
                }
            },
            error: function (xhr, textStatus, thrownError) {

                console.log("error to upload file part no: " + CurrentPart);
                console.log("xhr.status: " + xhr.status);
                console.log("textStatus: " + textStatus);
                JL("Distributeri_UploadFileChunk").fatal("failed to upload file part no: " + CurrentPart);
                alert('"failed to upload file part no: " + CurrentPart');
            }
        }).retry({ times: 10, timeout: 10000 }).then(function () {
            console.log("success from retry done");
        });
    });
}

function DownloadPDF() {
    try {
        JL("Distributeri_DownloadPDF").info("Počinjem download PDF potvrde.");

        var token = new Date().getTime(); //use the current timestamp as the token value

        $('#generiranjePotvrde').modal();
        blockUIForDownload();

        window.location = serverRoot + '/Distributeri/DownloadPDF?DownloadToken=' + token;

        JL("Distributeri_DownloadPDF").info("Gotov download PDF potvrde.");
    } catch (e) {
        JL("Distributeri_DownloadPDF").fatalException("Greška kod download potvrde", e);
    }
}

function SaveDataToDatabase() {
    JL("Distributeri_SaveDataToDatabase").info("Snimam podatke u bazu.");

    $('#pohranaPodataka').modal();

    jQuery(function ($) {
        $.ajax({
            type: "GET",
            url: 'SaveDataToDatabase',
            contentType: false,
            processData: false,
            success: function (data, textStatus, jqXHR) {
                if (data.status == true) {
                    $('#pohranaPodataka').modal('hide');
                    DownloadPDF();
                }
                else {
                    $('#pohranaPodataka').modal('hide');

                    JL("Distributeri_SaveDataToDatabase").fatal(
                        {
                            "msg": "Greška",
                            "poruka": data.message
                        });

                    alert('Snimanje podataka nije uspjelo');
                }
            },
            error: function (xhr, textStatus, thrownError) {
                JL("Distributeri_SaveDataToDatabase").fatal("Snimanje podataka nije uspjelo");
                $('#pohranaPodataka').modal('hide');
                alert('Snimanje podataka nije uspjelo');
            }
        });
    });

    JL("Distributeri_SaveDataToDatabase").info("Gotovo snimanje podataka u bazu.");
}

function SaveFormData() {
    var registrationDate = moment().format('YYYYMMDDHHmmss');
    $('#exampleModalCenter').modal('hide');
    var formData = new FormData();

    formData.append('Ime', $('#Ime').val());
    formData.append('Prezime', $('#Prezime').val());
    formData.append('OIB',$('#OIB').val());
    formData.append('DatumRodjenja', $('#DatumRodjenja').val());
    formData.append('MjestoRodjenja', $('#MjestoRodjenja').val());
    formData.append('DrzavaRodjenja', $('#DrzavaRodjenja').val());

    formData.append('Ulica', $('#Ulica').val());
    formData.append('KucniBroj', $('#KucniBroj').val());
    formData.append('Grad', $('#Grad').val());

    formData.append('BrojTelefona', $('#BrojTelefona').val());
    formData.append('Email', $('#Email').val());
    formData.append('StrucnaSprema', $('#StrucnaSprema').val());
    formData.append('Zanimanje', $('#Zanimanje').val());
    formData.append('ZaposlenKod', $('#ZaposlenKod').val());
    formData.append('IspitPolazem', $('#IspitPolazem').val());
    formData.append('SifraKandidata', $('#SifraKandidata').val());
    formData.append('ZivotnoOsiguranje',$('#ZivotnoOsiguranje').prop("checked"));
    formData.append('NezivotnoOsiguranje', $('#NezivotnoOsiguranje').prop("checked"));
    formData.append('DistribucijaOsiguranja', $('#DistribucijaOsiguranja').prop("checked"));
    formData.append('DistribucijaReosiguranja', $('#DistribucijaReosiguranja').prop("checked"));
    formData.append('ZastupnikUOsiguranju', $('#ZastupnikUOsiguranju').prop("checked"));
    formData.append('BrokerUOsiguranju', $('#BrokerUOsiguranju').prop("checked"));
    formData.append('BrokerUReosiguranju', $('#BrokerUReosiguranju').prop("checked"));
    formData.append('PosrednikUOsiguranju', $('#PosrednikUOsiguranju').prop("checked"));
    formData.append('VrijemePrijave', registrationDate);
    formData.append('PosaljiNaEmail', $('#PosaljiNaEmail').prop("checked"));

    jQuery(function ($) {
        $.ajax({
            type: "POST",
            url: 'SaveFormData',
            contentType: false,
            processData: false,
            data: formData,
            success: function (data, textStatus, jqXHR) {
                if (data.status == true) {
                    UploadFiles(registrationDate);
                }
                else {
                    var errors = data['errors'];
                    displayValidationErrors(errors);
                }
            },
            error: function (xhr, textStatus, thrownError) {

                console.log("xhr.status: " + xhr.status);
                console.log("textStatus: " + textStatus);
                JL("Distributeri_SaveFormData").fatal("Snimanje forme nije uspjelo");
                alert('Snimanje forme nije uspjelo');
            }
        });
    });
}

function UploadFile(targetFile, typeOfDocument, partNumber, registrationDate) {
    // create array to store the buffer chunks
    var FileChunks = [];
    // the file object itself that we will work with
    var file = targetFile;
    // set up other initial vars
    var MaxFileSizeMB = chunkSizeInMB;
    var BufferChunkSize = MaxFileSizeMB * (1024 * 1024);
    var FileStreamPos = 0;
    // set the initial chunk length
    var EndPos = BufferChunkSize;
    var Size = file.size;

    // add to the FileChunk array until we get to the end of the file
    while (FileStreamPos < Size) {
        // "slice" the file from the starting position/offset, to  the required length
        FileChunks.push(file.slice(FileStreamPos, EndPos));
        FileStreamPos = EndPos; // jump by the amount read
        EndPos = FileStreamPos + BufferChunkSize; // set next chunk length
    }

    var fileNameExt = file.name.split('.').pop();

    var ime = $('#Ime').val();
    var prezime = $('#Prezime').val();
    var sifraKandidata = $('#SifraKandidata').val();

    var fullFileName = registrationDate + '_' + ime + '_' + prezime + '_' + sifraKandidata + '_' + typeOfDocument + '_' + partNumber + '.' + fileNameExt;

    UploadFileChunk(FileChunks, fullFileName, 1, FileChunks.length);
}
    
function displayValidationErrors(errors) {
    var $ul = $('div.validation-summary-valid.text-danger > ul');

    $ul.empty();
    $.each(errors, function (idx, errorMessage) {
        $ul.append('<li>' + errorMessage + '</li>');
    });
}

$(function () {
    $('#PreslikeOSpremi').change(function () {
        var validator = $("#form1").validate();
        validator.element("#PreslikeOSpremi");
    });
});

$(function () {
    $('#DokaziOUplacenojNaknadi').change(function () {
        var validator = $("#form1").validate();
        validator.element("#DokaziOUplacenojNaknadi");
    });
});

$(function () {
    $('#Izjave').change(function () {
        var validator = $("#form1").validate();
        validator.element("#Izjave");
    });
});

$(function () {
    $('#Suglasnost').change(function () {
        var validator = $("#form1").validate();
        validator.element("#Suglasnost");
    });
});