using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000C8 RID: 200
	internal abstract class ConfigurableObjectLogSchema<T, Tschema> : ObjectLogSchema where T : ConfigurableObject where Tschema : ObjectSchema, new()
	{
		// Token: 0x06000A8A RID: 2698 RVA: 0x0002F388 File Offset: 0x0002D588
		private static ConfigurableObjectLogSchema<T, Tschema>.SchemaEntryList ComputeSchemaEntries()
		{
			ConfigurableObjectLogSchema<T, Tschema>.SchemaEntryList schemaEntryList = new ConfigurableObjectLogSchema<T, Tschema>.SchemaEntryList();
			ObjectSchema objectSchema = ObjectSchema.GetInstance<Tschema>();
			foreach (PropertyDefinition propertyDefinition in objectSchema.AllProperties)
			{
				SimpleProviderPropertyDefinition simpleProviderPropertyDefinition = (SimpleProviderPropertyDefinition)propertyDefinition;
				if (simpleProviderPropertyDefinition.IsMultivalued)
				{
					schemaEntryList.Add(new ConfigurableObjectLogSchema<T, Tschema>.MultiValuedProperty(), simpleProviderPropertyDefinition);
				}
				else
				{
					Type type = simpleProviderPropertyDefinition.Type;
					if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
					{
						type = type.GetGenericArguments()[0];
					}
					if (type.IsEnum && type.GetCustomAttributes(typeof(FlagsAttribute), true).Length > 0)
					{
						schemaEntryList.Add(new ConfigurableObjectLogSchema<T, Tschema>.SimpleProperty<int>(), simpleProviderPropertyDefinition);
					}
					else if (type == typeof(ADObjectId))
					{
						schemaEntryList.Add(new ConfigurableObjectLogSchema<T, Tschema>.ADObjectIdGuidProperty(), simpleProviderPropertyDefinition);
						schemaEntryList.Add(new ConfigurableObjectLogSchema<T, Tschema>.StringProperty(), simpleProviderPropertyDefinition);
					}
					else if (type == typeof(int))
					{
						schemaEntryList.Add(new ConfigurableObjectLogSchema<T, Tschema>.SimpleProperty<int>(), simpleProviderPropertyDefinition);
					}
					else if (type == typeof(Unlimited<int>))
					{
						schemaEntryList.Add(new ConfigurableObjectLogSchema<T, Tschema>.UnlimitedProperty<int, ConfigurableObjectLogSchema<T, Tschema>.SimpleProperty<int>>(), simpleProviderPropertyDefinition);
					}
					else if (type == typeof(byte[]))
					{
						schemaEntryList.Add(new ConfigurableObjectLogSchema<T, Tschema>.SimpleProperty<byte[]>(), simpleProviderPropertyDefinition);
					}
					else if (type == typeof(Guid))
					{
						schemaEntryList.Add(new ConfigurableObjectLogSchema<T, Tschema>.GuidProperty(), simpleProviderPropertyDefinition);
					}
					else if (type == typeof(DateTime))
					{
						schemaEntryList.Add(new ConfigurableObjectLogSchema<T, Tschema>.DateTimeProperty(), simpleProviderPropertyDefinition);
					}
					else if (type == typeof(ExDateTime))
					{
						schemaEntryList.Add(new ConfigurableObjectLogSchema<T, Tschema>.ExDateTimeProperty(), simpleProviderPropertyDefinition);
					}
					else if (type == typeof(TimeSpan))
					{
						schemaEntryList.Add(new ConfigurableObjectLogSchema<T, Tschema>.TimeSpanProperty(), simpleProviderPropertyDefinition);
					}
					else if (type == typeof(Unlimited<TimeSpan>))
					{
						schemaEntryList.Add(new ConfigurableObjectLogSchema<T, Tschema>.UnlimitedProperty<TimeSpan, ConfigurableObjectLogSchema<T, Tschema>.TimeSpanProperty>(), simpleProviderPropertyDefinition);
					}
					else if (type == typeof(EnhancedTimeSpan))
					{
						schemaEntryList.Add(new ConfigurableObjectLogSchema<T, Tschema>.EnhancedTimeSpanProperty(), simpleProviderPropertyDefinition);
					}
					else if (type == typeof(Unlimited<EnhancedTimeSpan>))
					{
						schemaEntryList.Add(new ConfigurableObjectLogSchema<T, Tschema>.UnlimitedProperty<EnhancedTimeSpan, ConfigurableObjectLogSchema<T, Tschema>.EnhancedTimeSpanProperty>(), simpleProviderPropertyDefinition);
					}
					else
					{
						schemaEntryList.Add(new ConfigurableObjectLogSchema<T, Tschema>.StringProperty(), simpleProviderPropertyDefinition);
					}
				}
			}
			return schemaEntryList;
		}

		// Token: 0x0400040F RID: 1039
		public static readonly IEnumerable<IObjectLogPropertyDefinition<T>> SchemaEntries = ConfigurableObjectLogSchema<T, Tschema>.ComputeSchemaEntries();

		// Token: 0x020000C9 RID: 201
		private class SchemaEntryList : List<IObjectLogPropertyDefinition<T>>
		{
			// Token: 0x06000A8D RID: 2701 RVA: 0x0002F618 File Offset: 0x0002D818
			public void Add(ConfigurableObjectLogSchema<T, Tschema>.PropertyBase schemaEntry, SimpleProviderPropertyDefinition property)
			{
				schemaEntry.Property = property;
				base.Add(schemaEntry);
			}
		}

		// Token: 0x020000CA RID: 202
		private abstract class PropertyBase : IObjectLogPropertyDefinition<T>
		{
			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0002F630 File Offset: 0x0002D830
			// (set) Token: 0x06000A90 RID: 2704 RVA: 0x0002F638 File Offset: 0x0002D838
			public SimpleProviderPropertyDefinition Property { get; set; }

			// Token: 0x06000A91 RID: 2705
			public abstract object GetValueInternal(object val);

			// Token: 0x06000A92 RID: 2706 RVA: 0x0002F641 File Offset: 0x0002D841
			public virtual string GetFieldName()
			{
				return this.Property.Name;
			}

			// Token: 0x170001F3 RID: 499
			// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0002F64E File Offset: 0x0002D84E
			string IObjectLogPropertyDefinition<!0>.FieldName
			{
				get
				{
					return this.GetFieldName();
				}
			}

			// Token: 0x06000A94 RID: 2708 RVA: 0x0002F658 File Offset: 0x0002D858
			object IObjectLogPropertyDefinition<!0>.GetValue(T objectToLog)
			{
				object obj = objectToLog[this.Property];
				if (obj == null)
				{
					return null;
				}
				if (obj.Equals(this.Property.DefaultValue))
				{
					return null;
				}
				return this.GetValueInternal(obj);
			}
		}

		// Token: 0x020000CB RID: 203
		private class SimpleProperty<Tprop> : ConfigurableObjectLogSchema<T, Tschema>.PropertyBase
		{
			// Token: 0x06000A96 RID: 2710 RVA: 0x0002F6A2 File Offset: 0x0002D8A2
			public override object GetValueInternal(object val)
			{
				return (Tprop)((object)val);
			}
		}

		// Token: 0x020000CC RID: 204
		private class StringProperty : ConfigurableObjectLogSchema<T, Tschema>.PropertyBase
		{
			// Token: 0x06000A98 RID: 2712 RVA: 0x0002F6B7 File Offset: 0x0002D8B7
			public override object GetValueInternal(object val)
			{
				return val.ToString();
			}
		}

		// Token: 0x020000CD RID: 205
		private class GuidProperty : ConfigurableObjectLogSchema<T, Tschema>.PropertyBase
		{
			// Token: 0x06000A9A RID: 2714 RVA: 0x0002F6C8 File Offset: 0x0002D8C8
			public override object GetValueInternal(object val)
			{
				Guid a = (Guid)val;
				if (a == Guid.Empty)
				{
					return null;
				}
				return val;
			}
		}

		// Token: 0x020000CE RID: 206
		private class DateTimeProperty : ConfigurableObjectLogSchema<T, Tschema>.PropertyBase
		{
			// Token: 0x06000A9C RID: 2716 RVA: 0x0002F6F4 File Offset: 0x0002D8F4
			public override object GetValueInternal(object val)
			{
				return ((DateTime)val).ToUniversalTime();
			}
		}

		// Token: 0x020000CF RID: 207
		private class ExDateTimeProperty : ConfigurableObjectLogSchema<T, Tschema>.PropertyBase
		{
			// Token: 0x06000A9E RID: 2718 RVA: 0x0002F71C File Offset: 0x0002D91C
			public override object GetValueInternal(object val)
			{
				return ((ExDateTime)val).ToUtc();
			}
		}

		// Token: 0x020000D0 RID: 208
		private class TimeSpanProperty : ConfigurableObjectLogSchema<T, Tschema>.PropertyBase
		{
			// Token: 0x06000AA0 RID: 2720 RVA: 0x0002F744 File Offset: 0x0002D944
			public override object GetValueInternal(object val)
			{
				return ((TimeSpan)val).Ticks;
			}
		}

		// Token: 0x020000D1 RID: 209
		private class EnhancedTimeSpanProperty : ConfigurableObjectLogSchema<T, Tschema>.PropertyBase
		{
			// Token: 0x06000AA2 RID: 2722 RVA: 0x0002F76C File Offset: 0x0002D96C
			public override object GetValueInternal(object val)
			{
				return ((EnhancedTimeSpan)val).Ticks;
			}
		}

		// Token: 0x020000D2 RID: 210
		private class UnlimitedProperty<Tprop, TUnderlyingPropertyBase> : ConfigurableObjectLogSchema<T, Tschema>.PropertyBase where Tprop : struct, IComparable where TUnderlyingPropertyBase : ConfigurableObjectLogSchema<T, Tschema>.PropertyBase, new()
		{
			// Token: 0x06000AA4 RID: 2724 RVA: 0x0002F794 File Offset: 0x0002D994
			public override object GetValueInternal(object val)
			{
				Unlimited<Tprop> unlimited = (Unlimited<Tprop>)val;
				if (unlimited.IsUnlimited)
				{
					return null;
				}
				return ConfigurableObjectLogSchema<T, Tschema>.UnlimitedProperty<Tprop, TUnderlyingPropertyBase>.underlyingProp.GetValueInternal(unlimited.Value);
			}

			// Token: 0x04000411 RID: 1041
			private static ConfigurableObjectLogSchema<T, Tschema>.PropertyBase underlyingProp = Activator.CreateInstance<TUnderlyingPropertyBase>();
		}

		// Token: 0x020000D3 RID: 211
		private class MultiValuedProperty : ConfigurableObjectLogSchema<T, Tschema>.PropertyBase
		{
			// Token: 0x06000AA7 RID: 2727 RVA: 0x0002F7E4 File Offset: 0x0002D9E4
			public override object GetValueInternal(object val)
			{
				MultiValuedPropertyBase multiValuedPropertyBase = val as MultiValuedPropertyBase;
				if (multiValuedPropertyBase == null)
				{
					return null;
				}
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				foreach (object arg in ((IEnumerable)multiValuedPropertyBase))
				{
					if (flag)
					{
						stringBuilder.AppendFormat("{0}", arg);
						flag = false;
					}
					else
					{
						stringBuilder.AppendFormat(";{0}", arg);
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x020000D4 RID: 212
		private class ADObjectIdGuidProperty : ConfigurableObjectLogSchema<T, Tschema>.PropertyBase
		{
			// Token: 0x06000AA9 RID: 2729 RVA: 0x0002F878 File Offset: 0x0002DA78
			public override string GetFieldName()
			{
				return string.Format("{0}_Guid", base.Property.Name);
			}

			// Token: 0x06000AAA RID: 2730 RVA: 0x0002F890 File Offset: 0x0002DA90
			public override object GetValueInternal(object val)
			{
				ADObjectId adobjectId = val as ADObjectId;
				if (adobjectId != null && adobjectId.ObjectGuid != Guid.Empty)
				{
					return adobjectId.ObjectGuid;
				}
				return null;
			}
		}
	}
}
