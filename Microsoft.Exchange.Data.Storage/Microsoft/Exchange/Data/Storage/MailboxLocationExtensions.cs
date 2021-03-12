using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000277 RID: 631
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MailboxLocationExtensions
	{
		// Token: 0x06001A57 RID: 6743 RVA: 0x0007BC58 File Offset: 0x00079E58
		public static bool IsLocal(this IMailboxLocation mailboxLocation)
		{
			string serverFqdn = mailboxLocation.ServerFqdn;
			return string.Equals(serverFqdn, ComputerInformation.DnsFullyQualifiedDomainName, StringComparison.OrdinalIgnoreCase) || string.Equals(serverFqdn, ComputerInformation.DnsHostName, StringComparison.OrdinalIgnoreCase) || string.Equals(serverFqdn, "localhost", StringComparison.OrdinalIgnoreCase) || string.Equals(serverFqdn, Environment.MachineName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x0007BCA4 File Offset: 0x00079EA4
		public static bool IsLegacyServer(this IMailboxLocation mailboxLocation)
		{
			int num = 0;
			if (mailboxLocation != null)
			{
				num = mailboxLocation.ServerVersion;
			}
			return num <= Server.E2007MinVersion;
		}
	}
}
