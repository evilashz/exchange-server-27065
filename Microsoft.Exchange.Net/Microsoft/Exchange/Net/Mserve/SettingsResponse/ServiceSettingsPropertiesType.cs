using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008E7 RID: 2279
	[XmlType(Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ServiceSettingsPropertiesType
	{
		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x060030F5 RID: 12533 RVA: 0x000731FF File Offset: 0x000713FF
		// (set) Token: 0x060030F6 RID: 12534 RVA: 0x00073207 File Offset: 0x00071407
		public int ServiceTimeOut
		{
			get
			{
				return this.serviceTimeOutField;
			}
			set
			{
				this.serviceTimeOutField = value;
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x060030F7 RID: 12535 RVA: 0x00073210 File Offset: 0x00071410
		// (set) Token: 0x060030F8 RID: 12536 RVA: 0x00073218 File Offset: 0x00071418
		public int MinSyncPollInterval
		{
			get
			{
				return this.minSyncPollIntervalField;
			}
			set
			{
				this.minSyncPollIntervalField = value;
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x060030F9 RID: 12537 RVA: 0x00073221 File Offset: 0x00071421
		// (set) Token: 0x060030FA RID: 12538 RVA: 0x00073229 File Offset: 0x00071429
		public int MinSettingsPollInterval
		{
			get
			{
				return this.minSettingsPollIntervalField;
			}
			set
			{
				this.minSettingsPollIntervalField = value;
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x060030FB RID: 12539 RVA: 0x00073232 File Offset: 0x00071432
		// (set) Token: 0x060030FC RID: 12540 RVA: 0x0007323A File Offset: 0x0007143A
		public double SyncMultiplier
		{
			get
			{
				return this.syncMultiplierField;
			}
			set
			{
				this.syncMultiplierField = value;
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x060030FD RID: 12541 RVA: 0x00073243 File Offset: 0x00071443
		// (set) Token: 0x060030FE RID: 12542 RVA: 0x0007324B File Offset: 0x0007144B
		public int MaxObjectsInSync
		{
			get
			{
				return this.maxObjectsInSyncField;
			}
			set
			{
				this.maxObjectsInSyncField = value;
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x060030FF RID: 12543 RVA: 0x00073254 File Offset: 0x00071454
		// (set) Token: 0x06003100 RID: 12544 RVA: 0x0007325C File Offset: 0x0007145C
		public int MaxNumberOfEmailAdds
		{
			get
			{
				return this.maxNumberOfEmailAddsField;
			}
			set
			{
				this.maxNumberOfEmailAddsField = value;
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06003101 RID: 12545 RVA: 0x00073265 File Offset: 0x00071465
		// (set) Token: 0x06003102 RID: 12546 RVA: 0x0007326D File Offset: 0x0007146D
		public int MaxNumberOfFolderAdds
		{
			get
			{
				return this.maxNumberOfFolderAddsField;
			}
			set
			{
				this.maxNumberOfFolderAddsField = value;
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06003103 RID: 12547 RVA: 0x00073276 File Offset: 0x00071476
		// (set) Token: 0x06003104 RID: 12548 RVA: 0x0007327E File Offset: 0x0007147E
		public int MaxNumberOfStatelessObjects
		{
			get
			{
				return this.maxNumberOfStatelessObjectsField;
			}
			set
			{
				this.maxNumberOfStatelessObjectsField = value;
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06003105 RID: 12549 RVA: 0x00073287 File Offset: 0x00071487
		// (set) Token: 0x06003106 RID: 12550 RVA: 0x0007328F File Offset: 0x0007148F
		public int DefaultStatelessEmailWindowSize
		{
			get
			{
				return this.defaultStatelessEmailWindowSizeField;
			}
			set
			{
				this.defaultStatelessEmailWindowSizeField = value;
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06003107 RID: 12551 RVA: 0x00073298 File Offset: 0x00071498
		// (set) Token: 0x06003108 RID: 12552 RVA: 0x000732A0 File Offset: 0x000714A0
		public int MaxStatelessEmailWindowSize
		{
			get
			{
				return this.maxStatelessEmailWindowSizeField;
			}
			set
			{
				this.maxStatelessEmailWindowSizeField = value;
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06003109 RID: 12553 RVA: 0x000732A9 File Offset: 0x000714A9
		// (set) Token: 0x0600310A RID: 12554 RVA: 0x000732B1 File Offset: 0x000714B1
		public int MaxTotalLengthOfForwardingAddresses
		{
			get
			{
				return this.maxTotalLengthOfForwardingAddressesField;
			}
			set
			{
				this.maxTotalLengthOfForwardingAddressesField = value;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x000732BA File Offset: 0x000714BA
		// (set) Token: 0x0600310C RID: 12556 RVA: 0x000732C2 File Offset: 0x000714C2
		public int MaxVacationResponseMessageLength
		{
			get
			{
				return this.maxVacationResponseMessageLengthField;
			}
			set
			{
				this.maxVacationResponseMessageLengthField = value;
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x0600310D RID: 12557 RVA: 0x000732CB File Offset: 0x000714CB
		// (set) Token: 0x0600310E RID: 12558 RVA: 0x000732D3 File Offset: 0x000714D3
		public string MinVacationResponseStartTime
		{
			get
			{
				return this.minVacationResponseStartTimeField;
			}
			set
			{
				this.minVacationResponseStartTimeField = value;
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x000732DC File Offset: 0x000714DC
		// (set) Token: 0x06003110 RID: 12560 RVA: 0x000732E4 File Offset: 0x000714E4
		public string MaxVacationResponseEndTime
		{
			get
			{
				return this.maxVacationResponseEndTimeField;
			}
			set
			{
				this.maxVacationResponseEndTimeField = value;
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06003111 RID: 12561 RVA: 0x000732ED File Offset: 0x000714ED
		// (set) Token: 0x06003112 RID: 12562 RVA: 0x000732F5 File Offset: 0x000714F5
		public int MaxNumberOfProvisionCommands
		{
			get
			{
				return this.maxNumberOfProvisionCommandsField;
			}
			set
			{
				this.maxNumberOfProvisionCommandsField = value;
			}
		}

		// Token: 0x04002A4F RID: 10831
		private int serviceTimeOutField;

		// Token: 0x04002A50 RID: 10832
		private int minSyncPollIntervalField;

		// Token: 0x04002A51 RID: 10833
		private int minSettingsPollIntervalField;

		// Token: 0x04002A52 RID: 10834
		private double syncMultiplierField;

		// Token: 0x04002A53 RID: 10835
		private int maxObjectsInSyncField;

		// Token: 0x04002A54 RID: 10836
		private int maxNumberOfEmailAddsField;

		// Token: 0x04002A55 RID: 10837
		private int maxNumberOfFolderAddsField;

		// Token: 0x04002A56 RID: 10838
		private int maxNumberOfStatelessObjectsField;

		// Token: 0x04002A57 RID: 10839
		private int defaultStatelessEmailWindowSizeField;

		// Token: 0x04002A58 RID: 10840
		private int maxStatelessEmailWindowSizeField;

		// Token: 0x04002A59 RID: 10841
		private int maxTotalLengthOfForwardingAddressesField;

		// Token: 0x04002A5A RID: 10842
		private int maxVacationResponseMessageLengthField;

		// Token: 0x04002A5B RID: 10843
		private string minVacationResponseStartTimeField;

		// Token: 0x04002A5C RID: 10844
		private string maxVacationResponseEndTimeField;

		// Token: 0x04002A5D RID: 10845
		private int maxNumberOfProvisionCommandsField;
	}
}
