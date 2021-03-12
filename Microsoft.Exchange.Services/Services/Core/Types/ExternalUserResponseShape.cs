using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000779 RID: 1913
	internal abstract class ExternalUserResponseShape
	{
		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x0600392C RID: 14636
		protected abstract List<PropertyPath> PropertiesAllowedForReadAccess { get; }

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x0600392D RID: 14637 RVA: 0x000CA33B File Offset: 0x000C853B
		// (set) Token: 0x0600392E RID: 14638 RVA: 0x000CA343 File Offset: 0x000C8543
		protected Permission Permissions { get; set; }

		// Token: 0x0600392F RID: 14639 RVA: 0x000CA34C File Offset: 0x000C854C
		protected static PropertyPath[] GetAllowedProperties(ResponseShape requestedShape, List<PropertyPath> allowedPropertyList)
		{
			if (allowedPropertyList == null)
			{
				return null;
			}
			if (requestedShape.BaseShape != ShapeEnum.IdOnly)
			{
				return allowedPropertyList.ToArray();
			}
			if (requestedShape.AdditionalProperties == null)
			{
				return null;
			}
			List<PropertyPath> list = new List<PropertyPath>();
			foreach (PropertyPath item in requestedShape.AdditionalProperties)
			{
				if (allowedPropertyList.Contains(item))
				{
					list.Add(item);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000CA3AC File Offset: 0x000C85AC
		protected virtual PropertyPath[] GetPropertiesForCustomPermissions(ItemResponseShape requestedResponseShape)
		{
			return null;
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000CA3B0 File Offset: 0x000C85B0
		public static ExternalUserResponseShape Create(StoreObjectId storeObjectId, ExternalUserIdAndSession externalUserIdAndSession)
		{
			StoreObjectType objectType = storeObjectId.ObjectType;
			switch (objectType)
			{
			case StoreObjectType.CalendarFolder:
				ExTraceGlobals.ExternalUserTracer.TraceDebug(0L, "ExternalUserResponseShape.Create: Defining response shape for calendar folder.");
				return new ExternalUserCalendarFolderResponseShape();
			case StoreObjectType.ContactsFolder:
				ExTraceGlobals.ExternalUserTracer.TraceDebug(0L, "ExternalUserResponseShape.Create: Defining response shape for contacts folder.");
				return new ExternalUserContactsFolderResponseShape();
			default:
				switch (objectType)
				{
				case StoreObjectType.CalendarItem:
				case StoreObjectType.CalendarItemOccurrence:
					ExTraceGlobals.ExternalUserTracer.TraceDebug(0L, "ExternalUserResponseShape.Create: Defining response shape for calendar item.");
					return new ExternalUserCalendarResponseShape(externalUserIdAndSession.PermissionGranted);
				case StoreObjectType.Contact:
					ExTraceGlobals.ExternalUserTracer.TraceDebug(0L, "ExternalUserResponseShape.Create: Defining response shape for contact item.");
					return new ExternalUserContactResponseShape(externalUserIdAndSession.PermissionGranted);
				default:
					ExTraceGlobals.ExternalUserTracer.TraceDebug(0L, "ExternalUserResponseShape.Create: Defining response shape for unknown item.");
					return new ExternalUserUnknownResponseShape();
				}
				break;
			}
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000CA46C File Offset: 0x000C866C
		public ItemResponseShape GetExternalResponseShape(ItemResponseShape requestedResponseShape)
		{
			ItemResponseShape itemResponseShape = new ItemResponseShape();
			itemResponseShape.BaseShape = ShapeEnum.IdOnly;
			if (requestedResponseShape.BaseShape == ShapeEnum.IdOnly && requestedResponseShape.AdditionalProperties == null)
			{
				return itemResponseShape;
			}
			itemResponseShape.BodyType = requestedResponseShape.BodyType;
			itemResponseShape.UniqueBodyType = requestedResponseShape.UniqueBodyType;
			itemResponseShape.NormalizedBodyType = requestedResponseShape.NormalizedBodyType;
			if (this.Permissions != null)
			{
				if (this.Permissions.CanReadItems)
				{
					ExTraceGlobals.ExternalUserTracer.TraceDebug<ExternalUserResponseShape>((long)this.GetHashCode(), "{0}: Overriding shape for Read permissions.", this);
					itemResponseShape.AdditionalProperties = ExternalUserResponseShape.GetAllowedProperties(requestedResponseShape, this.PropertiesAllowedForReadAccess);
				}
				else
				{
					if (this.Permissions.PermissionLevel != PermissionLevel.Custom)
					{
						return null;
					}
					ExTraceGlobals.ExternalUserTracer.TraceDebug<ExternalUserResponseShape>((long)this.GetHashCode(), "{0}: Overriding shape for Custom permissions.", this);
					itemResponseShape.AdditionalProperties = this.GetPropertiesForCustomPermissions(requestedResponseShape);
				}
			}
			return itemResponseShape;
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x000CA534 File Offset: 0x000C8734
		public PropertyPath[] GetAllowedProperties(FolderResponseShape requestedResponseShape)
		{
			return ExternalUserResponseShape.GetAllowedProperties(requestedResponseShape, this.PropertiesAllowedForReadAccess);
		}
	}
}
