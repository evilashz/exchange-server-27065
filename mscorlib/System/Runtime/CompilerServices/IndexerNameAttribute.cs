using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200088B RID: 2187
	[AttributeUsage(AttributeTargets.Property, Inherited = true)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class IndexerNameAttribute : Attribute
	{
		// Token: 0x06005C90 RID: 23696 RVA: 0x00144CB0 File Offset: 0x00142EB0
		[__DynamicallyInvokable]
		public IndexerNameAttribute(string indexerName)
		{
		}
	}
}
