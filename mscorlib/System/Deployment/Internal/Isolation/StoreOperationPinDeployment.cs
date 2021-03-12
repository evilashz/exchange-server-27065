using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000676 RID: 1654
	internal struct StoreOperationPinDeployment
	{
		// Token: 0x06004EAC RID: 20140 RVA: 0x00117E6D File Offset: 0x0011606D
		[SecuritySafeCritical]
		public StoreOperationPinDeployment(IDefinitionAppId AppId, StoreApplicationReference Ref)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationPinDeployment));
			this.Flags = StoreOperationPinDeployment.OpFlags.NeverExpires;
			this.Application = AppId;
			this.Reference = Ref.ToIntPtr();
			this.ExpirationTime = 0L;
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x00117EA7 File Offset: 0x001160A7
		public StoreOperationPinDeployment(IDefinitionAppId AppId, DateTime Expiry, StoreApplicationReference Ref)
		{
			this = new StoreOperationPinDeployment(AppId, Ref);
			this.Flags |= StoreOperationPinDeployment.OpFlags.NeverExpires;
		}

		// Token: 0x06004EAE RID: 20142 RVA: 0x00117EBF File Offset: 0x001160BF
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x04002178 RID: 8568
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002179 RID: 8569
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationPinDeployment.OpFlags Flags;

		// Token: 0x0400217A RID: 8570
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x0400217B RID: 8571
		[MarshalAs(UnmanagedType.I8)]
		public long ExpirationTime;

		// Token: 0x0400217C RID: 8572
		public IntPtr Reference;

		// Token: 0x02000C14 RID: 3092
		[Flags]
		public enum OpFlags
		{
			// Token: 0x04003689 RID: 13961
			Nothing = 0,
			// Token: 0x0400368A RID: 13962
			NeverExpires = 1
		}

		// Token: 0x02000C15 RID: 3093
		public enum Disposition
		{
			// Token: 0x0400368C RID: 13964
			Failed,
			// Token: 0x0400368D RID: 13965
			Pinned
		}
	}
}
