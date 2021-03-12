using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.DataProviders
{
	// Token: 0x02000002 RID: 2
	internal static class Strings
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static Strings()
		{
			Strings.stringIDs.Add(3579904699U, "ErrorAccessDenied");
			Strings.stringIDs.Add(2352838554U, "ErrorAllButLastNestedAttachmentMustBeItemAttachment");
			Strings.stringIDs.Add(3410698111U, "RightsManagementMailboxOnlySupport");
			Strings.stringIDs.Add(2624402344U, "ErrorItemCorrupt");
			Strings.stringIDs.Add(1702622873U, "RightsManagementInternalLicensingDisabled");
			Strings.stringIDs.Add(1408093181U, "ErrorInvalidComplianceId");
			Strings.stringIDs.Add(1426071079U, "ErrorNestedAttachmentsCannotBeRemoved");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002197 File Offset: 0x00000397
		public static LocalizedString ErrorAccessDenied
		{
			get
			{
				return new LocalizedString("ErrorAccessDenied", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021B0 File Offset: 0x000003B0
		public static LocalizedString ErrorRightsManagementDuplicateTemplateId(string id)
		{
			return new LocalizedString("ErrorRightsManagementDuplicateTemplateId", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000021D8 File Offset: 0x000003D8
		public static LocalizedString ErrorAllButLastNestedAttachmentMustBeItemAttachment
		{
			get
			{
				return new LocalizedString("ErrorAllButLastNestedAttachmentMustBeItemAttachment", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021F0 File Offset: 0x000003F0
		public static LocalizedString IrresolvableConflict(ConflictResolutionResult conflictResolutionResult)
		{
			return new LocalizedString("IrresolvableConflict", Strings.ResourceManager, new object[]
			{
				conflictResolutionResult
			});
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002218 File Offset: 0x00000418
		public static LocalizedString RightsManagementMailboxOnlySupport
		{
			get
			{
				return new LocalizedString("RightsManagementMailboxOnlySupport", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000222F File Offset: 0x0000042F
		public static LocalizedString ErrorItemCorrupt
		{
			get
			{
				return new LocalizedString("ErrorItemCorrupt", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002248 File Offset: 0x00000448
		public static LocalizedString InvalidRequest(LocalizedString violation)
		{
			return new LocalizedString("InvalidRequest", Strings.ResourceManager, new object[]
			{
				violation
			});
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002278 File Offset: 0x00000478
		public static LocalizedString CanNotUseFolderIdForItem(string id)
		{
			return new LocalizedString("CanNotUseFolderIdForItem", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022A0 File Offset: 0x000004A0
		public static LocalizedString ErrorMissingRequiredParameter(string name)
		{
			return new LocalizedString("ErrorMissingRequiredParameter", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022C8 File Offset: 0x000004C8
		public static LocalizedString ErrorUnsupportedOperation(string operationName)
		{
			return new LocalizedString("ErrorUnsupportedOperation", Strings.ResourceManager, new object[]
			{
				operationName
			});
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000022F0 File Offset: 0x000004F0
		public static LocalizedString RightsManagementInternalLicensingDisabled
		{
			get
			{
				return new LocalizedString("RightsManagementInternalLicensingDisabled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002308 File Offset: 0x00000508
		public static LocalizedString ItemWithGivenIdNotFound(string id)
		{
			return new LocalizedString("ItemWithGivenIdNotFound", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002330 File Offset: 0x00000530
		public static LocalizedString ErrorInvalidTimeZoneId(string id)
		{
			return new LocalizedString("ErrorInvalidTimeZoneId", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002358 File Offset: 0x00000558
		public static LocalizedString ErrorInvalidComplianceId
		{
			get
			{
				return new LocalizedString("ErrorInvalidComplianceId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000236F File Offset: 0x0000056F
		public static LocalizedString ErrorNestedAttachmentsCannotBeRemoved
		{
			get
			{
				return new LocalizedString("ErrorNestedAttachmentsCannotBeRemoved", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002388 File Offset: 0x00000588
		public static LocalizedString CanNotUseItemIdForFolder(string id)
		{
			return new LocalizedString("CanNotUseItemIdForFolder", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023B0 File Offset: 0x000005B0
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000001 RID: 1
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(7);

		// Token: 0x04000002 RID: 2
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Entities.DataProviders.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000003 RID: 3
		public enum IDs : uint
		{
			// Token: 0x04000004 RID: 4
			ErrorAccessDenied = 3579904699U,
			// Token: 0x04000005 RID: 5
			ErrorAllButLastNestedAttachmentMustBeItemAttachment = 2352838554U,
			// Token: 0x04000006 RID: 6
			RightsManagementMailboxOnlySupport = 3410698111U,
			// Token: 0x04000007 RID: 7
			ErrorItemCorrupt = 2624402344U,
			// Token: 0x04000008 RID: 8
			RightsManagementInternalLicensingDisabled = 1702622873U,
			// Token: 0x04000009 RID: 9
			ErrorInvalidComplianceId = 1408093181U,
			// Token: 0x0400000A RID: 10
			ErrorNestedAttachmentsCannotBeRemoved = 1426071079U
		}

		// Token: 0x02000004 RID: 4
		private enum ParamIDs
		{
			// Token: 0x0400000C RID: 12
			ErrorRightsManagementDuplicateTemplateId,
			// Token: 0x0400000D RID: 13
			IrresolvableConflict,
			// Token: 0x0400000E RID: 14
			InvalidRequest,
			// Token: 0x0400000F RID: 15
			CanNotUseFolderIdForItem,
			// Token: 0x04000010 RID: 16
			ErrorMissingRequiredParameter,
			// Token: 0x04000011 RID: 17
			ErrorUnsupportedOperation,
			// Token: 0x04000012 RID: 18
			ItemWithGivenIdNotFound,
			// Token: 0x04000013 RID: 19
			ErrorInvalidTimeZoneId,
			// Token: 0x04000014 RID: 20
			CanNotUseItemIdForFolder
		}
	}
}
