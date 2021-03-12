using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.QueueDigest
{
	// Token: 0x0200006E RID: 110
	internal class DagGroupOfServersKey : GroupOfServersKey
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0000F721 File Offset: 0x0000D921
		public DagGroupOfServersKey(ADObjectId dagId)
		{
			this.dagId = dagId;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000F730 File Offset: 0x0000D930
		public override bool Equals(object other)
		{
			DagGroupOfServersKey dagGroupOfServersKey = other as DagGroupOfServersKey;
			return dagGroupOfServersKey != null && this.dagId.Equals(dagGroupOfServersKey.dagId);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000F75E File Offset: 0x0000D95E
		public override int GetHashCode()
		{
			return this.dagId.GetHashCode();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000F76B File Offset: 0x0000D96B
		public override string ToString()
		{
			return this.dagId.ToString();
		}

		// Token: 0x04000179 RID: 377
		private readonly ADObjectId dagId;
	}
}
