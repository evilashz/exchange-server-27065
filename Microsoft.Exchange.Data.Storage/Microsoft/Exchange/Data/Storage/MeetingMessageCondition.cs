using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B92 RID: 2962
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MeetingMessageCondition : FormsCondition
	{
		// Token: 0x06006A81 RID: 27265 RVA: 0x001C705B File Offset: 0x001C525B
		private MeetingMessageCondition(Rule rule, string[] text) : base(ConditionType.MeetingMessageCondition, rule, text)
		{
		}

		// Token: 0x06006A82 RID: 27266 RVA: 0x001C7068 File Offset: 0x001C5268
		public static MeetingMessageCondition Create(Rule rule)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			string[] text = new string[]
			{
				"IPM.Schedule.Meeting.Request",
				"IPM.Schedule.Meeting.Canceled"
			};
			return new MeetingMessageCondition(rule, text);
		}
	}
}
