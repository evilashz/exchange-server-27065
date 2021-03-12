using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004EA RID: 1258
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseValidationException : LocalizedException
	{
		// Token: 0x06002E81 RID: 11905 RVA: 0x000C3885 File Offset: 0x000C1A85
		public DatabaseValidationException(string errorMsg) : base(ReplayStrings.DatabaseValidationException(errorMsg))
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x000C389A File Offset: 0x000C1A9A
		public DatabaseValidationException(string errorMsg, Exception innerException) : base(ReplayStrings.DatabaseValidationException(errorMsg), innerException)
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x000C38B0 File Offset: 0x000C1AB0
		protected DatabaseValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000C38DA File Offset: 0x000C1ADA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06002E85 RID: 11909 RVA: 0x000C38F5 File Offset: 0x000C1AF5
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x04001580 RID: 5504
		private readonly string errorMsg;
	}
}
