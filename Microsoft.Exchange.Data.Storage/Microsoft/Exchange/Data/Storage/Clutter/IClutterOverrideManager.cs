using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Clutter
{
	// Token: 0x02000039 RID: 57
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IClutterOverrideManager : ICollection<SmtpAddress>, IEnumerable<SmtpAddress>, IEnumerable, IDisposable
	{
		// Token: 0x06000532 RID: 1330
		void Save();
	}
}
