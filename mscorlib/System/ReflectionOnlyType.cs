using System;

namespace System
{
	// Token: 0x0200012D RID: 301
	[Serializable]
	internal class ReflectionOnlyType : RuntimeType
	{
		// Token: 0x060011AE RID: 4526 RVA: 0x0003697D File Offset: 0x00034B7D
		private ReflectionOnlyType()
		{
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x00036985 File Offset: 0x00034B85
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAllowedInReflectionOnly"));
			}
		}
	}
}
