using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000183 RID: 387
	[DebuggerStepThrough]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[XmlRoot(Namespace = "HMSYNC:", IsNullable = false)]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "HMSYNC:")]
	[Serializable]
	public class Fault
	{
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x0001DAE3 File Offset: 0x0001BCE3
		// (set) Token: 0x06000B05 RID: 2821 RVA: 0x0001DAEB File Offset: 0x0001BCEB
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

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0001DAF4 File Offset: 0x0001BCF4
		// (set) Token: 0x06000B07 RID: 2823 RVA: 0x0001DAFC File Offset: 0x0001BCFC
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

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0001DB05 File Offset: 0x0001BD05
		// (set) Token: 0x06000B09 RID: 2825 RVA: 0x0001DB0D File Offset: 0x0001BD0D
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

		// Token: 0x0400063A RID: 1594
		private string faultcodeField;

		// Token: 0x0400063B RID: 1595
		private string faultstringField;

		// Token: 0x0400063C RID: 1596
		private string detailField;
	}
}
