using System;
using System.Web;
using System.Web.Util;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.IdentityModel.Protocols.WSFederation;
using Microsoft.IdentityModel.Web;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200001C RID: 28
	public class AdfsRequestValidator : RequestValidator
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00007658 File Offset: 0x00005858
		protected override bool IsValidRequestString(HttpContext context, string value, RequestValidationSource requestValidationSource, string collectionKey, out int validationFailureIndex)
		{
			validationFailureIndex = 0;
			if (AdfsFederationAuthModule.IsAdfsAuthenticationEnabled && requestValidationSource == RequestValidationSource.Form && collectionKey.Equals("wresult", StringComparison.Ordinal) && context.Request.UrlReferrer.ToString().StartsWith(FederatedAuthentication.WSFederationAuthenticationModule.Issuer, StringComparison.OrdinalIgnoreCase))
			{
				SignInResponseMessage signInResponseMessage = WSFederationMessage.CreateFromFormPost(context.Request) as SignInResponseMessage;
				if (signInResponseMessage != null)
				{
					ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsRequestValidator::IsValidRequestString]: Allowing request posted from ADFS.");
					return true;
				}
			}
			ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsRequestValidator::IsValidRequestString]: Request not posted from ADFS. Falling through to base class handler. IsAdfsAuthenticationEnabled={0}, requestValidationSource={1},  collectionKey={2}, UrlReferrer={3}.", new object[]
			{
				AdfsFederationAuthModule.IsAdfsAuthenticationEnabled,
				requestValidationSource,
				collectionKey,
				context.Request.UrlReferrer
			});
			return base.IsValidRequestString(context, value, requestValidationSource, collectionKey, out validationFailureIndex);
		}
	}
}
