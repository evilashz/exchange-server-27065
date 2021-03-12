using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropertyBlob;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200012A RID: 298
	public class RowPropertyBag : IRowPropertyBag, ISimpleReadOnlyPropertyBag, ISimpleReadOnlyPropertyStorage
	{
		// Token: 0x06000B78 RID: 2936 RVA: 0x00039F14 File Offset: 0x00038114
		public RowPropertyBag(Table table, ObjectPropertySchema propertySchema, StorePropTag mailboxNumberPropTag, IRowAccess rowAccess)
		{
			this.table = table;
			this.propertySchema = propertySchema;
			this.mailboxNumberPropTag = mailboxNumberPropTag;
			this.rowAccess = rowAccess;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00039F39 File Offset: 0x00038139
		protected void SetRowAccess(IRowAccess rowAccess)
		{
			this.rowAccess = rowAccess;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00039F44 File Offset: 0x00038144
		protected virtual ICollection<ushort> GetDefaultPromotedProperties(Context context)
		{
			IMailboxContext mailboxContext = context.PrimaryMailboxContext;
			if (mailboxContext == null || mailboxContext.IsUnifiedMailbox)
			{
				int mailboxNumber = (int)this.GetPropertyValue(context, this.mailboxNumberPropTag);
				mailboxContext = context.GetMailboxContext(mailboxNumber);
			}
			return mailboxContext.GetDefaultPromotedMessagePropertyIds(context);
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00039F87 File Offset: 0x00038187
		public ObjectPropertySchema ObjectPropertySchema
		{
			get
			{
				return this.propertySchema;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00039F8F File Offset: 0x0003818F
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00039F97 File Offset: 0x00038197
		object IRowPropertyBag.GetPropertyValue(Connection connection, StorePropTag propTag)
		{
			return this.GetPropertyValue((Context)connection.OuterExecutionContext, propTag);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00039FAB File Offset: 0x000381AB
		public object GetPropertyValue(Context context, StorePropTag propTag)
		{
			if (this.propertySchema != null)
			{
				return this.propertySchema.GetPropertyValue(context, this, propTag);
			}
			return this.GetBlobPropertyValue(context, propTag);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00039FCC File Offset: 0x000381CC
		public virtual bool TryGetProperty(Context context, ushort propId, out StorePropTag propTag, out object value)
		{
			throw new InvalidOperationException("This method should never be called for a RowPropertyBag");
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00039FD8 File Offset: 0x000381D8
		public virtual void EnumerateProperties(Context context, Func<StorePropTag, object, bool> action, bool showValue)
		{
			if (this.propertySchema != null)
			{
				this.propertySchema.EnumerateProperties(context, this, action, showValue);
				return;
			}
			this.EnumerateBlobProperties(context, action, showValue);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00039FFC File Offset: 0x000381FC
		public object GetBlobPropertyValue(Context context, StorePropTag propTag)
		{
			object obj = null;
			if (this.table.SpecialCols.PropertyBlob != null)
			{
				byte[] blob = (byte[])this.rowAccess.GetPhysicalColumn(this.table.SpecialCols.PropertyBlob);
				PropertyBlob.BlobReader blobReader = new PropertyBlob.BlobReader(blob, 0);
				int index;
				if ((!blobReader.TryGetPropertyValueByTag(propTag.PropTag, out index, out obj) || blobReader.IsPropertyValueReference(index) || blobReader.GetPropertyAdditionalInfo(index) == AdditionalPropertyInfo.Truncated) && this.table.SpecialCols.OffPagePropertyBlob != null)
				{
					if (obj == null)
					{
						ICollection<ushort> defaultPromotedProperties = this.GetDefaultPromotedProperties(context);
						if (defaultPromotedProperties == null || !defaultPromotedProperties.Contains(propTag.PropId))
						{
							obj = ValueReference.Zero;
						}
					}
					if (obj != null)
					{
						if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.DbInteractionDetailTracer.TraceDebug<StorePropTag>(0L, "Property {0} caused us go to off-page blob", propTag);
						}
						if (this.ShouldReadBlobAsStream(this.table.SpecialCols.OffPagePropertyBlob))
						{
							return this.GetBlobPropertyValueFromBlobStream(context, propTag, this.table.SpecialCols.OffPagePropertyBlob);
						}
						blob = (byte[])this.rowAccess.GetPhysicalColumn(this.table.SpecialCols.OffPagePropertyBlob);
						blobReader = new PropertyBlob.BlobReader(blob, 0);
						obj = blobReader.GetPropertyValueByTag(propTag.PropTag);
					}
				}
			}
			return obj;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0003A14D File Offset: 0x0003834D
		public virtual bool TryGetBlobProperty(Context context, ushort propId, out StorePropTag propTag, out object value)
		{
			throw new InvalidOperationException("This method should never be called for a RowPropertyBag");
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0003A159 File Offset: 0x00038359
		object ISimpleReadOnlyPropertyStorage.GetPhysicalColumnValue(Context context, PhysicalColumn column)
		{
			return this.rowAccess.GetPhysicalColumn(column);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0003A168 File Offset: 0x00038368
		public void EnumerateBlobProperties(Context context, Func<StorePropTag, object, bool> action, bool showValue)
		{
			if (this.table.SpecialCols.PropertyBlob != null)
			{
				byte[] blob = (byte[])this.rowAccess.GetPhysicalColumn(this.table.SpecialCols.PropertyBlob);
				PropertyBlob.BlobReader onPageBlobReader = new PropertyBlob.BlobReader(blob, 0);
				for (int i = 0; i < onPageBlobReader.PropertyCount; i++)
				{
					if (!onPageBlobReader.IsPropertyValueNull(i) && !onPageBlobReader.IsPropertyValueReference(i))
					{
						StorePropTag arg = StorePropTag.CreateWithoutInfo(onPageBlobReader.GetPropertyTag(i));
						object arg2 = null;
						if (showValue)
						{
							arg2 = onPageBlobReader.GetPropertyValue(i);
						}
						if (!action(arg, arg2))
						{
							return;
						}
					}
				}
				if (this.table.SpecialCols.OffPagePropertyBlob != null)
				{
					if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, "Property enumeration caused us go to off-page blob");
					}
					if (this.ShouldReadBlobAsStream(this.table.SpecialCols.OffPagePropertyBlob))
					{
						this.EnumerateBlobPropertiesFromBlobStream(context, this.table.SpecialCols.OffPagePropertyBlob, onPageBlobReader, action, showValue);
						return;
					}
					byte[] blob2 = (byte[])this.rowAccess.GetPhysicalColumn(this.table.SpecialCols.OffPagePropertyBlob);
					PropertyBlob.BlobReader blobReader = new PropertyBlob.BlobReader(blob2, 0);
					for (int j = 0; j < blobReader.PropertyCount; j++)
					{
						StorePropTag arg3 = StorePropTag.CreateWithoutInfo(blobReader.GetPropertyTag(j));
						object arg4 = null;
						int index;
						if (!onPageBlobReader.TryFindPropertyById(arg3.PropId, out index) || onPageBlobReader.IsPropertyValueReference(index))
						{
							if (showValue)
							{
								arg4 = blobReader.GetPropertyValue(j);
							}
							if (!action(arg3, arg4))
							{
								return;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0003A303 File Offset: 0x00038503
		private bool ShouldReadBlobAsStream(PhysicalColumn blobColumn)
		{
			return blobColumn.StreamSupport && this.rowAccess is IColumnStreamAccess && ((IColumnStreamAccess)this.rowAccess).GetColumnSize(blobColumn) >= 65536;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0003A338 File Offset: 0x00038538
		private object GetBlobPropertyValueFromBlobStream(Context context, StorePropTag propTag, PhysicalColumn blobColumn)
		{
			IColumnStreamAccess columnStreamAccess = (IColumnStreamAccess)this.rowAccess;
			if (columnStreamAccess.GetColumnSize(blobColumn) == 0)
			{
				return null;
			}
			object propertyValueByTag;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				Stream stream = new PhysicalColumnStream(columnStreamAccess, blobColumn, true);
				disposeGuard.Add<Stream>(stream);
				Stream stream2 = new BufferedStream(stream, 8192);
				disposeGuard.Add<Stream>(stream2);
				PropertyBlob.BlobStreamReader blobStreamReader = new PropertyBlob.BlobStreamReader(stream2);
				propertyValueByTag = blobStreamReader.GetPropertyValueByTag(propTag.PropTag);
			}
			return propertyValueByTag;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0003A3CC File Offset: 0x000385CC
		private void EnumerateBlobPropertiesFromBlobStream(Context context, PhysicalColumn blobColumn, PropertyBlob.BlobReader onPageBlobReader, Func<StorePropTag, object, bool> action, bool showValue)
		{
			IColumnStreamAccess columnStreamAccess = (IColumnStreamAccess)this.rowAccess;
			if (columnStreamAccess.GetColumnSize(blobColumn) == 0)
			{
				return;
			}
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				Stream stream = new PhysicalColumnStream(columnStreamAccess, blobColumn, true);
				disposeGuard.Add<Stream>(stream);
				Stream stream2 = new BufferedStream(stream, 8192);
				disposeGuard.Add<Stream>(stream2);
				PropertyBlob.BlobStreamReader blobStreamReader = new PropertyBlob.BlobStreamReader(stream2);
				foreach (KeyValuePair<uint, object> keyValuePair in blobStreamReader.LoadProperties(showValue, false))
				{
					StorePropTag arg = StorePropTag.CreateWithoutInfo(keyValuePair.Key);
					int index;
					if ((!onPageBlobReader.TryFindPropertyById(arg.PropId, out index) || onPageBlobReader.IsPropertyValueReference(index)) && !action(arg, keyValuePair.Value))
					{
						break;
					}
				}
			}
		}

		// Token: 0x04000664 RID: 1636
		private Table table;

		// Token: 0x04000665 RID: 1637
		private ObjectPropertySchema propertySchema;

		// Token: 0x04000666 RID: 1638
		private IRowAccess rowAccess;

		// Token: 0x04000667 RID: 1639
		private StorePropTag mailboxNumberPropTag;
	}
}
