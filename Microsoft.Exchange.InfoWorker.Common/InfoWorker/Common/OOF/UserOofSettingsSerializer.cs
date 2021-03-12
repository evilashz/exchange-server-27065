using System;
using System.Xml.Serialization;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x02000023 RID: 35
	[XmlRoot(ElementName = "UserOofSettings")]
	public class UserOofSettingsSerializer
	{
		// Token: 0x0600009D RID: 157 RVA: 0x000043CC File Offset: 0x000025CC
		internal static UserOofSettingsSerializer Serialize(UserOofSettings userOofSettings)
		{
			return new UserOofSettingsSerializer
			{
				InternalReply = ReplyBodySerializer.Serialize(userOofSettings.InternalReply),
				ExternalReply = ReplyBodySerializer.Serialize(userOofSettings.ExternalReply),
				Duration = userOofSettings.Duration,
				OofState = userOofSettings.OofState,
				ExternalAudience = userOofSettings.ExternalAudience,
				SetByLegacyClient = userOofSettings.SetByLegacyClient,
				UserChangeTime = userOofSettings.UserChangeTime
			};
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004440 File Offset: 0x00002640
		internal static UserOofSettingsSerializerSerializer Serializer
		{
			get
			{
				if (UserOofSettingsSerializer.serializer == null)
				{
					lock (UserOofSettingsSerializer.serializerLocker)
					{
						if (UserOofSettingsSerializer.serializer == null)
						{
							try
							{
								UserOofSettingsSerializer.serializer = new UserOofSettingsSerializerSerializer();
							}
							catch (InvalidOperationException innerException)
							{
								throw new IWTransientException(Strings.descCorruptUserOofSettingsXmlDocument, innerException);
							}
						}
					}
				}
				return UserOofSettingsSerializer.serializer;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000044B4 File Offset: 0x000026B4
		internal UserOofSettings Deserialize()
		{
			if (!this.Validate())
			{
				throw new InvalidUserOofSettingsXmlDocument();
			}
			UserOofSettings userOofSettings = UserOofSettings.Create();
			userOofSettings.InternalReply = this.InternalReply.Deserialize();
			userOofSettings.ExternalReply = this.ExternalReply.Deserialize();
			userOofSettings.Duration = this.Duration;
			userOofSettings.OofState = this.OofState;
			userOofSettings.ExternalAudience = this.ExternalAudience;
			userOofSettings.SetByLegacyClient = this.SetByLegacyClient;
			userOofSettings.UserChangeTime = this.UserChangeTime;
			return userOofSettings;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004534 File Offset: 0x00002734
		private bool Validate()
		{
			return this.InternalReply != null && this.ExternalReply != null;
		}

		// Token: 0x04000056 RID: 86
		[XmlElement]
		public Duration Duration;

		// Token: 0x04000057 RID: 87
		[XmlElement]
		public OofState OofState;

		// Token: 0x04000058 RID: 88
		[XmlElement]
		public ExternalAudience ExternalAudience;

		// Token: 0x04000059 RID: 89
		[XmlElement]
		public ReplyBodySerializer InternalReply;

		// Token: 0x0400005A RID: 90
		[XmlElement]
		public ReplyBodySerializer ExternalReply;

		// Token: 0x0400005B RID: 91
		[XmlElement]
		public bool SetByLegacyClient;

		// Token: 0x0400005C RID: 92
		[XmlElement]
		public DateTime? UserChangeTime = null;

		// Token: 0x0400005D RID: 93
		private static UserOofSettingsSerializerSerializer serializer;

		// Token: 0x0400005E RID: 94
		private static object serializerLocker = new object();
	}
}
