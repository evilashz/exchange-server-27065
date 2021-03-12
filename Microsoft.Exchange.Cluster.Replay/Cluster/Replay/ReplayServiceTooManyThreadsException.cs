using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200039D RID: 925
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceTooManyThreadsException : TransientException
	{
		// Token: 0x06002759 RID: 10073 RVA: 0x000B5BBB File Offset: 0x000B3DBB
		public ReplayServiceTooManyThreadsException(long numberOfThreads, long maxNumberOfThreads) : base(ReplayStrings.ReplayServiceTooManyThreadsException(numberOfThreads, maxNumberOfThreads))
		{
			this.numberOfThreads = numberOfThreads;
			this.maxNumberOfThreads = maxNumberOfThreads;
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000B5BD8 File Offset: 0x000B3DD8
		public ReplayServiceTooManyThreadsException(long numberOfThreads, long maxNumberOfThreads, Exception innerException) : base(ReplayStrings.ReplayServiceTooManyThreadsException(numberOfThreads, maxNumberOfThreads), innerException)
		{
			this.numberOfThreads = numberOfThreads;
			this.maxNumberOfThreads = maxNumberOfThreads;
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x000B5BF8 File Offset: 0x000B3DF8
		protected ReplayServiceTooManyThreadsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.numberOfThreads = (long)info.GetValue("numberOfThreads", typeof(long));
			this.maxNumberOfThreads = (long)info.GetValue("maxNumberOfThreads", typeof(long));
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000B5C4D File Offset: 0x000B3E4D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("numberOfThreads", this.numberOfThreads);
			info.AddValue("maxNumberOfThreads", this.maxNumberOfThreads);
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x000B5C79 File Offset: 0x000B3E79
		public long NumberOfThreads
		{
			get
			{
				return this.numberOfThreads;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x000B5C81 File Offset: 0x000B3E81
		public long MaxNumberOfThreads
		{
			get
			{
				return this.maxNumberOfThreads;
			}
		}

		// Token: 0x0400138C RID: 5004
		private readonly long numberOfThreads;

		// Token: 0x0400138D RID: 5005
		private readonly long maxNumberOfThreads;
	}
}
