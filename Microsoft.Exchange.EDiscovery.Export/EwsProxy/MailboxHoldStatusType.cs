using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000196 RID: 406
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class MailboxHoldStatusType
	{
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x00024260 File Offset: 0x00022460
		// (set) Token: 0x0600115F RID: 4447 RVA: 0x00024268 File Offset: 0x00022468
		public string Mailbox
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

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x00024271 File Offset: 0x00022471
		// (set) Token: 0x06001161 RID: 4449 RVA: 0x00024279 File Offset: 0x00022479
		public HoldStatusType Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x00024282 File Offset: 0x00022482
		// (set) Token: 0x06001163 RID: 4451 RVA: 0x0002428A File Offset: 0x0002248A
		public string AdditionalInfo
		{
			get
			{
				return this.additionalInfoField;
			}
			set
			{
				this.additionalInfoField = value;
			}
		}

		// Token: 0x04000BF2 RID: 3058
		private string mailboxField;

		// Token: 0x04000BF3 RID: 3059
		private HoldStatusType statusField;

		// Token: 0x04000BF4 RID: 3060
		private string additionalInfoField;
	}
}
