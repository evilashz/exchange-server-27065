using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000102 RID: 258
	internal sealed class CalendarPermissionRenderer : PermissionRendererBase<CalendarFolderPermission, CalendarPermissionSetType, CalendarPermissionType>
	{
		// Token: 0x06000757 RID: 1879 RVA: 0x000242EA File Offset: 0x000224EA
		internal CalendarPermissionRenderer(CalendarFolder calendarFolder)
		{
			this.calendarPermissionSet = CalendarPermissionRenderer.GetPermissionSet(calendarFolder);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000242FE File Offset: 0x000224FE
		protected override CalendarFolderPermission GetDefaultPermission()
		{
			return this.calendarPermissionSet.DefaultPermission;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0002430B File Offset: 0x0002250B
		protected override CalendarFolderPermission GetAnonymousPermission()
		{
			return (CalendarFolderPermission)this.calendarPermissionSet.AnonymousPermission;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0002431D File Offset: 0x0002251D
		protected override IEnumerator<CalendarFolderPermission> GetPermissionEnumerator()
		{
			return this.calendarPermissionSet.GetEnumerator();
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0002432A File Offset: 0x0002252A
		protected override string GetPermissionsArrayElementName()
		{
			return "CalendarPermissions";
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00024331 File Offset: 0x00022531
		protected override string GetPermissionElementName()
		{
			return "CalendarPermission";
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00024338 File Offset: 0x00022538
		private CalendarPermissionLevelType CreatePermissionLevel(CalendarFolderPermission permission)
		{
			switch (permission.PermissionLevel)
			{
			case PermissionLevel.None:
				return CalendarPermissionLevelType.None;
			case PermissionLevel.Owner:
				return CalendarPermissionLevelType.Owner;
			case PermissionLevel.PublishingEditor:
				return CalendarPermissionLevelType.PublishingEditor;
			case PermissionLevel.Editor:
				return CalendarPermissionLevelType.Editor;
			case PermissionLevel.PublishingAuthor:
				return CalendarPermissionLevelType.PublishingAuthor;
			case PermissionLevel.Author:
				return CalendarPermissionLevelType.Author;
			case PermissionLevel.NonEditingAuthor:
				return CalendarPermissionLevelType.NoneditingAuthor;
			case PermissionLevel.Reviewer:
				return CalendarPermissionLevelType.Reviewer;
			case PermissionLevel.Contributor:
				return CalendarPermissionLevelType.Contributor;
			default:
				if (!permission.CanCreateItems && !permission.CanCreateSubfolders && !permission.CanReadItems && permission.DeleteItems == ItemPermissionScope.None && permission.EditItems == ItemPermissionScope.None && !permission.IsFolderOwner)
				{
					if (permission.FreeBusyAccess == FreeBusyAccess.Basic)
					{
						return CalendarPermissionLevelType.FreeBusyTimeOnly;
					}
					if (permission.FreeBusyAccess == FreeBusyAccess.Details)
					{
						return CalendarPermissionLevelType.FreeBusyTimeAndSubjectAndLocation;
					}
				}
				return CalendarPermissionLevelType.Custom;
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000243D6 File Offset: 0x000225D6
		private CalendarPermissionReadAccess CreateCalendarPermissionReadAccessType(bool canReadItems, FreeBusyAccess freeBusyAccess)
		{
			if (canReadItems)
			{
				return CalendarPermissionReadAccess.FullDetails;
			}
			if (FreeBusyAccess.Details == freeBusyAccess)
			{
				return CalendarPermissionReadAccess.TimeAndSubjectAndLocation;
			}
			if (FreeBusyAccess.Basic == freeBusyAccess)
			{
				return CalendarPermissionReadAccess.TimeOnly;
			}
			return CalendarPermissionReadAccess.None;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x000243EA File Offset: 0x000225EA
		protected override void RenderByTypePermissionDetails(CalendarPermissionType permissionType, CalendarFolderPermission permission)
		{
			permissionType.ReadItems = new CalendarPermissionReadAccess?(this.CreateCalendarPermissionReadAccessType(permission.CanReadItems, permission.FreeBusyAccess));
			permissionType.CalendarPermissionLevel = this.CreatePermissionLevel(permission);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00024418 File Offset: 0x00022618
		internal static CalendarFolderPermissionSet GetPermissionSet(CalendarFolder folder)
		{
			CalendarFolderPermissionSet permissionSet;
			try
			{
				FaultInjection.GenerateFault((FaultInjection.LIDs)3024497981U);
				permissionSet = folder.GetPermissionSet();
			}
			catch (StoragePermanentException ex)
			{
				if (ex.InnerException is MapiExceptionAmbiguousAlias)
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceError<StoragePermanentException>(0L, "Error occurred when fetching permission set for calendar folder. Exception '{0}'.", ex);
					throw new ObjectCorruptException(ex, false);
				}
				throw;
			}
			return permissionSet;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00024474 File Offset: 0x00022674
		protected override CalendarPermissionType CreatePermissionElement()
		{
			return new CalendarPermissionType();
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0002447B File Offset: 0x0002267B
		protected override void SetPermissionsOnSerializationObject(CalendarPermissionSetType serviceProperty, List<CalendarPermissionType> renderedPermissions)
		{
			serviceProperty.CalendarPermissions = renderedPermissions.ToArray();
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00024489 File Offset: 0x00022689
		protected override void SetUnknownEntriesOnSerializationObject(CalendarPermissionSetType serviceProperty, string[] entries)
		{
			serviceProperty.UnknownEntries = entries;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00024492 File Offset: 0x00022692
		protected override CalendarPermissionSetType CreatePermissionSetElement()
		{
			return new CalendarPermissionSetType();
		}

		// Token: 0x040006EE RID: 1774
		private CalendarFolderPermissionSet calendarPermissionSet;
	}
}
