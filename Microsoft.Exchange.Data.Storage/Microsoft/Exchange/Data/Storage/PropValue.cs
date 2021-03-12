using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CAA RID: 3242
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct PropValue : IEquatable<PropValue>
	{
		// Token: 0x06007102 RID: 28930 RVA: 0x001F53D4 File Offset: 0x001F35D4
		public PropValue(StorePropertyDefinition propDef, object value)
		{
			if (propDef == null)
			{
				throw new ArgumentNullException("propDef");
			}
			if (value == null)
			{
				throw PropertyError.ToException(new PropertyError[]
				{
					new PropertyError(propDef, PropertyErrorCode.NullValue)
				});
			}
			this.propDef = propDef;
			this.value = value;
		}

		// Token: 0x06007103 RID: 28931 RVA: 0x001F5418 File Offset: 0x001F3618
		public static PropValue CreatePropValue<T>(T propDef, object value) where T : StorePropertyDefinition
		{
			return new PropValue(propDef, value);
		}

		// Token: 0x17001E47 RID: 7751
		// (get) Token: 0x06007104 RID: 28932 RVA: 0x001F5426 File Offset: 0x001F3626
		public StorePropertyDefinition Property
		{
			get
			{
				return this.propDef;
			}
		}

		// Token: 0x17001E48 RID: 7752
		// (get) Token: 0x06007105 RID: 28933 RVA: 0x001F542E File Offset: 0x001F362E
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06007106 RID: 28934 RVA: 0x001F5436 File Offset: 0x001F3636
		public static implicit operator KeyValuePair<StorePropertyDefinition, object>(PropValue propValue)
		{
			return new KeyValuePair<StorePropertyDefinition, object>(propValue.propDef, propValue.value);
		}

		// Token: 0x06007107 RID: 28935 RVA: 0x001F544B File Offset: 0x001F364B
		public static implicit operator KeyValuePair<PropertyDefinition, object>(PropValue propValue)
		{
			return new KeyValuePair<PropertyDefinition, object>(propValue.propDef, propValue.value);
		}

		// Token: 0x06007108 RID: 28936 RVA: 0x001F5460 File Offset: 0x001F3660
		public static implicit operator PropValue(KeyValuePair<StorePropertyDefinition, object> kvp)
		{
			return new PropValue(kvp.Key, kvp.Value);
		}

		// Token: 0x06007109 RID: 28937 RVA: 0x001F5475 File Offset: 0x001F3675
		public static explicit operator PropValue(KeyValuePair<PropertyDefinition, object> kvp)
		{
			return new PropValue(InternalSchema.ToStorePropertyDefinition(kvp.Key), kvp.Value);
		}

		// Token: 0x0600710A RID: 28938 RVA: 0x001F564C File Offset: 0x001F384C
		public static IEnumerable<PropValue> ConvertEnumerator<PropDefType>(IEnumerable<KeyValuePair<PropDefType, object>> sourceEnumerator) where PropDefType : PropertyDefinition
		{
			if (sourceEnumerator != null)
			{
				foreach (KeyValuePair<PropDefType, object> pair in sourceEnumerator)
				{
					KeyValuePair<PropDefType, object> keyValuePair = pair;
					StorePropertyDefinition storePropertyDefinition = InternalSchema.ToStorePropertyDefinition(keyValuePair.Key);
					KeyValuePair<PropDefType, object> keyValuePair2 = pair;
					yield return new PropValue(storePropertyDefinition, keyValuePair2.Value);
				}
			}
			yield break;
		}

		// Token: 0x0600710B RID: 28939 RVA: 0x001F5669 File Offset: 0x001F3869
		public bool Equals(PropValue other)
		{
			return this.propDef.Equals(other.propDef) && Util.ValueEquals(this.value, other.value);
		}

		// Token: 0x0600710C RID: 28940 RVA: 0x001F5693 File Offset: 0x001F3893
		public override int GetHashCode()
		{
			return this.propDef.GetHashCode() ^ this.value.GetHashCode();
		}

		// Token: 0x0600710D RID: 28941 RVA: 0x001F56AC File Offset: 0x001F38AC
		public override bool Equals(object obj)
		{
			return obj is PropValue && this.Equals((PropValue)obj);
		}

		// Token: 0x04004E90 RID: 20112
		private readonly StorePropertyDefinition propDef;

		// Token: 0x04004E91 RID: 20113
		private readonly object value;
	}
}
