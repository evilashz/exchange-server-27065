using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200037A RID: 890
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ProxyPropertyBag : PropertyBag
	{
		// Token: 0x06002757 RID: 10071 RVA: 0x0009D76F File Offset: 0x0009B96F
		protected ProxyPropertyBag(PropertyBag propertyBag)
		{
			this.propertyBag = propertyBag;
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x0009D77E File Offset: 0x0009B97E
		public override void Load(ICollection<PropertyDefinition> propsToLoad)
		{
			this.propertyBag.Load(propsToLoad);
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06002759 RID: 10073 RVA: 0x0009D78C File Offset: 0x0009B98C
		public override bool IsDirty
		{
			get
			{
				return this.propertyBag.IsDirty;
			}
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x0009D799 File Offset: 0x0009B999
		protected override bool InternalIsPropertyDirty(AtomicStorePropertyDefinition propertyDefinition)
		{
			return ((IDirectPropertyBag)this.propertyBag).IsDirty(propertyDefinition);
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x0009D7A7 File Offset: 0x0009B9A7
		protected override void SetValidatedStoreProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
		{
			((IDirectPropertyBag)this.propertyBag).SetValue(propertyDefinition, propertyValue);
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x0009D7B6 File Offset: 0x0009B9B6
		protected override object TryGetStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			return ((IDirectPropertyBag)this.propertyBag).GetValue(propertyDefinition);
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x0009D7C4 File Offset: 0x0009B9C4
		protected override void DeleteStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			((IDirectPropertyBag)this.propertyBag).Delete(propertyDefinition);
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x0009D7D2 File Offset: 0x0009B9D2
		protected override bool IsLoaded(NativeStorePropertyDefinition propertyDefinition)
		{
			return ((IDirectPropertyBag)this.propertyBag).IsLoaded(propertyDefinition);
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x0600275F RID: 10079 RVA: 0x0009D7E0 File Offset: 0x0009B9E0
		// (set) Token: 0x06002760 RID: 10080 RVA: 0x0009D7ED File Offset: 0x0009B9ED
		internal override ExTimeZone ExTimeZone
		{
			get
			{
				return this.propertyBag.ExTimeZone;
			}
			set
			{
				this.propertyBag.ExTimeZone = value;
			}
		}

		// Token: 0x04001742 RID: 5954
		private readonly PropertyBag propertyBag;
	}
}
