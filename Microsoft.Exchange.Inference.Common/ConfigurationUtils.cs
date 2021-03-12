using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000007 RID: 7
	internal static class ConfigurationUtils
	{
		// Token: 0x0600002A RID: 42 RVA: 0x0000258C File Offset: 0x0000078C
		public static ByteQuantifiedSize ReadByteQuantifiedSizeValue(string configName, ByteQuantifiedSize defaultValue)
		{
			string expression = null;
			if (!AppConfigLoader.TryGetConfigRawValue(configName, out expression))
			{
				return defaultValue;
			}
			ByteQuantifiedSize result;
			if (!ByteQuantifiedSize.TryParse(expression, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025B4 File Offset: 0x000007B4
		public static List<string> ReadCommaSeperatedStringValue(string configName, List<string> defaultValue)
		{
			string text = null;
			if (!AppConfigLoader.TryGetConfigRawValue(configName, out text))
			{
				return defaultValue;
			}
			string[] array = text.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array == null || array.Length == 0)
			{
				return defaultValue;
			}
			return new List<string>(array);
		}
	}
}
