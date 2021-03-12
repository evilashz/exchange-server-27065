using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200019E RID: 414
	internal sealed class ContactsFolderSchema : Schema
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x00037EA8 File Offset: 0x000360A8
		static ContactsFolderSchema()
		{
			XmlElementInformation[] xmlElements = new XmlElementInformation[]
			{
				ContactsFolderSchema.SharingEffectiveRights,
				FolderSchema.PermissionSet
			};
			ContactsFolderSchema.schema = new ContactsFolderSchema(xmlElements);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00037F01 File Offset: 0x00036101
		private ContactsFolderSchema(XmlElementInformation[] xmlElements) : base(xmlElements)
		{
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00037F0A File Offset: 0x0003610A
		public static Schema GetSchema()
		{
			return ContactsFolderSchema.schema;
		}

		// Token: 0x04000874 RID: 2164
		private static Schema schema;

		// Token: 0x04000875 RID: 2165
		public static readonly PropertyInformation SharingEffectiveRights = new PropertyInformation("SharingEffectiveRights", ExchangeVersion.Exchange2010, null, new PropertyUri(PropertyUriEnum.FolderSharingEffectiveRights), new PropertyCommand.CreatePropertyCommand(SharingEffectiveRightsProperty.CreateCommand));
	}
}
