using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000E5 RID: 229
	[Serializable]
	internal sealed class DeepADObjectEqualityComparer : IEqualityComparer<ADObject>
	{
		// Token: 0x06000B47 RID: 2887 RVA: 0x00033C4C File Offset: 0x00031E4C
		public bool Equals(ADObject left, ADObject right)
		{
			if (left == null)
			{
				return null == right;
			}
			if (right == null)
			{
				return null == left;
			}
			return left.DistinguishedName.Equals(right.DistinguishedName, StringComparison.Ordinal) && left.GetType().Equals(right.GetType()) && left.Schema.AllProperties.Count == right.Schema.AllProperties.Count && this.Equals(left, right, left.Schema.AllProperties);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00033CCC File Offset: 0x00031ECC
		private bool AreMultivaluedPropertiesEqual(object leftValue, object rightValue, ADPropertyDefinition adProperty)
		{
			MultiValuedPropertyBase multiValuedPropertyBase = leftValue as MultiValuedPropertyBase;
			MultiValuedPropertyBase multiValuedPropertyBase2 = rightValue as MultiValuedPropertyBase;
			if (multiValuedPropertyBase != null && multiValuedPropertyBase.Count > 0 && rightValue == null)
			{
				return false;
			}
			if (multiValuedPropertyBase == null && multiValuedPropertyBase2 != null && multiValuedPropertyBase2.Count > 0)
			{
				return false;
			}
			if (multiValuedPropertyBase == null && multiValuedPropertyBase2 != null && multiValuedPropertyBase2.Count == 0)
			{
				return true;
			}
			if (multiValuedPropertyBase != null && multiValuedPropertyBase.Count == 0 && multiValuedPropertyBase2 == null)
			{
				return true;
			}
			if (multiValuedPropertyBase.Count == 0 && multiValuedPropertyBase2.Count == 0)
			{
				return true;
			}
			IEnumerable enumerable = (IEnumerable)leftValue;
			List<byte[]> list = new List<byte[]>();
			List<string> list2 = new List<string>();
			foreach (object originalValue in enumerable)
			{
				if (adProperty.IsBinary)
				{
					list.Add(ADValueConvertor.ConvertValueToBinary(originalValue, adProperty.FormatProvider));
				}
				else
				{
					list2.Add(ADValueConvertor.ConvertValueToString(originalValue, adProperty.FormatProvider));
				}
			}
			IEnumerable enumerable2 = (IEnumerable)rightValue;
			List<byte[]> list3 = new List<byte[]>();
			List<string> list4 = new List<string>();
			foreach (object originalValue2 in enumerable2)
			{
				if (adProperty.IsBinary)
				{
					list3.Add(ADValueConvertor.ConvertValueToBinary(originalValue2, adProperty.FormatProvider));
				}
				else
				{
					list4.Add(ADValueConvertor.ConvertValueToString(originalValue2, adProperty.FormatProvider));
				}
			}
			return list.Equals(list3, ByteArrayComparer.Instance) && list2.Equals(list4, StringComparer.Ordinal);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00033E70 File Offset: 0x00032070
		public int GetHashCode(ADObject obj)
		{
			ArgumentValidator.ThrowIfNull("obj", obj);
			return obj.GetHashCode();
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00033E84 File Offset: 0x00032084
		internal bool Equals(ADRawEntry left, ADRawEntry right, IEnumerable<PropertyDefinition> properties)
		{
			if (left == null)
			{
				return null == right;
			}
			if (right == null)
			{
				return null == left;
			}
			foreach (PropertyDefinition propertyDefinition in properties)
			{
				ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)propertyDefinition;
				if (adpropertyDefinition.LdapDisplayName != null && !adpropertyDefinition.IsCalculated && !adpropertyDefinition.LdapDisplayName.Equals(ADObjectSchema.Id.LdapDisplayName, StringComparison.Ordinal))
				{
					object obj = null;
					left.propertyBag.TryGetField(adpropertyDefinition, ref obj);
					object obj2 = null;
					right.propertyBag.TryGetField(adpropertyDefinition, ref obj2);
					if (obj != null || obj2 != null)
					{
						if (adpropertyDefinition.IsMultivalued)
						{
							if (!this.AreMultivaluedPropertiesEqual(obj, obj2, adpropertyDefinition))
							{
								return false;
							}
						}
						else
						{
							if (obj != null && obj2 == null)
							{
								return false;
							}
							if (obj == null && obj2 != null)
							{
								return false;
							}
							if (adpropertyDefinition.IsBinary)
							{
								byte[] left2 = ADValueConvertor.ConvertValueToBinary(obj, adpropertyDefinition.FormatProvider);
								byte[] right2 = ADValueConvertor.ConvertValueToBinary(obj2, adpropertyDefinition.FormatProvider);
								if (!ByteArrayComparer.Instance.Equals(left2, right2))
								{
									return false;
								}
							}
							else
							{
								string a = ADValueConvertor.ConvertValueToString(obj, adpropertyDefinition.FormatProvider);
								string b = ADValueConvertor.ConvertValueToString(obj2, adpropertyDefinition.FormatProvider);
								if (!string.Equals(a, b, StringComparison.Ordinal))
								{
									return false;
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x04000472 RID: 1138
		internal static readonly DeepADObjectEqualityComparer Instance = new DeepADObjectEqualityComparer();
	}
}
