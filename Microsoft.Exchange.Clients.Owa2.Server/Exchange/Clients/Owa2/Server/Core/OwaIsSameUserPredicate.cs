using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000220 RID: 544
	internal class OwaIsSameUserPredicate : IsSameUserPredicate
	{
		// Token: 0x060014CE RID: 5326 RVA: 0x00049E94 File Offset: 0x00048094
		public OwaIsSameUserPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00049E9F File Offset: 0x0004809F
		public void SetValue(ShortList<string> addressesToCompare)
		{
			if (addressesToCompare != null && addressesToCompare.Count > 0)
			{
				this.value = this.BuildValue(addressesToCompare, null);
			}
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00049EBC File Offset: 0x000480BC
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

		// Token: 0x04000B4B RID: 2891
		private bool areAdressesExpanded;
	}
}
