using System;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000E5 RID: 229
	internal abstract class CallerId<T> : CallerIdBase
	{
		// Token: 0x0600079E RID: 1950 RVA: 0x0001DEA6 File Offset: 0x0001C0A6
		internal CallerId(CallerIdTypeEnum type, T data) : base(type)
		{
			this.data = data;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001DEB6 File Offset: 0x0001C0B6
		protected T GetData()
		{
			return this.data;
		}

		// Token: 0x0400045B RID: 1115
		private T data;
	}
}
