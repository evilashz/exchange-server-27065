using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000431 RID: 1073
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcllCopyIsNotViableException : TransientException
	{
		// Token: 0x06002A7B RID: 10875 RVA: 0x000BBA55 File Offset: 0x000B9C55
		public AcllCopyIsNotViableException(string dbCopy) : base(ReplayStrings.AcllCopyIsNotViableException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x000BBA6A File Offset: 0x000B9C6A
		public AcllCopyIsNotViableException(string dbCopy, Exception innerException) : base(ReplayStrings.AcllCopyIsNotViableException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x000BBA80 File Offset: 0x000B9C80
		protected AcllCopyIsNotViableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x000BBAAA File Offset: 0x000B9CAA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06002A7F RID: 10879 RVA: 0x000BBAC5 File Offset: 0x000B9CC5
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x0400145E RID: 5214
		private readonly string dbCopy;
	}
}
