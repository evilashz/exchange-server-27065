using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000EA RID: 234
	internal class EseLogPos
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x0002D7D4 File Offset: 0x0002B9D4
		// (set) Token: 0x0600098F RID: 2447 RVA: 0x0002D7DC File Offset: 0x0002B9DC
		public long Generation { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0002D7E5 File Offset: 0x0002B9E5
		// (set) Token: 0x06000991 RID: 2449 RVA: 0x0002D7ED File Offset: 0x0002B9ED
		public int Sector { get; set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0002D7F6 File Offset: 0x0002B9F6
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x0002D7FE File Offset: 0x0002B9FE
		public int ByteOffset { get; set; }

		// Token: 0x06000994 RID: 2452 RVA: 0x0002D808 File Offset: 0x0002BA08
		public static EseLogPos Parse(string input)
		{
			Regex regex = new Regex(string.Format("\\s*(?<{0}>[0-9a-fA-F]+):(?<{1}>[0-9a-fA-F]+):(?<{2}>[0-9a-fA-F]+)", "Generation", "Sector", "ByteOffset"));
			Match match = regex.Match(input);
			return new EseLogPos
			{
				Generation = long.Parse(match.Groups["Generation"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture),
				Sector = int.Parse(match.Groups["Sector"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture),
				ByteOffset = int.Parse(match.Groups["ByteOffset"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture)
			};
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0002D8C1 File Offset: 0x0002BAC1
		public static int CheckSectorSize(int logSectorSize)
		{
			if (logSectorSize <= 0)
			{
				logSectorSize = 512;
			}
			return logSectorSize;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0002D8D0 File Offset: 0x0002BAD0
		public static EseLogPos BuildNextPos(EseLogPos inPos, int logRecordLen, int logSectorSize)
		{
			logSectorSize = EseLogPos.CheckSectorSize(logSectorSize);
			EseLogPos eseLogPos = new EseLogPos();
			eseLogPos.Generation = inPos.Generation;
			int num = inPos.Sector * logSectorSize + inPos.ByteOffset + logRecordLen;
			eseLogPos.Sector = num / logSectorSize;
			eseLogPos.ByteOffset = num % logSectorSize;
			return eseLogPos;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0002D91C File Offset: 0x0002BB1C
		public int ToBytePos(int logSectorSize)
		{
			logSectorSize = EseLogPos.CheckSectorSize(logSectorSize);
			return this.Sector * logSectorSize + this.ByteOffset;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0002D935 File Offset: 0x0002BB35
		public override string ToString()
		{
			return string.Format("{0:X8}:{1:X4}:{2:X4}", this.Generation, this.Sector, this.ByteOffset);
		}

		// Token: 0x040003E7 RID: 999
		private const string GenerationGroup = "Generation";

		// Token: 0x040003E8 RID: 1000
		private const string SectorGroup = "Sector";

		// Token: 0x040003E9 RID: 1001
		private const string ByteOffsetGroup = "ByteOffset";
	}
}
