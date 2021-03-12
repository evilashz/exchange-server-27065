using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006C8 RID: 1736
	[DataContract]
	public class ShouldContinueContext : ErrorRecordContext
	{
		// Token: 0x060049D7 RID: 18903 RVA: 0x000E147A File Offset: 0x000DF67A
		public ShouldContinueContext()
		{
			this.CmdletsPrompted = new List<string>();
		}

		// Token: 0x1700280C RID: 10252
		// (get) Token: 0x060049D8 RID: 18904 RVA: 0x000E148D File Offset: 0x000DF68D
		// (set) Token: 0x060049D9 RID: 18905 RVA: 0x000E1495 File Offset: 0x000DF695
		[DataMember]
		public List<string> CmdletsPrompted { get; set; }
	}
}
