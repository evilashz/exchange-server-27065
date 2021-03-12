using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200013B RID: 315
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public sealed class CopyStreamResult
	{
		// Token: 0x06000CE4 RID: 3300 RVA: 0x0003596D File Offset: 0x00033B6D
		public CopyStreamResult(TimeSpan timeReadingInput, TimeSpan timeWritingOutput, long TotalBytesCopied)
		{
			this.TimeReadingInput = timeReadingInput;
			this.TimeWritingOutput = timeWritingOutput;
			this.TotalBytesCopied = TotalBytesCopied;
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0003598A File Offset: 0x00033B8A
		// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x00035992 File Offset: 0x00033B92
		public TimeSpan TimeReadingInput { get; private set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0003599B File Offset: 0x00033B9B
		// (set) Token: 0x06000CE8 RID: 3304 RVA: 0x000359A3 File Offset: 0x00033BA3
		public TimeSpan TimeWritingOutput { get; private set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x000359AC File Offset: 0x00033BAC
		// (set) Token: 0x06000CEA RID: 3306 RVA: 0x000359B4 File Offset: 0x00033BB4
		public long TotalBytesCopied { get; private set; }

		// Token: 0x06000CEB RID: 3307 RVA: 0x000359C0 File Offset: 0x00033BC0
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"TimeReadingInput=",
				this.TimeReadingInput.TotalMilliseconds,
				"ms, TimeWritingOutput=",
				this.TimeWritingOutput.TotalMilliseconds,
				"ms, TotalBytesCopied=",
				this.TotalBytesCopied
			});
		}
	}
}
