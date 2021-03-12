using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000FC1 RID: 4033
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RecipientTypeInvalidException : LocalizedException
	{
		// Token: 0x0600AD9B RID: 44443 RVA: 0x00291F11 File Offset: 0x00290111
		public RecipientTypeInvalidException(string mailboxId) : base(Strings.RecipientTypeInvalidException(mailboxId))
		{
			this.mailboxId = mailboxId;
		}

		// Token: 0x0600AD9C RID: 44444 RVA: 0x00291F26 File Offset: 0x00290126
		public RecipientTypeInvalidException(string mailboxId, Exception innerException) : base(Strings.RecipientTypeInvalidException(mailboxId), innerException)
		{
			this.mailboxId = mailboxId;
		}

		// Token: 0x0600AD9D RID: 44445 RVA: 0x00291F3C File Offset: 0x0029013C
		protected RecipientTypeInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxId = (string)info.GetValue("mailboxId", typeof(string));
		}

		// Token: 0x0600AD9E RID: 44446 RVA: 0x00291F66 File Offset: 0x00290166
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxId", this.mailboxId);
		}

		// Token: 0x170037B4 RID: 14260
		// (get) Token: 0x0600AD9F RID: 44447 RVA: 0x00291F81 File Offset: 0x00290181
		public string MailboxId
		{
			get
			{
				return this.mailboxId;
			}
		}

		// Token: 0x0400611A RID: 24858
		private readonly string mailboxId;
	}
}
