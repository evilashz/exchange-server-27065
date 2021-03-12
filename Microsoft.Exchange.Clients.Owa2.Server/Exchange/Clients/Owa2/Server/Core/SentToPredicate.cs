using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200021D RID: 541
	internal class SentToPredicate : IsSameUserPredicate
	{
		// Token: 0x060014C4 RID: 5316 RVA: 0x00049C70 File Offset: 0x00047E70
		public SentToPredicate(Property property, ShortList<string> addressesToCompare, RulesCreationContext creationContext) : base(property, addressesToCompare, creationContext)
		{
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x00049C7B File Offset: 0x00047E7B
		public override string Name
		{
			get
			{
				return "SentTo";
			}
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x00049C82 File Offset: 0x00047E82
		public void SetValue(ShortList<string> addressesToCompare)
		{
			if (addressesToCompare != null && addressesToCompare.Count > 0)
			{
				this.value = this.BuildValue(addressesToCompare, null);
			}
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x00049CA0 File Offset: 0x00047EA0
		protected override object GetConditionValue(BaseTransportRulesEvaluationContext context)
		{
			if (!this.areAdressesExpanded)
			{
				OwaRulesEvaluationContext owaRulesEvaluationContext = context as OwaRulesEvaluationContext;
				this.SetValue(ADUtils.GetAllEmailAddresses(base.Value.RawValues, owaRulesEvaluationContext.OrganizationId));
				this.areAdressesExpanded = true;
			}
			return base.Value.GetValue(context);
		}

		// Token: 0x04000B43 RID: 2883
		private bool areAdressesExpanded;
	}
}
