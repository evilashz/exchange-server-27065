using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000F7 RID: 247
	internal sealed class EseDatabaseTrimRecord : EseLogRecord
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0002EA0F File Offset: 0x0002CC0F
		internal static string Identifier
		{
			get
			{
				return "LRTI";
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0002EA16 File Offset: 0x0002CC16
		public override string LogRecType
		{
			get
			{
				return EseDatabaseTrimRecord.Identifier;
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0002EA1D File Offset: 0x0002CC1D
		protected override Regex Regex()
		{
			return EseDatabaseTrimRecord.regex;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0002EA24 File Offset: 0x0002CC24
		internal EseDatabaseTrimRecord(string input)
		{
			Match match = base.Match(input);
			this.m_checksum = ulong.Parse(match.Groups["Checksum"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			this.m_databaseId = int.Parse(match.Groups["DatabaseId"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			this.m_operation = match.Groups["Operation"].ToString();
			base.SetLogPosAndSize(match);
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0002EAB5 File Offset: 0x0002CCB5
		public ulong Checksum
		{
			get
			{
				return this.m_checksum;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0002EABD File Offset: 0x0002CCBD
		public string Operation
		{
			get
			{
				return this.m_operation;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0002EAC5 File Offset: 0x0002CCC5
		public int DatabaseId
		{
			get
			{
				return this.m_databaseId;
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002EAD0 File Offset: 0x0002CCD0
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "Checksum={0},DatabaseId={1:x},Operation={2}", new object[]
			{
				this.m_checksum,
				this.m_databaseId,
				this.Operation
			});
		}

		// Token: 0x04000443 RID: 1091
		private const string ChecksumGroup = "Checksum";

		// Token: 0x04000444 RID: 1092
		private const string DatabaseIdGroup = "DatabaseId";

		// Token: 0x04000445 RID: 1093
		private const string OperationGroup = "Operation";

		// Token: 0x04000446 RID: 1094
		private readonly ulong m_checksum;

		// Token: 0x04000447 RID: 1095
		private readonly int m_databaseId;

		// Token: 0x04000448 RID: 1096
		private readonly string m_operation;

		// Token: 0x04000449 RID: 1097
		private static readonly Regex regex = new Regex(string.Format(CultureInfo.InvariantCulture, "^{0}\\s*,\\s*(?<{1}>[^,]+?)\\s*,\\s*(?<{2}>[0-9A-F]+)\\s*,\\s*(?<{3}>[0-9A-F]+)\\s*,\\s*(?<{4}>[^,]+?)\\s*,\\s*(?<{5}>[0-9A-F]+)\\s*$", new object[]
		{
			EseDatabaseTrimRecord.Identifier,
			"LogPos",
			"LogRecSize",
			"Checksum",
			"Operation",
			"DatabaseId"
		}), RegexOptions.CultureInvariant);
	}
}
