using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x0200019E RID: 414
	[DebuggerStepThrough]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[XmlRoot(Namespace = "HMSYNC:", IsNullable = false)]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "HMSYNC:")]
	[Serializable]
	public class Options
	{
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0001DF71 File Offset: 0x0001C171
		// (set) Token: 0x06000B8A RID: 2954 RVA: 0x0001DF79 File Offset: 0x0001C179
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

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0001DF82 File Offset: 0x0001C182
		// (set) Token: 0x06000B8C RID: 2956 RVA: 0x0001DF8A File Offset: 0x0001C18A
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

		// Token: 0x0400069D RID: 1693
		private byte conflictField;

		// Token: 0x0400069E RID: 1694
		private bool conflictFieldSpecified;
	}
}
