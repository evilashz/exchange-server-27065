using System;
using System.Web;
using Microsoft.Exchange.Autodiscover.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000084 RID: 132
	internal sealed class MServeQueryResult : ResultBase
	{
		// Token: 0x06000371 RID: 881 RVA: 0x00015B90 File Offset: 0x00013D90
		internal MServeQueryResult(UserResultMapping userResultMapping) : base(userResultMapping)
		{
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00015B9C File Offset: 0x00013D9C
		internal override UserResponse CreateResponse(IBudget budget)
		{
			UserResponse result;
			if (!string.IsNullOrEmpty(this.RedirectServer))
			{
				ExTraceGlobals.FrameworkTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Creating response using redirect url {0} for {1}.", this.RedirectServer, this.userResultMapping.Mailbox);
				result = this.CreateRedirectResponse();
			}
			else
			{
				ExTraceGlobals.FrameworkTracer.TraceDebug<string>((long)this.GetHashCode(), "MServer didn't find {0}.  Creating invalid user response.", this.userResultMapping.Mailbox);
				result = base.CreateInvalidUserResponse();
			}
			return result;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00015C10 File Offset: 0x00013E10
		internal UserResponse CreateRedirectResponse()
		{
			UriBuilder uriBuilder = new UriBuilder(HttpContext.Current.Request.Headers[WellKnownHeader.MsExchProxyUri]);
			uriBuilder.Host = this.RedirectServer;
			return ResultBase.GenerateUserResponseError(new UserConfigurationSettings
			{
				ErrorCode = UserConfigurationSettingsErrorCode.RedirectUrl,
				ErrorMessage = string.Format(Strings.RedirectUrlForUser, this.userResultMapping.Mailbox),
				RedirectTarget = ((!string.IsNullOrEmpty(base.UserResultMapping.Mailbox)) ? Common.AddUserHintToUrl(uriBuilder.Uri, base.UserResultMapping.Mailbox) : uriBuilder.ToString())
			}, this.userResultMapping.CallContext.SettingErrors);
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00015CC1 File Offset: 0x00013EC1
		// (set) Token: 0x06000375 RID: 885 RVA: 0x00015CC9 File Offset: 0x00013EC9
		internal string RedirectServer { get; set; }
	}
}
