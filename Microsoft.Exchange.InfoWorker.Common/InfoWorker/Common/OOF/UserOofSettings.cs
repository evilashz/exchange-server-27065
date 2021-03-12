using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x02000022 RID: 34
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class UserOofSettings
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00004100 File Offset: 0x00002300
		public static UserOofSettings CreateDefault()
		{
			return new UserOofSettings
			{
				internalReply = ReplyBody.CreateDefault(),
				externalReply = ReplyBody.CreateDefault(),
				oofState = OofState.Disabled,
				setByLegacyClient = false,
				externalAudience = ExternalAudience.None
			};
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000413F File Offset: 0x0000233F
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00004147 File Offset: 0x00002347
		public OofState OofState
		{
			get
			{
				return this.oofState;
			}
			set
			{
				this.oofState = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00004150 File Offset: 0x00002350
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00004158 File Offset: 0x00002358
		public ExternalAudience ExternalAudience
		{
			get
			{
				return this.externalAudience;
			}
			set
			{
				this.externalAudience = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00004161 File Offset: 0x00002361
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00004169 File Offset: 0x00002369
		public Duration Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				this.duration = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00004172 File Offset: 0x00002372
		// (set) Token: 0x06000089 RID: 137 RVA: 0x0000417A File Offset: 0x0000237A
		public ReplyBody InternalReply
		{
			get
			{
				return this.internalReply;
			}
			set
			{
				this.internalReply = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00004183 File Offset: 0x00002383
		// (set) Token: 0x0600008B RID: 139 RVA: 0x0000418B File Offset: 0x0000238B
		public ReplyBody ExternalReply
		{
			get
			{
				return this.externalReply;
			}
			set
			{
				this.externalReply = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004194 File Offset: 0x00002394
		// (set) Token: 0x0600008D RID: 141 RVA: 0x0000419C File Offset: 0x0000239C
		[XmlIgnore]
		public bool SetByLegacyClient
		{
			get
			{
				return this.setByLegacyClient;
			}
			set
			{
				this.setByLegacyClient = value;
				this.ExternalReply.SetByLegacyClient = value;
				this.InternalReply.SetByLegacyClient = value;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000041C0 File Offset: 0x000023C0
		internal static ExternalAudience GetUserPolicy(IExchangePrincipal mailboxOwner)
		{
			if (mailboxOwner == null)
			{
				throw new ArgumentNullException("mailboxOwner");
			}
			ExternalAudience result = ExternalAudience.All;
			try
			{
				result = UserOofSettingsStorage.GetUserPolicy(mailboxOwner);
			}
			catch (ADPossibleOperationException arg)
			{
				UserOofSettings.Tracer.TraceError<string, ADPossibleOperationException>(0L, "Mailbox:{0}: Exception while getting user policy, exception = {1}.", mailboxOwner.MailboxInfo.DisplayName, arg);
			}
			catch (MailStorageNotFoundException arg2)
			{
				UserOofSettings.Tracer.TraceError<string, MailStorageNotFoundException>(0L, "Mailbox:{0}: Exception while getting user policy, exception = {1}.", mailboxOwner.MailboxInfo.DisplayName, arg2);
			}
			return result;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004244 File Offset: 0x00002444
		internal static UserOofSettings Create()
		{
			return new UserOofSettings();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000424C File Offset: 0x0000244C
		internal static string OofLog(MailboxSession itemStore)
		{
			string itemContent = UserOofSettings.OofLogStore.GetItemContent(itemStore);
			if (itemContent == null)
			{
				return "empty oof log";
			}
			return itemContent;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000426F File Offset: 0x0000246F
		internal static UserOofSettings GetUserOofSettings(MailboxSession itemStore)
		{
			return UserOofSettingsStorage.LoadUserOofSettings(itemStore);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004277 File Offset: 0x00002477
		// (set) Token: 0x06000093 RID: 147 RVA: 0x0000427F File Offset: 0x0000247F
		internal DateTime? UserChangeTime
		{
			get
			{
				return this.userChangeTime;
			}
			set
			{
				this.userChangeTime = value;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004288 File Offset: 0x00002488
		internal void Save(MailboxSession itemStore)
		{
			UserOofSettings.Validate(itemStore, this);
			this.setByLegacyClient = false;
			this.userChangeTime = new DateTime?(DateTime.MaxValue);
			UserOofSettingsStorage.SaveUserOofSettings(itemStore, this);
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000042AF File Offset: 0x000024AF
		internal bool Scheduled
		{
			get
			{
				return this.oofState == OofState.Scheduled;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000042BA File Offset: 0x000024BA
		internal DateTime EndTime
		{
			get
			{
				return this.duration.EndTime;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000042C7 File Offset: 0x000024C7
		internal bool GlobalOofEnabled(MailboxSession itemStore)
		{
			return OofStateHandler.Get(itemStore);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000042CF File Offset: 0x000024CF
		internal DateTime StartTime
		{
			get
			{
				return this.duration.StartTime;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000042DC File Offset: 0x000024DC
		private static void ValidateDuration(Duration duration)
		{
			if (duration == null || duration.EndTime <= duration.StartTime || duration.EndTime <= DateTime.UtcNow || duration.StartTime.Kind != DateTimeKind.Utc || duration.EndTime.Kind != DateTimeKind.Utc)
			{
				throw new InvalidScheduledOofDuration();
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000433C File Offset: 0x0000253C
		private static void Validate(MailboxSession itemStore, UserOofSettings userOofSettings)
		{
			if (userOofSettings.internalReply == null || userOofSettings.externalReply == null)
			{
				throw new InvalidUserOofSettings();
			}
			if (userOofSettings.OofState == OofState.Scheduled)
			{
				UserOofSettings.ValidateDuration(userOofSettings.duration);
			}
			if ((userOofSettings.ExternalAudience == ExternalAudience.Known || userOofSettings.ExternalAudience == ExternalAudience.All) && UserOofSettings.GetUserPolicy(itemStore.MailboxOwner) == ExternalAudience.None)
			{
				userOofSettings.ExternalAudience = ExternalAudience.None;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000439B File Offset: 0x0000259B
		private UserOofSettings()
		{
		}

		// Token: 0x0400004D RID: 77
		private ReplyBody internalReply;

		// Token: 0x0400004E RID: 78
		private ReplyBody externalReply;

		// Token: 0x0400004F RID: 79
		private Duration duration;

		// Token: 0x04000050 RID: 80
		private OofState oofState;

		// Token: 0x04000051 RID: 81
		private bool setByLegacyClient;

		// Token: 0x04000052 RID: 82
		private ExternalAudience externalAudience;

		// Token: 0x04000053 RID: 83
		private DateTime? userChangeTime = null;

		// Token: 0x04000054 RID: 84
		private static readonly SingleInstanceItemHandler OofLogStore = new SingleInstanceItemHandler("IPM.Microsoft.OOF.Log", DefaultFolderType.Configuration);

		// Token: 0x04000055 RID: 85
		private static readonly Trace Tracer = ExTraceGlobals.OOFTracer;
	}
}
