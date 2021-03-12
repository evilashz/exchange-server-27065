using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200045F RID: 1119
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbActionTransientException : AmCommonTransientException
	{
		// Token: 0x06002B70 RID: 11120 RVA: 0x000BD67C File Offset: 0x000BB87C
		public AmDbActionTransientException(string actionError) : base(ReplayStrings.AmDbActionTransientException(actionError))
		{
			this.actionError = actionError;
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000BD696 File Offset: 0x000BB896
		public AmDbActionTransientException(string actionError, Exception innerException) : base(ReplayStrings.AmDbActionTransientException(actionError), innerException)
		{
			this.actionError = actionError;
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000BD6B1 File Offset: 0x000BB8B1
		protected AmDbActionTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.actionError = (string)info.GetValue("actionError", typeof(string));
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x000BD6DB File Offset: 0x000BB8DB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("actionError", this.actionError);
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06002B74 RID: 11124 RVA: 0x000BD6F6 File Offset: 0x000BB8F6
		public string ActionError
		{
			get
			{
				return this.actionError;
			}
		}

		// Token: 0x0400149B RID: 5275
		private readonly string actionError;
	}
}
