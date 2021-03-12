using System;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000BA RID: 186
	internal static class MailboxSessionEstablisher
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000677 RID: 1655 RVA: 0x000191A4 File Offset: 0x000173A4
		// (remove) Token: 0x06000678 RID: 1656 RVA: 0x000191D8 File Offset: 0x000173D8
		internal static event EventHandler<MailboxConnectionArgs> OnMailboxConnectionAttempted;

		// Token: 0x06000679 RID: 1657 RVA: 0x00019234 File Offset: 0x00017434
		public static MailboxSession OpenAsAdmin(ExchangePrincipal mailboxOwner, CultureInfo cultureInfo, string clientInfoString)
		{
			MailboxSession returnValue = null;
			MailboxSessionEstablisher.ConnectAndRaise(delegate
			{
				returnValue = MailboxSession.OpenAsAdmin(mailboxOwner, cultureInfo, clientInfoString);
			});
			return returnValue;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00019298 File Offset: 0x00017498
		public static bool ConnectWithStatus(MailboxSession session)
		{
			bool returnValue = false;
			MailboxSessionEstablisher.ConnectAndRaise(delegate
			{
				returnValue = session.ConnectWithStatus();
			});
			return returnValue;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x000192D0 File Offset: 0x000174D0
		private static void ConnectAndRaise(MailboxSessionEstablisher.CatchMe method)
		{
			bool connected = false;
			try
			{
				method();
				connected = true;
			}
			finally
			{
				if (MailboxSessionEstablisher.OnMailboxConnectionAttempted != null)
				{
					MailboxSessionEstablisher.OnMailboxConnectionAttempted(null, new MailboxConnectionArgs(connected));
				}
			}
		}

		// Token: 0x020000BB RID: 187
		// (Invoke) Token: 0x0600067D RID: 1661
		private delegate void CatchMe();
	}
}
