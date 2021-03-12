using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EEC RID: 3820
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorMoveActiveCopyNotFoundException : LocalizedException
	{
		// Token: 0x0600A984 RID: 43396 RVA: 0x0028BE3D File Offset: 0x0028A03D
		public ErrorMoveActiveCopyNotFoundException(Guid db, string errorMsg) : base(Strings.ErrorMoveActiveCopyNotFoundException(db, errorMsg))
		{
			this.db = db;
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600A985 RID: 43397 RVA: 0x0028BE5A File Offset: 0x0028A05A
		public ErrorMoveActiveCopyNotFoundException(Guid db, string errorMsg, Exception innerException) : base(Strings.ErrorMoveActiveCopyNotFoundException(db, errorMsg), innerException)
		{
			this.db = db;
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600A986 RID: 43398 RVA: 0x0028BE78 File Offset: 0x0028A078
		protected ErrorMoveActiveCopyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.db = (Guid)info.GetValue("db", typeof(Guid));
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x0600A987 RID: 43399 RVA: 0x0028BECD File Offset: 0x0028A0CD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("db", this.db);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x170036F1 RID: 14065
		// (get) Token: 0x0600A988 RID: 43400 RVA: 0x0028BEFE File Offset: 0x0028A0FE
		public Guid Db
		{
			get
			{
				return this.db;
			}
		}

		// Token: 0x170036F2 RID: 14066
		// (get) Token: 0x0600A989 RID: 43401 RVA: 0x0028BF06 File Offset: 0x0028A106
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x04006057 RID: 24663
		private readonly Guid db;

		// Token: 0x04006058 RID: 24664
		private readonly string errorMsg;
	}
}
