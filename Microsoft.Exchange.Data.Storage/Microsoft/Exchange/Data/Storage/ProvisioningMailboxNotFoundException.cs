using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000105 RID: 261
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ProvisioningMailboxNotFoundException : ObjectNotFoundException
	{
		// Token: 0x060013A2 RID: 5026 RVA: 0x000698D4 File Offset: 0x00067AD4
		public ProvisioningMailboxNotFoundException(string mailboxId) : base(ServerStrings.ProvisioningMailboxNotFound(mailboxId))
		{
			this.mailboxId = mailboxId;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x000698E9 File Offset: 0x00067AE9
		public ProvisioningMailboxNotFoundException(string mailboxId, Exception innerException) : base(ServerStrings.ProvisioningMailboxNotFound(mailboxId), innerException)
		{
			this.mailboxId = mailboxId;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x000698FF File Offset: 0x00067AFF
		protected ProvisioningMailboxNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxId = (string)info.GetValue("mailboxId", typeof(string));
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00069929 File Offset: 0x00067B29
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxId", this.mailboxId);
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x00069944 File Offset: 0x00067B44
		public string MailboxId
		{
			get
			{
				return this.mailboxId;
			}
		}

		// Token: 0x04000995 RID: 2453
		private readonly string mailboxId;
	}
}
