using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.DataModel.Serialization;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel
{
	// Token: 0x02000006 RID: 6
	public abstract class PropertyChangeTrackingObject : IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002270 File Offset: 0x00000470
		public static IDataContractSurrogate DataContractSurrogate
		{
			get
			{
				return PropertyChangeTrackingObject.DataContractSurrogateInstance;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002277 File Offset: 0x00000477
		public static IDataContractSurrogate LoggingSurrogate
		{
			get
			{
				return PropertyChangeTrackingObject.LoggingSurrogateInstance;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000227E File Offset: 0x0000047E
		public bool IsPropertySet(PropertyDefinition property)
		{
			return this.propertyBag.IsPropertySet(property);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000228C File Offset: 0x0000048C
		protected TPropertyValue GetPropertyValueOrDefault<TPropertyValue>(TypedPropertyDefinition<TPropertyValue> typedProperty)
		{
			return this.propertyBag.GetValueOrDefault<TPropertyValue>(typedProperty);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000229A File Offset: 0x0000049A
		protected void SetPropertyValue<TPropertyValue>(TypedPropertyDefinition<TPropertyValue> typedProperty, TPropertyValue value)
		{
			this.propertyBag.SetValue<TPropertyValue>(typedProperty, value);
			if (typedProperty.IsLoggable)
			{
				this.loggableProperties[typedProperty.Name] = value;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022C8 File Offset: 0x000004C8
		[OnDeserializing]
		private void SetValuesOnDeserialzing(StreamingContext context)
		{
			this.propertyBag = PropertyBag.CreateInstance();
		}

		// Token: 0x04000009 RID: 9
		private static readonly PropertyChangeTrackingObject.PropertyChangeTrackingSurrogate DataContractSurrogateInstance = new PropertyChangeTrackingObject.PropertyChangeTrackingSurrogate();

		// Token: 0x0400000A RID: 10
		private static readonly PropertyChangeTrackingObject.PropertyChangeTrackingLoggingSurrogate LoggingSurrogateInstance = new PropertyChangeTrackingObject.PropertyChangeTrackingLoggingSurrogate();

		// Token: 0x0400000B RID: 11
		private PropertyBag propertyBag = PropertyBag.CreateInstance();

		// Token: 0x0400000C RID: 12
		private Dictionary<string, object> loggableProperties = new Dictionary<string, object>();

		// Token: 0x02000007 RID: 7
		[DataContract]
		private class EntitySerializationData
		{
			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000015 RID: 21 RVA: 0x00002309 File Offset: 0x00000509
			// (set) Token: 0x06000016 RID: 22 RVA: 0x00002311 File Offset: 0x00000511
			[DataMember]
			public PropertyBag ChangedProperties { get; set; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000017 RID: 23 RVA: 0x0000231A File Offset: 0x0000051A
			// (set) Token: 0x06000018 RID: 24 RVA: 0x00002322 File Offset: 0x00000522
			[DataMember]
			public string OriginalTypeAssembly { get; set; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000019 RID: 25 RVA: 0x0000232B File Offset: 0x0000052B
			// (set) Token: 0x0600001A RID: 26 RVA: 0x00002333 File Offset: 0x00000533
			[DataMember]
			public string OriginalTypeName { get; set; }
		}

		// Token: 0x02000008 RID: 8
		[DataContract]
		private class EntityLoggingData
		{
			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600001C RID: 28 RVA: 0x00002344 File Offset: 0x00000544
			// (set) Token: 0x0600001D RID: 29 RVA: 0x0000234C File Offset: 0x0000054C
			public Dictionary<string, object> Properties
			{
				get
				{
					return this.properties;
				}
				set
				{
					this.properties = value;
				}
			}

			// Token: 0x04000010 RID: 16
			[DataMember]
			private Dictionary<string, object> properties;
		}

		// Token: 0x02000009 RID: 9
		private abstract class BasePropertyChangeTrackingSurrogate<T> : IDataContractSurrogate
		{
			// Token: 0x0600001F RID: 31 RVA: 0x0000235D File Offset: 0x0000055D
			public Type GetDataContractType(Type type)
			{
				if (typeof(PropertyChangeTrackingObject).IsAssignableFrom(type))
				{
					return typeof(T);
				}
				if (typeof(ExDateTime) == type)
				{
					return typeof(PropertyChangeTrackingObject.SerializableExDateTime);
				}
				return type;
			}

			// Token: 0x06000020 RID: 32
			public abstract object GetDeserializedObject(object obj, Type targetType);

			// Token: 0x06000021 RID: 33 RVA: 0x0000239C File Offset: 0x0000059C
			public object GetObjectToSerialize(object obj, Type targetType)
			{
				PropertyChangeTrackingObject propertyChangeTrackingObject = obj as PropertyChangeTrackingObject;
				if (propertyChangeTrackingObject != null)
				{
					return this.GetDataToSerialize(propertyChangeTrackingObject);
				}
				if (obj is ExDateTime)
				{
					return new PropertyChangeTrackingObject.SerializableExDateTime((ExDateTime)obj);
				}
				return obj;
			}

			// Token: 0x06000022 RID: 34 RVA: 0x000023D0 File Offset: 0x000005D0
			public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
			{
				return null;
			}

			// Token: 0x06000023 RID: 35 RVA: 0x000023D3 File Offset: 0x000005D3
			public object GetCustomDataToExport(Type clrType, Type dataContractType)
			{
				return null;
			}

			// Token: 0x06000024 RID: 36 RVA: 0x000023D6 File Offset: 0x000005D6
			public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
			{
			}

			// Token: 0x06000025 RID: 37 RVA: 0x000023D8 File Offset: 0x000005D8
			public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
			{
				return null;
			}

			// Token: 0x06000026 RID: 38 RVA: 0x000023DB File Offset: 0x000005DB
			public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
			{
				return null;
			}

			// Token: 0x06000027 RID: 39
			protected abstract object GetDataToSerialize(PropertyChangeTrackingObject outerObject);
		}

		// Token: 0x0200000A RID: 10
		private class PropertyChangeTrackingSurrogate : PropertyChangeTrackingObject.BasePropertyChangeTrackingSurrogate<PropertyChangeTrackingObject.EntitySerializationData>
		{
			// Token: 0x06000029 RID: 41 RVA: 0x000023E8 File Offset: 0x000005E8
			public override object GetDeserializedObject(object obj, Type targetType)
			{
				PropertyChangeTrackingObject.EntitySerializationData entitySerializationData = obj as PropertyChangeTrackingObject.EntitySerializationData;
				if (entitySerializationData != null)
				{
					string originalTypeName = entitySerializationData.OriginalTypeName;
					Type type = Type.GetType(originalTypeName, false);
					if (type == null)
					{
						string originalTypeAssembly = entitySerializationData.OriginalTypeAssembly;
						Assembly assembly = Assembly.Load(originalTypeAssembly);
						type = assembly.GetType(originalTypeName);
					}
					if (type == null)
					{
						throw new CorruptDataException(Strings.ErrorUnknownType(originalTypeName));
					}
					Type typeFromHandle = typeof(PropertyChangeTrackingObject);
					if (!typeFromHandle.IsAssignableFrom(type))
					{
						throw new CorruptDataException(Strings.ErrorNonEntityType(originalTypeName, typeFromHandle.Name));
					}
					try
					{
						PropertyChangeTrackingObject propertyChangeTrackingObject = (PropertyChangeTrackingObject)Activator.CreateInstance(type);
						propertyChangeTrackingObject.propertyBag = ((PropertyChangeTrackingObject.EntitySerializationData)obj).ChangedProperties;
						return propertyChangeTrackingObject;
					}
					catch (MissingMethodException)
					{
						throw new CorruptDataException(Strings.ErrorNoDefaultConstructor(originalTypeName));
					}
				}
				PropertyChangeTrackingObject.SerializableExDateTime serializableExDateTime = obj as PropertyChangeTrackingObject.SerializableExDateTime;
				if (serializableExDateTime != null)
				{
					return serializableExDateTime.ToExDateTime();
				}
				return obj;
			}

			// Token: 0x0600002A RID: 42 RVA: 0x000024D0 File Offset: 0x000006D0
			protected override object GetDataToSerialize(PropertyChangeTrackingObject outerObject)
			{
				return new PropertyChangeTrackingObject.EntitySerializationData
				{
					ChangedProperties = outerObject.propertyBag,
					OriginalTypeName = outerObject.GetType().FullName,
					OriginalTypeAssembly = outerObject.GetType().Assembly.FullName
				};
			}
		}

		// Token: 0x0200000B RID: 11
		[DataContract]
		private class SerializableExDateTime
		{
			// Token: 0x0600002C RID: 44 RVA: 0x0000251F File Offset: 0x0000071F
			public SerializableExDateTime(ExDateTime value)
			{
				this.utcDateTime = value.UniversalTime;
				this.timeZoneId = value.TimeZone.Id;
			}

			// Token: 0x0600002D RID: 45 RVA: 0x00002548 File Offset: 0x00000748
			public ExDateTime ToExDateTime()
			{
				ExTimeZone utcTimeZone;
				if (!ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(this.timeZoneId, out utcTimeZone))
				{
					utcTimeZone = ExTimeZone.UtcTimeZone;
				}
				return new ExDateTime(utcTimeZone, this.utcDateTime);
			}

			// Token: 0x04000011 RID: 17
			[DataMember]
			private readonly DateTime utcDateTime;

			// Token: 0x04000012 RID: 18
			[DataMember]
			private readonly string timeZoneId;
		}

		// Token: 0x0200000C RID: 12
		private class PropertyChangeTrackingLoggingSurrogate : PropertyChangeTrackingObject.BasePropertyChangeTrackingSurrogate<PropertyChangeTrackingObject.EntityLoggingData>
		{
			// Token: 0x0600002E RID: 46 RVA: 0x0000257B File Offset: 0x0000077B
			public override object GetDeserializedObject(object obj, Type targetType)
			{
				return new NotImplementedException();
			}

			// Token: 0x0600002F RID: 47 RVA: 0x00002584 File Offset: 0x00000784
			protected override object GetDataToSerialize(PropertyChangeTrackingObject outerObject)
			{
				PropertyChangeTrackingObject.EntityLoggingData result = null;
				if (outerObject.loggableProperties.Count > 0)
				{
					result = new PropertyChangeTrackingObject.EntityLoggingData
					{
						Properties = outerObject.loggableProperties
					};
				}
				return result;
			}
		}
	}
}
