using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x0200000E RID: 14
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IADComputer : IADObjectCommon
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000094 RID: 148
		string DnsHostName { get; }
	}
}
