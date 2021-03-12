using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x0200039C RID: 924
	[Cmdlet("Get", "MailFilterListReport")]
	[OutputType(new Type[]
	{
		typeof(MailFilterListReport)
	})]
	public sealed class GetMailFilterListReport : FfoReportingTask<MailFilterListReport>
	{
		// Token: 0x06002046 RID: 8262 RVA: 0x00088A4C File Offset: 0x00086C4C
		public GetMailFilterListReport()
		{
			this.Domain = new MultiValuedProperty<Fqdn>();
			this.delegates.Add(GetMailFilterListReport.SelectionTargets.DlpPolicy.ToString().ToLower(), new GetMailFilterListReport.GetFilterListDelegate(this.GetDlpPolicies));
			this.delegates.Add(GetMailFilterListReport.SelectionTargets.DlpRule.ToString().ToLower(), new GetMailFilterListReport.GetFilterListDelegate(this.GetDlpRules));
			this.delegates.Add(GetMailFilterListReport.SelectionTargets.TransportRule.ToString().ToLower(), new GetMailFilterListReport.GetFilterListDelegate(this.GetPolicyRules));
			this.delegates.Add(GetMailFilterListReport.SelectionTargets.Domain.ToString().ToLower(), new GetMailFilterListReport.GetFilterListDelegate(this.GetDomains));
			this.delegates.Add(GetMailFilterListReport.SelectionTargets.EventTypes.ToString().ToLower(), new GetMailFilterListReport.GetFilterListDelegate(this.GetEventTypes));
			this.delegates.Add(GetMailFilterListReport.SelectionTargets.Actions.ToString().ToLower(), new GetMailFilterListReport.GetFilterListDelegate(this.GetActions));
			this.delegates.Add(GetMailFilterListReport.SelectionTargets.FindOnPremConnector.ToString().ToLower(), new GetMailFilterListReport.GetFilterListDelegate(this.FindOnPremConnector));
			this.delegates.Add(GetMailFilterListReport.SelectionTargets.Sources.ToString().ToLower(), new GetMailFilterListReport.GetFilterListDelegate(this.GetSources));
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x00088BB8 File Offset: 0x00086DB8
		// (set) Token: 0x06002048 RID: 8264 RVA: 0x00088BC0 File Offset: 0x00086DC0
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[QueryParameter("DomainListQueryDefinition", new string[]
		{

		})]
		public MultiValuedProperty<Fqdn> Domain { get; set; }

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002049 RID: 8265 RVA: 0x00088BC9 File Offset: 0x00086DC9
		// (set) Token: 0x0600204A RID: 8266 RVA: 0x00088BD1 File Offset: 0x00086DD1
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(GetMailFilterListReport.SelectionTargets)
		}, ErrorMessage = Strings.IDs.InvalidSelectionTarget)]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> SelectionTarget
		{
			get
			{
				return this.selectionTarget;
			}
			set
			{
				this.selectionTarget = value;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x00088BDA File Offset: 0x00086DDA
		public override string ComponentName
		{
			get
			{
				return ExchangeComponent.FfoRws.Name;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x00088BE6 File Offset: 0x00086DE6
		public override string MonitorEventName
		{
			get
			{
				return "FFO GetFilterValueList Status Monitor";
			}
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x00088BF0 File Offset: 0x00086DF0
		protected override void CustomInternalValidate()
		{
			base.CustomInternalValidate();
			if (this.SelectionTarget.Count == 0)
			{
				this.SelectionTarget.Add(GetMailFilterListReport.SelectionTargets.DlpPolicy.ToString());
				this.SelectionTarget.Add(GetMailFilterListReport.SelectionTargets.DlpRule.ToString());
				this.SelectionTarget.Add(GetMailFilterListReport.SelectionTargets.TransportRule.ToString());
				this.SelectionTarget.Add(GetMailFilterListReport.SelectionTargets.Domain.ToString());
				this.SelectionTarget.Add(GetMailFilterListReport.SelectionTargets.EventTypes.ToString());
				this.SelectionTarget.Add(GetMailFilterListReport.SelectionTargets.Actions.ToString());
				this.SelectionTarget.Add(GetMailFilterListReport.SelectionTargets.FindOnPremConnector.ToString());
				this.SelectionTarget.Add(GetMailFilterListReport.SelectionTargets.Sources.ToString());
			}
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x00088CC4 File Offset: 0x00086EC4
		protected override IReadOnlyList<MailFilterListReport> AggregateOutput()
		{
			List<MailFilterListReport> list = new List<MailFilterListReport>();
			foreach (string text in this.SelectionTarget)
			{
				GetMailFilterListReport.GetFilterListDelegate getFilterListDelegate;
				if (!string.IsNullOrEmpty(text) && this.delegates.TryGetValue(text.ToLower(), out getFilterListDelegate))
				{
					try
					{
						base.Diagnostics.StartTimer(text);
						GetMailFilterListReport.SelectionTargets selectionTargets = (GetMailFilterListReport.SelectionTargets)Enum.Parse(typeof(GetMailFilterListReport.SelectionTargets), text, true);
						getFilterListDelegate(selectionTargets.ToString(), list);
					}
					finally
					{
						base.Diagnostics.StopTimer(text);
					}
				}
			}
			list.Sort(new Comparison<MailFilterListReport>(GetMailFilterListReport.CompareMailFilterListReport));
			return list;
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x00088D98 File Offset: 0x00086F98
		private static int CompareMailFilterListReport(MailFilterListReport first, MailFilterListReport second)
		{
			if (first == null)
			{
				if (second == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (second == null)
				{
					return 1;
				}
				int num = string.Compare(first.SelectionTarget, second.SelectionTarget);
				if (num == 0)
				{
					num = string.Compare(first.Display, second.Display);
				}
				return num;
			}
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00088DDC File Offset: 0x00086FDC
		private void GetDlpPolicies(string target, List<MailFilterListReport> values)
		{
			if (Schema.Utilities.HasDlpRole(this))
			{
				foreach (ADComplianceProgram adcomplianceProgram in DlpUtils.GetInstalledTenantDlpPolicies(base.ConfigSession))
				{
					values.Add(new MailFilterListReport
					{
						Organization = base.Organization.ToString(),
						SelectionTarget = target,
						Display = adcomplianceProgram.Name,
						Value = adcomplianceProgram.Name
					});
				}
			}
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x00088EF4 File Offset: 0x000870F4
		private void GetDlpRules(string target, List<MailFilterListReport> values)
		{
			if (Schema.Utilities.HasDlpRole(this))
			{
				values.AddRange(from rule in DlpUtils.GetTransportRules(base.ConfigSession, (Rule rule) => rule.DlpPolicyId != Guid.Empty)
				select new MailFilterListReport
				{
					Organization = this.Organization.ToString(),
					SelectionTarget = target,
					Display = rule.Name,
					Value = rule.Name,
					ParentTarget = GetMailFilterListReport.SelectionTargets.DlpPolicy.ToString(),
					ParentValue = rule.DlpPolicy
				});
			}
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x00088FD0 File Offset: 0x000871D0
		private void GetPolicyRules(string target, List<MailFilterListReport> values)
		{
			values.AddRange(from rule in DlpUtils.GetTransportRules(base.ConfigSession, (Rule rule) => rule.DlpPolicyId == Guid.Empty)
			select new MailFilterListReport
			{
				Organization = this.Organization.ToString(),
				SelectionTarget = target,
				Display = rule.Name,
				Value = rule.Name
			});
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x00089030 File Offset: 0x00087230
		private void GetDomains(string target, List<MailFilterListReport> values)
		{
			AcceptedDomainIdParameter acceptedDomainIdParameter = AcceptedDomainIdParameter.Parse("*");
			foreach (AcceptedDomain acceptedDomain in acceptedDomainIdParameter.GetObjects<AcceptedDomain>(null, base.ConfigSession))
			{
				values.Add(new MailFilterListReport
				{
					Organization = base.Organization.ToString(),
					SelectionTarget = target,
					Display = acceptedDomain.Name,
					Value = acceptedDomain.Name
				});
			}
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x000890C8 File Offset: 0x000872C8
		private void GetEventTypes(string target, List<MailFilterListReport> values)
		{
			IList<string> eventTypes = Schema.Utilities.GetEventTypes(this);
			foreach (string text in eventTypes)
			{
				values.Add(new MailFilterListReport
				{
					Organization = base.Organization.ToString(),
					SelectionTarget = target,
					Display = text,
					Value = text
				});
			}
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x00089144 File Offset: 0x00087344
		private void GetSources(string target, List<MailFilterListReport> values)
		{
			IList<string> sources = Schema.Utilities.GetSources(this);
			foreach (string text in sources)
			{
				values.Add(new MailFilterListReport
				{
					Organization = base.Organization.ToString(),
					SelectionTarget = target,
					Display = text,
					Value = text
				});
			}
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x000891C0 File Offset: 0x000873C0
		private void GetActions(string target, List<MailFilterListReport> values)
		{
			string[] names = Enum.GetNames(typeof(Schema.Actions));
			foreach (string text in names)
			{
				values.Add(new MailFilterListReport
				{
					Organization = base.Organization.ToString(),
					SelectionTarget = target,
					Display = text,
					Value = text
				});
			}
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x00089248 File Offset: 0x00087448
		private void FindOnPremConnector(string target, List<MailFilterListReport> values)
		{
			if (this.Domain.Count == 0)
			{
				return;
			}
			OutboundConnectorIdParameter outboundConnectorIdParameter = OutboundConnectorIdParameter.Parse("*");
			TenantOutboundConnector tenantOutboundConnector = null;
			int num = -1;
			string domain = this.Domain[0].ToString();
			foreach (TenantOutboundConnector tenantOutboundConnector2 in outboundConnectorIdParameter.GetObjects<TenantOutboundConnector>(null, base.ConfigSession))
			{
				if (tenantOutboundConnector2.Enabled && tenantOutboundConnector2.ConnectorType == TenantConnectorType.OnPremises)
				{
					if (tenantOutboundConnector2.RecipientDomains.Any((SmtpDomainWithSubdomains smtpDomain) => smtpDomain.Domain.Equals(domain, StringComparison.InvariantCultureIgnoreCase)))
					{
						tenantOutboundConnector = tenantOutboundConnector2;
						break;
					}
					foreach (SmtpDomainWithSubdomains smtpDomainWithSubdomains in tenantOutboundConnector2.RecipientDomains)
					{
						WildcardPattern wildcardPattern = new WildcardPattern(smtpDomainWithSubdomains.Address);
						int num2 = wildcardPattern.Match(domain);
						if (num2 > num)
						{
							num = num2;
							tenantOutboundConnector = tenantOutboundConnector2;
						}
					}
				}
			}
			if (tenantOutboundConnector != null)
			{
				values.Add(new MailFilterListReport
				{
					Organization = base.Organization.ToString(),
					SelectionTarget = target,
					Display = "Name",
					Value = tenantOutboundConnector.Name
				});
			}
		}

		// Token: 0x040019CF RID: 6607
		private Dictionary<string, GetMailFilterListReport.GetFilterListDelegate> delegates = new Dictionary<string, GetMailFilterListReport.GetFilterListDelegate>();

		// Token: 0x040019D0 RID: 6608
		private MultiValuedProperty<string> selectionTarget = new MultiValuedProperty<string>();

		// Token: 0x0200039D RID: 925
		[Flags]
		private enum SelectionTargets
		{
			// Token: 0x040019D5 RID: 6613
			Actions = 0,
			// Token: 0x040019D6 RID: 6614
			DlpPolicy = 1,
			// Token: 0x040019D7 RID: 6615
			DlpRule = 2,
			// Token: 0x040019D8 RID: 6616
			Domain = 3,
			// Token: 0x040019D9 RID: 6617
			EventTypes = 4,
			// Token: 0x040019DA RID: 6618
			FindOnPremConnector = 5,
			// Token: 0x040019DB RID: 6619
			TransportRule = 6,
			// Token: 0x040019DC RID: 6620
			Sources = 7
		}

		// Token: 0x0200039E RID: 926
		// (Invoke) Token: 0x0600205B RID: 8283
		private delegate void GetFilterListDelegate(string target, List<MailFilterListReport> list);
	}
}
