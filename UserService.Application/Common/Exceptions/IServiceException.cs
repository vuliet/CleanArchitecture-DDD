﻿using System.Net;

namespace UserService.Application.Common.Exceptions
{
    public interface IServiceException
    {
        public HttpStatusCode StatusCode { get; }

        public string ErrorMessage { get; }
    }
}
