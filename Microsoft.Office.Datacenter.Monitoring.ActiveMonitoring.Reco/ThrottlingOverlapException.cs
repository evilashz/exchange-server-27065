using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200005B RID: 91
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ThrottlingOverlapException : ThrottlingRejectedOperationException
	{
		// Token: 0x06000392 RID: 914 RVA: 0x0000C544 File Offset: 0x0000A744
		public ThrottlingOverlapException(long currentInstanceId, long overlapInstanceId, string currentRequester, string overlapRequester, DateTime currentStartTime, DateTime overlapStartTime) : base(StringsRecovery.ThrottlingOverlapException(currentInstanceId, overlapInstanceId, currentRequester, overlapRequester, currentStartTime, overlapStartTime))
		{
			this.currentInstanceId = currentInstanceId;
			this.overlapInstanceId = overlapInstanceId;
			this.currentRequester = currentRequester;
			this.overlapRequester = overlapRequester;
			this.currentStartTime = currentStartTime;
			this.overlapStartTime = overlapStartTime;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000C598 File Offset: 0x0000A798
		public ThrottlingOverlapException(long currentInstanceId, long overlapInstanceId, string currentRequester, string overlapRequester, DateTime currentStartTime, DateTime overlapStartTime, Exception innerException) : base(StringsRecovery.ThrottlingOverlapException(currentInstanceId, overlapInstanceId, currentRequester, overlapRequester, currentStartTime, overlapStartTime), innerException)
		{
			this.currentInstanceId = currentInstanceId;
			this.overlapInstanceId = overlapInstanceId;
			this.currentRequester = currentRequester;
			this.overlapRequester = overlapRequester;
			this.currentStartTime = currentStartTime;
			this.overlapStartTime = overlapStartTime;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
		protected ThrottlingOverlapException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.currentInstanceId = (long)info.GetValue("currentInstanceId", typeof(long));
			this.overlapInstanceId = (long)info.GetValue("overlapInstanceId", typeof(long));
			this.currentRequester = (string)info.GetValue("currentRequester", typeof(string));
			this.overlapRequester = (string)info.GetValue("overlapRequester", typeof(string));
			this.currentStartTime = (DateTime)info.GetValue("currentStartTime", typeof(DateTime));
			this.overlapStartTime = (DateTime)info.GetValue("overlapStartTime", typeof(DateTime));
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("currentInstanceId", this.currentInstanceId);
			info.AddValue("overlapInstanceId", this.overlapInstanceId);
			info.AddValue("currentRequester", this.currentRequester);
			info.AddValue("overlapRequester", this.overlapRequester);
			info.AddValue("currentStartTime", this.currentStartTime);
			info.AddValue("overlapStartTime", this.overlapStartTime);
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000C743 File Offset: 0x0000A943
		public long CurrentInstanceId
		{
			get
			{
				return this.currentInstanceId;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000C74B File Offset: 0x0000A94B
		public long OverlapInstanceId
		{
			get
			{
				return this.overlapInstanceId;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000C753 File Offset: 0x0000A953
		public string CurrentRequester
		{
			get
			{
				return this.currentRequester;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000C75B File Offset: 0x0000A95B
		public string OverlapRequester
		{
			get
			{
				return this.overlapRequester;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000C763 File Offset: 0x0000A963
		public DateTime CurrentStartTime
		{
			get
			{
				return this.currentStartTime;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000C76B File Offset: 0x0000A96B
		public DateTime OverlapStartTime
		{
			get
			{
				return this.overlapStartTime;
			}
		}

		// Token: 0x04000222 RID: 546
		private readonly long currentInstanceId;

		// Token: 0x04000223 RID: 547
		private readonly long overlapInstanceId;

		// Token: 0x04000224 RID: 548
		private readonly string currentRequester;

		// Token: 0x04000225 RID: 549
		private readonly string overlapRequester;

		// Token: 0x04000226 RID: 550
		private readonly DateTime currentStartTime;

		// Token: 0x04000227 RID: 551
		private readonly DateTime overlapStartTime;
	}
}
