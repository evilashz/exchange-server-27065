using System;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200002A RID: 42
	// (Invoke) Token: 0x060000F6 RID: 246
	internal delegate int ReadDatabaseCallback(byte[] buffer, ulong fileReadOffset, int bytesToRead);
}
