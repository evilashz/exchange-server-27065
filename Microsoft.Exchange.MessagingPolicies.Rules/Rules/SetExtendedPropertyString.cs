using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000084 RID: 132
	internal class SetExtendedPropertyString : TransportAction
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x00015704 File Offset: 0x00013904
		public SetExtendedPropertyString(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0001570D File Offset: 0x0001390D
		public override Type[] ArgumentsType
		{
			get
			{
				return SetExtendedPropertyString.argumentTypes;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00015714 File Offset: 0x00013914
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.BifurcationNeeded;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00015717 File Offset: 0x00013917
		public override string Name
		{
			get
			{
				return "SetExtendedPropertyString";
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00015720 File Offset: 0x00013920
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			string key = (string)base.Arguments[0].GetValue(transportRulesEvaluationContext);
			string value = (string)base.Arguments[1].GetValue(transportRulesEvaluationContext);
			MailItem mailItem = transportRulesEvaluationContext.MailItem;
			mailItem.Properties[key] = value;
			return ExecutionControl.Execute;
		}

		// Token: 0x0400026C RID: 620
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string),
			typeof(string)
		};
	}
}
