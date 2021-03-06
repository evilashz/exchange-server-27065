using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Authorization;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000241 RID: 577
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RwsExchangeRunspaceConfiguration : ExchangeRunspaceConfiguration
	{
		// Token: 0x06001475 RID: 5237 RVA: 0x0004C170 File Offset: 0x0004A370
		internal RwsExchangeRunspaceConfiguration(IIdentity logonIdentity, ExchangeRunspaceConfigurationSettings settings, List<RoleEntry> sortedRoleEntryFilter, SnapinSet snapinSet) : base(logonIdentity, null, settings, null, sortedRoleEntryFilter, null, false, false, true, snapinSet)
		{
			this.allRoleAssignments = null;
			foreach (RoleType key in this.allRoleTypes.Keys.ToList<RoleType>())
			{
				this.allRoleTypes[key] = null;
			}
			base.RestrictToFilteredCmdlet = true;
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x0004C1F4 File Offset: 0x0004A3F4
		protected override ADObjectId[] GetNestedSecurityGroupMembership(ADRawEntry user)
		{
			OrganizationId organizationId = (OrganizationId)user[ADObjectSchema.OrganizationId];
			if (organizationId != null)
			{
				ExTraceGlobals.RunspaceConfigTracer.TraceDebug(0L, "[RwsExchangeRunspaceConfiguration::GetNestedSecurityGroupMembership] Create ADSessionSettings from OrgId!");
				using (new MonitoredScope("ExchangeRunspaceConfiguration", "GetNestedSecurityGroupMembership", AuthZLogHelper.AuthZPerfMonitors))
				{
					ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(organizationId);
					IRecipientSession tenantOrRootOrgRecipientSession;
					using (new MonitoredScope("ExchangeRunspaceConfiguration", "GetNestedSecurityGroupMembership.GetTenantOrRootOrgRecipientSession", AuthZLogHelper.AuthZPerfMonitors))
					{
						tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 101, "GetNestedSecurityGroupMembership", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\RwsExchangeRunspaceConfiguration.cs");
					}
					return ExchangeRunspaceConfiguration.GetNestedSecurityGroupMembership(user, tenantOrRootOrgRecipientSession, AssignmentMethod.All);
				}
			}
			ExTraceGlobals.RunspaceConfigTracer.TraceDebug(0L, "[RwsExchangeRunspaceConfiguration::GetNestedSecurityGroupMembership] OrgId == null");
			return base.GetNestedSecurityGroupMembership(user);
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0004C308 File Offset: 0x0004A508
		internal static RwsExchangeRunspaceConfiguration NewInstance(IIdentity logonIdentity, ExchangeRunspaceConfigurationSettings settings, List<RoleEntry> sortedRoleEntryFilter, SnapinSet snapinSet)
		{
			RwsExchangeRunspaceConfiguration configuration = null;
			if (RwsExchangeRunspaceConfiguration.RunspaceConfigurationTimeoutEnabled.Value)
			{
				ExTraceGlobals.RunspaceConfigTracer.TraceDebug<int, int>(0L, "[RwsExchangeRunspaceConfiguration::NewInstance] Asynchronizely Creating RwsExchangeRunspaceConfiguration. Timeout = {0} MaxRetry = {1}", RwsExchangeRunspaceConfiguration.RunspaceConfigurationTimeoutMilliseconds.Value, RwsExchangeRunspaceConfiguration.RunspaceConfigurationRetryCount.Value);
				int num = 0;
				for (;;)
				{
					ExTraceGlobals.RunspaceConfigTracer.TraceDebug<int>(0L, "[RwsExchangeRunspaceConfiguration::NewInstance] RetryCount = {0}", num);
					RwsExchangeRunspaceConfiguration.RunActionWithTimeout(new TimeSpan(0, 0, 0, 0, RwsExchangeRunspaceConfiguration.RunspaceConfigurationTimeoutMilliseconds.Value), delegate
					{
						configuration = new RwsExchangeRunspaceConfiguration(logonIdentity, settings, sortedRoleEntryFilter, snapinSet);
					});
					if (configuration != null)
					{
						break;
					}
					if (num++ >= RwsExchangeRunspaceConfiguration.RunspaceConfigurationRetryCount.Value)
					{
						goto Block_4;
					}
				}
				ExTraceGlobals.RunspaceConfigTracer.TraceDebug(0L, "[RwsExchangeRunspaceConfiguration::NewInstance] Got Configuration.");
				return configuration;
				Block_4:
				throw new TimeoutException(string.Format("Timeout while creating RwsExchangeRunspaceConfiguration. Timeout={0}, MaxRetryCount={1}, CurrentRetryCount={2}.", RwsExchangeRunspaceConfiguration.RunspaceConfigurationTimeoutMilliseconds.Value, RwsExchangeRunspaceConfiguration.RunspaceConfigurationRetryCount.Value, num));
			}
			ExTraceGlobals.RunspaceConfigTracer.TraceDebug(0L, "[RwsExchangeRunspaceConfiguration::NewInstance] Synchronizely Creating RwsExchangeRunspaceConfiguration.");
			configuration = new RwsExchangeRunspaceConfiguration(logonIdentity, settings, sortedRoleEntryFilter, snapinSet);
			return configuration;
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x0004C4C8 File Offset: 0x0004A6C8
		internal static bool RunActionWithTimeout(TimeSpan timeout, Action action)
		{
			object sync = new object();
			bool isCompleted = false;
			WaitCallback callBack = delegate(object obj)
			{
				Thread thread = obj as Thread;
				lock (sync)
				{
					if (!isCompleted)
					{
						Monitor.Wait(sync, timeout);
					}
				}
				if (!isCompleted)
				{
					thread.Abort();
				}
			};
			bool result;
			try
			{
				ExTraceGlobals.RunspaceConfigTracer.TraceDebug(0L, "[RwsExchangeRunspaceConfiguration::RunActionWithTimeout] Taking action.");
				ThreadPool.QueueUserWorkItem(callBack, Thread.CurrentThread);
				action();
				result = true;
			}
			catch (ThreadAbortException)
			{
				ExTraceGlobals.RunspaceConfigTracer.TraceDebug(0L, "[RwsExchangeRunspaceConfiguration::RunActionWithTimeout] Thread got aborted!");
				Thread.ResetAbort();
				result = false;
			}
			finally
			{
				lock (sync)
				{
					isCompleted = true;
					Monitor.Pulse(sync);
				}
			}
			return result;
		}

		// Token: 0x040005E1 RID: 1505
		private static readonly BoolAppSettingsEntry RunspaceConfigurationTimeoutEnabled = new BoolAppSettingsEntry("RunspaceConfigurationTimeoutEnabled", true, ExTraceGlobals.RunspaceConfigTracer);

		// Token: 0x040005E2 RID: 1506
		private static readonly IntAppSettingsEntry RunspaceConfigurationTimeoutMilliseconds = new IntAppSettingsEntry("RunspaceConfigurationTimeoutMilliseconds", 10000, ExTraceGlobals.RunspaceConfigTracer);

		// Token: 0x040005E3 RID: 1507
		private static readonly IntAppSettingsEntry RunspaceConfigurationRetryCount = new IntAppSettingsEntry("RunspaceConfigurationRetryCount", 1, ExTraceGlobals.RunspaceConfigTracer);
	}
}
