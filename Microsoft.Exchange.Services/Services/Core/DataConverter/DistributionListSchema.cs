using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001A2 RID: 418
	internal sealed class DistributionListSchema : Schema
	{
		// Token: 0x06000B93 RID: 2963 RVA: 0x000392AC File Offset: 0x000374AC
		static DistributionListSchema()
		{
			XmlElementInformation[] xmlElements = new XmlElementInformation[]
			{
				DistributionListSchema.DisplayName,
				DistributionListSchema.FileAs,
				DistributionListSchema.Members,
				DistributionListSchema.MembersMember
			};
			DistributionListSchema.schema = new DistributionListSchema(xmlElements);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x000393C8 File Offset: 0x000375C8
		private DistributionListSchema(XmlElementInformation[] xmlElements) : base(xmlElements)
		{
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x000393D1 File Offset: 0x000375D1
		public static Schema GetSchema()
		{
			return DistributionListSchema.schema;
		}

		// Token: 0x040008CE RID: 2254
		public static readonly PropertyInformation DisplayName = new PropertyInformation(PropertyUriEnum.DisplayName, ExchangeVersion.Exchange2007, StoreObjectSchema.DisplayName, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x040008CF RID: 2255
		public static readonly PropertyInformation FileAs = new PropertyInformation(PropertyUriEnum.FileAs, ExchangeVersion.Exchange2007, ContactBaseSchema.FileAs, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008D0 RID: 2256
		public static readonly PropertyInformation Members = new PropertyInformation(PropertyUriEnum.Members, ExchangeVersion.Exchange2010, DistributionListSchema.Members, new PropertyCommand.CreatePropertyCommand(MembersProperty.CreateMembersCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsAppendUpdateCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x040008D1 RID: 2257
		public static readonly PropertyInformation MembersMember = new PropertyInformation(PropertyUriEnum.Members.ToString(), ServiceXml.GetFullyQualifiedName(DictionaryUriEnum.DistributionListMembersMember.ToString()), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2010, new PropertyDefinition[]
		{
			DistributionListSchema.Members
		}, new DictionaryPropertyUriBase(DictionaryUriEnum.DistributionListMembersMember), new PropertyCommand.CreatePropertyCommand(MemberProperty.CreateMembersMemberCommand), true, PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsAppendUpdateCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x040008D2 RID: 2258
		private static Schema schema;
	}
}
