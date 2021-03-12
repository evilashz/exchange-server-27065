using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.FederationProvisioning;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000611 RID: 1553
	[Cmdlet("Set", "AuthConfig", DefaultParameterSetName = "AuthConfigSettings", SupportsShouldProcess = true)]
	public sealed class SetAuthConfig : SetSingletonSystemConfigurationObjectTask<AuthConfig>
	{
		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x060036F1 RID: 14065 RVA: 0x000E37D4 File Offset: 0x000E19D4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetAuthConfig;
			}
		}

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x060036F2 RID: 14066 RVA: 0x000E37DB File Offset: 0x000E19DB
		// (set) Token: 0x060036F3 RID: 14067 RVA: 0x000E37F2 File Offset: 0x000E19F2
		[Parameter(Mandatory = false, ParameterSetName = "AuthConfigSettings")]
		public string ServiceName
		{
			get
			{
				return (string)base.Fields[AuthConfigSchema.ServiceName];
			}
			set
			{
				base.Fields[AuthConfigSchema.ServiceName] = value;
			}
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x060036F4 RID: 14068 RVA: 0x000E3805 File Offset: 0x000E1A05
		// (set) Token: 0x060036F5 RID: 14069 RVA: 0x000E381C File Offset: 0x000E1A1C
		[Parameter(Mandatory = false, ParameterSetName = "AuthConfigSettings")]
		public string Realm
		{
			get
			{
				return (string)base.Fields[AuthConfigSchema.Realm];
			}
			set
			{
				base.Fields[AuthConfigSchema.Realm] = value;
			}
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x060036F6 RID: 14070 RVA: 0x000E382F File Offset: 0x000E1A2F
		// (set) Token: 0x060036F7 RID: 14071 RVA: 0x000E3846 File Offset: 0x000E1A46
		[Parameter(Mandatory = true, ParameterSetName = "CurrentCertificateParameter")]
		public string CertificateThumbprint
		{
			get
			{
				return (string)base.Fields[AuthConfigSchema.CurrentCertificateThumbprint];
			}
			set
			{
				base.Fields[AuthConfigSchema.CurrentCertificateThumbprint] = value;
			}
		}

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x060036F8 RID: 14072 RVA: 0x000E3859 File Offset: 0x000E1A59
		// (set) Token: 0x060036F9 RID: 14073 RVA: 0x000E3870 File Offset: 0x000E1A70
		[Parameter(Mandatory = false, ParameterSetName = "NewCertificateParameter")]
		public string NewCertificateThumbprint
		{
			get
			{
				return (string)base.Fields[AuthConfigSchema.NextCertificateThumbprint];
			}
			set
			{
				base.Fields[AuthConfigSchema.NextCertificateThumbprint] = value;
			}
		}

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x060036FA RID: 14074 RVA: 0x000E3883 File Offset: 0x000E1A83
		// (set) Token: 0x060036FB RID: 14075 RVA: 0x000E389A File Offset: 0x000E1A9A
		[Parameter(Mandatory = false, ParameterSetName = "NewCertificateParameter")]
		public DateTime? NewCertificateEffectiveDate
		{
			get
			{
				return (DateTime?)base.Fields[AuthConfigSchema.NextCertificateEffectiveDate];
			}
			set
			{
				base.Fields[AuthConfigSchema.NextCertificateEffectiveDate] = value;
			}
		}

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x060036FC RID: 14076 RVA: 0x000E38B2 File Offset: 0x000E1AB2
		// (set) Token: 0x060036FD RID: 14077 RVA: 0x000E38D8 File Offset: 0x000E1AD8
		[Parameter(Mandatory = false, ParameterSetName = "NewCertificateParameter")]
		[Parameter(Mandatory = false, ParameterSetName = "CurrentCertificateParameter")]
		public SwitchParameter SkipImmediateCertificateDeployment
		{
			get
			{
				return (SwitchParameter)(base.Fields["SkipImmediateCertificateDeployment"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SkipImmediateCertificateDeployment"] = value;
			}
		}

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x060036FE RID: 14078 RVA: 0x000E38F0 File Offset: 0x000E1AF0
		// (set) Token: 0x060036FF RID: 14079 RVA: 0x000E3907 File Offset: 0x000E1B07
		[Parameter(Mandatory = false, ParameterSetName = "CurrentCertificateParameter")]
		[Parameter(Mandatory = false, ParameterSetName = "NewCertificateParameter")]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x06003700 RID: 14080 RVA: 0x000E391A File Offset: 0x000E1B1A
		// (set) Token: 0x06003701 RID: 14081 RVA: 0x000E3940 File Offset: 0x000E1B40
		[Parameter(Mandatory = false, ParameterSetName = "CurrentCertificateParameter")]
		[Parameter(Mandatory = false, ParameterSetName = "NewCertificateParameter")]
		[Parameter(Mandatory = false, ParameterSetName = "PublishAuthCertificateParameter")]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x06003702 RID: 14082 RVA: 0x000E3958 File Offset: 0x000E1B58
		// (set) Token: 0x06003703 RID: 14083 RVA: 0x000E397E File Offset: 0x000E1B7E
		[Parameter(Mandatory = false, ParameterSetName = "PublishAuthCertificateParameter")]
		public SwitchParameter PublishCertificate
		{
			get
			{
				return (SwitchParameter)(base.Fields["PublishCertificate"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["PublishCertificate"] = value;
			}
		}

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x06003704 RID: 14084 RVA: 0x000E3996 File Offset: 0x000E1B96
		// (set) Token: 0x06003705 RID: 14085 RVA: 0x000E39BC File Offset: 0x000E1BBC
		[Parameter(Mandatory = false, ParameterSetName = "PublishAuthCertificateParameter")]
		public SwitchParameter ClearPreviousCertificate
		{
			get
			{
				return (SwitchParameter)(base.Fields["ClearPreviousCertificate"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ClearPreviousCertificate"] = value;
			}
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x000E39D4 File Offset: 0x000E1BD4
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			this.ValidateAndSetServerContext();
			string parameterSetName;
			if ((parameterSetName = base.ParameterSetName) != null)
			{
				if (parameterSetName == "NewCertificateParameter")
				{
					this.ValidateNewCertificateParameters();
					return;
				}
				if (parameterSetName == "AuthConfigSettings")
				{
					this.ValidateAuthConfigSettingsParameters();
					return;
				}
				if (parameterSetName == "CurrentCertificateParameter")
				{
					this.ValidateCurrentCertificateParameters();
					return;
				}
				if (parameterSetName == "PublishAuthCertificateParameter")
				{
					return;
				}
			}
			throw new NotSupportedException(base.ParameterSetName + "is not a supported parameter set");
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x000E3A64 File Offset: 0x000E1C64
		private void ValidateAndSetServerContext()
		{
			if (this.Server == null)
			{
				return;
			}
			this.serverObject = (Server)base.GetDataObject<Server>(this.Server, base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound((string)this.Server)), new LocalizedString?(Strings.ErrorServerNotUnique((string)this.Server)));
			if (!this.serverObject.IsE14OrLater)
			{
				base.WriteError(new ArgumentException(Strings.RemoteExchangeVersionNotSupported), ErrorCategory.InvalidArgument, null);
			}
			base.VerifyIsWithinScopes(DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromCustomScopeSet(base.ScopeSet, ADSystemConfigurationSession.GetRootOrgContainerId(base.DomainController, null), base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true), 283, "ValidateAndSetServerContext", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\OAuth\\SetAuthConfig.cs"), this.serverObject, false, new DataAccessTask<AuthConfig>.ADObjectOutOfScopeString(Strings.ErrorServerOutOfScope));
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x000E3B44 File Offset: 0x000E1D44
		protected override void InternalProcessRecord()
		{
			string parameterSetName;
			if ((parameterSetName = base.ParameterSetName) != null)
			{
				bool flag;
				if (!(parameterSetName == "NewCertificateParameter"))
				{
					if (!(parameterSetName == "AuthConfigSettings"))
					{
						if (!(parameterSetName == "CurrentCertificateParameter"))
						{
							if (!(parameterSetName == "PublishAuthCertificateParameter"))
							{
								goto IL_66;
							}
							flag = this.ProcessCertificatePublish();
						}
						else
						{
							flag = this.ProcessEmergencyCertificateUpdate();
						}
					}
					else
					{
						flag = this.ProcessAuthConfigSettings();
					}
				}
				else
				{
					flag = this.ProcessNormalCertificateUpdate();
				}
				if (flag)
				{
					base.InternalProcessRecord();
				}
				return;
			}
			IL_66:
			throw new NotSupportedException(base.ParameterSetName + "is not a supported parameter set");
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x000E3BD6 File Offset: 0x000E1DD6
		private void ValidateAuthConfigSettingsParameters()
		{
			if (base.Fields.IsModified(AuthConfigSchema.ServiceName) && string.IsNullOrEmpty(this.ServiceName))
			{
				base.WriteError(new TaskException(Strings.ErrorAuthServiceNameNotBlank), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x000E3C0C File Offset: 0x000E1E0C
		private void ValidateNewCertificateParameters()
		{
			if (string.IsNullOrEmpty(this.NewCertificateThumbprint))
			{
				if (this.NewCertificateEffectiveDate != null && this.NewCertificateEffectiveDate != null)
				{
					if (base.Fields.IsModified(AuthConfigSchema.NextCertificateThumbprint) || string.IsNullOrEmpty(this.DataObject.NextCertificateThumbprint))
					{
						base.WriteError(new TaskException(Strings.ErrorAuthNewCertificateNeeded), ErrorCategory.InvalidArgument, null);
					}
					this.ValidateNewEffectiveDate();
					return;
				}
			}
			else
			{
				this.ValidateNewEffectiveDate();
				this.NewCertificateThumbprint = FederationCertificate.UnifyThumbprintFormat(this.NewCertificateThumbprint);
				this.ValidateCertificate(this.NewCertificateThumbprint, this.NewCertificateEffectiveDate);
				if (!string.IsNullOrEmpty(this.DataObject.CurrentCertificateThumbprint) && string.Compare(this.NewCertificateThumbprint, this.DataObject.CurrentCertificateThumbprint, StringComparison.OrdinalIgnoreCase) == 0)
				{
					base.WriteError(new TaskException(Strings.ErrorAuthSameAsCurrent), ErrorCategory.InvalidArgument, null);
				}
				if (!string.IsNullOrEmpty(this.DataObject.PreviousCertificateThumbprint) && string.Compare(this.NewCertificateThumbprint, this.DataObject.PreviousCertificateThumbprint, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.WriteWarning(Strings.WarningCertificateSameAsPrevious(this.NewCertificateThumbprint));
				}
			}
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x000E3D2C File Offset: 0x000E1F2C
		private void ValidateNewEffectiveDate()
		{
			if (this.NewCertificateEffectiveDate == null || this.NewCertificateEffectiveDate == null)
			{
				base.WriteError(new TaskException(Strings.ErrorAuthSetNewEffectDate), ErrorCategory.InvalidArgument, null);
			}
			if (((ExDateTime)this.NewCertificateEffectiveDate.Value).ToUtc() < ExDateTime.UtcNow.AddMinutes(-5.0))
			{
				base.WriteError(new TaskException(Strings.ErrorAuthInvalidNewEffectiveDate), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x000E3DB8 File Offset: 0x000E1FB8
		private void ValidateCurrentCertificateParameters()
		{
			if (!string.IsNullOrEmpty(this.CertificateThumbprint))
			{
				this.CertificateThumbprint = FederationCertificate.UnifyThumbprintFormat(this.CertificateThumbprint);
				this.ValidateCertificate(this.CertificateThumbprint, null);
				if (!string.IsNullOrEmpty(this.DataObject.PreviousCertificateThumbprint) && string.Compare(this.CertificateThumbprint, this.DataObject.PreviousCertificateThumbprint, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.WriteWarning(Strings.WarningCertificateSameAsPrevious(this.CertificateThumbprint));
					return;
				}
			}
			else
			{
				base.WriteError(new TaskException(Strings.ErrorAuthCannotDeleteCurrent), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x000E3E48 File Offset: 0x000E2048
		private void ValidateCertificate(string thumbprint, DateTime? futurePublishDate)
		{
			if (this.DataObject.OrganizationId.OrganizationalUnit != null)
			{
				base.WriteError(new TaskException(Strings.ErrorSettingCertOnTenant), ErrorCategory.InvalidArgument, null);
			}
			bool skipAutomatedDeploymentChecks = OAuthTaskHelper.IsDatacenter() || this.Force;
			if (this.Server == null)
			{
				OAuthTaskHelper.ValidateLocalCertificate(thumbprint, futurePublishDate, skipAutomatedDeploymentChecks, new Task.TaskErrorLoggingDelegate(base.WriteError));
				return;
			}
			OAuthTaskHelper.ValidateRemoteCertificate((string)this.Server, thumbprint, futurePublishDate, skipAutomatedDeploymentChecks, new Task.TaskErrorLoggingDelegate(base.WriteError));
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x000E3ECC File Offset: 0x000E20CC
		private bool ProcessAuthConfigSettings()
		{
			if (base.Fields.IsModified(AuthConfigSchema.ServiceName))
			{
				this.DataObject.ServiceName = this.ServiceName;
			}
			if (base.Fields.IsModified(AuthConfigSchema.Realm))
			{
				this.DataObject.Realm = this.Realm;
			}
			return true;
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x000E3F20 File Offset: 0x000E2120
		private bool ProcessEmergencyCertificateUpdate()
		{
			if (!string.IsNullOrEmpty(this.DataObject.CurrentCertificateThumbprint) && string.Compare(this.DataObject.CurrentCertificateThumbprint, this.CertificateThumbprint, StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.DataObject.CurrentCertificateThumbprint = this.CertificateThumbprint;
				return true;
			}
			if (!string.IsNullOrEmpty(this.DataObject.NextCertificateThumbprint) && string.Compare(this.DataObject.NextCertificateThumbprint, this.CertificateThumbprint, StringComparison.OrdinalIgnoreCase) == 0)
			{
				base.WriteError(new TaskException(Strings.ErrorCertificateAlreadyinPublish(this.CertificateThumbprint)), ErrorCategory.InvalidArgument, null);
			}
			if (!OAuthTaskHelper.IsDatacenter() && !this.Force && !base.ShouldContinue(Strings.ConfirmationMessageAuthSettingOutage))
			{
				return false;
			}
			this.DataObject.PreviousCertificateThumbprint = this.DataObject.CurrentCertificateThumbprint;
			this.DataObject.CurrentCertificateThumbprint = this.CertificateThumbprint;
			this.TryPushCertificateInSameSite(this.CertificateThumbprint);
			return true;
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x000E4008 File Offset: 0x000E2208
		private bool ProcessNormalCertificateUpdate()
		{
			if (base.Fields.IsModified(AuthConfigSchema.NextCertificateThumbprint) && string.IsNullOrEmpty(this.NewCertificateThumbprint))
			{
				if (string.IsNullOrEmpty(this.DataObject.NextCertificateThumbprint))
				{
					return false;
				}
				if (!this.Force && !base.ShouldContinue(Strings.ConfirmationMessageAuthNewInProgress))
				{
					return false;
				}
				this.DataObject.NextCertificateThumbprint = null;
				this.DataObject.NextCertificateEffectiveDate = null;
				return true;
			}
			else
			{
				if (((ExDateTime)this.NewCertificateEffectiveDate.Value).ToUtc() < ExDateTime.UtcNow.AddHours(48.0) && !this.Force && !base.ShouldContinue(Strings.ConfirmationMessageAuthNewDateTooShort(48)))
				{
					return false;
				}
				if (string.IsNullOrEmpty(this.NewCertificateThumbprint))
				{
					this.DataObject.NextCertificateEffectiveDate = new DateTime?(this.NewCertificateEffectiveDate.Value.ToUniversalTime());
					return true;
				}
				if (!string.IsNullOrEmpty(this.DataObject.NextCertificateThumbprint))
				{
					if (string.Compare(this.DataObject.NextCertificateThumbprint, this.NewCertificateThumbprint, StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.DataObject.NextCertificateEffectiveDate = new DateTime?(this.NewCertificateEffectiveDate.Value.ToUniversalTime());
						return true;
					}
					if (!this.Force && !base.ShouldContinue(Strings.ConfirmationMessageAuthNewInProgressReplace(this.DataObject.NextCertificateThumbprint, this.NewCertificateThumbprint)))
					{
						return false;
					}
				}
				this.DataObject.NextCertificateThumbprint = this.NewCertificateThumbprint;
				this.DataObject.NextCertificateEffectiveDate = new DateTime?(this.NewCertificateEffectiveDate.Value.ToUniversalTime());
				this.TryPushCertificateInSameSite(this.NewCertificateThumbprint);
				return true;
			}
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x000E41DC File Offset: 0x000E23DC
		private bool ProcessCertificatePublish()
		{
			if (!this.PublishCertificate)
			{
				if (!this.ClearPreviousCertificate)
				{
					return false;
				}
				this.DataObject.PreviousCertificateThumbprint = string.Empty;
				return true;
			}
			else
			{
				if (string.IsNullOrEmpty(this.DataObject.NextCertificateThumbprint))
				{
					base.WriteError(new NoNextCertificateException(), ErrorCategory.InvalidArgument, null);
				}
				if (this.DataObject.NextCertificateEffectiveDate != null && this.DataObject.NextCertificateEffectiveDate != null && ((ExDateTime)this.DataObject.NextCertificateEffectiveDate.Value).ToUtc() > ExDateTime.UtcNow && !this.Force && !base.ShouldContinue(Strings.ConfirmationMessageAuthEffectiveDateNotReached(this.DataObject.NextCertificateThumbprint)))
				{
					return false;
				}
				this.DataObject.PreviousCertificateThumbprint = (this.ClearPreviousCertificate ? string.Empty : this.DataObject.CurrentCertificateThumbprint);
				this.DataObject.CurrentCertificateThumbprint = this.DataObject.NextCertificateThumbprint;
				this.DataObject.NextCertificateThumbprint = null;
				this.DataObject.NextCertificateEffectiveDate = null;
				return true;
			}
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x000E4318 File Offset: 0x000E2518
		private void TryPushCertificateInSameSite(string thumbprint)
		{
			if (this.SkipImmediateCertificateDeployment)
			{
				return;
			}
			if (!OAuthTaskHelper.IsDatacenter())
			{
				try
				{
					if (this.serverObject != null)
					{
						FederationCertificate.PushCertificate(this.serverObject, new Task.TaskProgressLoggingDelegate(base.WriteProgress), new Task.TaskWarningLoggingDelegate(this.WriteWarning), thumbprint);
					}
					else
					{
						FederationCertificate.PushCertificate(new Task.TaskProgressLoggingDelegate(base.WriteProgress), new Task.TaskWarningLoggingDelegate(this.WriteWarning), thumbprint);
					}
				}
				catch (InvalidOperationException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidArgument, null);
				}
				catch (LocalizedException exception2)
				{
					base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
				}
			}
		}

		// Token: 0x04002573 RID: 9587
		private Server serverObject;
	}
}
