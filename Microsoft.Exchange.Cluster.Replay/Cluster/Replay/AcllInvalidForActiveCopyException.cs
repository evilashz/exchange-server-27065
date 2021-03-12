using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200042F RID: 1071
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcllInvalidForActiveCopyException : TransientException
	{
		// Token: 0x06002A70 RID: 10864 RVA: 0x000BB911 File Offset: 0x000B9B11
		public AcllInvalidForActiveCopyException(string dbCopy) : base(ReplayStrings.AcllInvalidForActiveCopyException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000BB926 File Offset: 0x000B9B26
		public AcllInvalidForActiveCopyException(string dbCopy, Exception innerException) : base(ReplayStrings.AcllInvalidForActiveCopyException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x000BB93C File Offset: 0x000B9B3C
		protected AcllInvalidForActiveCopyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000BB966 File Offset: 0x000B9B66
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06002A74 RID: 10868 RVA: 0x000BB981 File Offset: 0x000B9B81
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x0400145B RID: 5211
		private readonly string dbCopy;
	}
}
