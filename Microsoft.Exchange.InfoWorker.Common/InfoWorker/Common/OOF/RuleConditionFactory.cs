using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x02000036 RID: 54
	internal static class RuleConditionFactory
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00006B48 File Offset: 0x00004D48
		public static Restriction CreateInternalSendersGroupCondition()
		{
			return RuleConditionFactory.NoCondition;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006B50 File Offset: 0x00004D50
		public static Restriction CreateKnownExternalSendersGroupCondition(string[] knownExternalSenders)
		{
			Restriction[] array = new Restriction[knownExternalSenders.Length];
			for (int i = 0; i < knownExternalSenders.Length; i++)
			{
				array[i] = Restriction.EQ(PropTag.SenderEmailAddress, knownExternalSenders[i]);
			}
			return Restriction.Or(array);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006B8A File Offset: 0x00004D8A
		public static Restriction CreateAllExternalSendersGroupCondition()
		{
			return RuleConditionFactory.NoCondition;
		}

		// Token: 0x0400009F RID: 159
		private const string ExchangeAddressType = "EX";

		// Token: 0x040000A0 RID: 160
		private static readonly Restriction ExchangeAddressTypeCondition = Restriction.EQ(PropTag.SenderAddrType, "EX");

		// Token: 0x040000A1 RID: 161
		private static readonly Restriction NonExchangeAddressTypeCondition = Restriction.NE(PropTag.SenderAddrType, "EX");

		// Token: 0x040000A2 RID: 162
		private static readonly Restriction NoCondition = null;
	}
}
