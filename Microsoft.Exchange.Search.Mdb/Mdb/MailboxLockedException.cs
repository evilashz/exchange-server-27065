using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x0200003B RID: 59
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MailboxLockedException : OperationFailedException
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000D3EF File Offset: 0x0000B5EF
		public MailboxLockedException(StoreSessionCacheKey key) : base(Strings.MailboxLocked(key))
		{
			this.key = key;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000D404 File Offset: 0x0000B604
		public MailboxLockedException(StoreSessionCacheKey key, Exception innerException) : base(Strings.MailboxLocked(key), innerException)
		{
			this.key = key;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000D41A File Offset: 0x0000B61A
		protected MailboxLockedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.key = (StoreSessionCacheKey)info.GetValue("key", typeof(StoreSessionCacheKey));
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000D444 File Offset: 0x0000B644
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("key", this.key);
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000D45F File Offset: 0x0000B65F
		public StoreSessionCacheKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x04000138 RID: 312
		private readonly StoreSessionCacheKey key;
	}
}
