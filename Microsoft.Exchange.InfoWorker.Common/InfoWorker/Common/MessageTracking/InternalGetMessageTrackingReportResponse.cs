using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002C4 RID: 708
	internal class InternalGetMessageTrackingReportResponse
	{
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x0005BE77 File Offset: 0x0005A077
		// (set) Token: 0x060013BE RID: 5054 RVA: 0x0005BE7F File Offset: 0x0005A07F
		internal Microsoft.Exchange.SoapWebClient.EWS.GetMessageTrackingReportResponseMessageType Response { get; private set; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x0005BE88 File Offset: 0x0005A088
		// (set) Token: 0x060013C0 RID: 5056 RVA: 0x0005BE90 File Offset: 0x0005A090
		internal List<RecipientTrackingEvent> RecipientTrackingEvents { get; private set; }

		// Token: 0x060013C1 RID: 5057 RVA: 0x0005BE99 File Offset: 0x0005A099
		internal static InternalGetMessageTrackingReportResponse Create(string domain, Microsoft.Exchange.SoapWebClient.EWS.GetMessageTrackingReportResponseMessageType response)
		{
			if (!InternalGetMessageTrackingReportResponse.CheckValidAndFixupIfNeeded(response))
			{
				return null;
			}
			return new InternalGetMessageTrackingReportResponse(domain, response);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0005BEAC File Offset: 0x0005A0AC
		internal static InternalGetMessageTrackingReportResponse Create(string domain, Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.GetMessageTrackingReportResponseMessageType response)
		{
			if (!InternalGetMessageTrackingReportResponse.CheckValidAndFixupIfNeeded(response))
			{
				return null;
			}
			return new InternalGetMessageTrackingReportResponse(domain, response);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0005BEC0 File Offset: 0x0005A0C0
		private static bool CheckValidAndFixupIfNeeded(Microsoft.Exchange.SoapWebClient.EWS.GetMessageTrackingReportResponseMessageType response)
		{
			if (response == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(0, "Empty/Invalid response for GetMessageTrackingReport", new object[0]);
				return false;
			}
			if (response.MessageTrackingReport == null)
			{
				if (response.ResponseClass == Microsoft.Exchange.SoapWebClient.EWS.ResponseClassType.Success && response.Errors == null && response.Errors.Length == 0)
				{
					TraceWrapper.SearchLibraryTracer.TraceError(0, "Empty/Invalid response for GetMessageTrackingReport is only permitted if there were errors", new object[0]);
					return false;
				}
				TraceWrapper.SearchLibraryTracer.TraceError(0, "Fixing up error response by inserting empty MessageTrackingReportType", new object[0]);
				response.MessageTrackingReport = new Microsoft.Exchange.SoapWebClient.EWS.MessageTrackingReportType();
			}
			return true;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0005BF44 File Offset: 0x0005A144
		private static bool CheckValidAndFixupIfNeeded(Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.GetMessageTrackingReportResponseMessageType response)
		{
			if (response == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(0, "Empty/Invalid response for Proxy.GetMessageTrackingReport", new object[0]);
				return false;
			}
			if (response.MessageTrackingReport == null)
			{
				if (response.ResponseClass == Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.ResponseClassType.Success && response.Errors == null && response.Errors.Length == 0)
				{
					TraceWrapper.SearchLibraryTracer.TraceError(0, "Empty/Invalid response for Proxy.GetMessageTrackingReport is only permitted if there were errors", new object[0]);
					return false;
				}
				TraceWrapper.SearchLibraryTracer.TraceError(0, "Fixing up error response by inserting empty Proxy.MessageTrackingReportType", new object[0]);
				response.MessageTrackingReport = new Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.MessageTrackingReportType();
			}
			return true;
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0005BFC8 File Offset: 0x0005A1C8
		private InternalGetMessageTrackingReportResponse()
		{
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0005BFD0 File Offset: 0x0005A1D0
		private InternalGetMessageTrackingReportResponse(string domain, Microsoft.Exchange.SoapWebClient.EWS.GetMessageTrackingReportResponseMessageType response)
		{
			this.Response = response;
			this.RecipientTrackingEvents = InternalGetMessageTrackingReportResponse.CreateEventList<Microsoft.Exchange.SoapWebClient.EWS.RecipientTrackingEventType>(domain, response.MessageTrackingReport.RecipientTrackingEvents, InternalGetMessageTrackingReportResponse.ewsConversionDelegate);
			response.MessageTrackingReport.RecipientTrackingEvents = null;
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0005C008 File Offset: 0x0005A208
		private InternalGetMessageTrackingReportResponse(string domain, Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.GetMessageTrackingReportResponseMessageType dispatcherResponse)
		{
			this.Response = new Microsoft.Exchange.SoapWebClient.EWS.GetMessageTrackingReportResponseMessageType();
			this.Response.Diagnostics = dispatcherResponse.Diagnostics;
			Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.MessageTrackingReportType messageTrackingReport = dispatcherResponse.MessageTrackingReport;
			Microsoft.Exchange.SoapWebClient.EWS.MessageTrackingReportType messageTrackingReportType = new Microsoft.Exchange.SoapWebClient.EWS.MessageTrackingReportType();
			messageTrackingReportType.OriginalRecipients = MessageConverter.CopyEmailAddressArray(messageTrackingReport.OriginalRecipients);
			messageTrackingReportType.PurportedSender = MessageConverter.CopyEmailAddress(messageTrackingReport.PurportedSender);
			messageTrackingReportType.Sender = MessageConverter.CopyEmailAddress(messageTrackingReport.Sender);
			messageTrackingReportType.Subject = messageTrackingReport.Subject;
			messageTrackingReportType.SubmitTime = messageTrackingReport.SubmitTime;
			messageTrackingReportType.SubmitTimeSpecified = messageTrackingReport.SubmitTimeSpecified;
			messageTrackingReportType.Properties = MessageConverter.CopyTrackingProperties(dispatcherResponse.Properties);
			this.Response.MessageTrackingReport = messageTrackingReportType;
			this.Response.Properties = MessageConverter.CopyTrackingProperties(dispatcherResponse.Properties);
			this.Response.Errors = MessageConverter.CopyErrors(dispatcherResponse.Errors);
			Microsoft.Exchange.SoapWebClient.EWS.ResponseCodeType responseCode;
			if (EnumValidator<Microsoft.Exchange.SoapWebClient.EWS.ResponseCodeType>.TryParse(dispatcherResponse.ResponseCode, EnumParseOptions.Default, out responseCode))
			{
				this.Response.ResponseCode = responseCode;
			}
			else
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string>(0, "{0} cannot be converted to a valid ResponseCodeType, mapping to ErrorMessageTrackingPermanentError", dispatcherResponse.ResponseCode);
				this.Response.ResponseCode = Microsoft.Exchange.SoapWebClient.EWS.ResponseCodeType.ErrorMessageTrackingPermanentError;
			}
			this.Response.ResponseClass = EnumConverter<Microsoft.Exchange.SoapWebClient.EWS.ResponseClassType, Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.ResponseClassType>.Convert(dispatcherResponse.ResponseClass);
			this.Response.MessageText = dispatcherResponse.MessageText;
			messageTrackingReportType.RecipientTrackingEvents = null;
			this.RecipientTrackingEvents = InternalGetMessageTrackingReportResponse.CreateEventList<Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.RecipientTrackingEventType>(domain, messageTrackingReport.RecipientTrackingEvents, InternalGetMessageTrackingReportResponse.dispatcherConversionDelegate);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0005C170 File Offset: 0x0005A370
		private static List<RecipientTrackingEvent> CreateEventList<T>(string domain, T[] wsRecipientTrackingEvents, InternalGetMessageTrackingReportResponse.RecipientEventConversionDelegate<T> conversionMethod)
		{
			if (wsRecipientTrackingEvents == null)
			{
				return new List<RecipientTrackingEvent>(0);
			}
			List<RecipientTrackingEvent> list = new List<RecipientTrackingEvent>(wsRecipientTrackingEvents.Length);
			for (int i = 0; i < wsRecipientTrackingEvents.Length; i++)
			{
				RecipientTrackingEvent recipientTrackingEvent = conversionMethod(domain, wsRecipientTrackingEvents[i]);
				if (recipientTrackingEvent != null)
				{
					list.Add(recipientTrackingEvent);
				}
			}
			return list;
		}

		// Token: 0x04000D13 RID: 3347
		private static InternalGetMessageTrackingReportResponse.RecipientEventConversionDelegate<Microsoft.Exchange.SoapWebClient.EWS.RecipientTrackingEventType> ewsConversionDelegate = new InternalGetMessageTrackingReportResponse.RecipientEventConversionDelegate<Microsoft.Exchange.SoapWebClient.EWS.RecipientTrackingEventType>(RecipientTrackingEvent.Create);

		// Token: 0x04000D14 RID: 3348
		private static InternalGetMessageTrackingReportResponse.RecipientEventConversionDelegate<Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.RecipientTrackingEventType> dispatcherConversionDelegate = new InternalGetMessageTrackingReportResponse.RecipientEventConversionDelegate<Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.RecipientTrackingEventType>(RecipientTrackingEvent.Create);

		// Token: 0x020002C5 RID: 709
		// (Invoke) Token: 0x060013CB RID: 5067
		public delegate RecipientTrackingEvent RecipientEventConversionDelegate<T>(string domain, T wsEvent);
	}
}
