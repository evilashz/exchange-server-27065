using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ADServerSettings
{
	// Token: 0x0200005C RID: 92
	[Cmdlet("Set", "AdServerSettings", DefaultParameterSetName = "FullParams", SupportsShouldProcess = true)]
	public sealed class SetAdServerSettings : Task
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000A060 File Offset: 0x00008260
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000A077 File Offset: 0x00008277
		[Parameter(Mandatory = false, Position = 0, ParameterSetName = "SingleDC")]
		public Fqdn PreferredServer
		{
			get
			{
				return (Fqdn)base.Fields["PreferredServer"];
			}
			set
			{
				base.Fields["PreferredServer"] = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A08A File Offset: 0x0000828A
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000A0A1 File Offset: 0x000082A1
		[Parameter(Mandatory = false, ParameterSetName = "FullParams")]
		public Fqdn ConfigurationDomainController
		{
			get
			{
				return (Fqdn)base.Fields["ConfigurationDomainController"];
			}
			set
			{
				base.Fields["ConfigurationDomainController"] = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000A0B4 File Offset: 0x000082B4
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000A0CB File Offset: 0x000082CB
		[Parameter(Mandatory = false, ParameterSetName = "FullParams")]
		public Fqdn PreferredGlobalCatalog
		{
			get
			{
				return (Fqdn)base.Fields["PreferredGlobalCatalog"];
			}
			set
			{
				base.Fields["PreferredGlobalCatalog"] = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000A0DE File Offset: 0x000082DE
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000A0F5 File Offset: 0x000082F5
		[Parameter(Mandatory = false, ParameterSetName = "FullParams")]
		public MultiValuedProperty<Fqdn> SetPreferredDomainControllers
		{
			get
			{
				return (MultiValuedProperty<Fqdn>)base.Fields["SetPreferredDomainControllers"];
			}
			set
			{
				base.Fields["SetPreferredDomainControllers"] = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000A108 File Offset: 0x00008308
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000A11F File Offset: 0x0000831F
		[Parameter(Mandatory = false, ParameterSetName = "SingleDC")]
		[Parameter(Mandatory = false, ParameterSetName = "FullParams")]
		public string RecipientViewRoot
		{
			get
			{
				return (string)base.Fields["RecipientViewRoot"];
			}
			set
			{
				base.Fields["RecipientViewRoot"] = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000A132 File Offset: 0x00008332
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000A153 File Offset: 0x00008353
		[Parameter(Mandatory = false, ParameterSetName = "FullParams")]
		[Parameter(Mandatory = false, ParameterSetName = "SingleDC")]
		public bool ViewEntireForest
		{
			get
			{
				return (bool)(base.Fields["ViewEntireForest"] ?? false);
			}
			set
			{
				base.Fields["ViewEntireForest"] = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000A16B File Offset: 0x0000836B
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000A18C File Offset: 0x0000838C
		[Parameter(Mandatory = false)]
		public bool WriteOriginatingChangeTimestamp
		{
			get
			{
				return (bool)(base.Fields["WriteOriginatingChangeTimestamp"] ?? false);
			}
			set
			{
				base.Fields["WriteOriginatingChangeTimestamp"] = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000A1A4 File Offset: 0x000083A4
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000A1C5 File Offset: 0x000083C5
		[Parameter(Mandatory = false)]
		public bool WriteShadowProperties
		{
			get
			{
				return (bool)(base.Fields["WriteShadowProperties"] ?? false);
			}
			set
			{
				base.Fields["WriteShadowProperties"] = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000A1DD File Offset: 0x000083DD
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000A1F4 File Offset: 0x000083F4
		[Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Instance")]
		public RunspaceServerSettingsPresentationObject RunspaceServerSettings
		{
			get
			{
				return (RunspaceServerSettingsPresentationObject)base.Fields["Instance"];
			}
			set
			{
				base.Fields["Instance"] = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000A207 File Offset: 0x00008407
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000A21E File Offset: 0x0000841E
		[Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = "Gls")]
		public bool DisableGls
		{
			get
			{
				return (bool)base.Fields["DisableGls"];
			}
			set
			{
				base.Fields["DisableGls"] = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000A236 File Offset: 0x00008436
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000A24D File Offset: 0x0000844D
		[Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = "ForceADInTemplateScope")]
		public bool ForceADInTemplateScope
		{
			get
			{
				return (bool)base.Fields["ForceADInTemplateScope"];
			}
			set
			{
				base.Fields["ForceADInTemplateScope"] = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000A265 File Offset: 0x00008465
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000A28B File Offset: 0x0000848B
		[Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = "Reset")]
		public SwitchParameter ResetSettings
		{
			get
			{
				return (SwitchParameter)(base.Fields["Reset"] ?? false);
			}
			set
			{
				base.Fields["Reset"] = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000A2A3 File Offset: 0x000084A3
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.ResetSettings)
				{
					return Strings.ConfirmationMessageResetADServerSettings;
				}
				return Strings.ConfirmationMessageSetADServerSettings;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000A2C0 File Offset: 0x000084C0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (!(base.ServerSettings is RunspaceServerSettings))
			{
				this.WriteWarning(Strings.WarningSettingsNotModifiable);
				this.settingsNotModifiable = true;
				return;
			}
			if (base.ServerSettings == null && (this.RunspaceServerSettings == null || this.RunspaceServerSettings.RawServerSettings == null))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorRunspaceServerSettingsNotFound), ErrorCategory.InvalidOperation, this);
			}
			if (base.Fields.IsModified("Instance"))
			{
				this.modifiedServerSettings = ((this.RunspaceServerSettings.RawServerSettings != null) ? this.RunspaceServerSettings.RawServerSettings : ((RunspaceServerSettings)base.ServerSettings.Clone()));
				this.ConfigurationDomainController = this.RunspaceServerSettings.UserPreferredConfigurationDomainController;
				this.PreferredGlobalCatalog = this.RunspaceServerSettings.UserPreferredGlobalCatalog;
				this.RecipientViewRoot = this.RunspaceServerSettings.RecipientViewRoot;
				this.SetPreferredDomainControllers = this.RunspaceServerSettings.UserPreferredDomainControllers;
				this.ViewEntireForest = this.RunspaceServerSettings.ViewEntireForest;
				this.WriteOriginatingChangeTimestamp = this.RunspaceServerSettings.WriteOriginatingChangeTimestamp;
				this.WriteShadowProperties = this.RunspaceServerSettings.WriteShadowProperties;
			}
			else
			{
				this.modifiedServerSettings = (RunspaceServerSettings)base.ServerSettings.Clone();
			}
			if (base.Fields.IsModified("ViewEntireForest"))
			{
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.VerboseSettingViewEntireForest(this.ViewEntireForest.ToString()));
				}
				this.modifiedServerSettings.ViewEntireForest = this.ViewEntireForest;
				if (this.ViewEntireForest)
				{
					if (!string.IsNullOrEmpty(this.RecipientViewRoot))
					{
						base.WriteError(new ArgumentException(Strings.ErrorSetRecipientViewRootAndViewEntireForestToTrue), ErrorCategory.InvalidArgument, this);
					}
				}
				else if (!string.IsNullOrEmpty(this.RecipientViewRoot))
				{
					this.VerifyAndSetRecipientViewRoot(this.RecipientViewRoot);
				}
				else if (base.Fields.IsModified("RecipientViewRoot"))
				{
					base.WriteError(new ArgumentException(Strings.ErrorRecipientViewRootEmptyAndViewEntireForestToFalse), ErrorCategory.InvalidArgument, this);
				}
				else if (base.ScopeSet.RecipientReadScope != null && base.ScopeSet.RecipientReadScope.Root != null)
				{
					this.modifiedServerSettings.RecipientViewRoot = base.ScopeSet.RecipientReadScope.Root;
				}
			}
			else if (base.Fields.IsModified("RecipientViewRoot"))
			{
				if (!string.IsNullOrEmpty(this.RecipientViewRoot))
				{
					if (base.IsVerboseOn)
					{
						base.WriteVerbose(Strings.VerboseSettingViewEntireForest(false.ToString()));
					}
					this.modifiedServerSettings.ViewEntireForest = false;
					this.VerifyAndSetRecipientViewRoot(this.RecipientViewRoot);
				}
				else
				{
					if (base.IsVerboseOn)
					{
						base.WriteVerbose(Strings.VerboseSettingViewEntireForest(true.ToString()));
					}
					this.modifiedServerSettings.ViewEntireForest = true;
				}
			}
			if (this.PreferredServer != null)
			{
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.VerboseSettingPreferredServer("PreferredGlobalCatalog", this.PreferredServer));
				}
				this.modifiedServerSettings.SetUserPreferredGlobalCatalog(this.PreferredServer);
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.VerboseSettingPreferredServer("ConfigurationDomainController", this.PreferredServer));
				}
				this.modifiedServerSettings.SetUserConfigurationDomainController(this.PreferredServer);
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.VerboseSettingPreferredServer("SetPreferredDomainControllers", this.PreferredServer));
				}
				ADObjectId adobjectId;
				Fqdn fqdn;
				this.modifiedServerSettings.AddOrReplaceUserPreferredDC(this.PreferredServer, out adobjectId, out fqdn);
			}
			if (base.Fields.IsModified("PreferredGlobalCatalog"))
			{
				if (this.PreferredGlobalCatalog == null)
				{
					if (base.IsVerboseOn)
					{
						base.WriteVerbose(Strings.VerboseClearPreferredServer("PreferredGlobalCatalog"));
					}
					this.modifiedServerSettings.ClearUserPreferredGlobalCatalog();
				}
				else if (!string.Equals(this.PreferredGlobalCatalog, this.modifiedServerSettings.UserPreferredGlobalCatalog, StringComparison.OrdinalIgnoreCase))
				{
					if (base.IsVerboseOn)
					{
						base.WriteVerbose(Strings.VerboseSettingPreferredServer("PreferredGlobalCatalog", this.PreferredGlobalCatalog));
					}
					this.modifiedServerSettings.SetUserPreferredGlobalCatalog(this.PreferredGlobalCatalog);
				}
			}
			if (base.Fields.IsModified("ConfigurationDomainController"))
			{
				if (this.ConfigurationDomainController == null)
				{
					if (base.IsVerboseOn)
					{
						base.WriteVerbose(Strings.VerboseClearPreferredServer("ConfigurationDomainController"));
					}
					this.modifiedServerSettings.ClearUserConfigurationDomainController();
				}
				else if (!string.Equals(this.ConfigurationDomainController, this.modifiedServerSettings.UserConfigurationDomainController, StringComparison.OrdinalIgnoreCase))
				{
					if (base.IsVerboseOn)
					{
						base.WriteVerbose(Strings.VerboseSettingPreferredServer("ConfigurationDomainController", this.ConfigurationDomainController));
					}
					this.modifiedServerSettings.SetUserConfigurationDomainController(this.ConfigurationDomainController);
				}
			}
			if (base.Fields.IsModified("SetPreferredDomainControllers"))
			{
				if (this.SetPreferredDomainControllers == null || this.SetPreferredDomainControllers.Count == 0)
				{
					if (base.IsVerboseOn)
					{
						base.WriteVerbose(Strings.VerboseClearAllPreferredDC);
					}
					this.modifiedServerSettings.ClearAllUserPreferredDCs();
				}
				else
				{
					foreach (Fqdn fqdn2 in this.SetPreferredDomainControllers)
					{
						if (!this.modifiedServerSettings.UserPreferredDomainControllers.Contains(fqdn2))
						{
							if (base.IsVerboseOn)
							{
								base.WriteVerbose(Strings.VerboseSettingPreferredServer("SetPreferredDomainControllers", fqdn2));
							}
							ADObjectId adobjectId2 = null;
							Fqdn fqdn3 = null;
							this.modifiedServerSettings.AddOrReplaceUserPreferredDC(fqdn2, out adobjectId2, out fqdn3);
							if (fqdn3 != null)
							{
								this.WriteWarning(Strings.WarningPreferredServerReplaced(fqdn2, fqdn3, adobjectId2.ToCanonicalName()));
							}
						}
					}
				}
			}
			if (base.Fields.IsModified("WriteOriginatingChangeTimestamp"))
			{
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.VerboseSettingWriteOriginatingChangeTimestamp(this.WriteOriginatingChangeTimestamp.ToString()));
				}
				this.modifiedServerSettings.WriteOriginatingChangeTimestamp = this.WriteOriginatingChangeTimestamp;
			}
			if (base.Fields.IsModified("WriteShadowProperties"))
			{
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.VerboseSettingWriteShadowProperties(this.WriteOriginatingChangeTimestamp.ToString()));
				}
				this.modifiedServerSettings.WriteShadowProperties = this.WriteShadowProperties;
			}
			if (base.Fields.IsModified("DisableGls"))
			{
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.VerboseSettingDisableGls(this.DisableGls.ToString()));
				}
				this.modifiedServerSettings.DisableGls = this.DisableGls;
			}
			if (base.Fields.IsModified("ForceADInTemplateScope"))
			{
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.VerboseSettingDisableAggregation(this.ForceADInTemplateScope.ToString()));
				}
				this.modifiedServerSettings.ForceADInTemplateScope = this.ForceADInTemplateScope;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000A984 File Offset: 0x00008B84
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			if (!this.settingsNotModifiable)
			{
				bool flag = true;
				RunspaceServerSettings runspaceServerSettings = (RunspaceServerSettings)base.ServerSettings;
				if (runspaceServerSettings != null)
				{
					flag = !runspaceServerSettings.Equals(this.modifiedServerSettings);
				}
				if (flag)
				{
					if (base.IsVerboseOn)
					{
						base.WriteVerbose(Strings.VerboseSaveADServerSettings);
					}
					ExchangePropertyContainer.SetServerSettings(base.SessionState, this.modifiedServerSettings);
					if (base.IsVerboseOn)
					{
						base.WriteVerbose(Strings.VerboseSaveADServerSettingsSucceed);
					}
				}
				else
				{
					this.WriteWarning(Strings.WarningADServerSettingsUnchanged);
				}
			}
			if (this.ResetSettings)
			{
				if (ExchangePropertyContainer.IsContainerInitialized(base.SessionState))
				{
					ExchangePropertyContainer.SetServerSettings(base.SessionState, null);
				}
				base.SessionState.Variables[ExchangePropertyContainer.ADServerSettingsVarName] = null;
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.VerboseResetADServerSettingsSucceed);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000AA60 File Offset: 0x00008C60
		protected override bool IsKnownException(Exception e)
		{
			return base.IsKnownException(e) || DataAccessHelper.IsDataAccessKnownException(e);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000AA74 File Offset: 0x00008C74
		private void VerifyAndSetRecipientViewRoot(string root)
		{
			if (object.Equals(root, this.modifiedServerSettings.RecipientViewRoot))
			{
				return;
			}
			OrganizationalUnitIdParameter organizationalUnitIdParameter = null;
			try
			{
				organizationalUnitIdParameter = OrganizationalUnitIdParameter.Parse(root);
			}
			catch (ArgumentException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, this);
			}
			if (this.configSession == null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				this.configSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 588, "VerifyAndSetRecipientViewRoot", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ADServerSettings\\SetADServerSettings.cs");
				this.configSession.UseGlobalCatalog = true;
				this.configSession.UseConfigNC = false;
			}
			ADObjectId adobjectId = null;
			if (base.ScopeSet.RecipientReadScope != null && base.ScopeSet.RecipientReadScope.Root != null)
			{
				adobjectId = base.ScopeSet.RecipientReadScope.Root;
			}
			ExchangeOrganizationalUnit exchangeOrganizationalUnit = null;
			if (base.IsVerboseOn)
			{
				base.WriteVerbose(Strings.VerboseVerifyingRecipientViewRoot(root));
			}
			IEnumerable<ExchangeOrganizationalUnit> objects = organizationalUnitIdParameter.GetObjects<ExchangeOrganizationalUnit>(adobjectId, this.configSession);
			using (IEnumerator<ExchangeOrganizationalUnit> enumerator = objects.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					exchangeOrganizationalUnit = enumerator.Current;
					if (enumerator.MoveNext())
					{
						if (adobjectId != null)
						{
							base.WriteError(new ManagementObjectAmbiguousException(Strings.ErrorRecipientViewRootDuplicatedUnderScope(root, adobjectId.ToString())), ErrorCategory.InvalidArgument, this);
						}
						else
						{
							base.WriteError(new ManagementObjectAmbiguousException(Strings.ErrorRecipientViewRootDuplicated(root)), ErrorCategory.InvalidArgument, this);
						}
					}
				}
				else if (adobjectId != null)
				{
					base.WriteError(new ArgumentException(Strings.ErrorRecipientViewRootNotFoundUnderScope(root, adobjectId.ToString())), ErrorCategory.InvalidArgument, this);
				}
				else
				{
					base.WriteError(new ArgumentException(Strings.ErrorRecipientViewRootNotFound(root)), ErrorCategory.InvalidArgument, this);
				}
			}
			this.modifiedServerSettings.RecipientViewRoot = exchangeOrganizationalUnit.Id;
			if (base.IsVerboseOn)
			{
				base.WriteVerbose(Strings.VerboseVerifyRecipientViewRootSucceed);
			}
		}

		// Token: 0x0400013F RID: 319
		private const string ParameterSetSingleDC = "SingleDC";

		// Token: 0x04000140 RID: 320
		private const string ParameterSetFullParams = "FullParams";

		// Token: 0x04000141 RID: 321
		private const string ParameterSetInstance = "Instance";

		// Token: 0x04000142 RID: 322
		private const string ParameterSetGls = "Gls";

		// Token: 0x04000143 RID: 323
		private const string ParameterSetForceADInTemplateScope = "ForceADInTemplateScope";

		// Token: 0x04000144 RID: 324
		private const string ParameterSetReset = "Reset";

		// Token: 0x04000145 RID: 325
		private const string paramPreferredServer = "PreferredServer";

		// Token: 0x04000146 RID: 326
		private const string paramConfigurationDomainController = "ConfigurationDomainController";

		// Token: 0x04000147 RID: 327
		private const string paramPreferredGlobalCatalog = "PreferredGlobalCatalog";

		// Token: 0x04000148 RID: 328
		private const string paramSetPreferredDomainControllers = "SetPreferredDomainControllers";

		// Token: 0x04000149 RID: 329
		private const string paramRecipientViewRoot = "RecipientViewRoot";

		// Token: 0x0400014A RID: 330
		private const string paramViewEntireForest = "ViewEntireForest";

		// Token: 0x0400014B RID: 331
		private const string paramWriteOriginatingChangeTimestamp = "WriteOriginatingChangeTimestamp";

		// Token: 0x0400014C RID: 332
		private const string paramWriteShadowProperties = "WriteShadowProperties";

		// Token: 0x0400014D RID: 333
		private const string paramInstance = "Instance";

		// Token: 0x0400014E RID: 334
		private const string paramDisableGls = "DisableGls";

		// Token: 0x0400014F RID: 335
		private const string paramForceADInTemplateScope = "ForceADInTemplateScope";

		// Token: 0x04000150 RID: 336
		private const string paramReset = "Reset";

		// Token: 0x04000151 RID: 337
		private RunspaceServerSettings modifiedServerSettings;

		// Token: 0x04000152 RID: 338
		private IConfigurationSession configSession;

		// Token: 0x04000153 RID: 339
		private bool settingsNotModifiable;
	}
}
