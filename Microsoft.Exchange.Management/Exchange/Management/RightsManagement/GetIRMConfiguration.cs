using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000708 RID: 1800
	[Cmdlet("Get", "IRMConfiguration")]
	public sealed class GetIRMConfiguration : GetMultitenancySingletonSystemConfigurationObjectTask<IRMConfiguration>
	{
		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x06003FF6 RID: 16374 RVA: 0x00105B68 File Offset: 0x00103D68
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003FF7 RID: 16375 RVA: 0x00105B6C File Offset: 0x00103D6C
		protected override IEnumerable<IRMConfiguration> GetPagedData()
		{
			IEnumerable<IRMConfiguration> enumerable = base.GetPagedData().ToList<IRMConfiguration>();
			if (!enumerable.Any<IRMConfiguration>())
			{
				IRMConfiguration item = IRMConfiguration.Read((IConfigurationSession)base.DataSession);
				enumerable = new List<IRMConfiguration>
				{
					item
				};
			}
			return enumerable;
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x00105BAE File Offset: 0x00103DAE
		protected override void WriteResult(IConfigurable dataObject)
		{
			IRMConfiguration irmconfiguration = (IRMConfiguration)dataObject;
			base.WriteResult(dataObject);
		}
	}
}
