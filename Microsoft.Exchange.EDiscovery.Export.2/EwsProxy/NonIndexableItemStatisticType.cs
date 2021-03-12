using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000193 RID: 403
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class NonIndexableItemStatisticType
	{
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x000241D1 File Offset: 0x000223D1
		// (set) Token: 0x0600114E RID: 4430 RVA: 0x000241D9 File Offset: 0x000223D9
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

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x000241E2 File Offset: 0x000223E2
		// (set) Token: 0x06001150 RID: 4432 RVA: 0x000241EA File Offset: 0x000223EA
		public long ItemCount
		{
			get
			{
				return this.itemCountField;
			}
			set
			{
				this.itemCountField = value;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x000241F3 File Offset: 0x000223F3
		// (set) Token: 0x06001152 RID: 4434 RVA: 0x000241FB File Offset: 0x000223FB
		public string ErrorMessage
		{
			get
			{
				return this.errorMessageField;
			}
			set
			{
				this.errorMessageField = value;
			}
		}

		// Token: 0x04000BEB RID: 3051
		private string mailboxField;

		// Token: 0x04000BEC RID: 3052
		private long itemCountField;

		// Token: 0x04000BED RID: 3053
		private string errorMessageField;
	}
}
