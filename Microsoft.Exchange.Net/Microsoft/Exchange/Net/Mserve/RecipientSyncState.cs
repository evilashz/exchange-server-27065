using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x020008A9 RID: 2217
	[Serializable]
	internal sealed class RecipientSyncState
	{
		// Token: 0x06002F8C RID: 12172 RVA: 0x0006BF64 File Offset: 0x0006A164
		public static RecipientSyncState DeserializeRecipientSyncState(byte[] data)
		{
			RecipientSyncState result;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				RecipientSyncState recipientSyncState = (RecipientSyncState)RecipientSyncState.serializer.Deserialize(memoryStream);
				result = recipientSyncState;
			}
			return result;
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x0006BFA8 File Offset: 0x0006A1A8
		public static byte[] SerializeRecipientSyncState(RecipientSyncState state)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				RecipientSyncState.serializer.Serialize(memoryStream, state);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x0006BFEC File Offset: 0x0006A1EC
		public static HashSet<string> AddressHashSetFromConcatStringValue(string addresses)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			if (!string.IsNullOrEmpty(addresses))
			{
				string[] separator = new string[]
				{
					";"
				};
				string[] array = addresses.Split(separator, StringSplitOptions.RemoveEmptyEntries);
				foreach (string item in array)
				{
					if (!hashSet.Contains(item))
					{
						hashSet.Add(item);
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x0006C058 File Offset: 0x0006A258
		public static string AddressHashSetToConcatStringValue(HashSet<string> set)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in set)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(";");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x0006C0C0 File Offset: 0x0006A2C0
		public static List<string> AddressToList(string addresses)
		{
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(addresses))
			{
				string[] array = addresses.Split(new char[]
				{
					';'
				});
				foreach (string text in array)
				{
					if (!string.IsNullOrEmpty(text))
					{
						list.Add(text);
					}
				}
			}
			return list;
		}

		// Token: 0x04002931 RID: 10545
		public string ProxyAddresses;

		// Token: 0x04002932 RID: 10546
		public string SignupAddresses;

		// Token: 0x04002933 RID: 10547
		public int PartnerId;

		// Token: 0x04002934 RID: 10548
		public string UMProxyAddresses;

		// Token: 0x04002935 RID: 10549
		public string ArchiveAddress;

		// Token: 0x04002936 RID: 10550
		private static readonly RecipientSyncStateSerializer serializer = new RecipientSyncStateSerializer();
	}
}
