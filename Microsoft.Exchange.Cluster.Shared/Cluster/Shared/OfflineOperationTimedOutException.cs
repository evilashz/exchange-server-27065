using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000D6 RID: 214
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OfflineOperationTimedOutException : LocalizedException
	{
		// Token: 0x06000765 RID: 1893 RVA: 0x0001C13C File Offset: 0x0001A33C
		public OfflineOperationTimedOutException(string objectName, int count, int secondsTimeout) : base(Strings.OfflineOperationTimedOutException(objectName, count, secondsTimeout))
		{
			this.objectName = objectName;
			this.count = count;
			this.secondsTimeout = secondsTimeout;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001C161 File Offset: 0x0001A361
		public OfflineOperationTimedOutException(string objectName, int count, int secondsTimeout, Exception innerException) : base(Strings.OfflineOperationTimedOutException(objectName, count, secondsTimeout), innerException)
		{
			this.objectName = objectName;
			this.count = count;
			this.secondsTimeout = secondsTimeout;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001C188 File Offset: 0x0001A388
		protected OfflineOperationTimedOutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.objectName = (string)info.GetValue("objectName", typeof(string));
			this.count = (int)info.GetValue("count", typeof(int));
			this.secondsTimeout = (int)info.GetValue("secondsTimeout", typeof(int));
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001C1FD File Offset: 0x0001A3FD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("objectName", this.objectName);
			info.AddValue("count", this.count);
			info.AddValue("secondsTimeout", this.secondsTimeout);
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0001C23A File Offset: 0x0001A43A
		public string ObjectName
		{
			get
			{
				return this.objectName;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001C242 File Offset: 0x0001A442
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001C24A File Offset: 0x0001A44A
		public int SecondsTimeout
		{
			get
			{
				return this.secondsTimeout;
			}
		}

		// Token: 0x0400072D RID: 1837
		private readonly string objectName;

		// Token: 0x0400072E RID: 1838
		private readonly int count;

		// Token: 0x0400072F RID: 1839
		private readonly int secondsTimeout;
	}
}
