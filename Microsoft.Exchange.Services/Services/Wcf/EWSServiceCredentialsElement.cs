using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Storage.Authentication;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.PartnerToken;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B7A RID: 2938
	public sealed class EWSServiceCredentialsElement : ServiceCredentialsElement
	{
		// Token: 0x060055C1 RID: 21953 RVA: 0x00110510 File Offset: 0x0010E710
		protected override object CreateBehavior()
		{
			object obj;
			ServiceCredentials serviceCredentials;
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Ews.EwsServiceCredentials.Enabled)
			{
				obj = new EWSServiceCredentialsElement.EWSServiceCredentials();
				serviceCredentials = (obj as ServiceCredentials);
				base.ApplyConfiguration(serviceCredentials);
			}
			else
			{
				obj = base.CreateBehavior();
				serviceCredentials = (obj as ServiceCredentials);
			}
			if (serviceCredentials == null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceDebug<string>((long)this.GetHashCode(), "Not adding Exchange certificates to ServiceCredentials. Behavior is not a ServiceCredentials - instead, it is of type {0}", obj.GetType().FullName);
				return obj;
			}
			if (serviceCredentials.IssuedTokenAuthentication == null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceDebug((long)this.GetHashCode(), "Not adding Exchange certificates to ServiceCredentials. ServiceCredentials.IssuedTokenAuthentication is null");
				return obj;
			}
			if (serviceCredentials.IssuedTokenAuthentication.KnownCertificates == null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceDebug((long)this.GetHashCode(), "Not adding Exchange certificates to ServiceCredentials. ServiceCredentials.IssuedTokenAuthentication.KnownCertificates is null");
				return obj;
			}
			ExternalAuthentication current = ExternalAuthentication.GetCurrent();
			ApplicationPoolRecycler.EnableOnFederationTrustCertificateChange();
			if (!current.Enabled)
			{
				ExTraceGlobals.AuthenticationTracer.TraceDebug((long)this.GetHashCode(), "Not adding Exchange certificates to ServiceCredentials. ExternalAuthentication.Enabled is false");
				return obj;
			}
			serviceCredentials.IssuedTokenAuthentication.AllowedAudienceUris.Add(current.TokenValidator.TargetUri.OriginalString);
			foreach (X509Certificate2 x509Certificate in current.Certificates)
			{
				ExTraceGlobals.AuthenticationTracer.TraceDebug<X509Certificate2>((long)this.GetHashCode(), "Adding Exchange certificate to ServiceCredentials: {0}", x509Certificate);
				serviceCredentials.IssuedTokenAuthentication.KnownCertificates.Add(x509Certificate);
			}
			return obj;
		}

		// Token: 0x02000B7B RID: 2939
		internal class EWSServiceCredentials : ServiceCredentials
		{
			// Token: 0x060055C4 RID: 21956 RVA: 0x00110688 File Offset: 0x0010E888
			protected override ServiceCredentials CloneCore()
			{
				return new EWSServiceCredentialsElement.EWSServiceCredentials();
			}

			// Token: 0x060055C5 RID: 21957 RVA: 0x0011068F File Offset: 0x0010E88F
			public override SecurityTokenManager CreateSecurityTokenManager()
			{
				return new EWSServiceCredentialsElement.EWSServiceCredentialsSecurityTokenManager(this);
			}
		}

		// Token: 0x02000B7C RID: 2940
		internal class EWSServiceCredentialsSecurityTokenManager : ServiceCredentialsSecurityTokenManager
		{
			// Token: 0x060055C6 RID: 21958 RVA: 0x00110697 File Offset: 0x0010E897
			public EWSServiceCredentialsSecurityTokenManager(EWSServiceCredentialsElement.EWSServiceCredentials serviceCredentials) : base(serviceCredentials)
			{
			}

			// Token: 0x060055C7 RID: 21959 RVA: 0x001106A0 File Offset: 0x0010E8A0
			public override SecurityTokenAuthenticator CreateSecurityTokenAuthenticator(SecurityTokenRequirement tokenRequirement, out SecurityTokenResolver outOfBandTokenResolver)
			{
				SecurityTokenAuthenticator securityTokenAuthenticator = base.CreateSecurityTokenAuthenticator(tokenRequirement, out outOfBandTokenResolver);
				if (securityTokenAuthenticator is SamlSecurityTokenAuthenticator)
				{
					outOfBandTokenResolver = new EWSServiceCredentialsElement.EWSSecurityTokenResolver(outOfBandTokenResolver);
				}
				return securityTokenAuthenticator;
			}
		}

		// Token: 0x02000B7D RID: 2941
		internal class EWSSecurityTokenResolver : SecurityTokenResolver
		{
			// Token: 0x060055C8 RID: 21960 RVA: 0x001106C8 File Offset: 0x0010E8C8
			public EWSSecurityTokenResolver(SecurityTokenResolver underlyingTokenResolver)
			{
				this.underlyingTokenResolver = underlyingTokenResolver;
			}

			// Token: 0x17001427 RID: 5159
			// (get) Token: 0x060055C9 RID: 21961 RVA: 0x001106FC File Offset: 0x0010E8FC
			public static ExactTimeoutCache<EWSServiceCredentialsElement.EWSSecurityTokenResolver.EncryptedKeyIdentifierClauseWrapper, EWSServiceCredentialsElement.EWSSecurityTokenResolver.SecurityKeyAndToken> Cache
			{
				get
				{
					if (EWSServiceCredentialsElement.EWSSecurityTokenResolver.cache == null)
					{
						lock (EWSServiceCredentialsElement.EWSSecurityTokenResolver.lockObj)
						{
							if (EWSServiceCredentialsElement.EWSSecurityTokenResolver.cache == null)
							{
								EWSServiceCredentialsElement.EWSSecurityTokenResolver.cache = new ExactTimeoutCache<EWSServiceCredentialsElement.EWSSecurityTokenResolver.EncryptedKeyIdentifierClauseWrapper, EWSServiceCredentialsElement.EWSSecurityTokenResolver.SecurityKeyAndToken>(delegate(EWSServiceCredentialsElement.EWSSecurityTokenResolver.EncryptedKeyIdentifierClauseWrapper key, EWSServiceCredentialsElement.EWSSecurityTokenResolver.SecurityKeyAndToken value, RemoveReason reason)
								{
									ExTraceGlobals.AuthenticationTracer.TraceDebug<EWSServiceCredentialsElement.EWSSecurityTokenResolver.EncryptedKeyIdentifierClauseWrapper, RemoveReason>(0L, "Removing the cached entry with key {0} due to {1}", key, reason);
									PerformanceMonitor.UpdatePartnerTokenCacheEntries(EWSServiceCredentialsElement.EWSSecurityTokenResolver.Cache.Count);
								}, null, null, EWSServiceCredentialsElement.EWSSecurityTokenResolver.cacheSize.Value, false);
							}
						}
					}
					return EWSServiceCredentialsElement.EWSSecurityTokenResolver.cache;
				}
			}

			// Token: 0x060055CA RID: 21962 RVA: 0x0011077C File Offset: 0x0010E97C
			protected override bool TryResolveTokenCore(SecurityKeyIdentifier keyIdentifier, out SecurityToken token)
			{
				if (keyIdentifier.Count == 1 && keyIdentifier[0] is EncryptedKeyIdentifierClause)
				{
					EncryptedKeyIdentifierClause encryptedKeyIdentifierClause = keyIdentifier[0] as EncryptedKeyIdentifierClause;
					EWSServiceCredentialsElement.EWSSecurityTokenResolver.SecurityKeyAndToken securityKeyAndToken;
					if (this.IsPartnerTokenEncryptedKeyIdentifierClause(encryptedKeyIdentifierClause) && EWSServiceCredentialsElement.EWSSecurityTokenResolver.Cache.TryGetValue(new EWSServiceCredentialsElement.EWSSecurityTokenResolver.EncryptedKeyIdentifierClauseWrapper(encryptedKeyIdentifierClause), out securityKeyAndToken))
					{
						token = securityKeyAndToken.SecurityToken;
						return true;
					}
				}
				return this.underlyingTokenResolver.TryResolveToken(keyIdentifier, out token);
			}

			// Token: 0x060055CB RID: 21963 RVA: 0x001107E4 File Offset: 0x0010E9E4
			protected override bool TryResolveTokenCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityToken token)
			{
				EncryptedKeyIdentifierClause encryptedKeyIdentifierClause = keyIdentifierClause as EncryptedKeyIdentifierClause;
				EWSServiceCredentialsElement.EWSSecurityTokenResolver.SecurityKeyAndToken securityKeyAndToken;
				if (this.IsPartnerTokenEncryptedKeyIdentifierClause(encryptedKeyIdentifierClause) && EWSServiceCredentialsElement.EWSSecurityTokenResolver.Cache.TryGetValue(new EWSServiceCredentialsElement.EWSSecurityTokenResolver.EncryptedKeyIdentifierClauseWrapper(encryptedKeyIdentifierClause), out securityKeyAndToken))
				{
					token = securityKeyAndToken.SecurityToken;
					return true;
				}
				return this.underlyingTokenResolver.TryResolveToken(keyIdentifierClause, out token);
			}

			// Token: 0x060055CC RID: 21964 RVA: 0x0011082C File Offset: 0x0010EA2C
			protected override bool TryResolveSecurityKeyCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityKey key)
			{
				EncryptedKeyIdentifierClause encryptedKeyIdentifierClause = keyIdentifierClause as EncryptedKeyIdentifierClause;
				if (this.IsPartnerTokenEncryptedKeyIdentifierClause(encryptedKeyIdentifierClause))
				{
					EWSServiceCredentialsElement.EWSSecurityTokenResolver.SecurityKeyAndToken securityKeyAndToken;
					if (EWSServiceCredentialsElement.EWSSecurityTokenResolver.Cache.TryGetValue(new EWSServiceCredentialsElement.EWSSecurityTokenResolver.EncryptedKeyIdentifierClauseWrapper(encryptedKeyIdentifierClause), out securityKeyAndToken))
					{
						key = securityKeyAndToken.SecurityKey;
						return true;
					}
					SecurityKeyIdentifier encryptingKeyIdentifier = encryptedKeyIdentifierClause.EncryptingKeyIdentifier;
					SecurityToken securityToken;
					if (base.TryResolveToken(encryptingKeyIdentifier, out securityToken))
					{
						byte[] encryptedKey = encryptedKeyIdentifierClause.GetEncryptedKey();
						string encryptionMethod = encryptedKeyIdentifierClause.EncryptionMethod;
						SecurityKey securityKey = securityToken.SecurityKeys[0];
						byte[] array = securityKey.DecryptKey(encryptionMethod, encryptedKey);
						key = new InMemorySymmetricSecurityKey(array, false);
						SecurityToken token = new WrappedKeySecurityToken("uuid-" + Guid.NewGuid().ToString(), array, encryptionMethod, securityToken, encryptingKeyIdentifier);
						EWSServiceCredentialsElement.EWSSecurityTokenResolver.Cache.TryAddAbsolute(new EWSServiceCredentialsElement.EWSSecurityTokenResolver.EncryptedKeyIdentifierClauseWrapper(encryptedKeyIdentifierClause), new EWSServiceCredentialsElement.EWSSecurityTokenResolver.SecurityKeyAndToken(key, token), EWSServiceCredentialsElement.EWSSecurityTokenResolver.cacheTimeToLive.Value);
						ExTraceGlobals.AuthenticationTracer.TraceDebug<EncryptedKeyIdentifierClause>((long)this.GetHashCode(), "Adding a new entry with key: {0}", encryptedKeyIdentifierClause);
						PerformanceMonitor.UpdatePartnerTokenCacheEntries(EWSServiceCredentialsElement.EWSSecurityTokenResolver.Cache.Count);
						return true;
					}
					ExTraceGlobals.AuthenticationTracer.TraceDebug<SecurityKeyIdentifierClause>((long)this.GetHashCode(), "Calling the underlying TokenResolver.TryResolveSecurityKey, the clause is {0}", keyIdentifierClause);
				}
				return this.underlyingTokenResolver.TryResolveSecurityKey(keyIdentifierClause, out key);
			}

			// Token: 0x060055CD RID: 21965 RVA: 0x0011094F File Offset: 0x0010EB4F
			private bool IsPartnerTokenEncryptedKeyIdentifierClause(EncryptedKeyIdentifierClause keyClause)
			{
				return keyClause != null && keyClause.CarriedKeyName != null && keyClause.CarriedKeyName.Equals(PartnerInfo.KeyName);
			}

			// Token: 0x04002ECA RID: 11978
			private static readonly TimeSpanAppSettingsEntry cacheTimeToLive = new TimeSpanAppSettingsEntry("PartnerEncryptedKeyIdentifierClauseCacheTimeToLive", TimeSpanUnit.Minutes, TimeSpan.FromHours(4.0), ExTraceGlobals.CommonAlgorithmTracer);

			// Token: 0x04002ECB RID: 11979
			private static readonly IntAppSettingsEntry cacheSize = new IntAppSettingsEntry("PartnerEncryptedKeyIdentifierClauseCacheLimit", 1000, ExTraceGlobals.CommonAlgorithmTracer);

			// Token: 0x04002ECC RID: 11980
			private static readonly object lockObj = new object();

			// Token: 0x04002ECD RID: 11981
			private static ExactTimeoutCache<EWSServiceCredentialsElement.EWSSecurityTokenResolver.EncryptedKeyIdentifierClauseWrapper, EWSServiceCredentialsElement.EWSSecurityTokenResolver.SecurityKeyAndToken> cache;

			// Token: 0x04002ECE RID: 11982
			private SecurityTokenResolver underlyingTokenResolver;

			// Token: 0x02000B7E RID: 2942
			public sealed class EncryptedKeyIdentifierClauseWrapper
			{
				// Token: 0x060055D0 RID: 21968 RVA: 0x001109C4 File Offset: 0x0010EBC4
				public EncryptedKeyIdentifierClauseWrapper(EncryptedKeyIdentifierClause clause)
				{
					this.clause = clause;
					byte[] buffer = this.clause.GetBuffer();
					this.hashCode = ((int)buffer[0] << 24 | (int)buffer[1] << 16 | (int)buffer[2] << 8 | (int)buffer[3]);
				}

				// Token: 0x060055D1 RID: 21969 RVA: 0x00110A08 File Offset: 0x0010EC08
				public override bool Equals(object obj)
				{
					EWSServiceCredentialsElement.EWSSecurityTokenResolver.EncryptedKeyIdentifierClauseWrapper encryptedKeyIdentifierClauseWrapper = obj as EWSServiceCredentialsElement.EWSSecurityTokenResolver.EncryptedKeyIdentifierClauseWrapper;
					return encryptedKeyIdentifierClauseWrapper != null && this.clause.Matches(encryptedKeyIdentifierClauseWrapper.clause);
				}

				// Token: 0x060055D2 RID: 21970 RVA: 0x00110A32 File Offset: 0x0010EC32
				public override int GetHashCode()
				{
					return this.hashCode;
				}

				// Token: 0x04002ED0 RID: 11984
				private readonly EncryptedKeyIdentifierClause clause;

				// Token: 0x04002ED1 RID: 11985
				private readonly int hashCode;
			}

			// Token: 0x02000B7F RID: 2943
			public class SecurityKeyAndToken
			{
				// Token: 0x060055D3 RID: 21971 RVA: 0x00110A3A File Offset: 0x0010EC3A
				public SecurityKeyAndToken(SecurityKey key, SecurityToken token)
				{
					this.key = key;
					this.token = token;
				}

				// Token: 0x17001428 RID: 5160
				// (get) Token: 0x060055D4 RID: 21972 RVA: 0x00110A50 File Offset: 0x0010EC50
				public SecurityKey SecurityKey
				{
					get
					{
						return this.key;
					}
				}

				// Token: 0x17001429 RID: 5161
				// (get) Token: 0x060055D5 RID: 21973 RVA: 0x00110A58 File Offset: 0x0010EC58
				public SecurityToken SecurityToken
				{
					get
					{
						return this.token;
					}
				}

				// Token: 0x04002ED2 RID: 11986
				private readonly SecurityKey key;

				// Token: 0x04002ED3 RID: 11987
				private readonly SecurityToken token;
			}
		}
	}
}
