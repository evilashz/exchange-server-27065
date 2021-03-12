using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel
{
	// Token: 0x02000043 RID: 67
	internal static class Utils
	{
		// Token: 0x06000198 RID: 408 RVA: 0x0000A2E0 File Offset: 0x000084E0
		internal static byte[] DuplicateByteArray(byte[] src)
		{
			if (src == null || src.Length <= 0)
			{
				return null;
			}
			byte[] array = new byte[src.Length];
			Buffer.BlockCopy(src, 0, array, 0, src.Length);
			return array;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000A310 File Offset: 0x00008510
		internal static string[] JsonStringToStringArray(string jsonString)
		{
			if (jsonString == null)
			{
				return null;
			}
			return new JavaScriptSerializer
			{
				MaxJsonLength = 1073741824
			}.Deserialize<string[]>(jsonString);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000A33C File Offset: 0x0000853C
		internal static string StringArrayToJsonString(IEnumerable<string> strings)
		{
			return new JavaScriptSerializer
			{
				MaxJsonLength = 1073741824
			}.Serialize(strings);
		}
	}
}
