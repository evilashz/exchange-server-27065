using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C8E RID: 3214
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal static class NativeStorePropertyDefinitionDictionary
	{
		// Token: 0x06007071 RID: 28785 RVA: 0x001F212E File Offset: 0x001F032E
		internal static PropertyMatchResult TryFindInstance(PropertyTagPropertyDefinition.PropTagKey key, PropType type, out PropertyTagPropertyDefinition instance)
		{
			return NativeStorePropertyDefinitionDictionary.TryFindInstanceHelper<PropertyTagPropertyDefinition.PropTagKey, PropertyTagPropertyDefinition>(NativeStorePropertyDefinitionDictionary.definitionSets.PropertyTagPropertyDefinitions, key, type, false, out instance);
		}

		// Token: 0x06007072 RID: 28786 RVA: 0x001F2143 File Offset: 0x001F0343
		internal static PropertyMatchResult TryFindInstance(GuidIdPropertyDefinition.GuidIdKey key, PropType type, bool supportsCompatibleType, out GuidIdPropertyDefinition instance)
		{
			return NativeStorePropertyDefinitionDictionary.TryFindInstanceHelper<GuidIdPropertyDefinition.GuidIdKey, GuidIdPropertyDefinition>(NativeStorePropertyDefinitionDictionary.definitionSets.GuidIdPropertyDefinitions, key, type, supportsCompatibleType, out instance);
		}

		// Token: 0x06007073 RID: 28787 RVA: 0x001F2158 File Offset: 0x001F0358
		internal static PropertyMatchResult TryFindInstance(GuidNamePropertyDefinition.GuidNameKey key, PropType type, bool supportsCompatibleType, out GuidNamePropertyDefinition instance)
		{
			return NativeStorePropertyDefinitionDictionary.TryFindInstanceHelper<GuidNamePropertyDefinition.GuidNameKey, GuidNamePropertyDefinition>(NativeStorePropertyDefinitionDictionary.definitionSets.GuidNamePropertyDefinitions, key, type, supportsCompatibleType, out instance);
		}

		// Token: 0x06007074 RID: 28788 RVA: 0x001F2170 File Offset: 0x001F0370
		private static PropertyMatchResult TryFindInstanceHelper<KeyType, ValueType>(Dictionary<KeyType, ValueType[]> dictionary, KeyType key, PropType type, bool supportsCompatibleType, out ValueType instance) where ValueType : NativeStorePropertyDefinition
		{
			instance = default(ValueType);
			ValueType[] array;
			dictionary.TryGetValue(key, out array);
			if (array == null)
			{
				return PropertyMatchResult.Default;
			}
			foreach (ValueType valueType in array)
			{
				if (valueType.MapiPropertyType == type)
				{
					instance = valueType;
					return PropertyMatchResult.Found;
				}
				if (supportsCompatibleType && NativeStorePropertyDefinitionDictionary.IsCompatibleType(valueType.MapiPropertyType, type))
				{
					instance = valueType;
				}
			}
			if (instance != default(ValueType))
			{
				return PropertyMatchResult.Found;
			}
			return PropertyMatchResult.TypeMismatch;
		}

		// Token: 0x06007075 RID: 28789 RVA: 0x001F2203 File Offset: 0x001F0403
		private static bool IsCompatibleType(PropType type1, PropType type2)
		{
			return type1 == type2 || (type1 == PropType.Short && type2 == PropType.Int) || (type1 == PropType.Int && type2 == PropType.Short);
		}

		// Token: 0x04004D9A RID: 19866
		private static NativeStorePropertyDefinitionDictionary.PropertyDefinitionSets definitionSets = NativeStorePropertyDefinitionDictionary.PropertyDefinitionSets.BuildWellKnownSets();

		// Token: 0x02000C8F RID: 3215
		private struct PropertyDefinitionSets
		{
			// Token: 0x06007076 RID: 28790 RVA: 0x001F221D File Offset: 0x001F041D
			public PropertyDefinitionSets(Dictionary<PropertyTagPropertyDefinition.PropTagKey, PropertyTagPropertyDefinition[]> propertyTagPropertyDefinitions, Dictionary<GuidIdPropertyDefinition.GuidIdKey, GuidIdPropertyDefinition[]> guidIdPropertyDefinitions, Dictionary<GuidNamePropertyDefinition.GuidNameKey, GuidNamePropertyDefinition[]> guidNamePropertyDefinitions)
			{
				this.PropertyTagPropertyDefinitions = propertyTagPropertyDefinitions;
				this.GuidIdPropertyDefinitions = guidIdPropertyDefinitions;
				this.GuidNamePropertyDefinitions = guidNamePropertyDefinitions;
			}

			// Token: 0x06007077 RID: 28791 RVA: 0x001F223C File Offset: 0x001F043C
			public static NativeStorePropertyDefinitionDictionary.PropertyDefinitionSets BuildWellKnownSets()
			{
				Dictionary<PropertyTagPropertyDefinition.PropTagKey, PropertyTagPropertyDefinition[]> dictionary = new Dictionary<PropertyTagPropertyDefinition.PropTagKey, PropertyTagPropertyDefinition[]>();
				Dictionary<GuidIdPropertyDefinition.GuidIdKey, GuidIdPropertyDefinition[]> dictionary2 = new Dictionary<GuidIdPropertyDefinition.GuidIdKey, GuidIdPropertyDefinition[]>();
				Dictionary<GuidNamePropertyDefinition.GuidNameKey, GuidNamePropertyDefinition[]> dictionary3 = new Dictionary<GuidNamePropertyDefinition.GuidNameKey, GuidNamePropertyDefinition[]>();
				Type typeFromHandle = typeof(InternalSchema);
				foreach (FieldInfo fieldInfo in from x in typeFromHandle.GetTypeInfo().DeclaredFields
				where x.IsStatic
				select x)
				{
					Type fieldType = fieldInfo.FieldType;
					if (fieldType.GetTypeInfo().IsSubclassOf(typeof(NativeStorePropertyDefinition)))
					{
						NativeStorePropertyDefinition nativeStorePropertyDefinition = (NativeStorePropertyDefinition)fieldInfo.GetValue(null);
						if (nativeStorePropertyDefinition is PropertyTagPropertyDefinition)
						{
							PropertyTagPropertyDefinition propertyTagPropertyDefinition = nativeStorePropertyDefinition as PropertyTagPropertyDefinition;
							if (propertyTagPropertyDefinition.IsApplicationSpecific && propertyTagPropertyDefinition.Constraints.Count != 0)
							{
								throw new NotSupportedException(string.Format("Field {0} is a message class specific property and has constraints. Constraints currently are not supported on this range.", fieldInfo.Name));
							}
							NativeStorePropertyDefinitionDictionary.PropertyDefinitionSets.AddDefinition<PropertyTagPropertyDefinition.PropTagKey, PropertyTagPropertyDefinition>(dictionary, propertyTagPropertyDefinition.GetKey(), propertyTagPropertyDefinition);
						}
						else if (nativeStorePropertyDefinition is GuidIdPropertyDefinition)
						{
							GuidIdPropertyDefinition guidIdPropertyDefinition = nativeStorePropertyDefinition as GuidIdPropertyDefinition;
							NativeStorePropertyDefinitionDictionary.PropertyDefinitionSets.AddDefinition<GuidIdPropertyDefinition.GuidIdKey, GuidIdPropertyDefinition>(dictionary2, (GuidIdPropertyDefinition.GuidIdKey)guidIdPropertyDefinition.GetKey(), guidIdPropertyDefinition);
						}
						else
						{
							if (!(nativeStorePropertyDefinition is GuidNamePropertyDefinition))
							{
								string arg = "<null>";
								if (nativeStorePropertyDefinition != null)
								{
									arg = nativeStorePropertyDefinition.GetType().ToString();
								}
								throw new NotSupportedException(string.Format("Not supported field({0}) fieldtype({1}) type({2}).", fieldInfo.Name, fieldType, arg));
							}
							GuidNamePropertyDefinition guidNamePropertyDefinition = nativeStorePropertyDefinition as GuidNamePropertyDefinition;
							NativeStorePropertyDefinitionDictionary.PropertyDefinitionSets.AddDefinition<GuidNamePropertyDefinition.GuidNameKey, GuidNamePropertyDefinition>(dictionary3, (GuidNamePropertyDefinition.GuidNameKey)guidNamePropertyDefinition.GetKey(), guidNamePropertyDefinition);
						}
					}
					else if (fieldType == typeof(PropertyDefinition) || fieldType.GetTypeInfo().IsSubclassOf(typeof(PropertyDefinition)))
					{
						Type type = fieldInfo.GetValue(null).GetType();
						if (type.GetTypeInfo().IsSubclassOf(typeof(NativeStorePropertyDefinition)) || type == typeof(NativeStorePropertyDefinition))
						{
							throw new NotSupportedException(string.Format("Type of field {0} is incorrect. Field is defined as {1} and holds onto an instance of type {2}.", fieldInfo.Name, fieldType, fieldInfo.GetValue(null).GetType()));
						}
					}
				}
				return new NativeStorePropertyDefinitionDictionary.PropertyDefinitionSets(dictionary, dictionary2, dictionary3);
			}

			// Token: 0x06007078 RID: 28792 RVA: 0x001F2484 File Offset: 0x001F0684
			private static void AddDefinition<KeyType, ValueType>(Dictionary<KeyType, ValueType[]> dictionary, KeyType key, ValueType propertyDefinition)
			{
				ValueType[] array;
				if (!dictionary.TryGetValue(key, out array))
				{
					array = new ValueType[1];
				}
				else
				{
					int i = 0;
					while (i < array.Length)
					{
						if (object.ReferenceEquals(array[i], propertyDefinition))
						{
							return;
						}
						if (array[i].Equals(propertyDefinition))
						{
							PropertyTagPropertyDefinition propertyTagPropertyDefinition = propertyDefinition as PropertyTagPropertyDefinition;
							if (propertyTagPropertyDefinition != null && propertyTagPropertyDefinition.IsApplicationSpecific)
							{
								return;
							}
							throw new NotSupportedException(string.Format("PropertyDefinition {0} is equal at both the key level and the item level to {1}.", propertyDefinition, array[i]));
						}
						else
						{
							i++;
						}
					}
					Array.Resize<ValueType>(ref array, array.Length + 1);
				}
				array[array.Length - 1] = propertyDefinition;
				dictionary[key] = array;
			}

			// Token: 0x04004D9B RID: 19867
			public Dictionary<PropertyTagPropertyDefinition.PropTagKey, PropertyTagPropertyDefinition[]> PropertyTagPropertyDefinitions;

			// Token: 0x04004D9C RID: 19868
			public Dictionary<GuidIdPropertyDefinition.GuidIdKey, GuidIdPropertyDefinition[]> GuidIdPropertyDefinitions;

			// Token: 0x04004D9D RID: 19869
			public Dictionary<GuidNamePropertyDefinition.GuidNameKey, GuidNamePropertyDefinition[]> GuidNamePropertyDefinitions;
		}
	}
}
