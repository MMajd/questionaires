var AnswerModel = function (id, value) {
    var self = this;

    self.id = ko.observable(0);
    self.content = ko.observable("");


    if (id != null) self.id(id);
    if (value != null) self.content(value);

}