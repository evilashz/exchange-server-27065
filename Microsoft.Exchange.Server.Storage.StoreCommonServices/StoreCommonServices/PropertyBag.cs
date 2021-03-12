using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropertyBlob;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200009B RID: 155
	public abstract class PropertyBag : ISimplePropertyBagWithChangeTracking, ISimplePropertyBag, ISimpleReadOnlyPropertyBag, ISimplePropertyStorageWithChangeTracking, ISimplePropertyStorage, ISimpleReadOnlyPropertyStorage, ITWIR, IInstanceNumberOverride
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x0001E626 File Offset: 0x0001C826
		protected PropertyBag(bool changeTrackingEnabled)
		{
			this.changeTrackingEnabled = changeTrackingEnabled;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001E638 File Offset: 0x0001C838
		public static string PropertyBlobToString(byte[] buffer)
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			PropertyBlob.BlobReader blobReader = new PropertyBlob.BlobReader(buffer, 0);
			for (int i = 0; i < blobReader.PropertyCount; i++)
			{
				StorePropTag tag = StorePropTag.CreateWithoutInfo(blobReader.GetPropertyTag(i));
				object propertyValue = blobReader.GetPropertyValue(i);
				Property property = new Property(tag, propertyValue);
				property.AppendToString(stringBuilder);
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0001E6A4 File Offset: 0x0001C8A4
		public virtual bool IsDirty
		{
			get
			{
				return this.PropertiesDirty;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0001E6AC File Offset: 0x0001C8AC
		public bool ChangeTrackingEnabled
		{
			get
			{
				return this.changeTrackingEnabled;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0001E6B4 File Offset: 0x0001C8B4
		public virtual bool IsChanged
		{
			get
			{
				return this.originalProperties != null && this.originalProperties.Count != 0;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0001E6D1 File Offset: 0x0001C8D1
		public IOriginalPropertyBag OriginalBag
		{
			get
			{
				if (this.originalBag == null)
				{
					this.originalBag = new PropertyBag.OriginalPropertyBag(this);
				}
				return this.originalBag;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000555 RID: 1365
		public abstract Context CurrentOperationContext { get; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000556 RID: 1366
		public abstract ObjectPropertySchema Schema { get; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000557 RID: 1367
		public abstract ReplidGuidMap ReplidGuidMap { get; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0001E6ED File Offset: 0x0001C8ED
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x0001E6F5 File Offset: 0x0001C8F5
		public bool NoReplicateOperationInProgress { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0001E6FE File Offset: 0x0001C8FE
		public int CountOfBlobProperties
		{
			get
			{
				if (this.Properties == null)
				{
					return 0;
				}
				return this.Properties.Count;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600055B RID: 1371
		protected abstract Dictionary<ushort, KeyValuePair<StorePropTag, object>> Properties { get; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600055C RID: 1372
		// (set) Token: 0x0600055D RID: 1373
		protected abstract bool PropertiesDirty { get; set; }

		// Token: 0x0600055E RID: 1374
		protected abstract void AssignPropertiesToUse(Dictionary<ushort, KeyValuePair<StorePropTag, object>> properties);

		// Token: 0x0600055F RID: 1375 RVA: 0x0001E715 File Offset: 0x0001C915
		public virtual object GetPropertyValue(Context context, StorePropTag propTag)
		{
			if (this.Schema == null)
			{
				return this.GetBlobPropertyValue(context, propTag);
			}
			return this.Schema.GetPropertyValue(context, this, propTag);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001E736 File Offset: 0x0001C936
		public virtual bool TryGetProperty(Context context, ushort propId, out StorePropTag propTag, out object value)
		{
			if (this.Schema == null)
			{
				return this.TryGetBlobProperty(context, propId, out propTag, out value);
			}
			return this.Schema.TryGetProperty(context, this, propId, out propTag, out value);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001E75D File Offset: 0x0001C95D
		public virtual void EnumerateProperties(Context context, Func<StorePropTag, object, bool> action, bool showValue)
		{
			if (this.Schema == null)
			{
				this.EnumerateBlobProperties(context, action, showValue);
				return;
			}
			this.Schema.EnumerateProperties(context, this, action, showValue);
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0001E780 File Offset: 0x0001C980
		IReplidGuidMap ISimplePropertyBag.ReplidGuidMap
		{
			get
			{
				return this.ReplidGuidMap;
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001E788 File Offset: 0x0001C988
		public virtual ErrorCode SetProperty(Context context, StorePropTag propTag, object value)
		{
			if (this.Schema == null)
			{
				this.SetBlobProperty(context, propTag, value);
				return ErrorCode.NoError;
			}
			return this.Schema.SetProperty(context, this, propTag, value);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001E7B0 File Offset: 0x0001C9B0
		public virtual ErrorCode OpenPropertyReadStream(Context context, StorePropTag propTag, out Stream stream)
		{
			if (this.Schema == null)
			{
				stream = null;
				return ErrorCode.CreateNotSupported((LID)36632U, propTag.PropTag);
			}
			return this.Schema.OpenPropertyReadStream(context, this, propTag, out stream);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001E7E3 File Offset: 0x0001C9E3
		public virtual ErrorCode OpenPropertyWriteStream(Context context, StorePropTag propTag, out Stream stream)
		{
			if (this.Schema == null)
			{
				stream = null;
				return ErrorCode.CreateNotSupported((LID)38680U, propTag.PropTag);
			}
			return this.Schema.OpenPropertyWriteStream(context, this, propTag, out stream);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001E816 File Offset: 0x0001CA16
		public virtual bool IsPropertyChanged(Context context, StorePropTag propTag)
		{
			if (this.Schema == null)
			{
				return this.IsBlobPropertyChanged(context, propTag);
			}
			return this.Schema.IsPropertyChanged(context, this, propTag);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001E837 File Offset: 0x0001CA37
		public virtual object GetOriginalPropertyValue(Context context, StorePropTag propTag)
		{
			return this.OriginalBag.GetPropertyValue(context, propTag);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001E848 File Offset: 0x0001CA48
		public virtual object GetBlobPropertyValue(Context context, StorePropTag propTag)
		{
			if (this.Properties != null)
			{
				KeyValuePair<StorePropTag, object> keyValuePair;
				bool flag = this.Properties.TryGetValue(propTag.PropId, out keyValuePair);
				if (flag && keyValuePair.Key == propTag)
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001E890 File Offset: 0x0001CA90
		public virtual bool TryGetBlobProperty(Context context, ushort propId, out StorePropTag propTag, out object value)
		{
			KeyValuePair<StorePropTag, object> keyValuePair;
			if (this.Properties != null && this.Properties.TryGetValue(propId, out keyValuePair))
			{
				propTag = keyValuePair.Key;
				value = keyValuePair.Value;
				return true;
			}
			propTag = StorePropTag.Invalid;
			value = null;
			return false;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001E8DE File Offset: 0x0001CADE
		public virtual object GetPhysicalColumnValue(Context context, PhysicalColumn column)
		{
			return null;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001E8E4 File Offset: 0x0001CAE4
		public virtual void EnumerateBlobProperties(Context context, Func<StorePropTag, object, bool> action, bool showValue)
		{
			if (this.Properties != null)
			{
				foreach (KeyValuePair<ushort, KeyValuePair<StorePropTag, object>> keyValuePair in this.Properties)
				{
					if (!action(keyValuePair.Value.Key, showValue ? keyValuePair.Value.Value : null))
					{
						break;
					}
				}
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001E964 File Offset: 0x0001CB64
		public virtual void SetBlobProperty(Context context, StorePropTag propTag, object value)
		{
			this.SetBlobProperty(context, propTag, value, false);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001E970 File Offset: 0x0001CB70
		protected void SetBlobProperty(Context context, StorePropTag propTag, object value, bool keepNulls)
		{
			KeyValuePair<StorePropTag, object> keyValuePair = default(KeyValuePair<StorePropTag, object>);
			bool flag = this.Properties != null && this.Properties.TryGetValue(propTag.PropId, out keyValuePair);
			if ((flag && propTag == keyValuePair.Key && ValueHelper.ValuesEqual(keyValuePair.Value, value)) || (value == null && (!flag || keyValuePair.Value == null)))
			{
				return;
			}
			if (value == null && !keepNulls)
			{
				this.Properties.Remove(propTag.PropId);
			}
			else
			{
				if (this.Properties == null)
				{
					this.AssignPropertiesToUse(new Dictionary<ushort, KeyValuePair<StorePropTag, object>>(20));
				}
				this.Properties[propTag.PropId] = new KeyValuePair<StorePropTag, object>(propTag, value);
			}
			if (this.changeTrackingEnabled)
			{
				if (this.originalProperties == null)
				{
					this.originalProperties = new Dictionary<StorePropTag, object>(20);
				}
				if (!flag || keyValuePair.Value == null || propTag != keyValuePair.Key)
				{
					object y;
					if (!this.originalProperties.TryGetValue(propTag, out y))
					{
						this.originalProperties.Add(propTag, null);
					}
					else if (ValueHelper.ValuesEqual(value, y))
					{
						this.originalProperties.Remove(propTag);
					}
				}
				if (flag && keyValuePair.Value != null)
				{
					object x = (propTag == keyValuePair.Key) ? value : null;
					object y;
					if (!this.originalProperties.TryGetValue(keyValuePair.Key, out y))
					{
						this.originalProperties.Add(keyValuePair.Key, keyValuePair.Value);
					}
					else if (ValueHelper.ValuesEqual(x, y))
					{
						this.originalProperties.Remove(keyValuePair.Key);
					}
				}
			}
			if (!this.IsDirty)
			{
				this.OnDirty(context);
			}
			this.PropertiesDirty = true;
			if (!flag || keyValuePair.Value == null)
			{
				this.OnPropertyChanged(propTag, PropertyBag.GetPropertySize(propTag, value));
				return;
			}
			if (propTag == keyValuePair.Key || value == null)
			{
				long deltaSize = PropertyBag.GetPropertySize(propTag, value) - PropertyBag.GetPropertySize(keyValuePair.Key, keyValuePair.Value);
				this.OnPropertyChanged(keyValuePair.Key, deltaSize);
				return;
			}
			this.OnPropertyChanged(keyValuePair.Key, -PropertyBag.GetPropertySize(keyValuePair.Key, keyValuePair.Value));
			this.OnPropertyChanged(propTag, PropertyBag.GetPropertySize(propTag, value));
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001EB9C File Offset: 0x0001CD9C
		public virtual void SetPhysicalColumn(Context context, PhysicalColumn column, object value)
		{
			throw new NotSupportedException("physical column mapping not supported by a property bag without a table");
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001EBA8 File Offset: 0x0001CDA8
		public virtual ErrorCode OpenPhysicalColumnReadStream(Context context, PhysicalColumn column, out Stream stream)
		{
			stream = null;
			return ErrorCode.CreateNotSupported((LID)40728U);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001EBBC File Offset: 0x0001CDBC
		public virtual ErrorCode OpenPhysicalColumnWriteStream(Context context, PhysicalColumn column, out Stream stream)
		{
			stream = null;
			return ErrorCode.CreateNotSupported((LID)57112U);
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0001EBD0 File Offset: 0x0001CDD0
		ISimpleReadOnlyPropertyBag ISimplePropertyStorageWithChangeTracking.OriginalBag
		{
			get
			{
				return this.OriginalBag;
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001EBD8 File Offset: 0x0001CDD8
		public virtual bool IsBlobPropertyChanged(Context context, StorePropTag propTag)
		{
			return this.originalProperties != null && this.originalProperties.ContainsKey(propTag);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0001EBF0 File Offset: 0x0001CDF0
		public virtual bool IsPhysicalColumnChanged(Context context, PhysicalColumn column)
		{
			return false;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001EBF4 File Offset: 0x0001CDF4
		public virtual object GetOriginalBlobPropertyValue(Context context, StorePropTag propTag)
		{
			object result = null;
			if (this.originalProperties == null || !this.originalProperties.TryGetValue(propTag, out result))
			{
				result = this.GetBlobPropertyValue(context, propTag);
			}
			return result;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001EC25 File Offset: 0x0001CE25
		public virtual bool TryGetOriginalBlobProperty(Context context, ushort propId, out StorePropTag propTag, out object value)
		{
			throw new NotSupportedException("TryGetProperty by ID for original property is not supported");
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001EC31 File Offset: 0x0001CE31
		public virtual object GetOriginalPhysicalColumnValue(Context context, PhysicalColumn column)
		{
			return null;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001EC34 File Offset: 0x0001CE34
		public void EnumerateOriginalBlobProperties(Context context, Func<StorePropTag, object, bool> action, bool showValue)
		{
			if (this.Properties != null)
			{
				foreach (KeyValuePair<ushort, KeyValuePair<StorePropTag, object>> keyValuePair in this.Properties)
				{
					object obj = null;
					if (this.originalProperties == null || !this.originalProperties.TryGetValue(keyValuePair.Value.Key, out obj))
					{
						obj = keyValuePair.Value.Value;
					}
					if (obj != null && !action(keyValuePair.Value.Key, showValue ? obj : null))
					{
						return;
					}
				}
			}
			if (this.originalProperties != null)
			{
				foreach (KeyValuePair<StorePropTag, object> keyValuePair2 in this.originalProperties)
				{
					KeyValuePair<StorePropTag, object> keyValuePair3;
					if ((this.Properties == null || !this.Properties.TryGetValue(keyValuePair2.Key.PropId, out keyValuePair3) || keyValuePair3.Key != keyValuePair2.Key) && keyValuePair2.Value != null && !action(keyValuePair2.Key, showValue ? keyValuePair2.Value : null))
					{
						break;
					}
				}
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001ED98 File Offset: 0x0001CF98
		public void SetInstanceNumber(Context context, object instanceNumber)
		{
			this.instanceNumber = instanceNumber;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001EDA1 File Offset: 0x0001CFA1
		public object GetInstanceNumberOverride()
		{
			return this.instanceNumber;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001EDAC File Offset: 0x0001CFAC
		int ITWIR.GetColumnSize(Column column)
		{
			return ((IColumn)column).GetSize(this);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001EDC4 File Offset: 0x0001CFC4
		object ITWIR.GetColumnValue(Column column)
		{
			return ((IColumn)column).GetValue(this);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001EDDA File Offset: 0x0001CFDA
		int ITWIR.GetPhysicalColumnSize(PhysicalColumn column)
		{
			return 0;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001EDDD File Offset: 0x0001CFDD
		object ITWIR.GetPhysicalColumnValue(PhysicalColumn column)
		{
			return this.GetPhysicalColumnValue(this.CurrentOperationContext, column);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001EDEC File Offset: 0x0001CFEC
		int ITWIR.GetPropertyColumnSize(PropertyColumn column)
		{
			return 0;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001EDEF File Offset: 0x0001CFEF
		object ITWIR.GetPropertyColumnValue(PropertyColumn column)
		{
			return this.GetPropertyValue(this.CurrentOperationContext, column.StorePropTag);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001EE04 File Offset: 0x0001D004
		public virtual void Scrub(Context context)
		{
			if (this.Properties != null && this.Properties.Count != 0)
			{
				StorePropTag[] array = new StorePropTag[this.Properties.Count];
				int num = 0;
				foreach (KeyValuePair<ushort, KeyValuePair<StorePropTag, object>> keyValuePair in this.Properties)
				{
					array[num++] = keyValuePair.Value.Key;
				}
				foreach (StorePropTag propTag in array)
				{
					this.SetBlobProperty(context, propTag, null);
				}
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001EED0 File Offset: 0x0001D0D0
		public virtual void DiscardPrivateCache(Context context)
		{
			this.originalProperties = null;
			this.PropertiesDirty = false;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001EEE0 File Offset: 0x0001D0E0
		public virtual void MakeClean()
		{
			this.PropertiesDirty = false;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001EEE9 File Offset: 0x0001D0E9
		public virtual void CommitChanges()
		{
			this.MakeClean();
			if (this.changeTrackingEnabled && this.originalProperties != null)
			{
				this.originalProperties.Clear();
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001EF0C File Offset: 0x0001D10C
		public virtual ulong GetChangedPropertyGroups(Context context)
		{
			ulong num = 0UL;
			if (this.originalProperties != null)
			{
				foreach (StorePropTag storePropTag in this.originalProperties.Keys)
				{
					num |= storePropTag.GroupMask;
				}
			}
			return num;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001EF74 File Offset: 0x0001D174
		protected bool TryGetBlobPropertyValue(Context context, StorePropTag propTag, out object propValue)
		{
			propValue = null;
			if (this.Properties != null)
			{
				KeyValuePair<StorePropTag, object> keyValuePair;
				bool flag = this.Properties.TryGetValue(propTag.PropId, out keyValuePair);
				if (flag)
				{
					if (keyValuePair.Key == propTag)
					{
						propValue = keyValuePair.Value;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001EFBF File Offset: 0x0001D1BF
		protected virtual void OnPropertyChanged(StorePropTag propTag, long deltaSize)
		{
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001EFC1 File Offset: 0x0001D1C1
		protected virtual void OnDirty(Context context)
		{
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001EFC4 File Offset: 0x0001D1C4
		protected void LoadFromPropertyBlob(Context context, byte[] propertyBlob)
		{
			Dictionary<ushort, KeyValuePair<StorePropTag, object>> properties = null;
			this.LoadPropertiesFromPropertyBlob(context, propertyBlob, ref properties);
			this.AssignPropertiesToUse(properties);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001EFE4 File Offset: 0x0001D1E4
		protected byte[] SaveToPropertyBlob(Context context)
		{
			return PropertyBlob.BuildBlob(this.Properties);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001EFF4 File Offset: 0x0001D1F4
		protected void LoadPropertiesFromPropertyBlob(Context context, byte[] propertyBlob, ref Dictionary<ushort, KeyValuePair<StorePropTag, object>> properties)
		{
			if (propertyBlob != null)
			{
				PropertyBlob.BlobReader blobReader = new PropertyBlob.BlobReader(propertyBlob, 0);
				for (int i = 0; i < blobReader.PropertyCount; i++)
				{
					StorePropTag key = this.MapPropTag(context, blobReader.GetPropertyTag(i));
					object propertyValue = blobReader.GetPropertyValue(i);
					if (properties == null)
					{
						properties = new Dictionary<ushort, KeyValuePair<StorePropTag, object>>(blobReader.PropertyCount);
					}
					KeyValuePair<StorePropTag, object> keyValuePair;
					if (!properties.TryGetValue(key.PropId, out keyValuePair) || keyValuePair.Value is ValueReference)
					{
						properties[key.PropId] = new KeyValuePair<StorePropTag, object>(key, propertyValue);
					}
				}
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001F080 File Offset: 0x0001D280
		protected void LoadPropertiesFromPropertyBlobStream(Context context, Stream propertyBlobStream, ref Dictionary<ushort, KeyValuePair<StorePropTag, object>> properties)
		{
			if (propertyBlobStream != null)
			{
				using (Stream stream = new BufferedStream(propertyBlobStream, 8192))
				{
					PropertyBlob.BlobStreamReader blobStreamReader = new PropertyBlob.BlobStreamReader(stream);
					foreach (KeyValuePair<uint, object> keyValuePair in blobStreamReader.LoadProperties(true, true))
					{
						StorePropTag key = this.MapPropTag(context, keyValuePair.Key);
						if (properties == null)
						{
							properties = new Dictionary<ushort, KeyValuePair<StorePropTag, object>>();
						}
						KeyValuePair<StorePropTag, object> keyValuePair2;
						if (!properties.TryGetValue(key.PropId, out keyValuePair2) || keyValuePair2.Value is ValueReference)
						{
							properties[key.PropId] = new KeyValuePair<StorePropTag, object>(key, keyValuePair.Value);
						}
					}
				}
			}
		}

		// Token: 0x0600058C RID: 1420
		protected abstract StorePropTag MapPropTag(Context context, uint propertyTag);

		// Token: 0x0600058D RID: 1421 RVA: 0x0001F158 File Offset: 0x0001D358
		private static long GetPropertySize(StorePropTag propTag, object value)
		{
			if (value == null)
			{
				return 0L;
			}
			return (long)ValueTypeHelper.ValueSize(PropertyTypeHelper.GetExtendedTypeCode(propTag.PropType), value);
		}

		// Token: 0x040003ED RID: 1005
		public const int PropertyBlobStreamBlockSize = 8192;

		// Token: 0x040003EE RID: 1006
		private const int AverageCustomPropertyNumber = 20;

		// Token: 0x040003EF RID: 1007
		private bool changeTrackingEnabled;

		// Token: 0x040003F0 RID: 1008
		private Dictionary<StorePropTag, object> originalProperties;

		// Token: 0x040003F1 RID: 1009
		private IOriginalPropertyBag originalBag;

		// Token: 0x040003F2 RID: 1010
		private object instanceNumber;

		// Token: 0x0200009D RID: 157
		private class OriginalPropertyBag : IOriginalPropertyBag, ISimpleReadOnlyPropertyBag, ISimpleReadOnlyPropertyStorage, ITWIR, IInstanceNumberOverride
		{
			// Token: 0x0600058E RID: 1422 RVA: 0x0001F173 File Offset: 0x0001D373
			public OriginalPropertyBag(PropertyBag propbag)
			{
				this.propbag = propbag;
			}

			// Token: 0x0600058F RID: 1423 RVA: 0x0001F182 File Offset: 0x0001D382
			public object GetInstanceNumberOverride()
			{
				return this.propbag.GetInstanceNumberOverride();
			}

			// Token: 0x06000590 RID: 1424 RVA: 0x0001F18F File Offset: 0x0001D38F
			public object GetPropertyValue(Context context, StorePropTag propTag)
			{
				if (this.propbag.Schema == null)
				{
					return this.propbag.GetOriginalBlobPropertyValue(context, propTag);
				}
				return this.propbag.Schema.GetPropertyValue(context, this, propTag);
			}

			// Token: 0x06000591 RID: 1425 RVA: 0x0001F1BF File Offset: 0x0001D3BF
			public bool TryGetProperty(Context context, ushort propId, out StorePropTag propTag, out object value)
			{
				if (this.propbag.Schema == null)
				{
					return this.propbag.TryGetOriginalBlobProperty(context, propId, out propTag, out value);
				}
				return this.propbag.Schema.TryGetProperty(context, this, propId, out propTag, out value);
			}

			// Token: 0x06000592 RID: 1426 RVA: 0x0001F1F5 File Offset: 0x0001D3F5
			public void EnumerateProperties(Context context, Func<StorePropTag, object, bool> action, bool showValue)
			{
				if (this.propbag.Schema == null)
				{
					this.propbag.EnumerateOriginalBlobProperties(context, action, showValue);
					return;
				}
				this.propbag.Schema.EnumerateProperties(context, this, action, showValue);
			}

			// Token: 0x06000593 RID: 1427 RVA: 0x0001F227 File Offset: 0x0001D427
			public object GetBlobPropertyValue(Context context, StorePropTag propTag)
			{
				return this.propbag.GetOriginalBlobPropertyValue(context, propTag);
			}

			// Token: 0x06000594 RID: 1428 RVA: 0x0001F236 File Offset: 0x0001D436
			public bool TryGetBlobProperty(Context context, ushort propId, out StorePropTag propTag, out object value)
			{
				return this.propbag.TryGetOriginalBlobProperty(context, propId, out propTag, out value);
			}

			// Token: 0x06000595 RID: 1429 RVA: 0x0001F248 File Offset: 0x0001D448
			public object GetPhysicalColumnValue(Context context, PhysicalColumn column)
			{
				return this.propbag.GetOriginalPhysicalColumnValue(context, column);
			}

			// Token: 0x06000596 RID: 1430 RVA: 0x0001F257 File Offset: 0x0001D457
			public void EnumerateBlobProperties(Context context, Func<StorePropTag, object, bool> action, bool showValue)
			{
				this.propbag.EnumerateOriginalBlobProperties(context, action, showValue);
			}

			// Token: 0x06000597 RID: 1431 RVA: 0x0001F268 File Offset: 0x0001D468
			int ITWIR.GetColumnSize(Column column)
			{
				return ((IColumn)column).GetSize(this);
			}

			// Token: 0x06000598 RID: 1432 RVA: 0x0001F280 File Offset: 0x0001D480
			object ITWIR.GetColumnValue(Column column)
			{
				return ((IColumn)column).GetValue(this);
			}

			// Token: 0x06000599 RID: 1433 RVA: 0x0001F296 File Offset: 0x0001D496
			int ITWIR.GetPhysicalColumnSize(PhysicalColumn column)
			{
				return 0;
			}

			// Token: 0x0600059A RID: 1434 RVA: 0x0001F299 File Offset: 0x0001D499
			object ITWIR.GetPhysicalColumnValue(PhysicalColumn column)
			{
				return this.GetPhysicalColumnValue(this.propbag.CurrentOperationContext, column);
			}

			// Token: 0x0600059B RID: 1435 RVA: 0x0001F2AD File Offset: 0x0001D4AD
			int ITWIR.GetPropertyColumnSize(PropertyColumn column)
			{
				return 0;
			}

			// Token: 0x0600059C RID: 1436 RVA: 0x0001F2B0 File Offset: 0x0001D4B0
			object ITWIR.GetPropertyColumnValue(PropertyColumn column)
			{
				return this.GetPropertyValue(this.propbag.CurrentOperationContext, column.StorePropTag);
			}

			// Token: 0x040003F4 RID: 1012
			private PropertyBag propbag;
		}
	}
}
