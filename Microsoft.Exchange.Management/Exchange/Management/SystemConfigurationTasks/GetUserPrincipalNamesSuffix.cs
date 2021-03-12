using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009BA RID: 2490
	[Cmdlet("Get", "UserPrincipalNamesSuffix", DefaultParameterSetName = "UPNSuffix")]
	public sealed class GetUserPrincipalNamesSuffix : GetMultitenancySystemConfigurationObjectTask<ExtendedOrganizationalUnitIdParameter, ExtendedOrganizationalUnit>
	{
		// Token: 0x17001A79 RID: 6777
		// (get) Token: 0x060058B9 RID: 22713 RVA: 0x00172394 File Offset: 0x00170594
		// (set) Token: 0x060058BA RID: 22714 RVA: 0x001723AB File Offset: 0x001705AB
		[Parameter]
		public ExtendedOrganizationalUnitIdParameter OrganizationalUnit
		{
			get
			{
				return (ExtendedOrganizationalUnitIdParameter)base.Fields["OrganizationalUnit"];
			}
			set
			{
				base.Fields["OrganizationalUnit"] = value;
			}
		}

		// Token: 0x060058BB RID: 22715 RVA: 0x001723C0 File Offset: 0x001705C0
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 52, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\ExchangeServer\\GetUserPrincipalNamesSuffix.cs");
			configurationSession.UseConfigNC = false;
			configurationSession.UseGlobalCatalog = true;
			configurationSession.EnforceDefaultScope = false;
			return configurationSession;
		}

		// Token: 0x060058BC RID: 22716 RVA: 0x00172410 File Offset: 0x00170610
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ADForest localForest = ADForest.GetLocalForest();
			this.rootDomain = localForest.FindRootDomain();
			this.topLevelDomains = localForest.FindTopLevelDomains();
			List<string> list = new List<string>();
			ReadOnlyCollection<ADDomain> readOnlyCollection = localForest.FindDomains();
			foreach (ADDomain addomain in readOnlyCollection)
			{
				if (!list.Contains(addomain.Fqdn))
				{
					list.Add(addomain.Fqdn);
				}
			}
			IConfigurationSession configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 85, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\ExchangeServer\\GetUserPrincipalNamesSuffix.cs");
			configurationSession.UseGlobalCatalog = true;
			configurationSession.EnforceDefaultScope = false;
			ADCrossRefContainer[] array = configurationSession.Find<ADCrossRefContainer>(null, QueryScope.SubTree, null, null, 0);
			if (array != null && array.Length > 0)
			{
				foreach (string item in array[0].UPNSuffixes)
				{
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			IConfigurationSession configurationSession2 = (IConfigurationSession)this.CreateSession();
			if (this.OrganizationalUnit != null)
			{
				ExtendedOrganizationalUnit extendedOrganizationalUnit = null;
				IEnumerable<ExtendedOrganizationalUnit> objects = this.OrganizationalUnit.GetObjects<ExtendedOrganizationalUnit>(null, configurationSession2);
				using (IEnumerator<ExtendedOrganizationalUnit> enumerator3 = objects.GetEnumerator())
				{
					if (enumerator3.MoveNext())
					{
						extendedOrganizationalUnit = enumerator3.Current;
						if (enumerator3.MoveNext())
						{
							base.WriteError(new ManagementObjectAmbiguousException(Strings.SpecifiedOUNotUnique), ErrorCategory.InvalidArgument, extendedOrganizationalUnit);
						}
					}
					else
					{
						base.WriteError(new ManagementObjectNotFoundException(Strings.SpecifiedOUNotFound), ErrorCategory.InvalidArgument, extendedOrganizationalUnit);
					}
				}
				configurationSession2.UseConfigNC = false;
				configurationSession2.UseGlobalCatalog = false;
				extendedOrganizationalUnit = configurationSession2.Read<ExtendedOrganizationalUnit>(extendedOrganizationalUnit.Id);
				if (extendedOrganizationalUnit != null)
				{
					foreach (string item2 in extendedOrganizationalUnit.UPNSuffixes)
					{
						if (!list.Contains(item2))
						{
							list.Add(item2);
						}
					}
				}
				ADObjectId adobjectId = extendedOrganizationalUnit.Id.DomainId;
				bool flag = false;
				while (!flag)
				{
					string text = adobjectId.ToCanonicalName();
					if (this.IsTopLevelDomain(text))
					{
						flag = true;
					}
					if (!string.Equals(text, this.rootDomain.Fqdn, StringComparison.InvariantCultureIgnoreCase) && !list.Contains(text))
					{
						list.Add(text);
					}
					adobjectId = adobjectId.Parent;
				}
			}
			foreach (string sendToPipeline in list)
			{
				base.WriteObject(sendToPipeline);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060058BD RID: 22717 RVA: 0x001726E4 File Offset: 0x001708E4
		private bool IsTopLevelDomain(string domainFqdn)
		{
			bool result = false;
			foreach (object obj in this.topLevelDomains)
			{
				ADDomain addomain = (ADDomain)obj;
				if (string.Equals(domainFqdn, addomain.Fqdn, StringComparison.InvariantCultureIgnoreCase))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060058BE RID: 22718 RVA: 0x0017274C File Offset: 0x0017094C
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || DataAccessHelper.IsDataAccessKnownException(exception);
		}

		// Token: 0x040032D8 RID: 13016
		private ICollection topLevelDomains;

		// Token: 0x040032D9 RID: 13017
		private ADDomain rootDomain;
	}
}
