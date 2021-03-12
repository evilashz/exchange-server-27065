﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x0200079C RID: 1948
	[Cmdlet("Set", "CalendarProcessing", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetCalendarProcessing : SetXsoObjectWithIdentityTaskBase<CalendarConfiguration>
	{
		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x06004496 RID: 17558 RVA: 0x001192EB File Offset: 0x001174EB
		// (set) Token: 0x06004497 RID: 17559 RVA: 0x00119302 File Offset: 0x00117502
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ResourceDelegates
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ResourceDelegatesTaskTag"];
			}
			set
			{
				base.Fields["ResourceDelegatesTaskTag"] = value;
			}
		}

		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x06004498 RID: 17560 RVA: 0x00119315 File Offset: 0x00117515
		// (set) Token: 0x06004499 RID: 17561 RVA: 0x0011932C File Offset: 0x0011752C
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] RequestOutOfPolicy
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["RequestOutOfPolicyTaskTag"];
			}
			set
			{
				base.Fields["RequestOutOfPolicyTaskTag"] = value;
			}
		}

		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x0600449A RID: 17562 RVA: 0x0011933F File Offset: 0x0011753F
		// (set) Token: 0x0600449B RID: 17563 RVA: 0x00119356 File Offset: 0x00117556
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] BookInPolicy
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["BookInPolicyTaskTag"];
			}
			set
			{
				base.Fields["BookInPolicyTaskTag"] = value;
			}
		}

		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x0600449C RID: 17564 RVA: 0x00119369 File Offset: 0x00117569
		// (set) Token: 0x0600449D RID: 17565 RVA: 0x00119380 File Offset: 0x00117580
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] RequestInPolicy
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["RequestInPolicyTaskTag"];
			}
			set
			{
				base.Fields["RequestInPolicyTaskTag"] = value;
			}
		}

		// Token: 0x0600449E RID: 17566 RVA: 0x00119393 File Offset: 0x00117593
		internal override IConfigDataProvider CreateXsoMailboxDataProvider(ExchangePrincipal principal, ISecurityAccessToken userToken)
		{
			return new CalendarConfigurationDataProvider(principal, "Set-CalendarProcessing");
		}

		// Token: 0x0600449F RID: 17567 RVA: 0x001193A0 File Offset: 0x001175A0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!this.DataObject.IsResource && this.Instance.AutomateProcessing == CalendarProcessingFlags.AutoAccept)
			{
				base.WriteError(new ResourceOnlyException(CalendarProcessingFlags.AutoAccept.ToString()), ErrorCategory.InvalidData, this.DataObject.Identity);
			}
			if (!this.DataObject.IsResource && this.ResourceDelegates != null)
			{
				base.WriteError(new ResourceOnlyException("ResourceDelegates"), ErrorCategory.InvalidData, this.DataObject.Identity);
			}
			if (!this.DataObject.IsResource && this.DataObject.IsModified(CalendarConfigurationSchema.AutomateProcessing))
			{
				base.WriteError(new ResourceOnlyException("AutomateProcessing"), ErrorCategory.InvalidData, this.DataObject.Identity);
			}
			if (!this.DataObject.IsResource && this.DataObject.IsModified(CalendarConfigurationSchema.AddNewRequestsTentatively))
			{
				base.WriteError(new ResourceOnlyException("AddNewRequestsTentatively"), ErrorCategory.InvalidData, this.DataObject.Identity);
			}
			if (!this.DataObject.IsResource && this.DataObject.IsModified(CalendarConfigurationSchema.ProcessExternalMeetingMessages))
			{
				base.WriteError(new ResourceOnlyException("ProcessExternalMeetingMessages"), ErrorCategory.InvalidData, this.DataObject.Identity);
			}
			base.VerifyIsWithinScopes((IRecipientSession)base.DataSession, this.DataObject, true, new DataAccessTask<ADUser>.ADObjectOutOfScopeString(Strings.ErrorCannotChangeMailboxOutOfWriteScope));
			this.MergeArraysIntoList(new MultiValuedProperty<string>[]
			{
				this.Instance.RequestOutOfPolicy,
				this.Instance.BookInPolicy,
				this.Instance.RequestInPolicy
			});
			if (base.Fields.Contains("RequestOutOfPolicyTaskTag"))
			{
				this.Instance.RequestOutOfPolicy = this.ConstructAndValidate(this.RequestOutOfPolicy);
				this.Instance.RequestOutOfPolicyLegacy = MultiValuedProperty<ADObjectId>.Empty;
			}
			if (base.Fields.Contains("BookInPolicyTaskTag"))
			{
				this.Instance.BookInPolicy = this.ConstructAndValidate(this.BookInPolicy);
				this.Instance.BookInPolicyLegacy = MultiValuedProperty<ADObjectId>.Empty;
			}
			if (base.Fields.Contains("RequestInPolicyTaskTag"))
			{
				this.Instance.RequestInPolicy = this.ConstructAndValidate(this.RequestInPolicy);
				this.Instance.RequestInPolicyLegacy = MultiValuedProperty<ADObjectId>.Empty;
			}
			if (base.Fields.Contains("ResourceDelegatesTaskTag"))
			{
				this.Instance.ResourceDelegates = this.ConstructADObjectIdAndValidate(this.ResourceDelegates);
				this.delegatesToAddPermission = this.GetNotContained<ADObjectId>(this.Instance.ResourceDelegates, this.DataObject.GrantSendOnBehalfTo);
				this.delegatesToRemovePermission = this.GetNotContained<ADObjectId>(this.DataObject.GrantSendOnBehalfTo, this.Instance.ResourceDelegates);
			}
			else
			{
				this.delegatesToAddPermission = MultiValuedProperty<ADObjectId>.Empty;
				this.delegatesToRemovePermission = MultiValuedProperty<ADObjectId>.Empty;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060044A0 RID: 17568 RVA: 0x0011966C File Offset: 0x0011786C
		protected override void StampChangesOnXsoObject(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject
			});
			CalendarConfiguration calendarConfiguration = (CalendarConfiguration)dataObject;
			this.oldAllPolicy = (calendarConfiguration.AllRequestOutOfPolicy | calendarConfiguration.AllBookInPolicy | calendarConfiguration.AllRequestInPolicy);
			MultiValuedProperty<string> multiValuedProperty = this.MergeArraysIntoList(new MultiValuedProperty<string>[]
			{
				calendarConfiguration.RequestOutOfPolicy,
				calendarConfiguration.BookInPolicy,
				calendarConfiguration.RequestInPolicy
			});
			base.StampChangesOnXsoObject(dataObject);
			this.newAllPolicy = (calendarConfiguration.AllRequestOutOfPolicy | calendarConfiguration.AllBookInPolicy | calendarConfiguration.AllRequestInPolicy);
			this.newPolicyMembers = this.MergeArraysIntoList(new MultiValuedProperty<string>[]
			{
				calendarConfiguration.RequestOutOfPolicy,
				calendarConfiguration.BookInPolicy,
				calendarConfiguration.RequestInPolicy
			});
			this.policyMembersToAddPermission = this.GetNotContained<string>(this.newPolicyMembers, multiValuedProperty);
			this.policyMembersToRemovePermission = this.GetNotContained<string>(multiValuedProperty, this.newPolicyMembers);
			TaskLogger.LogExit();
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x00119758 File Offset: 0x00117958
		protected override void SaveXsoObject(IConfigDataProvider provider, IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject
			});
			base.SaveXsoObject(provider, dataObject);
			MailboxSession mailboxSession = ((XsoMailboxDataProviderBase)provider).MailboxSession;
			this.WorkWithPermissions(mailboxSession);
			TaskLogger.LogExit();
		}

		// Token: 0x060044A2 RID: 17570 RVA: 0x00119798 File Offset: 0x00117998
		protected override CalendarConfiguration GetDefaultConfiguration()
		{
			return new CalendarConfiguration
			{
				MailboxOwnerId = this.DataObject.Id
			};
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x001197BD File Offset: 0x001179BD
		protected override bool IsKnownException(Exception exception)
		{
			return exception is ExchangeDataException || exception is StorageTransientException || exception is TextConvertersException || base.IsKnownException(exception);
		}

		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x060044A4 RID: 17572 RVA: 0x001197E0 File Offset: 0x001179E0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetAutoAcceptMailboxSettings(this.Identity.ToString());
			}
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x001197F4 File Offset: 0x001179F4
		private MultiValuedProperty<string> ConstructAndValidate(RecipientIdParameter[] idList)
		{
			MultiValuedProperty<string> multiValuedProperty = null;
			if (idList != null && idList.Length > 0)
			{
				multiValuedProperty = new MultiValuedProperty<string>();
				foreach (RecipientIdParameter recipientIdParameter in idList)
				{
					ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(recipientIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(recipientIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(recipientIdParameter.ToString())));
					RecipientTaskHelper.CheckRecipientInSameOrganizationWithDataObject(this.DataObject, adrecipient, new Task.ErrorLoggerDelegate(base.WriteError));
					multiValuedProperty.Add(adrecipient.LegacyExchangeDN);
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x00119884 File Offset: 0x00117A84
		private MultiValuedProperty<ADObjectId> ConstructADObjectIdAndValidate(RecipientIdParameter[] idList)
		{
			MultiValuedProperty<ADObjectId> multiValuedProperty = null;
			if (idList != null && idList.Length > 0)
			{
				multiValuedProperty = new MultiValuedProperty<ADObjectId>();
				foreach (RecipientIdParameter recipientIdParameter in idList)
				{
					ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(recipientIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(recipientIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(recipientIdParameter.ToString())));
					RecipientTaskHelper.CheckRecipientInSameOrganizationWithDataObject(this.DataObject, adrecipient, new Task.ErrorLoggerDelegate(base.WriteError));
					multiValuedProperty.Add(adrecipient.Id);
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x00119914 File Offset: 0x00117B14
		private void WorkWithPermissions(MailboxSession itemStore)
		{
			if (this.delegatesToAddPermission.Count > 0 || this.delegatesToRemovePermission.Count > 0 || this.policyMembersToAddPermission.Count > 0 || this.policyMembersToRemovePermission.Count > 0 || this.oldAllPolicy != this.newAllPolicy)
			{
				using (CalendarFolder calendarFolder = CalendarFolder.Bind(itemStore, DefaultFolderType.Calendar))
				{
					using (Folder folder = Folder.Create(itemStore, itemStore.GetDefaultFolderId(DefaultFolderType.Configuration), StoreObjectType.Folder, "Freebusy Data", CreateMode.OpenIfExists))
					{
						CalendarFolderPermissionSet permissionSet = calendarFolder.GetPermissionSet();
						PermissionSet permissionSet2 = folder.GetPermissionSet();
						permissionSet.DefaultPermission.FreeBusyAccess = (this.newAllPolicy ? FreeBusyAccess.Details : FreeBusyAccess.Basic);
						this.AddPermissions(permissionSet, permissionSet2);
						this.RemovePermissions(permissionSet, permissionSet2);
						FolderSaveResult folderSaveResult = folder.Save();
						if (folderSaveResult != null && folderSaveResult.OperationResult != OperationResult.Succeeded)
						{
							base.ThrowTerminatingError(new FolderSaveException(Strings.CalendarSave, folderSaveResult), ErrorCategory.InvalidOperation, null);
						}
						folderSaveResult = calendarFolder.Save();
						if (folderSaveResult != null && folderSaveResult.OperationResult != OperationResult.Succeeded)
						{
							base.ThrowTerminatingError(new FolderSaveException(Strings.CalendarSave, folderSaveResult), ErrorCategory.InvalidOperation, null);
						}
					}
				}
				if (this.delegatesToAddPermission.Count > 0 || this.delegatesToRemovePermission.Count > 0)
				{
					this.DataObject.GrantSendOnBehalfTo = this.Instance.ResourceDelegates;
					base.DataSession.Save(this.DataObject);
				}
			}
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x00119AA0 File Offset: 0x00117CA0
		private MultiValuedProperty<string> MergeArraysIntoList(params MultiValuedProperty<string>[] propertyArrayArray)
		{
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			foreach (MultiValuedProperty<string> multiValuedProperty2 in propertyArrayArray)
			{
				if (multiValuedProperty2 != null)
				{
					using (MultiValuedProperty<string>.Enumerator enumerator = multiValuedProperty2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string value = enumerator.Current;
							if (multiValuedProperty.Find((string b) => string.Equals(value, b)) == null)
							{
								multiValuedProperty.Add(value);
							}
						}
					}
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x060044A9 RID: 17577 RVA: 0x00119B60 File Offset: 0x00117D60
		private MultiValuedProperty<ADObjectId> MergeArraysIntoList(params MultiValuedProperty<ADObjectId>[] propertyArrayArray)
		{
			MultiValuedProperty<ADObjectId> multiValuedProperty = new MultiValuedProperty<ADObjectId>();
			foreach (MultiValuedProperty<ADObjectId> multiValuedProperty2 in propertyArrayArray)
			{
				if (multiValuedProperty2 != null)
				{
					using (MultiValuedProperty<ADObjectId>.Enumerator enumerator = multiValuedProperty2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ADObjectId value = enumerator.Current;
							if (multiValuedProperty.Find((ADObjectId b) => ADObjectId.Equals(value, b)) == null)
							{
								multiValuedProperty.Add(value);
							}
						}
					}
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x060044AA RID: 17578 RVA: 0x00119C08 File Offset: 0x00117E08
		private MultiValuedProperty<T> MergeArraysIntoList<T>(params MultiValuedProperty<T>[] propertyArrayArray)
		{
			MultiValuedProperty<T> multiValuedProperty = new MultiValuedProperty<T>();
			foreach (MultiValuedProperty<T> multiValuedProperty2 in propertyArrayArray)
			{
				if (multiValuedProperty2 != null)
				{
					multiValuedProperty = (MultiValuedProperty<T>)multiValuedProperty.Union(multiValuedProperty2);
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x060044AB RID: 17579 RVA: 0x00119C40 File Offset: 0x00117E40
		private MultiValuedProperty<T> GetNotContained<T>(MultiValuedProperty<T> aList, MultiValuedProperty<T> bList)
		{
			MultiValuedProperty<T> multiValuedProperty = new MultiValuedProperty<T>();
			foreach (T item in aList.Except(bList))
			{
				multiValuedProperty.Add(item);
			}
			return multiValuedProperty;
		}

		// Token: 0x060044AC RID: 17580 RVA: 0x00119C98 File Offset: 0x00117E98
		private void AddPermissions(CalendarFolderPermissionSet permissions, PermissionSet fbPermissions)
		{
			List<ADRecipient> list = new List<ADRecipient>(this.policyMembersToAddPermission.Count + this.delegatesToAddPermission.Count);
			foreach (string legacyExchangeDN in this.policyMembersToAddPermission)
			{
				list.Add(base.TenantGlobalCatalogSession.FindByLegacyExchangeDN(legacyExchangeDN));
			}
			foreach (ADObjectId entryId in this.delegatesToAddPermission)
			{
				list.Add(base.TenantGlobalCatalogSession.Read(entryId));
			}
			foreach (ADRecipient adrecipient in list)
			{
				if (adrecipient != null && adrecipient.IsValidSecurityPrincipal && (adrecipient.RecipientType == RecipientType.UserMailbox || adrecipient.RecipientType == RecipientType.MailUser || adrecipient.RecipientType == RecipientType.MailUniversalSecurityGroup))
				{
					PermissionSecurityPrincipal securityPrincipal = new PermissionSecurityPrincipal(adrecipient);
					CalendarFolderPermission calendarFolderPermission = permissions.GetEntry(securityPrincipal);
					bool flag = this.delegatesToAddPermission.Contains(adrecipient.Id);
					if (calendarFolderPermission == null)
					{
						calendarFolderPermission = permissions.AddEntry(securityPrincipal, PermissionLevel.None);
					}
					calendarFolderPermission.FreeBusyAccess = FreeBusyAccess.Details;
					if (flag)
					{
						calendarFolderPermission.PermissionLevel = PermissionLevel.Editor;
						calendarFolderPermission.DeleteItems = ItemPermissionScope.AllItems;
						calendarFolderPermission.IsFolderVisible = true;
					}
				}
			}
		}

		// Token: 0x060044AD RID: 17581 RVA: 0x00119E20 File Offset: 0x00118020
		private void RemovePermissions(CalendarFolderPermissionSet permissions, PermissionSet fbPermissions)
		{
			List<ADRecipient> list = new List<ADRecipient>(this.policyMembersToRemovePermission.Count + this.delegatesToRemovePermission.Count);
			foreach (string legacyExchangeDN in this.policyMembersToRemovePermission)
			{
				list.Add(base.TenantGlobalCatalogSession.FindByLegacyExchangeDN(legacyExchangeDN));
			}
			foreach (ADObjectId entryId in this.delegatesToRemovePermission)
			{
				list.Add(base.TenantGlobalCatalogSession.Read(entryId));
			}
			foreach (ADRecipient adrecipient in list)
			{
				if (adrecipient != null && adrecipient.IsValidSecurityPrincipal)
				{
					PermissionSecurityPrincipal securityPrincipal = new PermissionSecurityPrincipal(adrecipient);
					CalendarFolderPermission entry = permissions.GetEntry(securityPrincipal);
					bool flag = this.delegatesToRemovePermission.Contains(adrecipient.Id);
					bool flag2 = this.policyMembersToRemovePermission.Contains(adrecipient.LegacyExchangeDN);
					if (entry != null)
					{
						if (flag)
						{
							if (!this.newPolicyMembers.Contains(adrecipient.LegacyExchangeDN))
							{
								permissions.RemoveEntry(securityPrincipal);
							}
							else
							{
								entry.PermissionLevel = PermissionLevel.None;
							}
						}
						if (flag2 && entry.PermissionLevel != PermissionLevel.Editor)
						{
							entry.FreeBusyAccess = FreeBusyAccess.None;
							if (entry.PermissionLevel == PermissionLevel.None)
							{
								permissions.RemoveEntry(securityPrincipal);
							}
						}
					}
					if (flag)
					{
						Permission entry2 = fbPermissions.GetEntry(securityPrincipal);
						if (entry2 != null)
						{
							fbPermissions.RemoveEntry(securityPrincipal);
						}
					}
				}
			}
		}

		// Token: 0x04002A6C RID: 10860
		private const string RequestOutOfPolicyTaskTag = "RequestOutOfPolicyTaskTag";

		// Token: 0x04002A6D RID: 10861
		private const string BookInPolicyTaskTag = "BookInPolicyTaskTag";

		// Token: 0x04002A6E RID: 10862
		private const string RequestInPolicyTaskTag = "RequestInPolicyTaskTag";

		// Token: 0x04002A6F RID: 10863
		private const string ResourceDelegatesTaskTag = "ResourceDelegatesTaskTag";

		// Token: 0x04002A70 RID: 10864
		private bool oldAllPolicy;

		// Token: 0x04002A71 RID: 10865
		private bool newAllPolicy;

		// Token: 0x04002A72 RID: 10866
		private MultiValuedProperty<string> newPolicyMembers = MultiValuedProperty<string>.Empty;

		// Token: 0x04002A73 RID: 10867
		private MultiValuedProperty<string> policyMembersToAddPermission = MultiValuedProperty<string>.Empty;

		// Token: 0x04002A74 RID: 10868
		private MultiValuedProperty<string> policyMembersToRemovePermission = MultiValuedProperty<string>.Empty;

		// Token: 0x04002A75 RID: 10869
		private MultiValuedProperty<ADObjectId> delegatesToAddPermission = MultiValuedProperty<ADObjectId>.Empty;

		// Token: 0x04002A76 RID: 10870
		private MultiValuedProperty<ADObjectId> delegatesToRemovePermission = MultiValuedProperty<ADObjectId>.Empty;
	}
}
