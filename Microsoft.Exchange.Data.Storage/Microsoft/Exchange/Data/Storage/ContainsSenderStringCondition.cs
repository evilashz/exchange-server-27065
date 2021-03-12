using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B9D RID: 2973
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ContainsSenderStringCondition : StringCondition
	{
		// Token: 0x06006AA0 RID: 27296 RVA: 0x001C74F8 File Offset: 0x001C56F8
		private ContainsSenderStringCondition(Rule rule, string[] text) : base(ConditionType.ContainsSenderStringCondition, rule, text)
		{
		}

		// Token: 0x06006AA1 RID: 27297 RVA: 0x001C7504 File Offset: 0x001C5704
		public static ContainsSenderStringCondition Create(Rule rule, string[] text)
		{
			Condition.CheckParams(new object[]
			{
				rule,
				text
			});
			return new ContainsSenderStringCondition(rule, text);
		}

		// Token: 0x06006AA2 RID: 27298 RVA: 0x001C7530 File Offset: 0x001C5730
		internal override Restriction BuildRestriction()
		{
			byte[][] array = new byte[base.Text.Length][];
			for (int i = 0; i < base.Text.Length; i++)
			{
				string s = base.Text[i].ToUpperInvariant();
				array[i] = CTSGlobals.AsciiEncoding.GetBytes(s);
			}
			return Condition.CreateORSearchKeyContentRestriction(array, PropTag.SenderSearchKey, ContentFlags.SubString);
		}
	}
}
