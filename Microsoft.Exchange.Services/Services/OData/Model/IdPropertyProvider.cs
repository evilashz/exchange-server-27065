using System;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E90 RID: 3728
	internal class IdPropertyProvider : AggregatedPropertyProvider
	{
		// Token: 0x06006127 RID: 24871 RVA: 0x0012ED33 File Offset: 0x0012CF33
		public override PropertyProvider SelectProvider(EntitySchema schema)
		{
			if (schema is ItemSchema)
			{
				return IdPropertyProvider.ItemIdProvider;
			}
			if (schema is FolderSchema || schema is ContactFolderSchema)
			{
				return IdPropertyProvider.FolderIdProvider;
			}
			if (schema is AttachmentSchema)
			{
				return IdPropertyProvider.AttachmentIdProvider;
			}
			return null;
		}

		// Token: 0x06006129 RID: 24873 RVA: 0x0012EDD8 File Offset: 0x0012CFD8
		// Note: this type is marked as 'beforefieldinit'.
		static IdPropertyProvider()
		{
			SimpleEwsPropertyProvider simpleEwsPropertyProvider = new SimpleEwsPropertyProvider(ItemSchema.ItemId);
			simpleEwsPropertyProvider.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				e.Id = EwsIdConverter.EwsIdToODataId((s[sp] as ItemId).Id);
			};
			simpleEwsPropertyProvider.QueryConstantBuilder = ((object o) => EwsIdConverter.ODataIdToEwsId((string)o));
			IdPropertyProvider.ItemIdProvider = simpleEwsPropertyProvider;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider2 = new SimpleEwsPropertyProvider(BaseFolderSchema.FolderId);
			simpleEwsPropertyProvider2.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				e.Id = EwsIdConverter.EwsIdToODataId((s[sp] as FolderId).Id);
			};
			simpleEwsPropertyProvider2.QueryConstantBuilder = ((object o) => EwsIdConverter.ODataIdToEwsId((string)o));
			IdPropertyProvider.FolderIdProvider = simpleEwsPropertyProvider2;
			GenericPropertyProvider<AttachmentType> genericPropertyProvider = new GenericPropertyProvider<AttachmentType>();
			genericPropertyProvider.Getter = delegate(Entity e, PropertyDefinition ep, AttachmentType a)
			{
				e.Id = EwsIdConverter.EwsIdToODataId(a.AttachmentId.Id);
			};
			IdPropertyProvider.AttachmentIdProvider = genericPropertyProvider;
		}

		// Token: 0x0400349C RID: 13468
		private static readonly SimpleEwsPropertyProvider ItemIdProvider;

		// Token: 0x0400349D RID: 13469
		private static readonly SimpleEwsPropertyProvider FolderIdProvider;

		// Token: 0x0400349E RID: 13470
		private static readonly GenericPropertyProvider<AttachmentType> AttachmentIdProvider;
	}
}
