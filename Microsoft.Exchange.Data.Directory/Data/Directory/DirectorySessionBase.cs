using System;
using System.Globalization;
using System.Net;
using System.Security.AccessControl;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000019 RID: 25
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal abstract class DirectorySessionBase
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AE RID: 174
		public abstract DirectoryBackendType DirectoryBackendType { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000045E1 File Offset: 0x000027E1
		protected bool IsDirectoryBackendAD
		{
			get
			{
				return (byte)(this.DirectoryBackendType & DirectoryBackendType.AD) == 1;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000045EF File Offset: 0x000027EF
		protected bool IsDirectoryBackendMServ
		{
			get
			{
				return (byte)(this.DirectoryBackendType & DirectoryBackendType.MServ) == 2;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000045FD File Offset: 0x000027FD
		protected bool IsDirectoryBackendMbx
		{
			get
			{
				return (byte)(this.DirectoryBackendType & DirectoryBackendType.Mbx) == 4;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000460B File Offset: 0x0000280B
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00004613 File Offset: 0x00002813
		public TimeSpan? ClientSideSearchTimeout
		{
			get
			{
				return this.clientSideSearchTimeout;
			}
			set
			{
				this.clientSideSearchTimeout = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000461C File Offset: 0x0000281C
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00004624 File Offset: 0x00002824
		public ConfigScopes ConfigScope
		{
			get
			{
				return this.configScope;
			}
			protected set
			{
				this.configScope = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000462D File Offset: 0x0000282D
		public ConsistencyMode ConsistencyMode
		{
			get
			{
				return this.consistencyMode;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004635 File Offset: 0x00002835
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x0000463D File Offset: 0x0000283D
		public string DomainController
		{
			get
			{
				return this.domainController;
			}
			set
			{
				this.CheckDomainControllerParameterConsistency(value);
				this.domainController = value;
				if (value == null)
				{
					this.stickyDC = false;
				}
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004657 File Offset: 0x00002857
		// (set) Token: 0x060000BA RID: 186 RVA: 0x0000465F File Offset: 0x0000285F
		public bool EnforceContainerizedScoping
		{
			get
			{
				return this.enforceContainerizedScoping;
			}
			set
			{
				this.enforceContainerizedScoping = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004668 File Offset: 0x00002868
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00004670 File Offset: 0x00002870
		public bool EnforceDefaultScope
		{
			get
			{
				return this.enforceDefaultScope;
			}
			set
			{
				this.enforceDefaultScope = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004679 File Offset: 0x00002879
		public string LastUsedDc
		{
			get
			{
				return this.ServerSettings.LastUsedDc(this.SessionSettings.GetAccountOrResourceForestFqdn());
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004691 File Offset: 0x00002891
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00004699 File Offset: 0x00002899
		public int Lcid
		{
			get
			{
				return this.lcid;
			}
			protected set
			{
				this.lcid = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000046A2 File Offset: 0x000028A2
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000046AA File Offset: 0x000028AA
		public string LinkResolutionServer
		{
			get
			{
				return this.linkResolutionServer;
			}
			set
			{
				this.linkResolutionServer = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000046B3 File Offset: 0x000028B3
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000046BB File Offset: 0x000028BB
		public bool LogSizeLimitExceededEvent
		{
			get
			{
				return this.logSizeLimitExceededEvent;
			}
			set
			{
				this.logSizeLimitExceededEvent = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000046C4 File Offset: 0x000028C4
		public NetworkCredential NetworkCredential
		{
			get
			{
				return this.networkCredential;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000046CC File Offset: 0x000028CC
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000046D4 File Offset: 0x000028D4
		public ADServerSettings ServerSettings
		{
			get
			{
				return this.SessionSettings.ServerSettings;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000046E1 File Offset: 0x000028E1
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000046EC File Offset: 0x000028EC
		public TimeSpan? ServerTimeout
		{
			get
			{
				return this.serverTimeout;
			}
			set
			{
				if (value != null && value <= TimeSpan.Zero)
				{
					throw new ArgumentOutOfRangeException("value", value, DirectoryStrings.ExceptionServerTimeoutNegative);
				}
				this.serverTimeout = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004747 File Offset: 0x00002947
		public ADSessionSettings SessionSettings
		{
			get
			{
				return this.sessionSettings;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000474F File Offset: 0x0000294F
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00004757 File Offset: 0x00002957
		public bool SkipRangedAttributes
		{
			get
			{
				return this.skipRangedAttributes;
			}
			set
			{
				this.skipRangedAttributes = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004760 File Offset: 0x00002960
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00004768 File Offset: 0x00002968
		public bool UseConfigNC
		{
			get
			{
				return this.useConfigNC;
			}
			set
			{
				this.useConfigNC = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004771 File Offset: 0x00002971
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00004779 File Offset: 0x00002979
		public bool UseGlobalCatalog
		{
			get
			{
				return this.useGlobalCatalog;
			}
			set
			{
				this.useGlobalCatalog = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004782 File Offset: 0x00002982
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x0000478A File Offset: 0x0000298A
		public string[] ExclusiveLdapAttributes { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004793 File Offset: 0x00002993
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x000047A0 File Offset: 0x000029A0
		public IActivityScope ActivityScope
		{
			get
			{
				return this.logContext.ActivityScope;
			}
			set
			{
				this.logContext.ActivityScope = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000047AE File Offset: 0x000029AE
		public string CallerInfo
		{
			get
			{
				return this.logContext.GetCallerInformation();
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000047BB File Offset: 0x000029BB
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x000047C3 File Offset: 0x000029C3
		protected ADObjectId SearchRoot
		{
			get
			{
				return this.searchRoot;
			}
			set
			{
				this.searchRoot = value;
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000047CC File Offset: 0x000029CC
		protected DirectorySessionBase(bool useConfigNC, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings)
		{
			if (sessionSettings == null)
			{
				throw new ArgumentNullException("sessionSettings");
			}
			this.domainController = null;
			this.consistencyMode = consistencyMode;
			this.lcid = CultureInfo.CurrentCulture.LCID;
			this.useGlobalCatalog = false;
			this.enforceDefaultScope = true;
			this.useConfigNC = useConfigNC;
			this.readOnly = readOnly;
			this.networkCredential = networkCredential;
			this.sessionSettings = sessionSettings;
			this.enforceContainerizedScoping = false;
			this.configScope = sessionSettings.ConfigScopes;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004860 File Offset: 0x00002A60
		public RawSecurityDescriptor ReadSecurityDescriptor(ADObjectId id)
		{
			SecurityDescriptor securityDescriptor = this.ReadSecurityDescriptorBlob(id);
			if (securityDescriptor == null)
			{
				return null;
			}
			return securityDescriptor.ToRawSecurityDescriptor();
		}

		// Token: 0x060000D9 RID: 217
		public abstract SecurityDescriptor ReadSecurityDescriptorBlob(ADObjectId id);

		// Token: 0x060000DA RID: 218 RVA: 0x00004880 File Offset: 0x00002A80
		internal void SetCallerInfo(string callerFilePath, string memberName, int callerFileLine)
		{
			this.logContext.FilePath = callerFilePath;
			this.logContext.FileLine = callerFileLine;
			this.logContext.MemberName = memberName;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000048A8 File Offset: 0x00002AA8
		protected void CheckDomainControllerParameterConsistency(string dcName)
		{
			if (!string.IsNullOrEmpty(dcName) && this.SessionSettings.PartitionId != null)
			{
				string forestFQDN = this.SessionSettings.PartitionId.ForestFQDN;
				if (!ADServerSettings.IsServerNamePartitionSameAsPartitionId(dcName, forestFQDN))
				{
					throw new DomainControllerFromWrongDomainException(DirectoryStrings.WrongDCForCurrentPartition(dcName, forestFQDN));
				}
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000048F8 File Offset: 0x00002AF8
		protected string GetStackTraceLine(int skipFrames = 4)
		{
			string[] array = Environment.StackTrace.Split(new string[]
			{
				"at "
			}, StringSplitOptions.RemoveEmptyEntries);
			for (int i = skipFrames; i < array.Length; i++)
			{
				string text = array[i];
				if (!string.IsNullOrEmpty(text.Trim()) && !text.Contains("Session.") && !text.Contains("Reader"))
				{
					return text.Substring(0, text.Length - skipFrames).Replace(',', ';');
				}
			}
			return null;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004974 File Offset: 0x00002B74
		protected ADRawEntry CreateAndInitializeADRawEntry(PropertyBag propertyBag)
		{
			ADRawEntry adrawEntry = new ADRawEntry((ADPropertyBag)propertyBag);
			adrawEntry.ResetChangeTracking(true);
			return adrawEntry;
		}

		// Token: 0x060000DE RID: 222
		protected abstract ADObject CreateAndInitializeObject<TResult>(ADPropertyBag propertyBag, ADRawEntry dummyInstance) where TResult : IConfigurable, new();

		// Token: 0x04000059 RID: 89
		protected readonly bool readOnly;

		// Token: 0x0400005A RID: 90
		protected TimeSpan? serverTimeout;

		// Token: 0x0400005B RID: 91
		protected TimeSpan? clientSideSearchTimeout;

		// Token: 0x0400005C RID: 92
		protected string domainController;

		// Token: 0x0400005D RID: 93
		protected string linkResolutionServer;

		// Token: 0x0400005E RID: 94
		protected int lcid;

		// Token: 0x0400005F RID: 95
		protected bool useConfigNC;

		// Token: 0x04000060 RID: 96
		protected bool useGlobalCatalog;

		// Token: 0x04000061 RID: 97
		protected bool enforceDefaultScope;

		// Token: 0x04000062 RID: 98
		protected bool logSizeLimitExceededEvent = true;

		// Token: 0x04000063 RID: 99
		[NonSerialized]
		protected NetworkCredential networkCredential;

		// Token: 0x04000064 RID: 100
		protected ConsistencyMode consistencyMode;

		// Token: 0x04000065 RID: 101
		protected ADObjectId searchRoot;

		// Token: 0x04000066 RID: 102
		protected ADSessionSettings sessionSettings;

		// Token: 0x04000067 RID: 103
		protected ConfigScopes configScope;

		// Token: 0x04000068 RID: 104
		protected bool skipRangedAttributes;

		// Token: 0x04000069 RID: 105
		protected bool enforceContainerizedScoping;

		// Token: 0x0400006A RID: 106
		protected bool isRehomed;

		// Token: 0x0400006B RID: 107
		protected bool stickyDC;

		// Token: 0x0400006C RID: 108
		private ADLogContext logContext = new ADLogContext();
	}
}
