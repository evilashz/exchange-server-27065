using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001F9 RID: 505
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class AlternateIdType : AlternateIdBaseType
	{
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x00025B15 File Offset: 0x00023D15
		// (set) Token: 0x0600144C RID: 5196 RVA: 0x00025B1D File Offset: 0x00023D1D
		[XmlAttribute]
		public string Id
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x00025B26 File Offset: 0x00023D26
		// (set) Token: 0x0600144E RID: 5198 RVA: 0x00025B2E File Offset: 0x00023D2E
		[XmlAttribute]
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

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x00025B37 File Offset: 0x00023D37
		// (set) Token: 0x06001450 RID: 5200 RVA: 0x00025B3F File Offset: 0x00023D3F
		[XmlAttribute]
		public bool IsArchive
		{
			get
			{
				return this.isArchiveField;
			}
			set
			{
				this.isArchiveField = value;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x00025B48 File Offset: 0x00023D48
		// (set) Token: 0x06001452 RID: 5202 RVA: 0x00025B50 File Offset: 0x00023D50
		[XmlIgnore]
		public bool IsArchiveSpecified
		{
			get
			{
				return this.isArchiveFieldSpecified;
			}
			set
			{
				this.isArchiveFieldSpecified = value;
			}
		}

		// Token: 0x04000E03 RID: 3587
		private string idField;

		// Token: 0x04000E04 RID: 3588
		private string mailboxField;

		// Token: 0x04000E05 RID: 3589
		private bool isArchiveField;

		// Token: 0x04000E06 RID: 3590
		private bool isArchiveFieldSpecified;
	}
}
