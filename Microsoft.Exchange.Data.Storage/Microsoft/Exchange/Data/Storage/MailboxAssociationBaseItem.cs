using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007DA RID: 2010
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MailboxAssociationBaseItem : Item, IMailboxAssociationBaseItem, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06004B44 RID: 19268 RVA: 0x0013A2F9 File Offset: 0x001384F9
		internal MailboxAssociationBaseItem(ICoreItem coreItem) : base(coreItem, false)
		{
			if (base.IsNew)
			{
				this.Initialize();
			}
		}

		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x06004B45 RID: 19269 RVA: 0x0013A311 File Offset: 0x00138511
		// (set) Token: 0x06004B46 RID: 19270 RVA: 0x0013A32A File Offset: 0x0013852A
		public string LegacyDN
		{
			get
			{
				this.CheckDisposed("LegacyDN::get");
				return base.GetValueOrDefault<string>(MailboxAssociationBaseSchema.LegacyDN, null);
			}
			set
			{
				this.CheckDisposed("LegacyDN::set");
				this[MailboxAssociationBaseSchema.LegacyDN] = value;
			}
		}

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x06004B47 RID: 19271 RVA: 0x0013A343 File Offset: 0x00138543
		// (set) Token: 0x06004B48 RID: 19272 RVA: 0x0013A35C File Offset: 0x0013855C
		public string ExternalId
		{
			get
			{
				this.CheckDisposed("ExternalId::get");
				return base.GetValueOrDefault<string>(MailboxAssociationBaseSchema.ExternalId, null);
			}
			set
			{
				this.CheckDisposed("ExternalId::set");
				this[MailboxAssociationBaseSchema.ExternalId] = value;
			}
		}

		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x06004B49 RID: 19273 RVA: 0x0013A378 File Offset: 0x00138578
		// (set) Token: 0x06004B4A RID: 19274 RVA: 0x0013A3B1 File Offset: 0x001385B1
		public SmtpAddress SmtpAddress
		{
			get
			{
				this.CheckDisposed("SmtpAddress::get");
				string valueOrDefault = base.GetValueOrDefault<string>(MailboxAssociationBaseSchema.SmtpAddress, null);
				if (!string.IsNullOrEmpty(valueOrDefault))
				{
					return new SmtpAddress(valueOrDefault);
				}
				return SmtpAddress.Empty;
			}
			set
			{
				this.CheckDisposed("SmtpAddress::set");
				this[MailboxAssociationBaseSchema.SmtpAddress] = (string)value;
			}
		}

		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x06004B4B RID: 19275 RVA: 0x0013A3CF File Offset: 0x001385CF
		// (set) Token: 0x06004B4C RID: 19276 RVA: 0x0013A3E8 File Offset: 0x001385E8
		public bool IsMember
		{
			get
			{
				this.CheckDisposed("IsMember::get");
				return base.GetValueOrDefault<bool>(MailboxAssociationBaseSchema.IsMember, false);
			}
			set
			{
				this.CheckDisposed("IsMember::set");
				this[MailboxAssociationBaseSchema.IsMember] = value;
			}
		}

		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x06004B4D RID: 19277 RVA: 0x0013A406 File Offset: 0x00138606
		// (set) Token: 0x06004B4E RID: 19278 RVA: 0x0013A41F File Offset: 0x0013861F
		public bool ShouldEscalate
		{
			get
			{
				this.CheckDisposed("ShouldEscalate::get");
				return base.GetValueOrDefault<bool>(MailboxAssociationBaseSchema.ShouldEscalate, false);
			}
			set
			{
				this.CheckDisposed("ShouldEscalate::set");
				this[MailboxAssociationBaseSchema.ShouldEscalate] = value;
			}
		}

		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x06004B4F RID: 19279 RVA: 0x0013A43D File Offset: 0x0013863D
		// (set) Token: 0x06004B50 RID: 19280 RVA: 0x0013A456 File Offset: 0x00138656
		public bool IsAutoSubscribed
		{
			get
			{
				this.CheckDisposed("IsAutoSubscribed::get");
				return base.GetValueOrDefault<bool>(MailboxAssociationBaseSchema.IsAutoSubscribed, false);
			}
			set
			{
				this.CheckDisposed("IsAutoSubscribed::set");
				this[MailboxAssociationBaseSchema.IsAutoSubscribed] = value;
			}
		}

		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x06004B51 RID: 19281 RVA: 0x0013A474 File Offset: 0x00138674
		// (set) Token: 0x06004B52 RID: 19282 RVA: 0x0013A48D File Offset: 0x0013868D
		public bool IsPin
		{
			get
			{
				this.CheckDisposed("IsPin::get");
				return base.GetValueOrDefault<bool>(MailboxAssociationBaseSchema.IsPin, false);
			}
			set
			{
				this.CheckDisposed("IsPin::set");
				this[MailboxAssociationBaseSchema.IsPin] = value;
			}
		}

		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x06004B53 RID: 19283 RVA: 0x0013A4AC File Offset: 0x001386AC
		// (set) Token: 0x06004B54 RID: 19284 RVA: 0x0013A4D8 File Offset: 0x001386D8
		public ExDateTime JoinDate
		{
			get
			{
				this.CheckDisposed("JoinDate::get");
				return base.GetValueOrDefault<ExDateTime>(MailboxAssociationBaseSchema.JoinDate, default(ExDateTime));
			}
			set
			{
				this.CheckDisposed("JoinDate::set");
				this[MailboxAssociationBaseSchema.JoinDate] = value;
			}
		}

		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x06004B55 RID: 19285 RVA: 0x0013A4F6 File Offset: 0x001386F6
		// (set) Token: 0x06004B56 RID: 19286 RVA: 0x0013A50F File Offset: 0x0013870F
		public string SyncedIdentityHash
		{
			get
			{
				this.CheckDisposed("SyncedIdentityHash::get");
				return base.GetValueOrDefault<string>(MailboxAssociationBaseSchema.SyncedIdentityHash, null);
			}
			set
			{
				this.CheckDisposed("SyncedIdentityHash::set");
				this[MailboxAssociationBaseSchema.SyncedIdentityHash] = value;
			}
		}

		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x06004B57 RID: 19287 RVA: 0x0013A528 File Offset: 0x00138728
		// (set) Token: 0x06004B58 RID: 19288 RVA: 0x0013A541 File Offset: 0x00138741
		public int CurrentVersion
		{
			get
			{
				this.CheckDisposed("CurrentVersion::get");
				return base.GetValueOrDefault<int>(MailboxAssociationBaseSchema.CurrentVersion, 0);
			}
			set
			{
				this.CheckDisposed("CurrentVersion::set");
				this[MailboxAssociationBaseSchema.CurrentVersion] = value;
			}
		}

		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x06004B59 RID: 19289 RVA: 0x0013A55F File Offset: 0x0013875F
		// (set) Token: 0x06004B5A RID: 19290 RVA: 0x0013A578 File Offset: 0x00138778
		public int SyncedVersion
		{
			get
			{
				this.CheckDisposed("SyncedVersion::get");
				return base.GetValueOrDefault<int>(MailboxAssociationBaseSchema.SyncedVersion, -1);
			}
			set
			{
				this.CheckDisposed("SyncedVersion::set");
				this[MailboxAssociationBaseSchema.SyncedVersion] = value;
			}
		}

		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x06004B5B RID: 19291 RVA: 0x0013A596 File Offset: 0x00138796
		// (set) Token: 0x06004B5C RID: 19292 RVA: 0x0013A5AF File Offset: 0x001387AF
		public string LastSyncError
		{
			get
			{
				this.CheckDisposed("LastSyncError::get");
				return base.GetValueOrDefault<string>(MailboxAssociationBaseSchema.LastSyncError, null);
			}
			set
			{
				this.CheckDisposed("LastSyncError::set");
				this[MailboxAssociationBaseSchema.LastSyncError] = value;
			}
		}

		// Token: 0x1700158C RID: 5516
		// (get) Token: 0x06004B5D RID: 19293 RVA: 0x0013A5C8 File Offset: 0x001387C8
		// (set) Token: 0x06004B5E RID: 19294 RVA: 0x0013A5E1 File Offset: 0x001387E1
		public int SyncAttempts
		{
			get
			{
				this.CheckDisposed("SyncAttempts::get");
				return base.GetValueOrDefault<int>(MailboxAssociationBaseSchema.SyncAttempts, 0);
			}
			set
			{
				this.CheckDisposed("SyncAttempts::set");
				this[MailboxAssociationBaseSchema.SyncAttempts] = value;
			}
		}

		// Token: 0x1700158D RID: 5517
		// (get) Token: 0x06004B5F RID: 19295 RVA: 0x0013A5FF File Offset: 0x001387FF
		// (set) Token: 0x06004B60 RID: 19296 RVA: 0x0013A618 File Offset: 0x00138818
		public string SyncedSchemaVersion
		{
			get
			{
				this.CheckDisposed("SyncedSchemaVersion::get");
				return base.GetValueOrDefault<string>(MailboxAssociationBaseSchema.SyncedSchemaVersion, null);
			}
			set
			{
				this.CheckDisposed("SyncedSchemaVersion::set");
				this[MailboxAssociationBaseSchema.SyncedSchemaVersion] = value;
			}
		}

		// Token: 0x1700158E RID: 5518
		// (get) Token: 0x06004B61 RID: 19297
		public abstract string AssociationItemClass { get; }

		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x06004B62 RID: 19298 RVA: 0x0013A631 File Offset: 0x00138831
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return MailboxAssociationBaseSchema.Instance;
			}
		}

		// Token: 0x06004B63 RID: 19299 RVA: 0x0013A644 File Offset: 0x00138844
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			this.AppendDescriptionTo(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06004B64 RID: 19300 RVA: 0x0013A668 File Offset: 0x00138868
		protected void AppendDescriptionTo(StringBuilder buffer)
		{
			ArgumentValidator.ThrowIfNull("buffer", buffer);
			buffer.Append("ItemClass=");
			buffer.Append(this.AssociationItemClass);
			buffer.Append(", Id=");
			buffer.Append(base.Id);
			buffer.Append(", LegacyDN=");
			buffer.Append(this.LegacyDN);
			buffer.Append(", ExternalId=");
			buffer.Append(this.ExternalId);
			buffer.Append(", SmtpAddress=");
			buffer.Append(this.SmtpAddress);
			buffer.Append(", IsMember=");
			buffer.Append(this.IsMember);
			buffer.Append(", ShouldEscalate=");
			buffer.Append(this.ShouldEscalate);
			buffer.Append(", IsAutoSubscribed=");
			buffer.Append(this.IsAutoSubscribed);
			buffer.Append(", IsPin=");
			buffer.Append(this.IsPin);
			buffer.Append(", JoinDate=");
			buffer.Append(this.JoinDate);
			buffer.Append(", CurrentVersion=");
			buffer.Append(this.CurrentVersion);
			buffer.Append(", SyncedVersion=");
			buffer.Append(this.SyncedVersion);
			buffer.Append(", LastSyncError=");
			buffer.Append(this.LastSyncError);
			buffer.Append(", SyncAttempts=");
			buffer.Append(this.SyncAttempts);
			buffer.Append(", SyncedSchemaVersion=");
			buffer.Append(this.SyncedSchemaVersion);
			buffer.Append(", LastModified=");
			buffer.Append(base.GetValueOrDefault<ExDateTime>(InternalSchema.LastModifiedTime, ExDateTime.MinValue));
		}

		// Token: 0x06004B65 RID: 19301 RVA: 0x0013A829 File Offset: 0x00138A29
		private void Initialize()
		{
			this[InternalSchema.ItemClass] = this.AssociationItemClass;
		}
	}
}
