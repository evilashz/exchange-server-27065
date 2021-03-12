using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200030F RID: 783
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetImItemsCommand : SingleStepServiceCommand<GetImItemsRequest, ImItemList>
	{
		// Token: 0x06001619 RID: 5657 RVA: 0x000727E7 File Offset: 0x000709E7
		public GetImItemsCommand(CallContext callContext, GetImItemsRequest request) : base(callContext, request)
		{
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x000727F1 File Offset: 0x000709F1
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetImItemsResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0007281C File Offset: 0x00070A1C
		internal override ServiceResult<ImItemList> Execute()
		{
			MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			ExtendedPropertyUri[] extendedProperties = base.Request.ExtendedProperties;
			RawImItemList rawImItemList = new GetImItems(mailboxIdentityMailboxSession, this.GetContactIds(), this.GetGroupIds(), extendedProperties, new XSOFactory()).Execute();
			return new ServiceResult<ImItemList>(ImItemList.LoadFrom(rawImItemList, extendedProperties, mailboxIdentityMailboxSession));
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00072871 File Offset: 0x00070A71
		private StoreId[] GetContactIds()
		{
			return this.ConvertItemIdsToStoreIds(base.Request.ContactIds);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00072884 File Offset: 0x00070A84
		private StoreId[] GetGroupIds()
		{
			return this.ConvertItemIdsToStoreIds(base.Request.GroupIds);
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x00072898 File Offset: 0x00070A98
		private StoreId[] ConvertItemIdsToStoreIds(ItemId[] toConvert)
		{
			if (toConvert == null)
			{
				return null;
			}
			List<StoreId> list = new List<StoreId>(toConvert.Length);
			foreach (ItemId baseItemId in toConvert)
			{
				try
				{
					IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(baseItemId);
					list.Add(idAndSession.Id);
				}
				catch (InvalidStoreIdException)
				{
				}
				catch (ObjectNotFoundException)
				{
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return list.ToArray();
		}
	}
}
