using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200031A RID: 794
	internal sealed class GetNonIndexableItemStatistics : SingleStepServiceCommand<GetNonIndexableItemStatisticsRequest, NonIndexableItemStatisticResult[]>, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600167C RID: 5756 RVA: 0x00076104 File Offset: 0x00074304
		public GetNonIndexableItemStatistics(CallContext callContext, GetNonIndexableItemStatisticsRequest request) : base(callContext, request)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.mailboxes = request.Mailboxes;
			this.searchArchiveOnly = request.SearchArchiveOnly;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00076132 File Offset: 0x00074332
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<GetNonIndexableItemStatistics>(this);
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0007613A File Offset: 0x0007433A
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0007614F File Offset: 0x0007434F
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00076171 File Offset: 0x00074371
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetNonIndexableItemStatisticsResponse(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00076199 File Offset: 0x00074399
		internal override ServiceResult<NonIndexableItemStatisticResult[]> Execute()
		{
			MailboxSearchHelper.PerformCommonAuthorization(base.CallContext.IsExternalUser, out this.runspaceConfig, out this.recipientSession);
			return this.ProcessRequest();
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x000761BD File Offset: 0x000743BD
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

		// Token: 0x06001683 RID: 5763 RVA: 0x000761E4 File Offset: 0x000743E4
		private ServiceResult<NonIndexableItemStatisticResult[]> ProcessRequest()
		{
			if (this.mailboxes.Length != 1)
			{
				throw new ServiceArgumentException((CoreResources.IDs)4136809189U);
			}
			Dictionary<string, ADRawEntry> dictionary = MailboxSearchHelper.FindADEntriesByLegacyExchangeDNs(this.recipientSession, this.mailboxes, MailboxSearchHelper.AdditionalProperties);
			if (dictionary != null)
			{
				foreach (KeyValuePair<string, ADRawEntry> keyValuePair in dictionary)
				{
					if (!MailboxSearchHelper.HasPermissionToSearchMailbox(this.runspaceConfig, keyValuePair.Value))
					{
						throw new ServiceInvalidOperationException((CoreResources.IDs)2354781453U);
					}
				}
			}
			List<NonIndexableItemStatisticResult> list = new List<NonIndexableItemStatisticResult>(this.mailboxes.Length);
			CallerInfo callerInfo = new CallerInfo(MailboxSearchHelper.IsOpenAsAdmin(base.CallContext), MailboxSearchConverter.GetCommonAccessToken(base.CallContext), base.CallContext.EffectiveCaller.ClientSecurityContext, base.CallContext.EffectiveCaller.PrimarySmtpAddress, this.recipientSession.SessionSettings.CurrentOrganizationId, string.Empty, MailboxSearchHelper.GetQueryCorrelationId(), MailboxSearchHelper.GetUserRolesFromAuthZClientInfo(base.CallContext.EffectiveCaller), MailboxSearchHelper.GetApplicationRolesFromAuthZClientInfo(base.CallContext.EffectiveCaller));
			NonIndexableItemStatisticsProvider nonIndexableItemStatisticsProvider = new NonIndexableItemStatisticsProvider(this.recipientSession, MailboxSearchHelper.GetTimeZone(), callerInfo, this.recipientSession.SessionSettings.CurrentOrganizationId, this.mailboxes, this.searchArchiveOnly);
			nonIndexableItemStatisticsProvider.ExecuteSearch();
			foreach (NonIndexableItemStatisticsInfo nonIndexableItemStatisticsInfo in nonIndexableItemStatisticsProvider.Results)
			{
				list.Add(new NonIndexableItemStatisticResult
				{
					Mailbox = nonIndexableItemStatisticsInfo.Mailbox,
					ItemCount = (long)nonIndexableItemStatisticsInfo.ItemCount,
					ErrorMessage = nonIndexableItemStatisticsInfo.ErrorMessage
				});
			}
			return new ServiceResult<NonIndexableItemStatisticResult[]>((list.Count == 0) ? null : list.ToArray());
		}

		// Token: 0x04000F1C RID: 3868
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000F1D RID: 3869
		private readonly string[] mailboxes;

		// Token: 0x04000F1E RID: 3870
		private readonly bool searchArchiveOnly;

		// Token: 0x04000F1F RID: 3871
		private bool disposed;

		// Token: 0x04000F20 RID: 3872
		private ExchangeRunspaceConfiguration runspaceConfig;

		// Token: 0x04000F21 RID: 3873
		private IRecipientSession recipientSession;
	}
}
