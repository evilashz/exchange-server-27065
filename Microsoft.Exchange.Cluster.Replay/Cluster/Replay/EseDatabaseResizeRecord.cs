using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000F6 RID: 246
	internal sealed class EseDatabaseResizeRecord : EseLogRecord
	{
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0002E823 File Offset: 0x0002CA23
		internal static string Identifier
		{
			get
			{
				return "LRRI";
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0002E82A File Offset: 0x0002CA2A
		public override string LogRecType
		{
			get
			{
				return EseDatabaseResizeRecord.Identifier;
			}
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002E831 File Offset: 0x0002CA31
		protected override Regex Regex()
		{
			return EseDatabaseResizeRecord.regex;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002E838 File Offset: 0x0002CA38
		internal EseDatabaseResizeRecord(string input)
		{
			Match match = base.Match(input);
			this.m_checksum = ulong.Parse(match.Groups["Checksum"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			this.m_databaseId = int.Parse(match.Groups["DatabaseId"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			string text = match.Groups["Operation"].ToString();
			if (string.Compare(text, "extenddb", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_operation = DatabaseResizeOperation.Extend;
			}
			else if (string.Compare(text, "shrinkdb", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_operation = DatabaseResizeOperation.Shrink;
			}
			else
			{
				ExDiagnostics.FailFast(string.Format(CultureInfo.CurrentCulture, "resize operation field {0} failed to match {1} or {2}. input is {3}, regex is {4}", new object[]
				{
					text,
					"extenddb",
					"shrinkdb",
					input,
					EseDatabaseResizeRecord.regex.ToString()
				}), true);
			}
			base.SetLogPosAndSize(match);
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0002E934 File Offset: 0x0002CB34
		public ulong Checksum
		{
			get
			{
				return this.m_checksum;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0002E93C File Offset: 0x0002CB3C
		public int DatabaseId
		{
			get
			{
				return this.m_databaseId;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0002E944 File Offset: 0x0002CB44
		public DatabaseResizeOperation Operation
		{
			get
			{
				return this.m_operation;
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0002E94C File Offset: 0x0002CB4C
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "Checksum={0},DatabaseId={1:x},ResizeOperation={2}", new object[]
			{
				this.m_checksum,
				this.m_databaseId,
				this.m_operation
			});
		}

		// Token: 0x0400043A RID: 1082
		private const string ChecksumGroup = "Checksum";

		// Token: 0x0400043B RID: 1083
		private const string DatabaseIdGroup = "DatabaseId";

		// Token: 0x0400043C RID: 1084
		private const string OperationGroup = "Operation";

		// Token: 0x0400043D RID: 1085
		private const string ExtendOperation = "extenddb";

		// Token: 0x0400043E RID: 1086
		private const string ShrinkOperation = "shrinkdb";

		// Token: 0x0400043F RID: 1087
		private readonly ulong m_checksum;

		// Token: 0x04000440 RID: 1088
		private readonly int m_databaseId;

		// Token: 0x04000441 RID: 1089
		private readonly DatabaseResizeOperation m_operation;

		// Token: 0x04000442 RID: 1090
		private static readonly Regex regex = new Regex(string.Format(CultureInfo.InvariantCulture, "^{0}\\s*,\\s*(?<{1}>[^,]+?)\\s*,\\s*(?<{2}>[0-9A-F]+)\\s*,\\s*(?<{3}>[0-9A-F]+)\\s*,\\s*(?i:(?<{4}>({5}|{6})))\\s*,\\s*(?<{7}>.+?)\\s*$", new object[]
		{
			EseDatabaseResizeRecord.Identifier,
			"LogPos",
			"LogRecSize",
			"Checksum",
			"Operation",
			"extenddb",
			"shrinkdb",
			"DatabaseId"
		}), RegexOptions.CultureInvariant);
	}
}
