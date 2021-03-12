using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x02000193 RID: 403
	[XmlType(AnonymousType = true, Namespace = "HMSYNC:")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[XmlRoot(Namespace = "HMSYNC:", IsNullable = false)]
	[Serializable]
	public class AuthPolicy
	{
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0001DD70 File Offset: 0x0001BF70
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x0001DD78 File Offset: 0x0001BF78
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

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0001DD81 File Offset: 0x0001BF81
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x0001DD89 File Offset: 0x0001BF89
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

		// Token: 0x04000684 RID: 1668
		private string sAPField;

		// Token: 0x04000685 RID: 1669
		private string versionField;
	}
}
