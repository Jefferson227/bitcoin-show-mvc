using System;
using System.Collections.Generic;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BitcoinShow.Test.Repositories
{
    public class QuestionRepositoryTest
    {
        // https://www.jankowskimichal.pl/en/2016/01/mocking-dbcontext-and-dbset-with-moq/
        [Fact]
        public void Add_Question_Without_Title_Error()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            QuestionRepository repository = new QuestionRepository(context);

            Question option = new Question();
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => repository.Add(option));
            Assert.NotNull(ex);
            Assert.Equal(nameof(option.Title), ex.ParamName);
        }

        [Fact]
        public void Add_Question_Wit_Title_Greater_Than_Max_Error()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            QuestionRepository repository = new QuestionRepository(context);

            Question option = new Question();
            option.Title = new String('a', 201);
            
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => repository.Add(option));
            Assert.NotNull(ex);
            Assert.Equal(nameof(option.Title), ex.ParamName);
        }

        [Fact]
        public void Add_Question_Without_Answer_Error()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            QuestionRepository repository = new QuestionRepository(context);

            Question question = new Question();
            question.Title = "How many times do you test your code?";
            
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => repository.Add(question));
            Assert.NotNull(ex);
            Assert.Equal(nameof(question.Answer), ex.ParamName);
        }

        [Fact]
        public void Add_Question_Success()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();

            var options = RandomOptions(4).ToList();
            options.ForEach(o => 
            {
                context.Options.Add(o);
            });
            context.SaveChanges();

            QuestionRepository repository = new QuestionRepository(context);
            Question question = new Question();
            question.Answer = context.Options.First();
            question.Title = "Test question";

            repository.Add(question);
            Assert.True(question.Id > 0);
        }

        [Fact]
        public void GetAll_Questions_Success()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            for (int i = 0; i < 1; i++)
            {
                context.Questions.Add(new Question
                { 
                    Title = $"Random Question {i + 1}",
                    Answer = new Option { Text = $"Random Option {i}"}
                });
            }
            context.SaveChanges();

            QuestionRepository repository = new QuestionRepository(context);

            repository.GetAll().ForEach(q => 
            {
                Assert.NotNull(q);
                Assert.True(q.Id > 0);
                Assert.False(String.IsNullOrEmpty(q.Title));
                Assert.NotNull(q.Answer);
                Assert.True(q.Answer.Id > 0);
                Assert.False(String.IsNullOrEmpty(q.Answer.Text));
            });
        }

        [Fact]
        public void Get_Question_Not_Found_Error()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            for (int i = 0; i < 10; i++)
            {
                context.Questions.Add(new Question
                { 
                    Title = $"Random Question {i + 1}",
                    Answer = new Option { Id = i, Text = $"Random Option {i}"}
                });
            }
            context.SaveChanges();

            QuestionRepository repository = new QuestionRepository(context);

            Question actual = repository.Get(50);
            Assert.Null(actual);
        }

        [Fact]
        public void Get_Question_Success()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            for (int i = 0; i < 98; i++)
            {
                context.Questions.Add(new Question
                { 
                    Title = $"Random Question {i + 1}",
                    Answer = new Option { Id = i, Text = $"Random Option {i}"}
                });
            }
            context.SaveChanges();

            QuestionRepository repository = new QuestionRepository(context);

            Question actual = repository.Get(50);
            Assert.NotNull(actual);
            Assert.NotNull(actual.Answer);
        }

        [Fact]
        public void Delete_Question_Not_Found_Error()
        {
            BitcoinShowDBContext context = DbContextFactory.GetContext();

            QuestionRepository repository = new QuestionRepository(context);
            Exception ex = Assert.Throws<Exception>(() => repository.Delete(99999999));
            Assert.NotNull(ex);
            Assert.Equal("The current Question does not exist.", ex.Message);
        }

        [Fact]
        public void Delete_Question_Success()
        {
            var question = new Question
            {
                Title = "Delete_Question_Success"
            };
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            context.Questions.Add(question);
            context.SaveChanges();
            int questionId = question.Id;

            QuestionRepository repository = new QuestionRepository(context);
            repository.Delete(questionId);
            Assert.Null(context.Questions.Find(questionId));
        }

        [Fact]
        public void Update_Question_Without_Title_Error()
        {
            var option = new Option {Text = "Update_Question_Without_Answer_Error Option"};
            var question = new Question
            {
                Title = "Update_Question_Without_Title_Error",
                Answer = option
            };
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            context.Questions.Add(question);
            context.SaveChanges();

            QuestionRepository repository = new QuestionRepository(context);
            question.Title = String.Empty;
            ArgumentNullException ex =  Assert.Throws<ArgumentNullException>(() => repository.Update(question));
            Assert.NotNull(ex);
            Assert.Equal(nameof(question.Title), ex.ParamName);
        }

        [Fact]
        public void Update_Question_With_Title_Greater_Than_Max_Error()
        {
            var option = new Option {Text = "Update_Question_With_Title_Greater_Than_Max_Error Option"};
            var question = new Question
            {
                Title = "Update_Question_With_Title_Greater_Than_Max_Error",
                Answer = option
            };
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            context.Questions.Add(question);
            context.SaveChanges();

            QuestionRepository repository = new QuestionRepository(context);
            question.Title = new String('a', 201);
            ArgumentOutOfRangeException ex =  Assert.Throws<ArgumentOutOfRangeException>(() => repository.Update(question));
            Assert.NotNull(ex);
            Assert.Equal(nameof(question.Title), ex.ParamName);
        }

        [Fact]
        public void Update_Question_Without_Answer_Error()
        {
            var option = new Option {Text = "Update_Question_Without_Answer_Error Option"};
            var question = new Question
            {
                Title = "Update_Question_Without_Answer_Error",
                Answer = option
            };
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            context.Questions.Add(question);
            context.SaveChanges();

            QuestionRepository repository = new QuestionRepository(context);
            question.Answer = null;
            ArgumentNullException ex =  Assert.Throws<ArgumentNullException>(() => repository.Update(question));
            Assert.NotNull(ex);
            Assert.Equal(nameof(question.Answer), ex.ParamName);
        }

        [Fact]
        public void Update_Question_Success()
        {
            var option = new Option {Text = "Update_Question_Success Option"};
            var option2 = new Option {Text = "Update_Question_Success Option2"};
            var question = new Question
            {
                Title = "Update_Question_Success",
                Answer = option
            };
            BitcoinShowDBContext context = DbContextFactory.GetContext();
            context.Questions.Add(question);
            context.Options.Add(option2);
            context.SaveChanges();

            var expected = new Question
            {
                Id = question.Id,
                Title = "Updated",
                Answer = question.Answer
            };

            QuestionRepository repository = new QuestionRepository(context);
            question.Title = "Updated";
            repository.Update(question);

            var actual = context.Questions.Find(question.Id);
            Assert.Equal(expected, actual);

            expected.Answer = option2;
            question.Answer = option2;
            repository.Update(question);

            actual = context.Questions.Find(question.Id);
            Assert.Equal(expected, actual);
        }

        private IEnumerable<Option> RandomOptions(int nOptions)
        {
            for (int i = 0; i < nOptions; i++)
            {
                yield return new Option 
                { 
                    Text = $"Random Option {i + 1}"
                };
            }
        }
    }
}
