using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x02000020 RID: 32
	internal static class AuthzAuthorization
	{
		// Token: 0x060000AB RID: 171 RVA: 0x000044EC File Offset: 0x000026EC
		private static Guid[] AllExtendedRights()
		{
			IList<Guid> list = WellKnownPermission.ToGuids(Permission.All);
			Guid[] array = new Guid[list.Count + 1];
			array[0] = new Guid("36909524-A6B8-4b59-94E2-25BFE4B3DAB3");
			list.CopyTo(array, 1);
			return array;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004534 File Offset: 0x00002734
		private static Permission[] ReverseMap(Guid[] guids)
		{
			Permission[] array = new Permission[guids.Length];
			int num = 0;
			foreach (Guid guid in guids)
			{
				array[num++] = WellKnownPermission.ToPermission(guid);
			}
			return array;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000457C File Offset: 0x0000277C
		internal static ResourceManagerHandle ResourceManagerHandle
		{
			get
			{
				return AuthzAuthorization.resourceManagerHandle;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004584 File Offset: 0x00002784
		public static bool CheckSingleExtendedRight(SecurityIdentifier sid, RawSecurityDescriptor rawSecurityDescriptor, Guid extendedRightGuid)
		{
			bool result;
			using (AuthzContextHandle authzContext = AuthzAuthorization.GetAuthzContext(sid, false))
			{
				bool[] array = AuthzAuthorization.CheckExtendedRights(authzContext, rawSecurityDescriptor, new Guid[]
				{
					extendedRightGuid
				}, null);
				if (array == null || array.Length < 1)
				{
					result = false;
				}
				else
				{
					result = array[0];
				}
			}
			return result;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000045E8 File Offset: 0x000027E8
		public static int CheckGenericPermission(SecurityIdentifier sid, RawSecurityDescriptor rawSecurityDescriptor, AccessMask requestedAccess)
		{
			if (rawSecurityDescriptor == null)
			{
				throw new ArgumentNullException("securityDescriptor");
			}
			int result;
			using (AuthzContextHandle authzContext = AuthzAuthorization.GetAuthzContext(sid, false))
			{
				using (SafeHGlobalHandle safeHGlobalHandle = AccessRequest.AllocateNativeStruct(requestedAccess, null, null))
				{
					using (SafeHGlobalHandle safeHGlobalHandle2 = AccessReply.AllocateNativeStruct(1))
					{
						byte[] array = new byte[rawSecurityDescriptor.BinaryLength];
						rawSecurityDescriptor.GetBinaryForm(array, 0);
						if (!NativeMethods.AuthzAccessCheck(0U, authzContext, safeHGlobalHandle, IntPtr.Zero, array, IntPtr.Zero, 0U, safeHGlobalHandle2, IntPtr.Zero))
						{
							throw new Win32Exception(Marshal.GetLastWin32Error());
						}
						AccessReply accessReply = AccessReply.Create(safeHGlobalHandle2);
						if (accessReply.Errors[0] == 0)
						{
							result = accessReply.GrantedAccessMasks[0];
						}
						else
						{
							result = 0;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000046C4 File Offset: 0x000028C4
		public static bool CheckSinglePermission(IntPtr token, RawSecurityDescriptor rawSecurityDescriptor, Permission permission)
		{
			return AuthzAuthorization.CheckSinglePermission(token, SecurityDescriptor.FromRawSecurityDescriptor(rawSecurityDescriptor), permission);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000046D4 File Offset: 0x000028D4
		public static bool CheckSinglePermission(IntPtr token, SecurityDescriptor securityDescriptor, Permission permission)
		{
			Guid guid = WellKnownPermission.ToGuid(permission);
			bool result;
			using (AuthzContextHandle authzContext = AuthzAuthorization.GetAuthzContext(token))
			{
				bool[] array = AuthzAuthorization.CheckExtendedRights(authzContext, securityDescriptor, new Guid[]
				{
					guid
				}, null, AccessMask.ControlAccess);
				result = array[0];
			}
			return result;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004738 File Offset: 0x00002938
		public static bool CheckSinglePermission(IntPtr token, RawSecurityDescriptor rawSecurityDescriptor, Permission permission, SecurityIdentifier principalSelfSid)
		{
			return AuthzAuthorization.CheckSinglePermission(token, SecurityDescriptor.FromRawSecurityDescriptor(rawSecurityDescriptor), permission, principalSelfSid);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004748 File Offset: 0x00002948
		public static bool CheckSinglePermission(IntPtr token, SecurityDescriptor securityDescriptor, Permission permission, SecurityIdentifier principalSelfSid)
		{
			Guid guid = WellKnownPermission.ToGuid(permission);
			bool result;
			using (AuthzContextHandle authzContext = AuthzAuthorization.GetAuthzContext(token))
			{
				bool[] array = AuthzAuthorization.CheckExtendedRights(authzContext, securityDescriptor, new Guid[]
				{
					guid
				}, principalSelfSid, AccessMask.ControlAccess);
				result = array[0];
			}
			return result;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000047AC File Offset: 0x000029AC
		public static bool CheckSinglePermission(SecurityIdentifier sid, bool isExchangeWellKnownSid, RawSecurityDescriptor rawSecurityDescriptor, Permission permission)
		{
			return AuthzAuthorization.CheckSinglePermission(sid, isExchangeWellKnownSid, rawSecurityDescriptor, permission, null);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000047B8 File Offset: 0x000029B8
		public static bool CheckSinglePermission(SecurityIdentifier sid, bool isExchangeWellKnownSid, RawSecurityDescriptor rawSecurityDescriptor, Permission permission, SecurityIdentifier principalSelfSid)
		{
			return AuthzAuthorization.CheckSinglePermission(sid, isExchangeWellKnownSid, SecurityDescriptor.FromRawSecurityDescriptor(rawSecurityDescriptor), permission, principalSelfSid);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000047CC File Offset: 0x000029CC
		public static bool CheckSinglePermission(SecurityIdentifier sid, bool isExchangeWellKnownSid, SecurityDescriptor securityDescriptor, Permission permission, SecurityIdentifier principalSelfSid)
		{
			Guid guid = WellKnownPermission.ToGuid(permission);
			bool result;
			using (AuthzContextHandle authzContext = AuthzAuthorization.GetAuthzContext(sid, isExchangeWellKnownSid))
			{
				bool[] array = AuthzAuthorization.CheckExtendedRights(authzContext, securityDescriptor, new Guid[]
				{
					guid
				}, principalSelfSid, AccessMask.ControlAccess);
				result = array[0];
			}
			return result;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004830 File Offset: 0x00002A30
		public static Permission CheckPermissions(IntPtr token, RawSecurityDescriptor rawSecurityDescriptor, SecurityIdentifier principalSelfSid)
		{
			Permission result;
			using (AuthzContextHandle authzContext = AuthzAuthorization.GetAuthzContext(token))
			{
				result = AuthzAuthorization.CheckPermissions(authzContext, rawSecurityDescriptor, principalSelfSid);
			}
			return result;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000486C File Offset: 0x00002A6C
		public static Permission CheckPermissions(SecurityIdentifier sid, RawSecurityDescriptor rawSecurityDescriptor, SecurityIdentifier principalSelfSid)
		{
			Permission result;
			using (AuthzContextHandle authzContext = AuthzAuthorization.GetAuthzContext(sid, true))
			{
				result = AuthzAuthorization.CheckPermissions(authzContext, rawSecurityDescriptor, principalSelfSid);
			}
			return result;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000048A8 File Offset: 0x00002AA8
		internal static AuthzContextHandle GetAuthzContext(IntPtr token)
		{
			AuthzContextHandle result = null;
			if (!NativeMethods.AuthzInitializeContextFromToken(AuthzFlags.Default, token, AuthzAuthorization.ResourceManagerHandle, IntPtr.Zero, default(NativeMethods.AuthzLuid), IntPtr.Zero, out result))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			return result;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000048E8 File Offset: 0x00002AE8
		internal static AuthzContextHandle GetAuthzContext(SecurityIdentifier securityIdentifier, bool skipTokenGroup)
		{
			AuthzContextHandle result = null;
			byte[] array = new byte[securityIdentifier.BinaryLength];
			securityIdentifier.GetBinaryForm(array, 0);
			if (!NativeMethods.AuthzInitializeContextFromSid(skipTokenGroup ? AuthzFlags.AuthzSkipTokenGroups : AuthzFlags.Default, array, AuthzAuthorization.ResourceManagerHandle, IntPtr.Zero, default(NativeMethods.AuthzLuid), IntPtr.Zero, out result))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			return result;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004944 File Offset: 0x00002B44
		private static GCHandle GetSidAndAttributes(Dictionary<SecurityIdentifier, uint> groups, out NativeMethods.SidAndAttributes[] sidAndAttributes)
		{
			sidAndAttributes = null;
			if (groups == null || groups.Count == 0)
			{
				return default(GCHandle);
			}
			bool flag = false;
			byte[] array = new byte[AuthzAuthorization.GetSidsSize(groups)];
			sidAndAttributes = new NativeMethods.SidAndAttributes[groups.Count];
			int num = 0;
			int num2 = 0;
			GCHandle gchandle = default(GCHandle);
			GCHandle result;
			try
			{
				gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
				foreach (KeyValuePair<SecurityIdentifier, uint> keyValuePair in groups)
				{
					keyValuePair.Key.GetBinaryForm(array, num);
					sidAndAttributes[num2].Sid = Marshal.UnsafeAddrOfPinnedArrayElement(array, num);
					num += keyValuePair.Key.BinaryLength;
					sidAndAttributes[num2].Attributes = (keyValuePair.Value & 20U);
					num2++;
				}
				flag = true;
				result = gchandle;
			}
			finally
			{
				if (!flag && gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			return result;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004A4C File Offset: 0x00002C4C
		private static int GetSidsSize(Dictionary<SecurityIdentifier, uint> groups)
		{
			int num = 0;
			if (groups != null)
			{
				foreach (SecurityIdentifier securityIdentifier in groups.Keys)
				{
					num += securityIdentifier.BinaryLength;
				}
			}
			return num;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004AA8 File Offset: 0x00002CA8
		internal static AuthzContextHandle AddSidsToContext(AuthzContextHandle authzContextHandle, Dictionary<SecurityIdentifier, uint> regularGroups, Dictionary<SecurityIdentifier, uint> restrictedGroups)
		{
			GCHandle gchandle = default(GCHandle);
			GCHandle gchandle2 = default(GCHandle);
			AuthzContextHandle authzContextHandle2 = null;
			AuthzContextHandle result;
			try
			{
				NativeMethods.SidAndAttributes[] groupSids = null;
				NativeMethods.SidAndAttributes[] restrictedSids = null;
				gchandle = AuthzAuthorization.GetSidAndAttributes(regularGroups, out groupSids);
				gchandle2 = AuthzAuthorization.GetSidAndAttributes(restrictedGroups, out restrictedSids);
				if (!NativeMethods.AuthzAddSidsToContext(authzContextHandle, groupSids, (uint)regularGroups.Count, restrictedSids, (uint)restrictedGroups.Count, out authzContextHandle2))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
				result = authzContextHandle2;
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (gchandle2.IsAllocated)
				{
					gchandle2.Free();
				}
				if (false)
				{
					authzContextHandle2.Dispose();
					authzContextHandle2 = null;
				}
			}
			return result;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004B4C File Offset: 0x00002D4C
		internal static bool[] CheckExtendedRights(AuthzContextHandle authzContextHandle, RawSecurityDescriptor rawSecurityDescriptor, Guid[] extendedRightGuids, SecurityIdentifier principalSelfSid)
		{
			return AuthzAuthorization.CheckExtendedRights(authzContextHandle, rawSecurityDescriptor, extendedRightGuids, principalSelfSid, AccessMask.ControlAccess);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004B5C File Offset: 0x00002D5C
		internal static bool[] CheckExtendedRights(AuthzContextHandle authzContextHandle, RawSecurityDescriptor rawSecurityDescriptor, Guid[] extendedRightGuids, SecurityIdentifier principalSelfSid, AccessMask accessMask)
		{
			if (rawSecurityDescriptor == null)
			{
				throw new ArgumentNullException("rawSecurityDescriptor");
			}
			return AuthzAuthorization.CheckExtendedRights(authzContextHandle, SecurityDescriptor.FromRawSecurityDescriptor(rawSecurityDescriptor), extendedRightGuids, principalSelfSid, accessMask);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004B7C File Offset: 0x00002D7C
		internal static bool[] CheckExtendedRights(AuthzContextHandle authzContextHandle, SecurityDescriptor securityDescriptor, Guid[] extendedRightGuids, SecurityIdentifier principalSelfSid, AccessMask accessMask)
		{
			if (securityDescriptor == null)
			{
				throw new ArgumentNullException("securityDescriptorBlob");
			}
			SafeHGlobalHandle safeHGlobalHandle = null;
			SafeHGlobalHandle safeHGlobalHandle2 = null;
			bool[] results;
			try
			{
				safeHGlobalHandle = AccessRequest.AllocateNativeStruct(accessMask, extendedRightGuids, principalSelfSid);
				safeHGlobalHandle2 = AccessReply.AllocateNativeStruct(extendedRightGuids.GetLength(0));
				if (!NativeMethods.AuthzAccessCheck(0U, authzContextHandle, safeHGlobalHandle, IntPtr.Zero, securityDescriptor.BinaryForm, IntPtr.Zero, 0U, safeHGlobalHandle2, IntPtr.Zero))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
				AccessReply accessReply = AccessReply.Create(safeHGlobalHandle2, accessMask);
				results = accessReply.Results;
			}
			finally
			{
				if (safeHGlobalHandle != null)
				{
					safeHGlobalHandle.Dispose();
				}
				if (safeHGlobalHandle2 != null)
				{
					safeHGlobalHandle2.Dispose();
				}
			}
			return results;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004C18 File Offset: 0x00002E18
		private static Permission CheckPermissions(AuthzContextHandle authzContextHandle, RawSecurityDescriptor rawSecurityDescriptor, SecurityIdentifier principalSelfSid)
		{
			bool[] array = AuthzAuthorization.CheckExtendedRights(authzContextHandle, rawSecurityDescriptor, AuthzAuthorization.allExtendedRightGuids, principalSelfSid);
			Permission permission = Permission.None;
			for (int i = 1; i < array.Length; i++)
			{
				if (array[i])
				{
					permission |= AuthzAuthorization.allExtendedRights[i];
				}
			}
			return permission;
		}

		// Token: 0x0400009D RID: 157
		private const string ResourceManagerName = "Authz Resource Manager";

		// Token: 0x0400009E RID: 158
		private static Guid[] allExtendedRightGuids = AuthzAuthorization.AllExtendedRights();

		// Token: 0x0400009F RID: 159
		private static Permission[] allExtendedRights = AuthzAuthorization.ReverseMap(AuthzAuthorization.allExtendedRightGuids);

		// Token: 0x040000A0 RID: 160
		private static ResourceManagerHandle resourceManagerHandle = ResourceManagerHandle.Create("Authz Resource Manager");
	}
}
