using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000EE RID: 238
	internal sealed class EseAttachInfoRecord : EseLogRecord
	{
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x0002DFBB File Offset: 0x0002C1BB
		internal static string Identifier
		{
			get
			{
				return "LHAI";
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0002DFC2 File Offset: 0x0002C1C2
		public override string LogRecType
		{
			get
			{
				return EseAttachInfoRecord.Identifier;
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0002DFC9 File Offset: 0x0002C1C9
		protected override Regex Regex()
		{
			return EseAttachInfoRecord.regex;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0002DFD0 File Offset: 0x0002C1D0
		internal EseAttachInfoRecord(string input)
		{
			Match match = base.Match(input);
			this.m_databaseId = int.Parse(match.Groups["databaseId"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			this.m_database = match.Groups["database"].ToString();
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x0002E030 File Offset: 0x0002C230
		public int DatabaseId
		{
			get
			{
				return this.m_databaseId;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0002E038 File Offset: 0x0002C238
		public string Database
		{
			get
			{
				return this.m_database;
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0002E040 File Offset: 0x0002C240
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "DatabaseId={0:x},Database={1}", new object[]
			{
				this.m_databaseId,
				this.m_database
			});
		}

		// Token: 0x0400040A RID: 1034
		private const string DatabaseIdGroup = "databaseId";

		// Token: 0x0400040B RID: 1035
		private const string DatabaseGroup = "database";

		// Token: 0x0400040C RID: 1036
		private readonly int m_databaseId;

		// Token: 0x0400040D RID: 1037
		private readonly string m_database;

		// Token: 0x0400040E RID: 1038
		private static readonly Regex regex = new Regex(string.Format(CultureInfo.InvariantCulture, "^{0}\\s*,\\s*(?<{1}>[0-9A-F]+)\\s*,\\s*(?<{2}>.+?)\\s*$", new object[]
		{
			EseAttachInfoRecord.Identifier,
			"databaseId",
			"database"
		}), RegexOptions.CultureInvariant);
	}
}
