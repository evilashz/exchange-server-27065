using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000926 RID: 2342
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ParticipantOrigin
	{
		// Token: 0x0600578C RID: 22412
		internal abstract IEnumerable<PropValue> GetProperties();

		// Token: 0x0600578D RID: 22413
		internal abstract ParticipantValidationStatus Validate(Participant participant);
	}
}
