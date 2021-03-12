using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000063 RID: 99
	internal class Disconnect : TransportAction
	{
		// Token: 0x06000357 RID: 855 RVA: 0x000130B9 File Offset: 0x000112B9
		public Disconnect(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000358 RID: 856 RVA: 0x000130C2 File Offset: 0x000112C2
		public override Version MinimumVersion
		{
			get
			{
				return TransportRuleConstants.VersionedContainerBaseVersion;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000359 RID: 857 RVA: 0x000130C9 File Offset: 0x000112C9
		public override string Name
		{
			get
			{
				return "Disconnect";
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000130D0 File Offset: 0x000112D0
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			if (transportRulesEvaluationContext.EventType == EventType.EndOfData)
			{
				transportRulesEvaluationContext.EndOfDataSource.Disconnect();
				return ExecutionControl.SkipAll;
			}
			return ExecutionControl.Execute;
		}
	}
}
