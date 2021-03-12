using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003A0 RID: 928
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceTooMuchMemoryNoDumpException : TransientException
	{
		// Token: 0x0600276B RID: 10091 RVA: 0x000B5E21 File Offset: 0x000B4021
		public ReplayServiceTooMuchMemoryNoDumpException(double memoryUsageInMib, long maximumMemoryUsageInMib, string enableWatsonRegKey) : base(ReplayStrings.ReplayServiceTooMuchMemoryNoDumpException(memoryUsageInMib, maximumMemoryUsageInMib, enableWatsonRegKey))
		{
			this.memoryUsageInMib = memoryUsageInMib;
			this.maximumMemoryUsageInMib = maximumMemoryUsageInMib;
			this.enableWatsonRegKey = enableWatsonRegKey;
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x000B5E46 File Offset: 0x000B4046
		public ReplayServiceTooMuchMemoryNoDumpException(double memoryUsageInMib, long maximumMemoryUsageInMib, string enableWatsonRegKey, Exception innerException) : base(ReplayStrings.ReplayServiceTooMuchMemoryNoDumpException(memoryUsageInMib, maximumMemoryUsageInMib, enableWatsonRegKey), innerException)
		{
			this.memoryUsageInMib = memoryUsageInMib;
			this.maximumMemoryUsageInMib = maximumMemoryUsageInMib;
			this.enableWatsonRegKey = enableWatsonRegKey;
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000B5E70 File Offset: 0x000B4070
		protected ReplayServiceTooMuchMemoryNoDumpException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.memoryUsageInMib = (double)info.GetValue("memoryUsageInMib", typeof(double));
			this.maximumMemoryUsageInMib = (long)info.GetValue("maximumMemoryUsageInMib", typeof(long));
			this.enableWatsonRegKey = (string)info.GetValue("enableWatsonRegKey", typeof(string));
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x000B5EE5 File Offset: 0x000B40E5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("memoryUsageInMib", this.memoryUsageInMib);
			info.AddValue("maximumMemoryUsageInMib", this.maximumMemoryUsageInMib);
			info.AddValue("enableWatsonRegKey", this.enableWatsonRegKey);
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x000B5F22 File Offset: 0x000B4122
		public double MemoryUsageInMib
		{
			get
			{
				return this.memoryUsageInMib;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x000B5F2A File Offset: 0x000B412A
		public long MaximumMemoryUsageInMib
		{
			get
			{
				return this.maximumMemoryUsageInMib;
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x000B5F32 File Offset: 0x000B4132
		public string EnableWatsonRegKey
		{
			get
			{
				return this.enableWatsonRegKey;
			}
		}

		// Token: 0x04001392 RID: 5010
		private readonly double memoryUsageInMib;

		// Token: 0x04001393 RID: 5011
		private readonly long maximumMemoryUsageInMib;

		// Token: 0x04001394 RID: 5012
		private readonly string enableWatsonRegKey;
	}
}
