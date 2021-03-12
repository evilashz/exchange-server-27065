using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x0200026B RID: 619
	[ComVisible(true)]
	public sealed class CspKeyContainerInfo
	{
		// Token: 0x060021EF RID: 8687 RVA: 0x00077F4E File Offset: 0x0007614E
		private CspKeyContainerInfo()
		{
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x00077F58 File Offset: 0x00076158
		[SecurityCritical]
		internal CspKeyContainerInfo(CspParameters parameters, bool randomKeyContainer)
		{
			if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(parameters, KeyContainerPermissionFlags.Open);
				keyContainerPermission.AccessEntries.Add(accessEntry);
				keyContainerPermission.Demand();
			}
			this.m_parameters = new CspParameters(parameters);
			if (this.m_parameters.KeyNumber == -1)
			{
				if (this.m_parameters.ProviderType == 1 || this.m_parameters.ProviderType == 24)
				{
					this.m_parameters.KeyNumber = 1;
				}
				else if (this.m_parameters.ProviderType == 13)
				{
					this.m_parameters.KeyNumber = 2;
				}
			}
			this.m_randomKeyContainer = randomKeyContainer;
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x00077FFB File Offset: 0x000761FB
		[SecuritySafeCritical]
		public CspKeyContainerInfo(CspParameters parameters) : this(parameters, false)
		{
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x00078005 File Offset: 0x00076205
		public bool MachineKeyStore
		{
			get
			{
				return (this.m_parameters.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x0007801A File Offset: 0x0007621A
		public string ProviderName
		{
			get
			{
				return this.m_parameters.ProviderName;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x00078027 File Offset: 0x00076227
		public int ProviderType
		{
			get
			{
				return this.m_parameters.ProviderType;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x00078034 File Offset: 0x00076234
		public string KeyContainerName
		{
			get
			{
				return this.m_parameters.KeyContainerName;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x00078044 File Offset: 0x00076244
		public string UniqueKeyContainerName
		{
			[SecuritySafeCritical]
			get
			{
				SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
				int num = Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle);
				if (num != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
				}
				string result = (string)Utils._GetProviderParameter(invalidHandle, this.m_parameters.KeyNumber, 8U);
				invalidHandle.Dispose();
				return result;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x00078099 File Offset: 0x00076299
		public KeyNumber KeyNumber
		{
			get
			{
				return (KeyNumber)this.m_parameters.KeyNumber;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060021F8 RID: 8696 RVA: 0x000780A8 File Offset: 0x000762A8
		public bool Exportable
		{
			[SecuritySafeCritical]
			get
			{
				if (this.HardwareDevice)
				{
					return false;
				}
				SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
				int num = Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle);
				if (num != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
				}
				byte[] array = (byte[])Utils._GetProviderParameter(invalidHandle, this.m_parameters.KeyNumber, 3U);
				invalidHandle.Dispose();
				return array[0] == 1;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060021F9 RID: 8697 RVA: 0x0007810C File Offset: 0x0007630C
		public bool HardwareDevice
		{
			[SecuritySafeCritical]
			get
			{
				SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
				CspParameters cspParameters = new CspParameters(this.m_parameters);
				cspParameters.KeyContainerName = null;
				cspParameters.Flags = (((cspParameters.Flags & CspProviderFlags.UseMachineKeyStore) != CspProviderFlags.NoFlags) ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags);
				uint flags = 4026531840U;
				int num = Utils._OpenCSP(cspParameters, flags, ref invalidHandle);
				if (num != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
				}
				byte[] array = (byte[])Utils._GetProviderParameter(invalidHandle, cspParameters.KeyNumber, 5U);
				invalidHandle.Dispose();
				return array[0] == 1;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060021FA RID: 8698 RVA: 0x0007818C File Offset: 0x0007638C
		public bool Removable
		{
			[SecuritySafeCritical]
			get
			{
				SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
				CspParameters cspParameters = new CspParameters(this.m_parameters);
				cspParameters.KeyContainerName = null;
				cspParameters.Flags = (((cspParameters.Flags & CspProviderFlags.UseMachineKeyStore) != CspProviderFlags.NoFlags) ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags);
				uint flags = 4026531840U;
				int num = Utils._OpenCSP(cspParameters, flags, ref invalidHandle);
				if (num != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
				}
				byte[] array = (byte[])Utils._GetProviderParameter(invalidHandle, cspParameters.KeyNumber, 4U);
				invalidHandle.Dispose();
				return array[0] == 1;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060021FB RID: 8699 RVA: 0x0007820C File Offset: 0x0007640C
		public bool Accessible
		{
			[SecuritySafeCritical]
			get
			{
				SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
				int num = Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle);
				if (num != 0)
				{
					return false;
				}
				byte[] array = (byte[])Utils._GetProviderParameter(invalidHandle, this.m_parameters.KeyNumber, 6U);
				invalidHandle.Dispose();
				return array[0] == 1;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060021FC RID: 8700 RVA: 0x00078258 File Offset: 0x00076458
		public bool Protected
		{
			[SecuritySafeCritical]
			get
			{
				if (this.HardwareDevice)
				{
					return true;
				}
				SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
				int num = Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle);
				if (num != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
				}
				byte[] array = (byte[])Utils._GetProviderParameter(invalidHandle, this.m_parameters.KeyNumber, 7U);
				invalidHandle.Dispose();
				return array[0] == 1;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x000782BC File Offset: 0x000764BC
		public CryptoKeySecurity CryptoKeySecurity
		{
			[SecuritySafeCritical]
			get
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this.m_parameters, KeyContainerPermissionFlags.ViewAcl | KeyContainerPermissionFlags.ChangeAcl);
				keyContainerPermission.AccessEntries.Add(accessEntry);
				keyContainerPermission.Demand();
				SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
				int num = Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle);
				if (num != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
				}
				CryptoKeySecurity keySetSecurityInfo;
				using (invalidHandle)
				{
					keySetSecurityInfo = Utils.GetKeySetSecurityInfo(invalidHandle, AccessControlSections.All);
				}
				return keySetSecurityInfo;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x0007834C File Offset: 0x0007654C
		public bool RandomlyGenerated
		{
			get
			{
				return this.m_randomKeyContainer;
			}
		}

		// Token: 0x04000C57 RID: 3159
		private CspParameters m_parameters;

		// Token: 0x04000C58 RID: 3160
		private bool m_randomKeyContainer;
	}
}
