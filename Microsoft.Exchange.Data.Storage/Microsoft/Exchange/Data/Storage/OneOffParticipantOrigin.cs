using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000927 RID: 2343
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OneOffParticipantOrigin : ParticipantOrigin
	{
		// Token: 0x0600578F RID: 22415 RVA: 0x00168248 File Offset: 0x00166448
		public override string ToString()
		{
			return "OneOff";
		}

		// Token: 0x06005790 RID: 22416 RVA: 0x0016824F File Offset: 0x0016644F
		internal override IEnumerable<PropValue> GetProperties()
		{
			return null;
		}

		// Token: 0x06005791 RID: 22417 RVA: 0x00168252 File Offset: 0x00166452
		internal override ParticipantValidationStatus Validate(Participant participant)
		{
			return ParticipantValidationStatus.NoError;
		}
	}
}
