using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000F2 RID: 242
	internal sealed class EsePageRecord : EseLogRecord
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0002E403 File Offset: 0x0002C603
		internal static string Identifier
		{
			get
			{
				return "LRPI";
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x0002E40A File Offset: 0x0002C60A
		public override string LogRecType
		{
			get
			{
				return EsePageRecord.Identifier;
			}
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0002E411 File Offset: 0x0002C611
		protected override Regex Regex()
		{
			return EsePageRecord.regex;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002E418 File Offset: 0x0002C618
		internal EsePageRecord(string input)
		{
			Match match = base.Match(input);
			this.m_operation = match.Groups["operation"].ToString();
			this.m_checksum = ulong.Parse(match.Groups["checksum"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			this.m_page = long.Parse(match.Groups["page"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			this.m_objectId = long.Parse(match.Groups["objectId"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			this.m_databaseId = long.Parse(match.Groups["databaseId"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			this.m_dbtimeBefore = ulong.Parse(match.Groups["dbtimeBefore"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			this.m_dbtimeAfter = ulong.Parse(match.Groups["dbtimeAfter"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			base.SetLogPosAndSize(match);
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x0002E551 File Offset: 0x0002C751
		public ulong Checksum
		{
			get
			{
				return this.m_checksum;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x0002E559 File Offset: 0x0002C759
		public long DatabaseId
		{
			get
			{
				return this.m_databaseId;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x0002E561 File Offset: 0x0002C761
		public string Operation
		{
			get
			{
				return this.m_operation;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x0002E569 File Offset: 0x0002C769
		public long PageNumber
		{
			get
			{
				return this.m_page;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0002E571 File Offset: 0x0002C771
		public long ObjectId
		{
			get
			{
				return this.m_objectId;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0002E579 File Offset: 0x0002C779
		public ulong DbtimeBefore
		{
			get
			{
				return this.m_dbtimeBefore;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0002E581 File Offset: 0x0002C781
		public ulong DbtimeAfter
		{
			get
			{
				return this.m_dbtimeAfter;
			}
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0002E58C File Offset: 0x0002C78C
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "Checksum={0:x},DatabaseId={1:X},Operation={2},Page={3:X},ObjectId={4:X},DbtimeBefore={5:X},DbtimeAfter={6:X}", new object[]
			{
				this.m_checksum,
				this.m_databaseId,
				this.m_operation,
				this.m_page,
				this.m_objectId,
				this.m_dbtimeBefore,
				this.m_dbtimeAfter
			});
		}

		// Token: 0x04000422 RID: 1058
		private const string ChecksumGroup = "checksum";

		// Token: 0x04000423 RID: 1059
		private const string OperationGroup = "operation";

		// Token: 0x04000424 RID: 1060
		private const string PageGroup = "page";

		// Token: 0x04000425 RID: 1061
		private const string ObjectIdGroup = "objectId";

		// Token: 0x04000426 RID: 1062
		private const string DatabaseIdGroup = "databaseId";

		// Token: 0x04000427 RID: 1063
		private const string DbtimeBeforeGroup = "dbtimeBefore";

		// Token: 0x04000428 RID: 1064
		private const string DbtimeAfterGroup = "dbtimeAfter";

		// Token: 0x04000429 RID: 1065
		private readonly ulong m_checksum;

		// Token: 0x0400042A RID: 1066
		private readonly long m_databaseId;

		// Token: 0x0400042B RID: 1067
		private readonly string m_operation;

		// Token: 0x0400042C RID: 1068
		private readonly long m_page;

		// Token: 0x0400042D RID: 1069
		private readonly long m_objectId;

		// Token: 0x0400042E RID: 1070
		private readonly ulong m_dbtimeBefore;

		// Token: 0x0400042F RID: 1071
		private readonly ulong m_dbtimeAfter;

		// Token: 0x04000430 RID: 1072
		private static readonly Regex regex = new Regex(string.Format(CultureInfo.InvariantCulture, "^{0}\\s*,\\s*(?<{1}>[^,]+?)\\s*,\\s*(?<{2}>[0-9A-F]+)\\s*,\\s*(?<{3}>[0-9A-F]+)\\s*,\\s*(?<{4}>[^,]+?)\\s*,\\s*(?<{5}>[0-9A-F]+)\\s*,\\s*(?<{6}>[0-9A-F]+)\\s*,\\s*(?<{7}>[0-9A-F]+)\\s*,\\s*(?<{8}>[0-9A-F]+)\\s*,\\s*(?<{9}>[0-9A-F]+)\\s*$", new object[]
		{
			EsePageRecord.Identifier,
			"LogPos",
			"LogRecSize",
			"checksum",
			"operation",
			"page",
			"objectId",
			"databaseId",
			"dbtimeBefore",
			"dbtimeAfter"
		}), RegexOptions.CultureInvariant);
	}
}
