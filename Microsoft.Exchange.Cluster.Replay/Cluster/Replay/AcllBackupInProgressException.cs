using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000429 RID: 1065
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcllBackupInProgressException : TransientException
	{
		// Token: 0x06002A4E RID: 10830 RVA: 0x000BB4F5 File Offset: 0x000B96F5
		public AcllBackupInProgressException(string dbCopy) : base(ReplayStrings.AcllBackupInProgressException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000BB50A File Offset: 0x000B970A
		public AcllBackupInProgressException(string dbCopy, Exception innerException) : base(ReplayStrings.AcllBackupInProgressException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x000BB520 File Offset: 0x000B9720
		protected AcllBackupInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x000BB54A File Offset: 0x000B974A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06002A52 RID: 10834 RVA: 0x000BB565 File Offset: 0x000B9765
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x04001451 RID: 5201
		private readonly string dbCopy;
	}
}
