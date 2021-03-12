using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000228 RID: 552
	internal class XsoMessageClassProperty : XsoStringProperty
	{
		// Token: 0x060014CE RID: 5326 RVA: 0x00079308 File Offset: 0x00077508
		public XsoMessageClassProperty(PropertyType type) : base(null, type, new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass
		})
		{
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x00079330 File Offset: 0x00077530
		public override string StringData
		{
			get
			{
				if (this.IsItemDelegated())
				{
					return "IPM.Note";
				}
				string text = base.XsoItem.GetValueOrDefault<string>(StoreObjectSchema.ItemClass);
				if (BodyConversionUtilities.IsMessageRestrictedAndDecoded((Item)base.XsoItem) && text != null && text.StartsWith("IPM.Note.RPMSG.Microsoft.Voicemail", StringComparison.OrdinalIgnoreCase))
				{
					text = text.ToLower();
					text = text.Replace(".rpmsg.", ".");
				}
				return text;
			}
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00079398 File Offset: 0x00077598
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			throw new ConversionException("Message-class is a read-only property and should not be set!");
		}
	}
}
