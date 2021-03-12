using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x0200012E RID: 302
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[Serializable]
	public class MailTipsResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00024F01 File Offset: 0x00023101
		// (set) Token: 0x06000834 RID: 2100 RVA: 0x00024F09 File Offset: 0x00023109
		public MailTips MailTips
		{
			get
			{
				return this.mailTipsField;
			}
			set
			{
				this.mailTipsField = value;
			}
		}

		// Token: 0x0400065D RID: 1629
		private MailTips mailTipsField;
	}
}
