using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000084 RID: 132
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AlternateMailbox
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0001F63E File Offset: 0x0001D83E
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x0001F646 File Offset: 0x0001D846
		[XmlElement(IsNullable = true)]
		public string Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0001F64F File Offset: 0x0001D84F
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x0001F657 File Offset: 0x0001D857
		[XmlElement(IsNullable = true)]
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

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0001F660 File Offset: 0x0001D860
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x0001F668 File Offset: 0x0001D868
		[XmlElement(IsNullable = true)]
		public string LegacyDN
		{
			get
			{
				return this.legacyDNField;
			}
			set
			{
				this.legacyDNField = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0001F671 File Offset: 0x0001D871
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x0001F679 File Offset: 0x0001D879
		[XmlElement(IsNullable = true)]
		public string Server
		{
			get
			{
				return this.serverField;
			}
			set
			{
				this.serverField = value;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0001F682 File Offset: 0x0001D882
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x0001F68A File Offset: 0x0001D88A
		[XmlElement(IsNullable = true)]
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

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x0001F693 File Offset: 0x0001D893
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x0001F69B File Offset: 0x0001D89B
		[XmlElement(IsNullable = true)]
		public string OwnerSmtpAddress
		{
			get
			{
				return this.ownerSmtpAddressField;
			}
			set
			{
				this.ownerSmtpAddressField = value;
			}
		}

		// Token: 0x04000324 RID: 804
		private string typeField;

		// Token: 0x04000325 RID: 805
		private string displayNameField;

		// Token: 0x04000326 RID: 806
		private string legacyDNField;

		// Token: 0x04000327 RID: 807
		private string serverField;

		// Token: 0x04000328 RID: 808
		private string smtpAddressField;

		// Token: 0x04000329 RID: 809
		private string ownerSmtpAddressField;
	}
}
