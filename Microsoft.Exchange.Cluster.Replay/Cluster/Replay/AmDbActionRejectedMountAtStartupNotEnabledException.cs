using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000476 RID: 1142
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbActionRejectedMountAtStartupNotEnabledException : AmCommonException
	{
		// Token: 0x06002BE9 RID: 11241 RVA: 0x000BE455 File Offset: 0x000BC655
		public AmDbActionRejectedMountAtStartupNotEnabledException(string actionCode) : base(ReplayStrings.AmDbActionRejectedMountAtStartupNotEnabledException(actionCode))
		{
			this.actionCode = actionCode;
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x000BE46F File Offset: 0x000BC66F
		public AmDbActionRejectedMountAtStartupNotEnabledException(string actionCode, Exception innerException) : base(ReplayStrings.AmDbActionRejectedMountAtStartupNotEnabledException(actionCode), innerException)
		{
			this.actionCode = actionCode;
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x000BE48A File Offset: 0x000BC68A
		protected AmDbActionRejectedMountAtStartupNotEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.actionCode = (string)info.GetValue("actionCode", typeof(string));
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x000BE4B4 File Offset: 0x000BC6B4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("actionCode", this.actionCode);
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06002BED RID: 11245 RVA: 0x000BE4CF File Offset: 0x000BC6CF
		public string ActionCode
		{
			get
			{
				return this.actionCode;
			}
		}

		// Token: 0x040014B8 RID: 5304
		private readonly string actionCode;
	}
}
