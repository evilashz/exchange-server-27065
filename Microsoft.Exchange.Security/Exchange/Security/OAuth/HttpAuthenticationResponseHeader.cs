using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000BA RID: 186
	internal sealed class HttpAuthenticationResponseHeader
	{
		// Token: 0x0600063F RID: 1599 RVA: 0x0002E9A8 File Offset: 0x0002CBA8
		private HttpAuthenticationResponseHeader(IEnumerable<HttpAuthenticationChallenge> challenges)
		{
			this._challenges = new List<HttpAuthenticationChallenge>(challenges);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0002E9BC File Offset: 0x0002CBBC
		internal static HttpAuthenticationResponseHeader Parse(string wwwAuthenticateHeader)
		{
			IEnumerable<HttpAuthenticationChallenge> challenges = HttpAuthenticationResponseHeader.EnumerateChallengesRelaxed(wwwAuthenticateHeader);
			return new HttpAuthenticationResponseHeader(challenges);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0002EC6C File Offset: 0x0002CE6C
		private static IEnumerable<HttpAuthenticationChallenge> EnumerateChallengesRelaxed(string header)
		{
			HttpTokenReader reader = new HttpTokenReader(header);
			HttpAuthenticationChallenge challenge = null;
			reader.SkipLinearWhiteSpace(true);
			while (!reader.EndOfContent)
			{
				if (reader.PeekChar() != ',')
				{
					break;
				}
				reader.SkipChar(',');
				reader.SkipLinearWhiteSpace(true);
			}
			while (!reader.EndOfContent)
			{
				string token = reader.ReadToken();
				reader.SkipLinearWhiteSpace(true);
				if (!reader.EndOfContent && reader.PeekChar() == '=')
				{
					reader.SkipChar('=');
					reader.SkipLinearWhiteSpace(true);
					string value = reader.ReadTokenOrQuotedString(true);
					reader.SkipLinearWhiteSpace(true);
					if (challenge != null)
					{
						challenge.AddParameter(token, value);
					}
				}
				else
				{
					if (challenge != null)
					{
						yield return challenge;
					}
					challenge = new HttpAuthenticationChallenge(token);
					if (!reader.EndOfContent && reader.PeekChar() == ',')
					{
						yield return challenge;
						challenge = null;
					}
				}
				while (!reader.EndOfContent && reader.PeekChar() == ',')
				{
					reader.SkipChar(',');
					reader.SkipLinearWhiteSpace(true);
				}
			}
			if (challenge != null)
			{
				yield return challenge;
			}
			yield break;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0002EC89 File Offset: 0x0002CE89
		internal IEnumerable<HttpAuthenticationChallenge> Challenges
		{
			get
			{
				return this._challenges.AsReadOnly();
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0002EC98 File Offset: 0x0002CE98
		internal HttpAuthenticationChallenge FindFirstChallenge(string scheme)
		{
			foreach (HttpAuthenticationChallenge httpAuthenticationChallenge in this.Challenges)
			{
				if (string.Compare(scheme, httpAuthenticationChallenge.Scheme, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return httpAuthenticationChallenge;
				}
			}
			return null;
		}

		// Token: 0x0400061C RID: 1564
		private List<HttpAuthenticationChallenge> _challenges;
	}
}
