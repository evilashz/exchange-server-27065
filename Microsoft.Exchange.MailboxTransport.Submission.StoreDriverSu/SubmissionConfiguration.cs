using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000042 RID: 66
	internal class SubmissionConfiguration : ISubmissionConfiguration
	{
		// Token: 0x0600029C RID: 668 RVA: 0x0000D96D File Offset: 0x0000BB6D
		private SubmissionConfiguration()
		{
			SubmissionConfiguration.components = new Components(string.Empty, false);
			SubmissionConfiguration.app = AppConfig.Load();
			this.isInitialized = false;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000D996 File Offset: 0x0000BB96
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000D9AE File Offset: 0x0000BBAE
		public static ISubmissionConfiguration Instance
		{
			get
			{
				if (SubmissionConfiguration.configuration == null)
				{
					SubmissionConfiguration.configuration = new SubmissionConfiguration();
				}
				return SubmissionConfiguration.configuration;
			}
			set
			{
				SubmissionConfiguration.configuration = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000D9B6 File Offset: 0x0000BBB6
		public IAppConfiguration App
		{
			get
			{
				return SubmissionConfiguration.app;
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000D9C0 File Offset: 0x0000BBC0
		public void Load()
		{
			if (!this.isInitialized)
			{
				SubmissionConfiguration.components.Start(new Components.StopServiceHandler(SubmissionConfiguration.OnStopServiceBecauseOfFailure), false, false, true, true);
				SubmissionConfiguration.components.Continue();
				Components.StoreDriverSubmission.Continue();
				LatencyTracker.Start(Components.TransportAppConfig.LatencyTracker, ProcessTransportRole.MailboxSubmission);
				SubmissionConfiguration.StartSystemProbe();
				this.isInitialized = true;
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000DA1F File Offset: 0x0000BC1F
		public void Unload()
		{
			if (this.isInitialized)
			{
				SystemProbe.Stop();
				SubmissionConfiguration.components.Stop();
				this.isInitialized = false;
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000DA3F File Offset: 0x0000BC3F
		public void ConfigUpdate()
		{
			SubmissionConfiguration.components.ConfigUpdate();
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000DA4C File Offset: 0x0000BC4C
		private static void StartSystemProbe()
		{
			try
			{
				SystemProbe.Start("SYSPRB", ProcessTransportRole.MailboxSubmission.ToString());
				ExTraceGlobals.GeneralTracer.TraceDebug(0L, "MBTSubmission: System probe started successfully.");
				SystemProbe.ActivityId = CombGuidGenerator.NewGuid();
				SystemProbe.TracePass("MBTSubmission", "System probe started successfully.", new object[0]);
				SystemProbe.ActivityId = Guid.Empty;
			}
			catch (LogException ex)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "MBTSubmission: Failed to initialize system probe. {0}", ex.Message);
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		private static void OnStopServiceBecauseOfFailure(string reason, bool canRetry, bool retryAlways, bool failServiceWithException)
		{
			Environment.Exit(1);
		}

		// Token: 0x04000169 RID: 361
		private static Components components;

		// Token: 0x0400016A RID: 362
		private static IAppConfiguration app;

		// Token: 0x0400016B RID: 363
		private static ISubmissionConfiguration configuration;

		// Token: 0x0400016C RID: 364
		private bool isInitialized;
	}
}
