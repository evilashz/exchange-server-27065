using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x020004C7 RID: 1223
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class EventWaitHandle : WaitHandle
	{
		// Token: 0x06003AB9 RID: 15033 RVA: 0x000DE3FE File Offset: 0x000DC5FE
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public EventWaitHandle(bool initialState, EventResetMode mode) : this(initialState, mode, null)
		{
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x000DE40C File Offset: 0x000DC60C
		[SecurityCritical]
		[__DynamicallyInvokable]
		public EventWaitHandle(bool initialState, EventResetMode mode, string name)
		{
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", new object[]
				{
					name
				}));
			}
			SafeWaitHandle safeWaitHandle;
			if (mode != EventResetMode.AutoReset)
			{
				if (mode != EventResetMode.ManualReset)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag", new object[]
					{
						name
					}));
				}
				safeWaitHandle = Win32Native.CreateEvent(null, true, initialState, name);
			}
			else
			{
				safeWaitHandle = Win32Native.CreateEvent(null, false, initialState, name);
			}
			if (safeWaitHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				safeWaitHandle.SetHandleAsInvalid();
				if (name != null && name.Length != 0 && 6 == lastWin32Error)
				{
					throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", new object[]
					{
						name
					}));
				}
				__Error.WinIOError(lastWin32Error, name);
			}
			base.SetHandleInternal(safeWaitHandle);
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x000DE4CF File Offset: 0x000DC6CF
		[SecurityCritical]
		[__DynamicallyInvokable]
		public EventWaitHandle(bool initialState, EventResetMode mode, string name, out bool createdNew) : this(initialState, mode, name, out createdNew, null)
		{
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x000DE4E0 File Offset: 0x000DC6E0
		[SecurityCritical]
		public unsafe EventWaitHandle(bool initialState, EventResetMode mode, string name, out bool createdNew, EventWaitHandleSecurity eventSecurity)
		{
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", new object[]
				{
					name
				}));
			}
			Win32Native.SECURITY_ATTRIBUTES security_ATTRIBUTES = null;
			if (eventSecurity != null)
			{
				security_ATTRIBUTES = new Win32Native.SECURITY_ATTRIBUTES();
				security_ATTRIBUTES.nLength = Marshal.SizeOf<Win32Native.SECURITY_ATTRIBUTES>(security_ATTRIBUTES);
				byte[] securityDescriptorBinaryForm = eventSecurity.GetSecurityDescriptorBinaryForm();
				byte* ptr = stackalloc byte[checked(unchecked((UIntPtr)securityDescriptorBinaryForm.Length) * 1)];
				Buffer.Memcpy(ptr, 0, securityDescriptorBinaryForm, 0, securityDescriptorBinaryForm.Length);
				security_ATTRIBUTES.pSecurityDescriptor = ptr;
			}
			bool isManualReset;
			if (mode != EventResetMode.AutoReset)
			{
				if (mode != EventResetMode.ManualReset)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag", new object[]
					{
						name
					}));
				}
				isManualReset = true;
			}
			else
			{
				isManualReset = false;
			}
			SafeWaitHandle safeWaitHandle = Win32Native.CreateEvent(security_ATTRIBUTES, isManualReset, initialState, name);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (safeWaitHandle.IsInvalid)
			{
				safeWaitHandle.SetHandleAsInvalid();
				if (name != null && name.Length != 0 && 6 == lastWin32Error)
				{
					throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", new object[]
					{
						name
					}));
				}
				__Error.WinIOError(lastWin32Error, name);
			}
			createdNew = (lastWin32Error != 183);
			base.SetHandleInternal(safeWaitHandle);
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x000DE5EE File Offset: 0x000DC7EE
		[SecurityCritical]
		private EventWaitHandle(SafeWaitHandle handle)
		{
			base.SetHandleInternal(handle);
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x000DE5FD File Offset: 0x000DC7FD
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static EventWaitHandle OpenExisting(string name)
		{
			return EventWaitHandle.OpenExisting(name, EventWaitHandleRights.Modify | EventWaitHandleRights.Synchronize);
		}

		// Token: 0x06003ABF RID: 15039 RVA: 0x000DE60C File Offset: 0x000DC80C
		[SecurityCritical]
		public static EventWaitHandle OpenExisting(string name, EventWaitHandleRights rights)
		{
			EventWaitHandle result;
			switch (EventWaitHandle.OpenExistingWorker(name, rights, out result))
			{
			case WaitHandle.OpenExistingResult.NameNotFound:
				throw new WaitHandleCannotBeOpenedException();
			case WaitHandle.OpenExistingResult.PathNotFound:
				__Error.WinIOError(3, "");
				return result;
			case WaitHandle.OpenExistingResult.NameInvalid:
				throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", new object[]
				{
					name
				}));
			default:
				return result;
			}
		}

		// Token: 0x06003AC0 RID: 15040 RVA: 0x000DE667 File Offset: 0x000DC867
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static bool TryOpenExisting(string name, out EventWaitHandle result)
		{
			return EventWaitHandle.OpenExistingWorker(name, EventWaitHandleRights.Modify | EventWaitHandleRights.Synchronize, out result) == WaitHandle.OpenExistingResult.Success;
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x000DE678 File Offset: 0x000DC878
		[SecurityCritical]
		public static bool TryOpenExisting(string name, EventWaitHandleRights rights, out EventWaitHandle result)
		{
			return EventWaitHandle.OpenExistingWorker(name, rights, out result) == WaitHandle.OpenExistingResult.Success;
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x000DE688 File Offset: 0x000DC888
		[SecurityCritical]
		private static WaitHandle.OpenExistingResult OpenExistingWorker(string name, EventWaitHandleRights rights, out EventWaitHandle result)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name", Environment.GetResourceString("ArgumentNull_WithParamName"));
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", new object[]
				{
					name
				}));
			}
			result = null;
			SafeWaitHandle safeWaitHandle = Win32Native.OpenEvent((int)rights, false, name);
			if (safeWaitHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (2 == lastWin32Error || 123 == lastWin32Error)
				{
					return WaitHandle.OpenExistingResult.NameNotFound;
				}
				if (3 == lastWin32Error)
				{
					return WaitHandle.OpenExistingResult.PathNotFound;
				}
				if (name != null && name.Length != 0 && 6 == lastWin32Error)
				{
					return WaitHandle.OpenExistingResult.NameInvalid;
				}
				__Error.WinIOError(lastWin32Error, "");
			}
			result = new EventWaitHandle(safeWaitHandle);
			return WaitHandle.OpenExistingResult.Success;
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x000DE744 File Offset: 0x000DC944
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public bool Reset()
		{
			bool flag = Win32Native.ResetEvent(this.safeWaitHandle);
			if (!flag)
			{
				__Error.WinIOError();
			}
			return flag;
		}

		// Token: 0x06003AC4 RID: 15044 RVA: 0x000DE768 File Offset: 0x000DC968
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public bool Set()
		{
			bool flag = Win32Native.SetEvent(this.safeWaitHandle);
			if (!flag)
			{
				__Error.WinIOError();
			}
			return flag;
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x000DE78C File Offset: 0x000DC98C
		[SecuritySafeCritical]
		public EventWaitHandleSecurity GetAccessControl()
		{
			return new EventWaitHandleSecurity(this.safeWaitHandle, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x000DE79D File Offset: 0x000DC99D
		[SecuritySafeCritical]
		public void SetAccessControl(EventWaitHandleSecurity eventSecurity)
		{
			if (eventSecurity == null)
			{
				throw new ArgumentNullException("eventSecurity");
			}
			eventSecurity.Persist(this.safeWaitHandle);
		}
	}
}
