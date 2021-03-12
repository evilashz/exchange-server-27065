using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200009F RID: 159
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class OwaAppConfigLoader
	{
		// Token: 0x06000637 RID: 1591 RVA: 0x000125B4 File Offset: 0x000107B4
		public static ByteQuantifiedSize GetConfigByteQuantifiedSizeValue(string configName, ByteQuantifiedSize defaultValue)
		{
			string expression = null;
			ByteQuantifiedSize result;
			if (AppConfigLoader.TryGetConfigRawValue(configName, out expression) && ByteQuantifiedSize.TryParse(expression, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x0400037F RID: 895
		internal const string TestOwaAllowHeaderOverrideKey = "Test_OwaAllowHeaderOverride";
	}
}
