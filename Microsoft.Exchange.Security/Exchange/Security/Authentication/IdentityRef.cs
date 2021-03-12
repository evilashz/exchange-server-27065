using System;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200006F RID: 111
	public class IdentityRef
	{
		// Token: 0x060003AB RID: 939 RVA: 0x0001CE58 File Offset: 0x0001B058
		public IdentityRef(CommonAccessToken cat, AuthenticationAuthority authenticationAuthority, bool isUsedForCanary) : this(IdentityRef.ExtractIdentityFromCat(cat), authenticationAuthority, isUsedForCanary)
		{
			if (cat == null)
			{
				throw new ArgumentNullException("cat", "You must specify the CommonAccessToken");
			}
			if (string.Equals(cat.TokenType, AccessTokenType.CompositeIdentity.ToString(), StringComparison.Ordinal))
			{
				throw new ArgumentException(string.Format("The CommonAccessToken type must not be '{0}'!", cat.TokenType));
			}
			this.CommonAccessToken = cat;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001CEBC File Offset: 0x0001B0BC
		public IdentityRef(GenericIdentity identity, AuthenticationAuthority authenticationAuthority, bool isUsedForCanary)
		{
			this.CommonAccessToken = null;
			this.Identity = identity;
			this.Authority = authenticationAuthority;
			this.IsUsedForCanary = isUsedForCanary;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0001CEE0 File Offset: 0x0001B0E0
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0001CEE8 File Offset: 0x0001B0E8
		public CommonAccessToken CommonAccessToken { get; private set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0001CEF1 File Offset: 0x0001B0F1
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0001CEF9 File Offset: 0x0001B0F9
		public AuthenticationAuthority Authority { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0001CF02 File Offset: 0x0001B102
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0001CF0A File Offset: 0x0001B10A
		public GenericIdentity Identity { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0001CF13 File Offset: 0x0001B113
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0001CF1B File Offset: 0x0001B11B
		public bool IsUsedForCanary { get; private set; }

		// Token: 0x060003B5 RID: 949 RVA: 0x0001CF24 File Offset: 0x0001B124
		private static GenericIdentity ExtractIdentityFromCat(CommonAccessToken cat)
		{
			if (cat == null)
			{
				throw new ArgumentNullException("cat", "You must specify the CommonAccessToken");
			}
			if (string.Equals(cat.TokenType, AccessTokenType.CompositeIdentity.ToString(), StringComparison.Ordinal))
			{
				throw new ArgumentException(string.Format("The CommonAccessToken type must not be '{0}'!", cat.TokenType));
			}
			BackendAuthenticator backendAuthenticator = null;
			IPrincipal principal = null;
			string text = null;
			IAccountValidationContext accountValidationContext = null;
			BackendAuthenticator.Rehydrate(cat, ref backendAuthenticator, false, out text, out principal, ref accountValidationContext);
			GenericIdentity genericIdentity = (principal != null) ? (principal.Identity as GenericIdentity) : null;
			if (genericIdentity == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The specified CAT({0}) does not contain a GenericIdentity!", new object[]
				{
					cat.TokenType
				}), "cat");
			}
			return genericIdentity;
		}

		// Token: 0x040003CA RID: 970
		public const int PrimaryIdentityIndex = -1;
	}
}
