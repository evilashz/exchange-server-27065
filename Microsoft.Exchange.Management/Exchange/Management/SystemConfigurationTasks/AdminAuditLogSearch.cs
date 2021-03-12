using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000087 RID: 135
	[Serializable]
	public class AdminAuditLogSearch : AuditLogSearchBase
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00011BE9 File Offset: 0x0000FDE9
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<AdminAuditLogSearchSchema>();
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00011BF0 File Offset: 0x0000FDF0
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x00011C02 File Offset: 0x0000FE02
		public MultiValuedProperty<string> Cmdlets
		{
			get
			{
				return (MultiValuedProperty<string>)this[AdminAuditLogSearchSchema.Cmdlets];
			}
			set
			{
				this[AdminAuditLogSearchSchema.Cmdlets] = value;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00011C10 File Offset: 0x0000FE10
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x00011C22 File Offset: 0x0000FE22
		public MultiValuedProperty<string> Parameters
		{
			get
			{
				return (MultiValuedProperty<string>)this[AdminAuditLogSearchSchema.Parameters];
			}
			set
			{
				this[AdminAuditLogSearchSchema.Parameters] = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00011C30 File Offset: 0x0000FE30
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x00011C42 File Offset: 0x0000FE42
		public MultiValuedProperty<string> ObjectIds
		{
			get
			{
				return (MultiValuedProperty<string>)this[AdminAuditLogSearchSchema.ObjectIds];
			}
			set
			{
				this[AdminAuditLogSearchSchema.ObjectIds] = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00011C50 File Offset: 0x0000FE50
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x00011C62 File Offset: 0x0000FE62
		public MultiValuedProperty<string> UserIds
		{
			get
			{
				return (MultiValuedProperty<string>)this[AdminAuditLogSearchSchema.UserIds];
			}
			set
			{
				this[AdminAuditLogSearchSchema.UserIds] = value;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00011C70 File Offset: 0x0000FE70
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x00011C82 File Offset: 0x0000FE82
		internal MultiValuedProperty<string> ResolvedUsers
		{
			get
			{
				return (MultiValuedProperty<string>)this[AdminAuditLogSearchSchema.ResolvedUsers];
			}
			set
			{
				this[AdminAuditLogSearchSchema.ResolvedUsers] = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00011C90 File Offset: 0x0000FE90
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x00011CA2 File Offset: 0x0000FEA2
		internal bool RedactDatacenterAdmins
		{
			get
			{
				return (bool)this[AdminAuditLogSearchSchema.RedactDatacenterAdmins];
			}
			set
			{
				this[AdminAuditLogSearchSchema.RedactDatacenterAdmins] = value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00011CB5 File Offset: 0x0000FEB5
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x00011CBD File Offset: 0x0000FEBD
		internal MultiValuedProperty<SecurityPrincipalIdParameter> UserIdsUserInput { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00011CC6 File Offset: 0x0000FEC6
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00011CCE File Offset: 0x0000FECE
		internal bool? Succeeded { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00011CD7 File Offset: 0x0000FED7
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00011CDF File Offset: 0x0000FEDF
		internal int StartIndex { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00011CE8 File Offset: 0x0000FEE8
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00011CF0 File Offset: 0x0000FEF0
		internal int ResultSize { get; set; }

		// Token: 0x06000459 RID: 1113 RVA: 0x00011CFC File Offset: 0x0000FEFC
		internal override void Initialize(AuditLogSearchItemBase item)
		{
			AdminAuditLogSearchItem adminAuditLogSearchItem = (AdminAuditLogSearchItem)item;
			base.Initialize(item);
			this.Cmdlets = adminAuditLogSearchItem.Cmdlets;
			this.Parameters = adminAuditLogSearchItem.Parameters;
			this.ObjectIds = adminAuditLogSearchItem.ObjectIds;
			this.UserIds = adminAuditLogSearchItem.RawUserIds;
			this.ResolvedUsers = adminAuditLogSearchItem.ResolvedUsers;
			this.RedactDatacenterAdmins = adminAuditLogSearchItem.RedactDatacenterAdmins;
			this.Succeeded = null;
			this.StartIndex = 0;
			this.ResultSize = 50000;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00011D80 File Offset: 0x0000FF80
		internal override void Initialize(AuditLogSearchBase item)
		{
			AdminAuditLogSearch adminAuditLogSearch = (AdminAuditLogSearch)item;
			base.Initialize(item);
			this.Cmdlets = adminAuditLogSearch.Cmdlets;
			this.Parameters = adminAuditLogSearch.Parameters;
			this.ObjectIds = adminAuditLogSearch.ObjectIds;
			this.UserIds = adminAuditLogSearch.UserIds;
			this.ResolvedUsers = adminAuditLogSearch.ResolvedUsers;
			this.RedactDatacenterAdmins = adminAuditLogSearch.RedactDatacenterAdmins;
			this.Succeeded = null;
			this.StartIndex = 0;
			this.ResultSize = 50000;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00011E04 File Offset: 0x00010004
		internal void Validate(Task.TaskErrorLoggingDelegate writeError)
		{
			if ((this.Cmdlets == null || this.Cmdlets.Count == 0) && this.Parameters != null && this.Parameters.Count != 0)
			{
				writeError(new ArgumentException(Strings.AdminAuditLogSearchMissingCmdletsWhileParameterProvided), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00011E54 File Offset: 0x00010054
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString());
			stringBuilder.AppendLine();
			AuditLogSearchBase.AppendStringSearchTerm(stringBuilder, "Cmdlets", this.Cmdlets);
			stringBuilder.AppendLine();
			AuditLogSearchBase.AppendStringSearchTerm(stringBuilder, "Parameters", this.Parameters);
			stringBuilder.AppendLine();
			AuditLogSearchBase.AppendStringSearchTerm(stringBuilder, "ObjectIds", this.ObjectIds);
			stringBuilder.AppendLine();
			AuditLogSearchBase.AppendStringSearchTerm(stringBuilder, "UserIds", this.UserIds);
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("Succeeded={0}", this.Succeeded);
			return stringBuilder.ToString();
		}

		// Token: 0x04000229 RID: 553
		internal const int MaxSearchResultSize = 250000;

		// Token: 0x0400022A RID: 554
		internal const int MaxLogsForEmailAttachment = 50000;
	}
}
