using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004D4 RID: 1236
	[ComVisible(false)]
	internal sealed class SafeMetadataHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002ADB RID: 10971 RVA: 0x000ABBF7 File Offset: 0x000A9DF7
		private SafeMetadataHandle() : base(true)
		{
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x000ABC00 File Offset: 0x000A9E00
		internal SafeMetadataHandle(IntPtr handle, IMSAdminBase adminBase) : base(true)
		{
			base.SetHandle(handle);
			this.adminBase = adminBase;
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06002ADD RID: 10973 RVA: 0x000ABC17 File Offset: 0x000A9E17
		public static SafeMetadataHandle MetadataMasterRootHandle
		{
			get
			{
				return new SafeMetadataHandle(IntPtr.Zero, null);
			}
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x000ABC24 File Offset: 0x000A9E24
		protected override bool ReleaseHandle()
		{
			if (this.adminBase != null)
			{
				bool result = this.adminBase.CloseKey(this.handle) == 0;
				this.adminBase = null;
				return result;
			}
			return false;
		}

		// Token: 0x04001FF8 RID: 8184
		private IMSAdminBase adminBase;
	}
}
