using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200035D RID: 861
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(UpdateDelegateType))]
	[XmlInclude(typeof(RemoveDelegateType))]
	[XmlInclude(typeof(AddDelegateType))]
	[XmlInclude(typeof(GetDelegateType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public abstract class BaseDelegateType : BaseRequestType
	{
		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x000298C7 File Offset: 0x00027AC7
		// (set) Token: 0x06001BA0 RID: 7072 RVA: 0x000298CF File Offset: 0x00027ACF
		public EmailAddressType Mailbox
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

		// Token: 0x0400126E RID: 4718
		private EmailAddressType mailboxField;
	}
}
