using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim
{
	// Token: 0x020000D5 RID: 213
	[Serializable]
	public class PimSubscriptionProxy : IConfigurable
	{
		// Token: 0x06000623 RID: 1571 RVA: 0x0001F17C File Offset: 0x0001D37C
		public PimSubscriptionProxy() : this(new PimAggregationSubscription())
		{
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001F189 File Offset: 0x0001D389
		internal PimSubscriptionProxy(PimAggregationSubscription subscription)
		{
			if (subscription == null)
			{
				throw new ArgumentNullException("subscription");
			}
			this.subscription = subscription;
			this.objectState = (subscription.IsNew ? ObjectState.New : ObjectState.Unchanged);
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001F1B8 File Offset: 0x0001D3B8
		public Guid SubscriptionGuid
		{
			get
			{
				return this.subscription.SubscriptionGuid;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x0001F1C5 File Offset: 0x0001D3C5
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x0001F1D9 File Offset: 0x0001D3D9
		public string Name
		{
			get
			{
				return this.RedactIfNeeded(this.subscription.Name, false);
			}
			internal set
			{
				this.subscription.Name = value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0001F1E7 File Offset: 0x0001D3E7
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x0001F1F4 File Offset: 0x0001D3F4
		public SubscriptionCreationType CreationType
		{
			get
			{
				return this.subscription.CreationType;
			}
			set
			{
				this.subscription.CreationType = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0001F202 File Offset: 0x0001D402
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x0001F20F File Offset: 0x0001D40F
		public AggregationType AggregationType
		{
			get
			{
				return this.subscription.AggregationType;
			}
			set
			{
				this.subscription.AggregationType = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001F21D File Offset: 0x0001D41D
		public SyncPhase SyncPhase
		{
			get
			{
				return this.subscription.SyncPhase;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0001F22A File Offset: 0x0001D42A
		public long Version
		{
			get
			{
				return this.subscription.Version;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001F237 File Offset: 0x0001D437
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0001F24B File Offset: 0x0001D44B
		public string DisplayName
		{
			get
			{
				return this.RedactIfNeeded(this.subscription.UserDisplayName, false);
			}
			set
			{
				this.subscription.UserDisplayName = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0001F259 File Offset: 0x0001D459
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0001F26C File Offset: 0x0001D46C
		public SmtpAddress EmailAddress
		{
			get
			{
				return this.RedactIfNeeded(this.subscription.UserEmailAddress);
			}
			set
			{
				this.subscription.UserEmailAddress = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001F27A File Offset: 0x0001D47A
		public AggregationSubscriptionType SubscriptionType
		{
			get
			{
				return this.subscription.SubscriptionType;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0001F287 File Offset: 0x0001D487
		public AggregationStatus Status
		{
			get
			{
				return this.subscription.Status;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001F294 File Offset: 0x0001D494
		public DetailedAggregationStatus DetailedAggregationStatus
		{
			get
			{
				return this.subscription.DetailedAggregationStatus;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001F2A1 File Offset: 0x0001D4A1
		public DateTime LastModifiedTime
		{
			get
			{
				return this.subscription.LastModifiedTime;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001F2AE File Offset: 0x0001D4AE
		public DateTime? LastSyncTime
		{
			get
			{
				return this.subscription.LastSyncTime;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0001F2BC File Offset: 0x0001D4BC
		public DateTime? LastSuccessfulSync
		{
			get
			{
				if (this.subscription.LastSuccessfulSyncTime != null && this.subscription.LastSuccessfulSyncTime.Value == SyncUtilities.ZeroTime)
				{
					return null;
				}
				return this.subscription.LastSuccessfulSyncTime;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0001F312 File Offset: 0x0001D512
		public bool IsWarningStatus
		{
			get
			{
				return this.Status == AggregationStatus.Delayed || this.Status == AggregationStatus.InvalidVersion;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001F328 File Offset: 0x0001D528
		public bool IsErrorStatus
		{
			get
			{
				return this.Status == AggregationStatus.Disabled || this.Status == AggregationStatus.Poisonous;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001F33E File Offset: 0x0001D53E
		public bool IsSuccessStatus
		{
			get
			{
				return this.Status == AggregationStatus.Succeeded || this.Status == AggregationStatus.InProgress;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0001F354 File Offset: 0x0001D554
		public string StatusDescription
		{
			get
			{
				switch (this.Status)
				{
				case AggregationStatus.Succeeded:
					return Strings.SuccessStatus;
				case AggregationStatus.InProgress:
					return Strings.InProgressStatus;
				case AggregationStatus.Delayed:
					switch (this.DetailedAggregationStatus)
					{
					case DetailedAggregationStatus.AuthenticationError:
						return Strings.AuthenticationErrorWithDelayedStatus;
					case DetailedAggregationStatus.ConnectionError:
						return Strings.ConnectionErrorWithDelayedStatus;
					case DetailedAggregationStatus.CommunicationError:
						return Strings.CommunicationErrorWithDelayedStatus;
					case DetailedAggregationStatus.RemoteMailboxQuotaWarning:
						return Strings.RemoteMailboxQuotaWarningWithDelayedStatus;
					case DetailedAggregationStatus.LabsMailboxQuotaWarning:
						return Strings.LabsMailboxQuotaWarningWithDelayedStatus;
					case DetailedAggregationStatus.MaxedOutSyncRelationshipsError:
						return Strings.MaxedOutSyncRelationshipsErrorWithDelayedStatus;
					case DetailedAggregationStatus.LeaveOnServerNotSupported:
						return Strings.LeaveOnServerNotSupportedStatus;
					case DetailedAggregationStatus.RemoteAccountDoesNotExist:
						return Strings.RemoteAccountDoesNotExistStatus;
					case DetailedAggregationStatus.RemoteServerIsSlow:
					case DetailedAggregationStatus.RemoteServerIsBackedOff:
						return Strings.RemoteServerIsSlowStatus;
					case DetailedAggregationStatus.TooManyFolders:
						return Strings.TooManyFoldersStatus;
					case DetailedAggregationStatus.ProviderException:
						return Strings.ProviderExceptionStatus;
					}
					return Strings.DelayedStatus;
				case AggregationStatus.Disabled:
					switch (this.DetailedAggregationStatus)
					{
					case DetailedAggregationStatus.AuthenticationError:
						return Strings.AuthenticationErrorWithDisabledStatus;
					case DetailedAggregationStatus.ConnectionError:
						return Strings.ConnectionErrorWithDisabledStatus;
					case DetailedAggregationStatus.CommunicationError:
						return Strings.CommunicationErrorWithDisabledStatus;
					case DetailedAggregationStatus.RemoteMailboxQuotaWarning:
						return Strings.RemoteMailboxQuotaWarningWithDisabledStatus;
					case DetailedAggregationStatus.LabsMailboxQuotaWarning:
						return Strings.LabsMailboxQuotaWarningWithDisabledStatus;
					case DetailedAggregationStatus.MaxedOutSyncRelationshipsError:
						return Strings.MaxedOutSyncRelationshipsErrorWithDisabledStatus;
					case DetailedAggregationStatus.LeaveOnServerNotSupported:
						return Strings.LeaveOnServerNotSupportedStatus;
					case DetailedAggregationStatus.RemoteAccountDoesNotExist:
						return Strings.RemoteAccountDoesNotExistStatus;
					case DetailedAggregationStatus.RemoteServerIsSlow:
					case DetailedAggregationStatus.RemoteServerIsBackedOff:
					case DetailedAggregationStatus.RemoteServerIsPoisonous:
						return Strings.RemoteServerIsSlowStatus;
					case DetailedAggregationStatus.TooManyFolders:
						return Strings.TooManyFoldersStatus;
					case DetailedAggregationStatus.SyncStateSizeError:
						return Strings.SyncStateSizeErrorStatus;
					case DetailedAggregationStatus.ConfigurationError:
						return Strings.ConfigurationErrorStatus;
					case DetailedAggregationStatus.RemoveSubscription:
						return Strings.RemoveSubscriptionStatus;
					}
					return Strings.DisabledStatus;
				case AggregationStatus.Poisonous:
					return Strings.PoisonStatus;
				case AggregationStatus.InvalidVersion:
					return Strings.InvalidVersionStatus;
				default:
					return LocalizedString.Empty;
				}
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001F590 File Offset: 0x0001D790
		public string DetailedStatus
		{
			get
			{
				switch (this.Status)
				{
				case AggregationStatus.Succeeded:
					return Strings.SuccessDetailedStatus;
				case AggregationStatus.InProgress:
					return Strings.InProgressDetailedStatus;
				case AggregationStatus.Delayed:
					switch (this.DetailedAggregationStatus)
					{
					case DetailedAggregationStatus.AuthenticationError:
						return Strings.AuthenticationErrorWithDelayedDetailedStatus;
					case DetailedAggregationStatus.ConnectionError:
						return this.GetConnectionErrorDetailedStatus();
					case DetailedAggregationStatus.CommunicationError:
						return Strings.CommunicationErrorWithDelayedDetailedStatus;
					case DetailedAggregationStatus.RemoteMailboxQuotaWarning:
						return Strings.RemoteMailboxQuotaWarningWithDelayedDetailedStatus;
					case DetailedAggregationStatus.LabsMailboxQuotaWarning:
						return Strings.LabsMailboxQuotaWarningWithDelayedDetailedStatus;
					case DetailedAggregationStatus.MaxedOutSyncRelationshipsError:
						return Strings.MaxedOutSyncRelationshipsErrorWithDelayedDetailedStatus;
					case DetailedAggregationStatus.LeaveOnServerNotSupported:
						return Strings.LeaveOnServerNotSupportedDetailedStatus;
					case DetailedAggregationStatus.RemoteAccountDoesNotExist:
						return Strings.RemoteAccountDoesNotExistDetailedStatus;
					case DetailedAggregationStatus.RemoteServerIsSlow:
					case DetailedAggregationStatus.RemoteServerIsBackedOff:
						return Strings.RemoteServerIsSlowDelayedDetailedStatus;
					case DetailedAggregationStatus.TooManyFolders:
						return Strings.TooManyFoldersDetailedStatus;
					}
					return this.GetDefaultDelayedStatus();
				case AggregationStatus.Disabled:
					switch (this.DetailedAggregationStatus)
					{
					case DetailedAggregationStatus.AuthenticationError:
						return Strings.AuthenticationErrorWithDisabledDetailedStatus;
					case DetailedAggregationStatus.ConnectionError:
						return this.GetConnectionErrorDetailedStatus();
					case DetailedAggregationStatus.CommunicationError:
						return Strings.CommunicationErrorWithDisabledDetailedStatus;
					case DetailedAggregationStatus.RemoteMailboxQuotaWarning:
						return Strings.RemoteMailboxQuotaWarningWithDisabledDetailedStatus;
					case DetailedAggregationStatus.LabsMailboxQuotaWarning:
						return Strings.LabsMailboxQuotaWarningWithDisabledDetailedStatus;
					case DetailedAggregationStatus.MaxedOutSyncRelationshipsError:
						return Strings.MaxedOutSyncRelationshipsErrorWithDisabledDetailedStatus;
					case DetailedAggregationStatus.LeaveOnServerNotSupported:
						return Strings.LeaveOnServerNotSupportedDetailedStatus;
					case DetailedAggregationStatus.RemoteAccountDoesNotExist:
						return Strings.RemoteAccountDoesNotExistDetailedStatus;
					case DetailedAggregationStatus.RemoteServerIsSlow:
					case DetailedAggregationStatus.RemoteServerIsBackedOff:
					case DetailedAggregationStatus.RemoteServerIsPoisonous:
						return Strings.RemoteServerIsSlowDisabledDetailedStatus;
					case DetailedAggregationStatus.TooManyFolders:
						return Strings.TooManyFoldersDetailedStatus;
					case DetailedAggregationStatus.SyncStateSizeError:
						return Strings.SyncStateSizeErrorDetailedStatus;
					case DetailedAggregationStatus.ProviderException:
						return Strings.ProviderExceptionDetailedStatus;
					}
					return Strings.DisabledDetailedStatus;
				case AggregationStatus.Poisonous:
					return Strings.PoisonDetailedStatus;
				case AggregationStatus.InvalidVersion:
					return Strings.InvalidVersionDetailedStatus;
				default:
					return LocalizedString.Empty;
				}
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0001F79A File Offset: 0x0001D99A
		public SendAsState SendAsState
		{
			get
			{
				return this.subscription.SendAsState;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0001F7A7 File Offset: 0x0001D9A7
		public VerificationEmailState VerificationEmailState
		{
			get
			{
				return this.subscription.VerificationEmailState;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0001F7B4 File Offset: 0x0001D9B4
		public string VerificationEmailMessageId
		{
			get
			{
				return this.subscription.VerificationEmailMessageId;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0001F7C1 File Offset: 0x0001D9C1
		public DateTime? VerificationEmailTimeStamp
		{
			get
			{
				return this.subscription.VerificationEmailTimeStamp;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0001F7CE File Offset: 0x0001D9CE
		public ObjectId Identity
		{
			get
			{
				return this.subscription.SubscriptionIdentity;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0001F7DB File Offset: 0x0001D9DB
		public virtual bool IsValid
		{
			get
			{
				return this.Validate().Length == 0 && this.subscription.IsValid;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001F7F4 File Offset: 0x0001D9F4
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x0001F7FC File Offset: 0x0001D9FC
		public virtual ObjectState ObjectState
		{
			get
			{
				return this.objectState;
			}
			set
			{
				this.objectState = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0001F805 File Offset: 0x0001DA05
		public string Diagnostics
		{
			get
			{
				if (!this.debugOn)
				{
					return string.Empty;
				}
				return this.RedactIfNeeded(this.subscription.Diagnostics, true);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x0001F827 File Offset: 0x0001DA27
		public string PoisonCallstack
		{
			get
			{
				if (!this.debugOn)
				{
					return string.Empty;
				}
				return this.subscription.PoisonCallstack;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0001F842 File Offset: 0x0001DA42
		public DateTime CreationTime
		{
			get
			{
				return this.subscription.CreationTime;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x0001F84F File Offset: 0x0001DA4F
		public DateTime AdjustedLastSuccessfulSyncTime
		{
			get
			{
				return this.subscription.AdjustedLastSuccessfulSyncTime;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0001F85C File Offset: 0x0001DA5C
		public string FoldersToExclude
		{
			get
			{
				return this.RedactIfNeeded(this.subscription.FoldersToExclude, true);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0001F870 File Offset: 0x0001DA70
		public string OutageDetectionDiagnostics
		{
			get
			{
				return this.RedactIfNeeded(this.subscription.OutageDetectionDiagnostics, true);
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0001F884 File Offset: 0x0001DA84
		public long SubscriptionVersion
		{
			get
			{
				return this.subscription.Version;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0001F891 File Offset: 0x0001DA91
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x0001F8A3 File Offset: 0x0001DAA3
		public Report Report
		{
			get
			{
				if (!this.NeedSuppressingPiiData)
				{
					return this.report;
				}
				return null;
			}
			set
			{
				this.report = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001F8AC File Offset: 0x0001DAAC
		public long TotalItemsSynced
		{
			get
			{
				return this.subscription.ItemsSynced;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0001F8B9 File Offset: 0x0001DAB9
		public long TotalItemsSkipped
		{
			get
			{
				return this.subscription.ItemsSkipped;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0001F8C6 File Offset: 0x0001DAC6
		public long? TotalItemsInSourceMailbox
		{
			get
			{
				return this.subscription.TotalItemsInSourceMailbox;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0001F8D4 File Offset: 0x0001DAD4
		public string TotalSizeOfSourceMailbox
		{
			get
			{
				if (this.subscription.TotalSizeOfSourceMailbox != null)
				{
					return new ByteQuantifiedSize(Convert.ToUInt64(this.subscription.TotalSizeOfSourceMailbox.Value)).ToString();
				}
				return null;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001F923 File Offset: 0x0001DB23
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x0001F92B File Offset: 0x0001DB2B
		public bool NeedSuppressingPiiData
		{
			get
			{
				return this.needSuppressingPiiData;
			}
			set
			{
				this.needSuppressingPiiData = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0001F934 File Offset: 0x0001DB34
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x0001F93C File Offset: 0x0001DB3C
		internal PimAggregationSubscription Subscription
		{
			get
			{
				return this.subscription;
			}
			set
			{
				this.subscription = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001F945 File Offset: 0x0001DB45
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x0001F94D File Offset: 0x0001DB4D
		internal bool SendAsCheckNeeded
		{
			get
			{
				return this.sendAsCheckNeeded;
			}
			set
			{
				this.sendAsCheckNeeded = value;
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001F956 File Offset: 0x0001DB56
		public override string ToString()
		{
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001F974 File Offset: 0x0001DB74
		public virtual ValidationError[] Validate()
		{
			ICollection<ValidationError> collection = PimSubscriptionValidator.Validate(this);
			ValidationError[] array = new ValidationError[collection.Count];
			collection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001F9A0 File Offset: 0x0001DBA0
		public virtual void CopyChangesFrom(IConfigurable source)
		{
			PimSubscriptionProxy pimSubscriptionProxy = source as PimSubscriptionProxy;
			this.subscription = pimSubscriptionProxy.subscription;
			this.objectState = pimSubscriptionProxy.ObjectState;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001F9CC File Offset: 0x0001DBCC
		public virtual void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001F9D4 File Offset: 0x0001DBD4
		protected string RedactIfNeeded(string value, bool omit = false)
		{
			if (!this.NeedSuppressingPiiData)
			{
				return value;
			}
			if (!omit)
			{
				return SuppressingPiiData.Redact(value);
			}
			string text;
			string text2;
			return SuppressingPiiData.RedactWithoutHashing(value, out text, out text2);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001FA00 File Offset: 0x0001DC00
		protected SmtpAddress RedactIfNeeded(SmtpAddress value)
		{
			if (this.NeedSuppressingPiiData)
			{
				string text;
				string text2;
				return SuppressingPiiData.Redact(value, out text, out text2);
			}
			return value;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001FA21 File Offset: 0x0001DC21
		internal void SetDebug(bool debugOn)
		{
			this.debugOn = debugOn;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001FA2C File Offset: 0x0001DC2C
		private string GetConnectionErrorDetailedStatus()
		{
			int num;
			int num2;
			SyncUtilities.GetHoursAndDaysWithoutSuccessfulSync(this.subscription, false, out num, out num2);
			return SyncUtilities.SelectTimeBasedString(num, num2, Strings.ConnectionErrorDetailedStatusDay(num), Strings.ConnectionErrorDetailedStatusDays(num), Strings.ConnectionErrorDetailedStatusHour(num2), Strings.ConnectionErrorDetailedStatusHours(num2), Strings.ConnectionErrorDetailedStatus);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001FA88 File Offset: 0x0001DC88
		private string GetDefaultDelayedStatus()
		{
			int num;
			int num2;
			SyncUtilities.GetHoursAndDaysWithoutSuccessfulSync(this.subscription, false, out num, out num2);
			return SyncUtilities.SelectTimeBasedString(num, num2, Strings.DelayedDetailedStatusDay(num), Strings.DelayedDetailedStatusDays(num), Strings.DelayedDetailedStatusHour(num2), Strings.DelayedDetailedStatusHours(num2), Strings.DelayedDetailedStatus);
		}

		// Token: 0x04000376 RID: 886
		private ObjectState objectState;

		// Token: 0x04000377 RID: 887
		private PimAggregationSubscription subscription;

		// Token: 0x04000378 RID: 888
		private bool debugOn;

		// Token: 0x04000379 RID: 889
		private bool sendAsCheckNeeded;

		// Token: 0x0400037A RID: 890
		private Report report;

		// Token: 0x0400037B RID: 891
		[NonSerialized]
		private bool needSuppressingPiiData;
	}
}
