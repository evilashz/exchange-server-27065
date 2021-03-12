using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000ED RID: 237
	internal sealed class EseLogHeaderRecord : EseLogRecord
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0002DC59 File Offset: 0x0002BE59
		internal static string Identifier
		{
			get
			{
				return "LHGI";
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0002DC60 File Offset: 0x0002BE60
		public override string LogRecType
		{
			get
			{
				return EseLogHeaderRecord.Identifier;
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002DC67 File Offset: 0x0002BE67
		protected override Regex Regex()
		{
			return EseLogHeaderRecord.regex;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002DC70 File Offset: 0x0002BE70
		internal EseLogHeaderRecord(string input)
		{
			Match match = base.Match(input);
			this.m_signature = match.Groups["Signature"].ToString();
			this.m_generation = long.Parse(match.Groups["Generation"].ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			this.m_logFormatVersion = match.Groups["LogFormatVersion"].ToString();
			this.m_creationTime = DateTime.ParseExact(match.Groups["CreationTime"].ToString(), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
			if ("00/00/1900 00:00:00" == match.Groups["PreviousGenerationCreationTime"].ToString())
			{
				DiagCore.RetailAssert(1L == this.m_generation, "Generation {0} has a blank PrevGenCreationTime ({1}). input is {2}, regex is {3}", new object[]
				{
					this.m_generation,
					match.Groups["PreviousGenerationCreationTime"].ToString(),
					input,
					EseLogHeaderRecord.regex.ToString()
				});
				this.m_previousGenerationCreationTime = DateTime.MinValue;
			}
			else
			{
				this.m_previousGenerationCreationTime = DateTime.ParseExact(match.Groups["PreviousGenerationCreationTime"].ToString(), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
			}
			string text = match.Groups["CircularLogging"].ToString();
			if ("0x1" == text)
			{
				this.m_isCircularLoggingOn = true;
			}
			else if ("0x0" == text)
			{
				this.m_isCircularLoggingOn = false;
			}
			else
			{
				ExDiagnostics.FailFast(string.Format(CultureInfo.CurrentCulture, "circular logging field {0} failed to match {1} or {2}. input is {3}, regex is {4}", new object[]
				{
					text,
					"0x1",
					"0x0",
					input,
					EseLogHeaderRecord.regex.ToString()
				}), true);
			}
			this.SectorSize = int.Parse(match.Groups["SectorSizeGroup"].ToString());
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0002DE64 File Offset: 0x0002C064
		public string Signature
		{
			get
			{
				return this.m_signature;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x0002DE6C File Offset: 0x0002C06C
		public long Generation
		{
			get
			{
				return this.m_generation;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0002DE74 File Offset: 0x0002C074
		public DateTime CreationTime
		{
			get
			{
				return this.m_creationTime;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0002DE7C File Offset: 0x0002C07C
		public DateTime PreviousGenerationCreationTime
		{
			get
			{
				return this.m_previousGenerationCreationTime;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0002DE84 File Offset: 0x0002C084
		public string LogFormatVersion
		{
			get
			{
				return this.m_logFormatVersion;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0002DE8C File Offset: 0x0002C08C
		public bool IsCircularLoggingOn
		{
			get
			{
				return this.m_isCircularLoggingOn;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0002DE94 File Offset: 0x0002C094
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x0002DE9C File Offset: 0x0002C09C
		public int SectorSize { get; private set; }

		// Token: 0x060009BB RID: 2491 RVA: 0x0002DEA8 File Offset: 0x0002C0A8
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "Signature={0},Generation={1},CreationTime={2},PrevGenCreationTime={3},Format={4},CircularLogging={5},SectorSize={6}", new object[]
			{
				this.m_signature,
				this.m_generation,
				this.m_creationTime,
				this.m_previousGenerationCreationTime,
				this.m_logFormatVersion,
				this.m_isCircularLoggingOn,
				this.SectorSize
			});
		}

		// Token: 0x040003F6 RID: 1014
		private const string SignatureGroup = "Signature";

		// Token: 0x040003F7 RID: 1015
		private const string GenerationGroup = "Generation";

		// Token: 0x040003F8 RID: 1016
		private const string CreationTimeGroup = "CreationTime";

		// Token: 0x040003F9 RID: 1017
		private const string PreviousGenerationCreationTimeGroup = "PreviousGenerationCreationTime";

		// Token: 0x040003FA RID: 1018
		private const string LogFormatVersionGroup = "LogFormatVersion";

		// Token: 0x040003FB RID: 1019
		private const string CircularLoggingGroup = "CircularLogging";

		// Token: 0x040003FC RID: 1020
		private const string SectorSizeGroup = "SectorSizeGroup";

		// Token: 0x040003FD RID: 1021
		private const string EseNullTime = "00/00/1900 00:00:00";

		// Token: 0x040003FE RID: 1022
		private const string DateTimeRegex = "[0-1]\\d/[0-3]\\d/\\d\\d\\d\\d [0-2]\\d:[0-5]\\d:[0-5]\\d";

		// Token: 0x040003FF RID: 1023
		private const string DateTimeFormat = "MM/dd/yyyy HH:mm:ss";

		// Token: 0x04000400 RID: 1024
		private const string CircularLoggingOn = "0x1";

		// Token: 0x04000401 RID: 1025
		private const string CircularLoggingOff = "0x0";

		// Token: 0x04000402 RID: 1026
		private readonly string m_signature;

		// Token: 0x04000403 RID: 1027
		private readonly long m_generation;

		// Token: 0x04000404 RID: 1028
		private readonly DateTime m_creationTime;

		// Token: 0x04000405 RID: 1029
		private readonly DateTime m_previousGenerationCreationTime;

		// Token: 0x04000406 RID: 1030
		private readonly string m_logFormatVersion;

		// Token: 0x04000407 RID: 1031
		private readonly bool m_isCircularLoggingOn;

		// Token: 0x04000408 RID: 1032
		private static readonly Regex regex = new Regex(string.Format(CultureInfo.InvariantCulture, "^{0}\\s*,\\s*(?<{1}>[^,]+?)\\s*,\\s*(?<{2}>[0-9A-F]+)\\s*,\\s*(?<{3}>{4})\\s*,\\s*(?<{5}>{6})\\s*,\\s*(?<{7}>[^,]+?)\\s*,\\s*(?<{8}>{9}|{10})\\s*,\\s*(?<{11}>[0-9]+)\\s*$", new object[]
		{
			EseLogHeaderRecord.Identifier,
			"Signature",
			"Generation",
			"CreationTime",
			"[0-1]\\d/[0-3]\\d/\\d\\d\\d\\d [0-2]\\d:[0-5]\\d:[0-5]\\d",
			"PreviousGenerationCreationTime",
			"[0-1]\\d/[0-3]\\d/\\d\\d\\d\\d [0-2]\\d:[0-5]\\d:[0-5]\\d",
			"LogFormatVersion",
			"CircularLogging",
			"0x1",
			"0x0",
			"SectorSizeGroup"
		}), RegexOptions.CultureInvariant);
	}
}
