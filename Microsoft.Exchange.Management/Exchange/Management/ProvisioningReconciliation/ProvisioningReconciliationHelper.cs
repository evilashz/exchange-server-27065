using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ProvisioningReconciliation
{
	// Token: 0x02000100 RID: 256
	internal static class ProvisioningReconciliationHelper
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x00020540 File Offset: 0x0001E740
		internal static ReconciliationCookie GetReconciliationCookie(ProvisioningReconciliationConfig provisioningReconciliationConfig, Task.TaskErrorLoggingDelegate errorLogger)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 42, "GetReconciliationCookie", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\ProvisioningReconciliationHelper.cs");
			ReconciliationCookie reconciliationCookie = null;
			if (provisioningReconciliationConfig.ReconciliationCookies != null && provisioningReconciliationConfig.ReconciliationCookies.Count > 0)
			{
				string empty = string.Empty;
				bool flag = false;
				using (MultiValuedProperty<ReconciliationCookie>.Enumerator enumerator = provisioningReconciliationConfig.ReconciliationCookies.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ReconciliationCookie reconciliationCookie2 = enumerator.Current;
						if (reconciliationCookie2.Version == ProvisioningReconciliationHelper.CurrentCookieVersion && ProvisioningReconciliationHelper.IsServerSuitable(reconciliationCookie2.InvocationId, topologyConfigurationSession, out empty, out flag))
						{
							reconciliationCookie = new ReconciliationCookie(reconciliationCookie2.Version, empty, reconciliationCookie2.InvocationId, reconciliationCookie2.HighestCommittedUsn);
							if (flag)
							{
								break;
							}
						}
					}
					goto IL_E8;
				}
			}
			Fqdn[] domainControllersInLocalSite = ProvisioningReconciliationHelper.GetDomainControllersInLocalSite(errorLogger);
			if (domainControllersInLocalSite != null)
			{
				foreach (Fqdn fqdn in domainControllersInLocalSite)
				{
					reconciliationCookie = ProvisioningReconciliationHelper.GetReconciliationCookieForDomainController(fqdn, topologyConfigurationSession, errorLogger);
					if (reconciliationCookie != null)
					{
						break;
					}
				}
			}
			IL_E8:
			if (reconciliationCookie != null)
			{
				return reconciliationCookie;
			}
			errorLogger(new TaskException(Strings.ErrorNoActiveDCForProvisioningReconciliationCookie), (ErrorCategory)1001, null);
			return null;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00020664 File Offset: 0x0001E864
		internal static MultiValuedProperty<ReconciliationCookie> GetReconciliationCookiesForNextCycle(string dc, Task.TaskErrorLoggingDelegate errorLogger)
		{
			MultiValuedProperty<ReconciliationCookie> multiValuedProperty = new MultiValuedProperty<ReconciliationCookie>();
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(dc, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromRootOrgScopeSet(), 126, "GetReconciliationCookiesForNextCycle", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\ProvisioningReconciliationHelper.cs");
			topologyConfigurationSession.UseConfigNC = false;
			MultiValuedProperty<ReplicationCursor> multiValuedProperty2 = topologyConfigurationSession.ReadReplicationCursors(ADSession.GetDomainNamingContextForLocalForest());
			topologyConfigurationSession.UseConfigNC = true;
			foreach (ReplicationCursor replicationCursor in multiValuedProperty2)
			{
				if (replicationCursor.SourceDsa != null)
				{
					ADServer adserver = topologyConfigurationSession.FindDCByInvocationId(replicationCursor.SourceInvocationId);
					if (adserver != null)
					{
						string dnsHostName = adserver.DnsHostName;
						ReconciliationCookie item = new ReconciliationCookie(ProvisioningReconciliationHelper.CurrentCookieVersion, dnsHostName, replicationCursor.SourceInvocationId, replicationCursor.UpToDatenessUsn);
						multiValuedProperty.Add(item);
					}
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00020734 File Offset: 0x0001E934
		private static ReconciliationCookie GetReconciliationCookieForDomainController(Fqdn fqdn, ITopologyConfigurationSession configSession, Task.TaskErrorLoggingDelegate errorLogger)
		{
			ADServer adserver = configSession.FindDCByFqdn(fqdn);
			if (adserver != null)
			{
				LocalizedString empty = LocalizedString.Empty;
				string text;
				if (SuitabilityVerifier.IsServerSuitableIgnoreExceptions(adserver.DnsHostName, true, null, out text, out empty))
				{
					ITopologyConfigurationSession sessionForDC = ProvisioningReconciliationHelper.GetSessionForDC(adserver);
					RootDse rootDse = sessionForDC.GetRootDse();
					Guid invocationIdByDC = sessionForDC.GetInvocationIdByDC(adserver);
					return new ReconciliationCookie(ProvisioningReconciliationHelper.CurrentCookieVersion, adserver.DnsHostName, invocationIdByDC, rootDse.HighestCommittedUSN);
				}
			}
			return null;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x000207A0 File Offset: 0x0001E9A0
		private static Fqdn[] GetDomainControllersInLocalSite(Task.TaskErrorLoggingDelegate errorLogger)
		{
			ADForest localForest = ADForest.GetLocalForest();
			if (localForest == null)
			{
				errorLogger(new TaskException(Strings.ErrorCannotRetrieveLocalForest), (ErrorCategory)1001, null);
			}
			List<ADServer> list = localForest.FindAllGlobalCatalogsInLocalSite();
			if (list == null || list.Count == 0)
			{
				errorLogger(new TaskException(Strings.ErrorNoDCInLocalSite), (ErrorCategory)1001, null);
			}
			Fqdn[] array = new Fqdn[list.Count];
			int num = 0;
			foreach (ADServer adserver in list)
			{
				array[num] = new Fqdn(adserver.DnsHostName);
				num++;
			}
			return array;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00020854 File Offset: 0x0001EA54
		private static ITopologyConfigurationSession GetSessionForDC(ADServer dc)
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(dc.DnsHostName, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 246, "GetSessionForDC", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\ProvisioningReconciliationHelper.cs");
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00020888 File Offset: 0x0001EA88
		private static bool IsServerSuitable(Guid invocationId, ITopologyConfigurationSession session, out string dnsHostName, out bool isInLocalSite)
		{
			dnsHostName = string.Empty;
			isInLocalSite = false;
			ADServer adserver = session.FindDCByInvocationId(invocationId);
			if (adserver == null)
			{
				return false;
			}
			dnsHostName = adserver.DnsHostName;
			isInLocalSite = adserver.IsInLocalSite;
			LocalizedString empty = LocalizedString.Empty;
			string text;
			return SuitabilityVerifier.IsServerSuitableIgnoreExceptions(adserver.DnsHostName, true, null, out text, out empty);
		}

		// Token: 0x0400039B RID: 923
		public static readonly int CurrentCookieVersion = 2;
	}
}
