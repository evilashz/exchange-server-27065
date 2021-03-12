using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001A5 RID: 421
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class PreviewItemMailboxType
	{
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x00024744 File Offset: 0x00022944
		// (set) Token: 0x060011F3 RID: 4595 RVA: 0x0002474C File Offset: 0x0002294C
		public string MailboxId
		{
			get
			{
				return this.mailboxIdField;
			}
			set
			{
				this.mailboxIdField = value;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00024755 File Offset: 0x00022955
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x0002475D File Offset: 0x0002295D
		public string PrimarySmtpAddress
		{
			get
			{
				return this.primarySmtpAddressField;
			}
			set
			{
				this.primarySmtpAddressField = value;
			}
		}

		// Token: 0x04000C43 RID: 3139
		private string mailboxIdField;

		// Token: 0x04000C44 RID: 3140
		private string primarySmtpAddressField;
	}
}
