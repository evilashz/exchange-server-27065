using System;

namespace System.Security.Util
{
	// Token: 0x0200035B RID: 859
	internal sealed class TokenizerStream
	{
		// Token: 0x06002BAD RID: 11181 RVA: 0x000A4435 File Offset: 0x000A2635
		internal TokenizerStream()
		{
			this.m_countTokens = 0;
			this.m_headTokens = new TokenizerShortBlock();
			this.m_headStrings = new TokenizerStringBlock();
			this.Reset();
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000A4460 File Offset: 0x000A2660
		internal void AddToken(short token)
		{
			if (this.m_currentTokens.m_block.Length <= this.m_indexTokens)
			{
				this.m_currentTokens.m_next = new TokenizerShortBlock();
				this.m_currentTokens = this.m_currentTokens.m_next;
				this.m_indexTokens = 0;
			}
			this.m_countTokens++;
			short[] block = this.m_currentTokens.m_block;
			int indexTokens = this.m_indexTokens;
			this.m_indexTokens = indexTokens + 1;
			block[indexTokens] = token;
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x000A44D8 File Offset: 0x000A26D8
		internal void AddString(string str)
		{
			if (this.m_currentStrings.m_block.Length <= this.m_indexStrings)
			{
				this.m_currentStrings.m_next = new TokenizerStringBlock();
				this.m_currentStrings = this.m_currentStrings.m_next;
				this.m_indexStrings = 0;
			}
			string[] block = this.m_currentStrings.m_block;
			int indexStrings = this.m_indexStrings;
			this.m_indexStrings = indexStrings + 1;
			block[indexStrings] = str;
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x000A4540 File Offset: 0x000A2740
		internal void Reset()
		{
			this.m_lastTokens = null;
			this.m_currentTokens = this.m_headTokens;
			this.m_currentStrings = this.m_headStrings;
			this.m_indexTokens = 0;
			this.m_indexStrings = 0;
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000A4570 File Offset: 0x000A2770
		internal short GetNextFullToken()
		{
			if (this.m_currentTokens.m_block.Length <= this.m_indexTokens)
			{
				this.m_lastTokens = this.m_currentTokens;
				this.m_currentTokens = this.m_currentTokens.m_next;
				this.m_indexTokens = 0;
			}
			short[] block = this.m_currentTokens.m_block;
			int indexTokens = this.m_indexTokens;
			this.m_indexTokens = indexTokens + 1;
			return block[indexTokens];
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x000A45D4 File Offset: 0x000A27D4
		internal short GetNextToken()
		{
			return this.GetNextFullToken() & 255;
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000A45F0 File Offset: 0x000A27F0
		internal string GetNextString()
		{
			if (this.m_currentStrings.m_block.Length <= this.m_indexStrings)
			{
				this.m_currentStrings = this.m_currentStrings.m_next;
				this.m_indexStrings = 0;
			}
			string[] block = this.m_currentStrings.m_block;
			int indexStrings = this.m_indexStrings;
			this.m_indexStrings = indexStrings + 1;
			return block[indexStrings];
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000A4647 File Offset: 0x000A2847
		internal void ThrowAwayNextString()
		{
			this.GetNextString();
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x000A4650 File Offset: 0x000A2850
		internal void TagLastToken(short tag)
		{
			if (this.m_indexTokens == 0)
			{
				this.m_lastTokens.m_block[this.m_lastTokens.m_block.Length - 1] = (short)((ushort)this.m_lastTokens.m_block[this.m_lastTokens.m_block.Length - 1] | (ushort)tag);
				return;
			}
			this.m_currentTokens.m_block[this.m_indexTokens - 1] = (short)((ushort)this.m_currentTokens.m_block[this.m_indexTokens - 1] | (ushort)tag);
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x000A46CE File Offset: 0x000A28CE
		internal int GetTokenCount()
		{
			return this.m_countTokens;
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x000A46D8 File Offset: 0x000A28D8
		internal void GoToPosition(int position)
		{
			this.Reset();
			for (int i = 0; i < position; i++)
			{
				if (this.GetNextToken() == 3)
				{
					this.ThrowAwayNextString();
				}
			}
		}

		// Token: 0x04001166 RID: 4454
		private int m_countTokens;

		// Token: 0x04001167 RID: 4455
		private TokenizerShortBlock m_headTokens;

		// Token: 0x04001168 RID: 4456
		private TokenizerShortBlock m_lastTokens;

		// Token: 0x04001169 RID: 4457
		private TokenizerShortBlock m_currentTokens;

		// Token: 0x0400116A RID: 4458
		private int m_indexTokens;

		// Token: 0x0400116B RID: 4459
		private TokenizerStringBlock m_headStrings;

		// Token: 0x0400116C RID: 4460
		private TokenizerStringBlock m_currentStrings;

		// Token: 0x0400116D RID: 4461
		private int m_indexStrings;
	}
}
