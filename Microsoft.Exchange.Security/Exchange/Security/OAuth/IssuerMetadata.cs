using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000BE RID: 190
	internal class IssuerMetadata
	{
		// Token: 0x0600065E RID: 1630 RVA: 0x0002F1F0 File Offset: 0x0002D3F0
		public IssuerMetadata(IssuerKind kind, string id, string realm)
		{
			this.kind = kind;
			this.id = id;
			this.realm = realm;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0002F20D File Offset: 0x0002D40D
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0002F215 File Offset: 0x0002D415
		public string Realm
		{
			get
			{
				return this.realm;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0002F21D File Offset: 0x0002D41D
		public IssuerKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0002F225 File Offset: 0x0002D425
		public bool HasEmptyRealm
		{
			get
			{
				return OAuthCommon.IsRealmEmpty(this.realm);
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0002F234 File Offset: 0x0002D434
		public static bool TryParse(string issuerString, out IssuerMetadata meta)
		{
			meta = null;
			issuerString = issuerString.Trim();
			int num = issuerString.LastIndexOf('@');
			if (num == -1)
			{
				return false;
			}
			string text = issuerString.Substring(num + 1);
			int num2 = issuerString.LastIndexOf('/');
			string text2;
			if (num2 == -1)
			{
				text2 = issuerString.Substring(0, num);
			}
			else
			{
				text2 = issuerString.Substring(0, num2);
			}
			meta = new IssuerMetadata(IssuerKind.External, text2, text);
			return true;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0002F294 File Offset: 0x0002D494
		public static IssuerMetadata[] Parse(string trustedIssuers)
		{
			if (string.IsNullOrEmpty(trustedIssuers))
			{
				return null;
			}
			List<IssuerMetadata> list = null;
			foreach (string issuerString in trustedIssuers.Split(new char[]
			{
				','
			}))
			{
				IssuerMetadata item;
				if (IssuerMetadata.TryParse(issuerString, out item))
				{
					if (list == null)
					{
						list = new List<IssuerMetadata>();
					}
					list.Add(item);
				}
			}
			if (list != null)
			{
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0002F300 File Offset: 0x0002D500
		public static IssuerMetadata Create(AuthServer authServer)
		{
			return new IssuerMetadata(IssuerKind.ACS, authServer.IssuerIdentifier, OAuthCommon.IsRealmEmpty(authServer.Realm) ? "*" : authServer.Realm);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0002F328 File Offset: 0x0002D528
		public bool MatchIdAndRealm(IssuerMetadata other)
		{
			return this.MatchId(other) && this.MatchRealm(other);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0002F33C File Offset: 0x0002D53C
		public bool MatchId(IssuerMetadata other)
		{
			return OAuthCommon.IsIdMatch(this.Id, other.Id);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0002F34F File Offset: 0x0002D54F
		public bool MatchRealm(IssuerMetadata other)
		{
			return OAuthCommon.IsRealmMatch(this.realm, other.realm);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0002F364 File Offset: 0x0002D564
		public string ToTrustedIssuerString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}@{1}", new object[]
			{
				this.Id,
				string.IsNullOrEmpty(this.Realm) ? "*" : this.Realm
			});
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0002F3B0 File Offset: 0x0002D5B0
		public override bool Equals(object obj)
		{
			IssuerMetadata issuerMetadata = obj as IssuerMetadata;
			return issuerMetadata != null && this.MatchIdAndRealm(issuerMetadata);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0002F3D0 File Offset: 0x0002D5D0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0002F3D8 File Offset: 0x0002D5D8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}@{1}", new object[]
			{
				this.id,
				this.realm
			});
		}

		// Token: 0x0400062C RID: 1580
		private readonly string id;

		// Token: 0x0400062D RID: 1581
		private readonly string realm;

		// Token: 0x0400062E RID: 1582
		private readonly IssuerKind kind;
	}
}
