using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006E7 RID: 1767
	[Serializable]
	public class CASMailboxPlan : ADPresentationObject
	{
		// Token: 0x17001B02 RID: 6914
		// (get) Token: 0x06005267 RID: 21095 RVA: 0x0012F58B File Offset: 0x0012D78B
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return CASMailboxPlan.schema;
			}
		}

		// Token: 0x17001B03 RID: 6915
		// (get) Token: 0x06005268 RID: 21096 RVA: 0x0012F592 File Offset: 0x0012D792
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x0012F599 File Offset: 0x0012D799
		public CASMailboxPlan()
		{
		}

		// Token: 0x0600526A RID: 21098 RVA: 0x0012F5A1 File Offset: 0x0012D7A1
		public CASMailboxPlan(ADUser dataObject) : base(dataObject)
		{
		}

		// Token: 0x0600526B RID: 21099 RVA: 0x0012F5AA File Offset: 0x0012D7AA
		internal static CASMailboxPlan FromDataObject(ADUser dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new CASMailboxPlan(dataObject);
		}

		// Token: 0x17001B04 RID: 6916
		// (get) Token: 0x0600526C RID: 21100 RVA: 0x0012F5B7 File Offset: 0x0012D7B7
		// (set) Token: 0x0600526D RID: 21101 RVA: 0x0012F5C9 File Offset: 0x0012D7C9
		public ADObjectId ActiveSyncMailboxPolicy
		{
			get
			{
				return (ADObjectId)this[CASMailboxPlanSchema.ActiveSyncMailboxPolicy];
			}
			set
			{
				this[CASMailboxPlanSchema.ActiveSyncMailboxPolicy] = value;
			}
		}

		// Token: 0x17001B05 RID: 6917
		// (get) Token: 0x0600526E RID: 21102 RVA: 0x0012F5D7 File Offset: 0x0012D7D7
		// (set) Token: 0x0600526F RID: 21103 RVA: 0x0012F5E4 File Offset: 0x0012D7E4
		[Parameter(Mandatory = false)]
		public bool ActiveSyncDebugLogging
		{
			get
			{
				return CASMailbox.IsMailboxProtocolLoggingEnabled(this, CASMailboxSchema.ActiveSyncDebugLogging);
			}
			set
			{
				CASMailbox.SetMailboxProtocolLoggingEnabled(this, CASMailboxSchema.ActiveSyncDebugLogging, value);
			}
		}

		// Token: 0x17001B06 RID: 6918
		// (get) Token: 0x06005270 RID: 21104 RVA: 0x0012F5F2 File Offset: 0x0012D7F2
		// (set) Token: 0x06005271 RID: 21105 RVA: 0x0012F604 File Offset: 0x0012D804
		[Parameter]
		public bool ActiveSyncEnabled
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.ActiveSyncEnabled];
			}
			set
			{
				this[CASMailboxPlanSchema.ActiveSyncEnabled] = value;
			}
		}

		// Token: 0x17001B07 RID: 6919
		// (get) Token: 0x06005272 RID: 21106 RVA: 0x0012F617 File Offset: 0x0012D817
		// (set) Token: 0x06005273 RID: 21107 RVA: 0x0012F629 File Offset: 0x0012D829
		[Parameter(Mandatory = false)]
		public string DisplayName
		{
			get
			{
				return (string)this[CASMailboxPlanSchema.DisplayName];
			}
			set
			{
				this[CASMailboxPlanSchema.DisplayName] = value;
			}
		}

		// Token: 0x17001B08 RID: 6920
		// (get) Token: 0x06005274 RID: 21108 RVA: 0x0012F637 File Offset: 0x0012D837
		// (set) Token: 0x06005275 RID: 21109 RVA: 0x0012F649 File Offset: 0x0012D849
		[Parameter(Mandatory = false)]
		public bool ECPEnabled
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.ECPEnabled];
			}
			set
			{
				this[CASMailboxPlanSchema.ECPEnabled] = value;
			}
		}

		// Token: 0x17001B09 RID: 6921
		// (get) Token: 0x06005276 RID: 21110 RVA: 0x0012F65C File Offset: 0x0012D85C
		// (set) Token: 0x06005277 RID: 21111 RVA: 0x0012F66E File Offset: 0x0012D86E
		[Parameter(Mandatory = false)]
		public bool ImapEnabled
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.ImapEnabled];
			}
			set
			{
				this[CASMailboxPlanSchema.ImapEnabled] = value;
			}
		}

		// Token: 0x17001B0A RID: 6922
		// (get) Token: 0x06005278 RID: 21112 RVA: 0x0012F681 File Offset: 0x0012D881
		// (set) Token: 0x06005279 RID: 21113 RVA: 0x0012F693 File Offset: 0x0012D893
		[Parameter(Mandatory = false)]
		public bool ImapUseProtocolDefaults
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.ImapUseProtocolDefaults];
			}
			set
			{
				this[CASMailboxPlanSchema.ImapUseProtocolDefaults] = value;
			}
		}

		// Token: 0x17001B0B RID: 6923
		// (get) Token: 0x0600527A RID: 21114 RVA: 0x0012F6A6 File Offset: 0x0012D8A6
		// (set) Token: 0x0600527B RID: 21115 RVA: 0x0012F6B8 File Offset: 0x0012D8B8
		[Parameter(Mandatory = false)]
		public MimeTextFormat ImapMessagesRetrievalMimeFormat
		{
			get
			{
				return (MimeTextFormat)this[CASMailboxPlanSchema.ImapMessagesRetrievalMimeFormat];
			}
			set
			{
				this[CASMailboxPlanSchema.ImapMessagesRetrievalMimeFormat] = value;
			}
		}

		// Token: 0x17001B0C RID: 6924
		// (get) Token: 0x0600527C RID: 21116 RVA: 0x0012F6CB File Offset: 0x0012D8CB
		// (set) Token: 0x0600527D RID: 21117 RVA: 0x0012F6DD File Offset: 0x0012D8DD
		[Parameter(Mandatory = false)]
		public bool ImapEnableExactRFC822Size
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.ImapEnableExactRFC822Size];
			}
			set
			{
				this[CASMailboxPlanSchema.ImapEnableExactRFC822Size] = value;
			}
		}

		// Token: 0x17001B0D RID: 6925
		// (get) Token: 0x0600527E RID: 21118 RVA: 0x0012F6F0 File Offset: 0x0012D8F0
		// (set) Token: 0x0600527F RID: 21119 RVA: 0x0012F6FD File Offset: 0x0012D8FD
		[Parameter(Mandatory = false)]
		public bool ImapProtocolLoggingEnabled
		{
			get
			{
				return CASMailbox.IsMailboxProtocolLoggingEnabled(this, CASMailboxPlanSchema.ImapProtocolLoggingEnabled);
			}
			set
			{
				CASMailbox.SetMailboxProtocolLoggingEnabled(this, CASMailboxPlanSchema.ImapProtocolLoggingEnabled, value);
			}
		}

		// Token: 0x17001B0E RID: 6926
		// (get) Token: 0x06005280 RID: 21120 RVA: 0x0012F70B File Offset: 0x0012D90B
		// (set) Token: 0x06005281 RID: 21121 RVA: 0x0012F71D File Offset: 0x0012D91D
		[Parameter(Mandatory = false)]
		public bool ImapSuppressReadReceipt
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.ImapSuppressReadReceipt];
			}
			set
			{
				this[CASMailboxPlanSchema.ImapSuppressReadReceipt] = value;
			}
		}

		// Token: 0x17001B0F RID: 6927
		// (get) Token: 0x06005282 RID: 21122 RVA: 0x0012F730 File Offset: 0x0012D930
		// (set) Token: 0x06005283 RID: 21123 RVA: 0x0012F742 File Offset: 0x0012D942
		[Parameter(Mandatory = false)]
		public bool ImapForceICalForCalendarRetrievalOption
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.ImapForceICalForCalendarRetrievalOption];
			}
			set
			{
				this[CASMailboxPlanSchema.ImapForceICalForCalendarRetrievalOption] = value;
			}
		}

		// Token: 0x17001B10 RID: 6928
		// (get) Token: 0x06005284 RID: 21124 RVA: 0x0012F755 File Offset: 0x0012D955
		// (set) Token: 0x06005285 RID: 21125 RVA: 0x0012F767 File Offset: 0x0012D967
		[Parameter(Mandatory = false)]
		public bool MAPIEnabled
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.MAPIEnabled];
			}
			set
			{
				this[CASMailboxPlanSchema.MAPIEnabled] = value;
			}
		}

		// Token: 0x17001B11 RID: 6929
		// (get) Token: 0x06005286 RID: 21126 RVA: 0x0012F77A File Offset: 0x0012D97A
		// (set) Token: 0x06005287 RID: 21127 RVA: 0x0012F78C File Offset: 0x0012D98C
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		[Parameter(Mandatory = false)]
		public bool? MapiHttpEnabled
		{
			get
			{
				return (bool?)this[CASMailboxSchema.MapiHttpEnabled];
			}
			set
			{
				this[CASMailboxSchema.MapiHttpEnabled] = value;
			}
		}

		// Token: 0x17001B12 RID: 6930
		// (get) Token: 0x06005288 RID: 21128 RVA: 0x0012F79F File Offset: 0x0012D99F
		// (set) Token: 0x06005289 RID: 21129 RVA: 0x0012F7B1 File Offset: 0x0012D9B1
		[Parameter(Mandatory = false)]
		public bool MAPIBlockOutlookNonCachedMode
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.MAPIBlockOutlookNonCachedMode];
			}
			set
			{
				this[CASMailboxPlanSchema.MAPIBlockOutlookNonCachedMode] = value;
			}
		}

		// Token: 0x17001B13 RID: 6931
		// (get) Token: 0x0600528A RID: 21130 RVA: 0x0012F7C4 File Offset: 0x0012D9C4
		// (set) Token: 0x0600528B RID: 21131 RVA: 0x0012F7D6 File Offset: 0x0012D9D6
		[Parameter(Mandatory = false)]
		public string MAPIBlockOutlookVersions
		{
			get
			{
				return (string)this[CASMailboxPlanSchema.MAPIBlockOutlookVersions];
			}
			set
			{
				this[CASMailboxPlanSchema.MAPIBlockOutlookVersions] = value;
			}
		}

		// Token: 0x17001B14 RID: 6932
		// (get) Token: 0x0600528C RID: 21132 RVA: 0x0012F7E4 File Offset: 0x0012D9E4
		// (set) Token: 0x0600528D RID: 21133 RVA: 0x0012F7F6 File Offset: 0x0012D9F6
		[Parameter(Mandatory = false)]
		public bool MAPIBlockOutlookRpcHttp
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.MAPIBlockOutlookRpcHttp];
			}
			set
			{
				this[CASMailboxPlanSchema.MAPIBlockOutlookRpcHttp] = value;
			}
		}

		// Token: 0x17001B15 RID: 6933
		// (get) Token: 0x0600528E RID: 21134 RVA: 0x0012F809 File Offset: 0x0012DA09
		// (set) Token: 0x0600528F RID: 21135 RVA: 0x0012F81B File Offset: 0x0012DA1B
		[Parameter(Mandatory = false)]
		public bool MAPIBlockOutlookExternalConnectivity
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.MAPIBlockOutlookExternalConnectivity];
			}
			set
			{
				this[CASMailboxPlanSchema.MAPIBlockOutlookExternalConnectivity] = value;
			}
		}

		// Token: 0x17001B16 RID: 6934
		// (get) Token: 0x06005290 RID: 21136 RVA: 0x0012F82E File Offset: 0x0012DA2E
		// (set) Token: 0x06005291 RID: 21137 RVA: 0x0012F840 File Offset: 0x0012DA40
		public ADObjectId OwaMailboxPolicy
		{
			get
			{
				return (ADObjectId)this[CASMailboxPlanSchema.OwaMailboxPolicy];
			}
			set
			{
				this[CASMailboxPlanSchema.OwaMailboxPolicy] = value;
			}
		}

		// Token: 0x17001B17 RID: 6935
		// (get) Token: 0x06005292 RID: 21138 RVA: 0x0012F84E File Offset: 0x0012DA4E
		// (set) Token: 0x06005293 RID: 21139 RVA: 0x0012F860 File Offset: 0x0012DA60
		[Parameter]
		public bool OWAEnabled
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.OWAEnabled];
			}
			set
			{
				this[CASMailboxPlanSchema.OWAEnabled] = value;
			}
		}

		// Token: 0x17001B18 RID: 6936
		// (get) Token: 0x06005294 RID: 21140 RVA: 0x0012F873 File Offset: 0x0012DA73
		// (set) Token: 0x06005295 RID: 21141 RVA: 0x0012F885 File Offset: 0x0012DA85
		[Parameter(Mandatory = false)]
		public bool OWAforDevicesEnabled
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.OWAforDevicesEnabled];
			}
			set
			{
				this[CASMailboxPlanSchema.OWAforDevicesEnabled] = value;
			}
		}

		// Token: 0x17001B19 RID: 6937
		// (get) Token: 0x06005296 RID: 21142 RVA: 0x0012F898 File Offset: 0x0012DA98
		// (set) Token: 0x06005297 RID: 21143 RVA: 0x0012F8AA File Offset: 0x0012DAAA
		[Parameter(Mandatory = false)]
		public bool PopEnabled
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.PopEnabled];
			}
			set
			{
				this[CASMailboxPlanSchema.PopEnabled] = value;
			}
		}

		// Token: 0x17001B1A RID: 6938
		// (get) Token: 0x06005298 RID: 21144 RVA: 0x0012F8BD File Offset: 0x0012DABD
		// (set) Token: 0x06005299 RID: 21145 RVA: 0x0012F8CF File Offset: 0x0012DACF
		[Parameter(Mandatory = false)]
		public bool PopUseProtocolDefaults
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.PopUseProtocolDefaults];
			}
			set
			{
				this[CASMailboxPlanSchema.PopUseProtocolDefaults] = value;
			}
		}

		// Token: 0x17001B1B RID: 6939
		// (get) Token: 0x0600529A RID: 21146 RVA: 0x0012F8E2 File Offset: 0x0012DAE2
		// (set) Token: 0x0600529B RID: 21147 RVA: 0x0012F8F4 File Offset: 0x0012DAF4
		[Parameter(Mandatory = false)]
		public MimeTextFormat PopMessagesRetrievalMimeFormat
		{
			get
			{
				return (MimeTextFormat)this[CASMailboxPlanSchema.PopMessagesRetrievalMimeFormat];
			}
			set
			{
				this[CASMailboxPlanSchema.PopMessagesRetrievalMimeFormat] = value;
			}
		}

		// Token: 0x17001B1C RID: 6940
		// (get) Token: 0x0600529C RID: 21148 RVA: 0x0012F907 File Offset: 0x0012DB07
		// (set) Token: 0x0600529D RID: 21149 RVA: 0x0012F919 File Offset: 0x0012DB19
		[Parameter(Mandatory = false)]
		public bool PopEnableExactRFC822Size
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.PopEnableExactRFC822Size];
			}
			set
			{
				this[CASMailboxPlanSchema.PopEnableExactRFC822Size] = value;
			}
		}

		// Token: 0x17001B1D RID: 6941
		// (get) Token: 0x0600529E RID: 21150 RVA: 0x0012F92C File Offset: 0x0012DB2C
		// (set) Token: 0x0600529F RID: 21151 RVA: 0x0012F939 File Offset: 0x0012DB39
		[Parameter(Mandatory = false)]
		public bool PopProtocolLoggingEnabled
		{
			get
			{
				return CASMailbox.IsMailboxProtocolLoggingEnabled(this, CASMailboxPlanSchema.PopProtocolLoggingEnabled);
			}
			set
			{
				CASMailbox.SetMailboxProtocolLoggingEnabled(this, CASMailboxPlanSchema.PopProtocolLoggingEnabled, value);
			}
		}

		// Token: 0x17001B1E RID: 6942
		// (get) Token: 0x060052A0 RID: 21152 RVA: 0x0012F947 File Offset: 0x0012DB47
		// (set) Token: 0x060052A1 RID: 21153 RVA: 0x0012F959 File Offset: 0x0012DB59
		[Parameter(Mandatory = false)]
		public bool PopSuppressReadReceipt
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.PopSuppressReadReceipt];
			}
			set
			{
				this[CASMailboxPlanSchema.PopSuppressReadReceipt] = value;
			}
		}

		// Token: 0x17001B1F RID: 6943
		// (get) Token: 0x060052A2 RID: 21154 RVA: 0x0012F96C File Offset: 0x0012DB6C
		// (set) Token: 0x060052A3 RID: 21155 RVA: 0x0012F97E File Offset: 0x0012DB7E
		[Parameter(Mandatory = false)]
		public bool PopForceICalForCalendarRetrievalOption
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.PopForceICalForCalendarRetrievalOption];
			}
			set
			{
				this[CASMailboxPlanSchema.PopForceICalForCalendarRetrievalOption] = value;
			}
		}

		// Token: 0x17001B20 RID: 6944
		// (get) Token: 0x060052A4 RID: 21156 RVA: 0x0012F991 File Offset: 0x0012DB91
		// (set) Token: 0x060052A5 RID: 21157 RVA: 0x0012F9A3 File Offset: 0x0012DBA3
		[Parameter(Mandatory = false)]
		public bool RemotePowerShellEnabled
		{
			get
			{
				return (bool)this[CASMailboxPlanSchema.RemotePowerShellEnabled];
			}
			set
			{
				this[CASMailboxPlanSchema.RemotePowerShellEnabled] = value;
			}
		}

		// Token: 0x17001B21 RID: 6945
		// (get) Token: 0x060052A6 RID: 21158 RVA: 0x0012F9B6 File Offset: 0x0012DBB6
		// (set) Token: 0x060052A7 RID: 21159 RVA: 0x0012F9CD File Offset: 0x0012DBCD
		[Parameter(Mandatory = false)]
		public bool? EwsEnabled
		{
			get
			{
				return CASMailboxHelper.ToBooleanNullable((int?)this[CASMailboxPlanSchema.EwsEnabled]);
			}
			set
			{
				this[CASMailboxPlanSchema.EwsEnabled] = CASMailboxHelper.ToInt32Nullable(value);
			}
		}

		// Token: 0x17001B22 RID: 6946
		// (get) Token: 0x060052A8 RID: 21160 RVA: 0x0012F9E5 File Offset: 0x0012DBE5
		// (set) Token: 0x060052A9 RID: 21161 RVA: 0x0012F9F7 File Offset: 0x0012DBF7
		[Parameter(Mandatory = false)]
		public bool? EwsAllowOutlook
		{
			get
			{
				return (bool?)this[CASMailboxPlanSchema.EwsAllowOutlook];
			}
			set
			{
				this[CASMailboxPlanSchema.EwsAllowOutlook] = value;
			}
		}

		// Token: 0x17001B23 RID: 6947
		// (get) Token: 0x060052AA RID: 21162 RVA: 0x0012FA0A File Offset: 0x0012DC0A
		// (set) Token: 0x060052AB RID: 21163 RVA: 0x0012FA1C File Offset: 0x0012DC1C
		[Parameter(Mandatory = false)]
		public bool? EwsAllowMacOutlook
		{
			get
			{
				return (bool?)this[CASMailboxPlanSchema.EwsAllowMacOutlook];
			}
			set
			{
				this[CASMailboxPlanSchema.EwsAllowMacOutlook] = value;
			}
		}

		// Token: 0x17001B24 RID: 6948
		// (get) Token: 0x060052AC RID: 21164 RVA: 0x0012FA2F File Offset: 0x0012DC2F
		// (set) Token: 0x060052AD RID: 21165 RVA: 0x0012FA41 File Offset: 0x0012DC41
		[Parameter(Mandatory = false)]
		public bool? EwsAllowEntourage
		{
			get
			{
				return (bool?)this[CASMailboxPlanSchema.EwsAllowEntourage];
			}
			set
			{
				this[CASMailboxPlanSchema.EwsAllowEntourage] = value;
			}
		}

		// Token: 0x17001B25 RID: 6949
		// (get) Token: 0x060052AE RID: 21166 RVA: 0x0012FA54 File Offset: 0x0012DC54
		// (set) Token: 0x060052AF RID: 21167 RVA: 0x0012FA66 File Offset: 0x0012DC66
		[Parameter(Mandatory = false)]
		public EwsApplicationAccessPolicy? EwsApplicationAccessPolicy
		{
			get
			{
				return (EwsApplicationAccessPolicy?)this[CASMailboxPlanSchema.EwsApplicationAccessPolicy];
			}
			set
			{
				this[CASMailboxPlanSchema.EwsApplicationAccessPolicy] = value;
			}
		}

		// Token: 0x17001B26 RID: 6950
		// (get) Token: 0x060052B0 RID: 21168 RVA: 0x0012FA7C File Offset: 0x0012DC7C
		// (set) Token: 0x060052B1 RID: 21169 RVA: 0x0012FAC3 File Offset: 0x0012DCC3
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> EwsAllowList
		{
			get
			{
				if ((EwsApplicationAccessPolicy?)this[CASMailboxPlanSchema.EwsApplicationAccessPolicy] == Microsoft.Exchange.Data.Directory.EwsApplicationAccessPolicy.EnforceAllowList)
				{
					return (MultiValuedProperty<string>)this[CASMailboxPlanSchema.EwsExceptions];
				}
				return null;
			}
			set
			{
				this[CASMailboxPlanSchema.EwsExceptions] = value;
			}
		}

		// Token: 0x17001B27 RID: 6951
		// (get) Token: 0x060052B2 RID: 21170 RVA: 0x0012FAD4 File Offset: 0x0012DCD4
		// (set) Token: 0x060052B3 RID: 21171 RVA: 0x0012FB1C File Offset: 0x0012DD1C
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> EwsBlockList
		{
			get
			{
				if ((EwsApplicationAccessPolicy?)this[CASMailboxPlanSchema.EwsApplicationAccessPolicy] == Microsoft.Exchange.Data.Directory.EwsApplicationAccessPolicy.EnforceBlockList)
				{
					return (MultiValuedProperty<string>)this[CASMailboxPlanSchema.EwsExceptions];
				}
				return null;
			}
			set
			{
				this[CASMailboxPlanSchema.EwsExceptions] = value;
			}
		}

		// Token: 0x040037DB RID: 14299
		private static CASMailboxPlanSchema schema = ObjectSchema.GetInstance<CASMailboxPlanSchema>();
	}
}
