using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200042C RID: 1068
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcllCopyStatusResumeBlockedException : TransientException
	{
		// Token: 0x06002A60 RID: 10848 RVA: 0x000BB752 File Offset: 0x000B9952
		public AcllCopyStatusResumeBlockedException(string dbCopy, string errorMsg) : base(ReplayStrings.AcllCopyStatusResumeBlockedException(dbCopy, errorMsg))
		{
			this.dbCopy = dbCopy;
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x000BB76F File Offset: 0x000B996F
		public AcllCopyStatusResumeBlockedException(string dbCopy, string errorMsg, Exception innerException) : base(ReplayStrings.AcllCopyStatusResumeBlockedException(dbCopy, errorMsg), innerException)
		{
			this.dbCopy = dbCopy;
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x000BB790 File Offset: 0x000B9990
		protected AcllCopyStatusResumeBlockedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x000BB7E5 File Offset: 0x000B99E5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06002A64 RID: 10852 RVA: 0x000BB811 File Offset: 0x000B9A11
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06002A65 RID: 10853 RVA: 0x000BB819 File Offset: 0x000B9A19
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x04001457 RID: 5207
		private readonly string dbCopy;

		// Token: 0x04001458 RID: 5208
		private readonly string errorMsg;
	}
}
