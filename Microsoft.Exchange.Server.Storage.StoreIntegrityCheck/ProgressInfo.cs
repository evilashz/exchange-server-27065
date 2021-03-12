using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000021 RID: 33
	public class ProgressInfo
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000061A4 File Offset: 0x000043A4
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000061AC File Offset: 0x000043AC
		public short Progress { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000061B5 File Offset: 0x000043B5
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000061BD File Offset: 0x000043BD
		public TimeSpan TimeInServer { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000061C6 File Offset: 0x000043C6
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000061CE File Offset: 0x000043CE
		public DateTime? CompletedTime { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000061D7 File Offset: 0x000043D7
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000061DF File Offset: 0x000043DF
		public DateTime? LastExecutionTime { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000061E8 File Offset: 0x000043E8
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000061F0 File Offset: 0x000043F0
		public int CorruptionsDetected { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000061F9 File Offset: 0x000043F9
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00006201 File Offset: 0x00004401
		public int CorruptionsFixed { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000620A File Offset: 0x0000440A
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00006212 File Offset: 0x00004412
		public ErrorCode Error { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000621B File Offset: 0x0000441B
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00006223 File Offset: 0x00004423
		public IList<Corruption> Corruptions { get; set; }
	}
}
