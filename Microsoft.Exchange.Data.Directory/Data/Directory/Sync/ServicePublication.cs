using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000966 RID: 2406
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class ServicePublication
	{
		// Token: 0x17002824 RID: 10276
		// (get) Token: 0x060070C3 RID: 28867 RVA: 0x0017777D File Offset: 0x0017597D
		// (set) Token: 0x060070C4 RID: 28868 RVA: 0x00177785 File Offset: 0x00175985
		[XmlElement(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11", Order = 0)]
		public DirectoryPropertyStringSingleLength1To454 CloudSipProxyAddress
		{
			get
			{
				return this.cloudSipProxyAddressField;
			}
			set
			{
				this.cloudSipProxyAddressField = value;
			}
		}

		// Token: 0x17002825 RID: 10277
		// (get) Token: 0x060070C5 RID: 28869 RVA: 0x0017778E File Offset: 0x0017598E
		// (set) Token: 0x060070C6 RID: 28870 RVA: 0x00177796 File Offset: 0x00175996
		[XmlElement(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11", Order = 1)]
		public DirectoryPropertyXmlProvisionedPlan ProvisionedPlan
		{
			get
			{
				return this.provisionedPlanField;
			}
			set
			{
				this.provisionedPlanField = value;
			}
		}

		// Token: 0x17002826 RID: 10278
		// (get) Token: 0x060070C7 RID: 28871 RVA: 0x0017779F File Offset: 0x0017599F
		// (set) Token: 0x060070C8 RID: 28872 RVA: 0x001777A7 File Offset: 0x001759A7
		[XmlElement(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11", Order = 2)]
		public DirectoryPropertyXmlServiceInfo ServiceInfo
		{
			get
			{
				return this.serviceInfoField;
			}
			set
			{
				this.serviceInfoField = value;
			}
		}

		// Token: 0x17002827 RID: 10279
		// (get) Token: 0x060070C9 RID: 28873 RVA: 0x001777B0 File Offset: 0x001759B0
		// (set) Token: 0x060070CA RID: 28874 RVA: 0x001777B8 File Offset: 0x001759B8
		[XmlElement(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11", Order = 3)]
		public DirectoryPropertyXmlValidationError ValidationError
		{
			get
			{
				return this.validationErrorField;
			}
			set
			{
				this.validationErrorField = value;
			}
		}

		// Token: 0x17002828 RID: 10280
		// (get) Token: 0x060070CB RID: 28875 RVA: 0x001777C1 File Offset: 0x001759C1
		// (set) Token: 0x060070CC RID: 28876 RVA: 0x001777C9 File Offset: 0x001759C9
		[XmlAttribute]
		public string ContextId
		{
			get
			{
				return this.contextIdField;
			}
			set
			{
				this.contextIdField = value;
			}
		}

		// Token: 0x17002829 RID: 10281
		// (get) Token: 0x060070CD RID: 28877 RVA: 0x001777D2 File Offset: 0x001759D2
		// (set) Token: 0x060070CE RID: 28878 RVA: 0x001777DA File Offset: 0x001759DA
		[XmlAttribute]
		public DirectoryObjectClassCapabilityTarget ObjectClass
		{
			get
			{
				return this.objectClassField;
			}
			set
			{
				this.objectClassField = value;
			}
		}

		// Token: 0x1700282A RID: 10282
		// (get) Token: 0x060070CF RID: 28879 RVA: 0x001777E3 File Offset: 0x001759E3
		// (set) Token: 0x060070D0 RID: 28880 RVA: 0x001777EB File Offset: 0x001759EB
		[XmlAttribute]
		public string ObjectId
		{
			get
			{
				return this.objectIdField;
			}
			set
			{
				this.objectIdField = value;
			}
		}

		// Token: 0x1700282B RID: 10283
		// (get) Token: 0x060070D1 RID: 28881 RVA: 0x001777F4 File Offset: 0x001759F4
		// (set) Token: 0x060070D2 RID: 28882 RVA: 0x001777FC File Offset: 0x001759FC
		[XmlIgnore]
		public Guid BatchId { get; set; }

		// Token: 0x1700282C RID: 10284
		// (get) Token: 0x060070D3 RID: 28883 RVA: 0x00177805 File Offset: 0x00175A05
		// (set) Token: 0x060070D4 RID: 28884 RVA: 0x0017780D File Offset: 0x00175A0D
		[XmlIgnore]
		public SyncObjectId SyncObjectId { get; set; }

		// Token: 0x0400492D RID: 18733
		private DirectoryPropertyStringSingleLength1To454 cloudSipProxyAddressField;

		// Token: 0x0400492E RID: 18734
		private DirectoryPropertyXmlProvisionedPlan provisionedPlanField;

		// Token: 0x0400492F RID: 18735
		private DirectoryPropertyXmlServiceInfo serviceInfoField;

		// Token: 0x04004930 RID: 18736
		private DirectoryPropertyXmlValidationError validationErrorField;

		// Token: 0x04004931 RID: 18737
		private string contextIdField;

		// Token: 0x04004932 RID: 18738
		private DirectoryObjectClassCapabilityTarget objectClassField;

		// Token: 0x04004933 RID: 18739
		private string objectIdField;
	}
}
