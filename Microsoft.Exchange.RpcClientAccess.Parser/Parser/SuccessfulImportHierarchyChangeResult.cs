using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000266 RID: 614
	internal sealed class SuccessfulImportHierarchyChangeResult : RopResult
	{
		// Token: 0x06000D44 RID: 3396 RVA: 0x00028AF1 File Offset: 0x00026CF1
		internal SuccessfulImportHierarchyChangeResult(StoreId folderId) : base(RopId.ImportHierarchyChange, ErrorCode.None, null)
		{
			this.folderId = folderId;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00028B04 File Offset: 0x00026D04
		internal SuccessfulImportHierarchyChangeResult(Reader reader) : base(reader)
		{
			this.folderId = StoreId.Parse(reader);
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00028B19 File Offset: 0x00026D19
		internal StoreId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00028B21 File Offset: 0x00026D21
		internal static SuccessfulImportHierarchyChangeResult Parse(Reader reader)
		{
			return new SuccessfulImportHierarchyChangeResult(reader);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00028B2C File Offset: 0x00026D2C
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			this.folderId.Serialize(writer);
		}

		// Token: 0x04000708 RID: 1800
		private readonly StoreId folderId;
	}
}
