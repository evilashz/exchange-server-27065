using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007C6 RID: 1990
	[Cmdlet("Set", "MailboxCalendarFolder", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxCalendarFolder : SetTenantXsoObjectWithFolderIdentityTaskBase<MailboxCalendarFolder>
	{
		// Token: 0x17001517 RID: 5399
		// (get) Token: 0x060045D4 RID: 17876 RVA: 0x0011EF24 File Offset: 0x0011D124
		private static string PreferredHostname
		{
			get
			{
				if (SetMailboxCalendarFolder.preferredHostname == null)
				{
					ServiceEndpoint serviceEndpoint = null;
					try
					{
						ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 56, "PreferredHostname", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\UserOptions\\MailboxCalendarFolder\\SetMailboxCalendarFolder.cs");
						ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
						serviceEndpoint = endpointContainer.GetEndpoint(ServiceEndpointId.AnonymousCalendarHostUrl);
					}
					catch (EndpointContainerNotFoundException)
					{
					}
					catch (ServiceEndpointNotFoundException)
					{
					}
					SetMailboxCalendarFolder.preferredHostname = ((serviceEndpoint != null) ? serviceEndpoint.Uri.Host : string.Empty);
				}
				return SetMailboxCalendarFolder.preferredHostname;
			}
		}

		// Token: 0x17001518 RID: 5400
		// (get) Token: 0x060045D5 RID: 17877 RVA: 0x0011EFB0 File Offset: 0x0011D1B0
		// (set) Token: 0x060045D6 RID: 17878 RVA: 0x0011EFD6 File Offset: 0x0011D1D6
		[Parameter(Mandatory = false)]
		public SwitchParameter ResetUrl
		{
			get
			{
				return (SwitchParameter)(base.Fields["ResetUrl"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ResetUrl"] = value;
				SetMailboxCalendarFolder.preferredHostname = null;
			}
		}

		// Token: 0x17001519 RID: 5401
		// (get) Token: 0x060045D7 RID: 17879 RVA: 0x0011EFF4 File Offset: 0x0011D1F4
		private bool IsMultitenancyEnabled
		{
			get
			{
				return VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;
			}
		}

		// Token: 0x1700151A RID: 5402
		// (get) Token: 0x060045D9 RID: 17881 RVA: 0x0011F020 File Offset: 0x0011D220
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageMailboxCalendarFolder(this.Identity.ToString());
			}
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x0011F032 File Offset: 0x0011D232
		protected override IConfigDataProvider CreateSession()
		{
			this.user = this.PrepareMailboxUser();
			base.InnerMailboxFolderDataProvider = new MailboxCalendarFolderDataProvider(base.SessionSettings, this.user, "Set-MailboxCalendarFolder");
			return base.InnerMailboxFolderDataProvider;
		}

		// Token: 0x060045DB RID: 17883 RVA: 0x0011F064 File Offset: 0x0011D264
		protected override IConfigurable PrepareDataObject()
		{
			MailboxCalendarFolder mailboxCalendarFolder = (MailboxCalendarFolder)base.PrepareDataObject();
			mailboxCalendarFolder.MailboxFolderId = this.Identity.InternalMailboxFolderId;
			return mailboxCalendarFolder;
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x0011F090 File Offset: 0x0011D290
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.DataObject.PublishEnabled)
			{
				if (this.DataObject.PublishedCalendarUrl == null || this.DataObject.PublishedICalUrl == null || this.ResetUrl || this.DataObject.IsChanged(MailboxCalendarFolderSchema.SearchableUrlEnabled))
				{
					string owaServiceUrl = this.GetOwaServiceUrl(base.InnerMailboxFolderDataProvider.MailboxSession.MailboxOwner);
					UriBuilder uriBuilder = new UriBuilder(owaServiceUrl);
					uriBuilder.Scheme = Uri.UriSchemeHttp;
					uriBuilder.Port = -1;
					if (!string.IsNullOrEmpty(SetMailboxCalendarFolder.PreferredHostname))
					{
						uriBuilder.Host = SetMailboxCalendarFolder.PreferredHostname;
					}
					string owaHttpExternalUrl = uriBuilder.Uri.ToString();
					if (this.DataObject.SearchableUrlEnabled)
					{
						StoreObjectId folderId = this.DataObject.MailboxFolderId.StoreObjectIdValue ?? base.InnerMailboxFolderDataProvider.ResolveStoreObjectIdFromFolderPath(this.DataObject.MailboxFolderId.MailboxFolderPath);
						this.GeneratePublicUrls(owaHttpExternalUrl, folderId);
					}
					else
					{
						this.GenerateObscureUrls(owaHttpExternalUrl);
					}
				}
			}
			else if (!this.DataObject.IsChanged(MailboxCalendarFolderSchema.PublishEnabled) && (this.ResetUrl || this.DataObject.IsModified(MailboxCalendarFolderSchema.SearchableUrlEnabled) || this.DataObject.IsModified(MailboxCalendarFolderSchema.PublishDateRangeFrom) || this.DataObject.IsModified(MailboxCalendarFolderSchema.PublishDateRangeTo) || this.DataObject.IsModified(MailboxCalendarFolderSchema.DetailLevel)))
			{
				base.WriteError(new PublishingNotEnabledException(), ExchangeErrorCategory.Client, this.Identity);
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x060045DD RID: 17885 RVA: 0x0011F21C File Offset: 0x0011D41C
		private void GeneratePublicUrls(string owaHttpExternalUrl, StoreObjectId folderId)
		{
			SmtpAddress primarySmtpAddress = this.user.PrimarySmtpAddress;
			string folderName = null;
			using (Folder folder = Folder.Bind(base.InnerMailboxFolderDataProvider.MailboxSession, folderId))
			{
				folderName = folder.DisplayName;
			}
			PublicUrl publicUrl = PublicUrl.Create(owaHttpExternalUrl, SharingDataType.Calendar, primarySmtpAddress, folderName, this.user.SharingAnonymousIdentities);
			this.DataObject.PublishedCalendarUrl = publicUrl.ToString() + ".html";
			this.DataObject.PublishedICalUrl = publicUrl.ToString() + ".ics";
		}

		// Token: 0x060045DE RID: 17886 RVA: 0x0011F2BC File Offset: 0x0011D4BC
		private void GenerateObscureUrls(string owaHttpExternalUrl)
		{
			string domain = this.user.PrimarySmtpAddress.Domain;
			Guid mailboxGuid = base.InnerMailboxFolderDataProvider.MailboxSession.MailboxGuid;
			ObscureUrl obscureUrl = ObscureUrl.CreatePublishCalendarUrl(owaHttpExternalUrl, mailboxGuid, domain);
			this.DataObject.PublishedCalendarUrl = obscureUrl.ToString() + ".html";
			this.DataObject.PublishedICalUrl = obscureUrl.ToString() + ".ics";
		}

		// Token: 0x060045DF RID: 17887 RVA: 0x0011F32D File Offset: 0x0011D52D
		private string GetOwaServiceUrl(IExchangePrincipal exchangePrincipal)
		{
			if (exchangePrincipal.MailboxInfo.Location.ServerVersion >= Server.E15MinVersion && this.IsMultitenancyEnabled)
			{
				return this.GetE15MultitenancyOwaServiceUrl(exchangePrincipal);
			}
			return this.GetEnterpriseOrE14OwaServiceUrl(exchangePrincipal);
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x0011F360 File Offset: 0x0011D560
		private string GetE15MultitenancyOwaServiceUrl(IExchangePrincipal exchangePrincipal)
		{
			Uri uri = null;
			Exception ex = null;
			try
			{
				uri = FrontEndLocator.GetFrontEndOwaUrl(exchangePrincipal);
			}
			catch (ServerNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (ADTransientException ex3)
			{
				ex = ex3;
			}
			catch (DataSourceOperationException ex4)
			{
				ex = ex4;
			}
			catch (DataValidationException ex5)
			{
				ex = ex5;
			}
			finally
			{
				if (ex != null)
				{
					throw new NoExternalOwaAvailableException(ex);
				}
			}
			return uri.ToString();
		}

		// Token: 0x060045E1 RID: 17889 RVA: 0x0011F3E8 File Offset: 0x0011D5E8
		private string GetEnterpriseOrE14OwaServiceUrl(IExchangePrincipal exchangePrincipal)
		{
			ServiceTopology serviceTopology = this.IsMultitenancyEnabled ? ServiceTopology.GetCurrentLegacyServiceTopology("f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\UserOptions\\MailboxCalendarFolder\\SetMailboxCalendarFolder.cs", "GetEnterpriseOrE14OwaServiceUrl", 335) : ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\UserOptions\\MailboxCalendarFolder\\SetMailboxCalendarFolder.cs", "GetEnterpriseOrE14OwaServiceUrl", 335);
			IList<OwaService> list = serviceTopology.FindAll<OwaService>(exchangePrincipal, ClientAccessType.External, SetMailboxCalendarFolder.serviceFilter, "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\UserOptions\\MailboxCalendarFolder\\SetMailboxCalendarFolder.cs", "GetEnterpriseOrE14OwaServiceUrl", 339);
			if (list.Count != 0)
			{
				return list[0].Url.ToString();
			}
			OwaService owaService = serviceTopology.FindAny<OwaService>(ClientAccessType.External, SetMailboxCalendarFolder.serviceFilter, "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\UserOptions\\MailboxCalendarFolder\\SetMailboxCalendarFolder.cs", "GetEnterpriseOrE14OwaServiceUrl", 348);
			if (owaService == null)
			{
				throw new NoExternalOwaAvailableException();
			}
			return owaService.Url.ToString();
		}

		// Token: 0x04002AEC RID: 10988
		private const string BrowseUrlType = ".html";

		// Token: 0x04002AED RID: 10989
		private const string ICalUrlType = ".ics";

		// Token: 0x04002AEE RID: 10990
		private static Predicate<OwaService> serviceFilter = (OwaService service) => service.AnonymousFeaturesEnabled;

		// Token: 0x04002AEF RID: 10991
		private static string preferredHostname;

		// Token: 0x04002AF0 RID: 10992
		private ADUser user;
	}
}
