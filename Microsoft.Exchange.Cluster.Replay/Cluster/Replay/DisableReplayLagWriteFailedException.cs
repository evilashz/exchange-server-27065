using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200041A RID: 1050
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DisableReplayLagWriteFailedException : TaskServerTransientException
	{
		// Token: 0x060029F4 RID: 10740 RVA: 0x000BA8BB File Offset: 0x000B8ABB
		public DisableReplayLagWriteFailedException(string dbCopy) : base(ReplayStrings.DisableReplayLagWriteFailedException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x000BA8D5 File Offset: 0x000B8AD5
		public DisableReplayLagWriteFailedException(string dbCopy, Exception innerException) : base(ReplayStrings.DisableReplayLagWriteFailedException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x000BA8F0 File Offset: 0x000B8AF0
		protected DisableReplayLagWriteFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x000BA91A File Offset: 0x000B8B1A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x060029F8 RID: 10744 RVA: 0x000BA935 File Offset: 0x000B8B35
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x04001433 RID: 5171
		private readonly string dbCopy;
	}
}
