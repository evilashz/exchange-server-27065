using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B59 RID: 2905
	[Cmdlet("Get", "TransportRulePredicate")]
	[OutputType(new Type[]
	{
		typeof(TransportRulePredicate)
	})]
	public sealed class GetTransportRulePredicate : Task
	{
		// Token: 0x1700207E RID: 8318
		// (get) Token: 0x0600695F RID: 26975 RVA: 0x001B2DD9 File Offset: 0x001B0FD9
		// (set) Token: 0x06006960 RID: 26976 RVA: 0x001B2DE1 File Offset: 0x001B0FE1
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

		// Token: 0x06006961 RID: 26977 RVA: 0x001B2DEC File Offset: 0x001B0FEC
		protected override void InternalProcessRecord()
		{
			bool enabled = VariantConfiguration.InvariantNoFlightingSnapshot.CompliancePolicy.ShowSupervisionPredicate.Enabled;
			IConfigDataProvider session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 49, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\TransportRule\\GetTransportRulePredicate.cs");
			TypeMapping[] availablePredicateMappings = TransportRulePredicate.GetAvailablePredicateMappings();
			if (string.IsNullOrEmpty(this.name))
			{
				foreach (TransportRulePredicate transportRulePredicate in TransportRulePredicate.CreateAllAvailablePredicates(availablePredicateMappings, session))
				{
					if (enabled || (!(transportRulePredicate is SenderInRecipientListPredicate) && !(transportRulePredicate is RecipientInSenderListPredicate)))
					{
						base.WriteObject(transportRulePredicate);
					}
				}
				return;
			}
			TransportRulePredicate transportRulePredicate2 = TransportRulePredicate.CreatePredicate(availablePredicateMappings, this.name, session);
			if (!enabled && (transportRulePredicate2 is SenderInRecipientListPredicate || transportRulePredicate2 is RecipientInSenderListPredicate))
			{
				transportRulePredicate2 = null;
			}
			if (transportRulePredicate2 == null)
			{
				base.WriteError(new ArgumentException(Strings.InvalidPredicate, "Name"), ErrorCategory.InvalidArgument, this.Name);
				return;
			}
			base.WriteObject(transportRulePredicate2);
		}

		// Token: 0x040036BF RID: 14015
		private string name;
	}
}
