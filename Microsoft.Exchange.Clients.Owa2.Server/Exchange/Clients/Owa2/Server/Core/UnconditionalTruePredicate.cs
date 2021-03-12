using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000221 RID: 545
	internal class UnconditionalTruePredicate : PredicateCondition
	{
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00049F07 File Offset: 0x00048107
		public override string Name
		{
			get
			{
				return "UnconditionalTrue";
			}
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00049F0E File Offset: 0x0004810E
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			return true;
		}
	}
}
