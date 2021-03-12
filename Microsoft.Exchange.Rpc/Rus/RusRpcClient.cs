using System;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.Rus
{
	// Token: 0x020003EA RID: 1002
	internal class RusRpcClient : RpcClientBase
	{
		// Token: 0x06001125 RID: 4389 RVA: 0x00055ADC File Offset: 0x00054EDC
		public RusRpcClient(string machineName, NetworkCredential nc) : base(machineName, nc, AuthenticationService.Kerberos)
		{
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00055AC4 File Offset: 0x00054EC4
		public RusRpcClient(string machineName) : base(machineName, null, AuthenticationService.Kerberos)
		{
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00055AF4 File Offset: 0x00054EF4
		private RusRpcClient(string machineName, string protocolSequence, NetworkCredential nc) : base(machineName, protocolSequence, nc, AuthenticationService.Kerberos)
		{
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00055B0C File Offset: 0x00054F0C
		[HandleProcessCorruptedStateExceptions]
		public unsafe int ResetConfiguration(string DC)
		{
			IntPtr hglobal = 0;
			int result;
			try
			{
				hglobal = Marshal.StringToHGlobalUni(DC);
				ushort* ptr = (ushort*)hglobal.ToPointer();
				try
				{
					result = <Module>.cli_ResetConfiguration(base.BindingHandle, ptr);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "ResetConfiguration");
				}
			}
			finally
			{
				Marshal.FreeHGlobal(hglobal);
			}
			return result;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00055B94 File Offset: 0x00054F94
		[HandleProcessCorruptedStateExceptions]
		public unsafe int ProcessObject(string DN, object Policies, object DirObject, out object Results)
		{
			IntPtr hglobal = 0;
			IntPtr hglobal2 = 0;
			IntPtr hglobal3 = 0;
			IntPtr intPtr = 0;
			tagVARIANT* ptr = null;
			tagVARIANT* ptr2 = null;
			tagVARIANT* ptr3 = null;
			int num;
			try
			{
				hglobal = Marshal.StringToHGlobalUni(DN);
				ushort* ptr4 = (ushort*)hglobal.ToPointer();
				IntPtr intPtr2 = Marshal.AllocHGlobal(24);
				hglobal2 = intPtr2;
				Marshal.GetNativeVariantForObject(Policies, intPtr2);
				ptr2 = (tagVARIANT*)hglobal2.ToPointer();
				IntPtr intPtr3 = Marshal.AllocHGlobal(24);
				hglobal3 = intPtr3;
				Marshal.GetNativeVariantForObject(DirObject, intPtr3);
				ptr3 = (tagVARIANT*)hglobal3.ToPointer();
				try
				{
					num = <Module>.cli_ProcessObject(base.BindingHandle, ptr4, ptr2, ptr3, &ptr);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "ProcessObject");
				}
				if (num >= 0)
				{
					IntPtr pSrcNativeVariant = new IntPtr((void*)ptr);
					Results = Marshal.GetObjectForNativeVariant(pSrcNativeVariant);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(hglobal);
				if (ptr != null)
				{
					<Module>.VariantClear(ptr);
					<Module>.MIDL_user_free((void*)ptr);
				}
				IntPtr intPtr4 = new IntPtr(0);
				if (ptr2 != null)
				{
					<Module>.VariantClear(ptr2);
				}
				Marshal.FreeHGlobal(hglobal2);
				if (ptr3 != null)
				{
					<Module>.VariantClear(ptr3);
				}
				Marshal.FreeHGlobal(hglobal3);
			}
			return num;
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00055CD4 File Offset: 0x000550D4
		[HandleProcessCorruptedStateExceptions]
		public unsafe int FilterAttributes(object Policies, out object Attributes)
		{
			IntPtr hglobal = 0;
			IntPtr intPtr = 0;
			tagVARIANT* ptr = null;
			tagVARIANT* ptr2 = null;
			int num;
			try
			{
				IntPtr intPtr2 = Marshal.AllocHGlobal(24);
				hglobal = intPtr2;
				Marshal.GetNativeVariantForObject(Policies, intPtr2);
				ptr2 = (tagVARIANT*)hglobal.ToPointer();
				try
				{
					num = <Module>.cli_FilterAttributes(base.BindingHandle, ptr2, &ptr);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "FilterAttributes");
				}
				if (num >= 0)
				{
					IntPtr pSrcNativeVariant = new IntPtr((void*)ptr);
					Attributes = Marshal.GetObjectForNativeVariant(pSrcNativeVariant);
				}
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.VariantClear(ptr);
					<Module>.MIDL_user_free((void*)ptr);
				}
				IntPtr intPtr3 = new IntPtr(0);
				if (ptr2 != null)
				{
					<Module>.VariantClear(ptr2);
				}
				Marshal.FreeHGlobal(hglobal);
			}
			return num;
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00055DB4 File Offset: 0x000551B4
		[HandleProcessCorruptedStateExceptions]
		public unsafe int ValidateLdapFilter(string filter)
		{
			IntPtr hglobal = 0;
			int result;
			try
			{
				hglobal = Marshal.StringToHGlobalUni(filter);
				ushort* ptr = (ushort*)hglobal.ToPointer();
				try
				{
					result = <Module>.cli_ValidateLdapFilter(base.BindingHandle, ptr);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "ValidateLdapFilter");
				}
			}
			finally
			{
				Marshal.FreeHGlobal(hglobal);
			}
			return result;
		}
	}
}
