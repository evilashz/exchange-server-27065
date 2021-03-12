using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BA5 RID: 2981
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class VoicemailCondition : FormsCondition
	{
		// Token: 0x06006AC3 RID: 27331 RVA: 0x001C7AF2 File Offset: 0x001C5CF2
		private VoicemailCondition(Rule rule, string[] text) : base(ConditionType.VoicemailCondition, rule, text)
		{
		}

		// Token: 0x06006AC4 RID: 27332 RVA: 0x001C7B00 File Offset: 0x001C5D00
		public static VoicemailCondition Create(Rule rule)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			string[] text = new string[]
			{
				"IPM.Note.Microsoft.Voicemail.UM.CA",
				"IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA",
				"IPM.Note.rpmsg.Microsoft.Voicemail.UM",
				"IPM.Note.Microsoft.Voicemail.UM",
				"IPM.Note.Microsoft.Missed.Voice"
			};
			return new VoicemailCondition(rule, text);
		}
	}
}
