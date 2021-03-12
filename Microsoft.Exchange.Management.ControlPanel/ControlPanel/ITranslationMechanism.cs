using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006A8 RID: 1704
	public interface ITranslationMechanism
	{
		// Token: 0x060048E7 RID: 18663
		string Translate(Identity id, Exception ex, string originalMessage);
	}
}
