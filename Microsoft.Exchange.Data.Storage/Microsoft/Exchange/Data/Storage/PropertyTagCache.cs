using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000A5 RID: 165
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PropertyTagCache
	{
		// Token: 0x06000B6E RID: 2926 RVA: 0x000503A4 File Offset: 0x0004E5A4
		public NativeStorePropertyDefinition[] GetPropertyDefinitionsIgnoreTypeChecking(StoreSession session, ICorePropertyBag corePropertyBag, uint[] propertyTags)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(corePropertyBag, "corePropertyBag");
			Util.ThrowOnNullArgument(propertyTags, "propertyTags");
			NativeStorePropertyDefinition[] result = null;
			uint num;
			if (!this.TryGetPropertyDefinitionsFromPropertyTags(session, corePropertyBag, propertyTags, NativeStorePropertyDefinition.TypeCheckingFlag.DisableTypeCheck, out result, out num))
			{
				throw new ResolvePropertyDefinitionException(num, ServerStrings.CannotResolvePropertyTagsToPropertyDefinitions(num));
			}
			return result;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x000503F4 File Offset: 0x0004E5F4
		public bool TryGetPropertyDefinitionsFromPropertyTagsWithCompatibleTypes(StoreSession session, ICorePropertyBag corePropertyBag, uint[] propertyTags, out NativeStorePropertyDefinition[] propertyDefinitions)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(corePropertyBag, "corePropertyBag");
			Util.ThrowOnNullArgument(propertyTags, "propertyTags");
			uint num;
			return this.TryGetPropertyDefinitionsFromPropertyTags(session, corePropertyBag, propertyTags, NativeStorePropertyDefinition.TypeCheckingFlag.AllowCompatibleType, out propertyDefinitions, out num);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00050430 File Offset: 0x0004E630
		public bool TryGetPropertyDefinitionsFromPropertyTags(StoreSession session, ICorePropertyBag corePropertyBag, uint[] propertyTags, out NativeStorePropertyDefinition[] propertyDefinitions)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(corePropertyBag, "corePropertyBag");
			Util.ThrowOnNullArgument(propertyTags, "propertyTags");
			uint num;
			return this.TryGetPropertyDefinitionsFromPropertyTags(session, corePropertyBag, propertyTags, NativeStorePropertyDefinition.TypeCheckingFlag.DoNotCreateInvalidType, out propertyDefinitions, out num);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00050470 File Offset: 0x0004E670
		public ICollection<uint> PropertyTagsFromPropertyDefinitions(StoreSession session, ICollection<NativeStorePropertyDefinition> propertyDefinitions)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(propertyDefinitions, "propertyDefinitions");
			ICollection<PropTag> first = this.PropTagsFromPropertyDefinitions(session.Mailbox.MapiStore, session, propertyDefinitions);
			return from propTag in first
			select (uint)propTag;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x000504CA File Offset: 0x0004E6CA
		public void Reset()
		{
			NamedPropConverter.Reset();
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x000504D4 File Offset: 0x0004E6D4
		public NativeStorePropertyDefinition[] PropertyDefinitionsFromPropTags(NativeStorePropertyDefinition.TypeCheckingFlag propertyTypeCheckingFlag, MapiProp mapiProp, StoreSession storeSession, params PropTag[] propTags)
		{
			int num;
			return this.InternalPropertyDefinitionsFromPropTags(propertyTypeCheckingFlag, mapiProp, storeSession, propTags, out num);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x000504F0 File Offset: 0x0004E6F0
		internal static void ResolveAndFilterPropertyValues(NativeStorePropertyDefinition.TypeCheckingFlag typeCheckingFlag, StoreSession storeSession, MapiProp mapiProp, ExTimeZone exTimeZone, PropValue[] mapiPropValues, out NativeStorePropertyDefinition[] propertyDefinitions, out PropTag[] mapiPropTags, out object[] propertyValues)
		{
			PropTag[] array = new PropTag[mapiPropValues.Length];
			for (int i = 0; i < mapiPropValues.Length; i++)
			{
				array[i] = mapiPropValues[i].PropTag;
			}
			int num;
			NativeStorePropertyDefinition[] array2 = PropertyTagCache.Cache.InternalPropertyDefinitionsFromPropTags(typeCheckingFlag, mapiProp, storeSession, array, out num);
			propertyDefinitions = new NativeStorePropertyDefinition[num];
			mapiPropTags = new PropTag[num];
			propertyValues = new object[num];
			int num2 = 0;
			for (int j = 0; j < array2.Length; j++)
			{
				if (array2[j] != null)
				{
					object valueFromPropValue = MapiPropertyBag.GetValueFromPropValue(storeSession, exTimeZone, array2[j], mapiPropValues[j]);
					propertyDefinitions[num2] = array2[j];
					mapiPropTags[num2] = PropTagHelper.PropTagFromIdAndType(array[j].Id(), array2[j].MapiPropertyType);
					propertyValues[num2] = valueFromPropValue;
					num2++;
				}
			}
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x000505C0 File Offset: 0x0004E7C0
		internal static UnresolvedPropertyDefinition[] UnresolvedPropertyDefinitionsFromPropTags(IList<PropTag> propTags)
		{
			UnresolvedPropertyDefinition[] array = new UnresolvedPropertyDefinition[propTags.Count];
			for (int i = 0; i < propTags.Count; i++)
			{
				array[i] = UnresolvedPropertyDefinition.Create(propTags[i]);
			}
			return array;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x000505FA File Offset: 0x0004E7FA
		internal PropTag PropTagFromPropertyDefinition(MapiProp mapiProp, StoreSession storeSession, NativeStorePropertyDefinition propertyDefinition)
		{
			return this.PropTagFromPropertyDefinition(mapiProp, storeSession, false, true, propertyDefinition);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00050608 File Offset: 0x0004E808
		internal PropTag PropTagFromPropertyDefinition(MapiProp mapiProp, StoreSession storeSession, bool allowUnresolvedHeaders, bool allowCreate, NativeStorePropertyDefinition propertyDefinition)
		{
			PropertyTagPropertyDefinition propertyTagPropertyDefinition = propertyDefinition as PropertyTagPropertyDefinition;
			if (propertyTagPropertyDefinition != null)
			{
				return (PropTag)propertyTagPropertyDefinition.PropertyTag;
			}
			ICollection<PropTag> source = this.PropTagsFromPropertyDefinitions(mapiProp, storeSession, allowUnresolvedHeaders, allowCreate, true, new NativeStorePropertyDefinition[]
			{
				propertyDefinition
			});
			return source.First<PropTag>();
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00050647 File Offset: 0x0004E847
		internal ICollection<PropTag> PropTagsFromPropertyDefinitions(MapiProp mapiProp, StoreSession storeSession, IEnumerable<NativeStorePropertyDefinition> propertyDefinitions)
		{
			return this.PropTagsFromPropertyDefinitions(mapiProp, storeSession, false, propertyDefinitions);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00050653 File Offset: 0x0004E853
		internal ICollection<PropTag> PropTagsFromPropertyDefinitions<T>(MapiProp mapiProp, StoreSession storeSession, bool allowUnresolvedHeaders, IEnumerable<T> propertyDefinitions) where T : PropertyDefinition
		{
			return this.PropTagsFromPropertyDefinitions(mapiProp, storeSession, allowUnresolvedHeaders, true, true, propertyDefinitions.Cast<T, NativeStorePropertyDefinition>());
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00050667 File Offset: 0x0004E867
		internal ICollection<PropTag> PropTagsFromPropertyDefinitions<T>(MapiProp mapiProp, StoreSession storeSession, bool allowUnresolvedHeaders, bool allowCreate, bool allowCreateInternetHeaders, IEnumerable<T> propertyDefinitions) where T : PropertyDefinition
		{
			return this.PropTagsFromPropertyDefinitions(mapiProp, storeSession, allowUnresolvedHeaders, allowCreate, allowCreateInternetHeaders, propertyDefinitions.Cast<T, NativeStorePropertyDefinition>());
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0005067D File Offset: 0x0004E87D
		internal ICollection<PropTag> PropTagsFromPropertyDefinitions(MapiProp mapiProp, StoreSession storeSession, bool allowUnresolvedHeaders, IEnumerable<NativeStorePropertyDefinition> propertyDefinitions)
		{
			return this.PropTagsFromPropertyDefinitions(mapiProp, storeSession, allowUnresolvedHeaders, true, true, propertyDefinitions);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0005068C File Offset: 0x0004E88C
		internal ICollection<PropTag> PropTagsFromPropertyDefinitions(MapiProp mapiProp, StoreSession storeSession, bool allowUnresolvedHeaders, bool allowCreate, bool allowCreateInternet, IEnumerable<NativeStorePropertyDefinition> propertyDefinitions)
		{
			if (mapiProp == null)
			{
				throw new ArgumentNullException("mapiProp");
			}
			if (storeSession == null)
			{
				throw new ArgumentNullException("storeSession");
			}
			if (propertyDefinitions == null)
			{
				throw new ArgumentNullException("propertyDefinitions");
			}
			return new PropertyDefinitionToPropTagCollection(mapiProp, storeSession, allowUnresolvedHeaders, allowCreate, allowCreateInternet, propertyDefinitions);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x000506C8 File Offset: 0x0004E8C8
		internal NativeStorePropertyDefinition[] SafePropertyDefinitionsFromPropTags(StoreSession session, PropTag[] propTags)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(propTags, "propTags");
			NativeStorePropertyDefinition[] array;
			try
			{
				array = PropertyTagCache.Cache.PropertyDefinitionsFromPropTags(session.IsMoveSource ? NativeStorePropertyDefinition.TypeCheckingFlag.DisableTypeCheck : NativeStorePropertyDefinition.TypeCheckingFlag.ThrowOnInvalidType, session.Mailbox.MapiStore, session, propTags);
			}
			catch (InvalidPropertyTypeException innerException)
			{
				throw new CorruptDataException(ServerStrings.ExCorruptPropertyTag, innerException);
			}
			NativeStorePropertyDefinition[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i] == null)
				{
					throw new CorruptDataException(ServerStrings.ExCorruptPropertyTag);
				}
			}
			return array;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00050764 File Offset: 0x0004E964
		internal NativeStorePropertyDefinition[] InternalPropertyDefinitionsFromPropTags(NativeStorePropertyDefinition.TypeCheckingFlag propertyTypeCheckingFlag, MapiProp mapiProp, StoreSession storeSession, PropTag[] propTags, out int resolvedPropertyCount)
		{
			EnumValidator.ThrowIfInvalid<NativeStorePropertyDefinition.TypeCheckingFlag>(propertyTypeCheckingFlag, "propertyTypeCheckingFlag");
			resolvedPropertyCount = 0;
			NativeStorePropertyDefinition[] array = new NativeStorePropertyDefinition[propTags.Length];
			List<PropertyTagCache.NamedPropertyToResolve> list = null;
			PropTag[] array2 = null;
			for (int i = 0; i < propTags.Length; i++)
			{
				PropTag propTag = propTags[i];
				if (!PropertyTagCache.TryFixPropTagWithErrorType(storeSession, mapiProp, ref array2, ref propTag))
				{
					ExTraceGlobals.PropertyMappingTracer.TraceError<PropTag>((long)storeSession.GetHashCode(), "Failed to infer the property type for PropertyTag {0:X}", propTag);
				}
				else
				{
					PropertyTagCache.ChangeStringPropTagTypeToUnicode(ref propTag);
					int num = propTag.Id();
					if (num < 32768)
					{
						NativeStorePropertyDefinition nativeStorePropertyDefinition = PropertyTagPropertyDefinition.InternalCreateCustom(string.Empty, propTag, PropertyFlags.None, propertyTypeCheckingFlag);
						array[i] = nativeStorePropertyDefinition;
						if (nativeStorePropertyDefinition != null)
						{
							resolvedPropertyCount++;
						}
					}
					else
					{
						if (list == null)
						{
							list = new List<PropertyTagCache.NamedPropertyToResolve>();
						}
						list.Add(new PropertyTagCache.NamedPropertyToResolve((ushort)num, propTag.ValueType(), i));
					}
				}
			}
			if (list != null)
			{
				NamedProp[] namedPropsFromIds = NamedPropConverter.GetNamedPropsFromIds(storeSession, mapiProp, from namedPropertyToResolve in list
				select namedPropertyToResolve.PropId);
				int num2 = 0;
				foreach (PropertyTagCache.NamedPropertyToResolve namedPropertyToResolve2 in list)
				{
					NativeStorePropertyDefinition propDefByMapiNamedProp = PropertyTagCache.GetPropDefByMapiNamedProp(namedPropsFromIds[num2++], namedPropertyToResolve2.PropType, propertyTypeCheckingFlag);
					array[namedPropertyToResolve2.Index] = propDefByMapiNamedProp;
					if (propDefByMapiNamedProp != null)
					{
						resolvedPropertyCount++;
					}
					else
					{
						ExTraceGlobals.PropertyMappingTracer.TraceDebug<ushort, PropType>((long)storeSession.GetHashCode(), "Failed to resolve a named property from PropertyId {0:X} [{1:X}]", namedPropertyToResolve2.PropId, namedPropertyToResolve2.PropType);
					}
				}
			}
			return array;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x000508F4 File Offset: 0x0004EAF4
		private PropertyTagCache()
		{
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x000508FC File Offset: 0x0004EAFC
		private static NativeStorePropertyDefinition GetPropDefByMapiNamedProp(NamedProp prop, PropType type, NativeStorePropertyDefinition.TypeCheckingFlag propertyTypeCheckingFlag)
		{
			if (prop == null)
			{
				return null;
			}
			switch (prop.Kind)
			{
			case NamedPropKind.Id:
				return GuidIdPropertyDefinition.InternalCreateCustom(string.Empty, type, prop.Guid, prop.Id, PropertyFlags.None, propertyTypeCheckingFlag, new PropertyDefinitionConstraint[0]);
			case NamedPropKind.String:
				if (GuidNamePropertyDefinition.IsValidName(prop.Guid, prop.Name))
				{
					return GuidNamePropertyDefinition.InternalCreate(string.Empty, InternalSchema.ClrTypeFromPropTagType(type), type, prop.Guid, prop.Name, PropertyFlags.None, propertyTypeCheckingFlag, true, PropertyDefinitionConstraint.None);
				}
				return null;
			default:
				throw new ArgumentOutOfRangeException("prop.Kind");
			}
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0005098C File Offset: 0x0004EB8C
		private static bool TryFixPropTagWithErrorType(StoreSession session, MapiProp mapiProp, ref PropTag[] completePropTagList, ref PropTag propTag)
		{
			if (propTag.ValueType() != PropType.Error && propTag.ValueType() != PropType.Unspecified)
			{
				return true;
			}
			if (completePropTagList == null)
			{
				object thisObject = null;
				bool flag = false;
				try
				{
					if (session != null)
					{
						session.BeginMapiCall();
						session.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					completePropTagList = mapiProp.GetPropList();
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ExGetPropsFailed, ex, session, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("PropertyTagCache.IsGoodMapiPropTag failed.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ExGetPropsFailed, ex2, session, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("PropertyTagCache.IsGoodMapiPropTag failed.", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (session != null)
						{
							session.EndMapiCall();
							if (flag)
							{
								session.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			int num = propTag.Id();
			for (int i = 0; i < completePropTagList.Length; i++)
			{
				if (completePropTagList[i].Id() == num)
				{
					propTag = completePropTagList[i];
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00050AF4 File Offset: 0x0004ECF4
		private static void ChangeStringPropTagTypeToUnicode(ref PropTag propTag)
		{
			PropType propType = propTag.ValueType();
			int propId = propTag.Id();
			if (propType == PropType.AnsiString)
			{
				propTag = PropTagHelper.PropTagFromIdAndType(propId, PropType.String);
				return;
			}
			if (propType == PropType.AnsiStringArray)
			{
				propTag = PropTagHelper.PropTagFromIdAndType(propId, PropType.StringArray);
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00050B38 File Offset: 0x0004ED38
		private bool TryGetPropertyDefinitionsFromPropertyTags(StoreSession session, ICorePropertyBag corePropertyBag, uint[] propertyTags, NativeStorePropertyDefinition.TypeCheckingFlag typeCheckingFlag, out NativeStorePropertyDefinition[] propertyDefinitions, out uint unresolvablePropTag)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(corePropertyBag, "corePropertyBag");
			Util.ThrowOnNullArgument(propertyTags, "propertyTags");
			EnumValidator.ThrowIfInvalid<NativeStorePropertyDefinition.TypeCheckingFlag>(typeCheckingFlag, PropertyTagCache.validOptionSet);
			unresolvablePropTag = 0U;
			PropTag[] array = new PropTag[propertyTags.Length];
			for (int i = 0; i < propertyTags.Length; i++)
			{
				array[i] = (PropTag)propertyTags[i];
			}
			propertyDefinitions = this.PropertyDefinitionsFromPropTags(typeCheckingFlag, PersistablePropertyBag.GetPersistablePropertyBag(corePropertyBag).MapiProp, session, array);
			for (int j = 0; j < propertyDefinitions.Length; j++)
			{
				if (propertyDefinitions[j] == null)
				{
					unresolvablePropTag = (uint)array[j];
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000328 RID: 808
		internal const PropTag PropTagUnresolved = PropTag.Unresolved;

		// Token: 0x04000329 RID: 809
		public static readonly PropertyTagCache Cache = new PropertyTagCache();

		// Token: 0x0400032A RID: 810
		private static NativeStorePropertyDefinition.TypeCheckingFlag[] validOptionSet = new NativeStorePropertyDefinition.TypeCheckingFlag[]
		{
			NativeStorePropertyDefinition.TypeCheckingFlag.DisableTypeCheck,
			NativeStorePropertyDefinition.TypeCheckingFlag.DoNotCreateInvalidType,
			NativeStorePropertyDefinition.TypeCheckingFlag.AllowCompatibleType
		};

		// Token: 0x020000A6 RID: 166
		private struct NamedPropertyToResolve
		{
			// Token: 0x06000B87 RID: 2951 RVA: 0x00050C00 File Offset: 0x0004EE00
			public NamedPropertyToResolve(ushort propId, PropType propType, int index)
			{
				this.PropId = propId;
				this.PropType = propType;
				this.Index = index;
			}

			// Token: 0x0400032D RID: 813
			public readonly ushort PropId;

			// Token: 0x0400032E RID: 814
			public readonly PropType PropType;

			// Token: 0x0400032F RID: 815
			public readonly int Index;
		}
	}
}
