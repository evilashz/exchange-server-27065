using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B73 RID: 2931
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class StopProcessingAction : Action
	{
		// Token: 0x06006A0B RID: 27147 RVA: 0x001C5A39 File Offset: 0x001C3C39
		private StopProcessingAction(Rule rule) : base(ActionType.StopProcessingAction, rule)
		{
		}

		// Token: 0x06006A0C RID: 27148 RVA: 0x001C5A44 File Offset: 0x001C3C44
		public static StopProcessingAction Create(Rule rule)
		{
			ActionBase.CheckParams(new object[]
			{
				rule
			});
			return new StopProcessingAction(rule);
		}

		// Token: 0x06006A0D RID: 27149 RVA: 0x001C5A68 File Offset: 0x001C3C68
		internal override RuleAction BuildRuleAction()
		{
			return null;
		}
	}
}
