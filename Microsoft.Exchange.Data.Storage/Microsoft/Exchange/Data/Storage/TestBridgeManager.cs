using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000694 RID: 1684
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class TestBridgeManager
	{
		// Token: 0x060044EC RID: 17644 RVA: 0x001255C4 File Offset: 0x001237C4
		internal static void SetBridge(ITestBridgeDelegateSessionCache bridge)
		{
			TestBridgeManager.bridge = bridge;
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x001255CC File Offset: 0x001237CC
		internal static int GetSize(int currentSize)
		{
			if (TestBridgeManager.bridge != null)
			{
				return TestBridgeManager.bridge.GetSize(currentSize);
			}
			return currentSize;
		}

		// Token: 0x060044EE RID: 17646 RVA: 0x001255E2 File Offset: 0x001237E2
		internal static void Removed(string mailboxLegacyDn)
		{
			if (TestBridgeManager.bridge != null)
			{
				TestBridgeManager.bridge.Removed(mailboxLegacyDn);
			}
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x001255F6 File Offset: 0x001237F6
		internal static void Added(string mailboxLegacyDn)
		{
			if (TestBridgeManager.bridge != null)
			{
				TestBridgeManager.bridge.Added(mailboxLegacyDn);
			}
		}

		// Token: 0x04002580 RID: 9600
		private static ITestBridgeDelegateSessionCache bridge;
	}
}
