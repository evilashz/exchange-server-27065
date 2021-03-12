using System;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002C4 RID: 708
	[Cmdlet("Initialize", "ExchangeLocalGroups", SupportsShouldProcess = true)]
	public sealed class InitializeExchangeLocalGroups : SetupTaskBase
	{
		// Token: 0x060018E3 RID: 6371 RVA: 0x0006D5B5 File Offset: 0x0006B7B5
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (!Datacenter.IsMicrosoftHostedOnly(true))
			{
				base.ThrowTerminatingError(new DatacenterEnvironmentOnlyOperationException(), ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0006D5DC File Offset: 0x0006B7DC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ADContainer container = this.FindRootUsersContainer(this.domainConfigurationSession, this.rootDomain.Id);
			string text = string.Format("{0}$$$", this.rootDomain.Name);
			ADGroup adgroup = this.FindADGroup(this.domainConfigurationSession, container, text);
			if (adgroup != null)
			{
				base.WriteVerbose(Strings.InfoGroupAlreadyPresent(text));
			}
			else
			{
				LocalizedString exchangeMigrationSidHistoryAuditingDSGDescription = Strings.ExchangeMigrationSidHistoryAuditingDSGDescription;
				this.CreateDomainLocalSecurityGroup(container, text, exchangeMigrationSidHistoryAuditingDSGDescription);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0006D654 File Offset: 0x0006B854
		private ADContainer FindRootUsersContainer(IConfigurationSession session, ADObjectId domain)
		{
			ADContainer[] array = session.Find<ADContainer>(domain, QueryScope.OneLevel, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, "Users"), null, 1);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0006D68C File Offset: 0x0006B88C
		private ADGroup FindADGroup(IConfigurationSession session, ADContainer container, string groupName)
		{
			ADGroup[] array = session.Find<ADGroup>(container.Id, QueryScope.OneLevel, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, groupName), null, 1);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0006D6C4 File Offset: 0x0006B8C4
		private ADGroup CreateDomainLocalSecurityGroup(ADContainer container, string groupName, LocalizedString groupDescription)
		{
			GroupTypeFlags groupType = GroupTypeFlags.DomainLocal | GroupTypeFlags.SecurityEnabled;
			return this.CreateGroup(this.rootDomainRecipientSession, container.Id, groupName, groupDescription, groupType);
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x0006D6F0 File Offset: 0x0006B8F0
		private ADGroup CreateGroup(IRecipientSession session, ADObjectId containerId, string groupName, LocalizedString groupDescription, GroupTypeFlags groupType)
		{
			ADGroup adgroup = new ADGroup(session, groupName, containerId, groupType);
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			multiValuedProperty.Add(groupDescription);
			adgroup[ADRecipientSchema.Description] = multiValuedProperty;
			adgroup.SamAccountName = groupName;
			SetupTaskBase.Save(adgroup, session);
			base.WriteVerbose(Strings.InfoCreatedGroup(adgroup.DistinguishedName));
			return adgroup;
		}

		// Token: 0x04000AE2 RID: 2786
		private const string rootUsersContainerCommonName = "Users";

		// Token: 0x04000AE3 RID: 2787
		private const GroupTypeFlags DSG_GROUPTYPE_FLAGS = GroupTypeFlags.DomainLocal | GroupTypeFlags.SecurityEnabled;
	}
}
