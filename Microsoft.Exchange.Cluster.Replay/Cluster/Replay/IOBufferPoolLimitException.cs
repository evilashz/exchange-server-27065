using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003AD RID: 941
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IOBufferPoolLimitException : LocalizedException
	{
		// Token: 0x060027B2 RID: 10162 RVA: 0x000B666A File Offset: 0x000B486A
		public IOBufferPoolLimitException(int limit, int bufSize) : base(ReplayStrings.IOBufferPoolLimitError(limit, bufSize))
		{
			this.limit = limit;
			this.bufSize = bufSize;
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x000B6687 File Offset: 0x000B4887
		public IOBufferPoolLimitException(int limit, int bufSize, Exception innerException) : base(ReplayStrings.IOBufferPoolLimitError(limit, bufSize), innerException)
		{
			this.limit = limit;
			this.bufSize = bufSize;
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x000B66A8 File Offset: 0x000B48A8
		protected IOBufferPoolLimitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.limit = (int)info.GetValue("limit", typeof(int));
			this.bufSize = (int)info.GetValue("bufSize", typeof(int));
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x000B66FD File Offset: 0x000B48FD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("limit", this.limit);
			info.AddValue("bufSize", this.bufSize);
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x060027B6 RID: 10166 RVA: 0x000B6729 File Offset: 0x000B4929
		public int Limit
		{
			get
			{
				return this.limit;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x000B6731 File Offset: 0x000B4931
		public int BufSize
		{
			get
			{
				return this.bufSize;
			}
		}

		// Token: 0x040013A5 RID: 5029
		private readonly int limit;

		// Token: 0x040013A6 RID: 5030
		private readonly int bufSize;
	}
}
