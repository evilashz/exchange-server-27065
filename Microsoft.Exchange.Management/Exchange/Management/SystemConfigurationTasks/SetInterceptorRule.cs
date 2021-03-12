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
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.Agent.InterceptorAgent;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B4E RID: 2894
	[Cmdlet("Set", "InterceptorRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetInterceptorRule : SetSystemConfigurationObjectTask<InterceptorRuleIdParameter, InterceptorAgentRule, InterceptorRule>
	{
		// Token: 0x060068E4 RID: 26852 RVA: 0x001B00BF File Offset: 0x001AE2BF
		public SetInterceptorRule()
		{
			base.Fields.ResetChangeTracking();
		}

		// Token: 0x1700205A RID: 8282
		// (get) Token: 0x060068E5 RID: 26853 RVA: 0x001B00F3 File Offset: 0x001AE2F3
		// (set) Token: 0x060068E6 RID: 26854 RVA: 0x001B010A File Offset: 0x001AE30A
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700205B RID: 8283
		// (get) Token: 0x060068E7 RID: 26855 RVA: 0x001B0122 File Offset: 0x001AE322
		// (set) Token: 0x060068E8 RID: 26856 RVA: 0x001B0139 File Offset: 0x001AE339
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700205C RID: 8284
		// (get) Token: 0x060068E9 RID: 26857 RVA: 0x001B0151 File Offset: 0x001AE351
		// (set) Token: 0x060068EA RID: 26858 RVA: 0x001B0168 File Offset: 0x001AE368
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700205D RID: 8285
		// (get) Token: 0x060068EB RID: 26859 RVA: 0x001B017B File Offset: 0x001AE37B
		// (set) Token: 0x060068EC RID: 26860 RVA: 0x001B0192 File Offset: 0x001AE392
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x1700205E RID: 8286
		// (get) Token: 0x060068ED RID: 26861 RVA: 0x001B01A5 File Offset: 0x001AE3A5
		// (set) Token: 0x060068EE RID: 26862 RVA: 0x001B01BC File Offset: 0x001AE3BC
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

		// Token: 0x1700205F RID: 8287
		// (get) Token: 0x060068EF RID: 26863 RVA: 0x001B01CF File Offset: 0x001AE3CF
		// (set) Token: 0x060068F0 RID: 26864 RVA: 0x001B01E6 File Offset: 0x001AE3E6
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
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

		// Token: 0x17002060 RID: 8288
		// (get) Token: 0x060068F1 RID: 26865 RVA: 0x001B01F9 File Offset: 0x001AE3F9
		// (set) Token: 0x060068F2 RID: 26866 RVA: 0x001B0210 File Offset: 0x001AE410
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
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

		// Token: 0x17002061 RID: 8289
		// (get) Token: 0x060068F3 RID: 26867 RVA: 0x001B0223 File Offset: 0x001AE423
		// (set) Token: 0x060068F4 RID: 26868 RVA: 0x001B023A File Offset: 0x001AE43A
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TimeInterval
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["TimeInterval"];
			}
			set
			{
				base.Fields["TimeInterval"] = value;
			}
		}

		// Token: 0x17002062 RID: 8290
		// (get) Token: 0x060068F5 RID: 26869 RVA: 0x001B0252 File Offset: 0x001AE452
		// (set) Token: 0x060068F6 RID: 26870 RVA: 0x001B0269 File Offset: 0x001AE469
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
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

		// Token: 0x17002063 RID: 8291
		// (get) Token: 0x060068F7 RID: 26871 RVA: 0x001B027C File Offset: 0x001AE47C
		// (set) Token: 0x060068F8 RID: 26872 RVA: 0x001B0293 File Offset: 0x001AE493
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x17002064 RID: 8292
		// (get) Token: 0x060068F9 RID: 26873 RVA: 0x001B02A6 File Offset: 0x001AE4A6
		// (set) Token: 0x060068FA RID: 26874 RVA: 0x001B02BD File Offset: 0x001AE4BD
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

		// Token: 0x17002065 RID: 8293
		// (get) Token: 0x060068FB RID: 26875 RVA: 0x001B02D5 File Offset: 0x001AE4D5
		// (set) Token: 0x060068FC RID: 26876 RVA: 0x001B02EC File Offset: 0x001AE4EC
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
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

		// Token: 0x17002066 RID: 8294
		// (get) Token: 0x060068FD RID: 26877 RVA: 0x001B02FF File Offset: 0x001AE4FF
		// (set) Token: 0x060068FE RID: 26878 RVA: 0x001B032A File Offset: 0x001AE52A
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

		// Token: 0x17002067 RID: 8295
		// (get) Token: 0x060068FF RID: 26879 RVA: 0x001B0342 File Offset: 0x001AE542
		// (set) Token: 0x06006900 RID: 26880 RVA: 0x001B0371 File Offset: 0x001AE571
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

		// Token: 0x17002068 RID: 8296
		// (get) Token: 0x06006901 RID: 26881 RVA: 0x001B0384 File Offset: 0x001AE584
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetInterceptorRule(this.Identity.ToString());
			}
		}

		// Token: 0x17002069 RID: 8297
		// (get) Token: 0x06006902 RID: 26882 RVA: 0x001B0396 File Offset: 0x001AE596
		protected override ObjectId RootId
		{
			get
			{
				return base.RootOrgContainerId.GetDescendantId(InterceptorRule.InterceptorRulesContainer);
			}
		}

		// Token: 0x06006903 RID: 26883 RVA: 0x001B03A8 File Offset: 0x001AE5A8
		protected override void InternalValidate()
		{
			bool flag = base.Fields.IsModified("Event");
			bool flag2 = base.Fields.IsModified("Action");
			bool flag3 = base.Fields.IsModified("Condition");
			bool flag4 = base.Fields.IsModified("CustomResponseCode");
			bool flag5 = base.Fields.IsModified("CustomResponseText");
			bool flag6 = base.Fields.IsModified("Description");
			bool flag7 = base.Fields.IsModified("TimeInterval");
			bool flag8 = base.Fields.IsModified("Path");
			this.xmlNeedsUpdating = (flag2 || flag || flag3 || flag4 || flag5 || flag7 || flag6 || flag8 || base.Fields.IsModified("Identity"));
			if (base.Fields.IsModified("ExpireTime") && DateTime.UtcNow > this.ExpireTime.ToUniversalTime())
			{
				base.WriteError(new LocalizedException(Strings.InterceptorErrorExpireTimePassed(this.ExpireTime.ToString("G"))), ErrorCategory.InvalidData, "ExpireTime");
			}
			this.DataObject = (InterceptorRule)this.ResolveDataObject();
			if (this.xmlNeedsUpdating)
			{
				try
				{
					this.rule = InterceptorAgentRule.CreateRuleFromXml(this.DataObject.Xml);
					this.rule.SetPropertiesFromAdObjet(this.DataObject);
				}
				catch (FormatException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidData, null);
					TaskLogger.LogExit();
					return;
				}
				catch (InvalidOperationException exception2)
				{
					base.WriteError(exception2, ErrorCategory.InvalidData, null);
					TaskLogger.LogExit();
					return;
				}
			}
			if (this.rule.RuleVersion > InterceptorAgentRule.Version)
			{
				base.WriteError(new LocalizedException(Strings.InterceptorErrorModifyingNewerVersion(this.rule.RuleVersion.ToString())), ErrorCategory.InvalidOperation, null);
			}
			if (flag2 || flag)
			{
				InterceptorAgentRuleBehavior action = flag2 ? this.Action : this.rule.Action.Action;
				InterceptorAgentEvent interceptorAgentEvent = flag ? this.Event : this.rule.Events;
				LocalizedString localizedString;
				if (!InterceptorHelper.ValidateEventActionPairs(interceptorAgentEvent, action, out localizedString))
				{
					base.WriteError(new LocalizedException(localizedString), ErrorCategory.InvalidArgument, this.Condition);
				}
				this.rule.Events = interceptorAgentEvent;
				string customResponseCode;
				if (flag2 && !flag4 && InterceptorHelper.TryGetStatusCodeForModifiedRejectAction(this.Action, this.rule.Action.Action, this.rule.Action.Response.StatusCode, out customResponseCode))
				{
					this.CustomResponseCode = customResponseCode;
					flag4 = base.Fields.IsModified("CustomResponseCode");
				}
			}
			if (flag3)
			{
				LocalizedString localizedString;
				List<InterceptorAgentCondition> conditions;
				if (!InterceptorHelper.TryCreateConditions(this.Condition, out conditions, out localizedString))
				{
					base.WriteError(new LocalizedException(localizedString), ErrorCategory.InvalidData, this.Condition);
					return;
				}
				InterceptorAgentEvent evt = flag ? this.Event : this.rule.Events;
				if (!InterceptorHelper.ValidateEventConditionPairs(evt, conditions, out localizedString))
				{
					base.WriteError(new LocalizedException(localizedString), ErrorCategory.InvalidArgument, this.Condition);
				}
				this.rule.Conditions = conditions;
			}
			if (flag)
			{
				LocalizedString localizedString;
				if (!flag3 && !InterceptorHelper.ValidateEventConditionPairs(this.Event, this.rule.Conditions, out localizedString))
				{
					base.WriteError(new LocalizedException(localizedString), ErrorCategory.InvalidArgument, this.Condition);
				}
				this.rule.Events = this.Event;
			}
			if (flag6)
			{
				this.rule.Description = this.Description;
			}
			this.SetAction(flag4, flag5, flag2, flag8, flag7);
			this.ResolveTargets();
		}

		// Token: 0x06006904 RID: 26884 RVA: 0x001B0748 File Offset: 0x001AE948
		protected override void InternalProcessRecord()
		{
			this.rule.Name = this.Identity.ToString();
			if (base.Fields.IsModified("Action") && this.Action == InterceptorAgentRuleBehavior.Delay && !base.ShouldContinue(Strings.InterceptorConfirmDelayAction))
			{
				TaskLogger.LogExit();
				return;
			}
			bool flag = false;
			if ((base.Fields.IsModified("Dag") || base.Fields.IsModified("Site") || base.Fields.IsModified("Server")) && this.dags.Count == 0 && this.servers.Count == 0 && this.sites.Count == 0)
			{
				if (!base.ShouldContinue(Strings.InterceptorConfirmEntireForestRule(this.Identity.ToString())))
				{
					TaskLogger.LogExit();
					return;
				}
				flag = true;
			}
			if (InterceptorAgentRule.Version > this.rule.RuleVersion && !base.ShouldContinue(Strings.InterceptorConfirmModifyingOlderVersion(this.rule.RuleVersion.ToString(), InterceptorAgentRule.Version.ToString())))
			{
				TaskLogger.LogExit();
				return;
			}
			this.DataObject.Version = InterceptorAgentRule.Version.ToString();
			if (this.xmlNeedsUpdating)
			{
				this.DataObject.Xml = this.rule.ToXmlString();
			}
			if (base.Fields.IsModified("ExpireTime"))
			{
				this.DataObject.ExpireTimeUtc = this.ExpireTime.ToUniversalTime();
			}
			if (this.dags.Count > 0 || this.servers.Count > 0 || this.sites.Count > 0)
			{
				this.DataObject.Target = new MultiValuedProperty<ADObjectId>(this.servers.ConvertAll<ADObjectId>((Server s) => s.OriginalId).Concat(this.dags.ConvertAll<ADObjectId>((DatabaseAvailabilityGroup d) => d.OriginalId)).Concat(this.sites.ConvertAll<ADObjectId>((ADSite s) => s.OriginalId)));
			}
			else if (flag)
			{
				this.DataObject.Target = new MultiValuedProperty<ADObjectId>();
			}
			base.InternalProcessRecord();
		}

		// Token: 0x06006905 RID: 26885 RVA: 0x001B0998 File Offset: 0x001AEB98
		private void ResolveTargets()
		{
			if (base.Fields.IsModified("Server") && this.Server != null)
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
			if (base.Fields.IsModified("Dag") && this.Dag != null)
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

		// Token: 0x06006906 RID: 26886 RVA: 0x001B0BA8 File Offset: 0x001AEDA8
		private void SetAction(bool responseCodeModified, bool responseTextModified, bool actionModified, bool pathModified, bool timeIntervalModified)
		{
			if (responseCodeModified || responseTextModified || actionModified || timeIntervalModified || pathModified)
			{
				string customResponseCode = this.CustomResponseCode;
				bool flag = false;
				if (!responseCodeModified && !SmtpResponse.Empty.Equals(this.rule.Action.Response))
				{
					customResponseCode = this.rule.Action.Response.StatusCode;
					flag = true;
				}
				string customResponseText = this.CustomResponseString;
				bool flag2 = false;
				if (!responseTextModified && !SmtpResponse.Empty.Equals(this.rule.Action.Response))
				{
					customResponseText = this.rule.Action.Response.StatusText[0];
					flag2 = true;
				}
				TimeSpan timeInterval = timeIntervalModified ? this.TimeInterval : this.rule.Action.Delay;
				InterceptorAgentAction interceptorAgentAction;
				LocalizedString warning;
				LocalizedString localizedString;
				if (!InterceptorHelper.TryCreateAction(actionModified ? this.Action : this.rule.Action.Action, customResponseCode, responseCodeModified || flag, customResponseText, responseTextModified || flag2, timeInterval, pathModified ? this.Path : this.rule.Action.Path, out interceptorAgentAction, out warning, out localizedString))
				{
					base.WriteError(new LocalizedException(localizedString), ErrorCategory.InvalidData, actionModified ? this.Action : this.rule.Action.Action);
				}
				this.WriteWarningAndReset(warning);
				if (actionModified && InterceptorAgentAction.IsArchivingBehavior(interceptorAgentAction.Action))
				{
					this.WriteWarning(InterceptorHelper.GetArchivedItemRetentionMessage(interceptorAgentAction.Action, this.Identity.ToString(), this.Path, 14));
				}
				this.rule.Action = interceptorAgentAction;
			}
		}

		// Token: 0x06006907 RID: 26887 RVA: 0x001B0D52 File Offset: 0x001AEF52
		private LocalizedString WriteWarningAndReset(LocalizedString warning)
		{
			if (warning != LocalizedString.Empty)
			{
				this.WriteWarning(warning);
			}
			return LocalizedString.Empty;
		}

		// Token: 0x0400369A RID: 13978
		private bool xmlNeedsUpdating;

		// Token: 0x0400369B RID: 13979
		private List<Server> servers = new List<Server>();

		// Token: 0x0400369C RID: 13980
		private List<DatabaseAvailabilityGroup> dags = new List<DatabaseAvailabilityGroup>();

		// Token: 0x0400369D RID: 13981
		private List<ADSite> sites = new List<ADSite>();

		// Token: 0x0400369E RID: 13982
		private InterceptorAgentRule rule;
	}
}
