using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D26 RID: 3366
	[Cmdlet("Get", "UMService", DefaultParameterSetName = "Identity")]
	public sealed class GetUMServer : GetSystemConfigurationObjectTask<UMServerIdParameter, Server>
	{
		// Token: 0x1700280B RID: 10251
		// (get) Token: 0x0600811E RID: 33054 RVA: 0x0021061B File Offset: 0x0020E81B
		protected override QueryFilter InternalFilter
		{
			get
			{
				return new BitMaskAndFilter(ServerSchema.CurrentServerRole, 16UL);
			}
		}

		// Token: 0x1700280C RID: 10252
		// (get) Token: 0x0600811F RID: 33055 RVA: 0x0021062A File Offset: 0x0020E82A
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06008120 RID: 33056 RVA: 0x00210630 File Offset: 0x0020E830
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			base.WriteResult(new UMServer((Server)dataObject));
			TaskLogger.LogExit();
		}
	}
}
