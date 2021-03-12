using System;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001F3 RID: 499
	[DataContract]
	public class JsonFaultResponse
	{
		// Token: 0x06001193 RID: 4499 RVA: 0x000436AF File Offset: 0x000418AF
		public JsonFaultResponse()
		{
			this.Body = new JsonFaultBody();
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x000436C4 File Offset: 0x000418C4
		public static JsonFaultResponse CreateFromException(Exception exception)
		{
			JsonFaultResponse jsonFaultResponse = new JsonFaultResponse();
			jsonFaultResponse.Body.ErrorCode = 500;
			if (!Globals.IsAnonymousCalendarApp && Global.WriteStackTraceOnISE)
			{
				jsonFaultResponse.Body.StackTrace = exception.ToString();
			}
			exception = JsonFaultResponse.UnwrapUnderlyingException(exception);
			jsonFaultResponse.Body.ExceptionName = exception.GetType().Name;
			OwaServiceFaultException ex = exception as OwaServiceFaultException;
			if (ex != null)
			{
				ServiceError serviceError = ex.ServiceError;
				jsonFaultResponse.Body.ResponseCode = serviceError.MessageKey.ToString();
				jsonFaultResponse.Body.FaultMessage = serviceError.MessageText;
				jsonFaultResponse.Body.IsTransient = OWAFaultHandler.IsTransientError(ex.ServiceException);
				OWAFaultHandler.SetBackOffPeriodInMs(ex.ServiceException, jsonFaultResponse.Body);
				return jsonFaultResponse;
			}
			MissingIdentityException ex2 = exception as MissingIdentityException;
			if (ex2 != null)
			{
				jsonFaultResponse.Body.ErrorCode = (int)ex2.StatusCode;
				jsonFaultResponse.Body.ResponseCode = ex2.StatusCode.ToString();
				jsonFaultResponse.Body.FaultMessage = ex2.StatusDescription;
				jsonFaultResponse.Body.IsTransient = false;
				OWAFaultHandler.SetBackOffPeriodInMs(exception, jsonFaultResponse.Body);
				return jsonFaultResponse;
			}
			jsonFaultResponse.Body.FaultMessage = exception.Message;
			jsonFaultResponse.Body.IsTransient = OWAFaultHandler.IsTransientError(exception);
			OWAFaultHandler.SetBackOffPeriodInMs(exception, jsonFaultResponse.Body);
			return jsonFaultResponse;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0004381A File Offset: 0x00041A1A
		private static Exception UnwrapUnderlyingException(Exception exception)
		{
			while (exception.InnerException != null && (exception is GrayException || exception is TargetInvocationException))
			{
				exception = exception.InnerException;
			}
			return exception;
		}

		// Token: 0x04000A53 RID: 2643
		[DataMember]
		public JsonFaultBody Body;
	}
}
