using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.Classification;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000134 RID: 308
	[OutputType(new Type[]
	{
		typeof(PsDlpSensitiveInformationType)
	})]
	[Cmdlet("Get", "DlpSensitiveInformationType")]
	public sealed class GetDlpSensitiveInformationType : Task
	{
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00030B30 File Offset: 0x0002ED30
		// (set) Token: 0x06000D8A RID: 3466 RVA: 0x00030B38 File Offset: 0x0002ED38
		[Parameter(Mandatory = false, Position = 0)]
		public string Identity { get; set; }

		// Token: 0x06000D8B RID: 3467 RVA: 0x00030B41 File Offset: 0x0002ED41
		protected override void InternalValidate()
		{
			base.InternalValidate();
			Utils.ThrowIfNotRunInEOP();
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00030B50 File Offset: 0x0002ED50
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			IClassificationRuleStore instance = InMemoryClassificationRuleStore.GetInstance();
			string locale = CultureInfo.CurrentCulture.ToString().ToLower();
			if (this.Identity != null)
			{
				RuleDefinitionDetails ruleDefinitionDetails = GetDlpSensitiveInformationType.GetRuleDefinitionDetails(instance, this.Identity, locale);
				base.WriteObject(new PsDlpSensitiveInformationType(ruleDefinitionDetails));
				return;
			}
			RULE_PACKAGE_DETAILS[] rulePackageDetails = instance.GetRulePackageDetails(null);
			foreach (RULE_PACKAGE_DETAILS rule_PACKAGE_DETAILS in rulePackageDetails)
			{
				foreach (string identity in rule_PACKAGE_DETAILS.RuleIDs)
				{
					RuleDefinitionDetails ruleDefinitionDetails2 = GetDlpSensitiveInformationType.GetRuleDefinitionDetails(instance, identity, locale);
					base.WriteObject(new PsDlpSensitiveInformationType(ruleDefinitionDetails2));
				}
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00030C1C File Offset: 0x0002EE1C
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || Utils.KnownExceptions.Any((Type exceptionType) => exceptionType.IsInstanceOfType(exception));
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00030C5C File Offset: 0x0002EE5C
		internal static RuleDefinitionDetails GetRuleDefinitionDetails(IClassificationRuleStore ruleStore, string identity, string locale)
		{
			RuleDefinitionDetails ruleDetails;
			try
			{
				ruleDetails = ruleStore.GetRuleDetails(identity, locale);
				if (ruleDetails == null || ruleDetails.LocalizableDetails == null || !ruleDetails.LocalizableDetails.Any<KeyValuePair<string, CLASSIFICATION_DEFINITION_DETAILS>>())
				{
					ruleDetails = ruleStore.GetRuleDetails(identity, "en-us");
				}
			}
			catch (ClassificationRuleStoreExceptionBase innerException)
			{
				throw new SensitiveInformationNotFoundException(identity, innerException);
			}
			return ruleDetails;
		}
	}
}
