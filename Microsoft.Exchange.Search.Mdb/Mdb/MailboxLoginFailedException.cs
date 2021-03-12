using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x0200003C RID: 60
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MailboxLoginFailedException : OperationFailedException
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x0000D467 File Offset: 0x0000B667
		public MailboxLoginFailedException(StoreSessionCacheKey key) : base(Strings.MailboxLoginFailed(key))
		{
			this.key = key;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000D47C File Offset: 0x0000B67C
		public MailboxLoginFailedException(StoreSessionCacheKey key, Exception innerException) : base(Strings.MailboxLoginFailed(key), innerException)
		{
			this.key = key;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000D492 File Offset: 0x0000B692
		protected MailboxLoginFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.key = (StoreSessionCacheKey)info.GetValue("key", typeof(StoreSessionCacheKey));
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000D4BC File Offset: 0x0000B6BC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("key", this.key);
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000D4D7 File Offset: 0x0000B6D7
		public StoreSessionCacheKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x04000139 RID: 313
		private readonly StoreSessionCacheKey key;
	}
}
