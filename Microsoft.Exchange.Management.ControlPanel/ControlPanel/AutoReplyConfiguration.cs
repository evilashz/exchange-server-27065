using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.InfoWorker.Common.OOF;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200025D RID: 605
	[DataContract]
	public class AutoReplyConfiguration : BaseRow
	{
		// Token: 0x060028E1 RID: 10465 RVA: 0x00080DE5 File Offset: 0x0007EFE5
		public AutoReplyConfiguration(MailboxAutoReplyConfiguration mailboxAutoReplyConfiguration) : base(mailboxAutoReplyConfiguration)
		{
			this.MailboxAutoReplyConfiguration = mailboxAutoReplyConfiguration;
		}

		// Token: 0x17001C79 RID: 7289
		// (get) Token: 0x060028E2 RID: 10466 RVA: 0x00080DF5 File Offset: 0x0007EFF5
		// (set) Token: 0x060028E3 RID: 10467 RVA: 0x00080DFD File Offset: 0x0007EFFD
		public MailboxAutoReplyConfiguration MailboxAutoReplyConfiguration { get; private set; }

		// Token: 0x17001C7A RID: 7290
		// (get) Token: 0x060028E4 RID: 10468 RVA: 0x00080E06 File Offset: 0x0007F006
		// (set) Token: 0x060028E5 RID: 10469 RVA: 0x00080E21 File Offset: 0x0007F021
		[DataMember]
		public string AutoReplyStateDisabled
		{
			get
			{
				return (this.MailboxAutoReplyConfiguration.AutoReplyState == OofState.Disabled).ToJsonString(null);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C7B RID: 7291
		// (get) Token: 0x060028E6 RID: 10470 RVA: 0x00080E28 File Offset: 0x0007F028
		// (set) Token: 0x060028E7 RID: 10471 RVA: 0x00080E38 File Offset: 0x0007F038
		[DataMember]
		public bool AutoReplyStateScheduled
		{
			get
			{
				return this.MailboxAutoReplyConfiguration.AutoReplyState == OofState.Scheduled;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C7C RID: 7292
		// (get) Token: 0x060028E8 RID: 10472 RVA: 0x00080E3F File Offset: 0x0007F03F
		// (set) Token: 0x060028E9 RID: 10473 RVA: 0x00080E51 File Offset: 0x0007F051
		[DataMember]
		public string StartTime
		{
			get
			{
				return this.MailboxAutoReplyConfiguration.StartTime.LocalToUserDateTimeGeneralFormatString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C7D RID: 7293
		// (get) Token: 0x060028EA RID: 10474 RVA: 0x00080E58 File Offset: 0x0007F058
		// (set) Token: 0x060028EB RID: 10475 RVA: 0x00080E6A File Offset: 0x0007F06A
		[DataMember]
		public string EndTime
		{
			get
			{
				return this.MailboxAutoReplyConfiguration.EndTime.LocalToUserDateTimeGeneralFormatString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C7E RID: 7294
		// (get) Token: 0x060028EC RID: 10476 RVA: 0x00080E71 File Offset: 0x0007F071
		// (set) Token: 0x060028ED RID: 10477 RVA: 0x00080E83 File Offset: 0x0007F083
		[DataMember]
		public string InternalMessage
		{
			get
			{
				return TextConverterHelper.SanitizeHtml(this.MailboxAutoReplyConfiguration.InternalMessage);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C7F RID: 7295
		// (get) Token: 0x060028EE RID: 10478 RVA: 0x00080E8A File Offset: 0x0007F08A
		// (set) Token: 0x060028EF RID: 10479 RVA: 0x00080E9D File Offset: 0x0007F09D
		[DataMember]
		public bool ExternalAudience
		{
			get
			{
				return this.MailboxAutoReplyConfiguration.ExternalAudience != Microsoft.Exchange.InfoWorker.Common.OOF.ExternalAudience.None;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C80 RID: 7296
		// (get) Token: 0x060028F0 RID: 10480 RVA: 0x00080EA4 File Offset: 0x0007F0A4
		// (set) Token: 0x060028F1 RID: 10481 RVA: 0x00080EBF File Offset: 0x0007F0BF
		[DataMember]
		public string ExternalAudienceKnownOnly
		{
			get
			{
				return (this.MailboxAutoReplyConfiguration.ExternalAudience == Microsoft.Exchange.InfoWorker.Common.OOF.ExternalAudience.Known).ToJsonString(null);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C81 RID: 7297
		// (get) Token: 0x060028F2 RID: 10482 RVA: 0x00080EC6 File Offset: 0x0007F0C6
		// (set) Token: 0x060028F3 RID: 10483 RVA: 0x00080ED8 File Offset: 0x0007F0D8
		[DataMember]
		public string ExternalMessage
		{
			get
			{
				return TextConverterHelper.SanitizeHtml(this.MailboxAutoReplyConfiguration.ExternalMessage);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
