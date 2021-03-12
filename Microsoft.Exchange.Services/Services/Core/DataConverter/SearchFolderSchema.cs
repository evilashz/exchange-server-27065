using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001C4 RID: 452
	internal sealed class SearchFolderSchema : Schema
	{
		// Token: 0x06000C5A RID: 3162 RVA: 0x0004047C File Offset: 0x0003E67C
		static SearchFolderSchema()
		{
			XmlElementInformation[] xmlElements = new XmlElementInformation[]
			{
				SearchFolderSchema.UnreadCount,
				SearchFolderSchema.SearchParameters
			};
			SearchFolderSchema.schema = new SearchFolderSchema(xmlElements);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x000404D4 File Offset: 0x0003E6D4
		private SearchFolderSchema(XmlElementInformation[] xmlElements) : base(xmlElements)
		{
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x000404DD File Offset: 0x0003E6DD
		public static Schema GetSchema()
		{
			return SearchFolderSchema.schema;
		}

		// Token: 0x04000A1B RID: 2587
		private static Schema schema;

		// Token: 0x04000A1C RID: 2588
		public static readonly PropertyInformation UnreadCount = FolderSchema.UnreadCount;

		// Token: 0x04000A1D RID: 2589
		public static readonly PropertyInformation SearchParameters = new PropertyInformation(PropertyUriEnum.SearchParameters, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(SearchParametersProperty.CreateCommand));
	}
}
