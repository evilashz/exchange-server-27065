using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200005C RID: 92
	public static class QuarantinedJobs
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0001CF5B File Offset: 0x0001B15B
		private static IDictionary<Guid, FailureRec> Jobs
		{
			get
			{
				return QuarantinedJobs.quarantinedJobs.Value;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0001CF67 File Offset: 0x0001B167
		public static bool Enabled
		{
			get
			{
				return QuarantinedJobs.enabled.Value;
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001CF74 File Offset: 0x0001B174
		public static void Add(Guid requestGuid, Exception ex)
		{
			if (!QuarantinedJobs.Enabled)
			{
				return;
			}
			JobQuarantineProvider.Instance.QuarantineJob(requestGuid, ex);
			lock (QuarantinedJobs.syncRoot)
			{
				QuarantinedJobs.Jobs.Add(requestGuid, FailureRec.Create(ex));
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001CFD4 File Offset: 0x0001B1D4
		public static void Remove(Guid requestGuid)
		{
			if (!QuarantinedJobs.Enabled)
			{
				return;
			}
			JobQuarantineProvider.Instance.UnquarantineJob(requestGuid);
			lock (QuarantinedJobs.syncRoot)
			{
				QuarantinedJobs.Jobs.Remove(requestGuid);
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001D02C File Offset: 0x0001B22C
		public static bool Contains(Guid requestGuid)
		{
			if (!QuarantinedJobs.Enabled)
			{
				return false;
			}
			bool result;
			lock (QuarantinedJobs.syncRoot)
			{
				result = QuarantinedJobs.Jobs.ContainsKey(requestGuid);
			}
			return result;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001D07C File Offset: 0x0001B27C
		public static FailureRec Get(Guid requestGuid)
		{
			if (!QuarantinedJobs.Enabled)
			{
				return null;
			}
			FailureRec result;
			lock (QuarantinedJobs.syncRoot)
			{
				FailureRec failureRec = null;
				QuarantinedJobs.Jobs.TryGetValue(requestGuid, out failureRec);
				result = failureRec;
			}
			return result;
		}

		// Token: 0x040001FF RID: 511
		private static readonly object syncRoot = new object();

		// Token: 0x04000200 RID: 512
		private static readonly Lazy<bool> enabled = new Lazy<bool>(() => ConfigBase<MRSConfigSchema>.GetConfig<bool>("QuarantineEnabled"));

		// Token: 0x04000201 RID: 513
		private static readonly Lazy<IDictionary<Guid, FailureRec>> quarantinedJobs = new Lazy<IDictionary<Guid, FailureRec>>(delegate()
		{
			if (QuarantinedJobs.Enabled)
			{
				return JobQuarantineProvider.Instance.GetQuarantinedJobs();
			}
			return null;
		});
	}
}
