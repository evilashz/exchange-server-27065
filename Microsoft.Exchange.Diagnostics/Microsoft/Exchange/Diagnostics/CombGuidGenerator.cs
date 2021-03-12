using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000114 RID: 276
	public static class CombGuidGenerator
	{
		// Token: 0x060007FD RID: 2045 RVA: 0x0002072B File Offset: 0x0001E92B
		public static Guid NewGuid()
		{
			return CombGuidGenerator.NewGuid(Guid.NewGuid(), DateTime.UtcNow);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0002073C File Offset: 0x0001E93C
		public static Guid NewGuid(DateTime dateTime)
		{
			return CombGuidGenerator.NewGuid(Guid.NewGuid(), dateTime);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0002074C File Offset: 0x0001E94C
		public static Guid NewGuid(Guid guid, DateTime dateTime)
		{
			byte[] array = guid.ToByteArray();
			byte[] bytes = BitConverter.GetBytes(dateTime.Ticks);
			Array.Reverse(bytes);
			Array.Copy(bytes, 0, array, 10, 6);
			Array.Copy(bytes, 6, array, 8, 2);
			return new Guid(array);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00020790 File Offset: 0x0001E990
		public static DateTime ExtractDateTimeFromCombGuid(Guid guid)
		{
			long ticksFromGuid = CombGuidGenerator.GetTicksFromGuid(guid);
			return new DateTime(ticksFromGuid, DateTimeKind.Utc);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000207AC File Offset: 0x0001E9AC
		public static bool IsCombGuid(Guid guid)
		{
			long ticksFromGuid = CombGuidGenerator.GetTicksFromGuid(guid);
			return DateTime.MinValue.Ticks <= ticksFromGuid && ticksFromGuid <= DateTime.MaxValue.Ticks;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000207E8 File Offset: 0x0001E9E8
		private static long GetTicksFromGuid(Guid guid)
		{
			byte[] sourceArray = guid.ToByteArray();
			byte[] array = new byte[8];
			Array.Copy(sourceArray, 10, array, 0, 6);
			Array.Copy(sourceArray, 8, array, 6, 2);
			Array.Reverse(array);
			return BitConverter.ToInt64(array, 0);
		}
	}
}
