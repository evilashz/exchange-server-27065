using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200021F RID: 543
	internal class SentToScopePredicate : PredicateCondition
	{
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x00049CEB File Offset: 0x00047EEB
		// (set) Token: 0x060014C9 RID: 5321 RVA: 0x00049CF3 File Offset: 0x00047EF3
		public ScopeType Type { get; private set; }

		// Token: 0x060014CA RID: 5322 RVA: 0x00049CFC File Offset: 0x00047EFC
		public SentToScopePredicate(Property property, ScopeType type, RulesCreationContext creationContext) : base(property, new ShortList<string>(), creationContext)
		{
			this.Type = type;
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x00049D12 File Offset: 0x00047F12
		public override string Name
		{
			get
			{
				return "SentToScope";
			}
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00049D19 File Offset: 0x00047F19
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			return null;
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00049D1C File Offset: 0x00047F1C
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			BaseTransportRulesEvaluationContext baseTransportRulesEvaluationContext = (BaseTransportRulesEvaluationContext)baseContext;
			if (baseTransportRulesEvaluationContext == null)
			{
				throw new ArgumentException("context is either null or not of type: BaseTransportRulesEvaluationContext");
			}
			baseTransportRulesEvaluationContext.PredicateName = this.Name;
			ShortList<string> shortList = (ShortList<string>)base.Property.GetValue(baseTransportRulesEvaluationContext);
			List<string> matches = new List<string>();
			bool flag = false;
			if (shortList != null && shortList.Count > 0)
			{
				List<RoutingAddress> list = new List<RoutingAddress>(shortList.Count);
				foreach (string address in shortList)
				{
					if (RoutingAddress.IsValidAddress(address))
					{
						list.Add((RoutingAddress)address);
					}
				}
				switch (this.Type)
				{
				case ScopeType.Internal:
					flag = ADUtils.IsAnyInternal(list, ((OwaRulesEvaluationContext)baseTransportRulesEvaluationContext).OrganizationId, base.EvaluationMode, ref matches);
					break;
				case ScopeType.External:
					flag = ADUtils.IsAnyExternal(list, ((OwaRulesEvaluationContext)baseTransportRulesEvaluationContext).OrganizationId, base.EvaluationMode, ref matches);
					break;
				case ScopeType.ExternalPartner:
					flag = ADUtils.IsAnyExternalPartner(list, ((OwaRulesEvaluationContext)baseTransportRulesEvaluationContext).OrganizationId, base.EvaluationMode, ref matches);
					break;
				case ScopeType.ExternalNonPartner:
					flag = ADUtils.IsAnyExternalNonPartner(list, ((OwaRulesEvaluationContext)baseTransportRulesEvaluationContext).OrganizationId, base.EvaluationMode, ref matches);
					break;
				default:
					throw new ArgumentException(string.Format("this.Type:{0} is not a valid ScopeType.", this.Type));
				}
			}
			base.UpdateEvaluationHistory(baseContext, flag, matches, 0);
			return flag;
		}
	}
}
