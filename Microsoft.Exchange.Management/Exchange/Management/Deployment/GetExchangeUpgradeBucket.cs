using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001D2 RID: 466
	[Cmdlet("Get", "ExchangeUpgradeBucket", DefaultParameterSetName = "Identity")]
	public sealed class GetExchangeUpgradeBucket : GetSystemConfigurationObjectTask<ExchangeUpgradeBucketIdParameter, ExchangeUpgradeBucket>
	{
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x00048387 File Offset: 0x00046587
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x0004838A File Offset: 0x0004658A
		// (set) Token: 0x0600102A RID: 4138 RVA: 0x00048392 File Offset: 0x00046592
		[Parameter(Mandatory = false)]
		public SwitchParameter EnableMailboxCounting { get; set; }

		// Token: 0x0600102B RID: 4139 RVA: 0x0004839C File Offset: 0x0004659C
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			if (this.EnableMailboxCounting)
			{
				ExchangeUpgradeBucket exchangeUpgradeBucket = (ExchangeUpgradeBucket)dataObject;
				exchangeUpgradeBucket.MailboxCount = UpgradeBucketTaskHelper.GetMailboxCount(exchangeUpgradeBucket);
			}
			base.WriteResult(dataObject);
			TaskLogger.LogExit();
		}
	}
}
