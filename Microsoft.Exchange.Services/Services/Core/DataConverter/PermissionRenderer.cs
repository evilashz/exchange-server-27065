using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000111 RID: 273
	internal sealed class PermissionRenderer : PermissionRendererBase<Permission, PermissionSetType, Microsoft.Exchange.Services.Core.Types.PermissionType>
	{
		// Token: 0x060007D1 RID: 2001 RVA: 0x000265B9 File Offset: 0x000247B9
		internal PermissionRenderer(Folder folder)
		{
			this.permissionSet = PermissionRenderer.GetPermissionSet(folder);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000265CD File Offset: 0x000247CD
		protected override Permission GetDefaultPermission()
		{
			return this.permissionSet.DefaultPermission;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x000265DA File Offset: 0x000247DA
		protected override Permission GetAnonymousPermission()
		{
			return this.permissionSet.AnonymousPermission;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x000265E7 File Offset: 0x000247E7
		protected override IEnumerator<Permission> GetPermissionEnumerator()
		{
			return this.permissionSet.GetEnumerator();
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x000265F4 File Offset: 0x000247F4
		protected override string GetPermissionsArrayElementName()
		{
			return "Permissions";
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000265FB File Offset: 0x000247FB
		protected override string GetPermissionElementName()
		{
			return "Permission";
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00026604 File Offset: 0x00024804
		private PermissionLevelType CreatePermissionLevel(PermissionLevel permissionLevel)
		{
			switch (permissionLevel)
			{
			case PermissionLevel.None:
				return PermissionLevelType.None;
			case PermissionLevel.Owner:
				return PermissionLevelType.Owner;
			case PermissionLevel.PublishingEditor:
				return PermissionLevelType.PublishingEditor;
			case PermissionLevel.Editor:
				return PermissionLevelType.Editor;
			case PermissionLevel.PublishingAuthor:
				return PermissionLevelType.PublishingAuthor;
			case PermissionLevel.Author:
				return PermissionLevelType.Author;
			case PermissionLevel.NonEditingAuthor:
				return PermissionLevelType.NoneditingAuthor;
			case PermissionLevel.Reviewer:
				return PermissionLevelType.Reviewer;
			case PermissionLevel.Contributor:
				return PermissionLevelType.Contributor;
			default:
				return PermissionLevelType.Custom;
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00026653 File Offset: 0x00024853
		private PermissionReadAccess CreatePermissionReadAccess(bool canReadItems)
		{
			if (!canReadItems)
			{
				return PermissionReadAccess.None;
			}
			return PermissionReadAccess.FullDetails;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0002665B File Offset: 0x0002485B
		protected override void RenderByTypePermissionDetails(Microsoft.Exchange.Services.Core.Types.PermissionType permissionElement, Permission permission)
		{
			permissionElement.ReadItems = new PermissionReadAccess?(this.CreatePermissionReadAccess(permission.CanReadItems));
			permissionElement.PermissionLevel = this.CreatePermissionLevel(permission.PermissionLevel);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00026688 File Offset: 0x00024888
		internal static PermissionSet GetPermissionSet(Folder folder)
		{
			PermissionSet result;
			try
			{
				FaultInjection.GenerateFault((FaultInjection.LIDs)3024497981U);
				result = folder.GetPermissionSet();
			}
			catch (StoragePermanentException ex)
			{
				if (ex.InnerException is MapiExceptionAmbiguousAlias)
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceError<StoragePermanentException>(0L, "Error occurred when fetching permission set for folder. Exception '{0}'.", ex);
					throw new ObjectCorruptException(ex, false);
				}
				throw;
			}
			return result;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000266E4 File Offset: 0x000248E4
		protected override Microsoft.Exchange.Services.Core.Types.PermissionType CreatePermissionElement()
		{
			return new Microsoft.Exchange.Services.Core.Types.PermissionType();
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x000266EB File Offset: 0x000248EB
		protected override void SetPermissionsOnSerializationObject(PermissionSetType serviceProperty, List<Microsoft.Exchange.Services.Core.Types.PermissionType> renderedPermissions)
		{
			serviceProperty.Permissions = renderedPermissions.ToArray();
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x000266F9 File Offset: 0x000248F9
		protected override void SetUnknownEntriesOnSerializationObject(PermissionSetType serviceProperty, string[] entries)
		{
			serviceProperty.UnknownEntries = entries;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00026702 File Offset: 0x00024902
		protected override PermissionSetType CreatePermissionSetElement()
		{
			return new PermissionSetType();
		}

		// Token: 0x04000703 RID: 1795
		private PermissionSet permissionSet;
	}
}
