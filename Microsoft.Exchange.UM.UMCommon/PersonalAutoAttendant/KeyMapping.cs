using System;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x02000103 RID: 259
	internal abstract class KeyMapping<T> : KeyMappingBase
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x0001FF4A File Offset: 0x0001E14A
		internal KeyMapping(KeyMappingTypeEnum type, int key, string context, T data) : base(type, key, context)
		{
			this.data = data;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x0001FF5D File Offset: 0x0001E15D
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x0001FF65 File Offset: 0x0001E165
		internal T Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x040004CF RID: 1231
		private T data;
	}
}
