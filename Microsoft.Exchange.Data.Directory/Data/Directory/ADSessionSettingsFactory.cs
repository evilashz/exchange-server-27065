using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000083 RID: 131
	internal class ADSessionSettingsFactory : ADSessionSettings.SessionSettingsFactory
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x00022BD8 File Offset: 0x00020DD8
		internal static void RunWithInactiveMailboxVisibilityEnablerForDatacenter(Action action)
		{
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				using (new ADSessionSettingsFactory.InactiveMailboxVisibilityEnabler())
				{
					action();
					return;
				}
			}
			action();
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00022C2C File Offset: 0x00020E2C
		internal override ADSessionSettings FromAllTenantsOrRootOrgAutoDetect(ADObjectId id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id.DomainId == null)
			{
				ExTraceGlobals.GetConnectionTracer.TraceDebug<string>(0L, "FromAllTenantsOrRootOrgAutoDetect(): Value '{0}' passed to id parameter doesn't have DomainId initialized, falling back to RootOrg scope set", id.ToString());
				return ADSessionSettings.FromRootOrgScopeSet();
			}
			PartitionId partitionId = id.GetPartitionId();
			if (!ADAccountPartitionLocator.IsKnownPartition(partitionId))
			{
				ExTraceGlobals.GetConnectionTracer.TraceDebug<string>(0L, "FromAllTenantsOrRootOrgAutoDetect(): Value '{0}' passed to id parameter doesn't match any known partition, falling back to RootOrg scope set", id.ToString());
				return ADSessionSettings.FromRootOrgScopeSet();
			}
			ExTraceGlobals.GetConnectionTracer.TraceDebug<string, string>(0L, "FromAllTenantsOrRootOrgAutoDetect(): Value '{0}' passed to id parameter matches partition {1}, returning settings bound to that partition", id.ToString(), partitionId.ToString());
			if (ADSession.IsTenantIdentity(id, partitionId.ForestFQDN))
			{
				return ADSessionSettings.FromAllTenantsObjectId(id);
			}
			if (!TopologyProvider.IsAdamTopology())
			{
				return ADSessionSettings.FromAccountPartitionRootOrgScopeSet(id.GetPartitionId());
			}
			return ADSessionSettings.FromRootOrgScopeSet();
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00022CE2 File Offset: 0x00020EE2
		internal override ADSessionSettings FromAllTenantsOrRootOrgAutoDetect(OrganizationId orgId)
		{
			if (orgId == null)
			{
				throw new ArgumentNullException("orgId");
			}
			if (!OrganizationId.ForestWideOrgId.Equals(orgId))
			{
				return ADSessionSettings.FromAllTenantsPartitionId(orgId.PartitionId);
			}
			return ADSessionSettings.FromRootOrgScopeSet();
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00022D16 File Offset: 0x00020F16
		internal override ADSessionSettings FromTenantCUName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			ExTraceGlobals.GetConnectionTracer.TraceDebug<string>((long)name.GetHashCode(), "FromTenantCUName(): Building session settings from CU name '{0}'", name);
			return ADSessionSettings.FromTenantAcceptedDomain(name);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00022D44 File Offset: 0x00020F44
		internal override ADSessionSettings FromTenantAcceptedDomain(string domain)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			SmtpDomain domainName;
			if (!SmtpDomain.TryParse(domain, out domainName))
			{
				throw new CannotResolveTenantNameException(DirectoryStrings.CannotResolveTenantNameByAcceptedDomain(domain));
			}
			if (ConsumerIdentityHelper.IsConsumerDomain(domainName))
			{
				return ADSessionSettings.FromConsumerOrganization();
			}
			OrganizationId scopingOrganizationId = OrganizationId.FromAcceptedDomain(domain);
			return ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(scopingOrganizationId);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00022D90 File Offset: 0x00020F90
		internal override ADSessionSettings FromTenantMSAUser(string msaUserNetID)
		{
			OrganizationId scopingOrganizationId = OrganizationId.FromMSAUserNetID(msaUserNetID);
			return ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(scopingOrganizationId);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00022DAA File Offset: 0x00020FAA
		internal override ADSessionSettings FromAllTenantsPartitionId(PartitionId partitionId)
		{
			return ADSessionSettings.SessionSettingsFactory.CreateADSessionSettings(ScopeSet.GetAllTenantsDefaultScopeSet(partitionId.ForestFQDN), null, OrganizationId.ForestWideOrgId, null, ConfigScopes.AllTenants, partitionId);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00022DC5 File Offset: 0x00020FC5
		internal override ADSessionSettings FromTenantPartitionHint(TenantPartitionHint partitionHint)
		{
			if (partitionHint == null)
			{
				throw new ArgumentNullException("partitionHint");
			}
			ExTraceGlobals.GetConnectionTracer.TraceDebug<string>(0L, "FromTenantPartitionHint(): Building session settings from partition hint '{0}'", partitionHint.ToString());
			return partitionHint.GetTenantScopedADSessionSettingsServiceOnly();
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00022DF4 File Offset: 0x00020FF4
		internal override ADSessionSettings FromExternalDirectoryOrganizationId(Guid externalDirectoryOrganizationId)
		{
			if (externalDirectoryOrganizationId == TemplateTenantConfiguration.TemplateTenantExternalDirectoryOrganizationIdGuid)
			{
				return ADSessionSettings.FromConsumerOrganization();
			}
			OrganizationId scopingOrganizationId = OrganizationId.FromExternalDirectoryOrganizationId(externalDirectoryOrganizationId);
			return ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(scopingOrganizationId);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00022E24 File Offset: 0x00021024
		internal override ADSessionSettings FromTenantForestAndCN(string exoAccountForest, string exoTenantContainer)
		{
			OrganizationId scopingOrganizationId = OrganizationId.FromTenantForestAndCN(exoAccountForest, exoTenantContainer);
			return ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(scopingOrganizationId);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00022E3F File Offset: 0x0002103F
		internal override ADSessionSettings FromAllTenantsObjectId(ADObjectId id)
		{
			return ADSessionSettings.FromAllTenantsPartitionId(id.GetPartitionId());
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00022E4C File Offset: 0x0002104C
		internal override bool InDomain()
		{
			if (this.inDomain == null)
			{
				if (ADSession.IsBoundToAdam)
				{
					this.inDomain = new bool?(false);
				}
				try
				{
					NativeHelpers.GetDomainName();
					this.inDomain = new bool?(true);
				}
				catch (CannotGetDomainInfoException)
				{
					this.inDomain = new bool?(false);
				}
			}
			return this.inDomain.Value;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00022EB8 File Offset: 0x000210B8
		protected override OrganizationId RehomeScopingOrganizationIdIfNeeded(OrganizationId currentOrganizationId)
		{
			if (currentOrganizationId != null && ADSessionSettings.SessionSettingsFactory.IsTenantScopedOrganization(currentOrganizationId))
			{
				try
				{
					bool flag;
					TenantRelocationState tenantRelocationState = TenantRelocationStateCache.GetTenantRelocationState(currentOrganizationId.OrganizationalUnit.Name, currentOrganizationId.PartitionId, out flag, false);
					if (flag && tenantRelocationState != null && tenantRelocationState.SourceForestState == TenantRelocationStatus.Retired && tenantRelocationState.TargetOrganizationId != null)
					{
						currentOrganizationId = tenantRelocationState.TargetOrganizationId;
					}
					else if (!flag && tenantRelocationState != null && tenantRelocationState.SourceForestState != TenantRelocationStatus.Retired && tenantRelocationState.OrganizationId != null)
					{
						currentOrganizationId = tenantRelocationState.OrganizationId;
					}
				}
				catch (CannotResolveTenantNameException)
				{
				}
			}
			return currentOrganizationId;
		}

		// Token: 0x04000290 RID: 656
		private bool? inDomain;

		// Token: 0x02000084 RID: 132
		internal abstract class ThreadSessionSettingEnabler : IDisposable
		{
			// Token: 0x060006BD RID: 1725 RVA: 0x00022F64 File Offset: 0x00021164
			public ThreadSessionSettingEnabler()
			{
				ADSessionSettingsFactory.ThreadSessionSettingEnabler.Enable(this.PostAction);
			}

			// Token: 0x060006BE RID: 1726 RVA: 0x00022F77 File Offset: 0x00021177
			public void Dispose()
			{
				ADSessionSettingsFactory.ThreadSessionSettingEnabler.Disable(this.PostAction);
			}

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x060006BF RID: 1727
			public abstract ADSessionSettings.SessionSettingsFactory.PostActionForSettings PostAction { get; }

			// Token: 0x060006C0 RID: 1728 RVA: 0x00022F84 File Offset: 0x00021184
			public static void Enable(ADSessionSettings.SessionSettingsFactory.PostActionForSettings postAction)
			{
				ADSessionSettings.SessionSettingsFactory.ThreadPostActionForSettings = (ADSessionSettings.SessionSettingsFactory.PostActionForSettings)Delegate.Combine(ADSessionSettings.SessionSettingsFactory.ThreadPostActionForSettings, postAction);
			}

			// Token: 0x060006C1 RID: 1729 RVA: 0x00022F9B File Offset: 0x0002119B
			public static void Disable(ADSessionSettings.SessionSettingsFactory.PostActionForSettings postAction)
			{
				ADSessionSettings.SessionSettingsFactory.ThreadPostActionForSettings = (ADSessionSettings.SessionSettingsFactory.PostActionForSettings)Delegate.Remove(ADSessionSettings.SessionSettingsFactory.ThreadPostActionForSettings, postAction);
			}
		}

		// Token: 0x02000085 RID: 133
		internal sealed class InactiveMailboxVisibilityEnabler : ADSessionSettingsFactory.ThreadSessionSettingEnabler
		{
			// Token: 0x1700015F RID: 351
			// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00022FB2 File Offset: 0x000211B2
			public override ADSessionSettings.SessionSettingsFactory.PostActionForSettings PostAction
			{
				get
				{
					return new ADSessionSettings.SessionSettingsFactory.PostActionForSettings(ADSessionSettingsFactory.InactiveMailboxVisibilityEnabler.AddInactiveMailBoxSupport);
				}
			}

			// Token: 0x060006C3 RID: 1731 RVA: 0x00022FC0 File Offset: 0x000211C0
			public static void Enable()
			{
				ADSessionSettingsFactory.ThreadSessionSettingEnabler.Enable(new ADSessionSettings.SessionSettingsFactory.PostActionForSettings(ADSessionSettingsFactory.InactiveMailboxVisibilityEnabler.AddInactiveMailBoxSupport));
			}

			// Token: 0x060006C4 RID: 1732 RVA: 0x00022FD3 File Offset: 0x000211D3
			public static void Disable()
			{
				ADSessionSettingsFactory.ThreadSessionSettingEnabler.Disable(new ADSessionSettings.SessionSettingsFactory.PostActionForSettings(ADSessionSettingsFactory.InactiveMailboxVisibilityEnabler.AddInactiveMailBoxSupport));
			}

			// Token: 0x060006C5 RID: 1733 RVA: 0x00022FE6 File Offset: 0x000211E6
			public static ADSessionSettings AddInactiveMailBoxSupport(ADSessionSettings settings)
			{
				settings.IncludeInactiveMailbox = true;
				return settings;
			}
		}

		// Token: 0x02000086 RID: 134
		internal sealed class SoftDeletedObjectVisibilityEnabler : ADSessionSettingsFactory.ThreadSessionSettingEnabler
		{
			// Token: 0x17000160 RID: 352
			// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00022FF8 File Offset: 0x000211F8
			public override ADSessionSettings.SessionSettingsFactory.PostActionForSettings PostAction
			{
				get
				{
					return new ADSessionSettings.SessionSettingsFactory.PostActionForSettings(ADSessionSettingsFactory.SoftDeletedObjectVisibilityEnabler.AddSoftDeletedObjectSupport);
				}
			}

			// Token: 0x060006C8 RID: 1736 RVA: 0x00023006 File Offset: 0x00021206
			public static ADSessionSettings AddSoftDeletedObjectSupport(ADSessionSettings settings)
			{
				settings.IncludeSoftDeletedObjects = true;
				return settings;
			}
		}
	}
}
