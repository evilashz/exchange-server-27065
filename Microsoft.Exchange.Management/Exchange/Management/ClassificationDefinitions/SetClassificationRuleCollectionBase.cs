using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000849 RID: 2121
	public abstract class SetClassificationRuleCollectionBase : SetSystemConfigurationObjectTask<ClassificationRuleCollectionIdParameter, TransportRule>
	{
		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x060049A8 RID: 18856 RVA: 0x0012F070 File Offset: 0x0012D270
		// (set) Token: 0x060049A9 RID: 18857 RVA: 0x0012F078 File Offset: 0x0012D278
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = false, ValueFromPipeline = false)]
		public override ClassificationRuleCollectionIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}
	}
}
