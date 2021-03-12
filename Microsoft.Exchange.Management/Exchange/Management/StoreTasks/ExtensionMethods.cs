using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x0200078B RID: 1931
	internal static class ExtensionMethods
	{
		// Token: 0x060043E2 RID: 17378 RVA: 0x0011698C File Offset: 0x00114B8C
		public static T GetProperty<T>(this Item item, PropertyDefinition prop)
		{
			T result;
			try
			{
				result = item.GetValueOrDefault<T>(prop, default(T));
			}
			catch (InvalidOperationException)
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x001169CC File Offset: 0x00114BCC
		public static T GetPropertyValue<T>(this CalendarLogAnalysis log, PropertyDefinition prop)
		{
			object obj = null;
			if (log.InternalProperties.TryGetValue(prop, out obj))
			{
				try
				{
					return (T)((object)obj);
				}
				catch (InvalidCastException)
				{
					return default(T);
				}
			}
			return default(T);
		}

		// Token: 0x060043E4 RID: 17380 RVA: 0x00116A1C File Offset: 0x00114C1C
		public static string To64BitString(this byte[] bytes)
		{
			return GlobalObjectId.ByteArrayToHexString(bytes);
		}
	}
}
