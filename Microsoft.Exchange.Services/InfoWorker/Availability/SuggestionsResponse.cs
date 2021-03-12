using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.InfoWorker.Availability
{
	// Token: 0x02000012 RID: 18
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SuggestionsResponse
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003A0F File Offset: 0x00001C0F
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003A17 File Offset: 0x00001C17
		[DataMember]
		[XmlElement(IsNullable = false)]
		public ResponseMessage ResponseMessage
		{
			get
			{
				return this.responseMessage;
			}
			set
			{
				this.responseMessage = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003A20 File Offset: 0x00001C20
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00003A28 File Offset: 0x00001C28
		[XmlArray(IsNullable = false)]
		[DataMember]
		[XmlArrayItem(Type = typeof(SuggestionDayResult), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public SuggestionDayResult[] SuggestionDayResultArray
		{
			get
			{
				return this.suggestionDayResultArray;
			}
			set
			{
				this.suggestionDayResultArray = value;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003A34 File Offset: 0x00001C34
		internal static SuggestionsResponse CreateFrom(SuggestionDayResult[] suggestionDayResultArray, LocalizedException suggestionsException)
		{
			if (suggestionDayResultArray == null && suggestionsException == null)
			{
				return null;
			}
			return new SuggestionsResponse
			{
				SuggestionDayResultArray = suggestionDayResultArray,
				ResponseMessage = ResponseMessageBuilder.ResponseMessageFromExchangeException(suggestionsException)
			};
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003A63 File Offset: 0x00001C63
		private SuggestionsResponse()
		{
		}

		// Token: 0x0400002A RID: 42
		private SuggestionDayResult[] suggestionDayResultArray;

		// Token: 0x0400002B RID: 43
		private ResponseMessage responseMessage;
	}
}
