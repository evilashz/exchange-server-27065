using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001AC RID: 428
	internal sealed class FolderSchema : Schema
	{
		// Token: 0x06000BB1 RID: 2993 RVA: 0x0003A270 File Offset: 0x00038470
		static FolderSchema()
		{
			XmlElementInformation[] xmlElements = new XmlElementInformation[]
			{
				FolderSchema.PermissionSet,
				FolderSchema.UnreadCount
			};
			FolderSchema.schema = new FolderSchema(xmlElements);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0003A2FC File Offset: 0x000384FC
		private FolderSchema(XmlElementInformation[] xmlElements) : base(xmlElements)
		{
			IList<PropertyInformation> propertyInformationListByShapeEnum = base.GetPropertyInformationListByShapeEnum(ShapeEnum.AllProperties);
			propertyInformationListByShapeEnum.Remove(FolderSchema.PermissionSet);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0003A324 File Offset: 0x00038524
		public static Schema GetSchema()
		{
			return FolderSchema.schema;
		}

		// Token: 0x040008FB RID: 2299
		private static Schema schema;

		// Token: 0x040008FC RID: 2300
		public static readonly PropertyInformation UnreadCount = new PropertyInformation("UnreadCount", ExchangeVersion.Exchange2007, FolderSchema.UnreadCount, new PropertyUri(PropertyUriEnum.UnreadCount), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008FD RID: 2301
		public static readonly PropertyInformation PermissionSet = new PropertyInformation("PermissionSet", ExchangeVersion.Exchange2007SP1, null, new PropertyUri(PropertyUriEnum.PermissionSet), new PropertyCommand.CreatePropertyCommand(PermissionSetProperty.CreateCommand));
	}
}
