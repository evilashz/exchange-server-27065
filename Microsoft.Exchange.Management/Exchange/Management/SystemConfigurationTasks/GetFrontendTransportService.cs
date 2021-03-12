using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009B2 RID: 2482
	[Cmdlet("Get", "FrontendTransportService", DefaultParameterSetName = "Identity")]
	public sealed class GetFrontendTransportService : GetSystemConfigurationObjectTask<FrontendTransportServerIdParameter, FrontendTransportServer>
	{
		// Token: 0x17001A6C RID: 6764
		// (get) Token: 0x06005894 RID: 22676 RVA: 0x00171BAC File Offset: 0x0016FDAC
		protected override QueryFilter InternalFilter
		{
			get
			{
				return new BitMaskOrFilter(FrontendTransportServerSchema.CurrentServerRole, 16384UL);
			}
		}

		// Token: 0x17001A6D RID: 6765
		// (get) Token: 0x06005895 RID: 22677 RVA: 0x00171BBE File Offset: 0x0016FDBE
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005896 RID: 22678 RVA: 0x00171BC4 File Offset: 0x0016FDC4
		protected override void WriteResult(IConfigurable dataObject)
		{
			FrontendTransportServer dataObject2 = (FrontendTransportServer)dataObject;
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			base.WriteResult(new FrontendTransportServerPresentationObject(dataObject2));
			TaskLogger.LogExit();
		}

		// Token: 0x06005897 RID: 22679 RVA: 0x00171C03 File Offset: 0x0016FE03
		protected override void InternalValidate()
		{
			if (this.Identity != null)
			{
				this.Identity = FrontendTransportServerIdParameter.CreateIdentity(this.Identity);
			}
			base.InternalValidate();
		}
	}
}
