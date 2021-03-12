using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200008C RID: 140
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AuditLogSearchItemBase : DisposeTrackableBase
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x00013169 File Offset: 0x00011369
		protected AuditLogSearchItemBase(MailboxSession session, Folder folder)
		{
			this.message = MessageItem.Create(session, folder.Id);
			this.message[StoreObjectSchema.ItemClass] = this.ItemClass;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00013199 File Offset: 0x00011399
		protected AuditLogSearchItemBase(MailboxSession session, VersionedId messageId)
		{
			this.message = MessageItem.Bind(session, messageId, this.PropertiesToLoad);
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x000131B4 File Offset: 0x000113B4
		protected MessageItem Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000483 RID: 1155
		protected abstract string ItemClass { get; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000484 RID: 1156
		protected abstract PropertyDefinition[] PropertiesToLoad { get; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x000131BC File Offset: 0x000113BC
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x000131D3 File Offset: 0x000113D3
		public Guid Identity
		{
			get
			{
				return (Guid)this.Message[AuditLogSearchItemSchema.Identity];
			}
			set
			{
				this.Message[AuditLogSearchItemSchema.Identity] = value;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x000131EB File Offset: 0x000113EB
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x00013202 File Offset: 0x00011402
		public string Name
		{
			get
			{
				return this.Message.GetValueOrDefault<string>(AuditLogSearchItemSchema.Name, string.Empty);
			}
			set
			{
				this.Message[AuditLogSearchItemSchema.Name] = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00013215 File Offset: 0x00011415
		public VersionedId MessageId
		{
			get
			{
				return this.Message.Id;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00013222 File Offset: 0x00011422
		public ExDateTime CreationTime
		{
			get
			{
				return this.Message.CreationTime;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0001322F File Offset: 0x0001142F
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x00013246 File Offset: 0x00011446
		public ExDateTime StartDate
		{
			get
			{
				return (ExDateTime)this.Message[AuditLogSearchItemSchema.StartDate];
			}
			set
			{
				this.Message[AuditLogSearchItemSchema.StartDate] = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0001325E File Offset: 0x0001145E
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x00013275 File Offset: 0x00011475
		public ExDateTime EndDate
		{
			get
			{
				return (ExDateTime)this.Message[AuditLogSearchItemSchema.EndDate];
			}
			set
			{
				this.Message[AuditLogSearchItemSchema.EndDate] = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00013298 File Offset: 0x00011498
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x000132F5 File Offset: 0x000114F5
		public MultiValuedProperty<SmtpAddress> StatusMailRecipients
		{
			get
			{
				return new MultiValuedProperty<SmtpAddress>((from x in (string[])this.Message[AuditLogSearchItemSchema.StatusMailRecipients]
				select new SmtpAddress(x)).ToList<SmtpAddress>());
			}
			set
			{
				this.Message[AuditLogSearchItemSchema.StatusMailRecipients] = (from x in value
				select x.ToString()).ToArray<string>();
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0001332F File Offset: 0x0001152F
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x0001334B File Offset: 0x0001154B
		public ADObjectId CreatedByEx
		{
			get
			{
				return new ADObjectId((byte[])this.Message[AuditLogSearchItemSchema.CreatedByEx]);
			}
			set
			{
				this.Message[AuditLogSearchItemSchema.CreatedByEx] = value.GetBytes();
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00013363 File Offset: 0x00011563
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x0001337A File Offset: 0x0001157A
		public string CreatedBy
		{
			get
			{
				return (string)this.Message[AuditLogSearchItemSchema.CreatedBy];
			}
			set
			{
				this.Message[AuditLogSearchItemSchema.CreatedBy] = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00013390 File Offset: 0x00011590
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x000133D0 File Offset: 0x000115D0
		public bool? ExternalAccess
		{
			get
			{
				string value = this.Message.TryGetProperty(AuditLogSearchItemSchema.ExternalAccess) as string;
				if (string.IsNullOrEmpty(value))
				{
					return null;
				}
				return new bool?(bool.Parse(value));
			}
			set
			{
				if (value == null)
				{
					this.Message[AuditLogSearchItemSchema.ExternalAccess] = string.Empty;
					return;
				}
				if (value.Value)
				{
					this.Message[AuditLogSearchItemSchema.ExternalAccess] = bool.TrueString;
					return;
				}
				this.Message[AuditLogSearchItemSchema.ExternalAccess] = bool.FalseString;
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00013430 File Offset: 0x00011630
		public void Save(SaveMode saveMode)
		{
			this.Message.Save(saveMode);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00013440 File Offset: 0x00011640
		protected MultiValuedProperty<T> GetPropertiesPossiblyNotFound<T>(StorePropertyDefinition propertyDefinition)
		{
			T[] valueOrDefault = this.Message.GetValueOrDefault<T[]>(propertyDefinition, new T[0]);
			return new MultiValuedProperty<T>(valueOrDefault);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00013466 File Offset: 0x00011666
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.Message != null)
			{
				this.Message.Dispose();
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001347E File Offset: 0x0001167E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AuditLogSearchItemBase>(this);
		}

		// Token: 0x0400023B RID: 571
		private readonly MessageItem message;
	}
}
