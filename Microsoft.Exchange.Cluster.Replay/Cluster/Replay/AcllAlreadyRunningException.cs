using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000428 RID: 1064
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcllAlreadyRunningException : TransientException
	{
		// Token: 0x06002A49 RID: 10825 RVA: 0x000BB47D File Offset: 0x000B967D
		public AcllAlreadyRunningException(string dbCopy) : base(ReplayStrings.AcllAlreadyRunningException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x000BB492 File Offset: 0x000B9692
		public AcllAlreadyRunningException(string dbCopy, Exception innerException) : base(ReplayStrings.AcllAlreadyRunningException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000BB4A8 File Offset: 0x000B96A8
		protected AcllAlreadyRunningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000BB4D2 File Offset: 0x000B96D2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06002A4D RID: 10829 RVA: 0x000BB4ED File Offset: 0x000B96ED
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x04001450 RID: 5200
		private readonly string dbCopy;
	}
}
