using System;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000753 RID: 1875
	internal interface IStreamable
	{
		// Token: 0x060052A1 RID: 21153
		[SecurityCritical]
		void Read(__BinaryParser input);

		// Token: 0x060052A2 RID: 21154
		void Write(__BinaryWriter sout);
	}
}
