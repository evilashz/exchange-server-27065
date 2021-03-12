using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000198 RID: 408
	internal sealed class CalendarFolderSchema : Schema
	{
		// Token: 0x06000B71 RID: 2929 RVA: 0x00036A44 File Offset: 0x00034C44
		static CalendarFolderSchema()
		{
			XmlElementInformation[] xmlElements = new XmlElementInformation[]
			{
				CalendarFolderSchema.SharingEffectiveRights,
				CalendarFolderSchema.PermissionSet
			};
			CalendarFolderSchema.schema = new CalendarFolderSchema(xmlElements);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00036AC5 File Offset: 0x00034CC5
		private CalendarFolderSchema(XmlElementInformation[] xmlElements) : base(xmlElements)
		{
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00036ACE File Offset: 0x00034CCE
		public static Schema GetSchema()
		{
			return CalendarFolderSchema.schema;
		}

		// Token: 0x04000825 RID: 2085
		private static Schema schema;

		// Token: 0x04000826 RID: 2086
		public static readonly PropertyInformation SharingEffectiveRights = new PropertyInformation("SharingEffectiveRights", ExchangeVersion.Exchange2010, null, new PropertyUri(PropertyUriEnum.FolderSharingEffectiveRights), new PropertyCommand.CreatePropertyCommand(SharingEffectiveRightsProperty.CreateCommand));

		// Token: 0x04000827 RID: 2087
		public static readonly PropertyInformation PermissionSet = new PropertyInformation("PermissionSet", ExchangeVersion.Exchange2007SP1, null, new PropertyUri(PropertyUriEnum.PermissionSet), new PropertyCommand.CreatePropertyCommand(CalendarPermissionSetProperty.CreateCommand));
	}
}
