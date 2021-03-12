using System;
using System.IO;

namespace Microsoft.Exchange.VariantConfiguration.DataLoad
{
	// Token: 0x02000005 RID: 5
	internal interface IDataSourceReader
	{
		// Token: 0x06000018 RID: 24
		Func<TextReader> GetContentReader(string dataSource);

		// Token: 0x06000019 RID: 25
		bool CanGetContentReader(string dataSource);
	}
}
