using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000070 RID: 112
	[Cmdlet("Get", "ActiveSyncDeviceClass", DefaultParameterSetName = "Identity")]
	public sealed class GetActiveSyncDeviceClass : GetMultitenancySystemConfigurationObjectTask<ActiveSyncDeviceClassIdParameter, ActiveSyncDeviceClass>
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000E7B5 File Offset: 0x0000C9B5
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0000E7CC File Offset: 0x0000C9CC
		[Parameter]
		public string SortBy
		{
			get
			{
				return (string)base.Fields["SortBy"];
			}
			set
			{
				base.Fields["SortBy"] = (string.IsNullOrEmpty(value) ? null : value);
				this.internalSortBy = QueryHelper.GetSortBy(this.SortBy, GetActiveSyncDeviceClass.SortPropertiesArray);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000E800 File Offset: 0x0000CA00
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0000E818 File Offset: 0x0000CA18
		[Parameter]
		[ValidateNotNullOrEmpty]
		public string Filter
		{
			get
			{
				return (string)base.Fields["Filter"];
			}
			set
			{
				MonadFilter monadFilter = new MonadFilter(value, this, GetActiveSyncDeviceClass.FilterableObjectSchema);
				this.inputFilter = monadFilter.InnerFilter;
				base.OptionalIdentityData.AdditionalFilter = monadFilter.InnerFilter;
				base.Fields["Filter"] = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000E860 File Offset: 0x0000CA60
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000E863 File Offset: 0x0000CA63
		protected override QueryFilter InternalFilter
		{
			get
			{
				return this.ConstructQueryFilterWithCustomFilter(this.inputFilter);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000E871 File Offset: 0x0000CA71
		protected override SortBy InternalSortBy
		{
			get
			{
				return this.internalSortBy;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000E87C File Offset: 0x0000CA7C
		private QueryFilter ConstructQueryFilterWithCustomFilter(QueryFilter customFilter)
		{
			QueryFilter internalFilter = base.InternalFilter;
			if (internalFilter == null)
			{
				return customFilter;
			}
			if (customFilter == null)
			{
				return internalFilter;
			}
			return new AndFilter(new QueryFilter[]
			{
				internalFilter,
				customFilter
			});
		}

		// Token: 0x04000200 RID: 512
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ActiveSyncDeviceClassSchema.DeviceType,
			ActiveSyncDeviceClassSchema.DeviceModel,
			ActiveSyncDeviceClassSchema.LastUpdateTime
		};

		// Token: 0x04000201 RID: 513
		private static readonly ActiveSyncDeviceClassSchema FilterableObjectSchema = ObjectSchema.GetInstance<ActiveSyncDeviceClassSchema>();

		// Token: 0x04000202 RID: 514
		private SortBy internalSortBy;

		// Token: 0x04000203 RID: 515
		private QueryFilter inputFilter;
	}
}
