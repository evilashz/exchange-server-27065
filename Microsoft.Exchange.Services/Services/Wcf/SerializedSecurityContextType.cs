using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DC6 RID: 3526
	[XmlRoot(ElementName = "SerializedSecurityContext", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class SerializedSecurityContextType
	{
		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x060059AA RID: 22954 RVA: 0x001183FA File Offset: 0x001165FA
		// (set) Token: 0x060059AB RID: 22955 RVA: 0x00118402 File Offset: 0x00116602
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

		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x060059AC RID: 22956 RVA: 0x0011840B File Offset: 0x0011660B
		// (set) Token: 0x060059AD RID: 22957 RVA: 0x00118413 File Offset: 0x00116613
		[XmlArray(Order = 1)]
		[XmlArrayItem("GroupIdentifier", IsNullable = false)]
		public SidAndAttributesType[] GroupSids
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

		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x060059AE RID: 22958 RVA: 0x0011841C File Offset: 0x0011661C
		// (set) Token: 0x060059AF RID: 22959 RVA: 0x00118424 File Offset: 0x00116624
		[XmlArray(Order = 2)]
		[XmlArrayItem("RestrictedGroupIdentifier", IsNullable = false)]
		public SidAndAttributesType[] RestrictedGroupSids
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

		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x060059B0 RID: 22960 RVA: 0x0011842D File Offset: 0x0011662D
		// (set) Token: 0x060059B1 RID: 22961 RVA: 0x00118435 File Offset: 0x00116635
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

		// Token: 0x060059B2 RID: 22962 RVA: 0x0011843E File Offset: 0x0011663E
		internal AuthZClientInfo ToAuthZClientInfo()
		{
			return AuthZClientInfo.FromSecurityAccessToken(this.ToSecurityAccessToken());
		}

		// Token: 0x060059B3 RID: 22963 RVA: 0x0011844C File Offset: 0x0011664C
		internal SerializedSecurityAccessToken ToSecurityAccessToken()
		{
			return new SerializedSecurityAccessToken
			{
				UserSid = this.UserSid,
				GroupSids = SerializedSecurityContextType.ToSidStringAndAttributesArray(this.GroupSids),
				RestrictedGroupSids = SerializedSecurityContextType.ToSidStringAndAttributesArray(this.RestrictedGroupSids),
				SmtpAddress = this.PrimarySmtpAddress
			};
		}

		// Token: 0x060059B4 RID: 22964 RVA: 0x0011849C File Offset: 0x0011669C
		private static SidStringAndAttributes[] ToSidStringAndAttributesArray(SidAndAttributesType[] sidAndAttributes)
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

		// Token: 0x040031A5 RID: 12709
		private string userSidField;

		// Token: 0x040031A6 RID: 12710
		private SidAndAttributesType[] groupSidsField;

		// Token: 0x040031A7 RID: 12711
		private SidAndAttributesType[] restrictedGroupSidsField;

		// Token: 0x040031A8 RID: 12712
		private string primarySmtpAddressField;
	}
}
