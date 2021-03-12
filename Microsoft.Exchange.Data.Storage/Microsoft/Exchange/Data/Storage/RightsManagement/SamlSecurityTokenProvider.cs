using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security.Tokens;
using Microsoft.Exchange.Data.Storage.Authentication;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B62 RID: 2914
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SamlSecurityTokenProvider : SecurityTokenProvider
	{
		// Token: 0x0600698B RID: 27019 RVA: 0x001C4C34 File Offset: 0x001C2E34
		public SamlSecurityTokenProvider(SamlClientCredentials samlCredentials)
		{
			if (samlCredentials == null)
			{
				throw new ArgumentNullException("samlCredentials");
			}
			this.identity = samlCredentials.Identity;
			this.targetUri = samlCredentials.TargetUri;
			this.offer = samlCredentials.Offer;
			this.latencyTracker = (samlCredentials.RmsLatencyTracker ?? NoopRmsLatencyTracker.Instance);
			this.securityTokenService = ExternalAuthentication.GetCurrent().GetSecurityTokenService(samlCredentials.OrganizationId);
		}

		// Token: 0x0600698C RID: 27020 RVA: 0x001C4CA4 File Offset: 0x001C2EA4
		protected override SecurityToken GetTokenCore(TimeSpan timeout)
		{
			return SamlSecurityTokenProvider.CreateToken(this.securityTokenService.IssueToken(this.CreateDelegationTokenRequest()));
		}

		// Token: 0x0600698D RID: 27021 RVA: 0x001C4CBC File Offset: 0x001C2EBC
		protected override IAsyncResult BeginGetTokenCore(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.cachedSecurityToken != null)
			{
				LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(null, state, callback);
				ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "Got cached saml token. Invoking callback.");
				lazyAsyncResult.InvokeCallback();
				return lazyAsyncResult;
			}
			this.latencyTracker.BeginTrackRmsLatency(RmsOperationType.RequestDelegationToken);
			return this.securityTokenService.BeginIssueToken(this.CreateDelegationTokenRequest(), callback, state);
		}

		// Token: 0x0600698E RID: 27022 RVA: 0x001C4D14 File Offset: 0x001C2F14
		protected override SecurityToken EndGetTokenCore(IAsyncResult result)
		{
			RmsOperationType rmsOperationType = this.offer.Equals(Offer.IPCCertificationSTS) ? RmsOperationType.AcquireB2BRac : RmsOperationType.AcquireB2BLicense;
			if (this.cachedSecurityToken != null)
			{
				this.latencyTracker.BeginTrackRmsLatency(rmsOperationType);
				return this.cachedSecurityToken;
			}
			RequestedToken rt = this.securityTokenService.EndIssueToken(result);
			this.latencyTracker.EndAndBeginTrackRmsLatency(RmsOperationType.RequestDelegationToken, rmsOperationType);
			this.cachedSecurityToken = SamlSecurityTokenProvider.CreateToken(rt);
			return this.cachedSecurityToken;
		}

		// Token: 0x0600698F RID: 27023 RVA: 0x001C4D84 File Offset: 0x001C2F84
		private static SecurityToken CreateToken(RequestedToken rt)
		{
			BinarySecretSecurityToken proofToken = new BinarySecretSecurityToken(rt.ProofToken.GetSymmetricKey());
			return new GenericXmlSecurityToken(rt.SecurityToken, proofToken, DateTime.UtcNow, DateTime.UtcNow.AddDays(2.0), new SamlAssertionKeyIdentifierClause(rt.SecurityTokenReference.InnerText), new SamlAssertionKeyIdentifierClause(rt.RequestUnattachedReference.InnerText), new ReadOnlyCollection<IAuthorizationPolicy>(new List<IAuthorizationPolicy>()));
		}

		// Token: 0x06006990 RID: 27024 RVA: 0x001C4DF4 File Offset: 0x001C2FF4
		private DelegationTokenRequest CreateDelegationTokenRequest()
		{
			return new DelegationTokenRequest
			{
				FederatedIdentity = new FederatedIdentity(this.identity.Email, IdentityType.UPN),
				EmailAddress = this.identity.Email,
				Target = new TokenTarget(this.targetUri),
				Offer = this.offer,
				EmailAddresses = ((this.identity.ProxyAddresses != null) ? new List<string>(this.identity.ProxyAddresses) : null)
			};
		}

		// Token: 0x04003C0D RID: 15373
		private LicenseIdentity identity;

		// Token: 0x04003C0E RID: 15374
		private Uri targetUri;

		// Token: 0x04003C0F RID: 15375
		private Offer offer;

		// Token: 0x04003C10 RID: 15376
		private IRmsLatencyTracker latencyTracker;

		// Token: 0x04003C11 RID: 15377
		private SecurityToken cachedSecurityToken;

		// Token: 0x04003C12 RID: 15378
		private SecurityTokenService securityTokenService;
	}
}
