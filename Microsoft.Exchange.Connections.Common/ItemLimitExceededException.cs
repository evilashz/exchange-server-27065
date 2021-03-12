using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200003C RID: 60
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ItemLimitExceededException : ItemLevelTransientException
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00003B10 File Offset: 0x00001D10
		public ItemLimitExceededException(string limitExceededMsg) : base(CXStrings.ItemLimitExceededExceptionMsg(limitExceededMsg))
		{
			this.limitExceededMsg = limitExceededMsg;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00003B2A File Offset: 0x00001D2A
		public ItemLimitExceededException(string limitExceededMsg, Exception innerException) : base(CXStrings.ItemLimitExceededExceptionMsg(limitExceededMsg), innerException)
		{
			this.limitExceededMsg = limitExceededMsg;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00003B45 File Offset: 0x00001D45
		protected ItemLimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.limitExceededMsg = (string)info.GetValue("limitExceededMsg", typeof(string));
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003B6F File Offset: 0x00001D6F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("limitExceededMsg", this.limitExceededMsg);
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00003B8A File Offset: 0x00001D8A
		public string LimitExceededMsg
		{
			get
			{
				return this.limitExceededMsg;
			}
		}

		// Token: 0x040000D4 RID: 212
		private readonly string limitExceededMsg;
	}
}
