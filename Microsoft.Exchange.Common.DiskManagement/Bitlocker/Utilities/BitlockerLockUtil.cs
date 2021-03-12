using System;
using System.Collections.Generic;
using System.Management;
using Microsoft.Exchange.Common.DiskManagement;
using Microsoft.Exchange.Common.DiskManagement.Utilities;

namespace Microsoft.Exchange.Common.Bitlocker.Utilities
{
	// Token: 0x02000002 RID: 2
	public static class BitlockerLockUtil
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002154 File Offset: 0x00000354
		public static Exception UnlockWithNumericalPassword(ManagementObject encryptableVolume, string numericalPassword, out int returnValueUnlockWithNumericalPassword)
		{
			ManagementBaseObject outParams = null;
			int returnValue = -1;
			Exception ex = Util.HandleExceptions(delegate
			{
				string deviceId = BitlockerUtil.GetDeviceId(encryptableVolume);
				Exception ex2;
				if (!BitlockerLockUtil.IsVolumeLocked(encryptableVolume, out ex2))
				{
					returnValue = 0;
					throw new VolumeLockedFindException(deviceId, ex2.Message, ex2);
				}
				returnValue = WMIUtil.CallWMIMethod(encryptableVolume, "UnlockWithNumericalPassword", new string[]
				{
					"NumericalPassword"
				}, new object[]
				{
					numericalPassword
				}, out outParams);
			});
			returnValueUnlockWithNumericalPassword = returnValue;
			Util.AssertReturnValueExceptionInconsistency(returnValueUnlockWithNumericalPassword, "UnlockWithNumericalPassword", ex);
			return Util.ReturnWMIErrorExceptionOnExceptionOrError(returnValueUnlockWithNumericalPassword, "UnlockWithNumericalPassword", ex);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000223C File Offset: 0x0000043C
		public static Exception LockDataVolume(ManagementObject encryptableVolume, out int returnValueLock)
		{
			ManagementBaseObject outParams = null;
			int returnValue = -1;
			Exception ex = Util.HandleExceptions(delegate
			{
				string deviceId = BitlockerUtil.GetDeviceId(encryptableVolume);
				Exception ex2;
				if (BitlockerLockUtil.IsVolumeLocked(encryptableVolume, out ex2))
				{
					if (ex2 != null)
					{
						throw new VolumeLockedFindException(deviceId, ex2.Message, ex2);
					}
				}
				else
				{
					returnValue = WMIUtil.CallWMIMethod(encryptableVolume, "Lock", null, null, out outParams);
					if (BitlockerLockUtil.IsVolumeLocked(encryptableVolume, out ex2) && ex2 != null)
					{
						throw new VolumeLockedFindException(deviceId, ex2.Message, ex2);
					}
				}
			});
			returnValueLock = returnValue;
			Util.AssertReturnValueExceptionInconsistency(returnValueLock, "LockDataVolume", ex);
			return Util.ReturnWMIErrorExceptionOnExceptionOrError(returnValueLock, "LockDataVolume", ex);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002308 File Offset: 0x00000508
		public static bool IsVolumeLocked(ManagementObject encryptableVolume, out Exception ex)
		{
			ManagementBaseObject outParams = null;
			bool success = false;
			ex = Util.HandleExceptions(delegate
			{
				if (WMIUtil.CallWMIMethod(encryptableVolume, "GetLockStatus", null, null, out outParams) == 0)
				{
					int num = Convert.ToInt32(outParams["LockStatus"]);
					if (Enum.IsDefined(typeof(BitlockerLockUtil.LockStatus), num))
					{
						BitlockerLockUtil.LockStatus lockStatus = (BitlockerLockUtil.LockStatus)num;
						success = (lockStatus == BitlockerLockUtil.LockStatus.Locked);
					}
				}
			});
			Util.ThrowIfNotNull(ex);
			return success;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000023F0 File Offset: 0x000005F0
		public static List<string> GetLockedVolumes(out Exception ex)
		{
			List<string> lockedVolumes = null;
			ex = Util.HandleExceptions(delegate
			{
				ManagementObjectCollection encryptableVolumes = BitlockerUtil.GetEncryptableVolumes();
				if (encryptableVolumes != null)
				{
					foreach (ManagementBaseObject managementBaseObject in encryptableVolumes)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						string item = managementObject.GetPropertyValue("DeviceId").ToString();
						Exception ex2;
						bool flag = BitlockerLockUtil.IsVolumeLocked(managementObject, out ex2);
						Util.ThrowIfNotNull(ex2);
						if (flag)
						{
							if (lockedVolumes == null)
							{
								lockedVolumes = new List<string>();
							}
							lockedVolumes.Add(item);
						}
					}
				}
			});
			return lockedVolumes;
		}

		// Token: 0x02000003 RID: 3
		public enum LockStatus
		{
			// Token: 0x04000002 RID: 2
			Unlocked,
			// Token: 0x04000003 RID: 3
			Locked
		}
	}
}
