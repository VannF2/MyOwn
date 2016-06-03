    function Note(name, content) {
        var self = this;
        self.Name = name;
        self.Content = content;
    }

    var AppViewModel = function () {
        var self = this;
        self.notepads = ko.observableArray([]);
        self.visible = ko.observable(0);
        self.message = ko.observable('');
        self.Name = ko.observable('');
        self.Content = ko.observable('');
        self.currentnotepad = ko.observable(new Note("", ""));
        self.selectedNote = ko.observable('');
        self.notepadImage = ko.observable('');
        OpenLoad();
        function OpenLoad() {
            $.ajax({
                url: '/Notepad/Load',
                success: function (data) {
                    if (data._list) {
                        for (var item in data._list) {
                            self.currentnotepad = ko.observable(new Note(data._list[item].Name, data._list[item].Content));
                            self.notepads.push(self.currentnotepad);
                        }
                    }

                    //for (var i = 0; i < data.list.length; i++) {
                    //    List.push({
                    //        "name": data.list[i].Name,
                    //        "body": data.list[i].Body
                    //    });
                    //}
                }
            }).then(function () {
                $("#notepads").parents().children().removeClass('active');
                var num = self.notepads().length - 1;
                if (num > 0) {
                    var name = self.notepads()[num].Name;
                    var content;
                    if (notepadname != "")
                        ko.utils.arrayForEach(self.notepads(), function (v) {
                            if (v().Name == notepadname) {
                                name = notepadname;
                                content = v().Content;
                            }
                        });
                    $("#notepads[name='" + name + "']").addClass('active');
                    selectedNote(name);
                    self.Name("");
                    if (notepadname == "")
                        self.Content("");
                    else
                    {
                        selectedNote(name);
                        self.Content(content);
                        Image();
                    }
                    self.message('<strong>Well done!</strong>Success loaded');
                    self.visible(1);
                }
            })
        }
        function Image() {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (this.readyState == 4 && this.status == 200) {
                    var url = window.URL || window.webkitURL;
                    self.notepadImage(url.createObjectURL(this.response));
                }
            }
            var formData = new FormData();
            formData.append('name', selectedNote());
            xhr.open('POST', '/Notepad/Image/', true)
            xhr.responseType = 'blob';
            xhr.send(formData);
        }
        self.addItem = function (data, element) {
            if (self.Name() != "") {
                var have = false;
                ko.utils.arrayForEach(self.notepads(), function (v) {
                    if (v().Name == self.Name()) {
                        have = true;
                    }
                });
                if (!have) {
                    self.currentnotepad = ko.observable(new Note(self.Name(), ""));
                    var data = ko.toJSON(self.currentnotepad);
                    $.post("/Notepad/Save", { note: data }, function (returnedData) {
                        self.notepads.push(self.currentnotepad);
                        $("#notepads").parents().children().removeClass('active');
                        $("#notepads[name='" + self.Name() + "']").addClass('active');
                        selectedNote(self.Name());
                        self.Name("");
                        self.Content("");
                        self.message(returnedData.message);
                        self.visible(1);
                        Image();
                    })
                } else {
                    self.Name("");
                    self.Content("");
                    self.message('<strong>Failed!</strong>This notepad is exist');
                    $("#notepads").parents().children().removeClass('active');
                    self.visible(2);
                    self.notepadImage('');
                }
                //$.ajax({
                //    type: 'POST',
                //    url: '/Home/Save',
                //    data: { note: data },
                //    success: function (returnedData) {
                //        alert(returnedData.message);
                //    }
                //});
                //ko.utils.postJson('Home/Save', {note:data});
            }
        }
        self.saveItem = function () {
            var item;
            ko.utils.arrayForEach(self.notepads(), function (v) {
                if (v().Name == selectedNote()) {
                    v().Content = self.Content();
                    item = v;
                }
            });
            var json = ko.toJSON(new Note(item().Name, item().Content));
            $.post("/Notepad/Save", { note: json }, function (returnedData) {
                self.message(returnedData.message);
                self.visible(1);
            });
        }
        self.removeItem = function () {
            var item;
            ko.utils.arrayForEach(self.notepads(), function (v) {
                if (v().Name == selectedNote()) {
                    item = v;
                    self.message('<strong>Well done!</strong>Deleted');
                    self.visible(1);
                    self.Content('');
                }
            });
            $.post("/Notepad/Remove", { Name: self.selectedNote() }, function (returnedData) {
                self.message(returnedData.message);
                self.visible(1);
            });
            self.notepads.remove(item);
            if (self.notepads().length > 0) {
                var num = self.notepads().length - 1;
                selectedNote(self.notepads()[num]().Name);
                $("#notepads[name='" + self.selectedNote() + "']").addClass('active');
                self.Name('');
                self.Content(self.notepads()[num]().Content);
                Image();
            } else {
                self.notepadImage('');
            }
        }
        self.selectnote = function (data, element, lab) {
            if (lab == null) { var label = $(element.target).text(); }
            else { var label = lab; }
            self.selectedNote(label);
            $(element.target).parent().children().removeClass('active');
            $(element.target).addClass('active');
            var item;
            ko.utils.arrayForEach(self.notepads(), function (v) {
                if (v().Name == label) {
                    item = v;
                    self.message('<strong>Well done!</strong>Selected');
                    self.visible(1);
                }
            });
            self.Content(item().Content);
            Image();
        };

    }

    ko.applyBindings(AppViewModel);