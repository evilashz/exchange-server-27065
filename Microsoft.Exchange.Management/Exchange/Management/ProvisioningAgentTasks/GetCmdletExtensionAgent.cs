using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Management.ProvisioningAgentTasks
{
	// Token: 0x02000CD7 RID: 3287
	[Cmdlet("Get", "CmdletExtensionAgent", DefaultParameterSetName = "Identity")]
	public sealed class GetCmdletExtensionAgent : GetSystemConfigurationObjectTask<CmdletExtensionAgentIdParameter, CmdletExtensionAgent>
	{
		// Token: 0x1700275C RID: 10076
		// (get) Token: 0x06007EBD RID: 32445 RVA: 0x00205C94 File Offset: 0x00203E94
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700275D RID: 10077
		// (get) Token: 0x06007EBE RID: 32446 RVA: 0x00205C97 File Offset: 0x00203E97
		// (set) Token: 0x06007EBF RID: 32447 RVA: 0x00205CAE File Offset: 0x00203EAE
		[Parameter(Mandatory = false, ParameterSetName = "Filters")]
		public string Assembly
		{
			get
			{
				return (string)base.Fields["Assembly"];
			}
			set
			{
				base.Fields["Assembly"] = value;
			}
		}

		// Token: 0x1700275E RID: 10078
		// (get) Token: 0x06007EC0 RID: 32448 RVA: 0x00205CC1 File Offset: 0x00203EC1
		// (set) Token: 0x06007EC1 RID: 32449 RVA: 0x00205CD8 File Offset: 0x00203ED8
		[Parameter(Mandatory = false, ParameterSetName = "Filters")]
		public bool Enabled
		{
			get
			{
				return (bool)base.Fields["Enabled"];
			}
			set
			{
				base.Fields["Enabled"] = value;
			}
		}

		// Token: 0x1700275F RID: 10079
		// (get) Token: 0x06007EC2 RID: 32450 RVA: 0x00205CF0 File Offset: 0x00203EF0
		protected override QueryFilter InternalFilter
		{
			get
			{
				return this.internalFilter ?? base.InternalFilter;
			}
		}

		// Token: 0x06007EC3 RID: 32451 RVA: 0x00205D04 File Offset: 0x00203F04
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			CmdletExtensionAgentsGlobalConfig cmdletExtensionAgentsGlobalConfig = new CmdletExtensionAgentsGlobalConfig((ITopologyConfigurationSession)base.DataSession);
			foreach (LocalizedString text in cmdletExtensionAgentsGlobalConfig.ConfigurationIssues)
			{
				this.WriteWarning(text);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007EC4 RID: 32452 RVA: 0x00205D5C File Offset: 0x00203F5C
		protected override void InternalValidate()
		{
			if (this.Assembly != null)
			{
				QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, CmdletExtensionAgentSchema.Assembly, this.Assembly);
				if (this.InternalFilter == null)
				{
					this.internalFilter = queryFilter;
				}
				else
				{
					this.internalFilter = new AndFilter(new QueryFilter[]
					{
						this.InternalFilter,
						queryFilter
					});
				}
			}
			if (base.Fields["Enabled"] != null)
			{
				QueryFilter queryFilter2 = new BitMaskAndFilter(CmdletExtensionAgentSchema.CmdletExtensionFlags, 1UL);
				if (!this.Enabled)
				{
					queryFilter2 = new NotFilter(queryFilter2);
				}
				if (this.InternalFilter == null)
				{
					this.internalFilter = queryFilter2;
				}
				else
				{
					this.internalFilter = new AndFilter(new QueryFilter[]
					{
						this.InternalFilter,
						queryFilter2
					});
				}
			}
			base.InternalValidate();
		}

		// Token: 0x04003E3F RID: 15935
		private QueryFilter internalFilter;
	}
}
