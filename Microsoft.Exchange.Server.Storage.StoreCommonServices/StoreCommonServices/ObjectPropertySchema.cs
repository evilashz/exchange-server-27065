using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000E7 RID: 231
	public class ObjectPropertySchema
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x0002AC10 File Offset: 0x00028E10
		public ObjectPropertySchema()
		{
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0002AC18 File Offset: 0x00028E18
		public ObjectPropertySchema(ObjectType objectType, Table table, Dictionary<StorePropTag, PropertyMapping> mappedProperties, Func<IRowAccess, IRowPropertyBag> rowPropBagCreator)
		{
			this.Initialize(objectType, table, mappedProperties, rowPropBagCreator, null);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0002AC2C File Offset: 0x00028E2C
		public void Initialize(ObjectType objectType, Table table, Dictionary<StorePropTag, PropertyMapping> mappedProperties, Func<IRowAccess, IRowPropertyBag> rowPropBagCreator, ObjectPropertySchema baseSchema)
		{
			this.objectType = objectType;
			this.table = table;
			this.rowPropBagCreator = rowPropBagCreator;
			this.mappedProperties = mappedProperties;
			this.baseSchema = baseSchema;
			this.propertyIdMapping = new Dictionary<ushort, PropertyMapping>(Math.Max(mappedProperties.Count, 1));
			foreach (PropertyMapping propertyMapping in this.mappedProperties.Values)
			{
				if (propertyMapping.IsPrimary)
				{
					this.propertyIdMapping.Add(propertyMapping.PropertyTag.PropId, propertyMapping);
				}
			}
			foreach (PropertyMapping propertyMapping2 in this.mappedProperties.Values)
			{
				if (!this.propertyIdMapping.ContainsKey(propertyMapping2.PropertyTag.PropId))
				{
					this.propertyIdMapping.Add(propertyMapping2.PropertyTag.PropId, propertyMapping2);
				}
			}
			if (objectType == ObjectType.Message)
			{
				this.columnGroups = new ulong[table.Columns.Count];
				foreach (PropertyMapping propertyMapping3 in this.mappedProperties.Values)
				{
					if (propertyMapping3.MappingKind == PropertyMappingKind.PhysicalColumn)
					{
						PhysicalColumn physicalColumn = (PhysicalColumn)propertyMapping3.Column.ActualColumn;
						if (physicalColumn.Index >= 0)
						{
							this.columnGroups[physicalColumn.Index] = propertyMapping3.PropertyTag.GroupMask;
						}
					}
				}
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0002ADF4 File Offset: 0x00028FF4
		public Func<IRowAccess, IRowPropertyBag> RowPropBagCreator
		{
			get
			{
				return this.rowPropBagCreator;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0002ADFC File Offset: 0x00028FFC
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0002AE04 File Offset: 0x00029004
		public ObjectType ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0002AE0C File Offset: 0x0002900C
		public ObjectType BaseObjectType
		{
			get
			{
				return WellKnownProperties.BaseObjectType[(int)this.objectType];
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0002AE1A File Offset: 0x0002901A
		public ulong[] ColumnGroups
		{
			get
			{
				return this.columnGroups;
			}
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0002AE24 File Offset: 0x00029024
		public PropertyMapping FindMapping(StorePropTag propertyTag)
		{
			PropertyMapping result;
			if (!this.mappedProperties.TryGetValue(propertyTag, out result))
			{
				result = null;
				if (this.BaseSchema != null && this.BaseSchema.Table == this.Table)
				{
					result = this.BaseSchema.FindMapping(propertyTag);
				}
			}
			return result;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0002AE71 File Offset: 0x00029071
		public bool TryGetPrimaryMapping(ushort propId, out PropertyMapping primaryMapping)
		{
			return this.propertyIdMapping.TryGetValue(propId, out primaryMapping);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0002AE80 File Offset: 0x00029080
		public void EnumerateMappings(Action<PropertyMapping> action)
		{
			foreach (PropertyMapping obj in this.mappedProperties.Values)
			{
				action(obj);
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0002AED8 File Offset: 0x000290D8
		public object GetPropertyValue(Context context, ISimpleReadOnlyPropertyBag bag, StorePropTag propTag)
		{
			PropertyMapping propertyMapping = this.FindMapping(propTag);
			PropertyMapping propertyMapping2;
			if (propertyMapping == null && this.TryGetPrimaryMapping(propTag.PropId, out propertyMapping2) && propertyMapping2.IsReservedPropId)
			{
				return null;
			}
			if (propertyMapping == null)
			{
				return bag.GetBlobPropertyValue(context, propTag);
			}
			return propertyMapping.GetPropertyValue(context, bag);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0002AF20 File Offset: 0x00029120
		public bool TryGetProperty(Context context, ISimpleReadOnlyPropertyBag bag, ushort propId, out StorePropTag propTag, out object value)
		{
			PropertyMapping propertyMapping;
			bool flag;
			if (this.TryGetPrimaryMapping(propId, out propertyMapping) && propertyMapping.IsReservedPropId)
			{
				propTag = propertyMapping.PropertyTag;
				value = propertyMapping.GetPropertyValue(context, bag);
				flag = (value != null);
			}
			else
			{
				flag = bag.TryGetBlobProperty(context, propId, out propTag, out value);
				if (!flag && propertyMapping != null && propertyMapping.MappingKind != PropertyMappingKind.Default)
				{
					propTag = propertyMapping.PropertyTag;
					value = propertyMapping.GetPropertyValue(context, bag);
					flag = (value != null);
				}
			}
			return flag;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0002AFA4 File Offset: 0x000291A4
		public ErrorCode SetProperty(Context context, ISimplePropertyBag bag, StorePropTag propTag, object value)
		{
			PropertyMapping propertyMapping = this.FindMapping(propTag);
			PropertyMapping propertyMapping2;
			if (propertyMapping == null && this.TryGetPrimaryMapping(propTag.PropId, out propertyMapping2) && propertyMapping2.IsReservedPropId)
			{
				return ErrorCode.CreateUnexpectedType((LID)53016U, propTag.PropTag);
			}
			if (propertyMapping == null)
			{
				bag.SetBlobProperty(context, propTag, value);
				return ErrorCode.NoError;
			}
			if (!propertyMapping.CanBeSet)
			{
				return ErrorCode.CreateNoAccess((LID)46872U, propTag.PropTag);
			}
			return propertyMapping.SetPropertyValue(context, bag, value);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0002B028 File Offset: 0x00029228
		public bool IsPropertyChanged(Context context, ISimplePropertyBagWithChangeTracking bag, StorePropTag propTag)
		{
			PropertyMapping propertyMapping = this.FindMapping(propTag);
			PropertyMapping propertyMapping2;
			if (propertyMapping == null && this.TryGetPrimaryMapping(propTag.PropId, out propertyMapping2) && propertyMapping2.IsReservedPropId)
			{
				return false;
			}
			if (propertyMapping == null)
			{
				return bag.IsBlobPropertyChanged(context, propTag);
			}
			return propertyMapping.IsPropertyChanged(context, bag);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0002B0BC File Offset: 0x000292BC
		public void EnumerateProperties(Context context, ISimpleReadOnlyPropertyBag bag, Func<StorePropTag, object, bool> action, bool showValue)
		{
			foreach (PropertyMapping propertyMapping in this.mappedProperties.Values)
			{
				if (propertyMapping.ShouldBeListed && propertyMapping.MappingKind != PropertyMappingKind.Default)
				{
					object propertyValue = propertyMapping.GetPropertyValue(context, bag);
					if (propertyValue != null && !action(propertyMapping.PropertyTag, showValue ? propertyValue : null))
					{
						return;
					}
				}
			}
			bag.EnumerateBlobProperties(context, delegate(StorePropTag propTag, object propValue)
			{
				PropertyMapping propertyMapping2;
				return (this.mappedProperties.TryGetValue(propTag, out propertyMapping2) && (propertyMapping2.MappingKind != PropertyMappingKind.Default || !propertyMapping2.ShouldBeListed)) || action(propTag, propValue);
			}, showValue);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0002B170 File Offset: 0x00029370
		public ErrorCode OpenPropertyReadStream(Context context, ISimplePropertyBag bag, StorePropTag propTag, out Stream readStream)
		{
			PropertyMapping propertyMapping = this.FindMapping(propTag);
			PropertyMapping propertyMapping2;
			if (propertyMapping == null && this.TryGetPrimaryMapping(propTag.PropId, out propertyMapping2) && propertyMapping2.IsReservedPropId)
			{
				readStream = null;
				return ErrorCode.CreateNotFound((LID)63256U, propTag.PropTag);
			}
			if (propertyMapping == null)
			{
				readStream = null;
				return ErrorCode.CreateNotSupported((LID)55064U, propTag.PropTag);
			}
			return propertyMapping.OpenPropertyReadStream(context, bag, out readStream);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0002B1E4 File Offset: 0x000293E4
		public ErrorCode OpenPropertyWriteStream(Context context, ISimplePropertyBag bag, StorePropTag propTag, out Stream writeStream)
		{
			PropertyMapping propertyMapping = this.FindMapping(propTag);
			PropertyMapping propertyMapping2;
			if (propertyMapping == null && this.TryGetPrimaryMapping(propTag.PropId, out propertyMapping2) && propertyMapping2.IsReservedPropId)
			{
				writeStream = null;
				return ErrorCode.CreateNoAccess((LID)42776U, propTag.PropTag);
			}
			if (propertyMapping == null)
			{
				writeStream = null;
				return ErrorCode.CreateNotSupported((LID)59160U, propTag.PropTag);
			}
			if (!propertyMapping.CanBeSet)
			{
				writeStream = null;
				return ErrorCode.CreateNoAccess((LID)50968U, propTag.PropTag);
			}
			return propertyMapping.OpenPropertyWriteStream(context, bag, out writeStream);
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0002B27B File Offset: 0x0002947B
		internal ObjectPropertySchema BaseSchema
		{
			get
			{
				return this.baseSchema;
			}
		}

		// Token: 0x04000537 RID: 1335
		private Dictionary<StorePropTag, PropertyMapping> mappedProperties;

		// Token: 0x04000538 RID: 1336
		private Dictionary<ushort, PropertyMapping> propertyIdMapping;

		// Token: 0x04000539 RID: 1337
		private Func<IRowAccess, IRowPropertyBag> rowPropBagCreator;

		// Token: 0x0400053A RID: 1338
		private Table table;

		// Token: 0x0400053B RID: 1339
		private ulong[] columnGroups;

		// Token: 0x0400053C RID: 1340
		private ObjectPropertySchema baseSchema;

		// Token: 0x0400053D RID: 1341
		private ObjectType objectType;

		// Token: 0x0400053E RID: 1342
		public static readonly ObjectPropertySchema Empty = new ObjectPropertySchema(ObjectType.Invalid, null, new Dictionary<StorePropTag, PropertyMapping>(1), null);
	}
}
