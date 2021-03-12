using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000059 RID: 89
	internal class ApplyDisclaimerWithSeparator : ApplyDisclaimerWithSeparatorAndReadingOrder
	{
		// Token: 0x06000325 RID: 805 RVA: 0x00011E85 File Offset: 0x00010085
		public ApplyDisclaimerWithSeparator(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00011E8E File Offset: 0x0001008E
		public override Type[] ArgumentsType
		{
			get
			{
				return ApplyDisclaimerWithSeparator.argumentTypes;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00011E95 File Offset: 0x00010095
		public override Version MinimumVersion
		{
			get
			{
				return ApplyDisclaimerWithSeparator.minimumVersion;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00011E9C File Offset: 0x0001009C
		public override string Name
		{
			get
			{
				return "ApplyDisclaimerWithSeparator";
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00011EA3 File Offset: 0x000100A3
		protected override string GetReadingOrder(RulesEvaluationContext context)
		{
			return "LeftToRight";
		}

		// Token: 0x040001F3 RID: 499
		private static Version minimumVersion = new Version("1.2");

		// Token: 0x040001F4 RID: 500
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string)
		};
	}
}
