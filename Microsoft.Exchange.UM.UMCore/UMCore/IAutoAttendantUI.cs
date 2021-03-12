using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000033 RID: 51
	internal interface IAutoAttendantUI
	{
		// Token: 0x060001F5 RID: 501
		object ReadProperty(string name);

		// Token: 0x060001F6 RID: 502
		void WriteProperty(string name, object value);

		// Token: 0x060001F7 RID: 503
		object ReadGlobalProperty(string name);

		// Token: 0x060001F8 RID: 504
		void WriteGlobalProperty(string name, object value);

		// Token: 0x060001F9 RID: 505
		void SetTextPrompt(string name, string promptText);

		// Token: 0x060001FA RID: 506
		void SetWavePrompt(string name, ITempWavFile promptFile);
	}
}
