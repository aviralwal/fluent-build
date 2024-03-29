﻿using System;


namespace FluentBuild.Utilities
{
    ///<summary>
    /// Represents a class that has continue/fail on error behavior
    ///</summary>
    ///<typeparam name="T"></typeparam>
    public interface IFailable<T>
    {
        ///<summary>
        /// On error throw an exception
        ///</summary>
        T FailOnError { get; }

        ///<summary>
        /// Swallow exceptions and continue
        ///</summary>
        T ContinueOnError { get; }
    }

    public abstract class InternalFailable<T>: InternalExecutable, IFailable<T>
    {
        protected internal OnError OnError;

        protected InternalFailable()
        {
            OnError = Defaults.OnError;
        }

        public T FailOnError
        {
            get
            {
                OnError = OnError.Fail;
                return (T)(object)this;
            }
        }

        public T ContinueOnError
        {
            get
            {
                OnError = OnError.Continue;
                return (T)(object)this;
            }
        }        
    }

    ///<summary>
    /// Represents a class that has continue/fail on error behavior
    ///</summary>
    ///<typeparam name="T"></typeparam>
    public abstract class Failable<T> : IFailable<T>
    {
        protected internal OnError OnError;

        protected Failable()
        {
            OnError = Defaults.OnError;
        }

        public T FailOnError
        {
            get
            {
                OnError = OnError.Fail;
                return (T)(object)this;
            }
        }

        public T ContinueOnError
        {
            get
            {
                OnError = OnError.Continue;
                return (T)(object)this;
            }
        }
    }
}