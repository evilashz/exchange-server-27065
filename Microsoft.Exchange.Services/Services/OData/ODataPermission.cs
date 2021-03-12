using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E2C RID: 3628
	internal abstract class ODataPermission
	{
		// Token: 0x06005D92 RID: 23954 RVA: 0x00123798 File Offset: 0x00121998
		public static ODataPermission Create(ODataRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			AuthZClientInfo.ApplicationAttachedAuthZClientInfo applicationAttachedAuthZClientInfo = request.ODataContext.CallContext.EffectiveCaller as AuthZClientInfo.ApplicationAttachedAuthZClientInfo;
			if (applicationAttachedAuthZClientInfo != null)
			{
				return new ODataPermission.OAuthRequestPermission(request, applicationAttachedAuthZClientInfo.OAuthIdentity);
			}
			return ODataPermission.FullPermission.Instance;
		}

		// Token: 0x06005D93 RID: 23955
		public abstract void Check();

		// Token: 0x02000E2D RID: 3629
		public sealed class FullPermission : ODataPermission
		{
			// Token: 0x1700152F RID: 5423
			// (get) Token: 0x06005D95 RID: 23957 RVA: 0x001237E3 File Offset: 0x001219E3
			public static ODataPermission.FullPermission Instance
			{
				get
				{
					return ODataPermission.FullPermission.instance;
				}
			}

			// Token: 0x06005D96 RID: 23958 RVA: 0x001237EA File Offset: 0x001219EA
			public override void Check()
			{
			}

			// Token: 0x0400327F RID: 12927
			private static ODataPermission.FullPermission instance = new ODataPermission.FullPermission();
		}

		// Token: 0x02000E2E RID: 3630
		public sealed class OAuthRequestPermission : ODataPermission
		{
			// Token: 0x06005D99 RID: 23961 RVA: 0x00123800 File Offset: 0x00121A00
			public OAuthRequestPermission(ODataRequest request, OAuthIdentity identity)
			{
				this.request = request;
				this.identity = identity;
			}

			// Token: 0x06005D9A RID: 23962 RVA: 0x00123874 File Offset: 0x00121A74
			public override void Check()
			{
				this.ThrowIfFalse(this.identity.OAuthApplication.ApplicationType == OAuthApplicationType.V1App || this.identity.OAuthApplication.ApplicationType == OAuthApplicationType.V1ExchangeSelfIssuedApp, OAuthErrors.TokenProfileNotApplicable);
				if (!this.identity.IsAppOnly)
				{
					this.ThrowIfFalse(this.identity.ActAsUser.Sid.Equals(this.request.ODataContext.TargetMailbox.Sid), OAuthErrors.AllowAccessOwnMailboxOnly);
				}
				V1ProfileAppInfo v1ProfileApp = this.identity.OAuthApplication.V1ProfileApp;
				string[] array = this.identity.IsAppOnly ? OAuthGrant.ExtractKnownGrantsFromRole(v1ProfileApp.Role) : OAuthGrant.ExtractKnownGrants(v1ProfileApp.Scope);
				this.ThrowIfFalse(array != null && array.Length > 0, OAuthErrors.NoGrantPresented);
				string[] required = ODataPermission.OAuthRequestPermission.dictionary.GetOrAdd(this.request.GetType(), (Type type) => (from AllowedOAuthGrantAttribute x in type.GetCustomAttributes(typeof(AllowedOAuthGrantAttribute), false)
				select x.Grant).ToArray<string>());
				this.ThrowIfFalse(required != null && required.Length > 0, OAuthErrors.NotSupportedWithV1AppToken);
				this.ThrowIfFalse(array.Any((string x) => required.Contains(x)), OAuthErrors.NotEnoughGrantPresented);
				this.request.PerformAdditionalGrantCheck(array);
			}

			// Token: 0x06005D9B RID: 23963 RVA: 0x001239CD File Offset: 0x00121BCD
			private void ThrowIfFalse(bool condition, OAuthErrors inboundError)
			{
				if (!condition)
				{
					throw new ODataAuthorizationException(new InvalidOAuthTokenException(inboundError, null, null));
				}
			}

			// Token: 0x04003280 RID: 12928
			private static ConcurrentDictionary<Type, string[]> dictionary = new ConcurrentDictionary<Type, string[]>();

			// Token: 0x04003281 RID: 12929
			private readonly ODataRequest request;

			// Token: 0x04003282 RID: 12930
			private readonly OAuthIdentity identity;
		}
	}
}
