using System;
using System.IO;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000146 RID: 326
	public static class TemporaryStorage
	{
		// Token: 0x06000CA6 RID: 3238 RVA: 0x0006EA94 File Offset: 0x0006CC94
		public static Stream Create()
		{
			TemporaryDataStorage temporaryDataStorage = new TemporaryDataStorage();
			Stream result = temporaryDataStorage.OpenWriteStream(false);
			temporaryDataStorage.Release();
			return result;
		}
	}
}
