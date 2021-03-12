using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000EF RID: 239
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationNspiQueryRowsRpcResult : MigrationProxyRpcResult
	{
		// Token: 0x06000C15 RID: 3093 RVA: 0x00034DA7 File Offset: 0x00032FA7
		public MigrationNspiQueryRowsRpcResult() : base(MigrationProxyRpcType.QueryRows)
		{
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00034DB0 File Offset: 0x00032FB0
		public MigrationNspiQueryRowsRpcResult(byte[] resultBlob) : base(resultBlob, MigrationProxyRpcType.QueryRows)
		{
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x00034DBC File Offset: 0x00032FBC
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x00034DFA File Offset: 0x00032FFA
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
