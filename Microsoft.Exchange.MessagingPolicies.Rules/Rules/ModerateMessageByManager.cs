using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000078 RID: 120
	internal class ModerateMessageByManager : ModerateMessageByUser
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x0001480E File Offset: 0x00012A0E
		public ModerateMessageByManager(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00014817 File Offset: 0x00012A17
		public override string Name
		{
			get
			{
				return "ModerateMessageByManager";
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0001481E File Offset: 0x00012A1E
		public override Type[] ArgumentsType
		{
			get
			{
				return ModerateMessageByManager.argumentTypes;
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00014825 File Offset: 0x00012A25
		protected override string GetModeratorList(RulesEvaluationContext baseContext)
		{
			return TransportUtils.GetSenderManagerAddress(baseContext);
		}

		// Token: 0x0400025A RID: 602
		private static Type[] argumentTypes = new Type[0];
	}
}
