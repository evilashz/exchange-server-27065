using System;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x0200000D RID: 13
	public static class EnumUtil
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002844 File Offset: 0x00000A44
		public static T Parse<T>(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return default(T);
			}
			return EnumUtil.enumTypeMap.Parse<T>(value);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000286E File Offset: 0x00000A6E
		public static string ToString<T>(T value)
		{
			return EnumUtil.enumTypeMap.ToString<T>(value);
		}

		// Token: 0x0400002C RID: 44
		private static EnumTypeMap enumTypeMap = new EnumTypeMap();
	}
}
