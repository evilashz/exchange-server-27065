using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay.Dumpster
{
	// Token: 0x02000176 RID: 374
	[Serializable]
	public class SafetyNetInfo
	{
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0004007D File Offset: 0x0003E27D
		[XmlIgnore]
		public Version Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00040085 File Offset: 0x0003E285
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x0004008D File Offset: 0x0003E28D
		public string VersionString
		{
			get
			{
				return this.m_versionString;
			}
			set
			{
				this.SetModifiedIfNecessary<string>(this.m_versionString, value);
				this.m_versionString = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x000400A3 File Offset: 0x0003E2A3
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x000400AB File Offset: 0x0003E2AB
		public string SourceServerName
		{
			get
			{
				return this.m_sourceServerName;
			}
			set
			{
				this.SetModifiedIfNecessary<string>(this.m_sourceServerName, value);
				this.m_sourceServerName = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x000400C1 File Offset: 0x0003E2C1
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x000400C9 File Offset: 0x0003E2C9
		public bool RedeliveryRequired
		{
			get
			{
				return this.m_redeliveryRequired;
			}
			set
			{
				this.SetModifiedIfNecessary<bool>(this.m_redeliveryRequired, value);
				this.m_redeliveryRequired = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x000400DF File Offset: 0x0003E2DF
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x000400E7 File Offset: 0x0003E2E7
		public long LastLogGenBeforeActivation
		{
			get
			{
				return this.m_lastLogGenBeforeActivation;
			}
			set
			{
				this.SetModifiedIfNecessary<long>(this.m_lastLogGenBeforeActivation, value);
				this.m_lastLogGenBeforeActivation = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x000400FD File Offset: 0x0003E2FD
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x00040105 File Offset: 0x0003E305
		public long NumberOfLogsLost
		{
			get
			{
				return this.m_numberOfLogsLost;
			}
			set
			{
				this.SetModifiedIfNecessary<long>(this.m_numberOfLogsLost, value);
				this.m_numberOfLogsLost = value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x0004011B File Offset: 0x0003E31B
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x00040123 File Offset: 0x0003E323
		public DateTime ShadowRequestCreateTimeUtc
		{
			get
			{
				return this.m_shadowRequestCreateTimeUtc;
			}
			set
			{
				this.SetModifiedIfNecessary<DateTime>(this.m_shadowRequestCreateTimeUtc, value);
				this.m_shadowRequestCreateTimeUtc = value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00040139 File Offset: 0x0003E339
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x00040141 File Offset: 0x0003E341
		public DateTime RequestLastAttemptedTimeUtc
		{
			get
			{
				return this.m_requestLastAttemptedTimeUtc;
			}
			set
			{
				this.SetModifiedIfNecessary<DateTime>(this.m_requestLastAttemptedTimeUtc, value);
				this.m_requestLastAttemptedTimeUtc = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00040157 File Offset: 0x0003E357
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x0004015F File Offset: 0x0003E35F
		public DateTime RequestNextDueTimeUtc
		{
			get
			{
				return this.m_requestNextDueTimeUtc;
			}
			set
			{
				this.SetModifiedIfNecessary<DateTime>(this.m_requestNextDueTimeUtc, value);
				this.m_requestNextDueTimeUtc = value;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x00040175 File Offset: 0x0003E375
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x0004017D File Offset: 0x0003E37D
		public DateTime RequestCompletedTimeUtc
		{
			get
			{
				return this.m_requestCompletedTimeUtc;
			}
			set
			{
				this.SetModifiedIfNecessary<DateTime>(this.m_requestCompletedTimeUtc, value);
				this.m_requestCompletedTimeUtc = value;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00040193 File Offset: 0x0003E393
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x0004019B File Offset: 0x0003E39B
		public DateTime FailoverTimeUtc
		{
			get
			{
				return this.m_failoverTimeUtc;
			}
			set
			{
				this.SetModifiedIfNecessary<DateTime>(this.m_failoverTimeUtc, value);
				this.m_failoverTimeUtc = value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x000401B1 File Offset: 0x0003E3B1
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x000401B9 File Offset: 0x0003E3B9
		public DateTime StartTimeUtc
		{
			get
			{
				return this.m_startTimeUtc;
			}
			set
			{
				this.SetModifiedIfNecessary<DateTime>(this.m_startTimeUtc, value);
				this.m_startTimeUtc = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x000401CF File Offset: 0x0003E3CF
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x000401D7 File Offset: 0x0003E3D7
		public DateTime EndTimeUtc
		{
			get
			{
				return this.m_endTimeUtc;
			}
			set
			{
				this.SetModifiedIfNecessary<DateTime>(this.m_endTimeUtc, value);
				this.m_endTimeUtc = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x000401ED File Offset: 0x0003E3ED
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x000401F5 File Offset: 0x0003E3F5
		public List<string> HubServers
		{
			get
			{
				return this.m_hubServers;
			}
			set
			{
				this.SetModifiedIfNecessary<List<string>>(this.m_hubServers, value);
				this.m_hubServers = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x0004020B File Offset: 0x0003E40B
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x00040213 File Offset: 0x0003E413
		public List<string> PrimaryHubServers
		{
			get
			{
				return this.m_primaryHubServers;
			}
			set
			{
				this.SetModifiedIfNecessary<List<string>>(this.m_primaryHubServers, value);
				this.m_primaryHubServers = value;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00040229 File Offset: 0x0003E429
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x00040231 File Offset: 0x0003E431
		public List<string> ShadowHubServers
		{
			get
			{
				return this.m_shadowHubServers;
			}
			set
			{
				this.SetModifiedIfNecessary<List<string>>(this.m_shadowHubServers, value);
				this.m_shadowHubServers = value;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x00040247 File Offset: 0x0003E447
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x0004024F File Offset: 0x0003E44F
		public string UniqueStr
		{
			get
			{
				return this.m_uniqueStr;
			}
			set
			{
				this.SetModifiedIfNecessary<string>(this.m_uniqueStr, value);
				this.m_uniqueStr = value;
			}
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00040268 File Offset: 0x0003E468
		public static SafetyNetInfo Deserialize(string dbName, string blob, Trace tracer, bool throwOnError)
		{
			Exception ex = null;
			SafetyNetInfo safetyNetInfo = null;
			object obj = SerializationUtil.XmlToObject(blob, typeof(SafetyNetInfo), out ex);
			if (ex == null)
			{
				safetyNetInfo = (obj as SafetyNetInfo);
				if (safetyNetInfo == null && tracer != null)
				{
					tracer.TraceError<string, string>(0L, "Deserialized object {0} was not compatible with expected type {1}.", (obj != null) ? obj.GetType().Name : "(null)", typeof(SafetyNetInfo).Name);
				}
				else
				{
					safetyNetInfo.m_version = new Version(safetyNetInfo.m_versionString);
					safetyNetInfo.m_fModified = false;
					safetyNetInfo.m_serializedForm = blob;
				}
			}
			if (ex != null && tracer != null)
			{
				tracer.TraceError<string, string>(0L, "Deserialization of object {0} failed:\n{1}", typeof(SafetyNetInfo).Name, ex.ToString());
			}
			if (safetyNetInfo == null && throwOnError)
			{
				throw new FailedToDeserializeDumpsterRequestStrException(dbName, blob, typeof(SafetyNetInfo).Name, AmExceptionHelper.GetExceptionMessageOrNoneString(ex), ex);
			}
			return safetyNetInfo;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0004033B File Offset: 0x0003E53B
		public string Serialize()
		{
			this.m_serializedForm = SerializationUtil.ObjectToXml(this);
			return this.m_serializedForm;
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x00040350 File Offset: 0x0003E550
		public SafetyNetInfo()
		{
			this.m_fModified = true;
			this.m_version = SafetyNetInfo.VersionNumber;
			this.m_versionString = this.m_version.ToString();
			this.RedeliveryRequired = false;
			this.StartTimeUtc = DateTime.MaxValue;
			this.EndTimeUtc = DateTime.MinValue;
			this.FailoverTimeUtc = DateTime.MinValue;
			this.ShadowRequestCreateTimeUtc = DateTime.MinValue;
			this.RequestLastAttemptedTimeUtc = DateTime.MinValue;
			this.RequestNextDueTimeUtc = DateTime.MinValue;
			this.RequestCompletedTimeUtc = DateTime.MinValue;
			this.HubServers = new List<string>(0);
			this.PrimaryHubServers = new List<string>(0);
			this.ShadowHubServers = new List<string>(0);
			this.SourceServerName = string.Empty;
			this.LastLogGenBeforeActivation = 0L;
			this.NumberOfLogsLost = 0L;
			this.UniqueStr = Guid.NewGuid().ToString().Substring(0, 8);
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0004043C File Offset: 0x0003E63C
		public SafetyNetInfo(string sourceServerName, long lastLogGenBeforeActivation, long numLogsLost, DateTime failoverTime, DateTime startTime, DateTime endTime)
		{
			this.m_fModified = true;
			this.m_version = SafetyNetInfo.VersionNumber;
			this.m_versionString = this.m_version.ToString();
			this.SourceServerName = sourceServerName;
			this.LastLogGenBeforeActivation = lastLogGenBeforeActivation;
			this.NumberOfLogsLost = numLogsLost;
			this.ShadowRequestCreateTimeUtc = DateTime.MinValue;
			this.RequestLastAttemptedTimeUtc = DateTime.MinValue;
			this.RequestNextDueTimeUtc = DateTime.MinValue;
			this.RequestCompletedTimeUtc = DateTime.MinValue;
			this.FailoverTimeUtc = failoverTime;
			this.StartTimeUtc = startTime;
			this.EndTimeUtc = endTime;
			this.RedeliveryRequired = true;
			this.HubServers = new List<string>(0);
			this.PrimaryHubServers = new List<string>(0);
			this.ShadowHubServers = new List<string>(0);
			this.UniqueStr = Guid.NewGuid().ToString().Substring(0, 8);
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x00040516 File Offset: 0x0003E716
		public override string ToString()
		{
			this.Serialize();
			return this.GetSerializedForm();
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00040525 File Offset: 0x0003E725
		public string GetSerializedForm()
		{
			return this.m_serializedForm;
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0004052D File Offset: 0x0003E72D
		public bool IsModified()
		{
			return this.m_fModified;
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00040535 File Offset: 0x0003E735
		public void ClearModified()
		{
			this.m_fModified = false;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0004053E File Offset: 0x0003E73E
		public bool IsVersionCompatible()
		{
			return this.IsVersionCompatibleImpl(SafetyNetInfo.VersionNumber);
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0004054B File Offset: 0x0003E74B
		internal bool TestIsVersionCompatible(Version fakeServerVersion)
		{
			return this.IsVersionCompatibleImpl(fakeServerVersion);
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00040554 File Offset: 0x0003E754
		internal void TestSetVersion(Version fakeVersion)
		{
			this.m_version = fakeVersion;
			this.m_versionString = this.m_version.ToString();
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0004056E File Offset: 0x0003E76E
		private bool IsVersionCompatibleImpl(Version serverVersion)
		{
			return this.Version.Major == serverVersion.Major && this.Version.Minor <= serverVersion.Minor;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0004059C File Offset: 0x0003E79C
		private bool IsPropertyChanged<T>(T oldValue, T newValue)
		{
			if (oldValue is string)
			{
				return !SharedHelper.StringIEquals(oldValue as string, newValue as string);
			}
			if (oldValue is List<string>)
			{
				List<string> first = oldValue as List<string>;
				List<string> second = newValue as List<string>;
				return !first.SequenceEqual(second, StringComparer.OrdinalIgnoreCase);
			}
			return !oldValue.Equals(newValue);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00040620 File Offset: 0x0003E820
		private void SetModifiedIfNecessary<T>(T oldValue, T newValue)
		{
			this.m_fModified = (this.m_fModified || this.IsPropertyChanged<T>(oldValue, newValue));
		}

		// Token: 0x0400062D RID: 1581
		public static readonly Version VersionNumber = new Version(1, 0);

		// Token: 0x0400062E RID: 1582
		private Version m_version;

		// Token: 0x0400062F RID: 1583
		private string m_versionString;

		// Token: 0x04000630 RID: 1584
		private string m_sourceServerName;

		// Token: 0x04000631 RID: 1585
		private bool m_redeliveryRequired;

		// Token: 0x04000632 RID: 1586
		private long m_lastLogGenBeforeActivation;

		// Token: 0x04000633 RID: 1587
		private long m_numberOfLogsLost;

		// Token: 0x04000634 RID: 1588
		private DateTime m_shadowRequestCreateTimeUtc;

		// Token: 0x04000635 RID: 1589
		private DateTime m_requestLastAttemptedTimeUtc;

		// Token: 0x04000636 RID: 1590
		private DateTime m_requestNextDueTimeUtc;

		// Token: 0x04000637 RID: 1591
		private DateTime m_requestCompletedTimeUtc;

		// Token: 0x04000638 RID: 1592
		private DateTime m_failoverTimeUtc;

		// Token: 0x04000639 RID: 1593
		private DateTime m_startTimeUtc;

		// Token: 0x0400063A RID: 1594
		private DateTime m_endTimeUtc;

		// Token: 0x0400063B RID: 1595
		private List<string> m_hubServers;

		// Token: 0x0400063C RID: 1596
		private List<string> m_primaryHubServers;

		// Token: 0x0400063D RID: 1597
		private List<string> m_shadowHubServers;

		// Token: 0x0400063E RID: 1598
		private string m_uniqueStr;

		// Token: 0x0400063F RID: 1599
		[NonSerialized]
		private bool m_fModified;

		// Token: 0x04000640 RID: 1600
		[NonSerialized]
		private string m_serializedForm;
	}
}
