using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Provisioning
{
	// Token: 0x020002AC RID: 684
	internal class LiveIdentityManager
	{
		// Token: 0x0600135A RID: 4954 RVA: 0x00087E33 File Offset: 0x00086033
		public LiveIdentityManager() : this(new NativeMethods())
		{
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00087E40 File Offset: 0x00086040
		internal LiveIdentityManager(NativeMethods nativeLibrary)
		{
			this.nativeMethods = nativeLibrary;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00087E65 File Offset: 0x00086065
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public string LogOnUser(string federationProviderId, string userName, string password, string siteName, string policy, string environment)
		{
			this.CloseIdentity();
			this.Initialize(environment);
			this.OpenIdentity(federationProviderId, userName, password);
			this.LogonPassport(policy, siteName);
			return this.LogonService(siteName, policy);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00087E94 File Offset: 0x00086094
		internal void CloseIdentity()
		{
			if (IntPtr.Zero != this.identityPtr && this.nativeMethods.CloseIdentityHandle(this.identityPtr) == 0)
			{
				this.identityPtr = IntPtr.Zero;
			}
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00087ED4 File Offset: 0x000860D4
		internal virtual void Initialize(string environment)
		{
			this.Uninitialize();
			NativeMethods.IdcrlOption[] array = null;
			uint dwOptions = 0U;
			GCHandle gchandle = default(GCHandle);
			try
			{
				if (!string.IsNullOrEmpty(environment))
				{
					array = new NativeMethods.IdcrlOption[1];
					dwOptions = 1U;
					byte[] bytes = Encoding.Unicode.GetBytes(environment);
					byte[] value = new byte[bytes.Length];
					gchandle = GCHandle.Alloc(value, GCHandleType.Pinned);
					IntPtr intPtr = gchandle.AddrOfPinnedObject();
					Marshal.Copy(bytes, 0, intPtr, bytes.Length);
					array[0].EnvironmentId = 64;
					array[0].EnvironmentValue = intPtr;
					array[0].EnvironmentLength = (uint)bytes.Length;
				}
				int num = this.nativeMethods.InitializeEx(ref this.serviceGuid, 1, 0U, array, dwOptions);
				if (num < 0)
				{
					string message = string.Format(CultureInfo.CurrentCulture, "Failed to initialize the environment: {0} , HR: {1}", new object[]
					{
						environment,
						num.ToString(CultureInfo.InvariantCulture)
					});
					WindowsLiveException ex = new WindowsLiveException(num, message);
					throw ex;
				}
				this.initialized = true;
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00087FEC File Offset: 0x000861EC
		internal void LogonPassport(string policy, string siteName)
		{
			NativeMethods.RstParams[] array = new NativeMethods.RstParams[1];
			array[0].CbSize = 0;
			array[0].ServiceName = siteName;
			array[0].ServicePolicy = policy;
			array[0].TokenFlags = 0;
			array[0].TokenParams = 0;
			int num = this.nativeMethods.LogonIdentityEx(this.identityPtr, policy, 0U, array, (uint)array.Length);
			this.GetAuthenticationStatus(siteName);
			if (num < 0)
			{
				string message = string.Format(CultureInfo.InvariantCulture, "FailLoginIdentity: policy: {0}, HR: {1}", new object[]
				{
					policy,
					num.ToString(CultureInfo.InvariantCulture)
				});
				WindowsLiveException ex = new WindowsLiveException(num, message);
				throw ex;
			}
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x000880A0 File Offset: 0x000862A0
		internal void GetAuthenticationStatus(string siteName)
		{
			IntPtr zero = IntPtr.Zero;
			try
			{
				int authenticationStatus = this.nativeMethods.GetAuthenticationStatus(this.identityPtr, siteName, 1U, out zero);
				if (authenticationStatus < 0)
				{
					string message = string.Format(CultureInfo.InvariantCulture, "FailGetAuthState: HR: {0}", new object[]
					{
						authenticationStatus.ToString(CultureInfo.InvariantCulture)
					});
					throw new WindowsLiveException(authenticationStatus, message);
				}
				NativeMethods.IdcrlStatusCurrent idcrlStatusCurrent = new NativeMethods.IdcrlStatusCurrent();
				Marshal.PtrToStructure(zero, idcrlStatusCurrent);
				if (296963 != idcrlStatusCurrent.AuthState)
				{
					string message2 = string.Format(CultureInfo.InvariantCulture, "FailGetAuthInvalidState: AuthState: {0}, RequestStatus: {1}, HR: {2}", new object[]
					{
						"0x" + idcrlStatusCurrent.AuthState.ToString("X", CultureInfo.InvariantCulture),
						"0x" + idcrlStatusCurrent.RequestStatus.ToString("X", CultureInfo.InvariantCulture),
						authenticationStatus.ToString(CultureInfo.InvariantCulture)
					});
					throw new WindowsLiveException(idcrlStatusCurrent.RequestStatus, message2);
				}
			}
			finally
			{
				this.FreeResource(ref zero);
			}
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x000881C0 File Offset: 0x000863C0
		internal virtual string LogonService(string siteName, string policy)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			uint num = 0U;
			uint num2 = 0U;
			int num3 = this.nativeMethods.AuthIdentityToService(this.identityPtr, siteName, policy, 131072U, out zero, out num2, out zero2, out num);
			if (num3 < 0)
			{
				string message = string.Format(CultureInfo.InvariantCulture, "FailAuthIdentityToService: site name: {0}, policy: {1}, HR: {2}", new object[]
				{
					siteName,
					policy,
					num3.ToString(CultureInfo.InvariantCulture)
				});
				throw new WindowsLiveException(num3, message);
			}
			string result;
			try
			{
				result = Marshal.PtrToStringUni(zero);
			}
			finally
			{
				this.FreeResource(ref zero);
				this.FreeResource(ref zero2);
			}
			return result;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00088274 File Offset: 0x00086474
		internal void OpenIdentity(string federationProviderId, string userName, string password)
		{
			try
			{
				int num = this.nativeMethods.CreateIdentityHandle2(federationProviderId, userName, 255U, out this.identityPtr);
				if (num < 0)
				{
					string message = string.Format(CultureInfo.InvariantCulture, "FailCreateIdentityHandle: user name: {0}, HR: {1}", new object[]
					{
						userName,
						num.ToString(CultureInfo.InvariantCulture)
					});
					throw new WindowsLiveException(num, message);
				}
				if (!string.IsNullOrEmpty(password))
				{
					num = this.nativeMethods.SetCredential(this.identityPtr, "ps:password", password);
					if (num < 0)
					{
						throw new WindowsLiveException(num, "Messages.FailSetCredential");
					}
				}
			}
			catch
			{
				this.CloseIdentity();
				throw;
			}
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00088320 File Offset: 0x00086520
		internal string GetLoggedOnUser()
		{
			string text = WindowsIdentity.GetCurrent().Name.ToLower();
			string[] array = text.Split(new char[]
			{
				'\\'
			});
			if (array.Length == 1 && text.Contains("@"))
			{
				return text;
			}
			IntPtr hEnumHandle;
			int num = this.nativeMethods.EnumIdentitiesWithCachedCredentials(null, out hEnumHandle);
			string text2 = null;
			string text3 = null;
			if (num == 0)
			{
				while (this.nativeMethods.NextIdentity(hEnumHandle, ref text2) == 0)
				{
					if (!string.IsNullOrEmpty(text2) && text2.Contains(array[1]))
					{
						text3 = text2;
						break;
					}
				}
			}
			this.nativeMethods.CloseEnumIdentitiesHandle(hEnumHandle);
			if (num != 0 || text3 == null)
			{
				throw new WindowsLiveException("Messages.FailAuthIdentityToService");
			}
			return text3;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x000883D0 File Offset: 0x000865D0
		internal void Uninitialize()
		{
			if (this.initialized)
			{
				int num = this.nativeMethods.Uninitialize();
				if (num < 0)
				{
					string message = string.Format(CultureInfo.InvariantCulture, "FailUninitialize: HR: {0}", new object[]
					{
						num.ToString(CultureInfo.InvariantCulture)
					});
					throw new WindowsLiveException(num, message);
				}
				this.initialized = false;
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00088430 File Offset: 0x00086630
		private void FreeResource(ref IntPtr resource)
		{
			if (IntPtr.Zero != resource && this.nativeMethods.PassportFreeMemory(resource) == 0)
			{
				resource = IntPtr.Zero;
			}
		}

		// Token: 0x04000EA0 RID: 3744
		private const string CredentialTypePassword = "ps:password";

		// Token: 0x04000EA1 RID: 3745
		private const int IdcrlCurrentVersion = 1;

		// Token: 0x04000EA2 RID: 3746
		private const int IdcrlAuthStateAuthenticatedPassword = 296963;

		// Token: 0x04000EA3 RID: 3747
		private const int ResultCode = 0;

		// Token: 0x04000EA4 RID: 3748
		private readonly NativeMethods nativeMethods;

		// Token: 0x04000EA5 RID: 3749
		private IntPtr identityPtr = IntPtr.Zero;

		// Token: 0x04000EA6 RID: 3750
		private bool initialized;

		// Token: 0x04000EA7 RID: 3751
		private Guid serviceGuid = Guid.NewGuid();
	}
}
