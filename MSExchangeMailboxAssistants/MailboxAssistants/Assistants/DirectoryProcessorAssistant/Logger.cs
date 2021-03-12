using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant
{
	// Token: 0x020001AD RID: 429
	internal class Logger
	{
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x00062627 File Offset: 0x00060827
		// (set) Token: 0x060010C8 RID: 4296 RVA: 0x0006262F File Offset: 0x0006082F
		private Trace Tracer { get; set; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x00062638 File Offset: 0x00060838
		// (set) Token: 0x060010CA RID: 4298 RVA: 0x00062640 File Offset: 0x00060840
		private string TaskName { get; set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x00062649 File Offset: 0x00060849
		public string TenantId
		{
			get
			{
				return this.RunData.TenantId;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x00062656 File Offset: 0x00060856
		// (set) Token: 0x060010CD RID: 4301 RVA: 0x0006265E File Offset: 0x0006085E
		public RunData RunData { get; private set; }

		// Token: 0x060010CE RID: 4302 RVA: 0x00062667 File Offset: 0x00060867
		public Logger(RunData runData, Trace tracer, string taskName)
		{
			this.RunData = runData;
			this.Tracer = tracer;
			this.TaskName = taskName;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00062684 File Offset: 0x00060884
		public void TraceError(object context, string format, params object[] args)
		{
			if (this.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				string message = Logger.FormatMessageWithId(this.RunData.TenantId, format, args);
				this.Tracer.TraceError((long)((context != null) ? context.GetHashCode() : 0), message);
			}
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x000626CC File Offset: 0x000608CC
		public void TraceDebug(object context, string format, params object[] args)
		{
			if (this.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				string message = Logger.FormatMessageWithId(this.RunData.TenantId, format, args);
				this.Tracer.TraceDebug((long)((context != null) ? context.GetHashCode() : 0), message);
			}
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00062714 File Offset: 0x00060914
		public void SetMetadataValues(object context, RecipientType recipientType, string chunkId)
		{
			this.TraceDebug(context, "Entering SetMetadataValues - TaskName='{0}', RecipientType='{1}', ChunkId='{2}'", new object[]
			{
				this.TaskName,
				recipientType,
				chunkId
			});
			Logger.RegisterMetadata();
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			if (currentActivityScope != null && currentActivityScope.Status == ActivityContextStatus.ActivityStarted)
			{
				this.TraceDebug(context, "Setting metadata values TaskName='{0}', RecipientType='{1}', ChunkId='{2}', MailboxGuid='{3}', TenantGuid='{4}'", new object[]
				{
					this.TaskName,
					recipientType,
					chunkId,
					this.RunData.MailboxGuid,
					this.RunData.TenantGuid
				});
				currentActivityScope.SetProperty(DirectoryProcessorMetadata.TaskName, this.TaskName);
				currentActivityScope.SetProperty(DirectoryProcessorMetadata.RecipientType, recipientType.ToString());
				currentActivityScope.SetProperty(DirectoryProcessorMetadata.ChunkId, chunkId);
				currentActivityScope.SetProperty(DirectoryProcessorMetadata.MailboxGuid, this.RunData.MailboxGuid.ToString());
				currentActivityScope.SetProperty(DirectoryProcessorMetadata.TenantGuid, this.RunData.TenantGuid.ToString());
			}
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00062838 File Offset: 0x00060A38
		private static void RegisterMetadata()
		{
			if (!Logger.metadataRegistered)
			{
				lock (Logger.metadataLock)
				{
					if (!Logger.metadataRegistered)
					{
						ActivityContext.RegisterMetadata(typeof(DirectoryProcessorMetadata));
						Logger.metadataRegistered = true;
					}
				}
			}
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00062894 File Offset: 0x00060A94
		private static string FormatMessageWithId(string tenantId, string format, params object[] args)
		{
			string text = string.Empty;
			if (args != null)
			{
				text = string.Format(CultureInfo.InvariantCulture, format, args);
			}
			if (!string.IsNullOrEmpty(tenantId))
			{
				text = string.Format(CultureInfo.InvariantCulture, " Tenant {0} : {1}", new object[]
				{
					tenantId,
					text
				});
			}
			return text;
		}

		// Token: 0x04000A94 RID: 2708
		private static bool metadataRegistered = false;

		// Token: 0x04000A95 RID: 2709
		private static object metadataLock = new object();
	}
}
