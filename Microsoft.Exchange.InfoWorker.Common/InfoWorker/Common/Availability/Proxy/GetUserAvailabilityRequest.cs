using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x02000112 RID: 274
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetUserAvailabilityRequest : BaseRequestType
	{
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0001F8ED File Offset: 0x0001DAED
		// (set) Token: 0x06000757 RID: 1879 RVA: 0x0001F8F5 File Offset: 0x0001DAF5
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public SerializableTimeZone TimeZone
		{
			get
			{
				return this.timeZone;
			}
			set
			{
				this.timeZone = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0001F8FE File Offset: 0x0001DAFE
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x0001F906 File Offset: 0x0001DB06
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public MailboxData[] MailboxDataArray
		{
			get
			{
				return this.mailboxDataArrayField;
			}
			set
			{
				this.mailboxDataArrayField = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0001F90F File Offset: 0x0001DB0F
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x0001F917 File Offset: 0x0001DB17
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public FreeBusyViewOptions FreeBusyViewOptions
		{
			get
			{
				return this.freeBusyViewOptionsField;
			}
			set
			{
				this.freeBusyViewOptionsField = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x0001F920 File Offset: 0x0001DB20
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x0001F928 File Offset: 0x0001DB28
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public SuggestionsViewOptions SuggestionsViewOptions
		{
			get
			{
				return this.suggestionsViewOptionsField;
			}
			set
			{
				this.suggestionsViewOptionsField = value;
			}
		}

		// Token: 0x0400046E RID: 1134
		private SerializableTimeZone timeZone;

		// Token: 0x0400046F RID: 1135
		private MailboxData[] mailboxDataArrayField;

		// Token: 0x04000470 RID: 1136
		private FreeBusyViewOptions freeBusyViewOptionsField;

		// Token: 0x04000471 RID: 1137
		private SuggestionsViewOptions suggestionsViewOptionsField;
	}
}
