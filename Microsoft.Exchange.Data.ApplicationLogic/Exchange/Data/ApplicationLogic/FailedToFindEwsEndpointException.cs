using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200000C RID: 12
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToFindEwsEndpointException : AuditLogException
	{
		// Token: 0x06000084 RID: 132 RVA: 0x000036CE File Offset: 0x000018CE
		public FailedToFindEwsEndpointException(string mailbox) : base(Strings.FailedToFindEwsEndpoint(mailbox))
		{
			this.mailbox = mailbox;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000036E3 File Offset: 0x000018E3
		public FailedToFindEwsEndpointException(string mailbox, Exception innerException) : base(Strings.FailedToFindEwsEndpoint(mailbox), innerException)
		{
			this.mailbox = mailbox;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000036F9 File Offset: 0x000018F9
		protected FailedToFindEwsEndpointException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailbox = (string)info.GetValue("mailbox", typeof(string));
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003723 File Offset: 0x00001923
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailbox", this.mailbox);
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000373E File Offset: 0x0000193E
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x0400006A RID: 106
		private readonly string mailbox;
	}
}
