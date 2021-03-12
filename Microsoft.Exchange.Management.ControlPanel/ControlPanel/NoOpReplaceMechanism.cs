using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006AF RID: 1711
	public class NoOpReplaceMechanism : ITranslationMechanism
	{
		// Token: 0x0600490D RID: 18701 RVA: 0x000DF63B File Offset: 0x000DD83B
		public string Translate(Identity id, Exception ex, string originalMessage)
		{
			return originalMessage;
		}
	}
}
