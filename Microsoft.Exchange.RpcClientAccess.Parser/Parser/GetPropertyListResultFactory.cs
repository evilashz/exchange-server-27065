using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000E8 RID: 232
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetPropertyListResultFactory : StandardResultFactory
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x0000F31C File Offset: 0x0000D51C
		internal GetPropertyListResultFactory() : base(RopId.GetPropertyList)
		{
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000F326 File Offset: 0x0000D526
		public RopResult CreateSuccessfulResult(PropertyTag[] propertyTags)
		{
			return new SuccessfulGetPropertyListResult(propertyTags);
		}
	}
}
