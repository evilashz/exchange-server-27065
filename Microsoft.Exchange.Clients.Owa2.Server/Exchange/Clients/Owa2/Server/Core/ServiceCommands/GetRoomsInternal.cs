using System;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x02000325 RID: 805
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetRoomsInternal : ServiceCommand<GetRoomsResponse>
	{
		// Token: 0x06001ABD RID: 6845 RVA: 0x00064F53 File Offset: 0x00063153
		public GetRoomsInternal(CallContext callContext, string roomList) : base(callContext)
		{
			this.roomList = roomList;
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00064F64 File Offset: 0x00063164
		protected override GetRoomsResponse InternalExecute()
		{
			ServiceResult<EwsRoomType[]> serviceResult = (this.roomList != null) ? this.GetSpecifiedRooms() : this.GetAllAvailableRooms();
			return new GetRoomsResponse(serviceResult.Code, serviceResult.Error, serviceResult.Value);
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00064FA0 File Offset: 0x000631A0
		private ServiceResult<EwsRoomType[]> GetSpecifiedRooms()
		{
			GetRoomsRequest getRoomsRequest = new GetRoomsRequest();
			getRoomsRequest.RoomList = new EmailAddressWrapper
			{
				EmailAddress = this.roomList
			};
			GetRooms getRooms = new GetRooms(base.CallContext, getRoomsRequest);
			getRooms.PreExecute();
			ServiceResult<EwsRoomType[]> serviceResult = getRooms.Execute();
			getRooms.SetCurrentStepResult(serviceResult);
			getRooms.PostExecute();
			return serviceResult;
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00064FF8 File Offset: 0x000631F8
		private ServiceResult<EwsRoomType[]> GetAllAvailableRooms()
		{
			UserContext userContext = UserContextManager.GetUserContext(base.CallContext.HttpContext, base.CallContext.EffectiveCaller, true);
			QueryFilter filter = null;
			AddressLists addressLists = new AddressLists(base.CallContext.EffectiveCaller.ClientSecurityContext, base.MailboxIdentityMailboxSession.MailboxOwner, userContext);
			ADSessionSettings sessionSettings;
			if (addressLists.AllRoomsAddressList == null)
			{
				filter = new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientDisplayType, RecipientDisplayType.ConferenceRoomMailbox),
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientDisplayType, RecipientDisplayType.SyncedConferenceRoomMailbox)
				});
				sessionSettings = ADSessionSettings.FromOrganizationIdWithAddressListScopeServiceOnly(addressLists.GlobalAddressList.OrganizationId, addressLists.GlobalAddressList.Id);
			}
			else
			{
				sessionSettings = ADSessionSettings.FromOrganizationIdWithAddressListScopeServiceOnly(addressLists.AllRoomsAddressList.OrganizationId, addressLists.AllRoomsAddressList.Id);
			}
			int lcid = 0;
			CultureInfo cultureInfo = base.CallContext.AccessingPrincipal.PreferredCultures.FirstOrDefault<CultureInfo>();
			if (cultureInfo != null)
			{
				lcid = cultureInfo.LCID;
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, null, lcid, true, ConsistencyMode.IgnoreInvalid, null, sessionSettings, 123, "GetAllAvailableRooms", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\ServiceCommands\\GetRoomsInternal.cs");
			ADRecipient[] rooms = tenantOrRootOrgRecipientSession.Find(null, QueryScope.SubTree, filter, new SortBy(ADRecipientSchema.DisplayName, SortOrder.Ascending), 100);
			return new ServiceResult<EwsRoomType[]>(GetRooms.GetRoomTypes(rooms, this.GetHashCode()).ToArray());
		}

		// Token: 0x04000ED6 RID: 3798
		private const int MaxNumberOfRoomsToReturn = 100;

		// Token: 0x04000ED7 RID: 3799
		private readonly string roomList;
	}
}
