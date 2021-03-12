using System;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000057 RID: 87
	[Cmdlet("Set", "GroupMailbox", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.Medium)]
	public sealed class SetGroupMailbox : SetRecipientObjectTask<RecipientIdParameter, GroupMailbox, ADUser>
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00017B7B File Offset: 0x00015D7B
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x00017B92 File Offset: 0x00015D92
		[Parameter(Mandatory = false)]
		public string Name
		{
			get
			{
				return (string)base.Fields[ADObjectSchema.Name];
			}
			set
			{
				base.Fields[ADObjectSchema.Name] = value;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00017BA5 File Offset: 0x00015DA5
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x00017BBC File Offset: 0x00015DBC
		[Parameter(Mandatory = false)]
		public string DisplayName
		{
			get
			{
				return (string)base.Fields[ADRecipientSchema.DisplayName];
			}
			set
			{
				base.Fields[ADRecipientSchema.DisplayName] = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00017BCF File Offset: 0x00015DCF
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x00017BE6 File Offset: 0x00015DE6
		[Parameter(Mandatory = false)]
		public string Description
		{
			get
			{
				return (string)base.Fields[ADRecipientSchema.Description];
			}
			set
			{
				base.Fields[ADRecipientSchema.Description] = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00017BF9 File Offset: 0x00015DF9
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x00017C10 File Offset: 0x00015E10
		[Parameter(Mandatory = false)]
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

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00017C23 File Offset: 0x00015E23
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x00017C3A File Offset: 0x00015E3A
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] Owners
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[ADUserSchema.Owners];
			}
			set
			{
				base.Fields[ADUserSchema.Owners] = (value ?? new RecipientIdParameter[0]);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00017C57 File Offset: 0x00015E57
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x00017C6E File Offset: 0x00015E6E
		[Parameter(Mandatory = false)]
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

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00017C81 File Offset: 0x00015E81
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x00017C98 File Offset: 0x00015E98
		[Parameter(Mandatory = false)]
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

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00017CAB File Offset: 0x00015EAB
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x00017CC2 File Offset: 0x00015EC2
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] AddedMembers
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["AddedMembers"];
			}
			set
			{
				base.Fields["AddedMembers"] = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00017CD5 File Offset: 0x00015ED5
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x00017CEC File Offset: 0x00015EEC
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] RemovedMembers
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["RemovedMembers"];
			}
			set
			{
				base.Fields["RemovedMembers"] = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00017CFF File Offset: 0x00015EFF
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x00017D16 File Offset: 0x00015F16
		[Parameter(Mandatory = false)]
		public Uri SharePointUrl
		{
			get
			{
				return (Uri)base.Fields[ADMailboxRecipientSchema.SharePointUrl];
			}
			set
			{
				base.Fields[ADMailboxRecipientSchema.SharePointUrl] = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00017D29 File Offset: 0x00015F29
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x00017D40 File Offset: 0x00015F40
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> SharePointResources
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields[ADMailboxRecipientSchema.SharePointResources];
			}
			set
			{
				base.Fields[ADMailboxRecipientSchema.SharePointResources] = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00017D53 File Offset: 0x00015F53
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00017D6A File Offset: 0x00015F6A
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public ModernGroupTypeInfo SwitchToGroupType
		{
			get
			{
				return (ModernGroupTypeInfo)base.Fields[ADRecipientSchema.ModernGroupType];
			}
			set
			{
				base.Fields[ADRecipientSchema.ModernGroupType] = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00017D82 File Offset: 0x00015F82
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x00017D99 File Offset: 0x00015F99
		[Parameter(Mandatory = false)]
		public bool RequireSenderAuthenticationEnabled
		{
			get
			{
				return (bool)base.Fields["RequireSenderAuthenticationEnabled"];
			}
			set
			{
				base.Fields["RequireSenderAuthenticationEnabled"] = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00017DB1 File Offset: 0x00015FB1
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x00017DC8 File Offset: 0x00015FC8
		[Parameter(Mandatory = false)]
		public string YammerGroupEmailAddress
		{
			get
			{
				return (string)base.Fields["YammerGroupEmailAddress"];
			}
			set
			{
				base.Fields["YammerGroupEmailAddress"] = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00017DDB File Offset: 0x00015FDB
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x00017DF2 File Offset: 0x00015FF2
		[Parameter(Mandatory = false)]
		public RecipientIdType RecipientIdType
		{
			get
			{
				return (RecipientIdType)base.Fields["RecipientIdType"];
			}
			set
			{
				base.Fields["RecipientIdType"] = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00017E0A File Offset: 0x0001600A
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x00017E30 File Offset: 0x00016030
		[Parameter(Mandatory = false)]
		public SwitchParameter FromSyncClient
		{
			get
			{
				return (SwitchParameter)(base.Fields["FromSyncClient"] ?? false);
			}
			set
			{
				base.Fields["FromSyncClient"] = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00017E48 File Offset: 0x00016048
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x00017E5F File Offset: 0x0001605F
		[Parameter(Mandatory = false)]
		private RecipientIdParameter[] PublicToGroups
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[ADMailboxRecipientSchema.DelegateListLink];
			}
			set
			{
				base.Fields[ADMailboxRecipientSchema.DelegateListLink] = (value ?? new RecipientIdParameter[0]);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00017E7C File Offset: 0x0001607C
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x00017E93 File Offset: 0x00016093
		[Parameter(Mandatory = false)]
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)base.Fields[GroupMailboxSchema.PrimarySmtpAddress];
			}
			set
			{
				base.Fields[GroupMailboxSchema.PrimarySmtpAddress] = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00017EAB File Offset: 0x000160AB
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x00017EC2 File Offset: 0x000160C2
		[Parameter(Mandatory = false)]
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return (ProxyAddressCollection)base.Fields[GroupMailboxSchema.EmailAddresses];
			}
			set
			{
				base.Fields[GroupMailboxSchema.EmailAddresses] = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00017ED5 File Offset: 0x000160D5
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x00017EEC File Offset: 0x000160EC
		[Parameter(Mandatory = false)]
		public string ExternalDirectoryObjectId
		{
			get
			{
				return (string)base.Fields[ADRecipientSchema.ExternalDirectoryObjectId];
			}
			set
			{
				base.Fields[ADRecipientSchema.ExternalDirectoryObjectId] = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00017EFF File Offset: 0x000160FF
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x00017F25 File Offset: 0x00016125
		[Parameter(Mandatory = false)]
		public SwitchParameter ForcePublishExternalResources
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForcePublishExternalResources"] ?? false);
			}
			set
			{
				base.Fields["ForcePublishExternalResources"] = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00017F3D File Offset: 0x0001613D
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x00017F54 File Offset: 0x00016154
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<GroupMailboxConfigurationActionType> ConfigurationActions
		{
			get
			{
				return (MultiValuedProperty<GroupMailboxConfigurationActionType>)base.Fields["ConfigurationActions"];
			}
			set
			{
				base.Fields["ConfigurationActions"] = value;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00017F67 File Offset: 0x00016167
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x00017F7E File Offset: 0x0001617E
		[Parameter(Mandatory = false)]
		public CultureInfo Language
		{
			get
			{
				return (CultureInfo)base.Fields["Language"];
			}
			set
			{
				base.Fields["Language"] = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00017F91 File Offset: 0x00016191
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x00017FA8 File Offset: 0x000161A8
		[Parameter(Mandatory = false)]
		public SwitchParameter AutoSubscribeNewGroupMembers
		{
			get
			{
				return (SwitchParameter)base.Fields["AutoSubscribeNewGroupMembers"];
			}
			set
			{
				base.Fields["AutoSubscribeNewGroupMembers"] = value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00017FC0 File Offset: 0x000161C0
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x00017FD7 File Offset: 0x000161D7
		[Parameter(Mandatory = false)]
		public int PermissionsVersion
		{
			get
			{
				return (int)base.Fields["PermissionsVersion"];
			}
			set
			{
				base.Fields["PermissionsVersion"] = value;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00017FEF File Offset: 0x000161EF
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x00017FF7 File Offset: 0x000161F7
		private new SwitchParameter IgnoreDefaultScope { get; set; }

		// Token: 0x06000581 RID: 1409 RVA: 0x00018000 File Offset: 0x00016200
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.Fields.IsModified(ADMailboxRecipientSchema.SharePointUrl))
			{
				Uri sharePointUrl = this.SharePointUrl;
				if (sharePointUrl != null && (!sharePointUrl.IsAbsoluteUri || (!(sharePointUrl.Scheme == Uri.UriSchemeHttps) && !(sharePointUrl.Scheme == Uri.UriSchemeHttp))))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorTeamMailboxSharePointUrl), ExchangeErrorCategory.Client, null);
				}
			}
			if (base.Fields.IsChanged(ADMailboxRecipientSchema.DelegateListLink) && this.DataObject.ModernGroupType != ModernGroupObjectType.Public)
			{
				base.WriteError(new GroupMailboxInvalidOperationException(Strings.ErrorInvalidGroupTypeForPublicToGroups), ExchangeErrorCategory.Client, null);
			}
			if (base.Fields.IsChanged(ADUserSchema.Owners) && this.Owners != null && this.Owners.Length == 0)
			{
				base.WriteError(new GroupMailboxInvalidOperationException(Strings.ErrorSetGroupMailboxNoOwners), ExchangeErrorCategory.Client, null);
			}
			if (base.Fields.IsChanged("YammerGroupEmailAddress") && !string.IsNullOrEmpty(this.YammerGroupEmailAddress) && !ProxyAddressBase.IsAddressStringValid(this.YammerGroupEmailAddress))
			{
				base.WriteError(new GroupMailboxInvalidOperationException(Strings.ErrorSetGroupMailboxInvalidYammerEmailAddress(this.YammerGroupEmailAddress)), ExchangeErrorCategory.Client, null);
			}
			if (base.Fields.IsChanged(GroupMailboxSchema.EmailAddresses) && base.Fields.IsChanged(GroupMailboxSchema.PrimarySmtpAddress))
			{
				base.ThrowTerminatingError(new RecipientTaskException(Strings.ErrorPrimarySmtpAndEmailAddressesSpecified), ExchangeErrorCategory.Client, null);
			}
			if (base.Fields.IsChanged(ADRecipientSchema.ExternalDirectoryObjectId) && !string.IsNullOrEmpty(this.ExternalDirectoryObjectId) && !RecipientTaskHelper.IsPropertyValueUnique(base.TenantGlobalCatalogSession, ADScope.Empty, null, new ADPropertyDefinition[]
			{
				ADRecipientSchema.ExternalDirectoryObjectId
			}, ADRecipientSchema.ExternalDirectoryObjectId, this.ExternalDirectoryObjectId, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), null, ExchangeErrorCategory.Client))
			{
				base.ThrowTerminatingError(new RecipientTaskException(Strings.ErrorExternalDirectoryObjectIdNotUnique(this.ExternalDirectoryObjectId)), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x000181E4 File Offset: 0x000163E4
		private bool MembersChanged
		{
			get
			{
				return (base.Fields.IsModified("AddedMembers") && this.AddedMembers != null && this.AddedMembers.Length > 0) || (base.Fields.IsModified("RemovedMembers") && this.RemovedMembers != null && this.RemovedMembers.Length > 0);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00018240 File Offset: 0x00016440
		private bool OwnersChanged
		{
			get
			{
				return (base.Fields.IsChanged("AddOwners") && this.AddOwners != null && this.AddOwners.Length > 0) || (base.Fields.IsChanged("RemoveOwners") && this.RemoveOwners != null && this.RemoveOwners.Length > 0) || (base.Fields.IsChanged(ADUserSchema.Owners) && this.Owners != null && this.Owners.Length > 0);
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x000182C0 File Offset: 0x000164C0
		internal bool RequireSenderAuthenticationEnabledChanged
		{
			get
			{
				return base.Fields.IsChanged("RequireSenderAuthenticationEnabled");
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000182EC File Offset: 0x000164EC
		protected override void InternalProcessRecord()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.DataSession;
			this.groupMailboxContext = new GroupMailboxContext(this.DataObject, base.CurrentOrganizationId, recipientSession, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADGroup>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.ErrorLoggerDelegate(base.WriteError));
			if (this.ExecutingUser != null)
			{
				this.groupMailboxContext.SetExecutingUser(this.ExecutingUser);
				this.executingUserIsOwner = this.DataObject.Owners.Any((ADObjectId ownerId) => ADObjectId.Equals(this.groupMailboxContext.ExecutingUser.Id, ownerId));
			}
			else
			{
				this.executingUserIsOwner = true;
			}
			if (base.Fields.IsChanged(ADObjectSchema.Name))
			{
				this.ThrowIfNotOwner("Name");
				this.DataObject.Name = (string)base.Fields[ADObjectSchema.Name];
			}
			if (base.Fields.IsChanged(ADRecipientSchema.DisplayName))
			{
				this.ThrowIfNotOwner("DisplayName");
				this.DataObject.DisplayName = (string)base.Fields[ADRecipientSchema.DisplayName];
			}
			if (base.Fields.IsChanged(ADRecipientSchema.Description))
			{
				this.ThrowIfNotOwner("Description");
				this.DataObject.Description.Clear();
				this.DataObject.Description.Add((string)base.Fields[ADRecipientSchema.Description]);
			}
			bool flag = false;
			if (base.Fields.IsChanged(ADUserSchema.Owners) && this.Owners != null)
			{
				this.ThrowIfNotOwner("Owners");
				flag |= this.groupMailboxContext.SetOwners(this.Owners);
			}
			if (base.Fields.IsModified("AddOwners") && this.AddOwners != null)
			{
				this.ThrowIfNotOwner("AddOwners");
				flag |= this.groupMailboxContext.AddOwners(this.AddOwners);
			}
			if (base.Fields.IsModified("RemoveOwners") && this.RemoveOwners != null)
			{
				this.ThrowIfNotOwner("RemoveOwners");
				flag |= this.groupMailboxContext.RemoveOwners(this.RemoveOwners);
			}
			if (base.Fields.IsChanged(ADMailboxRecipientSchema.DelegateListLink))
			{
				this.ThrowIfNotOwner("PublicToGroups");
				this.DataObject.DelegateListLink.Clear();
				this.groupMailboxContext.AddPublicToGroups(this.PublicToGroups);
			}
			if (base.Fields.IsChanged(ADRecipientSchema.ModernGroupType))
			{
				this.DataObject.ModernGroupType = (ModernGroupObjectType)base.Fields[ADRecipientSchema.ModernGroupType];
			}
			if (base.Fields.IsChanged("RequireSenderAuthenticationEnabled"))
			{
				this.ThrowIfNotOwner("RequireSenderAuthenticationEnabled");
				this.DataObject.RequireAllSendersAreAuthenticated = this.RequireSenderAuthenticationEnabled;
			}
			if (base.Fields.IsChanged("YammerGroupEmailAddress"))
			{
				this.DataObject.YammerGroupAddress = this.YammerGroupEmailAddress;
			}
			if (base.Fields.IsChanged("AutoSubscribeNewGroupMembers"))
			{
				this.ThrowIfNotOwner("AutoSubscribeNewGroupMembers");
				this.DataObject.AutoSubscribeNewGroupMembers = this.AutoSubscribeNewGroupMembers;
			}
			if (base.Fields.IsChanged("Language") && this.Language != null)
			{
				this.DataObject.Languages.Clear();
				this.DataObject.Languages.Add(this.Language);
			}
			if (this.MembersChanged)
			{
				this.AuthorizeAddedAndRemovedMembers(this.groupMailboxContext.ExecutingUser);
				this.groupMailboxContext.AddAndRemoveMembers(this.AddedMembers, this.RemovedMembers);
			}
			if (base.Fields.IsModified(ADMailboxRecipientSchema.SharePointUrl))
			{
				this.ThrowIfNotOwner("SharePointUrl");
				if (this.DataObject.SharePointResources == null)
				{
					this.DataObject.SharePointResources = new MultiValuedProperty<string>();
				}
				else
				{
					foreach (string text in this.DataObject.SharePointResources)
					{
						if (text.StartsWith("SiteUrl=", StringComparison.OrdinalIgnoreCase))
						{
							this.DataObject.SharePointResources.Remove(text);
							break;
						}
					}
				}
				if (this.SharePointUrl != null)
				{
					this.DataObject.SharePointResources.Add("SiteUrl=" + this.SharePointUrl);
				}
			}
			if (base.Fields.IsModified(ADMailboxRecipientSchema.SharePointResources))
			{
				this.ThrowIfNotOwner("SharePointResources");
				this.DataObject.SharePointResources = this.SharePointResources;
				this.DataObject.SharePointUrl = null;
			}
			if (base.Fields.IsModified("PermissionsVersion"))
			{
				this.groupMailboxContext.SetPermissionsVersion(this.PermissionsVersion);
			}
			GroupMailboxConfigurationActionType groupMailboxConfigurationActionType = (GroupMailboxConfigurationActionType)0;
			if (base.Fields.IsModified("ConfigurationActions") && this.ConfigurationActions != null)
			{
				foreach (GroupMailboxConfigurationActionType groupMailboxConfigurationActionType2 in this.ConfigurationActions)
				{
					groupMailboxConfigurationActionType |= groupMailboxConfigurationActionType2;
				}
			}
			Exception ex;
			ExchangeErrorCategory? exchangeErrorCategory;
			this.groupMailboxContext.SetGroupMailbox(groupMailboxConfigurationActionType, out ex, out exchangeErrorCategory);
			if (ex != null)
			{
				base.WriteError(new GroupMailboxFailedToLogonException(Strings.ErrorUnableToLogonGroupMailbox(this.DataObject.ExchangeGuid, string.Empty, recipientSession.LastUsedDc, ex.Message)), exchangeErrorCategory.GetValueOrDefault(ExchangeErrorCategory.ServerTransient), null);
			}
			bool flag2 = false;
			if (!this.FromSyncClient)
			{
				if (base.Fields.IsChanged(GroupMailboxSchema.PrimarySmtpAddress))
				{
					this.DataObject.PrimarySmtpAddress = this.PrimarySmtpAddress;
					flag2 = true;
				}
				if (base.Fields.IsChanged(GroupMailboxSchema.EmailAddresses))
				{
					this.DataObject.EmailAddresses = this.EmailAddresses;
					flag2 = true;
				}
			}
			else if (base.Fields.IsChanged(GroupMailboxSchema.EmailAddresses))
			{
				foreach (ProxyAddress proxyAddress in this.EmailAddresses)
				{
					if (!this.DataObject.EmailAddresses.Contains(proxyAddress))
					{
						if (proxyAddress.IsPrimaryAddress && proxyAddress is SmtpProxyAddress)
						{
							this.DataObject.EmailAddresses.Add(proxyAddress.ToSecondary());
						}
						else
						{
							this.DataObject.EmailAddresses.Add(proxyAddress);
						}
						flag2 = true;
					}
				}
			}
			if (flag2 && this.DataObject.EmailAddressPolicyEnabled)
			{
				this.DataObject.EmailAddressPolicyEnabled = false;
			}
			if (base.Fields.IsModified(ADRecipientSchema.ExternalDirectoryObjectId))
			{
				this.DataObject.ExternalDirectoryObjectId = this.ExternalDirectoryObjectId;
			}
			base.InternalProcessRecord();
			this.DataObject = recipientSession.FindADUserByObjectId(this.DataObject.ObjectId);
			if (flag)
			{
				this.groupMailboxContext.RefreshStoreCache();
			}
			if (!this.DataObject.GroupMailboxExternalResourcesSet || this.ForcePublishExternalResources)
			{
				this.groupMailboxContext.SetExternalResources(this.FromSyncClient);
			}
			this.groupMailboxContext.EnsureGroupIsInDirectoryCache("SetGroupMailbox.InternalProcessRecord");
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00018A1C File Offset: 0x00016C1C
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new SetGroupMailboxTaskModuleFactory();
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00018A23 File Offset: 0x00016C23
		private void ThrowIfNotOwner(string parameterName)
		{
			if (!this.executingUserIsOwner)
			{
				base.WriteError(new GroupMailboxNotAuthorizedException(Strings.ErrorNotAuthorizedForParameter(parameterName)), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00018A44 File Offset: 0x00016C44
		private void AuthorizeAddedAndRemovedMembers(ADRawEntry executingUser)
		{
			if (!this.executingUserIsOwner)
			{
				if (this.DataObject.ModernGroupType == ModernGroupObjectType.Private)
				{
					if (this.ContainsOtherUser(this.AddedMembers, executingUser))
					{
						base.WriteError(new GroupMailboxNotAuthorizedException(Strings.ErrorSetGroupMailboxAddMembersOtherUser), ExchangeErrorCategory.Client, null);
					}
					else if (this.AddedMembers != null && this.AddedMembers.Length > 0)
					{
						base.WriteError(new GroupMailboxNotAuthorizedException(Strings.ErrorNotAuthorizedForParameter("AddedMembers")), ExchangeErrorCategory.Client, null);
					}
				}
				if (this.ContainsOtherUser(this.RemovedMembers, executingUser))
				{
					base.WriteError(new GroupMailboxNotAuthorizedException(Strings.ErrorSetGroupMailboxRemoveMembersOtherUser), ExchangeErrorCategory.Client, null);
				}
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00018AE4 File Offset: 0x00016CE4
		private bool ContainsOtherUser(RecipientIdParameter[] members, ADRawEntry executingUser)
		{
			if (members == null || executingUser == null)
			{
				return false;
			}
			foreach (RecipientIdParameter id in members)
			{
				Exception ex;
				ADUser aduser = this.groupMailboxContext.ResolveUser(id, out ex);
				if (ex == null && !aduser.Id.Equals(executingUser.Id))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00018B41 File Offset: 0x00016D41
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return GroupMailbox.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x04000167 RID: 359
		private const string ParameterExecutingUser = "ExecutingUser";

		// Token: 0x04000168 RID: 360
		private const string ParameterAddedMembers = "AddedMembers";

		// Token: 0x04000169 RID: 361
		private const string ParameterRemovedMembers = "RemovedMembers";

		// Token: 0x0400016A RID: 362
		private const string ParameterRequireSenderAuthenticationEnabled = "RequireSenderAuthenticationEnabled";

		// Token: 0x0400016B RID: 363
		private const string ParameterYammerGroupEmailAddress = "YammerGroupEmailAddress";

		// Token: 0x0400016C RID: 364
		private const string ParameterAddOwners = "AddOwners";

		// Token: 0x0400016D RID: 365
		private const string ParameterRemoveOwners = "RemoveOwners";

		// Token: 0x0400016E RID: 366
		private const string ParameterRecipientIdType = "RecipientIdType";

		// Token: 0x0400016F RID: 367
		private const string ParameterFromSyncClient = "FromSyncClient";

		// Token: 0x04000170 RID: 368
		private const string ParameterForcePublishExternalResources = "ForcePublishExternalResources";

		// Token: 0x04000171 RID: 369
		private const string ParameterConfigurationActions = "ConfigurationActions";

		// Token: 0x04000172 RID: 370
		private const string ParameterAutoSubscribeNewGroupMembers = "AutoSubscribeNewGroupMembers";

		// Token: 0x04000173 RID: 371
		private const string ParameterPermissionsVersion = "PermissionsVersion";

		// Token: 0x04000174 RID: 372
		private const string ParameterLanguage = "Language";

		// Token: 0x04000175 RID: 373
		private GroupMailboxContext groupMailboxContext;

		// Token: 0x04000176 RID: 374
		private bool executingUserIsOwner;
	}
}
