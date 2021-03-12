using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000147 RID: 327
	internal sealed class SharingEffectiveRightsProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x06000901 RID: 2305 RVA: 0x0002BFCF File Offset: 0x0002A1CF
		public SharingEffectiveRightsProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0002BFD8 File Offset: 0x0002A1D8
		public static SharingEffectiveRightsProperty CreateCommand(CommandContext commandContext)
		{
			return new SharingEffectiveRightsProperty(commandContext);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0002BFE0 File Offset: 0x0002A1E0
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ExternalUserIdAndSession externalUserIdAndSession = commandSettings.IdAndSession as ExternalUserIdAndSession;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			if (externalUserIdAndSession == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<IdAndSession>((long)this.GetHashCode(), "SharingEffectiveRightsProperty.ToServiceObject: The id and session {0} is not an external user id.", commandSettings.IdAndSession);
				return;
			}
			MailboxSession mailboxSession = externalUserIdAndSession.Session as MailboxSession;
			DefaultFolderType defaultFolderType = mailboxSession.IsDefaultFolderType(externalUserIdAndSession.Id);
			if (defaultFolderType == DefaultFolderType.Calendar)
			{
				CalendarPermissionReadAccess calendarPermissionReadAccess = CalendarPermissionReadAccess.None;
				if (externalUserIdAndSession.PermissionGranted.CanReadItems)
				{
					calendarPermissionReadAccess = CalendarPermissionReadAccess.FullDetails;
				}
				else
				{
					CalendarFolderPermission calendarFolderPermission = externalUserIdAndSession.PermissionGranted as CalendarFolderPermission;
					if (calendarFolderPermission != null)
					{
						if (calendarFolderPermission.FreeBusyAccess == FreeBusyAccess.Details)
						{
							calendarPermissionReadAccess = CalendarPermissionReadAccess.TimeAndSubjectAndLocation;
						}
						else if (calendarFolderPermission.FreeBusyAccess == FreeBusyAccess.Basic)
						{
							calendarPermissionReadAccess = CalendarPermissionReadAccess.TimeOnly;
						}
					}
				}
				serviceObject[propertyInformation] = calendarPermissionReadAccess;
				return;
			}
			if (defaultFolderType == DefaultFolderType.Contacts)
			{
				PermissionReadAccess permissionReadAccess = PermissionReadAccess.None;
				if (externalUserIdAndSession.PermissionGranted.CanReadItems)
				{
					permissionReadAccess = PermissionReadAccess.FullDetails;
				}
				serviceObject[propertyInformation] = permissionReadAccess;
				return;
			}
			ExTraceGlobals.CommonAlgorithmTracer.TraceWarning<string>((long)this.GetHashCode(), "SharingEffectiveRightsProperty.ToServiceObject: The foldertype {0} is not supported.", defaultFolderType.ToString());
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0002C0F1 File Offset: 0x0002A2F1
		void IToXmlCommand.ToXml()
		{
			throw new InvalidOperationException("SharingEffectiveRightsProperty.ToXml should not be called.");
		}
	}
}
