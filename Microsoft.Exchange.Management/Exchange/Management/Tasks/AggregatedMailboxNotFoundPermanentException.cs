using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000ED0 RID: 3792
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AggregatedMailboxNotFoundPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A8E9 RID: 43241 RVA: 0x0028ABF9 File Offset: 0x00288DF9
		public AggregatedMailboxNotFoundPermanentException(Guid aggregatedMailboxGuid, string identity) : base(Strings.ErrorAggregatedMailboxNotFound(aggregatedMailboxGuid, identity))
		{
			this.aggregatedMailboxGuid = aggregatedMailboxGuid;
			this.identity = identity;
		}

		// Token: 0x0600A8EA RID: 43242 RVA: 0x0028AC16 File Offset: 0x00288E16
		public AggregatedMailboxNotFoundPermanentException(Guid aggregatedMailboxGuid, string identity, Exception innerException) : base(Strings.ErrorAggregatedMailboxNotFound(aggregatedMailboxGuid, identity), innerException)
		{
			this.aggregatedMailboxGuid = aggregatedMailboxGuid;
			this.identity = identity;
		}

		// Token: 0x0600A8EB RID: 43243 RVA: 0x0028AC34 File Offset: 0x00288E34
		protected AggregatedMailboxNotFoundPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.aggregatedMailboxGuid = (Guid)info.GetValue("aggregatedMailboxGuid", typeof(Guid));
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600A8EC RID: 43244 RVA: 0x0028AC89 File Offset: 0x00288E89
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("aggregatedMailboxGuid", this.aggregatedMailboxGuid);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x170036C6 RID: 14022
		// (get) Token: 0x0600A8ED RID: 43245 RVA: 0x0028ACBA File Offset: 0x00288EBA
		public Guid AggregatedMailboxGuid
		{
			get
			{
				return this.aggregatedMailboxGuid;
			}
		}

		// Token: 0x170036C7 RID: 14023
		// (get) Token: 0x0600A8EE RID: 43246 RVA: 0x0028ACC2 File Offset: 0x00288EC2
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x0400602C RID: 24620
		private readonly Guid aggregatedMailboxGuid;

		// Token: 0x0400602D RID: 24621
		private readonly string identity;
	}
}
