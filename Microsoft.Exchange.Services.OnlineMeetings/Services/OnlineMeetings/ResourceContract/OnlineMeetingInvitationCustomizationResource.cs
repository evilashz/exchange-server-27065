using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000069 RID: 105
	[Parent("user")]
	[Get(typeof(OnlineMeetingInvitationCustomizationResource))]
	[DataContract(Name = "OnlineMeetingInvitationCustomizationResource")]
	internal class OnlineMeetingInvitationCustomizationResource : OnlineMeetingCapabilityResource
	{
		// Token: 0x060002FC RID: 764 RVA: 0x000096DB File Offset: 0x000078DB
		public OnlineMeetingInvitationCustomizationResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000096E4 File Offset: 0x000078E4
		// (set) Token: 0x060002FE RID: 766 RVA: 0x000096F1 File Offset: 0x000078F1
		[DataMember(Name = "enterpriseHelpUrl", EmitDefaultValue = false)]
		public string EnterpriseHelpUrl
		{
			get
			{
				return base.GetValue<string>("enterpriseHelpUrl");
			}
			set
			{
				base.SetValue<string>("enterpriseHelpUrl", value);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002FF RID: 767 RVA: 0x000096FF File Offset: 0x000078FF
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000970C File Offset: 0x0000790C
		[DataMember(Name = "invitationFooterText", EmitDefaultValue = false)]
		public string InvitationFooterText
		{
			get
			{
				return base.GetValue<string>("invitationFooterText");
			}
			set
			{
				base.SetValue<string>("invitationFooterText", value);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000971A File Offset: 0x0000791A
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00009727 File Offset: 0x00007927
		[DataMember(Name = "invitationHelpUrl", EmitDefaultValue = false)]
		public string InvitationHelpUrl
		{
			get
			{
				return base.GetValue<string>("invitationHelpUrl");
			}
			set
			{
				base.SetValue<string>("invitationHelpUrl", value);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00009735 File Offset: 0x00007935
		// (set) Token: 0x06000304 RID: 772 RVA: 0x00009742 File Offset: 0x00007942
		[DataMember(Name = "invitationLegalUrl", EmitDefaultValue = false)]
		public string InvitationLegalUrl
		{
			get
			{
				return base.GetValue<string>("invitationLegalUrl");
			}
			set
			{
				base.SetValue<string>("invitationLegalUrl", value);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00009750 File Offset: 0x00007950
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000975D File Offset: 0x0000795D
		[DataMember(Name = "invitationLogoUrl", EmitDefaultValue = false)]
		public string InvitationLogoUrl
		{
			get
			{
				return base.GetValue<string>("invitationLogoUrl");
			}
			set
			{
				base.SetValue<string>("invitationLogoUrl", value);
			}
		}

		// Token: 0x040001E5 RID: 485
		public const string Token = "onlineMeetingInvitationCustomization";

		// Token: 0x040001E6 RID: 486
		private const string EnterpriseHelpUrlPropertyName = "enterpriseHelpUrl";

		// Token: 0x040001E7 RID: 487
		private const string InvitationFooterTextPropertyName = "invitationFooterText";

		// Token: 0x040001E8 RID: 488
		private const string InvitationHelpUrlPropertyName = "invitationHelpUrl";

		// Token: 0x040001E9 RID: 489
		private const string InvitationLegalUrlPropertyName = "invitationLegalUrl";

		// Token: 0x040001EA RID: 490
		private const string InvitationLogoUrlPropertyName = "invitationLogoUrl";
	}
}
