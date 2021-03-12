using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Configuration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001B7 RID: 439
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAggregatedUserConfigurationReader
	{
		// Token: 0x060017EB RID: 6123
		IReadableUserConfiguration Read(IMailboxSession session, UserConfigurationDescriptor descriptor);

		// Token: 0x060017EC RID: 6124
		bool TryRead<T>(string key, out T result) where T : SerializableDataBase;

		// Token: 0x060017ED RID: 6125
		void Validate(IUserConfigurationManager manager, IXSOFactory xsoFactory, IAggregationReValidator validator, Action<IEnumerable<UserConfigurationDescriptor.MementoClass>, IEnumerable<string>> callback);
	}
}
