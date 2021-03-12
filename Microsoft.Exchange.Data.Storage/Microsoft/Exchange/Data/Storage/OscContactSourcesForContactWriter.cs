using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004FF RID: 1279
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OscContactSourcesForContactWriter
	{
		// Token: 0x0600377E RID: 14206 RVA: 0x000DF55C File Offset: 0x000DD75C
		private OscContactSourcesForContactWriter()
		{
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x000DF564 File Offset: 0x000DD764
		public byte[] Write(Guid provider, string networkId, string userId)
		{
			Util.ThrowOnNullOrEmptyArgument(userId, "userId");
			return this.WriteNormalized(provider, networkId ?? string.Empty, userId);
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x000DF584 File Offset: 0x000DD784
		private byte[] WriteNormalized(Guid provider, string networkId, string userId)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(2);
					binaryWriter.Write(0);
					binaryWriter.Write(1);
					int num = this.WriteEntry(binaryWriter, provider, networkId, userId);
					binaryWriter.Seek(8, SeekOrigin.Begin);
					binaryWriter.Write((ushort)num);
					binaryWriter.Seek(2, SeekOrigin.Begin);
					binaryWriter.Write((ushort)memoryStream.Length);
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000DF620 File Offset: 0x000DD820
		private int WriteEntry(BinaryWriter writer, Guid provider, string networkId, string userId)
		{
			int num = 0;
			writer.Write(2);
			num += 2;
			writer.Write(0);
			num += 2;
			writer.Write(provider.ToByteArray());
			num += 16;
			int byteCount = Encoding.Unicode.GetByteCount(networkId);
			writer.Write((ushort)byteCount);
			num += 2;
			writer.Write(Encoding.Unicode.GetBytes(networkId));
			num += byteCount;
			int byteCount2 = Encoding.Unicode.GetByteCount(userId);
			writer.Write((ushort)byteCount2);
			num += 2;
			writer.Write(Encoding.Unicode.GetBytes(userId));
			return num + byteCount2;
		}

		// Token: 0x04001D6A RID: 7530
		private const ushort HeaderVersion = 2;

		// Token: 0x04001D6B RID: 7531
		private const int HeaderVersionLength = 2;

		// Token: 0x04001D6C RID: 7532
		private const ushort EntryVersion = 2;

		// Token: 0x04001D6D RID: 7533
		private const int EntryVersionLength = 2;

		// Token: 0x04001D6E RID: 7534
		private const int PropertySizeOffset = 2;

		// Token: 0x04001D6F RID: 7535
		private const int PropertySizeLength = 2;

		// Token: 0x04001D70 RID: 7536
		private const int EntryCountLength = 2;

		// Token: 0x04001D71 RID: 7537
		private const int EntrySizeOffset = 8;

		// Token: 0x04001D72 RID: 7538
		private const int EntrySizeLength = 2;

		// Token: 0x04001D73 RID: 7539
		private const int ProviderGuidLength = 16;

		// Token: 0x04001D74 RID: 7540
		private const int NetworkIdLength = 2;

		// Token: 0x04001D75 RID: 7541
		private const int UserIdLength = 2;

		// Token: 0x04001D76 RID: 7542
		public static readonly OscContactSourcesForContactWriter Instance = new OscContactSourcesForContactWriter();
	}
}
