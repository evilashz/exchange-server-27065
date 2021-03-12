using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200008D RID: 141
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class Watermark
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x000102A7 File Offset: 0x0000E4A7
		internal Watermark(Guid mailboxGuid, long counter)
		{
			this.mailboxGuid = mailboxGuid;
			this.consumerGuid = Guid.Empty;
			this.eventCounter = counter;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000102C8 File Offset: 0x0000E4C8
		internal unsafe Watermark(IntPtr watermark)
		{
			WatermarkNative* ptr = (WatermarkNative*)watermark.ToPointer();
			this.mailboxGuid = ptr->mailboxGuid;
			this.consumerGuid = ptr->consumerGuid;
			this.eventCounter = ptr->llEventCounter;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00010307 File Offset: 0x0000E507
		public static Watermark CreateLowestMark(Guid mailboxGuid)
		{
			return new Watermark(mailboxGuid, 0L);
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00010311 File Offset: 0x0000E511
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00010319 File Offset: 0x0000E519
		public Guid ConsumerGuid
		{
			get
			{
				return this.consumerGuid;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00010321 File Offset: 0x0000E521
		public long EventCounter
		{
			get
			{
				return this.eventCounter;
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00010329 File Offset: 0x0000E529
		public static Watermark GetMailboxWatermark(Guid mailboxGuid, long counter)
		{
			if (mailboxGuid == Guid.Empty)
			{
				throw MapiExceptionHelper.ArgumentException("mailboxGuid", "cannot be an empty GUID.");
			}
			return new Watermark(mailboxGuid, counter);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001034F File Offset: 0x0000E54F
		public static Watermark GetDatabaseWatermark(long counter)
		{
			return new Watermark(Guid.Empty, counter);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001035C File Offset: 0x0000E55C
		internal static Watermark[] Unmarshal(SafeExMemoryHandle ptrWatermarksNative, int countWatermarks)
		{
			Watermark[] array = new Watermark[countWatermarks];
			if (countWatermarks > 0)
			{
				IntPtr intPtr = ptrWatermarksNative.DangerousGetHandle();
				for (int i = 0; i < countWatermarks; i++)
				{
					array[i] = new Watermark(intPtr);
					intPtr = (IntPtr)((long)intPtr + (long)WatermarkNative.SizeOf);
				}
			}
			return array;
		}

		// Token: 0x0400057D RID: 1405
		private Guid mailboxGuid;

		// Token: 0x0400057E RID: 1406
		private Guid consumerGuid;

		// Token: 0x0400057F RID: 1407
		private long eventCounter;
	}
}
