using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002BA RID: 698
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RecipientPropertyIsNotWriteablePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002367 RID: 9063 RVA: 0x0004E81D File Offset: 0x0004CA1D
		public RecipientPropertyIsNotWriteablePermanentException(string recipient, string propertyName) : base(MrsStrings.RecipientPropertyIsNotWriteable(recipient, propertyName))
		{
			this.recipient = recipient;
			this.propertyName = propertyName;
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x0004E83A File Offset: 0x0004CA3A
		public RecipientPropertyIsNotWriteablePermanentException(string recipient, string propertyName, Exception innerException) : base(MrsStrings.RecipientPropertyIsNotWriteable(recipient, propertyName), innerException)
		{
			this.recipient = recipient;
			this.propertyName = propertyName;
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x0004E858 File Offset: 0x0004CA58
		protected RecipientPropertyIsNotWriteablePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipient = (string)info.GetValue("recipient", typeof(string));
			this.propertyName = (string)info.GetValue("propertyName", typeof(string));
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x0004E8AD File Offset: 0x0004CAAD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipient", this.recipient);
			info.AddValue("propertyName", this.propertyName);
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x0600236B RID: 9067 RVA: 0x0004E8D9 File Offset: 0x0004CAD9
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x0004E8E1 File Offset: 0x0004CAE1
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x04000FC8 RID: 4040
		private readonly string recipient;

		// Token: 0x04000FC9 RID: 4041
		private readonly string propertyName;
	}
}
