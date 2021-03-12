using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007D6 RID: 2006
	[Cmdlet("Get", "GlobalAddressList", DefaultParameterSetName = "Identity")]
	public sealed class GetGlobalAddressList : GetMultitenancySystemConfigurationObjectTask<GlobalAddressListIdParameter, AddressBookBase>
	{
		// Token: 0x17001534 RID: 5428
		// (get) Token: 0x06004630 RID: 17968 RVA: 0x001203C3 File Offset: 0x0011E5C3
		// (set) Token: 0x06004631 RID: 17969 RVA: 0x001203E9 File Offset: 0x0011E5E9
		[Parameter(Mandatory = true, ParameterSetName = "DefaultOnly")]
		public SwitchParameter DefaultOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["DefaultOnly"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DefaultOnly"] = value;
			}
		}

		// Token: 0x17001535 RID: 5429
		// (get) Token: 0x06004632 RID: 17970 RVA: 0x00120401 File Offset: 0x0011E601
		protected override ObjectId RootId
		{
			get
			{
				if (this.Identity == null)
				{
					return GlobalAddressListIdParameter.GetRootContainerId((IConfigurationSession)base.DataSession, base.CurrentOrganizationId);
				}
				return null;
			}
		}

		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x06004633 RID: 17971 RVA: 0x00120424 File Offset: 0x0011E624
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (this.DefaultOnly.IsPresent)
				{
					return new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Id, base.CurrentOrgContainerId.GetDescendantId(GlobalAddressList.RdnGalContainerToOrganization)),
						new ComparisonFilter(ComparisonOperator.Equal, AddressBookBaseSchema.IsDefaultGlobalAddressList, true)
					});
				}
				if (this.Identity == null)
				{
					return new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Id, base.CurrentOrgContainerId.GetDescendantId(GlobalAddressList.RdnGalContainerToOrganization));
				}
				return null;
			}
		}

		// Token: 0x17001537 RID: 5431
		// (get) Token: 0x06004634 RID: 17972 RVA: 0x001204A6 File Offset: 0x0011E6A6
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x001204A9 File Offset: 0x0011E6A9
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || DataAccessHelper.IsDataAccessKnownException(exception) || typeof(DataSourceOperationException).IsInstanceOfType(exception);
		}

		// Token: 0x06004636 RID: 17974 RVA: 0x001204D0 File Offset: 0x0011E6D0
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			base.WriteResult(new GlobalAddressList((AddressBookBase)dataObject));
			TaskLogger.LogExit();
		}
	}
}
