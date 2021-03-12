using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200039F RID: 927
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceTooMuchMemoryException : TransientException
	{
		// Token: 0x06002765 RID: 10085 RVA: 0x000B5D55 File Offset: 0x000B3F55
		public ReplayServiceTooMuchMemoryException(double memoryUsageInMib, long maximumMemoryUsageInMib) : base(ReplayStrings.ReplayServiceTooMuchMemoryException(memoryUsageInMib, maximumMemoryUsageInMib))
		{
			this.memoryUsageInMib = memoryUsageInMib;
			this.maximumMemoryUsageInMib = maximumMemoryUsageInMib;
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x000B5D72 File Offset: 0x000B3F72
		public ReplayServiceTooMuchMemoryException(double memoryUsageInMib, long maximumMemoryUsageInMib, Exception innerException) : base(ReplayStrings.ReplayServiceTooMuchMemoryException(memoryUsageInMib, maximumMemoryUsageInMib), innerException)
		{
			this.memoryUsageInMib = memoryUsageInMib;
			this.maximumMemoryUsageInMib = maximumMemoryUsageInMib;
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x000B5D90 File Offset: 0x000B3F90
		protected ReplayServiceTooMuchMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.memoryUsageInMib = (double)info.GetValue("memoryUsageInMib", typeof(double));
			this.maximumMemoryUsageInMib = (long)info.GetValue("maximumMemoryUsageInMib", typeof(long));
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x000B5DE5 File Offset: 0x000B3FE5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("memoryUsageInMib", this.memoryUsageInMib);
			info.AddValue("maximumMemoryUsageInMib", this.maximumMemoryUsageInMib);
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x000B5E11 File Offset: 0x000B4011
		public double MemoryUsageInMib
		{
			get
			{
				return this.memoryUsageInMib;
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x0600276A RID: 10090 RVA: 0x000B5E19 File Offset: 0x000B4019
		public long MaximumMemoryUsageInMib
		{
			get
			{
				return this.maximumMemoryUsageInMib;
			}
		}

		// Token: 0x04001390 RID: 5008
		private readonly double memoryUsageInMib;

		// Token: 0x04001391 RID: 5009
		private readonly long maximumMemoryUsageInMib;
	}
}
