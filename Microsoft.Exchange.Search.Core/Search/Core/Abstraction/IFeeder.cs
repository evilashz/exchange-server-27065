using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000024 RID: 36
	internal interface IFeeder : IExecutable, IDiagnosable, IDisposable
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000CA RID: 202
		FeederType FeederType { get; }
	}
}
