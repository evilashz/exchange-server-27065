using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000437 RID: 1079
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederOperationFailedException : SeederServerException
	{
		// Token: 0x06002AA0 RID: 10912 RVA: 0x000BBF69 File Offset: 0x000BA169
		public SeederOperationFailedException(string errMessage) : base(ReplayStrings.SeederOperationFailedException(errMessage))
		{
			this.errMessage = errMessage;
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x000BBF83 File Offset: 0x000BA183
		public SeederOperationFailedException(string errMessage, Exception innerException) : base(ReplayStrings.SeederOperationFailedException(errMessage), innerException)
		{
			this.errMessage = errMessage;
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x000BBF9E File Offset: 0x000BA19E
		protected SeederOperationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMessage = (string)info.GetValue("errMessage", typeof(string));
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000BBFC8 File Offset: 0x000BA1C8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMessage", this.errMessage);
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06002AA4 RID: 10916 RVA: 0x000BBFE3 File Offset: 0x000BA1E3
		public string ErrMessage
		{
			get
			{
				return this.errMessage;
			}
		}

		// Token: 0x0400146B RID: 5227
		private readonly string errMessage;
	}
}
