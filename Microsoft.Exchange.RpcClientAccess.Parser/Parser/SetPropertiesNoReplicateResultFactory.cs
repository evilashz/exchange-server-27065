using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000128 RID: 296
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetPropertiesNoReplicateResultFactory : StandardResultFactory
	{
		// Token: 0x060005CA RID: 1482 RVA: 0x00010D60 File Offset: 0x0000EF60
		internal SetPropertiesNoReplicateResultFactory() : base(RopId.SetPropertiesNoReplicate)
		{
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00010D6A File Offset: 0x0000EF6A
		public RopResult CreateSuccessfulResult(PropertyProblem[] propertyProblems)
		{
			return new SuccessfulSetPropertiesResult(RopId.SetPropertiesNoReplicate, propertyProblems);
		}
	}
}
