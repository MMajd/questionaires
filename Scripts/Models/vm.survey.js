/*
 *
 */

var SurveyModel = function (data) {
    var self = this;

    // Properties
    self.modal = ko.observable(); 
    self.questions = ko.observableArray([]);
    self.current = ko.observable(new QuestionModel(null));

    self.hasQuestions = ko.computed(function() {
        return self.questions().length > 0;
    }, self);

    self.questionCount = ko.computed(function() {
        return self.questions().length;
    }, self);
    
    

    // Behaviors/////////////////////////////////////////

    self.questionChoices = ko.observableArray([]);


    self.addChoice = function () {
        self.questionChoices.push(new AnswerModel());
        console.log(self.questionChoices().length);

        self.current().choices(new AnswerModel());

    };

    self.removeChoice = function(choice) {
        self.questionChoices.remove(choice); 
    }; 

    ////////////////////////////////////////////////////////
    self.newQuestion = function() {
        self.current(new QuestionModel(null));
        self.questionChoices([]);
        self.modal(true);
    };

    self.cancelNewQuestion = function () {
        self.modal(false);
        self.current(new QuestionModel());
        self.questionChoices([]); 
    }

    self.editQuestion = function (question) {
        self.current(question);
        self.questionChoices(question.choices()); 
        self.modal(true);
        console.log("End Edit");
    };

    self.saveQuestion = function () {
        var index;

        //var json = JSON.stringify($(data).serializeArray());
        //json = JSON.parse(json);

        //var question = new QuestionModel();
        //question.id(json[0].value); 
        //question.title(json[1].value);
        //question.body(json[2].value);

        //for (var i = 3; i < json.length; i++) {
        //    question.addChoice(json[i].value); 
        //}

        //question.printChoices();  



        if (self.validQuestion()) {
            self.current().choices(self.questionChoices());
            self.questionChoices([]); 
            index = self.questions.indexOf(self.current());

            if (index >= 0) {
                self.questions.splice(index, 1);
                self.questions.splice(index, 0, self.current());
            }
            else {
                self.questions.push(self.current());
            }
          
            self.modal(false);
        }
        else {
            alert('Error: Question should have title and at least to choices');
        }
    };

    self.moveUp = function(item) {
        var currIndex = self.questions.indexOf(item),
            prevIndex = currIndex - 1;

        if (currIndex > 0) {
            self.questions.splice(currIndex, 1);
            self.questions.splice(prevIndex, 0, item);
        }
    };
    
    self.moveDown = function(question) {
        var currIndex = self.questions.indexOf(question),
            nextIndex = currIndex + 1,
            lastIndex = self.questions().length - 1;

        if (currIndex < lastIndex) {
            self.questions.splice(currIndex, 1);
            self.questions.splice(nextIndex, 0, question);
        }
    };

    // Callbacks

    self.afterAdd = function(elem) {
        var el = $(elem);
        if (elem.nodeType === 1) {
            el.before("<div/>");
            el.prev()
                .width(el.innerWidth())
                .height(el.innerHeight())
                .css({
                    "position": "absolute",
                    "background-color": "#ffff99",
                    "opacity": "0.5"
                })
                .fadeOut(500);
        }
    };
    
    // Initialize
    // this used in edit only objects are initialized through json Survey.cs
    if (data != null) {

        for (var i = 0; i < data.Questions.length; i++) {

            var q = new QuestionModel(data.Questions[i]);

            q.id(data.Questions[i].Id);

            q.title(data.Questions[i].Title);

            q.type(data.Questions[i].Type);

            q.body(data.Questions[i].Body);

            q.isActive(data.Questions[i].IsActive);

            self.questions.push(q);
        }
    }

    self.validChoices = function() {
        for (var i = 0; i < self.questionChoices().length; i++) {
            if (self.questionChoices()[i].content() === "" || self.questionChoices()[i].content() == null) {
                self.questionChoices.splice(i, 1);
                i--; 
            }
        }
        return self.questionChoices().length >= 2; 
    };

    self.validQuestion = function () {
        
        return (self.current().title() !== "" && self.current().title() != null) && self.validChoices();
        //(self.questionChoices().length >= 2);
    };


    return self;
};