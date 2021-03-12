using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Storage.Messaging.Utah
{
	// Token: 0x02000110 RID: 272
	internal class MailRecipientStorage : DataRow, IMailRecipientStorage
	{
		// Token: 0x06000C17 RID: 3095 RVA: 0x0002A419 File Offset: 0x00028619
		public MailRecipientStorage(DataTable dataTable, long messageId) : this(dataTable)
		{
			this.InitializeDefaults();
			this.MsgId = messageId;
			this.RecipientRowId = ((RecipientTable)dataTable).GetNextRecipientRowId();
			this.AddToActive();
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002A446 File Offset: 0x00028646
		internal MailRecipientStorage(DataTableCursor cursor) : this(cursor.Table)
		{
			base.LoadFromCurrentRow(cursor);
			if (this.IsActive)
			{
				this.Table.IncrementActiveRecipientCount();
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002A470 File Offset: 0x00028670
		private MailRecipientStorage(DataTable dataTable) : base(dataTable)
		{
			BlobCollection blobCollection = new BlobCollection(this.Table.Schemas[16], this);
			this.componentExtendedProperties = new ExtendedPropertyDictionary(this, blobCollection, 1);
			this.componentInternalProperties = new ExtendedPropertyDictionary(this, blobCollection, 2);
			base.AddComponent(this.componentExtendedProperties);
			base.AddComponent(this.componentInternalProperties);
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0002A4D1 File Offset: 0x000286D1
		public IExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this.componentExtendedProperties;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0002A4D9 File Offset: 0x000286D9
		// (set) Token: 0x06000C1C RID: 3100 RVA: 0x0002A4E6 File Offset: 0x000286E6
		public string ORcpt
		{
			get
			{
				return this.ORcptColumn.Value;
			}
			set
			{
				this.ThrowIfDeleted();
				this.ORcptColumn.Value = value;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0002A4FA File Offset: 0x000286FA
		// (set) Token: 0x06000C1E RID: 3102 RVA: 0x0002A512 File Offset: 0x00028712
		public long MsgId
		{
			get
			{
				return this.Table.Generation.CombineIds(this.MessageRowId);
			}
			set
			{
				if (MessagingGeneration.GetGenerationId(value) != this.Table.Generation.GenerationId)
				{
					throw new ArgumentOutOfRangeException("value", "Message generation does not match recipient generation.");
				}
				this.MessageRowId = MessagingGeneration.GetRowId(value);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0002A548 File Offset: 0x00028748
		// (set) Token: 0x06000C20 RID: 3104 RVA: 0x0002A555 File Offset: 0x00028755
		public int MessageRowId
		{
			get
			{
				return this.MessageRowIdColumn.Value;
			}
			set
			{
				this.MessageRowIdColumn.Value = value;
				if (this.UndeliveredMessageRowIdColumn.HasValue)
				{
					this.UndeliveredMessageRowIdColumn.Value = this.MessageRowId;
				}
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0002A581 File Offset: 0x00028781
		// (set) Token: 0x06000C22 RID: 3106 RVA: 0x0002A599 File Offset: 0x00028799
		public long RecipId
		{
			get
			{
				return this.Table.Generation.CombineIds(this.RecipientRowId);
			}
			set
			{
				throw new NotImplementedException("Setting RecipId is deprecated.");
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0002A5A5 File Offset: 0x000287A5
		// (set) Token: 0x06000C24 RID: 3108 RVA: 0x0002A5B2 File Offset: 0x000287B2
		public int RecipientRowId
		{
			get
			{
				return this.RecipientRowIdColumn.Value;
			}
			protected set
			{
				this.RecipientRowIdColumn.Value = value;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0002A5C0 File Offset: 0x000287C0
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x0002A5CD File Offset: 0x000287CD
		public AdminActionStatus AdminActionStatus
		{
			get
			{
				return (AdminActionStatus)this.AdminActionStatusColumn.Value;
			}
			set
			{
				this.ThrowIfDeleted();
				this.AdminActionStatusColumn.Value = (byte)value;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0002A5E1 File Offset: 0x000287E1
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x0002A5EA File Offset: 0x000287EA
		public DsnRequestedFlags DsnRequested
		{
			get
			{
				return (DsnRequestedFlags)this.GetDsn(8);
			}
			set
			{
				this.ThrowIfDeleted();
				this.SetDsn(8, (byte)value);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0002A5FB File Offset: 0x000287FB
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x0002A604 File Offset: 0x00028804
		public DsnFlags DsnNeeded
		{
			get
			{
				return (DsnFlags)this.GetDsn(0);
			}
			set
			{
				this.ThrowIfDeleted();
				this.SetDsn(0, (byte)value);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0002A615 File Offset: 0x00028815
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x0002A61F File Offset: 0x0002881F
		public DsnFlags DsnCompleted
		{
			get
			{
				return (DsnFlags)this.GetDsn(16);
			}
			set
			{
				this.ThrowIfDeleted();
				this.SetDsn(16, (byte)value);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0002A631 File Offset: 0x00028831
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x0002A63E File Offset: 0x0002883E
		public Status Status
		{
			get
			{
				return (Status)this.StatusColumn.Value;
			}
			set
			{
				this.ThrowIfDeleted();
				this.StatusColumn.Value = (byte)value;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0002A654 File Offset: 0x00028854
		// (set) Token: 0x06000C30 RID: 3120 RVA: 0x0002A6AC File Offset: 0x000288AC
		public Destination DeliveredDestination
		{
			get
			{
				if (this.deliveredDestination == null && this.DeliveredDestinationColumn.HasValue && this.DeliveredDestinationTypeColumn.HasValue)
				{
					this.deliveredDestination = new Destination((Destination.DestinationType)this.DeliveredDestinationTypeColumn.Value, this.DeliveredDestinationColumn.Value);
				}
				return this.deliveredDestination;
			}
			set
			{
				if (value == null)
				{
					this.ReleaseFromSafetyNet();
					this.DeliveredDestinationColumn.HasValue = false;
					this.DeliveredDestinationTypeColumn.HasValue = false;
					this.deliveredDestination = null;
					return;
				}
				this.deliveredDestination = value;
				this.DeliveredDestinationColumn.Value = value.Blob;
				this.DeliveredDestinationTypeColumn.Value = (byte)value.Type;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0002A70B File Offset: 0x0002890B
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x0002A718 File Offset: 0x00028918
		public string PrimaryServerFqdnGuid
		{
			get
			{
				return this.PrimaryServerFqdnGuidColumn.Value;
			}
			set
			{
				this.ThrowIfDeleted();
				this.PrimaryServerFqdnGuidColumn.Value = value;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0002A72C File Offset: 0x0002892C
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x0002A760 File Offset: 0x00028960
		public DateTime? DeliveryTime
		{
			get
			{
				if (this.DeliveryTimeColumn.HasValue)
				{
					return new DateTime?(this.DeliveryTimeColumn.Value);
				}
				return null;
			}
			set
			{
				this.ThrowIfDeleted();
				if (value != null)
				{
					this.DeliveryTimeColumn.Value = value.Value;
					return;
				}
				this.ReleaseFromSafetyNet();
				this.DeliveryTimeColumn.HasValue = false;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0002A796 File Offset: 0x00028996
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x0002A7A3 File Offset: 0x000289A3
		public int RetryCount
		{
			get
			{
				return this.RetryCountColumn.Value;
			}
			set
			{
				this.ThrowIfDeleted();
				this.RetryCountColumn.Value = value;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0002A7B7 File Offset: 0x000289B7
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x0002A7C4 File Offset: 0x000289C4
		public string Email
		{
			get
			{
				return this.ToSmtpAddressColumn.Value;
			}
			set
			{
				this.ThrowIfDeleted();
				this.ToSmtpAddressColumn.Value = value;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0002A7D8 File Offset: 0x000289D8
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x0002A809 File Offset: 0x00028A09
		public RequiredTlsAuthLevel? TlsAuthLevel
		{
			get
			{
				byte value;
				if (this.componentInternalProperties.TryGetValue<byte>("Microsoft.Exchange.Transport.MailRecipient.RequiredTlsAuthLevel", out value))
				{
					return new RequiredTlsAuthLevel?((RequiredTlsAuthLevel)value);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.componentInternalProperties.SetValue<byte>("Microsoft.Exchange.Transport.MailRecipient.RequiredTlsAuthLevel", (byte)value.Value);
					return;
				}
				this.componentInternalProperties.Remove("Microsoft.Exchange.Transport.MailRecipient.RequiredTlsAuthLevel");
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0002A840 File Offset: 0x00028A40
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x0002A864 File Offset: 0x00028A64
		public int OutboundIPPool
		{
			get
			{
				int result;
				this.componentInternalProperties.TryGetValue<int>("Microsoft.Exchange.Transport.MailRecipient.OutboundIPPool", out result);
				return result;
			}
			set
			{
				if (value > 0)
				{
					int num;
					if (!this.componentInternalProperties.TryGetValue<int>("Microsoft.Exchange.Transport.MailRecipient.OutboundIPPool", out num) || num != value)
					{
						this.componentInternalProperties.SetValue<int>("Microsoft.Exchange.Transport.MailRecipient.OutboundIPPool", value);
						return;
					}
				}
				else
				{
					this.componentInternalProperties.Remove("Microsoft.Exchange.Transport.MailRecipient.OutboundIPPool");
				}
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0002A8B0 File Offset: 0x00028AB0
		public bool IsInSafetyNet
		{
			get
			{
				return this.DeliveryTimeOffsetColumn.HasValue && this.DestinationHashColumn.HasValue;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x0002A8CC File Offset: 0x00028ACC
		public bool IsActive
		{
			get
			{
				return !this.IsDeleted && this.UndeliveredMessageRowIdColumn.HasValue;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0002A8E3 File Offset: 0x00028AE3
		public new RecipientTable Table
		{
			get
			{
				return (RecipientTable)base.Table;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x0002A8F0 File Offset: 0x00028AF0
		private ColumnCache<int> RecipientRowIdColumn
		{
			get
			{
				return (ColumnCache<int>)base.Columns[0];
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0002A903 File Offset: 0x00028B03
		private ColumnCache<int> MessageRowIdColumn
		{
			get
			{
				return (ColumnCache<int>)base.Columns[1];
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0002A916 File Offset: 0x00028B16
		private ColumnCache<byte> AdminActionStatusColumn
		{
			get
			{
				return (ColumnCache<byte>)base.Columns[2];
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0002A929 File Offset: 0x00028B29
		private ColumnCache<byte> StatusColumn
		{
			get
			{
				return (ColumnCache<byte>)base.Columns[3];
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0002A93C File Offset: 0x00028B3C
		private ColumnCache<int> DsnColumn
		{
			get
			{
				return (ColumnCache<int>)base.Columns[4];
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0002A94F File Offset: 0x00028B4F
		private ColumnCache<int> RetryCountColumn
		{
			get
			{
				return (ColumnCache<int>)base.Columns[5];
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0002A962 File Offset: 0x00028B62
		private ColumnCache<int> DestinationHashColumn
		{
			get
			{
				return (ColumnCache<int>)base.Columns[6];
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x0002A975 File Offset: 0x00028B75
		private ColumnCache<int> DeliveryTimeOffsetColumn
		{
			get
			{
				return (ColumnCache<int>)base.Columns[7];
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0002A988 File Offset: 0x00028B88
		private ColumnCache<DateTime> DeliveryTimeColumn
		{
			get
			{
				return (ColumnCache<DateTime>)base.Columns[8];
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0002A99B File Offset: 0x00028B9B
		private ColumnCache<int> UndeliveredMessageRowIdColumn
		{
			get
			{
				return (ColumnCache<int>)base.Columns[9];
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0002A9AF File Offset: 0x00028BAF
		private ColumnCache<byte> DeliveredDestinationTypeColumn
		{
			get
			{
				return (ColumnCache<byte>)base.Columns[10];
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0002A9C3 File Offset: 0x00028BC3
		private ColumnCache<byte[]> DeliveredDestinationColumn
		{
			get
			{
				return (ColumnCache<byte[]>)base.Columns[11];
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0002A9D7 File Offset: 0x00028BD7
		private ColumnCache<string> ToSmtpAddressColumn
		{
			get
			{
				return (ColumnCache<string>)base.Columns[12];
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0002A9EB File Offset: 0x00028BEB
		private ColumnCache<string> ORcptColumn
		{
			get
			{
				return (ColumnCache<string>)base.Columns[14];
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0002A9FF File Offset: 0x00028BFF
		private ColumnCache<string> PrimaryServerFqdnGuidColumn
		{
			get
			{
				return (ColumnCache<string>)base.Columns[15];
			}
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002AA13 File Offset: 0x00028C13
		public static DateTime GetTimeFromOffset(int timeOffset)
		{
			return MailRecipientStorage.TimeOffsetReference + TimeSpan.FromSeconds((double)timeOffset);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0002AA28 File Offset: 0x00028C28
		public static int GetTimeOffset(DateTime timeStamp)
		{
			timeStamp = ((timeStamp < MailRecipientStorage.MinDate) ? MailRecipientStorage.MinDate : ((timeStamp > MailRecipientStorage.MaxDate) ? MailRecipientStorage.MaxDate : timeStamp));
			return (int)timeStamp.Subtract(MailRecipientStorage.TimeOffsetReference).TotalSeconds;
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002AA78 File Offset: 0x00028C78
		public IMailRecipientStorage MoveTo(long targetMailItemId)
		{
			if (MessagingGeneration.GetGenerationId(this.MsgId) == MessagingGeneration.GetGenerationId(targetMailItemId))
			{
				this.MsgId = targetMailItemId;
				return this;
			}
			if (Components.TransportAppConfig.QueueDatabase.CloneInOriginalGeneration)
			{
				throw new InvalidOperationException("Cannot move recipients between the generations.");
			}
			MailRecipientStorage mailRecipientStorage = (MailRecipientStorage)this.CopyTo(targetMailItemId);
			if (!base.IsNew)
			{
				mailRecipientStorage.SetCloneOrMoveSource(this, false);
				this.ReleaseFromActive();
				this.ReleaseFromSafetyNet();
			}
			return mailRecipientStorage;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002AAE8 File Offset: 0x00028CE8
		public IMailRecipientStorage CopyTo(long target)
		{
			MailRecipientStorage mailRecipientStorage = (MailRecipientStorage)this.Table.Generation.MessagingDatabase.NewRecipientStorage(target);
			int recipientRowId = mailRecipientStorage.RecipientRowId;
			mailRecipientStorage.Columns.CloneFrom(base.Columns);
			mailRecipientStorage.componentExtendedProperties.CloneFrom(this.componentExtendedProperties);
			mailRecipientStorage.componentInternalProperties.CloneFrom(this.componentInternalProperties);
			mailRecipientStorage.Columns.MarkDirtyForReload();
			mailRecipientStorage.componentExtendedProperties.Dirty = true;
			mailRecipientStorage.componentInternalProperties.Dirty = true;
			mailRecipientStorage.RecipientRowId = recipientRowId;
			mailRecipientStorage.MsgId = target;
			mailRecipientStorage.AddToActive();
			if (mailRecipientStorage.IsInSafetyNet)
			{
				mailRecipientStorage.Table.IncrementSafetyNetRecipientCount(mailRecipientStorage.DeliveredDestination.Type);
			}
			return mailRecipientStorage;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002ABA2 File Offset: 0x00028DA2
		public new void Commit(TransactionCommitMode commitMode)
		{
			base.Commit(commitMode);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002ABAB File Offset: 0x00028DAB
		public new void Materialize(Transaction transaction)
		{
			base.Materialize(transaction);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002ABB4 File Offset: 0x00028DB4
		public void ReleaseFromActive()
		{
			if (this.IsActive)
			{
				this.UndeliveredMessageRowIdColumn.HasValue = false;
				this.Table.DecrementActiveRecipientCount();
			}
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0002ABD8 File Offset: 0x00028DD8
		public override void MarkToDelete()
		{
			this.Table.DecrementRecipientCount();
			if (this.IsActive)
			{
				this.Table.DecrementActiveRecipientCount();
			}
			if (this.IsInSafetyNet)
			{
				this.Table.DecrementSafetyNetRecipientCount(this.DeliveredDestination.Type);
			}
			base.MarkToDelete();
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002AC2C File Offset: 0x00028E2C
		public void AddToSafetyNet()
		{
			if (this.IsInSafetyNet)
			{
				return;
			}
			if (this.DeliveredDestination == null)
			{
				throw new InvalidOperationException("DeliveredDestination cannot be null.");
			}
			if (this.DeliveryTime == null)
			{
				throw new InvalidOperationException("DeliveryTime cannot be null.");
			}
			this.DeliveryTimeOffsetColumn.Value = MailRecipientStorage.GetTimeOffset(this.DeliveryTime.Value);
			this.DestinationHashColumn.Value = this.DeliveredDestination.GetHashCode();
			this.Table.IncrementSafetyNetRecipientCount(this.DeliveredDestination.Type);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002ACBB File Offset: 0x00028EBB
		private void ReleaseFromSafetyNet()
		{
			if (this.IsInSafetyNet)
			{
				this.DeliveryTimeOffsetColumn.HasValue = false;
				this.DestinationHashColumn.HasValue = false;
				this.Table.DecrementSafetyNetRecipientCount(this.DeliveredDestination.Type);
			}
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002ACF4 File Offset: 0x00028EF4
		private void AddToActive()
		{
			if (!this.IsActive)
			{
				this.UndeliveredMessageRowIdColumn.Value = this.MessageRowId;
				this.Table.IncrementActiveRecipientCount();
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002AD1B File Offset: 0x00028F1B
		private void ThrowIfDeleted()
		{
			if (this.IsDeleted)
			{
				throw new InvalidOperationException("operations not allowed on a deleted recipient");
			}
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002AD30 File Offset: 0x00028F30
		private byte GetDsn(byte offset)
		{
			return (byte)((this.DsnColumn.Value & 255 << (int)offset) >> (int)offset);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002AD4E File Offset: 0x00028F4E
		private void SetDsn(byte offset, byte value)
		{
			this.DsnColumn.Value = ((this.DsnColumn.Value & ~(255 << (int)offset)) | (int)value << (int)offset);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002AD79 File Offset: 0x00028F79
		private void InitializeDefaults()
		{
			this.Email = string.Empty;
			this.DsnRequested = DsnRequestedFlags.Default;
			this.DsnNeeded = DsnFlags.None;
			this.DsnCompleted = DsnFlags.None;
			this.Status = Status.Ready;
			this.RetryCount = 0;
			this.AdminActionStatus = AdminActionStatus.None;
		}

		// Token: 0x04000511 RID: 1297
		private const int DsnNeededOffset = 0;

		// Token: 0x04000512 RID: 1298
		private const int DsnRequestedOffset = 8;

		// Token: 0x04000513 RID: 1299
		private const int DsnCompletedOffset = 16;

		// Token: 0x04000514 RID: 1300
		private static readonly DateTime TimeOffsetReference = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04000515 RID: 1301
		private static readonly DateTime MinDate = MailRecipientStorage.GetTimeFromOffset(int.MinValue);

		// Token: 0x04000516 RID: 1302
		private static readonly DateTime MaxDate = MailRecipientStorage.GetTimeFromOffset(int.MaxValue);

		// Token: 0x04000517 RID: 1303
		private readonly ExtendedPropertyDictionary componentExtendedProperties;

		// Token: 0x04000518 RID: 1304
		private readonly ExtendedPropertyDictionary componentInternalProperties;

		// Token: 0x04000519 RID: 1305
		private Destination deliveredDestination;

		// Token: 0x02000111 RID: 273
		private enum BlobCollectionKeys : byte
		{
			// Token: 0x0400051B RID: 1307
			ExtendedProperties = 1,
			// Token: 0x0400051C RID: 1308
			InternalProperties
		}
	}
}
