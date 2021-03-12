using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000014 RID: 20
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SubscriptionNumberExceedLimitException : LocalizedException
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00004EAD File Offset: 0x000030AD
		public SubscriptionNumberExceedLimitException(int number) : base(Strings.SubscriptionNumberExceedLimit(number))
		{
			this.number = number;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004EC2 File Offset: 0x000030C2
		public SubscriptionNumberExceedLimitException(int number, Exception innerException) : base(Strings.SubscriptionNumberExceedLimit(number), innerException)
		{
			this.number = number;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004ED8 File Offset: 0x000030D8
		protected SubscriptionNumberExceedLimitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.number = (int)info.GetValue("number", typeof(int));
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004F02 File Offset: 0x00003102
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("number", this.number);
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00004F1D File Offset: 0x0000311D
		public int Number
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x040000D8 RID: 216
		private readonly int number;
	}
}
