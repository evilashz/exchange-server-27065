using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001F3 RID: 499
	internal static class ParticipantsAddressesConverter
	{
		// Token: 0x06000D1F RID: 3359 RVA: 0x00042B8C File Offset: 0x00040D8C
		public static EmailAddressWrapper[] ToAddresses(IList<Participant> participants)
		{
			if (participants == null || participants.Count == 0)
			{
				return null;
			}
			EmailAddressWrapper[] array = new EmailAddressWrapper[participants.Count];
			for (int i = 0; i < participants.Count; i++)
			{
				EmailAddressWrapper emailAddressWrapper = new EmailAddressWrapper();
				Participant participant = participants[i];
				if (null != participant)
				{
					emailAddressWrapper.EmailAddress = participant.EmailAddress;
					emailAddressWrapper.Name = participant.DisplayName;
					emailAddressWrapper.RoutingType = participant.RoutingType;
					emailAddressWrapper.OriginalDisplayName = participant.OriginalDisplayName;
				}
				array[i] = emailAddressWrapper;
			}
			return array;
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00042C10 File Offset: 0x00040E10
		public static Participant[] ToParticipants(IList<EmailAddressWrapper> emailAddressList)
		{
			if (emailAddressList == null || emailAddressList.Count == 0)
			{
				return null;
			}
			Participant[] array = new Participant[emailAddressList.Count];
			for (int i = 0; i < emailAddressList.Count; i++)
			{
				EmailAddressWrapper emailAddressWrapper = emailAddressList[i];
				if (emailAddressWrapper != null)
				{
					if (string.IsNullOrEmpty(emailAddressWrapper.RoutingType))
					{
						emailAddressWrapper.RoutingType = "SMTP";
					}
					array[i] = new Participant(emailAddressWrapper.Name, emailAddressWrapper.EmailAddress, emailAddressWrapper.RoutingType, emailAddressWrapper.OriginalDisplayName);
				}
			}
			return array;
		}
	}
}
