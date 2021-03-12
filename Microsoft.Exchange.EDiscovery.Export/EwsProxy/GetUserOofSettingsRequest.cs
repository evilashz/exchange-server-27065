using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000364 RID: 868
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetUserOofSettingsRequest : BaseRequestType
	{
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x000299ED File Offset: 0x00027BED
		// (set) Token: 0x06001BC3 RID: 7107 RVA: 0x000299F5 File Offset: 0x00027BF5
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public EmailAddress Mailbox
		{
			get
			{
				return this.mailboxField;
			}
			set
			{
				this.mailboxField = value;
			}
		}

		// Token: 0x0400127C RID: 4732
		private EmailAddress mailboxField;
	}
}
