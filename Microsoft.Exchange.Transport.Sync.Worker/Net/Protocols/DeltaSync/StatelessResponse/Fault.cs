using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x02000189 RID: 393
	[XmlType(AnonymousType = true, Namespace = "HMSYNC:")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[XmlRoot(Namespace = "HMSYNC:", IsNullable = false)]
	[Serializable]
	public class Fault
	{
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0001DBCF File Offset: 0x0001BDCF
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x0001DBD7 File Offset: 0x0001BDD7
		public string Faultcode
		{
			get
			{
				return this.faultcodeField;
			}
			set
			{
				this.faultcodeField = value;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0001DBE0 File Offset: 0x0001BDE0
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x0001DBE8 File Offset: 0x0001BDE8
		public string Faultstring
		{
			get
			{
				return this.faultstringField;
			}
			set
			{
				this.faultstringField = value;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0001DBF1 File Offset: 0x0001BDF1
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x0001DBF9 File Offset: 0x0001BDF9
		public string Detail
		{
			get
			{
				return this.detailField;
			}
			set
			{
				this.detailField = value;
			}
		}

		// Token: 0x0400064D RID: 1613
		private string faultcodeField;

		// Token: 0x0400064E RID: 1614
		private string faultstringField;

		// Token: 0x0400064F RID: 1615
		private string detailField;
	}
}
