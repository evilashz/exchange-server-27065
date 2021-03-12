using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200077E RID: 1918
	internal static class ExternalUserHandler
	{
		// Token: 0x06003944 RID: 14660 RVA: 0x000CACE0 File Offset: 0x000C8EE0
		public static Permission GetExternalUserPermissions(string externalId, MailboxSession session, StoreObjectId targetFolderId, string userIdForTracing)
		{
			ExTraceGlobals.ExternalUserTracer.TraceDebug<string>(0L, "ExternalUserHandler.GetExternalUserPermissions: Getting permission for user {0}.", userIdForTracing);
			PermissionSecurityPrincipal securityPrincipal;
			using (ExternalUserCollection externalUsers = session.GetExternalUsers())
			{
				ExternalUser externalUser = externalUsers.FindExternalUser(externalId);
				if (externalUser == null)
				{
					ExTraceGlobals.ExternalUserTracer.TraceDebug<string, MailboxSession>(0L, "ExternalUserHandler.GetExternalUserPermissions: User {0} not found in the external user collection on mailbox {1}.", userIdForTracing, session);
					return null;
				}
				securityPrincipal = new PermissionSecurityPrincipal(externalUser);
			}
			using (Folder folder = Folder.Bind(session, targetFolderId))
			{
				PermissionSet permissionSet = folder.GetPermissionSet();
				Permission entry = permissionSet.GetEntry(securityPrincipal);
				if (entry != null)
				{
					ExTraceGlobals.ExternalUserTracer.TraceDebug<string, MemberRights, MailboxSession>(0L, "ExternalUserHandler.GetExternalUserPermissions: User {0} has member rights {1} on session {2}.", userIdForTracing, entry.MemberRights, session);
					return entry;
				}
			}
			ExTraceGlobals.ExternalUserTracer.TraceDebug<string, MailboxSession>(0L, "ExternalUserHandler.GetExternalUserPermissions: User {0} is known but does not have permissions on mailbox {1}.", userIdForTracing, session);
			return new Permission(securityPrincipal, MemberRights.None);
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x000CADC0 File Offset: 0x000C8FC0
		public static bool HasPermission(Permission permission)
		{
			if (permission.CanReadItems)
			{
				return true;
			}
			if (permission.PermissionLevel == PermissionLevel.Custom)
			{
				CalendarFolderPermission calendarFolderPermission = permission as CalendarFolderPermission;
				if (calendarFolderPermission != null && calendarFolderPermission.FreeBusyAccess != FreeBusyAccess.None)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x000CADF8 File Offset: 0x000C8FF8
		public static ServiceError ValidateItemTypeSupported(StoreObjectId id)
		{
			StoreObjectType objectType = id.ObjectType;
			switch (objectType)
			{
			case StoreObjectType.CalendarFolder:
			case StoreObjectType.ContactsFolder:
				break;
			default:
				switch (objectType)
				{
				case StoreObjectType.CalendarItem:
				case StoreObjectType.CalendarItemOccurrence:
				case StoreObjectType.Contact:
					break;
				default:
					ExTraceGlobals.ExternalUserTracer.TraceError<StoreObjectType>(0L, "ExternalUserHandler.IsItemTypeSupported: Item type of {0} is not supported for external users.", id.ObjectType);
					return new ServiceError((CoreResources.IDs)3579904699U, ResponseCodeType.ErrorAccessDenied, 0, ExchangeVersion.Exchange2007SP1);
				}
				break;
			}
			return null;
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x000CAE68 File Offset: 0x000C9068
		public static ServiceError CheckAndGetResponseShape(Type serviceCommandType, ExternalUserIdAndSession externalIdAndSession, ItemResponseShape requestedResponseShape, out ItemResponseShape allowedResponseShape)
		{
			if (externalIdAndSession == null)
			{
				ExTraceGlobals.ExternalUserTracer.TraceError(0L, "ExternalUserHandler.CheckAndGetResponseShape: ExternalUserIdAndSession is null.");
				throw new ServiceAccessDeniedException();
			}
			StoreObjectId asStoreObjectId = externalIdAndSession.GetAsStoreObjectId();
			ServiceError serviceError = ExternalUserHandler.ValidateItemTypeSupported(asStoreObjectId);
			if (serviceError == null)
			{
				allowedResponseShape = ExternalUserResponseShapeOverride.GetAllowedResponseShape(serviceCommandType, externalIdAndSession, requestedResponseShape);
				if (allowedResponseShape == null)
				{
					ExTraceGlobals.GetItemCallTracer.TraceError(0L, "ExternalUserHandler.CheckAndGetResponseShape: Unable to get the allowed shape for the external user.");
					serviceError = new ServiceError((CoreResources.IDs)3579904699U, ResponseCodeType.ErrorAccessDenied, 0, ExchangeVersion.Exchange2007SP1);
					allowedResponseShape = new ItemResponseShape();
					allowedResponseShape.BaseShape = ShapeEnum.IdOnly;
					allowedResponseShape.AdditionalProperties = null;
				}
				else
				{
					ExTraceGlobals.GetItemCallTracer.TraceDebug(0L, "ExternalUserHandler.CheckAndGetResponseShape: Adjusting the response with the allowed response.");
				}
			}
			else
			{
				ExTraceGlobals.GetItemCallTracer.TraceDebug(0L, "ExternalUserHandler.CheckAndGetResponseShape: Item type is not supported, returning a warning with IdOnly.");
				allowedResponseShape = new ItemResponseShape();
				allowedResponseShape.BaseShape = ShapeEnum.IdOnly;
				allowedResponseShape.AdditionalProperties = null;
			}
			return serviceError;
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x000CAF2C File Offset: 0x000C912C
		public static FolderResponseShape FilterFolderResponseShape(ExternalUserIdAndSession externalIdAndSession, FolderResponseShape requestedResponseShape, out ServiceError serviceError)
		{
			if (externalIdAndSession == null)
			{
				ExTraceGlobals.ExternalUserTracer.TraceError(0L, "ExternalUserHandler.FilterFolderResponseShape: ExternalUserIdAndSession is null.");
				throw new ServiceAccessDeniedException();
			}
			FolderResponseShape folderResponseShape = new FolderResponseShape();
			folderResponseShape.BaseShape = ShapeEnum.IdOnly;
			serviceError = null;
			StoreObjectId asStoreObjectId = externalIdAndSession.GetAsStoreObjectId();
			ExternalUserResponseShape externalUserResponseShape = ExternalUserResponseShape.Create(asStoreObjectId, externalIdAndSession);
			folderResponseShape.AdditionalProperties = externalUserResponseShape.GetAllowedProperties(requestedResponseShape);
			if (requestedResponseShape.AdditionalProperties != null && requestedResponseShape.AdditionalProperties.Length > 0 && (folderResponseShape.AdditionalProperties == null || folderResponseShape.AdditionalProperties.Length == 0))
			{
				ExTraceGlobals.GetFolderCallTracer.TraceDebug(0L, "ExternalUserHandler.FilterFolderResponseShape: Folder type is not supported, returning a warning with IdOnly.");
				serviceError = new ServiceError((CoreResources.IDs)3579904699U, ResponseCodeType.ErrorAccessDenied, 0, ExchangeVersion.Exchange2010);
			}
			return folderResponseShape;
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x000CAFD0 File Offset: 0x000C91D0
		public static List<PropertyPath> GetAllowedProperties(ExternalUserIdAndSession externalIdAndSession, Item xsoItem)
		{
			if (xsoItem == null || externalIdAndSession == null)
			{
				return null;
			}
			if (xsoItem.Sensitivity == Sensitivity.Normal)
			{
				return null;
			}
			bool flag = false;
			Permission permissionGranted = externalIdAndSession.PermissionGranted;
			if (permissionGranted.CanReadItems)
			{
				flag = true;
			}
			if (permissionGranted.PermissionLevel == PermissionLevel.Custom)
			{
				CalendarFolderPermission calendarFolderPermission = permissionGranted as CalendarFolderPermission;
				if (calendarFolderPermission.FreeBusyAccess == FreeBusyAccess.Details)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return null;
			}
			if (xsoItem is CalendarItemBase)
			{
				return ExternalUserCalendarResponseShape.CalendarPropertiesPrivateItem;
			}
			return ExternalUserContactResponseShape.PrivateItemProperties;
		}
	}
}
