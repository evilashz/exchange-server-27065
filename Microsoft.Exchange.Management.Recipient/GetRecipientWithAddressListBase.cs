using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000009 RID: 9
	public abstract class GetRecipientWithAddressListBase<TIdentity, TDataObject> : GetRecipientBase<TIdentity, TDataObject> where TIdentity : RecipientIdParameter, new() where TDataObject : ADObject, new()
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003CA8 File Offset: 0x00001EA8
		protected virtual string SystemAddressListRdn
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003CAC File Offset: 0x00001EAC
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled && !string.IsNullOrEmpty(this.SystemAddressListRdn) && this.ConfigurationSession.GetOrgContainer().IsAddressListPagingEnabled)
			{
				this.addressListId = base.CurrentOrgContainerId.GetDescendantId(SystemAddressList.RdnSystemAddressListContainerToOrganization).GetChildId(this.SystemAddressListRdn);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003D18 File Offset: 0x00001F18
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = base.InternalFilter;
				if (this.addressListId != null && this.Identity == null && base.AccountPartition == null && !base.SessionSettings.IncludeSoftDeletedObjects && !base.SessionSettings.IncludeInactiveMailbox && (base.ParameterSetName == "Identity" || base.ParameterSetName == "CookieSet"))
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.AddressListMembership, this.addressListId),
						new ExistsFilter(ADRecipientSchema.DisplayName),
						queryFilter
					});
					this.addressListUsed = true;
				}
				return queryFilter;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003DC8 File Offset: 0x00001FC8
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (this.addressListUsed && base.WriteObjectCount == 0U && this.ConfigurationSession.Read<AddressBookBase>(this.addressListId) == null)
			{
				this.WriteWarning(Strings.WarningSystemAddressListNotFound(this.addressListId.Name));
				this.addressListId = null;
				base.InternalProcessRecord();
			}
		}

		// Token: 0x0400000A RID: 10
		private ADObjectId addressListId;

		// Token: 0x0400000B RID: 11
		private bool addressListUsed;
	}
}
