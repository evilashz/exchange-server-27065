using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000038 RID: 56
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class InstallOrgCfgDataHandler : OrgCfgDataHandler
	{
		// Token: 0x06000246 RID: 582 RVA: 0x00009890 File Offset: 0x00007A90
		public InstallOrgCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, "Install-ExchangeOrganization", connection)
		{
			SetupLogger.TraceEnter(new object[0]);
			base.WorkUnit.Text = Strings.OrganizationInstallText;
			this.setPrepareSchema = false;
			this.setPrepareOrganization = false;
			this.setPrepareDomain = false;
			this.setPrepareSCT = false;
			this.setPrepareAllDomains = false;
			this.setDomain = null;
			this.setOrganizationName = null;
			SetupLogger.TraceExit();
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00009900 File Offset: 0x00007B00
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000990D File Offset: 0x00007B0D
		public IOrganizationName OrganizationName
		{
			get
			{
				return base.SetupContext.OrganizationName;
			}
			set
			{
				base.SetupContext.OrganizationName = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000991B File Offset: 0x00007B1B
		// (set) Token: 0x0600024A RID: 586 RVA: 0x00009928 File Offset: 0x00007B28
		public bool? ActiveDirectorySplitPermissions
		{
			get
			{
				return base.SetupContext.ActiveDirectorySplitPermissions;
			}
			set
			{
				base.SetupContext.ActiveDirectorySplitPermissions = value;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00009938 File Offset: 0x00007B38
		private void DetermineParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			this.setPrepareSchema = false;
			this.setPrepareOrganization = false;
			this.setPrepareDomain = false;
			this.setPrepareSCT = false;
			this.setPrepareAllDomains = false;
			this.setDomain = null;
			this.setOrganizationName = null;
			SetupLogger.Log(Strings.DeterminingOrgPrepParameters);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			if (base.SetupContext.ParsedArguments.ContainsKey("prepareschema"))
			{
				flag = true;
				SetupLogger.Log(Strings.CommandLineParameterSpecified("prepareschema"));
			}
			if (base.SetupContext.ParsedArguments.ContainsKey("preparead"))
			{
				flag2 = true;
				SetupLogger.Log(Strings.CommandLineParameterSpecified("preparead"));
			}
			if (base.SetupContext.ParsedArguments.ContainsKey("preparesct"))
			{
				flag3 = true;
				SetupLogger.Log(Strings.CommandLineParameterSpecified("preparesct"));
			}
			if (base.SetupContext.ParsedArguments.ContainsKey("preparedomain"))
			{
				flag4 = true;
				SetupLogger.Log(Strings.CommandLineParameterSpecified("preparedomain"));
			}
			if (base.SetupContext.ParsedArguments.ContainsKey("preparealldomains"))
			{
				flag5 = true;
				SetupLogger.Log(Strings.CommandLineParameterSpecified("preparealldomains"));
			}
			if (flag || flag2 || flag3 || flag4 || flag5)
			{
				if (flag)
				{
					this.setPrepareSchema = true;
					SetupLogger.Log(Strings.SettingArgumentBecauseOfCommandLineParameter("prepareschema", "PrepareSchema"));
				}
				if (flag2)
				{
					if (base.SetupContext.IsSchemaUpdateRequired)
					{
						this.setPrepareSchema = true;
						SetupLogger.Log(Strings.SettingArgumentBecauseItIsRequired("PrepareSchema"));
					}
					this.setPrepareOrganization = true;
					SetupLogger.Log(Strings.SettingArgumentBecauseOfCommandLineParameter("preparead", "PrepareOrganization"));
					this.setPrepareDomain = true;
					SetupLogger.Log(Strings.SettingArgumentBecauseOfCommandLineParameter("preparead", "PrepareDomain"));
				}
				if (flag3 && Datacenter.IsMicrosoftHostedOnly(true))
				{
					this.setPrepareSCT = true;
					SetupLogger.Log(Strings.SettingArgumentBecauseOfCommandLineParameter("preparesct", "PrepareSCT"));
					if (base.SetupContext.IsSchemaUpdateRequired)
					{
						this.setPrepareSchema = true;
						SetupLogger.Log(Strings.SettingArgumentBecauseItIsRequired("PrepareSchema"));
					}
					if (base.SetupContext.IsOrgConfigUpdateRequired)
					{
						this.setPrepareOrganization = true;
						SetupLogger.Log(Strings.SettingArgumentBecauseItIsRequired("PrepareOrganization"));
					}
					if (base.SetupContext.IsDomainConfigUpdateRequired)
					{
						this.setPrepareDomain = true;
						SetupLogger.Log(Strings.SettingArgumentBecauseItIsRequired("PrepareDomain"));
					}
				}
				if (flag5)
				{
					this.setPrepareAllDomains = true;
					SetupLogger.Log(Strings.SettingArgumentBecauseOfCommandLineParameter("preparealldomains", "PrepareAllDomains"));
				}
				if (flag4)
				{
					this.setPrepareDomain = true;
					SetupLogger.Log(Strings.SettingArgumentBecauseOfCommandLineParameter("preparedomain", "PrepareDomain"));
				}
			}
			else
			{
				if (base.SetupContext.IsSchemaUpdateRequired)
				{
					this.setPrepareSchema = true;
					SetupLogger.Log(Strings.SettingArgumentBecauseItIsRequired("PrepareSchema"));
				}
				if (base.SetupContext.IsOrgConfigUpdateRequired)
				{
					this.setPrepareOrganization = true;
					SetupLogger.Log(Strings.SettingArgumentBecauseItIsRequired("PrepareOrganization"));
					if (Datacenter.IsMicrosoftHostedOnly(true))
					{
						this.setPrepareSCT = true;
						SetupLogger.Log(Strings.SettingArgumentBecauseItIsRequired("PrepareSCT"));
					}
				}
				if (base.SetupContext.IsDomainConfigUpdateRequired)
				{
					this.setPrepareDomain = true;
					SetupLogger.Log(Strings.SettingArgumentBecauseItIsRequired("PrepareDomain"));
				}
			}
			if (this.OrganizationName != null)
			{
				this.setOrganizationName = this.OrganizationName.EscapedName;
				SetupLogger.Log(Strings.SettingArgumentToValue("OrganizationName", this.setOrganizationName));
			}
			if (base.SetupContext.ParsedArguments.ContainsKey("preparedomain"))
			{
				object obj = base.SetupContext.ParsedArguments["preparedomain"];
				this.setDomain = ((obj != null) ? obj.ToString() : null);
				SetupLogger.Log(Strings.SettingArgumentToValue("Domain", this.setDomain));
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00009CD8 File Offset: 0x00007ED8
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			this.DetermineParameters();
			if (this.setOrganizationName != null)
			{
				base.Parameters.AddWithValue("OrganizationName", this.setOrganizationName);
			}
			if (this.setPrepareSchema)
			{
				base.Parameters.AddWithValue("PrepareSchema", true);
			}
			if (this.setPrepareOrganization)
			{
				this.AddParametersForPrepareOrganization();
			}
			if (this.setPrepareAllDomains)
			{
				base.Parameters.AddWithValue("PrepareAllDomains", true);
			}
			if (this.setPrepareDomain)
			{
				base.Parameters.AddWithValue("PrepareDomain", true);
				if (this.setDomain != null)
				{
					base.Parameters.AddWithValue("Domain", this.setDomain);
				}
			}
			if (this.setPrepareSCT)
			{
				base.Parameters.AddWithValue("PrepareSCT", true);
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009DCC File Offset: 0x00007FCC
		private void AddParametersForPrepareOrganization()
		{
			base.Parameters.AddWithValue("PrepareOrganization", true);
			if (base.SetupContext.GlobalCustomerFeedbackEnabled != base.SetupContext.OriginalGlobalCustomerFeedbackEnabled)
			{
				base.Parameters.AddWithValue("CustomerFeedbackEnabled", base.SetupContext.GlobalCustomerFeedbackEnabled);
			}
			base.Parameters.AddWithValue("Industry", base.SetupContext.Industry);
			base.Parameters.AddWithValue("ActiveDirectorySplitPermissions", this.ActiveDirectorySplitPermissions);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009E90 File Offset: 0x00008090
		public override bool WillDataHandlerDoAnyWork()
		{
			bool result = false;
			this.DetermineParameters();
			if (this.setPrepareSchema || this.setPrepareOrganization || this.setPrepareAllDomains || this.setPrepareDomain || this.setPrepareSCT)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00009ED0 File Offset: 0x000080D0
		public override void UpdatePreCheckTaskDataHandler()
		{
			SetupLogger.TraceEnter(new object[0]);
			PrerequisiteAnalysisTaskDataHandler instance = PrerequisiteAnalysisTaskDataHandler.GetInstance(base.SetupContext, base.Connection);
			instance.AddRole("Global");
			this.DetermineParameters();
			if (this.setPrepareAllDomains)
			{
				instance.PrepareAllDomains = true;
			}
			if (this.setPrepareOrganization)
			{
				instance.PrepareOrganization = true;
				instance.ActiveDirectorySplitPermissions = this.ActiveDirectorySplitPermissions;
			}
			if (this.setPrepareSchema)
			{
				instance.PrepareSchema = true;
			}
			if (this.setPrepareDomain)
			{
				instance.PrepareDomain = true;
				if (this.setDomain != null)
				{
					instance.PrepareDomainTarget = this.setDomain;
				}
			}
			if (this.setPrepareSCT)
			{
				instance.PrepareSCT = true;
			}
			instance.CustomerFeedbackEnabled = base.SetupContext.GlobalCustomerFeedbackEnabled;
			SetupLogger.TraceExit();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00009F8C File Offset: 0x0000818C
		public override ValidationError[] ValidateConfiguration()
		{
			List<ValidationError> list = new List<ValidationError>(base.ValidateConfiguration());
			if (!base.SetupContext.ExchangeOrganizationExists && (base.SetupContext.ParsedArguments.ContainsKey("preparead") || base.SetupContext.HasRolesToInstall))
			{
				if (this.OrganizationName == null)
				{
					list.Add(new SetupValidationError(Strings.SpecifyExchangeOrganizationName));
				}
				else
				{
					string escapedName = this.OrganizationName.EscapedName;
					try
					{
						new NewOrganizationName(escapedName);
					}
					catch (FormatException)
					{
						list.Add(new SetupValidationError(Strings.InvalidOrganizationName(escapedName)));
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x04000099 RID: 153
		private const string prepareSchemaArgument = "PrepareSchema";

		// Token: 0x0400009A RID: 154
		private const string prepareOrganizationArgument = "PrepareOrganization";

		// Token: 0x0400009B RID: 155
		private const string customerFeedbackEnabledArgument = "CustomerFeedbackEnabled";

		// Token: 0x0400009C RID: 156
		private const string industryArgument = "Industry";

		// Token: 0x0400009D RID: 157
		private const string prepareDomainArgument = "PrepareDomain";

		// Token: 0x0400009E RID: 158
		private const string prepareSCTArgument = "PrepareSCT";

		// Token: 0x0400009F RID: 159
		private const string prepareAllDomainsArgument = "PrepareAllDomains";

		// Token: 0x040000A0 RID: 160
		private const string domainArgument = "Domain";

		// Token: 0x040000A1 RID: 161
		private const string organizationNameArgument = "OrganizationName";

		// Token: 0x040000A2 RID: 162
		private const string activeDirectorySplitPermissionsArgument = "ActiveDirectorySplitPermissions";

		// Token: 0x040000A3 RID: 163
		private bool setPrepareSchema;

		// Token: 0x040000A4 RID: 164
		private bool setPrepareOrganization;

		// Token: 0x040000A5 RID: 165
		private bool setPrepareDomain;

		// Token: 0x040000A6 RID: 166
		private bool setPrepareSCT;

		// Token: 0x040000A7 RID: 167
		private bool setPrepareAllDomains;

		// Token: 0x040000A8 RID: 168
		private string setDomain;

		// Token: 0x040000A9 RID: 169
		private string setOrganizationName;
	}
}
