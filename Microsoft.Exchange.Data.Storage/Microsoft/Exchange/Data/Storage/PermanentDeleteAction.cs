using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B81 RID: 2945
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PermanentDeleteAction : Action
	{
		// Token: 0x06006A3A RID: 27194 RVA: 0x001C5FEF File Offset: 0x001C41EF
		private PermanentDeleteAction(Rule rule) : base(ActionType.PermanentDeleteAction, rule)
		{
		}

		// Token: 0x06006A3B RID: 27195 RVA: 0x001C5FFC File Offset: 0x001C41FC
		public static PermanentDeleteAction Create(Rule rule)
		{
			ActionBase.CheckParams(new object[]
			{
				rule
			});
			return new PermanentDeleteAction(rule);
		}

		// Token: 0x06006A3C RID: 27196 RVA: 0x001C6020 File Offset: 0x001C4220
		internal override RuleAction BuildRuleAction()
		{
			return new RuleAction.Delete();
		}
	}
}
