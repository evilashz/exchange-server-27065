using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000A4 RID: 164
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AmBcsServerFailureLogger : IAmBcsErrorLogger
	{
		// Token: 0x060006A5 RID: 1701 RVA: 0x00020428 File Offset: 0x0001E628
		public AmBcsServerFailureLogger(Guid dbGuid, string dbName, bool fLogEvents)
		{
			this.m_dbGuid = dbGuid;
			this.m_dbName = dbName;
			this.m_failureTable = new Dictionary<AmServerName, OrderedDictionary>(5);
			this.IsEventLoggingEnabled = fLogEvents;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00020451 File Offset: 0x0001E651
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x00020459 File Offset: 0x0001E659
		public bool IsEventLoggingEnabled { get; private set; }

		// Token: 0x060006A8 RID: 1704 RVA: 0x00020462 File Offset: 0x0001E662
		public bool IsFailedForServer(AmServerName server)
		{
			return this.m_failureTable.ContainsKey(server);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00020470 File Offset: 0x0001E670
		public void ReportCopyStatusFailure(AmServerName server, string statusCheckThatFailed, string checksRun, string errorMessage)
		{
			this.ReportCopyStatusFailure(server, statusCheckThatFailed, checksRun, errorMessage, ReplayCrimsonEvents.BcsDbNodeChecksFailed, new object[]
			{
				this.m_dbName,
				this.m_dbGuid,
				server,
				checksRun,
				errorMessage
			});
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x000204BC File Offset: 0x0001E6BC
		public void ReportCopyStatusFailure(AmServerName server, string statusCheckThatFailed, string checksRun, string errorMessage, ReplayCrimsonEvent evt, params object[] evtArgs)
		{
			AmBcsServerFailureLogger.AmBcsCheckInfo checkInfo = new AmBcsServerFailureLogger.AmBcsCheckInfo(AmBcsServerFailureLogger.AmBcsCheckType.CopyStatus, statusCheckThatFailed);
			this.ReportFailureInternal(server, checkInfo, errorMessage, true, evt, evtArgs);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x000204E0 File Offset: 0x0001E6E0
		public void ReportServerFailure(AmServerName server, string serverCheckThatFailed, string errorMessage)
		{
			this.ReportServerFailure(server, serverCheckThatFailed, errorMessage, true);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000204EC File Offset: 0x0001E6EC
		public void ReportServerFailure(AmServerName server, string serverCheckThatFailed, string errorMessage, bool overwriteAllowed)
		{
			AmBcsServerFailureLogger.AmBcsCheckInfo checkInfo = new AmBcsServerFailureLogger.AmBcsCheckInfo(AmBcsServerFailureLogger.AmBcsCheckType.ServerLevel, serverCheckThatFailed);
			this.ReportFailureInternal(server, checkInfo, errorMessage, overwriteAllowed, ReplayCrimsonEvents.BcsDbNodeActivationBlocked, new object[]
			{
				this.m_dbName,
				this.m_dbGuid,
				server,
				errorMessage
			});
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00020538 File Offset: 0x0001E738
		public void ReportServerFailure(AmServerName server, string serverCheckThatFailed, string errorMessage, ReplayCrimsonEvent evt, params object[] evtArgs)
		{
			AmBcsServerFailureLogger.AmBcsCheckInfo checkInfo = new AmBcsServerFailureLogger.AmBcsCheckInfo(AmBcsServerFailureLogger.AmBcsCheckType.ServerLevel, serverCheckThatFailed);
			this.ReportFailureInternal(server, checkInfo, errorMessage, true, evt, evtArgs);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00020568 File Offset: 0x0001E768
		public string[] GetAllExceptions()
		{
			if (this.m_failureTable.Count == 0)
			{
				return null;
			}
			return this.m_failureTable.Values.SelectMany((OrderedDictionary ordered) => ordered.Values.Cast<string>()).ToArray<string>();
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000205B8 File Offset: 0x0001E7B8
		public Exception GetLastException()
		{
			string concatenatedErrorString = this.GetConcatenatedErrorString();
			if (concatenatedErrorString != null)
			{
				return new AmDbNotMountedMultipleServersException(this.m_dbName, this.GetConcatenatedErrorString());
			}
			return null;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00020620 File Offset: 0x0001E820
		public string GetConcatenatedErrorString()
		{
			if (this.m_failureTable.Count == 0)
			{
				return null;
			}
			IEnumerable<KeyValuePair<AmServerName, string>> enumerable = from kvp in this.m_failureTable
			where kvp.Value != null
			select new KeyValuePair<AmServerName, string>(kvp.Key, (string)kvp.Value[kvp.Value.Count - 1]);
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.AppendLine();
			foreach (KeyValuePair<AmServerName, string> keyValuePair in enumerable)
			{
				stringBuilder.AppendFormat("\r\n\r\n        {0}:\r\n        {1}\r\n        ", keyValuePair.Key.NetbiosName, keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000206F4 File Offset: 0x0001E8F4
		private void ReportFailureInternal(AmServerName server, AmBcsServerFailureLogger.AmBcsCheckInfo checkInfo, string errorMessage, bool overwriteAllowed, ReplayCrimsonEvent evt, params object[] evtArgs)
		{
			OrderedDictionary orderedDictionary;
			if (this.m_failureTable.ContainsKey(server))
			{
				orderedDictionary = this.m_failureTable[server];
			}
			else
			{
				orderedDictionary = new OrderedDictionary(10);
				this.m_failureTable.Add(server, orderedDictionary);
			}
			string str;
			bool flag = !this.TryGetErrorFromCheckTable(orderedDictionary, checkInfo, out str) || (overwriteAllowed && !SharedHelper.StringIEquals(str, errorMessage));
			if (flag)
			{
				this.AddErrorIntoCheckTable(orderedDictionary, checkInfo, errorMessage);
				if (this.IsEventLoggingEnabled)
				{
					evt.LogGeneric(evtArgs);
					return;
				}
			}
			else
			{
				AmTrace.Debug("BCS: Failure for server '{0}' and check [{1}] has already been recorded. Suppressing raising another event.", new object[]
				{
					server,
					checkInfo
				});
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00020798 File Offset: 0x0001E998
		private bool TryGetErrorFromCheckTable(OrderedDictionary checkTable, AmBcsServerFailureLogger.AmBcsCheckInfo checkInfo, out string errorMessage)
		{
			errorMessage = null;
			bool flag = checkTable.Contains(checkInfo);
			if (flag)
			{
				errorMessage = (string)checkTable[checkInfo];
				return true;
			}
			return false;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x000207C4 File Offset: 0x0001E9C4
		private void AddErrorIntoCheckTable(OrderedDictionary checkTable, AmBcsServerFailureLogger.AmBcsCheckInfo checkInfo, string errorMessage)
		{
			checkTable[checkInfo] = errorMessage;
		}

		// Token: 0x040002E8 RID: 744
		public const string ServerErrorFormatStr = "\r\n\r\n        {0}:\r\n        {1}\r\n        ";

		// Token: 0x040002E9 RID: 745
		private Dictionary<AmServerName, OrderedDictionary> m_failureTable;

		// Token: 0x040002EA RID: 746
		private Guid m_dbGuid;

		// Token: 0x040002EB RID: 747
		private string m_dbName;

		// Token: 0x020000A5 RID: 165
		private enum AmBcsCheckType
		{
			// Token: 0x040002F1 RID: 753
			CopyStatus,
			// Token: 0x040002F2 RID: 754
			ServerLevel
		}

		// Token: 0x020000A6 RID: 166
		private class AmBcsCheckInfo : IEquatable<AmBcsServerFailureLogger.AmBcsCheckInfo>
		{
			// Token: 0x060006B7 RID: 1719 RVA: 0x000207CE File Offset: 0x0001E9CE
			public AmBcsCheckInfo(AmBcsServerFailureLogger.AmBcsCheckType type, string checkName)
			{
				this.Type = type;
				this.CheckName = checkName;
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x060006B8 RID: 1720 RVA: 0x000207E4 File Offset: 0x0001E9E4
			// (set) Token: 0x060006B9 RID: 1721 RVA: 0x000207EC File Offset: 0x0001E9EC
			public AmBcsServerFailureLogger.AmBcsCheckType Type { get; private set; }

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x060006BA RID: 1722 RVA: 0x000207F5 File Offset: 0x0001E9F5
			// (set) Token: 0x060006BB RID: 1723 RVA: 0x000207FD File Offset: 0x0001E9FD
			public string CheckName { get; private set; }

			// Token: 0x060006BC RID: 1724 RVA: 0x00020806 File Offset: 0x0001EA06
			public static bool IsEqual(object a, object b)
			{
				if (object.ReferenceEquals(a, b))
				{
					return true;
				}
				if (a == null || b == null)
				{
					return false;
				}
				if (a is AmBcsServerFailureLogger.AmBcsCheckInfo && b is AmBcsServerFailureLogger.AmBcsCheckInfo)
				{
					return ((AmBcsServerFailureLogger.AmBcsCheckInfo)a).Equals((AmBcsServerFailureLogger.AmBcsCheckInfo)b);
				}
				return a.Equals(b);
			}

			// Token: 0x060006BD RID: 1725 RVA: 0x00020844 File Offset: 0x0001EA44
			public bool Equals(AmBcsServerFailureLogger.AmBcsCheckInfo other)
			{
				return this.Type == other.Type && SharedHelper.StringIEquals(this.CheckName, other.CheckName);
			}

			// Token: 0x060006BE RID: 1726 RVA: 0x00020867 File Offset: 0x0001EA67
			public override string ToString()
			{
				if (this.m_toString == null)
				{
					this.m_toString = string.Format(AmBcsServerFailureLogger.AmBcsCheckInfo.s_toStringFormat, this.Type, this.CheckName);
				}
				return this.m_toString;
			}

			// Token: 0x060006BF RID: 1727 RVA: 0x00020898 File Offset: 0x0001EA98
			public override bool Equals(object obj)
			{
				return AmBcsServerFailureLogger.AmBcsCheckInfo.IsEqual(this, obj);
			}

			// Token: 0x060006C0 RID: 1728 RVA: 0x000208A1 File Offset: 0x0001EAA1
			public override int GetHashCode()
			{
				return SharedHelper.GetStringIHashCode(this.ToString());
			}

			// Token: 0x040002F3 RID: 755
			private static string s_toStringFormat = "Type:{0}, Name:{1}";

			// Token: 0x040002F4 RID: 756
			private string m_toString;
		}
	}
}
