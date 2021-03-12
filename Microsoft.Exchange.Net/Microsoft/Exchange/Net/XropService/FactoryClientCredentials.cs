using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security.Tokens;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000B90 RID: 2960
	internal sealed class FactoryClientCredentials : ClientCredentials
	{
		// Token: 0x06003F62 RID: 16226 RVA: 0x000A7FEF File Offset: 0x000A61EF
		public FactoryClientCredentials()
		{
			base.SupportInteractive = false;
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x000A7FFE File Offset: 0x000A61FE
		private FactoryClientCredentials(FactoryClientCredentials other) : base(other)
		{
			base.SupportInteractive = other.SupportInteractive;
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x000A8013 File Offset: 0x000A6213
		public override SecurityTokenManager CreateSecurityTokenManager()
		{
			return new FactoryClientCredentials.FactoryClientCredentialsSecurityTokenManager(this);
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x000A801B File Offset: 0x000A621B
		protected override ClientCredentials CloneCore()
		{
			return new FactoryClientCredentials(this);
		}

		// Token: 0x02000B91 RID: 2961
		private sealed class FactoryClientCredentialsSecurityTokenManager : ClientCredentialsSecurityTokenManager
		{
			// Token: 0x06003F66 RID: 16230 RVA: 0x000A8023 File Offset: 0x000A6223
			public FactoryClientCredentialsSecurityTokenManager(FactoryClientCredentials factoryClientCredentials) : base(factoryClientCredentials)
			{
			}

			// Token: 0x06003F67 RID: 16231 RVA: 0x000A802C File Offset: 0x000A622C
			public override SecurityTokenProvider CreateSecurityTokenProvider(SecurityTokenRequirement tokenRequirement)
			{
				if (tokenRequirement.TokenType == SecurityTokenTypes.UserName)
				{
					FederatedClientCredentials federatedClientCredentials = FactoryClientCredentials.FactoryClientCredentialsSecurityTokenManager.FindChannelFederatedClientCredentials(tokenRequirement);
					return new FactoryClientCredentials.SupportingSecurityTokenProvider(federatedClientCredentials);
				}
				if (tokenRequirement.TokenType == SecurityTokenTypes.Saml || tokenRequirement.TokenType == "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1")
				{
					FederatedClientCredentials federatedClientCredentials2 = FactoryClientCredentials.FactoryClientCredentialsSecurityTokenManager.FindChannelFederatedClientCredentials(tokenRequirement);
					return new FactoryClientCredentials.PrimarySecurityTokenProvider(federatedClientCredentials2);
				}
				return base.CreateSecurityTokenProvider(tokenRequirement);
			}

			// Token: 0x06003F68 RID: 16232 RVA: 0x000A8094 File Offset: 0x000A6294
			internal static FederatedClientCredentials FindChannelFederatedClientCredentials(SecurityTokenRequirement tokenRequirement)
			{
				ChannelParameterCollection channelParameterCollection = null;
				if (tokenRequirement.TryGetProperty<ChannelParameterCollection>(ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty, out channelParameterCollection) && channelParameterCollection != null)
				{
					foreach (object obj in channelParameterCollection)
					{
						FederatedClientCredentials federatedClientCredentials = obj as FederatedClientCredentials;
						if (federatedClientCredentials != null)
						{
							return federatedClientCredentials;
						}
					}
				}
				ExTraceGlobals.XropServiceClientTracer.TraceError(0L, "XropFactoryCredentials: No federated credentials found in channel parameters.");
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000B92 RID: 2962
		private sealed class PrimarySecurityTokenProvider : SecurityTokenProvider
		{
			// Token: 0x06003F69 RID: 16233 RVA: 0x000A8118 File Offset: 0x000A6318
			public PrimarySecurityTokenProvider(FederatedClientCredentials federatedClientCredentials)
			{
				this.federatedClientCredentials = federatedClientCredentials;
			}

			// Token: 0x06003F6A RID: 16234 RVA: 0x000A8128 File Offset: 0x000A6328
			protected override SecurityToken GetTokenCore(TimeSpan timeout)
			{
				RequestedToken token = this.federatedClientCredentials.GetToken();
				ExTraceGlobals.XropServiceClientTracer.TraceDebug((long)this.GetHashCode(), "PrimarySecurityTokenProvider issuing SAML token");
				return token.GetSecurityToken();
			}

			// Token: 0x0400373C RID: 14140
			private FederatedClientCredentials federatedClientCredentials;
		}

		// Token: 0x02000B93 RID: 2963
		private sealed class SupportingSecurityTokenProvider : SecurityTokenProvider
		{
			// Token: 0x06003F6B RID: 16235 RVA: 0x000A815D File Offset: 0x000A635D
			public SupportingSecurityTokenProvider(FederatedClientCredentials federatedClientCredentials)
			{
				this.federatedClientCredentials = federatedClientCredentials;
			}

			// Token: 0x06003F6C RID: 16236 RVA: 0x000A816C File Offset: 0x000A636C
			protected override SecurityToken GetTokenCore(TimeSpan timeout)
			{
				if (string.IsNullOrEmpty(this.federatedClientCredentials.UserEmailAddress))
				{
					throw new ArgumentException("UserEmailAddress");
				}
				ExTraceGlobals.XropServiceClientTracer.TraceDebug((long)this.GetHashCode(), "SupportingSecurityTokenProvider issuing UserNameSecurityToken");
				return new UserNameSecurityToken(this.federatedClientCredentials.UserEmailAddress, null);
			}

			// Token: 0x0400373D RID: 14141
			private FederatedClientCredentials federatedClientCredentials;
		}
	}
}
