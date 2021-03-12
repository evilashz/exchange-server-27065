using System;
using System.IO;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002BE RID: 702
	internal class HealthCheckPipelineContext : PipelineContext
	{
		// Token: 0x06001544 RID: 5444 RVA: 0x0005B3F8 File Offset: 0x000595F8
		internal HealthCheckPipelineContext(string workId) : base(new SubmissionHelper(string.Empty, PhoneNumber.Empty, Guid.Empty, string.Empty, "en-US", string.Empty, string.Empty, Guid.Empty))
		{
			base.MessageType = "HealthCheck";
			this.attachmentPath = Path.Combine(Utils.VoiceMailFilePath, workId + ".HealthCheck");
			base.HeaderFileName = Path.Combine(Utils.VoiceMailFilePath, workId + ".txt");
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x0005B479 File Offset: 0x00059679
		internal override Pipeline Pipeline
		{
			get
			{
				return HealthCheckPipeline.Instance;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x0005B480 File Offset: 0x00059680
		internal PipelineDispatcher.PipelineResourceType ResourceBeingChecked
		{
			get
			{
				return this.resourceBeingChecked;
			}
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0005B488 File Offset: 0x00059688
		internal static void TryDeleteHealthCheckFiles()
		{
			foreach (string filename in HealthCheckPipelineContext.GetQueuedHealthCheckFilePaths())
			{
				Util.TryDeleteFile(filename);
			}
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0005B4B4 File Offset: 0x000596B4
		internal static string[] GetQueuedHealthCheckFilePaths()
		{
			string[] array = null;
			try
			{
				array = Directory.GetFiles(Utils.VoiceMailFilePath, "*.HealthCheck");
			}
			catch (IOException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, null, "Ignoring IOException while cleaning up health-check files.  {0}", new object[]
				{
					ex
				});
			}
			return array ?? new string[0];
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0005B510 File Offset: 0x00059710
		internal static bool IsPipelineHealthy()
		{
			return HealthCheckPipelineContext.GetQueuedHealthCheckFilePaths().Length <= 4;
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0005B51F File Offset: 0x0005971F
		internal override void WriteCustomHeaderFields(StreamWriter headerStream)
		{
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0005B524 File Offset: 0x00059724
		internal override void SaveMessage()
		{
			using (File.Create(this.attachmentPath))
			{
			}
			base.SaveMessage();
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0005B560 File Offset: 0x00059760
		public override string GetMailboxServerId()
		{
			return base.GetMailboxServerIdHelper();
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0005B568 File Offset: 0x00059768
		public override string GetRecipientIdForThrottling()
		{
			return base.GetRecipientIdHelper();
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0005B570 File Offset: 0x00059770
		internal void Passed(PipelineDispatcher.PipelineResourceType resourceType)
		{
			this.resourceBeingChecked++;
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0005B580 File Offset: 0x00059780
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					HealthCheckPipelineContext.TryDeleteHealthCheckFiles();
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x04000CD0 RID: 3280
		public const string HealthCheckExtension = ".HealthCheck";

		// Token: 0x04000CD1 RID: 3281
		public const int MaxMessagesAllowedInQueue = 4;

		// Token: 0x04000CD2 RID: 3282
		private PipelineDispatcher.PipelineResourceType resourceBeingChecked;

		// Token: 0x04000CD3 RID: 3283
		private string attachmentPath;
	}
}
