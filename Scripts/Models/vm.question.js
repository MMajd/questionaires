var QuestionModel = function(data) {
	var self = this;

	self.id = ko.observable(0);
	self.title = ko.observable().extend({ required: true });
	self.type = ko.observable().extend({ required: true });
	self.body = ko.observable().extend({ required: true });

	////////////////////////////////
	self.choices = ko.observableArray([]);
	////////////////////////////////
	

	self.isActive = ko.observable(true);

	self.activeText = ko.computed(function() {
		return self.isActive() ? "true" : "false";
	}, self);


	self.enable = function() {
		self.isActive(true);
	};


	self.disable = function() {
		self.isActive(false);
	};


	self.addChoice = function(data) {
		self.choices.push(new AnswerModel(0, data));
	}

	self.setChoicesArray =
	function (c)
	{
		console.log("SetChoicesArray");
		console.log("Length: " + c.length);
	}


	//initialization 
	if (data != null) {
		for (var i = 0; i < data.Choices.length ; i++) {
			var c = new AnswerModel(data.Choices[i].Id, data.Choices[i].Content);
			self.choices.push(c); 
		}
	}

	//debugging 
	self.printChoices = function () {
		console.log("PrintChoicesArray");
		for (var i=0; i < self.choices().length; i++) 
			console.log(self.choices()[i].content());
	}
	
	return self;
};
