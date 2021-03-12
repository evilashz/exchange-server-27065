using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000245 RID: 581
	internal static class WinRMAPIUserTokenHelper
	{
		// Token: 0x0600148C RID: 5260 RVA: 0x0004CC08 File Offset: 0x0004AE08
		internal static string GetIdentityForWinRMAPI(string originalIdentity, WinRMInfo winRMInfo)
		{
			if (winRMInfo == null || string.IsNullOrEmpty(winRMInfo.FomattedSessionId))
			{
				return originalIdentity;
			}
			return string.Concat(new object[]
			{
				"908C5F03-5125-46BF-8746-A47994D0DA77",
				winRMInfo.FomattedSessionId,
				',',
				originalIdentity
			});
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0004CC54 File Offset: 0x0004AE54
		internal static string GetOriginalIdentityForWinRMAPI(string curIdentity, out string sessionId)
		{
			string result;
			if (!WinRMAPIUserTokenHelper.ContainsSessionId(curIdentity, out sessionId, out result))
			{
				return curIdentity;
			}
			return result;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0004CC70 File Offset: 0x0004AE70
		private static bool ContainsSessionId(string curIdentity, out string sessionId, out string originalIdentity)
		{
			sessionId = null;
			originalIdentity = null;
			if (string.IsNullOrWhiteSpace(curIdentity))
			{
				return false;
			}
			if (!curIdentity.StartsWith("908C5F03-5125-46BF-8746-A47994D0DA77"))
			{
				return false;
			}
			int num = curIdentity.IndexOf(',');
			if (num < 0 || num <= WinRMAPIUserTokenHelper.PrefixForWinRMAPIIdentityLength || num >= curIdentity.Length - 1)
			{
				return false;
			}
			sessionId = curIdentity.Substring(WinRMAPIUserTokenHelper.PrefixForWinRMAPIIdentityLength, num - WinRMAPIUserTokenHelper.PrefixForWinRMAPIIdentityLength);
			Guid guid;
			if (!Guid.TryParse(sessionId, out guid))
			{
				sessionId = null;
				return false;
			}
			originalIdentity = curIdentity.Substring(num + 1);
			return true;
		}

		// Token: 0x040005ED RID: 1517
		private const char SeparatorChar = ',';

		// Token: 0x040005EE RID: 1518
		private const string PrefixForWinRMAPIIdentity = "908C5F03-5125-46BF-8746-A47994D0DA77";

		// Token: 0x040005EF RID: 1519
		private static readonly int PrefixForWinRMAPIIdentityLength = "908C5F03-5125-46BF-8746-A47994D0DA77".Length;
	}
}
