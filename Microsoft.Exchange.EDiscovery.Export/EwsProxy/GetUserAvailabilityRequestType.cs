using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000365 RID: 869
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class GetUserAvailabilityRequestType : BaseRequestType
	{
		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x00029A06 File Offset: 0x00027C06
		// (set) Token: 0x06001BC6 RID: 7110 RVA: 0x00029A0E File Offset: 0x00027C0E
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public SerializableTimeZone TimeZone
		{
			get
			{
				return this.timeZoneField;
			}
			set
			{
				this.timeZoneField = value;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x00029A17 File Offset: 0x00027C17
		// (set) Token: 0x06001BC8 RID: 7112 RVA: 0x00029A1F File Offset: 0x00027C1F
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

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x00029A28 File Offset: 0x00027C28
		// (set) Token: 0x06001BCA RID: 7114 RVA: 0x00029A30 File Offset: 0x00027C30
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public FreeBusyViewOptionsType FreeBusyViewOptions
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

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06001BCB RID: 7115 RVA: 0x00029A39 File Offset: 0x00027C39
		// (set) Token: 0x06001BCC RID: 7116 RVA: 0x00029A41 File Offset: 0x00027C41
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public SuggestionsViewOptionsType SuggestionsViewOptions
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

		// Token: 0x0400127D RID: 4733
		private SerializableTimeZone timeZoneField;

		// Token: 0x0400127E RID: 4734
		private MailboxData[] mailboxDataArrayField;

		// Token: 0x0400127F RID: 4735
		private FreeBusyViewOptionsType freeBusyViewOptionsField;

		// Token: 0x04001280 RID: 4736
		private SuggestionsViewOptionsType suggestionsViewOptionsField;
	}
}
