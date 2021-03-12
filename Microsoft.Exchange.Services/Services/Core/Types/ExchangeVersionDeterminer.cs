using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200076E RID: 1902
	internal static class ExchangeVersionDeterminer
	{
		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x060038CB RID: 14539 RVA: 0x000C8C68 File Offset: 0x000C6E68
		public static ExchangeVersionDeterminer.ExchangeServerVersion LocalServerVersion
		{
			get
			{
				return ExchangeVersionDeterminer.localServerVersion.Member;
			}
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x000C8C74 File Offset: 0x000C6E74
		public static bool MatchesLocalServerVersion(int exchangeServerVersionInt)
		{
			return exchangeServerVersionInt == 0 || ExchangeVersionDeterminer.LocalServerVersion == ExchangeVersionDeterminer.IntToServerVersion(exchangeServerVersionInt);
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x000C8C88 File Offset: 0x000C6E88
		public static bool IsCurrentOrOlderThanLocalServer(int exchangeServerVersionInt)
		{
			ExchangeVersionDeterminer.ExchangeServerVersion exchangeServerVersion = ExchangeVersionDeterminer.IntToServerVersion(exchangeServerVersionInt);
			return exchangeServerVersion != ExchangeVersionDeterminer.ExchangeServerVersion.Legacy && exchangeServerVersion <= ExchangeVersionDeterminer.LocalServerVersion;
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x000C8CAC File Offset: 0x000C6EAC
		public static bool ServerSupportsRequestVersion(int exchangeServerVersionInt)
		{
			if (exchangeServerVersionInt == 0)
			{
				return true;
			}
			switch (ExchangeVersionDeterminer.IntToServerVersion(exchangeServerVersionInt))
			{
			case ExchangeVersionDeterminer.ExchangeServerVersion.Legacy:
				return false;
			case ExchangeVersionDeterminer.ExchangeServerVersion.E12:
				return ExchangeVersion.Current.Version <= ExchangeVersionType.Exchange2007_SP1;
			case ExchangeVersionDeterminer.ExchangeServerVersion.E14:
				return ExchangeVersion.Current.Version <= ExchangeVersionType.Exchange2010_SP2;
			case ExchangeVersionDeterminer.ExchangeServerVersion.E15:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x000C8D06 File Offset: 0x000C6F06
		public static bool AreSameVersion(int exchangeServerVersionInt1, int exchangeServerVersionInt2)
		{
			return ExchangeVersionDeterminer.IntToServerVersion(exchangeServerVersionInt1) == ExchangeVersionDeterminer.IntToServerVersion(exchangeServerVersionInt2);
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x000C8D16 File Offset: 0x000C6F16
		public static bool AreSameVersion(ExchangePrincipal principal1, ExchangePrincipal principal2)
		{
			return ExchangeVersionDeterminer.AreSameVersion(principal1.MailboxInfo.Location.ServerVersion, principal2.MailboxInfo.Location.ServerVersion);
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x000C8D3D File Offset: 0x000C6F3D
		public static bool AllowCrossVersionAccess(int exchangeServerVersionInt)
		{
			return ExchangeVersionDeterminer.LocalServerVersion == ExchangeVersionDeterminer.ExchangeServerVersion.E14 && ExchangeVersionDeterminer.IntToServerVersion(exchangeServerVersionInt) == ExchangeVersionDeterminer.ExchangeServerVersion.E15;
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x000C8D54 File Offset: 0x000C6F54
		public static ExchangeVersionDeterminer.ExchangeServerVersion IntToServerVersion(int exchangeServerVersionInt)
		{
			if (exchangeServerVersionInt < 0)
			{
				throw new ArgumentException("Version integer cannot be negative", "exchangeServerVersionInt");
			}
			if (exchangeServerVersionInt >= Server.E2007MinVersion && exchangeServerVersionInt < Server.E14MinVersion)
			{
				return ExchangeVersionDeterminer.ExchangeServerVersion.E12;
			}
			if (exchangeServerVersionInt >= Server.E14MinVersion && exchangeServerVersionInt < Server.E15MinVersion)
			{
				return ExchangeVersionDeterminer.ExchangeServerVersion.E14;
			}
			if (exchangeServerVersionInt >= Server.E15MinVersion)
			{
				return ExchangeVersionDeterminer.ExchangeServerVersion.E15;
			}
			return ExchangeVersionDeterminer.ExchangeServerVersion.Legacy;
		}

		// Token: 0x04001F81 RID: 8065
		private static LazyMember<ExchangeVersionDeterminer.ExchangeServerVersion> localServerVersion = new LazyMember<ExchangeVersionDeterminer.ExchangeServerVersion>(() => ExchangeVersionDeterminer.IntToServerVersion(LocalServer.GetServer().VersionNumber));

		// Token: 0x0200076F RID: 1903
		public enum ExchangeServerVersion
		{
			// Token: 0x04001F84 RID: 8068
			Legacy,
			// Token: 0x04001F85 RID: 8069
			E12,
			// Token: 0x04001F86 RID: 8070
			E14,
			// Token: 0x04001F87 RID: 8071
			E15
		}
	}
}
