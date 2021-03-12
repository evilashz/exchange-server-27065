using System;
using System.Configuration;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000096 RID: 150
	public abstract class NewAuditLogSearchBase<TDataObject> : NewTenantADTaskBase<TDataObject> where TDataObject : AuditLogSearchBase, new()
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0001443B File Offset: 0x0001263B
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x00014452 File Offset: 0x00012652
		[Parameter(Mandatory = false, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00014465 File Offset: 0x00012665
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x0001447C File Offset: 0x0001267C
		[Parameter(Mandatory = false)]
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

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0001448F File Offset: 0x0001268F
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x000144A6 File Offset: 0x000126A6
		[Parameter(Mandatory = true)]
		public ExDateTime StartDate
		{
			get
			{
				return (ExDateTime)base.Fields["StartDate"];
			}
			set
			{
				base.Fields["StartDate"] = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x000144BE File Offset: 0x000126BE
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x000144D5 File Offset: 0x000126D5
		[Parameter(Mandatory = true)]
		public ExDateTime EndDate
		{
			get
			{
				return (ExDateTime)base.Fields["EndDate"];
			}
			set
			{
				base.Fields["EndDate"] = value;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x000144ED File Offset: 0x000126ED
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x00014504 File Offset: 0x00012704
		[Parameter(Mandatory = false)]
		public bool? ExternalAccess
		{
			get
			{
				return (bool?)base.Fields["ExternalAccess"];
			}
			set
			{
				base.Fields["ExternalAccess"] = value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0001451C File Offset: 0x0001271C
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00014533 File Offset: 0x00012733
		[Parameter(Mandatory = true)]
		public MultiValuedProperty<SmtpAddress> StatusMailRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)base.Fields["StatusMailRecipients"];
			}
			set
			{
				base.Fields["StatusMailRecipients"] = value;
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00014548 File Offset: 0x00012748
		protected override void InternalValidate()
		{
			if (this.StartDate >= this.EndDate)
			{
				base.WriteError(new ArgumentException(Strings.InvalidTimeRange, "StartDate"), ErrorCategory.InvalidArgument, null);
			}
			foreach (SmtpAddress smtpAddress in this.StatusMailRecipients)
			{
				if (!smtpAddress.IsValidAddress)
				{
					base.WriteError(new ArgumentException(Strings.InvalidSmtpAddressOrAlias(smtpAddress.ToString())), ErrorCategory.InvalidArgument, null);
				}
			}
			base.InternalValidate();
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000145F8 File Offset: 0x000127F8
		protected override IConfigurable PrepareDataObject()
		{
			TDataObject dataObject = this.DataObject;
			dataObject.OrganizationId = base.CurrentOrganizationId;
			TDataObject dataObject2 = this.DataObject;
			dataObject2.SetId(new AuditLogSearchId(Guid.NewGuid()));
			if (string.IsNullOrEmpty(this.Name))
			{
				TDataObject dataObject3 = this.DataObject;
				dataObject3.Name = string.Format(CultureInfo.InvariantCulture, "{0}{1:yyyyMMdd}{{{2}}}", new object[]
				{
					Strings.AuditLogSearchNamePrefix,
					DateTime.UtcNow,
					Guid.NewGuid()
				});
			}
			else
			{
				TDataObject dataObject4 = this.DataObject;
				dataObject4.Name = this.Name;
			}
			TDataObject dataObject5 = this.DataObject;
			ADObjectId adobjectId;
			dataObject5.CreatedByEx = (base.TryGetExecutingUserId(out adobjectId) ? adobjectId : NewAuditLogSearchBase<TDataObject>.dummyUserADId);
			TDataObject dataObject6 = this.DataObject;
			dataObject6.CreatedBy = this.GetExecutingUserDisplayName();
			TDataObject dataObject7 = this.DataObject;
			dataObject7.StatusMailRecipients = this.StatusMailRecipients;
			TDataObject dataObject8 = this.DataObject;
			dataObject8.ExternalAccess = this.ExternalAccess;
			if (!this.StartDate.HasTimeZone)
			{
				ExDateTime exDateTime = ExDateTime.Create(ExTimeZone.CurrentTimeZone, this.StartDate.UniversalTime)[0];
				TDataObject dataObject9 = this.DataObject;
				dataObject9.StartDateUtc = new DateTime?(exDateTime.UniversalTime);
			}
			else
			{
				TDataObject dataObject10 = this.DataObject;
				dataObject10.StartDateUtc = new DateTime?(this.StartDate.UniversalTime);
			}
			if (!this.EndDate.HasTimeZone)
			{
				ExDateTime exDateTime2 = ExDateTime.Create(ExTimeZone.CurrentTimeZone, this.EndDate.UniversalTime)[0];
				TDataObject dataObject11 = this.DataObject;
				dataObject11.EndDateUtc = new DateTime?(exDateTime2.UniversalTime);
			}
			else
			{
				TDataObject dataObject12 = this.DataObject;
				dataObject12.EndDateUtc = new DateTime?(this.EndDate.UniversalTime);
			}
			return this.DataObject;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00014840 File Offset: 0x00012A40
		protected override void InternalProcessRecord()
		{
			try
			{
				base.InternalProcessRecord();
			}
			catch (LocalizedException)
			{
				base.WriteError(new FailedToCreateAuditLogSearchException(), ErrorCategory.ResourceUnavailable, null);
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00014878 File Offset: 0x00012A78
		private string GetExecutingUserDisplayName()
		{
			ADObjectId adobjectId;
			if (base.TryGetExecutingUserId(out adobjectId))
			{
				return adobjectId.ToString();
			}
			if (base.ExchangeRunspaceConfig != null && !string.IsNullOrEmpty(base.ExchangeRunspaceConfig.ExecutingUserDisplayName))
			{
				return base.ExchangeRunspaceConfig.ExecutingUserDisplayName;
			}
			if (base.ExchangeRunspaceConfig != null && !string.IsNullOrEmpty(base.ExchangeRunspaceConfig.IdentityName))
			{
				return base.ExchangeRunspaceConfig.IdentityName;
			}
			return string.Empty;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000148E8 File Offset: 0x00012AE8
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 255, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\AuditLogSearch\\NewAuditLogSearch.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				return adorganizationalUnit.OrganizationId;
			}
			return base.CurrentOrganizationId ?? base.ExecutingUserOrganizationId;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000149A0 File Offset: 0x00012BA0
		protected override IConfigDataProvider CreateSession()
		{
			OrganizationId organizationId = this.ResolveCurrentOrganization();
			ADUser tenantArbitrationMailbox;
			try
			{
				tenantArbitrationMailbox = AdminAuditLogHelper.GetTenantArbitrationMailbox(organizationId);
			}
			catch (ObjectNotFoundException innerException)
			{
				TaskLogger.Trace("ObjectNotFoundException occurred when getting Exchange principal from the arbitration mailbox.", new object[0]);
				throw new AuditLogSearchArbitrationMailboxNotFoundException(organizationId.ToString(), innerException);
			}
			catch (NonUniqueRecipientException innerException2)
			{
				TaskLogger.Trace("More than one tenant arbitration mailbox found for the current organization.", new object[0]);
				throw new AuditLogSearchNonUniqueArbitrationMailboxFoundException(organizationId.ToString(), innerException2);
			}
			ExchangePrincipal principal = ExchangePrincipal.FromADUser(ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), tenantArbitrationMailbox, RemotingOptions.AllowCrossSite);
			ADSessionSettings sessionSettings = base.CurrentOrganizationId.ToADSessionSettings();
			this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.FullyConsistent, sessionSettings, 310, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\AuditLogSearch\\NewAuditLogSearch.cs");
			return this.InternalCreateSearchDataProvider(principal, organizationId);
		}

		// Token: 0x060004E8 RID: 1256
		internal abstract IConfigDataProvider InternalCreateSearchDataProvider(ExchangePrincipal principal, OrganizationId organizationId);

		// Token: 0x060004E9 RID: 1257 RVA: 0x00014A60 File Offset: 0x00012C60
		protected override void InternalEndProcessing()
		{
			this.DisposeSession();
			base.InternalEndProcessing();
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00014A6E File Offset: 0x00012C6E
		protected override void InternalStopProcessing()
		{
			this.DisposeSession();
			base.InternalStopProcessing();
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00014A7C File Offset: 0x00012C7C
		protected override void InternalStateReset()
		{
			this.DisposeSession();
			base.InternalStateReset();
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00014A8A File Offset: 0x00012C8A
		private void DisposeSession()
		{
			if (base.DataSession is IDisposable)
			{
				((IDisposable)base.DataSession).Dispose();
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00014AAC File Offset: 0x00012CAC
		internal static MultiValuedProperty<string> GetMultiValuedSmptAddressAsStrings(MultiValuedProperty<SmtpAddress> values)
		{
			string[] array = new string[values.Count];
			for (int i = 0; i < values.Count; i++)
			{
				array[i] = values[i].ToString();
			}
			return new MultiValuedProperty<string>(array);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00014AF4 File Offset: 0x00012CF4
		internal static MultiValuedProperty<SmtpAddress> GetMultiValuedStringsAsSmptAddresses(MultiValuedProperty<string> values)
		{
			SmtpAddress[] array = new SmtpAddress[values.Count];
			for (int i = 0; i < values.Count; i++)
			{
				array[i] = new SmtpAddress(values[i]);
			}
			return new MultiValuedProperty<SmtpAddress>(array);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00014B3C File Offset: 0x00012D3C
		internal static int? ReadIntegerAppSetting(string appSettingKey)
		{
			int? result;
			try
			{
				AppSettingsReader appSettingsReader = new AppSettingsReader();
				result = new int?((int)appSettingsReader.GetValue(appSettingKey, typeof(int)));
			}
			catch (InvalidOperationException)
			{
				TaskLogger.Trace("Failed to read {0}, either it does not exist, or it is misconfigured.", new object[]
				{
					appSettingKey
				});
				result = null;
			}
			return result;
		}

		// Token: 0x0400026F RID: 623
		private static ADObjectId dummyUserADId = new ADObjectId();

		// Token: 0x04000270 RID: 624
		internal IRecipientSession recipientSession;
	}
}
