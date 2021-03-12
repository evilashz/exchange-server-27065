using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200057A RID: 1402
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class SenderReputationConfig : MessageHygieneAgentConfig
	{
		// Token: 0x06003EA3 RID: 16035 RVA: 0x000EDA94 File Offset: 0x000EBC94
		public SenderReputationConfig()
		{
			this.Socks4Ports = new MultiValuedProperty<int>(new object[]
			{
				1080,
				1081
			});
			this.Socks5Ports = new MultiValuedProperty<int>(new object[]
			{
				1080,
				1081
			});
			this.HttpConnectPorts = new MultiValuedProperty<int>(new object[]
			{
				80,
				3128,
				6588
			});
			this.HttpPostPorts = new MultiValuedProperty<int>(new object[]
			{
				80,
				3128,
				6588
			});
			this.TelnetPorts = new MultiValuedProperty<int>(new object[]
			{
				23
			});
			this.CiscoPorts = new MultiValuedProperty<int>(new object[]
			{
				23
			});
			this.WingatePorts = new MultiValuedProperty<int>(new object[]
			{
				23
			});
		}

		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x06003EA4 RID: 16036 RVA: 0x000EDBCF File Offset: 0x000EBDCF
		internal override ADObjectSchema Schema
		{
			get
			{
				return SenderReputationConfig.schema;
			}
		}

		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x06003EA5 RID: 16037 RVA: 0x000EDBD6 File Offset: 0x000EBDD6
		internal override string MostDerivedObjectClass
		{
			get
			{
				return SenderReputationConfig.mostDerivedClass;
			}
		}

		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x06003EA6 RID: 16038 RVA: 0x000EDBDD File Offset: 0x000EBDDD
		// (set) Token: 0x06003EA7 RID: 16039 RVA: 0x000EDBEF File Offset: 0x000EBDEF
		public int MinMessagesPerDatabaseTransaction
		{
			get
			{
				return (int)this[SenderReputationConfigSchema.MinMessagesPerDatabaseTransaction];
			}
			internal set
			{
				this[SenderReputationConfigSchema.MinMessagesPerDatabaseTransaction] = value;
			}
		}

		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x06003EA8 RID: 16040 RVA: 0x000EDC02 File Offset: 0x000EBE02
		// (set) Token: 0x06003EA9 RID: 16041 RVA: 0x000EDC14 File Offset: 0x000EBE14
		[Parameter(Mandatory = false)]
		public int SrlBlockThreshold
		{
			get
			{
				return (int)this[SenderReputationConfigSchema.SrlBlockThreshold];
			}
			set
			{
				this[SenderReputationConfigSchema.SrlBlockThreshold] = value;
			}
		}

		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x06003EAA RID: 16042 RVA: 0x000EDC27 File Offset: 0x000EBE27
		// (set) Token: 0x06003EAB RID: 16043 RVA: 0x000EDC39 File Offset: 0x000EBE39
		public int MinMessagesPerTimeSlice
		{
			get
			{
				return (int)this[SenderReputationConfigSchema.MinMessagesPerTimeSlice];
			}
			internal set
			{
				this[SenderReputationConfigSchema.MinMessagesPerTimeSlice] = value;
			}
		}

		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06003EAC RID: 16044 RVA: 0x000EDC4C File Offset: 0x000EBE4C
		// (set) Token: 0x06003EAD RID: 16045 RVA: 0x000EDC5E File Offset: 0x000EBE5E
		public int TimeSliceInterval
		{
			get
			{
				return (int)this[SenderReputationConfigSchema.TimeSliceInterval];
			}
			internal set
			{
				this[SenderReputationConfigSchema.TimeSliceInterval] = value;
			}
		}

		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x06003EAE RID: 16046 RVA: 0x000EDC71 File Offset: 0x000EBE71
		// (set) Token: 0x06003EAF RID: 16047 RVA: 0x000EDC7A File Offset: 0x000EBE7A
		[Parameter(Mandatory = false)]
		public bool OpenProxyDetectionEnabled
		{
			get
			{
				return this.GetFlag(1);
			}
			set
			{
				this.SetFlag(1, value);
			}
		}

		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x06003EB0 RID: 16048 RVA: 0x000EDC84 File Offset: 0x000EBE84
		// (set) Token: 0x06003EB1 RID: 16049 RVA: 0x000EDC8D File Offset: 0x000EBE8D
		[Parameter(Mandatory = false)]
		public bool SenderBlockingEnabled
		{
			get
			{
				return this.GetFlag(2);
			}
			set
			{
				this.SetFlag(2, value);
			}
		}

		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x06003EB2 RID: 16050 RVA: 0x000EDC97 File Offset: 0x000EBE97
		// (set) Token: 0x06003EB3 RID: 16051 RVA: 0x000EDCA9 File Offset: 0x000EBEA9
		public int OpenProxyRescanInterval
		{
			get
			{
				return (int)this[SenderReputationConfigSchema.OpenProxyRescanInterval];
			}
			internal set
			{
				this[SenderReputationConfigSchema.OpenProxyRescanInterval] = value;
			}
		}

		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x06003EB4 RID: 16052 RVA: 0x000EDCBC File Offset: 0x000EBEBC
		// (set) Token: 0x06003EB5 RID: 16053 RVA: 0x000EDCCE File Offset: 0x000EBECE
		public int MinReverseDnsQueryPeriod
		{
			get
			{
				return (int)this[SenderReputationConfigSchema.MinReverseDnsQueryPeriod];
			}
			internal set
			{
				this[SenderReputationConfigSchema.MinReverseDnsQueryPeriod] = value;
			}
		}

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x06003EB6 RID: 16054 RVA: 0x000EDCE1 File Offset: 0x000EBEE1
		// (set) Token: 0x06003EB7 RID: 16055 RVA: 0x000EDCF3 File Offset: 0x000EBEF3
		[Parameter(Mandatory = false)]
		public int SenderBlockingPeriod
		{
			get
			{
				return (int)this[SenderReputationConfigSchema.SenderBlockingPeriod];
			}
			set
			{
				this[SenderReputationConfigSchema.SenderBlockingPeriod] = value;
			}
		}

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x06003EB8 RID: 16056 RVA: 0x000EDD06 File Offset: 0x000EBF06
		// (set) Token: 0x06003EB9 RID: 16057 RVA: 0x000EDD18 File Offset: 0x000EBF18
		public int MaxWorkQueueSize
		{
			get
			{
				return (int)this[SenderReputationConfigSchema.MaxWorkQueueSize];
			}
			internal set
			{
				this[SenderReputationConfigSchema.MaxWorkQueueSize] = value;
			}
		}

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x06003EBA RID: 16058 RVA: 0x000EDD2B File Offset: 0x000EBF2B
		// (set) Token: 0x06003EBB RID: 16059 RVA: 0x000EDD3D File Offset: 0x000EBF3D
		public int MaxIdleTime
		{
			get
			{
				return (int)this[SenderReputationConfigSchema.MaxIdleTime];
			}
			internal set
			{
				this[SenderReputationConfigSchema.MaxIdleTime] = value;
			}
		}

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x06003EBC RID: 16060 RVA: 0x000EDD50 File Offset: 0x000EBF50
		// (set) Token: 0x06003EBD RID: 16061 RVA: 0x000EDD62 File Offset: 0x000EBF62
		public MultiValuedProperty<int> Socks4Ports
		{
			get
			{
				return (MultiValuedProperty<int>)this[SenderReputationConfigSchema.Socks4Ports];
			}
			internal set
			{
				this[SenderReputationConfigSchema.Socks4Ports] = value;
			}
		}

		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x06003EBE RID: 16062 RVA: 0x000EDD70 File Offset: 0x000EBF70
		// (set) Token: 0x06003EBF RID: 16063 RVA: 0x000EDD82 File Offset: 0x000EBF82
		public MultiValuedProperty<int> Socks5Ports
		{
			get
			{
				return (MultiValuedProperty<int>)this[SenderReputationConfigSchema.Socks5Ports];
			}
			internal set
			{
				this[SenderReputationConfigSchema.Socks5Ports] = value;
			}
		}

		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x06003EC0 RID: 16064 RVA: 0x000EDD90 File Offset: 0x000EBF90
		// (set) Token: 0x06003EC1 RID: 16065 RVA: 0x000EDDA2 File Offset: 0x000EBFA2
		public MultiValuedProperty<int> WingatePorts
		{
			get
			{
				return (MultiValuedProperty<int>)this[SenderReputationConfigSchema.WingatePorts];
			}
			internal set
			{
				this[SenderReputationConfigSchema.WingatePorts] = value;
			}
		}

		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x06003EC2 RID: 16066 RVA: 0x000EDDB0 File Offset: 0x000EBFB0
		// (set) Token: 0x06003EC3 RID: 16067 RVA: 0x000EDDC2 File Offset: 0x000EBFC2
		public MultiValuedProperty<int> HttpConnectPorts
		{
			get
			{
				return (MultiValuedProperty<int>)this[SenderReputationConfigSchema.HttpConnectPorts];
			}
			internal set
			{
				this[SenderReputationConfigSchema.HttpConnectPorts] = value;
			}
		}

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x06003EC4 RID: 16068 RVA: 0x000EDDD0 File Offset: 0x000EBFD0
		// (set) Token: 0x06003EC5 RID: 16069 RVA: 0x000EDDE2 File Offset: 0x000EBFE2
		public MultiValuedProperty<int> HttpPostPorts
		{
			get
			{
				return (MultiValuedProperty<int>)this[SenderReputationConfigSchema.HttpPostPorts];
			}
			internal set
			{
				this[SenderReputationConfigSchema.HttpPostPorts] = value;
			}
		}

		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x06003EC6 RID: 16070 RVA: 0x000EDDF0 File Offset: 0x000EBFF0
		// (set) Token: 0x06003EC7 RID: 16071 RVA: 0x000EDE02 File Offset: 0x000EC002
		public MultiValuedProperty<int> TelnetPorts
		{
			get
			{
				return (MultiValuedProperty<int>)this[SenderReputationConfigSchema.TelnetPorts];
			}
			internal set
			{
				this[SenderReputationConfigSchema.TelnetPorts] = value;
			}
		}

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x06003EC8 RID: 16072 RVA: 0x000EDE10 File Offset: 0x000EC010
		// (set) Token: 0x06003EC9 RID: 16073 RVA: 0x000EDE22 File Offset: 0x000EC022
		public MultiValuedProperty<int> CiscoPorts
		{
			get
			{
				return (MultiValuedProperty<int>)this[SenderReputationConfigSchema.CiscoPorts];
			}
			internal set
			{
				this[SenderReputationConfigSchema.CiscoPorts] = value;
			}
		}

		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x06003ECA RID: 16074 RVA: 0x000EDE30 File Offset: 0x000EC030
		// (set) Token: 0x06003ECB RID: 16075 RVA: 0x000EDE42 File Offset: 0x000EC042
		public int TablePurgeInterval
		{
			get
			{
				return (int)this[SenderReputationConfigSchema.TablePurgeInterval];
			}
			internal set
			{
				this[SenderReputationConfigSchema.TablePurgeInterval] = value;
			}
		}

		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x06003ECC RID: 16076 RVA: 0x000EDE55 File Offset: 0x000EC055
		// (set) Token: 0x06003ECD RID: 16077 RVA: 0x000EDE67 File Offset: 0x000EC067
		public int MaxPendingOperations
		{
			get
			{
				return (int)this[SenderReputationConfigSchema.MaxPendingOperations];
			}
			internal set
			{
				this[SenderReputationConfigSchema.MaxPendingOperations] = value;
			}
		}

		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x06003ECE RID: 16078 RVA: 0x000EDE7A File Offset: 0x000EC07A
		// (set) Token: 0x06003ECF RID: 16079 RVA: 0x000EDE8C File Offset: 0x000EC08C
		[Parameter(Mandatory = false)]
		public string ProxyServerName
		{
			get
			{
				return (string)this[SenderReputationConfigSchema.ProxyServerIP];
			}
			set
			{
				this[SenderReputationConfigSchema.ProxyServerIP] = value;
			}
		}

		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x06003ED0 RID: 16080 RVA: 0x000EDE9A File Offset: 0x000EC09A
		// (set) Token: 0x06003ED1 RID: 16081 RVA: 0x000EDEAC File Offset: 0x000EC0AC
		[Parameter(Mandatory = false)]
		public int ProxyServerPort
		{
			get
			{
				return (int)this[SenderReputationConfigSchema.ProxyServerPort];
			}
			set
			{
				this[SenderReputationConfigSchema.ProxyServerPort] = value;
			}
		}

		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x06003ED2 RID: 16082 RVA: 0x000EDEBF File Offset: 0x000EC0BF
		// (set) Token: 0x06003ED3 RID: 16083 RVA: 0x000EDED1 File Offset: 0x000EC0D1
		[Parameter(Mandatory = false)]
		public ProxyType ProxyServerType
		{
			get
			{
				return (ProxyType)this[SenderReputationConfigSchema.ProxyServerType];
			}
			set
			{
				this[SenderReputationConfigSchema.ProxyServerType] = value;
			}
		}

		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x06003ED4 RID: 16084 RVA: 0x000EDEE4 File Offset: 0x000EC0E4
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x06003ED5 RID: 16085 RVA: 0x000EDEEC File Offset: 0x000EC0EC
		// (set) Token: 0x06003ED6 RID: 16086 RVA: 0x000EDEFE File Offset: 0x000EC0FE
		public int MinDownloadInterval
		{
			get
			{
				return (int)this[SenderReputationConfigSchema.MinDownloadInterval];
			}
			internal set
			{
				this[SenderReputationConfigSchema.MinDownloadInterval] = value;
			}
		}

		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x06003ED7 RID: 16087 RVA: 0x000EDF11 File Offset: 0x000EC111
		// (set) Token: 0x06003ED8 RID: 16088 RVA: 0x000EDF23 File Offset: 0x000EC123
		public int MaxDownloadInterval
		{
			get
			{
				return (int)this[SenderReputationConfigSchema.MaxDownloadInterval];
			}
			internal set
			{
				this[SenderReputationConfigSchema.MaxDownloadInterval] = value;
			}
		}

		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x06003ED9 RID: 16089 RVA: 0x000EDF36 File Offset: 0x000EC136
		// (set) Token: 0x06003EDA RID: 16090 RVA: 0x000EDF48 File Offset: 0x000EC148
		public string SrlSettingsDatabaseFileName
		{
			get
			{
				return (string)this[SenderReputationConfigSchema.SrlSettingsDatabaseFileName];
			}
			internal set
			{
				this[SenderReputationConfigSchema.SrlSettingsDatabaseFileName] = value;
			}
		}

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x06003EDB RID: 16091 RVA: 0x000EDF56 File Offset: 0x000EC156
		// (set) Token: 0x06003EDC RID: 16092 RVA: 0x000EDF68 File Offset: 0x000EC168
		public string ReputationServiceUrl
		{
			get
			{
				return (string)this[SenderReputationConfigSchema.ReputationServiceUrl];
			}
			internal set
			{
				this[SenderReputationConfigSchema.ReputationServiceUrl] = value;
			}
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x000EDF76 File Offset: 0x000EC176
		private bool GetFlag(int mask)
		{
			return 0 != (mask & (int)this[SenderReputationConfigSchema.OpenProxyFlags]);
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x000EDF90 File Offset: 0x000EC190
		private void SetFlag(int mask, bool bit)
		{
			int num = (int)this[SenderReputationConfigSchema.OpenProxyFlags];
			this[SenderReputationConfigSchema.OpenProxyFlags] = (bit ? (num | mask) : (num & ~mask));
		}

		// Token: 0x04002A77 RID: 10871
		private const int OpenProxyDetectionEnabledMask = 1;

		// Token: 0x04002A78 RID: 10872
		private const int SenderBlockingEnabledMask = 2;

		// Token: 0x04002A79 RID: 10873
		private static SenderReputationConfigSchema schema = ObjectSchema.GetInstance<SenderReputationConfigSchema>();

		// Token: 0x04002A7A RID: 10874
		private static string mostDerivedClass = "msExchSenderReputation";
	}
}
