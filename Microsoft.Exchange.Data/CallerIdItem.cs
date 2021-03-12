using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000127 RID: 295
	[Serializable]
	public class CallerIdItem
	{
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00020808 File Offset: 0x0001EA08
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x00020810 File Offset: 0x0001EA10
		public CallerIdItemType CallerIdType { get; set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x00020819 File Offset: 0x0001EA19
		// (set) Token: 0x06000A71 RID: 2673 RVA: 0x00020821 File Offset: 0x0001EA21
		public string PhoneNumber { get; set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0002082A File Offset: 0x0001EA2A
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x00020832 File Offset: 0x0001EA32
		public string GALContactLegacyDN { get; set; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0002083B File Offset: 0x0001EA3B
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x00020843 File Offset: 0x0001EA43
		public string PersonalContactStoreId { get; set; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0002084C File Offset: 0x0001EA4C
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x00020854 File Offset: 0x0001EA54
		public string PersonaEmailAddress { get; set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x0002085D File Offset: 0x0001EA5D
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x00020865 File Offset: 0x0001EA65
		public string DisplayName { get; set; }

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002086E File Offset: 0x0001EA6E
		private CallerIdItem()
		{
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00020876 File Offset: 0x0001EA76
		public CallerIdItem(CallerIdItemType callerIdType, string phoneNumber, string galContactLegacyDN, string personalContactStoreId, string personaEmailAddress = null, string displayName = null)
		{
			this.CallerIdType = callerIdType;
			this.PhoneNumber = phoneNumber;
			this.GALContactLegacyDN = galContactLegacyDN;
			this.PersonalContactStoreId = personalContactStoreId;
			this.PersonaEmailAddress = personaEmailAddress;
			this.DisplayName = displayName;
			this.Validate();
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x000208B4 File Offset: 0x0001EAB4
		public static CallerIdItem Parse(string callerIdItem)
		{
			if (string.IsNullOrEmpty(callerIdItem))
			{
				throw new FormatException(DataStrings.InvalidCallerIdItemFormat);
			}
			string[] array = callerIdItem.Split(new char[]
			{
				','
			});
			if (array == null || array.Length != 5)
			{
				throw new FormatException(DataStrings.InvalidCallerIdItemFormat);
			}
			int callerIdType = CallerIdItem.ValidateEnumValue(array[0], "CallerIdItemType", 1, 5);
			return new CallerIdItem((CallerIdItemType)callerIdType, array[1], array[2], array[3], array[4], null);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002092C File Offset: 0x0001EB2C
		public static int ValidateEnumValue(string enumValue, string typeName, int min, int max)
		{
			if (enumValue == null)
			{
				throw new ArgumentNullException("enumValue");
			}
			if (string.IsNullOrEmpty(typeName))
			{
				throw new ArgumentException("typeName");
			}
			int num;
			if (!int.TryParse(enumValue, out num) || num < min || num > max)
			{
				throw new FormatException(DataStrings.ConstraintViolationEnumValueNotDefined(enumValue, typeName));
			}
			return num;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00020980 File Offset: 0x0001EB80
		public override string ToString()
		{
			return string.Format("{0},{1},{2},{3},{4}", new object[]
			{
				(int)this.CallerIdType,
				this.PhoneNumber ?? string.Empty,
				this.GALContactLegacyDN ?? string.Empty,
				this.PersonalContactStoreId ?? string.Empty,
				this.PersonaEmailAddress ?? string.Empty
			});
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x000209F8 File Offset: 0x0001EBF8
		public void Validate()
		{
			this.TrimAllStringMembers();
			switch (this.CallerIdType)
			{
			case CallerIdItemType.PhoneNumber:
				if (!string.IsNullOrEmpty(this.GALContactLegacyDN) || !string.IsNullOrEmpty(this.PersonalContactStoreId) || !string.IsNullOrEmpty(this.PersonaEmailAddress) || string.IsNullOrEmpty(this.PhoneNumber))
				{
					throw new FormatException(DataStrings.InvalidCallerIdItemTypePhoneNumber);
				}
				break;
			case CallerIdItemType.GALContact:
				if (!string.IsNullOrEmpty(this.PhoneNumber) || !string.IsNullOrEmpty(this.PersonalContactStoreId) || !string.IsNullOrEmpty(this.PersonaEmailAddress) || string.IsNullOrEmpty(this.GALContactLegacyDN))
				{
					throw new FormatException(DataStrings.InvalidCallerIdItemTypeGALContactr);
				}
				break;
			case CallerIdItemType.PersonalContact:
				if (!string.IsNullOrEmpty(this.GALContactLegacyDN) || !string.IsNullOrEmpty(this.PhoneNumber) || !string.IsNullOrEmpty(this.PersonaEmailAddress) || string.IsNullOrEmpty(this.PersonalContactStoreId))
				{
					throw new FormatException(DataStrings.InvalidCallerIdItemTypePersonalContact);
				}
				break;
			case CallerIdItemType.DefaultContactsFolder:
				if (!string.IsNullOrEmpty(this.GALContactLegacyDN) || !string.IsNullOrEmpty(this.PersonalContactStoreId) || !string.IsNullOrEmpty(this.PhoneNumber) || !string.IsNullOrEmpty(this.PersonaEmailAddress))
				{
					throw new FormatException(DataStrings.InvalidCallerIdItemTypeDefaultContactsFolder);
				}
				break;
			case CallerIdItemType.PersonaContact:
				if (!string.IsNullOrEmpty(this.GALContactLegacyDN) || !string.IsNullOrEmpty(this.PhoneNumber) || !string.IsNullOrEmpty(this.PersonalContactStoreId) || string.IsNullOrEmpty(this.PersonaEmailAddress))
				{
					throw new FormatException(DataStrings.InvalidCallerIdItemTypePersonaContact);
				}
				break;
			default:
				throw new Exception("Unkown Enumeration type.");
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00020B9C File Offset: 0x0001ED9C
		private void TrimAllStringMembers()
		{
			this.GALContactLegacyDN = CustomMenuKeyMapping.TrimAndMapEmptyToNull(this.GALContactLegacyDN);
			this.PersonalContactStoreId = CustomMenuKeyMapping.TrimAndMapEmptyToNull(this.PersonalContactStoreId);
			this.PhoneNumber = CustomMenuKeyMapping.TrimAndMapEmptyToNull(this.PhoneNumber);
			this.PersonaEmailAddress = CustomMenuKeyMapping.TrimAndMapEmptyToNull(this.PersonaEmailAddress);
			this.DisplayName = CustomMenuKeyMapping.TrimAndMapEmptyToNull(this.DisplayName);
		}

		// Token: 0x04000647 RID: 1607
		private const int RequiredNumberOfTokens = 5;
	}
}
