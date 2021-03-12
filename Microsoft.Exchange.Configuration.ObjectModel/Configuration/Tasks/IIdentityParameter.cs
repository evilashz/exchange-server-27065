using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200002D RID: 45
	public interface IIdentityParameter
	{
		// Token: 0x060001B6 RID: 438
		IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new();

		// Token: 0x060001B7 RID: 439
		IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new();

		// Token: 0x060001B8 RID: 440
		void Initialize(ObjectId objectId);

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001B9 RID: 441
		string RawIdentity { get; }
	}
}
