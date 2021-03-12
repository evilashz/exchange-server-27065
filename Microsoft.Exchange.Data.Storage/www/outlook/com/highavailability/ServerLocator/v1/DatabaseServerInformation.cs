using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace www.outlook.com.highavailability.ServerLocator.v1
{
	// Token: 0x02000D35 RID: 3381
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DatabaseServerInformation", Namespace = "http://www.outlook.com/highavailability/ServerLocator/v1/")]
	public class DatabaseServerInformation : IExtensibleDataObject
	{
		// Token: 0x17001F72 RID: 8050
		// (get) Token: 0x06007538 RID: 30008 RVA: 0x002082B3 File Offset: 0x002064B3
		// (set) Token: 0x06007539 RID: 30009 RVA: 0x002082BB File Offset: 0x002064BB
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x17001F73 RID: 8051
		// (get) Token: 0x0600753A RID: 30010 RVA: 0x002082C4 File Offset: 0x002064C4
		// (set) Token: 0x0600753B RID: 30011 RVA: 0x002082CC File Offset: 0x002064CC
		[DataMember]
		public Guid DatabaseGuid
		{
			get
			{
				return this.DatabaseGuidField;
			}
			set
			{
				this.DatabaseGuidField = value;
			}
		}

		// Token: 0x17001F74 RID: 8052
		// (get) Token: 0x0600753C RID: 30012 RVA: 0x002082D5 File Offset: 0x002064D5
		// (set) Token: 0x0600753D RID: 30013 RVA: 0x002082DD File Offset: 0x002064DD
		[DataMember]
		public string ServerFqdn
		{
			get
			{
				return this.ServerFqdnField;
			}
			set
			{
				this.ServerFqdnField = value;
			}
		}

		// Token: 0x17001F75 RID: 8053
		// (get) Token: 0x0600753E RID: 30014 RVA: 0x002082E6 File Offset: 0x002064E6
		// (set) Token: 0x0600753F RID: 30015 RVA: 0x002082EE File Offset: 0x002064EE
		[DataMember(Order = 2)]
		public DateTime RequestSentUtc
		{
			get
			{
				return this.RequestSentUtcField;
			}
			set
			{
				this.RequestSentUtcField = value;
			}
		}

		// Token: 0x17001F76 RID: 8054
		// (get) Token: 0x06007540 RID: 30016 RVA: 0x002082F7 File Offset: 0x002064F7
		// (set) Token: 0x06007541 RID: 30017 RVA: 0x002082FF File Offset: 0x002064FF
		[DataMember(Order = 3)]
		public DateTime RequestReceivedUtc
		{
			get
			{
				return this.RequestReceivedUtcField;
			}
			set
			{
				this.RequestReceivedUtcField = value;
			}
		}

		// Token: 0x17001F77 RID: 8055
		// (get) Token: 0x06007542 RID: 30018 RVA: 0x00208308 File Offset: 0x00206508
		// (set) Token: 0x06007543 RID: 30019 RVA: 0x00208310 File Offset: 0x00206510
		[DataMember(Order = 4)]
		public DateTime ReplySentUtc
		{
			get
			{
				return this.ReplySentUtcField;
			}
			set
			{
				this.ReplySentUtcField = value;
			}
		}

		// Token: 0x17001F78 RID: 8056
		// (get) Token: 0x06007544 RID: 30020 RVA: 0x00208319 File Offset: 0x00206519
		// (set) Token: 0x06007545 RID: 30021 RVA: 0x00208321 File Offset: 0x00206521
		[DataMember(Order = 5)]
		public int ServerVersion
		{
			get
			{
				return this.ServerVersionField;
			}
			set
			{
				this.ServerVersionField = value;
			}
		}

		// Token: 0x17001F79 RID: 8057
		// (get) Token: 0x06007546 RID: 30022 RVA: 0x0020832A File Offset: 0x0020652A
		// (set) Token: 0x06007547 RID: 30023 RVA: 0x00208332 File Offset: 0x00206532
		[DataMember(Order = 6)]
		public DateTime MountedTimeUtc
		{
			get
			{
				return this.MountedTimeUtcField;
			}
			set
			{
				this.MountedTimeUtcField = value;
			}
		}

		// Token: 0x17001F7A RID: 8058
		// (get) Token: 0x06007548 RID: 30024 RVA: 0x0020833B File Offset: 0x0020653B
		// (set) Token: 0x06007549 RID: 30025 RVA: 0x00208343 File Offset: 0x00206543
		[DataMember(Order = 7)]
		public string LastMountedServerFqdn
		{
			get
			{
				return this.LastMountedServerFqdnField;
			}
			set
			{
				this.LastMountedServerFqdnField = value;
			}
		}

		// Token: 0x17001F7B RID: 8059
		// (get) Token: 0x0600754A RID: 30026 RVA: 0x0020834C File Offset: 0x0020654C
		// (set) Token: 0x0600754B RID: 30027 RVA: 0x00208354 File Offset: 0x00206554
		[DataMember(Order = 8)]
		public long FailoverSequenceNumber
		{
			get
			{
				return this.FailoverSequenceNumberField;
			}
			set
			{
				this.FailoverSequenceNumberField = value;
			}
		}

		// Token: 0x04005184 RID: 20868
		private ExtensionDataObject extensionDataField;

		// Token: 0x04005185 RID: 20869
		private Guid DatabaseGuidField;

		// Token: 0x04005186 RID: 20870
		private string ServerFqdnField;

		// Token: 0x04005187 RID: 20871
		private DateTime RequestSentUtcField;

		// Token: 0x04005188 RID: 20872
		private DateTime RequestReceivedUtcField;

		// Token: 0x04005189 RID: 20873
		private DateTime ReplySentUtcField;

		// Token: 0x0400518A RID: 20874
		private int ServerVersionField;

		// Token: 0x0400518B RID: 20875
		private DateTime MountedTimeUtcField;

		// Token: 0x0400518C RID: 20876
		private string LastMountedServerFqdnField;

		// Token: 0x0400518D RID: 20877
		private long FailoverSequenceNumberField;
	}
}
