using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003A5 RID: 933
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmTransientException : AmServerTransientException
	{
		// Token: 0x06002789 RID: 10121 RVA: 0x000B621A File Offset: 0x000B441A
		public AmTransientException(string errMessage) : base(ReplayStrings.AmTransientException(errMessage))
		{
			this.errMessage = errMessage;
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000B6234 File Offset: 0x000B4434
		public AmTransientException(string errMessage, Exception innerException) : base(ReplayStrings.AmTransientException(errMessage), innerException)
		{
			this.errMessage = errMessage;
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x000B624F File Offset: 0x000B444F
		protected AmTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMessage = (string)info.GetValue("errMessage", typeof(string));
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x000B6279 File Offset: 0x000B4479
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMessage", this.errMessage);
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x000B6294 File Offset: 0x000B4494
		public string ErrMessage
		{
			get
			{
				return this.errMessage;
			}
		}

		// Token: 0x0400139C RID: 5020
		private readonly string errMessage;
	}
}
