using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000634 RID: 1588
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class RightsManagementLicenseDataType
	{
		// Token: 0x06003179 RID: 12665 RVA: 0x000B717C File Offset: 0x000B537C
		public RightsManagementLicenseDataType()
		{
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x000B7184 File Offset: 0x000B5384
		internal RightsManagementLicenseDataType(ContentRight usageRights)
		{
			this.EditAllowed = usageRights.IsUsageRightGranted(ContentRight.Edit);
			this.ReplyAllowed = usageRights.IsUsageRightGranted(ContentRight.Reply);
			this.ReplyAllAllowed = usageRights.IsUsageRightGranted(ContentRight.ReplyAll);
			this.ForwardAllowed = usageRights.IsUsageRightGranted(ContentRight.Forward);
			this.PrintAllowed = usageRights.IsUsageRightGranted(ContentRight.Print);
			this.ExtractAllowed = usageRights.IsUsageRightGranted(ContentRight.Extract);
			this.ProgrammaticAccessAllowed = usageRights.IsUsageRightGranted(ContentRight.ObjectModel);
			this.IsOwner = usageRights.IsUsageRightGranted(ContentRight.Owner);
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x0600317B RID: 12667 RVA: 0x000B720D File Offset: 0x000B540D
		// (set) Token: 0x0600317C RID: 12668 RVA: 0x000B7215 File Offset: 0x000B5415
		[DataMember(EmitDefaultValue = false)]
		public int RightsManagedMessageDecryptionStatus { get; set; }

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x0600317D RID: 12669 RVA: 0x000B721E File Offset: 0x000B541E
		// (set) Token: 0x0600317E RID: 12670 RVA: 0x000B7226 File Offset: 0x000B5426
		[DataMember(EmitDefaultValue = false)]
		public string RmsTemplateId { get; set; }

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x000B722F File Offset: 0x000B542F
		// (set) Token: 0x06003180 RID: 12672 RVA: 0x000B7237 File Offset: 0x000B5437
		[DataMember(EmitDefaultValue = false)]
		public string TemplateName { get; set; }

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06003181 RID: 12673 RVA: 0x000B7240 File Offset: 0x000B5440
		// (set) Token: 0x06003182 RID: 12674 RVA: 0x000B7248 File Offset: 0x000B5448
		[DataMember(EmitDefaultValue = false)]
		public string TemplateDescription { get; set; }

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06003183 RID: 12675 RVA: 0x000B7251 File Offset: 0x000B5451
		// (set) Token: 0x06003184 RID: 12676 RVA: 0x000B7259 File Offset: 0x000B5459
		[DataMember(EmitDefaultValue = false)]
		public bool EditAllowed { get; set; }

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06003185 RID: 12677 RVA: 0x000B7262 File Offset: 0x000B5462
		// (set) Token: 0x06003186 RID: 12678 RVA: 0x000B726A File Offset: 0x000B546A
		[DataMember(EmitDefaultValue = false)]
		public bool ReplyAllowed { get; set; }

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06003187 RID: 12679 RVA: 0x000B7273 File Offset: 0x000B5473
		// (set) Token: 0x06003188 RID: 12680 RVA: 0x000B727B File Offset: 0x000B547B
		[DataMember(EmitDefaultValue = false)]
		public bool ReplyAllAllowed { get; set; }

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06003189 RID: 12681 RVA: 0x000B7284 File Offset: 0x000B5484
		// (set) Token: 0x0600318A RID: 12682 RVA: 0x000B728C File Offset: 0x000B548C
		[DataMember(EmitDefaultValue = false)]
		public bool ForwardAllowed { get; set; }

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x0600318B RID: 12683 RVA: 0x000B7295 File Offset: 0x000B5495
		// (set) Token: 0x0600318C RID: 12684 RVA: 0x000B729D File Offset: 0x000B549D
		[DataMember(EmitDefaultValue = false)]
		public bool ModifyRecipientsAllowed { get; set; }

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x0600318D RID: 12685 RVA: 0x000B72A6 File Offset: 0x000B54A6
		// (set) Token: 0x0600318E RID: 12686 RVA: 0x000B72AE File Offset: 0x000B54AE
		[DataMember(EmitDefaultValue = false)]
		public bool ExtractAllowed { get; set; }

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x0600318F RID: 12687 RVA: 0x000B72B7 File Offset: 0x000B54B7
		// (set) Token: 0x06003190 RID: 12688 RVA: 0x000B72BF File Offset: 0x000B54BF
		[DataMember(EmitDefaultValue = false)]
		public bool PrintAllowed { get; set; }

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06003191 RID: 12689 RVA: 0x000B72C8 File Offset: 0x000B54C8
		// (set) Token: 0x06003192 RID: 12690 RVA: 0x000B72D0 File Offset: 0x000B54D0
		[DataMember(EmitDefaultValue = false)]
		public bool ExportAllowed { get; set; }

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06003193 RID: 12691 RVA: 0x000B72D9 File Offset: 0x000B54D9
		// (set) Token: 0x06003194 RID: 12692 RVA: 0x000B72E1 File Offset: 0x000B54E1
		[DataMember(EmitDefaultValue = false)]
		public bool ProgrammaticAccessAllowed { get; set; }

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06003195 RID: 12693 RVA: 0x000B72EA File Offset: 0x000B54EA
		// (set) Token: 0x06003196 RID: 12694 RVA: 0x000B72F2 File Offset: 0x000B54F2
		[DataMember(EmitDefaultValue = false)]
		public bool IsOwner { get; set; }

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06003197 RID: 12695 RVA: 0x000B72FB File Offset: 0x000B54FB
		// (set) Token: 0x06003198 RID: 12696 RVA: 0x000B7303 File Offset: 0x000B5503
		[DataMember(EmitDefaultValue = false)]
		public string ContentOwner { get; set; }

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06003199 RID: 12697 RVA: 0x000B730C File Offset: 0x000B550C
		// (set) Token: 0x0600319A RID: 12698 RVA: 0x000B7314 File Offset: 0x000B5514
		[DateTimeString]
		[DataMember(EmitDefaultValue = false)]
		public string ContentExpiryDate { get; set; }

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x000B731D File Offset: 0x000B551D
		// (set) Token: 0x0600319C RID: 12700 RVA: 0x000B7325 File Offset: 0x000B5525
		[IgnoreDataMember]
		[XmlElement("BodyType")]
		public BodyType BodyType { get; set; }

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x000B732E File Offset: 0x000B552E
		// (set) Token: 0x0600319E RID: 12702 RVA: 0x000B733B File Offset: 0x000B553B
		[XmlIgnore]
		[DataMember(Name = "BodyType", EmitDefaultValue = false)]
		public string BodyTypeString
		{
			get
			{
				return EnumUtilities.ToString<BodyType>(this.BodyType);
			}
			set
			{
				this.BodyType = EnumUtilities.Parse<BodyType>(value);
			}
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x000B734C File Offset: 0x000B554C
		internal static RightsManagementLicenseDataType CreateNoRightsTemplate()
		{
			return new RightsManagementLicenseDataType
			{
				RmsTemplateId = Guid.Empty.ToString()
			};
		}
	}
}
