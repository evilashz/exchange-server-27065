using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000076 RID: 118
	internal class LogEvent : TransportAction
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x00014586 File Offset: 0x00012786
		public LogEvent(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0001458F File Offset: 0x0001278F
		public override Type[] ArgumentsType
		{
			get
			{
				return LogEvent.argumentTypes;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00014596 File Offset: 0x00012796
		public override string Name
		{
			get
			{
				return "LogEvent";
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000145A0 File Offset: 0x000127A0
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext context = (TransportRulesEvaluationContext)baseContext;
			string text = (string)base.Arguments[0].GetValue(context);
			TransportAction.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_RuleActionLogEvent, null, new object[]
			{
				text
			});
			return ExecutionControl.Execute;
		}

		// Token: 0x04000256 RID: 598
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string)
		};
	}
}
