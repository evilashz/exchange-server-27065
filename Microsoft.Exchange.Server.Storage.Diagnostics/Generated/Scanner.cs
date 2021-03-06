using System;
using System.IO;
using System.Reflection;
using mppg;

namespace Microsoft.Exchange.Server.Storage.Diagnostics.Generated
{
	// Token: 0x02000027 RID: 39
	public sealed class Scanner : ScanBase, IColorScan
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00009BB0 File Offset: 0x00007DB0
		private static int GetMaxParseToken()
		{
			FieldInfo field = typeof(Tokens).GetField("maxParseToken");
			if (!(field == null))
			{
				return (int)field.GetValue(null);
			}
			return int.MaxValue;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00009BED File Offset: 0x00007DED
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00009BF5 File Offset: 0x00007DF5
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

		// Token: 0x06000148 RID: 328 RVA: 0x00009C0B File Offset: 0x00007E0B
		public Scanner(string query)
		{
			this.query = query;
			this.SetSource(query, 0);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00009C2C File Offset: 0x00007E2C
		public override void yyerror(string format, params object[] args)
		{
			string message = "Syntax error";
			if (this.yylloc != null)
			{
				message = string.Format("Syntax error at {0}", this.yylloc.sCol + 1);
			}
			throw new DiagnosticQueryParserException(message, this.query);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00009C70 File Offset: 0x00007E70
		internal void LoadYylval()
		{
			this.yylval.Value = this.yytext;
			this.yylloc = new LexLocation(this.tokLin, this.tokCol, this.tokELin, this.tokECol);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00009CA6 File Offset: 0x00007EA6
		private sbyte Map(int chr)
		{
			if (chr < 123)
			{
				return Scanner.map0[chr];
			}
			return 0;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000A8C4 File Offset: 0x00008AC4
		static Scanner()
		{
			int[] array = new int[2];
			array[0] = 105;
			Scanner.startState = array;
			Scanner.map0 = new sbyte[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				41,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				23,
				31,
				2,
				0,
				0,
				0,
				0,
				1,
				35,
				36,
				40,
				0,
				29,
				7,
				9,
				0,
				10,
				8,
				8,
				8,
				8,
				8,
				8,
				8,
				8,
				8,
				0,
				0,
				33,
				30,
				32,
				0,
				0,
				17,
				12,
				19,
				21,
				15,
				16,
				37,
				27,
				22,
				37,
				34,
				6,
				25,
				4,
				24,
				20,
				37,
				14,
				18,
				13,
				5,
				37,
				26,
				11,
				28,
				37,
				38,
				0,
				39,
				0,
				37,
				3,
				17,
				12,
				19,
				21,
				15,
				16,
				37,
				27,
				22,
				37,
				34,
				6,
				25,
				4,
				24,
				20,
				37,
				14,
				18,
				13,
				5,
				37,
				26,
				11,
				28,
				37
			};
			Scanner.NxS = new Scanner.Table[123];
			Scanner.NxS[0] = new Scanner.Table(0, 0, 0, null);
			Scanner.NxS[1] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[2] = new Scanner.Table(1, 1, 122, new sbyte[]
			{
				104
			});
			Scanner.NxS[3] = new Scanner.Table(2, 2, 120, new sbyte[]
			{
				102,
				121
			});
			Scanner.NxS[4] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				98,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				97,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[5] = new Scanner.Table(20, 32, 11, new sbyte[]
			{
				92,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[6] = new Scanner.Table(22, 30, 11, new sbyte[]
			{
				89,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[7] = new Scanner.Table(23, 30, 86, new sbyte[]
			{
				-1,
				86,
				86,
				86,
				86,
				86,
				-1,
				-1,
				-1,
				-1,
				-1,
				86,
				-1,
				-1,
				86,
				118,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				86,
				86,
				86,
				86,
				87,
				-1,
				87
			});
			Scanner.NxS[8] = new Scanner.Table(23, 30, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				8,
				117,
				8
			});
			Scanner.NxS[9] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[10] = new Scanner.Table(23, 31, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				8,
				117,
				8,
				82
			});
			Scanner.NxS[11] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[12] = new Scanner.Table(23, 34, 11, new sbyte[]
			{
				-1,
				80,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				79
			});
			Scanner.NxS[13] = new Scanner.Table(4, 34, -1, new sbyte[]
			{
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				72,
				11,
				11,
				73,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11
			});
			Scanner.NxS[14] = new Scanner.Table(18, 34, 11, new sbyte[]
			{
				69,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				68,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[15] = new Scanner.Table(4, 34, -1, new sbyte[]
			{
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				62,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11
			});
			Scanner.NxS[16] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				58,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[17] = new Scanner.Table(4, 34, -1, new sbyte[]
			{
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				50,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11
			});
			Scanner.NxS[18] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				44,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[19] = new Scanner.Table(23, 19, -1, new sbyte[]
			{
				19,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				19
			});
			Scanner.NxS[20] = new Scanner.Table(23, 34, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				39
			});
			Scanner.NxS[21] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				35,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[22] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[23] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[24] = new Scanner.Table(30, 1, -1, new sbyte[]
			{
				34
			});
			Scanner.NxS[25] = new Scanner.Table(30, 1, -1, new sbyte[]
			{
				33
			});
			Scanner.NxS[26] = new Scanner.Table(30, 1, -1, new sbyte[]
			{
				32
			});
			Scanner.NxS[27] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[28] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[29] = new Scanner.Table(29, 17, 106, new sbyte[]
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				106,
				-1,
				-1,
				106,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			});
			Scanner.NxS[30] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[31] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[32] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[33] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[34] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[35] = new Scanner.Table(4, 34, -1, new sbyte[]
			{
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				36,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11
			});
			Scanner.NxS[36] = new Scanner.Table(23, 34, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				37
			});
			Scanner.NxS[37] = new Scanner.Table(4, 34, -1, new sbyte[]
			{
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				38,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11
			});
			Scanner.NxS[38] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[39] = new Scanner.Table(21, 31, 11, new sbyte[]
			{
				40,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[40] = new Scanner.Table(4, 34, -1, new sbyte[]
			{
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				41,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11
			});
			Scanner.NxS[41] = new Scanner.Table(23, 34, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				42
			});
			Scanner.NxS[42] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				107,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[43] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[44] = new Scanner.Table(18, 34, 11, new sbyte[]
			{
				45,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[45] = new Scanner.Table(4, 34, -1, new sbyte[]
			{
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				46,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11
			});
			Scanner.NxS[46] = new Scanner.Table(23, 34, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				47
			});
			Scanner.NxS[47] = new Scanner.Table(23, 33, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				48
			});
			Scanner.NxS[48] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				109,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[49] = new Scanner.Table(23, 1, -1, new sbyte[]
			{
				109
			});
			Scanner.NxS[50] = new Scanner.Table(18, 34, 11, new sbyte[]
			{
				52,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				51,
				11,
				11,
				-1
			});
			Scanner.NxS[51] = new Scanner.Table(4, 34, -1, new sbyte[]
			{
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				54,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11
			});
			Scanner.NxS[52] = new Scanner.Table(19, 33, 11, new sbyte[]
			{
				53,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[53] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[54] = new Scanner.Table(23, 33, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				55
			});
			Scanner.NxS[55] = new Scanner.Table(4, 34, -1, new sbyte[]
			{
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				56,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11
			});
			Scanner.NxS[56] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				113,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[57] = new Scanner.Table(23, 1, -1, new sbyte[]
			{
				113
			});
			Scanner.NxS[58] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				59,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[59] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				60,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[60] = new Scanner.Table(23, 33, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				61
			});
			Scanner.NxS[61] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[62] = new Scanner.Table(23, 33, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				63,
				11,
				11,
				-1,
				11,
				11,
				11,
				64
			});
			Scanner.NxS[63] = new Scanner.Table(4, 34, -1, new sbyte[]
			{
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				65,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11
			});
			Scanner.NxS[64] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[65] = new Scanner.Table(19, 33, 11, new sbyte[]
			{
				66,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[66] = new Scanner.Table(23, 33, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				67
			});
			Scanner.NxS[67] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[68] = new Scanner.Table(21, 31, 11, new sbyte[]
			{
				71,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[69] = new Scanner.Table(19, 33, 11, new sbyte[]
			{
				70,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[70] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[71] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[72] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				77,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[73] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				74,
				11,
				11,
				-1
			});
			Scanner.NxS[74] = new Scanner.Table(18, 34, 11, new sbyte[]
			{
				75,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[75] = new Scanner.Table(4, 34, -1, new sbyte[]
			{
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				76,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11
			});
			Scanner.NxS[76] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[77] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				78,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[78] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[79] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				75,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[80] = new Scanner.Table(20, 32, 11, new sbyte[]
			{
				81,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[81] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[82] = new Scanner.Table(4, 34, -1, new sbyte[]
			{
				11,
				11,
				11,
				11,
				83,
				-1,
				83,
				11,
				83,
				11,
				11,
				83,
				83,
				83,
				11,
				83,
				11,
				83,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11
			});
			Scanner.NxS[83] = new Scanner.Table(4, 34, -1, new sbyte[]
			{
				11,
				11,
				11,
				11,
				84,
				-1,
				84,
				11,
				84,
				11,
				11,
				84,
				84,
				84,
				11,
				84,
				11,
				84,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11
			});
			Scanner.NxS[84] = new Scanner.Table(4, 34, -1, new sbyte[]
			{
				11,
				11,
				11,
				11,
				83,
				-1,
				83,
				11,
				83,
				11,
				11,
				83,
				83,
				83,
				11,
				83,
				11,
				83,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11
			});
			Scanner.NxS[85] = new Scanner.Table(8, 3, -1, new sbyte[]
			{
				85,
				-1,
				85
			});
			Scanner.NxS[86] = new Scanner.Table(23, 29, 86, new sbyte[]
			{
				-1,
				86,
				86,
				86,
				86,
				86,
				-1,
				-1,
				-1,
				-1,
				-1,
				86,
				-1,
				-1,
				86,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				86,
				86,
				86,
				86,
				86,
				-1
			});
			Scanner.NxS[87] = new Scanner.Table(23, 30, 86, new sbyte[]
			{
				-1,
				86,
				86,
				86,
				86,
				86,
				-1,
				-1,
				-1,
				-1,
				-1,
				86,
				-1,
				-1,
				86,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				86,
				86,
				86,
				86,
				87,
				117,
				87
			});
			Scanner.NxS[88] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[89] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				90,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[90] = new Scanner.Table(4, 34, -1, new sbyte[]
			{
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				91,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11
			});
			Scanner.NxS[91] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[92] = new Scanner.Table(21, 31, 11, new sbyte[]
			{
				93,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[93] = new Scanner.Table(4, 34, -1, new sbyte[]
			{
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				94,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11
			});
			Scanner.NxS[94] = new Scanner.Table(23, 33, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				95
			});
			Scanner.NxS[95] = new Scanner.Table(4, 34, -1, new sbyte[]
			{
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				96,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11
			});
			Scanner.NxS[96] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[97] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				100,
				11,
				11,
				-1
			});
			Scanner.NxS[98] = new Scanner.Table(23, 33, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				11,
				11,
				99
			});
			Scanner.NxS[99] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[100] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				101,
				11,
				11,
				-1
			});
			Scanner.NxS[101] = new Scanner.Table(23, 29, 11, new sbyte[]
			{
				-1,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				-1
			});
			Scanner.NxS[102] = new Scanner.Table(0, 0, -1, null);
			Scanner.NxS[103] = new Scanner.Table(2, 2, 120, new sbyte[]
			{
				102,
				121
			});
			Scanner.NxS[104] = new Scanner.Table(1, 1, -1, new sbyte[]
			{
				122
			});
			Scanner.NxS[105] = new Scanner.Table(13, 40, 11, new sbyte[]
			{
				12,
				11,
				11,
				13,
				14,
				15,
				16,
				11,
				17,
				18,
				19,
				20,
				11,
				21,
				11,
				11,
				22,
				23,
				24,
				25,
				26,
				11,
				27,
				28,
				11,
				29,
				1,
				30,
				19,
				1,
				2,
				3,
				1,
				4,
				5,
				6,
				7,
				8,
				9,
				10
			});
			Scanner.NxS[106] = new Scanner.Table(29, 17, 106, new sbyte[]
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				106,
				-1,
				-1,
				106,
				-1,
				31,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			});
			Scanner.NxS[107] = new Scanner.Table(12, 1, -1, new sbyte[]
			{
				108
			});
			Scanner.NxS[108] = new Scanner.Table(28, 1, -1, new sbyte[]
			{
				43
			});
			Scanner.NxS[109] = new Scanner.Table(22, 1, -1, new sbyte[]
			{
				110
			});
			Scanner.NxS[110] = new Scanner.Table(4, 1, -1, new sbyte[]
			{
				111
			});
			Scanner.NxS[111] = new Scanner.Table(13, 1, -1, new sbyte[]
			{
				112
			});
			Scanner.NxS[112] = new Scanner.Table(24, 1, -1, new sbyte[]
			{
				49
			});
			Scanner.NxS[113] = new Scanner.Table(16, 1, -1, new sbyte[]
			{
				114
			});
			Scanner.NxS[114] = new Scanner.Table(14, 1, -1, new sbyte[]
			{
				115
			});
			Scanner.NxS[115] = new Scanner.Table(24, 1, -1, new sbyte[]
			{
				116
			});
			Scanner.NxS[116] = new Scanner.Table(25, 1, -1, new sbyte[]
			{
				57
			});
			Scanner.NxS[117] = new Scanner.Table(8, 3, -1, new sbyte[]
			{
				85,
				-1,
				85
			});
			Scanner.NxS[118] = new Scanner.Table(29, 17, 119, new sbyte[]
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				119,
				-1,
				-1,
				119,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			});
			Scanner.NxS[119] = new Scanner.Table(29, 17, 119, new sbyte[]
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				119,
				-1,
				-1,
				119,
				-1,
				88,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			});
			Scanner.NxS[120] = new Scanner.Table(2, 2, 120, new sbyte[]
			{
				102,
				121
			});
			Scanner.NxS[121] = new Scanner.Table(2, 2, 120, new sbyte[]
			{
				103,
				121
			});
			Scanner.NxS[122] = new Scanner.Table(1, 1, 122, new sbyte[]
			{
				104
			});
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000BD80 File Offset: 0x00009F80
		private int NextState(int qStat)
		{
			if (this.chr == -1)
			{
				if (qStat > 104 || qStat == this.currentStart)
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
					num += 42;
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

		// Token: 0x0600014E RID: 334 RVA: 0x0000BE1C File Offset: 0x0000A01C
		private int NextState()
		{
			if (this.chr == -1)
			{
				if (this.state > 104 || this.state == this.currentStart)
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
					num += 42;
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

		// Token: 0x0600014F RID: 335 RVA: 0x0000BED3 File Offset: 0x0000A0D3
		public Scanner(Stream file)
		{
			this.buffer = Scanner.TextBuff.NewTextBuff(file);
			this.cNum = -1;
			this.chr = 10;
			this.GetChr();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000BF04 File Offset: 0x0000A104
		public Scanner()
		{
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000BF14 File Offset: 0x0000A114
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

		// Token: 0x06000152 RID: 338 RVA: 0x0000BF66 File Offset: 0x0000A166
		private void MarkToken()
		{
			this.tokPos = this.buffer.ReadPos;
			this.tokNum = this.cNum;
			this.tokLin = this.lNum;
			this.tokCol = this.cNum - this.lineStartNum;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
		private void MarkEnd()
		{
			this.tokTxt = null;
			this.tokLen = this.cNum - this.tokNum;
			this.tokEPos = this.buffer.ReadPos;
			this.tokELin = this.lNum;
			this.tokECol = this.cNum - this.lineStartNum;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000BFFB File Offset: 0x0000A1FB
		public void SetSource(string source, int offset)
		{
			this.buffer = new Scanner.StringBuff(source);
			this.buffer.Pos = offset;
			this.cNum = offset - 1;
			this.chr = 10;
			this.GetChr();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000C02C File Offset: 0x0000A22C
		public int GetNext(ref int state, out int start, out int end)
		{
			this.EolState = state;
			Tokens result = (Tokens)this.Scan();
			state = this.EolState;
			start = this.tokPos;
			end = this.tokEPos - 1;
			return (int)result;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000C064 File Offset: 0x0000A264
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

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000C081 File Offset: 0x0000A281
		private int yyleng
		{
			get
			{
				return this.tokLen;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000C089 File Offset: 0x0000A289
		private int yypos
		{
			get
			{
				return this.tokPos;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000C091 File Offset: 0x0000A291
		private int yyline
		{
			get
			{
				return this.tokLin;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000C099 File Offset: 0x0000A299
		private int yycol
		{
			get
			{
				return this.tokCol;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000C0A1 File Offset: 0x0000A2A1
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

		// Token: 0x0600015C RID: 348 RVA: 0x0000C0D0 File Offset: 0x0000A2D0
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

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000C112 File Offset: 0x0000A312
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000C11A File Offset: 0x0000A31A
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

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000C123 File Offset: 0x0000A323
		// (set) Token: 0x06000160 RID: 352 RVA: 0x0000C12B File Offset: 0x0000A32B
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

		// Token: 0x06000161 RID: 353 RVA: 0x0000C134 File Offset: 0x0000A334
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
						if (flag && num > 104)
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
							if (this.state <= 104)
							{
								flag = true;
							}
						}
					}
					if (this.state > 104)
					{
						this.state = this.currentStart;
					}
					else
					{
						this.MarkEnd();
						switch (this.state)
						{
						case 0:
							goto IL_254;
						case 1:
						case 2:
						case 3:
						case 24:
						case 29:
							goto IL_25C;
						case 4:
						case 5:
						case 6:
						case 7:
						case 11:
						case 12:
						case 13:
						case 14:
						case 15:
						case 16:
						case 17:
						case 18:
						case 20:
						case 21:
						case 31:
						case 35:
						case 36:
						case 37:
						case 40:
						case 41:
						case 42:
						case 44:
						case 45:
						case 46:
						case 47:
						case 50:
						case 51:
						case 52:
						case 54:
						case 55:
						case 58:
						case 59:
						case 60:
						case 62:
						case 63:
						case 65:
						case 66:
						case 68:
						case 69:
						case 72:
						case 73:
						case 74:
						case 75:
						case 77:
						case 79:
						case 80:
						case 82:
						case 83:
						case 89:
						case 90:
						case 92:
						case 93:
						case 94:
						case 95:
						case 97:
						case 98:
						case 100:
							goto IL_265;
						case 8:
						case 10:
						case 87:
							goto IL_26E;
						case 9:
							goto IL_277;
						case 19:
							goto IL_280;
						case 22:
							goto IL_289;
						case 23:
							goto IL_292;
						case 25:
							goto IL_29B;
						case 26:
							goto IL_2A4;
						case 27:
							goto IL_2AD;
						case 28:
							goto IL_2B6;
						case 30:
							goto IL_2BF;
						case 32:
							goto IL_2C8;
						case 33:
							goto IL_2D1;
						case 34:
							goto IL_2DA;
						case 38:
							goto IL_2E3;
						case 39:
							goto IL_2EC;
						case 43:
							goto IL_2F2;
						case 48:
						case 49:
							goto IL_2F8;
						case 53:
							goto IL_2FD;
						case 56:
						case 57:
							goto IL_303;
						case 61:
							goto IL_308;
						case 64:
							goto IL_30D;
						case 67:
							goto IL_313;
						case 70:
							goto IL_318;
						case 71:
							goto IL_31E;
						case 76:
							goto IL_324;
						case 78:
							goto IL_32A;
						case 81:
							goto IL_330;
						case 84:
							goto IL_335;
						case 85:
							goto IL_33B;
						case 86:
						case 88:
							goto IL_341;
						case 91:
							goto IL_347;
						case 96:
							goto IL_34D;
						case 99:
							goto IL_352;
						case 101:
							goto IL_358;
						case 102:
						case 103:
							goto IL_35E;
						case 104:
							goto IL_364;
						}
					}
				}
				IL_254:
				return 2;
				IL_25C:
				return 36;
				IL_265:
				return 17;
				IL_26E:
				return 19;
				IL_277:
				return 16;
				IL_280:
				return 40;
				IL_289:
				return 25;
				IL_292:
				return 28;
				IL_29B:
				return 30;
				IL_2A4:
				return 31;
				IL_2AD:
				return 37;
				IL_2B6:
				return 38;
				IL_2BF:
				return 15;
				IL_2C8:
				return 33;
				IL_2D1:
				return 32;
				IL_2DA:
				return 29;
				IL_2E3:
				return 11;
				IL_2EC:
				return 26;
				IL_2F2:
				return 12;
				IL_2F8:
				return 5;
				IL_2FD:
				return 14;
				IL_303:
				return 6;
				IL_308:
				return 7;
				IL_30D:
				return 10;
				IL_313:
				return 3;
				IL_318:
				return 13;
				IL_31E:
				return 27;
				IL_324:
				return 23;
				IL_32A:
				return 9;
				IL_330:
				return 8;
				IL_335:
				return 21;
				IL_33B:
				return 20;
				IL_341:
				return 18;
				IL_347:
				return 35;
				IL_34D:
				return 4;
				IL_352:
				return 34;
				IL_358:
				return 24;
				IL_35E:
				return 22;
				IL_364:
				result2 = 22;
			}
			finally
			{
				this.LoadYylval();
			}
			return result2;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
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
				if (flag && next > 104)
				{
					this.SaveStateAndPos(ctx);
				}
				this.state = next;
				if (this.state == 0)
				{
					return Scanner.Result.accept;
				}
				this.GetChr();
				flag = (this.state <= 104);
			}
			if (flag)
			{
				return Scanner.Result.accept;
			}
			return Scanner.Result.noMatch;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000C54A File Offset: 0x0000A74A
		private void SaveStateAndPos(Scanner.Context ctx)
		{
			ctx.bPos = this.buffer.Pos;
			ctx.cNum = this.cNum;
			ctx.state = this.state;
			ctx.cChr = this.chr;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000C581 File Offset: 0x0000A781
		private void RestoreStateAndPos(Scanner.Context ctx)
		{
			this.buffer.Pos = ctx.bPos;
			this.cNum = ctx.cNum;
			this.state = ctx.state;
			this.chr = ctx.cChr;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000C5B8 File Offset: 0x0000A7B8
		private void RestorePos(Scanner.Context ctx)
		{
			this.buffer.Pos = ctx.bPos;
			this.cNum = ctx.cNum;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000C5D7 File Offset: 0x0000A7D7
		internal void BEGIN(int next)
		{
			this.CurrentSc = next;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
		internal void ECHO()
		{
			Console.Out.Write(this.yytext);
		}

		// Token: 0x040000DE RID: 222
		private const int maxAccept = 104;

		// Token: 0x040000DF RID: 223
		private const int initial = 105;

		// Token: 0x040000E0 RID: 224
		private const int eofNum = 0;

		// Token: 0x040000E1 RID: 225
		private const int goStart = -1;

		// Token: 0x040000E2 RID: 226
		private const int INITIAL = 0;

		// Token: 0x040000E3 RID: 227
		public ScanBuff buffer;

		// Token: 0x040000E4 RID: 228
		private IErrorHandler handler;

		// Token: 0x040000E5 RID: 229
		private int scState;

		// Token: 0x040000E6 RID: 230
		private static int parserMax = Scanner.GetMaxParseToken();

		// Token: 0x040000E7 RID: 231
		private readonly string query;

		// Token: 0x040000E8 RID: 232
		private int state;

		// Token: 0x040000E9 RID: 233
		private int currentStart = 105;

		// Token: 0x040000EA RID: 234
		private int chr;

		// Token: 0x040000EB RID: 235
		private int cNum;

		// Token: 0x040000EC RID: 236
		private int lNum;

		// Token: 0x040000ED RID: 237
		private int lineStartNum;

		// Token: 0x040000EE RID: 238
		private int tokPos;

		// Token: 0x040000EF RID: 239
		private int tokNum;

		// Token: 0x040000F0 RID: 240
		private int tokLen;

		// Token: 0x040000F1 RID: 241
		private int tokCol;

		// Token: 0x040000F2 RID: 242
		private int tokLin;

		// Token: 0x040000F3 RID: 243
		private int tokEPos;

		// Token: 0x040000F4 RID: 244
		private int tokECol;

		// Token: 0x040000F5 RID: 245
		private int tokELin;

		// Token: 0x040000F6 RID: 246
		private string tokTxt;

		// Token: 0x040000F7 RID: 247
		private static int[] startState;

		// Token: 0x040000F8 RID: 248
		private static sbyte[] map0;

		// Token: 0x040000F9 RID: 249
		private static Scanner.Table[] NxS;

		// Token: 0x02000028 RID: 40
		private enum Result
		{
			// Token: 0x040000FB RID: 251
			accept,
			// Token: 0x040000FC RID: 252
			noMatch,
			// Token: 0x040000FD RID: 253
			contextFound
		}

		// Token: 0x02000029 RID: 41
		private struct Table
		{
			// Token: 0x06000168 RID: 360 RVA: 0x0000C5F2 File Offset: 0x0000A7F2
			public Table(int m, int x, int d, sbyte[] n)
			{
				this.min = m;
				this.rng = x;
				this.dflt = d;
				this.nxt = n;
			}

			// Token: 0x040000FE RID: 254
			public int min;

			// Token: 0x040000FF RID: 255
			public int rng;

			// Token: 0x04000100 RID: 256
			public int dflt;

			// Token: 0x04000101 RID: 257
			public sbyte[] nxt;
		}

		// Token: 0x0200002A RID: 42
		internal class Context
		{
			// Token: 0x04000102 RID: 258
			public int bPos;

			// Token: 0x04000103 RID: 259
			public int cNum;

			// Token: 0x04000104 RID: 260
			public int state;

			// Token: 0x04000105 RID: 261
			public int cChr;
		}

		// Token: 0x0200002B RID: 43
		public sealed class StringBuff : ScanBuff
		{
			// Token: 0x0600016A RID: 362 RVA: 0x0000C619 File Offset: 0x0000A819
			public StringBuff(string str)
			{
				this.str = str;
				this.sLen = str.Length;
			}

			// Token: 0x0600016B RID: 363 RVA: 0x0000C634 File Offset: 0x0000A834
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

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x0600016C RID: 364 RVA: 0x0000C68C File Offset: 0x0000A88C
			public override int ReadPos
			{
				get
				{
					return this.bPos - 1;
				}
			}

			// Token: 0x0600016D RID: 365 RVA: 0x0000C696 File Offset: 0x0000A896
			public override int Peek()
			{
				if (this.bPos < this.sLen)
				{
					return (int)this.str[this.bPos];
				}
				return 10;
			}

			// Token: 0x0600016E RID: 366 RVA: 0x0000C6BA File Offset: 0x0000A8BA
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

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x0600016F RID: 367 RVA: 0x0000C6E6 File Offset: 0x0000A8E6
			// (set) Token: 0x06000170 RID: 368 RVA: 0x0000C6EE File Offset: 0x0000A8EE
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

			// Token: 0x04000106 RID: 262
			private readonly string str;

			// Token: 0x04000107 RID: 263
			private int bPos;

			// Token: 0x04000108 RID: 264
			private readonly int sLen;
		}

		// Token: 0x0200002C RID: 44
		public sealed class StreamBuff : ScanBuff
		{
			// Token: 0x06000171 RID: 369 RVA: 0x0000C6F7 File Offset: 0x0000A8F7
			public StreamBuff(Stream str)
			{
				this.bStrm = new BufferedStream(str);
			}

			// Token: 0x06000172 RID: 370 RVA: 0x0000C70B File Offset: 0x0000A90B
			public override int Read()
			{
				return this.bStrm.ReadByte();
			}

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x06000173 RID: 371 RVA: 0x0000C718 File Offset: 0x0000A918
			public override int ReadPos
			{
				get
				{
					return (int)this.bStrm.Position - 1;
				}
			}

			// Token: 0x06000174 RID: 372 RVA: 0x0000C728 File Offset: 0x0000A928
			public override int Peek()
			{
				int result = this.bStrm.ReadByte();
				this.bStrm.Seek(-1L, SeekOrigin.Current);
				return result;
			}

			// Token: 0x06000175 RID: 373 RVA: 0x0000C754 File Offset: 0x0000A954
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

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x06000176 RID: 374 RVA: 0x0000C7BE File Offset: 0x0000A9BE
			// (set) Token: 0x06000177 RID: 375 RVA: 0x0000C7CC File Offset: 0x0000A9CC
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

			// Token: 0x04000109 RID: 265
			private const int delta = 1;

			// Token: 0x0400010A RID: 266
			private BufferedStream bStrm;
		}

		// Token: 0x0200002D RID: 45
		public class TextBuff : ScanBuff
		{
			// Token: 0x06000178 RID: 376 RVA: 0x0000C7DB File Offset: 0x0000A9DB
			private Exception BadUTF8()
			{
				return new Exception(string.Format("BadUTF8 Character", new object[0]));
			}

			// Token: 0x06000179 RID: 377 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
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

			// Token: 0x0600017A RID: 378 RVA: 0x0000C873 File Offset: 0x0000AA73
			protected TextBuff(Stream str)
			{
				this.bStrm = new BufferedStream(str);
			}

			// Token: 0x0600017B RID: 379 RVA: 0x0000C890 File Offset: 0x0000AA90
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

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x0600017C RID: 380 RVA: 0x0000C95E File Offset: 0x0000AB5E
			public sealed override int ReadPos
			{
				get
				{
					return (int)this.bStrm.Position - this.delta;
				}
			}

			// Token: 0x0600017D RID: 381 RVA: 0x0000C974 File Offset: 0x0000AB74
			public sealed override int Peek()
			{
				int result = this.Read();
				this.bStrm.Seek((long)(-(long)this.delta), SeekOrigin.Current);
				return result;
			}

			// Token: 0x0600017E RID: 382 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
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

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x0600017F RID: 383 RVA: 0x0000CA10 File Offset: 0x0000AC10
			// (set) Token: 0x06000180 RID: 384 RVA: 0x0000CA1E File Offset: 0x0000AC1E
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

			// Token: 0x0400010B RID: 267
			protected BufferedStream bStrm;

			// Token: 0x0400010C RID: 268
			protected int delta = 1;
		}

		// Token: 0x0200002E RID: 46
		public sealed class BigEndTextBuff : Scanner.TextBuff
		{
			// Token: 0x06000181 RID: 385 RVA: 0x0000CA2D File Offset: 0x0000AC2D
			internal BigEndTextBuff(Stream str) : base(str)
			{
			}

			// Token: 0x06000182 RID: 386 RVA: 0x0000CA38 File Offset: 0x0000AC38
			public override int Read()
			{
				int num = this.bStrm.ReadByte();
				int num2 = this.bStrm.ReadByte();
				return (num << 8) + num2;
			}
		}

		// Token: 0x0200002F RID: 47
		public sealed class LittleEndTextBuff : Scanner.TextBuff
		{
			// Token: 0x06000183 RID: 387 RVA: 0x0000CA62 File Offset: 0x0000AC62
			internal LittleEndTextBuff(Stream str) : base(str)
			{
			}

			// Token: 0x06000184 RID: 388 RVA: 0x0000CA6C File Offset: 0x0000AC6C
			public override int Read()
			{
				int num = this.bStrm.ReadByte();
				int num2 = this.bStrm.ReadByte();
				return (num2 << 8) + num;
			}
		}
	}
}
