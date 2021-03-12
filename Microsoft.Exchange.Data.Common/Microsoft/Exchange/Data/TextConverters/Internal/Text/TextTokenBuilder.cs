using System;
using System.Text;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Text
{
	// Token: 0x02000287 RID: 647
	internal class TextTokenBuilder : TokenBuilder
	{
		// Token: 0x06001A13 RID: 6675 RVA: 0x000CF063 File Offset: 0x000CD263
		public TextTokenBuilder(char[] buffer, int maxRuns, bool testBoundaryConditions) : this(new TextToken(), buffer, maxRuns, testBoundaryConditions)
		{
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x000CF073 File Offset: 0x000CD273
		public TextTokenBuilder(TextToken token, char[] buffer, int maxRuns, bool testBoundaryConditions) : base(token, buffer, maxRuns, testBoundaryConditions)
		{
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001A15 RID: 6677 RVA: 0x000CF080 File Offset: 0x000CD280
		public new TextToken Token
		{
			get
			{
				return (TextToken)base.Token;
			}
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x000CF08D File Offset: 0x000CD28D
		public TextTokenId MakeEmptyToken(TextTokenId tokenId)
		{
			return (TextTokenId)base.MakeEmptyToken((TokenId)tokenId);
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x000CF096 File Offset: 0x000CD296
		public TextTokenId MakeEmptyToken(TextTokenId tokenId, int argument)
		{
			return (TextTokenId)base.MakeEmptyToken((TokenId)tokenId, argument);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x000CF0A0 File Offset: 0x000CD2A0
		public TextTokenId MakeEmptyToken(TextTokenId tokenId, Encoding encoding)
		{
			return (TextTokenId)base.MakeEmptyToken((TokenId)tokenId, encoding);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x000CF0AA File Offset: 0x000CD2AA
		public void SkipRunIfNecessary(int start, RunKind skippedRunKind)
		{
			base.SkipRunIfNecessary(start, (uint)skippedRunKind);
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x000CF0B4 File Offset: 0x000CD2B4
		public bool PrepareToAddMoreRuns(int numRuns, int start, RunKind skippedRunKind)
		{
			return base.PrepareToAddMoreRuns(numRuns, start, (uint)skippedRunKind);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x000CF0BF File Offset: 0x000CD2BF
		public void AddSpecialRun(TextRunKind kind, int startEnd, int value)
		{
			base.AddRun(RunType.Special, RunTextType.Unknown, (uint)kind, this.tailOffset, startEnd, value);
		}
	}
}
