using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000090 RID: 144
	[Cmdlet("New", "MicrosoftExchangeRecipient", SupportsShouldProcess = true)]
	public sealed class NewMicrosoftExchangeRecipient : NewRecipientObjectTaskBase<ADMicrosoftExchangeRecipient>
	{
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00029552 File Offset: 0x00027752
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMicrosoftExchangeRecipient;
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0002955C File Offset: 0x0002775C
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.CreateSession();
			recipientSession.UseConfigNC = true;
			return recipientSession;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00029580 File Offset: 0x00027780
		protected override void PrepareRecipientObject(ADMicrosoftExchangeRecipient dataObject)
		{
			this.DataObject = dataObject;
			dataObject.HiddenFromAddressListsEnabled = true;
			dataObject.EmailAddressPolicyEnabled = true;
			dataObject.Name = ADMicrosoftExchangeRecipient.DefaultName;
			dataObject.Alias = ADMicrosoftExchangeRecipient.DefaultName;
			dataObject.SetId(ADMicrosoftExchangeRecipient.GetDefaultId(this.ConfigurationSession));
			dataObject.ObjectCategory = base.PartitionConfigSession.SchemaNamingContext.GetChildId("ms-Exch-Exchange-Server-Recipient");
			if (string.IsNullOrEmpty(dataObject.LegacyExchangeDN))
			{
				string parentLegacyDN = string.Format(CultureInfo.InvariantCulture, "/o={0}/ou={1}/cn=Recipients", new object[]
				{
					base.RootOrgGlobalConfigSession.GetOrgContainerId().Name,
					base.RootOrgGlobalConfigSession.GetAdministrativeGroupId().Name
				});
				dataObject.LegacyExchangeDN = LegacyDN.GenerateLegacyDN(parentLegacyDN, dataObject, true, new LegacyDN.LegacyDNIsUnique(this.LegacyDNIsUnique));
			}
			AcceptedDomain defaultAcceptedDomain = this.ConfigurationSession.GetDefaultAcceptedDomain();
			if (defaultAcceptedDomain == null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorNoDefaultAcceptedDomainFound(base.CurrentOrganizationId.ToString())), ErrorCategory.InvalidOperation, null);
			}
			dataObject.EmailAddresses.Add(new SmtpProxyAddress(dataObject.Alias + "@" + defaultAcceptedDomain.DomainName.Domain.ToString(), true));
			dataObject.DeliverToMailboxAndForward = false;
			dataObject.OrganizationId = base.CurrentOrganizationId;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x000296C0 File Offset: 0x000278C0
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			this.existingObject = MailboxTaskHelper.FindMicrosoftExchangeRecipient((IRecipientSession)base.DataSession, this.ConfigurationSession);
			ADMicrosoftExchangeRecipient admicrosoftExchangeRecipient;
			if (this.existingObject != null)
			{
				this.WriteWarning(Strings.MicrosoftExchangeRecipientExists);
				admicrosoftExchangeRecipient = this.existingObject;
			}
			else
			{
				admicrosoftExchangeRecipient = (ADMicrosoftExchangeRecipient)base.PrepareDataObject();
			}
			admicrosoftExchangeRecipient.DisplayName = ADMicrosoftExchangeRecipient.DefaultDisplayName;
			if (!base.IsProvisioningLayerAvailable && this.DataObject.OrganizationId.OrganizationalUnit != null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorNoProvisioningHandlerAvailable), ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
			return admicrosoftExchangeRecipient;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00029758 File Offset: 0x00027958
		private bool LegacyDNIsUnique(string legacyDN)
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.LegacyExchangeDN, legacyDN),
				new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Id, this.DataObject.Id)
			});
			base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.RootOrgGlobalCatalogSession, typeof(ADRecipient), filter, null, true));
			ADRecipient[] array = base.RootOrgGlobalCatalogSession.Find(null, QueryScope.SubTree, filter, null, 1);
			bool flag = 0 == array.Length;
			if (VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.ServiceAccountForest.Enabled && base.CurrentOrganizationId != OrganizationId.ForestWideOrgId)
			{
				array = base.PartitionOrRootOrgGlobalCatalogSession.Find(null, QueryScope.SubTree, filter, null, 1);
				flag = (flag && 0 == array.Length);
			}
			return flag;
		}

		// Token: 0x040001F8 RID: 504
		private ADMicrosoftExchangeRecipient existingObject;
	}
}
