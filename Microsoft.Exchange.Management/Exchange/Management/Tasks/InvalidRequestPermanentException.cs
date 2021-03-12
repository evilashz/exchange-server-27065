using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EC9 RID: 3785
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidRequestPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A8C5 RID: 43205 RVA: 0x0028A85C File Offset: 0x00288A5C
		public InvalidRequestPermanentException(string identity, string validationMessage) : base(Strings.ErrorInvalidRequest(identity, validationMessage))
		{
			this.identity = identity;
			this.validationMessage = validationMessage;
		}

		// Token: 0x0600A8C6 RID: 43206 RVA: 0x0028A879 File Offset: 0x00288A79
		public InvalidRequestPermanentException(string identity, string validationMessage, Exception innerException) : base(Strings.ErrorInvalidRequest(identity, validationMessage), innerException)
		{
			this.identity = identity;
			this.validationMessage = validationMessage;
		}

		// Token: 0x0600A8C7 RID: 43207 RVA: 0x0028A898 File Offset: 0x00288A98
		protected InvalidRequestPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
			this.validationMessage = (string)info.GetValue("validationMessage", typeof(string));
		}

		// Token: 0x0600A8C8 RID: 43208 RVA: 0x0028A8ED File Offset: 0x00288AED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
			info.AddValue("validationMessage", this.validationMessage);
		}

		// Token: 0x170036BE RID: 14014
		// (get) Token: 0x0600A8C9 RID: 43209 RVA: 0x0028A919 File Offset: 0x00288B19
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x170036BF RID: 14015
		// (get) Token: 0x0600A8CA RID: 43210 RVA: 0x0028A921 File Offset: 0x00288B21
		public string ValidationMessage
		{
			get
			{
				return this.validationMessage;
			}
		}

		// Token: 0x04006024 RID: 24612
		private readonly string identity;

		// Token: 0x04006025 RID: 24613
		private readonly string validationMessage;
	}
}
