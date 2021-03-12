using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200067A RID: 1658
	internal struct StoreOperationMetadataProperty
	{
		// Token: 0x06004EB6 RID: 20150 RVA: 0x00117FB2 File Offset: 0x001161B2
		public StoreOperationMetadataProperty(Guid PropertySet, string Name)
		{
			this = new StoreOperationMetadataProperty(PropertySet, Name, null);
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x00117FBD File Offset: 0x001161BD
		public StoreOperationMetadataProperty(Guid PropertySet, string Name, string Value)
		{
			this.GuidPropertySet = PropertySet;
			this.Name = Name;
			this.Value = Value;
			this.ValueSize = ((Value != null) ? new IntPtr((Value.Length + 1) * 2) : IntPtr.Zero);
		}

		// Token: 0x04002189 RID: 8585
		public Guid GuidPropertySet;

		// Token: 0x0400218A RID: 8586
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x0400218B RID: 8587
		[MarshalAs(UnmanagedType.SysUInt)]
		public IntPtr ValueSize;

		// Token: 0x0400218C RID: 8588
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Value;
	}
}
