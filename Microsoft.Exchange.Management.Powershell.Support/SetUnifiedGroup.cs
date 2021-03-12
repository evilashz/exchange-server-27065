using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Directory;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Net.AAD;
using Microsoft.Exchange.UnifiedGroups;
using Microsoft.WindowsAzure.ActiveDirectory;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200001F RID: 31
	[Cmdlet("Set", "UnifiedGroup")]
	public sealed class SetUnifiedGroup : UnifiedGroupTask
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00006E9C File Offset: 0x0000509C
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00006EA4 File Offset: 0x000050A4
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public Guid Identity { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00006EAD File Offset: 0x000050AD
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00006EC4 File Offset: 0x000050C4
		[Parameter(Mandatory = false, ParameterSetName = "SetUnifiedGroup")]
		public string DisplayName
		{
			get
			{
				return (string)base.Fields["DisplayName"];
			}
			set
			{
				base.Fields["DisplayName"] = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00006ED7 File Offset: 0x000050D7
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00006EEE File Offset: 0x000050EE
		[Parameter(Mandatory = false, ParameterSetName = "SetUnifiedGroup")]
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

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00006F01 File Offset: 0x00005101
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00006F18 File Offset: 0x00005118
		[Parameter(Mandatory = false, ParameterSetName = "SetUnifiedGroup")]
		public RecipientIdParameter[] AddOwners
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["AddOwners"];
			}
			set
			{
				base.Fields["AddOwners"] = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00006F2B File Offset: 0x0000512B
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00006F42 File Offset: 0x00005142
		[Parameter(Mandatory = false, ParameterSetName = "SetUnifiedGroup")]
		public RecipientIdParameter[] RemoveOwners
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["RemoveOwners"];
			}
			set
			{
				base.Fields["RemoveOwners"] = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00006F55 File Offset: 0x00005155
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00006F6C File Offset: 0x0000516C
		[Parameter(Mandatory = false, ParameterSetName = "SetUnifiedGroup")]
		public RecipientIdParameter[] AddMembers
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["AddMembers"];
			}
			set
			{
				base.Fields["AddMembers"] = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00006F7F File Offset: 0x0000517F
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00006F96 File Offset: 0x00005196
		[Parameter(Mandatory = false, ParameterSetName = "SetUnifiedGroup")]
		public RecipientIdParameter[] RemoveMembers
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["RemoveMembers"];
			}
			set
			{
				base.Fields["RemoveMembers"] = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00006FA9 File Offset: 0x000051A9
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00006FCF File Offset: 0x000051CF
		[Parameter(Mandatory = false, ParameterSetName = "SetUnifiedGroup")]
		public SwitchParameter PublishExchangeResources
		{
			get
			{
				return (SwitchParameter)(base.Fields["PublishExchangeResources"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["PublishExchangeResources"] = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00006FE7 File Offset: 0x000051E7
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000700D File Offset: 0x0000520D
		[Parameter(Mandatory = false, ParameterSetName = "SyncGroupMailbox")]
		public SwitchParameter SyncGroupMailbox
		{
			get
			{
				return (SwitchParameter)(base.Fields["SyncGroupMailbox"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SyncGroupMailbox"] = value;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007048 File Offset: 0x00005248
		protected override void InternalValidate()
		{
			base.InternalValidate();
			this.recipientSession = base.GetRecipientSession();
			ADRecipient[] array = base.ResolveRecipientIdParameters<ADRecipient>(this.AddOwners, this.recipientSession);
			if (array != null)
			{
				this.addOwners = (from recipient in array
				select recipient.ExternalDirectoryObjectId).ToArray<string>();
			}
			array = base.ResolveRecipientIdParameters<ADRecipient>(this.RemoveOwners, this.recipientSession);
			if (array != null)
			{
				this.removeOwners = (from recipient in array
				select recipient.ExternalDirectoryObjectId).ToArray<string>();
			}
			array = base.ResolveRecipientIdParameters<ADRecipient>(this.AddMembers, this.recipientSession);
			if (array != null)
			{
				this.addMembers = (from recipient in array
				select recipient.ExternalDirectoryObjectId).ToArray<string>();
			}
			array = base.ResolveRecipientIdParameters<ADRecipient>(this.RemoveMembers, this.recipientSession);
			if (array != null)
			{
				this.removeMembers = (from recipient in array
				select recipient.ExternalDirectoryObjectId).ToArray<string>();
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007178 File Offset: 0x00005378
		protected override void InternalProcessRecord()
		{
			if (base.Fields.IsModified("SyncGroupMailbox") && this.SyncGroupMailbox)
			{
				this.UpdateFromGroupMailbox();
				return;
			}
			ADUser groupMailbox = this.recipientSession.FindADUserByExternalDirectoryObjectId(this.Identity.ToString());
			ADUser ownerFromAAD = this.GetOwnerFromAAD(groupMailbox, this.recipientSession);
			string[] exchangeResources = this.GetExchangeResources(groupMailbox);
			AADClient aadclient = AADClientFactory.Create(ownerFromAAD);
			if (aadclient == null)
			{
				base.WriteError(new TaskException(Strings.ErrorUnableToSessionWithAAD), ExchangeErrorCategory.Client, null);
			}
			this.UpdateGroup(this.DisplayName, this.Description, exchangeResources, this.Identity.ToString(), aadclient);
			base.AddOwnersInAAD(this.addOwners, aadclient, this.Identity.ToString());
			base.RemoveOwnersInAAD(this.removeOwners, aadclient, this.Identity.ToString());
			base.AddMembersInAAD(this.addMembers, aadclient, this.Identity.ToString());
			base.RemoveMembersInAAD(this.removeMembers, aadclient, this.Identity.ToString());
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000072B4 File Offset: 0x000054B4
		private void UpdateFromGroupMailbox()
		{
			ADUser aduser = this.recipientSession.FindADUserByExternalDirectoryObjectId(this.Identity.ToString());
			ADUser ownerFromAAD = this.GetOwnerFromAAD(aduser, this.recipientSession);
			string[] members = base.GetMembers(aduser, this.recipientSession, "Set-UnifiedGroup");
			string[] owners = base.GetOwners(aduser, null, this.recipientSession);
			string description = (aduser.Description != null && aduser.Description.Count > 0) ? aduser.Description[0] : string.Empty;
			string[] exchangeResources = this.GetExchangeResources(aduser);
			AADClient aadclient = AADClientFactory.Create(ownerFromAAD);
			if (aadclient == null)
			{
				base.WriteError(new TaskException(Strings.ErrorUnableToSessionWithAAD), ExchangeErrorCategory.Client, null);
			}
			this.UpdateGroup(aduser.DisplayName, description, exchangeResources, aduser.ExternalDirectoryObjectId, aadclient);
			base.AddOwnersInAAD(owners, aadclient, aduser.ExternalDirectoryObjectId);
			base.AddMembersInAAD(members, aadclient, aduser.ExternalDirectoryObjectId);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000073A0 File Offset: 0x000055A0
		private ADUser GetOwnerFromAAD(ADUser groupMailbox, IRecipientSession recipientSession)
		{
			Group group = null;
			AADClient aadclient = AADClientFactory.Create(base.OrganizationId, GraphProxyVersions.Version14);
			if (aadclient == null)
			{
				base.WriteError(new TaskException(Strings.ErrorUnableToSessionWithAAD), ExchangeErrorCategory.Client, null);
			}
			try
			{
				group = aadclient.GetGroup(groupMailbox.ExternalDirectoryObjectId, true);
				aadclient.Service.LoadProperty(group, "owners");
			}
			catch (AADException ex)
			{
				base.WriteVerbose("Failed to get group owner from AAD with exception: {0}", new object[]
				{
					ex
				});
				base.WriteError(new TaskException(Strings.ErrorUnableToGetGroupOwners), base.GetErrorCategory(ex), null);
			}
			if (group.owners != null)
			{
				foreach (DirectoryObject directoryObject in group.owners)
				{
					ADUser aduser = recipientSession.FindADUserByExternalDirectoryObjectId(directoryObject.objectId);
					if (aduser != null)
					{
						return aduser;
					}
				}
			}
			base.WriteError(new TaskException(Strings.ErrorUnableToGetGroupOwners), ExchangeErrorCategory.Client, null);
			return null;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000074B0 File Offset: 0x000056B0
		private void UpdateGroup(string displayName, string description, string[] exchangeResources, string groupObjectId, AADClient aadClient)
		{
			try
			{
				base.WriteVerbose("Updating UnifiedGroup for group {0}", new object[]
				{
					groupObjectId
				});
				aadClient.UpdateGroup(groupObjectId, description, exchangeResources, displayName, null);
				base.WriteVerbose("UnifiedGroup updated successfully", new object[0]);
			}
			catch (AADException ex)
			{
				base.WriteVerbose("UpdateGroup failed with exception: {0}", new object[]
				{
					ex
				});
				base.WriteError(new TaskException(Strings.ErrorUnableToUpdateUnifiedGroup), base.GetErrorCategory(ex), null);
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007544 File Offset: 0x00005744
		private string[] GetExchangeResources(ADUser groupMailbox)
		{
			try
			{
				MailboxUrls mailboxUrls = new MailboxUrls(ExchangePrincipal.FromADUser(groupMailbox, RemotingOptions.AllowCrossSite), true);
				return mailboxUrls.ToExchangeResources();
			}
			catch (LocalizedException ex)
			{
				base.WriteVerbose("Failed to get MailboxUrls with exception: {0}", new object[]
				{
					ex
				});
				this.WriteWarning(Strings.WarningUnableToUpdateExchangeResources);
			}
			return null;
		}

		// Token: 0x04000084 RID: 132
		private IRecipientSession recipientSession;

		// Token: 0x04000085 RID: 133
		private string[] addOwners;

		// Token: 0x04000086 RID: 134
		private string[] removeOwners;

		// Token: 0x04000087 RID: 135
		private string[] addMembers;

		// Token: 0x04000088 RID: 136
		private string[] removeMembers;
	}
}
