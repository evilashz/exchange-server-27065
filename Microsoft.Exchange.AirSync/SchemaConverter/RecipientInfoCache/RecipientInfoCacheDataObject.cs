using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.RecipientInfoCache
{
	// Token: 0x020001E9 RID: 489
	internal class RecipientInfoCacheDataObject : RecipientInfoCacheProperty, IPropertyContainer, IProperty, IDataObjectGeneratorContainer
	{
		// Token: 0x0600137E RID: 4990 RVA: 0x00070669 File Offset: 0x0006E869
		public RecipientInfoCacheDataObject(IList<IProperty> propertyFromSchemaLinkId)
		{
			base.State = PropertyState.Modified;
			this.propertyFromSchemaLinkId = propertyFromSchemaLinkId;
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x0007067F File Offset: 0x0006E87F
		public IList<IProperty> Children
		{
			get
			{
				return this.propertyFromSchemaLinkId;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x00070687 File Offset: 0x0006E887
		// (set) Token: 0x06001381 RID: 4993 RVA: 0x0007068F File Offset: 0x0006E88F
		public IDataObjectGenerator SchemaState
		{
			get
			{
				return this.schemaState;
			}
			set
			{
				this.schemaState = value;
			}
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00070698 File Offset: 0x0006E898
		public override void Bind(RecipientInfoCacheEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("Entry is null!");
			}
			foreach (IProperty property in this.propertyFromSchemaLinkId)
			{
				RecipientInfoCacheProperty recipientInfoCacheProperty = (RecipientInfoCacheProperty)property;
				recipientInfoCacheProperty.Bind(entry);
			}
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x000706F8 File Offset: 0x0006E8F8
		public void SetCopyDestination(IPropertyContainer dstPropertyContainer)
		{
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x000706FC File Offset: 0x0006E8FC
		public override void CopyFrom(IProperty srcRootProperty)
		{
			IPropertyContainer propertyContainer = srcRootProperty as IPropertyContainer;
			if (propertyContainer == null)
			{
				throw new ArgumentNullException("srcPropertyContainer");
			}
			propertyContainer.SetCopyDestination(this);
			foreach (IProperty property in propertyContainer.Children)
			{
				RecipientInfoCacheProperty recipientInfoCacheProperty = (RecipientInfoCacheProperty)this.propertyFromSchemaLinkId[property.SchemaLinkId];
				recipientInfoCacheProperty.CopyFrom(property);
			}
		}

		// Token: 0x04000C02 RID: 3074
		private IList<IProperty> propertyFromSchemaLinkId;

		// Token: 0x04000C03 RID: 3075
		private IDataObjectGenerator schemaState;
	}
}
