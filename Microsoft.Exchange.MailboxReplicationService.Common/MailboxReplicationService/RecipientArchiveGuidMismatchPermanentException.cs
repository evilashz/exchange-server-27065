using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002B8 RID: 696
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RecipientArchiveGuidMismatchPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002359 RID: 9049 RVA: 0x0004E5C1 File Offset: 0x0004C7C1
		public RecipientArchiveGuidMismatchPermanentException(string recipient, Guid recipientArchiveGuid, Guid targetArchiveGuid) : base(MrsStrings.RecipientArchiveGuidMismatch(recipient, recipientArchiveGuid, targetArchiveGuid))
		{
			this.recipient = recipient;
			this.recipientArchiveGuid = recipientArchiveGuid;
			this.targetArchiveGuid = targetArchiveGuid;
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x0004E5E6 File Offset: 0x0004C7E6
		public RecipientArchiveGuidMismatchPermanentException(string recipient, Guid recipientArchiveGuid, Guid targetArchiveGuid, Exception innerException) : base(MrsStrings.RecipientArchiveGuidMismatch(recipient, recipientArchiveGuid, targetArchiveGuid), innerException)
		{
			this.recipient = recipient;
			this.recipientArchiveGuid = recipientArchiveGuid;
			this.targetArchiveGuid = targetArchiveGuid;
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x0004E610 File Offset: 0x0004C810
		protected RecipientArchiveGuidMismatchPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipient = (string)info.GetValue("recipient", typeof(string));
			this.recipientArchiveGuid = (Guid)info.GetValue("recipientArchiveGuid", typeof(Guid));
			this.targetArchiveGuid = (Guid)info.GetValue("targetArchiveGuid", typeof(Guid));
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x0004E688 File Offset: 0x0004C888
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipient", this.recipient);
			info.AddValue("recipientArchiveGuid", this.recipientArchiveGuid);
			info.AddValue("targetArchiveGuid", this.targetArchiveGuid);
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x0600235D RID: 9053 RVA: 0x0004E6DA File Offset: 0x0004C8DA
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x0004E6E2 File Offset: 0x0004C8E2
		public Guid RecipientArchiveGuid
		{
			get
			{
				return this.recipientArchiveGuid;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x0600235F RID: 9055 RVA: 0x0004E6EA File Offset: 0x0004C8EA
		public Guid TargetArchiveGuid
		{
			get
			{
				return this.targetArchiveGuid;
			}
		}

		// Token: 0x04000FC2 RID: 4034
		private readonly string recipient;

		// Token: 0x04000FC3 RID: 4035
		private readonly Guid recipientArchiveGuid;

		// Token: 0x04000FC4 RID: 4036
		private readonly Guid targetArchiveGuid;
	}
}
