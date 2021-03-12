using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002CC RID: 716
	internal static class MessageConverter
	{
		// Token: 0x06001406 RID: 5126 RVA: 0x0005E220 File Offset: 0x0005C420
		internal static Microsoft.Exchange.SoapWebClient.EWS.EmailAddressType[] CopyEmailAddressArray(Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.EmailAddressType[] dispatcherArray)
		{
			if (dispatcherArray == null)
			{
				return null;
			}
			Microsoft.Exchange.SoapWebClient.EWS.EmailAddressType[] array = new Microsoft.Exchange.SoapWebClient.EWS.EmailAddressType[dispatcherArray.Length];
			for (int i = 0; i < dispatcherArray.Length; i++)
			{
				array[i] = MessageConverter.CopyEmailAddress(dispatcherArray[i]);
			}
			return array;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0005E258 File Offset: 0x0005C458
		internal static Microsoft.Exchange.SoapWebClient.EWS.EmailAddressType CopyEmailAddress(Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.EmailAddressType dispatcherEmail)
		{
			if (dispatcherEmail == null)
			{
				return null;
			}
			return new Microsoft.Exchange.SoapWebClient.EWS.EmailAddressType
			{
				EmailAddress = dispatcherEmail.EmailAddress,
				Name = dispatcherEmail.Name
			};
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0005E28C File Offset: 0x0005C48C
		internal static Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.EmailAddressType CopyEmailAddress(Microsoft.Exchange.SoapWebClient.EWS.EmailAddressType ewsEmail)
		{
			if (ewsEmail == null)
			{
				return null;
			}
			return new Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.EmailAddressType
			{
				EmailAddress = ewsEmail.EmailAddress,
				Name = ewsEmail.Name
			};
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0005E2C0 File Offset: 0x0005C4C0
		internal static Microsoft.Exchange.SoapWebClient.EWS.FindMessageTrackingReportResponseMessageType CopyDispatcherTypeToEWSType(Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.FindMessageTrackingReportResponseMessageType dispatcherResponse)
		{
			if (dispatcherResponse == null)
			{
				return null;
			}
			Microsoft.Exchange.SoapWebClient.EWS.FindMessageTrackingReportResponseMessageType findMessageTrackingReportResponseMessageType = new Microsoft.Exchange.SoapWebClient.EWS.FindMessageTrackingReportResponseMessageType();
			findMessageTrackingReportResponseMessageType.Diagnostics = dispatcherResponse.Diagnostics;
			findMessageTrackingReportResponseMessageType.Properties = MessageConverter.CopyTrackingProperties(dispatcherResponse.Properties);
			findMessageTrackingReportResponseMessageType.ExecutedSearchScope = dispatcherResponse.ExecutedSearchScope;
			findMessageTrackingReportResponseMessageType.Errors = MessageConverter.CopyErrors(dispatcherResponse.Errors);
			Microsoft.Exchange.SoapWebClient.EWS.ResponseCodeType responseCode;
			if (EnumValidator<Microsoft.Exchange.SoapWebClient.EWS.ResponseCodeType>.TryParse(dispatcherResponse.ResponseCode, EnumParseOptions.Default, out responseCode))
			{
				findMessageTrackingReportResponseMessageType.ResponseCode = responseCode;
			}
			else
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string>(0, "{0} cannot be converted to a valid ResponseCodeType, mapping to ErrorMessageTrackingPermanentError", dispatcherResponse.ResponseCode);
				findMessageTrackingReportResponseMessageType.ResponseCode = Microsoft.Exchange.SoapWebClient.EWS.ResponseCodeType.ErrorMessageTrackingPermanentError;
			}
			findMessageTrackingReportResponseMessageType.ResponseClass = EnumConverter<Microsoft.Exchange.SoapWebClient.EWS.ResponseClassType, Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.ResponseClassType>.Convert(dispatcherResponse.ResponseClass);
			findMessageTrackingReportResponseMessageType.MessageText = dispatcherResponse.MessageText;
			if (dispatcherResponse.MessageTrackingSearchResults == null)
			{
				return findMessageTrackingReportResponseMessageType;
			}
			findMessageTrackingReportResponseMessageType.MessageTrackingSearchResults = new Microsoft.Exchange.SoapWebClient.EWS.FindMessageTrackingSearchResultType[dispatcherResponse.MessageTrackingSearchResults.Length];
			for (int i = 0; i < dispatcherResponse.MessageTrackingSearchResults.Length; i++)
			{
				Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.FindMessageTrackingSearchResultType findMessageTrackingSearchResultType = dispatcherResponse.MessageTrackingSearchResults[i];
				Microsoft.Exchange.SoapWebClient.EWS.FindMessageTrackingSearchResultType findMessageTrackingSearchResultType2 = new Microsoft.Exchange.SoapWebClient.EWS.FindMessageTrackingSearchResultType();
				findMessageTrackingSearchResultType2.MessageTrackingReportId = findMessageTrackingSearchResultType.MessageTrackingReportId;
				findMessageTrackingSearchResultType2.PreviousHopServer = findMessageTrackingSearchResultType.PreviousHopServer;
				findMessageTrackingSearchResultType2.PurportedSender = MessageConverter.CopyEmailAddress(findMessageTrackingSearchResultType.PurportedSender);
				findMessageTrackingSearchResultType2.Recipients = MessageConverter.CopyEmailAddressArray(findMessageTrackingSearchResultType.Recipients);
				findMessageTrackingSearchResultType2.Sender = MessageConverter.CopyEmailAddress(findMessageTrackingSearchResultType.Sender);
				findMessageTrackingSearchResultType2.Subject = findMessageTrackingSearchResultType.Subject;
				findMessageTrackingSearchResultType2.SubmittedTime = findMessageTrackingSearchResultType.SubmittedTime;
				findMessageTrackingSearchResultType2.FirstHopServer = findMessageTrackingSearchResultType.FirstHopServer;
				findMessageTrackingSearchResultType2.Properties = MessageConverter.CopyTrackingProperties(findMessageTrackingSearchResultType.Properties);
				findMessageTrackingReportResponseMessageType.MessageTrackingSearchResults[i] = findMessageTrackingSearchResultType2;
			}
			return findMessageTrackingReportResponseMessageType;
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0005E444 File Offset: 0x0005C644
		internal static Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.FindMessageTrackingReportRequestType CopyEWSTypeToDispatcherType(Microsoft.Exchange.SoapWebClient.EWS.FindMessageTrackingReportRequestType request)
		{
			return new Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.FindMessageTrackingReportRequestType
			{
				DiagnosticsLevel = request.DiagnosticsLevel,
				Domain = request.Domain,
				EndDateTime = request.EndDateTime,
				EndDateTimeSpecified = request.EndDateTimeSpecified,
				FederatedDeliveryMailbox = MessageConverter.CopyEmailAddress(request.FederatedDeliveryMailbox),
				MessageId = request.MessageId,
				PurportedSender = MessageConverter.CopyEmailAddress(request.PurportedSender),
				Recipient = MessageConverter.CopyEmailAddress(request.Recipient),
				Scope = request.Scope,
				Sender = MessageConverter.CopyEmailAddress(request.Sender),
				StartDateTime = request.StartDateTime,
				StartDateTimeSpecified = request.StartDateTimeSpecified,
				Subject = request.Subject,
				ServerHint = request.ServerHint,
				Properties = MessageConverter.CopyTrackingProperties(request.Properties)
			};
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0005E528 File Offset: 0x0005C728
		internal static Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.GetMessageTrackingReportRequestType CopyEWSTypeToDispatcherType(Microsoft.Exchange.SoapWebClient.EWS.GetMessageTrackingReportRequestType request)
		{
			return new Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.GetMessageTrackingReportRequestType
			{
				DiagnosticsLevel = request.DiagnosticsLevel,
				MessageTrackingReportId = request.MessageTrackingReportId,
				RecipientFilter = MessageConverter.CopyEmailAddress(request.RecipientFilter),
				ReportTemplate = (Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.MessageTrackingReportTemplateType)request.ReportTemplate,
				ReturnQueueEvents = request.ReturnQueueEvents,
				ReturnQueueEventsSpecified = request.ReturnQueueEventsSpecified,
				Scope = request.Scope,
				Properties = MessageConverter.CopyTrackingProperties(request.Properties)
			};
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0005E5A8 File Offset: 0x0005C7A8
		internal static Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.TrackingPropertyType[] CopyTrackingProperties(Microsoft.Exchange.SoapWebClient.EWS.TrackingPropertyType[] ewsProperties)
		{
			if (ewsProperties == null)
			{
				return null;
			}
			Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.TrackingPropertyType[] array = new Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.TrackingPropertyType[ewsProperties.Length];
			for (int i = 0; i < ewsProperties.Length; i++)
			{
				array[i] = new Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.TrackingPropertyType();
				array[i].Name = ewsProperties[i].Name;
				array[i].Value = ewsProperties[i].Value;
			}
			return array;
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0005E5FC File Offset: 0x0005C7FC
		internal static Microsoft.Exchange.SoapWebClient.EWS.TrackingPropertyType[] CopyTrackingProperties(Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.TrackingPropertyType[] rdProperties)
		{
			if (rdProperties == null)
			{
				return null;
			}
			Microsoft.Exchange.SoapWebClient.EWS.TrackingPropertyType[] array = new Microsoft.Exchange.SoapWebClient.EWS.TrackingPropertyType[rdProperties.Length];
			for (int i = 0; i < rdProperties.Length; i++)
			{
				array[i] = new Microsoft.Exchange.SoapWebClient.EWS.TrackingPropertyType();
				array[i].Name = rdProperties[i].Name;
				array[i].Value = rdProperties[i].Value;
			}
			return array;
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0005E650 File Offset: 0x0005C850
		internal static Microsoft.Exchange.SoapWebClient.EWS.ArrayOfTrackingPropertiesType[] CopyErrors(Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.ArrayOfTrackingPropertiesType[] rdErrors)
		{
			if (rdErrors == null)
			{
				return null;
			}
			Microsoft.Exchange.SoapWebClient.EWS.ArrayOfTrackingPropertiesType[] array = new Microsoft.Exchange.SoapWebClient.EWS.ArrayOfTrackingPropertiesType[rdErrors.Length];
			for (int i = 0; i < rdErrors.Length; i++)
			{
				array[i] = new Microsoft.Exchange.SoapWebClient.EWS.ArrayOfTrackingPropertiesType();
				array[i].Items = MessageConverter.CopyTrackingProperties(rdErrors[i].Items);
			}
			return array;
		}
	}
}
