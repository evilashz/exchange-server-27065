using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EA0 RID: 3744
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RequestQueueIsTooLongTransientException : MailboxReplicationTransientException
	{
		// Token: 0x0600A7F6 RID: 42998 RVA: 0x00289404 File Offset: 0x00287604
		public RequestQueueIsTooLongTransientException(string requestQueueName, int currentQueueLength) : base(Strings.ErrorRequestQueueIsTooLong(requestQueueName, currentQueueLength))
		{
			this.requestQueueName = requestQueueName;
			this.currentQueueLength = currentQueueLength;
		}

		// Token: 0x0600A7F7 RID: 42999 RVA: 0x00289421 File Offset: 0x00287621
		public RequestQueueIsTooLongTransientException(string requestQueueName, int currentQueueLength, Exception innerException) : base(Strings.ErrorRequestQueueIsTooLong(requestQueueName, currentQueueLength), innerException)
		{
			this.requestQueueName = requestQueueName;
			this.currentQueueLength = currentQueueLength;
		}

		// Token: 0x0600A7F8 RID: 43000 RVA: 0x00289440 File Offset: 0x00287640
		protected RequestQueueIsTooLongTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.requestQueueName = (string)info.GetValue("requestQueueName", typeof(string));
			this.currentQueueLength = (int)info.GetValue("currentQueueLength", typeof(int));
		}

		// Token: 0x0600A7F9 RID: 43001 RVA: 0x00289495 File Offset: 0x00287695
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("requestQueueName", this.requestQueueName);
			info.AddValue("currentQueueLength", this.currentQueueLength);
		}

		// Token: 0x17003693 RID: 13971
		// (get) Token: 0x0600A7FA RID: 43002 RVA: 0x002894C1 File Offset: 0x002876C1
		public string RequestQueueName
		{
			get
			{
				return this.requestQueueName;
			}
		}

		// Token: 0x17003694 RID: 13972
		// (get) Token: 0x0600A7FB RID: 43003 RVA: 0x002894C9 File Offset: 0x002876C9
		public int CurrentQueueLength
		{
			get
			{
				return this.currentQueueLength;
			}
		}

		// Token: 0x04005FF9 RID: 24569
		private readonly string requestQueueName;

		// Token: 0x04005FFA RID: 24570
		private readonly int currentQueueLength;
	}
}
