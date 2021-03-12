using System;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Exchange.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000A1A RID: 2586
	internal class RuleStorageSchema : UnifiedPolicyStorageBaseSchema
	{
		// Token: 0x04004C8B RID: 19595
		public static ADPropertyDefinition EnforcementMode = new ADPropertyDefinition("Mode", ExchangeObjectVersion.Exchange2012, typeof(Mode), "msExchIMAP4Settings", ADPropertyDefinitionFlags.None, Mode.Enforce, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C8C RID: 19596
		public static ADPropertyDefinition Priority = new ADPropertyDefinition("Priority", ExchangeObjectVersion.Exchange2012, typeof(int), "msExchMobileSettings", ADPropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C8D RID: 19597
		public static ADPropertyDefinition RuleBlob = new ADPropertyDefinition("RuleBlob", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchConfigurationXML", ADPropertyDefinitionFlags.None, string.Empty, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 16384)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C8E RID: 19598
		public static ADPropertyDefinition ParentPolicyId = new ADPropertyDefinition("ParentPolicyId", ExchangeObjectVersion.Exchange2012, typeof(Guid), "msExchCanaryData1", ADPropertyDefinitionFlags.Binary, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C8F RID: 19599
		public static ADPropertyDefinition Comments = new ADPropertyDefinition("Comments", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchDefaultPublicFolderMailbox", ADPropertyDefinitionFlags.None, string.Empty, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C90 RID: 19600
		public static ADPropertyDefinition Description = new ADPropertyDefinition("Description", ExchangeObjectVersion.Exchange2012, typeof(string), "adminDescription", ADPropertyDefinitionFlags.None, string.Empty, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C91 RID: 19601
		public static readonly ADPropertyDefinition IsEnabled = new ADPropertyDefinition("IsEnabled", ExchangeObjectVersion.Exchange2012, typeof(bool), "msExchHideFromAddressLists", ADPropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C92 RID: 19602
		public static readonly ADPropertyDefinition CreatedBy = new ADPropertyDefinition("CreatedBy", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchShadowPostalCode", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C93 RID: 19603
		public static readonly ADPropertyDefinition LastModifiedBy = new ADPropertyDefinition("LastModifiedBy", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchShadowPhysicalDeliveryOfficeName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C94 RID: 19604
		public static ADPropertyDefinition Scenario = new ADPropertyDefinition("PolicyScenario", ExchangeObjectVersion.Exchange2012, typeof(PolicyScenario), "msExchPOP3Settings", ADPropertyDefinitionFlags.None, PolicyScenario.Retention, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C95 RID: 19605
		public static readonly ADPropertyDefinition ContentMatchQuery = new ADPropertyDefinition("ContentMatchQuery", ExchangeObjectVersion.Exchange2012, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 16384)
		}, null, null);

		// Token: 0x04004C96 RID: 19606
		public static readonly ADPropertyDefinition ContentDateFrom = new ADPropertyDefinition("ContentDateFrom", ExchangeObjectVersion.Exchange2012, typeof(DateTime?), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C97 RID: 19607
		public static readonly ADPropertyDefinition ContentDateTo = new ADPropertyDefinition("ContentDateTo", ExchangeObjectVersion.Exchange2012, typeof(DateTime?), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C98 RID: 19608
		public static readonly ADPropertyDefinition ContentPropertyContainsWords = new ADPropertyDefinition("ContentPropertyContainsWords", ExchangeObjectVersion.Exchange2012, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C99 RID: 19609
		public static readonly ADPropertyDefinition ContentContainsSensitiveInformation = new ADPropertyDefinition("ContentContainsSensitiveInformation", ExchangeObjectVersion.Exchange2012, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C9A RID: 19610
		public static readonly ADPropertyDefinition AccessScopeIs = new ADPropertyDefinition("AccessScopeIs", ExchangeObjectVersion.Exchange2012, typeof(AccessScope?), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C9B RID: 19611
		public static readonly ADPropertyDefinition HoldContent = new ADPropertyDefinition("HoldContent", ExchangeObjectVersion.Exchange2012, typeof(int?), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x04004C9C RID: 19612
		public static readonly ADPropertyDefinition HoldDurationDisplayHint = new ADPropertyDefinition("HoldDurationDisplayHint", ExchangeObjectVersion.Exchange2012, typeof(HoldDurationHint), null, ADPropertyDefinitionFlags.TaskPopulated, HoldDurationHint.Days, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C9D RID: 19613
		public static readonly ADPropertyDefinition BlockAccess = new ADPropertyDefinition("BlockAccess", ExchangeObjectVersion.Exchange2012, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
