using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020008FA RID: 2298
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class BuddyListUtilities
	{
		// Token: 0x06003FB4 RID: 16308 RVA: 0x000DC3C4 File Offset: 0x000DA5C4
		internal static List<IStorePropertyBag> FetchAllPropertyBags(this IQueryResult queryResult)
		{
			if (queryResult == null)
			{
				throw new ArgumentNullException("queryResult");
			}
			List<IStorePropertyBag> list = new List<IStorePropertyBag>();
			IStorePropertyBag[] propertyBags;
			do
			{
				propertyBags = queryResult.GetPropertyBags(10000);
				list.AddRange(propertyBags);
			}
			while (propertyBags.Length > 0);
			return list;
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x000DC400 File Offset: 0x000DA600
		internal static T TryGetValueOrDefault<T>(this IStorePropertyBag propertyBag, PropertyDefinition propertyDefinition, T defaultValue)
		{
			object obj = propertyBag.TryGetProperty(propertyDefinition);
			if (obj == null || obj is PropertyError)
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x000DC428 File Offset: 0x000DA628
		internal static StoreId GetSubFolderIdByClass(Folder folder, string folderClassName)
		{
			StoreId result = null;
			using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, new TextFilter(StoreObjectSchema.ContainerClass, folderClassName, MatchOptions.FullString, MatchFlags.Default), null, new PropertyDefinition[]
			{
				FolderSchema.Id
			}))
			{
				IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(1);
				if (propertyBags.Length > 0)
				{
					result = (StoreId)propertyBags[0].TryGetProperty(FolderSchema.Id);
				}
			}
			return result;
		}
	}
}
