using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000674 RID: 1652
	internal struct StoreOperationStageComponentFile
	{
		// Token: 0x06004EA6 RID: 20134 RVA: 0x00117DA0 File Offset: 0x00115FA0
		public StoreOperationStageComponentFile(IDefinitionAppId App, string CompRelPath, string SrcFile)
		{
			this = new StoreOperationStageComponentFile(App, null, CompRelPath, SrcFile);
		}

		// Token: 0x06004EA7 RID: 20135 RVA: 0x00117DAC File Offset: 0x00115FAC
		public StoreOperationStageComponentFile(IDefinitionAppId App, IDefinitionIdentity Component, string CompRelPath, string SrcFile)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationStageComponentFile));
			this.Flags = StoreOperationStageComponentFile.OpFlags.Nothing;
			this.Application = App;
			this.Component = Component;
			this.ComponentRelativePath = CompRelPath;
			this.SourceFilePath = SrcFile;
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x00117DE7 File Offset: 0x00115FE7
		public void Destroy()
		{
		}

		// Token: 0x0400216D RID: 8557
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400216E RID: 8558
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationStageComponentFile.OpFlags Flags;

		// Token: 0x0400216F RID: 8559
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x04002170 RID: 8560
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionIdentity Component;

		// Token: 0x04002171 RID: 8561
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ComponentRelativePath;

		// Token: 0x04002172 RID: 8562
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SourceFilePath;

		// Token: 0x02000C11 RID: 3089
		[Flags]
		public enum OpFlags
		{
			// Token: 0x04003680 RID: 13952
			Nothing = 0
		}

		// Token: 0x02000C12 RID: 3090
		public enum Disposition
		{
			// Token: 0x04003682 RID: 13954
			Failed,
			// Token: 0x04003683 RID: 13955
			Installed,
			// Token: 0x04003684 RID: 13956
			Refreshed,
			// Token: 0x04003685 RID: 13957
			AlreadyInstalled
		}
	}
}
