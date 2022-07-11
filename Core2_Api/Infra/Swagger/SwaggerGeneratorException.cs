﻿using System;

namespace Core2_Api.Infra.Swagger
{

		public class SwaggerGeneratorException : Exception
		{
			public SwaggerGeneratorException(string message) : base(message)
			{ }

			public SwaggerGeneratorException(string message, Exception innerException) : base(message, innerException)
			{ }
		}
	}

