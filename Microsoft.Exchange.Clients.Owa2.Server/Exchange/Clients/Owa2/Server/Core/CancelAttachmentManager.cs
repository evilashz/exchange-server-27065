using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200003B RID: 59
	internal class CancelAttachmentManager
	{
		// Token: 0x06000168 RID: 360 RVA: 0x00005A4D File Offset: 0x00003C4D
		public CancelAttachmentManager(UserContext userContext)
		{
			this.lockObject = new object();
			this.userContext = userContext;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00005A67 File Offset: 0x00003C67
		private Dictionary<string, CancelAttachmentManager.CancellationItem> CancellationItems
		{
			get
			{
				if (this.cancellationItems == null)
				{
					this.cancellationItems = new Dictionary<string, CancelAttachmentManager.CancellationItem>();
				}
				return this.cancellationItems;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005A84 File Offset: 0x00003C84
		public bool OnCreateAttachment(string cancellationId, CancellationTokenSource tokenSource)
		{
			bool result = false;
			lock (this.lockObject)
			{
				if (this.CancellationItems.ContainsKey(cancellationId) && this.CancellationItems[cancellationId].IsCancellationRequested)
				{
					OwaServerTraceLogger.AppendToLog(new TraceLogEvent("CancelAttachmentManager.OnCreateAttachment", this.userContext, "OnCreateAttachment", string.Format("Attachment was cancelled before CreateAttachment, CancellationId: {0}.", cancellationId)));
					result = true;
				}
				else
				{
					CancelAttachmentManager.CancellationItem cancellationItem = new CancelAttachmentManager.CancellationItem();
					cancellationItem.CancellationId = cancellationId;
					cancellationItem.CancellationTokenSource = tokenSource;
					cancellationItem.AttachmentCompletedEvent = new ManualResetEvent(false);
					this.CancellationItems[cancellationId] = cancellationItem;
				}
			}
			return result;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005B38 File Offset: 0x00003D38
		public void CreateAttachmentCancelled(string cancellationId)
		{
			OwaServerTraceLogger.AppendToLog(new TraceLogEvent("CancelAttachmentManager.CreateAttachmentCancelled", this.userContext, "CreateAttachmentCancelled", string.Format("CreateAttachment cancelled for CancellationId: {0}.", cancellationId)));
			if (cancellationId == null)
			{
				return;
			}
			this.CancellationItems[cancellationId].AttachmentCompletedEvent.Set();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005B88 File Offset: 0x00003D88
		public void CreateAttachmentCompleted(string cancellationId, AttachmentIdType attachmentId)
		{
			OwaServerTraceLogger.AppendToLog(new TraceLogEvent("CancelAttachmentManager.CreateAttachmentCompleted", this.userContext, "CreateAttachmentCompleted", string.Format("CreateAttachment completed for AttachmentId: {0}, CancellationId: {1}.", (attachmentId == null) ? "Null" : attachmentId.Id, string.IsNullOrEmpty(cancellationId) ? "Null" : cancellationId)));
			if (cancellationId == null)
			{
				return;
			}
			this.CancellationItems[cancellationId].AttachmentId = attachmentId;
			this.CancellationItems[cancellationId].AttachmentCompletedEvent.Set();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005C08 File Offset: 0x00003E08
		public bool CancelAttachment(string cancellationId, int timeout)
		{
			CancelAttachmentManager.CancellationItem cancellationItem;
			lock (this.lockObject)
			{
				if (!this.CancellationItems.ContainsKey(cancellationId))
				{
					OwaServerTraceLogger.AppendToLog(new TraceLogEvent("CancelAttachmentManager.CancelAttachment", this.userContext, "CancelAttachment", string.Format("CreateAttachment has not started, marking item as cancelled for CancellationId: {0}", cancellationId)));
					cancellationItem = new CancelAttachmentManager.CancellationItem();
					cancellationItem.CancellationId = cancellationId;
					cancellationItem.IsCancellationRequested = true;
					this.cancellationItems.Add(cancellationId, cancellationItem);
					return true;
				}
				cancellationItem = this.CancellationItems[cancellationId];
				cancellationItem.IsCancellationRequested = true;
				if (cancellationItem.CancellationTokenSource != null)
				{
					cancellationItem.CancellationTokenSource.Cancel();
				}
			}
			if (cancellationItem.AttachmentId == null && cancellationItem.AttachmentCompletedEvent != null && !cancellationItem.AttachmentCompletedEvent.WaitOne(timeout))
			{
				OwaServerTraceLogger.AppendToLog(new TraceLogEvent("CancelAttachmentManager.CancelAttachment", this.userContext, "CancelAttachment", string.Format("Cancel attachment timed out for CancellationId: {0}", cancellationId)));
				return false;
			}
			if (cancellationItem.AttachmentId == null)
			{
				OwaServerTraceLogger.AppendToLog(new TraceLogEvent("CancelAttachmentManager.CancelAttachment", this.userContext, "CancelAttachment", string.Format("AttachmentId not found. Returning success for cancellationId: {0}", cancellationId)));
				return true;
			}
			bool flag2 = AttachmentUtilities.DeleteAttachment(cancellationItem.AttachmentId);
			OwaServerTraceLogger.AppendToLog(new TraceLogEvent("CancelAttachmentManager.CancelAttachment", this.userContext, "CancelAttachment", string.Format("DeleteAttachmentSucceeded = {0} for CancellationId: {1}, AttachmentId: {2}", flag2, cancellationId, cancellationItem.AttachmentId.Id)));
			return flag2;
		}

		// Token: 0x04000085 RID: 133
		private Dictionary<string, CancelAttachmentManager.CancellationItem> cancellationItems;

		// Token: 0x04000086 RID: 134
		private object lockObject;

		// Token: 0x04000087 RID: 135
		private UserContext userContext;

		// Token: 0x0200003C RID: 60
		private class CancellationItem
		{
			// Token: 0x04000088 RID: 136
			internal string CancellationId;

			// Token: 0x04000089 RID: 137
			internal bool IsCancellationRequested;

			// Token: 0x0400008A RID: 138
			internal AttachmentIdType AttachmentId;

			// Token: 0x0400008B RID: 139
			internal ManualResetEvent AttachmentCompletedEvent;

			// Token: 0x0400008C RID: 140
			internal CancellationTokenSource CancellationTokenSource;
		}
	}
}
