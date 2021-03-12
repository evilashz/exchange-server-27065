using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200067B RID: 1659
	internal struct StoreOperationSetDeploymentMetadata
	{
		// Token: 0x06004EB8 RID: 20152 RVA: 0x00117FF3 File Offset: 0x001161F3
		public StoreOperationSetDeploymentMetadata(IDefinitionAppId Deployment, StoreApplicationReference Reference, StoreOperationMetadataProperty[] SetProperties)
		{
			this = new StoreOperationSetDeploymentMetadata(Deployment, Reference, SetProperties, null);
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x00118000 File Offset: 0x00116200
		[SecuritySafeCritical]
		public StoreOperationSetDeploymentMetadata(IDefinitionAppId Deployment, StoreApplicationReference Reference, StoreOperationMetadataProperty[] SetProperties, StoreOperationMetadataProperty[] TestProperties)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationSetDeploymentMetadata));
			this.Flags = StoreOperationSetDeploymentMetadata.OpFlags.Nothing;
			this.Deployment = Deployment;
			if (SetProperties != null)
			{
				this.PropertiesToSet = StoreOperationSetDeploymentMetadata.MarshalProperties(SetProperties);
				this.cPropertiesToSet = new IntPtr(SetProperties.Length);
			}
			else
			{
				this.PropertiesToSet = IntPtr.Zero;
				this.cPropertiesToSet = IntPtr.Zero;
			}
			if (TestProperties != null)
			{
				this.PropertiesToTest = StoreOperationSetDeploymentMetadata.MarshalProperties(TestProperties);
				this.cPropertiesToTest = new IntPtr(TestProperties.Length);
			}
			else
			{
				this.PropertiesToTest = IntPtr.Zero;
				this.cPropertiesToTest = IntPtr.Zero;
			}
			this.InstallerReference = Reference.ToIntPtr();
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x001180AC File Offset: 0x001162AC
		[SecurityCritical]
		public void Destroy()
		{
			if (this.PropertiesToSet != IntPtr.Zero)
			{
				StoreOperationSetDeploymentMetadata.DestroyProperties(this.PropertiesToSet, (ulong)this.cPropertiesToSet.ToInt64());
				this.PropertiesToSet = IntPtr.Zero;
				this.cPropertiesToSet = IntPtr.Zero;
			}
			if (this.PropertiesToTest != IntPtr.Zero)
			{
				StoreOperationSetDeploymentMetadata.DestroyProperties(this.PropertiesToTest, (ulong)this.cPropertiesToTest.ToInt64());
				this.PropertiesToTest = IntPtr.Zero;
				this.cPropertiesToTest = IntPtr.Zero;
			}
			if (this.InstallerReference != IntPtr.Zero)
			{
				StoreApplicationReference.Destroy(this.InstallerReference);
				this.InstallerReference = IntPtr.Zero;
			}
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x00118160 File Offset: 0x00116360
		[SecurityCritical]
		private static void DestroyProperties(IntPtr rgItems, ulong iItems)
		{
			if (rgItems != IntPtr.Zero)
			{
				ulong num = (ulong)((long)Marshal.SizeOf(typeof(StoreOperationMetadataProperty)));
				for (ulong num2 = 0UL; num2 < iItems; num2 += 1UL)
				{
					Marshal.DestroyStructure(new IntPtr((long)(num2 * num + (ulong)rgItems.ToInt64())), typeof(StoreOperationMetadataProperty));
				}
				Marshal.FreeCoTaskMem(rgItems);
			}
		}

		// Token: 0x06004EBC RID: 20156 RVA: 0x001181C0 File Offset: 0x001163C0
		[SecurityCritical]
		private static IntPtr MarshalProperties(StoreOperationMetadataProperty[] Props)
		{
			if (Props == null || Props.Length == 0)
			{
				return IntPtr.Zero;
			}
			int num = Marshal.SizeOf(typeof(StoreOperationMetadataProperty));
			IntPtr result = Marshal.AllocCoTaskMem(num * Props.Length);
			for (int num2 = 0; num2 != Props.Length; num2++)
			{
				Marshal.StructureToPtr<StoreOperationMetadataProperty>(Props[num2], new IntPtr((long)(num2 * num) + result.ToInt64()), false);
			}
			return result;
		}

		// Token: 0x0400218D RID: 8589
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400218E RID: 8590
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationSetDeploymentMetadata.OpFlags Flags;

		// Token: 0x0400218F RID: 8591
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Deployment;

		// Token: 0x04002190 RID: 8592
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr InstallerReference;

		// Token: 0x04002191 RID: 8593
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr cPropertiesToTest;

		// Token: 0x04002192 RID: 8594
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr PropertiesToTest;

		// Token: 0x04002193 RID: 8595
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr cPropertiesToSet;

		// Token: 0x04002194 RID: 8596
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr PropertiesToSet;

		// Token: 0x02000C1C RID: 3100
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040036A1 RID: 13985
			Nothing = 0
		}

		// Token: 0x02000C1D RID: 3101
		public enum Disposition
		{
			// Token: 0x040036A3 RID: 13987
			Failed,
			// Token: 0x040036A4 RID: 13988
			Set = 2
		}
	}
}
