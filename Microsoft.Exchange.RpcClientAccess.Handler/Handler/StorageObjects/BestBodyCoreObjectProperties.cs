using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects
{
	// Token: 0x02000093 RID: 147
	internal class BestBodyCoreObjectProperties : CoreObjectProperties
	{
		// Token: 0x060005E8 RID: 1512 RVA: 0x00028CD8 File Offset: 0x00026ED8
		public BestBodyCoreObjectProperties(ICoreItem coreItem, ICorePropertyBag corePropertyBag, Encoding string8Encoding, Func<BodyReadConfiguration, Stream> getBodyConversionStreamCallback) : base(corePropertyBag)
		{
			Util.ThrowOnNullArgument(coreItem, "coreItem");
			Util.ThrowOnNullArgument(string8Encoding, "string8Encoding");
			Util.ThrowOnNullArgument(getBodyConversionStreamCallback, "getBodyConversionStreamCallback");
			this.coreItem = coreItem;
			this.bodyHelper = new BodyHelper(coreItem, corePropertyBag, string8Encoding, getBodyConversionStreamCallback);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00028D25 File Offset: 0x00026F25
		public BestBodyCoreObjectProperties(ICoreItem coreItem, ICorePropertyBag corePropertyBag, BodyHelper bodyHelper) : base(corePropertyBag)
		{
			Util.ThrowOnNullArgument(coreItem, "coreItem");
			Util.ThrowOnNullArgument(bodyHelper, "bodyHelper");
			this.coreItem = coreItem;
			this.bodyHelper = bodyHelper;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00028D52 File Offset: 0x00026F52
		public BodyHelper BodyHelper
		{
			get
			{
				return this.bodyHelper;
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00028D5A File Offset: 0x00026F5A
		public override void SetProperty(PropertyDefinition propertyDefinition, object value)
		{
			this.bodyHelper.SetProperty(propertyDefinition, value);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00028D69 File Offset: 0x00026F69
		public override void DeleteProperty(PropertyDefinition propertyDefinition)
		{
			this.bodyHelper.DeleteProperty(propertyDefinition);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00028D77 File Offset: 0x00026F77
		public override Stream OpenStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode)
		{
			return this.bodyHelper.OpenStream(propertyDefinition, openMode);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00028D86 File Offset: 0x00026F86
		public override object TryGetProperty(PropertyDefinition propertyDefinition)
		{
			return this.bodyHelper.TryGetProperty(propertyDefinition);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00028D94 File Offset: 0x00026F94
		public void ResetBody()
		{
			this.bodyHelper.PrepareForSave();
			this.bodyHelper.UpdateBodyPreviewIfNeeded(this.coreItem.Body);
			this.bodyHelper.Reset();
		}

		// Token: 0x04000275 RID: 629
		private readonly ICoreItem coreItem;

		// Token: 0x04000276 RID: 630
		private readonly BodyHelper bodyHelper;
	}
}
