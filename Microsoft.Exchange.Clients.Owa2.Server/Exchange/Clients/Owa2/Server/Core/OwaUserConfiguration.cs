using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003F1 RID: 1009
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class OwaUserConfiguration
	{
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x00078D82 File Offset: 0x00076F82
		// (set) Token: 0x06002075 RID: 8309 RVA: 0x00078D8A File Offset: 0x00076F8A
		[DataMember]
		public UserOptionsType UserOptions
		{
			get
			{
				return this.userOptions;
			}
			set
			{
				this.userOptions = value;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x00078D93 File Offset: 0x00076F93
		// (set) Token: 0x06002077 RID: 8311 RVA: 0x00078D9B File Offset: 0x00076F9B
		[DataMember]
		public SessionSettingsType SessionSettings
		{
			get
			{
				return this.sessionSettings;
			}
			set
			{
				this.sessionSettings = value;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x00078DA4 File Offset: 0x00076FA4
		// (set) Token: 0x06002079 RID: 8313 RVA: 0x00078DAC File Offset: 0x00076FAC
		[DataMember]
		public string InlineExploreContent
		{
			get
			{
				return this.inlineExploreContent;
			}
			set
			{
				this.inlineExploreContent = value;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x00078DB5 File Offset: 0x00076FB5
		// (set) Token: 0x0600207B RID: 8315 RVA: 0x00078DBD File Offset: 0x00076FBD
		[DataMember]
		public SegmentationSettingsType SegmentationSettings
		{
			get
			{
				return this.segmentationSettings;
			}
			set
			{
				this.segmentationSettings = value;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x00078DC6 File Offset: 0x00076FC6
		// (set) Token: 0x0600207D RID: 8317 RVA: 0x00078DCE File Offset: 0x00076FCE
		[DataMember]
		public AttachmentPolicyType AttachmentPolicy
		{
			get
			{
				return this.attachmentPolicy;
			}
			set
			{
				this.attachmentPolicy = value;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x00078DD7 File Offset: 0x00076FD7
		// (set) Token: 0x0600207F RID: 8319 RVA: 0x00078DDF File Offset: 0x00076FDF
		[DataMember]
		public PolicySettingsType PolicySettings
		{
			get
			{
				return this.policySettings;
			}
			set
			{
				this.policySettings = value;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06002080 RID: 8320 RVA: 0x00078DE8 File Offset: 0x00076FE8
		// (set) Token: 0x06002081 RID: 8321 RVA: 0x00078DF0 File Offset: 0x00076FF0
		[DataMember]
		public MobileDevicePolicySettingsType MobileDevicePolicySettings
		{
			get
			{
				return this.mobileDevicePolicySettings;
			}
			set
			{
				this.mobileDevicePolicySettings = value;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06002082 RID: 8322 RVA: 0x00078DF9 File Offset: 0x00076FF9
		// (set) Token: 0x06002083 RID: 8323 RVA: 0x00078E01 File Offset: 0x00077001
		[DataMember]
		public ApplicationSettingsType ApplicationSettings
		{
			get
			{
				return this.applicationSettings;
			}
			set
			{
				this.applicationSettings = value;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06002084 RID: 8324 RVA: 0x00078E0A File Offset: 0x0007700A
		// (set) Token: 0x06002085 RID: 8325 RVA: 0x00078E12 File Offset: 0x00077012
		[DataMember]
		public OwaViewStateConfiguration ViewStateConfiguration
		{
			get
			{
				return this.viewStateConfiguration;
			}
			set
			{
				this.viewStateConfiguration = value;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x00078E1B File Offset: 0x0007701B
		// (set) Token: 0x06002087 RID: 8327 RVA: 0x00078E23 File Offset: 0x00077023
		[DataMember]
		public MasterCategoryListType MasterCategoryList
		{
			get
			{
				return this.masterCategoryList;
			}
			set
			{
				this.masterCategoryList = value;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x00078E2C File Offset: 0x0007702C
		// (set) Token: 0x06002089 RID: 8329 RVA: 0x00078E34 File Offset: 0x00077034
		[DataMember]
		public SmimeAdminSettingsType SmimeAdminSettings
		{
			get
			{
				return this.smimeAdminSettings;
			}
			set
			{
				this.smimeAdminSettings = value;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x0600208A RID: 8330 RVA: 0x00078E3D File Offset: 0x0007703D
		// (set) Token: 0x0600208B RID: 8331 RVA: 0x00078E45 File Offset: 0x00077045
		[DataMember]
		public uint MailTipsLargeAudienceThreshold { get; set; }

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x0600208C RID: 8332 RVA: 0x00078E4E File Offset: 0x0007704E
		// (set) Token: 0x0600208D RID: 8333 RVA: 0x00078E56 File Offset: 0x00077056
		[DataMember]
		public RetentionPolicyTag[] RetentionPolicyTags { get; set; }

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x00078E5F File Offset: 0x0007705F
		// (set) Token: 0x0600208F RID: 8335 RVA: 0x00078E67 File Offset: 0x00077067
		[DataMember]
		public int MaxRecipientsPerMessage { get; set; }

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002090 RID: 8336 RVA: 0x00078E70 File Offset: 0x00077070
		// (set) Token: 0x06002091 RID: 8337 RVA: 0x00078E78 File Offset: 0x00077078
		[DataMember]
		public bool RecoverDeletedItemsEnabled { get; set; }

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002092 RID: 8338 RVA: 0x00078E81 File Offset: 0x00077081
		// (set) Token: 0x06002093 RID: 8339 RVA: 0x00078E89 File Offset: 0x00077089
		[DataMember]
		public string[] FlightConfiguration { get; set; }

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002094 RID: 8340 RVA: 0x00078E92 File Offset: 0x00077092
		// (set) Token: 0x06002095 RID: 8341 RVA: 0x00078E9A File Offset: 0x0007709A
		[DataMember]
		public bool PublicComputersDetectionEnabled { get; set; }

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002096 RID: 8342 RVA: 0x00078EA3 File Offset: 0x000770A3
		// (set) Token: 0x06002097 RID: 8343 RVA: 0x00078EAB File Offset: 0x000770AB
		[DataMember]
		public bool PolicyTipsEnabled { get; set; }

		// Token: 0x04001260 RID: 4704
		private UserOptionsType userOptions;

		// Token: 0x04001261 RID: 4705
		private SessionSettingsType sessionSettings;

		// Token: 0x04001262 RID: 4706
		private SegmentationSettingsType segmentationSettings;

		// Token: 0x04001263 RID: 4707
		private AttachmentPolicyType attachmentPolicy;

		// Token: 0x04001264 RID: 4708
		private PolicySettingsType policySettings;

		// Token: 0x04001265 RID: 4709
		private MobileDevicePolicySettingsType mobileDevicePolicySettings;

		// Token: 0x04001266 RID: 4710
		private ApplicationSettingsType applicationSettings;

		// Token: 0x04001267 RID: 4711
		private OwaViewStateConfiguration viewStateConfiguration;

		// Token: 0x04001268 RID: 4712
		private MasterCategoryListType masterCategoryList;

		// Token: 0x04001269 RID: 4713
		private string inlineExploreContent;

		// Token: 0x0400126A RID: 4714
		private SmimeAdminSettingsType smimeAdminSettings;
	}
}
