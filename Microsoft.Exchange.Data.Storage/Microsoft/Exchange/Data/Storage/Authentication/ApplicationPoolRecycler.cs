using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Security.ExternalAuthentication;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Data.Storage.Authentication
{
	// Token: 0x02000DEA RID: 3562
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ApplicationPoolRecycler
	{
		// Token: 0x06007A7D RID: 31357 RVA: 0x0021D8E8 File Offset: 0x0021BAE8
		public static void EnableOnFederationTrustCertificateChange()
		{
			if (!ApplicationPoolRecycler.enabled)
			{
				lock (ApplicationPoolRecycler.locker)
				{
					if (!ApplicationPoolRecycler.enabled)
					{
						ApplicationPoolRecycler.initialCertificates = ApplicationPoolRecycler.GetCertificatesThumbprint();
						FederationTrustCache.Change += ApplicationPoolRecycler.ChangeHandler;
						ApplicationPoolRecycler.enabled = true;
					}
				}
			}
		}

		// Token: 0x06007A7E RID: 31358 RVA: 0x0021D950 File Offset: 0x0021BB50
		private static void ChangeHandler()
		{
			HashSet<string> certificatesThumbprint = ApplicationPoolRecycler.GetCertificatesThumbprint();
			if (!certificatesThumbprint.SetEquals(ApplicationPoolRecycler.initialCertificates))
			{
				ApplicationPoolRecycler.RecycleThisApplicationPool();
			}
		}

		// Token: 0x06007A7F RID: 31359 RVA: 0x0021D978 File Offset: 0x0021BB78
		private static void RecycleThisApplicationPool()
		{
			Process currentProcess = Process.GetCurrentProcess();
			ApplicationPoolRecycler.Tracer.TraceDebug<int>(0L, "Searching for application pool of the current process {0}", currentProcess.Id);
			using (ServerManager serverManager = new ServerManager())
			{
				foreach (WorkerProcess workerProcess in serverManager.WorkerProcesses)
				{
					if (workerProcess.ProcessId == currentProcess.Id)
					{
						ApplicationPool applicationPool = serverManager.ApplicationPools[workerProcess.AppPoolName];
						if (applicationPool != null)
						{
							ApplicationPoolRecycler.Tracer.TraceDebug<int, string>(0L, "Found application pool current process {0}: {1}. Recycling application pool now.", currentProcess.Id, applicationPool.Name);
							applicationPool.Recycle();
							return;
						}
					}
				}
			}
			ApplicationPoolRecycler.Tracer.TraceError<int>(0L, "Unable to find application pool of the current process {0}. Application pool will continue to run without updated certificates", currentProcess.Id);
		}

		// Token: 0x06007A80 RID: 31360 RVA: 0x0021DA60 File Offset: 0x0021BC60
		private static HashSet<string> GetCertificatesThumbprint()
		{
			IEnumerable<FederationTrust> federationTrusts = FederationTrustCache.GetFederationTrusts();
			HashSet<string> hashSet = new HashSet<string>();
			foreach (FederationTrust federationTrust in federationTrusts)
			{
				if (federationTrust.OrgPrivCertificate != null)
				{
					hashSet.Add(federationTrust.OrgPrivCertificate);
				}
				if (federationTrust.OrgPrevPrivCertificate != null)
				{
					hashSet.Add(federationTrust.OrgPrevPrivCertificate);
				}
				if (federationTrust.TokenIssuerCertificate != null)
				{
					hashSet.Add(federationTrust.TokenIssuerCertificate.Thumbprint);
				}
				if (federationTrust.TokenIssuerPrevCertificate != null)
				{
					hashSet.Add(federationTrust.TokenIssuerPrevCertificate.Thumbprint);
				}
			}
			return hashSet;
		}

		// Token: 0x04005458 RID: 21592
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.ConfigurationTracer;

		// Token: 0x04005459 RID: 21593
		private static bool enabled;

		// Token: 0x0400545A RID: 21594
		private static object locker = new object();

		// Token: 0x0400545B RID: 21595
		private static HashSet<string> initialCertificates;
	}
}
