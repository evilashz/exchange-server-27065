using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.TextProcessing.Boomerang
{
	// Token: 0x02000002 RID: 2
	internal static class BoomerangHelper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D6 File Offset: 0x000002D6
		internal static byte XorHashToByte(byte[] bytes)
		{
			if (bytes != null && bytes.Length != 0)
			{
				return bytes.Aggregate(186, (byte current, byte byteValue) => current ^ byteValue);
			}
			return 186;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000210E File Offset: 0x0000030E
		internal static byte XorHashToByte(string stringToHash)
		{
			return BoomerangHelper.XorHashToByte(Encoding.ASCII.GetBytes(stringToHash));
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002120 File Offset: 0x00000320
		internal static byte[] XorHashToByteArray(byte[] bytes, int resultLength)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			byte[] array = new byte[resultLength];
			for (int i = 0; i < bytes.Length; i++)
			{
				byte[] array2 = array;
				int num = i % resultLength;
				array2[num] ^= bytes[i];
			}
			return array;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000216C File Offset: 0x0000036C
		internal static long GetTimeIdentifier()
		{
			return DateTime.UtcNow.ToFileTimeUtc() / 10000000L / 86400L;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002194 File Offset: 0x00000394
		internal static bool IsConsumerMailbox(Guid externalOrganizationId)
		{
			return externalOrganizationId == BoomerangHelper.TemplateTenantExternalDirectoryOrganizationIdGuid;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000022B8 File Offset: 0x000004B8
		private static IEnumerable<string> SplitByTokenSize(string data, int tokenSize)
		{
			for (int index = 0; index < data.Length; index += tokenSize)
			{
				yield return data.Substring(index, tokenSize);
			}
			yield break;
		}

		// Token: 0x04000001 RID: 1
		private const byte InitialXorHashValue = 186;

		// Token: 0x04000002 RID: 2
		private const long FiletimeToSecondsDivisor = 10000000L;

		// Token: 0x04000003 RID: 3
		private const long SecondsPerInterval = 86400L;

		// Token: 0x04000004 RID: 4
		private const string TemplateTenantExternalDirectoryOrganizationId = "84df9e7f-e9f6-40af-b435-aaaaaaaaaaaa";

		// Token: 0x04000005 RID: 5
		private static readonly Guid TemplateTenantExternalDirectoryOrganizationIdGuid = new Guid("84df9e7f-e9f6-40af-b435-aaaaaaaaaaaa");
	}
}
