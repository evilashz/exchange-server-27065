using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Css
{
	// Token: 0x020001AC RID: 428
	internal class CssToken : Token
	{
		// Token: 0x0600123B RID: 4667 RVA: 0x00083253 File Offset: 0x00081453
		public CssToken()
		{
			this.Reset();
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x00083261 File Offset: 0x00081461
		public CssTokenId CssTokenId
		{
			get
			{
				return (CssTokenId)base.TokenId;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x00083269 File Offset: 0x00081469
		public CssToken.PropertyListPartMajor MajorPart
		{
			get
			{
				return this.PartMajor;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x00083271 File Offset: 0x00081471
		public CssToken.PropertyListPartMinor MinorPart
		{
			get
			{
				return this.PartMinor;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x00083279 File Offset: 0x00081479
		public bool IsPropertyListBegin
		{
			get
			{
				return (byte)(this.PartMajor & CssToken.PropertyListPartMajor.Begin) == 3;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00083287 File Offset: 0x00081487
		public bool IsPropertyListEnd
		{
			get
			{
				return (byte)(this.PartMajor & CssToken.PropertyListPartMajor.End) == 6;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x00083295 File Offset: 0x00081495
		public CssToken.PropertyEnumerator Properties
		{
			get
			{
				return new CssToken.PropertyEnumerator(this);
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x0008329D File Offset: 0x0008149D
		public CssToken.SelectorEnumerator Selectors
		{
			get
			{
				return new CssToken.SelectorEnumerator(this);
			}
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x000832A8 File Offset: 0x000814A8
		internal static bool AttemptUnescape(char[] parseBuffer, int parseEnd, ref char ch, ref int parseCurrent)
		{
			if (ch != '\\' || parseCurrent == parseEnd)
			{
				return false;
			}
			ch = parseBuffer[++parseCurrent];
			CharClass charClass = ParseSupport.GetCharClass(ch);
			int num = parseCurrent + 6;
			num = ((num < parseEnd) ? num : parseEnd);
			if (ParseSupport.HexCharacter(charClass))
			{
				int num2 = 0;
				do
				{
					num2 <<= 4;
					num2 |= ParseSupport.CharToHex(ch);
					if (parseCurrent == num)
					{
						goto IL_C3;
					}
					ch = parseBuffer[++parseCurrent];
					charClass = ParseSupport.GetCharClass(ch);
				}
				while (ParseSupport.HexCharacter(charClass));
				if (ch == '\r' && parseCurrent != parseEnd)
				{
					ch = parseBuffer[++parseCurrent];
					if (ch == '\n')
					{
						charClass = ParseSupport.GetCharClass(ch);
					}
					else
					{
						parseCurrent--;
					}
				}
				if (ch != ' ' && ch != '\t' && ch != '\r' && ch != '\n' && ch != '\f')
				{
					parseCurrent--;
				}
				IL_C3:
				ch = (char)num2;
				return true;
			}
			if (ch >= ' ' && ch != '\u007f')
			{
				return true;
			}
			parseCurrent--;
			ch = '\\';
			return false;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00083398 File Offset: 0x00081598
		internal new void Reset()
		{
			this.PartMajor = CssToken.PropertyListPartMajor.None;
			this.PartMinor = CssToken.PropertyListPartMinor.Empty;
			this.PropertyHead = (this.PropertyTail = 0);
			this.CurrentProperty = -1;
			this.SelectorHead = (this.SelectorTail = 0);
			this.CurrentSelector = -1;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x000833E4 File Offset: 0x000815E4
		protected internal void WriteEscapedOriginalTo(ref Token.Fragment fragment, ITextSink sink)
		{
			int num = fragment.Head;
			if (num != fragment.Tail)
			{
				int num2 = fragment.HeadOffset;
				do
				{
					Token.RunEntry runEntry = this.RunList[num];
					if (runEntry.Type == (RunType)2147483648U || runEntry.Type == (RunType)3221225472U)
					{
						if (runEntry.Kind == 184549376U && this.Buffer[num2] == '/')
						{
							string text = "/**/";
							for (int i = 0; i < text.Length; i++)
							{
								sink.Write((int)text[i]);
							}
						}
						else
						{
							this.EscapeAndWriteBuffer(this.Buffer, num2, runEntry.Length, sink);
						}
					}
					num2 += runEntry.Length;
				}
				while (++num != fragment.Tail && !sink.IsEnough);
			}
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000834B8 File Offset: 0x000816B8
		private void EscapeAndWriteBuffer(char[] buffer, int offset, int length, ITextSink sink)
		{
			int num = offset;
			int i = offset;
			while (i < offset + length)
			{
				char c = buffer[i];
				if (c == '>' || c == '<')
				{
					if (i - num > 0)
					{
						sink.Write(buffer, num, i - num);
					}
					uint num2 = (uint)c;
					char[] array = new char[]
					{
						'\\',
						'\0',
						'\0',
						' '
					};
					for (int j = 2; j > 0; j--)
					{
						uint num3 = num2 & 15U;
						array[j] = (char)((ulong)num3 + (ulong)((num3 < 10U) ? 48L : 55L));
						num2 >>= 4;
					}
					sink.Write(array, 0, 4);
					i = (num = i + 1);
				}
				else
				{
					CssToken.AttemptUnescape(buffer, offset + length, ref c, ref i);
					i++;
				}
			}
			sink.Write(buffer, num, length - (num - offset));
		}

		// Token: 0x040012D0 RID: 4816
		protected internal CssToken.PropertyListPartMajor PartMajor;

		// Token: 0x040012D1 RID: 4817
		protected internal CssToken.PropertyListPartMinor PartMinor;

		// Token: 0x040012D2 RID: 4818
		protected internal CssToken.PropertyEntry[] PropertyList;

		// Token: 0x040012D3 RID: 4819
		protected internal int PropertyHead;

		// Token: 0x040012D4 RID: 4820
		protected internal int PropertyTail;

		// Token: 0x040012D5 RID: 4821
		protected internal int CurrentProperty;

		// Token: 0x040012D6 RID: 4822
		protected internal Token.FragmentPosition PropertyNamePosition;

		// Token: 0x040012D7 RID: 4823
		protected internal Token.FragmentPosition PropertyValuePosition;

		// Token: 0x040012D8 RID: 4824
		protected internal CssToken.SelectorEntry[] SelectorList;

		// Token: 0x040012D9 RID: 4825
		protected internal int SelectorHead;

		// Token: 0x040012DA RID: 4826
		protected internal int SelectorTail;

		// Token: 0x040012DB RID: 4827
		protected internal int CurrentSelector;

		// Token: 0x040012DC RID: 4828
		protected internal Token.FragmentPosition SelectorNamePosition;

		// Token: 0x040012DD RID: 4829
		protected internal Token.FragmentPosition SelectorClassPosition;

		// Token: 0x020001AD RID: 429
		public enum PropertyListPartMajor : byte
		{
			// Token: 0x040012DF RID: 4831
			None,
			// Token: 0x040012E0 RID: 4832
			Begin = 3,
			// Token: 0x040012E1 RID: 4833
			Continue = 2,
			// Token: 0x040012E2 RID: 4834
			End = 6,
			// Token: 0x040012E3 RID: 4835
			Complete
		}

		// Token: 0x020001AE RID: 430
		public enum PropertyListPartMinor : byte
		{
			// Token: 0x040012E5 RID: 4837
			Empty,
			// Token: 0x040012E6 RID: 4838
			BeginProperty = 24,
			// Token: 0x040012E7 RID: 4839
			ContinueProperty = 16,
			// Token: 0x040012E8 RID: 4840
			EndProperty = 48,
			// Token: 0x040012E9 RID: 4841
			EndPropertyWithOtherProperties = 176,
			// Token: 0x040012EA RID: 4842
			PropertyPartMask = 56,
			// Token: 0x040012EB RID: 4843
			Properties = 128
		}

		// Token: 0x020001AF RID: 431
		public enum PropertyPartMajor : byte
		{
			// Token: 0x040012ED RID: 4845
			None,
			// Token: 0x040012EE RID: 4846
			Begin = 3,
			// Token: 0x040012EF RID: 4847
			Continue = 2,
			// Token: 0x040012F0 RID: 4848
			End = 6,
			// Token: 0x040012F1 RID: 4849
			Complete,
			// Token: 0x040012F2 RID: 4850
			ValueQuoted = 64,
			// Token: 0x040012F3 RID: 4851
			Deleted = 128,
			// Token: 0x040012F4 RID: 4852
			MaskOffFlags = 7
		}

		// Token: 0x020001B0 RID: 432
		public enum PropertyPartMinor : byte
		{
			// Token: 0x040012F6 RID: 4854
			Empty,
			// Token: 0x040012F7 RID: 4855
			BeginName = 3,
			// Token: 0x040012F8 RID: 4856
			ContinueName = 2,
			// Token: 0x040012F9 RID: 4857
			EndName = 6,
			// Token: 0x040012FA RID: 4858
			EndNameWithBeginValue = 30,
			// Token: 0x040012FB RID: 4859
			EndNameWithCompleteValue = 62,
			// Token: 0x040012FC RID: 4860
			CompleteName = 7,
			// Token: 0x040012FD RID: 4861
			CompleteNameWithBeginValue = 31,
			// Token: 0x040012FE RID: 4862
			CompleteNameWithCompleteValue = 63,
			// Token: 0x040012FF RID: 4863
			BeginValue = 24,
			// Token: 0x04001300 RID: 4864
			ContinueValue = 16,
			// Token: 0x04001301 RID: 4865
			EndValue = 48,
			// Token: 0x04001302 RID: 4866
			CompleteValue = 56
		}

		// Token: 0x020001B1 RID: 433
		public struct PropertyEnumerator
		{
			// Token: 0x06001247 RID: 4679 RVA: 0x00083571 File Offset: 0x00081771
			internal PropertyEnumerator(CssToken token)
			{
				this.token = token;
			}

			// Token: 0x17000504 RID: 1284
			// (get) Token: 0x06001248 RID: 4680 RVA: 0x0008357A File Offset: 0x0008177A
			public int Count
			{
				get
				{
					return this.token.PropertyTail - this.token.PropertyHead;
				}
			}

			// Token: 0x17000505 RID: 1285
			// (get) Token: 0x06001249 RID: 4681 RVA: 0x00083594 File Offset: 0x00081794
			public int ValidCount
			{
				get
				{
					int num = 0;
					for (int i = this.token.PropertyHead; i < this.token.PropertyTail; i++)
					{
						if (!this.token.PropertyList[i].IsPropertyDeleted)
						{
							num++;
						}
					}
					return num;
				}
			}

			// Token: 0x17000506 RID: 1286
			// (get) Token: 0x0600124A RID: 4682 RVA: 0x000835E0 File Offset: 0x000817E0
			public CssProperty Current
			{
				get
				{
					return new CssProperty(this.token);
				}
			}

			// Token: 0x17000507 RID: 1287
			// (get) Token: 0x0600124B RID: 4683 RVA: 0x000835ED File Offset: 0x000817ED
			public int CurrentIndex
			{
				get
				{
					return this.token.CurrentProperty;
				}
			}

			// Token: 0x17000508 RID: 1288
			public CssProperty this[int i]
			{
				get
				{
					this.token.CurrentProperty = i;
					this.token.PropertyNamePosition.Rewind(this.token.PropertyList[i].Name);
					this.token.PropertyValuePosition.Rewind(this.token.PropertyList[i].Value);
					return new CssProperty(this.token);
				}
			}

			// Token: 0x0600124D RID: 4685 RVA: 0x0008366C File Offset: 0x0008186C
			public bool MoveNext()
			{
				if (this.token.CurrentProperty != this.token.PropertyTail)
				{
					this.token.CurrentProperty++;
					if (this.token.CurrentProperty != this.token.PropertyTail)
					{
						this.token.PropertyNamePosition.Rewind(this.token.PropertyList[this.token.CurrentProperty].Name);
						this.token.PropertyValuePosition.Rewind(this.token.PropertyList[this.token.CurrentProperty].Value);
					}
				}
				return this.token.CurrentProperty != this.token.PropertyTail;
			}

			// Token: 0x0600124E RID: 4686 RVA: 0x0008373A File Offset: 0x0008193A
			public void Rewind()
			{
				this.token.CurrentProperty = this.token.PropertyHead - 1;
			}

			// Token: 0x0600124F RID: 4687 RVA: 0x00083754 File Offset: 0x00081954
			public CssToken.PropertyEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x06001250 RID: 4688 RVA: 0x0008375C File Offset: 0x0008195C
			public bool Find(CssNameIndex nameId)
			{
				for (int i = this.token.PropertyHead; i < this.token.PropertyTail; i++)
				{
					if (this.token.PropertyList[i].NameId == nameId)
					{
						this.token.CurrentProperty = i;
						this.token.PropertyNamePosition.Rewind(this.token.PropertyList[i].Name);
						this.token.PropertyValuePosition.Rewind(this.token.PropertyList[i].Value);
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001251 RID: 4689 RVA: 0x00083800 File Offset: 0x00081A00
			[Conditional("DEBUG")]
			private void AssertCurrent()
			{
			}

			// Token: 0x04001303 RID: 4867
			private CssToken token;
		}

		// Token: 0x020001B2 RID: 434
		public struct PropertyNameTextReader
		{
			// Token: 0x06001252 RID: 4690 RVA: 0x00083802 File Offset: 0x00081A02
			internal PropertyNameTextReader(CssToken token)
			{
				this.token = token;
			}

			// Token: 0x17000509 RID: 1289
			// (get) Token: 0x06001253 RID: 4691 RVA: 0x0008380B File Offset: 0x00081A0B
			public int Length
			{
				get
				{
					return this.token.GetLength(ref this.token.PropertyList[this.token.CurrentProperty].Name);
				}
			}

			// Token: 0x06001254 RID: 4692 RVA: 0x00083838 File Offset: 0x00081A38
			public int Read(char[] buffer, int offset, int count)
			{
				return this.token.Read(ref this.token.PropertyList[this.token.CurrentProperty].Name, ref this.token.PropertyNamePosition, buffer, offset, count);
			}

			// Token: 0x06001255 RID: 4693 RVA: 0x00083880 File Offset: 0x00081A80
			public void Rewind()
			{
				this.token.PropertyNamePosition.Rewind(this.token.PropertyList[this.token.CurrentProperty].Name);
			}

			// Token: 0x06001256 RID: 4694 RVA: 0x000838B2 File Offset: 0x00081AB2
			public void WriteTo(ITextSink sink)
			{
				this.token.WriteTo(ref this.token.PropertyList[this.token.CurrentProperty].Name, sink);
			}

			// Token: 0x06001257 RID: 4695 RVA: 0x000838E0 File Offset: 0x00081AE0
			public void WriteOriginalTo(ITextSink sink)
			{
				this.token.WriteOriginalTo(ref this.token.PropertyList[this.token.CurrentProperty].Name, sink);
			}

			// Token: 0x06001258 RID: 4696 RVA: 0x0008390E File Offset: 0x00081B0E
			public string GetString(int maxSize)
			{
				return this.token.GetString(ref this.token.PropertyList[this.token.CurrentProperty].Name, maxSize);
			}

			// Token: 0x06001259 RID: 4697 RVA: 0x0008393C File Offset: 0x00081B3C
			public void MakeEmpty()
			{
				this.token.PropertyList[this.token.CurrentProperty].Name.Reset();
				this.Rewind();
			}

			// Token: 0x0600125A RID: 4698 RVA: 0x00083969 File Offset: 0x00081B69
			[Conditional("DEBUG")]
			private void AssertCurrent()
			{
			}

			// Token: 0x04001304 RID: 4868
			private CssToken token;
		}

		// Token: 0x020001B3 RID: 435
		public struct PropertyValueTextReader
		{
			// Token: 0x0600125B RID: 4699 RVA: 0x0008396B File Offset: 0x00081B6B
			internal PropertyValueTextReader(CssToken token)
			{
				this.token = token;
			}

			// Token: 0x1700050A RID: 1290
			// (get) Token: 0x0600125C RID: 4700 RVA: 0x00083974 File Offset: 0x00081B74
			public int Length
			{
				get
				{
					return this.token.GetLength(ref this.token.PropertyList[this.token.CurrentProperty].Value);
				}
			}

			// Token: 0x1700050B RID: 1291
			// (get) Token: 0x0600125D RID: 4701 RVA: 0x000839A1 File Offset: 0x00081BA1
			public bool IsEmpty
			{
				get
				{
					return this.token.IsFragmentEmpty(ref this.token.PropertyList[this.token.CurrentProperty].Value);
				}
			}

			// Token: 0x1700050C RID: 1292
			// (get) Token: 0x0600125E RID: 4702 RVA: 0x000839CE File Offset: 0x00081BCE
			public bool IsContiguous
			{
				get
				{
					return this.token.IsContiguous(ref this.token.PropertyList[this.token.CurrentProperty].Value);
				}
			}

			// Token: 0x1700050D RID: 1293
			// (get) Token: 0x0600125F RID: 4703 RVA: 0x000839FC File Offset: 0x00081BFC
			public BufferString ContiguousBufferString
			{
				get
				{
					return new BufferString(this.token.Buffer, this.token.PropertyList[this.token.CurrentProperty].Value.HeadOffset, this.token.RunList[this.token.PropertyList[this.token.CurrentProperty].Value.Head].Length);
				}
			}

			// Token: 0x06001260 RID: 4704 RVA: 0x00083A78 File Offset: 0x00081C78
			public int Read(char[] buffer, int offset, int count)
			{
				return this.token.Read(ref this.token.PropertyList[this.token.CurrentProperty].Value, ref this.token.PropertyValuePosition, buffer, offset, count);
			}

			// Token: 0x06001261 RID: 4705 RVA: 0x00083AC0 File Offset: 0x00081CC0
			public void Rewind()
			{
				this.token.PropertyValuePosition.Rewind(this.token.PropertyList[this.token.CurrentProperty].Value);
			}

			// Token: 0x06001262 RID: 4706 RVA: 0x00083AF2 File Offset: 0x00081CF2
			public void WriteTo(ITextSink sink)
			{
				this.token.WriteTo(ref this.token.PropertyList[this.token.CurrentProperty].Value, sink);
			}

			// Token: 0x06001263 RID: 4707 RVA: 0x00083B20 File Offset: 0x00081D20
			public void WriteOriginalTo(ITextSink sink)
			{
				this.token.WriteOriginalTo(ref this.token.PropertyList[this.token.CurrentProperty].Value, sink);
			}

			// Token: 0x06001264 RID: 4708 RVA: 0x00083B4E File Offset: 0x00081D4E
			public void WriteEscapedOriginalTo(ITextSink sink)
			{
				this.token.WriteEscapedOriginalTo(ref this.token.PropertyList[this.token.CurrentProperty].Value, sink);
			}

			// Token: 0x06001265 RID: 4709 RVA: 0x00083B7C File Offset: 0x00081D7C
			public string GetString(int maxSize)
			{
				return this.token.GetString(ref this.token.PropertyList[this.token.CurrentProperty].Value, maxSize);
			}

			// Token: 0x06001266 RID: 4710 RVA: 0x00083BAA File Offset: 0x00081DAA
			public bool CaseInsensitiveCompareEqual(string str)
			{
				return this.token.CaseInsensitiveCompareEqual(ref this.token.PropertyList[this.token.CurrentProperty].Value, str);
			}

			// Token: 0x06001267 RID: 4711 RVA: 0x00083BD8 File Offset: 0x00081DD8
			public bool CaseInsensitiveContainsSubstring(string str)
			{
				return this.token.CaseInsensitiveContainsSubstring(ref this.token.PropertyList[this.token.CurrentProperty].Value, str);
			}

			// Token: 0x06001268 RID: 4712 RVA: 0x00083C06 File Offset: 0x00081E06
			public void MakeEmpty()
			{
				this.token.PropertyList[this.token.CurrentProperty].Value.Reset();
				this.Rewind();
			}

			// Token: 0x06001269 RID: 4713 RVA: 0x00083C33 File Offset: 0x00081E33
			[Conditional("DEBUG")]
			private void AssertCurrent()
			{
			}

			// Token: 0x04001305 RID: 4869
			private CssToken token;
		}

		// Token: 0x020001B4 RID: 436
		public struct SelectorEnumerator
		{
			// Token: 0x0600126A RID: 4714 RVA: 0x00083C35 File Offset: 0x00081E35
			internal SelectorEnumerator(CssToken token)
			{
				this.token = token;
			}

			// Token: 0x1700050E RID: 1294
			// (get) Token: 0x0600126B RID: 4715 RVA: 0x00083C3E File Offset: 0x00081E3E
			public int Count
			{
				get
				{
					return this.token.SelectorTail - this.token.SelectorHead;
				}
			}

			// Token: 0x1700050F RID: 1295
			// (get) Token: 0x0600126C RID: 4716 RVA: 0x00083C58 File Offset: 0x00081E58
			public int ValidCount
			{
				get
				{
					int num = 0;
					for (int i = this.token.SelectorHead; i < this.token.SelectorTail; i++)
					{
						if (!this.token.SelectorList[i].IsSelectorDeleted)
						{
							num++;
						}
					}
					return num;
				}
			}

			// Token: 0x17000510 RID: 1296
			// (get) Token: 0x0600126D RID: 4717 RVA: 0x00083CA4 File Offset: 0x00081EA4
			public CssSelector Current
			{
				get
				{
					return new CssSelector(this.token);
				}
			}

			// Token: 0x17000511 RID: 1297
			// (get) Token: 0x0600126E RID: 4718 RVA: 0x00083CB1 File Offset: 0x00081EB1
			public int CurrentIndex
			{
				get
				{
					return this.token.CurrentSelector;
				}
			}

			// Token: 0x17000512 RID: 1298
			public CssSelector this[int i]
			{
				get
				{
					this.token.CurrentSelector = i;
					this.token.SelectorNamePosition.Rewind(this.token.SelectorList[i].Name);
					this.token.SelectorClassPosition.Rewind(this.token.SelectorList[i].ClassName);
					return new CssSelector(this.token);
				}
			}

			// Token: 0x06001270 RID: 4720 RVA: 0x00083D30 File Offset: 0x00081F30
			public bool MoveNext()
			{
				if (this.token.CurrentSelector != this.token.SelectorTail)
				{
					this.token.CurrentSelector++;
					if (this.token.CurrentSelector != this.token.SelectorTail)
					{
						this.token.SelectorNamePosition.Rewind(this.token.SelectorList[this.token.CurrentSelector].Name);
						this.token.SelectorClassPosition.Rewind(this.token.SelectorList[this.token.CurrentSelector].ClassName);
					}
				}
				return this.token.CurrentSelector != this.token.SelectorTail;
			}

			// Token: 0x06001271 RID: 4721 RVA: 0x00083DFE File Offset: 0x00081FFE
			public void Rewind()
			{
				this.token.CurrentSelector = this.token.SelectorHead - 1;
			}

			// Token: 0x06001272 RID: 4722 RVA: 0x00083E18 File Offset: 0x00082018
			public CssToken.SelectorEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x06001273 RID: 4723 RVA: 0x00083E20 File Offset: 0x00082020
			public bool Find(HtmlNameIndex nameId)
			{
				for (int i = this.token.SelectorHead; i < this.token.SelectorTail; i++)
				{
					if (this.token.SelectorList[i].NameId == nameId)
					{
						this.token.CurrentSelector = i;
						this.token.SelectorNamePosition.Rewind(this.token.SelectorList[i].Name);
						this.token.SelectorClassPosition.Rewind(this.token.SelectorList[i].ClassName);
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001274 RID: 4724 RVA: 0x00083EC4 File Offset: 0x000820C4
			[Conditional("DEBUG")]
			private void AssertCurrent()
			{
			}

			// Token: 0x04001306 RID: 4870
			private CssToken token;
		}

		// Token: 0x020001B5 RID: 437
		public struct SelectorNameTextReader
		{
			// Token: 0x06001275 RID: 4725 RVA: 0x00083EC6 File Offset: 0x000820C6
			internal SelectorNameTextReader(CssToken token)
			{
				this.token = token;
			}

			// Token: 0x17000513 RID: 1299
			// (get) Token: 0x06001276 RID: 4726 RVA: 0x00083ECF File Offset: 0x000820CF
			public int Length
			{
				get
				{
					return this.token.GetLength(ref this.token.SelectorList[this.token.CurrentSelector].Name);
				}
			}

			// Token: 0x06001277 RID: 4727 RVA: 0x00083EFC File Offset: 0x000820FC
			public int Read(char[] buffer, int offset, int count)
			{
				return this.token.Read(ref this.token.SelectorList[this.token.CurrentSelector].Name, ref this.token.SelectorNamePosition, buffer, offset, count);
			}

			// Token: 0x06001278 RID: 4728 RVA: 0x00083F44 File Offset: 0x00082144
			public void Rewind()
			{
				this.token.SelectorNamePosition.Rewind(this.token.SelectorList[this.token.CurrentSelector].Name);
			}

			// Token: 0x06001279 RID: 4729 RVA: 0x00083F76 File Offset: 0x00082176
			public void WriteTo(ITextSink sink)
			{
				this.token.WriteTo(ref this.token.SelectorList[this.token.CurrentSelector].Name, sink);
			}

			// Token: 0x0600127A RID: 4730 RVA: 0x00083FA4 File Offset: 0x000821A4
			public void WriteOriginalTo(ITextSink sink)
			{
				this.token.WriteOriginalTo(ref this.token.SelectorList[this.token.CurrentSelector].Name, sink);
			}

			// Token: 0x0600127B RID: 4731 RVA: 0x00083FD2 File Offset: 0x000821D2
			public string GetString(int maxSize)
			{
				return this.token.GetString(ref this.token.SelectorList[this.token.CurrentSelector].Name, maxSize);
			}

			// Token: 0x0600127C RID: 4732 RVA: 0x00084000 File Offset: 0x00082200
			public void MakeEmpty()
			{
				this.token.SelectorList[this.token.CurrentSelector].Name.Reset();
				this.Rewind();
			}

			// Token: 0x0600127D RID: 4733 RVA: 0x0008402D File Offset: 0x0008222D
			[Conditional("DEBUG")]
			private void AssertCurrent()
			{
			}

			// Token: 0x04001307 RID: 4871
			private CssToken token;
		}

		// Token: 0x020001B6 RID: 438
		public struct SelectorClassTextReader
		{
			// Token: 0x0600127E RID: 4734 RVA: 0x0008402F File Offset: 0x0008222F
			internal SelectorClassTextReader(CssToken token)
			{
				this.token = token;
			}

			// Token: 0x17000514 RID: 1300
			// (get) Token: 0x0600127F RID: 4735 RVA: 0x00084038 File Offset: 0x00082238
			public int Length
			{
				get
				{
					return this.token.GetLength(ref this.token.SelectorList[this.token.CurrentSelector].ClassName);
				}
			}

			// Token: 0x06001280 RID: 4736 RVA: 0x00084068 File Offset: 0x00082268
			public int Read(char[] buffer, int offset, int count)
			{
				return this.token.Read(ref this.token.SelectorList[this.token.CurrentSelector].ClassName, ref this.token.SelectorClassPosition, buffer, offset, count);
			}

			// Token: 0x06001281 RID: 4737 RVA: 0x000840B0 File Offset: 0x000822B0
			public void Rewind()
			{
				this.token.SelectorClassPosition.Rewind(this.token.SelectorList[this.token.CurrentSelector].ClassName);
			}

			// Token: 0x06001282 RID: 4738 RVA: 0x000840E2 File Offset: 0x000822E2
			public void WriteTo(ITextSink sink)
			{
				this.token.WriteTo(ref this.token.SelectorList[this.token.CurrentSelector].ClassName, sink);
			}

			// Token: 0x06001283 RID: 4739 RVA: 0x00084110 File Offset: 0x00082310
			public void WriteOriginalTo(ITextSink sink)
			{
				this.token.WriteEscapedOriginalTo(ref this.token.SelectorList[this.token.CurrentSelector].ClassName, sink);
			}

			// Token: 0x06001284 RID: 4740 RVA: 0x0008413E File Offset: 0x0008233E
			public string GetString(int maxSize)
			{
				return this.token.GetString(ref this.token.SelectorList[this.token.CurrentSelector].ClassName, maxSize);
			}

			// Token: 0x06001285 RID: 4741 RVA: 0x0008416C File Offset: 0x0008236C
			public bool CaseInsensitiveCompareEqual(string str)
			{
				return this.token.CaseInsensitiveCompareEqual(ref this.token.SelectorList[this.token.CurrentSelector].ClassName, str);
			}

			// Token: 0x06001286 RID: 4742 RVA: 0x0008419A File Offset: 0x0008239A
			public bool CaseInsensitiveContainsSubstring(string str)
			{
				return this.token.CaseInsensitiveContainsSubstring(ref this.token.SelectorList[this.token.CurrentSelector].ClassName, str);
			}

			// Token: 0x06001287 RID: 4743 RVA: 0x000841C8 File Offset: 0x000823C8
			public void MakeEmpty()
			{
				this.token.SelectorList[this.token.CurrentSelector].ClassName.Reset();
				this.Rewind();
			}

			// Token: 0x06001288 RID: 4744 RVA: 0x000841F5 File Offset: 0x000823F5
			[Conditional("DEBUG")]
			private void AssertCurrent()
			{
			}

			// Token: 0x04001308 RID: 4872
			private CssToken token;
		}

		// Token: 0x020001B7 RID: 439
		protected internal struct PropertyEntry
		{
			// Token: 0x17000515 RID: 1301
			// (get) Token: 0x06001289 RID: 4745 RVA: 0x000841F7 File Offset: 0x000823F7
			public bool IsCompleteProperty
			{
				get
				{
					return this.MajorPart == CssToken.PropertyPartMajor.Complete;
				}
			}

			// Token: 0x17000516 RID: 1302
			// (get) Token: 0x0600128A RID: 4746 RVA: 0x00084202 File Offset: 0x00082402
			public bool IsPropertyBegin
			{
				get
				{
					return (byte)(this.PartMajor & CssToken.PropertyPartMajor.Begin) == 3;
				}
			}

			// Token: 0x17000517 RID: 1303
			// (get) Token: 0x0600128B RID: 4747 RVA: 0x00084210 File Offset: 0x00082410
			public bool IsPropertyEnd
			{
				get
				{
					return (byte)(this.PartMajor & CssToken.PropertyPartMajor.End) == 6;
				}
			}

			// Token: 0x17000518 RID: 1304
			// (get) Token: 0x0600128C RID: 4748 RVA: 0x0008421E File Offset: 0x0008241E
			public bool IsPropertyNameEnd
			{
				get
				{
					return (byte)(this.PartMinor & CssToken.PropertyPartMinor.EndName) == 6;
				}
			}

			// Token: 0x17000519 RID: 1305
			// (get) Token: 0x0600128D RID: 4749 RVA: 0x0008422C File Offset: 0x0008242C
			public bool IsPropertyValueBegin
			{
				get
				{
					return (byte)(this.PartMinor & CssToken.PropertyPartMinor.BeginValue) == 24;
				}
			}

			// Token: 0x1700051A RID: 1306
			// (get) Token: 0x0600128E RID: 4750 RVA: 0x0008423C File Offset: 0x0008243C
			public CssToken.PropertyPartMajor MajorPart
			{
				get
				{
					return this.PartMajor & CssToken.PropertyPartMajor.Complete;
				}
			}

			// Token: 0x1700051B RID: 1307
			// (get) Token: 0x0600128F RID: 4751 RVA: 0x00084247 File Offset: 0x00082447
			// (set) Token: 0x06001290 RID: 4752 RVA: 0x0008424F File Offset: 0x0008244F
			public CssToken.PropertyPartMinor MinorPart
			{
				get
				{
					return this.PartMinor;
				}
				set
				{
					this.PartMinor = value;
				}
			}

			// Token: 0x1700051C RID: 1308
			// (get) Token: 0x06001291 RID: 4753 RVA: 0x00084258 File Offset: 0x00082458
			// (set) Token: 0x06001292 RID: 4754 RVA: 0x00084268 File Offset: 0x00082468
			public bool IsPropertyValueQuoted
			{
				get
				{
					return (byte)(this.PartMajor & CssToken.PropertyPartMajor.ValueQuoted) == 64;
				}
				set
				{
					this.PartMajor = (value ? (this.PartMajor | CssToken.PropertyPartMajor.ValueQuoted) : (this.PartMajor & (CssToken.PropertyPartMajor)191));
				}
			}

			// Token: 0x1700051D RID: 1309
			// (get) Token: 0x06001293 RID: 4755 RVA: 0x0008428C File Offset: 0x0008248C
			// (set) Token: 0x06001294 RID: 4756 RVA: 0x000842A2 File Offset: 0x000824A2
			public bool IsPropertyDeleted
			{
				get
				{
					return (byte)(this.PartMajor & CssToken.PropertyPartMajor.Deleted) == 128;
				}
				set
				{
					this.PartMajor = (value ? (this.PartMajor | CssToken.PropertyPartMajor.Deleted) : (this.PartMajor & (CssToken.PropertyPartMajor)127));
				}
			}

			// Token: 0x04001309 RID: 4873
			public CssNameIndex NameId;

			// Token: 0x0400130A RID: 4874
			public byte QuoteChar;

			// Token: 0x0400130B RID: 4875
			public CssToken.PropertyPartMajor PartMajor;

			// Token: 0x0400130C RID: 4876
			public CssToken.PropertyPartMinor PartMinor;

			// Token: 0x0400130D RID: 4877
			public Token.Fragment Name;

			// Token: 0x0400130E RID: 4878
			public Token.Fragment Value;
		}

		// Token: 0x020001B8 RID: 440
		protected internal struct SelectorEntry
		{
			// Token: 0x1700051E RID: 1310
			// (get) Token: 0x06001295 RID: 4757 RVA: 0x000842C6 File Offset: 0x000824C6
			// (set) Token: 0x06001296 RID: 4758 RVA: 0x000842CE File Offset: 0x000824CE
			public bool IsSelectorDeleted
			{
				get
				{
					return this.deleted;
				}
				set
				{
					this.deleted = value;
				}
			}

			// Token: 0x0400130F RID: 4879
			public HtmlNameIndex NameId;

			// Token: 0x04001310 RID: 4880
			private bool deleted;

			// Token: 0x04001311 RID: 4881
			public Token.Fragment Name;

			// Token: 0x04001312 RID: 4882
			public Token.Fragment ClassName;

			// Token: 0x04001313 RID: 4883
			public CssSelectorClassType ClassType;

			// Token: 0x04001314 RID: 4884
			public CssSelectorCombinator Combinator;
		}
	}
}
