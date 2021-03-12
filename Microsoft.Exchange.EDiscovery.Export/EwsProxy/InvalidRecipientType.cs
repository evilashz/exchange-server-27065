using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001E6 RID: 486
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class InvalidRecipientType
	{
		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x00025852 File Offset: 0x00023A52
		// (set) Token: 0x060013F8 RID: 5112 RVA: 0x0002585A File Offset: 0x00023A5A
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddressField;
			}
			set
			{
				this.smtpAddressField = value;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x00025863 File Offset: 0x00023A63
		// (set) Token: 0x060013FA RID: 5114 RVA: 0x0002586B File Offset: 0x00023A6B
		public InvalidRecipientResponseCodeType ResponseCode
		{
			get
			{
				return this.responseCodeField;
			}
			set
			{
				this.responseCodeField = value;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x00025874 File Offset: 0x00023A74
		// (set) Token: 0x060013FC RID: 5116 RVA: 0x0002587C File Offset: 0x00023A7C
		public string MessageText
		{
			get
			{
				return this.messageTextField;
			}
			set
			{
				this.messageTextField = value;
			}
		}

		// Token: 0x04000DC5 RID: 3525
		private string smtpAddressField;

		// Token: 0x04000DC6 RID: 3526
		private InvalidRecipientResponseCodeType responseCodeField;

		// Token: 0x04000DC7 RID: 3527
		private string messageTextField;
	}
}
