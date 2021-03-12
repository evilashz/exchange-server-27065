using System;
using System.Resources;
using Microsoft.Reflection;

namespace System.Diagnostics.Tracing.Internal
{
	// Token: 0x0200045C RID: 1116
	internal static class Environment
	{
		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06003649 RID: 13897 RVA: 0x000D0A5C File Offset: 0x000CEC5C
		public static int TickCount
		{
			get
			{
				return Environment.TickCount;
			}
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x000D0A64 File Offset: 0x000CEC64
		public static string GetResourceString(string key, params object[] args)
		{
			string @string = Environment.rm.GetString(key);
			if (@string != null)
			{
				return string.Format(@string, args);
			}
			string text = string.Empty;
			foreach (object obj in args)
			{
				if (text != string.Empty)
				{
					text += ", ";
				}
				text += obj.ToString();
			}
			return key + " (" + text + ")";
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x000D0ADB File Offset: 0x000CECDB
		public static string GetRuntimeResourceString(string key, params object[] args)
		{
			return Environment.GetResourceString(key, args);
		}

		// Token: 0x040017F7 RID: 6135
		public static readonly string NewLine = Environment.NewLine;

		// Token: 0x040017F8 RID: 6136
		private static ResourceManager rm = new ResourceManager("Microsoft.Diagnostics.Tracing.Messages", typeof(Environment).Assembly());
	}
}
