using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004F6 RID: 1270
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseVolumeInfoException : LocalizedException
	{
		// Token: 0x06002EC3 RID: 11971 RVA: 0x000C4075 File Offset: 0x000C2275
		public DatabaseVolumeInfoException(string errorMsg) : base(ReplayStrings.DatabaseVolumeInfoException(errorMsg))
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x000C408A File Offset: 0x000C228A
		public DatabaseVolumeInfoException(string errorMsg, Exception innerException) : base(ReplayStrings.DatabaseVolumeInfoException(errorMsg), innerException)
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000C40A0 File Offset: 0x000C22A0
		protected DatabaseVolumeInfoException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000C40CA File Offset: 0x000C22CA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06002EC7 RID: 11975 RVA: 0x000C40E5 File Offset: 0x000C22E5
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x04001592 RID: 5522
		private readonly string errorMsg;
	}
}
