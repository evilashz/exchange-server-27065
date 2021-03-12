using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000B9 RID: 185
	public sealed class HttpAuthenticationChallenge
	{
		// Token: 0x06000636 RID: 1590 RVA: 0x0002E838 File Offset: 0x0002CA38
		internal HttpAuthenticationChallenge(string scheme)
		{
			if (string.IsNullOrWhiteSpace(scheme))
			{
				throw new ArgumentNullException("scheme");
			}
			this._scheme = scheme;
			this._parameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0002E86A File Offset: 0x0002CA6A
		public string Scheme
		{
			get
			{
				return this._scheme;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0002E872 File Offset: 0x0002CA72
		public string Realm
		{
			get
			{
				return this.GetParameter(Constants.ChallengeTokens.Realm);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0002E87F File Offset: 0x0002CA7F
		public string TrustedIssuers
		{
			get
			{
				return this.GetParameter(Constants.ChallengeTokens.TrustedIssuers);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0002E88C File Offset: 0x0002CA8C
		public string ClientId
		{
			get
			{
				return this.GetParameter(Constants.ChallengeTokens.ClientId);
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0002E89C File Offset: 0x0002CA9C
		public string GetParameter(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			string result = null;
			this._parameters.TryGetValue(name, out result);
			return result;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0002E8C9 File Offset: 0x0002CAC9
		internal void AddParameter(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this._parameters.ContainsKey(name))
			{
				this._parameters.Add(name, value);
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0002E904 File Offset: 0x0002CB04
		public override bool Equals(object obj)
		{
			HttpAuthenticationChallenge httpAuthenticationChallenge = obj as HttpAuthenticationChallenge;
			return httpAuthenticationChallenge != null && (this.ClientId == httpAuthenticationChallenge.ClientId && this.Realm == httpAuthenticationChallenge.Realm) && this.TrustedIssuers == httpAuthenticationChallenge.TrustedIssuers;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0002E958 File Offset: 0x0002CB58
		public override int GetHashCode()
		{
			int num = this.ClientId.GetHashCode();
			if (!string.IsNullOrEmpty(this.Realm))
			{
				num ^= this.Realm.GetHashCode();
			}
			if (!string.IsNullOrEmpty(this.TrustedIssuers))
			{
				num ^= this.TrustedIssuers.GetHashCode();
			}
			return num;
		}

		// Token: 0x0400061A RID: 1562
		private readonly string _scheme;

		// Token: 0x0400061B RID: 1563
		private Dictionary<string, string> _parameters;
	}
}
