using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x0200078E RID: 1934
	[Serializable]
	internal sealed class RecipientResourceCountQuota : ProvisioningResourceCountQuota
	{
		// Token: 0x06006077 RID: 24695 RVA: 0x00147D38 File Offset: 0x00145F38
		public RecipientResourceCountQuota(ADPropertyDefinition countQuotaProperty, string systemAddressListName, Type[] targetObjectTypes) : base(countQuotaProperty, systemAddressListName, targetObjectTypes)
		{
		}

		// Token: 0x06006078 RID: 24696 RVA: 0x00147D43 File Offset: 0x00145F43
		public RecipientResourceCountQuota(ADPropertyDefinition countQuotaProperty, string systemAddressListName, Type[] targetObjectTypes, RecipientTypeDetails[] targetObjectRecipientTypeDetails) : this(countQuotaProperty, systemAddressListName, targetObjectTypes)
		{
			if (targetObjectRecipientTypeDetails != null)
			{
				this.targetRecipientTypeDetails = new ReadOnlyCollection<RecipientTypeDetails>(targetObjectRecipientTypeDetails);
			}
		}

		// Token: 0x06006079 RID: 24697 RVA: 0x00147D60 File Offset: 0x00145F60
		public override bool IsApplicable(IConfigurable readOnlyPresentationObject)
		{
			if (base.Context != null && base.Context.UserSpecifiedParameters != null && base.Context.UserSpecifiedParameters.Contains("OverrideRecipientQuotas") && base.Context.UserSpecifiedParameters["OverrideRecipientQuotas"] != null && base.Context.UserSpecifiedParameters["OverrideRecipientQuotas"] is SwitchParameter && (SwitchParameter)base.Context.UserSpecifiedParameters["OverrideRecipientQuotas"])
			{
				return false;
			}
			ADObject adobject = readOnlyPresentationObject as ADObject;
			if (base.IsApplicable(readOnlyPresentationObject))
			{
				return this.IsApplicableRecipientTypeDetails(adobject);
			}
			if (adobject != null && adobject.IsChanged(ADRecipientSchema.RecipientType))
			{
				RecipientType recipientType = (RecipientType)adobject[ADRecipientSchema.RecipientType];
				if (recipientType == RecipientType.DynamicDistributionGroup || recipientType == RecipientType.MailContact || recipientType == RecipientType.MailNonUniversalGroup || recipientType == RecipientType.MailUniversalDistributionGroup || recipientType == RecipientType.MailUniversalSecurityGroup || recipientType == RecipientType.MailUser || recipientType == RecipientType.UserMailbox || recipientType == RecipientType.PublicFolder)
				{
					return this.IsApplicableRecipientTypeDetails(adobject);
				}
			}
			return false;
		}

		// Token: 0x0600607A RID: 24698 RVA: 0x00147E54 File Offset: 0x00146054
		public override ProvisioningValidationError[] Validate(ADProvisioningPolicy enforcementPolicy, IConfigurable readOnlyPresentationObject)
		{
			base.Validate(enforcementPolicy, readOnlyPresentationObject);
			ADObject adobject;
			if (readOnlyPresentationObject is ADPublicFolder)
			{
				adobject = (ADPublicFolder)readOnlyPresentationObject;
			}
			else
			{
				adobject = (MailEnabledRecipient)readOnlyPresentationObject;
			}
			Unlimited<int> fromValue = (Unlimited<int>)enforcementPolicy[base.CountQuotaProperty];
			if (!fromValue.IsUnlimited && (T)fromValue >= 0)
			{
				int num = (T)fromValue;
				bool flag;
				if (num == 0)
				{
					flag = true;
				}
				else
				{
					string domainController = null;
					if (base.Context != null && base.Context.UserSpecifiedParameters != null && base.Context.UserSpecifiedParameters.Contains("DomainController"))
					{
						object obj = base.Context.UserSpecifiedParameters["DomainController"];
						if (obj != null)
						{
							domainController = obj.ToString();
						}
					}
					ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), adobject.OrganizationId, null, false);
					IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(domainController, true, ConsistencyMode.FullyConsistent, sessionSettings, 178, "Validate", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Provisioning\\RecipientResourceCountQuota.cs");
					flag = SystemAddressListMemberCount.IsQuotaExceded(tenantOrTopologyConfigurationSession, adobject.OrganizationId, base.SystemAddressListName, num);
				}
				if (flag)
				{
					string policyId = string.Format("{0}: {1}", enforcementPolicy.Identity.ToString(), base.CountQuotaProperty.Name);
					LocalizedString description;
					if (adobject.OrganizationalUnitRoot == null)
					{
						description = DirectoryStrings.ErrorExceededHosterResourceCountQuota(policyId, (readOnlyPresentationObject.GetType() == typeof(ADPublicFolder)) ? "MailPublicFolder" : ProvisioningHelper.GetProvisioningObjectTag(readOnlyPresentationObject.GetType()), num);
					}
					else
					{
						description = DirectoryStrings.ErrorExceededMultiTenantResourceCountQuota(policyId, (readOnlyPresentationObject.GetType() == typeof(ADPublicFolder)) ? "MailPublicFolder" : ProvisioningHelper.GetProvisioningObjectTag(readOnlyPresentationObject.GetType()), adobject.OrganizationalUnitRoot.Name, num);
					}
					return new ProvisioningValidationError[]
					{
						new ProvisioningValidationError(description, ExchangeErrorCategory.ServerOperation, null)
					};
				}
			}
			return null;
		}

		// Token: 0x0600607B RID: 24699 RVA: 0x0014801C File Offset: 0x0014621C
		private bool IsApplicableRecipientTypeDetails(ADObject recipient)
		{
			return this.targetRecipientTypeDetails == null || this.targetRecipientTypeDetails.Count == 0 || (recipient != null && this.targetRecipientTypeDetails.Contains((RecipientTypeDetails)recipient[ADRecipientSchema.RecipientTypeDetails]));
		}

		// Token: 0x040040CF RID: 16591
		private ReadOnlyCollection<RecipientTypeDetails> targetRecipientTypeDetails;
	}
}
