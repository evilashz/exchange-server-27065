using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200081C RID: 2076
	public sealed class ActivationContextActivator : IDisposable
	{
		// Token: 0x060047F2 RID: 18418
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern ActivationContextActivator.ActivationContextHandle CreateActCtx(ref ActivationContextActivator.ACTCTX ActCtx);

		// Token: 0x060047F3 RID: 18419
		[DllImport("Kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool ActivateActCtx(ActivationContextActivator.ActivationContextHandle hActCtx, out ActivationContextActivator.ActivationContextCookie lpCookie);

		// Token: 0x060047F4 RID: 18420
		[DllImport("Kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool DeactivateActCtx(int dwFlags, IntPtr lpCookie);

		// Token: 0x060047F5 RID: 18421
		[DllImport("Kernel32.dll", SetLastError = true)]
		private static extern void ReleaseActCtx(IntPtr hActCtx);

		// Token: 0x060047F6 RID: 18422 RVA: 0x001279C1 File Offset: 0x00125BC1
		private ActivationContextActivator(ActivationContextActivator.ACTCTX actctx)
		{
			this.m_hActivationContext = ActivationContextActivator.CreateActCtx(ref actctx);
			if (this.m_hActivationContext.IsInvalid || !ActivationContextActivator.ActivateActCtx(this.m_hActivationContext, out this.m_cookie))
			{
				throw new ActivationContextActivatorException();
			}
		}

		// Token: 0x060047F7 RID: 18423 RVA: 0x001279FC File Offset: 0x00125BFC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060047F8 RID: 18424 RVA: 0x00127A0B File Offset: 0x00125C0B
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_cookie != null)
				{
					this.m_cookie.Dispose();
				}
				if (this.m_hActivationContext != null)
				{
					this.m_hActivationContext.Dispose();
				}
			}
		}

		// Token: 0x060047F9 RID: 18425 RVA: 0x00127A38 File Offset: 0x00125C38
		public static ActivationContextActivator FromExternalManifest(string source, string assemblyDirectory)
		{
			ActivationContextActivator.ACTCTX actctx = default(ActivationContextActivator.ACTCTX);
			actctx.cbSize = Marshal.SizeOf(actctx);
			actctx.lpSource = source;
			actctx.lpAssemblyDirectory = assemblyDirectory;
			actctx.dwFlags = 36U;
			return ActivationContextActivator.FromActivationContext(actctx);
		}

		// Token: 0x060047FA RID: 18426 RVA: 0x00127A80 File Offset: 0x00125C80
		public static ActivationContextActivator FromInternalManifest(string source, string assemblyDirectory)
		{
			ActivationContextActivator.ACTCTX actctx = default(ActivationContextActivator.ACTCTX);
			actctx.cbSize = Marshal.SizeOf(actctx);
			actctx.lpSource = source;
			actctx.lpAssemblyDirectory = assemblyDirectory;
			actctx.lpResourceName = 2;
			actctx.dwFlags = 44U;
			return ActivationContextActivator.FromActivationContext(actctx);
		}

		// Token: 0x060047FB RID: 18427 RVA: 0x00127ACE File Offset: 0x00125CCE
		internal static ActivationContextActivator FromActivationContext(ActivationContextActivator.ACTCTX actctx)
		{
			return new ActivationContextActivator(actctx);
		}

		// Token: 0x04002B8F RID: 11151
		private const uint ACTCTX_FLAG_PROCESSOR_ARCHITECTURE_VALID = 1U;

		// Token: 0x04002B90 RID: 11152
		private const uint ACTCTX_FLAG_LANGID_VALID = 2U;

		// Token: 0x04002B91 RID: 11153
		private const uint ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID = 4U;

		// Token: 0x04002B92 RID: 11154
		private const uint ACTCTX_FLAG_RESOURCE_NAME_VALID = 8U;

		// Token: 0x04002B93 RID: 11155
		private const uint ACTCTX_FLAG_SET_PROCESS_DEFAULT = 16U;

		// Token: 0x04002B94 RID: 11156
		private const uint ACTCTX_FLAG_APPLICATION_NAME_VALID = 32U;

		// Token: 0x04002B95 RID: 11157
		private const uint ACTCTX_FLAG_HMODULE_VALID = 128U;

		// Token: 0x04002B96 RID: 11158
		private const ushort ISOLATIONAWARE_MANIFEST_RESOURCE_ID = 2;

		// Token: 0x04002B97 RID: 11159
		private ActivationContextActivator.ActivationContextHandle m_hActivationContext;

		// Token: 0x04002B98 RID: 11160
		private ActivationContextActivator.ActivationContextCookie m_cookie;

		// Token: 0x0200081D RID: 2077
		internal struct ACTCTX
		{
			// Token: 0x04002B99 RID: 11161
			public int cbSize;

			// Token: 0x04002B9A RID: 11162
			public uint dwFlags;

			// Token: 0x04002B9B RID: 11163
			public string lpSource;

			// Token: 0x04002B9C RID: 11164
			public ushort wProcessorArchitecture;

			// Token: 0x04002B9D RID: 11165
			public ushort wLangId;

			// Token: 0x04002B9E RID: 11166
			public string lpAssemblyDirectory;

			// Token: 0x04002B9F RID: 11167
			public ushort lpResourceName;

			// Token: 0x04002BA0 RID: 11168
			public string lpApplicationName;
		}

		// Token: 0x0200081E RID: 2078
		private sealed class ActivationContextHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x060047FC RID: 18428 RVA: 0x00127AD6 File Offset: 0x00125CD6
			private ActivationContextHandle() : base(true)
			{
			}

			// Token: 0x060047FD RID: 18429 RVA: 0x00127ADF File Offset: 0x00125CDF
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			protected override bool ReleaseHandle()
			{
				ActivationContextActivator.ReleaseActCtx(this.handle);
				return true;
			}
		}

		// Token: 0x0200081F RID: 2079
		private sealed class ActivationContextCookie : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x060047FE RID: 18430 RVA: 0x00127AED File Offset: 0x00125CED
			private ActivationContextCookie() : base(true)
			{
			}

			// Token: 0x060047FF RID: 18431 RVA: 0x00127AF6 File Offset: 0x00125CF6
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			protected override bool ReleaseHandle()
			{
				return ActivationContextActivator.DeactivateActCtx(0, this.handle);
			}
		}
	}
}
