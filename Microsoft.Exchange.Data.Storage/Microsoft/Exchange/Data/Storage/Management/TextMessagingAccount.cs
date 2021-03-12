using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.VersionedXml;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A8A RID: 2698
	[Serializable]
	public sealed class TextMessagingAccount : VersionedXmlConfigurationObject
	{
		// Token: 0x17001B5B RID: 7003
		// (get) Token: 0x060062BB RID: 25275 RVA: 0x001A0EEB File Offset: 0x0019F0EB
		internal override XsoMailboxConfigurationObjectSchema Schema
		{
			get
			{
				return TextMessagingAccount.schema;
			}
		}

		// Token: 0x060062BC RID: 25276 RVA: 0x001A0EF2 File Offset: 0x0019F0F2
		public TextMessagingAccount()
		{
			((SimplePropertyBag)this.propertyBag).SetObjectIdentityPropertyDefinition(VersionedXmlConfigurationObjectSchema.Identity);
		}

		// Token: 0x17001B5C RID: 7004
		// (get) Token: 0x060062BD RID: 25277 RVA: 0x001A0F0F File Offset: 0x0019F10F
		public override ObjectId Identity
		{
			get
			{
				return (ADObjectId)this[VersionedXmlConfigurationObjectSchema.Identity];
			}
		}

		// Token: 0x17001B5D RID: 7005
		// (get) Token: 0x060062BE RID: 25278 RVA: 0x001A0F21 File Offset: 0x0019F121
		// (set) Token: 0x060062BF RID: 25279 RVA: 0x001A0F33 File Offset: 0x0019F133
		[Parameter]
		public RegionInfo CountryRegionId
		{
			get
			{
				return (RegionInfo)this[TextMessagingAccountSchema.CountryRegionId];
			}
			set
			{
				this[TextMessagingAccountSchema.CountryRegionId] = value;
			}
		}

		// Token: 0x17001B5E RID: 7006
		// (get) Token: 0x060062C0 RID: 25280 RVA: 0x001A0F41 File Offset: 0x0019F141
		// (set) Token: 0x060062C1 RID: 25281 RVA: 0x001A0F53 File Offset: 0x0019F153
		[Parameter]
		public int MobileOperatorId
		{
			get
			{
				return (int)this[TextMessagingAccountSchema.MobileOperatorId];
			}
			set
			{
				this[TextMessagingAccountSchema.MobileOperatorId] = value;
			}
		}

		// Token: 0x17001B5F RID: 7007
		// (get) Token: 0x060062C2 RID: 25282 RVA: 0x001A0F66 File Offset: 0x0019F166
		// (set) Token: 0x060062C3 RID: 25283 RVA: 0x001A0F78 File Offset: 0x0019F178
		[Parameter]
		public E164Number NotificationPhoneNumber
		{
			get
			{
				return (E164Number)this[TextMessagingAccountSchema.NotificationPhoneNumber];
			}
			set
			{
				this[TextMessagingAccountSchema.NotificationPhoneNumber] = value;
			}
		}

		// Token: 0x17001B60 RID: 7008
		// (get) Token: 0x060062C4 RID: 25284 RVA: 0x001A0F86 File Offset: 0x0019F186
		public bool NotificationPhoneNumberVerified
		{
			get
			{
				return (bool)this[TextMessagingAccountSchema.NotificationPhoneNumberVerified];
			}
		}

		// Token: 0x17001B61 RID: 7009
		// (get) Token: 0x060062C5 RID: 25285 RVA: 0x001A0F98 File Offset: 0x0019F198
		public bool EasEnabled
		{
			get
			{
				return (bool)this[TextMessagingAccountSchema.EasEnabled];
			}
		}

		// Token: 0x17001B62 RID: 7010
		// (get) Token: 0x060062C6 RID: 25286 RVA: 0x001A0FAA File Offset: 0x0019F1AA
		public E164Number EasPhoneNumber
		{
			get
			{
				return (E164Number)this[TextMessagingAccountSchema.EasPhoneNumber];
			}
		}

		// Token: 0x17001B63 RID: 7011
		// (get) Token: 0x060062C7 RID: 25287 RVA: 0x001A0FBC File Offset: 0x0019F1BC
		public string EasDeviceProtocol
		{
			get
			{
				return (string)this[TextMessagingAccountSchema.EasDeviceProtocol];
			}
		}

		// Token: 0x17001B64 RID: 7012
		// (get) Token: 0x060062C8 RID: 25288 RVA: 0x001A0FCE File Offset: 0x0019F1CE
		public string EasDeviceType
		{
			get
			{
				return (string)this[TextMessagingAccountSchema.EasDeviceType];
			}
		}

		// Token: 0x17001B65 RID: 7013
		// (get) Token: 0x060062C9 RID: 25289 RVA: 0x001A0FE0 File Offset: 0x0019F1E0
		public string EasDeviceId
		{
			get
			{
				return (string)this[TextMessagingAccountSchema.EasDeviceId];
			}
		}

		// Token: 0x17001B66 RID: 7014
		// (get) Token: 0x060062CA RID: 25290 RVA: 0x001A0FF2 File Offset: 0x0019F1F2
		public string EasDeviceName
		{
			get
			{
				return (string)this[TextMessagingAccountSchema.EasDeviceName];
			}
		}

		// Token: 0x17001B67 RID: 7015
		// (get) Token: 0x060062CB RID: 25291 RVA: 0x001A1004 File Offset: 0x0019F204
		// (set) Token: 0x060062CC RID: 25292 RVA: 0x001A1016 File Offset: 0x0019F216
		internal TextMessagingSettingsVersion1Point0 TextMessagingSettings
		{
			get
			{
				return (TextMessagingSettingsVersion1Point0)this[TextMessagingAccountSchema.TextMessagingSettings];
			}
			set
			{
				this[TextMessagingAccountSchema.TextMessagingSettings] = value;
			}
		}

		// Token: 0x060062CD RID: 25293 RVA: 0x001A1024 File Offset: 0x0019F224
		internal static object TextMessagingSettingsGetter(IPropertyBag propertyBag)
		{
			if (propertyBag[TextMessagingAccountSchema.RawTextMessagingSettings] == null)
			{
				bool flag = ((PropertyBag)propertyBag).IsChanged(TextMessagingAccountSchema.RawTextMessagingSettings);
				propertyBag[TextMessagingAccountSchema.RawTextMessagingSettings] = new TextMessagingSettingsVersion1Point0(null, new DeliveryPoint[]
				{
					new DeliveryPoint(0, DeliveryPointType.ExchangeActiveSync, null, null, null, null, null, -1, -1),
					new DeliveryPoint(1, DeliveryPointType.SmtpToSmsGateway, null, null, null, null, null, -1, 1)
				});
				if (!flag)
				{
					((PropertyBag)propertyBag).ResetChangeTracking(TextMessagingAccountSchema.RawTextMessagingSettings);
				}
			}
			return (TextMessagingSettingsVersion1Point0)propertyBag[TextMessagingAccountSchema.RawTextMessagingSettings];
		}

		// Token: 0x060062CE RID: 25294 RVA: 0x001A10AD File Offset: 0x0019F2AD
		internal static void TextMessagingSettingsSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[TextMessagingAccountSchema.RawTextMessagingSettings] = CloneHelper.SerializeObj(value);
		}

		// Token: 0x060062CF RID: 25295 RVA: 0x001A10C0 File Offset: 0x0019F2C0
		internal static object CountryRegionIdGetter(IPropertyBag propertyBag)
		{
			TextMessagingSettingsVersion1Point0 textMessagingSettingsVersion1Point = (TextMessagingSettingsVersion1Point0)propertyBag[TextMessagingAccountSchema.TextMessagingSettings];
			IList<PossibleRecipient> effectivePossibleRecipients = textMessagingSettingsVersion1Point.MachineToPersonMessagingPolicies.EffectivePossibleRecipients;
			if (effectivePossibleRecipients.Count == 0)
			{
				return null;
			}
			string region = effectivePossibleRecipients[0].Region;
			if (!string.IsNullOrEmpty(region))
			{
				return new RegionInfo(region);
			}
			return null;
		}

		// Token: 0x060062D0 RID: 25296 RVA: 0x001A1114 File Offset: 0x0019F314
		internal static void CountryRegionIdSetter(object value, IPropertyBag propertyBag)
		{
			TextMessagingSettingsVersion1Point0 settings = (TextMessagingSettingsVersion1Point0)propertyBag[TextMessagingAccountSchema.TextMessagingSettings];
			string region = (value == null) ? null : ((RegionInfo)value).TwoLetterISORegionName;
			TextMessagingAccount.GetWritablePossibleRecipient(settings).Region = region;
		}

		// Token: 0x060062D1 RID: 25297 RVA: 0x001A1150 File Offset: 0x0019F350
		internal static object MobileOperatorIdGetter(IPropertyBag propertyBag)
		{
			TextMessagingSettingsVersion1Point0 textMessagingSettingsVersion1Point = (TextMessagingSettingsVersion1Point0)propertyBag[TextMessagingAccountSchema.TextMessagingSettings];
			IList<PossibleRecipient> effectivePossibleRecipients = textMessagingSettingsVersion1Point.MachineToPersonMessagingPolicies.EffectivePossibleRecipients;
			if (effectivePossibleRecipients.Count == 0)
			{
				return -1;
			}
			int num = -1;
			if (!int.TryParse(effectivePossibleRecipients[0].Carrier, out num))
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x060062D2 RID: 25298 RVA: 0x001A11A8 File Offset: 0x0019F3A8
		internal static void MobileOperatorIdSetter(object value, IPropertyBag propertyBag)
		{
			TextMessagingSettingsVersion1Point0 settings = (TextMessagingSettingsVersion1Point0)propertyBag[TextMessagingAccountSchema.TextMessagingSettings];
			string carrier = (-1 == (int)value) ? null : ((int)value).ToString("00000");
			TextMessagingAccount.GetWritablePossibleRecipient(settings).Carrier = carrier;
		}

		// Token: 0x060062D3 RID: 25299 RVA: 0x001A11F4 File Offset: 0x0019F3F4
		internal static object NotificationPhoneNumberGetter(IPropertyBag propertyBag)
		{
			TextMessagingSettingsVersion1Point0 textMessagingSettingsVersion1Point = (TextMessagingSettingsVersion1Point0)propertyBag[TextMessagingAccountSchema.TextMessagingSettings];
			IList<PossibleRecipient> effectivePossibleRecipients = textMessagingSettingsVersion1Point.MachineToPersonMessagingPolicies.EffectivePossibleRecipients;
			if (effectivePossibleRecipients.Count == 0)
			{
				return null;
			}
			return effectivePossibleRecipients[0].PhoneNumber;
		}

		// Token: 0x060062D4 RID: 25300 RVA: 0x001A1234 File Offset: 0x0019F434
		internal static void NotificationPhoneNumberSetter(object value, IPropertyBag propertyBag)
		{
			TextMessagingSettingsVersion1Point0 textMessagingSettingsVersion1Point = (TextMessagingSettingsVersion1Point0)propertyBag[TextMessagingAccountSchema.TextMessagingSettings];
			if (value == null)
			{
				PossibleRecipient.MarkEffective(textMessagingSettingsVersion1Point.MachineToPersonMessagingPolicies.PossibleRecipients, false);
				return;
			}
			bool flag = false;
			PossibleRecipient writablePossibleRecipient = TextMessagingAccount.GetWritablePossibleRecipient(textMessagingSettingsVersion1Point, out flag);
			if (flag)
			{
				writablePossibleRecipient.PhoneNumber = (E164Number)value;
			}
			else
			{
				PossibleRecipient mathed = PossibleRecipient.GetMathed(textMessagingSettingsVersion1Point.MachineToPersonMessagingPolicies.PossibleRecipients, (E164Number)value, false);
				PossibleRecipient.MarkEffective(textMessagingSettingsVersion1Point.MachineToPersonMessagingPolicies.PossibleRecipients, false);
				if (mathed == null)
				{
					textMessagingSettingsVersion1Point.MachineToPersonMessagingPolicies.PossibleRecipients.Add(new PossibleRecipient(true, DateTime.UtcNow, writablePossibleRecipient.Region, writablePossibleRecipient.Carrier, (E164Number)value, false, null, null, null));
				}
				else
				{
					mathed.Region = writablePossibleRecipient.Region;
					mathed.Carrier = writablePossibleRecipient.Carrier;
					mathed.MarkEffective(true);
				}
			}
			PossibleRecipient.PurgeNonEffectiveBefore(textMessagingSettingsVersion1Point.MachineToPersonMessagingPolicies.PossibleRecipients, DateTime.UtcNow - TimeSpan.FromDays(7.0), 10);
		}

		// Token: 0x060062D5 RID: 25301 RVA: 0x001A132C File Offset: 0x0019F52C
		internal static object NotificationPhoneNumberVerifiedGetter(IPropertyBag propertyBag)
		{
			TextMessagingSettingsVersion1Point0 textMessagingSettingsVersion1Point = (TextMessagingSettingsVersion1Point0)propertyBag[TextMessagingAccountSchema.TextMessagingSettings];
			IList<PossibleRecipient> effectivePossibleRecipients = textMessagingSettingsVersion1Point.MachineToPersonMessagingPolicies.EffectivePossibleRecipients;
			return effectivePossibleRecipients.Count != 0 && effectivePossibleRecipients[0].Acknowledged;
		}

		// Token: 0x060062D6 RID: 25302 RVA: 0x001A1374 File Offset: 0x0019F574
		internal static object EasEnabledGetter(IPropertyBag propertyBag)
		{
			TextMessagingSettingsVersion1Point0 textMessagingSettingsVersion1Point = (TextMessagingSettingsVersion1Point0)propertyBag[TextMessagingAccountSchema.TextMessagingSettings];
			return textMessagingSettingsVersion1Point.DeliveryPoints[0].Ready && (-1 != textMessagingSettingsVersion1Point.DeliveryPoints[0].P2pMessagingPriority || -1 != textMessagingSettingsVersion1Point.DeliveryPoints[0].M2pMessagingPriority);
		}

		// Token: 0x060062D7 RID: 25303 RVA: 0x001A13DC File Offset: 0x0019F5DC
		internal static object EasPhoneNumberGetter(IPropertyBag propertyBag)
		{
			TextMessagingSettingsVersion1Point0 textMessagingSettingsVersion1Point = (TextMessagingSettingsVersion1Point0)propertyBag[TextMessagingAccountSchema.TextMessagingSettings];
			return textMessagingSettingsVersion1Point.DeliveryPoints[0].PhoneNumber;
		}

		// Token: 0x060062D8 RID: 25304 RVA: 0x001A140C File Offset: 0x0019F60C
		internal static object EasDeviceProtocolGetter(IPropertyBag propertyBag)
		{
			TextMessagingSettingsVersion1Point0 textMessagingSettingsVersion1Point = (TextMessagingSettingsVersion1Point0)propertyBag[TextMessagingAccountSchema.TextMessagingSettings];
			return textMessagingSettingsVersion1Point.DeliveryPoints[0].Protocol;
		}

		// Token: 0x060062D9 RID: 25305 RVA: 0x001A143C File Offset: 0x0019F63C
		internal static object EasDeviceTypeGetter(IPropertyBag propertyBag)
		{
			TextMessagingSettingsVersion1Point0 textMessagingSettingsVersion1Point = (TextMessagingSettingsVersion1Point0)propertyBag[TextMessagingAccountSchema.TextMessagingSettings];
			return textMessagingSettingsVersion1Point.DeliveryPoints[0].DeviceType;
		}

		// Token: 0x060062DA RID: 25306 RVA: 0x001A146C File Offset: 0x0019F66C
		internal static object EasDeviceIdGetter(IPropertyBag propertyBag)
		{
			TextMessagingSettingsVersion1Point0 textMessagingSettingsVersion1Point = (TextMessagingSettingsVersion1Point0)propertyBag[TextMessagingAccountSchema.TextMessagingSettings];
			return textMessagingSettingsVersion1Point.DeliveryPoints[0].DeviceId;
		}

		// Token: 0x060062DB RID: 25307 RVA: 0x001A149C File Offset: 0x0019F69C
		internal static object EasDeviceFriendlyNameGetter(IPropertyBag propertyBag)
		{
			TextMessagingSettingsVersion1Point0 textMessagingSettingsVersion1Point = (TextMessagingSettingsVersion1Point0)propertyBag[TextMessagingAccountSchema.TextMessagingSettings];
			return textMessagingSettingsVersion1Point.DeliveryPoints[0].DeviceFriendlyName;
		}

		// Token: 0x060062DC RID: 25308 RVA: 0x001A14CC File Offset: 0x0019F6CC
		private static PossibleRecipient GetWritablePossibleRecipient(TextMessagingSettingsVersion1Point0 settings)
		{
			bool flag = false;
			return TextMessagingAccount.GetWritablePossibleRecipient(settings, out flag);
		}

		// Token: 0x060062DD RID: 25309 RVA: 0x001A14E4 File Offset: 0x0019F6E4
		private static PossibleRecipient GetWritablePossibleRecipient(TextMessagingSettingsVersion1Point0 settings, out bool created)
		{
			created = false;
			IList<PossibleRecipient> list = settings.MachineToPersonMessagingPolicies.EffectivePossibleRecipients;
			if (list.Count == 0)
			{
				list = settings.MachineToPersonMessagingPolicies.PossibleRecipients;
			}
			if (list.Count == 0)
			{
				list.Add(new PossibleRecipient(true, DateTime.UtcNow, null, null, null, false, null, null, null));
				created = true;
			}
			return list[0];
		}

		// Token: 0x060062DE RID: 25310 RVA: 0x001A153E File Offset: 0x0019F73E
		private void UpdateOnceOrignalEasEnabled()
		{
			if (this.originalEasEnabled != null)
			{
				return;
			}
			this.originalEasEnabled = new bool?(this.EasEnabled);
		}

		// Token: 0x17001B68 RID: 7016
		// (get) Token: 0x060062DF RID: 25311 RVA: 0x001A1560 File Offset: 0x0019F760
		internal bool OrignalEasEnabled
		{
			get
			{
				bool? flag = this.originalEasEnabled;
				if (flag == null)
				{
					return this.EasEnabled;
				}
				return flag.GetValueOrDefault();
			}
		}

		// Token: 0x060062E0 RID: 25312 RVA: 0x001A158C File Offset: 0x0019F78C
		internal void SetEasEnabled(E164Number number, string protocol, string deviceType, string deviceId, string deviceFriendlyName)
		{
			if (null == number)
			{
				throw new ArgumentNullException("number");
			}
			this.UpdateOnceOrignalEasEnabled();
			this.TextMessagingSettings.DeliveryPoints[0].P2pMessagingPriority = 0;
			this.TextMessagingSettings.DeliveryPoints[0].M2pMessagingPriority = -1;
			this.TextMessagingSettings.DeliveryPoints[1].P2pMessagingPriority = -1;
			this.TextMessagingSettings.DeliveryPoints[1].M2pMessagingPriority = 1;
			this.TextMessagingSettings.DeliveryPoints[0].PhoneNumber = number;
			this.TextMessagingSettings.DeliveryPoints[0].Protocol = protocol;
			this.TextMessagingSettings.DeliveryPoints[0].DeviceType = deviceType;
			this.TextMessagingSettings.DeliveryPoints[0].DeviceId = deviceId;
			this.TextMessagingSettings.DeliveryPoints[0].DeviceFriendlyName = deviceFriendlyName;
		}

		// Token: 0x060062E1 RID: 25313 RVA: 0x001A1684 File Offset: 0x0019F884
		internal void SetEasDisabled()
		{
			this.UpdateOnceOrignalEasEnabled();
			this.TextMessagingSettings.DeliveryPoints[0].P2pMessagingPriority = -1;
			this.TextMessagingSettings.DeliveryPoints[0].M2pMessagingPriority = -1;
			this.TextMessagingSettings.DeliveryPoints[1].P2pMessagingPriority = -1;
			this.TextMessagingSettings.DeliveryPoints[1].M2pMessagingPriority = 1;
			this.TextMessagingSettings.DeliveryPoints[0].PhoneNumber = null;
			this.TextMessagingSettings.DeliveryPoints[0].Protocol = null;
			this.TextMessagingSettings.DeliveryPoints[0].DeviceType = null;
			this.TextMessagingSettings.DeliveryPoints[0].DeviceId = null;
			this.TextMessagingSettings.DeliveryPoints[0].DeviceFriendlyName = null;
		}

		// Token: 0x17001B69 RID: 7017
		// (get) Token: 0x060062E2 RID: 25314 RVA: 0x001A1766 File Offset: 0x0019F966
		// (set) Token: 0x060062E3 RID: 25315 RVA: 0x001A176E File Offset: 0x0019F96E
		internal CultureInfo NotificationPreferredCulture { get; set; }

		// Token: 0x060062E4 RID: 25316 RVA: 0x001A1777 File Offset: 0x0019F977
		public override string ToString()
		{
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			return base.ToString();
		}

		// Token: 0x17001B6A RID: 7018
		// (get) Token: 0x060062E5 RID: 25317 RVA: 0x001A1793 File Offset: 0x0019F993
		internal override string UserConfigurationName
		{
			get
			{
				return "TextMessaging.001";
			}
		}

		// Token: 0x17001B6B RID: 7019
		// (get) Token: 0x060062E6 RID: 25318 RVA: 0x001A179A File Offset: 0x0019F99A
		internal override ProviderPropertyDefinition RawVersionedXmlPropertyDefinition
		{
			get
			{
				return TextMessagingAccountSchema.RawTextMessagingSettings;
			}
		}

		// Token: 0x040037EA RID: 14314
		internal const int DefaultEasP2pPriority = 0;

		// Token: 0x040037EB RID: 14315
		internal const int DefaultEasM2pPriority = -1;

		// Token: 0x040037EC RID: 14316
		internal const int DefaultSmtp2SmsGatewayP2pPriority = -1;

		// Token: 0x040037ED RID: 14317
		internal const int DefaultSmtp2SmsGatewayM2pPriority = 1;

		// Token: 0x040037EE RID: 14318
		internal const int EasDeliveryPointIndex = 0;

		// Token: 0x040037EF RID: 14319
		internal const int Smtp2SmsGatewayDeliveryPointIndex = 1;

		// Token: 0x040037F0 RID: 14320
		internal const string ConfigurationName = "TextMessaging.001";

		// Token: 0x040037F1 RID: 14321
		private static XsoMailboxConfigurationObjectSchema schema = ObjectSchema.GetInstance<TextMessagingAccountSchema>();

		// Token: 0x040037F2 RID: 14322
		private bool? originalEasEnabled;
	}
}
