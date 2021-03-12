using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000086 RID: 134
	[Serializable]
	public class MailboxAuditLogSearch : AuditLogSearchBase
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00011524 File Offset: 0x0000F724
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<MailboxAuditLogSearchSchema>();
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0001152B File Offset: 0x0000F72B
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x0001153D File Offset: 0x0000F73D
		public MultiValuedProperty<ADObjectId> Mailboxes
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailboxAuditLogSearchSchema.MailboxIds];
			}
			set
			{
				this[MailboxAuditLogSearchSchema.MailboxIds] = value;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0001154B File Offset: 0x0000F74B
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x0001155D File Offset: 0x0000F75D
		public MultiValuedProperty<string> LogonTypes
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxAuditLogSearchSchema.LogonTypeStrings];
			}
			set
			{
				this[MailboxAuditLogSearchSchema.LogonTypeStrings] = value;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0001156B File Offset: 0x0000F76B
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0001157D File Offset: 0x0000F77D
		public MultiValuedProperty<string> Operations
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxAuditLogSearchSchema.Operations];
			}
			set
			{
				this[MailboxAuditLogSearchSchema.Operations] = value;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0001158B File Offset: 0x0000F78B
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0001159D File Offset: 0x0000F79D
		public bool ShowDetails
		{
			get
			{
				return (bool)this[MailboxAuditLogSearchSchema.ShowDetails];
			}
			set
			{
				this[MailboxAuditLogSearchSchema.ShowDetails] = value;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x000115B0 File Offset: 0x0000F7B0
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x000115B8 File Offset: 0x0000F7B8
		internal MultiValuedProperty<AuditScopes> LogonTypesUserInput { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x000115C1 File Offset: 0x0000F7C1
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x000115C9 File Offset: 0x0000F7C9
		internal MultiValuedProperty<MailboxAuditOperations> OperationsUserInput { get; set; }

		// Token: 0x0600043C RID: 1084 RVA: 0x000115D4 File Offset: 0x0000F7D4
		internal override void Initialize(AuditLogSearchItemBase item)
		{
			MailboxAuditLogSearchItem mailboxAuditLogSearchItem = (MailboxAuditLogSearchItem)item;
			base.Initialize(item);
			this.Mailboxes = mailboxAuditLogSearchItem.MailboxIds;
			this.LogonTypes = mailboxAuditLogSearchItem.LogonTypeStrings;
			this.Operations = mailboxAuditLogSearchItem.Operations;
			this.ShowDetails = mailboxAuditLogSearchItem.ShowDetails;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00011620 File Offset: 0x0000F820
		internal override void Initialize(AuditLogSearchBase item)
		{
			MailboxAuditLogSearch mailboxAuditLogSearch = (MailboxAuditLogSearch)item;
			base.Initialize(item);
			this.Mailboxes = mailboxAuditLogSearch.Mailboxes;
			this.LogonTypes = mailboxAuditLogSearch.LogonTypes;
			this.Operations = mailboxAuditLogSearch.Operations;
			this.ShowDetails = (mailboxAuditLogSearch.ShowDetails ?? false);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00011680 File Offset: 0x0000F880
		internal void Validate(Task.TaskErrorLoggingDelegate writeError)
		{
			this.LogonTypes = new MultiValuedProperty<string>();
			if (this.LogonTypesUserInput == null)
			{
				this.LogonTypes.Add("Admin");
				this.LogonTypes.Add("Delegate");
				if (this.ShowDetails)
				{
					this.LogonTypes.Add("Owner");
				}
			}
			else
			{
				if (!this.ShowDetails && base.ExternalAccess != null && base.ExternalAccess.Value && this.LogonTypesUserInput.Count == 1 && (this.LogonTypesUserInput[0] & AuditScopes.Delegate) == AuditScopes.Delegate)
				{
					writeError(new ArgumentException(Strings.ErrorInvalidLogonType), ErrorCategory.InvalidArgument, null);
				}
				foreach (AuditScopes auditScopes in this.LogonTypesUserInput)
				{
					if ((auditScopes & AuditScopes.Admin) == AuditScopes.Admin)
					{
						this.LogonTypes.Add("Admin");
					}
					if ((auditScopes & AuditScopes.Delegate) == AuditScopes.Delegate)
					{
						this.LogonTypes.Add("Delegate");
					}
					if ((auditScopes & AuditScopes.Owner) == AuditScopes.Owner)
					{
						if (!this.ShowDetails)
						{
							writeError(new ArgumentException(Strings.ErrorInvalidMailboxAuditLogSearchCriteria), ErrorCategory.InvalidArgument, null);
						}
						else
						{
							this.LogonTypes.Add("Owner");
						}
					}
				}
			}
			if (this.OperationsUserInput != null)
			{
				if (!this.LogonTypes.Contains("Owner") && this.OperationsUserInput.Contains(MailboxAuditOperations.MailboxLogin))
				{
					writeError(new ArgumentException(Strings.ErrorInvalidOperation), ErrorCategory.InvalidArgument, null);
				}
				foreach (MailboxAuditOperations mailboxAuditOperations in this.OperationsUserInput)
				{
					if (mailboxAuditOperations != MailboxAuditOperations.None)
					{
						this.Operations.Add(mailboxAuditOperations.ToString());
					}
				}
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001187C File Offset: 0x0000FA7C
		internal QueryFilter GetRecipientFilter()
		{
			QueryFilter recipientTypeDetailsFilter = RecipientIdParameter.GetRecipientTypeDetailsFilter(MailboxAuditLogSearch.SupportedRecipientTypes);
			List<QueryFilter> list = new List<QueryFilter>();
			DateTime value = base.EndDateUtc.Value;
			DateTime value2 = base.StartDateUtc.Value;
			if (base.ExternalAccess == null)
			{
				if (this.LogonTypes != null && this.LogonTypes.Contains("Admin"))
				{
					QueryFilter item = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADRecipientSchema.AuditLastAdminAccess, value2);
					list.Add(item);
				}
				if (this.LogonTypes != null && this.LogonTypes.Contains("Delegate"))
				{
					QueryFilter item2 = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADRecipientSchema.AuditLastDelegateAccess, value2);
					list.Add(item2);
				}
				if (this.LogonTypes == null || this.LogonTypes.Count != 1 || !this.LogonTypes[0].Equals("Delegate"))
				{
					QueryFilter item3 = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADRecipientSchema.AuditLastExternalAccess, value2);
					list.Add(item3);
				}
			}
			else if (base.ExternalAccess.Value)
			{
				QueryFilter item4 = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADRecipientSchema.AuditLastExternalAccess, value2);
				list.Add(item4);
			}
			else
			{
				if (this.LogonTypes != null && this.LogonTypes.Contains("Admin"))
				{
					QueryFilter item5 = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADRecipientSchema.AuditLastAdminAccess, value2);
					list.Add(item5);
				}
				if (this.LogonTypes != null && this.LogonTypes.Contains("Delegate"))
				{
					QueryFilter item6 = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADRecipientSchema.AuditLastDelegateAccess, value2);
					list.Add(item6);
				}
			}
			QueryFilter queryFilter = QueryFilter.OrTogether(list.ToArray());
			base.QueryComplexity = list.Count;
			return new AndFilter(new QueryFilter[]
			{
				recipientTypeDetailsFilter,
				queryFilter
			});
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00011A54 File Offset: 0x0000FC54
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString());
			stringBuilder.AppendLine();
			AuditLogSearchBase.AppendStringSearchTerm(stringBuilder, "LogonTypes", this.LogonTypes);
			AuditLogSearchBase.AppendStringSearchTerm(stringBuilder, "Operations", this.Operations);
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("ShowDetails={0}", this.ShowDetails);
			return stringBuilder.ToString();
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00011ABC File Offset: 0x0000FCBC
		internal static MultiValuedProperty<ADObjectId> ConvertTo(IRecipientSession recipientSession, MultiValuedProperty<MailboxIdParameter> mailboxIds, DataAccessHelper.GetDataObjectDelegate getDataObject, Task.TaskErrorLoggingDelegate writeError)
		{
			if (mailboxIds == null)
			{
				return null;
			}
			MultiValuedProperty<ADObjectId> multiValuedProperty = new MultiValuedProperty<ADObjectId>();
			foreach (MailboxIdParameter mailboxIdParameter in mailboxIds)
			{
				ADRecipient adrecipient = (ADRecipient)getDataObject(mailboxIdParameter, recipientSession, null, null, new LocalizedString?(Strings.ExceptionUserObjectNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorSearchUserNotUnique(mailboxIdParameter.ToString())));
				if (Array.IndexOf<RecipientTypeDetails>(MailboxAuditLogSearch.SupportedRecipientTypes, adrecipient.RecipientTypeDetails) == -1)
				{
					writeError(new ArgumentException(Strings.ErrorInvalidRecipientType(adrecipient.ToString(), adrecipient.RecipientTypeDetails.ToString())), ErrorCategory.InvalidArgument, null);
				}
				if (!multiValuedProperty.Contains(adrecipient.Id))
				{
					multiValuedProperty.Add(adrecipient.Id);
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x04000225 RID: 549
		internal const int MaxLogsForEmailAttachment = 50000;

		// Token: 0x04000226 RID: 550
		internal static readonly RecipientTypeDetails[] SupportedRecipientTypes = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.UserMailbox,
			RecipientTypeDetails.EquipmentMailbox,
			RecipientTypeDetails.LinkedMailbox,
			RecipientTypeDetails.RoomMailbox,
			RecipientTypeDetails.TeamMailbox,
			RecipientTypeDetails.SharedMailbox
		};
	}
}
