using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000ECE RID: 3790
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotSetCancelledRequestPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A8DF RID: 43231 RVA: 0x0028AB09 File Offset: 0x00288D09
		public CannotSetCancelledRequestPermanentException(string identity) : base(Strings.ErrorRequestAlreadyCanceled(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600A8E0 RID: 43232 RVA: 0x0028AB1E File Offset: 0x00288D1E
		public CannotSetCancelledRequestPermanentException(string identity, Exception innerException) : base(Strings.ErrorRequestAlreadyCanceled(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600A8E1 RID: 43233 RVA: 0x0028AB34 File Offset: 0x00288D34
		protected CannotSetCancelledRequestPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600A8E2 RID: 43234 RVA: 0x0028AB5E File Offset: 0x00288D5E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x170036C4 RID: 14020
		// (get) Token: 0x0600A8E3 RID: 43235 RVA: 0x0028AB79 File Offset: 0x00288D79
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x0400602A RID: 24618
		private readonly string identity;
	}
}
