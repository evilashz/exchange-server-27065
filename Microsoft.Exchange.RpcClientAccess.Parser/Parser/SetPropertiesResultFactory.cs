using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000129 RID: 297
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetPropertiesResultFactory : StandardResultFactory
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x00010D74 File Offset: 0x0000EF74
		internal SetPropertiesResultFactory() : base(RopId.SetProperties)
		{
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00010D7E File Offset: 0x0000EF7E
		public RopResult CreateSuccessfulResult(PropertyProblem[] propertyProblems)
		{
			return new SuccessfulSetPropertiesResult(RopId.SetProperties, propertyProblems);
		}
	}
}
