using System;
using System.Xml.Serialization;
using Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x02000110 RID: 272
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SuggestionsResponse
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001F899 File Offset: 0x0001DA99
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x0001F8A1 File Offset: 0x0001DAA1
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ResponseMessage ResponseMessage
		{
			get
			{
				return this.responseMessageField;
			}
			set
			{
				this.responseMessageField = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0001F8AA File Offset: 0x0001DAAA
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x0001F8B2 File Offset: 0x0001DAB2
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
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

		// Token: 0x0400046A RID: 1130
		private SuggestionDayResult[] suggestionDayResultArray;

		// Token: 0x0400046B RID: 1131
		private ResponseMessage responseMessageField;
	}
}
