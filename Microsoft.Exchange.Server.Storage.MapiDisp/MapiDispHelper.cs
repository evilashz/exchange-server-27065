using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Server.Storage.MapiDisp
{
	// Token: 0x02000003 RID: 3
	public static class MapiDispHelper
	{
		// Token: 0x06000139 RID: 313 RVA: 0x0002945E File Offset: 0x0002765E
		public static string GetDnsHostName()
		{
			return ComputerInformation.DnsHostName;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00029465 File Offset: 0x00027665
		public static bool IsSupportedMrsVersion(ulong clientVersion)
		{
			return clientVersion >= MapiVersion.MRS14SP1.Value;
		}
	}
}
