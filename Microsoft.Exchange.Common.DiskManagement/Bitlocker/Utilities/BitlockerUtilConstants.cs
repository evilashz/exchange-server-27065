using System;

namespace Microsoft.Exchange.Common.Bitlocker.Utilities
{
	// Token: 0x02000006 RID: 6
	public static class BitlockerUtilConstants
	{
		// Token: 0x0400000C RID: 12
		public const string MountPointWMIClassName = "Win32_MountPoint";

		// Token: 0x0400000D RID: 13
		public const string DefaultWMINamespace = "\\ROOT\\CIMV2";

		// Token: 0x0400000E RID: 14
		public const int WMISuccessReturnValue = 0;

		// Token: 0x0400000F RID: 15
		public const int WMIFailureReturnValue = -1;

		// Token: 0x04000010 RID: 16
		public const string MasterPassword = "563563-218372-416746-433752-541937-608069-594110-446754";

		// Token: 0x04000011 RID: 17
		public const string CertsPath = "C:\\LocalFiles\\Exchange\\DataCenter\\Certs";

		// Token: 0x04000012 RID: 18
		public const string BootVolumeQuery = "SELECT * FROM Win32_Volume Where BootVolume=True";

		// Token: 0x04000013 RID: 19
		public const string SystemVolumeQuery = "SELECT * FROM Win32_Volume Where SystemVolume=True";

		// Token: 0x04000014 RID: 20
		public const string VirtualDiskQuery = "Select * FROM Win32_DiskDrive WHERE Model like '%Virtual Disk%'";

		// Token: 0x04000015 RID: 21
		public const string VolumeQuery = "SELECT * FROM Win32_Volume WHERE DeviceId=\"{0}\"";

		// Token: 0x04000016 RID: 22
		public const string EncryptableVolumeWMIClassName = "Win32_EncryptableVolume";

		// Token: 0x04000017 RID: 23
		public const string EncryptableVolumeWMINamespace = "\\ROOT\\CIMV2\\Security\\MicrosoftVolumeEncryption";

		// Token: 0x04000018 RID: 24
		public const int EncryptionMethodAES128 = 3;

		// Token: 0x04000019 RID: 25
		public const int EncryptionMethodAES256 = 4;

		// Token: 0x0400001A RID: 26
		public const int UsedOnlyEncryptionModeFlag = 1;

		// Token: 0x0400001B RID: 27
		public const string EncryptMethodName = "Encrypt";

		// Token: 0x0400001C RID: 28
		public const string DecryptMethodName = "Decrypt";

		// Token: 0x0400001D RID: 29
		public const string LockMethodName = "Lock";

		// Token: 0x0400001E RID: 30
		public const string PauseConversionMethodName = "PauseConversion";

		// Token: 0x0400001F RID: 31
		public const string ResumeConversionMethodName = "ResumeConversion";

		// Token: 0x04000020 RID: 32
		public const string ProtectWithCertificateMethodName = "ProtectKeyWithCertificateThumbprint";

		// Token: 0x04000021 RID: 33
		public const string ProtectWithNumericalPasswordMethodName = "ProtectKeyWithNumericalPassword";

		// Token: 0x04000022 RID: 34
		public const string UnlockLockNumericalPasswordMethodName = "UnlockWithNumericalPassword";

		// Token: 0x04000023 RID: 35
		public const string NumericalPasswordMethodArgName = "NumericalPassword";

		// Token: 0x04000024 RID: 36
		public const string CertificateFilePathArgName = "PathWithFileName";

		// Token: 0x04000025 RID: 37
		public const string LockStatusMethodName = "GetLockStatus";

		// Token: 0x04000026 RID: 38
		public const string LockStatusPropertyName = "LockStatus";

		// Token: 0x04000027 RID: 39
		public const string EncryptionPercentagePropertyName = "EncryptionPercentage";

		// Token: 0x04000028 RID: 40
		public const string EncryptionStatusPropertyName = "ConversionStatus";

		// Token: 0x04000029 RID: 41
		public const string EncryptionStatusMethodName = "GetConversionStatus";

		// Token: 0x0400002A RID: 42
		public const string ReturnValuePropertyName = "ReturnValue";

		// Token: 0x0400002B RID: 43
		public const string DeviceIdPropertyName = "DeviceId";

		// Token: 0x0400002C RID: 44
		public const string WIn32VolumePropertyName = "Name";

		// Token: 0x0400002D RID: 45
		public const string VolumePropertyName = "Volume";

		// Token: 0x0400002E RID: 46
		public const string DirectoryPropertyName = "Directory";

		// Token: 0x0400002F RID: 47
		public const long EmptyVolumeMBinBytes = 524288000L;

		// Token: 0x04000030 RID: 48
		public const int Win8EmptyVolumeUsedOnlySpaceEncryptionTimeoutInSecs = 300;

		// Token: 0x04000031 RID: 49
		public const int Win8EmptyVolumeUsedOnlySpaceWaitIntervalInSecs = 1;
	}
}
