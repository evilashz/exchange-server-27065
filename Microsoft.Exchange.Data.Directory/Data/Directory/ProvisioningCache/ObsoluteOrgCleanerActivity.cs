using System;
using System.Threading;

namespace Microsoft.Exchange.Data.Directory.ProvisioningCache
{
	// Token: 0x020007A8 RID: 1960
	internal class ObsoluteOrgCleanerActivity : Activity
	{
		// Token: 0x0600615C RID: 24924 RVA: 0x0014B941 File Offset: 0x00149B41
		public ObsoluteOrgCleanerActivity(ProvisioningCache cache) : base(cache)
		{
		}

		// Token: 0x170022CC RID: 8908
		// (get) Token: 0x0600615D RID: 24925 RVA: 0x0014B94A File Offset: 0x00149B4A
		public override string Name
		{
			get
			{
				return "Expired organizations cleaner";
			}
		}

		// Token: 0x0600615E RID: 24926 RVA: 0x0014B951 File Offset: 0x00149B51
		protected override void InternalExecute()
		{
			while (!base.GotStopSignalFromTestCode)
			{
				Thread.Sleep(ObsoluteOrgCleanerActivity.cleanUpInterval);
				base.ProvisioningCache.ClearExpireOrganizations();
			}
		}

		// Token: 0x0600615F RID: 24927 RVA: 0x0014B972 File Offset: 0x00149B72
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x06006160 RID: 24928 RVA: 0x0014B974 File Offset: 0x00149B74
		internal override void StopExecute()
		{
			base.StopExecute();
			if (base.GotStopSignalFromTestCode && base.AsyncThread.ThreadState != ThreadState.Stopped && base.AsyncThread.ThreadState != ThreadState.Aborted)
			{
				base.AsyncThread.Abort();
				base.AsyncThread.Join();
			}
		}

		// Token: 0x0400415E RID: 16734
		private static readonly TimeSpan cleanUpInterval = new TimeSpan(2, 0, 0);
	}
}
