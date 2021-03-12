using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000106 RID: 262
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MultipleProvisioningMailboxesException : LocalizedException
	{
		// Token: 0x060013A7 RID: 5031 RVA: 0x0006994C File Offset: 0x00067B4C
		public MultipleProvisioningMailboxesException(string mailboxId) : base(ServerStrings.MultipleProvisioningMailboxes(mailboxId))
		{
			this.mailboxId = mailboxId;
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x00069961 File Offset: 0x00067B61
		public MultipleProvisioningMailboxesException(string mailboxId, Exception innerException) : base(ServerStrings.MultipleProvisioningMailboxes(mailboxId), innerException)
		{
			this.mailboxId = mailboxId;
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00069977 File Offset: 0x00067B77
		protected MultipleProvisioningMailboxesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxId = (string)info.GetValue("mailboxId", typeof(string));
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x000699A1 File Offset: 0x00067BA1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxId", this.mailboxId);
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x000699BC File Offset: 0x00067BBC
		public string MailboxId
		{
			get
			{
				return this.mailboxId;
			}
		}

		// Token: 0x04000996 RID: 2454
		private readonly string mailboxId;
	}
}
