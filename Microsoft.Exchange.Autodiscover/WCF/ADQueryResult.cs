using System;
using Microsoft.Exchange.Autodiscover.ConfigurationSettings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000078 RID: 120
	internal sealed class ADQueryResult : ResultBase
	{
		// Token: 0x06000328 RID: 808 RVA: 0x000147C8 File Offset: 0x000129C8
		internal ADQueryResult(UserResultMapping userResultMapping) : base(userResultMapping)
		{
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000147D4 File Offset: 0x000129D4
		internal override UserResponse CreateResponse(IBudget budget)
		{
			UserResponse result;
			if (this.Result.Error == null)
			{
				ExTraceGlobals.FrameworkTracer.TraceDebug<ADRecipient, string>((long)this.GetHashCode(), "Creating response using ADRecipient {0} for {1}.", this.Result.Data, this.userResultMapping.Mailbox);
				result = this.CreateResponseFromQueryResult(budget);
			}
			else if (this.Result.Error == ProviderError.NotFound)
			{
				ExTraceGlobals.FrameworkTracer.TraceDebug<string>((long)this.GetHashCode(), "No ADRecipient found for '{0}'.  Creating invalid user response.", this.userResultMapping.Mailbox);
				result = base.CreateInvalidUserResponse();
			}
			else
			{
				ValidationError validationError = this.Result.Error as ValidationError;
				object arg;
				if (validationError != null)
				{
					arg = validationError.Description;
				}
				else
				{
					arg = this.Result.Error;
				}
				ExTraceGlobals.FrameworkTracer.TraceDebug<object, string>((long)this.GetHashCode(), "Error {0} looking up {1}.  Creating internal server error response.", arg, this.userResultMapping.Mailbox);
				result = this.CreateInternalServerErrorResponse();
			}
			return result;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000148D0 File Offset: 0x00012AD0
		private UserResponse CreateInternalServerErrorResponse()
		{
			return ResultBase.GenerateUserResponseError(new UserConfigurationSettings
			{
				ErrorCode = UserConfigurationSettingsErrorCode.InternalServerError,
				ErrorMessage = Strings.InternalServerError
			}, this.userResultMapping.CallContext.SettingErrors);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00014910 File Offset: 0x00012B10
		internal UserResponse CreateResponseFromQueryResult(IBudget budget)
		{
			UserSettingsProvider userSettingsProvider = new UserSettingsProvider(this.Result.Data, this.userResultMapping.Mailbox, this.userResultMapping.CallContext.CallerCapabilities, this.userResultMapping.CallContext.UserAuthType, this.userResultMapping.CallContext.UseClientCertificateAuthentication, this.userResultMapping.CallContext.RequestedVersion, false);
			UserConfigurationSettings userConfigurationSettings = userSettingsProvider.GetRedirectionOrErrorSettings();
			if (userConfigurationSettings == null)
			{
				userConfigurationSettings = userSettingsProvider.GetUserSettings(this.userResultMapping.CallContext.RequestedSettings, budget);
			}
			UserResponse userResponse;
			if (userConfigurationSettings.ErrorCode == UserConfigurationSettingsErrorCode.NoError)
			{
				userResponse = new UserResponse();
				userResponse.ErrorCode = ErrorCode.NoError;
				userResponse.ErrorMessage = Strings.NoError;
				userResponse.UserSettingErrors = this.userResultMapping.CallContext.SettingErrors;
				userResponse.UserSettings = this.RenderUserSettings(userConfigurationSettings);
			}
			else
			{
				userResponse = ResultBase.GenerateUserResponseError(userConfigurationSettings, this.userResultMapping.CallContext.SettingErrors);
			}
			return userResponse;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00014A04 File Offset: 0x00012C04
		private UserSettingCollection RenderUserSettings(UserConfigurationSettings settings)
		{
			UserSettingCollection userSettingCollection = new UserSettingCollection();
			foreach (UserConfigurationSettingName userConfigurationSettingName in settings.Keys)
			{
				object obj = settings.Get<object>(userConfigurationSettingName);
				if (obj is string)
				{
					userSettingCollection.Add(new StringSetting
					{
						Name = userConfigurationSettingName.ToString(),
						Value = (string)obj
					});
				}
				else if (obj is bool)
				{
					userSettingCollection.Add(new StringSetting
					{
						Name = userConfigurationSettingName.ToString(),
						Value = obj.ToString()
					});
				}
				else if (obj is OwaUrlCollection)
				{
					WebClientUrlCollectionSetting webClientUrlCollectionSetting = new WebClientUrlCollectionSetting();
					webClientUrlCollectionSetting.Name = userConfigurationSettingName.ToString();
					webClientUrlCollectionSetting.WebClientUrls = new WebClientUrlCollection();
					OwaUrlCollection owaUrlCollection = obj as OwaUrlCollection;
					foreach (OwaUrl owaUrl in owaUrlCollection)
					{
						webClientUrlCollectionSetting.WebClientUrls.Add(new WebClientUrl
						{
							AuthenticationMethods = owaUrl.AuthenticationMethods,
							Url = owaUrl.Url
						});
					}
					userSettingCollection.Add(webClientUrlCollectionSetting);
				}
				else if (obj is PopImapSmtpConnectionCollection)
				{
					ProtocolConnectionCollectionSetting protocolConnectionCollectionSetting = new ProtocolConnectionCollectionSetting();
					protocolConnectionCollectionSetting.Name = userConfigurationSettingName.ToString();
					protocolConnectionCollectionSetting.ProtocolConnections = new ProtocolConnectionCollection();
					PopImapSmtpConnectionCollection popImapSmtpConnectionCollection = obj as PopImapSmtpConnectionCollection;
					foreach (PopImapSmtpConnection popImapSmtpConnection in popImapSmtpConnectionCollection)
					{
						protocolConnectionCollectionSetting.ProtocolConnections.Add(new ProtocolConnection
						{
							EncryptionMethod = popImapSmtpConnection.EncryptionMethod,
							Hostname = popImapSmtpConnection.Hostname,
							Port = popImapSmtpConnection.Port
						});
					}
					userSettingCollection.Add(protocolConnectionCollectionSetting);
				}
				else if (obj is AlternateMailboxCollection)
				{
					AlternateMailboxCollectionSetting alternateMailboxCollectionSetting = new AlternateMailboxCollectionSetting();
					alternateMailboxCollectionSetting.Name = userConfigurationSettingName.ToString();
					alternateMailboxCollectionSetting.AlternateMailboxes = new AlternateMailboxCollection();
					AlternateMailboxCollection alternateMailboxCollection = obj as AlternateMailboxCollection;
					foreach (Microsoft.Exchange.Autodiscover.ConfigurationSettings.AlternateMailbox alternateMailbox in alternateMailboxCollection)
					{
						alternateMailboxCollectionSetting.AlternateMailboxes.Add(new Microsoft.Exchange.Autodiscover.WCF.AlternateMailbox
						{
							Type = alternateMailbox.Type,
							DisplayName = alternateMailbox.DisplayName,
							LegacyDN = alternateMailbox.LegacyDN,
							Server = alternateMailbox.Server,
							SmtpAddress = alternateMailbox.SmtpAddress,
							OwnerSmtpAddress = alternateMailbox.OwnerSmtpAddress
						});
					}
					userSettingCollection.Add(alternateMailboxCollectionSetting);
				}
				else if (obj is DocumentSharingLocationCollection)
				{
					DocumentSharingLocationCollectionSetting documentSharingLocationCollectionSetting = new DocumentSharingLocationCollectionSetting();
					documentSharingLocationCollectionSetting.Name = userConfigurationSettingName.ToString();
					documentSharingLocationCollectionSetting.DocumentSharingLocations = new DocumentSharingLocationCollection();
					DocumentSharingLocationCollection documentSharingLocationCollection = obj as DocumentSharingLocationCollection;
					foreach (DocumentSharingLocation documentSharingLocation in documentSharingLocationCollection)
					{
						DocumentSharingLocation item = new DocumentSharingLocation(documentSharingLocation.ServiceUrl, documentSharingLocation.LocationUrl, documentSharingLocation.DisplayName, documentSharingLocation.SupportedFileExtensions, documentSharingLocation.ExternalAccessAllowed, documentSharingLocation.AnonymousAccessAllowed, documentSharingLocation.CanModifyPermissions, documentSharingLocation.IsDefault);
						documentSharingLocationCollectionSetting.DocumentSharingLocations.Add(item);
					}
					userSettingCollection.Add(documentSharingLocationCollectionSetting);
				}
				else if (!(obj is MapiHttpProtocolUrls))
				{
				}
			}
			return userSettingCollection;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00014E38 File Offset: 0x00013038
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00014E40 File Offset: 0x00013040
		internal Result<ADRecipient> Result { get; set; }
	}
}
