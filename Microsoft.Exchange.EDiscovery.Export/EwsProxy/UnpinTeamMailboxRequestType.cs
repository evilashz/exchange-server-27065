using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000351 RID: 849
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class UnpinTeamMailboxRequestType : BaseRequestType
	{
		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x0002975E File Offset: 0x0002795E
		// (set) Token: 0x06001B75 RID: 7029 RVA: 0x00029766 File Offset: 0x00027966
		public EmailAddressType EmailAddress
		{
			get
			{
				return this.emailAddressField;
			}
			set
			{
				this.emailAddressField = value;
			}
		}

		// Token: 0x0400124F RID: 4687
		private EmailAddressType emailAddressField;
	}
}
