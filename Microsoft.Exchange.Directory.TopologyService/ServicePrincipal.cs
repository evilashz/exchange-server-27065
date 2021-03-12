using System;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Directory.TopologyService.Configuration;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x02000013 RID: 19
	internal class ServicePrincipal : ServicePrincipal
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00005E5B File Offset: 0x0000405B
		public ServicePrincipal(IIdentity identity) : base(identity, ExTraceGlobals.WCFServiceEndpointTracer)
		{
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00005E6C File Offset: 0x0000406C
		protected override double LocalIPCacheRefreshInMilliseconds
		{
			get
			{
				return ConfigurationData.Instance.LocalIPAddressesCacheRefreshInterval.TotalMilliseconds;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005E8C File Offset: 0x0000408C
		protected override bool IsInRoleInternal(WindowsPrincipal principal, string role)
		{
			bool flag;
			if (role != null && role == "ReadOnlyAdmin")
			{
				flag = this.IsReadOnlyAdmin(principal);
				base.Tracer.TraceDebug<string>((long)this.GetHashCode(), "User is {0}ReadOnlyAdmin.", flag ? string.Empty : "NOT ");
			}
			else
			{
				flag = base.IsInRoleInternal(principal, role);
			}
			return flag;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005EE7 File Offset: 0x000040E7
		private bool IsReadOnlyAdmin(WindowsPrincipal wPrincipal)
		{
			ArgumentValidator.ThrowIfNull("wPrincipal", wPrincipal);
			return this.TryResolveReadOnlyAdmins() && wPrincipal.IsInRole(ServicePrincipal.readOnlyAdminsSid);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005F0C File Offset: 0x0000410C
		private bool TryResolveReadOnlyAdmins()
		{
			if (null != ServicePrincipal.readOnlyAdminsSid)
			{
				return true;
			}
			if (Globals.GetTickDifference(ServicePrincipal.lastResetTick, Environment.TickCount) > 60000UL)
			{
				Interlocked.Exchange(ref ServicePrincipal.attemptsToReadEraGroup, 0);
				Interlocked.Exchange(ref ServicePrincipal.lastResetTick, Environment.TickCount);
			}
			if (Interlocked.Increment(ref ServicePrincipal.readingEraGroup) != 1)
			{
				Interlocked.Decrement(ref ServicePrincipal.readingEraGroup);
				return false;
			}
			ADSessionSettings sessionSettings = ADSessionSettings.SessionSettingsFactory.Default.FromRootOrgScopeSet();
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, sessionSettings, 175, "TryResolveReadOnlyAdmins", "f:\\15.00.1497\\sources\\dev\\Directory\\src\\TopologyService\\Service\\ServicePrincipal.cs");
			IRootOrganizationRecipientSession rootOrganizationRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.FullyConsistent, sessionSettings, 179, "TryResolveReadOnlyAdmins", "f:\\15.00.1497\\sources\\dev\\Directory\\src\\TopologyService\\Service\\ServicePrincipal.cs");
			rootOrganizationRecipientSession.UseGlobalCatalog = true;
			Exception ex = null;
			string text = null;
			try
			{
				ex = null;
				text = topologyConfigurationSession.ConfigurationNamingContext.DistinguishedName;
				MiniRecipient miniRecipient = rootOrganizationRecipientSession.ResolveWellKnownGuid<MiniRecipient>(WellKnownGuid.EraWkGuid, topologyConfigurationSession.ConfigurationNamingContext);
				if (miniRecipient != null && null != miniRecipient.Sid)
				{
					ServicePrincipal.readOnlyAdminsSid = miniRecipient.Sid;
				}
			}
			catch (ADReferralException ex2)
			{
				base.Tracer.TraceError<Exception>((long)this.GetHashCode(), "Error trying to resolve Exchange Read Only Admins. Exception {0}", ex2);
				ex = ex2;
			}
			catch (ADTransientException ex3)
			{
				base.Tracer.TraceError<Exception>((long)this.GetHashCode(), "Error trying to resolve Exchange Read Only Admins. Exception {0}", ex3);
				ex = ex3;
			}
			finally
			{
				Interlocked.Increment(ref ServicePrincipal.attemptsToReadEraGroup);
				Interlocked.Decrement(ref ServicePrincipal.readingEraGroup);
			}
			if (null == ServicePrincipal.readOnlyAdminsSid)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_ADTOPO_RPC_RESOLVE_SID_FAILED, "TryResolveReadOnlyAdmins", new object[]
				{
					string.IsNullOrEmpty(text) ? "<WKGUID=1A9E39D35ABE5747B979FFC0C6E5EA26,CN=Microsoft Exchange,CN=Services,CN=Configuration,...>" : string.Format("<WKGUID=1A9E39D35ABE5747B979FFC0C6E5EA26,CN=Microsoft Exchange,CN=Services,{0}>", text),
					(ex != null) ? ex.Message : string.Empty
				});
			}
			return null != ServicePrincipal.readOnlyAdminsSid;
		}

		// Token: 0x04000040 RID: 64
		public const string ReadOnlyAdmin = "ReadOnlyAdmin";

		// Token: 0x04000041 RID: 65
		private const int MaxAttemptsToReadAdminsGroupPerHour = 5;

		// Token: 0x04000042 RID: 66
		private const string ReadOnlyAdminsFqdn = "<WKGUID=1A9E39D35ABE5747B979FFC0C6E5EA26,CN=Microsoft Exchange,CN=Services,CN=Configuration,...>";

		// Token: 0x04000043 RID: 67
		private const string ReadOnlyAdminsFqdnTemplate = "<WKGUID=1A9E39D35ABE5747B979FFC0C6E5EA26,CN=Microsoft Exchange,CN=Services,{0}>";

		// Token: 0x04000044 RID: 68
		private static readonly string[] RoleSeparator = new string[]
		{
			"+"
		};

		// Token: 0x04000045 RID: 69
		private static SecurityIdentifier readOnlyAdminsSid;

		// Token: 0x04000046 RID: 70
		private static int readingEraGroup = 0;

		// Token: 0x04000047 RID: 71
		private static int attemptsToReadEraGroup = 0;

		// Token: 0x04000048 RID: 72
		private static int lastResetTick = Environment.TickCount;
	}
}
