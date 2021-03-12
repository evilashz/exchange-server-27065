using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000ED RID: 237
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationNspiGetNewDsaRpcResult : MigrationProxyRpcResult
	{
		// Token: 0x06000C0D RID: 3085 RVA: 0x00034CAF File Offset: 0x00032EAF
		public MigrationNspiGetNewDsaRpcResult() : base(MigrationProxyRpcType.GetNewDSA)
		{
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00034CB8 File Offset: 0x00032EB8
		public MigrationNspiGetNewDsaRpcResult(byte[] resultBlob) : base(resultBlob, MigrationProxyRpcType.GetNewDSA)
		{
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00034CC4 File Offset: 0x00032EC4
		// (set) Token: 0x06000C10 RID: 3088 RVA: 0x00034CED File Offset: 0x00032EED
		public string NspiServer
		{
			get
			{
				object obj;
				if (this.PropertyCollection.TryGetValue(2432892959U, out obj))
				{
					return obj as string;
				}
				return null;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.PropertyCollection[2432892959U] = value;
					return;
				}
				this.PropertyCollection.Remove(2432892959U);
			}
		}
	}
}
