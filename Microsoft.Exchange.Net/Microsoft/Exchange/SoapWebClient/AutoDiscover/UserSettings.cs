using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x020006E3 RID: 1763
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class UserSettings
	{
		// Token: 0x06002174 RID: 8564 RVA: 0x00041E27 File Offset: 0x00040027
		public UserSettings(UserResponse userResponse)
		{
			this.userResponse = userResponse;
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x00041E38 File Offset: 0x00040038
		public bool IsSettingError(string settingsName)
		{
			if (this.userResponse.UserSettingErrors != null)
			{
				foreach (UserSettingError userSettingError in this.userResponse.UserSettingErrors)
				{
					if (userSettingError != null && StringComparer.Ordinal.Equals(userSettingError.SettingName, settingsName) && userSettingError.ErrorCode != ErrorCode.NoError)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x00041E98 File Offset: 0x00040098
		public UserSetting GetSetting(string settingsName)
		{
			if (this.userResponse.UserSettings != null)
			{
				foreach (UserSetting userSetting in this.userResponse.UserSettings)
				{
					if (userSetting != null && StringComparer.Ordinal.Equals(userSetting.Name, settingsName))
					{
						return userSetting;
					}
				}
			}
			return null;
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x00041EF0 File Offset: 0x000400F0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(1000);
			if (this.userResponse != null)
			{
				if (this.userResponse.UserSettingErrors != null)
				{
					foreach (UserSettingError userSettingError in this.userResponse.UserSettingErrors)
					{
						if (userSettingError != null && userSettingError.ErrorCode != ErrorCode.NoError)
						{
							stringBuilder.AppendLine(string.Format("Error:{0}:{1}:{2}", userSettingError.SettingName, userSettingError.ErrorCode, userSettingError.ErrorMessage));
						}
					}
				}
				if (this.userResponse.UserSettings != null)
				{
					foreach (UserSetting userSetting in this.userResponse.UserSettings)
					{
						if (userSetting != null)
						{
							stringBuilder.AppendLine(userSetting.Name + "={" + UserSettings.GetUserSettingValue(userSetting) + "}");
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x00041FD4 File Offset: 0x000401D4
		private static string GetUserSettingValue(UserSetting userSetting)
		{
			string userSettingValue = UserSettings.GetUserSettingValue(userSetting as StringSetting);
			if (userSettingValue != null)
			{
				return userSettingValue;
			}
			userSettingValue = UserSettings.GetUserSettingValue(userSetting as AlternateMailboxCollectionSetting);
			if (userSettingValue != null)
			{
				return userSettingValue;
			}
			userSettingValue = UserSettings.GetUserSettingValue(userSetting as WebClientUrlCollectionSetting);
			if (userSettingValue != null)
			{
				return userSettingValue;
			}
			userSettingValue = UserSettings.GetUserSettingValue(userSetting as ProtocolConnectionCollectionSetting);
			if (userSettingValue != null)
			{
				return userSettingValue;
			}
			return "<unknown:" + userSetting.GetType().Name + ">";
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x0004203F File Offset: 0x0004023F
		private static string GetUserSettingValue(StringSetting setting)
		{
			if (setting != null)
			{
				return "string:" + setting.Value;
			}
			return null;
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x00042058 File Offset: 0x00040258
		private static string GetUserSettingValue(AlternateMailboxCollectionSetting setting)
		{
			if (setting != null)
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.AppendLine("AlternateMailboxCollection: ");
				foreach (AlternateMailbox alternateMailbox in setting.AlternateMailboxes)
				{
					stringBuilder.AppendLine(string.Format("Type={0}, DisplayName={1}, LegacyDN={2}, Server={3}, SmtpAddress={4}, OwnerSmtpAddress={5}", new object[]
					{
						alternateMailbox.Type ?? "<null>",
						alternateMailbox.DisplayName ?? "<null>",
						alternateMailbox.LegacyDN ?? "<null>",
						alternateMailbox.Server ?? "<null>",
						alternateMailbox.SmtpAddress ?? "<null>",
						alternateMailbox.OwnerSmtpAddress ?? "<null>"
					}));
				}
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x00042134 File Offset: 0x00040334
		private static string GetUserSettingValue(WebClientUrlCollectionSetting setting)
		{
			if (setting != null)
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.AppendLine("WebClientUrlCollection: ");
				foreach (WebClientUrl webClientUrl in setting.WebClientUrls)
				{
					stringBuilder.AppendLine(string.Format("AuthenticationMethods={0}, Url={1}", webClientUrl.AuthenticationMethods ?? "<null>", webClientUrl.Url ?? "<null>"));
				}
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x000421A8 File Offset: 0x000403A8
		private static string GetUserSettingValue(ProtocolConnectionCollectionSetting setting)
		{
			if (setting != null)
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.AppendLine("ProtocolConnectionCollection: ");
				foreach (ProtocolConnection protocolConnection in setting.ProtocolConnections)
				{
					stringBuilder.AppendLine(string.Format("Hostname={0}, Port={1}, EncryptionMethod={2}", protocolConnection.Hostname ?? "<null>", protocolConnection.Port, protocolConnection.EncryptionMethod ?? "<null>"));
				}
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x04001F8A RID: 8074
		private UserResponse userResponse;
	}
}
