using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EB5 RID: 3765
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidRecipientTypePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A860 RID: 43104 RVA: 0x00289E7D File Offset: 0x0028807D
		public InvalidRecipientTypePermanentException(string recipientName, string recipientType) : base(Strings.ErrorInvalidRecipientType(recipientName, recipientType))
		{
			this.recipientName = recipientName;
			this.recipientType = recipientType;
		}

		// Token: 0x0600A861 RID: 43105 RVA: 0x00289E9A File Offset: 0x0028809A
		public InvalidRecipientTypePermanentException(string recipientName, string recipientType, Exception innerException) : base(Strings.ErrorInvalidRecipientType(recipientName, recipientType), innerException)
		{
			this.recipientName = recipientName;
			this.recipientType = recipientType;
		}

		// Token: 0x0600A862 RID: 43106 RVA: 0x00289EB8 File Offset: 0x002880B8
		protected InvalidRecipientTypePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipientName = (string)info.GetValue("recipientName", typeof(string));
			this.recipientType = (string)info.GetValue("recipientType", typeof(string));
		}

		// Token: 0x0600A863 RID: 43107 RVA: 0x00289F0D File Offset: 0x0028810D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipientName", this.recipientName);
			info.AddValue("recipientType", this.recipientType);
		}

		// Token: 0x170036A9 RID: 13993
		// (get) Token: 0x0600A864 RID: 43108 RVA: 0x00289F39 File Offset: 0x00288139
		public string RecipientName
		{
			get
			{
				return this.recipientName;
			}
		}

		// Token: 0x170036AA RID: 13994
		// (get) Token: 0x0600A865 RID: 43109 RVA: 0x00289F41 File Offset: 0x00288141
		public string RecipientType
		{
			get
			{
				return this.recipientType;
			}
		}

		// Token: 0x0400600F RID: 24591
		private readonly string recipientName;

		// Token: 0x04006010 RID: 24592
		private readonly string recipientType;
	}
}
