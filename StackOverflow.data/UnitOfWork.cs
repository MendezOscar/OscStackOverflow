﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowOsc.Domain.Entities;

namespace StackOverflow.data
{
    public class UnitOfWork : IDisposable
    {
        private StackOverflowContext context = new StackOverflowContext();
        private Repository<Account> accountRepository;
        private Repository<Question> questionRepository;
        private Repository<Answer> answerRepository;

        public Repository<Account> AccountRepository
        {
            get
            {

                if (this.accountRepository == null)
                {
                    this.accountRepository = new Repository<Account>(context);
                }
                return accountRepository;
            }
        }

        public Repository<Question> QuestionRepository
        {
            get
            {

                if (this.questionRepository == null)
                {
                    this.questionRepository = new Repository<Question>(context);
                }
                return questionRepository;
            }
        }

        public Repository<Answer> AnswerRepository
        {
            get
            {

                if (this.answerRepository == null)
                {
                    this.answerRepository = new Repository<Answer>(context);
                }
                return answerRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
        private bool set = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.set)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.set = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}