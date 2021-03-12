using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Diagnostics.Internal
{
	// Token: 0x020001DD RID: 477
	internal class LoggingFormatter
	{
		// Token: 0x06000D7D RID: 3453 RVA: 0x000382BB File Offset: 0x000364BB
		public static bool IsSeparator(char c)
		{
			return c == ';' || c == ':' || c == '=' || c == '|';
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x000382D4 File Offset: 0x000364D4
		public static List<KeyValuePair<string, object>> GetAgentInfoString(List<List<KeyValuePair<string, string>>> data)
		{
			if (data == null)
			{
				return null;
			}
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			foreach (List<KeyValuePair<string, string>> list2 in data)
			{
				if (list2.Count != 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					KeyValuePair<string, string> keyValuePair = list2[0];
					string key = LoggingFormatter.EncodeAgentName(keyValuePair.Key);
					stringBuilder.AppendFormat("{0}|", keyValuePair.Value);
					for (int i = 1; i < list2.Count; i++)
					{
						stringBuilder.AppendFormat("{0}={1}|", list2[i].Key, list2[i].Value);
					}
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
					list.Add(new KeyValuePair<string, object>(key, stringBuilder.ToString()));
				}
			}
			return list;
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x000383D0 File Offset: 0x000365D0
		public static string EncodeAgentName(string name)
		{
			StringBuilder stringBuilder = null;
			for (int i = 0; i < name.Length; i++)
			{
				char c = name[i];
				string text = null;
				if (LoggingFormatter.IsSeparator(c) || c == '-')
				{
					text = "_";
				}
				else if (Convert.ToInt32(c) < 32)
				{
					text = "?";
				}
				else if (Convert.ToInt32(c) > 127)
				{
					string str = "\\u";
					int num = (int)c;
					text = str + num.ToString("x4");
				}
				if (text != null)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(name.Length);
						stringBuilder.Append(name, 0, i);
					}
					stringBuilder.Append(text);
				}
				else if (stringBuilder != null)
				{
					stringBuilder.Append(c);
				}
			}
			if (stringBuilder != null)
			{
				return stringBuilder.ToString();
			}
			return name;
		}

		// Token: 0x040009CA RID: 2506
		public const char AgentNameSeparator = '-';

		// Token: 0x040009CB RID: 2507
		public const char ServerSeparator = ';';

		// Token: 0x040009CC RID: 2508
		public const char ServerFqdnSeparator = ':';

		// Token: 0x040009CD RID: 2509
		public const char ComponentValueSeparator = '=';

		// Token: 0x040009CE RID: 2510
		public const char LatencyRecordSeparator = '|';
	}
}
