
$(document).ready(function () {
    createSurvey.init();
});
$(document).on('click', '.delete-answer', function (e) {
    e.preventDefault()
    $(this).closest('tr.answer-choice').remove();
    $(".questionInputForDeleteAnswer").val(true);
    return false;
});
var createSurvey =
    {
        init: function () {
            this.createNewSurvey();
            this.addQuestion();
            this.editQuestion();
        },
        multipleChoiceTemplate: '<tr class="answer-choice"><td class="checkBox-for-response" ><label class="checkmark-label"><input type="radio" class="checkmark" value="" disabled></label></td>' +
            '<td class="input-for-response"> <input class="form-control createSurveyInput questionAnswerInputMultipleChoice" name="Question.QuestionAnswers[#ReplacebleIndex#].Description" type="text" value="" placeholder="Enter an answer choice"> </td>' +
            '<td class="add-delete-response" > ' +
            '<span class="delete-answer"><img src="/Content/img/icon-delete.png"></span>' +
            '</td ></tr > ',
        checkboxTemplate: '<tr class="answer-choice"><td class="checkBox-for-response" ><label class="checkmark-label"><input type="checkbox" class="checkmark" value="" disabled></label></td>' +
            '<td class="input-for-response"> <input class="form-control createSurveyInput questionAnswerInputCheckbox" name="Question.QuestionAnswers[#ReplacebleIndex#].Description" type="text" value="" placeholder="Enter an answer choice"> </td>' +
            '<td class="add-delete-response" > ' +
            '<span class="delete-answer"><img src="/Content/img/icon-delete.png"></span>' +
            '</td ></tr > ',

        createNewSurvey: function () {
            $(".dropdown-toggle").dropdown();
            $('.dropdown-menu').find('a').click(function (e) {
                e.preventDefault();
                var param = $(this).attr("href").replace("#", "");
                var concept = $(this).text();
                $('span#choiceCategory').text(concept);

                $('.input-group').val(param);

                //$('input.questionInputForType').text(concept);
                //$('input.questionInputForSurveyId').text(surveyId);

                $('input.questionInputForType').val(concept);
                $('input.questionInputForSurveyId').val(surveyId);

                if (concept === "Single Choice") {
                    $("#multipleChoice").toggle();
                    var form = $('#addQuestionForm');
                    var indexQ = $('input.questionAnswerInputMultipleChoice',form).length;
                    $(".tableForMultipleChoice").append(createSurvey.multipleChoiceTemplate.replace("#ReplacebleIndex#", indexQ));

                    $("#checkBox").hide();
                    $("#commentBox").hide();
                }
                else if (concept === "Checkboxes") {
                    $("#checkBox").toggle();
                    var form = $('#addQuestionForm');
                    var indexQ = $('input.questionAnswerInputCheckbox',form).length;
                    $(".tableForCheckbox").append(createSurvey.checkboxTemplate.replace("#ReplacebleIndex#", indexQ));

                    $("#multipleChoice").hide();
                    $("#commentBox").hide();
                }
                else if (concept === "Comment box") {
                    $("#commentBox").toggle();
                    $("#checkBox").hide();
                    $("#multipleChoice").hide();
                }
            });

        },

        addQuestion: function () {

            $("#add-response-multipleChoice").click(function (e) {
                e.preventDefault();
                var form = $('#addQuestionForm');
                var indexQ = $('input.questionAnswerInputMultipleChoice',form).length;
                $(".tableForMultipleChoice").append(createSurvey.multipleChoiceTemplate.replace(/#ReplacebleIndex#/g, indexQ));
            });

            $("#add-response-checkbox").click(function (e) {
                e.preventDefault();
                var form = $('#addQuestionForm');
                var indexQ = $('input.questionAnswerInputCheckbox',form).length;
                $(".tableForCheckbox").append(createSurvey.checkboxTemplate.replace(/#ReplacebleIndex#/g, indexQ));
            });
        },

        editQuestion: function () {

            var multipleChoiceTemplateEdit = '<tr class="answer-choice"><td class="checkBox-for-response"><label class="checkmark-label"><input type="radio" class="checkmark" value="" disabled></label></td>' +
                '<td class="input-for-response"><input class="form-control createSurveyInput questionAnswerInputMultipleChoice" name="QuestionAnswers[#ReplacebleIndex#].Description" type="text" value="" placeholder="Enter an answer choice">' +
                '<input class="form-control createSurveyInput questionInputForNewQ" name = "QuestionAnswers[#ReplacebleIndex#].IsNewQuestion" type = "text" value = "" hidden/></td > ' +
                    '<td class="add-delete-response" ><span class="delete-answer"><img src="/Content/img/icon-delete.png"></span></td >' +
                '</tr > ';
            var checkboxTemplateEdit = '<tr class="answer-choice"><td class="checkBox-for-response"><label class="checkmark-label"><input type="checkbox" class="checkmark" value="" disabled></label></td>' +
                '<td class="input-for-response"><input class="form-control createSurveyInput questionAnswerInputCheckbox" name="QuestionAnswers[#ReplacebleIndex#].Description" type="text" value="" placeholder="Enter an answer choice">' +
                '<input class="form-control createSurveyInput questionInputForNewQ" name = "QuestionAnswers[#ReplacebleIndex#].IsNewQuestion" type = "text" value = "" hidden/></td > ' +
                    '<td class="add-delete-response" ><span class="delete-answer"><img src="/Content/img/icon-delete.png"></span></td >' +
                '</tr > ';

            $(".edit-response-multipleChoice").click(function (e) {
                e.preventDefault();
                var id = $(this).data('edit-question-button');
                var form = $('form[data-form-edit-question=' + id + ']');
                var indexQ = $('input.questionAnswerInputMultipleChoice', form).length;
                $(".tableForMultipleChoice").append(multipleChoiceTemplateEdit.replace(/#ReplacebleIndex#/g, indexQ));
                $(".questionInputForNewQ").val(true);
            });

            $(".edit-response-checkbox").click(function (e) {
                e.preventDefault();
                var id = $(this).data('edit-question-button');
                var form = $('form[data-form-edit-question=' + id + ']');
                var indexQ = $('input.questionAnswerInputCheckbox', form).length;
                $(".tableForCheckbox").append(checkboxTemplateEdit.replace(/#ReplacebleIndex#/g, indexQ));
                $(".questionInputForNewQ").val(true);
            });
        }
    };