using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200042B RID: 1067
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcllCopyStatusFailedException : TransientException
	{
		// Token: 0x06002A59 RID: 10841 RVA: 0x000BB639 File Offset: 0x000B9839
		public AcllCopyStatusFailedException(string dbCopy, string status, string errorMsg) : base(ReplayStrings.AcllCopyStatusFailedException(dbCopy, status, errorMsg))
		{
			this.dbCopy = dbCopy;
			this.status = status;
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000BB65E File Offset: 0x000B985E
		public AcllCopyStatusFailedException(string dbCopy, string status, string errorMsg, Exception innerException) : base(ReplayStrings.AcllCopyStatusFailedException(dbCopy, status, errorMsg), innerException)
		{
			this.dbCopy = dbCopy;
			this.status = status;
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x000BB688 File Offset: 0x000B9888
		protected AcllCopyStatusFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
			this.status = (string)info.GetValue("status", typeof(string));
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x000BB6FD File Offset: 0x000B98FD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
			info.AddValue("status", this.status);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06002A5D RID: 10845 RVA: 0x000BB73A File Offset: 0x000B993A
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06002A5E RID: 10846 RVA: 0x000BB742 File Offset: 0x000B9942
		public string Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06002A5F RID: 10847 RVA: 0x000BB74A File Offset: 0x000B994A
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x04001454 RID: 5204
		private readonly string dbCopy;

		// Token: 0x04001455 RID: 5205
		private readonly string status;

		// Token: 0x04001456 RID: 5206
		private readonly string errorMsg;
	}
}
