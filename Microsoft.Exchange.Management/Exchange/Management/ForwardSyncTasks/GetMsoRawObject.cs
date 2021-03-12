using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000351 RID: 849
	[Cmdlet("Get", "MsoRawObject", DefaultParameterSetName = "ExchangeIdentity")]
	public sealed class GetMsoRawObject : Task
	{
		// Token: 0x06001D3F RID: 7487 RVA: 0x000812B0 File Offset: 0x0007F4B0
		private IRecipientSession GetRecipientSession(OrganizationId orgId)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), orgId, null, false);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.RescopeToSubtree(sessionSettings), 99, "GetRecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ForwardSync\\GetMsoRawObject.cs");
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = true;
			tenantOrRootOrgRecipientSession.SessionSettings.IncludeSoftDeletedObjects = true;
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x00081300 File Offset: 0x0007F500
		private IConfigurationSession GetOrgConfigurationSession(OrganizationId orgId)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), orgId, null, false);
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, sessionSettings, 116, "GetOrgConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ForwardSync\\GetMsoRawObject.cs");
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x00081337 File Offset: 0x0007F537
		// (set) Token: 0x06001D42 RID: 7490 RVA: 0x0008134E File Offset: 0x0007F54E
		[Parameter(ParameterSetName = "ExchangeIdentity", ValueFromPipeline = true, Mandatory = true, Position = 0)]
		public RecipientOrOrganizationIdParameter Identity
		{
			get
			{
				return (RecipientOrOrganizationIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x00081361 File Offset: 0x0007F561
		// (set) Token: 0x06001D44 RID: 7492 RVA: 0x00081378 File Offset: 0x0007F578
		[Parameter(ParameterSetName = "SyncObjectId", Mandatory = true)]
		public SyncObjectId ExternalObjectId
		{
			get
			{
				return (SyncObjectId)base.Fields["ExternalObjectID"];
			}
			set
			{
				base.Fields["ExternalObjectID"] = value;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x0008138B File Offset: 0x0007F58B
		// (set) Token: 0x06001D46 RID: 7494 RVA: 0x000813A2 File Offset: 0x0007F5A2
		[Parameter(ParameterSetName = "SyncObjectId", Mandatory = true)]
		public string ServiceInstanceId
		{
			get
			{
				return (string)base.Fields["ServiceInstanceId"];
			}
			set
			{
				base.Fields["ServiceInstanceId"] = value;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x000813B5 File Offset: 0x0007F5B5
		// (set) Token: 0x06001D48 RID: 7496 RVA: 0x000813DB File Offset: 0x0007F5DB
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeBackLinks
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeBackLinks"] ?? false);
			}
			set
			{
				base.Fields["IncludeBackLinks"] = value;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x000813F3 File Offset: 0x0007F5F3
		// (set) Token: 0x06001D4A RID: 7498 RVA: 0x00081419 File Offset: 0x0007F619
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeForwardLinks
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeForwardLinks"] ?? false);
			}
			set
			{
				base.Fields["IncludeForwardLinks"] = value;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x00081431 File Offset: 0x0007F631
		// (set) Token: 0x06001D4C RID: 7500 RVA: 0x00081448 File Offset: 0x0007F648
		[Parameter(Mandatory = false)]
		public int LinksResultSize
		{
			get
			{
				return (int)base.Fields["LinksResultSize"];
			}
			set
			{
				base.Fields["LinksResultSize"] = value;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x00081460 File Offset: 0x0007F660
		// (set) Token: 0x06001D4E RID: 7502 RVA: 0x00081486 File Offset: 0x0007F686
		[Parameter(Mandatory = false)]
		public SwitchParameter PopulateRawObject
		{
			get
			{
				return (SwitchParameter)(base.Fields["PopulateRawObject"] ?? false);
			}
			set
			{
				base.Fields["PopulateRawObject"] = value;
			}
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x0008149E File Offset: 0x0007F69E
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new GetTaskBaseModuleFactory();
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x000814A8 File Offset: 0x0007F6A8
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			bool flag = true;
			try
			{
				if (this.ExternalObjectId != null)
				{
					this.ProcessMsoRawObject(this.ExternalObjectId, this.ServiceInstanceId);
				}
				else if (this.Identity.ResolvedSyncObjectId != null && this.Identity.ResolvedServiceInstanceId != null)
				{
					this.ProcessMsoRawObject(this.Identity.ResolvedSyncObjectId, this.Identity.ResolvedServiceInstanceId);
				}
				else
				{
					if (this.Identity.RecipientParameter != null)
					{
						OrganizationId orgId = this.Identity.RecipientParameter.ResolveOrganizationIdBasedOnIdentity(OrganizationId.ForestWideOrgId);
						IRecipientSession recipientSession = this.GetRecipientSession(orgId);
						IEnumerable<ADRecipient> objects = this.Identity.RecipientParameter.GetObjects<ADRecipient>(null, recipientSession);
						foreach (ADRecipient adrecipient in objects)
						{
							flag = false;
							if (adrecipient.ConfigurationUnit == null)
							{
								this.WriteError(new Exception(Strings.RecipientFromFirstOrganization(adrecipient.ToString())), ErrorCategory.ObjectNotFound, null, false);
							}
							else if (string.IsNullOrEmpty(adrecipient.ExternalDirectoryObjectId))
							{
								this.WriteError(new Exception(Strings.RecipientHasNoExternalId(adrecipient.ToString())), ErrorCategory.ObjectNotFound, null, false);
							}
							else
							{
								DirectoryObjectClass msoObjectClassForRecipient = this.GetMsoObjectClassForRecipient(adrecipient.RecipientType, adrecipient.RecipientTypeDetails);
								if (msoObjectClassForRecipient != DirectoryObjectClass.Company)
								{
									IConfigurationSession orgConfigurationSession = this.GetOrgConfigurationSession(adrecipient.OrganizationId);
									ExchangeConfigurationUnit exchangeConfigurationUnit = orgConfigurationSession.Read<ExchangeConfigurationUnit>(adrecipient.ConfigurationUnit);
									if (exchangeConfigurationUnit == null)
									{
										this.WriteError(new Exception(Strings.RecipientTenantNotFound(adrecipient.ToString())), ErrorCategory.ObjectNotFound, null, false);
									}
									else if (string.IsNullOrEmpty(exchangeConfigurationUnit.DirSyncServiceInstance))
									{
										this.WriteError(new Exception(Strings.RecipientWithTenantServiceInstanceNotSet(adrecipient.ToString())), ErrorCategory.ObjectNotFound, null, false);
									}
									else
									{
										this.ProcessMsoRawObject(new SyncObjectId(exchangeConfigurationUnit.ExternalDirectoryOrganizationId, adrecipient.ExternalDirectoryObjectId, msoObjectClassForRecipient), exchangeConfigurationUnit.DirSyncServiceInstance);
									}
								}
							}
						}
					}
					if (this.Identity.OrganizationParameter != null)
					{
						OrganizationId orgId2 = this.Identity.OrganizationParameter.ResolveOrganizationIdBasedOnIdentity(OrganizationId.ForestWideOrgId);
						IConfigurationSession orgConfigurationSession2 = this.GetOrgConfigurationSession(orgId2);
						IEnumerable<ExchangeConfigurationUnit> objects2 = this.Identity.OrganizationParameter.GetObjects<ExchangeConfigurationUnit>(null, orgConfigurationSession2);
						foreach (ExchangeConfigurationUnit exchangeConfigurationUnit2 in objects2)
						{
							flag = false;
							if (string.IsNullOrEmpty(exchangeConfigurationUnit2.DirSyncServiceInstance))
							{
								this.WriteError(new Exception(Strings.TenantServiceInstanceNotSet(exchangeConfigurationUnit2.ConfigurationUnit.ToString())), ErrorCategory.ObjectNotFound, null, false);
							}
							else
							{
								this.ProcessMsoRawObject(new SyncObjectId(exchangeConfigurationUnit2.ExternalDirectoryOrganizationId, exchangeConfigurationUnit2.ExternalDirectoryOrganizationId, DirectoryObjectClass.Company), exchangeConfigurationUnit2.DirSyncServiceInstance);
							}
						}
					}
					if (flag)
					{
						if (this.Identity.RecipientParameter != null)
						{
							this.WriteError(new Exception(Strings.ExchangeRecipientNotFound(this.Identity.RecipientParameter.ToString())), ErrorCategory.ObjectNotFound, null, false);
						}
						if (this.Identity.OrganizationParameter != null)
						{
							this.WriteError(new Exception(Strings.ExchangeTenantNotFound(this.Identity.OrganizationParameter.ToString())), ErrorCategory.ObjectNotFound, null, false);
						}
					}
				}
			}
			catch (CouldNotCreateMsoSyncServiceException exception)
			{
				this.WriteError(exception, ErrorCategory.ObjectNotFound, null, true);
			}
			catch (InvalidMsoSyncServiceResponseException exception2)
			{
				this.WriteError(exception2, ErrorCategory.InvalidOperation, null, false);
			}
			catch (Exception exception3)
			{
				this.WriteError(exception3, ErrorCategory.InvalidOperation, null, true);
			}
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x00081890 File Offset: 0x0007FA90
		private DirectoryObjectClass GetMsoObjectClassForRecipient(RecipientType recipientType, RecipientTypeDetails recipientTypeDetails)
		{
			switch (recipientType)
			{
			case RecipientType.User:
			case RecipientType.UserMailbox:
			case RecipientType.MailUser:
				if ((recipientTypeDetails & RecipientTypeDetails.GroupMailbox) != RecipientTypeDetails.None)
				{
					return DirectoryObjectClass.Group;
				}
				return DirectoryObjectClass.User;
			case RecipientType.Contact:
			case RecipientType.MailContact:
				return DirectoryObjectClass.Contact;
			case RecipientType.Group:
			case RecipientType.MailUniversalDistributionGroup:
			case RecipientType.MailUniversalSecurityGroup:
			case RecipientType.MailNonUniversalGroup:
				return DirectoryObjectClass.Group;
			default:
				return DirectoryObjectClass.Company;
			}
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x000818E6 File Offset: 0x0007FAE6
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.service != null)
			{
				this.service.Dispose();
				this.service = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0008190C File Offset: 0x0007FB0C
		private void ProcessMsoRawObject(SyncObjectId syncObjectId, string serviceInstanceId)
		{
			if (this.service == null)
			{
				this.service = new MsoSyncService();
			}
			bool? allLinksCollected;
			DirectoryObjectsAndLinks msoRawObject = this.service.GetMsoRawObject(syncObjectId, serviceInstanceId, this.IncludeBackLinks.IsPresent, this.IncludeForwardLinks.IsPresent, base.Fields.IsModified("LinksResultSize") ? this.LinksResultSize : 1000, out allLinksCollected);
			if (msoRawObject.Objects.Length != 0)
			{
				MsoRawObject msoRawObject2 = new MsoRawObject(syncObjectId, serviceInstanceId, msoRawObject, allLinksCollected, this.PopulateRawObject.IsPresent);
				msoRawObject2.PopulateSyncObjectData();
				base.WriteObject(msoRawObject2);
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (msoRawObject.Errors.Length != 0)
			{
				foreach (DirectoryObjectError directoryObjectError in msoRawObject.Errors)
				{
					stringBuilder.Append(directoryObjectError.ErrorCode);
					stringBuilder.Append(";");
				}
			}
			else
			{
				stringBuilder.Append("no errors");
			}
			this.WriteError(new Exception(Strings.MsoObjectNotFound(syncObjectId.ToString(), stringBuilder.ToString())), ErrorCategory.ObjectNotFound, null, false);
		}

		// Token: 0x04001895 RID: 6293
		private const int DefaultLinksResultSize = 1000;

		// Token: 0x04001896 RID: 6294
		private MsoSyncService service;
	}
}
