using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200092C RID: 2348
	internal class ModernGroupMembershipRequestMessageDetailsCommand : ServiceCommand<ModernGroupMembershipRequestMessageDetailsResponse>
	{
		// Token: 0x060043FF RID: 17407 RVA: 0x000E8112 File Offset: 0x000E6312
		public ModernGroupMembershipRequestMessageDetailsCommand(CallContext callContext, ModernGroupMembershipRequestMessageDetailsRequest request) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(request, "request", "ModernGroupMembershipRequestMessageDetailsCommand::ModernGroupMembershipRequestMessageDetailsCommand");
			this.request = request;
			this.request.Validate();
		}

		// Token: 0x06004400 RID: 17408 RVA: 0x000E8158 File Offset: 0x000E6358
		protected override ModernGroupMembershipRequestMessageDetailsResponse InternalExecute()
		{
			ModernGroupMembershipRequestMessageDetailsResponse result = null;
			MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			IRecipientSession adrecipientSession = base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
			using (GroupMailboxJoinRequestMessageItem groupMailboxJoinRequestMessageItem = GroupMailboxJoinRequestMessageItem.Bind(mailboxIdentityMailboxSession, this.request.MessageStoreId))
			{
				ADUser aduser = adrecipientSession.FindByProxyAddress(new SmtpProxyAddress(groupMailboxJoinRequestMessageItem.GroupSmtpAddress, true)) as ADUser;
				ModernGroupMembershipRequestMessageDetailsResponse modernGroupMembershipRequestMessageDetailsResponse = new ModernGroupMembershipRequestMessageDetailsResponse();
				modernGroupMembershipRequestMessageDetailsResponse.GroupPersona = this.GetGroupPersona(aduser);
				modernGroupMembershipRequestMessageDetailsResponse.IsOwner = aduser.Owners.Any((ADObjectId owner) => owner.Equals(base.CallContext.AccessingADUser.ObjectId));
				result = modernGroupMembershipRequestMessageDetailsResponse;
			}
			return result;
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x000E8214 File Offset: 0x000E6414
		private Persona GetGroupPersona(ADUser groupADUser)
		{
			return new Persona
			{
				PersonaId = IdConverter.PersonaIdFromADObjectId(groupADUser.ObjectId.ObjectGuid),
				ADObjectId = groupADUser.ObjectId.ObjectGuid,
				DisplayName = groupADUser.DisplayName,
				Alias = groupADUser.Alias,
				PersonaType = PersonaTypeConverter.ToString(PersonType.ModernGroup),
				EmailAddress = new EmailAddressWrapper
				{
					Name = (groupADUser.DisplayName ?? string.Empty),
					EmailAddress = groupADUser.PrimarySmtpAddress.ToString(),
					RoutingType = "SMTP",
					MailboxType = MailboxHelper.MailboxTypeType.GroupMailbox.ToString()
				}
			};
		}

		// Token: 0x040027AE RID: 10158
		private ModernGroupMembershipRequestMessageDetailsRequest request;
	}
}
