using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B75 RID: 2933
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TokenValidator
	{
		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06003EEE RID: 16110 RVA: 0x000A5441 File Offset: 0x000A3641
		public Uri TargetUri
		{
			get
			{
				return this.targetUri;
			}
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06003EEF RID: 16111 RVA: 0x000A5449 File Offset: 0x000A3649
		public IEnumerable<X509Certificate2> TrustedTokenIssuerCertificates
		{
			get
			{
				return this.trustedTokenIssuerCertificates;
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06003EF0 RID: 16112 RVA: 0x000A5451 File Offset: 0x000A3651
		public IEnumerable<X509Certificate2> TokenDecryptionCertificates
		{
			get
			{
				return this.tokenDecryptionCertificates;
			}
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x000A5574 File Offset: 0x000A3774
		public TokenValidator(Uri targetUri, IEnumerable<X509Certificate2> trustedTokenIssuerCertificates, IEnumerable<X509Certificate2> tokenDecryptionCertificates)
		{
			this.trustedTokenIssuerCertificates = trustedTokenIssuerCertificates;
			this.tokenDecryptionCertificates = tokenDecryptionCertificates;
			this.targetUri = (targetUri.IsAbsoluteUri ? targetUri : new Uri("http://" + targetUri.OriginalString));
			CustomX509CertificateValidator validator = new CustomX509CertificateValidator(trustedTokenIssuerCertificates);
			this.authenticator = new SamlSecurityTokenAuthenticator(new List<SecurityTokenAuthenticator>(new SecurityTokenAuthenticator[]
			{
				new RsaSecurityTokenAuthenticator(),
				new X509SecurityTokenAuthenticator(validator)
			}), TokenValidator.MaximumTokenSkew);
			this.authenticator.AudienceUriMode = AudienceUriMode.Always;
			this.authenticator.AllowedAudienceUris.Add(this.targetUri.OriginalString);
			this.x509Authenticator = new X509SecurityTokenAuthenticator(validator);
			this.tokenDecryption = new TokenDecryption(trustedTokenIssuerCertificates, tokenDecryptionCertificates);
			if (TokenValidator.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				var array = new <>f__AnonymousType6<string, IEnumerable<X509Certificate2>>[]
				{
					new
					{
						Name = "STS",
						Collection = trustedTokenIssuerCertificates
					},
					new
					{
						Name = "Decryption",
						Collection = tokenDecryptionCertificates
					}
				};
				var array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					var <>f__AnonymousType = array2[i];
					StringBuilder stringBuilder = new StringBuilder();
					foreach (X509Certificate2 x509Certificate in <>f__AnonymousType.Collection)
					{
						stringBuilder.Append(" ");
						stringBuilder.Append(x509Certificate.Thumbprint);
					}
					TokenValidator.Tracer.TraceDebug<TokenValidator, string, StringBuilder>((long)this.GetHashCode(), "TokenValidator initialized with {0}={1}", this, <>f__AnonymousType.Name, stringBuilder);
				}
			}
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x000A570C File Offset: 0x000A390C
		public TokenValidationResults ValidateToken(SamlSecurityToken securityToken)
		{
			return this.ValidateTokenInternal(securityToken, null);
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x000A5716 File Offset: 0x000A3916
		public TokenValidationResults ValidateToken(SamlSecurityToken securityToken, Offer expectedOffer)
		{
			return this.ValidateTokenInternal(securityToken, expectedOffer);
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x000A5720 File Offset: 0x000A3920
		public TokenValidationResults ValidateToken(SupportingTokenSpecification supportingTokenSpecification, Offer expectedOffer)
		{
			AuthorizationContext authorizationContext = AuthorizationContext.CreateDefaultAuthorizationContext(supportingTokenSpecification.SecurityTokenPolicies);
			return this.ValidateTokenInternal(supportingTokenSpecification.SecurityToken, authorizationContext, expectedOffer);
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x000A5748 File Offset: 0x000A3948
		public TokenValidationResults ValidateToken(XmlElement token, Offer expectedOffer)
		{
			TokenValidator.Tracer.TraceDebug<string>((long)this.GetHashCode(), "TokenValidator to validate token: {0}", token.OuterXml);
			if (string.IsNullOrEmpty(token.OuterXml))
			{
				TokenValidator.Tracer.TraceError((long)this.GetHashCode(), "There is no token to validate");
				return TokenValidationResults.InvalidTokenFormat;
			}
			SecurityToken securityToken;
			try
			{
				securityToken = this.tokenDecryption.DecryptToken(token);
			}
			catch (TokenDecryptionException arg)
			{
				TokenValidator.Tracer.TraceError<string, TokenDecryptionException>((long)this.GetHashCode(), "Unable to decrypt encrypted XML token: {0}. Exception: {1}", token.OuterXml, arg);
				return TokenValidationResults.InvalidUnknownEncryption;
			}
			if (!this.authenticator.CanValidateToken(securityToken))
			{
				TokenValidator.Tracer.TraceError<string>((long)this.GetHashCode(), "It is not possible to validate this token: {0}", securityToken.Id);
				return TokenValidationResults.InvalidTokenFormat;
			}
			return this.ValidateTokenInternal(securityToken, expectedOffer);
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x000A581C File Offset: 0x000A3A1C
		public TokenValidationResults FindEmailAddress(XmlElement token, out bool isDelegationToken)
		{
			isDelegationToken = false;
			TokenValidator.Tracer.TraceDebug<string>((long)this.GetHashCode(), "TokenValidator to validate token: {0}", token.OuterXml);
			if (string.IsNullOrEmpty(token.OuterXml))
			{
				TokenValidator.Tracer.TraceError((long)this.GetHashCode(), "There is no token to validate");
				return TokenValidationResults.InvalidTokenFormat;
			}
			SecurityToken securityToken;
			try
			{
				securityToken = this.tokenDecryption.DecryptToken(token);
			}
			catch (TokenDecryptionException arg)
			{
				TokenValidator.Tracer.TraceError<string, TokenDecryptionException>((long)this.GetHashCode(), "Unable to decrypt encrypted XML token: {0}. Exception: {1}", token.OuterXml, arg);
				return TokenValidationResults.InvalidUnknownEncryption;
			}
			if (!this.authenticator.CanValidateToken(securityToken))
			{
				TokenValidator.Tracer.TraceError<string>((long)this.GetHashCode(), "It is not possible to validate this token: {0}", securityToken.Id);
				return TokenValidationResults.InvalidTokenFormat;
			}
			this.authenticator.AudienceUriMode = AudienceUriMode.Never;
			ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies;
			try
			{
				authorizationPolicies = this.authenticator.ValidateToken(securityToken);
			}
			catch (SecurityTokenException arg2)
			{
				TokenValidator.Tracer.TraceError<string, SecurityTokenException>((long)this.GetHashCode(), "Failed to validate token {0}. Exception {1}", securityToken.Id, arg2);
				return TokenValidationResults.InvalidTokenFailedValidation;
			}
			AuthorizationContext authorizationContext = AuthorizationContext.CreateDefaultAuthorizationContext(authorizationPolicies);
			foreach (ClaimSet claimSet in authorizationContext.ClaimSets)
			{
				foreach (Claim claim in claimSet.FindClaims("http://schemas.microsoft.com/ws/2006/04/identity/claims/ThirdPartyRequested", Rights.Identity))
				{
					isDelegationToken = true;
				}
			}
			string text = TokenValidator.FindEmailAddress(authorizationContext);
			if (text == null)
			{
				TokenValidator.Tracer.TraceError<string>((long)this.GetHashCode(), "Unable to find EmailAddress claim in token {0}", securityToken.Id);
				return TokenValidationResults.InvalidUnknownEmailAddress;
			}
			return new TokenValidationResults(null, text, null, null, null, null);
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x000A5A08 File Offset: 0x000A3C08
		public TokenValidationResults AuthorizationContextFromToken(XmlElement token, out AuthorizationContext authorizationContext)
		{
			authorizationContext = null;
			TokenValidator.Tracer.TraceDebug<string>((long)this.GetHashCode(), "TokenValidator to validate token: {0}", token.OuterXml);
			if (string.IsNullOrEmpty(token.OuterXml))
			{
				TokenValidator.Tracer.TraceError((long)this.GetHashCode(), "There is no token to validate");
				return TokenValidationResults.InvalidTokenFormat;
			}
			byte[] rawData;
			try
			{
				rawData = Convert.FromBase64String(token.InnerText);
			}
			catch (FormatException arg)
			{
				TokenValidator.Tracer.TraceError<FormatException>((long)this.GetHashCode(), "Failed to parse token. Exception {0}", arg);
				return TokenValidationResults.InvalidTokenFormat;
			}
			string text = string.Empty;
			XmlAttribute xmlAttribute = token.Attributes["wsu:Id"];
			if (xmlAttribute != null)
			{
				text = xmlAttribute.InnerText;
			}
			X509SecurityToken x509SecurityToken;
			try
			{
				X509Certificate2 certificate = new X509Certificate2(rawData);
				x509SecurityToken = new X509SecurityToken(certificate, text);
			}
			catch (SecurityTokenException arg2)
			{
				TokenValidator.Tracer.TraceError<string, SecurityTokenException>((long)this.GetHashCode(), "It is not possible to validate this token: {0}. Exception {1}", text, arg2);
				return TokenValidationResults.InvalidTokenFormat;
			}
			catch (CryptographicException arg3)
			{
				TokenValidator.Tracer.TraceError<string, CryptographicException>((long)this.GetHashCode(), "It is not possible to validate this token: {0}. Exception {1}", text, arg3);
				return TokenValidationResults.InvalidTokenFormat;
			}
			if (!this.x509Authenticator.CanValidateToken(x509SecurityToken))
			{
				TokenValidator.Tracer.TraceError<string>((long)this.GetHashCode(), "It is not possible to validate this token: {0}", x509SecurityToken.Id);
				return TokenValidationResults.InvalidTokenFormat;
			}
			ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies;
			try
			{
				authorizationPolicies = this.x509Authenticator.ValidateToken(x509SecurityToken);
			}
			catch (SecurityTokenException arg4)
			{
				TokenValidator.Tracer.TraceError<string, SecurityTokenException>((long)this.GetHashCode(), "Failed to validate token {0}. Exception {1}", x509SecurityToken.Id, arg4);
				return TokenValidationResults.InvalidTokenFailedValidation;
			}
			authorizationContext = AuthorizationContext.CreateDefaultAuthorizationContext(authorizationPolicies);
			return new TokenValidationResults(null, null, null, null, null, null);
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x000A5BCC File Offset: 0x000A3DCC
		private TokenValidationResults ValidateTokenInternal(SecurityToken securityToken, Offer expectedOffer)
		{
			ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies;
			try
			{
				authorizationPolicies = this.authenticator.ValidateToken(securityToken);
			}
			catch (SecurityTokenException arg)
			{
				TokenValidator.Tracer.TraceError<string, SecurityTokenException>((long)this.GetHashCode(), "Failed to validate token {0}. Exception {1}", securityToken.Id, arg);
				return TokenValidationResults.InvalidTokenFailedValidation;
			}
			AuthorizationContext authorizationContext = AuthorizationContext.CreateDefaultAuthorizationContext(authorizationPolicies);
			return this.ValidateTokenInternal(securityToken, authorizationContext, expectedOffer);
		}

		// Token: 0x06003EF9 RID: 16121 RVA: 0x000A5C30 File Offset: 0x000A3E30
		private TokenValidationResults ValidateTokenInternal(SecurityToken securityToken, AuthorizationContext authorizationContext, Offer expectedOffer)
		{
			string text = TokenValidator.FindUpnClaim(authorizationContext);
			if (text == null)
			{
				TokenValidator.Tracer.TraceError<string>((long)this.GetHashCode(), "Unable to find UPN claim in token {0}", securityToken.Id);
				return TokenValidationResults.InvalidUnknownExternalIdentity;
			}
			Offer offer = TokenValidator.FindOfferClaim(authorizationContext);
			if (offer == null)
			{
				TokenValidator.Tracer.TraceError<Offer, string>((long)this.GetHashCode(), "Unable to find offer {0} in token {1}", offer, securityToken.Id);
				return TokenValidationResults.InvalidOffer;
			}
			if (expectedOffer != null && offer != expectedOffer)
			{
				TokenValidator.Tracer.TraceError<Offer, Offer>((long)this.GetHashCode(), "Offer in token ({0}) is not expected ({1})", offer, expectedOffer);
				return TokenValidationResults.InvalidOffer;
			}
			DateTime utcNow = DateTime.UtcNow;
			DateTime dateTime = securityToken.ValidFrom + offer.Duration;
			if (utcNow > dateTime)
			{
				TokenValidator.Tracer.TraceError<DateTime, double, DateTime>((long)this.GetHashCode(), "Token is already expired according to ValidFrom field in token ({0}) plus offer duration ({1} secs). Calculated expiration is {2}.", securityToken.ValidFrom, offer.Duration.TotalSeconds, dateTime);
				return TokenValidationResults.InvalidExpired;
			}
			string text2 = TokenValidator.FindEmailAddress(authorizationContext);
			if (text2 == null)
			{
				TokenValidator.Tracer.TraceError<string>((long)this.GetHashCode(), "Unable to find EmailAddress claim in token {0}", securityToken.Id);
				return TokenValidationResults.InvalidUnknownEmailAddress;
			}
			SymmetricSecurityKey proofToken = TokenValidator.FindProofToken(securityToken);
			List<string> list = TokenValidator.FindEmailAddresses(authorizationContext);
			TokenValidator.Tracer.TraceDebug<int>((long)this.GetHashCode(), "Number of emailAddresses {0}", list.Count);
			TokenValidationResults tokenValidationResults = new TokenValidationResults(text, text2, offer, securityToken, proofToken, list);
			TokenValidator.Tracer.TraceDebug<TokenValidationResults>((long)this.GetHashCode(), "TokenValidator validation result: {0}", tokenValidationResults);
			return tokenValidationResults;
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x000A5D94 File Offset: 0x000A3F94
		private static string FindUpnClaim(AuthorizationContext authorizationContext)
		{
			foreach (ClaimSet claimSet in authorizationContext.ClaimSets)
			{
				foreach (Claim claim in claimSet.FindClaims(ClaimTypes.NameIdentifier, Rights.Identity))
				{
					SamlNameIdentifierClaimResource samlNameIdentifierClaimResource = claim.Resource as SamlNameIdentifierClaimResource;
					if (samlNameIdentifierClaimResource != null)
					{
						return samlNameIdentifierClaimResource.Name;
					}
				}
			}
			return null;
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x000A5E40 File Offset: 0x000A4040
		private static Offer FindOfferClaim(AuthorizationContext authorizationContext)
		{
			foreach (ClaimSet claimSet in authorizationContext.ClaimSets)
			{
				foreach (Claim claim in claimSet.FindClaims("http://schemas.xmlsoap.org/ws/2006/12/authorization/claims/action", Rights.PossessProperty))
				{
					string text = claim.Resource as string;
					if (text != null)
					{
						Offer offer = Offer.Find(text);
						if (offer != null)
						{
							return offer;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06003EFC RID: 16124 RVA: 0x000A5EF4 File Offset: 0x000A40F4
		private static string FindEmailAddress(AuthorizationContext authorizationContext)
		{
			foreach (ClaimSet claimSet in authorizationContext.ClaimSets)
			{
				foreach (Claim claim in claimSet.FindClaims("http://schemas.xmlsoap.org/claims/EmailAddress", Rights.PossessProperty))
				{
					string text = claim.Resource as string;
					if (!string.IsNullOrEmpty(text))
					{
						return text;
					}
				}
			}
			return null;
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x000A5FA0 File Offset: 0x000A41A0
		private static List<string> FindEmailAddresses(AuthorizationContext authorizationContext)
		{
			List<string> list = new List<string>();
			foreach (ClaimSet claimSet in authorizationContext.ClaimSets)
			{
				foreach (Claim claim in claimSet.FindClaims("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/EmailAddressList", Rights.PossessProperty))
				{
					string text = claim.Resource as string;
					if (!string.IsNullOrEmpty(text))
					{
						list.Add(text);
					}
				}
			}
			return list;
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x000A6054 File Offset: 0x000A4254
		private static SymmetricSecurityKey FindProofToken(SecurityToken securityToken)
		{
			SamlSecurityToken samlSecurityToken = securityToken as SamlSecurityToken;
			if (samlSecurityToken != null)
			{
				foreach (SamlStatement samlStatement in samlSecurityToken.Assertion.Statements)
				{
					SamlAuthenticationStatement samlAuthenticationStatement = samlStatement as SamlAuthenticationStatement;
					if (samlAuthenticationStatement != null)
					{
						SymmetricSecurityKey symmetricSecurityKey = samlAuthenticationStatement.SamlSubject.Crypto as SymmetricSecurityKey;
						if (symmetricSecurityKey != null)
						{
							return symmetricSecurityKey;
						}
					}
				}
				TokenValidator.Tracer.TraceError<string>(0L, "Unable to find proof token in security token: {0}", securityToken.Id);
				throw new ProofTokenNotFoundException();
			}
			TokenValidator.Tracer.TraceDebug<string>(0L, "Security token is not SAML so no proof token will be returned: {0}", securityToken.Id);
			return null;
		}

		// Token: 0x040036B9 RID: 14009
		private TokenDecryption tokenDecryption;

		// Token: 0x040036BA RID: 14010
		private SamlSecurityTokenAuthenticator authenticator;

		// Token: 0x040036BB RID: 14011
		private X509SecurityTokenAuthenticator x509Authenticator;

		// Token: 0x040036BC RID: 14012
		private Uri targetUri;

		// Token: 0x040036BD RID: 14013
		private IEnumerable<X509Certificate2> trustedTokenIssuerCertificates;

		// Token: 0x040036BE RID: 14014
		private IEnumerable<X509Certificate2> tokenDecryptionCertificates;

		// Token: 0x040036BF RID: 14015
		private static TimeSpan MaximumTokenSkew = TimeSpan.FromMinutes(5.0);

		// Token: 0x040036C0 RID: 14016
		private static readonly Trace Tracer = ExTraceGlobals.WSTrustTracer;
	}
}
