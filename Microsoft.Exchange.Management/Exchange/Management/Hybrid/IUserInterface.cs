using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008DE RID: 2270
	internal interface IUserInterface
	{
		// Token: 0x06005083 RID: 20611
		void WriteVerbose(LocalizedString text);

		// Token: 0x06005084 RID: 20612
		void WriteWarning(LocalizedString text);

		// Token: 0x06005085 RID: 20613
		void WriteProgessIndicator(LocalizedString activity, LocalizedString statusDescription, int percentCompleted);

		// Token: 0x06005086 RID: 20614
		bool ShouldContinue(LocalizedString message);
	}
}
