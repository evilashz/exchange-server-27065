using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x0200003A RID: 58
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MailboxQuarantinedException : OperationFailedException
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x0000D377 File Offset: 0x0000B577
		public MailboxQuarantinedException(StoreSessionCacheKey key) : base(Strings.MailboxQuarantined(key))
		{
			this.key = key;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000D38C File Offset: 0x0000B58C
		public MailboxQuarantinedException(StoreSessionCacheKey key, Exception innerException) : base(Strings.MailboxQuarantined(key), innerException)
		{
			this.key = key;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000D3A2 File Offset: 0x0000B5A2
		protected MailboxQuarantinedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.key = (StoreSessionCacheKey)info.GetValue("key", typeof(StoreSessionCacheKey));
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000D3CC File Offset: 0x0000B5CC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("key", this.key);
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000D3E7 File Offset: 0x0000B5E7
		public StoreSessionCacheKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x04000137 RID: 311
		private readonly StoreSessionCacheKey key;
	}
}
