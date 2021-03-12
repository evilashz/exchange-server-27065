using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.ProvisionRequest
{
	// Token: 0x02000896 RID: 2198
	[XmlRoot(Namespace = "DeltaSyncV2:", IsNullable = false)]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[Serializable]
	public class Provision
	{
		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06002F0D RID: 12045 RVA: 0x00069AFD File Offset: 0x00067CFD
		// (set) Token: 0x06002F0E RID: 12046 RVA: 0x00069B05 File Offset: 0x00067D05
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

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06002F0F RID: 12047 RVA: 0x00069B0E File Offset: 0x00067D0E
		// (set) Token: 0x06002F10 RID: 12048 RVA: 0x00069B16 File Offset: 0x00067D16
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

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06002F11 RID: 12049 RVA: 0x00069B1F File Offset: 0x00067D1F
		// (set) Token: 0x06002F12 RID: 12050 RVA: 0x00069B27 File Offset: 0x00067D27
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

		// Token: 0x040028EB RID: 10475
		private AccountType[] addField;

		// Token: 0x040028EC RID: 10476
		private AccountType[] deleteField;

		// Token: 0x040028ED RID: 10477
		private AccountType[] readField;
	}
}
