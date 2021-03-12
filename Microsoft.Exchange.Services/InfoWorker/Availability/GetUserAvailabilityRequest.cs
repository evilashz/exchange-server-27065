using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.InfoWorker.Availability
{
	// Token: 0x02000007 RID: 7
	[XmlType("GetUserAvailabilityRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserAvailabilityRequest : BaseAvailabilityRequest
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000309F File Offset: 0x0000129F
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000030A7 File Offset: 0x000012A7
		[DataMember]
		[XmlElement(IsNullable = false, Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000030B0 File Offset: 0x000012B0
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000030B8 File Offset: 0x000012B8
		[DataMember]
		[XmlArray(IsNullable = false)]
		[XmlArrayItem(ElementName = "MailboxData", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public MailboxData[] MailboxDataArray
		{
			get
			{
				return this.mailboxDataArray;
			}
			set
			{
				this.mailboxDataArray = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000030C1 File Offset: 0x000012C1
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000030C9 File Offset: 0x000012C9
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember]
		public FreeBusyViewOptions FreeBusyViewOptions
		{
			get
			{
				return this.freeBusyViewOptions;
			}
			set
			{
				this.freeBusyViewOptions = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000030D2 File Offset: 0x000012D2
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000030DA File Offset: 0x000012DA
		[DataMember]
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public SuggestionsViewOptions SuggestionsViewOptions
		{
			get
			{
				return this.suggestionsViewOptions;
			}
			set
			{
				this.suggestionsViewOptions = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000030E3 File Offset: 0x000012E3
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000030EB File Offset: 0x000012EB
		[XmlIgnore]
		internal bool DefaultFreeBusyAccessOnly
		{
			get
			{
				return this.defaultFreeBusyAccessOnly;
			}
			set
			{
				this.defaultFreeBusyAccessOnly = value;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000030F4 File Offset: 0x000012F4
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetUserAvailability(callContext, this);
		}

		// Token: 0x04000011 RID: 17
		private SerializableTimeZone timeZone;

		// Token: 0x04000012 RID: 18
		private MailboxData[] mailboxDataArray;

		// Token: 0x04000013 RID: 19
		private FreeBusyViewOptions freeBusyViewOptions;

		// Token: 0x04000014 RID: 20
		private SuggestionsViewOptions suggestionsViewOptions;

		// Token: 0x04000015 RID: 21
		private bool defaultFreeBusyAccessOnly;
	}
}
