using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000489 RID: 1161
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederFailedToInspectLogException : SeederServerException
	{
		// Token: 0x06002C59 RID: 11353 RVA: 0x000BF375 File Offset: 0x000BD575
		public SeederFailedToInspectLogException(string logfileName, string inspectionError) : base(ReplayStrings.SeederFailedToInspectLogException(logfileName, inspectionError))
		{
			this.logfileName = logfileName;
			this.inspectionError = inspectionError;
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x000BF397 File Offset: 0x000BD597
		public SeederFailedToInspectLogException(string logfileName, string inspectionError, Exception innerException) : base(ReplayStrings.SeederFailedToInspectLogException(logfileName, inspectionError), innerException)
		{
			this.logfileName = logfileName;
			this.inspectionError = inspectionError;
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x000BF3BC File Offset: 0x000BD5BC
		protected SeederFailedToInspectLogException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.logfileName = (string)info.GetValue("logfileName", typeof(string));
			this.inspectionError = (string)info.GetValue("inspectionError", typeof(string));
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000BF411 File Offset: 0x000BD611
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("logfileName", this.logfileName);
			info.AddValue("inspectionError", this.inspectionError);
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06002C5D RID: 11357 RVA: 0x000BF43D File Offset: 0x000BD63D
		public string LogfileName
		{
			get
			{
				return this.logfileName;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06002C5E RID: 11358 RVA: 0x000BF445 File Offset: 0x000BD645
		public string InspectionError
		{
			get
			{
				return this.inspectionError;
			}
		}

		// Token: 0x040014DC RID: 5340
		private readonly string logfileName;

		// Token: 0x040014DD RID: 5341
		private readonly string inspectionError;
	}
}
