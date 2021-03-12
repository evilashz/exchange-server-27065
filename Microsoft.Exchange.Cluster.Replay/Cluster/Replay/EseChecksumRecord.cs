using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000F1 RID: 241
	internal sealed class EseChecksumRecord : EseLogRecord
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x0002E311 File Offset: 0x0002C511
		internal static string Identifier
		{
			get
			{
				return "LRCI";
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0002E318 File Offset: 0x0002C518
		public override string LogRecType
		{
			get
			{
				return EseChecksumRecord.Identifier;
			}
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0002E31F File Offset: 0x0002C51F
		protected override Regex Regex()
		{
			return EseChecksumRecord.regex;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0002E328 File Offset: 0x0002C528
		internal EseChecksumRecord(string input)
		{
			Match match = base.Match(input);
			this.m_checksum = ulong.Parse(match.Groups["checksum"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			base.SetLogPosAndSize(match);
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0002E374 File Offset: 0x0002C574
		public ulong Checksum
		{
			get
			{
				return this.m_checksum;
			}
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0002E37C File Offset: 0x0002C57C
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "Checksum={0:X}", new object[]
			{
				this.m_checksum
			});
		}

		// Token: 0x0400041F RID: 1055
		private const string ChecksumGroup = "checksum";

		// Token: 0x04000420 RID: 1056
		private readonly ulong m_checksum;

		// Token: 0x04000421 RID: 1057
		private static readonly Regex regex = new Regex(string.Format(CultureInfo.InvariantCulture, "^{0}\\s*,\\s*(?<{1}>[^,]+?)\\s*,\\s*(?<{2}>[0-9A-F]+)\\s*,\\s*(?<{3}>[0-9A-F]+)\\s*$", new object[]
		{
			EseChecksumRecord.Identifier,
			"LogPos",
			"LogRecSize",
			"checksum"
		}), RegexOptions.CultureInvariant);
	}
}
