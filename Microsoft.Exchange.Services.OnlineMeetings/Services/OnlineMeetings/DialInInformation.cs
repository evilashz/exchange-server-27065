using System;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000005 RID: 5
	internal class DialInInformation
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002192 File Offset: 0x00000392
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000219A File Offset: 0x0000039A
		public DialInRegions DialInRegions { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000021A3 File Offset: 0x000003A3
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000021AB File Offset: 0x000003AB
		public string ExternalDirectoryUri { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000021B4 File Offset: 0x000003B4
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000021BC File Offset: 0x000003BC
		public string InternalDirectoryUri { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000021C5 File Offset: 0x000003C5
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000021CD File Offset: 0x000003CD
		public bool IsAudioConferenceProviderEnabled { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000021D6 File Offset: 0x000003D6
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000021DE File Offset: 0x000003DE
		public string ParticipantPassCode { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000021E7 File Offset: 0x000003E7
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000021EF File Offset: 0x000003EF
		public string[] TollFreeNumbers { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000021F8 File Offset: 0x000003F8
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002200 File Offset: 0x00000400
		public string TollNumber { get; set; }
	}
}
