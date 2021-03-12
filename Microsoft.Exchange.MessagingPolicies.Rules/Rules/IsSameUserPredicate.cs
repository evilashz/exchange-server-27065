using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x020000A0 RID: 160
	internal class IsSameUserPredicate : PredicateCondition
	{
		// Token: 0x06000483 RID: 1155 RVA: 0x00016C41 File Offset: 0x00014E41
		public IsSameUserPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
			if (!base.Property.IsString)
			{
				throw new RulesValidationException(RulesStrings.StringPropertyOrValueRequired(this.Name));
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00016C6A File Offset: 0x00014E6A
		public override string Name
		{
			get
			{
				return "isSameUser";
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00016C74 File Offset: 0x00014E74
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			BaseTransportRulesEvaluationContext baseTransportRulesEvaluationContext = (BaseTransportRulesEvaluationContext)baseContext;
			if (baseTransportRulesEvaluationContext == null)
			{
				throw new ArgumentException("context is either null or not of type: BaseTransportRulesEvaluationContext");
			}
			baseTransportRulesEvaluationContext.PredicateName = this.Name;
			object value = base.Property.GetValue(baseTransportRulesEvaluationContext);
			object conditionValue = this.GetConditionValue(baseTransportRulesEvaluationContext);
			List<string> list = new List<string>();
			IsSameUserPredicate.LoadExactMatchForIsSameUserPredicateInEOP();
			bool flag = RuleUtils.CompareStringValues(conditionValue, value, (DatacenterRegistry.IsForefrontForOffice() && IsSameUserPredicate.disableExactMatchInEOP != 1) ? ExactUserComparer.CreateInstance() : baseTransportRulesEvaluationContext.UserComparer, base.EvaluationMode, list);
			base.UpdateEvaluationHistory(baseContext, flag, list, 0);
			return flag;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00016CFD File Offset: 0x00014EFD
		protected virtual object GetConditionValue(BaseTransportRulesEvaluationContext context)
		{
			return base.Value.GetValue(context);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00016D0C File Offset: 0x00014F0C
		private static void LoadExactMatchForIsSameUserPredicateInEOP()
		{
			if (!IsSameUserPredicate.disableExactMatchInEOPRegkeyLoaded)
			{
				try
				{
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport"))
					{
						if (registryKey != null)
						{
							IsSameUserPredicate.disableExactMatchInEOP = (int)registryKey.GetValue("disableExactMatchInEOP", 0);
						}
					}
				}
				catch (SecurityException)
				{
				}
				catch (IOException)
				{
				}
				catch (UnauthorizedAccessException)
				{
				}
				catch (InvalidCastException)
				{
				}
				finally
				{
					IsSameUserPredicate.disableExactMatchInEOPRegkeyLoaded = true;
				}
			}
		}

		// Token: 0x04000287 RID: 647
		private static volatile bool disableExactMatchInEOPRegkeyLoaded;

		// Token: 0x04000288 RID: 648
		private static volatile int disableExactMatchInEOP;
	}
}
