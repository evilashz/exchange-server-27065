using System;
using Microsoft.Exchange.SoapWebClient.AutoDiscover;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000E3 RID: 227
	internal sealed class UserSoapAutoDiscoverRequest : SoapAutoDiscoverRequest
	{
		// Token: 0x060005DB RID: 1499 RVA: 0x00019538 File Offset: 0x00017738
		internal UserSoapAutoDiscoverRequest(Application application, ClientContext clientContext, RequestType requestType, RequestLogger requestLogger, AutoDiscoverAuthenticator authenticator, Uri targetUri, EmailAddress[] emailAddresses, AutodiscoverType autodiscoverType) : base(application, clientContext, requestLogger, "UserSoapAutoDiscoverRequest", authenticator, targetUri, emailAddresses, autodiscoverType)
		{
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001955C File Offset: 0x0001775C
		public override string ToString()
		{
			return "UserSoapAutoDiscoverRequest to " + base.TargetUri.ToString();
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00019574 File Offset: 0x00017774
		protected override IAsyncResult BeginGetSettings(AsyncCallback callback)
		{
			User[] array = new User[base.EmailAddresses.Length];
			for (int i = 0; i < base.EmailAddresses.Length; i++)
			{
				array[i] = new User
				{
					Mailbox = base.EmailAddresses[i].Address
				};
			}
			GetUserSettingsRequest request = new GetUserSettingsRequest
			{
				Users = array,
				RequestedSettings = UserSoapAutoDiscoverRequest.RequestedSettings,
				RequestedVersion = new ExchangeVersion?(base.Application.GetRequestedVersionForAutoDiscover(base.AutodiscoverType) ?? ExchangeVersion.Exchange2010)
			};
			if (this.authenticator.ProxyAuthenticator != null && this.authenticator.ProxyAuthenticator.AuthenticatorType == AuthenticatorType.WSSecurity)
			{
				this.client.HttpHeaders.Add("X-AnchorMailbox", base.EmailAddresses[0].Address);
			}
			SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceDebug<object, string, Uri>((long)this.GetHashCode(), "{0}: Adding header mailbox {1} to request to {2}.", TraceContext.Get(), base.EmailAddresses[0].Address, base.TargetUri);
			return this.client.BeginGetUserSettings(request, callback, null);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001968D File Offset: 0x0001788D
		protected override AutodiscoverResponse EndGetSettings(IAsyncResult asyncResult)
		{
			return this.client.EndGetUserSettings(asyncResult);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001969C File Offset: 0x0001789C
		protected override void HandleResponse(AutodiscoverResponse autodiscoverResponse)
		{
			GetUserSettingsResponse getUserSettingsResponse = (GetUserSettingsResponse)autodiscoverResponse;
			if (getUserSettingsResponse.UserResponses == null)
			{
				SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceError<object, UserSoapAutoDiscoverRequest>((long)this.GetHashCode(), "{0}: Request '{1}' got response with no UserResponses", TraceContext.Get(), this);
				this.HandleException(new AutoDiscoverFailedException(Strings.descSoapAutoDiscoverInvalidResponseError(this.client.Url), 39740U));
				return;
			}
			if (getUserSettingsResponse.UserResponses.Length != base.EmailAddresses.Length)
			{
				SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceError((long)this.GetHashCode(), "{0}: Request '{1}' got response with unexpected number of UserResponses EmailAddress length {2} UserResponses length{3}", new object[]
				{
					TraceContext.Get(),
					this,
					base.EmailAddresses.Length,
					getUserSettingsResponse.UserResponses.Length
				});
				for (int i = 0; i < getUserSettingsResponse.UserResponses.Length; i++)
				{
					if (getUserSettingsResponse.UserResponses[i].ErrorCodeSpecified)
					{
						SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceError((long)this.GetHashCode(), "{0}: UserResponses[{1}] ErrorCode {2} ErrorMessage {3}", new object[]
						{
							TraceContext.Get(),
							i,
							getUserSettingsResponse.UserResponses[i].ErrorCode,
							getUserSettingsResponse.UserResponses[i].ErrorMessage
						});
					}
				}
				this.HandleException(new AutoDiscoverFailedException(Strings.descSoapAutoDiscoverInvalidResponseError(this.client.Url), 56124U));
				return;
			}
			SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceDebug<object, UserSoapAutoDiscoverRequest>((long)this.GetHashCode(), "{0}: Request '{1}' received valid response.", TraceContext.Get(), this);
			AutoDiscoverRequestResult[] array = new AutoDiscoverRequestResult[base.EmailAddresses.Length];
			for (int j = 0; j < base.EmailAddresses.Length; j++)
			{
				array[j] = this.GetAutodiscoverResultFromUserResponse(base.EmailAddresses[j], getUserSettingsResponse.UserResponses[j]);
			}
			base.HandleResult(array);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00019854 File Offset: 0x00017A54
		private AutoDiscoverRequestResult GetAutodiscoverResultFromUserResponse(EmailAddress emailAddress, UserResponse userResponse)
		{
			AutoDiscoverRequestResult autoDiscoverRequestResult = this.GetAutodiscoverResultFromUserResponseInternal(emailAddress, userResponse);
			if (autoDiscoverRequestResult == null)
			{
				autoDiscoverRequestResult = new AutoDiscoverRequestResult(base.TargetUri, new AutoDiscoverFailedException(Strings.descSoapAutoDiscoverRequestUserSettingInvalidError(base.TargetUri.ToString(), "ExternalEwsUrl"), 43836U), base.FrontEndServerName, base.BackEndServerName);
			}
			return autoDiscoverRequestResult;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000198A8 File Offset: 0x00017AA8
		private AutoDiscoverRequestResult GetAutodiscoverResultFromUserResponseInternal(EmailAddress emailAddress, UserResponse userResponse)
		{
			if (userResponse == null)
			{
				SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceError<object, UserSoapAutoDiscoverRequest, string>((long)this.GetHashCode(), "{0}: Request '{1}' got empty response for user {2}.", TraceContext.Get(), this, emailAddress.Address);
				return null;
			}
			if (userResponse.ErrorCodeSpecified)
			{
				switch (userResponse.ErrorCode)
				{
				case ErrorCode.NoError:
					break;
				case ErrorCode.RedirectAddress:
					SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceDebug((long)this.GetHashCode(), "{0}: Request '{1}' got address redirect response for user {2} to {3}", new object[]
					{
						TraceContext.Get(),
						this,
						emailAddress.Address,
						userResponse.RedirectTarget
					});
					return new AutoDiscoverRequestResult(base.TargetUri, userResponse.RedirectTarget, null, null, base.FrontEndServerName, base.BackEndServerName);
				case ErrorCode.RedirectUrl:
					if (!Uri.IsWellFormedUriString(userResponse.RedirectTarget, UriKind.Absolute))
					{
						SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceError((long)this.GetHashCode(), "{0}: Request '{1}' got URL redirect response for user {2} but the redirect value is not valid: {3}", new object[]
						{
							TraceContext.Get(),
							this,
							"ExternalEwsUrl",
							userResponse.RedirectTarget
						});
						return null;
					}
					SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceDebug((long)this.GetHashCode(), "{0}: Request '{1}' got URL redirect response for user {2} to {3}", new object[]
					{
						TraceContext.Get(),
						this,
						emailAddress.Address,
						userResponse.RedirectTarget
					});
					return new AutoDiscoverRequestResult(base.TargetUri, null, new Uri(userResponse.RedirectTarget), null, base.FrontEndServerName, base.BackEndServerName);
				default:
					return new AutoDiscoverRequestResult(base.TargetUri, new AutoDiscoverInvalidUserException(Strings.descSoapAutoDiscoverRequestUserSettingInvalidError(base.TargetUri.ToString(), "ExternalEwsUrl")), base.FrontEndServerName, base.BackEndServerName);
				}
			}
			UserSettingError settingErrorFromResponse = this.GetSettingErrorFromResponse(userResponse, "ExternalEwsUrl");
			UserSettingError settingErrorFromResponse2 = this.GetSettingErrorFromResponse(userResponse, "InteropExternalEwsUrl");
			if (settingErrorFromResponse != null && settingErrorFromResponse2 != null)
			{
				SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceError((long)this.GetHashCode(), "{0}: Request '{1}' got error response for user {2}. Error: {3}:{4}:{5}", new object[]
				{
					TraceContext.Get(),
					this,
					emailAddress.Address,
					settingErrorFromResponse.SettingName,
					settingErrorFromResponse.ErrorCode,
					settingErrorFromResponse.ErrorMessage
				});
				return new AutoDiscoverRequestResult(base.TargetUri, new AutoDiscoverFailedException(Strings.descSoapAutoDiscoverRequestUserSettingError(base.TargetUri.ToString(), settingErrorFromResponse.SettingName, settingErrorFromResponse.ErrorMessage), 60220U), base.FrontEndServerName, base.BackEndServerName);
			}
			string versionValue = null;
			string text = null;
			if (settingErrorFromResponse == null)
			{
				text = this.GetStringSettingFromResponse(userResponse, emailAddress.Address, "ExternalEwsUrl");
				versionValue = this.GetStringSettingFromResponse(userResponse, emailAddress.Address, "ExternalEwsVersion");
			}
			if (text == null && settingErrorFromResponse2 == null)
			{
				text = this.GetStringSettingFromResponse(userResponse, emailAddress.Address, "InteropExternalEwsUrl");
				versionValue = this.GetStringSettingFromResponse(userResponse, emailAddress.Address, "InteropExternalEwsVersion");
			}
			if (text == null)
			{
				SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceError<object, UserSoapAutoDiscoverRequest, string>((long)this.GetHashCode(), "{0}: Request '{1}' for user {2} got no URL value.", TraceContext.Get(), this, emailAddress.Address);
				return null;
			}
			return base.GetAutodiscoverResult(text, versionValue, emailAddress);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00019B9C File Offset: 0x00017D9C
		private UserSettingError GetSettingErrorFromResponse(UserResponse userResponse, string settingName)
		{
			if (userResponse.UserSettingErrors != null)
			{
				UserSettingError[] userSettingErrors = userResponse.UserSettingErrors;
				int i = 0;
				while (i < userSettingErrors.Length)
				{
					UserSettingError userSettingError = userSettingErrors[i];
					if (userSettingError != null && StringComparer.InvariantCulture.Equals(userSettingError.SettingName, settingName))
					{
						if (userSettingError.ErrorCode != ErrorCode.NoError)
						{
							return userSettingError;
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}
			return null;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00019BF0 File Offset: 0x00017DF0
		private string GetStringSettingFromResponse(UserResponse userResponse, string emailAddress, string settingName)
		{
			if (userResponse.UserSettings == null)
			{
				return null;
			}
			UserSetting userSetting = null;
			foreach (UserSetting userSetting2 in userResponse.UserSettings)
			{
				if (StringComparer.InvariantCulture.Equals(userSetting2.Name, settingName))
				{
					userSetting = userSetting2;
					break;
				}
			}
			if (userSetting == null)
			{
				SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceError((long)this.GetHashCode(), "{0}: Request '{1}' for user {2} got response without setting {3}.", new object[]
				{
					TraceContext.Get(),
					this,
					emailAddress,
					settingName
				});
				return null;
			}
			StringSetting stringSetting = userSetting as StringSetting;
			if (stringSetting == null)
			{
				SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceError((long)this.GetHashCode(), "{0}: Request '{1}' for user {2} got response for setting {3} of unexpected type: {4}", new object[]
				{
					TraceContext.Get(),
					this,
					emailAddress,
					settingName,
					userSetting.GetType().Name
				});
				return null;
			}
			if (string.IsNullOrEmpty(stringSetting.Value))
			{
				SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceError((long)this.GetHashCode(), "{0}: Request '{1}' for user {2} got response with empty value for setting {3}", new object[]
				{
					TraceContext.Get(),
					this,
					emailAddress,
					settingName
				});
				return null;
			}
			SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceDebug((long)this.GetHashCode(), "{0}: Request '{1}' for user {2} got response for setting {3} with value: {4}", new object[]
			{
				TraceContext.Get(),
				this,
				emailAddress,
				settingName,
				stringSetting.Value
			});
			return stringSetting.Value;
		}

		// Token: 0x04000365 RID: 869
		private static readonly string[] RequestedSettings = new string[]
		{
			"ExternalEwsUrl",
			"ExternalEwsVersion",
			"InteropExternalEwsUrl"
		};
	}
}
