using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003D6 RID: 982
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskOperationFailedException : DagTaskServerException
	{
		// Token: 0x06002898 RID: 10392 RVA: 0x000B8310 File Offset: 0x000B6510
		public DagTaskOperationFailedException(string errMessage) : base(ReplayStrings.DagTaskOperationFailedException(errMessage))
		{
			this.errMessage = errMessage;
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000B832A File Offset: 0x000B652A
		public DagTaskOperationFailedException(string errMessage, Exception innerException) : base(ReplayStrings.DagTaskOperationFailedException(errMessage), innerException)
		{
			this.errMessage = errMessage;
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x000B8345 File Offset: 0x000B6545
		protected DagTaskOperationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMessage = (string)info.GetValue("errMessage", typeof(string));
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x000B836F File Offset: 0x000B656F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMessage", this.errMessage);
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x0600289C RID: 10396 RVA: 0x000B838A File Offset: 0x000B658A
		public string ErrMessage
		{
			get
			{
				return this.errMessage;
			}
		}

		// Token: 0x040013E7 RID: 5095
		private readonly string errMessage;
	}
}
