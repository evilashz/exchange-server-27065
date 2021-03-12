using System;
using System.Diagnostics;
using System.Text;

namespace Microsoft.Exchange.Data.TextConverters.Internal
{
	// Token: 0x02000199 RID: 409
	internal abstract class TokenBuilder
	{
		// Token: 0x0600117A RID: 4474 RVA: 0x0007D900 File Offset: 0x0007BB00
		public TokenBuilder(Token token, char[] buffer, int maxRuns, bool testBoundaryConditions)
		{
			int num = 64;
			if (!testBoundaryConditions)
			{
				this.maxRuns = maxRuns;
			}
			else
			{
				this.maxRuns = 55;
				num = 7;
			}
			this.token = token;
			this.token.Buffer = buffer;
			this.token.RunList = new Token.RunEntry[num];
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x0007D951 File Offset: 0x0007BB51
		public Token Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x0007D959 File Offset: 0x0007BB59
		public bool IsStarted
		{
			get
			{
				return this.state != 0;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x0007D967 File Offset: 0x0007BB67
		public bool Valid
		{
			get
			{
				return this.tokenValid;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x0007D96F File Offset: 0x0007BB6F
		public int TotalLength
		{
			get
			{
				return this.tailOffset - this.token.Whole.HeadOffset;
			}
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0007D988 File Offset: 0x0007BB88
		public void BufferChanged(char[] newBuffer, int newBase)
		{
			if (newBuffer != this.token.Buffer || newBase != this.token.Whole.HeadOffset)
			{
				this.token.Buffer = newBuffer;
				if (newBase != this.token.Whole.HeadOffset)
				{
					int deltaOffset = newBase - this.token.Whole.HeadOffset;
					this.Rebase(deltaOffset);
				}
			}
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0007D9EF File Offset: 0x0007BBEF
		public virtual void Reset()
		{
			if (this.state > 0)
			{
				this.token.Reset();
				this.tailOffset = 0;
				this.tokenValid = false;
				this.state = 0;
			}
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0007DA1A File Offset: 0x0007BC1A
		public TokenId MakeEmptyToken(TokenId tokenId)
		{
			this.token.TokenId = tokenId;
			this.state = 5;
			this.tokenValid = true;
			return tokenId;
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0007DA37 File Offset: 0x0007BC37
		public TokenId MakeEmptyToken(TokenId tokenId, int argument)
		{
			this.token.TokenId = tokenId;
			this.token.Argument = argument;
			this.state = 5;
			this.tokenValid = true;
			return tokenId;
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0007DA60 File Offset: 0x0007BC60
		public TokenId MakeEmptyToken(TokenId tokenId, Encoding tokenEncoding)
		{
			this.token.TokenId = tokenId;
			this.token.TokenEncoding = tokenEncoding;
			this.state = 5;
			this.tokenValid = true;
			return tokenId;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0007DA89 File Offset: 0x0007BC89
		public void StartText(int baseOffset)
		{
			this.token.TokenId = TokenId.Text;
			this.state = 10;
			this.token.Whole.HeadOffset = baseOffset;
			this.tailOffset = baseOffset;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0007DAB7 File Offset: 0x0007BCB7
		public void EndText()
		{
			this.state = 5;
			this.tokenValid = true;
			this.token.WholePosition.Rewind(this.token.Whole);
			this.AddSentinelRun();
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0007DAE8 File Offset: 0x0007BCE8
		public void SkipRunIfNecessary(int start, uint skippedRunKind)
		{
			if (start != this.tailOffset)
			{
				this.AddInvalidRun(start, skippedRunKind);
			}
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0007DAFB File Offset: 0x0007BCFB
		public bool PrepareToAddMoreRuns(int numRuns, int start, uint skippedRunKind)
		{
			return (start == this.tailOffset && this.token.Whole.Tail + numRuns < this.token.RunList.Length) || this.SlowPrepareToAddMoreRuns(numRuns, start, skippedRunKind);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0007DB34 File Offset: 0x0007BD34
		public bool SlowPrepareToAddMoreRuns(int numRuns, int start, uint skippedRunKind)
		{
			if (start != this.tailOffset)
			{
				numRuns++;
			}
			if (this.token.Whole.Tail + numRuns < this.token.RunList.Length || this.ExpandRunsArray(numRuns))
			{
				if (start != this.tailOffset)
				{
					this.AddInvalidRun(start, skippedRunKind);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0007DB8D File Offset: 0x0007BD8D
		public bool PrepareToAddMoreRuns(int numRuns)
		{
			return this.token.Whole.Tail + numRuns < this.token.RunList.Length || this.ExpandRunsArray(numRuns);
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0007DBB9 File Offset: 0x0007BDB9
		[Conditional("DEBUG")]
		public void AssertPreparedToAddMoreRuns(int numRuns)
		{
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0007DBBB File Offset: 0x0007BDBB
		[Conditional("DEBUG")]
		public void AssertCanAddMoreRuns(int numRuns)
		{
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0007DBBD File Offset: 0x0007BDBD
		[Conditional("DEBUG")]
		public void AssertCurrentRunPosition(int position)
		{
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0007DBBF File Offset: 0x0007BDBF
		[Conditional("DEBUG")]
		public void DebugPrepareToAddMoreRuns(int numRuns)
		{
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0007DBC1 File Offset: 0x0007BDC1
		public void AddTextRun(RunTextType textType, int start, int end)
		{
			this.AddRun((RunType)2147483648U, textType, 67108864U, start, end, 0);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0007DBD7 File Offset: 0x0007BDD7
		public void AddLiteralTextRun(RunTextType textType, int start, int end, int literal)
		{
			this.AddRun((RunType)3221225472U, textType, 67108864U, start, end, literal);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0007DBEE File Offset: 0x0007BDEE
		public void AddSpecialRun(RunKind kind, int startEnd, int value)
		{
			this.AddRun(RunType.Special, RunTextType.Unknown, (uint)kind, this.tailOffset, startEnd, value);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0007DC08 File Offset: 0x0007BE08
		internal void AddRun(RunType type, RunTextType textType, uint kind, int start, int end, int value)
		{
			Token.RunEntry[] runList = this.token.RunList;
			Token token = this.token;
			int tail;
			token.Whole.Tail = (tail = token.Whole.Tail) + 1;
			runList[tail].Initialize(type, textType, kind, end - start, value);
			this.tailOffset = end;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0007DC58 File Offset: 0x0007BE58
		internal void AddInvalidRun(int offset, uint kind)
		{
			Token.RunEntry[] runList = this.token.RunList;
			Token token = this.token;
			int tail;
			token.Whole.Tail = (tail = token.Whole.Tail) + 1;
			runList[tail].Initialize(RunType.Invalid, RunTextType.Unknown, kind, offset - this.tailOffset, 0);
			this.tailOffset = offset;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0007DCA8 File Offset: 0x0007BEA8
		internal void AddNullRun(uint kind)
		{
			Token.RunEntry[] runList = this.token.RunList;
			Token token = this.token;
			int tail;
			token.Whole.Tail = (tail = token.Whole.Tail) + 1;
			runList[tail].Initialize(RunType.Invalid, RunTextType.Unknown, kind, 0, 0);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0007DCEA File Offset: 0x0007BEEA
		internal void AddSentinelRun()
		{
			this.token.RunList[this.token.Whole.Tail].InitializeSentinel();
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0007DD11 File Offset: 0x0007BF11
		protected virtual void Rebase(int deltaOffset)
		{
			Token token = this.token;
			token.Whole.HeadOffset = token.Whole.HeadOffset + deltaOffset;
			Token token2 = this.token;
			token2.WholePosition.RunOffset = token2.WholePosition.RunOffset + deltaOffset;
			this.tailOffset += deltaOffset;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0007DD54 File Offset: 0x0007BF54
		private bool ExpandRunsArray(int numRuns)
		{
			int num = Math.Min(this.maxRuns, Math.Max(this.token.RunList.Length * 2, this.token.Whole.Tail + numRuns + 1));
			if (num - this.token.Whole.Tail < numRuns + 1)
			{
				return false;
			}
			Token.RunEntry[] array = new Token.RunEntry[num];
			Array.Copy(this.token.RunList, 0, array, 0, this.token.Whole.Tail);
			this.token.RunList = array;
			return true;
		}

		// Token: 0x040011CC RID: 4556
		protected const byte BuildStateInitialized = 0;

		// Token: 0x040011CD RID: 4557
		protected const byte BuildStateEnded = 5;

		// Token: 0x040011CE RID: 4558
		protected const byte FirstStarted = 10;

		// Token: 0x040011CF RID: 4559
		protected const byte BuildStateText = 10;

		// Token: 0x040011D0 RID: 4560
		protected byte state;

		// Token: 0x040011D1 RID: 4561
		protected Token token;

		// Token: 0x040011D2 RID: 4562
		protected int maxRuns;

		// Token: 0x040011D3 RID: 4563
		protected int tailOffset;

		// Token: 0x040011D4 RID: 4564
		protected bool tokenValid;
	}
}
