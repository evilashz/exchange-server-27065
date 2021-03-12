using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200042A RID: 1066
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcllCopyStatusInvalidException : TransientException
	{
		// Token: 0x06002A53 RID: 10835 RVA: 0x000BB56D File Offset: 0x000B976D
		public AcllCopyStatusInvalidException(string dbCopy, string status) : base(ReplayStrings.AcllCopyStatusInvalidException(dbCopy, status))
		{
			this.dbCopy = dbCopy;
			this.status = status;
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000BB58A File Offset: 0x000B978A
		public AcllCopyStatusInvalidException(string dbCopy, string status, Exception innerException) : base(ReplayStrings.AcllCopyStatusInvalidException(dbCopy, status), innerException)
		{
			this.dbCopy = dbCopy;
			this.status = status;
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000BB5A8 File Offset: 0x000B97A8
		protected AcllCopyStatusInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
			this.status = (string)info.GetValue("status", typeof(string));
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000BB5FD File Offset: 0x000B97FD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
			info.AddValue("status", this.status);
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x000BB629 File Offset: 0x000B9829
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06002A58 RID: 10840 RVA: 0x000BB631 File Offset: 0x000B9831
		public string Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x04001452 RID: 5202
		private readonly string dbCopy;

		// Token: 0x04001453 RID: 5203
		private readonly string status;
	}
}
