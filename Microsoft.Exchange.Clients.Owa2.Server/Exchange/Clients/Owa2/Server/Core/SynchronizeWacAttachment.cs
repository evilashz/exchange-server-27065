using System;
using System.Threading;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000360 RID: 864
	internal class SynchronizeWacAttachment : ServiceCommand<IAsyncResult>
	{
		// Token: 0x06001BDF RID: 7135 RVA: 0x0006C260 File Offset: 0x0006A460
		public SynchronizeWacAttachment(CallContext callContext, string attachmentId, AsyncCallback asyncCallback, object asyncState) : base(callContext)
		{
			OwsLogRegistry.Register("SynchronizeWacAttachment", typeof(SynchronizeWacAttachmentMetadata), new Type[0]);
			this.attachmentId = attachmentId;
			this.asyncResult = new ServiceAsyncResult<SynchronizeWacAttachmentResponse>();
			this.asyncResult.AsyncCallback = asyncCallback;
			this.asyncResult.AsyncState = asyncState;
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x0006C2CD File Offset: 0x0006A4CD
		protected override IAsyncResult InternalExecute()
		{
			this.timer = new Timer(new TimerCallback(this.Retry), null, TimeSpan.Zero, this.retryInterval);
			return this.asyncResult;
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x0006C2F8 File Offset: 0x0006A4F8
		private void Retry(object unused)
		{
			CallContext.SetCurrent(base.CallContext);
			if (this.ReadyToSend())
			{
				this.Complete(SynchronizeWacAttachmentResult.Success);
				return;
			}
			this.checks++;
			if (this.checks > 10)
			{
				this.Complete(SynchronizeWacAttachmentResult.StillEditing);
			}
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x0006C334 File Offset: 0x0006A534
		private void Complete(SynchronizeWacAttachmentResult result)
		{
			if (this.timer != null)
			{
				this.timer.Dispose();
			}
			this.asyncResult.Data = new SynchronizeWacAttachmentResponse(result);
			this.asyncResult.Complete(this);
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x0006C368 File Offset: 0x0006A568
		private bool ReadyToSend()
		{
			bool result;
			lock (this.asyncResult)
			{
				if (this.asyncResult.IsCompleted)
				{
					result = true;
				}
				else
				{
					base.CallContext.ProtocolLog.Set(SynchronizeWacAttachmentMetadata.Count, this.checks);
					string primarySmtpAddress = base.CallContext.MailboxIdentityPrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
					string cacheKey = CachedAttachmentInfo.GetCacheKey(primarySmtpAddress, this.attachmentId);
					CachedAttachmentInfo fromCache = CachedAttachmentInfo.GetFromCache(cacheKey);
					if (fromCache == null)
					{
						base.CallContext.ProtocolLog.Set(SynchronizeWacAttachmentMetadata.Result, SynchronizeWacAttachment.AttachmentState.NoCachedInfo);
						result = true;
					}
					else
					{
						base.CallContext.ProtocolLog.Set(SynchronizeWacAttachmentMetadata.SessionId, fromCache.SessionId);
						if (fromCache.CobaltStore == null)
						{
							if (fromCache.LockCount == 0)
							{
								base.CallContext.ProtocolLog.Set(SynchronizeWacAttachmentMetadata.Result, SynchronizeWacAttachment.AttachmentState.NoLocks);
								result = true;
							}
							else
							{
								base.CallContext.ProtocolLog.Set(SynchronizeWacAttachmentMetadata.Result, SynchronizeWacAttachment.AttachmentState.HasLocks);
								result = false;
							}
						}
						else if (fromCache.CobaltStore.EditorCount == 0)
						{
							base.CallContext.ProtocolLog.Set(SynchronizeWacAttachmentMetadata.Result, SynchronizeWacAttachment.AttachmentState.NoEditors);
							result = true;
						}
						else
						{
							base.CallContext.ProtocolLog.Set(SynchronizeWacAttachmentMetadata.Result, SynchronizeWacAttachment.AttachmentState.HasEditors);
							result = false;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04000FCE RID: 4046
		private const string ActionName = "SynchronizeWacAttachment";

		// Token: 0x04000FCF RID: 4047
		private const int maxChecks = 10;

		// Token: 0x04000FD0 RID: 4048
		private readonly TimeSpan retryInterval = TimeSpan.FromSeconds(1.0);

		// Token: 0x04000FD1 RID: 4049
		private readonly string attachmentId;

		// Token: 0x04000FD2 RID: 4050
		private readonly ServiceAsyncResult<SynchronizeWacAttachmentResponse> asyncResult;

		// Token: 0x04000FD3 RID: 4051
		private Timer timer;

		// Token: 0x04000FD4 RID: 4052
		private int checks;

		// Token: 0x02000361 RID: 865
		public enum AttachmentState
		{
			// Token: 0x04000FD6 RID: 4054
			NoEditors,
			// Token: 0x04000FD7 RID: 4055
			NoCachedInfo,
			// Token: 0x04000FD8 RID: 4056
			NoLocks,
			// Token: 0x04000FD9 RID: 4057
			HasLocks,
			// Token: 0x04000FDA RID: 4058
			HasEditors
		}
	}
}
