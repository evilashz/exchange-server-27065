using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x02000015 RID: 21
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ADComputerWrapper : ADObjectWrapperBase, IADComputer, IADObjectCommon
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00003BBA File Offset: 0x00001DBA
		private ADComputerWrapper(ADComputer adComputer) : base(adComputer)
		{
			this.DnsHostName = adComputer.DnsHostName;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003BCF File Offset: 0x00001DCF
		public static ADComputerWrapper CreateWrapper(ADComputer adComputer)
		{
			if (adComputer == null)
			{
				return null;
			}
			return new ADComputerWrapper(adComputer);
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00003BDC File Offset: 0x00001DDC
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public string DnsHostName { get; private set; }
	}
}
