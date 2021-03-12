using System;

namespace Microsoft.Exchange.Data.Globalization.Iso2022Jp
{
	// Token: 0x02000123 RID: 291
	internal class Escape
	{
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x00069142 File Offset: 0x00067342
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x0006914A File Offset: 0x0006734A
		public EscapeState State { get; set; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00069153 File Offset: 0x00067353
		// (set) Token: 0x06000B7B RID: 2939 RVA: 0x0006915B File Offset: 0x0006735B
		public int BytesInCurrentBuffer { get; set; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00069164 File Offset: 0x00067364
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x0006916C File Offset: 0x0006736C
		public int TotalBytes { get; set; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00069175 File Offset: 0x00067375
		// (set) Token: 0x06000B7F RID: 2943 RVA: 0x0006917D File Offset: 0x0006737D
		public string ErrorMessage { get; set; }

		// Token: 0x06000B80 RID: 2944 RVA: 0x00069186 File Offset: 0x00067386
		public Escape()
		{
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00069190 File Offset: 0x00067390
		public Escape(Escape from)
		{
			this.Sequence = from.Sequence;
			this.State = from.State;
			this.BytesInCurrentBuffer = from.BytesInCurrentBuffer;
			this.TotalBytes = from.TotalBytes;
			this.ErrorMessage = from.ErrorMessage;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x000691DF File Offset: 0x000673DF
		public void Reset()
		{
			this.Sequence = EscapeSequence.None;
			this.State = EscapeState.Begin;
			this.BytesInCurrentBuffer = 0;
			this.TotalBytes = 0;
			this.ErrorMessage = null;
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x00069204 File Offset: 0x00067404
		public bool IsValidEscapeSequence
		{
			get
			{
				return this.Sequence > EscapeSequence.Incomplete;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x00069210 File Offset: 0x00067410
		public char Abbreviation
		{
			get
			{
				switch (this.Sequence)
				{
				case EscapeSequence.None:
				case EscapeSequence.NotRecognized:
				case EscapeSequence.Incomplete:
					return 'x';
				case EscapeSequence.JisX0208_1978:
					return 'e';
				case EscapeSequence.JisX0208_1983:
					return 'E';
				case EscapeSequence.JisX0201K_1976:
					return 'K';
				case EscapeSequence.JisX0201_1976:
					return 'k';
				case EscapeSequence.JisX0212_1990:
					return 'm';
				case EscapeSequence.JisX0208_Nec:
					return 'n';
				case EscapeSequence.Iso646Irv:
					return 'a';
				case EscapeSequence.ShiftIn:
					return 'i';
				case EscapeSequence.ShiftOut:
					return 'o';
				case EscapeSequence.JisX0208_1990:
					return 'Z';
				case EscapeSequence.Cns11643_1992_1:
					return 'Y';
				case EscapeSequence.Kcs5601_1987:
					return 'R';
				case EscapeSequence.Unknown_1:
					return 'u';
				case EscapeSequence.EucKsc:
					return 'r';
				case EscapeSequence.Gb2312_1980:
					return 'y';
				case EscapeSequence.NECKanjiIn:
					return 'w';
				default:
					return 'X';
				}
			}
		}

		// Token: 0x04000E79 RID: 3705
		public EscapeSequence Sequence;
	}
}
