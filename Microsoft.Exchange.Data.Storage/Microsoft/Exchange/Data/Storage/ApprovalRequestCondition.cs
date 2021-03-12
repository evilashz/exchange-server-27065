using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B94 RID: 2964
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ApprovalRequestCondition : FormsCondition
	{
		// Token: 0x06006A85 RID: 27269 RVA: 0x001C70FA File Offset: 0x001C52FA
		private ApprovalRequestCondition(Rule rule, string[] text) : base(ConditionType.ApprovalRequestCondition, rule, text)
		{
		}

		// Token: 0x06006A86 RID: 27270 RVA: 0x001C7108 File Offset: 0x001C5308
		public static ApprovalRequestCondition Create(Rule rule)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			return new ApprovalRequestCondition(rule, new string[]
			{
				"IPM.Note.Microsoft.Approval.Request"
			});
		}
	}
}
