using System;
using System.Net;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C8A RID: 3210
	public class JsonServiceErrorHandler : IErrorHandler
	{
		// Token: 0x0600570B RID: 22283 RVA: 0x00111898 File Offset: 0x0010FA98
		public bool HandleError(Exception error)
		{
			return true;
		}

		// Token: 0x0600570C RID: 22284 RVA: 0x0011189C File Offset: 0x0010FA9C
		public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(RequestDetailsLogger.Current, error, "JsonServiceErrorHandler");
			JsonFaultResponse jsonFaultResponse = new JsonFaultResponse();
			jsonFaultResponse.Body.FaultMessage = error.Message;
			jsonFaultResponse.Body.ErrorCode = 500;
			if (Global.WriteStackTraceOnISE)
			{
				jsonFaultResponse.Body.StackTrace = error.StackTrace;
			}
			WebBodyFormatMessageProperty property = new WebBodyFormatMessageProperty(WebContentFormat.Json);
			HttpResponseMessageProperty httpResponseMessageProperty = new HttpResponseMessageProperty
			{
				StatusCode = HttpStatusCode.InternalServerError,
				StatusDescription = "Internal Server Error. See fault object for more information."
			};
			httpResponseMessageProperty.Headers.Set("Content-Type", "application/json; charset=utf-8");
			fault = Message.CreateMessage(version, string.Empty, jsonFaultResponse, new DataContractJsonSerializer(jsonFaultResponse.GetType()));
			fault.Properties.Add("WebBodyFormatMessageProperty", property);
			fault.Properties.Add(HttpResponseMessageProperty.Name, httpResponseMessageProperty);
		}
	}
}
