using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200002D RID: 45
	internal interface INodeManager
	{
		// Token: 0x060000F3 RID: 243
		bool AreAllNodesHealthy();

		// Token: 0x060000F4 RID: 244
		bool IsNodeHealthy(string nodeName);

		// Token: 0x060000F5 RID: 245
		bool IsNodeStopped(string nodeName);

		// Token: 0x060000F6 RID: 246
		void StartNode(string nodeName);

		// Token: 0x060000F7 RID: 247
		void KillNode(string nodeName);

		// Token: 0x060000F8 RID: 248
		void KillAndRestartNode(string nodeName);

		// Token: 0x060000F9 RID: 249
		void StopNode(string nodeName);
	}
}
