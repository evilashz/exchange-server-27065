using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200047E RID: 1150
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PropertySetMixer<CustomPropertyId, SetId>
	{
		// Token: 0x06003348 RID: 13128 RVA: 0x000D0777 File Offset: 0x000CE977
		internal PropertySetMixer() : this(new Predicate<CustomPropertyId>(PropertySetMixer<CustomPropertyId, SetId>.NeverIntercept))
		{
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x000D078C File Offset: 0x000CE98C
		internal PropertySetMixer(Predicate<CustomPropertyId> shouldIntercept)
		{
			this.shouldIntercept = shouldIntercept;
			this.propsToIndicies = null;
			this.propsToIndicies = new SortedDictionary<CustomPropertyId, PropertySetMixer<CustomPropertyId, SetId>.PropertyIndex>(typeof(CustomPropertyId).GetTypeInfo().IsValueType ? null : new PropertySetMixer<CustomPropertyId, SetId>.DefaultComparer());
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x000D07E1 File Offset: 0x000CE9E1
		public static explicit operator CustomPropertyId[](PropertySetMixer<CustomPropertyId, SetId> v)
		{
			return v.GetMergedSet();
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000D07EC File Offset: 0x000CE9EC
		internal void AddSet(SetId setId, params CustomPropertyId[] propIds)
		{
			int num = this.maxIndex;
			PropertySetMixer<CustomPropertyId, SetId>.PropertyMapping[] array = new PropertySetMixer<CustomPropertyId, SetId>.PropertyMapping[propIds.Length];
			for (int i = 0; i < propIds.Length; i++)
			{
				if (this.propsToIndicies.ContainsKey(propIds[i]))
				{
					array[i] = new PropertySetMixer<CustomPropertyId, SetId>.PropertyMapping(propIds[i], this.propsToIndicies[propIds[i]]);
				}
				else
				{
					array[i] = new PropertySetMixer<CustomPropertyId, SetId>.PropertyMapping(propIds[i], this.maxIndex++, this.shouldIntercept(propIds[i]) ? -1 : this.maxIndexAfterInterception++);
					this.propsToIndicies[propIds[i]] = array[i].Index;
				}
			}
			this.propSetIdsToSets.Add(setId, new PropertySetMixer<CustomPropertyId, SetId>.PropertySet(array, num, this.maxIndex - num));
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000D08F0 File Offset: 0x000CEAF0
		internal object[] FilterRow(object[] unfilteredRow, SetId setId)
		{
			PropertySetMixer<CustomPropertyId, SetId>.PropertySet propertySet = this.propSetIdsToSets[setId];
			object[] array = new object[propertySet.Mappings.Length];
			for (int i = 0; i < propertySet.Mappings.Length; i++)
			{
				array[i] = unfilteredRow[propertySet.Mappings[i].Index.BeforeInterception];
			}
			return array;
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x000D0950 File Offset: 0x000CEB50
		internal CustomPropertyId[] GetMergedSet()
		{
			CustomPropertyId[] array = new CustomPropertyId[this.maxIndex];
			foreach (KeyValuePair<CustomPropertyId, PropertySetMixer<CustomPropertyId, SetId>.PropertyIndex> keyValuePair in this.propsToIndicies)
			{
				array[keyValuePair.Value.BeforeInterception] = keyValuePair.Key;
			}
			return array;
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000D09BC File Offset: 0x000CEBBC
		internal CustomPropertyId[] GetFilteredMergedSet()
		{
			CustomPropertyId[] array = new CustomPropertyId[this.maxIndexAfterInterception];
			foreach (KeyValuePair<CustomPropertyId, PropertySetMixer<CustomPropertyId, SetId>.PropertyIndex> keyValuePair in this.propsToIndicies)
			{
				if (keyValuePair.Value.AfterInterception != -1)
				{
					array[keyValuePair.Value.AfterInterception] = keyValuePair.Key;
				}
			}
			return array;
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x000D0A38 File Offset: 0x000CEC38
		internal object[] GetProperties(object[] unfilteredRow, params CustomPropertyId[] propIds)
		{
			object[] array = new object[propIds.Length];
			for (int i = 0; i < propIds.Length; i++)
			{
				array[i] = this.TryGetProperty(unfilteredRow, propIds[i]);
			}
			return array;
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x000D0A70 File Offset: 0x000CEC70
		internal CustomPropertyId[] GetSet(SetId id)
		{
			PropertySetMixer<CustomPropertyId, SetId>.PropertySet propertySet = this.propSetIdsToSets[id];
			CustomPropertyId[] array = new CustomPropertyId[propertySet.Mappings.Length];
			for (int i = 0; i < propertySet.Mappings.Length; i++)
			{
				array[i] = propertySet.Mappings[i].Id;
			}
			return array;
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x000D0ACC File Offset: 0x000CECCC
		internal void MigrateSets(PropertySetMixer<CustomPropertyId, SetId> from, params SetId[] setIds)
		{
			foreach (SetId setId in setIds)
			{
				this.AddSet(setId, from.GetSet(setId));
			}
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x000D0B00 File Offset: 0x000CED00
		internal object[] RemitFilteredOffProperties(object[] filteredRow)
		{
			object[] array = new object[this.maxIndex];
			foreach (KeyValuePair<CustomPropertyId, PropertySetMixer<CustomPropertyId, SetId>.PropertyIndex> keyValuePair in this.propsToIndicies)
			{
				if (keyValuePair.Value.AfterInterception != -1)
				{
					array[keyValuePair.Value.BeforeInterception] = filteredRow[keyValuePair.Value.AfterInterception];
				}
				else
				{
					array[keyValuePair.Value.BeforeInterception] = null;
				}
			}
			return array;
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x000D0B90 File Offset: 0x000CED90
		internal void SetProperties(object[] unfilteredRowToModify, CustomPropertyId[] propIds, object[] newValues)
		{
			for (int i = 0; i < propIds.Length; i++)
			{
				this.SetProperty(unfilteredRowToModify, propIds[i], newValues[i]);
			}
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000D0BBC File Offset: 0x000CEDBC
		internal void SetProperty(object[] unfilteredRowToModify, CustomPropertyId propId, object newValue)
		{
			if (this.propsToIndicies.ContainsKey(propId))
			{
				unfilteredRowToModify[this.propsToIndicies[propId].BeforeInterception] = newValue;
			}
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x000D0BE0 File Offset: 0x000CEDE0
		public object TryGetProperty(object[] unfilteredRow, CustomPropertyId propId)
		{
			return unfilteredRow[this.propsToIndicies[propId].BeforeInterception];
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x000D0BF5 File Offset: 0x000CEDF5
		private static bool NeverIntercept(CustomPropertyId propId)
		{
			return false;
		}

		// Token: 0x04001BAA RID: 7082
		private int maxIndex;

		// Token: 0x04001BAB RID: 7083
		private int maxIndexAfterInterception;

		// Token: 0x04001BAC RID: 7084
		private IDictionary<SetId, PropertySetMixer<CustomPropertyId, SetId>.PropertySet> propSetIdsToSets = new SortedDictionary<SetId, PropertySetMixer<CustomPropertyId, SetId>.PropertySet>();

		// Token: 0x04001BAD RID: 7085
		private IDictionary<CustomPropertyId, PropertySetMixer<CustomPropertyId, SetId>.PropertyIndex> propsToIndicies;

		// Token: 0x04001BAE RID: 7086
		private Predicate<CustomPropertyId> shouldIntercept;

		// Token: 0x0200047F RID: 1151
		private struct PropertyIndex
		{
			// Token: 0x06003357 RID: 13143 RVA: 0x000D0BF8 File Offset: 0x000CEDF8
			public PropertyIndex(int beforeInterception, int afterInterception)
			{
				this.BeforeInterception = beforeInterception;
				this.AfterInterception = afterInterception;
			}

			// Token: 0x04001BAF RID: 7087
			public readonly int AfterInterception;

			// Token: 0x04001BB0 RID: 7088
			public readonly int BeforeInterception;
		}

		// Token: 0x02000480 RID: 1152
		private struct PropertyMapping
		{
			// Token: 0x06003358 RID: 13144 RVA: 0x000D0C08 File Offset: 0x000CEE08
			public PropertyMapping(CustomPropertyId id, PropertySetMixer<CustomPropertyId, SetId>.PropertyIndex index)
			{
				this.Id = id;
				this.Index = index;
			}

			// Token: 0x06003359 RID: 13145 RVA: 0x000D0C18 File Offset: 0x000CEE18
			public PropertyMapping(CustomPropertyId id, int totalIndex, int afterInterceptionIndex)
			{
				this = new PropertySetMixer<CustomPropertyId, SetId>.PropertyMapping(id, new PropertySetMixer<CustomPropertyId, SetId>.PropertyIndex(totalIndex, afterInterceptionIndex));
			}

			// Token: 0x04001BB1 RID: 7089
			public readonly CustomPropertyId Id;

			// Token: 0x04001BB2 RID: 7090
			public readonly PropertySetMixer<CustomPropertyId, SetId>.PropertyIndex Index;
		}

		// Token: 0x02000481 RID: 1153
		private struct PropertySet
		{
			// Token: 0x0600335A RID: 13146 RVA: 0x000D0C28 File Offset: 0x000CEE28
			public PropertySet(PropertySetMixer<CustomPropertyId, SetId>.PropertyMapping[] mappings, int deltaStartIndex, int deltaCount)
			{
				this.Mappings = mappings;
				this.DeltaStartIndex = deltaStartIndex;
				this.DeltaCount = deltaCount;
			}

			// Token: 0x04001BB3 RID: 7091
			public readonly int DeltaCount;

			// Token: 0x04001BB4 RID: 7092
			public readonly int DeltaStartIndex;

			// Token: 0x04001BB5 RID: 7093
			public readonly PropertySetMixer<CustomPropertyId, SetId>.PropertyMapping[] Mappings;
		}

		// Token: 0x02000482 RID: 1154
		private class DefaultComparer : IComparer<CustomPropertyId>
		{
			// Token: 0x0600335B RID: 13147 RVA: 0x000D0C40 File Offset: 0x000CEE40
			public int Compare(CustomPropertyId x, CustomPropertyId y)
			{
				if (!this.Equals(x, y))
				{
					if (x.GetType() == y.GetType())
					{
						IComparable comparable = x as IComparable;
						IComparable comparable2 = y as IComparable;
						if (comparable != null && comparable2 != null)
						{
							return comparable.CompareTo(comparable2);
						}
					}
					return Util.GetClassComparable(x).CompareTo(Util.GetClassComparable(y));
				}
				return 0;
			}

			// Token: 0x0600335C RID: 13148 RVA: 0x000D0CBB File Offset: 0x000CEEBB
			public bool Equals(CustomPropertyId x, CustomPropertyId y)
			{
				return x.Equals(y);
			}
		}
	}
}
