using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Reflection;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.ObjectModel;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x0200001E RID: 30
	internal class DataSourceManager : IDisposable, IConfigDataProvider
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00005364 File Offset: 0x00003564
		public DataSourceManager(SchemaManager schemaManager)
		{
			ExTraceGlobals.DataSourceManagerTracer.Information<string>((long)this.GetHashCode(), "DataSourceManager::DataSourceManager - initializing data source manager for data source info type {0}.", "null");
			this.schemaManager = schemaManager;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000538E File Offset: 0x0000358E
		public string Source
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005395 File Offset: 0x00003595
		public SchemaManager SchemaManager
		{
			get
			{
				return this.schemaManager;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000539D File Offset: 0x0000359D
		public virtual bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000053A5 File Offset: 0x000035A5
		protected Type PersistableType
		{
			get
			{
				return this.schemaManager.PersistableType;
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000053B4 File Offset: 0x000035B4
		public static DataSourceManager[] GetDataSourceManagers(Type classType, string propertyName)
		{
			ExTraceGlobals.DataSourceManagerTracer.Information<string, string>(0L, "DataSourceManager::GetDataSourceManagers - retrieving data source managers for class type {0} and data source info type {1}.", classType.Name, "null");
			SchemaManagerCollection schemaManagerCollection = (SchemaManagerCollection)DataSourceManager.schemaManagerHashtable[classType];
			if (schemaManagerCollection == null)
			{
				lock (typeof(DataSourceManager))
				{
					schemaManagerCollection = new SchemaManagerCollection(classType);
					DataSourceManager.schemaManagerHashtable[classType] = schemaManagerCollection;
				}
			}
			return schemaManagerCollection.GetDataSourceManagers(propertyName);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000543C File Offset: 0x0000363C
		public virtual void ReadLinked(ConfigObject instanceToRead, Type objectType, string linkValue)
		{
			throw new NotImplementedException("Multiple DSMs are not supported yet.");
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005448 File Offset: 0x00003648
		public virtual ConfigObject Read(Type configObjectType, string identity)
		{
			return null;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000544B File Offset: 0x0000364B
		public virtual void Save(ConfigObject instanceToSave)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000544D File Offset: 0x0000364D
		public virtual void Delete(ConfigObject instanceToDelete)
		{
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000544F File Offset: 0x0000364F
		public virtual ConfigObject[] Find(Type classType, string searchExpr, bool searchMany, ConfigObject[] objectArgs)
		{
			return null;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005452 File Offset: 0x00003652
		IConfigurable IConfigDataProvider.Read<T>(ObjectId identity)
		{
			return this.Read(typeof(T), ((ConfigObjectId)identity).ToString());
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000546F File Offset: 0x0000366F
		IConfigurable[] IConfigDataProvider.Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			return (IConfigurable[])this.Find(typeof(T), "", true, null);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000548D File Offset: 0x0000368D
		IEnumerable<T> IConfigDataProvider.FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			return (IEnumerable<T>)((IEnumerable<IConfigurable>)this.Find(typeof(T), "", true, null));
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000054B0 File Offset: 0x000036B0
		void IConfigDataProvider.Save(IConfigurable instance)
		{
			this.Save((ConfigObject)instance);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000054BE File Offset: 0x000036BE
		void IConfigDataProvider.Delete(IConfigurable instance)
		{
			this.Delete((ConfigObject)instance);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000054CC File Offset: 0x000036CC
		public virtual void Dispose()
		{
			ExTraceGlobals.DataSourceManagerTracer.Information((long)this.GetHashCode(), "DataSourceManager::Dispose - disposing of data source session.");
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000054F1 File Offset: 0x000036F1
		public virtual object StartRange(string identity, string propertyName, int pageSize)
		{
			return null;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000054F4 File Offset: 0x000036F4
		public virtual bool NextRange(object context, List<object> resultStore)
		{
			return false;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000054F7 File Offset: 0x000036F7
		public virtual void EndRange(object context)
		{
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000054FC File Offset: 0x000036FC
		protected void StampStorageIdentity(PropertyBag propertyBag)
		{
			object obj = propertyBag[DataSourceManager.StorageIdentityName];
			if (obj == null)
			{
				obj = propertyBag["Identity"];
			}
			propertyBag.Add(DataSourceManager.StorageIdentityName + "." + base.GetType().Name, obj);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005545 File Offset: 0x00003745
		protected string GetStorageIdentity(PropertyBag propertyBag)
		{
			return (string)propertyBag[DataSourceManager.StorageIdentityName + "." + base.GetType().Name];
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000556C File Offset: 0x0000376C
		protected virtual void Dispose(bool disposing)
		{
			ExTraceGlobals.DataSourceManagerTracer.Information((long)this.GetHashCode(), "DataSourceManager::Dispose - disposing of data source session.");
			if (!this.isDisposed && disposing)
			{
				this.isDisposed = true;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005598 File Offset: 0x00003798
		protected virtual object ConvertValue(object valueToConvert, Type newType)
		{
			ExTraceGlobals.DataSourceManagerTracer.Information<object, object, Type>((long)this.GetHashCode(), "DataSourceManager::ConvertValue - converting value {0} from type {1} to type {2}.", (valueToConvert == null) ? "null" : valueToConvert, (valueToConvert == null) ? "null" : valueToConvert.GetType(), newType);
			Type type = valueToConvert.GetType();
			if (type == newType)
			{
				return valueToConvert;
			}
			if (typeof(string) == newType)
			{
				return valueToConvert.ToString();
			}
			if (typeof(bool) == newType)
			{
				return bool.Parse(valueToConvert.ToString());
			}
			if (typeof(int) == newType)
			{
				return int.Parse(valueToConvert.ToString());
			}
			if (typeof(double) == newType)
			{
				return double.Parse(valueToConvert.ToString());
			}
			if (typeof(long) == newType)
			{
				return long.Parse(valueToConvert.ToString());
			}
			if (typeof(DateTime) == newType)
			{
				return DateTime.Parse(valueToConvert.ToString());
			}
			if (typeof(Guid) == newType)
			{
				return new Guid(valueToConvert.ToString());
			}
			if (typeof(IPAddress) == newType)
			{
				return IPAddress.Parse(valueToConvert.ToString());
			}
			if (newType.IsSubclassOf(typeof(Enum)))
			{
				return Enum.Parse(newType, valueToConvert.ToString(), true);
			}
			TypeConverter converter = TypeDescriptor.GetConverter(newType);
			if (converter != null && converter.CanConvertFrom(valueToConvert.GetType()))
			{
				return converter.ConvertFrom(valueToConvert);
			}
			ConstructorInfo constructor = newType.GetConstructor(new Type[]
			{
				type
			});
			if (null != constructor)
			{
				return constructor.Invoke(new object[]
				{
					valueToConvert
				});
			}
			if (typeof(string) == type)
			{
				MethodInfo method = newType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[]
				{
					type
				}, null);
				if (null != method)
				{
					return method.Invoke(null, new object[]
					{
						valueToConvert
					});
				}
			}
			throw new InvalidCastException(Strings.ExceptionNoConversion(valueToConvert.GetType(), newType));
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000057D0 File Offset: 0x000039D0
		protected ConfigObject ConstructConfigObject(Type objectType, DataSourceInfo dsi, PropertyBag propertyBag, bool fIsNew)
		{
			ConfigObjectDelegate configObjectDelegate = null;
			ConstructorInfo constructorInfo = null;
			this.StampStorageIdentity(propertyBag);
			object obj = DataSourceManager.constructorHashtable[objectType];
			if (obj == null)
			{
				DataSourceManager.CacheObjectCreationInfo(objectType, ref configObjectDelegate, ref constructorInfo);
			}
			else
			{
				configObjectDelegate = (obj as ConfigObjectDelegate);
				constructorInfo = (obj as ConstructorInfo);
			}
			ConfigObject configObject;
			if (configObjectDelegate != null)
			{
				configObject = configObjectDelegate(propertyBag);
			}
			else
			{
				configObject = (ConfigObject)constructorInfo.Invoke(null);
				configObject.SetIsNew(fIsNew);
				configObject.Fields = propertyBag;
			}
			configObject.SetDataSourceInfo(dsi);
			configObject.InitializeDefaults();
			configObject.Fields.ResetChangeTracking();
			return configObject;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005854 File Offset: 0x00003A54
		protected Type GetClassType(string typeName)
		{
			ExTraceGlobals.DataSourceManagerTracer.Information<string>((long)this.GetHashCode(), "DataSourceManager::GetClassType - getting class type for class name {0}.", (typeName == null) ? "null" : typeName);
			Type type = (Type)DataSourceManager.typeHashtable[typeName];
			if (null != type)
			{
				return type;
			}
			type = Assembly.GetCallingAssembly().GetType(typeName, false);
			if (null == type)
			{
				type = Assembly.GetExecutingAssembly().GetType(typeName, false);
				if (null == type)
				{
					foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
					{
						type = assembly.GetType(typeName, false);
						if (null != type)
						{
							break;
						}
					}
				}
			}
			if (null != type)
			{
				lock (typeof(DataSourceManager))
				{
					DataSourceManager.typeHashtable[typeName] = type;
				}
				return type;
			}
			throw new DataSourceManagerException(Strings.ExceptionTypeNotFound(typeName));
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005954 File Offset: 0x00003B54
		protected void CheckAllowedType(Type configObjectType)
		{
			if (!configObjectType.IsSubclassOf(typeof(ConfigObject)))
			{
				throw new DataSourceManagerException(Strings.ExceptionInvalidConfigObjectType(configObjectType));
			}
			if (configObjectType != this.PersistableType && !configObjectType.IsSubclassOf(this.PersistableType))
			{
				throw new DataSourceManagerException(Strings.ExceptionMismatchedConfigObjectType(this.PersistableType, configObjectType));
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000059B0 File Offset: 0x00003BB0
		private static void CacheObjectCreationInfo(Type classType, ref ConfigObjectDelegate configObjectDelegate, ref ConstructorInfo configObjectConstructorInfo)
		{
			MethodInfo method = classType.GetMethod("CreateInstance", BindingFlags.Static | BindingFlags.Public);
			object value;
			if (null != method)
			{
				configObjectDelegate = (ConfigObjectDelegate)Delegate.CreateDelegate(typeof(ConfigObjectDelegate), method);
				value = configObjectDelegate;
			}
			else
			{
				configObjectConstructorInfo = classType.GetConstructor(Type.EmptyTypes);
				value = configObjectConstructorInfo;
			}
			lock (typeof(DataSourceManager))
			{
				DataSourceManager.constructorHashtable[classType] = value;
			}
		}

		// Token: 0x0400005A RID: 90
		public static string LinkIdName = "Internal.LinkId";

		// Token: 0x0400005B RID: 91
		public static string StorageIdentityName = "Internal.StorageIdentity";

		// Token: 0x0400005C RID: 92
		private static Hashtable typeHashtable = new Hashtable();

		// Token: 0x0400005D RID: 93
		private static Hashtable constructorHashtable = new Hashtable();

		// Token: 0x0400005E RID: 94
		private static Hashtable schemaManagerHashtable = new Hashtable();

		// Token: 0x0400005F RID: 95
		private bool isDisposed;

		// Token: 0x04000060 RID: 96
		private SchemaManager schemaManager;
	}
}
