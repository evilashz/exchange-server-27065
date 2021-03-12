using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000210 RID: 528
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct ReadState
	{
		// Token: 0x060008C2 RID: 2242 RVA: 0x0002E6F6 File Offset: 0x0002C8F6
		public ReadState(PropValue sourceKey, bool read)
		{
			this.sourceKey = sourceKey;
			this.read = read;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0002E706 File Offset: 0x0002C906
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x0002E70E File Offset: 0x0002C90E
		public PropValue SourceKey
		{
			get
			{
				return this.sourceKey;
			}
			set
			{
				this.sourceKey = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0002E717 File Offset: 0x0002C917
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x0002E71F File Offset: 0x0002C91F
		public bool Read
		{
			get
			{
				return this.read;
			}
			set
			{
				this.read = value;
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0002E728 File Offset: 0x0002C928
		internal int GetBytesToMarshal()
		{
			int num = _ReadState.SizeOf + 7 & -8;
			return num + (this.sourceKey.GetBytes().Length + 7 & -8);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0002E758 File Offset: 0x0002C958
		internal unsafe void MarshalToNative(_ReadState* pread, ref byte* pbExtra)
		{
			pread->cbSourceKey = this.sourceKey.GetBytes().Length;
			pread->pbSourceKey = pbExtra;
			pread->ulFlags = (this.read ? 1 : 0);
			byte* ptr = pbExtra;
			pbExtra += (IntPtr)(this.sourceKey.GetBytes().Length + 7 & -8);
			for (int i = 0; i < this.sourceKey.GetBytes().Length; i++)
			{
				*ptr = this.sourceKey.GetBytes()[i];
				ptr++;
			}
		}

		// Token: 0x04000F73 RID: 3955
		private PropValue sourceKey;

		// Token: 0x04000F74 RID: 3956
		private bool read;
	}
}
