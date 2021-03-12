using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200039E RID: 926
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceTooManyHandlesException : TransientException
	{
		// Token: 0x0600275F RID: 10079 RVA: 0x000B5C89 File Offset: 0x000B3E89
		public ReplayServiceTooManyHandlesException(long numberOfHandles, long maxNumberOfHandles) : base(ReplayStrings.ReplayServiceTooManyHandlesException(numberOfHandles, maxNumberOfHandles))
		{
			this.numberOfHandles = numberOfHandles;
			this.maxNumberOfHandles = maxNumberOfHandles;
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000B5CA6 File Offset: 0x000B3EA6
		public ReplayServiceTooManyHandlesException(long numberOfHandles, long maxNumberOfHandles, Exception innerException) : base(ReplayStrings.ReplayServiceTooManyHandlesException(numberOfHandles, maxNumberOfHandles), innerException)
		{
			this.numberOfHandles = numberOfHandles;
			this.maxNumberOfHandles = maxNumberOfHandles;
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000B5CC4 File Offset: 0x000B3EC4
		protected ReplayServiceTooManyHandlesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.numberOfHandles = (long)info.GetValue("numberOfHandles", typeof(long));
			this.maxNumberOfHandles = (long)info.GetValue("maxNumberOfHandles", typeof(long));
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000B5D19 File Offset: 0x000B3F19
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("numberOfHandles", this.numberOfHandles);
			info.AddValue("maxNumberOfHandles", this.maxNumberOfHandles);
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06002763 RID: 10083 RVA: 0x000B5D45 File Offset: 0x000B3F45
		public long NumberOfHandles
		{
			get
			{
				return this.numberOfHandles;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x000B5D4D File Offset: 0x000B3F4D
		public long MaxNumberOfHandles
		{
			get
			{
				return this.maxNumberOfHandles;
			}
		}

		// Token: 0x0400138E RID: 5006
		private readonly long numberOfHandles;

		// Token: 0x0400138F RID: 5007
		private readonly long maxNumberOfHandles;
	}
}
