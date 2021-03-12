using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000485 RID: 1157
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmBcsSingleCopyValidationException : AmBcsException
	{
		// Token: 0x06002C41 RID: 11329 RVA: 0x000BF025 File Offset: 0x000BD225
		public AmBcsSingleCopyValidationException(string bcsMessage) : base(ReplayStrings.AmBcsSingleCopyValidationException(bcsMessage))
		{
			this.bcsMessage = bcsMessage;
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000BF03F File Offset: 0x000BD23F
		public AmBcsSingleCopyValidationException(string bcsMessage, Exception innerException) : base(ReplayStrings.AmBcsSingleCopyValidationException(bcsMessage), innerException)
		{
			this.bcsMessage = bcsMessage;
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000BF05A File Offset: 0x000BD25A
		protected AmBcsSingleCopyValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.bcsMessage = (string)info.GetValue("bcsMessage", typeof(string));
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000BF084 File Offset: 0x000BD284
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("bcsMessage", this.bcsMessage);
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x000BF09F File Offset: 0x000BD29F
		public string BcsMessage
		{
			get
			{
				return this.bcsMessage;
			}
		}

		// Token: 0x040014D4 RID: 5332
		private readonly string bcsMessage;
	}
}
