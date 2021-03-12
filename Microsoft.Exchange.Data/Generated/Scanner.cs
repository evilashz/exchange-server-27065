using System;
using System.IO;
using System.Reflection;
using mppg;

namespace Microsoft.Exchange.Data.Generated
{
	// Token: 0x0200023F RID: 575
	public sealed class Scanner : ScanBase, IColorScan
	{
		// Token: 0x060013BF RID: 5055 RVA: 0x0003C448 File Offset: 0x0003A648
		private static int GetMaxParseToken()
		{
			FieldInfo field = typeof(Tokens).GetField("maxParseToken");
			if (!(field == null))
			{
				return (int)field.GetValue(null);
			}
			return int.MaxValue;
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x0003C485 File Offset: 0x0003A685
		// (set) Token: 0x060013C1 RID: 5057 RVA: 0x0003C48D File Offset: 0x0003A68D
		protected override int CurrentSc
		{
			get
			{
				return this.scState;
			}
			set
			{
				this.scState = value;
				this.currentStart = Scanner.startState[value];
			}
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0003C4A3 File Offset: 0x0003A6A3
		public Scanner(string query)
		{
			this.query = query;
			this.SetSource(query, 0);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0003C4C1 File Offset: 0x0003A6C1
		public override void yyerror(string format, params object[] args)
		{
			throw new ParsingSyntaxException(this.query, this.yylloc.sCol + 1);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0003C4DB File Offset: 0x0003A6DB
		internal void LoadYylval()
		{
			this.yylval.Value = this.yytext;
			this.yylloc = new LexLocation(this.tokLin, this.tokCol, this.tokELin, this.tokECol);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0003C511 File Offset: 0x0003A711
		private sbyte Map(int chr)
		{
			if (chr < 126)
			{
				return Scanner.map0[chr];
			}
			return 26;
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0003C8A8 File Offset: 0x0003AAA8
		static Scanner()
		{
			int[] array = new int[2];
			array[0] = 1;
			Scanner.startState = array;
			Scanner.map0 = new sbyte[]
			{
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				26,
				27,
				28,
				2,
				26,
				22,
				26,
				28,
				1,
				20,
				21,
				26,
				8,
				26,
				4,
				6,
				26,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				26,
				26,
				28,
				28,
				28,
				26,
				26,
				17,
				23,
				23,
				18,
				7,
				23,
				11,
				23,
				14,
				23,
				15,
				9,
				23,
				13,
				16,
				23,
				12,
				19,
				23,
				10,
				23,
				23,
				23,
				23,
				23,
				23,
				26,
				26,
				26,
				26,
				24,
				3,
				17,
				23,
				23,
				18,
				7,
				23,
				11,
				23,
				14,
				23,
				15,
				9,
				23,
				13,
				16,
				23,
				12,
				19,
				23,
				10,
				23,
				23,
				23,
				23,
				23,
				23,
				25,
				28,
				0
			};
			Scanner.NxS = new Scanner.Table[50];
			Scanner.NxS[0] = new Scanner.Table(0, 0, 0, null);
			Scanner.NxS[1] = new Scanner.Table(20, 18, 3, new sbyte[]
			{
				4,
				5,
				44,
				3,
				-1,
				-1,
				-1,
				6,
				7,
				-1,
				41,
				42,
				-1,
				43,
				2,
				-1,
				3,
				-1
			});
			Scanner.NxS[2] = new Scanner.Table(5, 3, -1, new sbyte[]
			{
				2,
				47,
				48
			});
			Scanner.NxS[3] = new Scanner.Table(20, 18, 3, new sbyte[]
			{
				-1,
				-1,
				-1,
				3,
				3,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				3,
				-1,
				3,
				-1
			});
			Scanner.NxS[4] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[5] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[6] = new Scanner.Table(27, 1, -1, new sbyte[]
			{
				6
			});
			Scanner.NxS[7] = new Scanner.Table(28, 1, -1, new sbyte[]
			{
				7
			});
			Scanner.NxS[8] = new Scanner.Table(20, 18, 8, new sbyte[]
			{
				-1,
				-1,
				-1,
				8,
				8,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				8,
				-1,
				8,
				-1
			});
			Scanner.NxS[9] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[10] = new Scanner.Table(25, 8, 45, new sbyte[]
			{
				-1,
				45,
				45,
				45,
				9,
				45,
				45,
				46
			});
			Scanner.NxS[11] = new Scanner.Table(5, 1, -1, new sbyte[]
			{
				11
			});
			Scanner.NxS[12] = new Scanner.Table(5, 3, -1, new sbyte[]
			{
				12,
				-1,
				48
			});
			Scanner.NxS[13] = new Scanner.Table(5, 20, -1, new sbyte[]
			{
				15,
				-1,
				15,
				-1,
				15,
				15,
				15,
				37,
				15,
				15,
				15,
				15,
				15,
				15,
				15,
				-1,
				-1,
				-1,
				15,
				15
			});
			Scanner.NxS[14] = new Scanner.Table(5, 20, -1, new sbyte[]
			{
				15,
				-1,
				32,
				-1,
				15,
				33,
				15,
				15,
				15,
				34,
				15,
				15,
				15,
				15,
				15,
				-1,
				-1,
				-1,
				15,
				15
			});
			Scanner.NxS[15] = new Scanner.Table(20, 18, 15, new sbyte[]
			{
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				15,
				-1
			});
			Scanner.NxS[16] = new Scanner.Table(20, 20, 15, new sbyte[]
			{
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				30,
				-1,
				15,
				31
			});
			Scanner.NxS[17] = new Scanner.Table(5, 20, -1, new sbyte[]
			{
				15,
				-1,
				23,
				-1,
				15,
				15,
				15,
				15,
				15,
				15,
				15,
				24,
				15,
				15,
				15,
				-1,
				-1,
				-1,
				15,
				15
			});
			Scanner.NxS[18] = new Scanner.Table(19, 19, 15, new sbyte[]
			{
				22,
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				15,
				-1
			});
			Scanner.NxS[19] = new Scanner.Table(5, 20, -1, new sbyte[]
			{
				15,
				-1,
				15,
				-1,
				15,
				15,
				15,
				15,
				20,
				15,
				15,
				15,
				15,
				15,
				15,
				-1,
				-1,
				-1,
				15,
				15
			});
			Scanner.NxS[20] = new Scanner.Table(18, 20, 15, new sbyte[]
			{
				21,
				15,
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				15,
				-1
			});
			Scanner.NxS[21] = new Scanner.Table(20, 18, 15, new sbyte[]
			{
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				15,
				-1
			});
			Scanner.NxS[22] = new Scanner.Table(20, 18, 15, new sbyte[]
			{
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				15,
				-1
			});
			Scanner.NxS[23] = new Scanner.Table(20, 18, 15, new sbyte[]
			{
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				15,
				-1
			});
			Scanner.NxS[24] = new Scanner.Table(20, 20, 15, new sbyte[]
			{
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				15,
				-1,
				15,
				25
			});
			Scanner.NxS[25] = new Scanner.Table(20, 19, 15, new sbyte[]
			{
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				15,
				-1,
				26
			});
			Scanner.NxS[26] = new Scanner.Table(5, 20, -1, new sbyte[]
			{
				15,
				-1,
				15,
				-1,
				15,
				15,
				15,
				15,
				15,
				27,
				15,
				15,
				15,
				15,
				15,
				-1,
				-1,
				-1,
				15,
				15
			});
			Scanner.NxS[27] = new Scanner.Table(5, 20, -1, new sbyte[]
			{
				15,
				-1,
				15,
				-1,
				15,
				15,
				15,
				15,
				15,
				15,
				28,
				15,
				15,
				15,
				15,
				-1,
				-1,
				-1,
				15,
				15
			});
			Scanner.NxS[28] = new Scanner.Table(20, 18, 15, new sbyte[]
			{
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				29,
				-1
			});
			Scanner.NxS[29] = new Scanner.Table(20, 18, 15, new sbyte[]
			{
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				15,
				-1
			});
			Scanner.NxS[30] = new Scanner.Table(20, 18, 15, new sbyte[]
			{
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				15,
				-1
			});
			Scanner.NxS[31] = new Scanner.Table(20, 18, 15, new sbyte[]
			{
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				15,
				-1
			});
			Scanner.NxS[32] = new Scanner.Table(20, 18, 15, new sbyte[]
			{
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				15,
				-1
			});
			Scanner.NxS[33] = new Scanner.Table(20, 18, 15, new sbyte[]
			{
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				15,
				-1
			});
			Scanner.NxS[34] = new Scanner.Table(5, 20, -1, new sbyte[]
			{
				15,
				-1,
				15,
				-1,
				15,
				15,
				15,
				15,
				15,
				15,
				35,
				15,
				15,
				15,
				15,
				-1,
				-1,
				-1,
				15,
				15
			});
			Scanner.NxS[35] = new Scanner.Table(20, 18, 15, new sbyte[]
			{
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				36,
				-1
			});
			Scanner.NxS[36] = new Scanner.Table(20, 18, 15, new sbyte[]
			{
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				15,
				-1
			});
			Scanner.NxS[37] = new Scanner.Table(20, 18, 15, new sbyte[]
			{
				-1,
				-1,
				-1,
				15,
				15,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				15,
				-1,
				15,
				-1
			});
			Scanner.NxS[38] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[39] = new Scanner.Table(2, 2, 42, new sbyte[]
			{
				38,
				49
			});
			Scanner.NxS[40] = new Scanner.Table(1, 1, -1, new sbyte[]
			{
				41
			});
			Scanner.NxS[41] = new Scanner.Table(1, 1, 41, new sbyte[]
			{
				40
			});
			Scanner.NxS[42] = new Scanner.Table(2, 2, 42, new sbyte[]
			{
				38,
				49
			});
			Scanner.NxS[43] = new Scanner.Table(5, 19, -1, new sbyte[]
			{
				2,
				-1,
				13,
				-1,
				14,
				15,
				16,
				15,
				17,
				15,
				15,
				18,
				19,
				15,
				15,
				-1,
				-1,
				-1,
				15
			});
			Scanner.NxS[44] = new Scanner.Table(20, 18, 8, new sbyte[]
			{
				-1,
				-1,
				-1,
				8,
				8,
				45,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				8,
				-1,
				8,
				-1
			});
			Scanner.NxS[45] = new Scanner.Table(25, 8, 45, new sbyte[]
			{
				-1,
				45,
				45,
				45,
				9,
				45,
				45,
				46
			});
			Scanner.NxS[46] = new Scanner.Table(0, 4, 45, new sbyte[]
			{
				10,
				45,
				45,
				46
			});
			Scanner.NxS[47] = new Scanner.Table(5, 1, -1, new sbyte[]
			{
				12
			});
			Scanner.NxS[48] = new Scanner.Table(4, 5, -1, new sbyte[]
			{
				47,
				11,
				-1,
				-1,
				47
			});
			Scanner.NxS[49] = new Scanner.Table(2, 2, 42, new sbyte[]
			{
				39,
				49
			});
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0003D13C File Offset: 0x0003B33C
		private int NextState(int qStat)
		{
			if (this.chr == -1)
			{
				if (qStat > 40 || qStat == this.currentStart)
				{
					return 0;
				}
				return this.currentStart;
			}
			else
			{
				int num = (int)this.Map(this.chr) - Scanner.NxS[qStat].min;
				if (num < 0)
				{
					num += 29;
				}
				int num2;
				if (num >= Scanner.NxS[qStat].rng)
				{
					num2 = Scanner.NxS[qStat].dflt;
				}
				else
				{
					num2 = (int)Scanner.NxS[qStat].nxt[num];
				}
				if (num2 != -1)
				{
					return num2;
				}
				return this.currentStart;
			}
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0003D1D8 File Offset: 0x0003B3D8
		private int NextState()
		{
			if (this.chr == -1)
			{
				if (this.state > 40 || this.state == this.currentStart)
				{
					return 0;
				}
				return this.currentStart;
			}
			else
			{
				int num = (int)this.Map(this.chr) - Scanner.NxS[this.state].min;
				if (num < 0)
				{
					num += 29;
				}
				int num2;
				if (num >= Scanner.NxS[this.state].rng)
				{
					num2 = Scanner.NxS[this.state].dflt;
				}
				else
				{
					num2 = (int)Scanner.NxS[this.state].nxt[num];
				}
				if (num2 != -1)
				{
					return num2;
				}
				return this.currentStart;
			}
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0003D28F File Offset: 0x0003B48F
		public Scanner(Stream file)
		{
			this.buffer = Scanner.TextBuff.NewTextBuff(file);
			this.cNum = -1;
			this.chr = 10;
			this.GetChr();
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0003D2BF File Offset: 0x0003B4BF
		public Scanner()
		{
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		private void GetChr()
		{
			if (this.chr == 10)
			{
				this.lineStartNum = this.cNum + 1;
				this.lNum++;
			}
			this.chr = this.buffer.Read();
			this.cNum++;
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0003D322 File Offset: 0x0003B522
		private void MarkToken()
		{
			this.tokPos = this.buffer.ReadPos;
			this.tokNum = this.cNum;
			this.tokLin = this.lNum;
			this.tokCol = this.cNum - this.lineStartNum;
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0003D360 File Offset: 0x0003B560
		private void MarkEnd()
		{
			this.tokTxt = null;
			this.tokLen = this.cNum - this.tokNum;
			this.tokEPos = this.buffer.ReadPos;
			this.tokELin = this.lNum;
			this.tokECol = this.cNum - this.lineStartNum;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0003D3B7 File Offset: 0x0003B5B7
		public void SetSource(string source, int offset)
		{
			this.buffer = new Scanner.StringBuff(source);
			this.buffer.Pos = offset;
			this.cNum = offset - 1;
			this.chr = 10;
			this.GetChr();
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0003D3E8 File Offset: 0x0003B5E8
		public int GetNext(ref int state, out int start, out int end)
		{
			this.EolState = state;
			Tokens result = (Tokens)this.Scan();
			state = this.EolState;
			start = this.tokPos;
			end = this.tokEPos - 1;
			return (int)result;
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0003D420 File Offset: 0x0003B620
		public override int yylex()
		{
			int num;
			do
			{
				num = this.Scan();
			}
			while (num >= Scanner.parserMax);
			return num;
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0003D43D File Offset: 0x0003B63D
		private int yyleng
		{
			get
			{
				return this.tokLen;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x0003D445 File Offset: 0x0003B645
		private int yypos
		{
			get
			{
				return this.tokPos;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x0003D44D File Offset: 0x0003B64D
		private int yyline
		{
			get
			{
				return this.tokLin;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0003D455 File Offset: 0x0003B655
		private int yycol
		{
			get
			{
				return this.tokCol;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0003D45D File Offset: 0x0003B65D
		public string yytext
		{
			get
			{
				if (this.tokTxt == null)
				{
					this.tokTxt = this.buffer.GetString(this.tokPos, this.tokEPos);
				}
				return this.tokTxt;
			}
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0003D48C File Offset: 0x0003B68C
		private void yyless(int n)
		{
			this.buffer.Pos = this.tokPos;
			this.cNum = this.tokNum;
			for (int i = 0; i <= n; i++)
			{
				this.GetChr();
			}
			this.MarkEnd();
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0003D4CE File Offset: 0x0003B6CE
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x0003D4D6 File Offset: 0x0003B6D6
		public IErrorHandler Handler
		{
			get
			{
				return this.handler;
			}
			set
			{
				this.handler = value;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0003D4DF File Offset: 0x0003B6DF
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x0003D4E7 File Offset: 0x0003B6E7
		internal int YY_START
		{
			get
			{
				return this.CurrentSc;
			}
			set
			{
				this.CurrentSc = value;
			}
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0003D4F0 File Offset: 0x0003B6F0
		private int Scan()
		{
			int result2;
			try
			{
				for (;;)
				{
					bool flag = false;
					this.state = this.currentStart;
					while (this.NextState() == this.state)
					{
						this.GetChr();
					}
					this.MarkToken();
					int num;
					while ((num = this.NextState()) != this.currentStart)
					{
						if (flag && num > 40)
						{
							Scanner.Context ctx = new Scanner.Context();
							Scanner.Result result = this.Recurse2(ctx, num);
							if (result == Scanner.Result.noMatch)
							{
								this.RestoreStateAndPos(ctx);
								break;
							}
							break;
						}
						else
						{
							this.state = num;
							this.GetChr();
							if (this.state <= 40)
							{
								flag = true;
							}
						}
					}
					if (this.state > 40)
					{
						this.state = this.currentStart;
					}
					else
					{
						this.MarkEnd();
						switch (this.state)
						{
						case 0:
							goto IL_154;
						case 1:
						case 7:
							goto IL_15C;
						case 2:
						case 11:
						case 12:
							goto IL_162;
						case 3:
							goto IL_167;
						case 4:
							goto IL_16C;
						case 5:
							goto IL_172;
						case 6:
							goto IL_178;
						case 8:
							goto IL_17E;
						case 9:
						case 10:
							goto IL_183;
						case 13:
						case 14:
						case 15:
						case 16:
						case 17:
						case 18:
						case 19:
						case 20:
						case 24:
						case 26:
						case 27:
						case 28:
						case 34:
						case 35:
							goto IL_188;
						case 21:
							goto IL_18E;
						case 22:
							goto IL_194;
						case 23:
							goto IL_19A;
						case 25:
							goto IL_1A0;
						case 29:
							goto IL_1A6;
						case 30:
							goto IL_1AC;
						case 31:
							goto IL_1B2;
						case 32:
							goto IL_1B8;
						case 33:
							goto IL_1BE;
						case 36:
							goto IL_1C4;
						case 37:
							goto IL_1CA;
						case 38:
						case 39:
							goto IL_1D0;
						case 40:
							goto IL_1D5;
						}
					}
				}
				IL_154:
				return 2;
				IL_15C:
				return 22;
				IL_162:
				return 6;
				IL_167:
				return 3;
				IL_16C:
				return 9;
				IL_172:
				return 10;
				IL_178:
				return 24;
				IL_17E:
				return 7;
				IL_183:
				return 8;
				IL_188:
				return 22;
				IL_18E:
				return 12;
				IL_194:
				return 11;
				IL_19A:
				return 14;
				IL_1A0:
				return 19;
				IL_1A6:
				return 21;
				IL_1AC:
				return 17;
				IL_1B2:
				return 15;
				IL_1B8:
				return 18;
				IL_1BE:
				return 16;
				IL_1C4:
				return 20;
				IL_1CA:
				return 13;
				IL_1D0:
				return 5;
				IL_1D5:
				result2 = 4;
			}
			finally
			{
				this.LoadYylval();
			}
			return result2;
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0003D6FC File Offset: 0x0003B8FC
		private Scanner.Result Recurse2(Scanner.Context ctx, int next)
		{
			this.SaveStateAndPos(ctx);
			this.state = next;
			if (this.state == 0)
			{
				return Scanner.Result.accept;
			}
			this.GetChr();
			bool flag = false;
			while ((next = this.NextState()) != this.currentStart)
			{
				if (flag && next > 40)
				{
					this.SaveStateAndPos(ctx);
				}
				this.state = next;
				if (this.state == 0)
				{
					return Scanner.Result.accept;
				}
				this.GetChr();
				flag = (this.state <= 40);
			}
			if (flag)
			{
				return Scanner.Result.accept;
			}
			return Scanner.Result.noMatch;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0003D776 File Offset: 0x0003B976
		private void SaveStateAndPos(Scanner.Context ctx)
		{
			ctx.bPos = this.buffer.Pos;
			ctx.cNum = this.cNum;
			ctx.state = this.state;
			ctx.cChr = this.chr;
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0003D7AD File Offset: 0x0003B9AD
		private void RestoreStateAndPos(Scanner.Context ctx)
		{
			this.buffer.Pos = ctx.bPos;
			this.cNum = ctx.cNum;
			this.state = ctx.state;
			this.chr = ctx.cChr;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0003D7E4 File Offset: 0x0003B9E4
		private void RestorePos(Scanner.Context ctx)
		{
			this.buffer.Pos = ctx.bPos;
			this.cNum = ctx.cNum;
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0003D803 File Offset: 0x0003BA03
		internal void BEGIN(int next)
		{
			this.CurrentSc = next;
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0003D80C File Offset: 0x0003BA0C
		internal void ECHO()
		{
			Console.Out.Write(this.yytext);
		}

		// Token: 0x04000B8C RID: 2956
		private const int maxAccept = 40;

		// Token: 0x04000B8D RID: 2957
		private const int initial = 1;

		// Token: 0x04000B8E RID: 2958
		private const int eofNum = 0;

		// Token: 0x04000B8F RID: 2959
		private const int goStart = -1;

		// Token: 0x04000B90 RID: 2960
		private const int INITIAL = 0;

		// Token: 0x04000B91 RID: 2961
		public ScanBuff buffer;

		// Token: 0x04000B92 RID: 2962
		private IErrorHandler handler;

		// Token: 0x04000B93 RID: 2963
		private int scState;

		// Token: 0x04000B94 RID: 2964
		private static int parserMax = Scanner.GetMaxParseToken();

		// Token: 0x04000B95 RID: 2965
		private readonly string query;

		// Token: 0x04000B96 RID: 2966
		private int state;

		// Token: 0x04000B97 RID: 2967
		private int currentStart = 1;

		// Token: 0x04000B98 RID: 2968
		private int chr;

		// Token: 0x04000B99 RID: 2969
		private int cNum;

		// Token: 0x04000B9A RID: 2970
		private int lNum;

		// Token: 0x04000B9B RID: 2971
		private int lineStartNum;

		// Token: 0x04000B9C RID: 2972
		private int tokPos;

		// Token: 0x04000B9D RID: 2973
		private int tokNum;

		// Token: 0x04000B9E RID: 2974
		private int tokLen;

		// Token: 0x04000B9F RID: 2975
		private int tokCol;

		// Token: 0x04000BA0 RID: 2976
		private int tokLin;

		// Token: 0x04000BA1 RID: 2977
		private int tokEPos;

		// Token: 0x04000BA2 RID: 2978
		private int tokECol;

		// Token: 0x04000BA3 RID: 2979
		private int tokELin;

		// Token: 0x04000BA4 RID: 2980
		private string tokTxt;

		// Token: 0x04000BA5 RID: 2981
		private static int[] startState;

		// Token: 0x04000BA6 RID: 2982
		private static sbyte[] map0;

		// Token: 0x04000BA7 RID: 2983
		private static Scanner.Table[] NxS;

		// Token: 0x02000240 RID: 576
		private enum Result
		{
			// Token: 0x04000BA9 RID: 2985
			accept,
			// Token: 0x04000BAA RID: 2986
			noMatch,
			// Token: 0x04000BAB RID: 2987
			contextFound
		}

		// Token: 0x02000241 RID: 577
		private struct Table
		{
			// Token: 0x060013E2 RID: 5090 RVA: 0x0003D81E File Offset: 0x0003BA1E
			public Table(int m, int x, int d, sbyte[] n)
			{
				this.min = m;
				this.rng = x;
				this.dflt = d;
				this.nxt = n;
			}

			// Token: 0x04000BAC RID: 2988
			public int min;

			// Token: 0x04000BAD RID: 2989
			public int rng;

			// Token: 0x04000BAE RID: 2990
			public int dflt;

			// Token: 0x04000BAF RID: 2991
			public sbyte[] nxt;
		}

		// Token: 0x02000242 RID: 578
		internal class Context
		{
			// Token: 0x04000BB0 RID: 2992
			public int bPos;

			// Token: 0x04000BB1 RID: 2993
			public int cNum;

			// Token: 0x04000BB2 RID: 2994
			public int state;

			// Token: 0x04000BB3 RID: 2995
			public int cChr;
		}

		// Token: 0x02000243 RID: 579
		public sealed class StringBuff : ScanBuff
		{
			// Token: 0x060013E4 RID: 5092 RVA: 0x0003D845 File Offset: 0x0003BA45
			public StringBuff(string str)
			{
				this.str = str;
				this.sLen = str.Length;
			}

			// Token: 0x060013E5 RID: 5093 RVA: 0x0003D860 File Offset: 0x0003BA60
			public override int Read()
			{
				if (this.bPos < this.sLen)
				{
					return (int)this.str[this.bPos++];
				}
				if (this.bPos == this.sLen)
				{
					this.bPos++;
					return 10;
				}
				return -1;
			}

			// Token: 0x17000605 RID: 1541
			// (get) Token: 0x060013E6 RID: 5094 RVA: 0x0003D8B8 File Offset: 0x0003BAB8
			public override int ReadPos
			{
				get
				{
					return this.bPos - 1;
				}
			}

			// Token: 0x060013E7 RID: 5095 RVA: 0x0003D8C2 File Offset: 0x0003BAC2
			public override int Peek()
			{
				if (this.bPos < this.sLen)
				{
					return (int)this.str[this.bPos];
				}
				return 10;
			}

			// Token: 0x060013E8 RID: 5096 RVA: 0x0003D8E6 File Offset: 0x0003BAE6
			public override string GetString(int beg, int end)
			{
				if (end > this.sLen)
				{
					end = this.sLen;
				}
				if (end <= beg)
				{
					return "";
				}
				return this.str.Substring(beg, end - beg);
			}

			// Token: 0x17000606 RID: 1542
			// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0003D912 File Offset: 0x0003BB12
			// (set) Token: 0x060013EA RID: 5098 RVA: 0x0003D91A File Offset: 0x0003BB1A
			public override int Pos
			{
				get
				{
					return this.bPos;
				}
				set
				{
					this.bPos = value;
				}
			}

			// Token: 0x04000BB4 RID: 2996
			private readonly string str;

			// Token: 0x04000BB5 RID: 2997
			private int bPos;

			// Token: 0x04000BB6 RID: 2998
			private readonly int sLen;
		}

		// Token: 0x02000244 RID: 580
		public sealed class StreamBuff : ScanBuff
		{
			// Token: 0x060013EB RID: 5099 RVA: 0x0003D923 File Offset: 0x0003BB23
			public StreamBuff(Stream str)
			{
				this.bStrm = new BufferedStream(str);
			}

			// Token: 0x060013EC RID: 5100 RVA: 0x0003D937 File Offset: 0x0003BB37
			public override int Read()
			{
				return this.bStrm.ReadByte();
			}

			// Token: 0x17000607 RID: 1543
			// (get) Token: 0x060013ED RID: 5101 RVA: 0x0003D944 File Offset: 0x0003BB44
			public override int ReadPos
			{
				get
				{
					return (int)this.bStrm.Position - 1;
				}
			}

			// Token: 0x060013EE RID: 5102 RVA: 0x0003D954 File Offset: 0x0003BB54
			public override int Peek()
			{
				int result = this.bStrm.ReadByte();
				this.bStrm.Seek(-1L, SeekOrigin.Current);
				return result;
			}

			// Token: 0x060013EF RID: 5103 RVA: 0x0003D980 File Offset: 0x0003BB80
			public override string GetString(int beg, int end)
			{
				if (end - beg <= 0)
				{
					return "";
				}
				long position = this.bStrm.Position;
				char[] array = new char[end - beg];
				this.bStrm.Position = (long)beg;
				for (int i = 0; i < end - beg; i++)
				{
					array[i] = (char)this.bStrm.ReadByte();
				}
				this.bStrm.Position = position;
				return new string(array);
			}

			// Token: 0x17000608 RID: 1544
			// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0003D9EA File Offset: 0x0003BBEA
			// (set) Token: 0x060013F1 RID: 5105 RVA: 0x0003D9F8 File Offset: 0x0003BBF8
			public override int Pos
			{
				get
				{
					return (int)this.bStrm.Position;
				}
				set
				{
					this.bStrm.Position = (long)value;
				}
			}

			// Token: 0x04000BB7 RID: 2999
			private const int delta = 1;

			// Token: 0x04000BB8 RID: 3000
			private BufferedStream bStrm;
		}

		// Token: 0x02000245 RID: 581
		public class TextBuff : ScanBuff
		{
			// Token: 0x060013F2 RID: 5106 RVA: 0x0003DA07 File Offset: 0x0003BC07
			private Exception BadUTF8()
			{
				return new Exception(string.Format("BadUTF8 Character", new object[0]));
			}

			// Token: 0x060013F3 RID: 5107 RVA: 0x0003DA20 File Offset: 0x0003BC20
			public static Scanner.TextBuff NewTextBuff(Stream strm)
			{
				int num = strm.ReadByte();
				int num2 = strm.ReadByte();
				if (num == 254 && num2 == 255)
				{
					return new Scanner.BigEndTextBuff(strm);
				}
				if (num == 255 && num2 == 254)
				{
					return new Scanner.LittleEndTextBuff(strm);
				}
				int num3 = strm.ReadByte();
				if (num == 239 && num2 == 187 && num3 == 191)
				{
					return new Scanner.TextBuff(strm);
				}
				strm.Seek(0L, SeekOrigin.Begin);
				return new Scanner.TextBuff(strm);
			}

			// Token: 0x060013F4 RID: 5108 RVA: 0x0003DA9F File Offset: 0x0003BC9F
			protected TextBuff(Stream str)
			{
				this.bStrm = new BufferedStream(str);
			}

			// Token: 0x060013F5 RID: 5109 RVA: 0x0003DABC File Offset: 0x0003BCBC
			public override int Read()
			{
				int num = this.bStrm.ReadByte();
				if (num < 127)
				{
					this.delta = ((num == -1) ? 0 : 1);
					return num;
				}
				if ((num & 224) == 192)
				{
					this.delta = 2;
					int num2 = this.bStrm.ReadByte();
					if ((num2 & 192) == 128)
					{
						return ((num & 31) << 6) + (num2 & 63);
					}
					throw this.BadUTF8();
				}
				else
				{
					if ((num & 240) != 224)
					{
						throw this.BadUTF8();
					}
					this.delta = 3;
					int num2 = this.bStrm.ReadByte();
					int num3 = this.bStrm.ReadByte();
					if ((num2 & num3 & 192) == 128)
					{
						return ((num & 15) << 12) + ((num2 & 63) << 6) + (num3 & 63);
					}
					throw this.BadUTF8();
				}
			}

			// Token: 0x17000609 RID: 1545
			// (get) Token: 0x060013F6 RID: 5110 RVA: 0x0003DB8A File Offset: 0x0003BD8A
			public sealed override int ReadPos
			{
				get
				{
					return (int)this.bStrm.Position - this.delta;
				}
			}

			// Token: 0x060013F7 RID: 5111 RVA: 0x0003DBA0 File Offset: 0x0003BDA0
			public sealed override int Peek()
			{
				int result = this.Read();
				this.bStrm.Seek((long)(-(long)this.delta), SeekOrigin.Current);
				return result;
			}

			// Token: 0x060013F8 RID: 5112 RVA: 0x0003DBCC File Offset: 0x0003BDCC
			public sealed override string GetString(int beg, int end)
			{
				if (end - beg <= 0)
				{
					return "";
				}
				long position = this.bStrm.Position;
				char[] array = new char[end - beg];
				this.bStrm.Position = (long)beg;
				int num = 0;
				while (this.bStrm.Position < (long)end)
				{
					array[num] = (char)this.Read();
					num++;
				}
				this.bStrm.Position = position;
				return new string(array, 0, num);
			}

			// Token: 0x1700060A RID: 1546
			// (get) Token: 0x060013F9 RID: 5113 RVA: 0x0003DC3C File Offset: 0x0003BE3C
			// (set) Token: 0x060013FA RID: 5114 RVA: 0x0003DC4A File Offset: 0x0003BE4A
			public sealed override int Pos
			{
				get
				{
					return (int)this.bStrm.Position;
				}
				set
				{
					this.bStrm.Position = (long)value;
				}
			}

			// Token: 0x04000BB9 RID: 3001
			protected BufferedStream bStrm;

			// Token: 0x04000BBA RID: 3002
			protected int delta = 1;
		}

		// Token: 0x02000246 RID: 582
		public sealed class BigEndTextBuff : Scanner.TextBuff
		{
			// Token: 0x060013FB RID: 5115 RVA: 0x0003DC59 File Offset: 0x0003BE59
			internal BigEndTextBuff(Stream str) : base(str)
			{
			}

			// Token: 0x060013FC RID: 5116 RVA: 0x0003DC64 File Offset: 0x0003BE64
			public override int Read()
			{
				int num = this.bStrm.ReadByte();
				int num2 = this.bStrm.ReadByte();
				return (num << 8) + num2;
			}
		}

		// Token: 0x02000247 RID: 583
		public sealed class LittleEndTextBuff : Scanner.TextBuff
		{
			// Token: 0x060013FD RID: 5117 RVA: 0x0003DC8E File Offset: 0x0003BE8E
			internal LittleEndTextBuff(Stream str) : base(str)
			{
			}

			// Token: 0x060013FE RID: 5118 RVA: 0x0003DC98 File Offset: 0x0003BE98
			public override int Read()
			{
				int num = this.bStrm.ReadByte();
				int num2 = this.bStrm.ReadByte();
				return (num2 << 8) + num;
			}
		}
	}
}
