using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000F3 RID: 243
	internal sealed class EseMiscRecord : EseLogRecord
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0002E695 File Offset: 0x0002C895
		internal static string Identifier
		{
			get
			{
				return "LRMI";
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0002E69C File Offset: 0x0002C89C
		public override string LogRecType
		{
			get
			{
				return EseMiscRecord.Identifier;
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0002E6A3 File Offset: 0x0002C8A3
		protected override Regex Regex()
		{
			return EseMiscRecord.regex;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0002E6AC File Offset: 0x0002C8AC
		internal EseMiscRecord(string input)
		{
			Match match = base.Match(input);
			this.m_checksum = ulong.Parse(match.Groups["checksum"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			this.m_operation = match.Groups["operation"].ToString();
			base.SetLogPosAndSize(match);
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0002E713 File Offset: 0x0002C913
		public ulong Checksum
		{
			get
			{
				return this.m_checksum;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0002E71B File Offset: 0x0002C91B
		public string Operation
		{
			get
			{
				return this.m_operation;
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0002E724 File Offset: 0x0002C924
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "Checksum={0:x},Operation={1}", new object[]
			{
				this.m_checksum,
				this.m_operation
			});
		}

		// Token: 0x04000431 RID: 1073
		private const string ChecksumGroup = "checksum";

		// Token: 0x04000432 RID: 1074
		private const string OperationGroup = "operation";

		// Token: 0x04000433 RID: 1075
		private readonly ulong m_checksum;

		// Token: 0x04000434 RID: 1076
		private readonly string m_operation;

		// Token: 0x04000435 RID: 1077
		private static readonly Regex regex = new Regex(string.Format(CultureInfo.InvariantCulture, "^{0}\\s*,\\s*(?<{1}>[^,]+?)\\s*,\\s*(?<{2}>[0-9A-F]+)\\s*,\\s*(?<{3}>[0-9A-F]+)\\s*,\\s*(?<{4}>[^,]+?)\\s*$", new object[]
		{
			EseMiscRecord.Identifier,
			"LogPos",
			"LogRecSize",
			"checksum",
			"operation"
		}), RegexOptions.CultureInvariant);
	}
}
