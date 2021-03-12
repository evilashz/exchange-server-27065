using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003D4 RID: 980
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SuspendCommentTooLongException : LocalizedException
	{
		// Token: 0x0600288E RID: 10382 RVA: 0x000B8212 File Offset: 0x000B6412
		public SuspendCommentTooLongException(int length, int limit) : base(ReplayStrings.SuspendCommentTooLong(length, limit))
		{
			this.length = length;
			this.limit = limit;
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x000B822F File Offset: 0x000B642F
		public SuspendCommentTooLongException(int length, int limit, Exception innerException) : base(ReplayStrings.SuspendCommentTooLong(length, limit), innerException)
		{
			this.length = length;
			this.limit = limit;
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x000B8250 File Offset: 0x000B6450
		protected SuspendCommentTooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.length = (int)info.GetValue("length", typeof(int));
			this.limit = (int)info.GetValue("limit", typeof(int));
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x000B82A5 File Offset: 0x000B64A5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("length", this.length);
			info.AddValue("limit", this.limit);
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06002892 RID: 10386 RVA: 0x000B82D1 File Offset: 0x000B64D1
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06002893 RID: 10387 RVA: 0x000B82D9 File Offset: 0x000B64D9
		public int Limit
		{
			get
			{
				return this.limit;
			}
		}

		// Token: 0x040013E5 RID: 5093
		private readonly int length;

		// Token: 0x040013E6 RID: 5094
		private readonly int limit;
	}
}
