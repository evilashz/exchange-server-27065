using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000478 RID: 1144
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbActionRejectedLastAdminActionDidNotSucceedException : AmCommonException
	{
		// Token: 0x06002BF3 RID: 11251 RVA: 0x000BE559 File Offset: 0x000BC759
		public AmDbActionRejectedLastAdminActionDidNotSucceedException(string actionCode) : base(ReplayStrings.AmDbActionRejectedLastAdminActionDidNotSucceedException(actionCode))
		{
			this.actionCode = actionCode;
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x000BE573 File Offset: 0x000BC773
		public AmDbActionRejectedLastAdminActionDidNotSucceedException(string actionCode, Exception innerException) : base(ReplayStrings.AmDbActionRejectedLastAdminActionDidNotSucceedException(actionCode), innerException)
		{
			this.actionCode = actionCode;
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x000BE58E File Offset: 0x000BC78E
		protected AmDbActionRejectedLastAdminActionDidNotSucceedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.actionCode = (string)info.GetValue("actionCode", typeof(string));
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x000BE5B8 File Offset: 0x000BC7B8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("actionCode", this.actionCode);
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x000BE5D3 File Offset: 0x000BC7D3
		public string ActionCode
		{
			get
			{
				return this.actionCode;
			}
		}

		// Token: 0x040014BA RID: 5306
		private readonly string actionCode;
	}
}
