﻿using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Microsoft.Exchange.Rpc.ProcessAccess
{
	// Token: 0x0200039C RID: 924
	internal abstract class ProcessAccessRpcServer : RpcServerBase
	{
		// Token: 0x06001032 RID: 4146 RVA: 0x0004AE18 File Offset: 0x0004A218
		private unsafe void SetObjectType(ValueType guid)
		{
			_GUID guid2 = <Module>.Microsoft.Exchange.Rpc.?A0xd6bdf0e7.GUIDFromGuid(guid);
			int num = <Module>.RpcObjectSetType(&guid2, &guid2);
			if (num != null && num != 1711)
			{
				RpcServerBase.ThrowRpcException(string.Format("RpcObjectSetType failed; status={0}", num), num);
			}
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0004AE58 File Offset: 0x0004A258
		private unsafe void ClearObjectType(ValueType guid)
		{
			_GUID guid2 = <Module>.Microsoft.Exchange.Rpc.?A0xd6bdf0e7.GUIDFromGuid(guid);
			int num = <Module>.RpcObjectSetType(&guid2, null);
			if (num != null && num != 1900)
			{
				RpcServerBase.ThrowRpcException(string.Format("RpcObjectSetType failed; status={0}", num), num);
			}
		}

		// Token: 0x06001034 RID: 4148
		public abstract byte[] RunProcessCommand(byte[] inBlob);

		// Token: 0x06001035 RID: 4149 RVA: 0x0004AC88 File Offset: 0x0004A088
		private unsafe static void RunProcessCommand(void* hBinding, int inBytesLen, byte* pInBytes, int* pOutBytesLen, byte** ppOutBytes)
		{
			WindowsIdentity windowsIdentity = null;
			byte[] array = null;
			ProcessAccessRpcServer processAccessRpcServer = (ProcessAccessRpcServer)RpcServerBase.GetServerInstance(ProcessAccessRpcServer.RpcIntfHandle);
			windowsIdentity = null;
			if (<Module>.RpcImpersonateClient(null) == null)
			{
				try
				{
					try
					{
						windowsIdentity = WindowsIdentity.GetCurrent();
						if (windowsIdentity != null)
						{
							goto IL_32;
						}
					}
					catch (SecurityException)
					{
					}
					return;
					IL_32:;
				}
				finally
				{
					<Module>.RpcRevertToSelf();
				}
				IntPtr uPtrData = new IntPtr((void*)pInBytes);
				byte[] inBlob = <Module>.UToMBytes(inBytesLen, uPtrData);
				try
				{
					array = processAccessRpcServer.RunProcessCommand(inBlob);
				}
				finally
				{
					if (windowsIdentity != null)
					{
						((IDisposable)windowsIdentity).Dispose();
					}
				}
				*pOutBytesLen = array.Length;
				int num = -2147024882;
				byte[] array2 = array;
				if (num == <Module>.MToUBytesN(array2, array2.Length, ppOutBytes))
				{
					throw new OutOfMemoryException();
				}
			}
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0004AE98 File Offset: 0x0004A298
		protected ProcessAccessRpcServer()
		{
			RunProcessCommandDelegate d = new RunProcessCommandDelegate(ProcessAccessRpcServer.RunProcessCommand);
			this.runProcessCommandDelegate = d;
			<Module>.?A0xd6bdf0e7.SetRunProcessCommandFunction(Marshal.GetFunctionPointerForDelegate(d).ToPointer());
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0004AED4 File Offset: 0x0004A2D4
		protected unsafe override void RegisterInterface(void* ifSpec, ValueType mgrTypeGuid, _GUID* pMgrTypeUuid, void* pMgrEpv, uint flags, uint maxCalls)
		{
			if (null == mgrTypeGuid)
			{
				throw new RpcException("mgrTypeGuid is null");
			}
			this.m_mgrTypeGuid = mgrTypeGuid;
			this.SetObjectType(ProcessAccessRpcServer.ProcessLocatorGuid);
			this.SetObjectType(this.m_mgrTypeGuid);
			_GUID guid = <Module>.Microsoft.Exchange.Rpc.?A0xd6bdf0e7.GUIDFromGuid(ProcessAccessRpcServer.ProcessLocatorGuid);
			base.RegisterInterface(ifSpec, null, &guid, pMgrEpv, flags, maxCalls);
			guid = <Module>.Microsoft.Exchange.Rpc.?A0xd6bdf0e7.GUIDFromGuid(this.m_mgrTypeGuid);
			base.RegisterInterface(ifSpec, null, &guid, pMgrEpv, flags, maxCalls);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0004AF50 File Offset: 0x0004A350
		protected unsafe override void UnregisterInterface(void* ifSpec, _GUID* pMgrTypeUuid, uint waitForCallsToComplete)
		{
			_GUID guid = <Module>.Microsoft.Exchange.Rpc.?A0xd6bdf0e7.GUIDFromGuid(ProcessAccessRpcServer.ProcessLocatorGuid);
			int num = <Module>.RpcServerUnregisterIf(ifSpec, &guid, waitForCallsToComplete);
			if (num != null)
			{
				RpcServerBase.ThrowRpcException("Could not unregister interface", num);
			}
			guid = <Module>.Microsoft.Exchange.Rpc.?A0xd6bdf0e7.GUIDFromGuid(this.m_mgrTypeGuid);
			int num2 = <Module>.RpcServerUnregisterIf(ifSpec, &guid, waitForCallsToComplete);
			if (num2 != null)
			{
				RpcServerBase.ThrowRpcException("Could not unregister interface", num2);
			}
			this.ClearObjectType(this.m_mgrTypeGuid);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0004AFB8 File Offset: 0x0004A3B8
		protected unsafe override void RegisterEp(void* ifSpec, _RPC_BINDING_VECTOR* pBindingVector, _UUID_VECTOR* pUuidVector, ushort* wszAnnotation)
		{
			_UUID_VECTOR uuid_VECTOR = 1;
			_GUID guid;
			*(ref uuid_VECTOR + 8) = ref guid;
			guid = <Module>.Microsoft.Exchange.Rpc.?A0xd6bdf0e7.GUIDFromGuid(ProcessAccessRpcServer.ProcessLocatorGuid);
			int num = <Module>.RpcEpRegisterW(ifSpec, pBindingVector, &uuid_VECTOR, wszAnnotation);
			if (num != null)
			{
				RpcServerBase.ThrowRpcException("RpcEpRegister", num);
			}
			guid = <Module>.Microsoft.Exchange.Rpc.?A0xd6bdf0e7.GUIDFromGuid(this.m_mgrTypeGuid);
			int num2 = <Module>.RpcEpRegisterW(ifSpec, pBindingVector, &uuid_VECTOR, wszAnnotation);
			if (num2 != null)
			{
				RpcServerBase.ThrowRpcException("RpcEpRegister", num2);
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0004B020 File Offset: 0x0004A420
		protected unsafe override void UnregisterEp(void* ifSpec, _RPC_BINDING_VECTOR* pBindingVector, _UUID_VECTOR* pUuidVector)
		{
			_UUID_VECTOR uuid_VECTOR = 1;
			_GUID guid;
			*(ref uuid_VECTOR + 8) = ref guid;
			guid = <Module>.Microsoft.Exchange.Rpc.?A0xd6bdf0e7.GUIDFromGuid(ProcessAccessRpcServer.ProcessLocatorGuid);
			base.UnregisterEp(ifSpec, pBindingVector, &uuid_VECTOR);
			guid = <Module>.Microsoft.Exchange.Rpc.?A0xd6bdf0e7.GUIDFromGuid(this.m_mgrTypeGuid);
			base.UnregisterEp(ifSpec, pBindingVector, &uuid_VECTOR);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0004B068 File Offset: 0x0004A468
		[HandleProcessCorruptedStateExceptions]
		protected unsafe static List<KeyValuePair<Guid, string>> GetRegisteredProcessGuids()
		{
			List<KeyValuePair<Guid, string>> list = new List<KeyValuePair<Guid, string>>();
			_RPC_IF_ID rpc_IF_ID;
			int num = <Module>.RpcIfInqId(ProcessAccessRpcServer.RpcIntfHandle.ToPointer(), &rpc_IF_ID);
			if (num != null)
			{
				RpcServerBase.ThrowRpcException("RpcIfInqId", num);
			}
			void** ptr;
			num = <Module>.RpcMgmtEpEltInqBegin(null, 1, &rpc_IF_ID, 1, null, &ptr);
			if (num != null)
			{
				RpcServerBase.ThrowRpcException("RpcMgmtEpEltInqBegin", num);
			}
			try
			{
				for (int i = 0; i < 65535; i++)
				{
					ushort* ptr2 = null;
					try
					{
						_RPC_IF_ID rpc_IF_ID2;
						_GUID guid;
						num = <Module>.RpcMgmtEpEltInqNextW(ptr, &rpc_IF_ID2, null, &guid, &ptr2);
						if (num == 1772)
						{
							break;
						}
						if (num != null)
						{
							RpcServerBase.ThrowRpcException("RpcMgmtEpEltInqNext", num);
						}
						string value;
						if (null == ptr2)
						{
							value = string.Empty;
						}
						else
						{
							IntPtr ptr3 = new IntPtr((void*)ptr2);
							value = Marshal.PtrToStringUni(ptr3);
						}
						Guid key = <Module>.Microsoft.Exchange.Rpc.?A0xd6bdf0e7.GuidFromGUID(ref guid);
						KeyValuePair<Guid, string> item = new KeyValuePair<Guid, string>(key, value);
						list.Add(item);
					}
					finally
					{
						if (null != ptr2)
						{
							<Module>.RpcStringFreeW(&ptr2);
						}
					}
				}
			}
			finally
			{
				num = <Module>.RpcMgmtEpEltInqDone(&ptr);
			}
			if (num != null)
			{
				RpcServerBase.ThrowRpcException("RpcMgmtEpEltInqDone", num);
			}
			return list;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0004B194 File Offset: 0x0004A594
		public unsafe static RpcServerBase RegisterServer(Type type, ObjectSecurity sd, uint accessMask, ValueType mgrTypeGuid, string annotation, [MarshalAs(UnmanagedType.U1)] bool isLocalOnly, uint maxCalls)
		{
			return RpcServerBase.RegisterServer(type, sd, accessMask, mgrTypeGuid, (void*)(&<Module>.Microsoft.Exchange.Rpc.ProcessAccess.?A0xd6bdf0e7.DEFAULT_EPV), annotation, isLocalOnly, maxCalls);
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0004AD5C File Offset: 0x0004A15C
		// Note: this type is marked as 'beforefieldinit'.
		static ProcessAccessRpcServer()
		{
			Guid processLocatorGuid = new Guid("37518031-BBA5-48de-98DC-8BCCFF43B608");
			ProcessAccessRpcServer.ProcessLocatorGuid = processLocatorGuid;
			ProcessAccessRpcServer.RpcIntfHandle = (IntPtr)<Module>.IProcessAccess_v1_0_s_ifspec;
		}

		// Token: 0x04000F76 RID: 3958
		private readonly RunProcessCommandDelegate runProcessCommandDelegate;

		// Token: 0x04000F77 RID: 3959
		protected static Guid ProcessLocatorGuid;

		// Token: 0x04000F78 RID: 3960
		protected ValueType m_mgrTypeGuid;

		// Token: 0x04000F79 RID: 3961
		public static IntPtr RpcIntfHandle;
	}
}
