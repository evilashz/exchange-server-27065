using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000045 RID: 69
	internal class AmDbStateInfo
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x000118A4 File Offset: 0x0000FAA4
		internal AmDbStateInfo(Guid databaseGuid)
		{
			this.IsEntryExist = false;
			this.DatabaseGuid = databaseGuid;
			this.ActiveServer = AmServerName.Empty;
			this.LastMountedServer = AmServerName.Empty;
			this.MountStatus = MountStatus.Dismounted;
			this.IsAdminDismounted = false;
			this.IsAutomaticActionsAllowed = false;
			this.LastMountedTime = DateTime.UtcNow;
			this.FailoverSequenceNumber = 0L;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00011903 File Offset: 0x0000FB03
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0001190B File Offset: 0x0000FB0B
		internal AmServerName ActiveServer { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00011914 File Offset: 0x0000FB14
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0001191C File Offset: 0x0000FB1C
		internal AmServerName LastMountedServer { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00011925 File Offset: 0x0000FB25
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0001192D File Offset: 0x0000FB2D
		internal DateTime LastMountedTime { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00011936 File Offset: 0x0000FB36
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0001193E File Offset: 0x0000FB3E
		internal MountStatus MountStatus { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00011947 File Offset: 0x0000FB47
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0001194F File Offset: 0x0000FB4F
		internal bool IsAdminDismounted { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00011958 File Offset: 0x0000FB58
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00011960 File Offset: 0x0000FB60
		internal bool IsAutomaticActionsAllowed { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00011969 File Offset: 0x0000FB69
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00011971 File Offset: 0x0000FB71
		internal long FailoverSequenceNumber { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0001197A File Offset: 0x0000FB7A
		// (set) Token: 0x06000309 RID: 777 RVA: 0x00011982 File Offset: 0x0000FB82
		internal bool IsEntryExist { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0001198B File Offset: 0x0000FB8B
		// (set) Token: 0x0600030B RID: 779 RVA: 0x00011993 File Offset: 0x0000FB93
		internal Guid DatabaseGuid { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0001199C File Offset: 0x0000FB9C
		internal bool IsActiveServerValid
		{
			get
			{
				return !AmServerName.IsNullOrEmpty(this.ActiveServer);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600030D RID: 781 RVA: 0x000119AC File Offset: 0x0000FBAC
		internal bool IsMountSucceededAtleastOnce
		{
			get
			{
				return !AmServerName.IsNullOrEmpty(this.LastMountedServer);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600030E RID: 782 RVA: 0x000119BC File Offset: 0x0000FBBC
		internal bool IsMountAttemptedAtleastOnce
		{
			get
			{
				return !AmServerName.IsNullOrEmpty(this.ActiveServer);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600030F RID: 783 RVA: 0x000119CC File Offset: 0x0000FBCC
		internal bool IsMounted
		{
			get
			{
				return this.MountStatus == MountStatus.Mounted;
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000119D8 File Offset: 0x0000FBD8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(500);
			AmDbStateInfo.AppendNameValue(stringBuilder, "IsEntryExist", this.IsEntryExist.ToString());
			AmDbStateInfo.AppendNameValue(stringBuilder, AmDbStateInfo.PropertyNames.ActiveServer.ToString(), this.ActiveServer.NetbiosName);
			AmDbStateInfo.AppendNameValue(stringBuilder, AmDbStateInfo.PropertyNames.LastMountedServer.ToString(), this.LastMountedServer.NetbiosName);
			AmDbStateInfo.AppendNameValue(stringBuilder, AmDbStateInfo.PropertyNames.LastMountedTime.ToString(), this.LastMountedTime.ToString("s"));
			AmDbStateInfo.AppendNameValue(stringBuilder, AmDbStateInfo.PropertyNames.MountStatus.ToString(), this.MountStatus.ToString());
			AmDbStateInfo.AppendNameValue(stringBuilder, AmDbStateInfo.PropertyNames.IsAdminDismounted.ToString(), this.IsAdminDismounted.ToString());
			AmDbStateInfo.AppendNameValue(stringBuilder, AmDbStateInfo.PropertyNames.IsAutomaticActionsAllowed.ToString(), this.IsAutomaticActionsAllowed.ToString());
			AmDbStateInfo.AppendNameValue(stringBuilder, AmDbStateInfo.PropertyNames.FailoverSequenceNumber.ToString(), this.FailoverSequenceNumber.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00011AEB File Offset: 0x0000FCEB
		internal static void AppendNameValue(StringBuilder sb, string propName, object value)
		{
			sb.Append(propName);
			sb.Append('?');
			sb.Append(value);
			sb.Append('*');
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00011B0F File Offset: 0x0000FD0F
		[Conditional("DEBUG")]
		internal static void AssertAreEqual(AmDbStateInfo stateA, AmDbStateInfo stateB)
		{
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00011B78 File Offset: 0x0000FD78
		internal static AmDbStateInfo ReplaceProperty(AmDbStateInfo oldStateInfo, AmDbStateInfo.PropertyNames propName, string propValue)
		{
			bool isPropExist = false;
			string str = oldStateInfo.ToString();
			StringBuilder sb = new StringBuilder(500);
			AmDbStateInfo.ParseNameValuePairs(str, delegate(string name, string value)
			{
				if (AmDbStateInfo.IsMatching(name, propName))
				{
					if (!isPropExist)
					{
						AmDbStateInfo.AppendNameValue(sb, propName.ToString(), propValue);
						isPropExist = true;
						return;
					}
				}
				else
				{
					AmDbStateInfo.AppendNameValue(sb, name, value);
				}
			});
			if (!isPropExist)
			{
				AmDbStateInfo.AppendNameValue(sb, propName.ToString(), propValue);
			}
			return AmDbStateInfo.Parse(oldStateInfo.DatabaseGuid, sb.ToString());
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00011D04 File Offset: 0x0000FF04
		internal static AmDbStateInfo Parse(Guid databaseGuid, string entryStr)
		{
			AmDbStateInfo stateInfo = new AmDbStateInfo(databaseGuid);
			stateInfo.IsEntryExist = true;
			AmDbStateInfo.ParseNameValuePairs(entryStr, delegate(string name, string value)
			{
				if (AmDbStateInfo.IsMatching(name, AmDbStateInfo.PropertyNames.ActiveServer))
				{
					stateInfo.ActiveServer = new AmServerName(SharedHelper.GetNodeNameFromFqdn(value));
					return;
				}
				if (AmDbStateInfo.IsMatching(name, AmDbStateInfo.PropertyNames.LastMountedServer))
				{
					stateInfo.LastMountedServer = new AmServerName(SharedHelper.GetNodeNameFromFqdn(value));
					return;
				}
				if (AmDbStateInfo.IsMatching(name, AmDbStateInfo.PropertyNames.LastMountedTime))
				{
					DateTime lastMountedTime;
					DateTime.TryParseExact(value, DateTimeFormatInfo.InvariantInfo.SortableDateTimePattern, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.RoundtripKind, out lastMountedTime);
					stateInfo.LastMountedTime = lastMountedTime;
					return;
				}
				if (AmDbStateInfo.IsMatching(name, AmDbStateInfo.PropertyNames.MountStatus))
				{
					MountStatus mountStatus;
					EnumUtility.TryParse<MountStatus>(value, out mountStatus, MountStatus.Dismounted);
					stateInfo.MountStatus = mountStatus;
					return;
				}
				if (AmDbStateInfo.IsMatching(name, AmDbStateInfo.PropertyNames.IsAdminDismounted))
				{
					stateInfo.IsAdminDismounted = bool.Parse(value);
					return;
				}
				if (AmDbStateInfo.IsMatching(name, AmDbStateInfo.PropertyNames.IsAutomaticActionsAllowed))
				{
					stateInfo.IsAutomaticActionsAllowed = bool.Parse(value);
					return;
				}
				if (AmDbStateInfo.IsMatching(name, AmDbStateInfo.PropertyNames.FailoverSequenceNumber))
				{
					stateInfo.FailoverSequenceNumber = long.Parse(value);
				}
			});
			return stateInfo;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00011D48 File Offset: 0x0000FF48
		internal AmDbStateInfo Copy()
		{
			return new AmDbStateInfo(this.DatabaseGuid)
			{
				ActiveServer = new AmServerName(this.ActiveServer),
				LastMountedServer = new AmServerName(this.LastMountedServer),
				MountStatus = this.MountStatus,
				IsAdminDismounted = this.IsAdminDismounted,
				IsAutomaticActionsAllowed = this.IsAutomaticActionsAllowed,
				LastMountedTime = this.LastMountedTime,
				FailoverSequenceNumber = this.FailoverSequenceNumber
			};
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00011DC0 File Offset: 0x0000FFC0
		internal void UpdateActiveServerAndIncrementFailoverSequenceNumber(AmServerName activeServer)
		{
			long ticks = DateTime.UtcNow.Ticks;
			bool flag = false;
			if (!this.IsEntryExist || !AmServerName.IsEqual(this.ActiveServer, activeServer))
			{
				flag = true;
			}
			this.ActiveServer = activeServer;
			if (flag)
			{
				if (!this.IsEntryExist || this.FailoverSequenceNumber == 0L)
				{
					this.FailoverSequenceNumber = ticks;
					return;
				}
				if (this.FailoverSequenceNumber < ticks)
				{
					this.FailoverSequenceNumber = ticks;
					return;
				}
				this.FailoverSequenceNumber += 1L;
				ReplayCrimsonEvents.DatabaseFailoverSequenceNumberNotBasedOnTime.LogPeriodic<Guid, long>(this.DatabaseGuid, TimeSpan.FromMinutes(30.0), this.DatabaseGuid, this.FailoverSequenceNumber);
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00011E69 File Offset: 0x00010069
		private static bool IsMatching(string key, AmDbStateInfo.PropertyNames propName)
		{
			return string.Equals(key, propName.ToString(), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00011E80 File Offset: 0x00010080
		private static int ParseNameValuePairs(string str, AmDbStateInfo.ProcessNameValuePair processNameValueDelegate)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(str))
			{
				string[] array = str.Split(new char[]
				{
					'*'
				});
				if (array != null && array.Length > 0)
				{
					foreach (string text in array)
					{
						string text2 = string.Empty;
						string value = string.Empty;
						string[] array3 = text.Split(new char[]
						{
							'?'
						});
						if (array3 != null && array3.Length > 0)
						{
							text2 = array3[0].Trim();
							if (array3.Length > 1)
							{
								value = array3[1].Trim();
							}
							if (!string.IsNullOrEmpty(text2))
							{
								num++;
								processNameValueDelegate(text2, value);
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x04000120 RID: 288
		private const char NameValueSeparatorChar = '?';

		// Token: 0x04000121 RID: 289
		private const char EntrySeparatorChar = '*';

		// Token: 0x02000046 RID: 70
		internal enum PropertyNames
		{
			// Token: 0x0400012C RID: 300
			None,
			// Token: 0x0400012D RID: 301
			ActiveServer,
			// Token: 0x0400012E RID: 302
			LastMountedServer,
			// Token: 0x0400012F RID: 303
			LastMountedTime,
			// Token: 0x04000130 RID: 304
			MountStatus,
			// Token: 0x04000131 RID: 305
			IsAdminDismounted,
			// Token: 0x04000132 RID: 306
			IsAutomaticActionsAllowed,
			// Token: 0x04000133 RID: 307
			FailoverSequenceNumber
		}

		// Token: 0x02000047 RID: 71
		// (Invoke) Token: 0x0600031A RID: 794
		private delegate void ProcessNameValuePair(string name, string value);
	}
}
