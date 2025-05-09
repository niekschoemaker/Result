﻿using System.Collections.Generic;

namespace Ardalis.Result
{
    public class Result : Result<Result>
    {
        public Result() : base() { }

        protected internal Result(ResultStatus status) : base(status) { }

        /// <summary>
        /// Represents a successful operation without return type
        /// </summary>
        /// <returns>A Result</returns>
        public static Result Success() => new();

        /// <summary>
        /// Represents a successful operation without return type
        /// </summary>
        /// <param name="successMessage">Sets the SuccessMessage property</param>
        /// <returns>A Result</returns>
        public static Result SuccessWithMessage(string successMessage) => new() { SuccessMessage = successMessage };

        /// <summary>
        /// Represents a successful operation and accepts a values as the result of the operation
        /// </summary>
        /// <param name="value">Sets the Value property</param>
        /// <returns>A Result<typeparamref name="T"/></returns>
        public static Result<T> Success<T>(T value) => new(value);

        /// <summary>
        /// Represents a successful operation and accepts a values as the result of the operation
        /// Sets the SuccessMessage property to the provided value
        /// </summary>
        /// <param name="value">Sets the Value property</param>
        /// <param name="successMessage">Sets the SuccessMessage property</param>
        /// <returns>A Result<typeparamref name="T"/></returns>
        public static Result<T> Success<T>(T value, string successMessage) => new(value, successMessage);

        /// <summary>
        /// Represents a successful operation that resulted in the creation of a new resource.
        /// Accepts a value as the result of the operation.
        /// </summary>		
        /// <param name="value">Sets the Value property</param>
        /// <returns>A Result<typeparamref name="T"/></returns>
        public static Result<T> Created<T>(T value)
        {
            return Result<T>.Created(value);
        }

        /// <summary>
        /// Represents a successful operation that resulted in the creation of a new resource.
        /// Accepts a value as the result of the operation.
        /// Accepts a location for the new resource.
        /// </summary>		
        /// <param name="value">Sets the Value property</param>
        /// <param name="location">The location of the newly created resource</param>
        /// <returns>A Result<typeparamref name="T"/></returns>
        public static Result<T> Created<T>(T value, string location)
        {
            return Result<T>.Created(value, location);
        }

        /// <summary>
        /// Represents an error that occurred during the execution of the service.
        /// Error messages may be provided and will be exposed via the Errors property.
        /// </summary>
        /// <param name="error">An optional instance of ErrorList with list of string error messages and CorrelationId.</param>
        /// <returns>A Result</returns>
        public new static Result Error(ErrorList error = null) => new(ResultStatus.Error)
        {
            CorrelationId = error?.CorrelationId ?? string.Empty,
            Errors = error?.ErrorMessages ?? []
        };

        /// <summary>
        /// Represents an error that occurred during the execution of the service.
        /// A single error message may be provided and will be exposed via the Errors property.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns>A Result</returns>
        public new static Result Error(string errorMessage) => new(ResultStatus.Error) { Errors = new[] { errorMessage } };

        /// <summary>
        /// Represents the validation error that prevents the underlying service from completing.
        /// </summary>
        /// <param name="validationError">The validation error encountered</param>
        /// <returns>A Result</returns>
        public new static Result Invalid(ValidationError validationError)
            => new(ResultStatus.Invalid) { ValidationErrors = [validationError] };

        /// <summary>
        /// Represents validation errors that prevent the underlying service from completing.
        /// </summary>
        /// <param name="validationErrors">A list of validation errors encountered</param>
        /// <returns>A Result</returns>
        public new static Result Invalid(params ValidationError[] validationErrors)
            => new(ResultStatus.Invalid) { ValidationErrors = new List<ValidationError>(validationErrors) };

        /// <summary>
        /// Represents validation errors that prevent the underlying service from completing.
        /// </summary>
        /// <param name="validationErrors">A list of validation errors encountered</param>
        /// <returns>A Result</returns>
        public new static Result Invalid(params IEnumerable<ValidationError> validationErrors)
            => new(ResultStatus.Invalid) { ValidationErrors = validationErrors };

        /// <summary>
        /// Represents the situation where a service was unable to find a requested resource.
        /// </summary>
        /// <returns>A Result</returns>
        public new static Result NotFound() => new Result(ResultStatus.NotFound);

        /// <summary>
        /// Represents the situation where a service was unable to find a requested resource.
        /// Error messages may be provided and will be exposed via the Errors property.
        /// </summary>
        /// <param name="errorMessages">A list of string error messages.</param>
        /// <returns>A Result</returns>
        public new static Result NotFound(params string[] errorMessages) => new(ResultStatus.NotFound) { Errors = errorMessages };

