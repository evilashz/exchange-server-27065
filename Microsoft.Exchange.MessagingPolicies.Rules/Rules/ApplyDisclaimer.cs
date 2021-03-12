using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200005A RID: 90
	internal class ApplyDisclaimer : ApplyDisclaimerWithSeparator
	{
		// Token: 0x0600032B RID: 811 RVA: 0x00011F30 File Offset: 0x00010130
		public ApplyDisclaimer(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00011F39 File Offset: 0x00010139
		public override Type[] ArgumentsType
		{
			get
			{
				return ApplyDisclaimer.argumentTypes;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00011F40 File Offset: 0x00010140
		public override Version MinimumVersion
		{
			get
			{
				return TransportRuleConstants.VersionedContainerBaseVersion;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00011F47 File Offset: 0x00010147
		public override string Name
		{
			get
			{
				return "ApplyDisclaimer";
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00011F4E File Offset: 0x0001014E
		protected override string GetSeparator(RulesEvaluationContext context)
		{
			return "WithoutSeparator";
		}

		// Token: 0x040001F5 RID: 501
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string)
		};
	}
}
