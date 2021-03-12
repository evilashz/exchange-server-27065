using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x020010A1 RID: 4257
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SubscriptionCacheOperationFailedException : LocalizedException
	{
		// Token: 0x0600B224 RID: 45604 RVA: 0x00299710 File Offset: 0x00297910
		public SubscriptionCacheOperationFailedException(LocalizedString info) : base(Strings.SubscriptionCacheOperationFailed(info))
		{
			this.info = info;
		}

		// Token: 0x0600B225 RID: 45605 RVA: 0x00299725 File Offset: 0x00297925
		public SubscriptionCacheOperationFailedException(LocalizedString info, Exception innerException) : base(Strings.SubscriptionCacheOperationFailed(info), innerException)
		{
			this.info = info;
		}

		// Token: 0x0600B226 RID: 45606 RVA: 0x0029973B File Offset: 0x0029793B
		protected SubscriptionCacheOperationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.info = (LocalizedString)info.GetValue("info", typeof(LocalizedString));
		}

		// Token: 0x0600B227 RID: 45607 RVA: 0x00299765 File Offset: 0x00297965
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("info", this.info);
		}

		// Token: 0x170038BD RID: 14525
		// (get) Token: 0x0600B228 RID: 45608 RVA: 0x00299785 File Offset: 0x00297985
		public LocalizedString Info
		{
			get
			{
				return this.info;
			}
		}

		// Token: 0x04006223 RID: 25123
		private readonly LocalizedString info;
	}
}
