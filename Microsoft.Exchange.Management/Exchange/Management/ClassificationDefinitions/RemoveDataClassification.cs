using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200083D RID: 2109
	[Cmdlet("Remove", "DataClassification", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveDataClassification : RemoveSystemConfigurationObjectTask<DataClassificationIdParameter, TransportRule>
	{
		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x0600493B RID: 18747 RVA: 0x0012D207 File Offset: 0x0012B407
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveDataClassification(this.Identity.ToString());
			}
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x0012D21C File Offset: 0x0012B41C
		protected override IConfigurable ResolveDataObject()
		{
			TaskLogger.LogEnter();
			this.implementation = new DataClassificationCmdletsImplementation(this);
			TransportRule transportRule = this.implementation.Initialize(base.DataSession, this.Identity, base.OptionalIdentityData);
			if (TaskHelper.ShouldUnderscopeDataSessionToOrganization((IDirectorySession)base.DataSession, transportRule))
			{
				base.UnderscopeDataSession(transportRule.OrganizationId);
				base.CurrentOrganizationId = transportRule.OrganizationId;
			}
			TaskLogger.LogExit();
			return transportRule;
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x0012D294 File Offset: 0x0012B494
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = ClassificationDefinitionConstants.ClassificationDefinitionsRdn;
			}
			base.InternalValidate();
			string name = ((DataClassificationObjectId)this.implementation.DataClassificationPresentationObject.Identity).Name;
			ILookup<string, Rule> dataClassificationsInUse = DlpUtils.GetDataClassificationsInUse(base.DataSession, new string[]
			{
				name
			}, ClassificationDefinitionConstants.RuleIdComparer);
			if (dataClassificationsInUse.Contains(name))
			{
				List<string> list = (from transportRule in dataClassificationsInUse[name]
				select transportRule.Name).ToList<string>();
				if (list.Count > 0)
				{
					base.WriteError(new DataClassificationInUseException(this.implementation.DataClassificationPresentationObject.Name, string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, list)), ErrorCategory.InvalidOperation, null);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600493E RID: 18750 RVA: 0x0012D370 File Offset: 0x0012B570
		protected override void InternalProcessRecord()
		{
			ValidationContext validationContext = new ValidationContext(ClassificationRuleCollectionOperationType.Update, base.CurrentOrganizationId, false, true, (IConfigurationSession)base.DataSession, base.DataObject, null, null);
			if (this.implementation.Delete(validationContext))
			{
				base.InternalProcessRecord();
				return;
			}
			base.DataSession.Save(base.DataObject);
		}

		// Token: 0x04002C3C RID: 11324
		private DataClassificationCmdletsImplementation implementation;
	}
}
