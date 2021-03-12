using System;
using System.Management.Automation;
using System.Net;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000061 RID: 97
	[Cmdlet("New", "LinkedUser", SupportsShouldProcess = true)]
	public sealed class NewLinkedUser : NewGeneralRecipientObjectTask<ADUser>
	{
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001B3D0 File Offset: 0x000195D0
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x0001B3DD File Offset: 0x000195DD
		[Parameter(Mandatory = true)]
		public string UserPrincipalName
		{
			get
			{
				return this.DataObject.UserPrincipalName;
			}
			set
			{
				this.DataObject.UserPrincipalName = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001B3EB File Offset: 0x000195EB
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x0001B402 File Offset: 0x00019602
		[Parameter(Mandatory = false)]
		public UserIdParameter LinkedMasterAccount
		{
			get
			{
				return (UserIdParameter)base.Fields["LinkedMasterAccount"];
			}
			set
			{
				base.Fields["LinkedMasterAccount"] = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0001B415 File Offset: 0x00019615
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x0001B42C File Offset: 0x0001962C
		[Parameter(Mandatory = false)]
		public string LinkedDomainController
		{
			get
			{
				return (string)base.Fields["LinkedDomainController"];
			}
			set
			{
				base.Fields["LinkedDomainController"] = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0001B43F File Offset: 0x0001963F
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x0001B456 File Offset: 0x00019656
		[Parameter(Mandatory = false)]
		public PSCredential LinkedCredential
		{
			get
			{
				return (PSCredential)base.Fields["LinkedCredential"];
			}
			set
			{
				base.Fields["LinkedCredential"] = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0001B469 File Offset: 0x00019669
		// (set) Token: 0x06000691 RID: 1681 RVA: 0x0001B476 File Offset: 0x00019676
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<X509Identifier> CertificateSubject
		{
			get
			{
				return this.DataObject.CertificateSubject;
			}
			set
			{
				this.DataObject.CertificateSubject = value;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0001B484 File Offset: 0x00019684
		// (set) Token: 0x06000693 RID: 1683 RVA: 0x0001B48B File Offset: 0x0001968B
		private new string ExternalDirectoryObjectId
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0001B492 File Offset: 0x00019692
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewUser(base.Name.ToString());
			}
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001B4AC File Offset: 0x000196AC
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.LinkedMasterAccount != null)
			{
				if (string.IsNullOrEmpty(this.LinkedDomainController))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorMissLinkedDomainController), ErrorCategory.InvalidArgument, base.Name);
				}
				try
				{
					NetworkCredential userForestCredential = (this.LinkedCredential == null) ? null : this.LinkedCredential.GetNetworkCredential();
					this.linkedUserSid = MailboxTaskHelper.GetAccountSidFromAnotherForest(this.LinkedMasterAccount, this.LinkedDomainController, userForestCredential, base.GlobalConfigSession, new MailboxTaskHelper.GetUniqueObject(base.GetDataObject<ADUser>), new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
				}
				catch (PSArgumentException exception)
				{
					base.ThrowTerminatingError(exception, ErrorCategory.InvalidArgument, this.LinkedCredential);
				}
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001B564 File Offset: 0x00019764
		protected override void PrepareRecipientObject(ADUser user)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(user);
			user.RecipientTypeDetails = RecipientTypeDetails.LinkedUser;
			user.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.LinkedUser);
			if (this.LinkedMasterAccount != null)
			{
				user.MasterAccountSid = this.linkedUserSid;
			}
			RecipientTaskHelper.IsUserPrincipalNameUnique(base.TenantGlobalCatalogSession, user, user.UserPrincipalName, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client);
			TaskLogger.LogExit();
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001B5E0 File Offset: 0x000197E0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			NewLinkedUser.ValidateCertificateSubject(this.CertificateSubject, OrganizationId.ForestWideOrgId.Equals(base.CurrentOrganizationId) ? null : base.CurrentOrganizationId.PartitionId, null, new Task.TaskErrorLoggingDelegate(base.WriteError));
			TaskLogger.LogExit();
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001B638 File Offset: 0x00019838
		internal static void ValidateCertificateSubject(MultiValuedProperty<X509Identifier> certificateSubjects, PartitionId partitionId, ADObjectId excludeObjectId, Task.TaskErrorLoggingDelegate errorLogger)
		{
			if (errorLogger == null)
			{
				throw new ArgumentNullException("errorLogger");
			}
			ADSessionSettings sessionSettings;
			if (partitionId != null)
			{
				sessionSettings = ADSessionSettings.FromAllTenantsPartitionId(partitionId);
			}
			else
			{
				sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 220, "ValidateCertificateSubject", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\LinkedUser\\NewLinkedUser.cs");
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = true;
			if (certificateSubjects != null && certificateSubjects.Count != 0)
			{
				foreach (X509Identifier x509Identifier in certificateSubjects)
				{
					QueryFilter queryFilter = ADUser.GetCertificateMatchFilter(x509Identifier);
					if (excludeObjectId != null)
					{
						queryFilter = new AndFilter(new QueryFilter[]
						{
							queryFilter,
							new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Id, excludeObjectId)
						});
					}
					ADRecipient[] array = tenantOrRootOrgRecipientSession.Find(null, QueryScope.SubTree, queryFilter, null, 1);
					if (array != null && array.Length > 0)
					{
						errorLogger(new RecipientTaskException(Strings.ErrorDuplicateCertificateSubject(x509Identifier.ToString())), ErrorCategory.InvalidArgument, excludeObjectId);
					}
				}
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001B73C File Offset: 0x0001993C
		protected override void WriteResult(ADObject result)
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Identity
			});
			ADUser aduser = (ADUser)result;
			if (null != aduser.MasterAccountSid)
			{
				aduser.LinkedMasterAccount = SecurityPrincipalIdParameter.GetFriendlyUserName(aduser.MasterAccountSid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				aduser.ResetChangeTracking();
			}
			base.WriteResult(new LinkedUser(aduser));
			TaskLogger.LogExit();
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001B7AD File Offset: 0x000199AD
		protected override string ClonableTypeName
		{
			get
			{
				return typeof(LinkedUser).FullName;
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001B7BE File Offset: 0x000199BE
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return User.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001B7CB File Offset: 0x000199CB
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x0400018B RID: 395
		private SecurityIdentifier linkedUserSid;
	}
}
