using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x02000111 RID: 273
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetUserAvailabilityResponse
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001F8C3 File Offset: 0x0001DAC3
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0001F8CB File Offset: 0x0001DACB
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", IsNullable = false)]
		public FreeBusyResponse[] FreeBusyResponseArray
		{
			get
			{
				return this.freeBusyResponseArrayField;
			}
			set
			{
				this.freeBusyResponseArrayField = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001F8D4 File Offset: 0x0001DAD4
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0001F8DC File Offset: 0x0001DADC
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", IsNullable = false)]
		public SuggestionsResponse SuggestionsResponse
		{
			get
			{
				return this.suggestionsResponseField;
			}
			set
			{
				this.suggestionsResponseField = value;
			}
		}

		// Token: 0x0400046C RID: 1132
		private FreeBusyResponse[] freeBusyResponseArrayField;

		// Token: 0x0400046D RID: 1133
		private SuggestionsResponse suggestionsResponseField;
	}
}
