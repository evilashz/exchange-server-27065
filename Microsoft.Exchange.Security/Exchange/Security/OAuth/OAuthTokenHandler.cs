using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000A0 RID: 160
	internal abstract class OAuthTokenHandler : JwtSecurityTokenHandler
	{
		// Token: 0x0600055F RID: 1375 RVA: 0x0002B725 File Offset: 0x00029925
		protected OAuthTokenHandler(JwtSecurityToken token, Uri targetUri)
		{
			this.token = token;
			this.targetUri = targetUri;
			base.RequireSignedTokens = true;
			base.RequireExpirationTime = true;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0002B759 File Offset: 0x00029959
		public static OAuthTokenHandler DummyHandler
		{
			get
			{
				return OAuthTokenHandler.Dummy.Instance;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0002B760 File Offset: 0x00029960
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0002B768 File Offset: 0x00029968
		public LocalConfiguration LocalConfiguration
		{
			get
			{
				return this.localConfiguration;
			}
			set
			{
				this.localConfiguration = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0002B771 File Offset: 0x00029971
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0002B779 File Offset: 0x00029979
		public bool IsUnitTestOnlyPath
		{
			get
			{
				return this.isUnitTestOnlyPath;
			}
			set
			{
				this.isUnitTestOnlyPath = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0002B782 File Offset: 0x00029982
		public virtual bool IsSignatureValidated
		{
			get
			{
				return this.signatureValidated;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0002B78A File Offset: 0x0002998A
		public JwtSecurityToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000567 RID: 1383
		public abstract string TokenCategory { get; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x0002B792 File Offset: 0x00029992
		public virtual IEnumerable<string> ClaimTypesForLogging
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0002B795 File Offset: 0x00029995
		public TrustedIssuer MatchedIssuer
		{
			get
			{
				return this.matchedIssuer;
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0002B7A0 File Offset: 0x000299A0
		public static OAuthIdentity GetOAuthIdentity(string rawToken, out string loggableToken)
		{
			TrustedIssuer trustedIssuer;
			return OAuthTokenHandler.GetOAuthIdentity(rawToken, ConfigProvider.Instance.Configuration, false, out loggableToken, out trustedIssuer);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0002B7C4 File Offset: 0x000299C4
		internal static OAuthIdentity GetOAuthIdentity(string rawToken, LocalConfiguration localConfiguration, bool isUnitTestOnlyPath, out string loggableToken, out TrustedIssuer trustedIssuer)
		{
			OAuthTokenHandler oauthTokenHandler = null;
			loggableToken = null;
			trustedIssuer = null;
			OAuthIdentity oauthIdentity;
			try
			{
				oauthTokenHandler = OAuthTokenHandler.CreateTokenHandler(rawToken, null, out loggableToken);
				oauthTokenHandler.LocalConfiguration = localConfiguration;
				oauthTokenHandler.IsUnitTestOnlyPath = isUnitTestOnlyPath;
				oauthIdentity = oauthTokenHandler.GetOAuthIdentity();
			}
			finally
			{
				if (oauthTokenHandler != null)
				{
					trustedIssuer = oauthTokenHandler.MatchedIssuer;
				}
			}
			return oauthIdentity;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0002B818 File Offset: 0x00029A18
		public virtual AuthenticationAuthority AuthenticationAuthority
		{
			get
			{
				return AuthenticationAuthority.ORGID;
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0002B81C File Offset: 0x00029A1C
		public OAuthIdentity GetOAuthIdentity()
		{
			this.ThrowIfFalse(this.isUnitTestOnlyPath || OAuthAppPoolLevelPolicy.Instance.IsAllowedProfiles(this.TokenCategory), OAuthErrors.TokenProfileNotApplicable, new object[0]);
			this.ValidateToken();
			this.ThrowIfFalse(this.IsSignatureValidated, OAuthErrors.UnexpectedErrorOccurred, new object[0]);
			return OAuthIdentity.Create(this.organizationId, this.oauthApplication, this.oauthActAsUser);
		}

		// Token: 0x0600056E RID: 1390
		public abstract OAuthPreAuthIdentity GetPreAuthIdentity();

		// Token: 0x0600056F RID: 1391 RVA: 0x0002B88C File Offset: 0x00029A8C
		public static string ValidateWacCallbackToken(string rawToken)
		{
			JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadToken(rawToken) as JwtSecurityToken;
			OAuthTokenHandler oauthTokenHandler = OAuthTokenHandler.CreateTokenHandler(jwtSecurityToken, null);
			ExTraceGlobals.OAuthTracer.TraceDebug<OAuthTokenHandler>(0L, "[GetOAuthIdentity] Created the token handler: {0}", oauthTokenHandler);
			oauthTokenHandler.PreValidateToken();
			oauthTokenHandler.ValidateIssuer();
			TokenValidationParameters tokenValidationParameters = oauthTokenHandler.CreateTokenValidationParameters();
			oauthTokenHandler.ValidateToken(jwtSecurityToken, tokenValidationParameters);
			oauthTokenHandler.ThrowIfFalse(oauthTokenHandler.IsSignatureValidated, OAuthErrors.UnexpectedErrorOccurred, new object[0]);
			string value;
			oauthTokenHandler.ThrowIfFalse(OAuthCommon.TryGetClaimValue(jwtSecurityToken, Constants.ClaimTypes.AppContext, out value), OAuthErrors.UnexpectedErrorOccurred, new object[0]);
			Dictionary<string, string> dictionary = value.DeserializeFromJson<Dictionary<string, string>>();
			string result;
			oauthTokenHandler.ThrowIfFalse(dictionary.TryGetValue(Constants.ClaimTypes.Scope, out result), OAuthErrors.UnexpectedErrorOccurred, new object[0]);
			return result;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0002B940 File Offset: 0x00029B40
		public static OAuthTokenHandler CreateTokenHandler(string rawToken, Uri targetUri, out string loggableToken)
		{
			JwtSecurityToken jwtSecurityToken = null;
			OAuthTokenHandler result;
			try
			{
				jwtSecurityToken = (new JwtSecurityTokenHandler().ReadToken(rawToken) as JwtSecurityToken);
				result = OAuthTokenHandler.CreateTokenHandler(jwtSecurityToken, targetUri);
			}
			finally
			{
				loggableToken = OAuthCommon.GetLoggableTokenString(rawToken, jwtSecurityToken);
			}
			return result;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0002B988 File Offset: 0x00029B88
		private static OAuthTokenHandler CreateTokenHandler(JwtSecurityToken token, Uri targetUri)
		{
			string a = null;
			if (OAuthCommon.TryGetClaimValue(token, Constants.ClaimTypes.Ver, out a))
			{
				if (string.Equals(a, Constants.ClaimValues.ExchangeSelfIssuedVersion1, StringComparison.OrdinalIgnoreCase))
				{
					return new OAuthTokenHandler.V1ProfileExchangeSelfIssuedActAsTokenHandler(token, targetUri);
				}
				if (!string.Equals(a, Constants.ClaimValues.Version1, StringComparison.OrdinalIgnoreCase))
				{
					throw new InvalidOAuthTokenException(OAuthErrors.UnexpectedErrorOccurred, null, null);
				}
				string text = null;
				string text2 = null;
				string text3 = null;
				if (OAuthCommon.TryGetClaimValue(token, Constants.ClaimTypes.MsExchCallback, out text3))
				{
					return new OAuthTokenHandler.V1CallbackTokenHandler(token, targetUri);
				}
				if (OAuthCommon.TryGetClaimValue(token, Constants.ClaimTypes.Scp, out text))
				{
					return new OAuthTokenHandler.V1ProfileAppActAsTokenHandler(token, targetUri);
				}
				if (OAuthCommon.TryGetClaimValue(token, Constants.ClaimTypes.AppId, out text2))
				{
					return new OAuthTokenHandler.V1ProfileAppTokenHandler(token, targetUri);
				}
				return new OAuthTokenHandler.V1ProfileIdTokenHandler(token, targetUri);
			}
			else
			{
				string actorToken = null;
				if (OAuthCommon.TryGetClaimValue(token, Constants.ClaimTypes.ActorToken, out actorToken))
				{
					return new OAuthTokenHandler.ActAsTokenHandler(token, targetUri, actorToken);
				}
				string appContextString = null;
				if (OAuthCommon.TryGetClaimValue(token, Constants.ClaimTypes.AppContext, out appContextString))
				{
					return new OAuthTokenHandler.CallbackTokenHandler(token, targetUri, appContextString);
				}
				return new OAuthTokenHandler.ActorTokenHandler(token, targetUri);
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0002BA6C File Offset: 0x00029C6C
		protected virtual void ValidateToken()
		{
			this.PreValidateToken();
			this.ValidateAudience();
			this.ValidateIssuer();
			TokenValidationParameters tokenValidationParameters = this.CreateTokenValidationParameters();
			base.ValidateToken(this.token, tokenValidationParameters);
			this.ResolveOrganizationId();
			this.ResolveOAuthApplication();
			this.ResolveUserInfo();
			this.PostValidateToken();
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0002BAB8 File Offset: 0x00029CB8
		protected virtual void PreValidateToken()
		{
		}

		// Token: 0x06000574 RID: 1396
		protected abstract void ValidateAudience();

		// Token: 0x06000575 RID: 1397 RVA: 0x0002BABC File Offset: 0x00029CBC
		protected virtual void ValidateIssuer()
		{
			if (this.token.Header.TryGetValue(Constants.ClaimTypes.X509CertificateThumbprint, out this.x5tHint))
			{
				ExTraceGlobals.OAuthTracer.TraceDebug<string>((long)this.GetHashCode(), "[ValidateIssuer] found x5t in the header, the value is '{0}'", this.x5tHint);
			}
			string issuer = this.token.Issuer;
			this.ThrowIfFalse(!string.IsNullOrEmpty(issuer), OAuthErrors.MissingIssuer, new object[0]);
			this.FindMatchedTrustedIssuer(issuer);
			this.ThrowIfFalse(this.matchedIssuer != null, OAuthErrors.NoConfiguredIssuerMatched, new object[]
			{
				issuer
			});
		}

		// Token: 0x06000576 RID: 1398
		protected abstract void FindMatchedTrustedIssuer(string issuer);

		// Token: 0x06000577 RID: 1399 RVA: 0x0002BB54 File Offset: 0x00029D54
		protected virtual TokenValidationParameters CreateTokenValidationParameters()
		{
			TokenValidationParameters tokenValidationParameters = new TokenValidationParameters();
			this.matchedIssuer.SetSigningTokens(this.x5tHint, tokenValidationParameters);
			return tokenValidationParameters;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0002BB7C File Offset: 0x00029D7C
		protected override void ValidateSignature(JwtSecurityToken jwt, TokenValidationParameters validationParameters)
		{
			try
			{
				base.ValidateSignature(jwt, validationParameters);
			}
			catch (SecurityTokenValidationException ex)
			{
				ExTraceGlobals.OAuthTracer.TraceDebug<string, SecurityTokenValidationException>((long)this.GetHashCode(), "[OAuthTokenHandler:ValidateSignature] the x5t value is {0}, wif library throws: {1}", this.x5tHint, ex);
				this.matchedIssuer.PokeOnlineCertificateProvider();
				this.Throw(OAuthErrors.InvalidSignature, null, ex, this.x5tHint);
			}
			finally
			{
				this.signatureValidated = true;
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0002BBF8 File Offset: 0x00029DF8
		protected override void ValidateSigningToken(JwtSecurityToken jwt)
		{
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0002BBFA File Offset: 0x00029DFA
		protected override void ValidateAudience(JwtSecurityToken jwt, TokenValidationParameters validationParameters)
		{
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0002BBFC File Offset: 0x00029DFC
		protected override string ValidateIssuer(JwtSecurityToken jwt, TokenValidationParameters validationParameters)
		{
			return null;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0002BBFF File Offset: 0x00029DFF
		protected virtual void PostValidateToken()
		{
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0002BC04 File Offset: 0x00029E04
		protected virtual void ResolveOrganizationId()
		{
			this.organizationId = this.ResolveOrganizationByRealm(this.realmFromAudience);
			this.ThrowIfFalse(this.organizationId != null, OAuthErrors.OrganizationIdNotFoundFromRealm, new object[]
			{
				this.realmFromAudience
			});
		}

		// Token: 0x0600057E RID: 1406
		protected abstract void ResolveOAuthApplication();

		// Token: 0x0600057F RID: 1407
		protected abstract void ResolveUserInfo();

		// Token: 0x06000580 RID: 1408 RVA: 0x0002BC68 File Offset: 0x00029E68
		protected OrganizationId ResolveOrganizationByRealm(string realm)
		{
			if (AuthCommon.IsMultiTenancyEnabled)
			{
				Guid guid;
				if (Guid.TryParse(realm, out guid))
				{
					try
					{
						return ADSessionSettings.FromExternalDirectoryOrganizationId(guid).GetCurrentOrganizationIdPopulated();
					}
					catch (CannotResolveExternalDirectoryOrganizationIdException innerException)
					{
						this.Throw(OAuthErrors.ExternalOrgIdNotFound, new object[]
						{
							guid
						}, innerException, null);
						goto IL_124;
					}
					catch (CannotResolveTenantNameException innerException2)
					{
						this.Throw(OAuthErrors.ExternalOrgIdNotFound, new object[]
						{
							guid
						}, innerException2, null);
						goto IL_124;
					}
				}
				SmtpDomain smtpDomain;
				if (SmtpDomain.TryParse(realm, out smtpDomain))
				{
					return DomainToOrganizationIdCache.Singleton.Get(smtpDomain);
				}
			}
			else
			{
				SmtpDomain smtpDomain;
				if (SmtpDomain.TryParse(realm, out smtpDomain) && OrganizationId.ForestWideOrgId == DomainToOrganizationIdCache.Singleton.Get(smtpDomain))
				{
					return OrganizationId.ForestWideOrgId;
				}
				if (OAuthCommon.IsRealmMatch(this.localConfiguration.SingleTenancyRealm, realm))
				{
					return OrganizationId.ForestWideOrgId;
				}
				if (this.localConfiguration.AuthServers.Any((AuthServer authServer) => OAuthCommon.IsRealmMatch(authServer.Realm, realm)))
				{
					return OrganizationId.ForestWideOrgId;
				}
			}
			IL_124:
			return null;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0002BDBC File Offset: 0x00029FBC
		public string GetExtraLoggingInfo()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			stringBuilder.AppendFormat("Category:{0}|", this.TokenCategory);
			stringBuilder.AppendFormat("AppId:{0}|", (this.oauthApplication == null) ? string.Empty : this.oauthApplication.Id);
			if (this.ClaimTypesForLogging != null)
			{
				foreach (string text in this.ClaimTypesForLogging)
				{
					object arg;
					if (this.token.Payload.TryGetValue(text, out arg))
					{
						stringBuilder.AppendFormat("{0}:{1}|", text, arg);
					}
				}
			}
			if (this.inboundError != OAuthErrors.NoError)
			{
				stringBuilder.AppendFormat("ErrorCode:{0}", this.inboundError);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0002BE98 File Offset: 0x0002A098
		public string GetClaimValue(JwtSecurityToken token, string claimType)
		{
			string text = OAuthCommon.TryGetClaimValue(token.Payload, claimType);
			this.ThrowIfFalse(!string.IsNullOrEmpty(text), OAuthErrors.NoClaimFound, new object[]
			{
				claimType
			});
			return text;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0002BED3 File Offset: 0x0002A0D3
		public void Throw(OAuthErrors inboundError, object[] args = null, Exception innerException = null, string periodicKey = null)
		{
			this.ThrowIfFalse(false, inboundError, args, innerException, periodicKey);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0002BEE1 File Offset: 0x0002A0E1
		public void ThrowIfFalse(bool condition, OAuthErrors inboundError, params object[] args)
		{
			this.ThrowIfFalse(condition, inboundError, args, null, null);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0002BEF0 File Offset: 0x0002A0F0
		public void ThrowIfFalse(bool condition, OAuthErrors inboundError, object[] args, Exception innerException, string logPeriodicKey = null)
		{
			if (!condition)
			{
				this.inboundError = inboundError;
				InvalidOAuthTokenException ex = new InvalidOAuthTokenException(inboundError, args, innerException);
				if (!string.IsNullOrEmpty(logPeriodicKey))
				{
					ex.LogEvent = true;
					ex.LogPeriodicKey = logPeriodicKey;
				}
				throw ex;
			}
		}

		// Token: 0x0400059C RID: 1436
		protected readonly JwtSecurityToken token;

		// Token: 0x0400059D RID: 1437
		protected readonly Uri targetUri;

		// Token: 0x0400059E RID: 1438
		protected LocalConfiguration localConfiguration = ConfigProvider.Instance.Configuration;

		// Token: 0x0400059F RID: 1439
		protected bool isUnitTestOnlyPath;

		// Token: 0x040005A0 RID: 1440
		protected string x5tHint;

		// Token: 0x040005A1 RID: 1441
		protected string realmFromAudience;

		// Token: 0x040005A2 RID: 1442
		protected string realmFromIssuer;

		// Token: 0x040005A3 RID: 1443
		protected TrustedIssuer matchedIssuer;

		// Token: 0x040005A4 RID: 1444
		protected OrganizationId organizationId;

		// Token: 0x040005A5 RID: 1445
		protected OAuthApplication oauthApplication;

		// Token: 0x040005A6 RID: 1446
		protected OAuthActAsUser oauthActAsUser;

		// Token: 0x040005A7 RID: 1447
		protected bool signatureValidated;

		// Token: 0x040005A8 RID: 1448
		private OAuthErrors inboundError;

		// Token: 0x020000A1 RID: 161
		public abstract class S2STokenHandlerBase : OAuthTokenHandler
		{
			// Token: 0x06000586 RID: 1414 RVA: 0x0002BF2B File Offset: 0x0002A12B
			protected S2STokenHandlerBase(JwtSecurityToken token, Uri targetUri) : base(token, targetUri)
			{
			}

			// Token: 0x06000587 RID: 1415 RVA: 0x0002BF38 File Offset: 0x0002A138
			protected override void ValidateAudience()
			{
				string audience = this.token.Audience;
				string text = null;
				string text2 = null;
				base.ThrowIfFalse(this.TryParseAudience(audience, out this.appIdFromAudience, out text, out text2), OAuthErrors.InvalidAudience, new object[]
				{
					audience
				});
				base.ThrowIfFalse(OAuthCommon.IsIdMatch(this.appIdFromAudience, this.localConfiguration.ApplicationId), OAuthErrors.InvalidAudience, new object[]
				{
					audience
				});
				if (!this.isUnitTestOnlyPath)
				{
					string authority = this.targetUri.Authority;
					base.ThrowIfFalse(string.Equals(authority, text, StringComparison.OrdinalIgnoreCase), OAuthErrors.UnexpectedHostNameInAudience, new object[]
					{
						authority,
						text
					});
				}
				base.ThrowIfFalse(!string.IsNullOrEmpty(text2), OAuthErrors.EmptyRealmFromAudience, new object[0]);
				this.realmFromAudience = text2;
			}

			// Token: 0x06000588 RID: 1416 RVA: 0x0002C00C File Offset: 0x0002A20C
			protected override void FindMatchedTrustedIssuer(string issuer)
			{
				IssuerMetadata issuerMetadata = null;
				base.ThrowIfFalse(IssuerMetadata.TryParse(issuer, out issuerMetadata), OAuthErrors.InvalidIssuerFormat, new object[]
				{
					issuer
				});
				this.realmFromIssuer = issuerMetadata.Realm;
				this.InternalResolveTrustedIssuer(issuerMetadata);
			}

			// Token: 0x06000589 RID: 1417 RVA: 0x0002C050 File Offset: 0x0002A250
			protected virtual void InternalResolveTrustedIssuer(IssuerMetadata issuerMetadataFromToken)
			{
				foreach (TrustedIssuer trustedIssuer in this.localConfiguration.TrustedIssuers)
				{
					IssuerMetadata issuerMetadata = trustedIssuer.IssuerMetadata;
					if (issuerMetadata.Kind == IssuerKind.ACS)
					{
						if (issuerMetadata.MatchId(issuerMetadataFromToken))
						{
							if (issuerMetadata.HasEmptyRealm)
							{
								if (OAuthCommon.IsRealmMatch(this.realmFromIssuer, this.realmFromAudience))
								{
									this.matchedIssuer = trustedIssuer;
									return;
								}
							}
							else if (issuerMetadata.MatchRealm(issuerMetadataFromToken))
							{
								this.matchedIssuer = trustedIssuer;
								return;
							}
						}
					}
					else if (issuerMetadata.Kind == IssuerKind.PartnerApp && issuerMetadata.MatchIdAndRealm(issuerMetadataFromToken))
					{
						this.matchedIssuer = trustedIssuer;
						return;
					}
				}
			}

			// Token: 0x0600058A RID: 1418 RVA: 0x0002C0E4 File Offset: 0x0002A2E4
			protected bool TryParseNameId(string nameId, out string appId, out string realm)
			{
				string text;
				realm = (text = null);
				appId = text;
				int num = nameId.IndexOf('@');
				if (num == -1)
				{
					return false;
				}
				realm = nameId.Substring(num + 1);
				appId = nameId.Substring(0, num);
				return true;
			}

			// Token: 0x0600058B RID: 1419 RVA: 0x0002C120 File Offset: 0x0002A320
			private bool TryParseAudience(string audience, out string appId, out string host, out string realm)
			{
				string text;
				realm = (text = null);
				string text2;
				host = (text2 = text);
				appId = text2;
				int num = audience.LastIndexOf('@');
				if (num == -1)
				{
					return false;
				}
				int num2 = audience.IndexOf('/');
				if (num2 == -1)
				{
					return false;
				}
				if (num < num2)
				{
					return false;
				}
				appId = audience.Substring(0, num2);
				realm = audience.Substring(num + 1);
				host = audience.Substring(num2 + 1, num - num2 - 1);
				return true;
			}

			// Token: 0x040005A9 RID: 1449
			protected string appIdFromAudience;
		}

		// Token: 0x020000A2 RID: 162
		public sealed class ActAsTokenHandler : OAuthTokenHandler.S2STokenHandlerBase
		{
			// Token: 0x0600058C RID: 1420 RVA: 0x0002C188 File Offset: 0x0002A388
			public ActAsTokenHandler(JwtSecurityToken token, Uri targetUri, string actorToken) : base(token, targetUri)
			{
				base.RequireSignedTokens = false;
				JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadToken(actorToken) as JwtSecurityToken;
				base.ThrowIfFalse(!string.IsNullOrEmpty(jwtSecurityToken.EncodedSignature), OAuthErrors.ActorTokenMustBeSigned, new object[0]);
				base.ThrowIfFalse(string.IsNullOrEmpty(token.EncodedSignature), OAuthErrors.OuterTokenAlsoSigned, new object[0]);
				this.innerTokenHandler = new OAuthTokenHandler.ActorTokenHandler(jwtSecurityToken, targetUri);
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x0600058D RID: 1421 RVA: 0x0002C1FD File Offset: 0x0002A3FD
			public override bool IsSignatureValidated
			{
				get
				{
					return this.innerTokenHandler.signatureValidated;
				}
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x0600058E RID: 1422 RVA: 0x0002C20A File Offset: 0x0002A40A
			public override string TokenCategory
			{
				get
				{
					return Constants.TokenCategories.S2SAppActAsToken;
				}
			}

			// Token: 0x0600058F RID: 1423 RVA: 0x0002C211 File Offset: 0x0002A411
			protected override void ValidateToken()
			{
				this.innerTokenHandler.localConfiguration = this.localConfiguration;
				this.innerTokenHandler.isUnitTestOnlyPath = this.isUnitTestOnlyPath;
				this.innerTokenHandler.ValidateToken();
				base.ValidateToken();
			}

			// Token: 0x06000590 RID: 1424 RVA: 0x0002C248 File Offset: 0x0002A448
			protected override void ValidateIssuer()
			{
				IssuerMetadata issuerMetadata = null;
				string issuer = this.token.Issuer;
				base.ThrowIfFalse(IssuerMetadata.TryParse(issuer, out issuerMetadata), OAuthErrors.InvalidOuterTokenIssuerFormat, new object[]
				{
					issuer
				});
				string applicationIdentifier = this.innerTokenHandler.PartnerApplication.ApplicationIdentifier;
				base.ThrowIfFalse(OAuthCommon.IsIdMatch(issuerMetadata.Id, applicationIdentifier), OAuthErrors.InvalidOuterTokenIssuerIdValue, new object[]
				{
					issuerMetadata.Id,
					applicationIdentifier
				});
				this.realmFromIssuer = issuerMetadata.Realm;
			}

			// Token: 0x06000591 RID: 1425 RVA: 0x0002C2CF File Offset: 0x0002A4CF
			protected override void ValidateAudience()
			{
			}

			// Token: 0x06000592 RID: 1426 RVA: 0x0002C2D1 File Offset: 0x0002A4D1
			protected override TokenValidationParameters CreateTokenValidationParameters()
			{
				return OAuthTokenHandler.ActAsTokenHandler.defaultTokenValidationParameters;
			}

			// Token: 0x06000593 RID: 1427 RVA: 0x0002C2D8 File Offset: 0x0002A4D8
			protected override void ValidateSignature(JwtSecurityToken jwt, TokenValidationParameters validationParameters)
			{
			}

			// Token: 0x06000594 RID: 1428 RVA: 0x0002C2DA File Offset: 0x0002A4DA
			protected override void ResolveOrganizationId()
			{
				this.organizationId = this.innerTokenHandler.organizationId;
			}

			// Token: 0x06000595 RID: 1429 RVA: 0x0002C2ED File Offset: 0x0002A4ED
			protected override void ResolveOAuthApplication()
			{
				this.oauthApplication = this.innerTokenHandler.oauthApplication;
			}

			// Token: 0x06000596 RID: 1430 RVA: 0x0002C300 File Offset: 0x0002A500
			protected override void ResolveUserInfo()
			{
				this.oauthActAsUser = OAuthActAsUser.CreateFromOuterToken(this, this.organizationId, this.token, this.innerTokenHandler.PartnerApplication.AcceptSecurityIdentifierInformation);
			}

			// Token: 0x06000597 RID: 1431 RVA: 0x0002C32C File Offset: 0x0002A52C
			protected override void PostValidateToken()
			{
				if (!OAuthCommon.IsRealmMatch(this.realmFromIssuer, this.innerTokenHandler.realmFromIssuer))
				{
					OrganizationId other = base.ResolveOrganizationByRealm(this.realmFromIssuer);
					base.ThrowIfFalse(this.innerTokenHandler.organizationId.Equals(other), OAuthErrors.InvalidRealmFromOuterTokenIssuer, new object[]
					{
						this.realmFromIssuer
					});
				}
			}

			// Token: 0x06000598 RID: 1432 RVA: 0x0002C38C File Offset: 0x0002A58C
			public override OAuthPreAuthIdentity GetPreAuthIdentity()
			{
				string a;
				string lookupValue;
				if (OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Nii, out a) && string.Equals(a, Constants.NiiClaimValues.BusinessLiveId, StringComparison.OrdinalIgnoreCase))
				{
					if (OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Smtp, out lookupValue))
					{
						return new OAuthPreAuthIdentity(OAuthPreAuthType.Smtp, null, lookupValue);
					}
					if (OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Upn, out lookupValue))
					{
						return new OAuthPreAuthIdentity(OAuthPreAuthType.WindowsLiveID, null, lookupValue);
					}
				}
				else if (OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Smtp, out lookupValue))
				{
					return new OAuthPreAuthIdentity(OAuthPreAuthType.Smtp, null, lookupValue);
				}
				return null;
			}

			// Token: 0x040005AA RID: 1450
			private static readonly TokenValidationParameters defaultTokenValidationParameters = new TokenValidationParameters();

			// Token: 0x040005AB RID: 1451
			private readonly OAuthTokenHandler.ActorTokenHandler innerTokenHandler;
		}

		// Token: 0x020000A3 RID: 163
		public sealed class ActorTokenHandler : OAuthTokenHandler.S2STokenHandlerBase
		{
			// Token: 0x0600059A RID: 1434 RVA: 0x0002C41F File Offset: 0x0002A61F
			public ActorTokenHandler(JwtSecurityToken token, Uri targetUri) : base(token, targetUri)
			{
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x0600059B RID: 1435 RVA: 0x0002C429 File Offset: 0x0002A629
			public PartnerApplication PartnerApplication
			{
				get
				{
					return this.partnerApplication;
				}
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x0600059C RID: 1436 RVA: 0x0002C431 File Offset: 0x0002A631
			public override string TokenCategory
			{
				get
				{
					return Constants.TokenCategories.S2SAppOnlyToken;
				}
			}

			// Token: 0x0600059D RID: 1437 RVA: 0x0002C438 File Offset: 0x0002A638
			protected override void ResolveOAuthApplication()
			{
				string claimValue = base.GetClaimValue(this.token, Constants.ClaimTypes.NameIdentifier);
				string text = null;
				string text2 = null;
				base.ThrowIfFalse(base.TryParseNameId(claimValue, out text, out text2), OAuthErrors.InvalidNameIdFormat, new object[]
				{
					claimValue
				});
				if (this.matchedIssuer.IssuerMetadata.Kind == IssuerKind.PartnerApp)
				{
					this.partnerApplication = this.matchedIssuer.PartnerApplication;
					string id = this.matchedIssuer.IssuerMetadata.Id;
					base.ThrowIfFalse(OAuthCommon.IsIdMatch(text, id), OAuthErrors.UnexpectedAppIdInNameId, new object[]
					{
						id,
						text
					});
					base.ThrowIfFalse(OAuthCommon.IsRealmMatch(text2, this.realmFromIssuer), OAuthErrors.UnexpectedRealmInNameId, new object[]
					{
						this.realmFromIssuer,
						text2
					});
				}
				else if (this.matchedIssuer.IssuerMetadata.Kind == IssuerKind.ACS)
				{
					foreach (PartnerApplication partnerApplication in this.localConfiguration.PartnerApplications)
					{
						if (partnerApplication.UseAuthServer && OAuthCommon.IsIdMatch(partnerApplication.ApplicationIdentifier, text))
						{
							if (!OAuthCommon.IsRealmEmpty(partnerApplication.Realm))
							{
								if (OAuthCommon.IsRealmMatch(text2, partnerApplication.Realm))
								{
									this.partnerApplication = partnerApplication;
									break;
								}
							}
							else if (OAuthCommon.IsRealmMatch(text2, this.realmFromIssuer))
							{
								this.partnerApplication = partnerApplication;
								break;
							}
						}
					}
				}
				if (this.partnerApplication == null && AuthCommon.IsMultiTenancyEnabled)
				{
					PartnerApplication partnerApplication2 = TenantLevelPartnerApplicationCache.Singleton.Get(this.organizationId, text);
					if (partnerApplication2 != null)
					{
						if (partnerApplication2.UseAuthServer && OAuthCommon.IsRealmEmpty(partnerApplication2.Realm) && OAuthCommon.IsRealmMatch(text2, this.realmFromIssuer))
						{
							this.partnerApplication = partnerApplication2;
						}
						else
						{
							ExTraceGlobals.OAuthTracer.TraceDebug((long)this.GetHashCode(), "[OAuthTokenHandler:CustomValidateActorToken] skip the tenant level PA {0}, where UseAuthServer is {1}, realm {2}; the realm from issuer is {3}", new object[]
							{
								partnerApplication2.Name,
								partnerApplication2.UseAuthServer,
								partnerApplication2.Realm,
								this.realmFromIssuer
							});
						}
					}
				}
				base.ThrowIfFalse(this.partnerApplication != null, OAuthErrors.NoMatchingPartnerAppFound, new object[]
				{
					claimValue
				});
				this.oauthApplication = new OAuthApplication(this.partnerApplication);
				this.oauthApplication.IsFromSameOrgExchange = new bool?(OAuthCommon.IsIdMatch(this.appIdFromAudience, text) && OAuthCommon.IsRealmMatch(this.realmFromAudience, text2));
			}

			// Token: 0x0600059E RID: 1438 RVA: 0x0002C6AF File Offset: 0x0002A8AF
			protected override void ResolveUserInfo()
			{
			}

			// Token: 0x0600059F RID: 1439 RVA: 0x0002C6B1 File Offset: 0x0002A8B1
			protected override void PostValidateToken()
			{
				if (this.matchedIssuer.IssuerMetadata.Kind == IssuerKind.ACS)
				{
					base.ThrowIfFalse(OAuthCommon.IsRealmMatch(this.realmFromAudience, this.realmFromIssuer), OAuthErrors.MismatchedRealmBetweenAudienceAndIssuer, new object[0]);
				}
			}

			// Token: 0x060005A0 RID: 1440 RVA: 0x0002C6E8 File Offset: 0x0002A8E8
			public override OAuthPreAuthIdentity GetPreAuthIdentity()
			{
				string claimValue = base.GetClaimValue(this.token, Constants.ClaimTypes.NameIdentifier);
				string text = null;
				string text2 = null;
				base.ThrowIfFalse(base.TryParseNameId(claimValue, out text, out text2), OAuthErrors.InvalidNameIdFormat, new object[]
				{
					claimValue
				});
				return new OAuthPreAuthIdentity(OAuthPreAuthType.OrganizationOnly, base.ResolveOrganizationByRealm(text2), text2);
			}

			// Token: 0x040005AC RID: 1452
			private PartnerApplication partnerApplication;
		}

		// Token: 0x020000A4 RID: 164
		internal sealed class CallbackTokenHandler : OAuthTokenHandler.S2STokenHandlerBase
		{
			// Token: 0x060005A1 RID: 1441 RVA: 0x0002C73B File Offset: 0x0002A93B
			public CallbackTokenHandler(JwtSecurityToken token, Uri targetUri, string appContextString) : base(token, targetUri)
			{
				this.appContextString = appContextString;
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0002C74C File Offset: 0x0002A94C
			public override IEnumerable<string> ClaimTypesForLogging
			{
				get
				{
					return OAuthTokenHandler.CallbackTokenHandler.claimTypesForLogging;
				}
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0002C753 File Offset: 0x0002A953
			public override string TokenCategory
			{
				get
				{
					return Constants.TokenCategories.CallbackToken;
				}
			}

			// Token: 0x060005A4 RID: 1444 RVA: 0x0002C75C File Offset: 0x0002A95C
			protected override void PreValidateToken()
			{
				base.PreValidateToken();
				this.claimsInsideAppContext = this.ExtractClaimsFromAppContextClaim(this.appContextString);
				string a;
				base.ThrowIfFalse(this.claimsInsideAppContext.TryGetValue(Constants.ClaimTypes.MsExchCallback, out a), OAuthErrors.CallbackClaimNotFound, new object[0]);
				base.ThrowIfFalse(string.Equals(a, Constants.ClaimValues.MsExtensionV1, StringComparison.OrdinalIgnoreCase) || string.Equals(a, Constants.ClaimValues.MsOabDownloadV1, StringComparison.OrdinalIgnoreCase), OAuthErrors.InvalidCallbackClaimValue, new object[0]);
				string text = OAuthCommon.TryGetClaimValue(this.token.Payload, Constants.ClaimTypes.NameIdentifier);
				if (string.IsNullOrEmpty(text))
				{
					this.extensionId = base.GetClaimValue(this.token, Constants.ClaimTypes.AppCtxSender);
				}
				else
				{
					string text2 = null;
					string text3 = null;
					base.ThrowIfFalse(base.TryParseNameId(text, out text2, out text3), OAuthErrors.InvalidNameIdFormat, new object[]
					{
						text
					});
					this.extensionId = text2;
				}
				this.claimsInsideAppContext.TryGetValue(Constants.ClaimTypes.Scope, out this.scope);
			}

			// Token: 0x060005A5 RID: 1445 RVA: 0x0002C850 File Offset: 0x0002AA50
			protected override void ValidateAudience()
			{
				base.ValidateAudience();
				string text;
				if (!this.isUnitTestOnlyPath && this.claimsInsideAppContext.TryGetValue(Constants.ClaimTypes.MsExchProtocol, out text))
				{
					base.ThrowIfFalse(this.targetUri.LocalPath.StartsWith("/" + text, StringComparison.OrdinalIgnoreCase), OAuthErrors.InvalidCallbackTokenScope, new object[]
					{
						text
					});
				}
			}

			// Token: 0x060005A6 RID: 1446 RVA: 0x0002C8B4 File Offset: 0x0002AAB4
			protected override void InternalResolveTrustedIssuer(IssuerMetadata issuerMetadataFromToken)
			{
				base.ThrowIfFalse(OAuthCommon.IsIdMatch(issuerMetadataFromToken.Id, this.localConfiguration.ApplicationId), OAuthErrors.InvalidCallbackTokenIssuer, new object[]
				{
					this.token.Issuer
				});
				this.matchedIssuer = TrustedIssuer.CreateFromExchangeCallback(this.localConfiguration, issuerMetadataFromToken.Realm);
			}

			// Token: 0x060005A7 RID: 1447 RVA: 0x0002C90F File Offset: 0x0002AB0F
			protected override void ResolveOAuthApplication()
			{
				this.oauthApplication = new OAuthApplication(new OfficeExtensionInfo(this.extensionId, this.scope));
			}

			// Token: 0x060005A8 RID: 1448 RVA: 0x0002C92D File Offset: 0x0002AB2D
			protected override void ResolveUserInfo()
			{
				this.oauthActAsUser = OAuthActAsUser.CreateFromAppContext(this, this.organizationId, this.claimsInsideAppContext, false);
			}

			// Token: 0x060005A9 RID: 1449 RVA: 0x0002C948 File Offset: 0x0002AB48
			protected override void PostValidateToken()
			{
				base.PostValidateToken();
				LocalTokenIssuerMetadata localTokenIssuerMetadata = new LocalTokenIssuerMetadata(this.localConfiguration.ApplicationId, OAuthConfigHelper.GetOrganizationRealm(this.organizationId));
				base.ThrowIfFalse(string.Equals(this.token.Issuer, localTokenIssuerMetadata.GetIssuer(), StringComparison.OrdinalIgnoreCase), OAuthErrors.InvalidCallbackTokenIssuer, new object[]
				{
					this.token.Issuer
				});
			}

			// Token: 0x060005AA RID: 1450 RVA: 0x0002C9B0 File Offset: 0x0002ABB0
			private Dictionary<string, string> ExtractClaimsFromAppContextClaim(string appContextValue)
			{
				Exception ex = null;
				Dictionary<string, string> result = null;
				try
				{
					result = appContextValue.DeserializeFromJson<Dictionary<string, string>>();
				}
				catch (ArgumentException ex2)
				{
					ex = ex2;
				}
				catch (InvalidOperationException ex3)
				{
					ex = ex3;
				}
				if (ex != null)
				{
					ExTraceGlobals.OAuthTracer.TraceWarning<string, Exception>((long)this.GetHashCode(), "[ValidateExchangeCallbackToken] unable to deserialize appctx with {0}, exception {1}", appContextValue, ex);
					base.Throw(OAuthErrors.ExtensionInvalidAppCtxFormat, new object[]
					{
						appContextValue
					}, ex, null);
				}
				return result;
			}

			// Token: 0x060005AB RID: 1451 RVA: 0x0002CA28 File Offset: 0x0002AC28
			public override OAuthPreAuthIdentity GetPreAuthIdentity()
			{
				this.claimsInsideAppContext = this.ExtractClaimsFromAppContextClaim(this.appContextString);
				string lookupValue;
				if (this.claimsInsideAppContext.TryGetValue(Constants.ClaimTypes.Smtp, out lookupValue))
				{
					return new OAuthPreAuthIdentity(OAuthPreAuthType.Smtp, null, lookupValue);
				}
				return null;
			}

			// Token: 0x040005AD RID: 1453
			private readonly string appContextString;

			// Token: 0x040005AE RID: 1454
			private Dictionary<string, string> claimsInsideAppContext;

			// Token: 0x040005AF RID: 1455
			private string extensionId;

			// Token: 0x040005B0 RID: 1456
			private string scope;

			// Token: 0x040005B1 RID: 1457
			private static readonly string[] claimTypesForLogging = new string[]
			{
				Constants.ClaimTypes.AppCtxSender
			};
		}

		// Token: 0x020000A5 RID: 165
		private sealed class Dummy : OAuthTokenHandler
		{
			// Token: 0x060005AD RID: 1453 RVA: 0x0002CA8A File Offset: 0x0002AC8A
			public Dummy() : base(null, null)
			{
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x060005AE RID: 1454 RVA: 0x0002CA94 File Offset: 0x0002AC94
			public static OAuthTokenHandler Instance
			{
				get
				{
					return OAuthTokenHandler.Dummy.instance;
				}
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x060005AF RID: 1455 RVA: 0x0002CA9B File Offset: 0x0002AC9B
			public override string TokenCategory
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x060005B0 RID: 1456 RVA: 0x0002CAA2 File Offset: 0x0002ACA2
			protected override void ValidateAudience()
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005B1 RID: 1457 RVA: 0x0002CAA9 File Offset: 0x0002ACA9
			protected override void FindMatchedTrustedIssuer(string issuer)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005B2 RID: 1458 RVA: 0x0002CAB0 File Offset: 0x0002ACB0
			protected override void ResolveOAuthApplication()
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005B3 RID: 1459 RVA: 0x0002CAB7 File Offset: 0x0002ACB7
			protected override void ResolveUserInfo()
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005B4 RID: 1460 RVA: 0x0002CABE File Offset: 0x0002ACBE
			public override OAuthPreAuthIdentity GetPreAuthIdentity()
			{
				throw new NotImplementedException();
			}

			// Token: 0x040005B2 RID: 1458
			private static readonly OAuthTokenHandler.Dummy instance = new OAuthTokenHandler.Dummy();
		}

		// Token: 0x020000A6 RID: 166
		public abstract class V1ProfileTokenHandlerBase : OAuthTokenHandler
		{
			// Token: 0x060005B6 RID: 1462 RVA: 0x0002CAD1 File Offset: 0x0002ACD1
			public V1ProfileTokenHandlerBase(JwtSecurityToken token, Uri targetUri) : base(token, targetUri)
			{
				this.hasTenantId = OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Tid, out this.tenantId);
			}

			// Token: 0x060005B7 RID: 1463 RVA: 0x0002CAF8 File Offset: 0x0002ACF8
			protected override void ValidateAudience()
			{
				if (!this.isUnitTestOnlyPath)
				{
					string audience = this.token.Audience;
					Uri targetUri = this.targetUri;
					targetUri.ToString();
					Uri uri;
					base.ThrowIfFalse(Uri.TryCreate(audience, UriKind.Absolute, out uri), OAuthErrors.InvalidAudience, new object[]
					{
						audience
					});
					string leftPart = uri.GetLeftPart(UriPartial.Authority);
					string leftPart2 = targetUri.GetLeftPart(UriPartial.Authority);
					base.ThrowIfFalse(string.Equals(leftPart, leftPart2, StringComparison.OrdinalIgnoreCase), OAuthErrors.UnexpectedHostNameInAudience, new object[]
					{
						leftPart,
						leftPart2
					});
					base.ThrowIfFalse(leftPart2.StartsWith(leftPart, StringComparison.OrdinalIgnoreCase), OAuthErrors.WrongAudience, new object[]
					{
						leftPart,
						leftPart2
					});
				}
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x0002CBB0 File Offset: 0x0002ADB0
			protected override void FindMatchedTrustedIssuer(string issuer)
			{
				base.ThrowIfFalse(Uri.IsWellFormedUriString(issuer, UriKind.Absolute), OAuthErrors.InvalidIssuerFormat, new object[]
				{
					issuer
				});
				foreach (TrustedIssuer trustedIssuer in this.localConfiguration.TrustedIssuers)
				{
					if (trustedIssuer.IssuerMetadata.Kind == IssuerKind.AzureAD || trustedIssuer.IssuerMetadata.Kind == IssuerKind.ADFS)
					{
						string text = trustedIssuer.IssuerMetadata.Id;
						if (this.hasTenantId)
						{
							text = text.Replace(Constants.AzureADCommonEntityIdHint, this.tenantId);
						}
						if (OAuthCommon.IsIdMatch(text, issuer))
						{
							this.matchedIssuer = trustedIssuer;
							return;
						}
					}
				}
			}

			// Token: 0x060005B9 RID: 1465 RVA: 0x0002CC54 File Offset: 0x0002AE54
			protected override void ResolveOrganizationId()
			{
				if (this.hasTenantId)
				{
					this.realmFromAudience = this.tenantId;
					base.ResolveOrganizationId();
					return;
				}
				if (AuthCommon.IsMultiTenancyEnabled)
				{
					base.ThrowIfFalse(false, OAuthErrors.MissingTenantIdClaim, new object[0]);
					return;
				}
				this.organizationId = OrganizationId.ForestWideOrgId;
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x0002CCA4 File Offset: 0x0002AEA4
			protected override void ResolveOAuthApplication()
			{
				V1ProfileAppInfo v1ProfileAppInfo = new V1ProfileAppInfo(this, this.token);
				this.ValidateV1AppInfo(v1ProfileAppInfo);
				this.oauthApplication = new OAuthApplication(v1ProfileAppInfo);
			}

			// Token: 0x060005BB RID: 1467 RVA: 0x0002CCD4 File Offset: 0x0002AED4
			protected virtual void ValidateV1AppInfo(V1ProfileAppInfo appInfo)
			{
				if (this.isUnitTestOnlyPath)
				{
					return;
				}
				string scope = appInfo.Scope;
				if (!string.IsNullOrEmpty(scope))
				{
					string[] array = OAuthGrant.ExtractKnownGrants(scope);
					if (array.Length > 0)
					{
						appInfo.Scope = string.Join(" ", OAuthAppPoolLevelPolicy.Instance.GetV1AppScope(array));
					}
				}
				string role = appInfo.Role;
				if (!string.IsNullOrEmpty(role))
				{
					string[] array2 = OAuthGrant.ExtractKnownGrantsFromRole(role);
					if (array2.Length > 0)
					{
						appInfo.Role = string.Join(" ", OAuthAppPoolLevelPolicy.Instance.GetV1AppRole(array2));
					}
				}
			}

			// Token: 0x040005B3 RID: 1459
			protected readonly bool hasTenantId;

			// Token: 0x040005B4 RID: 1460
			protected readonly string tenantId;
		}

		// Token: 0x020000A7 RID: 167
		internal sealed class V1CallbackTokenHandler : OAuthTokenHandler.V1ProfileTokenHandlerBase
		{
			// Token: 0x060005BC RID: 1468 RVA: 0x0002CD58 File Offset: 0x0002AF58
			public V1CallbackTokenHandler(JwtSecurityToken token, Uri targetUri) : base(token, targetUri)
			{
			}

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x060005BD RID: 1469 RVA: 0x0002CD62 File Offset: 0x0002AF62
			public override IEnumerable<string> ClaimTypesForLogging
			{
				get
				{
					return OAuthTokenHandler.V1CallbackTokenHandler.claimTypesForLogging;
				}
			}

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x060005BE RID: 1470 RVA: 0x0002CD69 File Offset: 0x0002AF69
			public override string TokenCategory
			{
				get
				{
					return Constants.TokenCategories.V1AppActAsToken;
				}
			}

			// Token: 0x060005BF RID: 1471 RVA: 0x0002CD70 File Offset: 0x0002AF70
			protected override void PreValidateToken()
			{
				base.PreValidateToken();
				string a;
				base.ThrowIfFalse(OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.MsExchCallback, out a), OAuthErrors.CallbackClaimNotFound, new object[0]);
				base.ThrowIfFalse(string.Equals(a, Constants.ClaimValues.ExCallbackV1, StringComparison.OrdinalIgnoreCase), OAuthErrors.InvalidCallbackClaimValue, new object[0]);
			}

			// Token: 0x060005C0 RID: 1472 RVA: 0x0002CDC4 File Offset: 0x0002AFC4
			protected override void ValidateAudience()
			{
				base.ValidateAudience();
				string text;
				if (!this.isUnitTestOnlyPath && OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.MsExchProtocol, out text))
				{
					base.ThrowIfFalse(this.targetUri.LocalPath.StartsWith("/" + text, StringComparison.OrdinalIgnoreCase), OAuthErrors.InvalidCallbackTokenScope, new object[]
					{
						text
					});
				}
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x0002CE28 File Offset: 0x0002B028
			protected override void FindMatchedTrustedIssuer(string issuer)
			{
				IssuerMetadata issuerMetadata = null;
				base.ThrowIfFalse(IssuerMetadata.TryParse(issuer, out issuerMetadata), OAuthErrors.InvalidIssuerFormat, new object[]
				{
					issuer
				});
				this.realmFromIssuer = issuerMetadata.Realm;
				base.ThrowIfFalse(OAuthCommon.IsIdMatch(issuerMetadata.Id, this.localConfiguration.ApplicationId), OAuthErrors.InvalidCallbackTokenIssuer, new object[]
				{
					this.token.Issuer
				});
				this.matchedIssuer = TrustedIssuer.CreateFromExchangeCallback(this.localConfiguration, issuerMetadata.Realm);
			}

			// Token: 0x060005C2 RID: 1474 RVA: 0x0002CEB0 File Offset: 0x0002B0B0
			protected override void ResolveUserInfo()
			{
				string externalDirectoryObjectId;
				if (this.hasTenantId && OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Oid, out externalDirectoryObjectId))
				{
					this.oauthActAsUser = OAuthActAsUser.CreateFromExternalDirectoryObjectId(this.organizationId, externalDirectoryObjectId);
					return;
				}
				string text;
				if (OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.PrimarySid, out text))
				{
					SecurityIdentifier securityIdentifier = null;
					Exception ex = null;
					try
					{
						securityIdentifier = new SecurityIdentifier(text);
					}
					catch (ArgumentException ex2)
					{
						ex = ex2;
					}
					catch (SystemException ex3)
					{
						ex = ex3;
					}
					base.ThrowIfFalse(ex == null, OAuthErrors.InvalidSidValue, new object[]
					{
						text
					}, ex, null);
					this.oauthActAsUser = OAuthActAsUser.CreateFromPrimarySid(this.organizationId, securityIdentifier);
					return;
				}
				base.Throw(OAuthErrors.NoUserClaimsFound, null, null, null);
			}

			// Token: 0x060005C3 RID: 1475 RVA: 0x0002CF78 File Offset: 0x0002B178
			protected override void ValidateV1AppInfo(V1ProfileAppInfo appInfo)
			{
				base.ValidateV1AppInfo(appInfo);
				string scope = appInfo.Scope;
				base.ThrowIfFalse(!string.IsNullOrEmpty(scope), OAuthErrors.InvalidClaimValueFound, new object[]
				{
					Constants.ClaimTypes.Scp,
					scope
				});
			}

			// Token: 0x060005C4 RID: 1476 RVA: 0x0002CFBC File Offset: 0x0002B1BC
			protected override void PostValidateToken()
			{
				base.PostValidateToken();
				LocalTokenIssuerMetadata localTokenIssuerMetadata = new LocalTokenIssuerMetadata(this.localConfiguration.ApplicationId, OAuthConfigHelper.GetOrganizationRealm(this.organizationId));
				base.ThrowIfFalse(string.Equals(this.token.Issuer, localTokenIssuerMetadata.GetIssuer(), StringComparison.OrdinalIgnoreCase), OAuthErrors.InvalidCallbackTokenIssuer, new object[]
				{
					this.token.Issuer
				});
			}

			// Token: 0x060005C5 RID: 1477 RVA: 0x0002D024 File Offset: 0x0002B224
			public override OAuthPreAuthIdentity GetPreAuthIdentity()
			{
				string lookupValue;
				if (this.hasTenantId && OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Oid, out lookupValue))
				{
					return new OAuthPreAuthIdentity(OAuthPreAuthType.ExternalDirectoryObjectId, base.ResolveOrganizationByRealm(this.tenantId), lookupValue);
				}
				return null;
			}

			// Token: 0x040005B5 RID: 1461
			private static readonly string[] claimTypesForLogging = new string[]
			{
				Constants.ClaimTypes.Oid,
				Constants.ClaimTypes.PrimarySid,
				Constants.ClaimTypes.Upn,
				Constants.ClaimTypes.Scp
			};
		}

		// Token: 0x020000A8 RID: 168
		public sealed class V1ProfileAppActAsTokenHandler : OAuthTokenHandler.V1ProfileTokenHandlerBase
		{
			// Token: 0x060005C7 RID: 1479 RVA: 0x0002D09E File Offset: 0x0002B29E
			public V1ProfileAppActAsTokenHandler(JwtSecurityToken token, Uri targetUri) : base(token, targetUri)
			{
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0002D0A8 File Offset: 0x0002B2A8
			public override string TokenCategory
			{
				get
				{
					return Constants.TokenCategories.V1AppActAsToken;
				}
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0002D0AF File Offset: 0x0002B2AF
			public override IEnumerable<string> ClaimTypesForLogging
			{
				get
				{
					return OAuthTokenHandler.V1ProfileAppActAsTokenHandler.claimTypesForLogging;
				}
			}

			// Token: 0x060005CA RID: 1482 RVA: 0x0002D0B8 File Offset: 0x0002B2B8
			protected override void ResolveUserInfo()
			{
				if (!AuthCommon.IsMultiTenancyEnabled)
				{
					string text = null;
					if (OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.PrimarySid, out text) || OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.OnPremSid, out text))
					{
						SecurityIdentifier securityIdentifier = null;
						Exception ex = null;
						try
						{
							securityIdentifier = new SecurityIdentifier(text);
						}
						catch (ArgumentException ex2)
						{
							ex = ex2;
						}
						catch (SystemException ex3)
						{
							ex = ex3;
						}
						base.ThrowIfFalse(ex == null, OAuthErrors.InvalidSidValue, new object[]
						{
							text
						}, ex, null);
						this.oauthActAsUser = OAuthActAsUser.CreateFromPrimarySid(this.organizationId, securityIdentifier);
						return;
					}
				}
				else
				{
					string externalDirectoryObjectId = null;
					if (OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Oid, out externalDirectoryObjectId))
					{
						this.oauthActAsUser = OAuthActAsUser.CreateFromExternalDirectoryObjectId(this.organizationId, externalDirectoryObjectId);
						return;
					}
				}
				base.Throw(OAuthErrors.NoUserClaimsFound, null, null, null);
			}

			// Token: 0x060005CB RID: 1483 RVA: 0x0002D198 File Offset: 0x0002B398
			protected override void ValidateV1AppInfo(V1ProfileAppInfo appInfo)
			{
				base.ValidateV1AppInfo(appInfo);
				string scope = appInfo.Scope;
				base.ThrowIfFalse(!string.IsNullOrEmpty(scope), OAuthErrors.InvalidClaimValueFound, new object[]
				{
					Constants.ClaimTypes.Scp,
					scope
				});
			}

			// Token: 0x060005CC RID: 1484 RVA: 0x0002D1DC File Offset: 0x0002B3DC
			public override OAuthPreAuthIdentity GetPreAuthIdentity()
			{
				string lookupValue;
				if (AuthCommon.IsMultiTenancyEnabled && this.hasTenantId && OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Oid, out lookupValue))
				{
					return new OAuthPreAuthIdentity(OAuthPreAuthType.ExternalDirectoryObjectId, base.ResolveOrganizationByRealm(this.tenantId), lookupValue);
				}
				return null;
			}

			// Token: 0x040005B6 RID: 1462
			private static readonly string[] claimTypesForLogging = new string[]
			{
				Constants.ClaimTypes.Oid,
				Constants.ClaimTypes.PrimarySid,
				"upn",
				"acr",
				"appidacr",
				"amr",
				"scp"
			};
		}

		// Token: 0x020000A9 RID: 169
		public sealed class V1ProfileAppTokenHandler : OAuthTokenHandler.V1ProfileTokenHandlerBase
		{
			// Token: 0x060005CE RID: 1486 RVA: 0x0002D276 File Offset: 0x0002B476
			public V1ProfileAppTokenHandler(JwtSecurityToken token, Uri targetUri) : base(token, targetUri)
			{
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x060005CF RID: 1487 RVA: 0x0002D280 File Offset: 0x0002B480
			public override string TokenCategory
			{
				get
				{
					return Constants.TokenCategories.V1AppOnlyToken;
				}
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0002D287 File Offset: 0x0002B487
			public override IEnumerable<string> ClaimTypesForLogging
			{
				get
				{
					return OAuthTokenHandler.V1ProfileAppTokenHandler.claimTypesForLogging;
				}
			}

			// Token: 0x060005D1 RID: 1489 RVA: 0x0002D28E File Offset: 0x0002B48E
			protected override void ResolveUserInfo()
			{
			}

			// Token: 0x060005D2 RID: 1490 RVA: 0x0002D290 File Offset: 0x0002B490
			protected override void ValidateV1AppInfo(V1ProfileAppInfo appInfo)
			{
				base.ValidateV1AppInfo(appInfo);
				string role = appInfo.Role;
				base.ThrowIfFalse(!string.IsNullOrEmpty(role), OAuthErrors.InvalidClaimValueFound, new object[]
				{
					Constants.ClaimTypes.Roles,
					role
				});
			}

			// Token: 0x060005D3 RID: 1491 RVA: 0x0002D2D3 File Offset: 0x0002B4D3
			public override OAuthPreAuthIdentity GetPreAuthIdentity()
			{
				if (this.hasTenantId)
				{
					return new OAuthPreAuthIdentity(OAuthPreAuthType.OrganizationOnly, base.ResolveOrganizationByRealm(this.tenantId), this.tenantId);
				}
				return null;
			}

			// Token: 0x040005B7 RID: 1463
			private static readonly string[] claimTypesForLogging = new string[]
			{
				Constants.ClaimTypes.Oid
			};
		}

		// Token: 0x020000AA RID: 170
		public abstract class V1ProfileExchangeSelfIssuedTokenHandlerBase : OAuthTokenHandler.V1ProfileTokenHandlerBase
		{
			// Token: 0x060005D5 RID: 1493 RVA: 0x0002D31A File Offset: 0x0002B51A
			public V1ProfileExchangeSelfIssuedTokenHandlerBase(JwtSecurityToken token, Uri targetUri) : base(token, targetUri)
			{
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0002D324 File Offset: 0x0002B524
			public bool IsConsumerMailbox
			{
				get
				{
					if (this.targetOrg == null)
					{
						this.SetTargetOrg();
					}
					return OAuthCommon.IsIdMatch(this.targetOrg, Constants.ConsumerMailboxIdentifier);
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0002D344 File Offset: 0x0002B544
			public override string TokenCategory
			{
				get
				{
					return Constants.TokenCategories.V1ExchangeSelfIssuedToken;
				}
			}

			// Token: 0x060005D8 RID: 1496 RVA: 0x0002D34B File Offset: 0x0002B54B
			protected override void ValidateAudience()
			{
				base.ValidateAudience();
				this.SetTargetOrg();
			}

			// Token: 0x060005D9 RID: 1497 RVA: 0x0002D35C File Offset: 0x0002B55C
			protected override void FindMatchedTrustedIssuer(string issuer)
			{
				base.ThrowIfFalse(Uri.IsWellFormedUriString(issuer, UriKind.Absolute), OAuthErrors.InvalidIssuerFormat, new object[]
				{
					issuer
				});
				foreach (TrustedIssuer trustedIssuer in this.localConfiguration.TrustedIssuers)
				{
					if (trustedIssuer.IssuerMetadata.Kind == IssuerKind.PartnerApp)
					{
						string id = trustedIssuer.IssuerMetadata.Id;
						if (OAuthCommon.IsIdMatch(id, issuer))
						{
							this.matchedIssuer = trustedIssuer;
							return;
						}
					}
				}
			}

			// Token: 0x060005DA RID: 1498 RVA: 0x0002D3D8 File Offset: 0x0002B5D8
			protected override void ResolveOrganizationId()
			{
				if (this.IsConsumerMailbox)
				{
					base.ThrowIfFalse(!this.hasTenantId, OAuthErrors.TenantIdClaimShouldNotBeSet, new object[0]);
					this.organizationId = base.ResolveOrganizationByRealm(TemplateTenantConfiguration.TemplateTenantExternalDirectoryOrganizationId);
					return;
				}
				base.ResolveOrganizationByRealm(this.targetOrg);
			}

			// Token: 0x060005DB RID: 1499 RVA: 0x0002D428 File Offset: 0x0002B628
			protected override void ResolveOAuthApplication()
			{
				V1ProfileAppInfo v1ProfileAppInfo = new V1ProfileAppInfo(this, this.token);
				base.ThrowIfFalse(OAuthCommon.IsIdMatch(this.matchedIssuer.PartnerApplication.ApplicationIdentifier, v1ProfileAppInfo.AppId), OAuthErrors.NoMatchingPartnerAppFound, new object[]
				{
					v1ProfileAppInfo.AppId
				});
				if (this.IsConsumerMailbox)
				{
					base.ValidateV1AppInfo(v1ProfileAppInfo);
					if (!string.IsNullOrEmpty(v1ProfileAppInfo.Scope))
					{
						string[] second = v1ProfileAppInfo.Scope.Split(OAuthTokenHandler.V1ProfileExchangeSelfIssuedTokenHandlerBase.delimiter);
						string[] actAsPermissions = this.matchedIssuer.PartnerApplication.ActAsPermissions;
						IEnumerable<string> values = actAsPermissions.Intersect(second);
						v1ProfileAppInfo.Scope = string.Join(" ", values);
					}
					if (!string.IsNullOrEmpty(v1ProfileAppInfo.Role))
					{
						string[] second2 = v1ProfileAppInfo.Role.Split(OAuthTokenHandler.V1ProfileExchangeSelfIssuedTokenHandlerBase.delimiter);
						string[] appOnlyPermissions = this.matchedIssuer.PartnerApplication.AppOnlyPermissions;
						IEnumerable<string> values2 = appOnlyPermissions.Intersect(second2);
						v1ProfileAppInfo.Role = string.Join(" ", values2);
					}
				}
				this.oauthApplication = new OAuthApplication(v1ProfileAppInfo, this.matchedIssuer.PartnerApplication);
			}

			// Token: 0x060005DC RID: 1500 RVA: 0x0002D53C File Offset: 0x0002B73C
			private void SetTargetOrg()
			{
				Uri uri;
				if (Uri.TryCreate(this.token.Audience, UriKind.Absolute, out uri))
				{
					string text = uri.AbsolutePath.Substring(1);
					this.targetOrg = (text.EndsWith("/") ? text.Substring(0, text.Length - 1) : text);
					return;
				}
				this.targetOrg = "";
			}

			// Token: 0x040005B8 RID: 1464
			private static char[] delimiter = new char[]
			{
				' '
			};

			// Token: 0x040005B9 RID: 1465
			private string targetOrg;
		}

		// Token: 0x020000AB RID: 171
		public sealed class V1ProfileExchangeSelfIssuedActAsTokenHandler : OAuthTokenHandler.V1ProfileExchangeSelfIssuedTokenHandlerBase
		{
			// Token: 0x060005DE RID: 1502 RVA: 0x0002D5BB File Offset: 0x0002B7BB
			public V1ProfileExchangeSelfIssuedActAsTokenHandler(JwtSecurityToken token, Uri targetUri) : base(token, targetUri)
			{
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x060005DF RID: 1503 RVA: 0x0002D5C5 File Offset: 0x0002B7C5
			public override string TokenCategory
			{
				get
				{
					return Constants.TokenCategories.V1ExchangeSelfIssuedToken;
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0002D5CC File Offset: 0x0002B7CC
			public override IEnumerable<string> ClaimTypesForLogging
			{
				get
				{
					return OAuthTokenHandler.V1ProfileExchangeSelfIssuedActAsTokenHandler.claimTypesForLogging;
				}
			}

			// Token: 0x060005E1 RID: 1505 RVA: 0x0002D5D4 File Offset: 0x0002B7D4
			protected override void ResolveOAuthApplication()
			{
				base.ResolveOAuthApplication();
				if (string.IsNullOrEmpty(this.oauthApplication.V1ProfileApp.Scope) && this.oauthApplication.PartnerApplication.LinkedAccount == null)
				{
					base.ThrowIfFalse(false, OAuthErrors.NoAuthorizationValuePresent, new object[]
					{
						Constants.ClaimValues.ExchangeSelfIssuedVersion1,
						this.oauthApplication.V1ProfileApp.AppId
					});
				}
			}

			// Token: 0x060005E2 RID: 1506 RVA: 0x0002D640 File Offset: 0x0002B840
			protected override void ResolveUserInfo()
			{
				string text = null;
				string text2 = null;
				string text3 = null;
				OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Puid, out text);
				OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Smtp, out text2);
				OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Oid, out text3);
				if (base.IsConsumerMailbox)
				{
					base.ThrowIfFalse(!string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(text2), OAuthErrors.NoSmtpOrPuidClaimFound, new object[]
					{
						this.oauthApplication.Id
					});
				}
				if (!string.IsNullOrEmpty(text3))
				{
					this.oauthActAsUser = OAuthActAsUser.CreateFromExternalDirectoryObjectId(this.organizationId, text3);
					return;
				}
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
				{
					this.oauthActAsUser = OAuthActAsUser.CreateFromPuid(this, this.organizationId, text, text2);
					return;
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.oauthActAsUser = OAuthActAsUser.CreateFromPuidOnly(this.organizationId, new NetID(text));
					return;
				}
				if (!string.IsNullOrEmpty(text2))
				{
					this.oauthActAsUser = OAuthActAsUser.CreateFromSmtpOnly(this.organizationId, text2);
					return;
				}
				base.Throw(OAuthErrors.NoUserClaimsFound, null, null, null);
			}

			// Token: 0x060005E3 RID: 1507 RVA: 0x0002D754 File Offset: 0x0002B954
			public override OAuthPreAuthIdentity GetPreAuthIdentity()
			{
				if (this.hasTenantId)
				{
					string lookupValue;
					if (OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Oid, out lookupValue))
					{
						return new OAuthPreAuthIdentity(OAuthPreAuthType.ExternalDirectoryObjectId, base.ResolveOrganizationByRealm(this.tenantId), lookupValue);
					}
					string lookupValue2;
					if (OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Smtp, out lookupValue2))
					{
						return new OAuthPreAuthIdentity(OAuthPreAuthType.Smtp, base.ResolveOrganizationByRealm(this.tenantId), lookupValue2);
					}
				}
				else
				{
					string lookupValue3;
					if (OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Puid, out lookupValue3))
					{
						return new OAuthPreAuthIdentity(OAuthPreAuthType.Puid, base.ResolveOrganizationByRealm(TemplateTenantConfiguration.TemplateTenantExternalDirectoryOrganizationId), lookupValue3);
					}
					string lookupValue2;
					if (OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Smtp, out lookupValue2))
					{
						return new OAuthPreAuthIdentity(OAuthPreAuthType.Smtp, base.ResolveOrganizationByRealm(TemplateTenantConfiguration.TemplateTenantExternalDirectoryOrganizationId), lookupValue2);
					}
				}
				return null;
			}

			// Token: 0x040005BA RID: 1466
			private static readonly string[] claimTypesForLogging = new string[]
			{
				Constants.ClaimTypes.Smtp,
				Constants.ClaimTypes.AppId,
				Constants.ClaimTypes.Puid,
				Constants.ClaimTypes.Oid
			};
		}

		// Token: 0x020000AC RID: 172
		public sealed class V1ProfileIdTokenHandler : OAuthTokenHandler.V1ProfileTokenHandlerBase
		{
			// Token: 0x060005E5 RID: 1509 RVA: 0x0002D842 File Offset: 0x0002BA42
			public V1ProfileIdTokenHandler(JwtSecurityToken token, Uri targetUri) : base(token, targetUri)
			{
				this.authAuthority = this.GetAuthenticationAuthority();
				this.puid = this.GetPuid(this.authAuthority);
				this.smtpAddress = this.GetSmtpAddress(this.puid, this.authAuthority);
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0002D882 File Offset: 0x0002BA82
			public override AuthenticationAuthority AuthenticationAuthority
			{
				get
				{
					return this.authAuthority;
				}
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0002D88A File Offset: 0x0002BA8A
			public override string TokenCategory
			{
				get
				{
					return Constants.TokenCategories.V1IdToken;
				}
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0002D891 File Offset: 0x0002BA91
			public override IEnumerable<string> ClaimTypesForLogging
			{
				get
				{
					return OAuthTokenHandler.V1ProfileIdTokenHandler.claimTypesForLogging;
				}
			}

			// Token: 0x060005E9 RID: 1513 RVA: 0x0002D898 File Offset: 0x0002BA98
			public override OAuthPreAuthIdentity GetPreAuthIdentity()
			{
				return new OAuthPreAuthIdentity(OAuthPreAuthType.Puid, null, this.puid);
			}

			// Token: 0x060005EA RID: 1514 RVA: 0x0002D8A8 File Offset: 0x0002BAA8
			protected override void ValidateAudience()
			{
				base.ThrowIfFalse(string.Equals(this.token.Audience, this.localConfiguration.ApplicationId, StringComparison.OrdinalIgnoreCase), OAuthErrors.InvalidAudience, new object[]
				{
					this.token.Audience
				});
			}

			// Token: 0x060005EB RID: 1515 RVA: 0x0002D8F2 File Offset: 0x0002BAF2
			protected override void ResolveOrganizationId()
			{
				if (this.authAuthority == AuthenticationAuthority.MSA)
				{
					this.organizationId = OrganizationId.FromMSAUserNetID(this.puid);
					return;
				}
				base.ResolveOrganizationId();
			}

			// Token: 0x060005EC RID: 1516 RVA: 0x0002D914 File Offset: 0x0002BB14
			protected override void ResolveUserInfo()
			{
				this.oauthActAsUser = OAuthActAsUser.CreateFromPuid(this, this.organizationId, this.puid, this.smtpAddress);
			}

			// Token: 0x060005ED RID: 1517 RVA: 0x0002D934 File Offset: 0x0002BB34
			protected override void ResolveOAuthApplication()
			{
				this.oauthApplication = Constants.IdTokenApplication;
			}

			// Token: 0x060005EE RID: 1518 RVA: 0x0002D944 File Offset: 0x0002BB44
			private AuthenticationAuthority GetAuthenticationAuthority()
			{
				string text;
				if (OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.IdentityProvider, out text))
				{
					if (!string.IsNullOrWhiteSpace(text) && text.Equals("live.com", StringComparison.OrdinalIgnoreCase))
					{
						return AuthenticationAuthority.MSA;
					}
				}
				else if (OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.AlternateSecurityId, out text) && !string.IsNullOrWhiteSpace(text) && text.StartsWith("1:live.com", StringComparison.OrdinalIgnoreCase))
				{
					return AuthenticationAuthority.MSA;
				}
				return AuthenticationAuthority.ORGID;
			}

			// Token: 0x060005EF RID: 1519 RVA: 0x0002D9AC File Offset: 0x0002BBAC
			private string GetPuid(AuthenticationAuthority authenticationAuthority)
			{
				string text;
				if (!OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.AlternateSecurityId, out text))
				{
					if (!OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Puid, out text))
					{
						base.Throw(OAuthErrors.NoPuidFound, new object[]
						{
							authenticationAuthority
						}, null, null);
					}
				}
				else
				{
					int num = text.LastIndexOf(':');
					base.ThrowIfFalse(num > 0 || text.Length - num <= 1, OAuthErrors.InvalidClaimValueFound, new object[]
					{
						Constants.ClaimTypes.AlternateSecurityId,
						text
					});
					text = text.Substring(num + 1);
				}
				return text;
			}

			// Token: 0x060005F0 RID: 1520 RVA: 0x0002DA4C File Offset: 0x0002BC4C
			private string GetSmtpAddress(string puid, AuthenticationAuthority authenticationAuthority)
			{
				string result;
				if (!OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.EmailAddress, out result) && !OAuthCommon.TryGetClaimValue(this.token, Constants.ClaimTypes.Upn, out result))
				{
					base.Throw(OAuthErrors.NoEmailAddressFound, new object[]
					{
						authenticationAuthority,
						puid
					}, null, null);
				}
				return result;
			}

			// Token: 0x040005BB RID: 1467
			private readonly string puid;

			// Token: 0x040005BC RID: 1468
			private readonly AuthenticationAuthority authAuthority;

			// Token: 0x040005BD RID: 1469
			private readonly string smtpAddress;

			// Token: 0x040005BE RID: 1470
			private static readonly string[] claimTypesForLogging = new string[]
			{
				Constants.ClaimTypes.AlternateSecurityId,
				Constants.ClaimTypes.Audience,
				Constants.ClaimTypes.IdentityProvider,
				Constants.ClaimTypes.Puid
			};
		}
	}
}
