using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000C5 RID: 197
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DeletePropertiesResultFactory : StandardResultFactory
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x0000ED35 File Offset: 0x0000CF35
		internal DeletePropertiesResultFactory() : base(RopId.DeleteProperties)
		{
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000ED3F File Offset: 0x0000CF3F
		public RopResult CreateSuccessfulResult(PropertyProblem[] propertyProblems)
		{
			return new SuccessfulDeletePropertiesResult(propertyProblems);
		}
	}
}
