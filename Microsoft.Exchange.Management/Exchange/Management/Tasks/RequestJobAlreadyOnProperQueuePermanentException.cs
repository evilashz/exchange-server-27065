using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EDD RID: 3805
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RequestJobAlreadyOnProperQueuePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A930 RID: 43312 RVA: 0x0028B425 File Offset: 0x00289625
		public RequestJobAlreadyOnProperQueuePermanentException(string identity, string queue) : base(Strings.ErrorRequestJobAlreadyOnProperQueue(identity, queue))
		{
			this.identity = identity;
			this.queue = queue;
		}

		// Token: 0x0600A931 RID: 43313 RVA: 0x0028B442 File Offset: 0x00289642
		public RequestJobAlreadyOnProperQueuePermanentException(string identity, string queue, Exception innerException) : base(Strings.ErrorRequestJobAlreadyOnProperQueue(identity, queue), innerException)
		{
			this.identity = identity;
			this.queue = queue;
		}

		// Token: 0x0600A932 RID: 43314 RVA: 0x0028B460 File Offset: 0x00289660
		protected RequestJobAlreadyOnProperQueuePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
			this.queue = (string)info.GetValue("queue", typeof(string));
		}

		// Token: 0x0600A933 RID: 43315 RVA: 0x0028B4B5 File Offset: 0x002896B5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
			info.AddValue("queue", this.queue);
		}

		// Token: 0x170036D9 RID: 14041
		// (get) Token: 0x0600A934 RID: 43316 RVA: 0x0028B4E1 File Offset: 0x002896E1
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x170036DA RID: 14042
		// (get) Token: 0x0600A935 RID: 43317 RVA: 0x0028B4E9 File Offset: 0x002896E9
		public string Queue
		{
			get
			{
				return this.queue;
			}
		}

		// Token: 0x0400603F RID: 24639
		private readonly string identity;

		// Token: 0x04006040 RID: 24640
		private readonly string queue;
	}
}
