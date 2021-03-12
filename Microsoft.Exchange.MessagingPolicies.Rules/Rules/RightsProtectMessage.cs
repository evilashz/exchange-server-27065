using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200007E RID: 126
	internal sealed class RightsProtectMessage : SetHeaderUniqueValue
	{
		// Token: 0x060003E6 RID: 998 RVA: 0x00014FCF File Offset: 0x000131CF
		public RightsProtectMessage(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00014FD8 File Offset: 0x000131D8
		public override string Name
		{
			get
			{
				return "RightsProtectMessage";
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00014FDF File Offset: 0x000131DF
		public override Version MinimumVersion
		{
			get
			{
				return TransportRuleConstants.VersionedContainerBaseVersion;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00014FE6 File Offset: 0x000131E6
		public override Type[] ArgumentsType
		{
			get
			{
				return RightsProtectMessage.ArgumentTypes;
			}
		}

		// Token: 0x04000260 RID: 608
		private static readonly Type[] ArgumentTypes = new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string)
		};
	}
}
