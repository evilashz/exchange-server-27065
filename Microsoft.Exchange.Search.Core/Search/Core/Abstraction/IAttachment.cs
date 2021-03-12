using System;
using System.IO;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000013 RID: 19
	internal interface IAttachment : IDisposable
	{
		// Token: 0x06000067 RID: 103
		Stream GetContentStream();
	}
}
