using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000023 RID: 35
	internal class AmClusterRawData : SafeDisposeTrackerHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000147 RID: 327 RVA: 0x0000663A File Offset: 0x0000483A
		internal AmClusterRawData(IntPtr data, uint dataSize, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(data);
			this.Size = dataSize;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006651 File Offset: 0x00004851
		private AmClusterRawData(IntPtr unused)
		{
			throw new NotImplementedException("Do not call this!");
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00006663 File Offset: 0x00004863
		internal IntPtr Buffer
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000666B File Offset: 0x0000486B
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00006673 File Offset: 0x00004873
		internal uint Size { get; private set; }

		// Token: 0x0600014C RID: 332 RVA: 0x0000667C File Offset: 0x0000487C
		internal static AmClusterRawData Allocate(uint dataSize)
		{
			return new AmClusterRawData(Marshal.AllocHGlobal((int)dataSize), dataSize, true);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006698 File Offset: 0x00004898
		internal int ReadInt32()
		{
			return Marshal.ReadInt32(this.handle);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000066A5 File Offset: 0x000048A5
		internal string ReadString()
		{
			return Marshal.PtrToStringUni(this.handle);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000066B2 File Offset: 0x000048B2
		protected override bool ReleaseHandle()
		{
			Marshal.FreeHGlobal(this.handle);
			return true;
		}
	}
}
