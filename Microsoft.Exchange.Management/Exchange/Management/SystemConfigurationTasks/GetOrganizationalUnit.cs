using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C5A RID: 3162
	[Cmdlet("Get", "OrganizationalUnit", DefaultParameterSetName = "Identity")]
	public sealed class GetOrganizationalUnit : GetMultitenancySystemConfigurationObjectTask<ExtendedOrganizationalUnitIdParameter, ExtendedOrganizationalUnit>
	{
		// Token: 0x1700250D RID: 9485
		// (get) Token: 0x060077EB RID: 30699 RVA: 0x001E8B3B File Offset: 0x001E6D3B
		// (set) Token: 0x060077EC RID: 30700 RVA: 0x001E8B61 File Offset: 0x001E6D61
		[Parameter(ParameterSetName = "Identity")]
		public SwitchParameter SingleNodeOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["SingleNodeOnly"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SingleNodeOnly"] = value;
			}
		}

		// Token: 0x1700250E RID: 9486
		// (get) Token: 0x060077ED RID: 30701 RVA: 0x001E8B79 File Offset: 0x001E6D79
		// (set) Token: 0x060077EE RID: 30702 RVA: 0x001E8B9F File Offset: 0x001E6D9F
		[Parameter]
		public SwitchParameter IncludeContainers
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeContainers"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IncludeContainers"] = value;
			}
		}

		// Token: 0x1700250F RID: 9487
		// (get) Token: 0x060077EF RID: 30703 RVA: 0x001E8BB7 File Offset: 0x001E6DB7
		// (set) Token: 0x060077F0 RID: 30704 RVA: 0x001E8BBF File Offset: 0x001E6DBF
		[Parameter]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return base.InternalResultSize;
			}
			set
			{
				base.InternalResultSize = value;
			}
		}

		// Token: 0x17002510 RID: 9488
		// (get) Token: 0x060077F1 RID: 30705 RVA: 0x001E8BC8 File Offset: 0x001E6DC8
		// (set) Token: 0x060077F2 RID: 30706 RVA: 0x001E8BD0 File Offset: 0x001E6DD0
		[Parameter(ParameterSetName = "SearchSet")]
		public string SearchText { get; set; }

		// Token: 0x17002511 RID: 9489
		// (get) Token: 0x060077F3 RID: 30707 RVA: 0x001E8BD9 File Offset: 0x001E6DD9
		protected override ObjectId RootId
		{
			get
			{
				if (!this.IsTenant)
				{
					return base.RootId;
				}
				return base.CurrentOrganizationId.OrganizationalUnit;
			}
		}

		// Token: 0x060077F4 RID: 30708 RVA: 0x001E8C18 File Offset: 0x001E6E18
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			IConfigurationSession configurationSession = base.DataSession as IConfigurationSession;
			if (this.Identity != null)
			{
				this.Identity.IncludeContainers = this.IncludeContainers;
				if (this.SingleNodeOnly)
				{
					IConfigurable dataObject = base.GetDataObject(this.Identity);
					base.WriteResult(dataObject);
					IEnumerable<ExtendedOrganizationalUnit> dataObjects = ExtendedOrganizationalUnit.FindFirstLevelChildOrganizationalUnit(this.IncludeContainers, configurationSession, dataObject.Identity as ADObjectId, this.InternalFilter, this.InternalSortBy, this.PageSize);
					base.WriteResult<ExtendedOrganizationalUnit>(dataObjects);
				}
				else
				{
					base.InternalProcessRecord();
				}
			}
			else
			{
				if (this.SingleNodeOnly)
				{
					if (this.IsTenant)
					{
						ExtendedOrganizationalUnit dataObject2 = configurationSession.Read<ExtendedOrganizationalUnit>((ADObjectId)this.RootId);
						this.WriteResult(dataObject2);
						goto IL_195;
					}
					ReadOnlyCollection<ADDomain> readOnlyCollection = ADForest.GetLocalForest(configurationSession.DomainController).FindTopLevelDomains();
					using (ReadOnlyCollection<ADDomain>.Enumerator enumerator = readOnlyCollection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ADDomain addomain = enumerator.Current;
							ExtendedOrganizationalUnit dataObject3 = configurationSession.Read<ExtendedOrganizationalUnit>(addomain.Id);
							this.WriteResult(dataObject3);
						}
						goto IL_195;
					}
				}
				IEnumerable<ExtendedOrganizationalUnit> enumerable = ExtendedOrganizationalUnit.FindSubTreeChildOrganizationalUnit(this.IncludeContainers, configurationSession, this.IsTenant ? ((ADObjectId)this.RootId) : null, this.InternalFilter);
				if (!string.IsNullOrEmpty(this.SearchText))
				{
					string nameToSearch = this.SearchText.ToUpper();
					enumerable = from ou in enumerable
					where ou.CanonicalName.ToUpper().Contains(nameToSearch)
					select ou;
				}
				this.WriteResult<ExtendedOrganizationalUnit>(enumerable);
			}
			IL_195:
			TaskLogger.LogExit();
		}

		// Token: 0x17002512 RID: 9490
		// (get) Token: 0x060077F5 RID: 30709 RVA: 0x001E8DD0 File Offset: 0x001E6FD0
		private bool IsTenant
		{
			get
			{
				return !base.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId);
			}
		}

		// Token: 0x060077F6 RID: 30710 RVA: 0x001E8DE8 File Offset: 0x001E6FE8
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession configurationSession;
			if (this.IsTenant)
			{
				configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, ConfigScopes.TenantSubTree, 182, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\GetOrganizationalUnit.cs");
			}
			else
			{
				configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 192, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\GetOrganizationalUnit.cs");
			}
			configurationSession.UseConfigNC = false;
			configurationSession.UseGlobalCatalog = true;
			configurationSession.EnforceDefaultScope = false;
			return configurationSession;
		}
	}
}
