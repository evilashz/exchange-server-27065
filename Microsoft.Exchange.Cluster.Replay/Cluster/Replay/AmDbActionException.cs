using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200045E RID: 1118
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbActionException : AmCommonException
	{
		// Token: 0x06002B6B RID: 11115 RVA: 0x000BD5FA File Offset: 0x000BB7FA
		public AmDbActionException(string actionError) : base(ReplayStrings.AmDbActionException(actionError))
		{
			this.actionError = actionError;
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x000BD614 File Offset: 0x000BB814
		public AmDbActionException(string actionError, Exception innerException) : base(ReplayStrings.AmDbActionException(actionError), innerException)
		{
			this.actionError = actionError;
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x000BD62F File Offset: 0x000BB82F
		protected AmDbActionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.actionError = (string)info.GetValue("actionError", typeof(string));
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x000BD659 File Offset: 0x000BB859
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("actionError", this.actionError);
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06002B6F RID: 11119 RVA: 0x000BD674 File Offset: 0x000BB874
		public string ActionError
		{
			get
			{
				return this.actionError;
			}
		}

		// Token: 0x0400149A RID: 5274
		private readonly string actionError;
	}
}
