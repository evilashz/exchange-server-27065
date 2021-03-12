using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200004E RID: 78
	public class EcpVdirConfiguration : VdirConfiguration
	{
		// Token: 0x06000250 RID: 592 RVA: 0x0000D70C File Offset: 0x0000B90C
		private EcpVdirConfiguration(ADEcpVirtualDirectory ecpVirtualDirectory) : base(ecpVirtualDirectory)
		{
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000D715 File Offset: 0x0000B915
		public new static EcpVdirConfiguration Instance
		{
			get
			{
				return VdirConfiguration.Instance as EcpVdirConfiguration;
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000D724 File Offset: 0x0000B924
		internal static EcpVdirConfiguration CreateInstance(ITopologyConfigurationSession session, ADObjectId virtualDirectoryDN)
		{
			ADEcpVirtualDirectory adecpVirtualDirectory = null;
			ADEcpVirtualDirectory[] array = session.Find<ADEcpVirtualDirectory>(virtualDirectoryDN, QueryScope.Base, null, null, 1);
			if (array != null && array.Length == 1)
			{
				adecpVirtualDirectory = array[0];
			}
			if (adecpVirtualDirectory == null)
			{
				throw new ADNoSuchObjectException(LocalizedString.Empty);
			}
			return new EcpVdirConfiguration(adecpVirtualDirectory);
		}
	}
}
