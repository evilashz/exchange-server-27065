using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000725 RID: 1829
	internal class TypeLoadExceptionHolder
	{
		// Token: 0x060051AB RID: 20907 RVA: 0x0011E51B File Offset: 0x0011C71B
		internal TypeLoadExceptionHolder(string typeName)
		{
			this.m_typeName = typeName;
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x060051AC RID: 20908 RVA: 0x0011E52A File Offset: 0x0011C72A
		internal string TypeName
		{
			get
			{
				return this.m_typeName;
			}
		}

		// Token: 0x040023F9 RID: 9209
		private string m_typeName;
	}
}
