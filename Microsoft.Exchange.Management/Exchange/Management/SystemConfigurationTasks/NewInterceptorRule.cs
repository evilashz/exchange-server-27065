using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.Agent.InterceptorAgent;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B3B RID: 2875
	[Cmdlet("New", "InterceptorRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class NewInterceptorRule : NewFixedNameSystemConfigurationObjectTask<InterceptorRule>
	{
		// Token: 0x06006789 RID: 26505 RVA: 0x001AC878 File Offset: 0x001AAA78
		public NewInterceptorRule()
		{
			base.Fields.ResetChangeTracking();
		}

		// Token: 0x17001FCA RID: 8138
		// (get) Token: 0x0600678A RID: 26506 RVA: 0x001AC8AC File Offset: 0x001AAAAC
		// (set) Token: 0x0600678B RID: 26507 RVA: 0x001AC8C3 File Offset: 0x001AAAC3
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[ValidateNotNullOrEmpty]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x17001FCB RID: 8139
		// (get) Token: 0x0600678C RID: 26508 RVA: 0x001AC8D6 File Offset: 0x001AAAD6
		// (set) Token: 0x0600678D RID: 26509 RVA: 0x001AC8ED File Offset: 0x001AAAED
		[Parameter(Mandatory = true)]
		public InterceptorAgentRuleBehavior Action
		{
			get
			{
				return (InterceptorAgentRuleBehavior)base.Fields["Action"];
			}
			set
			{
				base.Fields["Action"] = value;
			}
		}

		// Token: 0x17001FCC RID: 8140
		// (get) Token: 0x0600678E RID: 26510 RVA: 0x001AC905 File Offset: 0x001AAB05
		// (set) Token: 0x0600678F RID: 26511 RVA: 0x001AC91C File Offset: 0x001AAB1C
		[Parameter(Mandatory = true)]
		public InterceptorAgentEvent Event
		{
			get
			{
				return (InterceptorAgentEvent)base.Fields["Event"];
			}
			set
			{
				base.Fields["Event"] = value;
			}
		}

		// Token: 0x17001FCD RID: 8141
		// (get) Token: 0x06006790 RID: 26512 RVA: 0x001AC934 File Offset: 0x001AAB34
		// (set) Token: 0x06006791 RID: 26513 RVA: 0x001AC94B File Offset: 0x001AAB4B
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true)]
		public string Condition
		{
			get
			{
				return (string)base.Fields["Condition"];
			}
			set
			{
				base.Fields["Condition"] = value;
			}
		}

		// Token: 0x17001FCE RID: 8142
		// (get) Token: 0x06006792 RID: 26514 RVA: 0x001AC95E File Offset: 0x001AAB5E
		// (set) Token: 0x06006793 RID: 26515 RVA: 0x001AC975 File Offset: 0x001AAB75
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true)]
		public string Description
		{
			get
			{
				return (string)base.Fields["Description"];
			}
			set
			{
				base.Fields["Description"] = value;
			}
		}

		// Token: 0x17001FCF RID: 8143
		// (get) Token: 0x06006794 RID: 26516 RVA: 0x001AC988 File Offset: 0x001AAB88
		// (set) Token: 0x06006795 RID: 26517 RVA: 0x001AC99F File Offset: 0x001AAB9F
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public MultiValuedProperty<ServerIdParameter> Server
		{
			get
			{
				return (MultiValuedProperty<ServerIdParameter>)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x17001FD0 RID: 8144
		// (get) Token: 0x06006796 RID: 26518 RVA: 0x001AC9B2 File Offset: 0x001AABB2
		// (set) Token: 0x06006797 RID: 26519 RVA: 0x001AC9C9 File Offset: 0x001AABC9
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		[ValidateNotNullOrEmpty]
		public MultiValuedProperty<DatabaseAvailabilityGroupIdParameter> Dag
		{
			get
			{
				return (MultiValuedProperty<DatabaseAvailabilityGroupIdParameter>)base.Fields["Dag"];
			}
			set
			{
				base.Fields["Dag"] = value;
			}
		}

		// Token: 0x17001FD1 RID: 8145
		// (get) Token: 0x06006798 RID: 26520 RVA: 0x001AC9DC File Offset: 0x001AABDC
		// (set) Token: 0x06006799 RID: 26521 RVA: 0x001AC9F3 File Offset: 0x001AABF3
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		[ValidateNotNullOrEmpty]
		public MultiValuedProperty<AdSiteIdParameter> Site
		{
			get
			{
				return (MultiValuedProperty<AdSiteIdParameter>)base.Fields["Site"];
			}
			set
			{
				base.Fields["Site"] = value;
			}
		}

		// Token: 0x17001FD2 RID: 8146
		// (get) Token: 0x0600679A RID: 26522 RVA: 0x001ACA06 File Offset: 0x001AAC06
		// (set) Token: 0x0600679B RID: 26523 RVA: 0x001ACA2B File Offset: 0x001AAC2B
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TimeInterval
		{
			get
			{
				return (EnhancedTimeSpan)(base.Fields["TimeInterval"] ?? NewInterceptorRule.DefaultTimeInterval);
			}
			set
			{
				base.Fields["TimeInterval"] = value;
			}
		}

		// Token: 0x17001FD3 RID: 8147
		// (get) Token: 0x0600679C RID: 26524 RVA: 0x001ACA43 File Offset: 0x001AAC43
		// (set) Token: 0x0600679D RID: 26525 RVA: 0x001ACA5A File Offset: 0x001AAC5A
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string CustomResponseString
		{
			get
			{
				return (string)base.Fields["CustomResponseText"];
			}
			set
			{
				base.Fields["CustomResponseText"] = value;
			}
		}

		// Token: 0x17001FD4 RID: 8148
		// (get) Token: 0x0600679E RID: 26526 RVA: 0x001ACA6D File Offset: 0x001AAC6D
		// (set) Token: 0x0600679F RID: 26527 RVA: 0x001ACA84 File Offset: 0x001AAC84
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string CustomResponseCode
		{
			get
			{
				return (string)base.Fields["CustomResponseCode"];
			}
			set
			{
				base.Fields["CustomResponseCode"] = value;
			}
		}

		// Token: 0x17001FD5 RID: 8149
		// (get) Token: 0x060067A0 RID: 26528 RVA: 0x001ACA97 File Offset: 0x001AAC97
		// (set) Token: 0x060067A1 RID: 26529 RVA: 0x001ACAAE File Offset: 0x001AACAE
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public DateTime ExpireTime
		{
			get
			{
				return (DateTime)base.Fields["ExpireTime"];
			}
			set
			{
				base.Fields["ExpireTime"] = value;
			}
		}

		// Token: 0x17001FD6 RID: 8150
		// (get) Token: 0x060067A2 RID: 26530 RVA: 0x001ACAC6 File Offset: 0x001AACC6
		// (set) Token: 0x060067A3 RID: 26531 RVA: 0x001ACADD File Offset: 0x001AACDD
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string Path
		{
			get
			{
				return (string)base.Fields["Path"];
			}
			set
			{
				base.Fields["Path"] = value;
			}
		}

		// Token: 0x17001FD7 RID: 8151
		// (get) Token: 0x060067A4 RID: 26532 RVA: 0x001ACAF0 File Offset: 0x001AACF0
		// (set) Token: 0x060067A5 RID: 26533 RVA: 0x001ACB1B File Offset: 0x001AAD1B
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public SourceType Source
		{
			get
			{
				if (base.Fields.Contains("Source"))
				{
					return (SourceType)base.Fields["Source"];
				}
				return SourceType.User;
			}
			set
			{
				base.Fields["Source"] = value;
			}
		}

		// Token: 0x17001FD8 RID: 8152
		// (get) Token: 0x060067A6 RID: 26534 RVA: 0x001ACB33 File Offset: 0x001AAD33
		// (set) Token: 0x060067A7 RID: 26535 RVA: 0x001ACB62 File Offset: 0x001AAD62
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string CreatedBy
		{
			get
			{
				if (base.Fields.Contains("CreatedBy"))
				{
					return (string)base.Fields["CreatedBy"];
				}
				return InterceptorAgentRule.DefaultUser;
			}
			set
			{
				base.Fields["CreatedBy"] = value;
			}
		}

		// Token: 0x17001FD9 RID: 8153
		// (get) Token: 0x060067A8 RID: 26536 RVA: 0x001ACB75 File Offset: 0x001AAD75
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewInterceptorRule(this.Name);
			}
		}

		// Token: 0x17001FDA RID: 8154
		// (get) Token: 0x060067A9 RID: 26537 RVA: 0x001ACB82 File Offset: 0x001AAD82
		protected override bool SkipWriteResult
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060067AA RID: 26538 RVA: 0x001ACB88 File Offset: 0x001AAD88
		protected override IConfigurable PrepareDataObject()
		{
			ADObjectId descendantId = base.RootOrgContainerId.GetDescendantId(InterceptorRule.InterceptorRulesContainer);
			this.DataObject.Name = this.Name;
			InterceptorRule interceptorRule = (InterceptorRule)base.PrepareDataObject();
			interceptorRule.SetId(descendantId.GetChildId(this.DataObject.Name));
			return interceptorRule;
		}

		// Token: 0x060067AB RID: 26539 RVA: 0x001ACBDC File Offset: 0x001AADDC
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.Fields.IsModified("ExpireTime") && DateTime.UtcNow > this.ExpireTime.ToUniversalTime())
			{
				base.WriteError(new LocalizedException(Strings.InterceptorErrorExpireTimePassed(this.ExpireTime.ToString("G"))), ErrorCategory.InvalidData, "ExpireTime");
			}
			List<InterceptorAgentCondition> conditions;
			LocalizedString localizedString;
			if (!InterceptorHelper.TryCreateConditions(this.Condition, out conditions, out localizedString))
			{
				base.WriteError(new LocalizedException(localizedString), ErrorCategory.InvalidData, this.Condition);
				return;
			}
			if (this.Event == InterceptorAgentEvent.Invalid)
			{
				base.WriteError(new LocalizedException(Strings.InterceptorErrorEventInvalid), ErrorCategory.InvalidData, this.Event);
			}
			if (this.Action == InterceptorAgentRuleBehavior.NoOp)
			{
				base.WriteError(new LocalizedException(Strings.InterceptorErrorActionInvalid), ErrorCategory.InvalidData, this.Action);
			}
			if (!InterceptorHelper.ValidateEventConditionPairs(this.Event, conditions, out localizedString))
			{
				base.WriteError(new LocalizedException(localizedString), ErrorCategory.InvalidArgument, this.Condition);
			}
			if (!InterceptorHelper.ValidateEventActionPairs(this.Event, this.Action, out localizedString))
			{
				base.WriteError(new LocalizedException(localizedString), ErrorCategory.InvalidArgument, this.Condition);
			}
			InterceptorAgentAction interceptorAgentAction;
			LocalizedString warning;
			if (!InterceptorHelper.TryCreateAction(this.Action, this.CustomResponseCode, base.Fields.IsChanged("CustomResponseCode"), this.CustomResponseString, base.Fields.IsChanged("CustomResponseText"), this.TimeInterval, this.Path, out interceptorAgentAction, out warning, out localizedString))
			{
				base.WriteError(new LocalizedException(localizedString), ErrorCategory.InvalidData, this.Action);
			}
			this.WriteWarningAndReset(warning);
			if (InterceptorAgentAction.IsArchivingBehavior(interceptorAgentAction.Action))
			{
				this.WriteWarning(InterceptorHelper.GetArchivedItemRetentionMessage(interceptorAgentAction.Action, this.Name, this.Path, 14));
			}
			this.ResolveTargets();
			if (!base.HasErrors)
			{
				this.rule = new InterceptorAgentRule(this.Name, this.Description, conditions, interceptorAgentAction, this.Event, this.Source, this.CreatedBy);
			}
		}

		// Token: 0x060067AC RID: 26540 RVA: 0x001ACDEC File Offset: 0x001AAFEC
		protected override void InternalProcessRecord()
		{
			if (this.Action == InterceptorAgentRuleBehavior.Delay && !base.ShouldProcess(Strings.InterceptorConfirmDelayAction))
			{
				TaskLogger.LogExit();
				return;
			}
			if (this.dags.Count == 0 && this.servers.Count == 0 && this.sites.Count == 0 && !base.ShouldProcess(Strings.InterceptorConfirmEntireForestRule(this.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			this.DataObject.Version = InterceptorAgentRule.Version.ToString();
			this.DataObject.Xml = this.rule.ToXmlString();
			if (this.dags.Count > 0 || this.servers.Count > 0 || this.sites.Count > 0)
			{
				this.DataObject.Target = new MultiValuedProperty<ADObjectId>(this.servers.ConvertAll<ADObjectId>((Server s) => s.OriginalId).Concat(this.dags.ConvertAll<ADObjectId>((DatabaseAvailabilityGroup d) => d.OriginalId)).Concat(this.sites.ConvertAll<ADObjectId>((ADSite s) => s.OriginalId)));
			}
			if (base.Fields.IsChanged("ExpireTime"))
			{
				this.DataObject.ExpireTimeUtc = this.ExpireTime.ToUniversalTime();
			}
			else
			{
				this.DataObject.ExpireTimeUtc = DateTime.UtcNow + NewInterceptorRule.DefaultExpireTime;
			}
			base.InternalProcessRecord();
			this.rule.SetPropertiesFromAdObjet(this.DataObject);
			this.WriteResult(this.rule);
		}

		// Token: 0x060067AD RID: 26541 RVA: 0x001ACFAC File Offset: 0x001AB1AC
		private void ResolveTargets()
		{
			if (base.Fields.IsChanged("Server") && this.Server != null)
			{
				foreach (ServerIdParameter serverIdParameter in this.Server)
				{
					Server item = (Server)base.GetDataObject<Server>(serverIdParameter, base.RootOrgGlobalConfigSession, null, new LocalizedString?(Strings.ErrorServerNotFound(serverIdParameter.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(serverIdParameter.ToString())));
					if (!this.servers.Contains(item))
					{
						this.servers.Add(item);
					}
				}
			}
			if (base.Fields.IsChanged("Dag") && this.Dag != null)
			{
				foreach (DatabaseAvailabilityGroupIdParameter databaseAvailabilityGroupIdParameter in this.Dag)
				{
					DatabaseAvailabilityGroup item2 = (DatabaseAvailabilityGroup)base.GetDataObject<DatabaseAvailabilityGroup>(databaseAvailabilityGroupIdParameter, base.RootOrgGlobalConfigSession, null, new LocalizedString?(Strings.ErrorDagNotFound(databaseAvailabilityGroupIdParameter.ToString())), new LocalizedString?(Strings.ErrorDagNotUnique(databaseAvailabilityGroupIdParameter.ToString())));
					if (!this.dags.Contains(item2))
					{
						this.dags.Add(item2);
					}
				}
			}
			if (base.Fields.IsChanged("Site") && this.Site != null)
			{
				foreach (AdSiteIdParameter adSiteIdParameter in this.Site)
				{
					ADSite item3 = (ADSite)base.GetDataObject<ADSite>(adSiteIdParameter, base.RootOrgGlobalConfigSession, null, new LocalizedString?(Strings.ErrorSiteNotFound(adSiteIdParameter.ToString())), new LocalizedString?(Strings.ErrorSiteNotUnique(adSiteIdParameter.ToString())));
					if (!this.sites.Contains(item3))
					{
						this.sites.Add(item3);
					}
				}
			}
		}

		// Token: 0x060067AE RID: 26542 RVA: 0x001AD1BC File Offset: 0x001AB3BC
		private LocalizedString WriteWarningAndReset(LocalizedString warning)
		{
			if (warning != LocalizedString.Empty)
			{
				this.WriteWarning(warning);
			}
			return LocalizedString.Empty;
		}

		// Token: 0x04003669 RID: 13929
		internal static readonly TimeSpan DefaultExpireTime = TimeSpan.FromDays(7.0);

		// Token: 0x0400366A RID: 13930
		private InterceptorAgentRule rule;

		// Token: 0x0400366B RID: 13931
		private List<Server> servers = new List<Server>();

		// Token: 0x0400366C RID: 13932
		private List<DatabaseAvailabilityGroup> dags = new List<DatabaseAvailabilityGroup>();

		// Token: 0x0400366D RID: 13933
		private List<ADSite> sites = new List<ADSite>();

		// Token: 0x0400366E RID: 13934
		private static readonly EnhancedTimeSpan DefaultTimeInterval = EnhancedTimeSpan.FromMinutes(2.0);
	}
}
