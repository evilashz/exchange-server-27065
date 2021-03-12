using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200001B RID: 27
	internal class CiMdbInfo
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00004D00 File Offset: 0x00002F00
		public CiMdbInfo(IEnumerable<CiMdbCopyInfo> info)
		{
			this.Copies = info;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004D0F File Offset: 0x00002F0F
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00004D17 File Offset: 0x00002F17
		public IEnumerable<CiMdbCopyInfo> Copies { get; private set; }

		// Token: 0x06000102 RID: 258 RVA: 0x00004D20 File Offset: 0x00002F20
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Join<CiMdbCopyInfo>(";", this.Copies);
			}
			return this.toString;
		}

		// Token: 0x04000074 RID: 116
		private string toString;
	}
}
