using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002B0 RID: 688
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CommunicationWithRemoteServiceFailedTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002331 RID: 9009 RVA: 0x0004E1F1 File Offset: 0x0004C3F1
		public CommunicationWithRemoteServiceFailedTransientException(string endpoint) : base(MrsStrings.CommunicationWithRemoteServiceFailed(endpoint))
		{
			this.endpoint = endpoint;
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x0004E206 File Offset: 0x0004C406
		public CommunicationWithRemoteServiceFailedTransientException(string endpoint, Exception innerException) : base(MrsStrings.CommunicationWithRemoteServiceFailed(endpoint), innerException)
		{
			this.endpoint = endpoint;
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x0004E21C File Offset: 0x0004C41C
		protected CommunicationWithRemoteServiceFailedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.endpoint = (string)info.GetValue("endpoint", typeof(string));
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x0004E246 File Offset: 0x0004C446
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("endpoint", this.endpoint);
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06002335 RID: 9013 RVA: 0x0004E261 File Offset: 0x0004C461
		public string Endpoint
		{
			get
			{
				return this.endpoint;
			}
		}

		// Token: 0x04000FBA RID: 4026
		private readonly string endpoint;
	}
}
