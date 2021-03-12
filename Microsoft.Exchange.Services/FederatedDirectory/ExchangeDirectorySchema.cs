using System;
using System.Collections.Generic;
using Microsoft.Office.Server.Directory;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x02000070 RID: 112
	internal static class ExchangeDirectorySchema
	{
		// Token: 0x06000299 RID: 665 RVA: 0x0000E498 File Offset: 0x0000C698
		public static void Initialize()
		{
			if (!ExchangeDirectorySchema.isInitialized)
			{
				lock (ExchangeDirectorySchema.initializeLock)
				{
					if (!ExchangeDirectorySchema.isInitialized)
					{
						ExchangeDirectorySchema.MembershipRelation.PropertyTypes.Add(ExchangeDirectorySchema.JoinDateProperty.SchemaType.Name, ExchangeDirectorySchema.JoinDateProperty.SchemaType);
						ExchangeDirectorySchema.isInitialized = true;
					}
				}
			}
		}

		// Token: 0x0400057A RID: 1402
		public static readonly Guid AdaptorId = new Guid("05A4FA99-31B4-4868-B71E-7695E7388AC7");

		// Token: 0x0400057B RID: 1403
		public static readonly PropertyTypeBuilder ExchangeDirectoryObjectIdProperty = new PropertyTypeBuilder
		{
			Id = new Guid("3bcc38db-d959-4a23-9cee-be9b91d0b5d5"),
			Name = "ExchangeDirectoryObjectId",
			ParentObjectType = 2,
			MasterAdapter = ExchangeDirectorySchema.AdaptorId,
			Type = typeof(Guid),
			IsIndexed = true,
			IsReadOnly = true,
			DelayLoad = true
		};

		// Token: 0x0400057C RID: 1404
		public static readonly ResourceTypeBuilder GroupPictureUrlResource = new ResourceTypeBuilder
		{
			Id = new Guid("75B833A6-7808-490A-AD19-B4A1391CE31F"),
			Name = "PictureUrl",
			ParentObjectType = 2,
			MasterAdapter = ExchangeDirectorySchema.AdaptorId,
			Type = typeof(string),
			IsReadOnly = true,
			DelayLoad = true
		};

		// Token: 0x0400057D RID: 1405
		public static readonly ResourceTypeBuilder UserPictureUrlResource = new ResourceTypeBuilder
		{
			Id = new Guid("5883BB09-A7AD-48C4-9A98-19458B90B9FC"),
			Name = "PictureUrl",
			ParentObjectType = 1,
			MasterAdapter = ExchangeDirectorySchema.AdaptorId,
			Type = typeof(string),
			IsReadOnly = true,
			DelayLoad = true
		};

		// Token: 0x0400057E RID: 1406
		public static readonly ResourceTypeBuilder InboxUrlResource = new ResourceTypeBuilder
		{
			Id = new Guid("f5ff0df8-2eba-44f7-9920-29e5169b589c"),
			Name = "InboxUrl",
			Type = typeof(string),
			ParentObjectType = 2,
			IsRequired = false,
			MasterAdapter = ExchangeDirectorySchema.AdaptorId,
			DelayLoad = true
		};

		// Token: 0x0400057F RID: 1407
		public static readonly ResourceTypeBuilder CalendarUrlResource = new ResourceTypeBuilder
		{
			Id = new Guid("04cbc616-4a10-4ffe-8160-183892b44ce3"),
			Name = "CalendarUrl",
			Type = typeof(string),
			ParentObjectType = 2,
			IsRequired = false,
			MasterAdapter = ExchangeDirectorySchema.AdaptorId,
			DelayLoad = true
		};

		// Token: 0x04000580 RID: 1408
		public static readonly ResourceTypeBuilder PeopleUrlResource = new ResourceTypeBuilder
		{
			Id = new Guid("b02e0f08-9c4c-4a48-a976-21093b74099d"),
			Name = "PeopleUrl",
			Type = typeof(string),
			ParentObjectType = 2,
			IsRequired = false,
			MasterAdapter = ExchangeDirectorySchema.AdaptorId,
			DelayLoad = true
		};

		// Token: 0x04000581 RID: 1409
		public static readonly RelationTypeBuilder OwnersRelation = new RelationTypeBuilder
		{
			Id = new Guid("cf58be5f-dd0f-4321-982e-72deaccca8a4"),
			Name = "Owners",
			ParentObjectType = 2,
			MasterAdapter = ExchangeDirectorySchema.AdaptorId,
			TargetObjectType = 1,
			IsRequired = true,
			DelayLoad = true
		};

		// Token: 0x04000582 RID: 1410
		public static readonly RelationTypeBuilder MembersRelation = new RelationTypeBuilder
		{
			Id = new Guid("3d420ade-03d2-493c-831b-adebde7b9702"),
			Name = "Members",
			ParentObjectType = 2,
			MasterAdapter = ExchangeDirectorySchema.AdaptorId,
			TargetObjectType = 1,
			IsRequired = false,
			DelayLoad = true
		};

		// Token: 0x04000583 RID: 1411
		public static readonly RelationTypeBuilder MembershipRelation = new RelationTypeBuilder
		{
			Id = new Guid("c8006305-b7f6-45b3-b70f-f849fc95987f"),
			Name = "Membership",
			ParentObjectType = 1,
			MasterAdapter = ExchangeDirectorySchema.AdaptorId,
			TargetObjectType = 2,
			IsRequired = false,
			DelayLoad = true
		};

		// Token: 0x04000584 RID: 1412
		public static readonly PropertyTypeBuilder JoinDateProperty = new PropertyTypeBuilder
		{
			Id = new Guid("C099653E-6167-473F-A0C0-EB79F45D3199"),
			Name = "JoinDate",
			MasterAdapter = ExchangeDirectorySchema.AdaptorId,
			Type = typeof(DateTime),
			IsRequired = true,
			DelayLoad = true
		};

		// Token: 0x04000585 RID: 1413
		public static readonly ICollection<PropertyType> PropertyDefinitions = new List<PropertyType>(new PropertyType[]
		{
			ExchangeDirectorySchema.ExchangeDirectoryObjectIdProperty.SchemaType
		});

		// Token: 0x04000586 RID: 1414
		public static readonly ICollection<ResourceType> ResourceDefinitions = new List<ResourceType>(new ResourceType[]
		{
			ExchangeDirectorySchema.GroupPictureUrlResource.SchemaType,
			ExchangeDirectorySchema.UserPictureUrlResource.SchemaType,
			ExchangeDirectorySchema.InboxUrlResource.SchemaType,
			ExchangeDirectorySchema.CalendarUrlResource.SchemaType,
			ExchangeDirectorySchema.PeopleUrlResource.SchemaType
		});

		// Token: 0x04000587 RID: 1415
		public static readonly ICollection<RelationType> RelationDefinitions = new List<RelationType>(new RelationType[]
		{
			ExchangeDirectorySchema.OwnersRelation.SchemaType,
			ExchangeDirectorySchema.MembersRelation.SchemaType,
			ExchangeDirectorySchema.MembershipRelation.SchemaType
		});

		// Token: 0x04000588 RID: 1416
		private static bool isInitialized;

		// Token: 0x04000589 RID: 1417
		private static object initializeLock = new object();
	}
}
