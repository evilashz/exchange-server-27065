using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001E8 RID: 488
	[Serializable]
	public class X509Identifier : IEquatable<X509Identifier>
	{
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x000335EA File Offset: 0x000317EA
		// (set) Token: 0x060010D1 RID: 4305 RVA: 0x000335F2 File Offset: 0x000317F2
		public string Issuer { get; private set; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x000335FB File Offset: 0x000317FB
		// (set) Token: 0x060010D3 RID: 4307 RVA: 0x00033603 File Offset: 0x00031803
		public string Subject { get; private set; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x0003360C File Offset: 0x0003180C
		public bool IsGenericIdentifier
		{
			get
			{
				return string.IsNullOrEmpty(this.Subject);
			}
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00033619 File Offset: 0x00031819
		public X509Identifier(string issuer, string subject)
		{
			if (string.IsNullOrEmpty(issuer))
			{
				throw new ArgumentNullException("issuer");
			}
			this.Issuer = issuer;
			this.Subject = subject;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00033642 File Offset: 0x00031842
		public X509Identifier(string issuer) : this(issuer, null)
		{
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0003364C File Offset: 0x0003184C
		public X509Identifier(X509Certificate certificate)
		{
			this.Subject = certificate.Subject;
			this.Issuer = certificate.Issuer;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0003366C File Offset: 0x0003186C
		public bool IsMatchWith(X509Identifier other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.IsGenericIdentifier)
			{
				return this.Issuer.Equals(other.Issuer, StringComparison.OrdinalIgnoreCase);
			}
			return this.Equals(other);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0003369C File Offset: 0x0003189C
		public static X509Identifier Parse(string x509Identifier)
		{
			X509Identifier result = null;
			if (!X509Identifier.TryParse(x509Identifier, out result))
			{
				throw new FormatException(DataStrings.InvalidX509IdentifierFormat(x509Identifier));
			}
			return result;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x000336C8 File Offset: 0x000318C8
		public static bool TryParse(string x509Identifier, out X509Identifier instance)
		{
			Match match = Regex.Match(x509Identifier, "^X509:<I>([^<]+)(<S>(.+))?", RegexOptions.IgnoreCase);
			instance = null;
			bool flag = match.Success;
			if (flag)
			{
				try
				{
					instance = new X509Identifier(new X500DistinguishedName(match.Groups[1].Value, X500DistinguishedNameFlags.None).Format(false), new X500DistinguishedName(match.Groups[3].Value, X500DistinguishedNameFlags.None).Format(false));
				}
				catch (CryptographicException)
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00033748 File Offset: 0x00031948
		public override string ToString()
		{
			string result;
			if (this.IsGenericIdentifier)
			{
				result = this.ToIssuerString();
			}
			else
			{
				result = string.Format("{0}{1}{2}{3}{4}", new object[]
				{
					"X509:",
					"<I>",
					this.Issuer,
					"<S>",
					this.Subject
				});
			}
			return result;
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x000337A4 File Offset: 0x000319A4
		internal string ToIssuerString()
		{
			return string.Format("{0}{1}{2}", "X509:", "<I>", this.Issuer);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x000337C0 File Offset: 0x000319C0
		public bool Equals(X509Identifier other)
		{
			return other != null && this.Subject == other.Subject && this.Issuer == other.Issuer;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x000337ED File Offset: 0x000319ED
		public override bool Equals(object obj)
		{
			return this.Equals(obj as X509Identifier);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x000337FB File Offset: 0x000319FB
		public static bool operator ==(X509Identifier left, X509Identifier right)
		{
			if (left != null)
			{
				return left.Equals(right);
			}
			return right == null;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0003380C File Offset: 0x00031A0C
		public static bool operator !=(X509Identifier left, X509Identifier right)
		{
			return !(left == right);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00033818 File Offset: 0x00031A18
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x04000A74 RID: 2676
		internal const string Prefix = "X509:";

		// Token: 0x04000A75 RID: 2677
		private const string IssuerPrefix = "<I>";

		// Token: 0x04000A76 RID: 2678
		private const string SubjectPrefix = "<S>";

		// Token: 0x04000A77 RID: 2679
		private const string FormatRegularExpression = "^X509:<I>([^<]+)(<S>(.+))?";
	}
}
