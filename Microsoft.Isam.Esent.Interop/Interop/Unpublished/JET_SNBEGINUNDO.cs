using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200004B RID: 75
	public class JET_SNBEGINUNDO : JET_RECOVERYCONTROL
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000B1B5 File Offset: 0x000093B5
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x0000B1BD File Offset: 0x000093BD
		public int cdbinfomisc { get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000B1C6 File Offset: 0x000093C6
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0000B1CE File Offset: 0x000093CE
		public JET_DBINFOMISC[] rgdbinfomisc { get; private set; }

		// Token: 0x06000469 RID: 1129 RVA: 0x0000B1D8 File Offset: 0x000093D8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_SNBEGINUNDO({0})", new object[]
			{
				this.cdbinfomisc
			});
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000B20C File Offset: 0x0000940C
		internal void SetFromNativeSnbeginundo(ref NATIVE_SNBEGINUNDO native)
		{
			base.SetFromNativeSnrecoverycontrol(ref native.recoveryControl);
			this.cdbinfomisc = checked((int)native.cdbinfomisc);
			if (this.cdbinfomisc > 0)
			{
				NATIVE_DBINFOMISC7[] array = new NATIVE_DBINFOMISC7[this.cdbinfomisc];
				array[0] = (NATIVE_DBINFOMISC7)Marshal.PtrToStructure(native.rgdbinfomisc, typeof(NATIVE_DBINFOMISC7));
			}
		}
	}
}
