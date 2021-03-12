using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008B9 RID: 2233
	internal sealed class UserAgentPattern
	{
		// Token: 0x06003F4E RID: 16206 RVA: 0x000DB340 File Offset: 0x000D9540
		internal static bool IsMatch(string pattern, string userAgent)
		{
			if (pattern == null)
			{
				throw new ArgumentNullException("pattern");
			}
			if (string.Compare(pattern, "*", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(pattern, "**", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			if (pattern.StartsWith("*"))
			{
				string value;
				if (pattern.EndsWith("*"))
				{
					value = pattern.Substring(1, pattern.Length - 2);
					return !string.IsNullOrEmpty(userAgent) && userAgent.Contains(value);
				}
				value = pattern.Remove(0, 1);
				return !string.IsNullOrEmpty(userAgent) && userAgent.EndsWith(value);
			}
			else
			{
				if (pattern.EndsWith("*"))
				{
					string value = pattern.Remove(pattern.Length - 1, 1);
					return !string.IsNullOrEmpty(userAgent) && userAgent.StartsWith(value);
				}
				return string.Compare(pattern, userAgent, StringComparison.OrdinalIgnoreCase) == 0;
			}
		}
	}
}
