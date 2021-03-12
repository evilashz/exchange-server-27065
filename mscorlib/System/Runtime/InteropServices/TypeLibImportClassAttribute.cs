using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008EC RID: 2284
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibImportClassAttribute : Attribute
	{
		// Token: 0x06005EE4 RID: 24292 RVA: 0x00146BB4 File Offset: 0x00144DB4
		public TypeLibImportClassAttribute(Type importClass)
		{
			this._importClassName = importClass.ToString();
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x06005EE5 RID: 24293 RVA: 0x00146BC8 File Offset: 0x00144DC8
		public string Value
		{
			get
			{
				return this._importClassName;
			}
		}

		// Token: 0x040029B7 RID: 10679
		internal string _importClassName;
	}
}
