using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000455 RID: 1109
	internal static class CommandParsingHelper
	{
		// Token: 0x06003379 RID: 13177 RVA: 0x000CD81C File Offset: 0x000CBA1C
		public static int GetNameValuePairSeparatorIndex(byte[] protocolCommand, Offset nameValuePairOffset, byte separator = 61)
		{
			ArgumentValidator.ThrowIfNull("protocolCommand", protocolCommand);
			ArgumentValidator.ThrowIfInvalidValue<Offset>("nameValuePairOffset", nameValuePairOffset, (Offset offset) => offset.Length > 0);
			ArgumentValidator.ThrowIfOutOfRange<int>("nameValuePairOffset.Start", nameValuePairOffset.Start, 0, protocolCommand.Length);
			return Array.IndexOf<byte>(protocolCommand, separator, nameValuePairOffset.Start, nameValuePairOffset.Length);
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x000CD884 File Offset: 0x000CBA84
		public static bool ShouldRejectMailItem(RoutingAddress fromAddress, SmtpInSessionState sessionState, bool checkRecipientCount, out SmtpResponse failureSmtpResponse)
		{
			ArgumentValidator.ThrowIfNull("sessionState", sessionState);
			failureSmtpResponse = SmtpResponse.Empty;
			if (sessionState.ServerState.RejectSubmits)
			{
				failureSmtpResponse = sessionState.ServerState.RejectionSmtpResponse;
				return true;
			}
			if (sessionState.ServerState.RejectMailFromInternet && (fromAddress == RoutingAddress.NullReversePath || (!sessionState.Configuration.TransportConfiguration.FirstOrgAcceptedDomainTable.CheckInternal(SmtpDomain.GetDomainPart(fromAddress)) && !sessionState.Configuration.TransportConfiguration.SmtpAcceptAnyRecipient)))
			{
				failureSmtpResponse = sessionState.ServerState.RejectionSmtpResponse;
				return true;
			}
			if (checkRecipientCount && sessionState.TransportMailItem != null && sessionState.TransportMailItem.Recipients != null && sessionState.TransportMailItem.Recipients.Count <= 0)
			{
				failureSmtpResponse = SmtpResponse.MailboxDisabled;
				return true;
			}
			return false;
		}
	}
}
