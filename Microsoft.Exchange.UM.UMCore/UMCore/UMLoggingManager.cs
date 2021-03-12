using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200016E RID: 366
	internal abstract class UMLoggingManager : DisposableBase
	{
		// Token: 0x06000AB8 RID: 2744
		internal abstract void EnterTurn(string turnName);

		// Token: 0x06000AB9 RID: 2745
		internal abstract void ExitTurn();

		// Token: 0x06000ABA RID: 2746
		internal abstract void EnterTask(string name);

		// Token: 0x06000ABB RID: 2747
		internal abstract void ExitTask(UMNavigationState state, string message);

		// Token: 0x06000ABC RID: 2748
		internal abstract void LogApplicationInformation(string format, params object[] args);
	}
}
