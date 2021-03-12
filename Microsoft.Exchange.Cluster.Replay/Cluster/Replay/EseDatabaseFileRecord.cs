using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000F0 RID: 240
	internal sealed class EseDatabaseFileRecord : EseLogRecord
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0002E0C7 File Offset: 0x0002C2C7
		internal static string Identifier
		{
			get
			{
				return "LRDI";
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0002E0CE File Offset: 0x0002C2CE
		public override string LogRecType
		{
			get
			{
				return EseDatabaseFileRecord.Identifier;
			}
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0002E0D5 File Offset: 0x0002C2D5
		protected override Regex Regex()
		{
			return EseDatabaseFileRecord.regex;
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002E0DC File Offset: 0x0002C2DC
		internal EseDatabaseFileRecord(string input)
		{
			Match match = base.Match(input);
			this.m_checksum = ulong.Parse(match.Groups["Checksum"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			this.m_databaseId = int.Parse(match.Groups["DatabaseId"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			this.m_database = match.Groups["Database"].ToString();
			string text = match.Groups["Operation"].ToString();
			if (string.Compare(text, "createdb", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_operation = DatabaseOperation.Create;
			}
			else if (string.Compare(text, "attachdb", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_operation = DatabaseOperation.Attach;
			}
			else if (string.Compare(text, "detachdb", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_operation = DatabaseOperation.Detach;
			}
			else
			{
				ExDiagnostics.FailFast(string.Format(CultureInfo.CurrentCulture, "operation field {0} failed to match {1}, {2} or {3}. input is {4}, regex is {5}", new object[]
				{
					text,
					"createdb",
					"attachdb",
					"detachdb",
					input,
					EseDatabaseFileRecord.regex.ToString()
				}), true);
			}
			base.SetLogPosAndSize(match);
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0002E212 File Offset: 0x0002C412
		public ulong Checksum
		{
			get
			{
				return this.m_checksum;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x0002E21A File Offset: 0x0002C41A
		public int DatabaseId
		{
			get
			{
				return this.m_databaseId;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0002E222 File Offset: 0x0002C422
		public string Database
		{
			get
			{
				return this.m_database;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0002E22A File Offset: 0x0002C42A
		public DatabaseOperation Operation
		{
			get
			{
				return this.m_operation;
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0002E234 File Offset: 0x0002C434
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "Checksum={0},DatabaseId={1:x},Database={2},Operation={3}", new object[]
			{
				this.m_checksum,
				this.m_databaseId,
				this.m_database,
				this.m_operation
			});
		}

		// Token: 0x04000413 RID: 1043
		private const string ChecksumGroup = "Checksum";

		// Token: 0x04000414 RID: 1044
		private const string DatabaseIdGroup = "DatabaseId";

		// Token: 0x04000415 RID: 1045
		private const string DatabaseGroup = "Database";

		// Token: 0x04000416 RID: 1046
		private const string OperationGroup = "Operation";

		// Token: 0x04000417 RID: 1047
		private const string CreateOperation = "createdb";

		// Token: 0x04000418 RID: 1048
		private const string AttachOperation = "attachdb";

		// Token: 0x04000419 RID: 1049
		private const string DetachOperation = "detachdb";

		// Token: 0x0400041A RID: 1050
		private readonly ulong m_checksum;

		// Token: 0x0400041B RID: 1051
		private readonly int m_databaseId;

		// Token: 0x0400041C RID: 1052
		private readonly string m_database;

		// Token: 0x0400041D RID: 1053
		private readonly DatabaseOperation m_operation;

		// Token: 0x0400041E RID: 1054
		private static readonly Regex regex = new Regex(string.Format(CultureInfo.InvariantCulture, "^{0}\\s*,\\s*(?<{1}>[^,]+?)\\s*,\\s*(?<{2}>[0-9A-F]+)\\s*,\\s*(?<{3}>[0-9A-F]+)\\s*,\\s*(?i:(?<{4}>({5}|{6}|{7})))\\s*,\\s*(?<{8}>[0-9A-F]+)\\s*,\\s*(?<{9}>.+?)\\s*$", new object[]
		{
			EseDatabaseFileRecord.Identifier,
			"LogPos",
			"LogRecSize",
			"Checksum",
			"Operation",
			"createdb",
			"attachdb",
			"detachdb",
			"DatabaseId",
			"Database"
		}), RegexOptions.CultureInvariant);
	}
}
