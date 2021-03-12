using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000442 RID: 1090
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederInstanceInvalidStateForEndException : SeederServerException
	{
		// Token: 0x06002AD9 RID: 10969 RVA: 0x000BC5B7 File Offset: 0x000BA7B7
		public SeederInstanceInvalidStateForEndException(string dbGuid) : base(ReplayStrings.SeederInstanceInvalidStateForEndException(dbGuid))
		{
			this.dbGuid = dbGuid;
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x000BC5D1 File Offset: 0x000BA7D1
		public SeederInstanceInvalidStateForEndException(string dbGuid, Exception innerException) : base(ReplayStrings.SeederInstanceInvalidStateForEndException(dbGuid), innerException)
		{
			this.dbGuid = dbGuid;
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x000BC5EC File Offset: 0x000BA7EC
		protected SeederInstanceInvalidStateForEndException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbGuid = (string)info.GetValue("dbGuid", typeof(string));
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x000BC616 File Offset: 0x000BA816
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbGuid", this.dbGuid);
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002ADD RID: 10973 RVA: 0x000BC631 File Offset: 0x000BA831
		public string DbGuid
		{
			get
			{
				return this.dbGuid;
			}
		}

		// Token: 0x04001478 RID: 5240
		private readonly string dbGuid;
	}
}
