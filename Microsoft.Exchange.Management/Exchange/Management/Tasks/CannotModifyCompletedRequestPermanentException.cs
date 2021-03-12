using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000ECB RID: 3787
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotModifyCompletedRequestPermanentException : CannotSetCompletedPermanentException
	{
		// Token: 0x0600A8D0 RID: 43216 RVA: 0x0028A9A1 File Offset: 0x00288BA1
		public CannotModifyCompletedRequestPermanentException(string identity) : base(Strings.ErrorCannotModifyCompletedRequest(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600A8D1 RID: 43217 RVA: 0x0028A9B6 File Offset: 0x00288BB6
		public CannotModifyCompletedRequestPermanentException(string identity, Exception innerException) : base(Strings.ErrorCannotModifyCompletedRequest(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600A8D2 RID: 43218 RVA: 0x0028A9CC File Offset: 0x00288BCC
		protected CannotModifyCompletedRequestPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600A8D3 RID: 43219 RVA: 0x0028A9F6 File Offset: 0x00288BF6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x170036C1 RID: 14017
		// (get) Token: 0x0600A8D4 RID: 43220 RVA: 0x0028AA11 File Offset: 0x00288C11
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04006027 RID: 24615
		private readonly string identity;
	}
}
