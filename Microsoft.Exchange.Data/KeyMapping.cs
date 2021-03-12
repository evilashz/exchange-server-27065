using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000167 RID: 359
	[Serializable]
	public class KeyMapping
	{
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x00024EEA File Offset: 0x000230EA
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x00024EF2 File Offset: 0x000230F2
		public KeyMappingType KeyMappingType { get; set; }

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00024EFB File Offset: 0x000230FB
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x00024F03 File Offset: 0x00023103
		public string Context { get; set; }

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x00024F0C File Offset: 0x0002310C
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x00024F14 File Offset: 0x00023114
		public int Key { get; set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00024F1D File Offset: 0x0002311D
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x00024F25 File Offset: 0x00023125
		public string FindMeFirstNumber { get; set; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x00024F2E File Offset: 0x0002312E
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x00024F36 File Offset: 0x00023136
		public int FindMeFirstNumberDuration { get; set; }

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00024F3F File Offset: 0x0002313F
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x00024F47 File Offset: 0x00023147
		public string FindMeSecondNumber { get; set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x00024F50 File Offset: 0x00023150
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x00024F58 File Offset: 0x00023158
		public int FindMeSecondNumberDuration { get; set; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x00024F61 File Offset: 0x00023161
		// (set) Token: 0x06000BEA RID: 3050 RVA: 0x00024F69 File Offset: 0x00023169
		public string TransferToNumber { get; set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00024F72 File Offset: 0x00023172
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x00024F7A File Offset: 0x0002317A
		public string TransferToGALContactLegacyDN { get; set; }

		// Token: 0x06000BED RID: 3053 RVA: 0x00024F83 File Offset: 0x00023183
		private KeyMapping()
		{
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00024F8C File Offset: 0x0002318C
		public KeyMapping(KeyMappingType keyMappingType, int key, string context, string findMeFirstNumber, int findMeFirstNumberDuration, string findMeSecondNumber, int findMeSecondNumberDuration, string transferToNumber, string transferToGALContactLegacyDN)
		{
			this.KeyMappingType = keyMappingType;
			this.Key = key;
			this.Context = context;
			this.FindMeFirstNumber = findMeFirstNumber;
			this.FindMeFirstNumberDuration = findMeFirstNumberDuration;
			this.FindMeSecondNumber = findMeSecondNumber;
			this.FindMeSecondNumberDuration = findMeSecondNumberDuration;
			this.TransferToNumber = transferToNumber;
			this.TransferToGALContactLegacyDN = transferToGALContactLegacyDN;
			this.Validate();
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00024FEC File Offset: 0x000231EC
		public override string ToString()
		{
			return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", new object[]
			{
				(int)this.KeyMappingType,
				this.Key,
				this.Context ?? string.Empty,
				this.FindMeFirstNumber ?? string.Empty,
				this.FindMeFirstNumberDuration,
				this.FindMeSecondNumber ?? string.Empty,
				this.FindMeSecondNumberDuration,
				this.TransferToNumber ?? string.Empty,
				this.TransferToGALContactLegacyDN ?? string.Empty
			});
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x000250A0 File Offset: 0x000232A0
		public void Validate()
		{
			this.TrimAllStringMembers();
			switch (this.KeyMappingType)
			{
			case KeyMappingType.TransferToNumber:
				this.ValidateForTransferToNumber();
				return;
			case KeyMappingType.TransferToGALContact:
				this.ValidateForTransferToGALContact();
				return;
			case KeyMappingType.TransferToGALContactVoiceMail:
				this.ValidateForTransferToGALContactVoiceMail();
				return;
			case KeyMappingType.VoiceMail:
				this.ValidateForVoiceMail();
				return;
			case KeyMappingType.FindMe:
				this.ValidateForFindMe();
				return;
			default:
				throw new Exception("Unknown KeyMappingType enum value");
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00025108 File Offset: 0x00023308
		public static KeyMapping Parse(string keyMappingValue)
		{
			if (string.IsNullOrEmpty(keyMappingValue))
			{
				throw new FormatException(DataStrings.InvalidKeyMappingFormat);
			}
			string[] array = keyMappingValue.Split(new char[]
			{
				','
			});
			if (array == null || array.Length != 9)
			{
				throw new FormatException(DataStrings.InvalidKeyMappingFormat);
			}
			KeyMappingType keyMappingType = (KeyMappingType)CallerIdItem.ValidateEnumValue(array[0], "KeyMappingType", 1, 5);
			if (string.IsNullOrEmpty(array[1]))
			{
				throw new FormatException(DataStrings.InvalidKeyMappingKey);
			}
			int key = int.Parse(array[1]);
			int findMeFirstNumberDuration = 0;
			if (!string.IsNullOrEmpty(array[4]))
			{
				findMeFirstNumberDuration = int.Parse(array[4]);
			}
			int findMeSecondNumberDuration = 0;
			if (!string.IsNullOrEmpty(array[6]))
			{
				findMeSecondNumberDuration = int.Parse(array[6]);
			}
			return new KeyMapping(keyMappingType, key, array[2], array[3], findMeFirstNumberDuration, array[5], findMeSecondNumberDuration, array[7], array[8]);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000251D8 File Offset: 0x000233D8
		private void TrimAllStringMembers()
		{
			this.Context = CustomMenuKeyMapping.TrimAndMapEmptyToNull(this.Context);
			this.FindMeFirstNumber = CustomMenuKeyMapping.TrimAndMapEmptyToNull(this.FindMeFirstNumber);
			this.FindMeSecondNumber = CustomMenuKeyMapping.TrimAndMapEmptyToNull(this.FindMeSecondNumber);
			this.TransferToNumber = CustomMenuKeyMapping.TrimAndMapEmptyToNull(this.TransferToNumber);
			this.TransferToGALContactLegacyDN = CustomMenuKeyMapping.TrimAndMapEmptyToNull(this.TransferToGALContactLegacyDN);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0002523C File Offset: 0x0002343C
		private void ValidateForVoiceMail()
		{
			if (this.Key != 10 || !string.IsNullOrEmpty(this.TransferToNumber) || !string.IsNullOrEmpty(this.FindMeFirstNumber) || !string.IsNullOrEmpty(this.FindMeSecondNumber) || !string.IsNullOrEmpty(this.Context) || !string.IsNullOrEmpty(this.TransferToGALContactLegacyDN))
			{
				throw new FormatException(DataStrings.InvalidKeyMappingVoiceMail);
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x000252A4 File Offset: 0x000234A4
		private void ValidateForFindMe()
		{
			this.ValidateKey();
			this.ValidateContext();
			if (!string.IsNullOrEmpty(this.TransferToNumber) || string.IsNullOrEmpty(this.FindMeFirstNumber) || !string.IsNullOrEmpty(this.TransferToGALContactLegacyDN))
			{
				throw new FormatException(DataStrings.InvalidKeyMappingFindMe);
			}
			this.ValidateFindMeDuration();
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x000252FA File Offset: 0x000234FA
		private void ValidateForTransferToGALContactVoiceMail()
		{
			this.ValidateForTransferToGALContact();
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00025304 File Offset: 0x00023504
		private void ValidateForTransferToGALContact()
		{
			this.ValidateKey();
			this.ValidateContext();
			if (!string.IsNullOrEmpty(this.TransferToNumber) || !string.IsNullOrEmpty(this.FindMeFirstNumber) || !string.IsNullOrEmpty(this.FindMeSecondNumber) || string.IsNullOrEmpty(this.TransferToGALContactLegacyDN))
			{
				throw new FormatException(DataStrings.InvalidKeyMappingTransferToGalContact);
			}
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00025364 File Offset: 0x00023564
		private void ValidateForTransferToNumber()
		{
			this.ValidateKey();
			this.ValidateContext();
			if (string.IsNullOrEmpty(this.TransferToNumber) || !string.IsNullOrEmpty(this.FindMeFirstNumber) || !string.IsNullOrEmpty(this.FindMeSecondNumber) || !string.IsNullOrEmpty(this.TransferToGALContactLegacyDN))
			{
				throw new FormatException(DataStrings.InvalidKeyMappingTransferToNumber);
			}
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x000253C1 File Offset: 0x000235C1
		private void ValidateKey()
		{
			if (this.Key < 1 || this.Key > 9)
			{
				throw new FormatException(DataStrings.InvalidKeyMappingKey);
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x000253E6 File Offset: 0x000235E6
		private void ValidateContext()
		{
			if (!string.IsNullOrEmpty(this.Context) && this.Context.Length > 80)
			{
				throw new FormatException(DataStrings.InvalidKeyMappingContext);
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00025414 File Offset: 0x00023614
		private void ValidateFindMeDuration()
		{
			if (this.FindMeFirstNumberDuration < 20 || this.FindMeFirstNumberDuration > 99)
			{
				throw new FormatException(DataStrings.InvalidKeyMappingFindMeFirstNumberDuration);
			}
			if ((string.IsNullOrEmpty(this.FindMeSecondNumber) && this.FindMeSecondNumberDuration != 0) || (!string.IsNullOrEmpty(this.FindMeSecondNumber) && (this.FindMeSecondNumberDuration < 20 || this.FindMeSecondNumberDuration > 99)))
			{
				throw new FormatException(DataStrings.InvalidKeyMappingFindMeSecondNumber);
			}
		}

		// Token: 0x04000725 RID: 1829
		private const int RequiredNumberOfTokens = 9;

		// Token: 0x04000726 RID: 1830
		private const int MinFindMeDuration = 20;

		// Token: 0x04000727 RID: 1831
		private const int MaxFindMeDuration = 99;

		// Token: 0x04000728 RID: 1832
		private const int MaxContextLength = 80;

		// Token: 0x04000729 RID: 1833
		private const int MinKey = 1;

		// Token: 0x0400072A RID: 1834
		private const int MaxKey = 9;

		// Token: 0x0400072B RID: 1835
		private const int KeyForVoiceMailKeyType = 10;

		// Token: 0x02000168 RID: 360
		private enum KeyMappingStringToken
		{
			// Token: 0x04000736 RID: 1846
			Type,
			// Token: 0x04000737 RID: 1847
			Key,
			// Token: 0x04000738 RID: 1848
			Context,
			// Token: 0x04000739 RID: 1849
			FindMeFirstNumber,
			// Token: 0x0400073A RID: 1850
			FindMeFirstNumberDuration,
			// Token: 0x0400073B RID: 1851
			FindMeSecondNumber,
			// Token: 0x0400073C RID: 1852
			FindMeSecondNumberDuration,
			// Token: 0x0400073D RID: 1853
			TransferToNumber,
			// Token: 0x0400073E RID: 1854
			TransferToGALContactLegacyDN
		}
	}
}
