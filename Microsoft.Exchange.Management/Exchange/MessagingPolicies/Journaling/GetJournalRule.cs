﻿using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000A21 RID: 2593
	[Cmdlet("Get", "JournalRule", DefaultParameterSetName = "Identity")]
	public sealed class GetJournalRule : GetMultitenancySystemConfigurationObjectTask<RuleIdParameter, TransportRule>
	{
		// Token: 0x17001BDC RID: 7132
		// (get) Token: 0x06005CE0 RID: 23776 RVA: 0x001875AC File Offset: 0x001857AC
		// (set) Token: 0x06005CE1 RID: 23777 RVA: 0x001875D2 File Offset: 0x001857D2
		[Parameter(Mandatory = false)]
		public SwitchParameter LawfulInterception
		{
			get
			{
				return (SwitchParameter)(base.Fields["LawfulInterception"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["LawfulInterception"] = value;
			}
		}

		// Token: 0x17001BDD RID: 7133
		// (get) Token: 0x06005CE2 RID: 23778 RVA: 0x001875EA File Offset: 0x001857EA
		protected override ObjectId RootId
		{
			get
			{
				if (this.Identity != null)
				{
					return null;
				}
				return RuleIdParameter.GetRuleCollectionId(base.DataSession, "JournalingVersioned");
			}
		}

		// Token: 0x06005CE3 RID: 23779 RVA: 0x00187606 File Offset: 0x00185806
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new GetTaskBaseModuleFactory();
		}

		// Token: 0x06005CE4 RID: 23780 RVA: 0x00187610 File Offset: 0x00185810
		protected override void InternalValidate()
		{
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = RuleIdParameter.GetRuleCollectionRdn("JournalingVersioned");
			}
			if (base.Fields.IsModified("LawfulInterception") && base.Fields.IsModified("Organization"))
			{
				base.WriteError(new InvalidOperationException(Strings.JournalingParameterErrorGccWithOrganization), ErrorCategory.InvalidOperation, null);
				return;
			}
			base.InternalValidate();
		}

		// Token: 0x06005CE5 RID: 23781 RVA: 0x0018767C File Offset: 0x0018587C
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			foreach (TransportRule transportRule in ((IEnumerable<TransportRule>)dataObjects))
			{
				this.WriteRuleObject(transportRule);
			}
		}

		// Token: 0x06005CE6 RID: 23782 RVA: 0x001876CC File Offset: 0x001858CC
		private void WriteRuleObject(TransportRule transportRule)
		{
			JournalingRule journalingRule = null;
			ParserException ex = null;
			try
			{
				journalingRule = (JournalingRule)JournalingRuleParser.Instance.GetRule(transportRule.Xml);
			}
			catch (ParserException ex2)
			{
				ex = ex2;
			}
			Exception ex3 = null;
			if (journalingRule != null && journalingRule.GccRuleType != GccType.None && !this.LawfulInterception)
			{
				return;
			}
			if (journalingRule != null && journalingRule.GccRuleType == GccType.None && this.LawfulInterception)
			{
				return;
			}
			JournalRuleObject journalRuleObject;
			if (journalingRule == null)
			{
				journalRuleObject = JournalRuleObject.CreateCorruptJournalRuleObject(transportRule, Strings.CorruptRule(transportRule.Name, ex.Message));
			}
			else if (journalingRule.IsTooAdvancedToParse)
			{
				journalRuleObject = JournalRuleObject.CreateCorruptJournalRuleObject(transportRule, Strings.CannotParseRuleDueToVersion(transportRule.Name));
			}
			else
			{
				journalRuleObject = new JournalRuleObject();
				try
				{
					journalRuleObject.Deserialize(journalingRule);
				}
				catch (RecipientInvalidException ex4)
				{
					ex3 = ex4;
				}
				catch (JournalRuleCorruptException ex5)
				{
					ex3 = ex5;
				}
			}
			if (ex3 != null)
			{
				journalRuleObject = JournalRuleObject.CreateCorruptJournalRuleObject(transportRule, Strings.CorruptRule(transportRule.Name, ex3.Message));
			}
			journalRuleObject.SetTransportRule(transportRule);
			this.WriteResult(journalRuleObject);
		}
	}
}