        /// <inheritdoc cref="NotFound(string[])" />
        public new static Result NotFound(params IEnumerable<string> errorMessages) => new(ResultStatus.NotFound) { Errors = errorMessages };

        /// <summary>
        /// The parameters to the call were correct, but the user does not have permission to perform some action.
        /// See also HTTP 403 Forbidden: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
        /// </summary>
        /// <returns>A Result</returns>
        public new static Result Forbidden() => new(ResultStatus.Forbidden);

        /// <summary>
        /// The parameters to the call were correct, but the user does not have permission to perform some action.
        /// See also HTTP 403 Forbidden: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
        /// </summary>
        /// <param name="errorMessages">A list of string error messages.</param> 
        /// <returns>A Result</returns>
        public new static Result Forbidden(params string[] errorMessages) => new(ResultStatus.Forbidden) { Errors = errorMessages };

        /// <inheritdoc cref="Forbidden(string[])" />
        public new static Result Forbidden(params IEnumerable<string> errorMessages) => new(ResultStatus.Forbidden) { Errors = errorMessages };

        /// <summary>
        /// This is similar to Forbidden, but should be used when the user has not authenticated or has attempted to authenticate but failed.
        /// See also HTTP 401 Unauthorized: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
        /// </summary>
        /// <returns>A Result</returns>
        public new static Result Unauthorized() => new(ResultStatus.Unauthorized);

        /// <summary>
        /// This is similar to Forbidden, but should be used when the user has not authenticated or has attempted to authenticate but failed.
        /// See also HTTP 401 Unauthorized: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
        /// </summary>
        /// <param name="errorMessages">A list of string error messages.</param>  
        /// <returns>A Result</returns>
        public new static Result Unauthorized(params string[] errorMessages) => new(ResultStatus.Unauthorized) { Errors = errorMessages };

        /// <inheritdoc cref="Unauthorized(string[])" />
        public new static Result Unauthorized(params IEnumerable<string> errorMessages) => new(ResultStatus.Unauthorized) { Errors = errorMessages };

        /// <summary>
        /// Represents a situation where a service is in conflict due to the current state of a resource,
        /// such as an edit conflict between multiple concurrent updates.
        /// See also HTTP 409 Conflict: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
        /// </summary>
        /// <returns>A Result</returns>
        public new static Result Conflict() => new(ResultStatus.Conflict);

        /// <summary>
        /// Represents a situation where a service is in conflict due to the current state of a resource,
        /// such as an edit conflict between multiple concurrent updates.
        /// Error messages may be provided and will be exposed via the Errors property.
        /// See also HTTP 409 Conflict: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
        /// </summary>
        /// <param name="errorMessages">A list of string error messages.</param>
        /// <returns>A Result</returns>
        public new static Result Conflict(params string[] errorMessages) => new(ResultStatus.Conflict) { Errors = errorMessages };

        /// <inheritdoc cref="Conflict(string[])" />
        public new static Result Conflict(params IEnumerable<string> errorMessages) => new(ResultStatus.Conflict) { Errors = errorMessages };

        /// <summary>
        /// Represents a situation where a service is unavailable, such as when the underlying data store is unavailable.
        /// Errors may be transient, so the caller may wish to retry the operation.
        /// See also HTTP 503 Service Unavailable: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#5xx_server_errors
        /// </summary>
        /// <param name="errorMessages">A list of string error messages</param>
        /// <returns></returns>
        public new static Result Unavailable(params string[] errorMessages) => new(ResultStatus.Unavailable) { Errors = errorMessages };

        /// <inheritdoc cref="Unavailable(string[])" />
        public new static Result Unavailable(params IEnumerable<string> errorMessages) => new(ResultStatus.Unavailable) { Errors = errorMessages };

        /// <summary>
        /// Represents a critical error that occurred during the execution of the service.
        /// Everything provided by the user was valid, but the service was unable to complete due to an exception.
        /// See also HTTP 500 Internal Server Error: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#5xx_server_errors
        /// </summary>
        /// <param name="errorMessages">A list of string error messages.</param>
        /// <returns>A Result</returns>
        public new static Result CriticalError(params string[] errorMessages) => new(ResultStatus.CriticalError) { Errors = errorMessages };

        /// <inheritdoc cref="CriticalError(string[])" />
        public new static Result CriticalError(params IEnumerable<string> errorMessages) => new(ResultStatus.CriticalError) { Errors = errorMessages };

        /// <summary>
        /// Represents a situation where the server has successfully fulfilled the request, but there is no content to send back in the response body.
        /// </summary>
        /// <returns>A Result object</returns>
        public new static Result NoContent() => new(ResultStatus.NoContent);
    }
}
