using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000325 RID: 805
	internal sealed class GetRoomLists : SingleStepServiceCommand<GetRoomListsRequest, EmailAddressWrapper[]>
	{
		// Token: 0x060016C0 RID: 5824 RVA: 0x000787F1 File Offset: 0x000769F1
		public GetRoomLists(CallContext callContext, GetRoomListsRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x000787FB File Offset: 0x000769FB
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetRoomListsResponse(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00078824 File Offset: 0x00076A24
		internal override ServiceResult<EmailAddressWrapper[]> Execute()
		{
			if (base.CallContext.AccessingPrincipal == null)
			{
				throw new NameResolutionNoMailboxException();
			}
			int lcid = 0;
			if (base.CallContext.AccessingPrincipal.PreferredCultures.Any<CultureInfo>())
			{
				lcid = base.CallContext.AccessingPrincipal.PreferredCultures.First<CultureInfo>().LCID;
			}
			ExchangePrincipal accessingPrincipal = base.CallContext.AccessingPrincipal;
			IRecipientSession recipientSession = Directory.CreateAddressListScopedADRecipientSessionForOrganization(DirectoryHelper.GetGlobalAddressListFromAddressBookPolicy(accessingPrincipal.MailboxInfo.Configuration.AddressBookPolicy, base.MailboxIdentityMailboxSession.GetADConfigurationSession(true, ConsistencyMode.IgnoreInvalid)), base.CallContext.AccessingADUser.QueryBaseDN, lcid, accessingPrincipal.MailboxInfo.OrganizationId, base.CallContext.EffectiveCaller.ClientSecurityContext);
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.RoomList);
			ADPagedReader<ADRecipient> roomLists = recipientSession.FindPaged(null, QueryScope.SubTree, filter, new SortBy(ADRecipientSchema.DisplayName, SortOrder.Ascending), 0);
			return new ServiceResult<EmailAddressWrapper[]>(this.GenerateXmlForRoomList(roomLists).ToArray<EmailAddressWrapper>());
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00078920 File Offset: 0x00076B20
		private IEnumerable<EmailAddressWrapper> GenerateXmlForRoomList(IEnumerable<ADRecipient> roomLists)
		{
			List<EmailAddressWrapper> list = new List<EmailAddressWrapper>();
			foreach (ADRecipient adrecipient in roomLists)
			{
				MailboxHelper.MailboxTypeType mailboxTypeType = MailboxHelper.ConvertToMailboxType(adrecipient.RecipientType, adrecipient.RecipientTypeDetails);
				if (mailboxTypeType == MailboxHelper.MailboxTypeType.PublicDL)
				{
					SmtpAddress primarySmtpAddress = adrecipient.PrimarySmtpAddress;
					list.Add(new EmailAddressWrapper
					{
						Name = (string)adrecipient[ADRecipientSchema.DisplayName],
						EmailAddress = primarySmtpAddress.ToString(),
						RoutingType = "SMTP",
						MailboxType = mailboxTypeType.ToString()
					});
				}
				else
				{
					ExTraceGlobals.GetRoomsCallTracer.TraceDebug((long)this.GetHashCode(), "MailboxType is not a PublicDL");
				}
			}
			list = list.OrderBy((EmailAddressWrapper roomList) => roomList.Name, StringComparer.CurrentCulture).ToList<EmailAddressWrapper>();
			ExTraceGlobals.GetRoomsCallTracer.TraceDebug((long)this.GetHashCode(), "RoomLists length is:" + list.Count);
			return list;
		}
	}
}
