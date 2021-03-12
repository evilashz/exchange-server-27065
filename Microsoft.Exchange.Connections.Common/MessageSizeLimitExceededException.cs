using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200003D RID: 61
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MessageSizeLimitExceededException : ItemLevelTransientException
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00003B92 File Offset: 0x00001D92
		public MessageSizeLimitExceededException(string limitExceededMsg) : base(CXStrings.MessageSizeLimitExceededError(limitExceededMsg))
		{
			this.limitExceededMsg = limitExceededMsg;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00003BAC File Offset: 0x00001DAC
		public MessageSizeLimitExceededException(string limitExceededMsg, Exception innerException) : base(CXStrings.MessageSizeLimitExceededError(limitExceededMsg), innerException)
		{
			this.limitExceededMsg = limitExceededMsg;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003BC7 File Offset: 0x00001DC7
		protected MessageSizeLimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.limitExceededMsg = (string)info.GetValue("limitExceededMsg", typeof(string));
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003BF1 File Offset: 0x00001DF1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("limitExceededMsg", this.limitExceededMsg);
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00003C0C File Offset: 0x00001E0C
		public string LimitExceededMsg
		{
			get
			{
				return this.limitExceededMsg;
			}
		}

		// Token: 0x040000D5 RID: 213
		private readonly string limitExceededMsg;
	}
}
