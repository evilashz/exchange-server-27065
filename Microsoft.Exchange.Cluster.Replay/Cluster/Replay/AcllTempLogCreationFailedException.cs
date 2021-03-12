using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000430 RID: 1072
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcllTempLogCreationFailedException : TransientException
	{
		// Token: 0x06002A75 RID: 10869 RVA: 0x000BB989 File Offset: 0x000B9B89
		public AcllTempLogCreationFailedException(string dbCopy, string err) : base(ReplayStrings.AcllTempLogCreationFailedException(dbCopy, err))
		{
			this.dbCopy = dbCopy;
			this.err = err;
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000BB9A6 File Offset: 0x000B9BA6
		public AcllTempLogCreationFailedException(string dbCopy, string err, Exception innerException) : base(ReplayStrings.AcllTempLogCreationFailedException(dbCopy, err), innerException)
		{
			this.dbCopy = dbCopy;
			this.err = err;
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000BB9C4 File Offset: 0x000B9BC4
		protected AcllTempLogCreationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
			this.err = (string)info.GetValue("err", typeof(string));
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000BBA19 File Offset: 0x000B9C19
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
			info.AddValue("err", this.err);
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06002A79 RID: 10873 RVA: 0x000BBA45 File Offset: 0x000B9C45
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06002A7A RID: 10874 RVA: 0x000BBA4D File Offset: 0x000B9C4D
		public string Err
		{
			get
			{
				return this.err;
			}
		}

		// Token: 0x0400145C RID: 5212
		private readonly string dbCopy;

		// Token: 0x0400145D RID: 5213
		private readonly string err;
	}
}
