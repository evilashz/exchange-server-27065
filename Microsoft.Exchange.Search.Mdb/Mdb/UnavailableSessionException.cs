using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000039 RID: 57
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnavailableSessionException : OperationFailedException
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000D2FF File Offset: 0x0000B4FF
		public UnavailableSessionException(StoreSessionCacheKey key) : base(Strings.UnavailableSession(key))
		{
			this.key = key;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000D314 File Offset: 0x0000B514
		public UnavailableSessionException(StoreSessionCacheKey key, Exception innerException) : base(Strings.UnavailableSession(key), innerException)
		{
			this.key = key;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000D32A File Offset: 0x0000B52A
		protected UnavailableSessionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.key = (StoreSessionCacheKey)info.GetValue("key", typeof(StoreSessionCacheKey));
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000D354 File Offset: 0x0000B554
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("key", this.key);
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000D36F File Offset: 0x0000B56F
		public StoreSessionCacheKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x04000136 RID: 310
		private readonly StoreSessionCacheKey key;
	}
}
