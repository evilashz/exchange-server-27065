using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000066 RID: 102
	[Cmdlet("enable", "SystemAttendantMailbox", DefaultParameterSetName = "Identity")]
	public sealed class EnableSystemAttendantMailbox : RecipientObjectActionTask<SystemAttendantIdParameter, ADSystemAttendantMailbox>
	{
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001C9B2 File Offset: 0x0001ABB2
		protected override ObjectId RootId
		{
			get
			{
				return this.ConfigurationSession.GetOrgContainerId();
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001C9C0 File Offset: 0x0001ABC0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			ADSystemAttendantMailbox dataObject = this.DataObject;
			if (string.IsNullOrEmpty(dataObject.DisplayName))
			{
				dataObject.DisplayName = dataObject.Name;
			}
			((IRecipientSession)base.DataSession).UseConfigNC = true;
			if (string.IsNullOrEmpty(this.DataObject.LegacyExchangeDN))
			{
				string parentLegacyDN = string.Format(CultureInfo.InvariantCulture, "/o={0}/ou={1}/cn=Recipients", new object[]
				{
					this.ConfigurationSession.GetOrgContainerId().Name,
					((ITopologyConfigurationSession)this.ConfigurationSession).GetAdministrativeGroupId().Name
				});
				this.DataObject.LegacyExchangeDN = LegacyDN.GenerateLegacyDN(parentLegacyDN, this.DataObject, true, null);
			}
			if (base.IsProvisioningLayerAvailable)
			{
				ProvisioningLayer.UpdateAffectedIConfigurable(this, this.ConvertDataObjectToPresentationObject(dataObject), false);
			}
			else
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorNoProvisioningHandlerAvailable), ErrorCategory.InvalidOperation, null);
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001CABD File Offset: 0x0001ACBD
		protected override void ProvisioningUpdateConfigurationObject()
		{
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001CAC0 File Offset: 0x0001ACC0
		private bool LegacyDNIsUnique(string legacyDN)
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.LegacyExchangeDN, legacyDN),
				new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Id, this.DataObject.Id)
			});
			base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.TenantGlobalCatalogSession, typeof(ADRecipient), filter, null, true));
			ADRecipient[] array = base.TenantGlobalCatalogSession.Find(null, QueryScope.SubTree, filter, null, 1);
			return 0 == array.Length;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001CB3C File Offset: 0x0001AD3C
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return RecipientTaskHelper.ConvertRecipientToPresentationObject((ADRecipient)dataObject);
		}
	}
}
