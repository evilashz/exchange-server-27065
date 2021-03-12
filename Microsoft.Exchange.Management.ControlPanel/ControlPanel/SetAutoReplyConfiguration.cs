using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.InfoWorker.Common.OOF;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200025E RID: 606
	[DataContract]
	public class SetAutoReplyConfiguration : SetObjectProperties
	{
		// Token: 0x17001C82 RID: 7298
		// (get) Token: 0x060028F4 RID: 10484 RVA: 0x00080EDF File Offset: 0x0007F0DF
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-MailboxAutoReplyConfiguration";
			}
		}

		// Token: 0x17001C83 RID: 7299
		// (get) Token: 0x060028F5 RID: 10485 RVA: 0x00080EE6 File Offset: 0x0007F0E6
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x17001C84 RID: 7300
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x00080EED File Offset: 0x0007F0ED
		// (set) Token: 0x060028F7 RID: 10487 RVA: 0x00080EF5 File Offset: 0x0007F0F5
		[DataMember]
		public string AutoReplyStateDisabled { get; set; }

		// Token: 0x17001C85 RID: 7301
		// (get) Token: 0x060028F8 RID: 10488 RVA: 0x00080EFE File Offset: 0x0007F0FE
		// (set) Token: 0x060028F9 RID: 10489 RVA: 0x00080F06 File Offset: 0x0007F106
		[DataMember]
		public bool AutoReplyStateScheduled { get; set; }

		// Token: 0x17001C86 RID: 7302
		// (get) Token: 0x060028FA RID: 10490 RVA: 0x00080F0F File Offset: 0x0007F10F
		// (set) Token: 0x060028FB RID: 10491 RVA: 0x00080F17 File Offset: 0x0007F117
		[DataMember]
		public string StartTime { get; set; }

		// Token: 0x17001C87 RID: 7303
		// (get) Token: 0x060028FC RID: 10492 RVA: 0x00080F20 File Offset: 0x0007F120
		// (set) Token: 0x060028FD RID: 10493 RVA: 0x00080F28 File Offset: 0x0007F128
		[DataMember]
		public string EndTime { get; set; }

		// Token: 0x17001C88 RID: 7304
		// (get) Token: 0x060028FE RID: 10494 RVA: 0x00080F31 File Offset: 0x0007F131
		// (set) Token: 0x060028FF RID: 10495 RVA: 0x00080F43 File Offset: 0x0007F143
		[DataMember]
		public string InternalMessage
		{
			get
			{
				return (string)base["InternalMessage"];
			}
			set
			{
				base["InternalMessage"] = value;
			}
		}

		// Token: 0x17001C89 RID: 7305
		// (get) Token: 0x06002900 RID: 10496 RVA: 0x00080F51 File Offset: 0x0007F151
		// (set) Token: 0x06002901 RID: 10497 RVA: 0x00080F59 File Offset: 0x0007F159
		[DataMember]
		public bool? ExternalAudience { get; set; }

		// Token: 0x17001C8A RID: 7306
		// (get) Token: 0x06002902 RID: 10498 RVA: 0x00080F62 File Offset: 0x0007F162
		// (set) Token: 0x06002903 RID: 10499 RVA: 0x00080F6A File Offset: 0x0007F16A
		[DataMember]
		public string ExternalAudienceKnownOnly { get; set; }

		// Token: 0x17001C8B RID: 7307
		// (get) Token: 0x06002904 RID: 10500 RVA: 0x00080F73 File Offset: 0x0007F173
		// (set) Token: 0x06002905 RID: 10501 RVA: 0x00080F85 File Offset: 0x0007F185
		[DataMember]
		public string ExternalMessage
		{
			get
			{
				return (string)base["ExternalMessage"];
			}
			set
			{
				base["ExternalMessage"] = value;
			}
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x00080F94 File Offset: 0x0007F194
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			bool flag;
			if (bool.TryParse(this.AutoReplyStateDisabled, out flag))
			{
				if (flag)
				{
					base["AutoReplyState"] = OofState.Disabled;
				}
				else
				{
					base["AutoReplyState"] = (this.AutoReplyStateScheduled ? OofState.Scheduled : OofState.Enabled);
				}
			}
			bool flag2;
			if (this.ExternalAudience != null && !this.ExternalAudience.Value)
			{
				base["ExternalAudience"] = Microsoft.Exchange.InfoWorker.Common.OOF.ExternalAudience.None;
			}
			else if (!this.ExternalAudienceKnownOnly.IsNullOrBlank() && bool.TryParse(this.ExternalAudienceKnownOnly, out flag2))
			{
				base["ExternalAudience"] = (flag2 ? Microsoft.Exchange.InfoWorker.Common.OOF.ExternalAudience.Known : Microsoft.Exchange.InfoWorker.Common.OOF.ExternalAudience.All);
			}
			if (!this.StartTime.IsNullOrBlank())
			{
				ExDateTime? exDateTime = this.StartTime.ToEcpExDateTime("yyyy/MM/dd HH:mm:ss");
				if (exDateTime != null)
				{
					base["StartTime"] = exDateTime.Value.UniversalTime;
				}
			}
			if (!this.EndTime.IsNullOrBlank())
			{
				ExDateTime? exDateTime2 = this.EndTime.ToEcpExDateTime("yyyy/MM/dd HH:mm:ss");
				if (exDateTime2 != null)
				{
					base["EndTime"] = exDateTime2.Value.UniversalTime;
				}
			}
		}
	}
}
