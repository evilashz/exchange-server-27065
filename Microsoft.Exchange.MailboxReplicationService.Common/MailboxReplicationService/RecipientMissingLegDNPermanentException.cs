using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002B5 RID: 693
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RecipientMissingLegDNPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600234A RID: 9034 RVA: 0x0004E459 File Offset: 0x0004C659
		public RecipientMissingLegDNPermanentException(string recipient) : base(MrsStrings.RecipientMissingLegDN(recipient))
		{
			this.recipient = recipient;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x0004E46E File Offset: 0x0004C66E
		public RecipientMissingLegDNPermanentException(string recipient, Exception innerException) : base(MrsStrings.RecipientMissingLegDN(recipient), innerException)
		{
			this.recipient = recipient;
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x0004E484 File Offset: 0x0004C684
		protected RecipientMissingLegDNPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipient = (string)info.GetValue("recipient", typeof(string));
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x0004E4AE File Offset: 0x0004C6AE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipient", this.recipient);
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x0600234E RID: 9038 RVA: 0x0004E4C9 File Offset: 0x0004C6C9
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x04000FBF RID: 4031
		private readonly string recipient;
	}
}
