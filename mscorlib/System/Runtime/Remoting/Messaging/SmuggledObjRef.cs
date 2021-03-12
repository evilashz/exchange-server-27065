using System;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000849 RID: 2121
	internal class SmuggledObjRef
	{
		// Token: 0x06005B12 RID: 23314 RVA: 0x0013E754 File Offset: 0x0013C954
		[SecurityCritical]
		public SmuggledObjRef(ObjRef objRef)
		{
			this._objRef = objRef;
		}

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06005B13 RID: 23315 RVA: 0x0013E763 File Offset: 0x0013C963
		public ObjRef ObjRef
		{
			[SecurityCritical]
			get
			{
				return this._objRef;
			}
		}

		// Token: 0x040028E3 RID: 10467
		[SecurityCritical]
		private ObjRef _objRef;
	}
}
