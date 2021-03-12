using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000326 RID: 806
	internal sealed class GetRooms : SingleStepServiceCommand<GetRoomsRequest, EwsRoomType[]>
	{
		// Token: 0x060016C5 RID: 5829 RVA: 0x00078A54 File Offset: 0x00076C54
		private static QueryFilter GetRoomListFilter(string roomListAddress)
		{
			return new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, "SMTP:" + roomListAddress),
				GetRooms.RecipientTypeFilter,
				GetRooms.RecipientRoomListsFilter
			});
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00078A97 File Offset: 0x00076C97
		public GetRooms(CallContext callContext, GetRoomsRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00078AA1 File Offset: 0x00076CA1
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetRoomsResponse(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00078ACC File Offset: 0x00076CCC
		internal override ServiceResult<EwsRoomType[]> Execute()
		{
			if (base.CallContext.AccessingPrincipal == null)
			{
				throw new NameResolutionNoMailboxException();
			}
			this.roomListEmailAddress = base.Request.RoomList.EmailAddress;
			int num = 0;
			if (base.CallContext.AccessingPrincipal.PreferredCultures.Any<CultureInfo>())
			{
				num = LocaleMap.GetLcidFromCulture(base.CallContext.AccessingPrincipal.PreferredCultures.First<CultureInfo>());
			}
			ExchangePrincipal accessingPrincipal = base.CallContext.AccessingPrincipal;
			IRecipientSession recipientSession = Directory.CreateAddressListScopedADRecipientSessionForOrganization(DirectoryHelper.GetGlobalAddressListFromAddressBookPolicy(accessingPrincipal.MailboxInfo.Configuration.AddressBookPolicy, base.MailboxIdentityMailboxSession.GetADConfigurationSession(true, ConsistencyMode.IgnoreInvalid)), base.CallContext.AccessingADUser.QueryBaseDN, num, accessingPrincipal.MailboxInfo.OrganizationId, base.CallContext.EffectiveCaller.ClientSecurityContext);
			QueryFilter roomListFilter = GetRooms.GetRoomListFilter(this.roomListEmailAddress);
			ADRecipient[] array = recipientSession.Find(null, QueryScope.SubTree, roomListFilter, new SortBy(ADRecipientSchema.DisplayName, SortOrder.Ascending), 0);
			if (array.Length != 1)
			{
				throw new NameResolutionNoResultsException();
			}
			ADGroup adgroup = array[0] as ADGroup;
			if (adgroup == null)
			{
				throw new NameResolutionNoResultsException();
			}
			ADPagedReader<ADRawEntry> rooms = this.expandRoomListDG(adgroup, accessingPrincipal, num);
			return new ServiceResult<EwsRoomType[]>(GetRooms.GetRoomTypes(rooms, this.GetHashCode()).ToArray());
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x00078C00 File Offset: 0x00076E00
		public static List<EwsRoomType> GetRoomTypes(IEnumerable<ADRawEntry> rooms, int callerHashCode)
		{
			int num = 0;
			List<EwsRoomType> list = new List<EwsRoomType>();
			foreach (ADRawEntry adrawEntry in rooms)
			{
				bool flag = (bool)adrawEntry[ADRecipientSchema.HiddenFromAddressListsEnabled];
				if (flag)
				{
					ExTraceGlobals.GetRoomsCallTracer.TraceDebug((long)callerHashCode, "RoomList address is hidden.");
				}
				else
				{
					MailboxHelper.MailboxTypeType mailboxTypeType = MailboxHelper.ConvertToMailboxType((RecipientType)adrawEntry[ADRecipientSchema.RecipientType], (RecipientTypeDetails)adrawEntry[ADRecipientSchema.RecipientTypeDetails]);
					if (mailboxTypeType == MailboxHelper.MailboxTypeType.Mailbox || mailboxTypeType == MailboxHelper.MailboxTypeType.Contact)
					{
						EmailAddressWrapper emailAddressWrapper = new EmailAddressWrapper();
						emailAddressWrapper.Name = (string)adrawEntry[ADRecipientSchema.DisplayName];
						emailAddressWrapper.EmailAddress = ((SmtpAddress)adrawEntry[ADRecipientSchema.PrimarySmtpAddress]).ToString();
						emailAddressWrapper.RoutingType = "SMTP";
						emailAddressWrapper.MailboxType = mailboxTypeType.ToString();
						list.Add(new EwsRoomType
						{
							Mailbox = emailAddressWrapper
						});
						num++;
						if (num >= 100)
						{
							break;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x00078D38 File Offset: 0x00076F38
		private ADPagedReader<ADRawEntry> expandRoomListDG(ADGroup group, ExchangePrincipal exchangePrincipal, int directoryLcid)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, exchangePrincipal.MailboxInfo.OrganizationId.ToADSessionSettings(), 204, "expandRoomListDG", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\servicecommands\\getrooms.cs");
			AddressBookBase allRoomsAddressList = AddressBookBase.GetAllRoomsAddressList(base.CallContext.EffectiveCaller.ClientSecurityContext, tenantOrTopologyConfigurationSession, exchangePrincipal.MailboxInfo.Configuration.AddressBookPolicy);
			IRecipientSession recipientSession = Directory.CreateAddressListScopedADRecipientSessionForOrganization(allRoomsAddressList, base.CallContext.AccessingADUser.QueryBaseDN, directoryLcid, exchangePrincipal.MailboxInfo.OrganizationId, base.CallContext.EffectiveCaller.ClientSecurityContext);
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MemberOfGroup, group.Id);
			return recipientSession.FindPagedADRawEntry(null, QueryScope.SubTree, filter, new SortBy(ADRecipientSchema.DisplayName, SortOrder.Ascending), 100, ExpandDL.DirectoryDLDefaultProps);
		}

		// Token: 0x04000F5D RID: 3933
		private const int MaxNumberReturnedRooms = 100;

		// Token: 0x04000F5E RID: 3934
		private static readonly QueryFilter RecipientTypeFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.MailUniversalDistributionGroup);

		// Token: 0x04000F5F RID: 3935
		private static readonly QueryFilter RecipientRoomListsFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.RoomList);

		// Token: 0x04000F60 RID: 3936
		private string roomListEmailAddress;
	}
}
