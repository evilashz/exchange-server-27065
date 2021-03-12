using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000A1 RID: 161
	internal class FilterControlHelper
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x00013D38 File Offset: 0x00011F38
		static FilterControlHelper()
		{
			FilterControlHelper.operators.Add(typeof(string), new PropertyFilterOperator[]
			{
				PropertyFilterOperator.Equal,
				PropertyFilterOperator.NotEqual,
				PropertyFilterOperator.Contains,
				PropertyFilterOperator.NotContains,
				PropertyFilterOperator.StartsWith,
				PropertyFilterOperator.EndsWith
			});
			FilterControlHelper.operators.Add(typeof(Enum), new PropertyFilterOperator[]
			{
				PropertyFilterOperator.Equal,
				PropertyFilterOperator.NotEqual
			});
			Dictionary<Type, PropertyFilterOperator[]> dictionary = FilterControlHelper.operators;
			Type typeFromHandle = typeof(bool);
			PropertyFilterOperator[] value = new PropertyFilterOperator[1];
			dictionary.Add(typeFromHandle, value);
			FilterControlHelper.operators.Add(typeof(int), new PropertyFilterOperator[]
			{
				PropertyFilterOperator.Equal,
				PropertyFilterOperator.NotEqual,
				PropertyFilterOperator.GreaterThan,
				PropertyFilterOperator.GreaterThanOrEqual,
				PropertyFilterOperator.LessThan,
				PropertyFilterOperator.LessThanOrEqual
			});
			FilterControlHelper.operators.Add(typeof(ulong), new PropertyFilterOperator[]
			{
				PropertyFilterOperator.Equal,
				PropertyFilterOperator.NotEqual,
				PropertyFilterOperator.GreaterThan,
				PropertyFilterOperator.GreaterThanOrEqual,
				PropertyFilterOperator.LessThan,
				PropertyFilterOperator.LessThanOrEqual
			});
			FilterControlHelper.operators.Add(typeof(long), new PropertyFilterOperator[]
			{
				PropertyFilterOperator.Equal,
				PropertyFilterOperator.NotEqual,
				PropertyFilterOperator.GreaterThan,
				PropertyFilterOperator.GreaterThanOrEqual,
				PropertyFilterOperator.LessThan,
				PropertyFilterOperator.LessThanOrEqual
			});
			FilterControlHelper.operators.Add(typeof(ADObjectId), new PropertyFilterOperator[]
			{
				PropertyFilterOperator.Equal,
				PropertyFilterOperator.NotEqual
			});
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00013E80 File Offset: 0x00012080
		public static PropertyFilterOperator[] GetFilterOperators(Type type)
		{
			Type key = typeof(Enum).IsAssignableFrom(type) ? typeof(Enum) : type;
			if (!FilterControlHelper.operators.ContainsKey(key))
			{
				return new PropertyFilterOperator[0];
			}
			return FilterControlHelper.operators[key];
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00013ECC File Offset: 0x000120CC
		public static ProviderPropertyDefinition GenerateEmptyPropertyDefinition(string propertyName, Type type)
		{
			return new SimpleProviderPropertyDefinition(propertyName, ExchangeObjectVersion.Exchange2003, FilterControlHelper.GetEffectiveType(type), FilterControlHelper.GetDefaultPropertyDefinitionFlagsForType(type), FilterControlHelper.GetDefaultValueForType(type), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00013EF8 File Offset: 0x000120F8
		public static ProviderPropertyDefinition CopyPropertyDefinition(string propertyName, ProviderPropertyDefinition definition)
		{
			return new SimpleProviderPropertyDefinition(propertyName, definition.VersionAdded, definition.Type, (definition as SimpleProviderPropertyDefinition).Flags, definition.DefaultValue, PropertyDefinitionConstraint.None, definition.AllConstraints.ToArray<PropertyDefinitionConstraint>(), definition.SupportingProperties.ToArray<ProviderPropertyDefinition>(), definition.CustomFilterBuilderDelegate, definition.GetterDelegate, definition.SetterDelegate);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00013F58 File Offset: 0x00012158
		public static Type GetEffectiveType(Type type)
		{
			if (null == type)
			{
				throw new ArgumentNullException("type");
			}
			if (typeof(ICollection).IsAssignableFrom(type) && !type.ContainsGenericParameters)
			{
				Type[] genericArguments = type.GetGenericArguments();
				if (genericArguments.Length == 1)
				{
					return genericArguments[0];
				}
			}
			return type;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00013FA8 File Offset: 0x000121A8
		public static object GetDefaultValueForType(Type type)
		{
			object result = type.IsValueType ? Activator.CreateInstance(type) : null;
			if (!(type == typeof(string)))
			{
				return result;
			}
			return string.Empty;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00013FE0 File Offset: 0x000121E0
		public static PropertyDefinitionFlags GetDefaultPropertyDefinitionFlagsForType(Type type)
		{
			PropertyDefinitionFlags result = PropertyDefinitionFlags.None;
			if (typeof(ICollection).IsAssignableFrom(type))
			{
				result = PropertyDefinitionFlags.MultiValued;
			}
			else if (type == typeof(int) || type == typeof(long) || type == typeof(ulong))
			{
				result = PropertyDefinitionFlags.PersistDefaultValue;
			}
			return result;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00014040 File Offset: 0x00012240
		public static int GetIntFromUnlimitedInt(object unlimitedValue)
		{
			Unlimited<int> unlimited = (Unlimited<int>)unlimitedValue;
			if (!unlimited.IsUnlimited)
			{
				return unlimited.Value;
			}
			return int.MaxValue;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001406C File Offset: 0x0001226C
		public static long GetDaysFromEnhancedTimeSpan(object timeSpanObj)
		{
			long result = long.MinValue;
			if (!timeSpanObj.IsNullValue())
			{
				if (timeSpanObj.GetType() == typeof(EnhancedTimeSpan))
				{
					result = (long)((EnhancedTimeSpan)timeSpanObj).TotalDays;
				}
				else
				{
					Unlimited<EnhancedTimeSpan> unlimited = (Unlimited<EnhancedTimeSpan>)timeSpanObj;
					result = (unlimited.IsUnlimited ? long.MaxValue : ((long)unlimited.Value.TotalDays));
				}
			}
			return result;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000140E4 File Offset: 0x000122E4
		public static ulong GetMBFromByteQuantifiedSize(object sizeObj)
		{
			ulong result = 0UL;
			if (!sizeObj.IsNullValue())
			{
				if (sizeObj.GetType() == typeof(ByteQuantifiedSize))
				{
					result = ((ByteQuantifiedSize)sizeObj).ToMB();
				}
				else
				{
					Unlimited<ByteQuantifiedSize> unlimited = (Unlimited<ByteQuantifiedSize>)sizeObj;
					result = (unlimited.IsUnlimited ? ulong.MaxValue : unlimited.Value.ToMB());
				}
			}
			return result;
		}

		// Token: 0x040001BB RID: 443
		private static Dictionary<Type, PropertyFilterOperator[]> operators = new Dictionary<Type, PropertyFilterOperator[]>();
	}
}
