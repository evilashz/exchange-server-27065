using System;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services
{
	// Token: 0x0200001C RID: 28
	public class UnifiedGroupsHelper
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x00008428 File Offset: 0x00006628
		internal static Persona UnifiedGroupParticipantToPersona(UnifiedGroupParticipant participant)
		{
			Persona persona = new Persona();
			persona.DisplayName = participant.DisplayName;
			persona.Alias = participant.Alias;
			persona.ADObjectId = participant.Id.ObjectGuid;
			persona.PersonaId = IdConverter.PersonaIdFromADObjectId(persona.ADObjectId);
			persona.Title = participant.Title;
			persona.ImAddress = participant.SipAddress;
			persona.EmailAddress = new EmailAddressWrapper
			{
				Name = (persona.DisplayName ?? string.Empty),
				EmailAddress = participant.PrimarySmtpAddressToString,
				RoutingType = "SMTP",
				MailboxType = MailboxHelper.MailboxTypeType.Mailbox.ToString()
			};
			return persona;
		}
	}
}
