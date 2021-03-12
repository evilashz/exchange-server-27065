using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200010C RID: 268
	public abstract class AdObjectResolver
	{
		// Token: 0x17001A1C RID: 6684
		// (get) Token: 0x06001FA1 RID: 8097 RVA: 0x0005F34A File Offset: 0x0005D54A
		internal ADSessionSettings TenantSessionSetting
		{
			get
			{
				return ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), RbacPrincipal.Current.RbacConfiguration.OrganizationId, null, false);
			}
		}

		// Token: 0x17001A1D RID: 6685
		// (get) Token: 0x06001FA2 RID: 8098 RVA: 0x0005F368 File Offset: 0x0005D568
		internal ADSessionSettings TenantSharedConfigurationSessionSetting
		{
			get
			{
				LocalSession localSession = LocalSession.Current;
				if (localSession.IsDehydrated)
				{
					SharedConfiguration sharedConfiguration = SharedConfiguration.GetSharedConfiguration(localSession.RbacConfiguration.OrganizationId);
					if (sharedConfiguration != null)
					{
						return sharedConfiguration.GetSharedConfigurationSessionSettings();
					}
				}
				return null;
			}
		}

		// Token: 0x06001FA3 RID: 8099
		internal abstract IDirectorySession CreateAdSession();

		// Token: 0x06001FA4 RID: 8100 RVA: 0x0005F3B8 File Offset: 0x0005D5B8
		protected IEnumerable<T> ResolveObjects<T>(IEnumerable<ADObjectId> identities, IEnumerable<PropertyDefinition> propertiesToRead, Func<ADRawEntry, T> factory)
		{
			if (identities == null)
			{
				throw new FaultException(new ArgumentNullException("identities").Message);
			}
			return from entry in this.FindObjects(identities.ToArray<ADObjectId>(), propertiesToRead)
			select factory(entry);
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x0005F798 File Offset: 0x0005D998
		private IEnumerable<ADRawEntry> FindObjects(ADObjectId[] identities, IEnumerable<PropertyDefinition> propertiesToRead)
		{
			if (ADSessionSettings.GetThreadServerSettings() == null)
			{
				EcpRunspaceFactory ecpRunspaceFactory = new EcpRunspaceFactory(new InitialSessionStateSectionFactory());
				ADSessionSettings.SetThreadADContext(new ADDriverContext(ecpRunspaceFactory.CreateRunspaceServerSettings(), ContextMode.Cmdlet));
			}
			IDirectorySession session = this.CreateAdSession();
			List<QueryFilter> filters = new List<QueryFilter>(this.properMaxCustomFilterTreeSize);
			int filterLenRemain = 31197;
			int i = 0;
			while (i < identities.Length)
			{
				QueryFilter idFilter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, identities[i]);
				filters.Add(idFilter);
				string ldapIdFilter = LdapFilterBuilder.LdapFilterFromQueryFilter(idFilter);
				if (ldapIdFilter.Length > filterLenRemain || filters.Count == this.properMaxCustomFilterTreeSize || i == identities.Length - 1)
				{
					ADPagedReader<ADRawEntry> entries = null;
					try
					{
						entries = session.FindPagedADRawEntry(null, QueryScope.SubTree, new OrFilter(filters.ToArray()), null, this.properMaxCustomFilterTreeSize, propertiesToRead);
					}
					catch (ADFilterException)
					{
						if (this.isFirstError)
						{
							i -= filters.Count;
							this.properMaxCustomFilterTreeSize /= 2;
							filters.Clear();
							filterLenRemain = 31197;
							this.isFirstError = false;
							goto IL_22E;
						}
						throw;
					}
					foreach (ADRawEntry entry in entries)
					{
						yield return entry;
					}
					filters.Clear();
					filterLenRemain = 31197;
					goto IL_216;
				}
				goto IL_216;
				IL_22E:
				i++;
				continue;
				IL_216:
				filterLenRemain -= ldapIdFilter.Length;
				goto IL_22E;
			}
			yield break;
		}

		// Token: 0x04001C80 RID: 7296
		private const int MaxLdapFilterSize = 31197;

		// Token: 0x04001C81 RID: 7297
		private int properMaxCustomFilterTreeSize = Util.IsDataCenter ? (LdapFilterBuilder.MaxCustomFilterTreeSize / 2) : LdapFilterBuilder.MaxCustomFilterTreeSize;

		// Token: 0x04001C82 RID: 7298
		private bool isFirstError = true;
	}
}
