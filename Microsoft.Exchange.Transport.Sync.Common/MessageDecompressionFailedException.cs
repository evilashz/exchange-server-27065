using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000026 RID: 38
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MessageDecompressionFailedException : LocalizedException
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00005572 File Offset: 0x00003772
		public MessageDecompressionFailedException(string serverId) : base(Strings.MessageDecompressionFailedException(serverId))
		{
			this.serverId = serverId;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005587 File Offset: 0x00003787
		public MessageDecompressionFailedException(string serverId, Exception innerException) : base(Strings.MessageDecompressionFailedException(serverId), innerException)
		{
			this.serverId = serverId;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000559D File Offset: 0x0000379D
		protected MessageDecompressionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverId = (string)info.GetValue("serverId", typeof(string));
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000055C7 File Offset: 0x000037C7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverId", this.serverId);
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000055E2 File Offset: 0x000037E2
		public string ServerId
		{
			get
			{
				return this.serverId;
			}
		}

		// Token: 0x040000E4 RID: 228
		private readonly string serverId;
	}
}
