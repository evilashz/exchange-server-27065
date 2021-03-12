using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000EE RID: 238
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationNspiGetRecipientRpcResult : MigrationProxyRpcResult
	{
		// Token: 0x06000C11 RID: 3089 RVA: 0x00034D1A File Offset: 0x00032F1A
		public MigrationNspiGetRecipientRpcResult() : base(MigrationProxyRpcType.GetRecipient)
		{
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00034D23 File Offset: 0x00032F23
		public MigrationNspiGetRecipientRpcResult(byte[] resultBlob) : base(resultBlob, MigrationProxyRpcType.GetRecipient)
		{
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00034D30 File Offset: 0x00032F30
		// (set) Token: 0x06000C14 RID: 3092 RVA: 0x00034D6E File Offset: 0x00032F6E
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
