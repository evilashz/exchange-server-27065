using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200052B RID: 1323
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class PropertyAggregationStrategy
	{
		// Token: 0x060038DD RID: 14557 RVA: 0x000E94E8 File Offset: 0x000E76E8
		protected PropertyAggregationStrategy(params StorePropertyDefinition[] propertyDefinitions)
		{
			List<PropertyDependency> list = new List<PropertyDependency>(propertyDefinitions.Length);
			foreach (StorePropertyDefinition storePropertyDefinition in propertyDefinitions)
			{
				SmartPropertyDefinition smartPropertyDefinition = storePropertyDefinition as SmartPropertyDefinition;
				NativeStorePropertyDefinition nativeStorePropertyDefinition = storePropertyDefinition as NativeStorePropertyDefinition;
				if (smartPropertyDefinition != null)
				{
					list.AddRange(smartPropertyDefinition.Dependencies);
				}
				else if (nativeStorePropertyDefinition != null)
				{
					list.Add(new PropertyDependency(nativeStorePropertyDefinition, PropertyDependencyType.NeedForRead));
				}
			}
			this.dependencies = list.ToArray();
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x000E955B File Offset: 0x000E775B
		public PropertyDependency[] Dependencies
		{
			get
			{
				return this.dependencies;
			}
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x000E9564 File Offset: 0x000E7764
		public void Aggregate(PropertyDefinition aggregatedProperty, PropertyAggregationContext context, PropertyBag target)
		{
			Util.ThrowOnNullArgument(aggregatedProperty, "aggregatedProperty");
			Util.ThrowOnNullArgument(context, "context");
			Util.ThrowOnNullArgument(target, "target");
			PropertyAggregationStrategy.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Aggregating property {0}", aggregatedProperty.Name);
			object obj;
			if (this.TryAggregate(context, out obj))
			{
				this.Trace(obj, aggregatedProperty, context);
				target[aggregatedProperty] = obj;
				return;
			}
			PropertyAggregationStrategy.Tracer.TraceDebug<string>((long)this.GetHashCode(), "No value returned for property {0}", aggregatedProperty.Name);
		}

		// Token: 0x060038E0 RID: 14560
		protected abstract bool TryAggregate(PropertyAggregationContext context, out object value);

		// Token: 0x060038E1 RID: 14561 RVA: 0x000E95E8 File Offset: 0x000E77E8
		private static string ObjectValueToString(object value)
		{
			string text = value as string;
			if (text != null)
			{
				return text;
			}
			byte[] array = value as byte[];
			if (array != null)
			{
				return BitConverter.ToString(array);
			}
			IEnumerable enumerable = value as IEnumerable;
			if (enumerable != null)
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("{");
				foreach (object value2 in enumerable)
				{
					if (stringBuilder.Length > 1)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(value2);
				}
				stringBuilder.Append("}");
				return stringBuilder.ToString();
			}
			return value.ToString();
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x000E96AC File Offset: 0x000E78AC
		private static string GetPropertyValuePair(PropertyDefinition property, object value)
		{
			return PropertyAggregationStrategy.GetPropertyValuePair(property, PropertyAggregationStrategy.ObjectValueToString(value));
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x000E96BC File Offset: 0x000E78BC
		private static string GetPropertyValuePair(PropertyDefinition property, string value)
		{
			return string.Concat(new string[]
			{
				"[",
				property.Name,
				":",
				value,
				"]"
			});
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x000E96FC File Offset: 0x000E78FC
		private void Trace(object aggregatedValue, PropertyDefinition aggregatedProperty, PropertyAggregationContext context)
		{
			if (PropertyAggregationStrategy.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("{");
				bool flag = true;
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(", ");
					}
					foreach (PropertyDependency propertyDependency in this.Dependencies)
					{
						object obj = storePropertyBag.TryGetProperty(propertyDependency.Property);
						if (obj != null)
						{
							PropertyError propertyError = obj as PropertyError;
							if (propertyError != null)
							{
								if (propertyError.PropertyErrorCode != PropertyErrorCode.NotFound)
								{
									stringBuilder.Append(PropertyAggregationStrategy.GetPropertyValuePair(propertyDependency.Property, "ERROR:" + propertyError.PropertyErrorCode.ToString()));
								}
							}
							else
							{
								stringBuilder.Append(PropertyAggregationStrategy.GetPropertyValuePair(propertyDependency.Property, obj));
							}
						}
					}
				}
				stringBuilder.Append("}");
				PropertyAggregationStrategy.Tracer.TraceDebug<string, StringBuilder>((long)this.GetHashCode(), "Aggregated={0} Sources={1}", PropertyAggregationStrategy.GetPropertyValuePair(aggregatedProperty, aggregatedValue), stringBuilder);
			}
		}

		// Token: 0x04001E7F RID: 7807
		public static readonly PropertyAggregationStrategy None = new PropertyAggregationStrategy.NoAggregation(Array<StorePropertyDefinition>.Empty);

		// Token: 0x04001E80 RID: 7808
		public static readonly PropertyAggregationStrategy EntryIdsProperty = new PropertyAggregationStrategy.EntryIdsAggregation();

		// Token: 0x04001E81 RID: 7809
		public static readonly PropertyAggregationStrategy ItemClassesProperty = new PropertyAggregationStrategy.ItemClassesAggregation();

		// Token: 0x04001E82 RID: 7810
		private static readonly Trace Tracer = ExTraceGlobals.AggregationTracer;

		// Token: 0x04001E83 RID: 7811
		private readonly PropertyDependency[] dependencies;

		// Token: 0x0200052C RID: 1324
		private sealed class NoAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060038E6 RID: 14566 RVA: 0x000E986F File Offset: 0x000E7A6F
			public NoAggregation(StorePropertyDefinition[] dependencies) : base(dependencies)
			{
			}

			// Token: 0x060038E7 RID: 14567 RVA: 0x000E9878 File Offset: 0x000E7A78
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				value = null;
				return false;
			}
		}

		// Token: 0x0200052D RID: 1325
		internal sealed class CreationTimeAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060038E8 RID: 14568 RVA: 0x000E9880 File Offset: 0x000E7A80
			public CreationTimeAggregation() : base(new StorePropertyDefinition[]
			{
				InternalSchema.CreationTime
			})
			{
			}

			// Token: 0x060038E9 RID: 14569 RVA: 0x000E98A4 File Offset: 0x000E7AA4
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				ExDateTime exDateTime = ExDateTime.MinValue;
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					ExDateTime valueOrDefault = storePropertyBag.GetValueOrDefault<ExDateTime>(InternalSchema.CreationTime, ExDateTime.MinValue);
					if (valueOrDefault > exDateTime)
					{
						exDateTime = valueOrDefault;
					}
				}
				value = exDateTime;
				return true;
			}
		}

		// Token: 0x0200052E RID: 1326
		internal sealed class SingleValuePropertyAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060038EA RID: 14570 RVA: 0x000E9918 File Offset: 0x000E7B18
			public SingleValuePropertyAggregation(SelectionStrategy selectionStrategy) : base(selectionStrategy.Dependencies)
			{
				this.selectionStrategy = selectionStrategy;
			}

			// Token: 0x060038EB RID: 14571 RVA: 0x000E9930 File Offset: 0x000E7B30
			protected sealed override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				IStorePropertyBag storePropertyBag = null;
				foreach (IStorePropertyBag storePropertyBag2 in context.Sources)
				{
					if (this.selectionStrategy.IsSelectable(storePropertyBag2) && (storePropertyBag == null || this.selectionStrategy.HasPriority(storePropertyBag2, storePropertyBag)))
					{
						storePropertyBag = storePropertyBag2;
					}
				}
				if (storePropertyBag != null)
				{
					value = this.selectionStrategy.GetValue(storePropertyBag);
				}
				else
				{
					value = null;
				}
				return value != null;
			}

			// Token: 0x04001E84 RID: 7812
			private readonly SelectionStrategy selectionStrategy;
		}

		// Token: 0x0200052F RID: 1327
		internal sealed class EntryIdsAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060038EC RID: 14572 RVA: 0x000E99B8 File Offset: 0x000E7BB8
			public EntryIdsAggregation() : base(new StorePropertyDefinition[]
			{
				InternalSchema.EntryId
			})
			{
			}

			// Token: 0x060038ED RID: 14573 RVA: 0x000E99DC File Offset: 0x000E7BDC
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				List<StoreObjectId> list = new List<StoreObjectId>(context.Sources.Count);
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					byte[] valueOrDefault = storePropertyBag.GetValueOrDefault<byte[]>(InternalSchema.EntryId, null);
					if (valueOrDefault != null)
					{
						StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(valueOrDefault, StoreObjectType.Unknown);
						if (storeObjectId != null)
						{
							list.Add(storeObjectId);
						}
					}
				}
				value = list.ToArray();
				return true;
			}
		}

		// Token: 0x02000530 RID: 1328
		internal sealed class ItemClassesAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060038EE RID: 14574 RVA: 0x000E9A64 File Offset: 0x000E7C64
			public ItemClassesAggregation() : base(new StorePropertyDefinition[]
			{
				InternalSchema.ItemClass
			})
			{
			}

			// Token: 0x060038EF RID: 14575 RVA: 0x000E9A88 File Offset: 0x000E7C88
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				HashSet<string> hashSet = new HashSet<string>();
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					string valueOrDefault = storePropertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass, StoreObjectType.Message.ToString());
					hashSet.Add(valueOrDefault);
				}
				value = hashSet.ToArray<string>();
				return true;
			}
		}
	}
}
