using System;
using System.Text;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200004E RID: 78
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class DeltaSyncDataConversions
	{
		// Token: 0x06000376 RID: 886 RVA: 0x0001067C File Offset: 0x0000E87C
		internal static string EncodeEmailAddress(string nativeEmailAddress)
		{
			if (nativeEmailAddress == null)
			{
				return null;
			}
			if (!DeltaSyncDataConversions.IsProcessingRequired(nativeEmailAddress))
			{
				return nativeEmailAddress;
			}
			Participant participant = null;
			if (!Participant.TryParse(nativeEmailAddress, out participant))
			{
				return nativeEmailAddress;
			}
			string displayName = participant.DisplayName;
			if (displayName == null)
			{
				return nativeEmailAddress;
			}
			Participant participant2 = new Participant(MimeInternalHelpers.Rfc2047Encode(displayName, Encoding.UTF8), participant.EmailAddress, "SMTP");
			return participant2.ToString(AddressFormat.Rfc822Smtp);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000106D8 File Offset: 0x0000E8D8
		internal static string DecodeEmailAddress(string encodedEmailAddress)
		{
			if (encodedEmailAddress == null)
			{
				return null;
			}
			if (!DeltaSyncDataConversions.IsProcessingRequired(encodedEmailAddress))
			{
				return encodedEmailAddress;
			}
			Participant participant = null;
			if (!Participant.TryParse(encodedEmailAddress, out participant))
			{
				return encodedEmailAddress;
			}
			string displayName = participant.DisplayName;
			if (displayName == null)
			{
				return encodedEmailAddress;
			}
			Participant participant2 = new Participant(MimeInternalHelpers.Rfc2047Decode(displayName), participant.EmailAddress, "SMTP");
			return participant2.ToString(AddressFormat.Rfc822Smtp);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001072C File Offset: 0x0000E92C
		private static bool IsProcessingRequired(string emailAddress)
		{
			return emailAddress.EndsWith(">") && emailAddress.StartsWith("\"");
		}
	}
}
