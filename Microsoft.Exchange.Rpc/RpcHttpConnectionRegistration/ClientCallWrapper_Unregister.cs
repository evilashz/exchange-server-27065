using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.RpcHttpConnectionRegistration
{
	// Token: 0x020003E8 RID: 1000
	internal class ClientCallWrapper_Unregister : ClientCallWrapper, IDisposable
	{
		// Token: 0x06001117 RID: 4375 RVA: 0x00055708 File Offset: 0x00054B08
		protected override string Name()
		{
			return "RpcHttpConnectionRegistration::Unregister";
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0005571C File Offset: 0x00054B1C
		protected unsafe override int InternalExecute()
		{
			return <Module>.cli_RpcHttpConnectionRegistration_Unregister(base.HBinding, (_GUID*)this.m_pAssociationGroupId.ToPointer(), (_GUID*)this.m_pRequestId.ToPointer());
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0005574C File Offset: 0x00054B4C
		protected override void InternalCleanup()
		{
			if (this.m_pAssociationGroupId != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pAssociationGroupId);
				this.m_pAssociationGroupId = IntPtr.Zero;
			}
			if (this.m_pRequestId != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pRequestId);
				this.m_pRequestId = IntPtr.Zero;
			}
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000557C0 File Offset: 0x00054BC0
		public unsafe ClientCallWrapper_Unregister(void* hBinding, Guid associationGroupId, Guid requestId) : base(hBinding)
		{
			this.m_pAssociationGroupId = IntPtr.Zero;
			this.m_pRequestId = IntPtr.Zero;
			bool flag = false;
			try
			{
				IntPtr pAssociationGroupId = Marshal.AllocHGlobal(16);
				this.m_pAssociationGroupId = pAssociationGroupId;
				Marshal.Copy(associationGroupId.ToByteArray(), 0, this.m_pAssociationGroupId, 16);
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

		// Token: 0x0600111B RID: 4379 RVA: 0x0005586C File Offset: 0x00054C6C
		private void ~ClientCallWrapper_Unregister()
		{
			this.InternalCleanup();
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00055880 File Offset: 0x00054C80
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

		// Token: 0x0600111D RID: 4381 RVA: 0x00055984 File Offset: 0x00054D84
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400100F RID: 4111
		private IntPtr m_pAssociationGroupId;

		// Token: 0x04001010 RID: 4112
		private IntPtr m_pRequestId;
	}
}
