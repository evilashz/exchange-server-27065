using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001A7 RID: 423
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class MailboxStatisticsItemType
	{
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x000247BA File Offset: 0x000229BA
		// (set) Token: 0x06001201 RID: 4609 RVA: 0x000247C2 File Offset: 0x000229C2
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

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x000247CB File Offset: 0x000229CB
		// (set) Token: 0x06001203 RID: 4611 RVA: 0x000247D3 File Offset: 0x000229D3
		public string DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x000247DC File Offset: 0x000229DC
		// (set) Token: 0x06001205 RID: 4613 RVA: 0x000247E4 File Offset: 0x000229E4
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

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x000247ED File Offset: 0x000229ED
		// (set) Token: 0x06001207 RID: 4615 RVA: 0x000247F5 File Offset: 0x000229F5
		public long Size
		{
			get
			{
				return this.sizeField;
			}
			set
			{
				this.sizeField = value;
			}
		}

		// Token: 0x04000C49 RID: 3145
		private string mailboxIdField;

		// Token: 0x04000C4A RID: 3146
		private string displayNameField;

		// Token: 0x04000C4B RID: 3147
		private long itemCountField;

		// Token: 0x04000C4C RID: 3148
		private long sizeField;
	}
}
