﻿using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics.Components.Common;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x0200000F RID: 15
	public class RpcServerBase
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x0000159C File Offset: 0x0000099C
		public static RpcServerBase RegisterInterface(Type type, [MarshalAs(UnmanagedType.U1)] bool registerEndpoint, [MarshalAs(UnmanagedType.U1)] bool allowInstanceEndpoints, string annotation)
		{
			return RpcServerBase.RegisterInterface(type, RpcServerBase.BuildDefaultSecurityDescriptor(), 1, 0, 1234, false, false, registerEndpoint, allowInstanceEndpoints, false, annotation);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00001574 File Offset: 0x00000974
		public static RpcServerBase RegisterInterface(Type type, [MarshalAs(UnmanagedType.U1)] bool registerEndpoint, string annotation)
		{
			return RpcServerBase.RegisterInterface(type, RpcServerBase.BuildDefaultSecurityDescriptor(), 1, 0, 1234, false, false, registerEndpoint, false, false, annotation);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000197C File Offset: 0x00000D7C
		public static RpcServerBase RegisterInterface(Type type)
		{
			return RpcServerBase.RegisterInterface(type, RpcServerBase.BuildDefaultSecurityDescriptor(), 1, 0, 1234, false, false, true, false, false, null);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000154C File Offset: 0x0000094C
		public static RpcServerBase RegisterInterface(Type type, ObjectSecurity sd, uint desiredAccess, [MarshalAs(UnmanagedType.U1)] bool isLocalOnly, string annotation)
		{
			return RpcServerBase.RegisterInterface(type, RpcServerBase.BuildDefaultSecurityDescriptor(), 1, 0, 1234, false, false, true, false, false, annotation);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00001524 File Offset: 0x00000924
		public static RpcServerBase RegisterInterface(Type type, ObjectSecurity sd, uint desiredAccess, [MarshalAs(UnmanagedType.U1)] bool isLocalOnly, [MarshalAs(UnmanagedType.U1)] bool isSecureOnly)
		{
			return RpcServerBase.RegisterInterface(type, RpcServerBase.BuildDefaultSecurityDescriptor(), 1, 0, 1234, false, false, true, false, false, null);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000519C0 File Offset: 0x00050DC0
		protected unsafe virtual void RegisterInterface(void* ifSpec, ValueType mgrTypeGuid, _GUID* pMgrTypeUuid, void* pMgrEpv, uint flags, uint maxCalls)
		{
			int num = <Module>.RpcServerRegisterIfEx(ifSpec, pMgrTypeUuid, pMgrEpv, flags, maxCalls, <Module>.__unep@?_CommonRpcSecurityCallback@@$$FYAJPEAX0@Z);
			if (num != null)
			{
				RpcServerBase.ThrowRpcException("RpcServerRegisterIf", num);
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00052150 File Offset: 0x00051550
		private unsafe static RpcServerBase RegisterInterface(Type type, ObjectSecurity sd, uint desiredAccess, int flags, int maxCalls, [MarshalAs(UnmanagedType.U1)] bool isLocalOnly, [MarshalAs(UnmanagedType.U1)] bool isSecureOnly, [MarshalAs(UnmanagedType.U1)] bool registerEndpoint, [MarshalAs(UnmanagedType.U1)] bool allowInstanceEndpoints, [MarshalAs(UnmanagedType.U1)] bool allowNoAuthentication, string annotation)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (sd == null)
			{
				throw new ArgumentNullException("sd");
			}
			if (isLocalOnly && !isSecureOnly)
			{
				throw new ArgumentException("isLocalOnly true without isSecureOnly also true");
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			IntPtr intPtr = (IntPtr)type.GetField("RpcIntfHandle", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy).GetValue(null);
			RpcServerBase rpcServerBase = (RpcServerBase)Activator.CreateInstance(type);
			_RPC_IF_ID rpc_IF_ID;
			initblk(ref rpc_IF_ID, 0, 20L);
			rpcServerBase.m_binarySd = sd.GetSecurityDescriptorBinaryForm();
			rpcServerBase.m_isLocalOnly = isLocalOnly;
			rpcServerBase.m_isSecureOnly = isSecureOnly;
			rpcServerBase.m_desiredAccess = desiredAccess;
			rpcServerBase.m_isHttpAllowed = true;
			rpcServerBase.m_allowNoAuthentication = allowNoAuthentication;
			_RPC_BINDING_VECTOR* ptr = null;
			try
			{
				int num = <Module>.RpcIfInqId(intPtr.ToPointer(), &rpc_IF_ID);
				if (num != null)
				{
					RpcServerBase.TraceError("RpcIfInqId failed with status {2} in file {0} line {1}", new object[]
					{
						"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
						2302,
						num
					});
					RpcServerBase.ThrowRpcException("RpcIfInqId", num);
				}
				if (registerEndpoint)
				{
					_GUID guid = 0;
					initblk(ref guid + 4, 0, 12L);
					RpcServerBase.CheckDuplicateEndpoint(ref rpc_IF_ID, ref guid, allowInstanceEndpoints);
				}
				int num2 = flags | 16;
				if (isLocalOnly)
				{
					num2 |= 32;
				}
				else if (isSecureOnly)
				{
					num2 |= 8;
				}
				num = <Module>.RpcServerRegisterIfEx(intPtr.ToPointer(), null, null, (uint)num2, (uint)maxCalls, <Module>.__unep@?_CommonRpcSecurityCallback@@$$FYAJPEAX0@Z);
				if (num != null)
				{
					RpcServerBase.ThrowRpcException("RpcServerRegisterIf", num);
				}
				flag2 = true;
				if (registerEndpoint)
				{
					IntPtr intPtr2 = Marshal.StringToHGlobalUni(annotation);
					try
					{
						num = <Module>.RpcServerInqBindings(&ptr);
						if (num != null)
						{
							RpcServerBase.ThrowRpcException("RpcServerInqBindings", num);
						}
						num = <Module>.RpcEpRegisterW(intPtr.ToPointer(), ptr, null, (ushort*)intPtr2.ToPointer());
						if (num != null)
						{
							RpcServerBase.ThrowRpcException("RpcEpRegister", num);
						}
					}
					finally
					{
						if (ptr != null)
						{
							<Module>.RpcBindingVectorFree(&ptr);
							ptr = null;
						}
						if (intPtr2 != IntPtr.Zero)
						{
							Marshal.FreeHGlobal(intPtr2);
						}
					}
					flag3 = true;
				}
				_GUID guid2;
				cpblk(ref guid2, ref rpc_IF_ID, 16);
				Guid uuid = <Module>.Microsoft.Exchange.Rpc.?A0x2ca62c65.GuidFromGUID(ref guid2);
				rpcServerBase.m_uuid = uuid;
				RpcServerBase.serverCache.Add(rpcServerBase.m_uuid, rpcServerBase);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					if (flag3)
					{
						try
						{
							int num = <Module>.RpcServerInqBindings(&ptr);
							if (num == null)
							{
								num = <Module>.RpcEpUnregister(intPtr.ToPointer(), ptr, null);
								if (num != null)
								{
									RpcServerBase.TraceError("Function RpcEpUnregister failed with {0}", new object[]
									{
										num
									});
								}
							}
							else
							{
								RpcServerBase.TraceError("Function RpcServerInqBindings failed with {0}", new object[]
								{
									num
								});
							}
						}
						finally
						{
							if (ptr != null)
							{
								<Module>.RpcBindingVectorFree(&ptr);
								ptr = null;
							}
						}
					}
					if (flag2)
					{
						int num = <Module>.RpcServerUnregisterIf(intPtr.ToPointer(), null, 0U);
						if (num != null)
						{
							RpcServerBase.TraceError("Function RpcServerUnregisterIf failed with {0}", new object[]
							{
								num
							});
						}
					}
				}
			}
			return rpcServerBase;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00051708 File Offset: 0x00050B08
		static RpcServerBase()
		{
			ArraySegment<byte> emptyArraySegment = new ArraySegment<byte>(new byte[0]);
			RpcServerBase.EmptyArraySegment = emptyArraySegment;
			RpcServerBase.DefaultMaxRpcCalls = 1234;
			RpcServerBase.checkSecurityDelegate = new CheckSecurityDelegate(RpcServerBase.CheckSecurity);
			<Module>.?A0x2ca62c65.securityCallback = Marshal.GetFunctionPointerForDelegate(RpcServerBase.checkSecurityDelegate).ToPointer();
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00050368 File Offset: 0x0004F768
		protected static FileSecurity BuildDefaultSecurityDescriptor()
		{
			FileSecurity fileSecurity = new FileSecurity();
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);
			FileSystemAccessRule accessRule = new FileSystemAccessRule(securityIdentifier, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.SetOwner(securityIdentifier);
			fileSecurity.SetAccessRule(accessRule);
			return fileSecurity;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00050340 File Offset: 0x0004F740
		protected static void TraceError(string traceString, params object[] args)
		{
			if (Debugger.IsLogging())
			{
				Debugger.Log(0, RpcServerBase.TraceCategory, string.Format(traceString, args));
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000014FC File Offset: 0x000008FC
		public static RpcServerBase RegisterServer(Type type)
		{
			ObjectSecurity sd = RpcServerBase.BuildDefaultSecurityDescriptor();
			return RpcServerBase.RegisterServer(type, sd, 1, null, null, null, false, true, 1234U);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000014DC File Offset: 0x000008DC
		public static RpcServerBase RegisterServer(Type type, ObjectSecurity sd, uint accessMask)
		{
			return RpcServerBase.RegisterServer(type, sd, accessMask, null, null, null, false, true, 1234U);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000014BC File Offset: 0x000008BC
		public static RpcServerBase RegisterServer(Type type, ObjectSecurity sd, uint accessMask, [MarshalAs(UnmanagedType.U1)] bool isLocalOnly)
		{
			return RpcServerBase.RegisterServer(type, sd, accessMask, null, null, null, isLocalOnly, true, 1234U);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000149C File Offset: 0x0000089C
		public static RpcServerBase RegisterServer(Type type, ObjectSecurity sd, uint accessMask, [MarshalAs(UnmanagedType.U1)] bool isLocalOnly, uint maxCalls)
		{
			return RpcServerBase.RegisterServer(type, sd, accessMask, null, null, null, isLocalOnly, true, maxCalls);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00051E64 File Offset: 0x00051264
		[HandleProcessCorruptedStateExceptions]
		protected unsafe static RpcServerBase RegisterServer(Type type, ObjectSecurity sd, uint desiredAccess, ValueType mgrTypeGuid, void* mgrEpv, string annotation, [MarshalAs(UnmanagedType.U1)] bool isLocalOnly, [MarshalAs(UnmanagedType.U1)] bool autoListen, uint maxCalls)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (sd == null)
			{
				throw new ArgumentNullException("sd");
			}
			IntPtr intPtr = (IntPtr)type.GetField("RpcIntfHandle", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy).GetValue(null);
			RpcServerBase rpcServerBase = (RpcServerBase)Activator.CreateInstance(type);
			_RPC_IF_ID rpc_IF_ID;
			initblk(ref rpc_IF_ID, 0, 20L);
			rpcServerBase.m_binarySd = sd.GetSecurityDescriptorBinaryForm();
			rpcServerBase.m_isLocalOnly = isLocalOnly;
			rpcServerBase.m_isSecureOnly = true;
			rpcServerBase.m_isHttpAllowed = false;
			rpcServerBase.m_desiredAccess = desiredAccess;
			int num;
			if (!isLocalOnly)
			{
				num = <Module>.RpcServerUseProtseqW((ushort*)(&<Module>.??_C@_1BK@BPGFLIHL@?$AAn?$AAc?$AAa?$AAc?$AAn?$AA_?$AAi?$AAp?$AA_?$AAt?$AAc?$AAp?$AA?$AA@), 10U, null);
				if (num != null)
				{
					RpcServerBase.ThrowRpcException("RpcServerUseProtseq TCP/IP", num);
				}
			}
			num = <Module>.RpcServerUseProtseqW((ushort*)(&<Module>.??_C@_1BA@EONDGCCM@?$AAn?$AAc?$AAa?$AAl?$AAr?$AAp?$AAc?$AA?$AA@), 0U, null);
			if (num != null)
			{
				RpcServerBase.ThrowRpcException("RpcServerUseProtseq LRPC", num);
			}
			int num2 = isLocalOnly ? 32 : 8;
			int num3 = autoListen ? 1 : 0;
			rpcServerBase.RegisterInterface(intPtr.ToPointer(), mgrTypeGuid, null, mgrEpv, (uint)(num3 | num2), maxCalls);
			num = <Module>.RpcIfInqId(intPtr.ToPointer(), &rpc_IF_ID);
			if (num != null)
			{
				RpcServerBase.TraceError("RpcIfInqId failed with status {2} in file {0} line {1}", new object[]
				{
					"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
					1873,
					num
				});
				RpcServerBase.ThrowRpcException("RpcIfInqId", num);
			}
			_RPC_BINDING_VECTOR* pBindingVector = null;
			ushort* ptr = null;
			ushort* ptr2 = null;
			try
			{
				num = <Module>.RpcServerInqBindings(&pBindingVector);
				if (num != null)
				{
					RpcServerBase.ThrowRpcException("RpcServerInqBindings", num);
				}
				if (annotation != null && annotation.Length > 0)
				{
					ptr2 = (ushort*)Marshal.StringToHGlobalUni(annotation).ToPointer();
				}
				rpcServerBase.RegisterEp(intPtr.ToPointer(), pBindingVector, null, ptr2);
				uint num4 = isLocalOnly ? 10U : 9U;
				if (!isLocalOnly)
				{
					ptr = (ushort*)Marshal.StringToHGlobalUni(string.Format("{0}$", Environment.MachineName)).ToPointer();
				}
				num = <Module>.RpcServerRegisterAuthInfoW(ptr, num4, 0L, null);
				if (num != null)
				{
					RpcServerBase.ThrowRpcException("RpcServerRegisterAuthInfo", num);
				}
			}
			finally
			{
				if (ptr != null)
				{
					IntPtr hglobal = new IntPtr((void*)ptr);
					Marshal.FreeHGlobal(hglobal);
				}
				if (null != ptr2)
				{
					IntPtr hglobal2 = new IntPtr((void*)ptr2);
					Marshal.FreeHGlobal(hglobal2);
				}
				num = <Module>.RpcBindingVectorFree(&pBindingVector);
				if (num != null)
				{
					RpcServerBase.ThrowRpcException("RpcBindingVectorFree", num);
				}
			}
			_GUID guid;
			cpblk(ref guid, ref rpc_IF_ID, 16);
			Guid uuid = <Module>.Microsoft.Exchange.Rpc.?A0x2ca62c65.GuidFromGUID(ref guid);
			rpcServerBase.m_uuid = uuid;
			RpcServerBase.serverCache.Add(rpcServerBase.m_uuid, rpcServerBase);
			return rpcServerBase;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000013B0 File Offset: 0x000007B0
		protected unsafe static RpcServerBase RegisterServer(Type type, ObjectSecurity sd, uint desiredAccess, ValueType mgrTypeGuid, void* mgrEpv, string annotation, [MarshalAs(UnmanagedType.U1)] bool isLocalOnly, uint maxCalls)
		{
			return RpcServerBase.RegisterServer(type, sd, desiredAccess, mgrTypeGuid, mgrEpv, annotation, isLocalOnly, true, maxCalls);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000519F0 File Offset: 0x00050DF0
		public unsafe static void UnregisterInterface(IntPtr hIntf, [MarshalAs(UnmanagedType.U1)] bool fWaitForCalls)
		{
			if (hIntf == IntPtr.Zero)
			{
				throw new ArgumentNullException();
			}
			RpcServerBase serverInstance = RpcServerBase.GetServerInstance(hIntf);
			if (serverInstance != null)
			{
				_RPC_BINDING_VECTOR* ptr = null;
				try
				{
					int num = <Module>.RpcServerInqBindings(&ptr);
					if (num != null)
					{
						RpcServerBase.ThrowRpcException("RpcServerInqBindings", num);
					}
					serverInstance.UnregisterEp(hIntf.ToPointer(), ptr, null);
				}
				finally
				{
					if (ptr != null)
					{
						<Module>.RpcBindingVectorFree(&ptr);
						ptr = null;
					}
				}
				int waitForCallsToComplete = fWaitForCalls ? 1 : 0;
				serverInstance.UnregisterInterface(hIntf.ToPointer(), null, (uint)waitForCallsToComplete);
				RpcServerBase.serverCache.Remove(serverInstance.m_uuid);
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0000160C File Offset: 0x00000A0C
		public static void UnregisterInterface(IntPtr hIntf)
		{
			RpcServerBase.UnregisterInterface(hIntf, false);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000013D0 File Offset: 0x000007D0
		protected unsafe virtual void UnregisterInterface(void* ifSpec, _GUID* pMgrTypeUuid, uint waitForCallsToComplete)
		{
			int num = <Module>.RpcServerUnregisterIf(ifSpec, pMgrTypeUuid, waitForCallsToComplete);
			if (num != null)
			{
				RpcServerBase.ThrowRpcException("Could not unregister interface", num);
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000013F4 File Offset: 0x000007F4
		protected unsafe virtual void RegisterEp(void* ifSpec, _RPC_BINDING_VECTOR* pBindingVector, _UUID_VECTOR* pUuidVector, ushort* wszAnnotation)
		{
			int num = <Module>.RpcEpRegisterW(ifSpec, pBindingVector, pUuidVector, wszAnnotation);
			if (num != null)
			{
				RpcServerBase.ThrowRpcException("RpcEpRegister", num);
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000141C File Offset: 0x0000081C
		protected unsafe virtual void UnregisterEp(void* ifSpec, _RPC_BINDING_VECTOR* pBindingVector, _UUID_VECTOR* pUuidVector)
		{
			int num = <Module>.RpcEpUnregister(ifSpec, pBindingVector, pUuidVector);
			if (num != null)
			{
				RpcServerBase.TraceError("Function {0} failed with {1}", new object[]
				{
					"Microsoft::Exchange::Rpc::RpcServerBase::UnregisterEp",
					num
				});
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00051AA0 File Offset: 0x00050EA0
		protected unsafe static void CheckDuplicateEndpoint(_RPC_IF_ID* pRpcIfId, _GUID* objectUUID, [MarshalAs(UnmanagedType.U1)] bool allowInstanceEndpoints)
		{
			void** ptr = null;
			int num = allowInstanceEndpoints ? 3 : 1;
			int num2 = <Module>.RpcMgmtEpEltInqBegin(null, num, pRpcIfId, 1, objectUUID, &ptr);
			if (num2 != null)
			{
				RpcServerBase.TraceError("RpcMgmtEpEltInqBegin failed with status {2} in file {0} line {1}", new object[]
				{
					"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
					2541,
					num2
				});
				RpcServerBase.ThrowRpcException("RpcMgmtEpEltInqBegin", num2);
			}
			try
			{
				_RPC_IF_ID rpc_IF_ID;
				initblk(ref rpc_IF_ID, 0, 20L);
				_RPC_IF_ID rpc_IF_ID2 = rpc_IF_ID;
				_GUID guid;
				num2 = <Module>.RpcMgmtEpEltInqNextW(ptr, &rpc_IF_ID2, null, &guid, null);
				if (num2 == null)
				{
					if (allowInstanceEndpoints)
					{
						ulong num3 = 16UL;
						_GUID* ptr2 = objectUUID;
						_GUID* ptr3 = &guid;
						int num4 = 0;
						for (;;)
						{
							byte b = *(byte*)ptr3;
							byte b2 = *ptr2;
							if (b < b2 || b > b2)
							{
								goto IL_152;
							}
							if (num3 == 1UL)
							{
								break;
							}
							num3 -= 1UL;
							ptr3 += 1L / (long)sizeof(_GUID);
							ptr2 += 1L;
						}
						if (0 != num4)
						{
							goto IL_152;
						}
					}
					RpcServerBase.TraceError("The interface we are trying to register already exists. RpcMgmtEpEltInqNext returned status {2} in file {0} line {1}", new object[]
					{
						"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
						2565,
						num2
					});
					throw new DuplicateRpcEndpointException("The interface we are trying to register already exists.");
				}
				if (num2 != 1772)
				{
					RpcServerBase.TraceError("RpcMgmtEpEltInqNext failed with status {2} in file {0} line {1}", new object[]
					{
						"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
						2572,
						num2
					});
					RpcServerBase.ThrowRpcException("RpcMgmtEpEltInqNext", num2);
				}
				IL_152:;
			}
			finally
			{
				<Module>.RpcMgmtEpEltInqDone(&ptr);
			}
		}

		// Token: 0x17000005 RID: 5
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x0000146C File Offset: 0x0000086C
		public ObjectSecurity SecurityDescriptor
		{
			set
			{
				this.m_binarySd = value.GetSecurityDescriptorBinaryForm();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00001488 File Offset: 0x00000888
		public byte[] SecurityDescriptorBinaryForm
		{
			get
			{
				return this.m_binarySd;
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00050FBC File Offset: 0x000503BC
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe static bool InterfaceAlreadyRegistered(IntPtr hintf)
		{
			bool result = true;
			_RPC_IF_ID rpc_IF_ID;
			int num = <Module>.RpcIfInqId(hintf.ToPointer(), &rpc_IF_ID);
			if (num != null)
			{
				RpcServerBase.ThrowRpcException("RpcIfInqId", num);
			}
			void** ptr;
			int num2 = <Module>.RpcMgmtEpEltInqBegin(null, 1, &rpc_IF_ID, 1, null, &ptr);
			if (num2 != null)
			{
				RpcServerBase.ThrowRpcException("RpcMgmtEpEltInqBegin", num2);
			}
			int num3;
			try
			{
				_RPC_IF_ID rpc_IF_ID2;
				num2 = <Module>.RpcMgmtEpEltInqNextW(ptr, &rpc_IF_ID2, null, null, null);
				if (num2 == 1772)
				{
					result = false;
				}
				else if (num2 != null)
				{
					RpcServerBase.ThrowRpcException("RpcMgmtEpEltInqNext", num2);
				}
			}
			finally
			{
				num3 = <Module>.RpcMgmtEpEltInqDone(&ptr);
			}
			if (num3 != null)
			{
				RpcServerBase.ThrowRpcException("RpcMgmtEpEltInqDone", num3);
			}
			return result;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000520E4 File Offset: 0x000514E4
		public static void StopServer(IntPtr hIntf)
		{
			RpcServerBase.UnregisterInterface(hIntf, false);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00051770 File Offset: 0x00050B70
		public unsafe static RpcServerBase GetServerInstance(IntPtr hIntf)
		{
			_RPC_IF_ID rpc_IF_ID;
			int num = <Module>.RpcIfInqId(hIntf.ToPointer(), &rpc_IF_ID);
			if (num != null)
			{
				RpcServerBase.TraceError("RpcIfInqId failed with status {2} in file {0} line {1}", new object[]
				{
					"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
					1777,
					num
				});
				RpcServerBase.ThrowRpcException("RpcIfInqId - this probably means this interface is not registered", num);
			}
			_GUID guid;
			cpblk(ref guid, ref rpc_IF_ID, 16);
			Guid guid2 = <Module>.Microsoft.Exchange.Rpc.?A0x2ca62c65.GuidFromGUID(ref guid);
			return RpcServerBase.serverCache.Lookup(guid2);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000517E8 File Offset: 0x00050BE8
		public unsafe static void RegisterProtocols(string[] protocolSequences, string[] protocolEndpoints)
		{
			if (protocolSequences == null)
			{
				throw new ArgumentNullException("protocolSequences");
			}
			if (protocolEndpoints != null && protocolSequences.Length != protocolEndpoints.Length)
			{
				throw new ArgumentException("protocolSequences and protocolEndpoints arrays aren't same length");
			}
			ushort* ptr = null;
			ushort* ptr2 = null;
			if (<Module>.Microsoft.Exchange.Rpc.ManagedExceptionCrashWrapper.FInitializeCrashThread() == null)
			{
				throw new Exception("Unable to initialize crash thread");
			}
			int num2;
			for (int i = 0; i < protocolSequences.Length; i++)
			{
				string text = protocolSequences[i];
				string text2;
				if (protocolEndpoints == null)
				{
					text2 = null;
				}
				else
				{
					text2 = protocolEndpoints[i];
				}
				string arg = text2;
				try
				{
					int num = 0;
					ptr = <Module>.StringToUnmanaged(text);
					ptr2 = ((!(text2 != null)) ? null : <Module>.StringToUnmanaged(text2));
					for (;;)
					{
						if (ptr2 != null)
						{
							num2 = <Module>.RpcServerUseProtseqEpW(ptr, 10U, ptr2, null);
						}
						else
						{
							num2 = <Module>.RpcServerUseProtseqW(ptr, 10U, null);
						}
						if (num2 != 1740)
						{
							goto IL_B1;
						}
						num++;
						if (num > 15)
						{
							break;
						}
						<Module>.Sleep(1000);
					}
					goto IL_B4;
					IL_B1:
					if (num2 == null)
					{
						goto IL_FE;
					}
					IL_B4:
					string routineName = string.Format((ptr2 == null) ? "RpcServerUseProtseq({0})" : "RpcServerUseProtseqEp({0}, {1})", text, arg);
					throw <Module>.Microsoft.Exchange.Rpc.GetRpcExceptionWithEEInfo(num2, routineName);
				}
				finally
				{
					if (ptr != null)
					{
						<Module>.MIDL_user_free((void*)ptr);
						ptr = null;
					}
					if (ptr2 != null)
					{
						<Module>.MIDL_user_free((void*)ptr2);
						ptr2 = null;
					}
				}
				IL_FE:;
			}
			num2 = <Module>.RpcServerRegisterAuthInfoW(null, 10, 0L, null);
			if (num2 != null)
			{
				RpcServerBase.ThrowRpcException("RpcServerRegisterAuthInfo(RPC_C_AUTHN_WINNT)", num2);
			}
			num2 = <Module>.RpcServerRegisterAuthInfoW(null, 16, 0L, null);
			if (num2 != null)
			{
				RpcServerBase.ThrowRpcException("RpcServerRegisterAuthInfo(RPC_C_AUTHN_GSS_KERBEROS)", num2);
			}
			num2 = <Module>.RpcServerRegisterAuthInfoW(null, 9, 0L, null);
			if (num2 != null)
			{
				RpcServerBase.ThrowRpcException("RpcServerRegisterAuthInfo(RPC_C_AUTHN_GSS_NEGOTIATE)", num2);
			}
			num2 = <Module>.RpcServerRegisterAuthInfoW(null, 14, 0L, null);
			if (num2 != null)
			{
				RpcServerBase.ThrowRpcException("RpcServerRegisterAuthInfo(RPC_C_AUTHN_GSS_SCHANNEL)", num2);
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000520F8 File Offset: 0x000514F8
		public static void StartGlobalServer(string[] protocolSequences, string[] protocolEndpoints, uint minCalls, uint maxCalls)
		{
			bool flag = false;
			try
			{
				RpcServerBase.RegisterProtocols(protocolSequences, protocolEndpoints);
				int num = <Module>.RpcServerListen(minCalls, maxCalls, 1U);
				if (num != null)
				{
					RpcServerBase.ThrowRpcException("RpcServerListen", num);
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					RpcServerBase.StopGlobalServer();
				}
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000529F0 File Offset: 0x00051DF0
		public static void StartGlobalServer(string[] protocolSequences, string[] protocolEndpoints, uint maxCalls)
		{
			RpcServerBase.StartGlobalServer(protocolSequences, protocolEndpoints, maxCalls, maxCalls);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00051988 File Offset: 0x00050D88
		public static void StopGlobalServer()
		{
			int num = <Module>.RpcMgmtStopServerListening(null);
			if (num != null)
			{
				RpcServerBase.ThrowRpcException("RpcMgmtStopServerListening", num);
			}
			int num2 = <Module>.RpcMgmtWaitServerListen();
			if (num2 != null)
			{
				RpcServerBase.ThrowRpcException("RpcMgmtWaitServerListening", num2);
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000015E8 File Offset: 0x000009E8
		public static RpcServerBase RegisterAutoListenInterface(Type type, ObjectSecurity securityDescriptor, int maxCalls, [MarshalAs(UnmanagedType.U1)] bool isLocalOnly, [MarshalAs(UnmanagedType.U1)] bool isSecureOnly, string annotation, [MarshalAs(UnmanagedType.U1)] bool registerEndpoint, [MarshalAs(UnmanagedType.U1)] bool allowInstanceEndpoints, [MarshalAs(UnmanagedType.U1)] bool allowNoAuthentication)
		{
			return RpcServerBase.RegisterInterface(type, securityDescriptor, 1, 1, maxCalls, isLocalOnly, isSecureOnly, registerEndpoint, allowInstanceEndpoints, allowNoAuthentication, annotation);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x000015C4 File Offset: 0x000009C4
		public static RpcServerBase RegisterAutoListenInterface(Type type, int maxCalls, [MarshalAs(UnmanagedType.U1)] bool isLocalOnly, string annotation, [MarshalAs(UnmanagedType.U1)] bool registerEndpoint, [MarshalAs(UnmanagedType.U1)] bool allowInstanceEndpoints, [MarshalAs(UnmanagedType.U1)] bool allowNoAuthentication)
		{
			return RpcServerBase.RegisterInterface(type, RpcServerBase.BuildDefaultSecurityDescriptor(), 1, 1, maxCalls, isLocalOnly, isLocalOnly, registerEndpoint, allowInstanceEndpoints, allowNoAuthentication, annotation);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000019EC File Offset: 0x00000DEC
		public static RpcServerBase RegisterAutoListenInterface(Type type, string annotation, [MarshalAs(UnmanagedType.U1)] bool registerEndpoint)
		{
			return RpcServerBase.RegisterInterface(type, RpcServerBase.BuildDefaultSecurityDescriptor(), 1, 1, 1234, false, false, registerEndpoint, false, false, annotation);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000019A4 File Offset: 0x00000DA4
		public static RpcServerBase RegisterAutoListenInterface(Type type, int maxCalls, [MarshalAs(UnmanagedType.U1)] bool isLocalOnly, [MarshalAs(UnmanagedType.U1)] bool allowInstanceEndpoints, string annotation)
		{
			return RpcServerBase.RegisterInterface(type, RpcServerBase.BuildDefaultSecurityDescriptor(), 1, 1, maxCalls, isLocalOnly, isLocalOnly, true, allowInstanceEndpoints, false, annotation);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00001A30 File Offset: 0x00000E30
		public static RpcServerBase RegisterAutoListenInterface(Type type, int maxCalls)
		{
			return RpcServerBase.RegisterInterface(type, RpcServerBase.BuildDefaultSecurityDescriptor(), 1, 1, maxCalls, false, false, true, false, false, null);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000019C8 File Offset: 0x00000DC8
		public static RpcServerBase RegisterAutoListenInterfaceSupportingAnonymous(Type type, int maxCalls, string annotation, [MarshalAs(UnmanagedType.U1)] bool registerEndpoint)
		{
			return RpcServerBase.RegisterInterface(type, RpcServerBase.BuildDefaultSecurityDescriptor(), 1, 1, maxCalls, false, false, registerEndpoint, false, true, annotation);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00050F88 File Offset: 0x00050388
		public static void ThrowRpcException(string message, int rpcStatus)
		{
			RpcServerBase.TraceError("ThrowRpcException has been invoked on server with rpcStatus = {0}. Message = {1}", new object[]
			{
				rpcStatus,
				message
			});
			throw <Module>.GetRpcException(rpcStatus, message);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00050C18 File Offset: 0x00050018
		public unsafe static int GetSecurityDetails(void* Interface, void* InterfaceDetails)
		{
			Guid guid = <Module>.Microsoft.Exchange.Rpc.?A0x2ca62c65.GuidFromGUID(Interface);
			RpcServerBase rpcServerBase = RpcServerBase.serverCache.Lookup(guid);
			if (rpcServerBase == null)
			{
				return 0;
			}
			*(int*)((byte*)InterfaceDetails + 4L) = (int)rpcServerBase.m_desiredAccess;
			byte[] binarySd = rpcServerBase.m_binarySd;
			if (binarySd != null)
			{
				uint num = 0;
				byte* ptr = (byte*)<Module>.MToUBytes(binarySd, (int*)(&num)).ToPointer();
				if (<Module>.X_InterlockedCompareExchangePointer((void**)((byte*)InterfaceDetails + 8L), (void*)ptr, null) != null)
				{
					IntPtr hglobal = new IntPtr((void*)ptr);
					Marshal.FreeHGlobal(hglobal);
				}
			}
			int num2 = rpcServerBase.m_isSecureOnly ? 1 : 0;
			*(int*)((byte*)InterfaceDetails + 16L) = num2;
			int num3 = rpcServerBase.m_isLocalOnly ? 1 : 0;
			*(int*)((byte*)InterfaceDetails + 20L) = num3;
			int num4 = rpcServerBase.m_allowNoAuthentication ? 1 : 0;
			*(int*)((byte*)InterfaceDetails + 24L) = num4;
			int num5 = rpcServerBase.m_isHttpAllowed ? 1 : 0;
			*(int*)((byte*)InterfaceDetails + 28L) = num5;
			return 1;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00050CD4 File Offset: 0x000500D4
		public unsafe static int CheckSecurity(void* Interface, void* Context)
		{
			Guid guid = <Module>.Microsoft.Exchange.Rpc.?A0x2ca62c65.GuidFromGUID(Interface);
			RpcServerBase rpcServerBase = RpcServerBase.serverCache.Lookup(guid);
			int num;
			if (rpcServerBase == null)
			{
				RpcServerBase.TraceError("intfToInstanceHash returned nullptr in file {0} line {1}", new object[]
				{
					"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
					2659
				});
				num = -2147418113;
			}
			else
			{
				tagRPC_CALL_ATTRIBUTES_V2_W tagRPC_CALL_ATTRIBUTES_V2_W;
				initblk(ref tagRPC_CALL_ATTRIBUTES_V2_W, 0, 112L);
				tagRPC_CALL_ATTRIBUTES_V2_W = 2;
				*(ref tagRPC_CALL_ATTRIBUTES_V2_W + 4) = 64;
				num = <Module>.RpcServerInqCallAttributesW(Context, (void*)(&tagRPC_CALL_ATTRIBUTES_V2_W));
				if (num == null)
				{
					if (((*(ref tagRPC_CALL_ATTRIBUTES_V2_W + 48) == 1) ? 1 : 0) != 0)
					{
						RpcServerBase.TraceError("We do not support Null RPC Sessions in file {0} line {1}", new object[]
						{
							"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
							2695
						});
						num = -2147418113;
						goto IL_27E;
					}
					if (rpcServerBase.m_isSecureOnly && 6 != *(ref tagRPC_CALL_ATTRIBUTES_V2_W + 40))
					{
						RpcServerBase.TraceError("We only support PKT_PRIVACY Authentication Level in file {0} line {1}", new object[]
						{
							"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
							2706
						});
						num = -2147418113;
						goto IL_27E;
					}
					if (<Module>.Microsoft.Exchange.Rpc.FIsProtSeq(Context, (ushort*)(&<Module>.??_C@_1BA@EONDGCCM@?$AAn?$AAc?$AAa?$AAl?$AAr?$AAp?$AAc?$AA?$AA@)) != 1)
					{
						if (rpcServerBase.m_isLocalOnly)
						{
							RpcServerBase.TraceError("Refusing connection from non local source (LRPC only configured) in file {0} line {1}", new object[]
							{
								"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
								2717
							});
							num = -2147417829;
							goto IL_27E;
						}
						if (16 != *(ref tagRPC_CALL_ATTRIBUTES_V2_W + 44) && 9 != *(ref tagRPC_CALL_ATTRIBUTES_V2_W + 44) && 10 != *(ref tagRPC_CALL_ATTRIBUTES_V2_W + 44) && 14 != *(ref tagRPC_CALL_ATTRIBUTES_V2_W + 44) && 32 != *(ref tagRPC_CALL_ATTRIBUTES_V2_W + 44) && 82 != *(ref tagRPC_CALL_ATTRIBUTES_V2_W + 44) && !rpcServerBase.m_allowNoAuthentication)
						{
							RpcServerBase.TraceError("Unsupported authentication service {2} used in file {0} line {1}", new object[]
							{
								"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
								2734,
								(uint)(*(ref tagRPC_CALL_ATTRIBUTES_V2_W + 44))
							});
							num = -2147417829;
							goto IL_27E;
						}
						if (<Module>.Microsoft.Exchange.Rpc.FIsProtSeq(Context, (ushort*)(&<Module>.??_C@_1BK@BPGFLIHL@?$AAn?$AAc?$AAa?$AAc?$AAn?$AA_?$AAi?$AAp?$AA_?$AAt?$AAc?$AAp?$AA?$AA@)) != 1)
						{
							if (<Module>.Microsoft.Exchange.Rpc.FIsProtSeq(Context, (ushort*)(&<Module>.??_C@_1BG@FOAHFCPJ@?$AAn?$AAc?$AAa?$AAc?$AAn?$AA_?$AAh?$AAt?$AAt?$AAp?$AA?$AA@)) != 1)
							{
								object[] args = new object[0];
								RpcServerBase.TraceError("Refusing connection because it's not a supported protocol sequence", args);
								num = -2147417829;
								goto IL_27E;
							}
							if (!rpcServerBase.m_isHttpAllowed)
							{
								object[] args2 = new object[0];
								RpcServerBase.TraceError("Refusing RPC/HTTP connection because it's not support for this interface", args2);
								num = -2147417829;
								goto IL_27E;
							}
						}
					}
					if (rpcServerBase.m_allowNoAuthentication)
					{
						return num;
					}
					num = RpcServerBase.CheckObjectSecurity(rpcServerBase.m_binarySd, rpcServerBase.m_desiredAccess, Context);
				}
				else
				{
					RpcServerBase.TraceError("RpcServerInqCallAttributes returned {2} in file {0} line {1}", new object[]
					{
						"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
						2686,
						num
					});
				}
				if (num == null)
				{
					return num;
				}
			}
			IL_27E:
			RpcServerBase.TraceError("Function {0} failed with {1}", new object[]
			{
				"Microsoft::Exchange::Rpc::RpcServerBase::CheckSecurity",
				num
			});
			return num;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0005039C File Offset: 0x0004F79C
		public unsafe static int CheckObjectSecurity(byte[] sd, uint desiredAccess, void* Context)
		{
			_LUID luid;
			initblk(ref luid, 0, 8L);
			AUTHZ_CLIENT_CONTEXT_HANDLE__* ptr = null;
			_AUTHZ_ACCESS_REQUEST authz_ACCESS_REQUEST;
			initblk(ref authz_ACCESS_REQUEST, 0, 40L);
			*(ref authz_ACCESS_REQUEST + 8) = 0L;
			*(ref authz_ACCESS_REQUEST + 16) = 0L;
			*(ref authz_ACCESS_REQUEST + 24) = 0;
			*(ref authz_ACCESS_REQUEST + 32) = 0L;
			_AUTHZ_ACCESS_REPLY authz_ACCESS_REPLY;
			initblk(ref authz_ACCESS_REPLY, 0, 32L);
			uint num = 0;
			uint num2 = 0;
			uint num3 = 0;
			authz_ACCESS_REPLY = 1;
			*(ref authz_ACCESS_REPLY + 8) = ref num;
			*(ref authz_ACCESS_REPLY + 16) = ref num3;
			*(ref authz_ACCESS_REPLY + 24) = ref num2;
			int num4;
			if (sd == null)
			{
				RpcServerBase.TraceError("Security Descriptor on RpcServerBase is null in file {0} line {1}", new object[]
				{
					"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
					2817
				});
				num4 = -2147418113;
			}
			else
			{
				authz_ACCESS_REQUEST = desiredAccess;
				num4 = <Module>.RpcGetAuthorizationContextForClient(Context, 0, null, null, luid, 0, null, (void**)(&ptr));
				if (num4 != null)
				{
					RpcServerBase.TraceError("RpcGetAuthorizationContextForClient failed with status {2} in file {0} line {1}", new object[]
					{
						"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
						2837,
						num4
					});
				}
				else
				{
					ref byte byte& = ref sd[0];
					if (<Module>.AuthzAccessCheck(0, ptr, &authz_ACCESS_REQUEST, null, ref byte&, null, 0, &authz_ACCESS_REPLY, null) != 1)
					{
						num4 = <Module>.GetLastError();
						num4 = ((num4 == 0) ? -2147418113 : num4);
						RpcServerBase.TraceError("AuthzAccessCheck failed with status {2:x} in file {0} line {1}", new object[]
						{
							"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
							2860,
							num4
						});
					}
					else if (*(ref authz_ACCESS_REPLY + 24) != 0L && *(*(ref authz_ACCESS_REPLY + 24)) == 0)
					{
						num4 = 0;
					}
					else
					{
						num4 = 5;
						RpcServerBase.TraceError("Access Denied detected in file {0} line {1}", new object[]
						{
							"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
							2872
						});
					}
				}
			}
			if (ptr != null && <Module>.AuthzFreeContext(ptr) != 1)
			{
				RpcServerBase.TraceError("AuthzFreeContext failed with {2} in file {0} line {1}", new object[]
				{
					"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
					2881,
					<Module>.GetLastError()
				});
			}
			if (num4 != null)
			{
				RpcServerBase.TraceError("Function {0} failed with {1}", new object[]
				{
					"Microsoft::Exchange::Rpc::RpcServerBase::CheckObjectSecurity",
					num4
				});
			}
			return num4;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000505A8 File Offset: 0x0004F9A8
		internal unsafe static ClientSecurityContext GetClientSecurityContext(void* rpcHandle)
		{
			AUTHZ_CLIENT_CONTEXT_HANDLE__* value = null;
			_LUID luid;
			initblk(ref luid, 0, 8L);
			int num = <Module>.RpcGetAuthorizationContextForClient(rpcHandle, 0, null, null, luid, 0, null, (void**)(&value));
			if (num != null)
			{
				RpcServerBase.TraceError("RpcGetAuthorizationContextForClient failed with status {2} in file {0} line {1}", new object[]
				{
					"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\rpchelper.cpp",
					2924,
					num
				});
				return null;
			}
			IntPtr authenticatedUserHandle = new IntPtr((void*)value);
			return new ClientSecurityContext(authenticatedUserHandle);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00001620 File Offset: 0x00000A20
		internal static ClientSecurityContext GetClientSecurityContext(IntPtr rpcHandle)
		{
			return RpcServerBase.GetClientSecurityContext(rpcHandle.ToPointer());
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0005061C File Offset: 0x0004FA1C
		internal unsafe static void GetBindingInformation(void* rpcHandle, out string clientNetworkAddress, out string clientEndpoint, out string serverNetworkAddress, out string serverEndpoint, out string protocolSequence)
		{
			void* ptr = null;
			ushort* ptr2 = null;
			ushort* ptr3 = null;
			ushort* ptr4 = null;
			ushort* ptr5 = null;
			ushort* ptr6 = null;
			ushort* ptr7 = null;
			ushort* ptr8 = null;
			clientNetworkAddress = null;
			clientEndpoint = null;
			serverNetworkAddress = null;
			serverEndpoint = null;
			protocolSequence = null;
			try
			{
				if (<Module>.RpcBindingServerFromClient(rpcHandle, &ptr) == null && <Module>.RpcBindingToStringBindingW(ptr, &ptr3) == null)
				{
					<Module>.RpcStringBindingParseW(ptr3, null, &ptr8, &ptr4, &ptr5, null);
				}
				if (<Module>.RpcBindingToStringBindingW(rpcHandle, &ptr2) == null)
				{
					<Module>.RpcStringBindingParseW(ptr2, null, null, &ptr6, &ptr7, null);
				}
				if (ptr4 != null)
				{
					IntPtr ptr9 = new IntPtr((void*)ptr4);
					clientNetworkAddress = Marshal.PtrToStringUni(ptr9);
				}
				if (ptr5 != null)
				{
					IntPtr ptr10 = new IntPtr((void*)ptr5);
					clientEndpoint = Marshal.PtrToStringUni(ptr10);
				}
				if (ptr6 != null)
				{
					IntPtr ptr11 = new IntPtr((void*)ptr6);
					serverNetworkAddress = Marshal.PtrToStringUni(ptr11);
				}
				if (ptr7 != null)
				{
					IntPtr ptr12 = new IntPtr((void*)ptr7);
					serverEndpoint = Marshal.PtrToStringUni(ptr12);
				}
				if (ptr8 != null)
				{
					IntPtr ptr13 = new IntPtr((void*)ptr8);
					protocolSequence = Marshal.PtrToStringUni(ptr13);
				}
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.RpcBindingFree(&ptr);
				}
				if (ptr2 != null)
				{
					<Module>.RpcStringFreeW(&ptr2);
				}
				if (ptr3 != null)
				{
					<Module>.RpcStringFreeW(&ptr3);
				}
				if (ptr4 != null)
				{
					<Module>.RpcStringFreeW(&ptr4);
				}
				if (ptr5 != null)
				{
					<Module>.RpcStringFreeW(&ptr5);
				}
				if (ptr6 != null)
				{
					<Module>.RpcStringFreeW(&ptr6);
				}
				if (ptr7 != null)
				{
					<Module>.RpcStringFreeW(&ptr7);
				}
				if (ptr8 != null)
				{
					<Module>.RpcStringFreeW(&ptr8);
				}
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000163C File Offset: 0x00000A3C
		internal static void GetBindingInformation(IntPtr rpcHandle, out string clientNetworkAddress, out string clientEndpoint, out string serverNetworkAddress, out string serverEndpoint, out string protocolSequence)
		{
			RpcServerBase.GetBindingInformation(rpcHandle.ToPointer(), out clientNetworkAddress, out clientEndpoint, out serverNetworkAddress, out serverEndpoint, out protocolSequence);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00050784 File Offset: 0x0004FB84
		[return: MarshalAs(UnmanagedType.U1)]
		internal unsafe static bool IsConnectionEncrypted(void* hBinding)
		{
			bool flag = false;
			tagRPC_CALL_ATTRIBUTES_V2_W tagRPC_CALL_ATTRIBUTES_V2_W;
			initblk(ref tagRPC_CALL_ATTRIBUTES_V2_W, 0, 112L);
			tagRPC_CALL_ATTRIBUTES_V2_W = 2;
			int num = <Module>.RpcServerInqCallAttributesW(hBinding, (void*)(&tagRPC_CALL_ATTRIBUTES_V2_W));
			if (0 == num || 234 == num)
			{
				flag = (6 == *(ref tagRPC_CALL_ATTRIBUTES_V2_W + 40) || flag);
			}
			return flag;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000507C4 File Offset: 0x0004FBC4
		internal unsafe static AuthenticationService GetAuthenticationType(void* hBinding)
		{
			AuthenticationService authenticationService = AuthenticationService.None;
			tagRPC_CALL_ATTRIBUTES_V2_W tagRPC_CALL_ATTRIBUTES_V2_W;
			initblk(ref tagRPC_CALL_ATTRIBUTES_V2_W, 0, 112L);
			tagRPC_CALL_ATTRIBUTES_V2_W = 2;
			int num = <Module>.RpcServerInqCallAttributesW(hBinding, (void*)(&tagRPC_CALL_ATTRIBUTES_V2_W));
			if (0 == num || 234 == num)
			{
				authenticationService = (AuthenticationService)(*(ref tagRPC_CALL_ATTRIBUTES_V2_W + 44));
			}
			ExTraceGlobals.RpcTracer.Information<AuthenticationService>((long)hBinding, "Authentication type: {0}", authenticationService);
			return authenticationService;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00050810 File Offset: 0x0004FC10
		internal unsafe static Guid GetClientIdentifier(void* hBinding)
		{
			tagRPC_CALL_ATTRIBUTES_V3_W tagRPC_CALL_ATTRIBUTES_V3_W;
			initblk(ref tagRPC_CALL_ATTRIBUTES_V3_W, 0, 120L);
			Guid result;
			try
			{
				tagRPC_CALL_ATTRIBUTES_V3_W = 3;
				*(ref tagRPC_CALL_ATTRIBUTES_V3_W + 4) = 192;
				int num = <Module>.RpcServerInqCallAttributesW(hBinding, (void*)(&tagRPC_CALL_ATTRIBUTES_V3_W));
				if (num == 234)
				{
					*(ref tagRPC_CALL_ATTRIBUTES_V3_W + 112) = <Module>.malloc((ulong)(*(ref tagRPC_CALL_ATTRIBUTES_V3_W + 108)));
					num = <Module>.RpcServerInqCallAttributesW(hBinding, (void*)(&tagRPC_CALL_ATTRIBUTES_V3_W));
				}
				if (num == 87)
				{
					ExTraceGlobals.RpcTracer.TraceError(0L, "RpcServerBase::GetClientIdentifier - Required hotfix (KB2619234) doesn't appear to be installed on machine");
					result = Guid.Empty;
				}
				else if (num != null)
				{
					ExTraceGlobals.RpcTracer.TraceError<int>(0L, "RpcServerBase::GetClientIdentifier - Unable to get client identifier {0}", num);
					result = Guid.Empty;
				}
				else if (*(ref tagRPC_CALL_ATTRIBUTES_V3_W + 108) == 16 && *(ref tagRPC_CALL_ATTRIBUTES_V3_W + 112) != 0L)
				{
					result = (Guid)Marshal.PtrToStructure((IntPtr)(*(ref tagRPC_CALL_ATTRIBUTES_V3_W + 112)), typeof(Guid));
				}
				else
				{
					IntPtr arg = (IntPtr)(*(ref tagRPC_CALL_ATTRIBUTES_V3_W + 112));
					ExTraceGlobals.RpcTracer.TraceError<uint, IntPtr>(0L, "RpcServerBase::GetClientIdentifier - Client identifier not valid [size={0}, ptr={1}]", *(ref tagRPC_CALL_ATTRIBUTES_V3_W + 108), arg);
					result = Guid.Empty;
				}
			}
			finally
			{
				if (*(ref tagRPC_CALL_ATTRIBUTES_V3_W + 112) != 0L)
				{
					<Module>.free(*(ref tagRPC_CALL_ATTRIBUTES_V3_W + 112));
				}
			}
			return result;
		}

		// Token: 0x0400081E RID: 2078
		private static readonly RpcServerCache serverCache = new RpcServerCache();

		// Token: 0x0400081F RID: 2079
		private static string TraceCategory = "RpcServerBase";

		// Token: 0x04000820 RID: 2080
		private byte[] m_binarySd;

		// Token: 0x04000821 RID: 2081
		private bool m_isLocalOnly;

		// Token: 0x04000822 RID: 2082
		private bool m_isSecureOnly;

		// Token: 0x04000823 RID: 2083
		private bool m_isHttpAllowed;

		// Token: 0x04000824 RID: 2084
		private bool m_allowNoAuthentication;

		// Token: 0x04000825 RID: 2085
		private Guid m_uuid;

		// Token: 0x04000826 RID: 2086
		private uint m_desiredAccess;

		// Token: 0x04000827 RID: 2087
		private static readonly CheckSecurityDelegate checkSecurityDelegate;

		// Token: 0x04000828 RID: 2088
		public static readonly ArraySegment<byte> EmptyArraySegment;

		// Token: 0x04000829 RID: 2089
		public static readonly int DefaultMaxRpcCalls;
	}
}
