using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BA6 RID: 2982
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EncryptedCondition : StringCondition
	{
		// Token: 0x06006AC5 RID: 27333 RVA: 0x001C7B56 File Offset: 0x001C5D56
		private EncryptedCondition(Rule rule, string[] text) : base(ConditionType.EncryptedCondition, rule, text)
		{
		}

		// Token: 0x06006AC6 RID: 27334 RVA: 0x001C7B64 File Offset: 0x001C5D64
		public static EncryptedCondition Create(Rule rule)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			string[] text = new string[]
			{
				"IPM.Note.Secure",
				"IPM.Note" + "." + "SMIME.SignedEncrypted",
				"IPM.Note" + "." + "SMIME.Encrypted"
			};
			return new EncryptedCondition(rule, text);
		}

		// Token: 0x06006AC7 RID: 27335 RVA: 0x001C7BC8 File Offset: 0x001C5DC8
		internal override Restriction BuildRestriction()
		{
			Restriction restriction = Condition.CreateORStringContentRestriction(base.Text, PropTag.MessageClass, ContentFlags.IgnoreCase | ContentFlags.Loose);
			if (Restriction.ResType.Content == restriction.Type)
			{
				return Restriction.Or(new Restriction[]
				{
					restriction
				});
			}
			return restriction;
		}
	}
}
