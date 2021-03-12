using System;
using System.Collections.Generic;
using System.Management.Automation;
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
	// Token: 0x020000C0 RID: 192
	[Cmdlet("Update", "SyncDistributionGroupMember", SupportsShouldProcess = true, DefaultParameterSetName = "AddOrRemove")]
	public sealed class UpdateSyncDistributionGroupMember : UpdateDistributionGroupMemberBase
	{
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x00032BE0 File Offset: 0x00030DE0
		internal override IReferenceErrorReporter ReferenceErrorReporter
		{
			get
			{
				if (this.batchReferenceErrorReporter == null)
				{
					this.batchReferenceErrorReporter = new BatchReferenceErrorReporter();
				}
				return this.batchReferenceErrorReporter;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00032BFB File Offset: 0x00030DFB
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x00032C03 File Offset: 0x00030E03
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override DistributionGroupIdParameter Identity
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

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00032C0C File Offset: 0x00030E0C
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x00032C23 File Offset: 0x00030E23
		[Parameter(Mandatory = false, ParameterSetName = "Replace")]
		public new MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>> Members
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>>)base.Fields["Members"];
			}
			set
			{
				base.Fields["Members"] = value;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00032C36 File Offset: 0x00030E36
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x00032C4D File Offset: 0x00030E4D
		[Parameter(Mandatory = false, ParameterSetName = "AddOrRemove")]
		[ValidateNotNullOrEmpty]
		public MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>> AddedMembers
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>>)base.Fields["AddedMembers"];
			}
			set
			{
				base.Fields["AddedMembers"] = value;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00032C60 File Offset: 0x00030E60
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x00032C77 File Offset: 0x00030E77
		[Parameter(Mandatory = false, ParameterSetName = "AddOrRemove")]
		[ValidateNotNullOrEmpty]
		public MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>> RemovedMembers
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>>)base.Fields["RemovedMembers"];
			}
			set
			{
				base.Fields["RemovedMembers"] = value;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00032C8A File Offset: 0x00030E8A
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x00032CA1 File Offset: 0x00030EA1
		[Parameter(Mandatory = false, ParameterSetName = "RawMembers")]
		public MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>> RawMembers
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>>)base.Fields["RawMembers"];
			}
			set
			{
				base.Fields["RawMembers"] = value;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00032CB4 File Offset: 0x00030EB4
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x00032CDA File Offset: 0x00030EDA
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeSoftDeletedObjects
		{
			get
			{
				return (SwitchParameter)(base.Fields["SoftDeletedObject"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SoftDeletedObject"] = value;
			}
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00032CF4 File Offset: 0x00030EF4
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.IncludeSoftDeletedObjects.IsPresent)
			{
				base.TenantGlobalCatalogSession.SessionSettings.IncludeSoftDeletedObjects = true;
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00032D28 File Offset: 0x00030F28
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (base.Fields.IsModified("AddedMembers") && this.AddedMembers != null && this.AddedMembers.Count > 0)
			{
				foreach (RecipientWithAdUserGroupIdParameter<RecipientIdParameter> recipientWithAdUserGroupIdParameter in this.AddedMembers)
				{
					ADObjectId memberid;
					if (!ADObjectId.TryParseDnOrGuid(recipientWithAdUserGroupIdParameter.RawIdentity, out memberid) || !MailboxTaskHelper.GroupContainsMember(this.DataObject, memberid, base.TenantGlobalCatalogSession))
					{
						MailboxTaskHelper.ValidateAndAddMember(base.TenantGlobalCatalogSession, this.DataObject, recipientWithAdUserGroupIdParameter, false, new Task.ErrorLoggerDelegate(base.WriteError), new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>));
					}
				}
			}
			if (base.Fields.IsModified("RemovedMembers") && this.RemovedMembers != null && this.RemovedMembers.Count > 0)
			{
				foreach (RecipientWithAdUserGroupIdParameter<RecipientIdParameter> recipientWithAdUserGroupIdParameter2 in this.RemovedMembers)
				{
					ADObjectId memberid2;
					if (!ADObjectId.TryParseDnOrGuid(recipientWithAdUserGroupIdParameter2.RawIdentity, out memberid2) || MailboxTaskHelper.GroupContainsMember(this.DataObject, memberid2, base.TenantGlobalCatalogSession))
					{
						MailboxTaskHelper.ValidateAndRemoveMember(base.TenantGlobalCatalogSession, this.DataObject, recipientWithAdUserGroupIdParameter2, this.Identity.RawIdentity, false, new Task.TaskErrorLoggingDelegate(base.WriteError), new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>));
					}
				}
			}
			if (this.RawMembers != null && this.RawMembers.IsChangesOnlyCopy)
			{
				this.UpdateMembersWhenRawMembersChanged();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00032EEC File Offset: 0x000310EC
		protected override bool ShouldSkipRangedAttributes()
		{
			int num = 0;
			if (base.Fields.IsModified("AddedMembers") && this.AddedMembers != null)
			{
				num += this.AddedMembers.Count;
			}
			if (base.Fields.IsModified("RemovedMembers") && this.RemovedMembers != null)
			{
				num += this.RemovedMembers.Count;
			}
			if (base.Fields.IsModified("RawMembers") && this.RawMembers != null && this.RawMembers.IsChangesOnlyCopy)
			{
				this.addedValues = this.GetChangedValues(true);
				this.removedValues = this.GetChangedValues(false);
				num += this.addedValues.Length;
				num += this.removedValues.Length;
			}
			return base.Fields.IsModified("Members") || num < 100;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00032FBC File Offset: 0x000311BC
		private void UpdateMembersWhenRawMembersChanged()
		{
			if (this.addedValues.Length > 0)
			{
				SyncTaskHelper.ResolveModifiedMultiReferenceParameter<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>>("RawMembers", "AddedMembers", this.addedValues, new GetRecipientDelegate<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>>(this.GetRecipient), this.ReferenceErrorReporter, this.recipientIdsDictionary, this.recipientsDictionary, this.parameterDictionary);
				SyncTaskHelper.ValidateModifiedMultiReferenceParameter<ADGroup>("RawMembers", "AddedMembers", this.DataObject, SyncTaskHelper.ValidateWithBaseObjectBypassADUser<ADGroup>(new ValidateRecipientWithBaseObjectDelegate<ADGroup>(MailboxTaskHelper.ValidateGroupMember)), this.ReferenceErrorReporter, this.recipientsDictionary, this.parameterDictionary);
				SyncTaskHelper.AddModifiedRecipientIds("AddedMembers", SyncDistributionGroupSchema.Members, this.DataObject, this.recipientIdsDictionary, new Func<ADGroup, ADObjectId, IConfigDataProvider, bool>(MailboxTaskHelper.GroupContainsMember), base.TenantGlobalCatalogSession);
			}
			if (this.removedValues.Length > 0)
			{
				SyncTaskHelper.ResolveModifiedMultiReferenceParameter<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>>("RawMembers", "RemovedMembers", this.removedValues, new GetRecipientDelegate<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>>(this.GetRecipient), this.ReferenceErrorReporter, this.recipientIdsDictionary, this.recipientsDictionary, this.parameterDictionary);
				SyncTaskHelper.RemoveModifiedRecipientIds("RemovedMembers", SyncDistributionGroupSchema.Members, this.DataObject, this.recipientIdsDictionary, new Func<ADGroup, ADObjectId, IConfigDataProvider, bool>(MailboxTaskHelper.GroupContainsMember), base.TenantGlobalCatalogSession);
			}
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x000330E8 File Offset: 0x000312E8
		private ADRecipient GetRecipient(RecipientWithAdUserGroupIdParameter<RecipientIdParameter> IdParameter, Task.ErrorLoggerDelegate writeError)
		{
			return (ADRecipient)base.GetDataObject<ADRecipient>(IdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(IdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(IdParameter.ToString())));
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0003312C File Offset: 0x0003132C
		private RecipientWithAdUserGroupIdParameter<RecipientIdParameter>[] GetChangedValues(bool added)
		{
			if (this.RawMembers == null)
			{
				return new RecipientWithAdUserGroupIdParameter<RecipientIdParameter>[0];
			}
			object[] array = added ? this.RawMembers.Added : this.RawMembers.Removed;
			RecipientWithAdUserGroupIdParameter<RecipientIdParameter>[] array2 = new RecipientWithAdUserGroupIdParameter<RecipientIdParameter>[array.Length];
			array.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x0400029B RID: 667
		private Dictionary<object, MultiValuedProperty<ADObjectId>> recipientIdsDictionary = new Dictionary<object, MultiValuedProperty<ADObjectId>>();

		// Token: 0x0400029C RID: 668
		private Dictionary<object, MultiValuedProperty<ADRecipient>> recipientsDictionary = new Dictionary<object, MultiValuedProperty<ADRecipient>>();

		// Token: 0x0400029D RID: 669
		private Dictionary<ADRecipient, IIdentityParameter> parameterDictionary = new Dictionary<ADRecipient, IIdentityParameter>();

		// Token: 0x0400029E RID: 670
		private BatchReferenceErrorReporter batchReferenceErrorReporter;

		// Token: 0x0400029F RID: 671
		private RecipientWithAdUserGroupIdParameter<RecipientIdParameter>[] addedValues = new RecipientWithAdUserGroupIdParameter<RecipientIdParameter>[0];

		// Token: 0x040002A0 RID: 672
		private RecipientWithAdUserGroupIdParameter<RecipientIdParameter>[] removedValues = new RecipientWithAdUserGroupIdParameter<RecipientIdParameter>[0];
	}
}
