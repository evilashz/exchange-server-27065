using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000084 RID: 132
	public static class HashHelpers
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x0001D7A8 File Offset: 0x0001B9A8
		public static int GetConversationTopicHash(string input)
		{
			return HashHelpers.GetInternetMessageIdHash(input);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001D7B0 File Offset: 0x0001B9B0
		public static int GetInternetMessageIdHash(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return 0;
			}
			uint num = 0U;
			string text = input.ToUpperInvariant();
			foreach (uint num2 in text)
			{
				num ^= num2;
				for (int j = 0; j < 16; j++)
				{
					uint num3 = num & 1U;
					num >>= 1;
					if (num3 != 0U)
					{
						num ^= 3988292384U;
					}
				}
			}
			return (int)num;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001D81C File Offset: 0x0001BA1C
		public static int GetConversationIdHash(byte[] inputBytes)
		{
			if (inputBytes == null || inputBytes.Length == 0)
			{
				return 0;
			}
			uint num = 0U;
			foreach (uint num2 in inputBytes)
			{
				num ^= num2;
				for (int j = 0; j < 8; j++)
				{
					uint num3 = num & 1U;
					num >>= 1;
					if (num3 != 0U)
					{
						num ^= 3988292384U;
					}
				}
			}
			return (int)num;
		}
	}
}
