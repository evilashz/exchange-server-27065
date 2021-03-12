using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000481 RID: 1153
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmInvalidDbStateException : AmCommonException
	{
		// Token: 0x06002C29 RID: 11305 RVA: 0x000BECC9 File Offset: 0x000BCEC9
		public AmInvalidDbStateException(Guid databaseGuid, string stateStr) : base(ReplayStrings.AmInvalidDbState(databaseGuid, stateStr))
		{
			this.databaseGuid = databaseGuid;
			this.stateStr = stateStr;
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000BECEB File Offset: 0x000BCEEB
		public AmInvalidDbStateException(Guid databaseGuid, string stateStr, Exception innerException) : base(ReplayStrings.AmInvalidDbState(databaseGuid, stateStr), innerException)
		{
			this.databaseGuid = databaseGuid;
			this.stateStr = stateStr;
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000BED10 File Offset: 0x000BCF10
		protected AmInvalidDbStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseGuid = (Guid)info.GetValue("databaseGuid", typeof(Guid));
			this.stateStr = (string)info.GetValue("stateStr", typeof(string));
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000BED65 File Offset: 0x000BCF65
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseGuid", this.databaseGuid);
			info.AddValue("stateStr", this.stateStr);
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06002C2D RID: 11309 RVA: 0x000BED96 File Offset: 0x000BCF96
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06002C2E RID: 11310 RVA: 0x000BED9E File Offset: 0x000BCF9E
		public string StateStr
		{
			get
			{
				return this.stateStr;
			}
		}

		// Token: 0x040014CC RID: 5324
		private readonly Guid databaseGuid;

		// Token: 0x040014CD RID: 5325
		private readonly string stateStr;
	}
}
