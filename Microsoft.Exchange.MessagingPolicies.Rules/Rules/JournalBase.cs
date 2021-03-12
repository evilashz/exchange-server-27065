using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200006B RID: 107
	internal abstract class JournalBase : TransportAction
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000383 RID: 899
		protected abstract string MailItemProperty { get; }

		// Token: 0x06000384 RID: 900
		protected abstract string GetItemToAdd(TransportRulesEvaluationContext context);

		// Token: 0x06000385 RID: 901 RVA: 0x00013E43 File Offset: 0x00012043
		public JournalBase(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00013E4C File Offset: 0x0001204C
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			MailItem mailItem = transportRulesEvaluationContext.MailItem;
			string itemToAdd = this.GetItemToAdd(transportRulesEvaluationContext);
			List<string> list = null;
			object obj;
			if (mailItem.Properties.TryGetValue(this.MailItemProperty, out obj))
			{
				list = (obj as List<string>);
			}
			if (transportRulesEvaluationContext.RulesEvaluationHistory.History != null)
			{
				List<string> list2 = new List<string>();
				foreach (KeyValuePair<Guid, RuleEvaluationResult> keyValuePair in transportRulesEvaluationContext.RulesEvaluationHistory.History)
				{
					if (keyValuePair.Value.IsMatch)
					{
						list2.Add(keyValuePair.Key.ToString("D"));
					}
				}
				if (mailItem.Properties.ContainsKey("Microsoft.Exchange.JournalRuleIds"))
				{
					((List<string>)mailItem.Properties["Microsoft.Exchange.JournalRuleIds"]).AddRange(list2);
				}
				else
				{
					mailItem.Properties["Microsoft.Exchange.JournalRuleIds"] = list2;
				}
			}
			if (list == null)
			{
				list = new List<string>();
				mailItem.Properties[this.MailItemProperty] = list;
			}
			int num = list.BinarySearch(itemToAdd, StringComparer.OrdinalIgnoreCase);
			if (num >= 0)
			{
				return ExecutionControl.Execute;
			}
			list.Insert(~num, itemToAdd);
			return ExecutionControl.Execute;
		}
	}
}
