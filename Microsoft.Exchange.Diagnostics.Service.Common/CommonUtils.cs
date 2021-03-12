using System;
using Microsoft.Win32;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x02000006 RID: 6
	public static class CommonUtils
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public static bool TryGetRegistryValue(string keyName, string valueName, object defaultValue, out object value)
		{
			bool result = false;
			value = defaultValue;
			try
			{
				object value2 = Registry.GetValue(keyName, valueName, null);
				if (value2 != null)
				{
					result = true;
					value = value2;
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00003CE4 File Offset: 0x00001EE4
		public static string FoldIntoSingleLine(string input)
		{
			return input.Replace(Environment.NewLine, "\t");
		}
	}
}
