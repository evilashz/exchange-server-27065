using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BA4 RID: 2980
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PermissionControlledCondition : FormsCondition
	{
		// Token: 0x06006AC1 RID: 27329 RVA: 0x001C7AA5 File Offset: 0x001C5CA5
		private PermissionControlledCondition(Rule rule, string[] text) : base(ConditionType.PermissionControlledCondition, rule, text)
		{
		}

		// Token: 0x06006AC2 RID: 27330 RVA: 0x001C7AB4 File Offset: 0x001C5CB4
		public static PermissionControlledCondition Create(Rule rule)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			string[] text = new string[]
			{
				"IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA",
				"IPM.Note.rpmsg.Microsoft.Voicemail.UM"
			};
			return new PermissionControlledCondition(rule, text);
		}
	}
}
