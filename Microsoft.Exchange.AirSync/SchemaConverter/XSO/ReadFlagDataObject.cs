using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x020001F4 RID: 500
	internal class ReadFlagDataObject : PropertyBase, IPropertyContainer, IProperty
	{
		// Token: 0x0600139F RID: 5023 RVA: 0x00070F40 File Offset: 0x0006F140
		public ReadFlagDataObject(IList<IProperty> propertyFromSchemaLinkId, SyncOperation syncOp)
		{
			if (syncOp == null)
			{
				throw new ArgumentNullException("syncOp");
			}
			if (propertyFromSchemaLinkId == null)
			{
				throw new ArgumentNullException("propertyFromSchemaLinkId");
			}
			this.propertyFromSchemaLinkId = propertyFromSchemaLinkId;
			ReadFlagProperty readFlagProperty = new ReadFlagProperty(syncOp.IsRead);
			this.propertyList = new ReadFlagProperty[]
			{
				readFlagProperty
			};
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x00070F94 File Offset: 0x0006F194
		public IList<IProperty> Children
		{
			get
			{
				return this.propertyList;
			}
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00070F9C File Offset: 0x0006F19C
		public void SetChangedProperties(PropertyDefinition[] changedProperties)
		{
			if (changedProperties == null)
			{
				return;
			}
			if (changedProperties != SyncCollection.ReadFlagChangedOnly)
			{
				throw new ArgumentException("The only acceptable value for changedProperties is SyncCollection.ReadFlagChangedOnly", "changedProperties");
			}
			foreach (IProperty property in this.propertyFromSchemaLinkId)
			{
				XsoProperty xsoProperty = (XsoProperty)property;
				if (xsoProperty.StorePropertyDefinition == changedProperties[0])
				{
					this.propertyList[0].State = PropertyState.Modified;
					this.propertyList[0].SchemaLinkId = xsoProperty.SchemaLinkId;
					break;
				}
			}
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00071034 File Offset: 0x0006F234
		public override void CopyFrom(IProperty srcRootProperty)
		{
			throw new InvalidOperationException("ReadFlagDataObject is read-only, thus CopyFrom is not implemented.");
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00071040 File Offset: 0x0006F240
		public void SetCopyDestination(IPropertyContainer dstPropertyContainer)
		{
		}

		// Token: 0x04000C2C RID: 3116
		private IProperty[] propertyList;

		// Token: 0x04000C2D RID: 3117
		private IList<IProperty> propertyFromSchemaLinkId;
	}
}
