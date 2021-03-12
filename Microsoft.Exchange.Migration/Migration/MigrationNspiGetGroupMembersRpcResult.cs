using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000EC RID: 236
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationNspiGetGroupMembersRpcResult : MigrationProxyRpcResult
	{
		// Token: 0x06000C09 RID: 3081 RVA: 0x00034C23 File Offset: 0x00032E23
		public MigrationNspiGetGroupMembersRpcResult() : base(MigrationProxyRpcType.GetGroupMembers)
		{
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00034C2C File Offset: 0x00032E2C
		public MigrationNspiGetGroupMembersRpcResult(byte[] resultBlob) : base(resultBlob, MigrationProxyRpcType.GetGroupMembers)
		{
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x00034C38 File Offset: 0x00032E38
		// (set) Token: 0x06000C0C RID: 3084 RVA: 0x00034C76 File Offset: 0x00032E76
		public int? TotalRowCount
		{
			get
			{
				object obj;
				if (this.PropertyCollection.TryGetValue(2432827395U, out obj) && obj is int)
				{
					return new int?((int)obj);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.PropertyCollection[2432827395U] = value.Value;
					return;
				}
				this.PropertyCollection.Remove(2432827395U);
			}
		}
	}
}
