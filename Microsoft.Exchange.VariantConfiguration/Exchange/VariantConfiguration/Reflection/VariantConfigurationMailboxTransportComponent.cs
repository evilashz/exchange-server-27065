using System;
using Microsoft.Exchange.Flighting;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000110 RID: 272
	public sealed class VariantConfigurationMailboxTransportComponent : VariantConfigurationComponent
	{
		// Token: 0x06000C8A RID: 3210 RVA: 0x0001DFD8 File Offset: 0x0001C1D8
		internal VariantConfigurationMailboxTransportComponent() : base("MailboxTransport")
		{
			base.Add(new VariantConfigurationSection("MailboxTransport.settings.ini", "ParkedMeetingMessagesRetentionPeriod", typeof(ISettingsValue), false));
			base.Add(new VariantConfigurationSection("MailboxTransport.settings.ini", "MailboxTransportSmtpIn", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxTransport.settings.ini", "DeliveryHangRecovery", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxTransport.settings.ini", "InferenceClassificationAgent", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxTransport.settings.ini", "UseParticipantSmtpEmailAddress", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxTransport.settings.ini", "CheckArbitrationMailboxCapacity", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxTransport.settings.ini", "ProcessSeriesMeetingMessages", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxTransport.settings.ini", "UseFopeReceivedSpfHeader", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxTransport.settings.ini", "OrderSeriesMeetingMessages", typeof(IFeature), false));
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0001E110 File Offset: 0x0001C310
		public VariantConfigurationSection ParkedMeetingMessagesRetentionPeriod
		{
			get
			{
				return base["ParkedMeetingMessagesRetentionPeriod"];
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0001E11D File Offset: 0x0001C31D
		public VariantConfigurationSection MailboxTransportSmtpIn
		{
			get
			{
				return base["MailboxTransportSmtpIn"];
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0001E12A File Offset: 0x0001C32A
		public VariantConfigurationSection DeliveryHangRecovery
		{
			get
			{
				return base["DeliveryHangRecovery"];
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0001E137 File Offset: 0x0001C337
		public VariantConfigurationSection InferenceClassificationAgent
		{
			get
			{
				return base["InferenceClassificationAgent"];
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0001E144 File Offset: 0x0001C344
		public VariantConfigurationSection UseParticipantSmtpEmailAddress
		{
			get
			{
				return base["UseParticipantSmtpEmailAddress"];
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0001E151 File Offset: 0x0001C351
		public VariantConfigurationSection CheckArbitrationMailboxCapacity
		{
			get
			{
				return base["CheckArbitrationMailboxCapacity"];
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0001E15E File Offset: 0x0001C35E
		public VariantConfigurationSection ProcessSeriesMeetingMessages
		{
			get
			{
				return base["ProcessSeriesMeetingMessages"];
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0001E16B File Offset: 0x0001C36B
		public VariantConfigurationSection UseFopeReceivedSpfHeader
		{
			get
			{
				return base["UseFopeReceivedSpfHeader"];
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0001E178 File Offset: 0x0001C378
		public VariantConfigurationSection OrderSeriesMeetingMessages
		{
			get
			{
				return base["OrderSeriesMeetingMessages"];
			}
		}
	}
}
