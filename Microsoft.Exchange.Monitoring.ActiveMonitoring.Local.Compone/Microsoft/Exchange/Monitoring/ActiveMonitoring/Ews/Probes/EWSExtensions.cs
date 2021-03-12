using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Ews.Probes
{
	// Token: 0x02000173 RID: 371
	public static class EWSExtensions
	{
		// Token: 0x06000AA3 RID: 2723 RVA: 0x000436D8 File Offset: 0x000418D8
		public static void AddUnique(this Dictionary<string, string> dictionary, string key, string val)
		{
			lock (EWSExtensions.lockObj)
			{
				string key2 = string.Format("{0}{1}{2}", EWSExtensions.uniqueId, EWSExtensions.logSeparator, key);
				dictionary.Add(key2, val);
				EWSExtensions.uniqueId++;
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00043740 File Offset: 0x00041940
		public static bool TryConvertRegularLogKey(string unique, out string normal)
		{
			normal = unique;
			try
			{
				if (unique.Contains(EWSExtensions.logSeparator))
				{
					int num = unique.IndexOf(EWSExtensions.logSeparator) + EWSExtensions.logSeparator.Length;
					normal = unique.Substring(num, unique.Length - num);
					return true;
				}
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x04000806 RID: 2054
		private static int uniqueId = 1;

		// Token: 0x04000807 RID: 2055
		private static object lockObj = new object();

		// Token: 0x04000808 RID: 2056
		private static string logSeparator = "{$#!";
	}
}
