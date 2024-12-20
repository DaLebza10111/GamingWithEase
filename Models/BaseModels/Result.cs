﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BaseModels
{
    public class Result
    {
        internal Result()
        {
            Errors = new string[] { };
        }

        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public string ErrorMessage => string.Join(", ", Errors ?? new string[] { });

        public bool Succeeded { get; init; }

        public string[] Errors { get; init; }

        public override string ToString()
        {
            return $"Result succeeded: {Succeeded} Errors: {ErrorMessage}";
        }

        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }

        public static Task<Result> SuccessAsync()
        {
            return Task.FromResult(new Result(true, Array.Empty<string>()));
        }

        public static Result Failure(params string[] errors)
        {
            return new Result(false, errors);
        }

        public static Task<Result> FailureAsync(params string[] errors)
        {
            return Task.FromResult(new Result(false, errors));
        }
    }

    public class Result<T> : Result
    {
        public T? Data { get; set; }

        public new static Result<T> Failure(params string[] errors)
        {
            return new Result<T> { Succeeded = false, Errors = errors.ToArray() };
        }

        public new static async Task<Result<T>> FailureAsync(params string[] errors)
        {
            return await Task.FromResult(Failure(errors));
        }

        public static Result<T> Success(T data)
        {
            return new Result<T> { Succeeded = true, Data = data };
        }

        public static async Task<Result<T>> SuccessAsync(T data)
        {
            return await Task.FromResult(Success(data));
        }
    }
}
