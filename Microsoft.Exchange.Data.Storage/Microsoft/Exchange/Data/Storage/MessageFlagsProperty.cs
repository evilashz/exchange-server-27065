using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C61 RID: 3169
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class MessageFlagsProperty : FlagsProperty
	{
		// Token: 0x06006FA2 RID: 28578 RVA: 0x001E0BD0 File Offset: 0x001DEDD0
		internal MessageFlagsProperty(string displayName, NativeStorePropertyDefinition nativeProperty, int flag) : base(displayName, nativeProperty, flag, PropertyDefinitionConstraint.None)
		{
		}

		// Token: 0x06006FA3 RID: 28579 RVA: 0x001E0BE0 File Offset: 0x001DEDE0
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			Item item = propertyBag.Context.StoreObject as Item;
			if (item == null)
			{
				return base.InternalTryGetValue(propertyBag);
			}
			bool? flagsApiProperties = item.GetFlagsApiProperties(base.NativeProperty, base.Flag);
			if (flagsApiProperties == null)
			{
				return base.InternalTryGetValue(propertyBag);
			}
			return flagsApiProperties.GetValueOrDefault();
		}

		// Token: 0x06006FA4 RID: 28580 RVA: 0x001E0C3C File Offset: 0x001DEE3C
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			if (!(value is bool))
			{
				string message = ServerStrings.ExInvalidValueForFlagsCalculatedProperty(base.Flag);
				ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), message);
				throw new ArgumentException(message);
			}
			Item item = propertyBag.Context.StoreObject as Item;
			if (item != null)
			{
				item.SetFlagsApiProperties(base.NativeProperty, base.Flag, (bool)value);
				return;
			}
			base.InternalSetValue(propertyBag, value);
		}
	}
}
