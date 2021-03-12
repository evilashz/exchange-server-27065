using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200030B RID: 779
	internal sealed class GetHoldOnMailboxes : SingleStepServiceCommand<GetHoldOnMailboxesRequest, MailboxHoldResult>, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600160A RID: 5642 RVA: 0x00072558 File Offset: 0x00070758
		public GetHoldOnMailboxes(CallContext callContext, GetHoldOnMailboxesRequest request) : base(callContext, request)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.holdId = request.HoldId;
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x0007257A File Offset: 0x0007077A
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<GetHoldOnMailboxes>(this);
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x00072582 File Offset: 0x00070782
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00072597 File Offset: 0x00070797
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x000725B9 File Offset: 0x000707B9
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetHoldOnMailboxesResponse(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x000725E1 File Offset: 0x000707E1
		internal override ServiceResult<MailboxHoldResult> Execute()
		{
			MailboxSearchHelper.PerformCommonAuthorization(base.CallContext.IsExternalUser, out this.runspaceConfig, out this.recipientSession);
			return this.ProcessRequest();
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00072605 File Offset: 0x00070805
		private void Dispose(bool fromDispose)
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			if (!this.disposed)
			{
				this.disposed = true;
			}
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x0007262C File Offset: 0x0007082C
		private ServiceResult<MailboxHoldResult> ProcessRequest()
		{
			List<MailboxHoldStatus> list = new List<MailboxHoldStatus>();
			DiscoverySearchDataProvider discoverySearchDataProvider = new DiscoverySearchDataProvider(this.recipientSession.SessionSettings.CurrentOrganizationId);
			MailboxDiscoverySearch mailboxDiscoverySearch = discoverySearchDataProvider.FindByAlternativeId<MailboxDiscoverySearch>(this.holdId);
			if (mailboxDiscoverySearch == null)
			{
				ExTraceGlobals.SearchTracer.TraceError<string>((long)this.GetHashCode(), "Specific hold id: {0} is not found in the system.", this.holdId);
				throw new MailboxHoldNotFoundException(CoreResources.IDs.ErrorHoldIsNotFound);
			}
			MailboxSearchHelper.GetMailboxHoldStatuses(mailboxDiscoverySearch, this.recipientSession, list);
			return new ServiceResult<MailboxHoldResult>(new MailboxHoldResult(this.holdId, mailboxDiscoverySearch.Query, list.ToArray()));
		}

		// Token: 0x04000ED2 RID: 3794
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000ED3 RID: 3795
		private bool disposed;

		// Token: 0x04000ED4 RID: 3796
		private string holdId;

		// Token: 0x04000ED5 RID: 3797
		private ExchangeRunspaceConfiguration runspaceConfig;

		// Token: 0x04000ED6 RID: 3798
		private IRecipientSession recipientSession;
	}
}
