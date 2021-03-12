using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.InfoWorker.Common.OOF;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020007BD RID: 1981
	[Serializable]
	public class MailboxAutoReplyConfiguration : XsoMailboxConfigurationObject
	{
		// Token: 0x17001509 RID: 5385
		// (get) Token: 0x06004596 RID: 17814 RVA: 0x0011E017 File Offset: 0x0011C217
		internal override XsoMailboxConfigurationObjectSchema Schema
		{
			get
			{
				return MailboxAutoReplyConfiguration.schema;
			}
		}

		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x06004597 RID: 17815 RVA: 0x0011E01E File Offset: 0x0011C21E
		// (set) Token: 0x06004598 RID: 17816 RVA: 0x0011E030 File Offset: 0x0011C230
		[Parameter(Mandatory = false)]
		public OofState AutoReplyState
		{
			get
			{
				return (OofState)this[MailboxAutoReplyConfigurationSchema.AutoReplyState];
			}
			set
			{
				this[MailboxAutoReplyConfigurationSchema.AutoReplyState] = value;
			}
		}

		// Token: 0x1700150B RID: 5387
		// (get) Token: 0x06004599 RID: 17817 RVA: 0x0011E043 File Offset: 0x0011C243
		// (set) Token: 0x0600459A RID: 17818 RVA: 0x0011E055 File Offset: 0x0011C255
		[Parameter(Mandatory = false)]
		public DateTime EndTime
		{
			get
			{
				return (DateTime)this[MailboxAutoReplyConfigurationSchema.EndTime];
			}
			set
			{
				this[MailboxAutoReplyConfigurationSchema.EndTime] = value;
			}
		}

		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x0600459B RID: 17819 RVA: 0x0011E068 File Offset: 0x0011C268
		// (set) Token: 0x0600459C RID: 17820 RVA: 0x0011E07A File Offset: 0x0011C27A
		[Parameter(Mandatory = false)]
		public ExternalAudience ExternalAudience
		{
			get
			{
				return (ExternalAudience)this[MailboxAutoReplyConfigurationSchema.ExternalAudience];
			}
			set
			{
				this[MailboxAutoReplyConfigurationSchema.ExternalAudience] = value;
			}
		}

		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x0600459D RID: 17821 RVA: 0x0011E08D File Offset: 0x0011C28D
		// (set) Token: 0x0600459E RID: 17822 RVA: 0x0011E09F File Offset: 0x0011C29F
		[Parameter(Mandatory = false)]
		public string ExternalMessage
		{
			get
			{
				return (string)this[MailboxAutoReplyConfigurationSchema.ExternalMessage];
			}
			set
			{
				this[MailboxAutoReplyConfigurationSchema.ExternalMessage] = value;
			}
		}

		// Token: 0x1700150E RID: 5390
		// (get) Token: 0x0600459F RID: 17823 RVA: 0x0011E0AD File Offset: 0x0011C2AD
		// (set) Token: 0x060045A0 RID: 17824 RVA: 0x0011E0BF File Offset: 0x0011C2BF
		[Parameter(Mandatory = false)]
		public string InternalMessage
		{
			get
			{
				return (string)this[MailboxAutoReplyConfigurationSchema.InternalMessage];
			}
			set
			{
				this[MailboxAutoReplyConfigurationSchema.InternalMessage] = value;
			}
		}

		// Token: 0x1700150F RID: 5391
		// (get) Token: 0x060045A1 RID: 17825 RVA: 0x0011E0CD File Offset: 0x0011C2CD
		// (set) Token: 0x060045A2 RID: 17826 RVA: 0x0011E0DF File Offset: 0x0011C2DF
		[Parameter(Mandatory = false)]
		public DateTime StartTime
		{
			get
			{
				return (DateTime)this[MailboxAutoReplyConfigurationSchema.StartTime];
			}
			set
			{
				this[MailboxAutoReplyConfigurationSchema.StartTime] = value;
			}
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x0011E0F4 File Offset: 0x0011C2F4
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.AutoReplyState == OofState.Scheduled)
			{
				if (this.StartTime.ToUniversalTime() >= this.EndTime.ToUniversalTime())
				{
					errors.Add(new PropertyValidationError(Strings.ErrorEndTimeSmallerThanStartTime, MailboxAutoReplyConfigurationSchema.EndTime, this.EndTime));
				}
				if (this.EndTime.ToUniversalTime() <= DateTime.UtcNow)
				{
					errors.Add(new PropertyValidationError(Strings.ErrorEndTimeSmallerThanNow, MailboxAutoReplyConfigurationSchema.EndTime, this.EndTime));
				}
			}
		}

		// Token: 0x04002AE8 RID: 10984
		internal const int MaxAutoReplySize = 128000;

		// Token: 0x04002AE9 RID: 10985
		private static MailboxAutoReplyConfigurationSchema schema = ObjectSchema.GetInstance<MailboxAutoReplyConfigurationSchema>();
	}
}
