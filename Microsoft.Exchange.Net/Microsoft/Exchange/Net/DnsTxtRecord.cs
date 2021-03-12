using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C0E RID: 3086
	internal class DnsTxtRecord : DnsRecord
	{
		// Token: 0x0600438C RID: 17292 RVA: 0x000B5990 File Offset: 0x000B3B90
		internal DnsTxtRecord(Win32DnsRecordHeader header, IntPtr dataPointer) : base(header)
		{
			DnsTxtRecord.Win32DnsTxtRecord win32DnsTxtRecord = (DnsTxtRecord.Win32DnsTxtRecord)Marshal.PtrToStructure(dataPointer, typeof(DnsTxtRecord.Win32DnsTxtRecord));
			if (win32DnsTxtRecord.stringCount > DnsTxtRecord.MaxStrings)
			{
				win32DnsTxtRecord.stringCount = DnsTxtRecord.MaxStrings;
				this.dnsStatus = DnsStatus.ErrorInvalidData;
			}
			StringBuilder stringBuilder = new StringBuilder((int)header.dataLength);
			IntPtr ptr = new IntPtr((long)dataPointer + (long)Marshal.OffsetOf(typeof(DnsTxtRecord.Win32DnsTxtRecord), "arypStringArray"));
			int num = 0;
			while ((long)num < (long)((ulong)win32DnsTxtRecord.stringCount))
			{
				IntPtr ptr2 = Marshal.ReadIntPtr(ptr, num * Marshal.SizeOf(typeof(IntPtr)));
				stringBuilder.Append(Marshal.PtrToStringUni(ptr2));
				num++;
			}
			this.text = stringBuilder.ToString();
		}

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x0600438E RID: 17294 RVA: 0x000B5A5E File Offset: 0x000B3C5E
		// (set) Token: 0x0600438D RID: 17293 RVA: 0x000B5A56 File Offset: 0x000B3C56
		public static uint MaxStrings
		{
			get
			{
				return DnsTxtRecord.maxStrings;
			}
			set
			{
				DnsTxtRecord.maxStrings = value;
			}
		}

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x0600438F RID: 17295 RVA: 0x000B5A68 File Offset: 0x000B3C68
		public string Text
		{
			get
			{
				return string.Concat(new string[]
				{
					this.text
				});
			}
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x06004390 RID: 17296 RVA: 0x000B5A8B File Offset: 0x000B3C8B
		public DnsStatus Status
		{
			get
			{
				return this.dnsStatus;
			}
		}

		// Token: 0x04003990 RID: 14736
		public const int DefaultMaxStrings = 4096;

		// Token: 0x04003991 RID: 14737
		private static uint maxStrings = 4096U;

		// Token: 0x04003992 RID: 14738
		private string text;

		// Token: 0x04003993 RID: 14739
		private DnsStatus dnsStatus;

		// Token: 0x02000C0F RID: 3087
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct Win32DnsTxtRecord
		{
			// Token: 0x04003994 RID: 14740
			public uint stringCount;

			// Token: 0x04003995 RID: 14741
			public IntPtr arypStringArray;
		}
	}
}
