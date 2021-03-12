using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x02000673 RID: 1651
	[Cmdlet("Update", "RoleGroupMember", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class UpdateRoleGroupMember : RoleGroupMemberTaskBase
	{
		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x000F68E5 File Offset: 0x000F4AE5
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageUpdateRoleGroupMember(this.Identity.ToString(), RoleGroupCommon.NamesFromObjects(this.Members));
			}
		}

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x06003A63 RID: 14947 RVA: 0x000F6902 File Offset: 0x000F4B02
		// (set) Token: 0x06003A64 RID: 14948 RVA: 0x000F6919 File Offset: 0x000F4B19
		[AllowNull]
		[Parameter]
		public MultiValuedProperty<SecurityPrincipalIdParameter> Members
		{
			get
			{
				return (MultiValuedProperty<SecurityPrincipalIdParameter>)base.Fields[ADGroupSchema.Members];
			}
			set
			{
				base.Fields[ADGroupSchema.Members] = value;
			}
		}

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x06003A65 RID: 14949 RVA: 0x000F692C File Offset: 0x000F4B2C
		private new SecurityPrincipalIdParameter Member
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x06003A66 RID: 14950 RVA: 0x000F692F File Offset: 0x000F4B2F
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

		// Token: 0x06003A67 RID: 14951 RVA: 0x000F694C File Offset: 0x000F4B4C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			bool flag = false;
			if (base.Fields.IsModified(ADGroupSchema.Members))
			{
				if (this.DataObject.RoleGroupType == RoleGroupType.Linked)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorLinkedRoleGroupCannotHaveMembers), (ErrorCategory)1000, null);
				}
				List<ADObjectId> list = null;
				SecurityPrincipalIdParameter[] parameters = this.GetChangedValues(true);
				SecurityPrincipalIdParameter[] changedValues = this.GetChangedValues(false);
				if (this.Members == null || !this.Members.IsChangesOnlyCopy)
				{
					flag = true;
					if (this.Members != null)
					{
						parameters = this.Members.ToArray();
					}
					list = this.DataObject.Members.ToList<ADObjectId>();
					this.DataObject.Members = new MultiValuedProperty<ADObjectId>();
				}
				SyncTaskHelper.ResolveModifiedMultiReferenceParameter<SecurityPrincipalIdParameter>("Members", "AddedMembers", parameters, new GetRecipientDelegate<SecurityPrincipalIdParameter>(this.GetRecipient), this.ReferenceErrorReporter, this.recipientIdsDictionary, this.recipientsDictionary, this.parameterDictionary);
				SyncTaskHelper.ValidateModifiedMultiReferenceParameter<ADGroup>("Members", "AddedMembers", this.DataObject, new ValidateRecipientWithBaseObjectDelegate<ADGroup>(MailboxTaskHelper.ValidateGroupMember), this.ReferenceErrorReporter, this.recipientsDictionary, this.parameterDictionary);
				SyncTaskHelper.AddModifiedRecipientIds("AddedMembers", SyncDistributionGroupSchema.Members, this.DataObject, this.recipientIdsDictionary);
				if (flag)
				{
					MultiValuedProperty<ADObjectId> multiValuedProperty = new MultiValuedProperty<ADObjectId>();
					foreach (ADObjectId item in list)
					{
						if (!this.DataObject.Members.Contains(item))
						{
							multiValuedProperty.Add(item);
						}
					}
					this.recipientIdsDictionary["RemovedMembers"] = multiValuedProperty;
				}
				else
				{
					SyncTaskHelper.ResolveModifiedMultiReferenceParameter<SecurityPrincipalIdParameter>("Members", "RemovedMembers", changedValues, new GetRecipientDelegate<SecurityPrincipalIdParameter>(this.GetRecipient), this.ReferenceErrorReporter, this.recipientIdsDictionary, this.recipientsDictionary, this.parameterDictionary);
					SyncTaskHelper.RemoveModifiedRecipientIds("RemovedMembers", SyncDistributionGroupSchema.Members, this.DataObject, this.recipientIdsDictionary);
				}
			}
			MailboxTaskHelper.ValidateAddedMembers(base.TenantGlobalCatalogSession, this.DataObject, new Task.ErrorLoggerDelegate(base.WriteError), new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>));
			if (this.recipientIdsDictionary.ContainsKey("RemovedMembers") && this.recipientIdsDictionary["RemovedMembers"].Count > 0)
			{
				RoleAssignmentsGlobalConstraints roleAssignmentsGlobalConstraints = new RoleAssignmentsGlobalConstraints(this.ConfigurationSession, base.TenantGlobalCatalogSession, new Task.ErrorLoggerDelegate(base.WriteError));
				roleAssignmentsGlobalConstraints.ValidateIsSafeToRemoveRoleGroupMember(this.DataObject, new List<ADObjectId>(this.recipientIdsDictionary["RemovedMembers"]));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003A68 RID: 14952 RVA: 0x000F6BE0 File Offset: 0x000F4DE0
		protected override void PerformGroupMemberAction()
		{
		}

		// Token: 0x06003A69 RID: 14953 RVA: 0x000F6BE4 File Offset: 0x000F4DE4
		internal ADRecipient GetRecipient(SecurityPrincipalIdParameter securityPrincipalIdParameter, Task.ErrorLoggerDelegate writeError)
		{
			return (ADRecipient)base.GetDataObject<ADRecipient>(securityPrincipalIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(securityPrincipalIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(securityPrincipalIdParameter.ToString())));
		}

		// Token: 0x06003A6A RID: 14954 RVA: 0x000F6C28 File Offset: 0x000F4E28
		private SecurityPrincipalIdParameter[] GetChangedValues(bool added)
		{
			if (this.Members == null)
			{
				return new SecurityPrincipalIdParameter[0];
			}
			object[] array = added ? this.Members.Added : this.Members.Removed;
			SecurityPrincipalIdParameter[] array2 = new SecurityPrincipalIdParameter[array.Length];
			array.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x04002650 RID: 9808
		private static object[] emptyObjectArray = new object[0];

		// Token: 0x04002651 RID: 9809
		private Dictionary<object, MultiValuedProperty<ADObjectId>> recipientIdsDictionary = new Dictionary<object, MultiValuedProperty<ADObjectId>>();

		// Token: 0x04002652 RID: 9810
		private Dictionary<object, MultiValuedProperty<ADRecipient>> recipientsDictionary = new Dictionary<object, MultiValuedProperty<ADRecipient>>();

		// Token: 0x04002653 RID: 9811
		private Dictionary<ADRecipient, IIdentityParameter> parameterDictionary = new Dictionary<ADRecipient, IIdentityParameter>();

		// Token: 0x04002654 RID: 9812
		private BatchReferenceErrorReporter batchReferenceErrorReporter;
	}
}
