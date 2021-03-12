using System;
using System.IO;
using Microsoft.Exchange.Data.ContentTypes.Tnef;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x020000A8 RID: 168
	internal static class MsgStoragePropertyData
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x00017E9F File Offset: 0x0001609F
		internal static TnefPropertyTag ReadPropertyTag(byte[] data, int offset)
		{
			return BitConverter.ToInt32(data, offset);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00017EAD File Offset: 0x000160AD
		internal static int ReadPropertyByteCount(byte[] data, int propertyOffset)
		{
			return BitConverter.ToInt32(data, propertyOffset + MsgStoragePropertyData.byteCountOffset);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00017EBC File Offset: 0x000160BC
		internal static short ReadValueAsInt16(byte[] data, int propertyOffset)
		{
			return BitConverter.ToInt16(data, propertyOffset + MsgStoragePropertyData.valueOffset);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00017ECB File Offset: 0x000160CB
		internal static int ReadValueAsInt32(byte[] data, int propertyOffset)
		{
			return BitConverter.ToInt32(data, propertyOffset + MsgStoragePropertyData.valueOffset);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00017EDA File Offset: 0x000160DA
		internal static long ReadValueAsInt64(byte[] data, int propertyOffset)
		{
			return BitConverter.ToInt64(data, propertyOffset + MsgStoragePropertyData.valueOffset);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00017EE9 File Offset: 0x000160E9
		internal static float ReadValueAsSingle(byte[] data, int propertyOffset)
		{
			return BitConverter.ToSingle(data, propertyOffset + MsgStoragePropertyData.valueOffset);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00017EF8 File Offset: 0x000160F8
		internal static double ReadValueAsDouble(byte[] data, int propertyOffset)
		{
			return BitConverter.ToDouble(data, propertyOffset + MsgStoragePropertyData.valueOffset);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00017F07 File Offset: 0x00016107
		internal static int WriteProperty(BinaryWriter writer, TnefPropertyTag propertyTag, short propertyValue)
		{
			writer.Write(propertyTag);
			writer.Write(6);
			writer.Write(propertyValue);
			writer.Write(MsgStoragePropertyData.padding, 0, 6);
			return 16;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00017F32 File Offset: 0x00016132
		internal static int WriteProperty(BinaryWriter writer, TnefPropertyTag propertyTag, int propertyValue)
		{
			writer.Write(propertyTag);
			writer.Write(6);
			writer.Write(propertyValue);
			writer.Write(MsgStoragePropertyData.padding, 0, 4);
			return 16;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00017F5D File Offset: 0x0001615D
		internal static int WriteProperty(BinaryWriter writer, TnefPropertyTag propertyTag, long propertyValue)
		{
			writer.Write(propertyTag);
			writer.Write(6);
			writer.Write(propertyValue);
			return 16;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00017F7B File Offset: 0x0001617B
		internal static int WriteProperty(BinaryWriter writer, TnefPropertyTag propertyTag, float propertyValue)
		{
			writer.Write(propertyTag);
			writer.Write(6);
			writer.Write(propertyValue);
			writer.Write(MsgStoragePropertyData.padding, 0, 4);
			return 16;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00017FA6 File Offset: 0x000161A6
		internal static int WriteProperty(BinaryWriter writer, TnefPropertyTag propertyTag, double propertyValue)
		{
			writer.Write(propertyTag);
			writer.Write(6);
			writer.Write(propertyValue);
			return 16;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00017FC4 File Offset: 0x000161C4
		internal static int WriteStream(BinaryWriter writer, TnefPropertyTag propertyTag, int streamSize)
		{
			writer.Write(propertyTag);
			writer.Write(6);
			writer.Write(streamSize);
			writer.Write(3);
			writer.Write(MsgStoragePropertyData.padding, 0, 2);
			return 16;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00017FF6 File Offset: 0x000161F6
		internal static int WriteObject(BinaryWriter writer, TnefPropertyTag propertyTag, MsgStoragePropertyData.ObjectType objectType)
		{
			writer.Write(propertyTag);
			writer.Write(6);
			writer.Write(uint.MaxValue);
			writer.Write((ushort)objectType);
			writer.Write(MsgStoragePropertyData.padding, 0, 2);
			return 16;
		}

		// Token: 0x04000569 RID: 1385
		internal const int Size = 16;

		// Token: 0x0400056A RID: 1386
		private static int valueOffset = 8;

		// Token: 0x0400056B RID: 1387
		private static int byteCountOffset = 8;

		// Token: 0x0400056C RID: 1388
		private static byte[] padding = new byte[8];

		// Token: 0x020000A9 RID: 169
		internal enum ObjectType
		{
			// Token: 0x0400056E RID: 1390
			Ms,
			// Token: 0x0400056F RID: 1391
			Message,
			// Token: 0x04000570 RID: 1392
			Attachment,
			// Token: 0x04000571 RID: 1393
			Stream,
			// Token: 0x04000572 RID: 1394
			Storage,
			// Token: 0x04000573 RID: 1395
			Recipient,
			// Token: 0x04000574 RID: 1396
			EnumStatStg
		}

		// Token: 0x020000AA RID: 170
		private enum PropAttribute
		{
			// Token: 0x04000576 RID: 1398
			Readable = 2,
			// Token: 0x04000577 RID: 1399
			Writeable = 4,
			// Token: 0x04000578 RID: 1400
			Default = 6
		}
	}
}
