using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000187 RID: 391
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationRecipientNotFoundException : MigrationPermanentException
	{
		// Token: 0x060016F8 RID: 5880 RVA: 0x0006FFF0 File Offset: 0x0006E1F0
		public MigrationRecipientNotFoundException(string mailboxName) : base(Strings.MigrationRecipientNotFound(mailboxName))
		{
			this.mailboxName = mailboxName;
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00070005 File Offset: 0x0006E205
		public MigrationRecipientNotFoundException(string mailboxName, Exception innerException) : base(Strings.MigrationRecipientNotFound(mailboxName), innerException)
		{
			this.mailboxName = mailboxName;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0007001B File Offset: 0x0006E21B
		protected MigrationRecipientNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxName = (string)info.GetValue("mailboxName", typeof(string));
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00070045 File Offset: 0x0006E245
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxName", this.mailboxName);
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x00070060 File Offset: 0x0006E260
		public string MailboxName
		{
			get
			{
				return this.mailboxName;
			}
		}

		// Token: 0x04000B04 RID: 2820
		private readonly string mailboxName;
	}
}
