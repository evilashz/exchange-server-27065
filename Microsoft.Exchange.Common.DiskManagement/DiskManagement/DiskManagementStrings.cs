using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x02000009 RID: 9
	internal static class DiskManagementStrings
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00004084 File Offset: 0x00002284
		static DiskManagementStrings()
		{
			DiskManagementStrings.stringIDs.Add(2896272628U, "BitlockerCertificatesNotFoundError");
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000040D4 File Offset: 0x000022D4
		public static LocalizedString BitlockerUtilError(string errorMsg)
		{
			return new LocalizedString("BitlockerUtilError", DiskManagementStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000040FC File Offset: 0x000022FC
		public static LocalizedString InvalidCallWMIMethodArgumentsError(string[] inParamNameList, object inParamValueList, int inParamNameListLenght, int inParamValueListLenght)
		{
			return new LocalizedString("InvalidCallWMIMethodArgumentsError", DiskManagementStrings.ResourceManager, new object[]
			{
				inParamNameList,
				inParamValueList,
				inParamNameListLenght,
				inParamValueListLenght
			});
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000413C File Offset: 0x0000233C
		public static LocalizedString Win8EmptyVolumeNotFullyEncryptedAfterWaitError(string volume, int milliseconds, string bitlockerState)
		{
			return new LocalizedString("Win8EmptyVolumeNotFullyEncryptedAfterWaitError", DiskManagementStrings.ResourceManager, new object[]
			{
				volume,
				milliseconds,
				bitlockerState
			});
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00004171 File Offset: 0x00002371
		public static LocalizedString BitlockerCertificatesNotFoundError
		{
			get
			{
				return new LocalizedString("BitlockerCertificatesNotFoundError", DiskManagementStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00004188 File Offset: 0x00002388
		public static LocalizedString FullVolumeEncryptionOnWin8ServerError(string volumeID)
		{
			return new LocalizedString("FullVolumeEncryptionOnWin8ServerError", DiskManagementStrings.ResourceManager, new object[]
			{
				volumeID
			});
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000041B0 File Offset: 0x000023B0
		public static LocalizedString WMIError(int returnValue, string methodName, string errorMessage)
		{
			return new LocalizedString("WMIError", DiskManagementStrings.ResourceManager, new object[]
			{
				returnValue,
				methodName,
				errorMessage
			});
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000041E8 File Offset: 0x000023E8
		public static LocalizedString MountpointsFindError(string error)
		{
			return new LocalizedString("MountpointsFindError", DiskManagementStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00004210 File Offset: 0x00002410
		public static LocalizedString Win7EmptyVolumeNotEncryptingAfterStartingEncryptionError(string volume, string bitlockerState)
		{
			return new LocalizedString("Win7EmptyVolumeNotEncryptingAfterStartingEncryptionError", DiskManagementStrings.ResourceManager, new object[]
			{
				volume,
				bitlockerState
			});
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000423C File Offset: 0x0000243C
		public static LocalizedString EncryptingVolumesFindError(string error)
		{
			return new LocalizedString("EncryptingVolumesFindError", DiskManagementStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004264 File Offset: 0x00002464
		public static LocalizedString UsedOnlySpaceEncryptionAttemptOnANonEmptyVolumeError(string volume)
		{
			return new LocalizedString("UsedOnlySpaceEncryptionAttemptOnANonEmptyVolumeError", DiskManagementStrings.ResourceManager, new object[]
			{
				volume
			});
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000428C File Offset: 0x0000248C
		public static LocalizedString UsedOnlyEncryptionOnNonWin8ServerError(string volumeID)
		{
			return new LocalizedString("UsedOnlyEncryptionOnNonWin8ServerError", DiskManagementStrings.ResourceManager, new object[]
			{
				volumeID
			});
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000042B4 File Offset: 0x000024B4
		public static LocalizedString Win7EmptyVolumeNotStartedDueToPreviousFailureBadBlocksError(string volume, string mountPoint, string eventXML)
		{
			return new LocalizedString("Win7EmptyVolumeNotStartedDueToPreviousFailureBadBlocksError", DiskManagementStrings.ResourceManager, new object[]
			{
				volume,
				mountPoint,
				eventXML
			});
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000042E4 File Offset: 0x000024E4
		public static LocalizedString InvalidVolumeIdError(string volumeId)
		{
			return new LocalizedString("InvalidVolumeIdError", DiskManagementStrings.ResourceManager, new object[]
			{
				volumeId
			});
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000430C File Offset: 0x0000250C
		public static LocalizedString VolumeLockedError(string volume)
		{
			return new LocalizedString("VolumeLockedError", DiskManagementStrings.ResourceManager, new object[]
			{
				volume
			});
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00004334 File Offset: 0x00002534
		public static LocalizedString VolumeCannotBeBothEncryptingAndEncrypted(string volumeName)
		{
			return new LocalizedString("VolumeCannotBeBothEncryptingAndEncrypted", DiskManagementStrings.ResourceManager, new object[]
			{
				volumeName
			});
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000435C File Offset: 0x0000255C
		public static LocalizedString ReturnValueExceptionInconsistency(string methodName, string errorMsg)
		{
			return new LocalizedString("ReturnValueExceptionInconsistency", DiskManagementStrings.ResourceManager, new object[]
			{
				methodName,
				errorMsg
			});
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00004388 File Offset: 0x00002588
		public static LocalizedString EncryptableVolumeArgNullError(string methodName)
		{
			return new LocalizedString("EncryptableVolumeArgNullError", DiskManagementStrings.ResourceManager, new object[]
			{
				methodName
			});
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000043B0 File Offset: 0x000025B0
		public static LocalizedString LockedVolumesFindError(string error)
		{
			return new LocalizedString("LockedVolumesFindError", DiskManagementStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000043D8 File Offset: 0x000025D8
		public static LocalizedString InvalidFilePathError(string filePath)
		{
			return new LocalizedString("InvalidFilePathError", DiskManagementStrings.ResourceManager, new object[]
			{
				filePath
			});
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00004400 File Offset: 0x00002600
		public static LocalizedString FullVolumeEncryptionAttemptOnANonEmptyVolumeError(string volume)
		{
			return new LocalizedString("FullVolumeEncryptionAttemptOnANonEmptyVolumeError", DiskManagementStrings.ResourceManager, new object[]
			{
				volume
			});
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004428 File Offset: 0x00002628
		public static LocalizedString VolumeLockedFindError(string volumeId, string error)
		{
			return new LocalizedString("VolumeLockedFindError", DiskManagementStrings.ResourceManager, new object[]
			{
				volumeId,
				error
			});
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004454 File Offset: 0x00002654
		public static LocalizedString GetLocalizedString(DiskManagementStrings.IDs key)
		{
			return new LocalizedString(DiskManagementStrings.stringIDs[(uint)key], DiskManagementStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000032 RID: 50
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(1);

		// Token: 0x04000033 RID: 51
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Common.DiskManagement.Strings", typeof(DiskManagementStrings).GetTypeInfo().Assembly);

		// Token: 0x0200000A RID: 10
		public enum IDs : uint
		{
			// Token: 0x04000035 RID: 53
			BitlockerCertificatesNotFoundError = 2896272628U
		}

		// Token: 0x0200000B RID: 11
		private enum ParamIDs
		{
			// Token: 0x04000037 RID: 55
			BitlockerUtilError,
			// Token: 0x04000038 RID: 56
			InvalidCallWMIMethodArgumentsError,
			// Token: 0x04000039 RID: 57
			Win8EmptyVolumeNotFullyEncryptedAfterWaitError,
			// Token: 0x0400003A RID: 58
			FullVolumeEncryptionOnWin8ServerError,
			// Token: 0x0400003B RID: 59
			WMIError,
			// Token: 0x0400003C RID: 60
			MountpointsFindError,
			// Token: 0x0400003D RID: 61
			Win7EmptyVolumeNotEncryptingAfterStartingEncryptionError,
			// Token: 0x0400003E RID: 62
			EncryptingVolumesFindError,
			// Token: 0x0400003F RID: 63
			UsedOnlySpaceEncryptionAttemptOnANonEmptyVolumeError,
			// Token: 0x04000040 RID: 64
			UsedOnlyEncryptionOnNonWin8ServerError,
			// Token: 0x04000041 RID: 65
			Win7EmptyVolumeNotStartedDueToPreviousFailureBadBlocksError,
			// Token: 0x04000042 RID: 66
			InvalidVolumeIdError,
			// Token: 0x04000043 RID: 67
			VolumeLockedError,
			// Token: 0x04000044 RID: 68
			VolumeCannotBeBothEncryptingAndEncrypted,
			// Token: 0x04000045 RID: 69
			ReturnValueExceptionInconsistency,
			// Token: 0x04000046 RID: 70
			EncryptableVolumeArgNullError,
			// Token: 0x04000047 RID: 71
			LockedVolumesFindError,
			// Token: 0x04000048 RID: 72
			InvalidFilePathError,
			// Token: 0x04000049 RID: 73
			FullVolumeEncryptionAttemptOnANonEmptyVolumeError,
			// Token: 0x0400004A RID: 74
			VolumeLockedFindError
		}
	}
}
