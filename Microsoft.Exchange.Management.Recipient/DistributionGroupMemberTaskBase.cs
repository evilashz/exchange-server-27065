using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Approval.Applications;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Approval;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000018 RID: 24
	public abstract class DistributionGroupMemberTaskBase<TMemberIdentity> : RecipientObjectActionTask<DistributionGroupIdParameter, ADGroup> where TMemberIdentity : IIdentityParameter, new()
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000075D6 File Offset: 0x000057D6
		// (set) Token: 0x06000146 RID: 326 RVA: 0x000075ED File Offset: 0x000057ED
		[ValidateNotNull]
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public TMemberIdentity Member
		{
			get
			{
				return (TMemberIdentity)((object)base.Fields["Member"]);
			}
			set
			{
				base.Fields["Member"] = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00007605 File Offset: 0x00005805
		protected bool IsSelfMemberAction
		{
			get
			{
				return this.isSelfMemberAction;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000760D File Offset: 0x0000580D
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00007633 File Offset: 0x00005833
		[Parameter(Mandatory = false)]
		public SwitchParameter BypassSecurityGroupManagerCheck
		{
			get
			{
				return (SwitchParameter)(base.Fields["BypassSecurityGroupManagerCheck"] ?? false);
			}
			set
			{
				base.Fields["BypassSecurityGroupManagerCheck"] = value;
			}
		}

		// Token: 0x0600014A RID: 330
		protected abstract void PerformGroupMemberAction();

		// Token: 0x0600014B RID: 331
		protected abstract void GroupMemberCheck(ADRecipient requester);

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600014C RID: 332
		protected abstract MemberUpdateType MemberUpdateRestriction { get; }

		// Token: 0x0600014D RID: 333
		protected abstract TMemberIdentity IdentityFactory(ADObjectId id);

		// Token: 0x0600014E RID: 334
		protected abstract void WriteApprovalRequiredWarning(string messageId);

		// Token: 0x0600014F RID: 335
		protected abstract LocalizedString ApprovalMessageSubject();

		// Token: 0x06000150 RID: 336
		protected abstract void WriteClosedUpdateError();

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000151 RID: 337
		protected abstract string ApprovalAction { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000764B File Offset: 0x0000584B
		internal IRecipientSession GlobalCatalogRBACSession
		{
			get
			{
				return this.globalCatalogRBACSession;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007654 File Offset: 0x00005854
		protected override IConfigurable PrepareDataObject()
		{
			ADGroup adgroup = (ADGroup)base.PrepareDataObject();
			if (adgroup != null && (adgroup.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox || adgroup.RecipientTypeDetails == RecipientTypeDetails.RemoteGroupMailbox))
			{
				base.WriteError(new RecipientTaskException(Strings.NotAValidDistributionGroup), ExchangeErrorCategory.Client, this.Identity.ToString());
			}
			return adgroup;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000076B4 File Offset: 0x000058B4
		protected override IConfigDataProvider CreateSession()
		{
			this.globalCatalogRBACSession = DistributionGroupMemberTaskBase<TMemberIdentity>.CreateGlobalCatalogRBACSession(base.DomainController, base.SessionSettings);
			IDirectorySession directorySession = (IDirectorySession)base.CreateSession();
			directorySession.ExclusiveLdapAttributes = DistributionGroupMemberTaskBase<TMemberIdentity>.exclusiveLdapAttributes;
			return (IConfigDataProvider)directorySession;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000076F8 File Offset: 0x000058F8
		internal static IRecipientSession CreateGlobalCatalogRBACSession(Fqdn domainController, ADSessionSettings sessionSettings)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(domainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 188, "CreateGlobalCatalogRBACSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\DistributionList\\DistributionGroupMemberTaskBase.cs");
			if (!tenantOrRootOrgRecipientSession.IsReadConnectionAvailable())
			{
				tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 198, "CreateGlobalCatalogRBACSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\DistributionList\\DistributionGroupMemberTaskBase.cs");
			}
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = true;
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000775C File Offset: 0x0000595C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.approvalRequired = false;
			this.requester = null;
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			ADObjectId executingUserAndCheckGroupOwnership = DistributionGroupMemberTaskBase<TMemberIdentity>.GetExecutingUserAndCheckGroupOwnership(this, (IDirectorySession)base.DataSession, base.TenantGlobalCatalogSession, this.DataObject, this.BypassSecurityGroupManagerCheck);
			if (base.Fields.IsModified("Member"))
			{
				this.isSelfMemberAction = false;
				this.PerformGroupMemberAction();
			}
			else
			{
				if (executingUserAndCheckGroupOwnership == null)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorCannotDetermineRequester), ErrorCategory.InvalidArgument, this.DataObject.Identity);
				}
				this.Member = this.IdentityFactory(executingUserAndCheckGroupOwnership);
				this.requester = (ADRecipient)base.GetDataObject<ADRecipient>(this.Member, base.TenantGlobalCatalogSession, this.DataObject.OrganizationId.OrganizationalUnit, new LocalizedString?(Strings.ErrorRecipientNotFound(executingUserAndCheckGroupOwnership.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(executingUserAndCheckGroupOwnership.ToString())));
				if (this.MemberUpdateRestriction == MemberUpdateType.Open || (this.DataObject.ManagedBy != null && this.DataObject.ManagedBy.Contains(this.requester.Id)) || this.IsExecutingUserGroupAdmin())
				{
					this.PerformGroupMemberAction();
				}
				else
				{
					switch (this.MemberUpdateRestriction)
					{
					case MemberUpdateType.Closed:
						this.WriteClosedUpdateError();
						break;
					case MemberUpdateType.ApprovalRequired:
						this.approvalRequired = true;
						break;
					}
					this.GroupMemberCheck(this.requester);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000078F4 File Offset: 0x00005AF4
		internal static ADObjectId GetExecutingUserAndCheckGroupOwnership(Task task, IDirectorySession dataSession, IRecipientSession gcSession, ADGroup group, bool bypassSecurityGroupManagerCheck)
		{
			ADScopeException ex2 = null;
			ADObjectId adobjectId = null;
			bool flag = task.TryGetExecutingUserId(out adobjectId);
			LocalizedException ex = null;
			ExchangeErrorCategory errCategory = ExchangeErrorCategory.Client;
			object targetObj = null;
			bool flag2 = false;
			if (flag && adobjectId != null && !dataSession.TryVerifyIsWithinScopes(group, true, out ex2))
			{
				task.WriteVerbose(Strings.VerboseDGOwnershipDeepSearch(adobjectId.ToString(), group.Identity.ToString()));
				RecipientTaskHelper.ValidateUserIsGroupManager(adobjectId, group, delegate(LocalizedException exception, ExchangeErrorCategory category, object target)
				{
					ex = exception;
					errCategory = category;
					targetObj = target;
				}, true, gcSession);
				flag2 = true;
				group.IsExecutingUserGroupOwner = (ex == null);
			}
			if (RecipientType.MailUniversalSecurityGroup == group.RecipientType && !bypassSecurityGroupManagerCheck)
			{
				if (!flag)
				{
					task.WriteError(new RecipientTaskException(Strings.ErrorExecutingUserOutOfTargetOrg(task.MyInvocation.MyCommand.Name)), ExchangeErrorCategory.Client, group.Identity.ToString());
				}
				if (!flag2)
				{
					task.WriteVerbose(Strings.VerboseDGOwnershipDeepSearch(adobjectId.ToString(), group.Identity.ToString()));
					RecipientTaskHelper.ValidateUserIsGroupManager(adobjectId, group, new Task.ErrorLoggerDelegate(task.WriteError), true, gcSession);
					group.IsExecutingUserGroupOwner = true;
				}
				else if (ex != null)
				{
					task.WriteError(ex, errCategory, targetObj);
				}
			}
			group.propertyBag.ResetChangeTracking(ADGroupSchema.IsExecutingUserGroupOwner);
			return adobjectId;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007A4C File Offset: 0x00005C4C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.approvalRequired)
			{
				string text = this.SubmitApprovalRequest(this.ApprovalAction);
				this.WriteApprovalRequiredWarning(text);
				string text2 = string.Format("Approval Request Message ID: {0}", text);
				base.AdditionalLogData = text2;
				CmdletLogger.SafeAppendGenericInfo(base.CurrentTaskContext.UniqueId, "DGMemberTaskBase.Log", text2);
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00007AF8 File Offset: 0x00005CF8
		protected string SubmitApprovalRequest(string command)
		{
			if (this.DataObject.ArbitrationMailbox == null)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorArbitrationMailboxNotSet(this.Identity.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
			ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(new MailboxIdParameter(this.DataObject.ArbitrationMailbox), base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(this.DataObject.ArbitrationMailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(this.DataObject.ArbitrationMailbox.ToString())));
			if (adrecipient.RecipientTypeDetails != RecipientTypeDetails.ArbitrationMailbox)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorInvalidArbitrationMbxType(adrecipient.Identity.ToString())), ErrorCategory.InvalidData, this.DataObject.Identity);
			}
			if (!adrecipient.IsValid)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorInvalidArbitrationMailbox(adrecipient.Identity.ToString())), ErrorCategory.InvalidData, this.DataObject.Identity);
			}
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ApprovalApplicationSchema.ArbitrationMailboxesBacklink, adrecipient.Id),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, "AutoGroup")
			});
			ApprovalApplication[] array = this.ConfigurationSession.Find<ApprovalApplication>(null, QueryScope.SubTree, filter, null, 1);
			if (array == null || array.Length == 0)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorNoAutoGroupApprovalApplicationInOrg(adrecipient.OrganizationId.ToString())), ErrorCategory.InvalidData, null);
			}
			Guid policyTag = Guid.Empty;
			int? retentionPeriod = null;
			RetentionPolicyTag retentionPolicyTag = null;
			if (array[0].ELCRetentionPolicyTag != null)
			{
				this.ConfigurationSession.SessionSettings.IsSharedConfigChecked = true;
				retentionPolicyTag = this.ConfigurationSession.Read<RetentionPolicyTag>(array[0].ELCRetentionPolicyTag);
			}
			else
			{
				IConfigurationSession configurationSession = SharedConfiguration.CreateScopedToSharedConfigADSession(adrecipient.OrganizationId);
				if (configurationSession != null)
				{
					IList<RetentionPolicyTag> defaultRetentionPolicyTag = ApprovalUtils.GetDefaultRetentionPolicyTag(configurationSession, ApprovalApplicationId.AutoGroup, 1);
					if (defaultRetentionPolicyTag != null && defaultRetentionPolicyTag.Count > 0)
					{
						retentionPolicyTag = defaultRetentionPolicyTag[0];
					}
				}
			}
			if (retentionPolicyTag == null)
			{
				this.WriteWarning(Strings.WarningRetentionPolicyTagNotFoundForApproval(array[0].Name, adrecipient.OrganizationId.ToString()));
			}
			else
			{
				policyTag = retentionPolicyTag.RetentionId;
				EnhancedTimeSpan? timeSpanForRetention = retentionPolicyTag.TimeSpanForRetention;
				retentionPeriod = ((timeSpanForRetention != null) ? new int?((int)timeSpanForRetention.Value.TotalDays) : null);
			}
			Result<ADRawEntry>[] array2 = base.TenantGlobalCatalogSession.ReadMultiple(this.DataObject.ManagedBy.ToArray(), new PropertyDefinition[]
			{
				ADObjectSchema.Id,
				ADRecipientSchema.PrimarySmtpAddress,
				ADUserSchema.Languages,
				ADRecipientSchema.RecipientTypeDetails
			});
			ADRawEntry[] array3 = new ADRawEntry[array2.Length];
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i] = array2[i].Data;
			}
			SmtpAddress[] array4 = (from approver in array3
			where (RecipientTypeDetails)approver[ADRecipientSchema.RecipientTypeDetails] != RecipientTypeDetails.MailUniversalSecurityGroup && (RecipientTypeDetails)approver[ADRecipientSchema.RecipientTypeDetails] != RecipientTypeDetails.UniversalSecurityGroup
			select (SmtpAddress)approver[ADRecipientSchema.PrimarySmtpAddress]).ToArray<SmtpAddress>();
			if (array4.Length == 0)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorApproversNotSet(this.Identity.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
			base.WriteVerbose(Strings.VerboseStartToSubmitApprovalRequest(this.DataObject.DisplayName, string.Join<SmtpAddress>(", ", array4)));
			CultureInfo moderatorCommonCulture = this.SelectApproverCommonCulture(array3);
			string text = ApprovalProcessor.SubmitRequest(0, adrecipient.PrimarySmtpAddress, this.requester.PrimarySmtpAddress, array4, moderatorCommonCulture, policyTag, retentionPeriod, this.ApprovalMessageSubject(), AutoGroupApplication.BuildApprovalData(command, this.DataObject.Id));
			base.WriteVerbose(Strings.VerboseApprovalRequestSubmitted(this.DataObject.DisplayName, text));
			return text;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00007EA8 File Offset: 0x000060A8
		private bool IsExecutingUserGroupAdmin()
		{
			return base.ExchangeRunspaceConfig != null && base.ExchangeRunspaceConfig.IsCmdletAllowedInScope(base.MyInvocation.MyCommand.Name, new string[]
			{
				"Identity",
				"Member"
			}, this.DataObject, ScopeLocation.RecipientWrite);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007EFC File Offset: 0x000060FC
		private CultureInfo SelectApproverCommonCulture(ADRawEntry[] approvers)
		{
			int num = 0;
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
			int culture = 0;
			int num2 = 0;
			int culture2 = 0;
			int num3 = 0;
			string value = string.Empty;
			CultureInfo cultureInfo = null;
			int i = 0;
			while (i < approvers.Length)
			{
				MultiValuedProperty<CultureInfo> multiValuedProperty = (MultiValuedProperty<CultureInfo>)approvers[i][ADUserSchema.Languages];
				if (multiValuedProperty.Count > 0)
				{
					if (cultureInfo == null)
					{
						cultureInfo = multiValuedProperty[0];
					}
					List<string> list = new List<string>();
					using (MultiValuedProperty<CultureInfo>.Enumerator enumerator = multiValuedProperty.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							CultureInfo cultureInfo2 = enumerator.Current;
							if (dictionary.ContainsKey(cultureInfo2.LCID))
							{
								Dictionary<int, int> dictionary3;
								int lcid;
								(dictionary3 = dictionary)[lcid = cultureInfo2.LCID] = dictionary3[lcid] + 1;
							}
							else
							{
								dictionary.Add(cultureInfo2.LCID, 1);
							}
							if (num2 < dictionary[cultureInfo2.LCID])
							{
								culture = cultureInfo2.LCID;
								num2 = dictionary[cultureInfo2.LCID];
							}
							if (!list.Contains(cultureInfo2.TwoLetterISOLanguageName))
							{
								list.Add(cultureInfo2.TwoLetterISOLanguageName);
								if (dictionary2.ContainsKey(cultureInfo2.TwoLetterISOLanguageName))
								{
									Dictionary<string, int> dictionary4;
									string twoLetterISOLanguageName;
									(dictionary4 = dictionary2)[twoLetterISOLanguageName = cultureInfo2.TwoLetterISOLanguageName] = dictionary4[twoLetterISOLanguageName] + 1;
								}
								else
								{
									dictionary2.Add(cultureInfo2.TwoLetterISOLanguageName, 1);
								}
								if (num3 < dictionary2[cultureInfo2.TwoLetterISOLanguageName])
								{
									num3 = dictionary2[cultureInfo2.TwoLetterISOLanguageName];
									if (!cultureInfo2.TwoLetterISOLanguageName.Equals(value, StringComparison.InvariantCultureIgnoreCase))
									{
										value = cultureInfo2.TwoLetterISOLanguageName;
										culture2 = cultureInfo2.LCID;
									}
								}
							}
						}
						goto IL_19F;
					}
					goto IL_19B;
				}
				goto IL_19B;
				IL_19F:
				i++;
				continue;
				IL_19B:
				num++;
				goto IL_19F;
			}
			if (num == approvers.Length)
			{
				return null;
			}
			if ((double)num2 - (double)(approvers.Length - num) / 2.0 >= 0.0)
			{
				return new CultureInfo(culture);
			}
			if ((double)num3 - (double)(approvers.Length - num) / 2.0 >= 0.0)
			{
				return new CultureInfo(culture2);
			}
			MultiValuedProperty<CultureInfo> multiValuedProperty2 = (MultiValuedProperty<CultureInfo>)this.requester[ADUserSchema.Languages];
			foreach (CultureInfo cultureInfo3 in multiValuedProperty2)
			{
				foreach (int num4 in dictionary.Keys)
				{
					if (num4 == cultureInfo3.LCID)
					{
						return cultureInfo3;
					}
				}
			}
			foreach (CultureInfo cultureInfo4 in multiValuedProperty2)
			{
				foreach (string value2 in dictionary2.Keys)
				{
					if (cultureInfo4.TwoLetterISOLanguageName.Equals(value2, StringComparison.InvariantCultureIgnoreCase))
					{
						return cultureInfo4;
					}
				}
			}
			return cultureInfo;
		}

		// Token: 0x0400002A RID: 42
		private bool approvalRequired;

		// Token: 0x0400002B RID: 43
		private bool isSelfMemberAction = true;

		// Token: 0x0400002C RID: 44
		private static readonly string[] exclusiveLdapAttributes = new string[]
		{
			ADGroupSchema.Members.LdapDisplayName
		};

		// Token: 0x0400002D RID: 45
		protected ADRecipient requester;

		// Token: 0x0400002E RID: 46
		private IRecipientSession globalCatalogRBACSession;
	}
}
