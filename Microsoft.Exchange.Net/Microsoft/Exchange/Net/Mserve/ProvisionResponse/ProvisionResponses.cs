using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.ProvisionResponse
{
	// Token: 0x020008A1 RID: 2209
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DesignerCategory("code")]
	[Serializable]
	public class ProvisionResponses
	{
		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06002F4E RID: 12110 RVA: 0x0006A948 File Offset: 0x00068B48
		// (set) Token: 0x06002F4F RID: 12111 RVA: 0x0006A950 File Offset: 0x00068B50
		[XmlArrayItem("Account", IsNullable = false)]
		public AccountType[] Add
		{
			get
			{
				return this.addField;
			}
			set
			{
				this.addField = value;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06002F50 RID: 12112 RVA: 0x0006A959 File Offset: 0x00068B59
		// (set) Token: 0x06002F51 RID: 12113 RVA: 0x0006A961 File Offset: 0x00068B61
		[XmlArrayItem("Account", IsNullable = false)]
		public AccountType[] Delete
		{
			get
			{
				return this.deleteField;
			}
			set
			{
				this.deleteField = value;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06002F52 RID: 12114 RVA: 0x0006A96A File Offset: 0x00068B6A
		// (set) Token: 0x06002F53 RID: 12115 RVA: 0x0006A972 File Offset: 0x00068B72
		[XmlArrayItem("Account", IsNullable = false)]
		public AccountType[] Read
		{
			get
			{
				return this.readField;
			}
			set
			{
				this.readField = value;
			}
		}

		// Token: 0x0400290B RID: 10507
		private AccountType[] addField;

		// Token: 0x0400290C RID: 10508
		private AccountType[] deleteField;

		// Token: 0x0400290D RID: 10509
		private AccountType[] readField;
	}
}
