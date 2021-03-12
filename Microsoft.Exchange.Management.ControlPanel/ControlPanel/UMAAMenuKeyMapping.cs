using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004A8 RID: 1192
	[DataContract]
	public class UMAAMenuKeyMapping
	{
		// Token: 0x06003B1A RID: 15130 RVA: 0x000B2C44 File Offset: 0x000B0E44
		private UMAAMenuKeyMapping(CustomMenuKeyMapping keymapping, Dictionary<string, ADRecipient> legacyToRecipient)
		{
			this.Prompt = keymapping.Description;
			this.Key = keymapping.Key;
			this.ActionPromptFileName = keymapping.PromptFileName;
			this.TransferToExtension = string.Empty;
			if (!string.IsNullOrEmpty(keymapping.Extension))
			{
				this.ActionType = UMAAMenuActionTypeEnum.TransferToExtension;
				this.TransferToExtension = keymapping.Extension;
				this.ActionSummary = Strings.UMKeyMappingActionSummaryTransferToExtension(keymapping.Extension);
				return;
			}
			if (!string.IsNullOrEmpty(keymapping.AutoAttendantName))
			{
				this.TransferToAutoAttendant = new Identity(keymapping.AutoAttendantName, keymapping.AutoAttendantName);
				this.ActionType = UMAAMenuActionTypeEnum.TransferToAutoAttendant;
				this.ActionSummary = Strings.UMKeyMappingActionSummaryTransferToAA(keymapping.AutoAttendantName);
				return;
			}
			if (!string.IsNullOrEmpty(keymapping.LegacyDNToUseForLeaveVoicemailFor))
			{
				this.ActionType = UMAAMenuActionTypeEnum.LeaveVoicemailFor;
				ADRecipient adrecipient;
				if (legacyToRecipient.TryGetValue(keymapping.LegacyDNToUseForLeaveVoicemailFor, out adrecipient))
				{
					this.LeaveVoicemailForUser = new Identity(adrecipient.Id, adrecipient.DisplayName);
					this.ActionSummary = Strings.UMKeyMappingActionSummaryLeaveVM(adrecipient.DisplayName);
					return;
				}
				this.LeaveVoicemailForUser = new Identity(keymapping.LegacyDNToUseForLeaveVoicemailFor, keymapping.LegacyDNToUseForLeaveVoicemailFor);
				this.ActionSummary = Strings.UMKeyMappingActionSummaryLeaveVM(keymapping.LegacyDNToUseForLeaveVoicemailFor);
				return;
			}
			else
			{
				if (!string.IsNullOrEmpty(keymapping.AnnounceBusinessLocation))
				{
					this.ActionType = UMAAMenuActionTypeEnum.AnnounceBusinessLocation;
					this.ActionSummary = Strings.UMKeyMappingActionSummaryAnnounceBusinessLocation;
					return;
				}
				if (!string.IsNullOrEmpty(keymapping.AnnounceBusinessHours))
				{
					this.ActionType = UMAAMenuActionTypeEnum.AnnounceBusinessHours;
					this.ActionSummary = Strings.UMKeyMappingActionSummaryAnnounceBusinessHours;
					return;
				}
				this.ActionType = UMAAMenuActionTypeEnum.None;
				this.ActionSummary = string.Empty;
				return;
			}
		}

		// Token: 0x17002359 RID: 9049
		// (get) Token: 0x06003B1B RID: 15131 RVA: 0x000B2DDB File Offset: 0x000B0FDB
		// (set) Token: 0x06003B1C RID: 15132 RVA: 0x000B2DE3 File Offset: 0x000B0FE3
		[DataMember]
		public string Prompt { get; private set; }

		// Token: 0x1700235A RID: 9050
		// (get) Token: 0x06003B1D RID: 15133 RVA: 0x000B2DEC File Offset: 0x000B0FEC
		// (set) Token: 0x06003B1E RID: 15134 RVA: 0x000B2DF4 File Offset: 0x000B0FF4
		[DataMember]
		public UMAAMenuActionTypeEnum ActionType { get; private set; }

		// Token: 0x1700235B RID: 9051
		// (get) Token: 0x06003B1F RID: 15135 RVA: 0x000B2DFD File Offset: 0x000B0FFD
		// (set) Token: 0x06003B20 RID: 15136 RVA: 0x000B2E05 File Offset: 0x000B1005
		[DataMember]
		public string ActionSummary { get; private set; }

		// Token: 0x1700235C RID: 9052
		// (get) Token: 0x06003B21 RID: 15137 RVA: 0x000B2E0E File Offset: 0x000B100E
		// (set) Token: 0x06003B22 RID: 15138 RVA: 0x000B2E16 File Offset: 0x000B1016
		[DataMember]
		public string Key { get; private set; }

		// Token: 0x1700235D RID: 9053
		// (get) Token: 0x06003B23 RID: 15139 RVA: 0x000B2E1F File Offset: 0x000B101F
		// (set) Token: 0x06003B24 RID: 15140 RVA: 0x000B2E27 File Offset: 0x000B1027
		[DataMember]
		public string ActionPromptFileName { get; private set; }

		// Token: 0x1700235E RID: 9054
		// (get) Token: 0x06003B25 RID: 15141 RVA: 0x000B2E30 File Offset: 0x000B1030
		// (set) Token: 0x06003B26 RID: 15142 RVA: 0x000B2E38 File Offset: 0x000B1038
		[DataMember]
		public string TransferToExtension { get; private set; }

		// Token: 0x1700235F RID: 9055
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x000B2E41 File Offset: 0x000B1041
		// (set) Token: 0x06003B28 RID: 15144 RVA: 0x000B2E49 File Offset: 0x000B1049
		[DataMember]
		public Identity TransferToAutoAttendant { get; private set; }

		// Token: 0x17002360 RID: 9056
		// (get) Token: 0x06003B29 RID: 15145 RVA: 0x000B2E52 File Offset: 0x000B1052
		// (set) Token: 0x06003B2A RID: 15146 RVA: 0x000B2E5A File Offset: 0x000B105A
		[DataMember]
		public Identity LeaveVoicemailForUser { get; private set; }

		// Token: 0x06003B2B RID: 15147 RVA: 0x000B2E64 File Offset: 0x000B1064
		internal static void CreateMappings(MultiValuedProperty<CustomMenuKeyMapping> businessHours, MultiValuedProperty<CustomMenuKeyMapping> afterHours, out List<UMAAMenuKeyMapping> businessHoursContract, out List<UMAAMenuKeyMapping> afterHoursContract)
		{
			businessHoursContract = new List<UMAAMenuKeyMapping>(businessHours.Count);
			afterHoursContract = new List<UMAAMenuKeyMapping>(afterHours.Count);
			Dictionary<string, ADRecipient> legacyToRecipient = UMAAMenuKeyMapping.CreateRecipientMapping(new MultiValuedProperty<CustomMenuKeyMapping>[]
			{
				businessHours,
				afterHours
			});
			foreach (CustomMenuKeyMapping keymapping in businessHours)
			{
				businessHoursContract.Add(new UMAAMenuKeyMapping(keymapping, legacyToRecipient));
			}
			foreach (CustomMenuKeyMapping keymapping2 in afterHours)
			{
				afterHoursContract.Add(new UMAAMenuKeyMapping(keymapping2, legacyToRecipient));
			}
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x000B2F30 File Offset: 0x000B1130
		internal CustomMenuKeyMapping ToCustomKeyMapping()
		{
			this.Validate();
			return new CustomMenuKeyMapping(this.Key, this.Prompt, (this.ActionType == UMAAMenuActionTypeEnum.TransferToExtension) ? this.TransferToExtension : null, (this.ActionType == UMAAMenuActionTypeEnum.TransferToAutoAttendant) ? this.TransferToAutoAttendant.DisplayName : null, this.ActionPromptFileName, null, (this.ActionType == UMAAMenuActionTypeEnum.LeaveVoicemailFor) ? this.LeaveVoicemailForUser.RawIdentity : null, null, (this.ActionType == UMAAMenuActionTypeEnum.AnnounceBusinessLocation) ? "1" : null, (this.ActionType == UMAAMenuActionTypeEnum.AnnounceBusinessHours) ? "1" : null);
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x000B2FC0 File Offset: 0x000B11C0
		private static Dictionary<string, ADRecipient> CreateRecipientMapping(MultiValuedProperty<CustomMenuKeyMapping>[] customMappings)
		{
			List<string> list = new List<string>();
			Dictionary<string, ADRecipient> dictionary = new Dictionary<string, ADRecipient>(StringComparer.OrdinalIgnoreCase);
			foreach (MultiValuedProperty<CustomMenuKeyMapping> multiValuedProperty in customMappings)
			{
				foreach (CustomMenuKeyMapping customMenuKeyMapping in multiValuedProperty)
				{
					if (!string.IsNullOrEmpty(customMenuKeyMapping.LegacyDNToUseForLeaveVoicemailFor) && !list.Contains(customMenuKeyMapping.LegacyDNToUseForLeaveVoicemailFor))
					{
						list.Add(customMenuKeyMapping.LegacyDNToUseForLeaveVoicemailFor);
					}
				}
			}
			IEnumerable<ADRecipient> enumerable = RecipientObjectResolver.Instance.ResolveLegacyDNs(list);
			if (enumerable != null)
			{
				foreach (ADRecipient adrecipient in enumerable)
				{
					dictionary.Add(adrecipient.LegacyExchangeDN, adrecipient);
				}
			}
			return dictionary;
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x000B30B4 File Offset: 0x000B12B4
		private void Validate()
		{
			switch (this.ActionType)
			{
			case UMAAMenuActionTypeEnum.TransferToExtension:
				this.TransferToExtension.FaultIfNullOrEmpty(Strings.UMHolidayScheduleTransferToExtensionNotSet);
				return;
			case UMAAMenuActionTypeEnum.TransferToAutoAttendant:
				this.TransferToAutoAttendant.FaultIfNull(Strings.UMHolidayScheduleTransferToAutoAttendantNotSet);
				return;
			case UMAAMenuActionTypeEnum.LeaveVoicemailFor:
				this.LeaveVoicemailForUser.FaultIfNull(Strings.UMHolidayScheduleLeaveVoicemailForNotSet);
				return;
			default:
				return;
			}
		}

		// Token: 0x04002758 RID: 10072
		public const string TimeoutKey = "-";
	}
}
