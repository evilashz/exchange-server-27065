using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.TextProcessing;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200003D RID: 61
	internal class MatchFactory
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		private Dictionary<BoundaryType, TrieInfo> TrieMap
		{
			get
			{
				if (this.trieMap == null)
				{
					this.trieMap = new Dictionary<BoundaryType, TrieInfo>();
					this.AddTrieToMap(BoundaryType.None);
					this.AddTrieToMap(BoundaryType.Normal);
					this.AddTrieToMap(BoundaryType.NormalLeftOnly);
					this.AddTrieToMap(BoundaryType.NormalRightOnly);
					this.AddTrieToMap(BoundaryType.Url);
					this.AddTrieToMap(BoundaryType.FullUrl);
				}
				return this.trieMap;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000E308 File Offset: 0x0000C508
		private TrieInfo NoBoundaryTrie
		{
			get
			{
				if (this.noBoundaryTrie == null)
				{
					this.noBoundaryTrie = new TrieInfo(IDGenerator.GetNextID(), new Trie(BoundaryType.None, false));
				}
				return this.noBoundaryTrie;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000E32F File Offset: 0x0000C52F
		public IMatch CreateTermSet(IEnumerable<string> terms, BoundaryType boundaryType)
		{
			return new TermMatch(terms, boundaryType, this.TrieMap);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000E33E File Offset: 0x0000C53E
		public IMatch CreateRegexTermSet(IEnumerable<string> terms)
		{
			return new RegexTermMatch(terms);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000E346 File Offset: 0x0000C546
		public IMatch CreateSingleExecutionTermSet(ICollection<string> terms)
		{
			if (terms.Count > 50)
			{
				return new TermMatch(terms, BoundaryType.Normal, this.TrieMap);
			}
			return new RegexTermMatch(terms);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000E366 File Offset: 0x0000C566
		public IMatch CreateSingleTrieTermSet(ICollection<string> terms, BoundaryType boundaryType)
		{
			return new SingleTrieTermMatch(terms, boundaryType, this.NoBoundaryTrie);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000E375 File Offset: 0x0000C575
		public IMatch CreateRegex(string pattern, bool caseSensitive = true, bool compiled = false)
		{
			return this.CreateRegex(pattern, caseSensitive ? CaseSensitivityMode.Sensitive : CaseSensitivityMode.Insensitive, compiled ? MatchRegexOptions.Compiled : MatchRegexOptions.None, MatcherConstants.DefaultRegexMatchTimeout);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000E391 File Offset: 0x0000C591
		public IMatch CreateRegex(string pattern, CaseSensitivityMode caseSensitivityMode, MatchRegexOptions options)
		{
			return this.CreateRegex(pattern, caseSensitivityMode, options, MatcherConstants.DefaultRegexMatchTimeout);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000E3A1 File Offset: 0x0000C5A1
		public IMatch CreateRegex(string pattern, CaseSensitivityMode caseSensitivityMode, MatchRegexOptions options, TimeSpan matchTimeout)
		{
			return new RegexMatch(pattern, caseSensitivityMode, options, matchTimeout);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public IMatch CreateConditional(IMatch match, IMatch precondition)
		{
			if (match == null || precondition == null)
			{
				throw new ArgumentException(Strings.NullIMatch);
			}
			return new ConditionalMatch(match, precondition);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000E3CC File Offset: 0x0000C5CC
		public IMatch CreateConditionalRegex(string pattern, IEnumerable<string> termConditions)
		{
			return new ConditionalMatch(this.CreateRegex(pattern, true, true), this.CreateTermSet(termConditions, BoundaryType.None));
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000E3E4 File Offset: 0x0000C5E4
		public IMatch CreateSimilarityMatch(LshFingerprint fingerprint, double coefficient)
		{
			if (fingerprint == null)
			{
				throw new ArgumentException(Strings.NullFingerprint);
			}
			if (coefficient < MatcherConstants.MinimumCoefficient || coefficient > MatcherConstants.MaximumCoefficient)
			{
				throw new ArgumentException(Strings.InvalidCoefficient(coefficient));
			}
			return new FingerprintMatch(fingerprint, coefficient, false);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000E422 File Offset: 0x0000C622
		public IMatch CreateContainmentMatch(LshFingerprint fingerprint, double coefficient)
		{
			if (fingerprint == null)
			{
				throw new ArgumentException(Strings.NullFingerprint);
			}
			if (coefficient < MatcherConstants.MinimumCoefficient || coefficient > MatcherConstants.MaximumCoefficient)
			{
				throw new ArgumentException(Strings.InvalidCoefficient(coefficient));
			}
			return new FingerprintMatch(fingerprint, coefficient, true);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000E460 File Offset: 0x0000C660
		public IMatch Copy(IMatch match)
		{
			if (match is ConditionalMatch)
			{
				return new ConditionalMatch(match as ConditionalMatch, this);
			}
			if (match is RegexMatch)
			{
				return new RegexMatch(match as RegexMatch);
			}
			if (match is TermMatch)
			{
				return new TermMatch(match as TermMatch, this.TrieMap);
			}
			if (match is RegexTermMatch)
			{
				return match;
			}
			if (match is SingleTrieTermMatch)
			{
				return new SingleTrieTermMatch(match as SingleTrieTermMatch, this.NoBoundaryTrie);
			}
			if (match is FingerprintMatch)
			{
				return match;
			}
			if (match is NullMatch)
			{
				return match;
			}
			ExTraceGlobals.MatcherTracer.TraceDebug<Type>((long)this.GetHashCode(), "Attempting to copy an unknown match type {0}", match.GetType());
			throw new ArgumentException(Strings.UnsupportedMatch);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000E513 File Offset: 0x0000C713
		private void AddTrieToMap(BoundaryType boundaryType)
		{
			this.trieMap[boundaryType] = new TrieInfo(IDGenerator.GetNextID(), new Trie(boundaryType, false));
		}

		// Token: 0x04000148 RID: 328
		private const int RegexTermsCreationThreshold = 50;

		// Token: 0x04000149 RID: 329
		private Dictionary<BoundaryType, TrieInfo> trieMap;

		// Token: 0x0400014A RID: 330
		private TrieInfo noBoundaryTrie;
	}
}
