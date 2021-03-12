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
	// Token: 0x02000063 RID: 99
	[Cmdlet("set", "LinkedUser", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetLinkedUser : SetOrgPersonObjectTask<UserIdParameter, LinkedUser, ADUser>
	{
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001B857 File Offset: 0x00019A57
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetLinkedUser(this.Identity.ToString());
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001B869 File Offset: 0x00019A69
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0001B880 File Offset: 0x00019A80
		[Parameter(Mandatory = false)]
		public UserIdParameter LinkedMasterAccount
		{
			get
			{
				return (UserIdParameter)base.Fields[UserSchema.LinkedMasterAccount];
			}
			set
			{
				base.Fields[UserSchema.LinkedMasterAccount] = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001B893 File Offset: 0x00019A93
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0001B8AA File Offset: 0x00019AAA
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

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001B8BD File Offset: 0x00019ABD
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x0001B8D4 File Offset: 0x00019AD4
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

		// Token: 0x060006AA RID: 1706 RVA: 0x0001B8F0 File Offset: 0x00019AF0
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.LinkedMasterAccount != null)
			{
				if (string.IsNullOrEmpty(this.LinkedDomainController))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorMissLinkedDomainController), ErrorCategory.InvalidArgument, this.Identity);
				}
				try
				{
					NetworkCredential userForestCredential = (this.LinkedCredential == null) ? null : this.LinkedCredential.GetNetworkCredential();
					this.linkedUserSid = MailboxTaskHelper.GetAccountSidFromAnotherForest(this.LinkedMasterAccount, this.LinkedDomainController, userForestCredential, base.GlobalConfigSession, new MailboxTaskHelper.GetUniqueObject(base.GetDataObject<ADUser>), new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
				}
				catch (PSArgumentException exception)
				{
					TaskLogger.LogExit();
					base.ThrowTerminatingError(exception, ErrorCategory.InvalidArgument, this.LinkedCredential);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001B9B4 File Offset: 0x00019BB4
		protected override IConfigurable ResolveDataObject()
		{
			ADUser aduser = (ADUser)base.ResolveDataObject();
			if (aduser.RecipientTypeDetails != RecipientTypeDetails.LinkedUser)
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), ErrorCategory.InvalidData, this.Identity);
			}
			if (base.Fields.IsModified(UserSchema.LinkedMasterAccount))
			{
				aduser.MasterAccountSid = this.linkedUserSid;
			}
			return aduser;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001BA44 File Offset: 0x00019C44
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADUser aduser = (ADUser)base.PrepareDataObject();
			aduser.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.LinkedUser);
			if ("Linked" == base.ParameterSetName)
			{
				aduser.MasterAccountSid = this.linkedUserSid;
			}
			TaskLogger.LogExit();
			return aduser;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001BA94 File Offset: 0x00019C94
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			SetADUserBase<UserIdParameter, User>.ValidateUserParameters(this.DataObject, this.ConfigurationSession, RecipientTaskHelper.CreatePartitionOrRootOrgScopedGcSession(base.DomainController, this.DataObject.Id), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client, this.ShouldCheckAcceptedDomains(), base.ProvisioningCache);
			if (this.DataObject.IsModified(UserSchema.CertificateSubject))
			{
				NewLinkedUser.ValidateCertificateSubject(this.DataObject.CertificateSubject, OrganizationId.ForestWideOrgId.Equals(this.DataObject.OrganizationId) ? null : this.DataObject.OrganizationId.PartitionId, this.DataObject.Id, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001BB69 File Offset: 0x00019D69
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return new LinkedUser((ADUser)dataObject);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001BB76 File Offset: 0x00019D76
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x0400018C RID: 396
		private SecurityIdentifier linkedUserSid;
	}
}
