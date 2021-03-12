using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000513 RID: 1299
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseCopyLayoutException : LocalizedException
	{
		// Token: 0x06002F74 RID: 12148 RVA: 0x000C5989 File Offset: 0x000C3B89
		public DatabaseCopyLayoutException(string errorMsg) : base(ReplayStrings.DatabaseCopyLayoutException(errorMsg))
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x000C599E File Offset: 0x000C3B9E
		public DatabaseCopyLayoutException(string errorMsg, Exception innerException) : base(ReplayStrings.DatabaseCopyLayoutException(errorMsg), innerException)
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x000C59B4 File Offset: 0x000C3BB4
		protected DatabaseCopyLayoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x000C59DE File Offset: 0x000C3BDE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06002F78 RID: 12152 RVA: 0x000C59F9 File Offset: 0x000C3BF9
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x040015CF RID: 5583
		private readonly string errorMsg;
	}
}
