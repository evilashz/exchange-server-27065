using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200024E RID: 590
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetPasswordExpirationDateResponseMessageType : ResponseMessageType
	{
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x00026A27 File Offset: 0x00024C27
		// (set) Token: 0x06001616 RID: 5654 RVA: 0x00026A2F File Offset: 0x00024C2F
		public DateTime PasswordExpirationDate
		{
			get
			{
				return this.passwordExpirationDateField;
			}
			set
			{
				this.passwordExpirationDateField = value;
			}
		}

		// Token: 0x04000F2B RID: 3883
		private DateTime passwordExpirationDateField;
	}
}
