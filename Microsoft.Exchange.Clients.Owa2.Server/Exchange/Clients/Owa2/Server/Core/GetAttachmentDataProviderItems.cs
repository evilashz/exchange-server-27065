using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000309 RID: 777
	internal class GetAttachmentDataProviderItems : ServiceCommand<GetAttachmentDataProviderItemsResponse>
	{
		// Token: 0x060019E7 RID: 6631 RVA: 0x0005D4C0 File Offset: 0x0005B6C0
		public GetAttachmentDataProviderItems(CallContext callContext, GetAttachmentDataProviderItemsRequest request) : base(callContext)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (string.IsNullOrEmpty(request.AttachmentDataProviderId))
			{
				throw new ArgumentException("The parameter cannot be null or empty.", "attachmentDataProviderId");
			}
			if (request.Paging == null)
			{
				request.Paging = new AttachmentItemsPagingDetails
				{
					Sort = new AttachmentItemsSort
					{
						SortColumn = AttachmentItemsSortColumn.Name,
						SortOrder = AttachmentItemsSortOrder.Descending
					}
				};
			}
			this.request = request;
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x0005D538 File Offset: 0x0005B738
		protected override GetAttachmentDataProviderItemsResponse InternalExecute()
		{
			UserContext userContext = UserContextManager.GetUserContext(base.CallContext.HttpContext, base.CallContext.EffectiveCaller, true);
			AttachmentDataProvider provider = userContext.AttachmentDataProviderManager.GetProvider(base.CallContext, this.request.AttachmentDataProviderId);
			return provider.GetItems(this.request.Paging, this.request.Scope, base.MailboxIdentityMailboxSession);
		}

		// Token: 0x04000E54 RID: 3668
		private GetAttachmentDataProviderItemsRequest request;
	}
}
