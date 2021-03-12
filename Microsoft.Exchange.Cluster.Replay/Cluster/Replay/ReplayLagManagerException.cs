using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004E8 RID: 1256
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayLagManagerException : LocalizedException
	{
		// Token: 0x06002E76 RID: 11894 RVA: 0x000C3736 File Offset: 0x000C1936
		public ReplayLagManagerException(string errorMsg) : base(ReplayStrings.ReplayLagManagerException(errorMsg))
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x000C374B File Offset: 0x000C194B
		public ReplayLagManagerException(string errorMsg, Exception innerException) : base(ReplayStrings.ReplayLagManagerException(errorMsg), innerException)
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x000C3761 File Offset: 0x000C1961
		protected ReplayLagManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x000C378B File Offset: 0x000C198B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06002E7A RID: 11898 RVA: 0x000C37A6 File Offset: 0x000C19A6
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x0400157D RID: 5501
		private readonly string errorMsg;
	}
}
