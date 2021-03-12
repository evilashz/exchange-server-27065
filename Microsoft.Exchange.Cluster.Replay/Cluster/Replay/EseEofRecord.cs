using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000F4 RID: 244
	internal sealed class EseEofRecord : EseLogRecord
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0002E7BB File Offset: 0x0002C9BB
		internal static string Identifier
		{
			get
			{
				return "LTEL";
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0002E7C2 File Offset: 0x0002C9C2
		public override string LogRecType
		{
			get
			{
				return EseEofRecord.Identifier;
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0002E7C9 File Offset: 0x0002C9C9
		protected override Regex Regex()
		{
			return EseEofRecord.regex;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0002E7D0 File Offset: 0x0002C9D0
		internal EseEofRecord(string input)
		{
			base.Match(input);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0002E7E0 File Offset: 0x0002C9E0
		public override string ToString()
		{
			return "End of dump";
		}

		// Token: 0x04000436 RID: 1078
		private static readonly Regex regex = new Regex(string.Format(CultureInfo.InvariantCulture, "^{0}\\s*$", new object[]
		{
			EseEofRecord.Identifier
		}), RegexOptions.CultureInvariant);
	}
}
