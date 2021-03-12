using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200052F RID: 1327
	internal class Shared<T>
	{
		// Token: 0x06003F51 RID: 16209 RVA: 0x000EBBED File Offset: 0x000E9DED
		internal Shared(T value)
		{
			this.Value = value;
		}

		// Token: 0x04001A32 RID: 6706
		internal T Value;
	}
}
