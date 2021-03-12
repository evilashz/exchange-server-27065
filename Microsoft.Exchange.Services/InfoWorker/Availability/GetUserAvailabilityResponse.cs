using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.InfoWorker.Availability
{
	// Token: 0x02000009 RID: 9
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserAvailabilityResponse : IExchangeWebMethodResponse
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003105 File Offset: 0x00001305
		// (set) Token: 0x0600004A RID: 74 RVA: 0x0000310D File Offset: 0x0000130D
		[DataMember]
		[XmlArrayItem(Type = typeof(FreeBusyResponse), Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", IsNullable = false)]
		[XmlArray(IsNullable = false)]
		public FreeBusyResponse[] FreeBusyResponseArray
		{
			get
			{
				return this.freeBusyResponseArray;
			}
			set
			{
				this.freeBusyResponseArray = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003116 File Offset: 0x00001316
		// (set) Token: 0x0600004C RID: 76 RVA: 0x0000311E File Offset: 0x0000131E
		[DataMember]
		[XmlElement(IsNullable = false)]
		public SuggestionsResponse SuggestionsResponse
		{
			get
			{
				return this.suggestionsResponse;
			}
			set
			{
				this.suggestionsResponse = value;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003128 File Offset: 0x00001328
		internal static GetUserAvailabilityResponse CreateFrom(AvailabilityQueryResult queryResult)
		{
			if (queryResult == null)
			{
				return null;
			}
			GetUserAvailabilityResponse getUserAvailabilityResponse = new GetUserAvailabilityResponse();
			if (queryResult.FreeBusyResults != null && queryResult.FreeBusyResults.Length > 0)
			{
				int num = queryResult.FreeBusyResults.Length;
				getUserAvailabilityResponse.FreeBusyResponseArray = new FreeBusyResponse[num];
				for (int i = 0; i < num; i++)
				{
					FreeBusyQueryResult freeBusyQueryResult = queryResult.FreeBusyResults[i];
					if (freeBusyQueryResult != null)
					{
						getUserAvailabilityResponse.FreeBusyResponseArray[i] = FreeBusyResponse.CreateFrom(freeBusyQueryResult, i);
					}
				}
			}
			getUserAvailabilityResponse.SuggestionsResponse = SuggestionsResponse.CreateFrom(queryResult.DailyMeetingSuggestions, queryResult.MeetingSuggestionsException);
			return getUserAvailabilityResponse;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000031A7 File Offset: 0x000013A7
		public ResponseType GetResponseType()
		{
			return ResponseType.GetUserAvailabilityResponseMessage;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000031AC File Offset: 0x000013AC
		ResponseCodeType IExchangeWebMethodResponse.GetErrorCodeToLog()
		{
			ResponseCodeType responseCodeType = ResponseCodeType.NoError;
			if (this.FreeBusyResponseArray != null)
			{
				foreach (FreeBusyResponse freeBusyResponse in this.FreeBusyResponseArray)
				{
					if (freeBusyResponse != null && freeBusyResponse.ResponseMessage != null && freeBusyResponse.ResponseMessage.ResponseCode != ResponseCodeType.NoError)
					{
						responseCodeType = freeBusyResponse.ResponseMessage.ResponseCode;
						break;
					}
				}
			}
			if (responseCodeType == ResponseCodeType.NoError && this.SuggestionsResponse != null && this.SuggestionsResponse.ResponseMessage != null)
			{
				responseCodeType = this.SuggestionsResponse.ResponseMessage.ResponseCode;
			}
			return responseCodeType;
		}

		// Token: 0x04000016 RID: 22
		private FreeBusyResponse[] freeBusyResponseArray;

		// Token: 0x04000017 RID: 23
		private SuggestionsResponse suggestionsResponse;
	}
}
