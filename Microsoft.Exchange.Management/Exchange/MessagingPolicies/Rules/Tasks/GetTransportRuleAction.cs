using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B58 RID: 2904
	[Cmdlet("Get", "TransportRuleAction")]
	[OutputType(new Type[]
	{
		typeof(TransportRuleAction)
	})]
	public sealed class GetTransportRuleAction : Task
	{
		// Token: 0x1700207D RID: 8317
		// (get) Token: 0x0600695B RID: 26971 RVA: 0x001B2D1C File Offset: 0x001B0F1C
		// (set) Token: 0x0600695C RID: 26972 RVA: 0x001B2D24 File Offset: 0x001B0F24
		[Parameter(Mandatory = false, Position = 0)]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x0600695D RID: 26973 RVA: 0x001B2D30 File Offset: 0x001B0F30
		protected override void InternalProcessRecord()
		{
			TypeMapping[] availableActionMappings = TransportRuleAction.GetAvailableActionMappings();
			IConfigDataProvider session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 47, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\TransportRule\\GetTransportRuleAction.cs");
			if (string.IsNullOrEmpty(this.name))
			{
				foreach (TransportRuleAction sendToPipeline in TransportRuleAction.CreateAllAvailableActions(availableActionMappings, session))
				{
					base.WriteObject(sendToPipeline);
				}
				return;
			}
			TransportRuleAction transportRuleAction = TransportRuleAction.CreateAction(availableActionMappings, this.name, session);
			if (transportRuleAction == null)
			{
				base.WriteError(new ArgumentException(Strings.InvalidAction, "Name"), ErrorCategory.InvalidArgument, this.Name);
				return;
			}
			base.WriteObject(transportRuleAction);
		}

		// Token: 0x040036BE RID: 14014
		private string name;
	}
}
