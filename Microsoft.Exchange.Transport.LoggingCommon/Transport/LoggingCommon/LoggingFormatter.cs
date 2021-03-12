using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Transport.LoggingCommon
{
	// Token: 0x02000012 RID: 18
	internal class LoggingFormatter
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000025EB File Offset: 0x000007EB
		public static bool IsSeparator(char c)
		{
			return c == ';' || c == ':' || c == '=' || c == '|';
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002604 File Offset: 0x00000804
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

		// Token: 0x0600000E RID: 14 RVA: 0x00002700 File Offset: 0x00000900
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

		// Token: 0x040000A3 RID: 163
		public const char AgentNameSeparator = '-';

		// Token: 0x040000A4 RID: 164
		public const char ServerSeparator = ';';

		// Token: 0x040000A5 RID: 165
		public const char ServerFqdnSeparator = ':';

		// Token: 0x040000A6 RID: 166
		public const char ComponentValueSeparator = '=';

		// Token: 0x040000A7 RID: 167
		public const char LatencyRecordSeparator = '|';
	}
}
