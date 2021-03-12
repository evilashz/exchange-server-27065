using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.Delivery;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000028 RID: 40
	internal class DeliveryThrottlingLogWorker : IDeliveryThrottlingLogWorker
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000AD1C File Offset: 0x00008F1C
		public KeyValuePair<Dictionary<string, DeliveryThrottlingLogData>, ReaderWriterLockSlim>[] ResourceDicts
		{
			get
			{
				return this.resourceDicts;
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000AD24 File Offset: 0x00008F24
		public DeliveryThrottlingLogWorker(IDeliveryThrottlingLog deliveryThrottlingLog)
		{
			ArgumentValidator.ThrowIfNull("deliveryThrottlingLog", deliveryThrottlingLog);
			this.resourceDicts = new KeyValuePair<Dictionary<string, DeliveryThrottlingLogData>, ReaderWriterLockSlim>[]
			{
				new KeyValuePair<Dictionary<string, DeliveryThrottlingLogData>, ReaderWriterLockSlim>(this.serverThrottleInfo, this.serverThrottleInfoLock),
				new KeyValuePair<Dictionary<string, DeliveryThrottlingLogData>, ReaderWriterLockSlim>(this.mdbThrottleInfoDynamicThrottleDisabled, this.mdbThrottleInfoDynamicThrottleDisabledLock),
				new KeyValuePair<Dictionary<string, DeliveryThrottlingLogData>, ReaderWriterLockSlim>(this.mdbThrottleInfoPendingConnections, this.mdbThrottleInfoPendingConnectionsLock),
				new KeyValuePair<Dictionary<string, DeliveryThrottlingLogData>, ReaderWriterLockSlim>(this.mdbThrottleInfoTimeout, this.mdbThrottleInfoTimeoutLock),
				new KeyValuePair<Dictionary<string, DeliveryThrottlingLogData>, ReaderWriterLockSlim>(this.recipientThrottleInfo, this.recipientThrottleInfoLock),
				new KeyValuePair<Dictionary<string, DeliveryThrottlingLogData>, ReaderWriterLockSlim>(this.concurrentMsgSizeThrottleInfo, this.concurrentMsgSizeThrottleInfoLock)
			};
			this.deliveryThrottlingLog = deliveryThrottlingLog;
			if (this.deliveryThrottlingLog.Enabled)
			{
				this.deliveryThrottlingLog.TrackSummary += this.LogSummaryData;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000AEAD File Offset: 0x000090AD
		public IDeliveryThrottlingLog DeliveryThrottlingLog
		{
			get
			{
				return this.deliveryThrottlingLog;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000AEB8 File Offset: 0x000090B8
		public void TrackMDBServerThrottle(bool isThrottle, double mdbServerThreadThreshold)
		{
			if (!this.deliveryThrottlingLog.Enabled)
			{
				return;
			}
			List<KeyValuePair<string, string>> list = null;
			if (mdbServerThreadThreshold <= 0.0)
			{
				list = new List<KeyValuePair<string, string>>();
				list.Add(new KeyValuePair<string, string>("InvalidServerThreadThreshold", mdbServerThreadThreshold.ToString("F0", NumberFormatInfo.InvariantInfo)));
				mdbServerThreadThreshold = double.NaN;
			}
			this.TrackData(this.serverThrottleInfo, this.serverThrottleInfoLock, "localhost", isThrottle, ThrottlingScope.MBServer, ThrottlingResource.Threads, mdbServerThreadThreshold, ThrottlingImpactUnits.Sessions, 1U, Guid.Empty, string.Empty, string.Empty, null, list);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000AF44 File Offset: 0x00009144
		public void TrackMDBThrottle(bool isThrottle, string mdbName, double mdbResourceThreshold, List<KeyValuePair<string, double>> healthMonitorList, ThrottlingResource throttleResource)
		{
			if (!this.deliveryThrottlingLog.Enabled)
			{
				return;
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			if (string.IsNullOrEmpty(mdbName))
			{
				list.Add(new KeyValuePair<string, string>("InvalidMDBName", (mdbName == null) ? "NULL" : "EMPTY"));
				mdbName = string.Empty;
			}
			if (!mdbResourceThreshold.Equals(double.NaN) && mdbResourceThreshold <= 0.0)
			{
				list.Add(new KeyValuePair<string, string>("InvalidMDBResourceThreshold", mdbResourceThreshold.ToString("F0", NumberFormatInfo.InvariantInfo)));
				mdbResourceThreshold = double.NaN;
			}
			if (list.Count == 0)
			{
				list = null;
			}
			Dictionary<string, DeliveryThrottlingLogData> dictionary = null;
			ReaderWriterLockSlim dictionaryLock = null;
			switch (throttleResource)
			{
			case ThrottlingResource.Threads:
				dictionary = this.mdbThrottleInfoDynamicThrottleDisabled;
				dictionaryLock = this.mdbThrottleInfoDynamicThrottleDisabledLock;
				break;
			case ThrottlingResource.Threads_MaxPerHub:
				dictionary = this.mdbThrottleInfoPendingConnections;
				dictionaryLock = this.mdbThrottleInfoPendingConnectionsLock;
				break;
			case ThrottlingResource.Threads_PendingConnectionTimedOut:
				dictionary = this.mdbThrottleInfoTimeout;
				dictionaryLock = this.mdbThrottleInfoTimeoutLock;
				break;
			}
			this.TrackData(dictionary, dictionaryLock, mdbName, isThrottle, ThrottlingScope.MDB, throttleResource, mdbResourceThreshold, ThrottlingImpactUnits.Sessions, 1U, Guid.Empty, string.Empty, mdbName, healthMonitorList, list);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000B050 File Offset: 0x00009250
		public void TrackRecipientThrottle(bool isThrottle, string recipient, Guid orgID, string mdbName, double recipientThreadThreshold)
		{
			if (!this.deliveryThrottlingLog.Enabled)
			{
				return;
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			if (string.IsNullOrEmpty(recipient))
			{
				list.Add(new KeyValuePair<string, string>("InvalidRecipient", (recipient == null) ? "NULL" : "EMPTY"));
				recipient = string.Empty;
			}
			if (string.IsNullOrEmpty(mdbName))
			{
				list.Add(new KeyValuePair<string, string>("InvalidMDBName", (mdbName == null) ? "NULL" : "EMPTY"));
				mdbName = string.Empty;
			}
			if (recipientThreadThreshold <= 0.0)
			{
				list.Add(new KeyValuePair<string, string>("InvalidRecipientThreadThreshold", recipientThreadThreshold.ToString("F0", NumberFormatInfo.InvariantInfo)));
				recipientThreadThreshold = double.NaN;
			}
			if (list.Count == 0)
			{
				list = null;
			}
			this.TrackData(this.recipientThrottleInfo, this.recipientThrottleInfoLock, recipient, isThrottle, ThrottlingScope.Recipient, ThrottlingResource.Threads, recipientThreadThreshold, ThrottlingImpactUnits.Recipients, 1U, orgID, recipient, mdbName, null, list);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000B134 File Offset: 0x00009334
		public void TrackConcurrentMessageSizeThrottle(bool isThrottle, ulong concurrentMessageSizeThreshold, int numOfRecipients)
		{
			if (!this.deliveryThrottlingLog.Enabled)
			{
				return;
			}
			List<KeyValuePair<string, string>> list = null;
			if (numOfRecipients <= 0)
			{
				list = new List<KeyValuePair<string, string>>();
				list.Add(new KeyValuePair<string, string>("InvalidRecipientCount", numOfRecipients.ToString("D", NumberFormatInfo.InvariantInfo)));
				numOfRecipients = 1;
			}
			this.TrackData(this.concurrentMsgSizeThrottleInfo, this.concurrentMsgSizeThrottleInfoLock, "localhost", isThrottle, ThrottlingScope.MBServer, ThrottlingResource.Memory, concurrentMessageSizeThreshold, ThrottlingImpactUnits.Recipients, Convert.ToUInt32(numOfRecipients), Guid.Empty, string.Empty, string.Empty, null, list);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000B1B4 File Offset: 0x000093B4
		private void LogSummaryData(string sequenceNumber)
		{
			foreach (KeyValuePair<Dictionary<string, DeliveryThrottlingLogData>, ReaderWriterLockSlim> keyValuePair in this.resourceDicts)
			{
				this.LogSummaryDataPerDictionaryAndClean(keyValuePair.Key, keyValuePair.Value, sequenceNumber);
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000B1F8 File Offset: 0x000093F8
		private void LogSummaryDataPerDictionaryAndClean(Dictionary<string, DeliveryThrottlingLogData> dictionary, ReaderWriterLockSlim dictonaryLock, string sequenceNumber)
		{
			dictonaryLock.EnterWriteLock();
			try
			{
				foreach (KeyValuePair<string, DeliveryThrottlingLogData> keyValuePair in dictionary)
				{
					if (keyValuePair.Value.Impact > 0U)
					{
						this.DeliveryThrottlingLog.LogSummary(sequenceNumber, keyValuePair.Value.ThrottlingScope, keyValuePair.Value.ThrottlingResource, keyValuePair.Value.Threshold, keyValuePair.Value.ImpactUnits, keyValuePair.Value.Impact, Math.Round(keyValuePair.Value.Impact / (double)keyValuePair.Value.Total, 4, MidpointRounding.AwayFromZero), keyValuePair.Value.ExternalOrganizationId, keyValuePair.Value.Recipient, keyValuePair.Value.MDBName, keyValuePair.Value.MDBHealth, keyValuePair.Value.CustomData);
					}
				}
				dictionary.Clear();
			}
			finally
			{
				dictonaryLock.ExitWriteLock();
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000B320 File Offset: 0x00009520
		private void TrackData(Dictionary<string, DeliveryThrottlingLogData> dictionary, ReaderWriterLockSlim dictionaryLock, string key, bool isThrottle, ThrottlingScope scope, ThrottlingResource resource, double threshold, ThrottlingImpactUnits impactUnits, uint impactDelta, Guid tenantID, string recipient, string mdbName, IList<KeyValuePair<string, double>> health, IList<KeyValuePair<string, string>> customData)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			if (dictionaryLock == null)
			{
				throw new ArgumentNullException("dictionaryLock");
			}
			dictionaryLock.EnterWriteLock();
			try
			{
				if (dictionary.ContainsKey(key))
				{
					if (isThrottle)
					{
						dictionary[key].Impact += impactDelta;
					}
					dictionary[key].Total += (long)((ulong)impactDelta);
					if (dictionary[key].MDBHealth == null && health != null && health.Count > 0)
					{
						dictionary[key].MDBHealth = health;
					}
					if (dictionary[key].CustomData == null && customData != null && customData.Count > 0)
					{
						dictionary[key].CustomData = customData;
					}
				}
				else
				{
					DeliveryThrottlingLogData value;
					if (isThrottle)
					{
						value = new DeliveryThrottlingLogData(scope, resource, threshold, impactUnits, impactDelta, (long)((ulong)impactDelta), tenantID, recipient, mdbName, health, customData);
					}
					else
					{
						value = new DeliveryThrottlingLogData(scope, resource, threshold, impactUnits, 0U, (long)((ulong)impactDelta), tenantID, recipient, mdbName, health, customData);
					}
					dictionary.Add(key, value);
				}
			}
			finally
			{
				dictionaryLock.ExitWriteLock();
			}
		}

		// Token: 0x040000D6 RID: 214
		private const string MDBServer = "localhost";

		// Token: 0x040000D7 RID: 215
		private const int NumOfResourceDictionaries = 6;

		// Token: 0x040000D8 RID: 216
		private const string InvalidMDBName = "InvalidMDBName";

		// Token: 0x040000D9 RID: 217
		private const string InvalidRecipient = "InvalidRecipient";

		// Token: 0x040000DA RID: 218
		private const string InvalidServerThreadThreshold = "InvalidServerThreadThreshold";

		// Token: 0x040000DB RID: 219
		private const string InvalidMDBResourceThreshold = "InvalidMDBResourceThreshold";

		// Token: 0x040000DC RID: 220
		private const string InvalidOrgID = "InvalidOrgID";

		// Token: 0x040000DD RID: 221
		private const string InvalidNumOfRecipients = "InvalidRecipientCount";

		// Token: 0x040000DE RID: 222
		private const string InvalidRecipientThreadThreshold = "InvalidRecipientThreadThreshold";

		// Token: 0x040000DF RID: 223
		private const string InvalidNullValue = "NULL";

		// Token: 0x040000E0 RID: 224
		private const string InvalidEmptyValue = "EMPTY";

		// Token: 0x040000E1 RID: 225
		private KeyValuePair<Dictionary<string, DeliveryThrottlingLogData>, ReaderWriterLockSlim>[] resourceDicts;

		// Token: 0x040000E2 RID: 226
		private readonly Dictionary<string, DeliveryThrottlingLogData> serverThrottleInfo = new Dictionary<string, DeliveryThrottlingLogData>();

		// Token: 0x040000E3 RID: 227
		private readonly ReaderWriterLockSlim serverThrottleInfoLock = new ReaderWriterLockSlim();

		// Token: 0x040000E4 RID: 228
		private readonly Dictionary<string, DeliveryThrottlingLogData> mdbThrottleInfoDynamicThrottleDisabled = new Dictionary<string, DeliveryThrottlingLogData>();

		// Token: 0x040000E5 RID: 229
		private readonly ReaderWriterLockSlim mdbThrottleInfoDynamicThrottleDisabledLock = new ReaderWriterLockSlim();

		// Token: 0x040000E6 RID: 230
		private readonly Dictionary<string, DeliveryThrottlingLogData> mdbThrottleInfoPendingConnections = new Dictionary<string, DeliveryThrottlingLogData>();

		// Token: 0x040000E7 RID: 231
		private readonly ReaderWriterLockSlim mdbThrottleInfoPendingConnectionsLock = new ReaderWriterLockSlim();

		// Token: 0x040000E8 RID: 232
		private readonly Dictionary<string, DeliveryThrottlingLogData> mdbThrottleInfoTimeout = new Dictionary<string, DeliveryThrottlingLogData>();

		// Token: 0x040000E9 RID: 233
		private readonly ReaderWriterLockSlim mdbThrottleInfoTimeoutLock = new ReaderWriterLockSlim();

		// Token: 0x040000EA RID: 234
		private readonly Dictionary<string, DeliveryThrottlingLogData> recipientThrottleInfo = new Dictionary<string, DeliveryThrottlingLogData>();

		// Token: 0x040000EB RID: 235
		private readonly ReaderWriterLockSlim recipientThrottleInfoLock = new ReaderWriterLockSlim();

		// Token: 0x040000EC RID: 236
		private readonly Dictionary<string, DeliveryThrottlingLogData> concurrentMsgSizeThrottleInfo = new Dictionary<string, DeliveryThrottlingLogData>();

		// Token: 0x040000ED RID: 237
		private readonly ReaderWriterLockSlim concurrentMsgSizeThrottleInfoLock = new ReaderWriterLockSlim();

		// Token: 0x040000EE RID: 238
		private readonly IDeliveryThrottlingLog deliveryThrottlingLog;
	}
}
