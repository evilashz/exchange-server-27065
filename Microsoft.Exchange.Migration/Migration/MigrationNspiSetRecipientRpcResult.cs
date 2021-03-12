using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000F0 RID: 240
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationNspiSetRecipientRpcResult : MigrationProxyRpcResult
	{
		// Token: 0x06000C19 RID: 3097 RVA: 0x00034E33 File Offset: 0x00033033
		public MigrationNspiSetRecipientRpcResult() : base(MigrationProxyRpcType.SetRecipient)
		{
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00034E3C File Offset: 0x0003303C
		public MigrationNspiSetRecipientRpcResult(byte[] resultBlob) : base(resultBlob, MigrationProxyRpcType.SetRecipient)
		{
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x00034E48 File Offset: 0x00033048
		// (set) Token: 0x06000C1C RID: 3100 RVA: 0x00034E86 File Offset: 0x00033086
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
