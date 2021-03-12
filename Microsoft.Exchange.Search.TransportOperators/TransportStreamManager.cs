using System;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.TransportOperators
{
	// Token: 0x02000013 RID: 19
	internal class TransportStreamManager
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000035AE File Offset: 0x000017AE
		internal static StreamManager Instance
		{
			get
			{
				return TransportStreamManager.instance;
			}
		}

		// Token: 0x04000039 RID: 57
		private static StreamManager instance = StreamManager.Create();
	}
}
