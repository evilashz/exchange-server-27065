using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.AdminRpc
{
	// Token: 0x02000146 RID: 326
	internal abstract class AdminRpcServerBase : RpcServerBase
	{
		// Token: 0x060008DB RID: 2267 RVA: 0x00008D34 File Offset: 0x00008134
		protected unsafe override void RegisterInterface(void* ifSpec, ValueType mgrTypeGuid, _GUID* pMgrTypeUuid, void* pMgrEpv, uint flags, uint maxCalls)
		{
			if (null == mgrTypeGuid)
			{
				throw new RpcException("mgrTypeGuid is null");
			}
			Guid? mgrTypeGuid2 = (Guid)mgrTypeGuid;
			this.m_mgrTypeGuid = mgrTypeGuid2;
			base.RegisterInterface(ifSpec, null, null, null, flags, maxCalls);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00008D78 File Offset: 0x00008178
		protected unsafe override void UnregisterInterface(void* ifSpec, _GUID* pMgrTypeUuid, uint waitForCallsToComplete)
		{
			if (this.m_mgrTypeGuid != null)
			{
				int num = <Module>.RpcServerUnregisterIf(ifSpec, null, waitForCallsToComplete);
				if (num != null)
				{
					RpcServerBase.ThrowRpcException("Could not unregister interface", num);
				}
			}
			else
			{
				int num2 = <Module>.RpcServerUnregisterIf(ifSpec, pMgrTypeUuid, waitForCallsToComplete);
				if (num2 != null)
				{
					RpcServerBase.ThrowRpcException("Could not unregister interface", num2);
				}
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00008DC4 File Offset: 0x000081C4
		protected unsafe override void RegisterEp(void* ifSpec, _RPC_BINDING_VECTOR* pBindingVector, _UUID_VECTOR* pUuidVector, ushort* wszAnnotation)
		{
			_RPC_IF_ID rpc_IF_ID;
			initblk(ref rpc_IF_ID, 0, 20L);
			int num = <Module>.RpcIfInqId(ifSpec, &rpc_IF_ID);
			if (num != null)
			{
				RpcServerBase.TraceError("RpcIfInqId failed with status {2} in file {0} line {1}", new object[]
				{
					"f:\\15.00.1497\\sources\\dev\\common\\src\\rpc\\dll\\adminrpcserver.h",
					1060,
					num
				});
				RpcServerBase.ThrowRpcException("RpcIfInqId", num);
			}
			if (this.m_mgrTypeGuid != null)
			{
				_GUID guid = <Module>.Microsoft.Exchange.Rpc.?A0x72697679.GUIDFromGuid(this.m_mgrTypeGuid.Value);
				_UUID_VECTOR uuid_VECTOR = 1;
				*(ref uuid_VECTOR + 8) = ref guid;
				RpcServerBase.CheckDuplicateEndpoint(ref rpc_IF_ID, ref guid, true);
				int num2 = <Module>.RpcEpRegisterW(ifSpec, pBindingVector, &uuid_VECTOR, wszAnnotation);
				if (num2 != null)
				{
					RpcServerBase.ThrowRpcException("RpcEpRegister", num2);
				}
			}
			else
			{
				_GUID guid2 = 0;
				initblk(ref guid2 + 4, 0, 12L);
				RpcServerBase.CheckDuplicateEndpoint(ref rpc_IF_ID, ref guid2, false);
				int num3 = <Module>.RpcEpRegisterW(ifSpec, pBindingVector, pUuidVector, wszAnnotation);
				if (num3 != null)
				{
					RpcServerBase.ThrowRpcException("RpcEpRegister", num3);
				}
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00008EAC File Offset: 0x000082AC
		protected unsafe override void UnregisterEp(void* ifSpec, _RPC_BINDING_VECTOR* pBindingVector, _UUID_VECTOR* pUuidVector)
		{
			if (this.m_mgrTypeGuid != null)
			{
				_GUID guid = <Module>.Microsoft.Exchange.Rpc.?A0x72697679.GUIDFromGuid(this.m_mgrTypeGuid.Value);
				_UUID_VECTOR uuid_VECTOR = 1;
				*(ref uuid_VECTOR + 8) = ref guid;
				base.UnregisterEp(ifSpec, pBindingVector, &uuid_VECTOR);
			}
			else
			{
				base.UnregisterEp(ifSpec, pBindingVector, pUuidVector);
			}
		}

		// Token: 0x060008DF RID: 2271
		public abstract int GetInterfaceInstance(Guid instanceGuid, out IAdminRpcServer instance);

		// Token: 0x060008E0 RID: 2272 RVA: 0x00008EFC File Offset: 0x000082FC
		public static RpcServerBase RegisterServerInstance(Type type, Guid? instanceGuid, [MarshalAs(UnmanagedType.U1)] bool isLocalOnly, string annotation)
		{
			if (instanceGuid != null)
			{
				Guid value = instanceGuid.Value;
				return RpcServerBase.RegisterServer(type, RpcServerBase.BuildDefaultSecurityDescriptor(), 1, value, null, annotation, isLocalOnly, false, 1234U);
			}
			return RpcServerBase.RegisterInterface(type, isLocalOnly, true, annotation);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00008F40 File Offset: 0x00008340
		public AdminRpcServerBase()
		{
		}

		// Token: 0x04000AD8 RID: 2776
		protected Guid? m_mgrTypeGuid;

		// Token: 0x04000AD9 RID: 2777
		public static IntPtr Admin20IntfHandle = (IntPtr)<Module>.mdbadmin20_v2_0_s_ifspec;

		// Token: 0x04000ADA RID: 2778
		public static IntPtr Admin40IntfHandle = (IntPtr)<Module>.mdbadmin40_v4_0_s_ifspec;

		// Token: 0x04000ADB RID: 2779
		public static IntPtr Admin50IntfHandle = (IntPtr)<Module>.mdbadmin50_v5_0_s_ifspec;
	}
}
