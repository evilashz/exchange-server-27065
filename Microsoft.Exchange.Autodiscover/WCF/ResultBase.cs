using System;
using Microsoft.Exchange.Autodiscover.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000077 RID: 119
	internal abstract class ResultBase
	{
		// Token: 0x06000323 RID: 803 RVA: 0x00014646 File Offset: 0x00012846
		internal ResultBase(UserResultMapping userResultMapping)
		{
			ExTraceGlobals.FrameworkTracer.TraceDebug<ResultBase, string>((long)this.GetHashCode(), "{0} constructor called for '{1}'.", this, userResultMapping.Mailbox);
			this.userResultMapping = userResultMapping;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00014672 File Offset: 0x00012872
		internal UserResultMapping UserResultMapping
		{
			get
			{
				return this.userResultMapping;
			}
		}

		// Token: 0x06000325 RID: 805
		internal abstract UserResponse CreateResponse(IBudget budget);

		// Token: 0x06000326 RID: 806 RVA: 0x0001467C File Offset: 0x0001287C
		protected static UserResponse GenerateUserResponseError(UserConfigurationSettings settings, UserSettingErrorCollection settingErrors)
		{
			UserResponse userResponse = new UserResponse();
			switch (settings.ErrorCode)
			{
			case UserConfigurationSettingsErrorCode.RedirectAddress:
				userResponse.ErrorCode = ErrorCode.RedirectAddress;
				userResponse.ErrorMessage = settings.ErrorMessage;
				RequestDetailsLoggerBase<RequestDetailsLogger>.Current.SetRedirectionType(RedirectionType.EmailAddressRedirect);
				userResponse.RedirectTarget = settings.RedirectTarget;
				return userResponse;
			case UserConfigurationSettingsErrorCode.RedirectUrl:
				userResponse.ErrorCode = ErrorCode.RedirectUrl;
				userResponse.ErrorMessage = settings.ErrorMessage;
				RequestDetailsLoggerBase<RequestDetailsLogger>.Current.SetRedirectionType(RedirectionType.UrlRedirect);
				userResponse.RedirectTarget = settings.RedirectTarget;
				return userResponse;
			case UserConfigurationSettingsErrorCode.InvalidUser:
				userResponse.ErrorCode = ErrorCode.InvalidUser;
				userResponse.ErrorMessage = settings.ErrorMessage;
				return userResponse;
			case UserConfigurationSettingsErrorCode.InternalServerError:
				userResponse.ErrorCode = ErrorCode.InternalServerError;
				userResponse.ErrorMessage = settings.ErrorMessage;
				return userResponse;
			}
			userResponse.UserSettingErrors = settingErrors;
			userResponse.ErrorCode = ErrorCode.InvalidRequest;
			userResponse.ErrorMessage = (string.IsNullOrEmpty(settings.ErrorMessage) ? Strings.InvalidRequest : settings.ErrorMessage);
			return userResponse;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00014778 File Offset: 0x00012978
		protected UserResponse CreateInvalidUserResponse()
		{
			return ResultBase.GenerateUserResponseError(new UserConfigurationSettings
			{
				ErrorCode = UserConfigurationSettingsErrorCode.InvalidUser,
				ErrorMessage = string.Format(Strings.InvalidUser, this.userResultMapping.Mailbox)
			}, this.userResultMapping.CallContext.SettingErrors);
		}

		// Token: 0x040002F2 RID: 754
		protected UserResultMapping userResultMapping;
	}
}
