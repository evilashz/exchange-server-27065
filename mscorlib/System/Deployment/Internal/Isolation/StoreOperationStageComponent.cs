using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000673 RID: 1651
	internal struct StoreOperationStageComponent
	{
		// Token: 0x06004EA3 RID: 20131 RVA: 0x00117D60 File Offset: 0x00115F60
		public void Destroy()
		{
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x00117D62 File Offset: 0x00115F62
		public StoreOperationStageComponent(IDefinitionAppId app, string Manifest)
		{
			this = new StoreOperationStageComponent(app, null, Manifest);
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x00117D6D File Offset: 0x00115F6D
		public StoreOperationStageComponent(IDefinitionAppId app, IDefinitionIdentity comp, string Manifest)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationStageComponent));
			this.Flags = StoreOperationStageComponent.OpFlags.Nothing;
			this.Application = app;
			this.Component = comp;
			this.ManifestPath = Manifest;
		}

		// Token: 0x04002168 RID: 8552
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002169 RID: 8553
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationStageComponent.OpFlags Flags;

		// Token: 0x0400216A RID: 8554
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x0400216B RID: 8555
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionIdentity Component;

		// Token: 0x0400216C RID: 8556
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ManifestPath;

		// Token: 0x02000C0F RID: 3087
		[Flags]
		public enum OpFlags
		{
			// Token: 0x04003679 RID: 13945
			Nothing = 0
		}

		// Token: 0x02000C10 RID: 3088
		public enum Disposition
		{
			// Token: 0x0400367B RID: 13947
			Failed,
			// Token: 0x0400367C RID: 13948
			Installed,
			// Token: 0x0400367D RID: 13949
			Refreshed,
			// Token: 0x0400367E RID: 13950
			AlreadyInstalled
		}
	}
}
