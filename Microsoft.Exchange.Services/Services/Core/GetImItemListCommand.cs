using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200030D RID: 781
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetImItemListCommand : SingleStepServiceCommand<GetImItemListRequest, ImItemList>
	{
		// Token: 0x06001614 RID: 5652 RVA: 0x000726F1 File Offset: 0x000708F1
		public GetImItemListCommand(CallContext callContext, GetImItemListRequest request) : base(callContext, request)
		{
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x000726FB File Offset: 0x000708FB
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetImItemListResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x00072724 File Offset: 0x00070924
		internal override ServiceResult<ImItemList> Execute()
		{
			MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			UnifiedContactStoreUtilities unifiedContactStoreUtilities = new UnifiedContactStoreUtilities(mailboxIdentityMailboxSession, new XSOFactory(), Global.UnifiedContactStoreConfiguration);
			ExtendedPropertyUri[] extendedProperties = base.Request.ExtendedProperties;
			ImItemList value = new GetImItemList(extendedProperties, unifiedContactStoreUtilities).Execute();
			return new ServiceResult<ImItemList>(value);
		}
	}
}
