using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.RpcHttpConnectionRegistration
{
	// Token: 0x020003E7 RID: 999
	internal class ClientCallWrapper_Register : ClientCallWrapper, IDisposable
	{
		// Token: 0x0600110E RID: 4366 RVA: 0x00055374 File Offset: 0x00054774
		protected override string Name()
		{
			return "RpcHttpConnectionRegistration::Register";
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00055388 File Offset: 0x00054788
		protected unsafe override int InternalExecute()
		{
			byte* ptr = null;
			byte* ptr2 = null;
			int result;
			try
			{
				int num = <Module>.cli_RpcHttpConnectionRegistration_Register(base.HBinding, (_GUID*)this.m_pAssociationGroupId.ToPointer(), (byte*)this.m_pToken.ToPointer(), (byte*)this.m_pServerTarget.ToPointer(), (byte*)this.m_pSessionCookie.ToPointer(), (byte*)this.m_pClientIp.ToPointer(), (_GUID*)this.m_pRequestId.ToPointer(), &ptr, &ptr2);
				if (ptr != null)
				{
					IntPtr ptr3 = new IntPtr((void*)ptr);
					this.m_failureMessage = Marshal.PtrToStringAnsi(ptr3);
				}
				if (ptr2 != null)
				{
					IntPtr ptr4 = new IntPtr((void*)ptr2);
					this.m_failureDetails = Marshal.PtrToStringAnsi(ptr4);
				}
				result = num;
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
			return result;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00055458 File Offset: 0x00054858
		protected override void InternalCleanup()
		{
			if (this.m_pAssociationGroupId != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pAssociationGroupId);
				this.m_pAssociationGroupId = IntPtr.Zero;
			}
			if (this.m_pToken != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pToken);
				this.m_pToken = IntPtr.Zero;
			}
			if (this.m_pServerTarget != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pServerTarget);
				this.m_pServerTarget = IntPtr.Zero;
			}
			if (this.m_pSessionCookie != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pSessionCookie);
				this.m_pSessionCookie = IntPtr.Zero;
			}
			if (this.m_pClientIp != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pClientIp);
				this.m_pClientIp = IntPtr.Zero;
			}
			if (this.m_pRequestId != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pRequestId);
				this.m_pRequestId = IntPtr.Zero;
			}
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00055594 File Offset: 0x00054994
		public unsafe ClientCallWrapper_Register(void* hBinding, Guid associationGroupId, string token, string serverTarget, string sessionCookie, string clientIp, Guid requestId) : base(hBinding)
		{
			this.m_pAssociationGroupId = IntPtr.Zero;
			this.m_pToken = IntPtr.Zero;
			this.m_pServerTarget = IntPtr.Zero;
			this.m_pSessionCookie = IntPtr.Zero;
			this.m_pClientIp = IntPtr.Zero;
			this.m_pRequestId = IntPtr.Zero;
			bool flag = false;
			try
			{
				IntPtr pAssociationGroupId = Marshal.AllocHGlobal(16);
				this.m_pAssociationGroupId = pAssociationGroupId;
				Marshal.Copy(associationGroupId.ToByteArray(), 0, this.m_pAssociationGroupId, 16);
				IntPtr pToken = Marshal.StringToHGlobalAnsi(token);
				this.m_pToken = pToken;
				IntPtr pServerTarget = Marshal.StringToHGlobalAnsi(serverTarget);
				this.m_pServerTarget = pServerTarget;
				IntPtr pSessionCookie = Marshal.StringToHGlobalAnsi(sessionCookie);
				this.m_pSessionCookie = pSessionCookie;
				IntPtr pClientIp = Marshal.StringToHGlobalAnsi(clientIp);
				this.m_pClientIp = pClientIp;
				IntPtr pRequestId = Marshal.AllocHGlobal(16);
				this.m_pRequestId = pRequestId;
				Marshal.Copy(requestId.ToByteArray(), 0, this.m_pRequestId, 16);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.InternalCleanup();
				}
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x000556AC File Offset: 0x00054AAC
		public string FailureMessage
		{
			get
			{
				return this.m_failureMessage;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x000556C0 File Offset: 0x00054AC0
		public string FailureDetails
		{
			get
			{
				return this.m_failureDetails;
			}
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000556D4 File Offset: 0x00054AD4
		private void ~ClientCallWrapper_Register()
		{
			this.InternalCleanup();
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x000556E8 File Offset: 0x00054AE8
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.InternalCleanup();
			}
			else
			{
				base.Finalize();
			}
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00055968 File Offset: 0x00054D68
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04001007 RID: 4103
		private IntPtr m_pAssociationGroupId;

		// Token: 0x04001008 RID: 4104
		private IntPtr m_pToken;

		// Token: 0x04001009 RID: 4105
		private IntPtr m_pServerTarget;

		// Token: 0x0400100A RID: 4106
		private IntPtr m_pSessionCookie;

		// Token: 0x0400100B RID: 4107
		private IntPtr m_pClientIp;

		// Token: 0x0400100C RID: 4108
		private IntPtr m_pRequestId;

		// Token: 0x0400100D RID: 4109
		private string m_failureMessage;

		// Token: 0x0400100E RID: 4110
		private string m_failureDetails;
	}
}
