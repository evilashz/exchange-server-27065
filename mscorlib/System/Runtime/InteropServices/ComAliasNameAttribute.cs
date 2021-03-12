using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200090A RID: 2314
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComAliasNameAttribute : Attribute
	{
		// Token: 0x06005F2C RID: 24364 RVA: 0x0014736D File Offset: 0x0014556D
		public ComAliasNameAttribute(string alias)
		{
			this._val = alias;
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x06005F2D RID: 24365 RVA: 0x0014737C File Offset: 0x0014557C
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A66 RID: 10854
		internal string _val;
	}
}
