using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000EC RID: 236
	internal abstract class EseLogRecord
	{
		// Token: 0x060009A4 RID: 2468
		protected abstract Regex Regex();

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002D9F4 File Offset: 0x0002BBF4
		protected Match Match(string input)
		{
			Regex regex = this.Regex();
			Match match = regex.Match(input);
			if (!match.Success)
			{
				EseLogRecord.ThrowParseError(input, regex);
			}
			return match;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0002DA20 File Offset: 0x0002BC20
		private static void ThrowParseError(string input, Regex regex)
		{
			throw new EseutilParseErrorException(input, regex.ToString());
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0002DA30 File Offset: 0x0002BC30
		public static EseLogRecord Parse(string input)
		{
			Match match = EseLogRecord.regex.Match(input);
			if (!match.Success)
			{
				EseLogRecord.ThrowParseError(input, EseLogRecord.regex);
			}
			string text = match.Groups["Identifier"].ToString();
			if (text == EseLogHeaderRecord.Identifier)
			{
				return new EseLogHeaderRecord(input);
			}
			if (text == EseAttachInfoRecord.Identifier)
			{
				return new EseAttachInfoRecord(input);
			}
			if (text == EseDatabaseFileRecord.Identifier)
			{
				return new EseDatabaseFileRecord(input);
			}
			if (text == EseChecksumRecord.Identifier)
			{
				return new EseChecksumRecord(input);
			}
			if (text == EsePageRecord.Identifier)
			{
				return new EsePageRecord(input);
			}
			if (text == EseMiscRecord.Identifier)
			{
				return new EseMiscRecord(input);
			}
			if (text == EseEofRecord.Identifier)
			{
				return new EseEofRecord(input);
			}
			if (text == EseDatabaseResizeRecord.Identifier)
			{
				return new EseDatabaseResizeRecord(input);
			}
			if (text == EseDatabaseTrimRecord.Identifier)
			{
				return new EseDatabaseTrimRecord(input);
			}
			ExDiagnostics.FailFast(string.Format(CultureInfo.CurrentCulture, "identifier field {0} failed to match. input is {1}, regex is {2}", new object[]
			{
				text,
				input,
				EseLogRecord.regex.ToString()
			}), true);
			return null;
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0002DB59 File Offset: 0x0002BD59
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x0002DB61 File Offset: 0x0002BD61
		public EseLogPos LogPos { get; protected set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0002DB6A File Offset: 0x0002BD6A
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x0002DB72 File Offset: 0x0002BD72
		public int LogRecSize { get; protected set; }

		// Token: 0x060009AC RID: 2476 RVA: 0x0002DB7C File Offset: 0x0002BD7C
		protected void SetLogPosAndSize(Match m)
		{
			this.LogPos = EseLogPos.Parse(m.Groups["LogPos"].ToString());
			this.LogRecSize = int.Parse(m.Groups["LogRecSize"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060009AD RID: 2477
		public abstract string LogRecType { get; }

		// Token: 0x040003F0 RID: 1008
		private const string IdentifierGroup = "Identifier";

		// Token: 0x040003F1 RID: 1009
		protected const string LogPosGroup = "LogPos";

		// Token: 0x040003F2 RID: 1010
		protected const string LogRecSizeGroup = "LogRecSize";

		// Token: 0x040003F3 RID: 1011
		private static readonly Regex regex = new Regex(string.Format(CultureInfo.InvariantCulture, "^(?<{0}>{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9})\\s*", new object[]
		{
			"Identifier",
			EseLogHeaderRecord.Identifier,
			EseAttachInfoRecord.Identifier,
			EseDatabaseFileRecord.Identifier,
			EseChecksumRecord.Identifier,
			EsePageRecord.Identifier,
			EseMiscRecord.Identifier,
			EseEofRecord.Identifier,
			EseDatabaseResizeRecord.Identifier,
			EseDatabaseTrimRecord.Identifier
		}), RegexOptions.CultureInvariant);
	}
}
