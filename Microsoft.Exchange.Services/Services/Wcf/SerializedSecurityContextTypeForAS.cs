using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DC7 RID: 3527
	[XmlRoot(ElementName = "SerializedSecurityContext", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SerializedSecurityContextTypeForAS
	{
		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x060059B6 RID: 22966 RVA: 0x001184EB File Offset: 0x001166EB
		// (set) Token: 0x060059B7 RID: 22967 RVA: 0x001184F3 File Offset: 0x001166F3
		[XmlElement(Order = 0)]
		public string UserSid
		{
			get
			{
				return this.userSidField;
			}
			set
			{
				this.userSidField = value;
			}
		}

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x060059B8 RID: 22968 RVA: 0x001184FC File Offset: 0x001166FC
		// (set) Token: 0x060059B9 RID: 22969 RVA: 0x00118504 File Offset: 0x00116704
		[XmlArrayItem("GroupIdentifier", IsNullable = false)]
		[XmlArray(Order = 1)]
		public SidAndAttributesTypeForAS[] GroupSids
		{
			get
			{
				return this.groupSidsField;
			}
			set
			{
				this.groupSidsField = value;
			}
		}

		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x060059BA RID: 22970 RVA: 0x0011850D File Offset: 0x0011670D
		// (set) Token: 0x060059BB RID: 22971 RVA: 0x00118515 File Offset: 0x00116715
		[XmlArrayItem("RestrictedGroupIdentifier", IsNullable = false)]
		[XmlArray(Order = 2)]
		public SidAndAttributesTypeForAS[] RestrictedGroupSids
		{
			get
			{
				return this.restrictedGroupSidsField;
			}
			set
			{
				this.restrictedGroupSidsField = value;
			}
		}

		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x060059BC RID: 22972 RVA: 0x0011851E File Offset: 0x0011671E
		// (set) Token: 0x060059BD RID: 22973 RVA: 0x00118526 File Offset: 0x00116726
		[XmlElement(Order = 3)]
		public string PrimarySmtpAddress
		{
			get
			{
				return this.primarySmtpAddressField;
			}
			set
			{
				this.primarySmtpAddressField = value;
			}
		}

		// Token: 0x060059BE RID: 22974 RVA: 0x0011852F File Offset: 0x0011672F
		internal AuthZClientInfo ToAuthZClientInfo()
		{
			return AuthZClientInfo.FromSecurityAccessToken(this.ToSecurityAccessToken());
		}

		// Token: 0x060059BF RID: 22975 RVA: 0x0011853C File Offset: 0x0011673C
		internal SerializedSecurityAccessToken ToSecurityAccessToken()
		{
			return new SerializedSecurityAccessToken
			{
				UserSid = this.UserSid,
				GroupSids = SerializedSecurityContextTypeForAS.ToSidStringAndAttributesArray(this.GroupSids),
				RestrictedGroupSids = SerializedSecurityContextTypeForAS.ToSidStringAndAttributesArray(this.RestrictedGroupSids),
				SmtpAddress = this.PrimarySmtpAddress
			};
		}

		// Token: 0x060059C0 RID: 22976 RVA: 0x0011858C File Offset: 0x0011678C
		private static SidStringAndAttributes[] ToSidStringAndAttributesArray(SidAndAttributesTypeForAS[] sidAndAttributes)
		{
			if (sidAndAttributes == null || sidAndAttributes.Length == 0)
			{
				return null;
			}
			SidStringAndAttributes[] array = new SidStringAndAttributes[sidAndAttributes.Length];
			for (int i = 0; i < sidAndAttributes.Length; i++)
			{
				array[i] = new SidStringAndAttributes(sidAndAttributes[i].SecurityIdentifier, sidAndAttributes[i].Attributes);
			}
			return array;
		}

		// Token: 0x040031A9 RID: 12713
		private string userSidField;

		// Token: 0x040031AA RID: 12714
		private SidAndAttributesTypeForAS[] groupSidsField;

		// Token: 0x040031AB RID: 12715
		private SidAndAttributesTypeForAS[] restrictedGroupSidsField;

		// Token: 0x040031AC RID: 12716
		private string primarySmtpAddressField;
	}
}
