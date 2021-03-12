using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E7F RID: 3711
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class CustomDataLogger
	{
		// Token: 0x060080AC RID: 32940 RVA: 0x00233044 File Offset: 0x00231244
		static CustomDataLogger()
		{
			int num = 0;
			foreach (string key in CustomDataLogger.CustomFields)
			{
				CustomDataLogger.customFieldMappingTable[key] = CustomDataLogger.startingIndex + num;
				num++;
			}
		}

		// Token: 0x060080AD RID: 32941 RVA: 0x002330C9 File Offset: 0x002312C9
		public static string GetFieldFromTag(CustomDataLogger.Tag tag)
		{
			return CustomDataLogger.CustomFields[tag - (CustomDataLogger.Tag)CustomDataLogger.startingIndex];
		}

		// Token: 0x060080AE RID: 32942 RVA: 0x002330D8 File Offset: 0x002312D8
		public static void Log(KeyValuePair<string, object>[] customData, LogRowFormatter logRow, out Stream storeStream)
		{
			storeStream = null;
			if (customData != null)
			{
				if (logRow == null)
				{
					throw new ArgumentNullException("logRow");
				}
				foreach (KeyValuePair<string, object> keyValuePair in customData)
				{
					if (!CustomDataLogger.customFieldMappingTable.ContainsKey(keyValuePair.Key))
					{
						throw new ArgumentException(string.Format("Invalid custom field name {0}", keyValuePair.Key));
					}
					if (CustomDataLogger.customFieldMappingTable[keyValuePair.Key] != 16)
					{
						logRow[CustomDataLogger.customFieldMappingTable[keyValuePair.Key]] = keyValuePair.Value;
					}
					else
					{
						storeStream = (Stream)keyValuePair.Value;
					}
				}
			}
		}

		// Token: 0x040056CC RID: 22220
		public static readonly string[] CustomFields = new string[]
		{
			"Subcomponent",
			"SyncMailboxGuid",
			"SyncSvcUrl",
			"StoreStream"
		};

		// Token: 0x040056CD RID: 22221
		private static Dictionary<string, int> customFieldMappingTable = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040056CE RID: 22222
		private static int startingIndex = 13;

		// Token: 0x02000E80 RID: 3712
		public enum Tag
		{
			// Token: 0x040056D0 RID: 22224
			Subcomponent = 13,
			// Token: 0x040056D1 RID: 22225
			SyncMailboxGuid,
			// Token: 0x040056D2 RID: 22226
			SyncSvcUrl,
			// Token: 0x040056D3 RID: 22227
			StoreStream
		}
	}
}
