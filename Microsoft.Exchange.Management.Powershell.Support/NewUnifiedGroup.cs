using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Net.AAD;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Assistants;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.UnifiedGroups;
using Microsoft.WindowsAzure.ActiveDirectory;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200001D RID: 29
	[Cmdlet("New", "UnifiedGroup")]
	public sealed class NewUnifiedGroup : UnifiedGroupTask
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000065D6 File Offset: 0x000047D6
		// (set) Token: 0x06000146 RID: 326 RVA: 0x000065ED File Offset: 0x000047ED
		[Parameter(Mandatory = true, ParameterSetName = "NewUnifiedGroup")]
		public string Alias
		{
			get
			{
				return (string)base.Fields["Alias"];
			}
			set
			{
				base.Fields["Alias"] = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006600 File Offset: 0x00004800
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00006617 File Offset: 0x00004817
		[Parameter(Mandatory = false, ParameterSetName = "NewUnifiedGroup")]
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

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000662A File Offset: 0x0000482A
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00006641 File Offset: 0x00004841
		[Parameter(Mandatory = true, ParameterSetName = "NewUnifiedGroup", Position = 0)]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00006654 File Offset: 0x00004854
		// (set) Token: 0x0600014C RID: 332 RVA: 0x0000666B File Offset: 0x0000486B
		[Parameter(Mandatory = true, ParameterSetName = "NewUnifiedGroup")]
		[ValidateNotNullOrEmpty]
		public RecipientIdParameter ExecutingUser
		{
			get
			{
				return (RecipientIdParameter)base.Fields["ExecutingUser"];
			}
			set
			{
				base.Fields["ExecutingUser"] = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000667E File Offset: 0x0000487E
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00006695 File Offset: 0x00004895
		[Parameter(Mandatory = false, ParameterSetName = "NewUnifiedGroup")]
		public bool? IsPublic
		{
			get
			{
				return (bool?)base.Fields["IsPublic"];
			}
			set
			{
				base.Fields["IsPublic"] = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000066AD File Offset: 0x000048AD
		// (set) Token: 0x06000150 RID: 336 RVA: 0x000066C4 File Offset: 0x000048C4
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "NewUnifiedGroup")]
		public RecipientIdParameter[] Members
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["Members"];
			}
			set
			{
				base.Fields["Members"] = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000066D7 File Offset: 0x000048D7
		// (set) Token: 0x06000152 RID: 338 RVA: 0x000066EE File Offset: 0x000048EE
		[Parameter(Mandatory = false, ParameterSetName = "NewUnifiedGroup")]
		[ValidateNotNullOrEmpty]
		public RecipientIdParameter[] Owners
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["Owners"];
			}
			set
			{
				base.Fields["Owners"] = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00006701 File Offset: 0x00004901
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00006718 File Offset: 0x00004918
		[Parameter(Mandatory = true, ParameterSetName = "FromGroupMailbox")]
		public MailboxIdParameter FromGroupMailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["FromGroupMailbox"];
			}
			set
			{
				base.Fields["FromGroupMailbox"] = value;
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000673C File Offset: 0x0000493C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			this.recipientSession = base.GetRecipientSession();
			this.executingUser = base.ResolveRecipientIdParameter<ADUser>(this.ExecutingUser, this.recipientSession);
			ADRecipient[] array = base.ResolveRecipientIdParameters<ADRecipient>(this.Owners, this.recipientSession);
			if (array != null)
			{
				this.owners = (from recipient in array
				select recipient.ExternalDirectoryObjectId).ToArray<string>();
			}
			array = base.ResolveRecipientIdParameters<ADRecipient>(this.Members, this.recipientSession);
			if (array != null)
			{
				this.members = (from recipient in array
				select recipient.ExternalDirectoryObjectId).ToArray<string>();
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000067FB File Offset: 0x000049FB
		protected override void InternalProcessRecord()
		{
			this.recipientSession = base.GetRecipientSession();
			if (base.Fields.IsModified("FromGroupMailbox"))
			{
				this.CreateFromGroupMailbox();
				return;
			}
			this.CreateUnifiedGroup();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006828 File Offset: 0x00004A28
		private void CreateUnifiedGroup()
		{
			AADClient aadclient = this.CreateAADClient(this.executingUser);
			string groupObjectId = this.CreateGroup(this.DisplayName, this.Alias, this.Description, this.IsPublic == null || this.IsPublic.Value, aadclient);
			aadclient.AddMembers(groupObjectId, new string[]
			{
				this.executingUser.ExternalDirectoryObjectId
			});
			base.AddOwnersInAAD(this.owners, aadclient, groupObjectId);
			base.AddMembersInAAD(this.members, aadclient, groupObjectId);
			this.WriteGroup(aadclient, groupObjectId);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000068C4 File Offset: 0x00004AC4
		private void CreateFromGroupMailbox()
		{
			ADUser groupMailbox = this.GetGroupMailbox(this.FromGroupMailbox, this.recipientSession);
			ADUser creator = this.GetCreator(groupMailbox, this.recipientSession);
			string[] array = base.GetMembers(groupMailbox, this.recipientSession, "New-UnifiedGroup");
			string[] array2 = base.GetOwners(groupMailbox, creator, this.recipientSession);
			string description = (groupMailbox.Description != null && groupMailbox.Description.Count > 0) ? groupMailbox.Description[0] : string.Empty;
			AADClient aadclient = this.CreateAADClient(creator);
			string text = this.CreateGroup(groupMailbox.DisplayName, groupMailbox.Name, description, groupMailbox.ModernGroupType == ModernGroupObjectType.Public, aadclient);
			base.AddOwnersInAAD(array2, aadclient, text);
			base.AddMembersInAAD(array, aadclient, text);
			try
			{
				base.WriteVerbose("Updating group mailbox with new ExternalDirectoryObjectId", new object[0]);
				groupMailbox.ExternalDirectoryObjectId = text;
				groupMailbox.CustomAttribute13 = "AADGroup";
				this.recipientSession.Save(groupMailbox);
				base.WriteVerbose("Updated group mailbox with new ExternalDirectoryObjectId", new object[0]);
			}
			catch (LocalizedException ex)
			{
				base.WriteVerbose("Failed to update ExternalDirectoryObjectId property on group mailbox due exception: {0}", new object[]
				{
					ex
				});
				this.WriteWarning(Strings.ErrorCannotUpdateExternalDirectoryObjectId(groupMailbox.Id.ToString(), text));
				aadclient.DeleteGroup(text);
				return;
			}
			this.ReplicateMailboxAssociation(groupMailbox);
			this.WriteGroup(aadclient, text);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006A24 File Offset: 0x00004C24
		private AADClient CreateAADClient(ADUser user)
		{
			AADClient aadclient = AADClientFactory.Create(user);
			if (aadclient == null)
			{
				base.WriteError(new TaskException(Strings.ErrorUnableToSessionWithAAD), ExchangeErrorCategory.Client, null);
			}
			return aadclient;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006A54 File Offset: 0x00004C54
		private ADUser GetGroupMailbox(MailboxIdParameter mailboxId, IRecipientSession recipientSession)
		{
			LocalizedString? localizedString;
			ADUser objectInOrganization = mailboxId.GetObjectInOrganization<ADUser>(null, recipientSession, null, out localizedString);
			if (objectInOrganization == null)
			{
				base.WriteError(new TaskException(localizedString.Value), (ErrorCategory)1003, null);
			}
			return objectInOrganization;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006A8C File Offset: 0x00004C8C
		private ADUser GetCreator(ADUser groupMailbox, IRecipientSession recipientSession)
		{
			foreach (ADObjectId entryId in ((MultiValuedProperty<ADObjectId>)groupMailbox[GroupMailboxSchema.Owners]))
			{
				ADUser aduser = recipientSession.Read(entryId) as ADUser;
				if (aduser != null)
				{
					base.WriteVerbose("Group creator: {0}", new object[]
					{
						aduser.Id
					});
					return aduser;
				}
			}
			base.WriteError(new TaskException(Strings.ErrorUnableToGetCreatorFromGroupMailbox), ExchangeErrorCategory.Client, null);
			return null;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006B30 File Offset: 0x00004D30
		private string CreateGroup(string displayName, string alias, string description, bool isPublic, AADClient aadClient)
		{
			string text = null;
			try
			{
				base.WriteVerbose("Creating UnifiedGroup for group {0}", new object[]
				{
					alias
				});
				text = aadClient.CreateGroup(displayName, alias, description, isPublic);
				base.WriteVerbose("UnifiedGroup created successfully, ObjectId={0}", new object[]
				{
					text
				});
			}
			catch (AADException ex)
			{
				base.WriteVerbose("CreateGroup failed with exception: {0}", new object[]
				{
					ex
				});
				base.WriteError(new TaskException(Strings.ErrorUnableToCreateUnifiedGroup), base.GetErrorCategory(ex), null);
			}
			return text;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006BC4 File Offset: 0x00004DC4
		private void ReplicateMailboxAssociation(ADUser groupMailbox)
		{
			this.PopulateADCache(groupMailbox);
			this.CallMailboxAssociationReplicationAssistant(groupMailbox);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006BD4 File Offset: 0x00004DD4
		private void PopulateADCache(ADUser groupMailbox)
		{
			Uri backEndWebServicesUrl = BackEndLocator.GetBackEndWebServicesUrl(groupMailbox);
			UpdateGroupMailboxBase updateGroupMailboxBase = new UpdateGroupMailboxViaEWS(groupMailbox, null, backEndWebServicesUrl, (GroupMailboxConfigurationActionType)0, null, null, null);
			updateGroupMailboxBase.Execute();
			if (updateGroupMailboxBase.Error != null)
			{
				base.WriteVerbose("UpdateGroupMailbox error: {0}", new object[]
				{
					updateGroupMailboxBase.Error
				});
				this.WriteWarning(Strings.WarningUnableToUpdateUserMailboxes);
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006C34 File Offset: 0x00004E34
		private void CallMailboxAssociationReplicationAssistant(ADUser groupMailbox)
		{
			Exception ex = null;
			try
			{
				ActiveManager activeManagerInstance = ActiveManager.GetActiveManagerInstance();
				DatabaseLocationInfo serverForDatabase = activeManagerInstance.GetServerForDatabase(groupMailbox.Database.ObjectGuid);
				base.WriteVerbose("Group mailbox database is on server {0}", new object[]
				{
					serverForDatabase.ServerFqdn
				});
				using (AssistantsRpcClient assistantsRpcClient = new AssistantsRpcClient(serverForDatabase.ServerFqdn))
				{
					assistantsRpcClient.StartWithParams("MailboxAssociationReplicationAssistant", groupMailbox.ExchangeGuid, groupMailbox.Database.ObjectGuid, string.Empty);
				}
				base.WriteVerbose("Started update of the user mailboxes for the new ExternalDirectoryObjectId", new object[0]);
			}
			catch (RpcException ex2)
			{
				ex = ex2;
			}
			catch (LocalizedException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				base.WriteVerbose("Failed to call RPC to MailboxAssociationReplicationAssistant due the following exception: {0}", new object[]
				{
					ex
				});
				this.WriteWarning(Strings.WarningUnableToUpdateUserMailboxes);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006D30 File Offset: 0x00004F30
		private void WriteGroup(AADClient aadClient, string groupObjectId)
		{
			try
			{
				Group group = aadClient.GetGroup(groupObjectId, true);
				aadClient.Service.LoadProperty(group, "members");
				aadClient.Service.LoadProperty(group, "owners");
				base.WriteObject(new AADGroupPresentationObject(group));
			}
			catch (AADException ex)
			{
				base.WriteVerbose("GetGroup failed with exception: {0}", new object[]
				{
					ex
				});
				this.WriteWarning(Strings.ErrorUnableToGetUnifiedGroup);
			}
		}

		// Token: 0x0400007D RID: 125
		private IRecipientSession recipientSession;

		// Token: 0x0400007E RID: 126
		private ADUser executingUser;

		// Token: 0x0400007F RID: 127
		private string[] owners;

		// Token: 0x04000080 RID: 128
		private string[] members;
	}
}
