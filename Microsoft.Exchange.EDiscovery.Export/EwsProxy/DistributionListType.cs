using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200018A RID: 394
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class DistributionListType : ItemType
	{
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x00023FCE File Offset: 0x000221CE
		// (set) Token: 0x06001111 RID: 4369 RVA: 0x00023FD6 File Offset: 0x000221D6
		public string DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00023FDF File Offset: 0x000221DF
		// (set) Token: 0x06001113 RID: 4371 RVA: 0x00023FE7 File Offset: 0x000221E7
		public string FileAs
		{
			get
			{
				return this.fileAsField;
			}
			set
			{
				this.fileAsField = value;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x00023FF0 File Offset: 0x000221F0
		// (set) Token: 0x06001115 RID: 4373 RVA: 0x00023FF8 File Offset: 0x000221F8
		public ContactSourceType ContactSource
		{
			get
			{
				return this.contactSourceField;
			}
			set
			{
				this.contactSourceField = value;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x00024001 File Offset: 0x00022201
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x00024009 File Offset: 0x00022209
		[XmlIgnore]
		public bool ContactSourceSpecified
		{
			get
			{
				return this.contactSourceFieldSpecified;
			}
			set
			{
				this.contactSourceFieldSpecified = value;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00024012 File Offset: 0x00022212
		// (set) Token: 0x06001119 RID: 4377 RVA: 0x0002401A File Offset: 0x0002221A
		[XmlArrayItem("Member", IsNullable = false)]
		public MemberType[] Members
		{
			get
			{
				return this.membersField;
			}
			set
			{
				this.membersField = value;
			}
		}

		// Token: 0x04000BC3 RID: 3011
		private string displayNameField;

		// Token: 0x04000BC4 RID: 3012
		private string fileAsField;

		// Token: 0x04000BC5 RID: 3013
		private ContactSourceType contactSourceField;

		// Token: 0x04000BC6 RID: 3014
		private bool contactSourceFieldSpecified;

		// Token: 0x04000BC7 RID: 3015
		private MemberType[] membersField;
	}
}
