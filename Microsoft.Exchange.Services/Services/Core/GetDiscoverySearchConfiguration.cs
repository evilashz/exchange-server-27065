using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000306 RID: 774
	internal sealed class GetDiscoverySearchConfiguration : SingleStepServiceCommand<GetDiscoverySearchConfigurationRequest, DiscoverySearchConfiguration[]>, IDisposeTrackable, IDisposable
	{
		// Token: 0x060015F2 RID: 5618 RVA: 0x00071DA5 File Offset: 0x0006FFA5
		public GetDiscoverySearchConfiguration(CallContext callContext, GetDiscoverySearchConfigurationRequest request) : base(callContext, request)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.SaveRequestData(request);
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00071DC2 File Offset: 0x0006FFC2
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<GetDiscoverySearchConfiguration>(this);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00071DCA File Offset: 0x0006FFCA
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00071DDF File Offset: 0x0006FFDF
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00071E01 File Offset: 0x00070001
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetDiscoverySearchConfigurationResponse(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00071E2C File Offset: 0x0007002C
		internal override ServiceResult<DiscoverySearchConfiguration[]> Execute()
		{
			MailboxSearchHelper.PerformCommonAuthorization(base.CallContext.IsExternalUser, out this.runspaceConfig, out this.recipientSession);
			DiscoverySearchDataProvider discoverySearchDataProvider = new DiscoverySearchDataProvider(this.recipientSession.SessionSettings.CurrentOrganizationId);
			List<DiscoverySearchConfiguration> list = new List<DiscoverySearchConfiguration>();
			if (!string.IsNullOrEmpty(this.searchId))
			{
				MailboxDiscoverySearch mailboxDiscoverySearch = discoverySearchDataProvider.Find<MailboxDiscoverySearch>(this.searchId);
				if (mailboxDiscoverySearch == null)
				{
					throw new ServiceArgumentException((CoreResources.IDs)2524429953U);
				}
				list.Add(this.QueryDiscoverySearchConfiguration(mailboxDiscoverySearch));
			}
			else
			{
				foreach (MailboxDiscoverySearch searchObject in discoverySearchDataProvider.GetAll<MailboxDiscoverySearch>())
				{
					list.Add(this.QueryDiscoverySearchConfiguration(searchObject));
				}
			}
			return new ServiceResult<DiscoverySearchConfiguration[]>(list.ToArray());
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00071F04 File Offset: 0x00070104
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

		// Token: 0x060015F9 RID: 5625 RVA: 0x00071F28 File Offset: 0x00070128
		private void SaveRequestData(GetDiscoverySearchConfigurationRequest request)
		{
			this.searchId = request.SearchId;
			this.expandGroupMembership = request.ExpandGroupMembership;
			this.inPlaceHoldConfigurationOnly = request.InPlaceHoldConfigurationOnly;
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00071F50 File Offset: 0x00070150
		private DiscoverySearchConfiguration QueryDiscoverySearchConfiguration(MailboxDiscoverySearch searchObject)
		{
			if (this.inPlaceHoldConfigurationOnly)
			{
				return new DiscoverySearchConfiguration(searchObject.Name, null, searchObject.CalculatedQuery, searchObject.InPlaceHoldIdentity, searchObject.ManagedByOrganization, searchObject.Language);
			}
			List<FailedSearchMailbox> list;
			List<SearchableMailbox> searchableMailboxes = MailboxSearchHelper.GetSearchableMailboxes(searchObject, this.expandGroupMembership, this.recipientSession, this.runspaceConfig, out list);
			return new DiscoverySearchConfiguration(searchObject.Name, searchableMailboxes.ToArray(), searchObject.CalculatedQuery, null, null, searchObject.Language);
		}

		// Token: 0x04000EC7 RID: 3783
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000EC8 RID: 3784
		private bool disposed;

		// Token: 0x04000EC9 RID: 3785
		private string searchId;

		// Token: 0x04000ECA RID: 3786
		private bool expandGroupMembership;

		// Token: 0x04000ECB RID: 3787
		private bool inPlaceHoldConfigurationOnly;

		// Token: 0x04000ECC RID: 3788
		private ExchangeRunspaceConfiguration runspaceConfig;

		// Token: 0x04000ECD RID: 3789
		private IRecipientSession recipientSession;
	}
}
