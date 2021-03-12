using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004EC RID: 1260
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseHealthTrackerException : LocalizedException
	{
		// Token: 0x06002E8B RID: 11915 RVA: 0x000C397F File Offset: 0x000C1B7F
		public DatabaseHealthTrackerException(string errorMsg) : base(ReplayStrings.DatabaseHealthTrackerException(errorMsg))
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000C3994 File Offset: 0x000C1B94
		public DatabaseHealthTrackerException(string errorMsg, Exception innerException) : base(ReplayStrings.DatabaseHealthTrackerException(errorMsg), innerException)
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x000C39AA File Offset: 0x000C1BAA
		protected DatabaseHealthTrackerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x000C39D4 File Offset: 0x000C1BD4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06002E8F RID: 11919 RVA: 0x000C39EF File Offset: 0x000C1BEF
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x04001582 RID: 5506
		private readonly string errorMsg;
	}
}
