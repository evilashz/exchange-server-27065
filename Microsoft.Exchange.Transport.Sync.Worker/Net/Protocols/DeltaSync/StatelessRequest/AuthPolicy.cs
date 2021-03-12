using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000184 RID: 388
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "HMSYNC:", IsNullable = false)]
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "HMSYNC:")]
	[Serializable]
	public class AuthPolicy
	{
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0001DB1E File Offset: 0x0001BD1E
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x0001DB26 File Offset: 0x0001BD26
		public string SAP
		{
			get
			{
				return this.sAPField;
			}
			set
			{
				this.sAPField = value;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0001DB2F File Offset: 0x0001BD2F
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x0001DB37 File Offset: 0x0001BD37
		public string Version
		{
			get
			{
				return this.versionField;
			}
			set
			{
				this.versionField = value;
			}
		}

		// Token: 0x0400063D RID: 1597
		private string sAPField;

		// Token: 0x0400063E RID: 1598
		private string versionField;
	}
}
