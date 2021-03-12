using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000623 RID: 1571
	internal sealed class InternalModuleBuilder : RuntimeModule
	{
		// Token: 0x06004AE9 RID: 19177 RVA: 0x0010E799 File Offset: 0x0010C999
		private InternalModuleBuilder()
		{
		}

		// Token: 0x06004AEA RID: 19178 RVA: 0x0010E7A1 File Offset: 0x0010C9A1
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InternalModuleBuilder)
			{
				return this == obj;
			}
			return obj.Equals(this);
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x0010E7BC File Offset: 0x0010C9BC
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
