using System;
using System.Net;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001F2 RID: 498
	public class OWAFaultHandler : IErrorHandler
	{
		// Token: 0x0600118B RID: 4491 RVA: 0x000432FD File Offset: 0x000414FD
		public bool HandleError(Exception error)
		{
			return true;
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00043318 File Offset: 0x00041518
		public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
			OWAFaultHandler.LogException(error);
			WebBodyFormatMessageProperty property = new WebBodyFormatMessageProperty(WebContentFormat.Json);
			HttpResponseMessageProperty responseMessageProp = new HttpResponseMessageProperty
			{
				StatusCode = HttpStatusCode.InternalServerError,
				StatusDescription = Strings.GetLocalizedString(-1269063878)
			};
			responseMessageProp.Headers.Set("Content-Type", "application/json; charset=utf-8");
			if (error is OwaExtendedErrorCodeException)
			{
				OwaExtendedErrorCodeException ex = (OwaExtendedErrorCodeException)error;
				OwaExtendedError.SendError(responseMessageProp.Headers, delegate(HttpStatusCode sc)
				{
					responseMessageProp.StatusCode = sc;
				}, ex.ExtendedErrorCode, ex.Message, ex.User, ex.ErrorData);
			}
			else
			{
				MissingIdentityException ex2 = error as MissingIdentityException;
				if (ex2 != null)
				{
					responseMessageProp.StatusCode = ex2.StatusCode;
					responseMessageProp.StatusDescription = ex2.StatusDescription;
					responseMessageProp.Headers.Set(WellKnownHeader.MSDiagnostics, ex2.DiagnosticText ?? string.Empty);
					responseMessageProp.Headers.Set(WellKnownHeader.Authentication, ex2.ChallengeString ?? string.Empty);
				}
			}
			JsonFaultResponse jsonFaultResponse = JsonFaultResponse.CreateFromException(error);
			fault = Message.CreateMessage(version, string.Empty, jsonFaultResponse, new DataContractJsonSerializer(jsonFaultResponse.GetType()));
			fault.Properties.Add("WebBodyFormatMessageProperty", property);
			fault.Properties.Add(HttpResponseMessageProperty.Name, responseMessageProp);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x000434AC File Offset: 0x000416AC
		public void ProvideFault(Exception error, HttpResponse httpResponse)
		{
			OWAFaultHandler.LogException(error);
			if (!httpResponse.IsClientConnected)
			{
				return;
			}
			httpResponse.TrySkipIisCustomErrors = true;
			httpResponse.StatusCode = 500;
			httpResponse.StatusDescription = Strings.GetLocalizedString(-1269063878);
			httpResponse.ContentType = "application/json; charset=utf-8";
			if (error is OwaExtendedErrorCodeException)
			{
				OwaExtendedErrorCodeException ex = (OwaExtendedErrorCodeException)error;
				OwaExtendedError.SendError(httpResponse.Headers, delegate(HttpStatusCode sc)
				{
					httpResponse.StatusCode = (int)sc;
				}, ex.ExtendedErrorCode, ex.Message, ex.User, ex.ErrorData);
			}
			else
			{
				MissingIdentityException ex2 = error as MissingIdentityException;
				if (ex2 != null)
				{
					httpResponse.StatusCode = (int)ex2.StatusCode;
					httpResponse.StatusDescription = ex2.StatusDescription;
					httpResponse.Headers[WellKnownHeader.MSDiagnostics] = (ex2.DiagnosticText ?? string.Empty);
					httpResponse.Headers[WellKnownHeader.Authentication] = (ex2.ChallengeString ?? string.Empty);
				}
			}
			JsonFaultResponse jsonFaultResponse = JsonFaultResponse.CreateFromException(error);
			DataContractJsonSerializer dataContractJsonSerializer = OWAFaultHandler.CreateJsonSerializer(jsonFaultResponse.GetType());
			dataContractJsonSerializer.WriteObject(httpResponse.OutputStream, jsonFaultResponse);
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00043614 File Offset: 0x00041814
		private static void LogException(Exception error)
		{
			if (!Globals.IsAnonymousCalendarApp)
			{
				RequestDetailsLogger.LogException(error, "OwaFaultHandler", "ProvideFault");
			}
			RequestContext requestContext = RequestContext.Current;
			if (requestContext != null)
			{
				ErrorHandlerUtilities.LogExceptionCodeInIIS(requestContext, error);
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00043648 File Offset: 0x00041848
		private static DataContractJsonSerializer CreateJsonSerializer(Type objectType)
		{
			DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings
			{
				MaxItemsInObjectGraph = int.MaxValue
			};
			return new DataContractJsonSerializer(objectType, settings);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0004366F File Offset: 0x0004186F
		public static bool IsTransientError(Exception exception)
		{
			return exception is OwaTransientException || FaultExceptionUtilities.GetIsTransient(exception);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00043684 File Offset: 0x00041884
		public static void SetBackOffPeriodInMs(Exception exception, JsonFaultBody response)
		{
			OverBudgetException ex = exception as OverBudgetException;
			if (ex != null)
			{
				response.BackOffPeriodInMs = ex.BackoffTime;
			}
		}

		// Token: 0x04000A52 RID: 2642
		private const string jsonContentType = "application/json; charset=utf-8";
	}
}
