using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Common;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x02000023 RID: 35
	public sealed class ClientSecurityContext : DisposeTrackableBase
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00004CC0 File Offset: 0x00002EC0
		public ClientSecurityContext(WindowsIdentity identity) : this(identity, true)
		{
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004CCC File Offset: 0x00002ECC
		public ClientSecurityContext(WindowsIdentity identity, bool retainIdentity)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				if (identity == null)
				{
					throw new ArgumentNullException("identity");
				}
				this.identity = identity;
				this.InitializeContextFromIdentity();
				disposeGuard.Success();
			}
			if (!retainIdentity)
			{
				this.identity = null;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004D34 File Offset: 0x00002F34
		public ClientSecurityContext(IntPtr authenticatedUserHandle) : this(new AuthzContextHandle(authenticatedUserHandle))
		{
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004D42 File Offset: 0x00002F42
		internal ClientSecurityContext(AuthzContextHandle authZContextHandle)
		{
			this.clientContextHandle = authZContextHandle;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004D51 File Offset: 0x00002F51
		public ClientSecurityContext(ISecurityAccessToken securityAccessToken) : this(securityAccessToken, AuthzFlags.Default)
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004D5C File Offset: 0x00002F5C
		public static ClientSecurityContext DuplicateAuthZContextHandle(IntPtr clientContextHandle)
		{
			if (clientContextHandle == IntPtr.Zero)
			{
				throw new InvalidOperationException(NetException.NoTokenContext);
			}
			AuthzContextHandle authZContextHandle = null;
			NativeMethods.AuthzLuid identifier = default(NativeMethods.AuthzLuid);
			identifier.LowPart = 0U;
			identifier.HighPart = 0;
			if (!NativeMethods.AuthzInitializeContextFromAuthzContext(AuthzFlags.Default, clientContextHandle, IntPtr.Zero, identifier, IntPtr.Zero, out authZContextHandle))
			{
				Exception innerException = new Win32Exception(Marshal.GetLastWin32Error());
				throw new AuthzException(NetException.AuthzInitializeContextFromDuplicateAuthZFailed, innerException);
			}
			return new ClientSecurityContext(authZContextHandle);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004DDC File Offset: 0x00002FDC
		public ClientSecurityContext(ISecurityAccessToken securityAccessToken, AuthzFlags flags)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				if (securityAccessToken == null)
				{
					throw new ArgumentNullException("securityAccessToken");
				}
				this.securityAccessToken = new ClientSecurityContext.LazyInitSecurityAccessTokenEx(securityAccessToken);
				this.InitializeContextFromSecurityAccessToken(flags);
				disposeGuard.Success();
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004E40 File Offset: 0x00003040
		public ClientSecurityContext(ISecurityAccessTokenEx securityAccessToken, AuthzFlags flags)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				if (securityAccessToken == null)
				{
					throw new ArgumentNullException("securityAccessToken");
				}
				this.securityAccessToken = securityAccessToken;
				this.InitializeContextFromSecurityAccessToken(flags);
				disposeGuard.Success();
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004EA0 File Offset: 0x000030A0
		internal WindowsIdentity Identity
		{
			get
			{
				base.CheckDisposed();
				return this.identity;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004EAE File Offset: 0x000030AE
		internal AuthzContextHandle ClientContextHandle
		{
			get
			{
				base.CheckDisposed();
				return this.clientContextHandle;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004EBC File Offset: 0x000030BC
		public SecurityIdentifier UserSid
		{
			get
			{
				base.CheckDisposed();
				if (this.identity != null)
				{
					return this.identity.User;
				}
				if (this.securityAccessToken != null)
				{
					return this.securityAccessToken.UserSid;
				}
				if (this.clientContextHandle != null)
				{
					if (this.userSid == null)
					{
						this.userSid = this.GetUserSid();
					}
					return this.userSid;
				}
				throw new InvalidOperationException(NetException.NoContext);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004F30 File Offset: 0x00003130
		public SecurityIdentifier PrimaryGroupSid
		{
			get
			{
				SecurityIdentifier result = null;
				base.CheckDisposed();
				if (this.identity != null)
				{
					result = (this.identity.Groups.FirstOrDefault<IdentityReference>() as SecurityIdentifier);
				}
				else if (this.securityAccessToken != null)
				{
					SidBinaryAndAttributes[] groupSids = this.securityAccessToken.GroupSids;
					if (groupSids != null && groupSids.Length != 0)
					{
						result = groupSids[0].SecurityIdentifier;
					}
				}
				else
				{
					if (this.clientContextHandle == null)
					{
						throw new InvalidOperationException(NetException.NoContext);
					}
					NativeMethods.SecurityIdentifierAndAttributes[] array = NativeMethods.AuthzGetInformationFromContextTokenGroup(this.clientContextHandle);
					if (array != null && array.Length != 0)
					{
						result = array[0].sid;
					}
				}
				return result;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004FC8 File Offset: 0x000031C8
		public bool IsAnonymous
		{
			get
			{
				base.CheckDisposed();
				if (this.identity != null)
				{
					return this.identity.IsAnonymous;
				}
				return !(this.UserSid != null) || this.UserSid.IsWellKnown(WellKnownSidType.AnonymousSid);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005004 File Offset: 0x00003204
		public bool IsSystem
		{
			get
			{
				base.CheckDisposed();
				if (this.identity != null)
				{
					return this.identity.IsSystem;
				}
				return this.UserSid != null && (this.UserSid.IsWellKnown(WellKnownSidType.NTAuthoritySid) || this.UserSid.IsWellKnown(WellKnownSidType.LocalSystemSid));
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005058 File Offset: 0x00003258
		public bool IsAuthenticated
		{
			get
			{
				base.CheckDisposed();
				if (this.identity != null)
				{
					return this.identity.IsAuthenticated;
				}
				return !this.IsAnonymous && !this.IsGuest;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005088 File Offset: 0x00003288
		public bool IsGuest
		{
			get
			{
				base.CheckDisposed();
				if (this.isGuest == null)
				{
					if (this.identity != null)
					{
						this.isGuest = new bool?(this.identity.IsGuest);
					}
					else if (this.UserSid == null)
					{
						this.isGuest = new bool?(false);
					}
					else if (this.UserSid.IsWellKnown(WellKnownSidType.BuiltinGuestsSid) || this.UserSid.IsWellKnown(WellKnownSidType.AccountGuestSid))
					{
						this.isGuest = new bool?(true);
					}
					else
					{
						int binaryLength = this.UserSid.BinaryLength;
						if (binaryLength < 4)
						{
							this.isGuest = new bool?(false);
						}
						else
						{
							byte[] array = new byte[binaryLength];
							this.UserSid.GetBinaryForm(array, 0);
							int num = binaryLength - 4;
							int num2 = (int)array[num] << 24 | (int)array[num + 1] << 16 | (int)array[num + 2] << 8 | (int)array[num + 3];
							this.isGuest = new bool?(num2 == 501);
						}
					}
				}
				return this.isGuest.Value;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000518B File Offset: 0x0000338B
		public override void SuppressDisposeTracker()
		{
			if (this.clientContextHandle != null)
			{
				this.clientContextHandle.SuppressDisposeTracker();
			}
			base.SuppressDisposeTracker();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000051A6 File Offset: 0x000033A6
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.clientContextHandle != null && !this.clientContextHandle.IsInvalid)
			{
				this.clientContextHandle.Dispose();
				this.clientContextHandle = null;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000051D2 File Offset: 0x000033D2
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ClientSecurityContext>(this);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000051DC File Offset: 0x000033DC
		public override string ToString()
		{
			string text = string.Empty;
			try
			{
				SecurityIdentifier securityIdentifier = this.UserSid;
				text = ((securityIdentifier == null) ? "<Null Sid>" : securityIdentifier.ToString());
			}
			catch (InvalidSidException)
			{
				text = "<Invalid Sid>";
			}
			return string.Format(CultureInfo.InvariantCulture, "User SID: {0}", new object[]
			{
				text
			});
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005278 File Offset: 0x00003478
		public void SetSecurityAccessToken(ISecurityAccessToken token)
		{
			base.CheckDisposed();
			SecurityAccessTokenEx securityAccessTokenEx = new SecurityAccessTokenEx();
			this.SetSecurityAccessToken(securityAccessTokenEx);
			token.UserSid = securityAccessTokenEx.UserSid.ToString();
			SidStringAndAttributes[] groupSids;
			if (securityAccessTokenEx.GroupSids == null)
			{
				groupSids = null;
			}
			else
			{
				groupSids = (from @group in securityAccessTokenEx.GroupSids
				select new SidStringAndAttributes(@group.SecurityIdentifier.ToString(), @group.Attributes)).ToArray<SidStringAndAttributes>();
			}
			token.GroupSids = groupSids;
			SidStringAndAttributes[] restrictedGroupSids;
			if (securityAccessTokenEx.RestrictedGroupSids == null)
			{
				restrictedGroupSids = null;
			}
			else
			{
				restrictedGroupSids = (from @group in securityAccessTokenEx.RestrictedGroupSids
				select new SidStringAndAttributes(@group.SecurityIdentifier.ToString(), @group.Attributes)).ToArray<SidStringAndAttributes>();
			}
			token.RestrictedGroupSids = restrictedGroupSids;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005328 File Offset: 0x00003528
		public void SetSecurityAccessToken(SecurityAccessTokenEx token)
		{
			base.CheckDisposed();
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			if (this.clientContextHandle == null || this.clientContextHandle.IsInvalid)
			{
				throw new AuthzException(NetException.AuthzUnableToGetTokenFromNullOrInvalidHandle(this.ToString()));
			}
			SecurityIdentifier localMachineAuthoritySid = new SecurityIdentifier(WellKnownSidType.BuiltinDomainSid, null);
			this.SerializeUserSid(token);
			this.SerializeGroupsToken(AuthzContextInformation.GroupSids, token, localMachineAuthoritySid);
			this.SerializeGroupsToken(AuthzContextInformation.RestrictedSids, token, localMachineAuthoritySid);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005396 File Offset: 0x00003596
		public int GetGrantedAccess(RawSecurityDescriptor rawSecurityDescriptor, AccessMask requestedAccess)
		{
			base.CheckDisposed();
			return this.GetGrantedAccess(rawSecurityDescriptor, null, requestedAccess);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000053A7 File Offset: 0x000035A7
		public int GetGrantedAccess(RawSecurityDescriptor rawSecurityDescriptor, SecurityIdentifier principalSelfSid, AccessMask requestedAccess)
		{
			base.CheckDisposed();
			return this.GetGrantedAccess(SecurityDescriptor.FromRawSecurityDescriptor(rawSecurityDescriptor), principalSelfSid, requestedAccess);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000053BD File Offset: 0x000035BD
		public int GetGrantedAccess(SecurityDescriptor securityDescriptor, AccessMask requestedAccess)
		{
			base.CheckDisposed();
			return this.GetGrantedAccess(securityDescriptor, null, requestedAccess);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000053D0 File Offset: 0x000035D0
		public int GetGrantedAccess(SecurityDescriptor securityDescriptor, SecurityIdentifier principalSelfSid, AccessMask requestedAccess)
		{
			base.CheckDisposed();
			if (this.clientContextHandle == null || this.clientContextHandle.IsInvalid)
			{
				throw new AuthzException(NetException.AuthzUnableToDoAccessCheckFromNullOrInvalidHandle);
			}
			SafeHGlobalHandle safeHGlobalHandle = null;
			SafeHGlobalHandle safeHGlobalHandle2 = null;
			int result;
			try
			{
				safeHGlobalHandle = AccessRequest.AllocateNativeStruct(requestedAccess, null, principalSelfSid);
				safeHGlobalHandle2 = AccessReply.AllocateNativeStruct(1);
				if (!NativeMethods.AuthzAccessCheck(0U, this.clientContextHandle, safeHGlobalHandle, IntPtr.Zero, securityDescriptor.BinaryForm, IntPtr.Zero, 0U, safeHGlobalHandle2, IntPtr.Zero))
				{
					Exception innerException = new Win32Exception(Marshal.GetLastWin32Error());
					throw new AuthzException(NetException.AuthzUnableToPerformAccessCheck(this.ToString()), innerException);
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
			return result;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000054B0 File Offset: 0x000036B0
		public IdentityReferenceCollection GetGroups()
		{
			base.CheckDisposed();
			if (this.identity != null)
			{
				return this.identity.Groups;
			}
			if (this.securityAccessToken != null)
			{
				SidBinaryAndAttributes[] groupSids = this.securityAccessToken.GroupSids;
				if (groupSids != null && groupSids.Length != 0)
				{
					IdentityReferenceCollection identityReferenceCollection = new IdentityReferenceCollection(groupSids.Length);
					foreach (SidBinaryAndAttributes sidBinaryAndAttributes in groupSids)
					{
						identityReferenceCollection.Add(sidBinaryAndAttributes.SecurityIdentifier);
					}
					return identityReferenceCollection;
				}
				return new IdentityReferenceCollection();
			}
			else
			{
				if (this.clientContextHandle == null)
				{
					throw new InvalidOperationException(NetException.NoContext);
				}
				NativeMethods.SecurityIdentifierAndAttributes[] array2 = NativeMethods.AuthzGetInformationFromContextTokenGroup(this.clientContextHandle);
				if (array2 != null && array2.Length != 0)
				{
					IdentityReferenceCollection identityReferenceCollection2 = new IdentityReferenceCollection(array2.Length);
					foreach (NativeMethods.SecurityIdentifierAndAttributes securityIdentifierAndAttributes in array2)
					{
						identityReferenceCollection2.Add(securityIdentifierAndAttributes.sid);
					}
					return identityReferenceCollection2;
				}
				return new IdentityReferenceCollection();
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000559E File Offset: 0x0000379E
		public bool HasExtendedRightOnObject(SecurityDescriptor securityDescriptor, Guid extendedRightGuid)
		{
			base.CheckDisposed();
			return this.HasExtendedRightOnObject(securityDescriptor, extendedRightGuid, AccessMask.ControlAccess, null);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000055B4 File Offset: 0x000037B4
		public bool HasExtendedRightOnObject(SecurityDescriptor securityDescriptor, Guid extendedRightGuid, AccessMask accessMask, SecurityIdentifier principalSelfSid)
		{
			base.CheckDisposed();
			bool[] array = AuthzAuthorization.CheckExtendedRights(this.clientContextHandle, securityDescriptor, new Guid[]
			{
				extendedRightGuid
			}, principalSelfSid, accessMask);
			return array[0];
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000055F0 File Offset: 0x000037F0
		public bool AddGroupSids(SidBinaryAndAttributes[] groupSids)
		{
			AuthzContextHandle authzContextHandle = this.clientContextHandle;
			AuthzContextHandle authzContextHandle2 = null;
			NativeMethods.GroupsToken groupsTokenFromGroups = ClientSecurityContext.GetGroupsTokenFromGroups(groupSids);
			bool result;
			try
			{
				bool flag = NativeMethods.AuthzAddSidsToContext(this.clientContextHandle, groupsTokenFromGroups.Groups, groupsTokenFromGroups.GroupCount, null, 0U, out authzContextHandle2);
				if (flag)
				{
					this.clientContextHandle = authzContextHandle2;
					if (authzContextHandle != null && !authzContextHandle.IsClosed)
					{
						authzContextHandle.Dispose();
					}
				}
				result = flag;
			}
			finally
			{
				ClientSecurityContext.ReleaseUnmanagedGroupSidBlocks(ref groupsTokenFromGroups);
			}
			return result;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005668 File Offset: 0x00003868
		private static NativeMethods.GroupsToken GetGroupsTokenFromGroups(SidBinaryAndAttributes[] groups)
		{
			NativeMethods.GroupsToken result = default(NativeMethods.GroupsToken);
			if (groups == null || groups.Length == 0)
			{
				result.GroupCount = 0U;
				result.Groups = null;
			}
			else
			{
				result.GroupCount = (uint)groups.Length;
				result.Groups = new NativeMethods.SidAndAttributes[groups.Length];
				bool flag = false;
				try
				{
					for (int i = 0; i < groups.Length; i++)
					{
						SecurityIdentifier securityIdentifier = groups[i].SecurityIdentifier;
						result.Groups[i] = default(NativeMethods.SidAndAttributes);
						result.Groups[i].Sid = Marshal.AllocHGlobal(securityIdentifier.BinaryLength);
						byte[] array = new byte[securityIdentifier.BinaryLength];
						securityIdentifier.GetBinaryForm(array, 0);
						Marshal.Copy(array, 0, result.Groups[i].Sid, securityIdentifier.BinaryLength);
						uint num = groups[i].Attributes;
						if ((num & 4U) != 0U)
						{
							num = 20U;
						}
						else
						{
							num = 16U;
						}
						result.Groups[i].Attributes = num;
					}
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						ClientSecurityContext.ReleaseUnmanagedGroupSidBlocks(ref result);
					}
				}
			}
			return result;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005788 File Offset: 0x00003988
		private static void ReleaseUnmanagedGroupSidBlocks(ref NativeMethods.GroupsToken groupsToken)
		{
			if (groupsToken.Groups == null)
			{
				return;
			}
			foreach (NativeMethods.SidAndAttributes sidAndAttributes in groupsToken.Groups)
			{
				if (sidAndAttributes.Sid != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(sidAndAttributes.Sid);
				}
			}
			groupsToken.Groups = null;
			groupsToken.GroupCount = 0U;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000057ED File Offset: 0x000039ED
		private void SerializeUserSid(SecurityAccessTokenEx token)
		{
			token.UserSid = this.GetUserSid();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000057FC File Offset: 0x000039FC
		private void SerializeGroupsToken(AuthzContextInformation contextInfo, SecurityAccessTokenEx token, SecurityIdentifier localMachineAuthoritySid)
		{
			using (SafeHGlobalHandle informationFromContext = this.GetInformationFromContext(contextInfo))
			{
				int num = 0;
				int num2 = Marshal.ReadInt32(informationFromContext.DangerousGetHandle(), num);
				num += Marshal.SizeOf(typeof(IntPtr));
				List<SidBinaryAndAttributes> list = null;
				if (num2 > 0)
				{
					list = new List<SidBinaryAndAttributes>(num2);
					for (int i = 0; i < num2; i++)
					{
						SidBinaryAndAttributes sidBinaryAndAttributes = SidBinaryAndAttributes.Read(informationFromContext.DangerousGetHandle(), localMachineAuthoritySid, ref num);
						if (sidBinaryAndAttributes != null)
						{
							list.Add(sidBinaryAndAttributes);
						}
					}
				}
				SidBinaryAndAttributes[] array = (list == null || list.Count == 0) ? null : list.ToArray();
				if (contextInfo == AuthzContextInformation.GroupSids)
				{
					token.GroupSids = array;
				}
				else
				{
					token.RestrictedGroupSids = array;
				}
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000058B4 File Offset: 0x00003AB4
		private SafeHGlobalHandle GetInformationFromContext(AuthzContextInformation contextInfo)
		{
			SafeHGlobalHandle safeHGlobalHandle = SafeHGlobalHandle.InvalidHandle;
			uint num = 0U;
			if (NativeMethods.AuthzGetInformationFromContext(this.clientContextHandle, contextInfo, num, ref num, safeHGlobalHandle))
			{
				throw new AuthzException(NetException.AuthzGetInformationFromContextReturnedSuccessForSize);
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error != 122)
			{
				throw new Win32Exception(lastWin32Error);
			}
			safeHGlobalHandle = NativeMethods.AllocHGlobal((int)num);
			if (!NativeMethods.AuthzGetInformationFromContext(this.clientContextHandle, contextInfo, num, ref num, safeHGlobalHandle))
			{
				Exception innerException = new Win32Exception(Marshal.GetLastWin32Error());
				throw new AuthzException(NetException.AuthzGetInformationFromContextFailed(this.ToString()), innerException);
			}
			return safeHGlobalHandle;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000593C File Offset: 0x00003B3C
		private bool CheckForInvalidGroupSid(AuthzContextHandle tempHandle, SidBinaryAndAttributes group)
		{
			bool flag = true;
			AuthzContextHandle authzContextHandle = null;
			try
			{
				SidBinaryAndAttributes[] groups = new SidBinaryAndAttributes[]
				{
					group
				};
				NativeMethods.GroupsToken groupsToken = default(NativeMethods.GroupsToken);
				groupsToken = ClientSecurityContext.GetGroupsTokenFromGroups(groups);
				flag = NativeMethods.AuthzAddSidsToContext(tempHandle, groupsToken.Groups, groupsToken.GroupCount, null, 0U, out authzContextHandle);
				ClientSecurityContext.ReleaseUnmanagedGroupSidBlocks(ref groupsToken);
				if (!flag)
				{
					ExTraceGlobals.AuthorizationTracer.TraceError<SecurityIdentifier>(0L, "Group Sid {1} is broken.", group.SecurityIdentifier);
				}
			}
			finally
			{
				if (authzContextHandle != null && !authzContextHandle.IsInvalid)
				{
					authzContextHandle.Dispose();
					authzContextHandle = null;
				}
			}
			return flag;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000059D0 File Offset: 0x00003BD0
		private void InitializeContextFromIdentity()
		{
			if (this.identity == null)
			{
				throw new AuthzException(NetException.AuthzIdentityNotSet);
			}
			if (AuthzAuthorization.ResourceManagerHandle == null || AuthzAuthorization.ResourceManagerHandle.IsInvalid)
			{
				throw new ResourceManagerHandleInvalidException(NetException.AuthManagerNotInitialized);
			}
			this.clientContextHandle = null;
			NativeMethods.AuthzLuid identifier = default(NativeMethods.AuthzLuid);
			identifier.LowPart = 0U;
			identifier.HighPart = 0;
			if (!NativeMethods.AuthzInitializeContextFromToken(AuthzFlags.Default, this.identity.Token, AuthzAuthorization.ResourceManagerHandle, IntPtr.Zero, identifier, IntPtr.Zero, out this.clientContextHandle))
			{
				Exception innerException = new Win32Exception(Marshal.GetLastWin32Error());
				throw new AuthzException(NetException.AuthzInitializeContextFromTokenFailed(this.ToString()), innerException);
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005A84 File Offset: 0x00003C84
		private void InitializeContextFromSecurityAccessToken(AuthzFlags flags)
		{
			if (this.securityAccessToken == null)
			{
				throw new InvalidOperationException(NetException.NoTokenContext);
			}
			if (AuthzAuthorization.ResourceManagerHandle == null || AuthzAuthorization.ResourceManagerHandle.IsInvalid)
			{
				throw new ResourceManagerHandleInvalidException(NetException.AuthManagerNotInitialized);
			}
			if (flags == AuthzFlags.AuthzSkipTokenGroups && (this.securityAccessToken.GroupSids == null || this.securityAccessToken.GroupSids.Length == 0))
			{
				throw new MissingPrimaryGroupSidException(NetException.MissingPrimaryGroupSid);
			}
			AuthzContextHandle authzContextHandle = null;
			try
			{
				NativeMethods.AuthzLuid identifier = default(NativeMethods.AuthzLuid);
				SecurityIdentifier securityIdentifier = this.securityAccessToken.UserSid;
				int num = (securityIdentifier == null) ? 0 : securityIdentifier.BinaryLength;
				byte[] binaryForm = new byte[num];
				if (securityIdentifier != null)
				{
					securityIdentifier.GetBinaryForm(binaryForm, 0);
				}
				if (!NativeMethods.AuthzInitializeContextFromSid(flags, binaryForm, AuthzAuthorization.ResourceManagerHandle, IntPtr.Zero, identifier, IntPtr.Zero, out authzContextHandle))
				{
					Exception innerException = new Win32Exception(Marshal.GetLastWin32Error());
					throw new AuthzException(NetException.AuthzInitializeContextFromSidFailed(this.ToString()), innerException);
				}
				if (flags == AuthzFlags.AuthzSkipTokenGroups)
				{
					AuthzContextHandle authzContextHandle2 = null;
					NativeMethods.GroupsToken groupsToken = default(NativeMethods.GroupsToken);
					NativeMethods.GroupsToken groupsTokenFromGroups = ClientSecurityContext.GetGroupsTokenFromGroups(this.securityAccessToken.GroupSids);
					try
					{
						groupsToken = ClientSecurityContext.GetGroupsTokenFromGroups(this.securityAccessToken.RestrictedGroupSids);
						if (this.clientContextHandle != null && !this.clientContextHandle.IsInvalid)
						{
							this.clientContextHandle.Dispose();
							this.clientContextHandle = null;
						}
						if (!NativeMethods.AuthzAddSidsToContext(authzContextHandle, groupsTokenFromGroups.Groups, groupsTokenFromGroups.GroupCount, groupsToken.Groups, groupsToken.GroupCount, out authzContextHandle2))
						{
							Exception innerException2 = new Win32Exception(Marshal.GetLastWin32Error());
							if (ExTraceGlobals.AuthorizationTracer.IsTraceEnabled(TraceType.ErrorTrace))
							{
								ExTraceGlobals.CertificateValidationTracer.TraceError(0L, "AuthzAddSidsToContext failed.");
								int num2 = 0;
								while ((long)num2 < (long)((ulong)groupsTokenFromGroups.GroupCount))
								{
									if (!this.CheckForInvalidGroupSid(authzContextHandle, this.securityAccessToken.GroupSids[num2]))
									{
										ExTraceGlobals.AuthorizationTracer.TraceError<int>(0L, "{0}: Group Sid in token is invalid.", num2);
										break;
									}
									num2++;
								}
								int num3 = 0;
								while ((long)num3 < (long)((ulong)groupsToken.GroupCount))
								{
									if (!this.CheckForInvalidGroupSid(authzContextHandle, this.securityAccessToken.RestrictedGroupSids[num3]))
									{
										ExTraceGlobals.AuthorizationTracer.TraceError<int>(0L, "{0}: Restricted Group Sid in token is invalid.", num3);
										break;
									}
									num3++;
								}
							}
							throw new AuthzException(NetException.AuthzAddSidsToContextFailed(this.ToString()), innerException2);
						}
					}
					finally
					{
						ClientSecurityContext.ReleaseUnmanagedGroupSidBlocks(ref groupsTokenFromGroups);
						ClientSecurityContext.ReleaseUnmanagedGroupSidBlocks(ref groupsToken);
					}
					this.clientContextHandle = authzContextHandle2;
				}
				else
				{
					this.clientContextHandle = authzContextHandle;
				}
			}
			finally
			{
				if (authzContextHandle != null && (flags == AuthzFlags.AuthzSkipTokenGroups || this.clientContextHandle == null))
				{
					authzContextHandle.Dispose();
					authzContextHandle = null;
				}
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005D50 File Offset: 0x00003F50
		public ClientSecurityContext Clone()
		{
			base.CheckDisposed();
			if (this.ClientContextHandle != null && !this.ClientContextHandle.IsInvalid)
			{
				return ClientSecurityContext.DuplicateAuthZContextHandle(this.clientContextHandle.DangerousGetHandle());
			}
			throw new AuthzException(NetException.AuthzUnableToDoAccessCheckFromNullOrInvalidHandle);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005D90 File Offset: 0x00003F90
		private SecurityIdentifier GetUserSid()
		{
			SecurityIdentifier result;
			using (SafeHGlobalHandle informationFromContext = this.GetInformationFromContext(AuthzContextInformation.UserSid))
			{
				IntPtr binaryForm = Marshal.ReadIntPtr(informationFromContext.DangerousGetHandle(), 0);
				SecurityIdentifier securityIdentifier = new SecurityIdentifier(binaryForm);
				result = securityIdentifier;
			}
			return result;
		}

		// Token: 0x040000A1 RID: 161
		internal const string EveryoneIdentity = "S-1-1-0";

		// Token: 0x040000A2 RID: 162
		private WindowsIdentity identity;

		// Token: 0x040000A3 RID: 163
		private AuthzContextHandle clientContextHandle;

		// Token: 0x040000A4 RID: 164
		private ISecurityAccessTokenEx securityAccessToken;

		// Token: 0x040000A5 RID: 165
		private SecurityIdentifier userSid;

		// Token: 0x040000A6 RID: 166
		private bool? isGuest;

		// Token: 0x040000A7 RID: 167
		internal static readonly SidStringAndAttributes[] DisabledEveryoneOnlySidStringAndAttributesArray = new SidStringAndAttributes[]
		{
			new SidStringAndAttributes("S-1-1-0", 0U)
		};

		// Token: 0x040000A8 RID: 168
		internal static readonly ClientSecurityContext FreeBusyPermissionDefaultClientSecurityContext = new ClientSecurityContext(new SecurityAccessToken
		{
			UserSid = "S-1-1-0",
			GroupSids = ClientSecurityContext.DisabledEveryoneOnlySidStringAndAttributesArray
		}, AuthzFlags.AuthzSkipTokenGroups);

		// Token: 0x02000025 RID: 37
		private class LazyInitSecurityAccessTokenEx : ISecurityAccessTokenEx
		{
			// Token: 0x060000F7 RID: 247 RVA: 0x00005E28 File Offset: 0x00004028
			public LazyInitSecurityAccessTokenEx(ISecurityAccessToken securityAccessToken)
			{
				this.securityAccessToken = securityAccessToken;
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005E37 File Offset: 0x00004037
			SecurityIdentifier ISecurityAccessTokenEx.UserSid
			{
				get
				{
					if (this.userSid == null)
					{
						this.userSid = ClientSecurityContext.LazyInitSecurityAccessTokenEx.SidFromString(this.securityAccessToken.UserSid);
					}
					return this.userSid;
				}
			}

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005E63 File Offset: 0x00004063
			SidBinaryAndAttributes[] ISecurityAccessTokenEx.GroupSids
			{
				get
				{
					if (this.groupSids == null)
					{
						this.groupSids = ClientSecurityContext.LazyInitSecurityAccessTokenEx.TranslateGroup(this.securityAccessToken.GroupSids);
					}
					return this.groupSids;
				}
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060000FA RID: 250 RVA: 0x00005E89 File Offset: 0x00004089
			SidBinaryAndAttributes[] ISecurityAccessTokenEx.RestrictedGroupSids
			{
				get
				{
					if (this.restrictedGroupSids == null)
					{
						this.restrictedGroupSids = ClientSecurityContext.LazyInitSecurityAccessTokenEx.TranslateGroup(this.securityAccessToken.RestrictedGroupSids);
					}
					return this.restrictedGroupSids;
				}
			}

			// Token: 0x060000FB RID: 251 RVA: 0x00005EB0 File Offset: 0x000040B0
			private static SidBinaryAndAttributes[] TranslateGroup(SidStringAndAttributes[] input)
			{
				if (input == null)
				{
					return null;
				}
				SidBinaryAndAttributes[] array = new SidBinaryAndAttributes[input.Length];
				for (int num = 0; num != input.Length; num++)
				{
					array[num] = new SidBinaryAndAttributes(ClientSecurityContext.LazyInitSecurityAccessTokenEx.SidFromString(input[num].SecurityIdentifier), input[num].Attributes);
				}
				return array;
			}

			// Token: 0x060000FC RID: 252 RVA: 0x00005EF8 File Offset: 0x000040F8
			private static SecurityIdentifier SidFromString(string sidString)
			{
				if (string.IsNullOrEmpty(sidString))
				{
					throw new InvalidSidException(sidString);
				}
				SecurityIdentifier result;
				try
				{
					result = new SecurityIdentifier(sidString);
				}
				catch (ArgumentException innerException)
				{
					throw new InvalidSidException(sidString, innerException);
				}
				return result;
			}

			// Token: 0x040000AB RID: 171
			private readonly ISecurityAccessToken securityAccessToken;

			// Token: 0x040000AC RID: 172
			private SecurityIdentifier userSid;

			// Token: 0x040000AD RID: 173
			private SidBinaryAndAttributes[] groupSids;

			// Token: 0x040000AE RID: 174
			private SidBinaryAndAttributes[] restrictedGroupSids;
		}
	}
}
