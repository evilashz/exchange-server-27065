using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200042D RID: 1069
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcllCouldNotControlReplicaInstanceException : TransientException
	{
		// Token: 0x06002A66 RID: 10854 RVA: 0x000BB821 File Offset: 0x000B9A21
		public AcllCouldNotControlReplicaInstanceException(string dbCopy) : base(ReplayStrings.AcllCouldNotControlReplicaInstanceException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x000BB836 File Offset: 0x000B9A36
		public AcllCouldNotControlReplicaInstanceException(string dbCopy, Exception innerException) : base(ReplayStrings.AcllCouldNotControlReplicaInstanceException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x000BB84C File Offset: 0x000B9A4C
		protected AcllCouldNotControlReplicaInstanceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x000BB876 File Offset: 0x000B9A76
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06002A6A RID: 10858 RVA: 0x000BB891 File Offset: 0x000B9A91
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x04001459 RID: 5209
		private readonly string dbCopy;
	}
}
