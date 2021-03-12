using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200014F RID: 335
	public class Set : ICloneable
	{
		// Token: 0x17001A69 RID: 6761
		// (get) Token: 0x06002163 RID: 8547 RVA: 0x000648E4 File Offset: 0x00062AE4
		// (set) Token: 0x06002164 RID: 8548 RVA: 0x000648EC File Offset: 0x00062AEC
		public string Variable { get; set; }

		// Token: 0x17001A6A RID: 6762
		// (get) Token: 0x06002165 RID: 8549 RVA: 0x000648F5 File Offset: 0x00062AF5
		// (set) Token: 0x06002166 RID: 8550 RVA: 0x000648FD File Offset: 0x00062AFD
		public object Value { get; set; }

		// Token: 0x06002167 RID: 8551 RVA: 0x00064908 File Offset: 0x00062B08
		public object Clone()
		{
			return new Set
			{
				Variable = this.Variable,
				Value = this.Value
			};
		}
	}
}
