using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CB3 RID: 3251
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class RecipientToIndexProperty : SmartPropertyDefinition
	{
		// Token: 0x06007137 RID: 28983 RVA: 0x001F635C File Offset: 0x001F455C
		internal RecipientToIndexProperty(string displayName, RecipientItemType? recipientItemType, NativeStorePropertyDefinition nativeStoreProperty) : base(displayName, typeof(List<RecipientToIndex>), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.GroupExpansionRecipients, PropertyDependencyType.NeedForRead)
		})
		{
			this.recipientItemType = recipientItemType;
			this.nativeStoreProperty = nativeStoreProperty;
		}

		// Token: 0x17001E53 RID: 7763
		// (get) Token: 0x06007138 RID: 28984 RVA: 0x001F63A4 File Offset: 0x001F45A4
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return base.Capabilities | StorePropertyCapabilities.CanQuery;
			}
		}

		// Token: 0x06007139 RID: 28985 RVA: 0x001F63C8 File Offset: 0x001F45C8
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			MessageItem messageItem = propertyBag.Context.StoreObject as MessageItem;
			if (messageItem != null)
			{
				GroupExpansionRecipients groupExpansionRecipients = GroupExpansionRecipients.RetrieveFromStore(messageItem, InternalSchema.GroupExpansionRecipients);
				if (groupExpansionRecipients != null)
				{
					if (this.recipientItemType != null && this.recipientItemType != null)
					{
						RecipientItemType recipientType = this.recipientItemType.Value;
						return new List<RecipientToIndex>(from r in groupExpansionRecipients.Recipients
						where r.RecipientType == recipientType
						select r);
					}
					return groupExpansionRecipients.Recipients;
				}
			}
			return new PropertyError(this, PropertyErrorCode.GetCalculatedPropertyError);
		}

		// Token: 0x0600713A RID: 28986 RVA: 0x001F6460 File Offset: 0x001F4660
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return base.SinglePropertyNativeFilterToSmartFilter(filter, this.nativeStoreProperty);
		}

		// Token: 0x0600713B RID: 28987 RVA: 0x001F646F File Offset: 0x001F466F
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return base.SinglePropertySmartFilterToNativeFilter(filter, this.nativeStoreProperty);
		}

		// Token: 0x04004EA1 RID: 20129
		private readonly RecipientItemType? recipientItemType;

		// Token: 0x04004EA2 RID: 20130
		private readonly NativeStorePropertyDefinition nativeStoreProperty;
	}
}
