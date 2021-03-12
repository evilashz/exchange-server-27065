using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000ECD RID: 3789
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotModifyRehomingRequestTransientException : MailboxReplicationTransientException
	{
		// Token: 0x0600A8DA RID: 43226 RVA: 0x0028AA91 File Offset: 0x00288C91
		public CannotModifyRehomingRequestTransientException(string identity) : base(Strings.ErrorCannotModifyRehomingRequest(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600A8DB RID: 43227 RVA: 0x0028AAA6 File Offset: 0x00288CA6
		public CannotModifyRehomingRequestTransientException(string identity, Exception innerException) : base(Strings.ErrorCannotModifyRehomingRequest(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600A8DC RID: 43228 RVA: 0x0028AABC File Offset: 0x00288CBC
		protected CannotModifyRehomingRequestTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600A8DD RID: 43229 RVA: 0x0028AAE6 File Offset: 0x00288CE6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x170036C3 RID: 14019
		// (get) Token: 0x0600A8DE RID: 43230 RVA: 0x0028AB01 File Offset: 0x00288D01
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04006029 RID: 24617
		private readonly string identity;
	}
}
