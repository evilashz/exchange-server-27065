using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Principal
{
	// Token: 0x020002FF RID: 767
	[ComVisible(true)]
	[Serializable]
	public class WindowsIdentity : ClaimsIdentity, ISerializable, IDeserializationCallback, IDisposable
	{
		// Token: 0x06002780 RID: 10112 RVA: 0x000904D5 File Offset: 0x0008E6D5
		[SecurityCritical]
		private WindowsIdentity() : base(null, null, null, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid")
		{
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x00090512 File Offset: 0x0008E712
		[SecurityCritical]
		internal WindowsIdentity(SafeAccessTokenHandle safeTokenHandle) : this(safeTokenHandle.DangerousGetHandle(), null, -1)
		{
			GC.KeepAlive(safeTokenHandle);
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x00090528 File Offset: 0x0008E728
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public WindowsIdentity(IntPtr userToken) : this(userToken, null, -1)
		{
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x00090533 File Offset: 0x0008E733
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public WindowsIdentity(IntPtr userToken, string type) : this(userToken, type, -1)
		{
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x0009053E File Offset: 0x0008E73E
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType) : this(userToken, type, -1)
		{
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x00090549 File Offset: 0x0008E749
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType, bool isAuthenticated) : this(userToken, type, isAuthenticated ? 1 : 0)
		{
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x0009055C File Offset: 0x0008E75C
		[SecurityCritical]
		private WindowsIdentity(IntPtr userToken, string authType, int isAuthenticated) : base(null, null, null, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid")
		{
			this.CreateFromToken(userToken);
			this.m_authType = authType;
			this.m_isAuthenticated = isAuthenticated;
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x000905BC File Offset: 0x0008E7BC
		[SecurityCritical]
		private void CreateFromToken(IntPtr userToken)
		{
			if (userToken == IntPtr.Zero)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_TokenZero"));
			}
			uint num = (uint)Marshal.SizeOf(typeof(uint));
			bool tokenInformation = Win32Native.GetTokenInformation(userToken, 8U, SafeLocalAllocHandle.InvalidHandle, 0U, out num);
			if (Marshal.GetLastWin32Error() == 6)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidImpersonationToken"));
			}
			if (!Win32Native.DuplicateHandle(Win32Native.GetCurrentProcess(), userToken, Win32Native.GetCurrentProcess(), ref this.m_safeTokenHandle, 0U, true, 2U))
			{
				throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
			}
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x00090649 File Offset: 0x0008E849
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		public WindowsIdentity(string sUserPrincipalName) : this(sUserPrincipalName, null)
		{
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x00090654 File Offset: 0x0008E854
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		public WindowsIdentity(string sUserPrincipalName, string type) : base(null, null, null, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid")
		{
			WindowsIdentity.KerbS4ULogon(sUserPrincipalName, ref this.m_safeTokenHandle);
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000906A9 File Offset: 0x0008E8A9
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public WindowsIdentity(SerializationInfo info, StreamingContext context) : this(info)
		{
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x000906B4 File Offset: 0x0008E8B4
		[SecurityCritical]
		private WindowsIdentity(SerializationInfo info) : base(info)
		{
			this.m_claimsInitialized = false;
			IntPtr intPtr = (IntPtr)info.GetValue("m_userToken", typeof(IntPtr));
			if (intPtr != IntPtr.Zero)
			{
				this.CreateFromToken(intPtr);
			}
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x00090728 File Offset: 0x0008E928
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("m_userToken", this.m_safeTokenHandle.DangerousGetHandle());
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x0009074D File Offset: 0x0008E94D
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x0009074F File Offset: 0x0008E94F
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		public static WindowsIdentity GetCurrent()
		{
			return WindowsIdentity.GetCurrentInternal(TokenAccessLevels.MaximumAllowed, false);
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x0009075C File Offset: 0x0008E95C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		public static WindowsIdentity GetCurrent(bool ifImpersonating)
		{
			return WindowsIdentity.GetCurrentInternal(TokenAccessLevels.MaximumAllowed, ifImpersonating);
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x00090769 File Offset: 0x0008E969
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		public static WindowsIdentity GetCurrent(TokenAccessLevels desiredAccess)
		{
			return WindowsIdentity.GetCurrentInternal(desiredAccess, false);
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x00090772 File Offset: 0x0008E972
		[SecuritySafeCritical]
		public static WindowsIdentity GetAnonymous()
		{
			return new WindowsIdentity();
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06002792 RID: 10130 RVA: 0x0009077C File Offset: 0x0008E97C
		public sealed override string AuthenticationType
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_safeTokenHandle.IsInvalid)
				{
					return string.Empty;
				}
				if (this.m_authType == null)
				{
					Win32Native.LUID logonAuthId = WindowsIdentity.GetLogonAuthId(this.m_safeTokenHandle);
					if (logonAuthId.LowPart == 998U)
					{
						return string.Empty;
					}
					SafeLsaReturnBufferHandle invalidHandle = SafeLsaReturnBufferHandle.InvalidHandle;
					try
					{
						int num = Win32Native.LsaGetLogonSessionData(ref logonAuthId, ref invalidHandle);
						if (num < 0)
						{
							throw WindowsIdentity.GetExceptionFromNtStatus(num);
						}
						invalidHandle.Initialize((ulong)Marshal.SizeOf(typeof(Win32Native.SECURITY_LOGON_SESSION_DATA)));
						Win32Native.SECURITY_LOGON_SESSION_DATA security_LOGON_SESSION_DATA = invalidHandle.Read<Win32Native.SECURITY_LOGON_SESSION_DATA>(0UL);
						return Marshal.PtrToStringUni(security_LOGON_SESSION_DATA.AuthenticationPackage.Buffer);
					}
					finally
					{
						if (!invalidHandle.IsInvalid)
						{
							invalidHandle.Dispose();
						}
					}
				}
				return this.m_authType;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06002793 RID: 10131 RVA: 0x0009083C File Offset: 0x0008EA3C
		[ComVisible(false)]
		public TokenImpersonationLevel ImpersonationLevel
		{
			[SecuritySafeCritical]
			get
			{
				if (!this.m_impersonationLevelInitialized)
				{
					TokenImpersonationLevel impersonationLevel;
					if (this.m_safeTokenHandle.IsInvalid)
					{
						impersonationLevel = TokenImpersonationLevel.Anonymous;
					}
					else
					{
						TokenType tokenInformation = (TokenType)this.GetTokenInformation<int>(TokenInformationClass.TokenType);
						if (tokenInformation == TokenType.TokenPrimary)
						{
							impersonationLevel = TokenImpersonationLevel.None;
						}
						else
						{
							int tokenInformation2 = this.GetTokenInformation<int>(TokenInformationClass.TokenImpersonationLevel);
							impersonationLevel = tokenInformation2 + TokenImpersonationLevel.Anonymous;
						}
					}
					this.m_impersonationLevel = impersonationLevel;
					this.m_impersonationLevelInitialized = true;
				}
				return this.m_impersonationLevel;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06002794 RID: 10132 RVA: 0x0009089D File Offset: 0x0008EA9D
		public override bool IsAuthenticated
		{
			get
			{
				if (this.m_isAuthenticated == -1)
				{
					this.m_isAuthenticated = (this.CheckNtTokenForSid(new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[]
					{
						11
					})) ? 1 : 0);
				}
				return this.m_isAuthenticated == 1;
			}
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x000908D8 File Offset: 0x0008EAD8
		[SecuritySafeCritical]
		[ComVisible(false)]
		private bool CheckNtTokenForSid(SecurityIdentifier sid)
		{
			if (this.m_safeTokenHandle.IsInvalid)
			{
				return false;
			}
			SafeAccessTokenHandle invalidHandle = SafeAccessTokenHandle.InvalidHandle;
			TokenImpersonationLevel impersonationLevel = this.ImpersonationLevel;
			bool result = false;
			try
			{
				if (impersonationLevel == TokenImpersonationLevel.None && !Win32Native.DuplicateTokenEx(this.m_safeTokenHandle, 8U, IntPtr.Zero, 2U, 2U, ref invalidHandle))
				{
					throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
				}
				if (!Win32Native.CheckTokenMembership((impersonationLevel != TokenImpersonationLevel.None) ? this.m_safeTokenHandle : invalidHandle, sid.BinaryForm, ref result))
				{
					throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
				}
			}
			finally
			{
				if (invalidHandle != SafeAccessTokenHandle.InvalidHandle)
				{
					invalidHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06002796 RID: 10134 RVA: 0x0009097C File Offset: 0x0008EB7C
		public virtual bool IsGuest
		{
			[SecuritySafeCritical]
			get
			{
				return !this.m_safeTokenHandle.IsInvalid && this.CheckNtTokenForSid(new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[]
				{
					32,
					546
				}));
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06002797 RID: 10135 RVA: 0x000909B0 File Offset: 0x0008EBB0
		public virtual bool IsSystem
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_safeTokenHandle.IsInvalid)
				{
					return false;
				}
				SecurityIdentifier right = new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[]
				{
					18
				});
				return this.User == right;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06002798 RID: 10136 RVA: 0x000909EC File Offset: 0x0008EBEC
		public virtual bool IsAnonymous
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_safeTokenHandle.IsInvalid)
				{
					return true;
				}
				SecurityIdentifier right = new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[]
				{
					7
				});
				return this.User == right;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06002799 RID: 10137 RVA: 0x00090A26 File Offset: 0x0008EC26
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				return this.GetName();
			}
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x00090A30 File Offset: 0x0008EC30
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal string GetName()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (this.m_safeTokenHandle.IsInvalid)
			{
				return string.Empty;
			}
			if (this.m_name == null)
			{
				using (WindowsIdentity.SafeRevertToSelf(ref stackCrawlMark))
				{
					NTAccount ntaccount = this.User.Translate(typeof(NTAccount)) as NTAccount;
					this.m_name = ntaccount.ToString();
				}
			}
			return this.m_name;
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600279B RID: 10139 RVA: 0x00090AAC File Offset: 0x0008ECAC
		[ComVisible(false)]
		public SecurityIdentifier Owner
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_safeTokenHandle.IsInvalid)
				{
					return null;
				}
				if (this.m_owner == null)
				{
					using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenOwner))
					{
						this.m_owner = new SecurityIdentifier(tokenInformation.Read<IntPtr>(0UL), true);
					}
				}
				return this.m_owner;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x00090B1C File Offset: 0x0008ED1C
		[ComVisible(false)]
		public SecurityIdentifier User
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_safeTokenHandle.IsInvalid)
				{
					return null;
				}
				if (this.m_user == null)
				{
					using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenUser))
					{
						this.m_user = new SecurityIdentifier(tokenInformation.Read<IntPtr>(0UL), true);
					}
				}
				return this.m_user;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600279D RID: 10141 RVA: 0x00090B8C File Offset: 0x0008ED8C
		public IdentityReferenceCollection Groups
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_safeTokenHandle.IsInvalid)
				{
					return null;
				}
				if (this.m_groups == null)
				{
					IdentityReferenceCollection identityReferenceCollection = new IdentityReferenceCollection();
					using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenGroups))
					{
						uint num = tokenInformation.Read<uint>(0UL);
						if (num != 0U)
						{
							Win32Native.TOKEN_GROUPS token_GROUPS = tokenInformation.Read<Win32Native.TOKEN_GROUPS>(0UL);
							Win32Native.SID_AND_ATTRIBUTES[] array = new Win32Native.SID_AND_ATTRIBUTES[token_GROUPS.GroupCount];
							tokenInformation.ReadArray<Win32Native.SID_AND_ATTRIBUTES>((ulong)Marshal.OffsetOf(typeof(Win32Native.TOKEN_GROUPS), "Groups").ToInt32(), array, 0, array.Length);
							foreach (Win32Native.SID_AND_ATTRIBUTES sid_AND_ATTRIBUTES in array)
							{
								uint num2 = 3221225492U;
								if ((sid_AND_ATTRIBUTES.Attributes & num2) == 4U)
								{
									identityReferenceCollection.Add(new SecurityIdentifier(sid_AND_ATTRIBUTES.Sid, true));
								}
							}
						}
					}
					Interlocked.CompareExchange(ref this.m_groups, identityReferenceCollection, null);
				}
				return this.m_groups as IdentityReferenceCollection;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600279E RID: 10142 RVA: 0x00090C94 File Offset: 0x0008EE94
		public virtual IntPtr Token
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this.m_safeTokenHandle.DangerousGetHandle();
			}
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x00090CA4 File Offset: 0x0008EEA4
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void RunImpersonated(SafeAccessTokenHandle safeAccessTokenHandle, Action action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			WindowsIdentity wi = null;
			if (!safeAccessTokenHandle.IsInvalid)
			{
				wi = new WindowsIdentity(safeAccessTokenHandle);
			}
			using (WindowsIdentity.SafeImpersonate(safeAccessTokenHandle, wi, ref stackCrawlMark))
			{
				action();
			}
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x00090D00 File Offset: 0x0008EF00
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static T RunImpersonated<T>(SafeAccessTokenHandle safeAccessTokenHandle, Func<T> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			WindowsIdentity wi = null;
			if (!safeAccessTokenHandle.IsInvalid)
			{
				wi = new WindowsIdentity(safeAccessTokenHandle);
			}
			T result = default(T);
			using (WindowsIdentity.SafeImpersonate(safeAccessTokenHandle, wi, ref stackCrawlMark))
			{
				result = func();
			}
			return result;
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x00090D64 File Offset: 0x0008EF64
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public virtual WindowsImpersonationContext Impersonate()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.Impersonate(ref stackCrawlMark);
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x00090D7C File Offset: 0x0008EF7C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.ControlPrincipal))]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static WindowsImpersonationContext Impersonate(IntPtr userToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (userToken == IntPtr.Zero)
			{
				return WindowsIdentity.SafeRevertToSelf(ref stackCrawlMark);
			}
			WindowsIdentity windowsIdentity = new WindowsIdentity(userToken, null, -1);
			return windowsIdentity.Impersonate(ref stackCrawlMark);
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x00090DB1 File Offset: 0x0008EFB1
		[SecurityCritical]
		internal WindowsImpersonationContext Impersonate(ref StackCrawlMark stackMark)
		{
			if (this.m_safeTokenHandle.IsInvalid)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AnonymousCannotImpersonate"));
			}
			return WindowsIdentity.SafeImpersonate(this.m_safeTokenHandle, this, ref stackMark);
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x00090DDD File Offset: 0x0008EFDD
		[SecuritySafeCritical]
		[ComVisible(false)]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.m_safeTokenHandle != null && !this.m_safeTokenHandle.IsClosed)
			{
				this.m_safeTokenHandle.Dispose();
			}
			this.m_name = null;
			this.m_owner = null;
			this.m_user = null;
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x00090E17 File Offset: 0x0008F017
		[ComVisible(false)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060027A6 RID: 10150 RVA: 0x00090E20 File Offset: 0x0008F020
		public SafeAccessTokenHandle AccessToken
		{
			[SecurityCritical]
			get
			{
				return this.m_safeTokenHandle;
			}
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x00090E28 File Offset: 0x0008F028
		[SecurityCritical]
		internal static WindowsImpersonationContext SafeRevertToSelf(ref StackCrawlMark stackMark)
		{
			return WindowsIdentity.SafeImpersonate(WindowsIdentity.s_invalidTokenHandle, null, ref stackMark);
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x00090E38 File Offset: 0x0008F038
		[SecurityCritical]
		internal static WindowsImpersonationContext SafeImpersonate(SafeAccessTokenHandle userToken, WindowsIdentity wi, ref StackCrawlMark stackMark)
		{
			int num = 0;
			bool isImpersonating;
			SafeAccessTokenHandle currentToken = WindowsIdentity.GetCurrentToken(TokenAccessLevels.MaximumAllowed, false, out isImpersonating, out num);
			if (currentToken == null || currentToken.IsInvalid)
			{
				throw new SecurityException(Win32Native.GetMessage(num));
			}
			FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, true);
			if (securityObjectForFrame == null)
			{
				throw new SecurityException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
			}
			WindowsImpersonationContext windowsImpersonationContext = new WindowsImpersonationContext(currentToken, WindowsIdentity.GetCurrentThreadWI(), isImpersonating, securityObjectForFrame);
			if (userToken.IsInvalid)
			{
				num = Win32.RevertToSelf();
				if (num < 0)
				{
					Environment.FailFast(Win32Native.GetMessage(num));
				}
				WindowsIdentity.UpdateThreadWI(wi);
				securityObjectForFrame.SetTokenHandles(currentToken, (wi == null) ? null : wi.AccessToken);
			}
			else
			{
				num = Win32.RevertToSelf();
				if (num < 0)
				{
					Environment.FailFast(Win32Native.GetMessage(num));
				}
				num = Win32.ImpersonateLoggedOnUser(userToken);
				if (num < 0)
				{
					windowsImpersonationContext.Undo();
					throw new SecurityException(Environment.GetResourceString("Argument_ImpersonateUser"));
				}
				WindowsIdentity.UpdateThreadWI(wi);
				securityObjectForFrame.SetTokenHandles(currentToken, (wi == null) ? null : wi.AccessToken);
			}
			return windowsImpersonationContext;
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x00090F22 File Offset: 0x0008F122
		[SecurityCritical]
		internal static WindowsIdentity GetCurrentThreadWI()
		{
			return SecurityContext.GetCurrentWI(Thread.CurrentThread.GetExecutionContextReader());
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x00090F34 File Offset: 0x0008F134
		[SecurityCritical]
		internal static void UpdateThreadWI(WindowsIdentity wi)
		{
			Thread currentThread = Thread.CurrentThread;
			if (currentThread.GetExecutionContextReader().SecurityContext.WindowsIdentity != wi)
			{
				ExecutionContext mutableExecutionContext = currentThread.GetMutableExecutionContext();
				SecurityContext securityContext = mutableExecutionContext.SecurityContext;
				if (wi != null && securityContext == null)
				{
					securityContext = new SecurityContext();
					mutableExecutionContext.SecurityContext = securityContext;
				}
				if (securityContext != null)
				{
					securityContext.WindowsIdentity = wi;
				}
			}
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x00090F94 File Offset: 0x0008F194
		[SecurityCritical]
		internal static WindowsIdentity GetCurrentInternal(TokenAccessLevels desiredAccess, bool threadOnly)
		{
			int errorCode = 0;
			bool flag;
			SafeAccessTokenHandle currentToken = WindowsIdentity.GetCurrentToken(desiredAccess, threadOnly, out flag, out errorCode);
			if (currentToken != null && !currentToken.IsInvalid)
			{
				WindowsIdentity windowsIdentity = new WindowsIdentity();
				windowsIdentity.m_safeTokenHandle.Dispose();
				windowsIdentity.m_safeTokenHandle = currentToken;
				return windowsIdentity;
			}
			if (threadOnly && !flag)
			{
				return null;
			}
			throw new SecurityException(Win32Native.GetMessage(errorCode));
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x00090FE7 File Offset: 0x0008F1E7
		internal static RuntimeConstructorInfo GetSpecialSerializationCtor()
		{
			return WindowsIdentity.s_specialSerializationCtor;
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x00090FEE File Offset: 0x0008F1EE
		private static int GetHRForWin32Error(int dwLastError)
		{
			if (((long)dwLastError & (long)((ulong)-2147483648)) == (long)((ulong)-2147483648))
			{
				return dwLastError;
			}
			return (dwLastError & 65535) | -2147024896;
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x00091010 File Offset: 0x0008F210
		[SecurityCritical]
		private static Exception GetExceptionFromNtStatus(int status)
		{
			if (status == -1073741790)
			{
				return new UnauthorizedAccessException();
			}
			if (status == -1073741670 || status == -1073741801)
			{
				return new OutOfMemoryException();
			}
			int errorCode = Win32Native.LsaNtStatusToWinError(status);
			return new SecurityException(Win32Native.GetMessage(errorCode));
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x00091054 File Offset: 0x0008F254
		[SecurityCritical]
		private static SafeAccessTokenHandle GetCurrentToken(TokenAccessLevels desiredAccess, bool threadOnly, out bool isImpersonating, out int hr)
		{
			isImpersonating = true;
			SafeAccessTokenHandle safeAccessTokenHandle = WindowsIdentity.GetCurrentThreadToken(desiredAccess, out hr);
			if (safeAccessTokenHandle == null && hr == WindowsIdentity.GetHRForWin32Error(1008))
			{
				isImpersonating = false;
				if (!threadOnly)
				{
					safeAccessTokenHandle = WindowsIdentity.GetCurrentProcessToken(desiredAccess, out hr);
				}
			}
			return safeAccessTokenHandle;
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x0009108C File Offset: 0x0008F28C
		[SecurityCritical]
		private static SafeAccessTokenHandle GetCurrentProcessToken(TokenAccessLevels desiredAccess, out int hr)
		{
			hr = 0;
			SafeAccessTokenHandle result;
			if (!Win32Native.OpenProcessToken(Win32Native.GetCurrentProcess(), desiredAccess, out result))
			{
				hr = WindowsIdentity.GetHRForWin32Error(Marshal.GetLastWin32Error());
			}
			return result;
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000910B8 File Offset: 0x0008F2B8
		[SecurityCritical]
		internal static SafeAccessTokenHandle GetCurrentThreadToken(TokenAccessLevels desiredAccess, out int hr)
		{
			SafeAccessTokenHandle result;
			hr = Win32.OpenThreadToken(desiredAccess, WinSecurityContext.Both, out result);
			return result;
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000910D4 File Offset: 0x0008F2D4
		[SecurityCritical]
		private T GetTokenInformation<T>(TokenInformationClass tokenInformationClass) where T : struct
		{
			T result;
			using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, tokenInformationClass))
			{
				result = tokenInformation.Read<T>(0UL);
			}
			return result;
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x00091114 File Offset: 0x0008F314
		[SecurityCritical]
		internal static ImpersonationQueryResult QueryImpersonation()
		{
			SafeAccessTokenHandle safeAccessTokenHandle = null;
			int num = Win32.OpenThreadToken(TokenAccessLevels.Query, WinSecurityContext.Thread, out safeAccessTokenHandle);
			if (safeAccessTokenHandle != null)
			{
				safeAccessTokenHandle.Close();
				return ImpersonationQueryResult.Impersonated;
			}
			if (num == WindowsIdentity.GetHRForWin32Error(5))
			{
				return ImpersonationQueryResult.Impersonated;
			}
			if (num == WindowsIdentity.GetHRForWin32Error(1008))
			{
				return ImpersonationQueryResult.NotImpersonated;
			}
			return ImpersonationQueryResult.Failed;
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x00091154 File Offset: 0x0008F354
		[SecurityCritical]
		private static Win32Native.LUID GetLogonAuthId(SafeAccessTokenHandle safeTokenHandle)
		{
			Win32Native.LUID authenticationId;
			using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(safeTokenHandle, TokenInformationClass.TokenStatistics))
			{
				Win32Native.TOKEN_STATISTICS token_STATISTICS = tokenInformation.Read<Win32Native.TOKEN_STATISTICS>(0UL);
				authenticationId = token_STATISTICS.AuthenticationId;
			}
			return authenticationId;
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x00091198 File Offset: 0x0008F398
		[SecurityCritical]
		private static SafeLocalAllocHandle GetTokenInformation(SafeAccessTokenHandle tokenHandle, TokenInformationClass tokenInformationClass)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			uint num = (uint)Marshal.SizeOf(typeof(uint));
			bool tokenInformation = Win32Native.GetTokenInformation(tokenHandle, (uint)tokenInformationClass, safeLocalAllocHandle, 0U, out num);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 6)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidImpersonationToken"));
			}
			if (lastWin32Error != 24 && lastWin32Error != 122)
			{
				throw new SecurityException(Win32Native.GetMessage(lastWin32Error));
			}
			UIntPtr sizetdwBytes = new UIntPtr(num);
			safeLocalAllocHandle.Dispose();
			safeLocalAllocHandle = Win32Native.LocalAlloc(0, sizetdwBytes);
			if (safeLocalAllocHandle == null || safeLocalAllocHandle.IsInvalid)
			{
				throw new OutOfMemoryException();
			}
			safeLocalAllocHandle.Initialize((ulong)num);
			if (!Win32Native.GetTokenInformation(tokenHandle, (uint)tokenInformationClass, safeLocalAllocHandle, num, out num))
			{
				throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
			}
			return safeLocalAllocHandle;
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x00091248 File Offset: 0x0008F448
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		private unsafe static SafeAccessTokenHandle KerbS4ULogon(string upn, ref SafeAccessTokenHandle safeTokenHandle)
		{
			byte[] array = new byte[]
			{
				67,
				76,
				82
			};
			UIntPtr sizetdwBytes = new UIntPtr((uint)(array.Length + 1));
			SafeAccessTokenHandle result;
			using (SafeLocalAllocHandle safeLocalAllocHandle = Win32Native.LocalAlloc(64, sizetdwBytes))
			{
				if (safeLocalAllocHandle == null || safeLocalAllocHandle.IsInvalid)
				{
					throw new OutOfMemoryException();
				}
				safeLocalAllocHandle.Initialize((ulong)((long)array.Length + 1L));
				safeLocalAllocHandle.WriteArray<byte>(0UL, array, 0, array.Length);
				Win32Native.UNICODE_INTPTR_STRING unicode_INTPTR_STRING = new Win32Native.UNICODE_INTPTR_STRING(array.Length, safeLocalAllocHandle);
				SafeLsaLogonProcessHandle invalidHandle = SafeLsaLogonProcessHandle.InvalidHandle;
				SafeLsaReturnBufferHandle invalidHandle2 = SafeLsaReturnBufferHandle.InvalidHandle;
				try
				{
					Privilege privilege = null;
					RuntimeHelpers.PrepareConstrainedRegions();
					int num;
					try
					{
						try
						{
							privilege = new Privilege("SeTcbPrivilege");
							privilege.Enable();
						}
						catch (PrivilegeNotHeldException)
						{
						}
						IntPtr zero = IntPtr.Zero;
						num = Win32Native.LsaRegisterLogonProcess(ref unicode_INTPTR_STRING, ref invalidHandle, ref zero);
						if (5 == Win32Native.LsaNtStatusToWinError(num))
						{
							num = Win32Native.LsaConnectUntrusted(ref invalidHandle);
						}
					}
					catch
					{
						if (privilege != null)
						{
							privilege.Revert();
						}
						throw;
					}
					finally
					{
						if (privilege != null)
						{
							privilege.Revert();
						}
					}
					if (num < 0)
					{
						throw WindowsIdentity.GetExceptionFromNtStatus(num);
					}
					byte[] array2 = new byte["Kerberos".Length + 1];
					Encoding.ASCII.GetBytes("Kerberos", 0, "Kerberos".Length, array2, 0);
					sizetdwBytes = new UIntPtr((uint)array2.Length);
					using (SafeLocalAllocHandle safeLocalAllocHandle2 = Win32Native.LocalAlloc(0, sizetdwBytes))
					{
						if (safeLocalAllocHandle2 == null || safeLocalAllocHandle2.IsInvalid)
						{
							throw new OutOfMemoryException();
						}
						safeLocalAllocHandle2.Initialize((ulong)array2.Length);
						safeLocalAllocHandle2.WriteArray<byte>(0UL, array2, 0, array2.Length);
						Win32Native.UNICODE_INTPTR_STRING unicode_INTPTR_STRING2 = new Win32Native.UNICODE_INTPTR_STRING("Kerberos".Length, safeLocalAllocHandle2);
						uint authenticationPackage = 0U;
						num = Win32Native.LsaLookupAuthenticationPackage(invalidHandle, ref unicode_INTPTR_STRING2, ref authenticationPackage);
						if (num < 0)
						{
							throw WindowsIdentity.GetExceptionFromNtStatus(num);
						}
						Win32Native.TOKEN_SOURCE token_SOURCE = default(Win32Native.TOKEN_SOURCE);
						if (!Win32Native.AllocateLocallyUniqueId(ref token_SOURCE.SourceIdentifier))
						{
							throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
						}
						token_SOURCE.Name = new char[8];
						token_SOURCE.Name[0] = 'C';
						token_SOURCE.Name[1] = 'L';
						token_SOURCE.Name[2] = 'R';
						uint num2 = 0U;
						Win32Native.LUID luid = default(Win32Native.LUID);
						Win32Native.QUOTA_LIMITS quota_LIMITS = default(Win32Native.QUOTA_LIMITS);
						int num3 = 0;
						byte[] bytes = Encoding.Unicode.GetBytes(upn);
						uint num4 = (uint)(Marshal.SizeOf(typeof(Win32Native.KERB_S4U_LOGON)) + bytes.Length);
						using (SafeLocalAllocHandle safeLocalAllocHandle3 = Win32Native.LocalAlloc(64, new UIntPtr(num4)))
						{
							if (safeLocalAllocHandle3 == null || safeLocalAllocHandle3.IsInvalid)
							{
								throw new OutOfMemoryException();
							}
							safeLocalAllocHandle3.Initialize((ulong)num4);
							ulong num5 = (ulong)((long)Marshal.SizeOf(typeof(Win32Native.KERB_S4U_LOGON)));
							safeLocalAllocHandle3.WriteArray<byte>(num5, bytes, 0, bytes.Length);
							byte* ptr = null;
							RuntimeHelpers.PrepareConstrainedRegions();
							try
							{
								safeLocalAllocHandle3.AcquirePointer(ref ptr);
								safeLocalAllocHandle3.Write<Win32Native.KERB_S4U_LOGON>(0UL, new Win32Native.KERB_S4U_LOGON
								{
									MessageType = 12U,
									Flags = 0U,
									ClientUpn = new Win32Native.UNICODE_INTPTR_STRING(bytes.Length, new IntPtr((void*)(ptr + num5)))
								});
								num = Win32Native.LsaLogonUser(invalidHandle, ref unicode_INTPTR_STRING, 3U, authenticationPackage, new IntPtr((void*)ptr), (uint)safeLocalAllocHandle3.ByteLength, IntPtr.Zero, ref token_SOURCE, ref invalidHandle2, ref num2, ref luid, ref safeTokenHandle, ref quota_LIMITS, ref num3);
								if (num == -1073741714 && num3 < 0)
								{
									num = num3;
								}
								if (num < 0)
								{
									throw WindowsIdentity.GetExceptionFromNtStatus(num);
								}
								if (num3 < 0)
								{
									throw WindowsIdentity.GetExceptionFromNtStatus(num3);
								}
							}
							finally
							{
								if (ptr != null)
								{
									safeLocalAllocHandle3.ReleasePointer();
								}
							}
						}
						result = safeTokenHandle;
					}
				}
				finally
				{
					if (!invalidHandle.IsInvalid)
					{
						invalidHandle.Dispose();
					}
					if (!invalidHandle2.IsInvalid)
					{
						invalidHandle2.Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x0009167C File Offset: 0x0008F87C
		[SecuritySafeCritical]
		protected WindowsIdentity(WindowsIdentity identity) : base(identity, null, identity.m_authType, null, null, false)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (!identity.m_safeTokenHandle.IsInvalid && identity.m_safeTokenHandle != SafeAccessTokenHandle.InvalidHandle && identity.m_safeTokenHandle.DangerousGetHandle() != IntPtr.Zero)
				{
					identity.m_safeTokenHandle.DangerousAddRef(ref flag);
					if (!identity.m_safeTokenHandle.IsInvalid && identity.m_safeTokenHandle.DangerousGetHandle() != IntPtr.Zero)
					{
						this.CreateFromToken(identity.m_safeTokenHandle.DangerousGetHandle());
					}
					this.m_authType = identity.m_authType;
					this.m_isAuthenticated = identity.m_isAuthenticated;
				}
			}
			finally
			{
				if (flag)
				{
					identity.m_safeTokenHandle.DangerousRelease();
				}
			}
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x00091784 File Offset: 0x0008F984
		[SecurityCritical]
		internal IntPtr GetTokenInternal()
		{
			return this.m_safeTokenHandle.DangerousGetHandle();
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x00091794 File Offset: 0x0008F994
		[SecurityCritical]
		internal WindowsIdentity(ClaimsIdentity claimsIdentity, IntPtr userToken) : base(claimsIdentity)
		{
			if (userToken != IntPtr.Zero && userToken.ToInt64() > 0L)
			{
				this.CreateFromToken(userToken);
			}
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x000917EF File Offset: 0x0008F9EF
		internal ClaimsIdentity CloneAsBase()
		{
			return base.Clone();
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x000917F7 File Offset: 0x0008F9F7
		public override ClaimsIdentity Clone()
		{
			return new WindowsIdentity(this);
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060027BC RID: 10172 RVA: 0x000917FF File Offset: 0x0008F9FF
		public virtual IEnumerable<Claim> UserClaims
		{
			get
			{
				this.InitializeClaims();
				return this.m_userClaims.AsReadOnly();
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060027BD RID: 10173 RVA: 0x00091812 File Offset: 0x0008FA12
		public virtual IEnumerable<Claim> DeviceClaims
		{
			get
			{
				this.InitializeClaims();
				return this.m_deviceClaims.AsReadOnly();
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060027BE RID: 10174 RVA: 0x00091828 File Offset: 0x0008FA28
		public override IEnumerable<Claim> Claims
		{
			get
			{
				if (!this.m_claimsInitialized)
				{
					this.InitializeClaims();
				}
				foreach (Claim claim in base.Claims)
				{
					yield return claim;
				}
				IEnumerator<Claim> enumerator = null;
				foreach (Claim claim2 in this.m_userClaims)
				{
					yield return claim2;
				}
				List<Claim>.Enumerator enumerator2 = default(List<Claim>.Enumerator);
				foreach (Claim claim3 in this.m_deviceClaims)
				{
					yield return claim3;
				}
				enumerator2 = default(List<Claim>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x00091848 File Offset: 0x0008FA48
		[SecuritySafeCritical]
		private void InitializeClaims()
		{
			if (!this.m_claimsInitialized)
			{
				object claimsIntiailizedLock = this.m_claimsIntiailizedLock;
				lock (claimsIntiailizedLock)
				{
					if (!this.m_claimsInitialized)
					{
						this.m_userClaims = new List<Claim>();
						this.m_deviceClaims = new List<Claim>();
						if (!string.IsNullOrEmpty(this.Name))
						{
							this.m_userClaims.Add(new Claim(base.NameClaimType, this.Name, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this));
						}
						this.AddPrimarySidClaim(this.m_userClaims);
						this.AddGroupSidClaims(this.m_userClaims);
						if (Environment.IsWindows8OrAbove)
						{
							this.AddDeviceGroupSidClaims(this.m_deviceClaims, TokenInformationClass.TokenDeviceGroups);
							this.AddTokenClaims(this.m_userClaims, TokenInformationClass.TokenUserClaimAttributes, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsuserclaim");
							this.AddTokenClaims(this.m_deviceClaims, TokenInformationClass.TokenDeviceClaimAttributes, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdeviceclaim");
						}
						this.m_claimsInitialized = true;
					}
				}
			}
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x0009194C File Offset: 0x0008FB4C
		[SecurityCritical]
		private void AddDeviceGroupSidClaims(List<Claim> instanceClaims, TokenInformationClass tokenInformationClass)
		{
			if (this.m_safeTokenHandle.IsInvalid)
			{
				return;
			}
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			try
			{
				safeLocalAllocHandle = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, tokenInformationClass);
				int num = Marshal.ReadInt32(safeLocalAllocHandle.DangerousGetHandle());
				IntPtr intPtr = new IntPtr((long)safeLocalAllocHandle.DangerousGetHandle() + (long)Marshal.OffsetOf(typeof(Win32Native.TOKEN_GROUPS), "Groups"));
				for (int i = 0; i < num; i++)
				{
					Win32Native.SID_AND_ATTRIBUTES sid_AND_ATTRIBUTES = (Win32Native.SID_AND_ATTRIBUTES)Marshal.PtrToStructure(intPtr, typeof(Win32Native.SID_AND_ATTRIBUTES));
					uint num2 = 3221225492U;
					SecurityIdentifier securityIdentifier = new SecurityIdentifier(sid_AND_ATTRIBUTES.Sid, true);
					if ((sid_AND_ATTRIBUTES.Attributes & num2) == 4U)
					{
						string text = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdevicegroup";
						instanceClaims.Add(new Claim(text, securityIdentifier.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString(securityIdentifier.IdentifierAuthority, CultureInfo.InvariantCulture))
						{
							Properties = 
							{
								{
									text,
									""
								}
							}
						});
					}
					else if ((sid_AND_ATTRIBUTES.Attributes & num2) == 16U)
					{
						string text = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlywindowsdevicegroup";
						instanceClaims.Add(new Claim(text, securityIdentifier.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString(securityIdentifier.IdentifierAuthority, CultureInfo.InvariantCulture))
						{
							Properties = 
							{
								{
									text,
									""
								}
							}
						});
					}
					intPtr = new IntPtr((long)intPtr + Win32Native.SID_AND_ATTRIBUTES.SizeOf);
				}
			}
			finally
			{
				safeLocalAllocHandle.Close();
			}
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x00091B04 File Offset: 0x0008FD04
		[SecurityCritical]
		private void AddGroupSidClaims(List<Claim> instanceClaims)
		{
			if (this.m_safeTokenHandle.IsInvalid)
			{
				return;
			}
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			SafeLocalAllocHandle safeLocalAllocHandle2 = SafeLocalAllocHandle.InvalidHandle;
			try
			{
				safeLocalAllocHandle2 = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenPrimaryGroup);
				Win32Native.TOKEN_PRIMARY_GROUP token_PRIMARY_GROUP = (Win32Native.TOKEN_PRIMARY_GROUP)Marshal.PtrToStructure(safeLocalAllocHandle2.DangerousGetHandle(), typeof(Win32Native.TOKEN_PRIMARY_GROUP));
				SecurityIdentifier securityIdentifier = new SecurityIdentifier(token_PRIMARY_GROUP.PrimaryGroup, true);
				bool flag = false;
				safeLocalAllocHandle = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenGroups);
				int num = Marshal.ReadInt32(safeLocalAllocHandle.DangerousGetHandle());
				IntPtr intPtr = new IntPtr((long)safeLocalAllocHandle.DangerousGetHandle() + (long)Marshal.OffsetOf(typeof(Win32Native.TOKEN_GROUPS), "Groups"));
				for (int i = 0; i < num; i++)
				{
					Win32Native.SID_AND_ATTRIBUTES sid_AND_ATTRIBUTES = (Win32Native.SID_AND_ATTRIBUTES)Marshal.PtrToStructure(intPtr, typeof(Win32Native.SID_AND_ATTRIBUTES));
					uint num2 = 3221225492U;
					SecurityIdentifier securityIdentifier2 = new SecurityIdentifier(sid_AND_ATTRIBUTES.Sid, true);
					if ((sid_AND_ATTRIBUTES.Attributes & num2) == 4U)
					{
						if (!flag && StringComparer.Ordinal.Equals(securityIdentifier2.Value, securityIdentifier.Value))
						{
							instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid", securityIdentifier2.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString(securityIdentifier2.IdentifierAuthority, CultureInfo.InvariantCulture)));
							flag = true;
						}
						instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid", securityIdentifier2.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString(securityIdentifier2.IdentifierAuthority, CultureInfo.InvariantCulture)));
					}
					else if ((sid_AND_ATTRIBUTES.Attributes & num2) == 16U)
					{
						if (!flag && StringComparer.Ordinal.Equals(securityIdentifier2.Value, securityIdentifier.Value))
						{
							instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid", securityIdentifier2.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString(securityIdentifier2.IdentifierAuthority, CultureInfo.InvariantCulture)));
							flag = true;
						}
						instanceClaims.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid", securityIdentifier2.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString(securityIdentifier2.IdentifierAuthority, CultureInfo.InvariantCulture)));
					}
					intPtr = new IntPtr((long)intPtr + Win32Native.SID_AND_ATTRIBUTES.SizeOf);
				}
			}
			finally
			{
				safeLocalAllocHandle.Close();
				safeLocalAllocHandle2.Close();
			}
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x00091DA4 File Offset: 0x0008FFA4
		[SecurityCritical]
		private void AddPrimarySidClaim(List<Claim> instanceClaims)
		{
			if (this.m_safeTokenHandle.IsInvalid)
			{
				return;
			}
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			try
			{
				safeLocalAllocHandle = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenUser);
				Win32Native.SID_AND_ATTRIBUTES sid_AND_ATTRIBUTES = (Win32Native.SID_AND_ATTRIBUTES)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(Win32Native.SID_AND_ATTRIBUTES));
				uint num = 16U;
				SecurityIdentifier securityIdentifier = new SecurityIdentifier(sid_AND_ATTRIBUTES.Sid, true);
				if (sid_AND_ATTRIBUTES.Attributes == 0U)
				{
					instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid", securityIdentifier.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString(securityIdentifier.IdentifierAuthority, CultureInfo.InvariantCulture)));
				}
				else if ((sid_AND_ATTRIBUTES.Attributes & num) == 16U)
				{
					instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid", securityIdentifier.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString(securityIdentifier.IdentifierAuthority, CultureInfo.InvariantCulture)));
				}
			}
			finally
			{
				safeLocalAllocHandle.Close();
			}
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x00091EB0 File Offset: 0x000900B0
		[SecurityCritical]
		private void AddTokenClaims(List<Claim> instanceClaims, TokenInformationClass tokenInformationClass, string propertyValue)
		{
			if (this.m_safeTokenHandle.IsInvalid)
			{
				return;
			}
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			try
			{
				SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
				safeLocalAllocHandle = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, tokenInformationClass);
				Win32Native.CLAIM_SECURITY_ATTRIBUTES_INFORMATION claim_SECURITY_ATTRIBUTES_INFORMATION = (Win32Native.CLAIM_SECURITY_ATTRIBUTES_INFORMATION)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(Win32Native.CLAIM_SECURITY_ATTRIBUTES_INFORMATION));
				long num = 0L;
				int num2 = 0;
				while ((long)num2 < (long)((ulong)claim_SECURITY_ATTRIBUTES_INFORMATION.AttributeCount))
				{
					IntPtr ptr = new IntPtr(claim_SECURITY_ATTRIBUTES_INFORMATION.Attribute.pAttributeV1.ToInt64() + num);
					Win32Native.CLAIM_SECURITY_ATTRIBUTE_V1 claim_SECURITY_ATTRIBUTE_V = (Win32Native.CLAIM_SECURITY_ATTRIBUTE_V1)Marshal.PtrToStructure(ptr, typeof(Win32Native.CLAIM_SECURITY_ATTRIBUTE_V1));
					switch (claim_SECURITY_ATTRIBUTE_V.ValueType)
					{
					case 1:
					{
						long[] array = new long[claim_SECURITY_ATTRIBUTE_V.ValueCount];
						Marshal.Copy(claim_SECURITY_ATTRIBUTE_V.Values.pInt64, array, 0, (int)claim_SECURITY_ATTRIBUTE_V.ValueCount);
						int num3 = 0;
						while ((long)num3 < (long)((ulong)claim_SECURITY_ATTRIBUTE_V.ValueCount))
						{
							instanceClaims.Add(new Claim(claim_SECURITY_ATTRIBUTE_V.Name, Convert.ToString(array[num3], CultureInfo.InvariantCulture), "http://www.w3.org/2001/XMLSchema#integer64", this.m_issuerName, this.m_issuerName, this, propertyValue, string.Empty));
							num3++;
						}
						break;
					}
					case 2:
					{
						long[] array2 = new long[claim_SECURITY_ATTRIBUTE_V.ValueCount];
						Marshal.Copy(claim_SECURITY_ATTRIBUTE_V.Values.pUint64, array2, 0, (int)claim_SECURITY_ATTRIBUTE_V.ValueCount);
						int num4 = 0;
						while ((long)num4 < (long)((ulong)claim_SECURITY_ATTRIBUTE_V.ValueCount))
						{
							instanceClaims.Add(new Claim(claim_SECURITY_ATTRIBUTE_V.Name, Convert.ToString((ulong)array2[num4], CultureInfo.InvariantCulture), "http://www.w3.org/2001/XMLSchema#uinteger64", this.m_issuerName, this.m_issuerName, this, propertyValue, string.Empty));
							num4++;
						}
						break;
					}
					case 3:
					{
						IntPtr[] array3 = new IntPtr[claim_SECURITY_ATTRIBUTE_V.ValueCount];
						Marshal.Copy(claim_SECURITY_ATTRIBUTE_V.Values.ppString, array3, 0, (int)claim_SECURITY_ATTRIBUTE_V.ValueCount);
						int num5 = 0;
						while ((long)num5 < (long)((ulong)claim_SECURITY_ATTRIBUTE_V.ValueCount))
						{
							instanceClaims.Add(new Claim(claim_SECURITY_ATTRIBUTE_V.Name, Marshal.PtrToStringAuto(array3[num5]), "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, propertyValue, string.Empty));
							num5++;
						}
						break;
					}
					case 6:
					{
						long[] array4 = new long[claim_SECURITY_ATTRIBUTE_V.ValueCount];
						Marshal.Copy(claim_SECURITY_ATTRIBUTE_V.Values.pUint64, array4, 0, (int)claim_SECURITY_ATTRIBUTE_V.ValueCount);
						int num6 = 0;
						while ((long)num6 < (long)((ulong)claim_SECURITY_ATTRIBUTE_V.ValueCount))
						{
							instanceClaims.Add(new Claim(claim_SECURITY_ATTRIBUTE_V.Name, (array4[num6] == 0L) ? Convert.ToString(false, CultureInfo.InvariantCulture) : Convert.ToString(true, CultureInfo.InvariantCulture), "http://www.w3.org/2001/XMLSchema#boolean", this.m_issuerName, this.m_issuerName, this, propertyValue, string.Empty));
							num6++;
						}
						break;
					}
					}
					num += (long)Marshal.SizeOf<Win32Native.CLAIM_SECURITY_ATTRIBUTE_V1>(claim_SECURITY_ATTRIBUTE_V);
					num2++;
				}
			}
			finally
			{
				safeLocalAllocHandle.Close();
			}
		}

		// Token: 0x04000F78 RID: 3960
		[SecurityCritical]
		private static SafeAccessTokenHandle s_invalidTokenHandle = SafeAccessTokenHandle.InvalidHandle;

		// Token: 0x04000F79 RID: 3961
		private string m_name;

		// Token: 0x04000F7A RID: 3962
		private SecurityIdentifier m_owner;

		// Token: 0x04000F7B RID: 3963
		private SecurityIdentifier m_user;

		// Token: 0x04000F7C RID: 3964
		private object m_groups;

		// Token: 0x04000F7D RID: 3965
		[SecurityCritical]
		private SafeAccessTokenHandle m_safeTokenHandle = SafeAccessTokenHandle.InvalidHandle;

		// Token: 0x04000F7E RID: 3966
		private string m_authType;

		// Token: 0x04000F7F RID: 3967
		private int m_isAuthenticated = -1;

		// Token: 0x04000F80 RID: 3968
		private volatile TokenImpersonationLevel m_impersonationLevel;

		// Token: 0x04000F81 RID: 3969
		private volatile bool m_impersonationLevelInitialized;

		// Token: 0x04000F82 RID: 3970
		private static RuntimeConstructorInfo s_specialSerializationCtor = typeof(WindowsIdentity).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
		{
			typeof(SerializationInfo)
		}, null) as RuntimeConstructorInfo;

		// Token: 0x04000F83 RID: 3971
		[NonSerialized]
		public new const string DefaultIssuer = "AD AUTHORITY";

		// Token: 0x04000F84 RID: 3972
		[NonSerialized]
		private string m_issuerName = "AD AUTHORITY";

		// Token: 0x04000F85 RID: 3973
		[NonSerialized]
		private object m_claimsIntiailizedLock = new object();

		// Token: 0x04000F86 RID: 3974
		[NonSerialized]
		private volatile bool m_claimsInitialized;

		// Token: 0x04000F87 RID: 3975
		[NonSerialized]
		private List<Claim> m_deviceClaims;

		// Token: 0x04000F88 RID: 3976
		[NonSerialized]
		private List<Claim> m_userClaims;
	}
}
