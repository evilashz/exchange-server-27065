using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Cache;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	public class SubscriptionsCache : ConfigurableObject
	{
		// Token: 0x06000274 RID: 628 RVA: 0x0000BB69 File Offset: 0x00009D69
		public SubscriptionsCache() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000BB76 File Offset: 0x00009D76
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000BB80 File Offset: 0x00009D80
		public List<SubscriptionCacheObject> SubscriptionCacheObjects
		{
			get
			{
				return this.cacheObjects;
			}
			internal set
			{
				this.cacheObjects = value;
				if (this.cacheObjects != null)
				{
					foreach (SubscriptionCacheObject subscriptionCacheObject in this.cacheObjects)
					{
						if (subscriptionCacheObject.ObjectState != SubscriptionCacheObjectState.Valid)
						{
							this.invalid = true;
							break;
						}
					}
				}
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000BBEC File Offset: 0x00009DEC
		public override ObjectId Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		public override bool IsValid
		{
			get
			{
				return !this.invalid;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000BBFF File Offset: 0x00009DFF
		public ObjectState CacheObjectState
		{
			get
			{
				return base.ObjectState;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000BC07 File Offset: 0x00009E07
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return SubscriptionsCache.schema;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000BC0E File Offset: 0x00009E0E
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0000BC16 File Offset: 0x00009E16
		internal string FailureReason
		{
			get
			{
				return this.failureReason;
			}
			set
			{
				this.failureReason = value;
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000BC1F File Offset: 0x00009E1F
		internal void SetIdentity(ADObjectId userIdentity)
		{
			SyncUtilities.ThrowIfArgumentNull("userIdentity", userIdentity);
			this.identity = userIdentity;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000BC34 File Offset: 0x00009E34
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			if (!string.IsNullOrEmpty(this.failureReason))
			{
				ValidationError item = new CacheValidationError(new LocalizedString(this.failureReason));
				errors.Add(item);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000BC70 File Offset: 0x00009E70
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (!string.IsNullOrEmpty(this.failureReason))
			{
				ValidationError item = new CacheValidationError(new LocalizedString(this.failureReason));
				errors.Add(item);
			}
		}

		// Token: 0x040000B0 RID: 176
		private static readonly SimpleProviderObjectSchema schema = ObjectSchema.GetInstance<SimpleProviderObjectSchema>();

		// Token: 0x040000B1 RID: 177
		private static readonly ValidationError[] EmptyValidationError = new ValidationError[0];

		// Token: 0x040000B2 RID: 178
		private List<SubscriptionCacheObject> cacheObjects;

		// Token: 0x040000B3 RID: 179
		private ADObjectId identity;

		// Token: 0x040000B4 RID: 180
		private bool invalid;

		// Token: 0x040000B5 RID: 181
		private string failureReason;
	}
}
