using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000085 RID: 133
	[Serializable]
	public class AuditLogSearchBase : ConfigurableObject
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x000110E0 File Offset: 0x0000F2E0
		public AuditLogSearchBase() : base(new SimplePropertyBag(AuditLogSearchBaseSchema.Identity, AuditLogSearchBaseSchema.ObjectState, AuditLogSearchBaseSchema.ExchangeVersion))
		{
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x000110FC File Offset: 0x0000F2FC
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<AuditLogSearchBaseSchema>();
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00011103 File Offset: 0x0000F303
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0001110A File Offset: 0x0000F30A
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x00011112 File Offset: 0x0000F312
		internal OrganizationId OrganizationId { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0001111B File Offset: 0x0000F31B
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x0001112D File Offset: 0x0000F32D
		public string Name
		{
			get
			{
				return (string)this[AuditLogSearchBaseSchema.Name];
			}
			set
			{
				this[AuditLogSearchBaseSchema.Name] = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0001113B File Offset: 0x0000F33B
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x0001114D File Offset: 0x0000F34D
		public DateTime? StartDateUtc
		{
			get
			{
				return (DateTime?)this[AuditLogSearchBaseSchema.StartDateUtc];
			}
			set
			{
				this[AuditLogSearchBaseSchema.StartDateUtc] = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00011160 File Offset: 0x0000F360
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x00011172 File Offset: 0x0000F372
		public DateTime? EndDateUtc
		{
			get
			{
				return (DateTime?)this[AuditLogSearchBaseSchema.EndDateUtc];
			}
			set
			{
				this[AuditLogSearchBaseSchema.EndDateUtc] = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00011185 File Offset: 0x0000F385
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x00011197 File Offset: 0x0000F397
		public MultiValuedProperty<SmtpAddress> StatusMailRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[AuditLogSearchBaseSchema.StatusMailRecipients];
			}
			set
			{
				this[AuditLogSearchBaseSchema.StatusMailRecipients] = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x000111A5 File Offset: 0x0000F3A5
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x000111B7 File Offset: 0x0000F3B7
		internal ADObjectId CreatedByEx
		{
			get
			{
				return (ADObjectId)this[AuditLogSearchBaseSchema.CreatedByEx];
			}
			set
			{
				this[AuditLogSearchBaseSchema.CreatedByEx] = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x000111C5 File Offset: 0x0000F3C5
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x000111D7 File Offset: 0x0000F3D7
		public string CreatedBy
		{
			get
			{
				return (string)this[AuditLogSearchBaseSchema.CreatedBy];
			}
			set
			{
				this[AuditLogSearchBaseSchema.CreatedBy] = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x000111E5 File Offset: 0x0000F3E5
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x000111F7 File Offset: 0x0000F3F7
		public bool? ExternalAccess
		{
			get
			{
				return (bool?)this[AuditLogSearchBaseSchema.ExternalAccess];
			}
			set
			{
				this[AuditLogSearchBaseSchema.ExternalAccess] = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0001120A File Offset: 0x0000F40A
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x00011212 File Offset: 0x0000F412
		internal DateTime CreationTime { get; private set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0001121B File Offset: 0x0000F41B
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x00011223 File Offset: 0x0000F423
		internal VersionedId MessageId { get; private set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0001122C File Offset: 0x0000F42C
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x00011234 File Offset: 0x0000F434
		public int QueryComplexity { get; set; }

		// Token: 0x06000428 RID: 1064 RVA: 0x0001123D File Offset: 0x0000F43D
		internal void SetId(AuditLogSearchId id)
		{
			this[AuditLogSearchBaseSchema.Identity] = id;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001124C File Offset: 0x0000F44C
		internal virtual void Initialize(AuditLogSearchItemBase item)
		{
			this.SetId(new AuditLogSearchId(item.Identity));
			this.Name = item.Name;
			this.StartDateUtc = new DateTime?(item.StartDate.UniversalTime);
			this.EndDateUtc = new DateTime?(item.EndDate.UniversalTime);
			this.StatusMailRecipients = item.StatusMailRecipients;
			this.CreatedBy = item.CreatedBy;
			this.CreatedByEx = item.CreatedByEx;
			this.ExternalAccess = item.ExternalAccess;
			this.CreationTime = item.CreationTime.UniversalTime;
			this.MessageId = item.MessageId;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000112F8 File Offset: 0x0000F4F8
		internal virtual void Initialize(AuditLogSearchBase item)
		{
			this.SetId(item.Identity);
			this.Name = item.Name;
			this.StartDateUtc = item.StartDateUtc;
			this.EndDateUtc = item.EndDateUtc;
			this.StatusMailRecipients = AuditLogSearchBase.ConvertToSmtpAddressMVP(item.StatusMailRecipients);
			this.CreatedBy = item.CreatedBy;
			this.CreatedByEx = item.CreatedByEx;
			this.ExternalAccess = (string.IsNullOrEmpty(item.ExternalAccess) ? null : new bool?(bool.Parse(item.ExternalAccess)));
			this.CreationTime = item.CreationTime;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00011398 File Offset: 0x0000F598
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("OrganizationId={0}", this.OrganizationId);
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("StartDateUtc={0}", this.StartDateUtc);
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("EndDateUtc={0}", this.EndDateUtc);
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("ExternalAccess={0}", (this.ExternalAccess != null) ? this.ExternalAccess.Value.ToString() : "NULL");
			return stringBuilder.ToString();
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00011440 File Offset: 0x0000F640
		protected static void AppendStringSearchTerm(StringBuilder stringBuilder, string name, IEnumerable<string> values)
		{
			stringBuilder.Append(name + "=");
			if (values != null)
			{
				foreach (string value in values)
				{
					stringBuilder.Append(value);
					stringBuilder.Append(",");
				}
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000114BC File Offset: 0x0000F6BC
		protected static MultiValuedProperty<SmtpAddress> ConvertToSmtpAddressMVP(MultiValuedProperty<string> mvp)
		{
			MultiValuedProperty<SmtpAddress> multiValuedProperty = new MultiValuedProperty<SmtpAddress>();
			if (mvp != null)
			{
				foreach (string address in mvp)
				{
					multiValuedProperty.Add(SmtpAddress.Parse(address));
				}
			}
			return multiValuedProperty;
		}
	}
}
