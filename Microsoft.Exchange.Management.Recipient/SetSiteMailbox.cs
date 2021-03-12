using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000E3 RID: 227
	[Cmdlet("Set", "SiteMailbox", DefaultParameterSetName = "TeamMailboxITPro", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
	public sealed class SetSiteMailbox : SetRecipientObjectTask<RecipientIdParameter, TeamMailbox, ADUser>
	{
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0003F1C2 File Offset: 0x0003D3C2
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x0003F1CA File Offset: 0x0003D3CA
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public new RecipientIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0003F1D3 File Offset: 0x0003D3D3
		// (set) Token: 0x060011C8 RID: 4552 RVA: 0x0003F1EA File Offset: 0x0003D3EA
		[Parameter]
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

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0003F1FD File Offset: 0x0003D3FD
		// (set) Token: 0x060011CA RID: 4554 RVA: 0x0003F214 File Offset: 0x0003D414
		[Parameter]
		public bool Active
		{
			get
			{
				return (bool)base.Fields["Active"];
			}
			set
			{
				base.Fields["Active"] = value;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x0003F22C File Offset: 0x0003D42C
		// (set) Token: 0x060011CC RID: 4556 RVA: 0x0003F243 File Offset: 0x0003D443
		[Parameter]
		public bool RemoveDuplicateMessages
		{
			get
			{
				return (bool)base.Fields["RemoveDuplicateMessages"];
			}
			set
			{
				base.Fields["RemoveDuplicateMessages"] = value;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x0003F25B File Offset: 0x0003D45B
		// (set) Token: 0x060011CE RID: 4558 RVA: 0x0003F272 File Offset: 0x0003D472
		[Parameter]
		public RecipientIdParameter[] Members
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["Members"];
			}
			set
			{
				base.Fields["Members"] = (value ?? new RecipientIdParameter[0]);
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0003F28F File Offset: 0x0003D48F
		// (set) Token: 0x060011D0 RID: 4560 RVA: 0x0003F2A6 File Offset: 0x0003D4A6
		[ValidateNotNullOrEmpty]
		[Parameter]
		public RecipientIdParameter[] Owners
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["Owners"];
			}
			set
			{
				base.Fields["Owners"] = (value ?? new RecipientIdParameter[0]);
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x0003F2C3 File Offset: 0x0003D4C3
		// (set) Token: 0x060011D2 RID: 4562 RVA: 0x0003F2DC File Offset: 0x0003D4DC
		[Parameter(Mandatory = false)]
		public Uri SharePointUrl
		{
			get
			{
				return (Uri)base.Fields["SharePointUrl"];
			}
			set
			{
				if (value != null && (!value.IsAbsoluteUri || (!(value.Scheme == Uri.UriSchemeHttps) && !(value.Scheme == Uri.UriSchemeHttp))))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorTeamMailboxSharePointUrl), ExchangeErrorCategory.Client, this.Identity);
				}
				base.Fields["SharePointUrl"] = value;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x0003F34A File Offset: 0x0003D54A
		// (set) Token: 0x060011D4 RID: 4564 RVA: 0x0003F361 File Offset: 0x0003D561
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxIW")]
		public bool ShowInMyClient
		{
			get
			{
				return (bool)base.Fields["ShowInMyClient"];
			}
			set
			{
				base.Fields["ShowInMyClient"] = value;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x0003F379 File Offset: 0x0003D579
		// (set) Token: 0x060011D6 RID: 4566 RVA: 0x0003F39F File Offset: 0x0003D59F
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x0003F3B7 File Offset: 0x0003D5B7
		// (set) Token: 0x060011D8 RID: 4568 RVA: 0x0003F3BF File Offset: 0x0003D5BF
		private new SwitchParameter IgnoreDefaultScope { get; set; }

		// Token: 0x060011D9 RID: 4569 RVA: 0x0003F3C8 File Offset: 0x0003D5C8
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.ParameterSetName == "TeamMailboxIW" && !base.TryGetExecutingUserId(out this.executingUserId))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorTeamMailboxCannotIdentifyTheUser), ExchangeErrorCategory.Client, this.Identity);
			}
			bool flag = TeamMailbox.IsRemoteTeamMailbox(this.DataObject);
			if ((!TeamMailbox.IsLocalTeamMailbox(this.DataObject) && !flag) || TeamMailbox.IsPendingDeleteSiteMailbox(this.DataObject))
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), ExchangeErrorCategory.Client, this.Identity);
			}
			if (flag && (base.Fields.IsModified("DisplayName") || base.Fields.IsModified("Owners") || base.Fields.IsModified("Members") || base.Fields.IsModified("SharePointUrl") || base.Fields.IsModified("Active")))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorRemoteTeamMailboxIsNotChangeable(this.DataObject.DisplayName)), ExchangeErrorCategory.Client, this.Identity);
			}
			this.tm = TeamMailbox.FromDataObject(this.DataObject);
			this.teamMailboxHelper = new TeamMailboxHelper(this.tm, base.ExchangeRunspaceConfig.ExecutingUser, base.ExchangeRunspaceConfig.ExecutingUserOrganizationId, (IRecipientSession)base.DataSession, new TeamMailboxGetDataObject<ADUser>(base.GetDataObject<ADUser>));
			this.membershipHelper = new TeamMailboxMembershipHelper(this.tm, (IRecipientSession)base.DataSession);
			if (base.ParameterSetName == "TeamMailboxIW")
			{
				if (this.tm.OwnersAndMembers.Contains(this.executingUserId) && !base.Fields.IsModified("DisplayName") && !base.Fields.IsModified("Owners") && !base.Fields.IsModified("Members") && !base.Fields.IsModified("SharePointUrl") && !base.Fields.IsModified("Active") && base.Fields.IsModified("ShowInMyClient"))
				{
					this.executingUserIsMember = true;
				}
				if (!this.executingUserIsMember)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorTeamMailboxChangeByNonMember(this.DataObject.DisplayName)), ExchangeErrorCategory.Client, this.Identity);
				}
			}
			int num = base.Fields.IsModified("Owners") ? this.Owners.Length : this.tm.Owners.Count;
			num += (base.Fields.IsModified("Members") ? this.Members.Length : this.tm.Members.Count);
			if (num > 1800)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorTeamMailboxTooManyOwnersAndMembers(num, 1800)), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0003F6CC File Offset: 0x0003D8CC
		protected override void InternalProcessRecord()
		{
			IList<ADObjectId> list = null;
			IList<ADObjectId> list2 = null;
			IList<ADObjectId> list3 = null;
			IList<ADObjectId> usersToRemove = null;
			bool flag = false;
			if (!this.executingUserIsMember)
			{
				if (this.DisplayName != null && this.DisplayName != this.DataObject.DisplayName)
				{
					this.DataObject.DisplayName = this.DisplayName;
					this.changeTracking = true;
				}
				if (base.Fields.IsModified("Active"))
				{
					if (this.Active)
					{
						if (!this.tm.Active)
						{
							this.tm.ClosedTime = null;
							flag = true;
							this.changeTracking = true;
						}
					}
					else if (this.tm.Active)
					{
						this.tm.ClosedTime = new DateTime?(DateTime.UtcNow);
						flag = true;
						this.changeTracking = true;
					}
				}
				if (base.Fields.IsModified("RemoveDuplicateMessages") && this.tm.RemoveDuplicateMessages != this.RemoveDuplicateMessages)
				{
					this.tm.RemoveDuplicateMessages = this.RemoveDuplicateMessages;
					this.changeTracking = true;
				}
				if (base.Fields.IsModified("Owners") || base.Fields.IsModified("Members"))
				{
					IList<ADObjectId> list4 = this.tm.Owners;
					if (base.Fields.IsModified("Owners"))
					{
						IList<RecipientIdParameter> list5;
						IList<ADUser> list6;
						list4 = this.teamMailboxHelper.RecipientIds2ADObjectIds(this.Owners, out list5, out list6);
						if (list5 != null && list5.Count > 0)
						{
							base.WriteError(new RecipientTaskException(Strings.ErrorTeamMailboxUserNotResolved(TeamMailboxHelper.GetAggreatedIds(list5))), ExchangeErrorCategory.Client, null);
						}
						if (list6 != null && list6.Count > 0)
						{
							base.WriteError(new RecipientTaskException(Strings.ErrorTeamMailboxUserNotResolved(TeamMailboxHelper.GetAggreatedUsers(list6))), ExchangeErrorCategory.Client, null);
						}
					}
					IList<ADObjectId> userList = this.tm.Members;
					if (base.Fields.IsModified("Members"))
					{
						IList<RecipientIdParameter> list5;
						IList<ADUser> list6;
						userList = this.teamMailboxHelper.RecipientIds2ADObjectIds(this.Members, out list5, out list6);
						if (list5 != null && list5.Count > 0)
						{
							base.WriteError(new RecipientTaskException(Strings.ErrorTeamMailboxUserNotResolved(TeamMailboxHelper.GetAggreatedIds(list5))), ExchangeErrorCategory.Client, null);
						}
						if (list6 != null && list6.Count > 0)
						{
							base.WriteError(new RecipientTaskException(Strings.ErrorTeamMailboxUserNotResolved(TeamMailboxHelper.GetAggreatedUsers(list6))), ExchangeErrorCategory.Client, null);
						}
					}
					IList<ADObjectId> newUserList = TeamMailbox.MergeUsers(list4, userList);
					if (base.Fields.IsModified("Owners") && this.membershipHelper.UpdateTeamMailboxUserList(this.tm.Owners, list4, out list, out list2))
					{
						this.changeTracking = true;
					}
					if (this.membershipHelper.UpdateTeamMailboxUserList(this.tm.OwnersAndMembers, newUserList, out list3, out usersToRemove))
					{
						if (list != null && list.Count != 0)
						{
							TeamMailbox.DiffUsers(list3, list);
						}
						Exception ex = null;
						try
						{
							this.membershipHelper.SetTeamMailboxUserPermissions(list3, usersToRemove, null, true);
						}
						catch (OverflowException ex2)
						{
							ex = ex2;
						}
						catch (COMException ex3)
						{
							ex = ex3;
						}
						catch (UnauthorizedAccessException ex4)
						{
							ex = ex4;
						}
						catch (TransientException ex5)
						{
							ex = ex5;
						}
						catch (DataSourceOperationException ex6)
						{
							ex = ex6;
						}
						if (ex != null)
						{
							base.WriteError(new RecipientTaskException(Strings.ErrorSetTeamMailboxUserPermissions(this.tm.DisplayName, ex.Message)), ExchangeErrorCategory.Client, null);
						}
						try
						{
							new TeamMailboxSecurityRefresher().Refresh(this.DataObject, (IRecipientSession)base.DataSession);
						}
						catch (DatabaseNotFoundException ex7)
						{
							ex = ex7;
						}
						catch (ObjectNotFoundException ex8)
						{
							ex = ex8;
						}
						catch (FormatException ex9)
						{
							ex = ex9;
						}
						if (ex != null)
						{
							base.WriteError(new RecipientTaskException(Strings.ErrorSetTeamMailboxUserPermissions(this.tm.DisplayName, ex.Message)), ExchangeErrorCategory.Client, null);
						}
						this.changeTracking = true;
					}
				}
				if (base.Fields.IsModified("SharePointUrl"))
				{
					try
					{
						this.changeTracking = this.teamMailboxHelper.LinkSharePointSite(this.SharePointUrl, false, this.Force);
					}
					catch (RecipientTaskException exception)
					{
						base.WriteError(exception, ExchangeErrorCategory.Client, this.Identity);
					}
				}
			}
			if (base.Fields.IsModified("ShowInMyClient"))
			{
				if (!this.tm.Active)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorTeamMailboxSetShowInMyClientForClosedMailbox(this.DataObject.DisplayName)), ExchangeErrorCategory.Client, this.Identity);
				}
				bool flag2;
				Exception ex10;
				if (this.membershipHelper.SetShowInMyClient(this.executingUserId, this.ShowInMyClient, out flag2, out ex10))
				{
					this.changeTracking = true;
				}
				else if (ex10 != null)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorTeamMailboxSetShowInMyClient(this.DataObject.DisplayName, ex10.ToString())), ExchangeErrorCategory.Client, this.Identity);
				}
				else if (flag2)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorTeamMailboxMaxShowInMyClientReached(this.DataObject.DisplayName, 10)), ExchangeErrorCategory.Client, this.Identity);
				}
			}
			base.InternalProcessRecord();
			if (flag)
			{
				TeamMailboxADUserResolver.RemoveIdIfExists(this.tm.Id);
			}
			IList<Exception> list7;
			this.membershipHelper.SetShowInMyClient(list3, usersToRemove, out list7);
			foreach (Exception ex11 in list7)
			{
				this.WriteWarning(Strings.ErrorTeamMailboxResolveUser(ex11.Message));
			}
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0003FC5C File Offset: 0x0003DE5C
		protected override IConfigurable PrepareDataObject()
		{
			IConfigurable configurable = base.PrepareDataObject();
			if (!TeamMailbox.IsRemoteTeamMailbox(this.DataObject))
			{
				CmdletProxy.ThrowExceptionIfProxyIsNeeded(base.CurrentTaskContext, (ADUser)configurable, false, this.ConfirmationMessage, null);
			}
			return configurable;
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x0003FC99 File Offset: 0x0003DE99
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetTeamMailbox(this.Identity.ToString());
			}
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0003FCAB File Offset: 0x0003DEAB
		protected override bool IsObjectStateChanged()
		{
			return this.changeTracking || base.IsObjectStateChanged();
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0003FCBD File Offset: 0x0003DEBD
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return TeamMailbox.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x04000352 RID: 850
		private TeamMailbox tm;

		// Token: 0x04000353 RID: 851
		private ADObjectId executingUserId;

		// Token: 0x04000354 RID: 852
		private bool changeTracking;

		// Token: 0x04000355 RID: 853
		private bool executingUserIsMember;

		// Token: 0x04000356 RID: 854
		private TeamMailboxMembershipHelper membershipHelper;

		// Token: 0x04000357 RID: 855
		private TeamMailboxHelper teamMailboxHelper;
	}
}
