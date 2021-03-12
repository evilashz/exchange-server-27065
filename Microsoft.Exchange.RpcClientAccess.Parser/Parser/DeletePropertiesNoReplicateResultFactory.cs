using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000C4 RID: 196
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DeletePropertiesNoReplicateResultFactory : StandardResultFactory
	{
		// Token: 0x0600046E RID: 1134 RVA: 0x0000ED23 File Offset: 0x0000CF23
		internal DeletePropertiesNoReplicateResultFactory() : base(RopId.DeletePropertiesNoReplicate)
		{
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000ED2D File Offset: 0x0000CF2D
		public RopResult CreateSuccessfulResult(PropertyProblem[] propertyProblems)
		{
			return new SuccessfulDeletePropertiesNoReplicateResult(propertyProblems);
		}
	}
}
