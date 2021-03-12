using System;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200021D RID: 541
	[XmlType(TypeName = "MoveRequestXML")]
	[Serializable]
	public sealed class RequestJobXML : RequestJobBase
	{
		// Token: 0x06001BCF RID: 7119 RVA: 0x0003B483 File Offset: 0x00039683
		public RequestJobXML()
		{
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x0003B48B File Offset: 0x0003968B
		internal RequestJobXML(SimpleProviderPropertyBag propertyBag) : base(propertyBag)
		{
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x0003B494 File Offset: 0x00039694
		internal RequestJobXML(TransactionalRequestJob requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
			this.UnknownElements = requestJob.UnknownElements;
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x0003B4BA File Offset: 0x000396BA
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x0003B4C7 File Offset: 0x000396C7
		[XmlElement(ElementName = "Identity")]
		public ADObjectIdXML UserIdentityXML
		{
			get
			{
				return ADObjectIdXML.Serialize(base.UserId);
			}
			set
			{
				base.UserId = ADObjectIdXML.Deserialize(value);
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x0003B4D5 File Offset: 0x000396D5
		// (set) Token: 0x06001BD5 RID: 7125 RVA: 0x0003B4DD File Offset: 0x000396DD
		[XmlElement(ElementName = "ExternalDirectoryOrganizationId")]
		public new Guid ExternalDirectoryOrganizationId
		{
			get
			{
				return base.ExternalDirectoryOrganizationId;
			}
			set
			{
				base.ExternalDirectoryOrganizationId = value;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x0003B4E6 File Offset: 0x000396E6
		// (set) Token: 0x06001BD7 RID: 7127 RVA: 0x0003B4FD File Offset: 0x000396FD
		[XmlElement(ElementName = "PartitionHint")]
		public byte[] PersistablePartitionHint
		{
			get
			{
				if (base.PartitionHint != null)
				{
					return base.PartitionHint.GetPersistablePartitionHint();
				}
				return null;
			}
			set
			{
				base.PartitionHint = ((value == null) ? null : TenantPartitionHint.FromPersistablePartitionHint(value));
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x0003B511 File Offset: 0x00039711
		// (set) Token: 0x06001BD9 RID: 7129 RVA: 0x0003B519 File Offset: 0x00039719
		[XmlElement(ElementName = "ExchangeGuid")]
		public new Guid ExchangeGuid
		{
			get
			{
				return base.ExchangeGuid;
			}
			set
			{
				base.ExchangeGuid = value;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x0003B522 File Offset: 0x00039722
		// (set) Token: 0x06001BDB RID: 7131 RVA: 0x0003B52A File Offset: 0x0003972A
		[XmlElement(ElementName = "ArchiveGuid")]
		public new Guid? ArchiveGuid
		{
			get
			{
				return base.ArchiveGuid;
			}
			set
			{
				base.ArchiveGuid = value;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x0003B533 File Offset: 0x00039733
		// (set) Token: 0x06001BDD RID: 7133 RVA: 0x0003B53B File Offset: 0x0003973B
		[XmlElement(ElementName = "MoveRequestGuid")]
		public new Guid RequestGuid
		{
			get
			{
				return base.RequestGuid;
			}
			set
			{
				base.RequestGuid = value;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x0003B544 File Offset: 0x00039744
		// (set) Token: 0x06001BDF RID: 7135 RVA: 0x0003B54C File Offset: 0x0003974C
		[XmlElement(ElementName = "Status")]
		public int StatusInt
		{
			get
			{
				return (int)base.Status;
			}
			set
			{
				base.Status = (RequestStatus)value;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x0003B555 File Offset: 0x00039755
		// (set) Token: 0x06001BE1 RID: 7137 RVA: 0x0003B55D File Offset: 0x0003975D
		[XmlElement(ElementName = "MoveState")]
		public int RequestJobStateInt
		{
			get
			{
				return (int)base.RequestJobState;
			}
			set
			{
				base.RequestJobState = (JobProcessingState)value;
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x0003B566 File Offset: 0x00039766
		// (set) Token: 0x06001BE3 RID: 7139 RVA: 0x0003B56E File Offset: 0x0003976E
		[XmlElement(ElementName = "SyncStage")]
		public int SyncStageInt
		{
			get
			{
				return (int)base.SyncStage;
			}
			set
			{
				base.SyncStage = (SyncStage)value;
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x0003B577 File Offset: 0x00039777
		// (set) Token: 0x06001BE5 RID: 7141 RVA: 0x0003B57F File Offset: 0x0003977F
		[XmlElement(ElementName = "Flags")]
		public int FlagsInt
		{
			get
			{
				return (int)base.Flags;
			}
			set
			{
				base.Flags = (RequestFlags)value;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x0003B588 File Offset: 0x00039788
		// (set) Token: 0x06001BE7 RID: 7143 RVA: 0x0003B590 File Offset: 0x00039790
		[XmlElement(ElementName = "SourceExchangeGuid")]
		public new Guid SourceExchangeGuid
		{
			get
			{
				return base.SourceExchangeGuid;
			}
			set
			{
				base.SourceExchangeGuid = value;
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x0003B599 File Offset: 0x00039799
		// (set) Token: 0x06001BE9 RID: 7145 RVA: 0x0003B5A1 File Offset: 0x000397A1
		[XmlElement(ElementName = "SourceIsArchive")]
		public new bool SourceIsArchive
		{
			get
			{
				return base.SourceIsArchive;
			}
			set
			{
				base.SourceIsArchive = value;
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x0003B5AA File Offset: 0x000397AA
		// (set) Token: 0x06001BEB RID: 7147 RVA: 0x0003B5B2 File Offset: 0x000397B2
		[XmlElement(ElementName = "SourceRootFolder")]
		public new string SourceRootFolder
		{
			get
			{
				return base.SourceRootFolder;
			}
			set
			{
				base.SourceRootFolder = value;
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x0003B5BB File Offset: 0x000397BB
		// (set) Token: 0x06001BED RID: 7149 RVA: 0x0003B5C3 File Offset: 0x000397C3
		[XmlElement(ElementName = "TargetExchangeGuid")]
		public new Guid TargetExchangeGuid
		{
			get
			{
				return base.TargetExchangeGuid;
			}
			set
			{
				base.TargetExchangeGuid = value;
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x0003B5CC File Offset: 0x000397CC
		// (set) Token: 0x06001BEF RID: 7151 RVA: 0x0003B5D4 File Offset: 0x000397D4
		[XmlElement(ElementName = "TargetIsArchive")]
		public new bool TargetIsArchive
		{
			get
			{
				return base.TargetIsArchive;
			}
			set
			{
				base.TargetIsArchive = value;
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x0003B5DD File Offset: 0x000397DD
		// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x0003B5E5 File Offset: 0x000397E5
		[XmlElement(ElementName = "TargetRootFolder")]
		public new string TargetRootFolder
		{
			get
			{
				return base.TargetRootFolder;
			}
			set
			{
				base.TargetRootFolder = value;
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x0003B5EE File Offset: 0x000397EE
		// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x0003B5F6 File Offset: 0x000397F6
		[XmlElement(ElementName = "ArchiveDomain")]
		public new string ArchiveDomain
		{
			get
			{
				return base.ArchiveDomain;
			}
			set
			{
				base.ArchiveDomain = value;
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x0003B5FF File Offset: 0x000397FF
		// (set) Token: 0x06001BF5 RID: 7157 RVA: 0x0003B60C File Offset: 0x0003980C
		[XmlElement(ElementName = "SourceDatabase")]
		public ADObjectIdXML SourceDatabaseXML
		{
			get
			{
				return ADObjectIdXML.Serialize(base.SourceDatabase);
			}
			set
			{
				base.SourceDatabase = ADObjectIdXML.Deserialize(value);
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x0003B61A File Offset: 0x0003981A
		// (set) Token: 0x06001BF7 RID: 7159 RVA: 0x0003B627 File Offset: 0x00039827
		[XmlElement(ElementName = "SourceArchiveDatabase")]
		public ADObjectIdXML SourceArchiveDatabaseXML
		{
			get
			{
				return ADObjectIdXML.Serialize(base.SourceArchiveDatabase);
			}
			set
			{
				base.SourceArchiveDatabase = ADObjectIdXML.Deserialize(value);
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x0003B635 File Offset: 0x00039835
		// (set) Token: 0x06001BF9 RID: 7161 RVA: 0x0003B63D File Offset: 0x0003983D
		[XmlElement(ElementName = "SourceVersion")]
		public new int SourceVersion
		{
			get
			{
				return base.SourceVersion;
			}
			set
			{
				base.SourceVersion = value;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x0003B646 File Offset: 0x00039846
		// (set) Token: 0x06001BFB RID: 7163 RVA: 0x0003B64E File Offset: 0x0003984E
		[XmlElement(ElementName = "SourceArchiveVersion")]
		public new int SourceArchiveVersion
		{
			get
			{
				return base.SourceArchiveVersion;
			}
			set
			{
				base.SourceArchiveVersion = value;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x0003B657 File Offset: 0x00039857
		// (set) Token: 0x06001BFD RID: 7165 RVA: 0x0003B65F File Offset: 0x0003985F
		[XmlElement(ElementName = "SourceServer")]
		public new string SourceServer
		{
			get
			{
				return base.SourceServer;
			}
			set
			{
				base.SourceServer = value;
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x0003B668 File Offset: 0x00039868
		// (set) Token: 0x06001BFF RID: 7167 RVA: 0x0003B670 File Offset: 0x00039870
		[XmlElement(ElementName = "SourceArchiveServer")]
		public new string SourceArchiveServer
		{
			get
			{
				return base.SourceArchiveServer;
			}
			set
			{
				base.SourceArchiveServer = value;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x0003B679 File Offset: 0x00039879
		// (set) Token: 0x06001C01 RID: 7169 RVA: 0x0003B686 File Offset: 0x00039886
		[XmlElement(ElementName = "DestinationDatabase")]
		public ADObjectIdXML DestinationDatabaseXML
		{
			get
			{
				return ADObjectIdXML.Serialize(base.TargetDatabase);
			}
			set
			{
				base.TargetDatabase = ADObjectIdXML.Deserialize(value);
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x0003B694 File Offset: 0x00039894
		// (set) Token: 0x06001C03 RID: 7171 RVA: 0x0003B6A1 File Offset: 0x000398A1
		[XmlElement(ElementName = "ArchiveDestinationDatabase")]
		public ADObjectIdXML ArchiveDestinationDatabaseXML
		{
			get
			{
				return ADObjectIdXML.Serialize(base.TargetArchiveDatabase);
			}
			set
			{
				base.TargetArchiveDatabase = ADObjectIdXML.Deserialize(value);
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x0003B6AF File Offset: 0x000398AF
		// (set) Token: 0x06001C05 RID: 7173 RVA: 0x0003B6B7 File Offset: 0x000398B7
		[XmlElement(ElementName = "DestinationVersion")]
		public int DestinationVersion
		{
			get
			{
				return base.TargetVersion;
			}
			set
			{
				base.TargetVersion = value;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x0003B6C0 File Offset: 0x000398C0
		// (set) Token: 0x06001C07 RID: 7175 RVA: 0x0003B6C8 File Offset: 0x000398C8
		[XmlElement(ElementName = "DestinationArchiveVersion")]
		public int DestinationArchiveVersion
		{
			get
			{
				return base.TargetArchiveVersion;
			}
			set
			{
				base.TargetArchiveVersion = value;
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x0003B6D1 File Offset: 0x000398D1
		// (set) Token: 0x06001C09 RID: 7177 RVA: 0x0003B6D9 File Offset: 0x000398D9
		[XmlElement(ElementName = "DestinationServer")]
		public string DestinationServer
		{
			get
			{
				return base.TargetServer;
			}
			set
			{
				base.TargetServer = value;
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x0003B6E2 File Offset: 0x000398E2
		// (set) Token: 0x06001C0B RID: 7179 RVA: 0x0003B6EA File Offset: 0x000398EA
		[XmlElement(ElementName = "DestinationContainerGuid")]
		public Guid? DestinationContainerGuid
		{
			get
			{
				return base.TargetContainerGuid;
			}
			set
			{
				base.TargetContainerGuid = value;
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x0003B6F3 File Offset: 0x000398F3
		// (set) Token: 0x06001C0D RID: 7181 RVA: 0x0003B70A File Offset: 0x0003990A
		[XmlElement(ElementName = "DestinationUnifiedMailboxId")]
		public byte[] DestinationUnifiedMailboxId
		{
			get
			{
				if (base.TargetUnifiedMailboxId != null)
				{
					return base.TargetUnifiedMailboxId.GetBytes();
				}
				return null;
			}
			set
			{
				base.TargetUnifiedMailboxId = ((value == null) ? null : CrossTenantObjectId.Parse(value, true));
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x0003B71F File Offset: 0x0003991F
		// (set) Token: 0x06001C0F RID: 7183 RVA: 0x0003B727 File Offset: 0x00039927
		[XmlElement(ElementName = "DestinationArchiveServer")]
		public string DestinationArchiveServer
		{
			get
			{
				return base.TargetArchiveServer;
			}
			set
			{
				base.TargetArchiveServer = value;
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x0003B730 File Offset: 0x00039930
		// (set) Token: 0x06001C11 RID: 7185 RVA: 0x0003B73D File Offset: 0x0003993D
		[XmlElement(ElementName = "RequestQueue")]
		public ADObjectIdXML RequestQueueXML
		{
			get
			{
				return ADObjectIdXML.Serialize(base.RequestQueue);
			}
			set
			{
				base.RequestQueue = ADObjectIdXML.Deserialize(value);
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x0003B74B File Offset: 0x0003994B
		// (set) Token: 0x06001C13 RID: 7187 RVA: 0x0003B753 File Offset: 0x00039953
		[XmlElement(ElementName = "RehomeRequest")]
		public new bool RehomeRequest
		{
			get
			{
				return base.RehomeRequest;
			}
			set
			{
				base.RehomeRequest = value;
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x0003B75C File Offset: 0x0003995C
		// (set) Token: 0x06001C15 RID: 7189 RVA: 0x0003B764 File Offset: 0x00039964
		[XmlArrayItem(ElementName = "F")]
		[XmlArray(ElementName = "IncludeFolders")]
		public new string[] IncludeFolders
		{
			get
			{
				return base.IncludeFolders;
			}
			set
			{
				base.IncludeFolders = value;
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x0003B76D File Offset: 0x0003996D
		// (set) Token: 0x06001C17 RID: 7191 RVA: 0x0003B775 File Offset: 0x00039975
		[XmlArray(ElementName = "ExcludeFolders")]
		[XmlArrayItem(ElementName = "F")]
		public new string[] ExcludeFolders
		{
			get
			{
				return base.ExcludeFolders;
			}
			set
			{
				base.ExcludeFolders = value;
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06001C18 RID: 7192 RVA: 0x0003B77E File Offset: 0x0003997E
		// (set) Token: 0x06001C19 RID: 7193 RVA: 0x0003B786 File Offset: 0x00039986
		[XmlElement(ElementName = "ExcludeDumpster")]
		public new bool ExcludeDumpster
		{
			get
			{
				return base.ExcludeDumpster;
			}
			set
			{
				base.ExcludeDumpster = value;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x0003B78F File Offset: 0x0003998F
		// (set) Token: 0x06001C1B RID: 7195 RVA: 0x0003B797 File Offset: 0x00039997
		[XmlElement(ElementName = "SourceDCName")]
		public new string SourceDCName
		{
			get
			{
				return base.SourceDCName;
			}
			set
			{
				base.SourceDCName = value;
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x0003B7A0 File Offset: 0x000399A0
		// (set) Token: 0x06001C1D RID: 7197 RVA: 0x0003B7AD File Offset: 0x000399AD
		[XmlElement(ElementName = "SourceCredential")]
		public NetworkCredentialXML SourceCredentialXML
		{
			get
			{
				return NetworkCredentialXML.Get(base.SourceCredential);
			}
			set
			{
				base.SourceCredential = NetworkCredentialXML.Get(value);
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06001C1E RID: 7198 RVA: 0x0003B7BB File Offset: 0x000399BB
		// (set) Token: 0x06001C1F RID: 7199 RVA: 0x0003B7C3 File Offset: 0x000399C3
		[XmlElement(ElementName = "DestinationDCName")]
		public string DestinationDCName
		{
			get
			{
				return base.TargetDCName;
			}
			set
			{
				base.TargetDCName = value;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x0003B7CC File Offset: 0x000399CC
		// (set) Token: 0x06001C21 RID: 7201 RVA: 0x0003B7D9 File Offset: 0x000399D9
		[XmlElement(ElementName = "DestinationCredential")]
		public NetworkCredentialXML DestinationCredentialXML
		{
			get
			{
				return NetworkCredentialXML.Get(base.TargetCredential);
			}
			set
			{
				base.TargetCredential = NetworkCredentialXML.Get(value);
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x0003B7E7 File Offset: 0x000399E7
		// (set) Token: 0x06001C23 RID: 7203 RVA: 0x0003B7EF File Offset: 0x000399EF
		[XmlElement(ElementName = "RemoteHostName")]
		public new string RemoteHostName
		{
			get
			{
				return base.RemoteHostName;
			}
			set
			{
				base.RemoteHostName = value;
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x0003B7F8 File Offset: 0x000399F8
		// (set) Token: 0x06001C25 RID: 7205 RVA: 0x0003B800 File Offset: 0x00039A00
		[XmlElement(ElementName = "BatchName")]
		public new string BatchName
		{
			get
			{
				return base.BatchName;
			}
			set
			{
				base.BatchName = value;
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x0003B809 File Offset: 0x00039A09
		// (set) Token: 0x06001C27 RID: 7207 RVA: 0x0003B816 File Offset: 0x00039A16
		[XmlElement(ElementName = "RemoteCredential")]
		public NetworkCredentialXML RemoteCredentialXML
		{
			get
			{
				return NetworkCredentialXML.Get(base.RemoteCredential);
			}
			set
			{
				base.RemoteCredential = NetworkCredentialXML.Get(value);
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x0003B824 File Offset: 0x00039A24
		// (set) Token: 0x06001C29 RID: 7209 RVA: 0x0003B82C File Offset: 0x00039A2C
		[XmlElement(ElementName = "RemoteDatabaseName")]
		public new string RemoteDatabaseName
		{
			get
			{
				return base.RemoteDatabaseName;
			}
			set
			{
				base.RemoteDatabaseName = value;
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06001C2A RID: 7210 RVA: 0x0003B835 File Offset: 0x00039A35
		// (set) Token: 0x06001C2B RID: 7211 RVA: 0x0003B83D File Offset: 0x00039A3D
		[XmlElement(ElementName = "RemoteDatabaseGuid")]
		public new Guid? RemoteDatabaseGuid
		{
			get
			{
				return base.RemoteDatabaseGuid;
			}
			set
			{
				base.RemoteDatabaseGuid = value;
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06001C2C RID: 7212 RVA: 0x0003B846 File Offset: 0x00039A46
		// (set) Token: 0x06001C2D RID: 7213 RVA: 0x0003B84E File Offset: 0x00039A4E
		[XmlElement(ElementName = "RemoteArchiveDatabaseName")]
		public new string RemoteArchiveDatabaseName
		{
			get
			{
				return base.RemoteArchiveDatabaseName;
			}
			set
			{
				base.RemoteArchiveDatabaseName = value;
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06001C2E RID: 7214 RVA: 0x0003B857 File Offset: 0x00039A57
		// (set) Token: 0x06001C2F RID: 7215 RVA: 0x0003B85F File Offset: 0x00039A5F
		[XmlElement(ElementName = "RemoteArchiveDatabaseGuid")]
		public new Guid? RemoteArchiveDatabaseGuid
		{
			get
			{
				return base.RemoteArchiveDatabaseGuid;
			}
			set
			{
				base.RemoteArchiveDatabaseGuid = value;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x0003B868 File Offset: 0x00039A68
		// (set) Token: 0x06001C31 RID: 7217 RVA: 0x0003B895 File Offset: 0x00039A95
		[XmlElement(ElementName = "BadItemLimit")]
		public int BadItemLimitInt
		{
			get
			{
				if (!base.BadItemLimit.IsUnlimited)
				{
					return base.BadItemLimit.Value;
				}
				return -1;
			}
			set
			{
				base.BadItemLimit = ((value < 0) ? Unlimited<int>.UnlimitedValue : new Unlimited<int>(value));
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x0003B8AE File Offset: 0x00039AAE
		// (set) Token: 0x06001C33 RID: 7219 RVA: 0x0003B8B6 File Offset: 0x00039AB6
		[XmlElement(ElementName = "BadItemsEncountered")]
		public new int BadItemsEncountered
		{
			get
			{
				return base.BadItemsEncountered;
			}
			set
			{
				base.BadItemsEncountered = value;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06001C34 RID: 7220 RVA: 0x0003B8C0 File Offset: 0x00039AC0
		// (set) Token: 0x06001C35 RID: 7221 RVA: 0x0003B8ED File Offset: 0x00039AED
		[XmlElement(ElementName = "LargeItemLimit")]
		public int LargeItemLimitInt
		{
			get
			{
				if (!base.LargeItemLimit.IsUnlimited)
				{
					return base.LargeItemLimit.Value;
				}
				return -1;
			}
			set
			{
				base.LargeItemLimit = ((value < 0) ? Unlimited<int>.UnlimitedValue : new Unlimited<int>(value));
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06001C36 RID: 7222 RVA: 0x0003B906 File Offset: 0x00039B06
		// (set) Token: 0x06001C37 RID: 7223 RVA: 0x0003B90E File Offset: 0x00039B0E
		[XmlElement(ElementName = "LargeItemsEncountered")]
		public new int LargeItemsEncountered
		{
			get
			{
				return base.LargeItemsEncountered;
			}
			set
			{
				base.LargeItemsEncountered = value;
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0003B917 File Offset: 0x00039B17
		// (set) Token: 0x06001C39 RID: 7225 RVA: 0x0003B91F File Offset: 0x00039B1F
		[XmlElement(ElementName = "AllowLargeItems")]
		public new bool AllowLargeItems
		{
			get
			{
				return base.AllowLargeItems;
			}
			set
			{
				base.AllowLargeItems = value;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x0003B928 File Offset: 0x00039B28
		// (set) Token: 0x06001C3B RID: 7227 RVA: 0x0003B930 File Offset: 0x00039B30
		[XmlElement(ElementName = "MissingItemsEncountered")]
		public new int MissingItemsEncountered
		{
			get
			{
				return base.MissingItemsEncountered;
			}
			set
			{
				base.MissingItemsEncountered = value;
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x0003B939 File Offset: 0x00039B39
		// (set) Token: 0x06001C3D RID: 7229 RVA: 0x0003B941 File Offset: 0x00039B41
		[XmlElement(ElementName = "TimeTracker")]
		public new RequestJobTimeTracker TimeTracker
		{
			get
			{
				return base.TimeTracker;
			}
			set
			{
				base.TimeTracker = value;
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x0003B94A File Offset: 0x00039B4A
		// (set) Token: 0x06001C3F RID: 7231 RVA: 0x0003B952 File Offset: 0x00039B52
		[XmlElement(ElementName = "ProgressTracker")]
		public new TransferProgressTracker ProgressTracker
		{
			get
			{
				return base.ProgressTracker;
			}
			set
			{
				base.ProgressTracker += value;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x0003B966 File Offset: 0x00039B66
		// (set) Token: 0x06001C41 RID: 7233 RVA: 0x0003B973 File Offset: 0x00039B73
		[XmlArrayItem(ElementName = "Folder")]
		[XmlArray(ElementName = "FolderList")]
		public MoveFolderInfo[] Folders
		{
			get
			{
				return base.FolderList.ToArray();
			}
			set
			{
				base.FolderList.Clear();
				if (value != null)
				{
					base.FolderList.AddRange(value);
				}
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06001C42 RID: 7234 RVA: 0x0003B98F File Offset: 0x00039B8F
		// (set) Token: 0x06001C43 RID: 7235 RVA: 0x0003B997 File Offset: 0x00039B97
		[XmlElement(ElementName = "MoveServerName")]
		public new string MRSServerName
		{
			get
			{
				return base.MRSServerName;
			}
			set
			{
				base.MRSServerName = value;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x0003B9A0 File Offset: 0x00039BA0
		// (set) Token: 0x06001C45 RID: 7237 RVA: 0x0003B9A8 File Offset: 0x00039BA8
		[XmlElement(ElementName = "TotalMailboxSize")]
		public new ulong TotalMailboxSize
		{
			get
			{
				return base.TotalMailboxSize;
			}
			set
			{
				base.TotalMailboxSize = value;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x0003B9B1 File Offset: 0x00039BB1
		// (set) Token: 0x06001C47 RID: 7239 RVA: 0x0003B9B9 File Offset: 0x00039BB9
		[XmlElement(ElementName = "TotalMailboxItemCount")]
		public new ulong TotalMailboxItemCount
		{
			get
			{
				return base.TotalMailboxItemCount;
			}
			set
			{
				base.TotalMailboxItemCount = value;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x0003B9C2 File Offset: 0x00039BC2
		// (set) Token: 0x06001C49 RID: 7241 RVA: 0x0003B9CA File Offset: 0x00039BCA
		[XmlElement(ElementName = "TotalArchiveSize")]
		public new ulong? TotalArchiveSize
		{
			get
			{
				return base.TotalArchiveSize;
			}
			set
			{
				base.TotalArchiveSize = value;
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06001C4A RID: 7242 RVA: 0x0003B9D3 File Offset: 0x00039BD3
		// (set) Token: 0x06001C4B RID: 7243 RVA: 0x0003B9DB File Offset: 0x00039BDB
		[XmlElement(ElementName = "TotalArchiveItemCount")]
		public new ulong? TotalArchiveItemCount
		{
			get
			{
				return base.TotalArchiveItemCount;
			}
			set
			{
				base.TotalArchiveItemCount = value;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06001C4C RID: 7244 RVA: 0x0003B9E4 File Offset: 0x00039BE4
		// (set) Token: 0x06001C4D RID: 7245 RVA: 0x0003B9E8 File Offset: 0x00039BE8
		[XmlElement(ElementName = "BytesTransferred")]
		public string BytesTransferred
		{
			get
			{
				return null;
			}
			set
			{
				ulong num;
				if (value != null && ulong.TryParse(value, out num))
				{
					this.ProgressTracker.BytesTransferred += num;
				}
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x0003BA15 File Offset: 0x00039C15
		// (set) Token: 0x06001C4F RID: 7247 RVA: 0x0003BA18 File Offset: 0x00039C18
		[XmlElement(ElementName = "ItemsTransferred")]
		public string ItemsTransferred
		{
			get
			{
				return null;
			}
			set
			{
				ulong num;
				if (value != null && ulong.TryParse(value, out num))
				{
					this.ProgressTracker.ItemsTransferred += num;
				}
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x0003BA45 File Offset: 0x00039C45
		// (set) Token: 0x06001C51 RID: 7249 RVA: 0x0003BA4D File Offset: 0x00039C4D
		[XmlElement(ElementName = "PercentComplete")]
		public new int PercentComplete
		{
			get
			{
				return base.PercentComplete;
			}
			set
			{
				base.PercentComplete = value;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x0003BA56 File Offset: 0x00039C56
		// (set) Token: 0x06001C53 RID: 7251 RVA: 0x0003BA5E File Offset: 0x00039C5E
		[XmlElement(ElementName = "FailureCode")]
		public new int? FailureCode
		{
			get
			{
				return base.FailureCode;
			}
			set
			{
				base.FailureCode = value;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06001C54 RID: 7252 RVA: 0x0003BA67 File Offset: 0x00039C67
		// (set) Token: 0x06001C55 RID: 7253 RVA: 0x0003BA6F File Offset: 0x00039C6F
		[XmlElement(ElementName = "FailureType")]
		public new string FailureType
		{
			get
			{
				return base.FailureType;
			}
			set
			{
				base.FailureType = value;
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06001C56 RID: 7254 RVA: 0x0003BA78 File Offset: 0x00039C78
		// (set) Token: 0x06001C57 RID: 7255 RVA: 0x0003BAB4 File Offset: 0x00039CB4
		[XmlElement(ElementName = "FailureSide")]
		public int? FailureSideInt
		{
			get
			{
				if (base.FailureSide == null)
				{
					return null;
				}
				return new int?((int)base.FailureSide.Value);
			}
			set
			{
				base.FailureSide = ((value != null) ? new ExceptionSide?((ExceptionSide)value.Value) : null);
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x0003BAE7 File Offset: 0x00039CE7
		// (set) Token: 0x06001C59 RID: 7257 RVA: 0x0003BAF4 File Offset: 0x00039CF4
		[XmlElement(ElementName = "MessageData")]
		public byte[] MessageData
		{
			get
			{
				return CommonUtils.ByteSerialize(base.Message);
			}
			set
			{
				base.Message = CommonUtils.ByteDeserialize(value);
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x0003BB02 File Offset: 0x00039D02
		// (set) Token: 0x06001C5B RID: 7259 RVA: 0x0003BB0A File Offset: 0x00039D0A
		[XmlElement(ElementName = "RetryCount")]
		public new int RetryCount
		{
			get
			{
				return base.RetryCount;
			}
			set
			{
				base.RetryCount = value;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x0003BB13 File Offset: 0x00039D13
		// (set) Token: 0x06001C5D RID: 7261 RVA: 0x0003BB1B File Offset: 0x00039D1B
		[XmlElement(ElementName = "TotalRetryCount")]
		public new int TotalRetryCount
		{
			get
			{
				return base.TotalRetryCount;
			}
			set
			{
				base.TotalRetryCount = value;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x0003BB24 File Offset: 0x00039D24
		// (set) Token: 0x06001C5F RID: 7263 RVA: 0x0003BB2C File Offset: 0x00039D2C
		[XmlElement(ElementName = "AllowedToFinishMove")]
		public new bool AllowedToFinishMove
		{
			get
			{
				return base.AllowedToFinishMove;
			}
			set
			{
				base.AllowedToFinishMove = value;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x0003BB35 File Offset: 0x00039D35
		// (set) Token: 0x06001C61 RID: 7265 RVA: 0x0003BB3D File Offset: 0x00039D3D
		[XmlElement(ElementName = "PreserveMailboxSignature")]
		public new bool PreserveMailboxSignature
		{
			get
			{
				return base.PreserveMailboxSignature;
			}
			set
			{
				base.PreserveMailboxSignature = value;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x0003BB46 File Offset: 0x00039D46
		// (set) Token: 0x06001C63 RID: 7267 RVA: 0x0003BB4E File Offset: 0x00039D4E
		[XmlElement(ElementName = "RestartingAfterSignatureChange")]
		public new bool RestartingAfterSignatureChange
		{
			get
			{
				return base.RestartingAfterSignatureChange;
			}
			set
			{
				base.RestartingAfterSignatureChange = value;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x0003BB57 File Offset: 0x00039D57
		// (set) Token: 0x06001C65 RID: 7269 RVA: 0x0003BB5F File Offset: 0x00039D5F
		[XmlElement(ElementName = "IsIntegData")]
		public new int? IsIntegData
		{
			get
			{
				return base.IsIntegData;
			}
			set
			{
				base.IsIntegData = value;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x0003BB68 File Offset: 0x00039D68
		// (set) Token: 0x06001C67 RID: 7271 RVA: 0x0003BB70 File Offset: 0x00039D70
		[XmlElement(ElementName = "UserPuid")]
		public new long? UserPuid
		{
			get
			{
				return base.UserPuid;
			}
			set
			{
				base.UserPuid = value;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x0003BB79 File Offset: 0x00039D79
		// (set) Token: 0x06001C69 RID: 7273 RVA: 0x0003BB81 File Offset: 0x00039D81
		[XmlElement(ElementName = "OlcDGroup")]
		public new int? OlcDGroup
		{
			get
			{
				return base.OlcDGroup;
			}
			set
			{
				base.OlcDGroup = value;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x0003BB8A File Offset: 0x00039D8A
		// (set) Token: 0x06001C6B RID: 7275 RVA: 0x0003BB92 File Offset: 0x00039D92
		[XmlElement(ElementName = "CancelMove")]
		public new bool CancelRequest
		{
			get
			{
				return base.CancelRequest;
			}
			set
			{
				base.CancelRequest = value;
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x0003BB9B File Offset: 0x00039D9B
		// (set) Token: 0x06001C6D RID: 7277 RVA: 0x0003BBA3 File Offset: 0x00039DA3
		[XmlElement(ElementName = "DomainControllerToUpdate")]
		public new string DomainControllerToUpdate
		{
			get
			{
				return base.DomainControllerToUpdate;
			}
			set
			{
				base.DomainControllerToUpdate = value;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x0003BBAC File Offset: 0x00039DAC
		// (set) Token: 0x06001C6F RID: 7279 RVA: 0x0003BBB4 File Offset: 0x00039DB4
		[XmlElement(ElementName = "RemoteDomainControllerToUpdate")]
		public new string RemoteDomainControllerToUpdate
		{
			get
			{
				return base.RemoteDomainControllerToUpdate;
			}
			set
			{
				base.RemoteDomainControllerToUpdate = value;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x0003BBBD File Offset: 0x00039DBD
		// (set) Token: 0x06001C71 RID: 7281 RVA: 0x0003BBC5 File Offset: 0x00039DC5
		[XmlElement(ElementName = "RemoteOrgName")]
		public new string RemoteOrgName
		{
			get
			{
				return base.RemoteOrgName;
			}
			set
			{
				base.RemoteOrgName = value;
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x0003BBCE File Offset: 0x00039DCE
		// (set) Token: 0x06001C73 RID: 7283 RVA: 0x0003BBD6 File Offset: 0x00039DD6
		[XmlElement(ElementName = "IgnoreRuleLimitErrors")]
		public new bool IgnoreRuleLimitErrors
		{
			get
			{
				return base.IgnoreRuleLimitErrors;
			}
			set
			{
				base.IgnoreRuleLimitErrors = value;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x0003BBDF File Offset: 0x00039DDF
		// (set) Token: 0x06001C75 RID: 7285 RVA: 0x0003BBE7 File Offset: 0x00039DE7
		[XmlElement(ElementName = "TargetDeliveryDomain")]
		public new string TargetDeliveryDomain
		{
			get
			{
				return base.TargetDeliveryDomain;
			}
			set
			{
				base.TargetDeliveryDomain = value;
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x0003BBF0 File Offset: 0x00039DF0
		// (set) Token: 0x06001C77 RID: 7287 RVA: 0x0003BBF8 File Offset: 0x00039DF8
		[XmlElement(ElementName = "JobType")]
		public int JobTypeInt
		{
			get
			{
				return (int)base.JobType;
			}
			set
			{
				base.JobType = (MRSJobType)value;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x0003BC01 File Offset: 0x00039E01
		// (set) Token: 0x06001C79 RID: 7289 RVA: 0x0003BC0E File Offset: 0x00039E0E
		[XmlArray(ElementName = "IndexIds")]
		[XmlArrayItem(ElementName = "IndexId")]
		public RequestIndexId[] IndexIdsArray
		{
			get
			{
				return base.IndexIds.ToArray();
			}
			set
			{
				base.IndexIds.Clear();
				if (value != null)
				{
					base.IndexIds.AddRange(value);
				}
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x0003BC2A File Offset: 0x00039E2A
		// (set) Token: 0x06001C7B RID: 7291 RVA: 0x0003BC37 File Offset: 0x00039E37
		[XmlArray(ElementName = "FolderToMailboxes")]
		[XmlArrayItem(ElementName = "FolderToMailbox")]
		public FolderToMailboxMapping[] FolderToMailboxArray
		{
			get
			{
				return base.FolderToMailboxMap.ToArray();
			}
			set
			{
				base.FolderToMailboxMap.Clear();
				if (value != null)
				{
					base.FolderToMailboxMap.AddRange(value);
				}
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x0003BC53 File Offset: 0x00039E53
		// (set) Token: 0x06001C7D RID: 7293 RVA: 0x0003BC5B File Offset: 0x00039E5B
		[XmlElement(ElementName = "Name")]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x0003BC64 File Offset: 0x00039E64
		// (set) Token: 0x06001C7F RID: 7295 RVA: 0x0003BC6C File Offset: 0x00039E6C
		[XmlElement(ElementName = "RequestType")]
		public int RequestTypeInt
		{
			get
			{
				return (int)base.RequestType;
			}
			set
			{
				base.RequestType = (MRSRequestType)value;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x0003BC75 File Offset: 0x00039E75
		// (set) Token: 0x06001C81 RID: 7297 RVA: 0x0003BC7D File Offset: 0x00039E7D
		[XmlElement(ElementName = "FileName")]
		public new string FilePath
		{
			get
			{
				return base.FilePath;
			}
			set
			{
				base.FilePath = value;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x0003BC88 File Offset: 0x00039E88
		// (set) Token: 0x06001C83 RID: 7299 RVA: 0x0003BCC4 File Offset: 0x00039EC4
		[XmlElement(ElementName = "MailboxRestoreFlags")]
		public int? MailboxRestoreFlagsInt
		{
			get
			{
				if (base.MailboxRestoreFlags == null)
				{
					return null;
				}
				return new int?((int)base.MailboxRestoreFlags.Value);
			}
			set
			{
				base.MailboxRestoreFlags = ((value != null) ? new MailboxRestoreType?((MailboxRestoreType)value.Value) : null);
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x0003BCF7 File Offset: 0x00039EF7
		// (set) Token: 0x06001C85 RID: 7301 RVA: 0x0003BD04 File Offset: 0x00039F04
		[XmlElement(ElementName = "TargetUserId")]
		public ADObjectIdXML TargetUserIdXML
		{
			get
			{
				return ADObjectIdXML.Serialize(base.TargetUserId);
			}
			set
			{
				base.TargetUserId = ADObjectIdXML.Deserialize(value);
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x0003BD12 File Offset: 0x00039F12
		// (set) Token: 0x06001C87 RID: 7303 RVA: 0x0003BD1F File Offset: 0x00039F1F
		[XmlElement(ElementName = "SourceUserId")]
		public ADObjectIdXML SourceUserIdXML
		{
			get
			{
				return ADObjectIdXML.Serialize(base.SourceUserId);
			}
			set
			{
				base.SourceUserId = ADObjectIdXML.Deserialize(value);
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x0003BD2D File Offset: 0x00039F2D
		// (set) Token: 0x06001C89 RID: 7305 RVA: 0x0003BD35 File Offset: 0x00039F35
		[XmlElement(ElementName = "RemoteMailboxLegacyDN")]
		public new string RemoteMailboxLegacyDN
		{
			get
			{
				return base.RemoteMailboxLegacyDN;
			}
			set
			{
				base.RemoteMailboxLegacyDN = value;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x0003BD3E File Offset: 0x00039F3E
		// (set) Token: 0x06001C8B RID: 7307 RVA: 0x0003BD46 File Offset: 0x00039F46
		[XmlElement(ElementName = "RemoteUserLegacyDN")]
		public new string RemoteUserLegacyDN
		{
			get
			{
				return base.RemoteUserLegacyDN;
			}
			set
			{
				base.RemoteUserLegacyDN = value;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x0003BD4F File Offset: 0x00039F4F
		// (set) Token: 0x06001C8D RID: 7309 RVA: 0x0003BD57 File Offset: 0x00039F57
		[XmlElement(ElementName = "RemoteMailboxServerLegacyDN")]
		public new string RemoteMailboxServerLegacyDN
		{
			get
			{
				return base.RemoteMailboxServerLegacyDN;
			}
			set
			{
				base.RemoteMailboxServerLegacyDN = value;
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x0003BD60 File Offset: 0x00039F60
		// (set) Token: 0x06001C8F RID: 7311 RVA: 0x0003BD68 File Offset: 0x00039F68
		[XmlElement(ElementName = "OutlookAnywhereHostName")]
		public new string OutlookAnywhereHostName
		{
			get
			{
				return base.OutlookAnywhereHostName;
			}
			set
			{
				base.OutlookAnywhereHostName = value;
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x0003BD71 File Offset: 0x00039F71
		// (set) Token: 0x06001C91 RID: 7313 RVA: 0x0003BD79 File Offset: 0x00039F79
		[XmlElement(ElementName = "AuthenticationMethod")]
		public new AuthenticationMethod? AuthenticationMethod
		{
			get
			{
				return base.AuthenticationMethod;
			}
			set
			{
				base.AuthenticationMethod = value;
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06001C92 RID: 7314 RVA: 0x0003BD82 File Offset: 0x00039F82
		// (set) Token: 0x06001C93 RID: 7315 RVA: 0x0003BD8A File Offset: 0x00039F8A
		[XmlElement(ElementName = "IsAdministrativeCredential")]
		public new bool? IsAdministrativeCredential
		{
			get
			{
				return base.IsAdministrativeCredential;
			}
			set
			{
				base.IsAdministrativeCredential = value;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x0003BD93 File Offset: 0x00039F93
		// (set) Token: 0x06001C95 RID: 7317 RVA: 0x0003BD9B File Offset: 0x00039F9B
		[XmlElement(ElementName = "ConflictResolutionOption")]
		public new ConflictResolutionOption? ConflictResolutionOption
		{
			get
			{
				return base.ConflictResolutionOption;
			}
			set
			{
				base.ConflictResolutionOption = value;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06001C96 RID: 7318 RVA: 0x0003BDA4 File Offset: 0x00039FA4
		// (set) Token: 0x06001C97 RID: 7319 RVA: 0x0003BDAC File Offset: 0x00039FAC
		[XmlElement(ElementName = "AssociatedMessagesCopyOption")]
		public new FAICopyOption? AssociatedMessagesCopyOption
		{
			get
			{
				return base.AssociatedMessagesCopyOption;
			}
			set
			{
				base.AssociatedMessagesCopyOption = value;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x0003BDB5 File Offset: 0x00039FB5
		// (set) Token: 0x06001C99 RID: 7321 RVA: 0x0003BDC2 File Offset: 0x00039FC2
		[XmlElement(ElementName = "OrganizationalUnitRoot")]
		public ADObjectIdXML OrganizationalUnitRootXML
		{
			get
			{
				return ADObjectIdXML.Serialize(base.OrganizationalUnitRoot);
			}
			set
			{
				base.OrganizationalUnitRoot = ADObjectIdXML.Deserialize(value);
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06001C9A RID: 7322 RVA: 0x0003BDD0 File Offset: 0x00039FD0
		// (set) Token: 0x06001C9B RID: 7323 RVA: 0x0003BDDD File Offset: 0x00039FDD
		[XmlElement(ElementName = "ConfigurationUnit")]
		public ADObjectIdXML ConfigurationUnitXML
		{
			get
			{
				return ADObjectIdXML.Serialize(base.ConfigurationUnit);
			}
			set
			{
				base.ConfigurationUnit = ADObjectIdXML.Deserialize(value);
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06001C9C RID: 7324 RVA: 0x0003BDEB File Offset: 0x00039FEB
		// (set) Token: 0x06001C9D RID: 7325 RVA: 0x0003BDF3 File Offset: 0x00039FF3
		[XmlElement(ElementName = "ContentFilter")]
		public new string ContentFilter
		{
			get
			{
				return base.ContentFilter;
			}
			set
			{
				base.ContentFilter = value;
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x0003BDFC File Offset: 0x00039FFC
		// (set) Token: 0x06001C9F RID: 7327 RVA: 0x0003BE04 File Offset: 0x0003A004
		[XmlElement(ElementName = "ContentFilterLCID")]
		public new int ContentFilterLCID
		{
			get
			{
				return base.ContentFilterLCID;
			}
			set
			{
				base.ContentFilterLCID = value;
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x0003BE0D File Offset: 0x0003A00D
		// (set) Token: 0x06001CA1 RID: 7329 RVA: 0x0003BE15 File Offset: 0x0003A015
		[XmlElement(ElementName = "Priority")]
		public new int Priority
		{
			get
			{
				return (int)base.Priority;
			}
			set
			{
				base.Priority = (RequestPriority)value;
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x0003BE1E File Offset: 0x0003A01E
		// (set) Token: 0x06001CA3 RID: 7331 RVA: 0x0003BE26 File Offset: 0x0003A026
		[XmlElement(ElementName = "WorkloadType")]
		public new int WorkloadType
		{
			get
			{
				return (int)base.WorkloadType;
			}
			set
			{
				base.WorkloadType = (RequestWorkloadType)value;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x0003BE2F File Offset: 0x0003A02F
		// (set) Token: 0x06001CA5 RID: 7333 RVA: 0x0003BE37 File Offset: 0x0003A037
		[XmlElement(ElementName = "RequestFlags")]
		public int RequestJobInternalFlagsInt
		{
			get
			{
				return (int)base.RequestJobInternalFlags;
			}
			set
			{
				base.RequestJobInternalFlags = (RequestJobInternalFlags)value;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06001CA6 RID: 7334 RVA: 0x0003BE40 File Offset: 0x0003A040
		// (set) Token: 0x06001CA7 RID: 7335 RVA: 0x0003BE82 File Offset: 0x0003A082
		[XmlElement(ElementName = "CompletedRequestAgeLimitTicks")]
		public long? CompletedRequestAgeLimitTicks
		{
			get
			{
				if (!base.CompletedRequestAgeLimit.IsUnlimited)
				{
					return new long?(base.CompletedRequestAgeLimit.Value.Ticks);
				}
				return null;
			}
			set
			{
				base.CompletedRequestAgeLimit = ((value != null) ? new Unlimited<EnhancedTimeSpan>(new TimeSpan(value.Value)) : Unlimited<EnhancedTimeSpan>.UnlimitedValue);
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x0003BEB0 File Offset: 0x0003A0B0
		// (set) Token: 0x06001CA9 RID: 7337 RVA: 0x0003BEF4 File Offset: 0x0003A0F4
		[XmlElement(ElementName = "LastPickupTime")]
		public long? LastPickupTimeTicks
		{
			get
			{
				if (base.LastPickupTime == null)
				{
					return null;
				}
				return new long?(base.LastPickupTime.Value.Ticks);
			}
			set
			{
				base.LastPickupTime = ((value != null) ? new DateTime?(new DateTime(value.Value)) : null);
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x0003BF2C File Offset: 0x0003A12C
		// (set) Token: 0x06001CAB RID: 7339 RVA: 0x0003BF34 File Offset: 0x0003A134
		[XmlElement(ElementName = "RequestCreator")]
		public new string RequestCreator
		{
			get
			{
				return base.RequestCreator;
			}
			set
			{
				base.RequestCreator = value;
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x0003BF3D File Offset: 0x0003A13D
		// (set) Token: 0x06001CAD RID: 7341 RVA: 0x0003BF45 File Offset: 0x0003A145
		[XmlElement(ElementName = "PoisonCount")]
		public new int PoisonCount
		{
			get
			{
				return base.PoisonCount;
			}
			set
			{
				base.PoisonCount = value;
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x0003BF4E File Offset: 0x0003A14E
		// (set) Token: 0x06001CAF RID: 7343 RVA: 0x0003BF56 File Offset: 0x0003A156
		[XmlElement(ElementName = "ContentCodePage")]
		public new int? ContentCodePage
		{
			get
			{
				return base.ContentCodePage;
			}
			set
			{
				base.ContentCodePage = value;
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x0003BF5F File Offset: 0x0003A15F
		// (set) Token: 0x06001CB1 RID: 7345 RVA: 0x0003BF67 File Offset: 0x0003A167
		[XmlElement(ElementName = "RemoteHostPort")]
		public new int RemoteHostPort
		{
			get
			{
				return base.RemoteHostPort;
			}
			set
			{
				base.RemoteHostPort = value;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x0003BF70 File Offset: 0x0003A170
		// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x0003BF78 File Offset: 0x0003A178
		[XmlElement(ElementName = "SmtpServerName")]
		public new string SmtpServerName
		{
			get
			{
				return base.SmtpServerName;
			}
			set
			{
				base.SmtpServerName = value;
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x0003BF81 File Offset: 0x0003A181
		// (set) Token: 0x06001CB5 RID: 7349 RVA: 0x0003BF89 File Offset: 0x0003A189
		[XmlElement(ElementName = "SmtpServerPort")]
		public new int SmtpServerPort
		{
			get
			{
				return base.SmtpServerPort;
			}
			set
			{
				base.SmtpServerPort = value;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x0003BF92 File Offset: 0x0003A192
		// (set) Token: 0x06001CB7 RID: 7351 RVA: 0x0003BF9A File Offset: 0x0003A19A
		[XmlElement(ElementName = "SecurityMechanism")]
		public int SecurityMechanismInt
		{
			get
			{
				return (int)base.SecurityMechanism;
			}
			set
			{
				base.SecurityMechanism = (IMAPSecurityMechanism)value;
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x0003BFA3 File Offset: 0x0003A1A3
		// (set) Token: 0x06001CB9 RID: 7353 RVA: 0x0003BFAB File Offset: 0x0003A1AB
		[XmlElement(ElementName = "SyncProtocol")]
		public int SyncProtocolInt
		{
			get
			{
				return (int)base.SyncProtocol;
			}
			set
			{
				base.SyncProtocol = (SyncProtocol)value;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x0003BFB4 File Offset: 0x0003A1B4
		// (set) Token: 0x06001CBB RID: 7355 RVA: 0x0003BFD5 File Offset: 0x0003A1D5
		[XmlElement(ElementName = "EmailAddress")]
		public string EmailAddressString
		{
			get
			{
				return base.EmailAddress.ToString();
			}
			set
			{
				base.EmailAddress = new SmtpAddress(value);
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x0003BFE4 File Offset: 0x0003A1E4
		// (set) Token: 0x06001CBD RID: 7357 RVA: 0x0003BFFF File Offset: 0x0003A1FF
		[XmlElement(ElementName = "IncrementalSyncInterval")]
		public long IncrementalSyncIntervalTicks
		{
			get
			{
				return base.IncrementalSyncInterval.Ticks;
			}
			set
			{
				base.IncrementalSyncInterval = new TimeSpan(value);
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x0003C00D File Offset: 0x0003A20D
		// (set) Token: 0x06001CBF RID: 7359 RVA: 0x0003C015 File Offset: 0x0003A215
		[XmlElement(ElementName = "SkippedItemCounts")]
		public new SkippedItemCounts SkippedItemCounts
		{
			get
			{
				return base.SkippedItemCounts;
			}
			set
			{
				base.SkippedItemCounts = value;
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x0003C01E File Offset: 0x0003A21E
		// (set) Token: 0x06001CC1 RID: 7361 RVA: 0x0003C026 File Offset: 0x0003A226
		[XmlElement(ElementName = "FailureHistory")]
		public new FailureHistory FailureHistory
		{
			get
			{
				return base.FailureHistory;
			}
			set
			{
				base.FailureHistory = value;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x0003C02F File Offset: 0x0003A22F
		// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x0003C037 File Offset: 0x0003A237
		[XmlAnyElement]
		public XmlElement[] UnknownElements
		{
			get
			{
				return this.unknownElements;
			}
			set
			{
				this.unknownElements = value;
			}
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0003C040 File Offset: 0x0003A240
		internal static MapiFolder GetRequestJobsFolder(MapiStore systemMbx)
		{
			return MapiUtils.OpenFolderUnderRoot(systemMbx, RequestJobXML.RequestJobsFolderName, true);
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0003C050 File Offset: 0x0003A250
		internal static string CreateMessageSubject(Guid guid)
		{
			ExDateTime utcNow = ExDateTime.UtcNow;
			return string.Format(CultureInfo.InvariantCulture, "Move Mailbox Work Item : {0} :: {1} :: {2} ", new object[]
			{
				guid.ToString(),
				utcNow.ToLongDateString(),
				utcNow.ToLongTimeString()
			});
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0003C09E File Offset: 0x0003A29E
		internal static byte[] CreateMessageSearchKey(Guid guid)
		{
			return guid.ToByteArray();
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0003C0A8 File Offset: 0x0003A2A8
		internal static bool IsMessageTypeSupported(MapiMessage message, MapiStore store)
		{
			RequestJobNamedPropertySet requestJobNamedPropertySet = RequestJobNamedPropertySet.Get(store);
			PropValue prop = message.GetProp(requestJobNamedPropertySet.PropTags[9]);
			MRSJobType value = MapiUtils.GetValue<MRSJobType>(prop, MRSJobType.RequestJobE14R3);
			return RequestJobXML.IsKnownJobType(value);
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0003C0DC File Offset: 0x0003A2DC
		internal static bool IsKnownJobType(MRSJobType jobType)
		{
			switch (jobType)
			{
			case MRSJobType.RequestJobE14R4_WithDurations:
			case MRSJobType.RequestJobE14R5_WithImportExportMerge:
			case MRSJobType.RequestJobE14R5_PrimaryOrArchiveExclusiveMoves:
			case MRSJobType.RequestJobE14R6_CompressedReports:
			case MRSJobType.RequestJobE15_TenantHint:
			case MRSJobType.RequestJobE15_AutoResume:
			case MRSJobType.RequestJobE15_SubType:
			case MRSJobType.RequestJobE15_AutoResumeMerges:
			case MRSJobType.RequestJobE15_CreatePublicFoldersUnderParentInSecondary:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0003C11C File Offset: 0x0003A31C
		internal static MRSCapabilities MapJobTypeToCapability(MRSJobType jobType)
		{
			switch (jobType)
			{
			case MRSJobType.Unknown:
				return MRSCapabilities.E14_RTM;
			case MRSJobType.RequestJobE14R4_WithDurations:
				return MRSCapabilities.E14_RTM;
			case MRSJobType.RequestJobE14R5_WithImportExportMerge:
			case MRSJobType.RequestJobE14R5_PrimaryOrArchiveExclusiveMoves:
			case MRSJobType.RequestJobE14R6_CompressedReports:
				return MRSCapabilities.Merges;
			case MRSJobType.RequestJobE15_TenantHint:
				return MRSCapabilities.TenantHint;
			case MRSJobType.RequestJobE15_AutoResume:
				return MRSCapabilities.AutoResume;
			case MRSJobType.RequestJobE15_SubType:
				return MRSCapabilities.SubType;
			case MRSJobType.RequestJobE15_AutoResumeMerges:
				return MRSCapabilities.AutoResumeMerges;
			case MRSJobType.RequestJobE15_CreatePublicFoldersUnderParentInSecondary:
				return MRSCapabilities.CreatePublicFoldersUnderParentInSecondary;
			}
			return MRSCapabilities.E14_RTM;
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0003C184 File Offset: 0x0003A384
		internal static bool IsKnownRequestType(MRSRequestType requestType)
		{
			switch (requestType)
			{
			case MRSRequestType.Move:
			case MRSRequestType.Merge:
			case MRSRequestType.MailboxImport:
			case MRSRequestType.MailboxExport:
			case MRSRequestType.MailboxRestore:
			case MRSRequestType.PublicFolderMove:
			case MRSRequestType.PublicFolderMigration:
			case MRSRequestType.Sync:
			case MRSRequestType.MailboxRelocation:
			case MRSRequestType.FolderMove:
			case MRSRequestType.PublicFolderMailboxMigration:
				return true;
			}
			return false;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0003C1D0 File Offset: 0x0003A3D0
		internal PropValue[] GetPropertiesWrittenOnRequestJob(MapiStore store)
		{
			RequestJobNamedPropertySet requestJobNamedPropertySet = RequestJobNamedPropertySet.Get(store);
			return requestJobNamedPropertySet.GetValuesFromRequestJob(this);
		}

		// Token: 0x04000C27 RID: 3111
		internal static readonly string RequestJobsFolderName = "MailboxReplicationService Move Jobs";

		// Token: 0x04000C28 RID: 3112
		internal static readonly string RequestJobsMessageClass = "IPM.MS-Exchange.MailboxMove";

		// Token: 0x04000C29 RID: 3113
		[NonSerialized]
		private XmlElement[] unknownElements;
	}
}
