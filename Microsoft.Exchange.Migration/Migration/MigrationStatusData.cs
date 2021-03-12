using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000082 RID: 130
	internal class MigrationStatusData<TStatus> : IMigrationSerializable where TStatus : struct
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x00021E3C File Offset: 0x0002003C
		public MigrationStatusData(TStatus status, long version, MigrationState? state = null) : this(version)
		{
			this.Status = status;
			this.StateLastUpdated = new ExDateTime?(ExDateTime.UtcNow);
			if (state != null)
			{
				this.State = state.Value;
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00021E72 File Offset: 0x00020072
		public MigrationStatusData(TStatus status, Exception error, long version, MigrationState? state = null) : this(status, version, state)
		{
			this.FailureRecord = MigrationStatusData<TStatus>.GetFailureRecord(error);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00021E8A File Offset: 0x0002008A
		public MigrationStatusData(long version)
		{
			this.StatusHistory = string.Empty;
			this.Version = version;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00021EA4 File Offset: 0x000200A4
		internal MigrationStatusData(MigrationStatusData<TStatus> statusData)
		{
			this.Status = statusData.Status;
			this.State = statusData.State;
			this.StateLastUpdated = statusData.StateLastUpdated;
			this.TransientErrorCount = statusData.TransientErrorCount;
			this.PreviousStatus = statusData.PreviousStatus;
			this.InternalError = statusData.InternalError;
			this.InternalErrorTime = statusData.InternalErrorTime;
			this.StatusHistory = statusData.StatusHistory;
			this.SameStatusCount = statusData.SameStatusCount;
			this.FailureRecord = statusData.FailureRecord;
			this.WatsonHash = statusData.WatsonHash;
			this.Version = statusData.Version;
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00021F47 File Offset: 0x00020147
		// (set) Token: 0x06000761 RID: 1889 RVA: 0x00021F4F File Offset: 0x0002014F
		public FailureRec FailureRecord { get; private set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00021F58 File Offset: 0x00020158
		// (set) Token: 0x06000763 RID: 1891 RVA: 0x00021F60 File Offset: 0x00020160
		public string InternalError { get; private set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x00021F69 File Offset: 0x00020169
		// (set) Token: 0x06000765 RID: 1893 RVA: 0x00021F71 File Offset: 0x00020171
		public int TransientErrorCount { get; private set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x00021F7A File Offset: 0x0002017A
		// (set) Token: 0x06000767 RID: 1895 RVA: 0x00021F82 File Offset: 0x00020182
		public ExDateTime? StateLastUpdated { get; protected set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x00021F8C File Offset: 0x0002018C
		public LocalizedString? LocalizedError
		{
			get
			{
				if (this.FailureRecord != null)
				{
					return new LocalizedString?(new LocalizedString(this.FailureRecord.Message));
				}
				return null;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x00021FC0 File Offset: 0x000201C0
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x00021FC8 File Offset: 0x000201C8
		public ExDateTime? InternalErrorTime { get; private set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x00021FD1 File Offset: 0x000201D1
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x00021FD9 File Offset: 0x000201D9
		public TStatus Status { get; private set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x00021FE2 File Offset: 0x000201E2
		// (set) Token: 0x0600076E RID: 1902 RVA: 0x00021FEA File Offset: 0x000201EA
		public MigrationState State { get; private set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x00021FF3 File Offset: 0x000201F3
		// (set) Token: 0x06000770 RID: 1904 RVA: 0x00021FFB File Offset: 0x000201FB
		public TStatus? PreviousStatus { get; private set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00022004 File Offset: 0x00020204
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x0002200C File Offset: 0x0002020C
		public string StatusHistory { get; private set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00022015 File Offset: 0x00020215
		// (set) Token: 0x06000774 RID: 1908 RVA: 0x0002201D File Offset: 0x0002021D
		public int SameStatusCount { get; private set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00022026 File Offset: 0x00020226
		// (set) Token: 0x06000776 RID: 1910 RVA: 0x0002202E File Offset: 0x0002022E
		public string WatsonHash { get; private set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x00022037 File Offset: 0x00020237
		// (set) Token: 0x06000778 RID: 1912 RVA: 0x0002203F File Offset: 0x0002023F
		public long Version { get; private set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00022048 File Offset: 0x00020248
		PropertyDefinition[] IMigrationSerializable.PropertyDefinitions
		{
			get
			{
				return MigrationStatusData<TStatus>.StatusPropertyDefinition;
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00022050 File Offset: 0x00020250
		public override string ToString()
		{
			if (this.Version >= 2L)
			{
				return string.Format("{0}:{1}", this.State, this.LocalizedError);
			}
			return string.Format("{0}:{1}:{2}:{3}", new object[]
			{
				this.Status,
				this.StateLastUpdated,
				this.LocalizedError,
				this.InternalError.ToTruncatedString()
			});
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000220D4 File Offset: 0x000202D4
		public bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			MigrationUtil.ThrowOnNullArgument(message, "message");
			if (this.Version >= 2L)
			{
				this.State = MigrationHelper.GetEnumProperty<MigrationState>(message, MigrationBatchMessageSchema.MigrationState);
			}
			this.Status = MigrationHelper.GetEnumProperty<TStatus>(message, MigrationBatchMessageSchema.MigrationUserStatus);
			this.TransientErrorCount = message.GetValueOrDefault<int>(MigrationBatchMessageSchema.MigrationJobItemTransientErrorCount, 0);
			this.PreviousStatus = MigrationHelper.GetEnumPropertyOrNull<TStatus>(message, MigrationBatchMessageSchema.MigrationJobItemPreviousStatus);
			this.InternalError = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobInternalError, null);
			this.StateLastUpdated = MigrationHelperBase.GetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobItemStateLastUpdated);
			this.InternalErrorTime = MigrationHelper.GetExDateTimePropertyOrNull(message, MigrationBatchMessageSchema.MigrationJobInternalErrorTime);
			this.StatusHistory = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobItemStatusHistory, string.Empty);
			this.SameStatusCount = message.GetValueOrDefault<int>(MigrationBatchMessageSchema.MigrationSameStatusCount, 1);
			this.WatsonHash = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationStatusDataFailureWatsonHash, null);
			string valueOrDefault = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationFailureRecord, string.Empty);
			if (string.IsNullOrEmpty(valueOrDefault))
			{
				string valueOrDefault2 = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobItemLocalizedError, null);
				if (!string.IsNullOrEmpty(valueOrDefault2))
				{
					LocalizedString localizedErrorMessage = new LocalizedString(valueOrDefault2);
					MigrationPermanentException ex = new MigrationPermanentException(localizedErrorMessage);
					this.FailureRecord = FailureRec.Create(ex);
				}
			}
			else
			{
				this.FailureRecord = XMLSerializableBase.Deserialize<FailureRec>(valueOrDefault, MigrationBatchMessageSchema.MigrationFailureRecord);
			}
			return true;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00022208 File Offset: 0x00020408
		public void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			MigrationUtil.ThrowOnNullArgument(message, "message");
			message[MigrationBatchMessageSchema.MigrationUserStatus] = this.Status;
			message[MigrationBatchMessageSchema.MigrationJobItemTransientErrorCount] = this.TransientErrorCount;
			message[MigrationBatchMessageSchema.MigrationJobItemStatusHistory] = this.StatusHistory;
			message[MigrationBatchMessageSchema.MigrationSameStatusCount] = this.SameStatusCount;
			if (this.PreviousStatus != null)
			{
				message[MigrationBatchMessageSchema.MigrationJobItemPreviousStatus] = this.PreviousStatus.Value;
			}
			if (this.StateLastUpdated != null)
			{
				MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobItemStateLastUpdated, this.StateLastUpdated);
			}
			if (loaded && string.IsNullOrEmpty(this.InternalError))
			{
				message.Delete(MigrationBatchMessageSchema.MigrationJobInternalError);
			}
			else if (!string.IsNullOrEmpty(this.InternalError))
			{
				message[MigrationBatchMessageSchema.MigrationJobInternalError] = this.InternalError;
			}
			if (loaded && this.InternalErrorTime == null)
			{
				message.Delete(MigrationBatchMessageSchema.MigrationJobInternalErrorTime);
			}
			else if (this.InternalErrorTime != null)
			{
				MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobInternalErrorTime, this.InternalErrorTime);
			}
			if (loaded && this.FailureRecord == null)
			{
				message.Delete(MigrationBatchMessageSchema.MigrationFailureRecord);
			}
			else if (this.FailureRecord != null)
			{
				message[MigrationBatchMessageSchema.MigrationFailureRecord] = MigrationXmlSerializer.Serialize(this.FailureRecord);
			}
			if (loaded && string.IsNullOrEmpty(this.WatsonHash))
			{
				message.Delete(MigrationBatchMessageSchema.MigrationStatusDataFailureWatsonHash);
			}
			else if (!string.IsNullOrEmpty(this.WatsonHash))
			{
				message[MigrationBatchMessageSchema.MigrationStatusDataFailureWatsonHash] = this.WatsonHash;
			}
			if (this.Version >= 2L)
			{
				message[MigrationBatchMessageSchema.MigrationState] = this.State;
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x000223D0 File Offset: 0x000205D0
		public XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			XElement xelement = new XElement("MigrationStatusData", new object[]
			{
				new XElement("state", this.State),
				new XElement("status", this.Status),
				new XElement("stateLastUpdated", this.StateLastUpdated),
				new XElement("previousStatus", this.PreviousStatus),
				new XElement("error", this.LocalizedError),
				new XElement("transientErrorCount", this.TransientErrorCount),
				new XElement("SameStatusCount", this.SameStatusCount),
				new XElement("internalError", this.InternalError),
				new XElement("internalErrorTime", this.InternalErrorTime),
				new XElement("statusHistory", this.StatusHistory),
				new XElement("watsonHash", this.WatsonHash)
			});
			XElement xelement2 = new XElement("FailureRecord");
			if (this.FailureRecord != null)
			{
				xelement2.Add(this.FailureRecord.GetDiagnosticData());
			}
			xelement.Add(xelement2);
			return xelement;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0002255C File Offset: 0x0002075C
		public void ClearTransientErrorCount()
		{
			MigrationEventType eventType = (this.TransientErrorCount > 0) ? MigrationEventType.Information : MigrationEventType.Verbose;
			MigrationLogger.Log(eventType, "resetting transient error count, former {0}", new object[]
			{
				this.TransientErrorCount
			});
			this.TransientErrorCount = 0;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x000225A0 File Offset: 0x000207A0
		public void ClearError()
		{
			MigrationLogger.Log(MigrationEventType.Verbose, "clearing error, former {0}", new object[]
			{
				this
			});
			this.StateLastUpdated = new ExDateTime?(ExDateTime.UtcNow);
			this.InternalError = null;
			this.InternalErrorTime = null;
			this.FailureRecord = null;
			this.ClearTransientErrorCount();
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000225F7 File Offset: 0x000207F7
		public bool UpdateStatus(TStatus status, MigrationState? state = null)
		{
			return this.InternalUpdateStatus(status, null, null, false, state);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00022604 File Offset: 0x00020804
		public bool RevertStatus()
		{
			if (this.PreviousStatus != null)
			{
				MigrationLogger.Log(MigrationEventType.Information, "reverting status: from {0} to {1}", new object[]
				{
					this.PreviousStatus,
					this.Status
				});
				return this.UpdateStatus(this.PreviousStatus.Value, null);
			}
			return false;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00022670 File Offset: 0x00020870
		public void SetTransientError(Exception error, TStatus? status = null, MigrationState? state = null)
		{
			MigrationUtil.ThrowOnNullArgument(error, "error");
			if (this.StateLastUpdated == null || this.TransientErrorCount <= 0)
			{
				this.ResetStateLastUpdated();
			}
			if (status != null && state != null)
			{
				TStatus status2 = this.Status;
				if (!status2.Equals(status.Value) || !this.State.Equals(state.Value))
				{
					this.ResetStateLastUpdated();
					this.PreviousStatus = new TStatus?(this.Status);
					this.Status = status.Value;
					this.State = state.Value;
					this.SameStatusCount = 1;
				}
				else
				{
					this.SameStatusCount++;
				}
			}
			this.TransientErrorCount++;
			this.FailureRecord = FailureRec.Create(error);
			MigrationLogger.Log(MigrationEventType.Warning, "Set TransientError: {0} count {1} original time {2}", new object[]
			{
				this,
				this.TransientErrorCount,
				this.StateLastUpdated
			});
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00022794 File Offset: 0x00020994
		public void SetFailedStatus(TStatus failureStatus, Exception exception, string internalError, MigrationState? state = null)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(internalError, "internalError");
			this.UpdateStatus(failureStatus, exception, internalError, false, state);
			MigrationLogger.Log(MigrationEventType.Warning, "set corrupt status {0}", new object[]
			{
				this
			});
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000227D0 File Offset: 0x000209D0
		public bool UpdateStatus(TStatus status, Exception localizedError, string internalError, MigrationState? state = null)
		{
			return this.UpdateStatus(status, localizedError, internalError, false, state);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x000227DE File Offset: 0x000209DE
		public bool UpdateStatus(TStatus status, Exception exception, string internalError, bool forceUpdate, MigrationState? state = null)
		{
			if (internalError != null)
			{
				internalError = MigrationLogger.GetDiagnosticInfo(new StackTrace(), internalError);
			}
			return this.InternalUpdateStatus(status, exception, internalError, forceUpdate, state);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00022800 File Offset: 0x00020A00
		internal static MigrationStatusData<TStatus> Create(IMigrationStoreObject message, long version)
		{
			MigrationStatusData<TStatus> migrationStatusData = new MigrationStatusData<TStatus>(version);
			migrationStatusData.ReadFromMessageItem(message);
			return migrationStatusData;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0002281D File Offset: 0x00020A1D
		private static FailureRec GetFailureRecord(Exception exception)
		{
			return FailureRec.Create(exception);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00022828 File Offset: 0x00020A28
		private bool InternalUpdateStatus(TStatus status, Exception exception, string internalError, bool forceUpdate, MigrationState? state = null)
		{
			if (exception != null && string.IsNullOrEmpty(internalError))
			{
				throw new ArgumentException("when setting localized error, always set internal error as well");
			}
			bool flag = false;
			FailureRec failureRec = FailureRec.Create(exception);
			string x = (this.FailureRecord == null) ? string.Empty : this.FailureRecord.FailureType;
			bool flag2 = failureRec == null || !StringComparer.InvariantCultureIgnoreCase.Equals(x, failureRec.FailureType);
			MigrationState migrationState = (state != null) ? state.Value : this.State;
			TStatus status2 = this.Status;
			if (!status2.Equals(status) || !this.State.Equals(migrationState))
			{
				this.ResetStateLastUpdated();
				this.PreviousStatus = new TStatus?(this.Status);
				this.Status = status;
				this.State = migrationState;
				this.SameStatusCount = 1;
				MigrationLogger.Log(MigrationEventType.Information, "update status: {0} previous status {1}", new object[]
				{
					this,
					this.PreviousStatus
				});
				if (flag2)
				{
					this.WatsonHash = null;
					this.ClearError();
				}
				flag = true;
			}
			else
			{
				if (forceUpdate)
				{
					this.ResetStateLastUpdated();
				}
				this.SameStatusCount++;
			}
			this.ClearTransientErrorCount();
			if (flag2)
			{
				this.FailureRecord = MigrationStatusData<TStatus>.GetFailureRecord(exception);
			}
			if (!string.IsNullOrEmpty(internalError))
			{
				this.SetInternalError(internalError, flag);
			}
			if (exception != null)
			{
				string watsonHash;
				if (ConfigBase<MigrationServiceConfigSchema>.GetConfig<bool>("SendGenericWatson"))
				{
					CommonUtils.SendGenericWatson(exception, internalError, out watsonHash);
				}
				else
				{
					watsonHash = CommonUtils.ComputeCallStackHash(exception, 5);
				}
				this.WatsonHash = watsonHash;
			}
			MigrationLogger.Log(MigrationEventType.Verbose, "set status {0}, did an update occur {1}", new object[]
			{
				this,
				flag
			});
			return flag;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000229D4 File Offset: 0x00020BD4
		private void ResetStateLastUpdated()
		{
			ExDateTime utcNow = ExDateTime.UtcNow;
			if (this.StateLastUpdated == null)
			{
				this.StateLastUpdated = new ExDateTime?(utcNow);
			}
			this.StatusHistory = MigrationHelper.AppendDiagnosticHistory(this.StatusHistory, new string[]
			{
				Convert.ToInt32(this.Status).ToString(CultureInfo.InvariantCulture),
				string.Format("{0:N0}", (utcNow - this.StateLastUpdated.Value).TotalSeconds)
			});
			this.StateLastUpdated = new ExDateTime?(utcNow);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00022A78 File Offset: 0x00020C78
		private void SetInternalError(string internalError, bool useStateLastUpdated)
		{
			if (this.StateLastUpdated == null)
			{
				throw new MigrationDataCorruptionException(string.Format("expect state last updated to be set {0}", this));
			}
			ExDateTime value = useStateLastUpdated ? this.StateLastUpdated.Value : ExDateTime.UtcNow;
			this.InternalError = internalError;
			this.InternalErrorTime = new ExDateTime?(value);
		}

		// Token: 0x0400031C RID: 796
		internal const long MinimumVersion = 1L;

		// Token: 0x0400031D RID: 797
		internal const long PAWVersion = 2L;

		// Token: 0x0400031E RID: 798
		internal static readonly PropertyDefinition[] StatusPropertyDefinition = new PropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationUserStatus,
			MigrationBatchMessageSchema.MigrationJobItemStateLastUpdated,
			MigrationBatchMessageSchema.MigrationJobItemTransientErrorCount,
			MigrationBatchMessageSchema.MigrationJobItemPreviousStatus,
			MigrationBatchMessageSchema.MigrationJobItemLocalizedError,
			MigrationBatchMessageSchema.MigrationJobItemLocalizedErrorID,
			MigrationBatchMessageSchema.MigrationJobInternalError,
			MigrationBatchMessageSchema.MigrationJobInternalErrorTime,
			MigrationBatchMessageSchema.MigrationJobItemStatusHistory,
			MigrationBatchMessageSchema.MigrationJobItemLocalizedMessage,
			MigrationBatchMessageSchema.MigrationJobItemLocalizedMessageID,
			MigrationBatchMessageSchema.MigrationSameStatusCount,
			MigrationBatchMessageSchema.MigrationFailureRecord,
			MigrationBatchMessageSchema.MigrationStatusDataFailureWatsonHash,
			MigrationBatchMessageSchema.MigrationState
		};
	}
}
