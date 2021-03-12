using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000182 RID: 386
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "HMSYNC:", IsNullable = false)]
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "HMSYNC:")]
	[Serializable]
	public class Options
	{
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0001DAB9 File Offset: 0x0001BCB9
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x0001DAC1 File Offset: 0x0001BCC1
		public byte Conflict
		{
			get
			{
				return this.conflictField;
			}
			set
			{
				this.conflictField = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0001DACA File Offset: 0x0001BCCA
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x0001DAD2 File Offset: 0x0001BCD2
		[XmlIgnore]
		public bool ConflictSpecified
		{
			get
			{
				return this.conflictFieldSpecified;
			}
			set
			{
				this.conflictFieldSpecified = value;
			}
		}

		// Token: 0x04000638 RID: 1592
		private byte conflictField;

		// Token: 0x04000639 RID: 1593
		private bool conflictFieldSpecified;
	}
}
