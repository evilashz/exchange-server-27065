using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009C4 RID: 2500
	[Guid("60D27C8D-5F61-4CCE-B751-690FAE66AA53")]
	[ComImport]
	internal interface IManagedActivationFactory
	{
		// Token: 0x060063A2 RID: 25506
		void RunClassConstructor();
	}
}
