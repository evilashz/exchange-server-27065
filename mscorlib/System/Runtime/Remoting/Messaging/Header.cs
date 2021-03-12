using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200085C RID: 2140
	[ComVisible(true)]
	[Serializable]
	public class Header
	{
		// Token: 0x06005B87 RID: 23431 RVA: 0x00140477 File Offset: 0x0013E677
		public Header(string _Name, object _Value) : this(_Name, _Value, true)
		{
		}

		// Token: 0x06005B88 RID: 23432 RVA: 0x00140482 File Offset: 0x0013E682
		public Header(string _Name, object _Value, bool _MustUnderstand)
		{
			this.Name = _Name;
			this.Value = _Value;
			this.MustUnderstand = _MustUnderstand;
		}

		// Token: 0x06005B89 RID: 23433 RVA: 0x0014049F File Offset: 0x0013E69F
		public Header(string _Name, object _Value, bool _MustUnderstand, string _HeaderNamespace)
		{
			this.Name = _Name;
			this.Value = _Value;
			this.MustUnderstand = _MustUnderstand;
			this.HeaderNamespace = _HeaderNamespace;
		}

		// Token: 0x04002918 RID: 10520
		public string Name;

		// Token: 0x04002919 RID: 10521
		public object Value;

		// Token: 0x0400291A RID: 10522
		public bool MustUnderstand;

		// Token: 0x0400291B RID: 10523
		public string HeaderNamespace;
	}
}
