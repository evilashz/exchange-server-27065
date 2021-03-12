using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001BF RID: 447
	[Serializable]
	public class CustomMenuKeyMapping : IComparable, IEquatable<CustomMenuKeyMapping>
	{
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0002FA93 File Offset: 0x0002DC93
		[XmlIgnore]
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0002FA9B File Offset: 0x0002DC9B
		// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x0002FAA3 File Offset: 0x0002DCA3
		public CustomMenuKey MappedKey
		{
			get
			{
				return this.mappedKey;
			}
			set
			{
				this.mappedKey = value;
				this.key = CustomMenuKeyMapping.MapKeyToString(this.mappedKey);
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0002FABD File Offset: 0x0002DCBD
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x0002FAC5 File Offset: 0x0002DCC5
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
				CustomMenuKeyMapping.validateDescription(value);
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0002FAD4 File Offset: 0x0002DCD4
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x0002FADC File Offset: 0x0002DCDC
		public string Extension
		{
			get
			{
				return this.extension;
			}
			set
			{
				this.extension = value;
				CustomMenuKeyMapping.validateExtension(value);
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0002FAEB File Offset: 0x0002DCEB
		// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x0002FAF3 File Offset: 0x0002DCF3
		public string AutoAttendantName
		{
			get
			{
				return this.autoAttendantName;
			}
			set
			{
				this.autoAttendantName = value;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x0002FAFC File Offset: 0x0002DCFC
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x0002FB04 File Offset: 0x0002DD04
		public string LeaveVoicemailFor
		{
			get
			{
				return this.leaveVoicemailFor;
			}
			set
			{
				this.leaveVoicemailFor = value;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0002FB0D File Offset: 0x0002DD0D
		// (set) Token: 0x06000FAA RID: 4010 RVA: 0x0002FB15 File Offset: 0x0002DD15
		public string LegacyDNToUseForLeaveVoicemailFor
		{
			get
			{
				return this.legacyDNToUseForLeaveVoicemailFor;
			}
			set
			{
				this.legacyDNToUseForLeaveVoicemailFor = value;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0002FB1E File Offset: 0x0002DD1E
		// (set) Token: 0x06000FAC RID: 4012 RVA: 0x0002FB26 File Offset: 0x0002DD26
		public string TransferToMailbox
		{
			get
			{
				return this.transferToMailbox;
			}
			set
			{
				this.transferToMailbox = value;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x0002FB2F File Offset: 0x0002DD2F
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x0002FB37 File Offset: 0x0002DD37
		public string LegacyDNToUseForTransferToMailbox
		{
			get
			{
				return this.legacyDNToUseForTransferToMailbox;
			}
			set
			{
				this.legacyDNToUseForTransferToMailbox = value;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x0002FB40 File Offset: 0x0002DD40
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x0002FB48 File Offset: 0x0002DD48
		public string PromptFileName
		{
			get
			{
				return this.promptFileName;
			}
			set
			{
				this.promptFileName = value;
				CustomMenuKeyMapping.ValidatePromptFileName("PromptFileName", value);
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0002FB5C File Offset: 0x0002DD5C
		// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x0002FB64 File Offset: 0x0002DD64
		public string AsrPhrases
		{
			get
			{
				return this.asrPhrases;
			}
			set
			{
				this.asrPhrases = CustomMenuKeyMapping.TrimAndMapEmptyToNull(value);
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0002FB74 File Offset: 0x0002DD74
		[XmlIgnore]
		public string[] AsrPhraseList
		{
			get
			{
				string[] result = null;
				if (!string.IsNullOrEmpty(this.asrPhrases))
				{
					result = this.asrPhrases.Split(new char[]
					{
						';'
					});
				}
				return result;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x0002FBAA File Offset: 0x0002DDAA
		// (set) Token: 0x06000FB5 RID: 4021 RVA: 0x0002FBB2 File Offset: 0x0002DDB2
		public string AnnounceBusinessLocation
		{
			get
			{
				return this.announceBusinessLocation;
			}
			set
			{
				CustomMenuKeyMapping.ValidateFlag(CustomMenuKeyMapping.TrimAndMapEmptyToNull(value), "AnnounceBusinessLocation");
				this.announceBusinessLocation = value;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0002FBCB File Offset: 0x0002DDCB
		// (set) Token: 0x06000FB7 RID: 4023 RVA: 0x0002FBD3 File Offset: 0x0002DDD3
		public string AnnounceBusinessHours
		{
			get
			{
				return this.announceBusinessHours;
			}
			set
			{
				CustomMenuKeyMapping.ValidateFlag(CustomMenuKeyMapping.TrimAndMapEmptyToNull(value), "AnnounceBusinessHours");
				this.announceBusinessHours = value;
			}
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0002FBEC File Offset: 0x0002DDEC
		public CustomMenuKeyMapping()
		{
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0002FBF4 File Offset: 0x0002DDF4
		public CustomMenuKeyMapping(string key, string name, string extension, string autoAttendant, string promptFileName) : this(key, name, extension, autoAttendant, promptFileName, null)
		{
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0002FC04 File Offset: 0x0002DE04
		public CustomMenuKeyMapping(string key, string name, string extension, string autoAttendant, string promptFileName, string asrPhrases) : this(key, name, extension, autoAttendant, promptFileName, asrPhrases, null, null, null, null)
		{
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0002FC24 File Offset: 0x0002DE24
		public CustomMenuKeyMapping(PSObject importedObject) : this(CustomMenuKeyMapping.GetObjectProperty(importedObject, "Key"), CustomMenuKeyMapping.GetObjectProperty(importedObject, "Description"), CustomMenuKeyMapping.GetObjectProperty(importedObject, "Extension"), CustomMenuKeyMapping.GetObjectProperty(importedObject, "AutoAttendantName"), CustomMenuKeyMapping.GetObjectProperty(importedObject, "PromptFileName"), CustomMenuKeyMapping.GetObjectProperty(importedObject, "AsrPhrases"), CustomMenuKeyMapping.GetObjectProperty(importedObject, "LeaveVoicemailFor"), CustomMenuKeyMapping.GetObjectProperty(importedObject, "TransferToMailbox"), CustomMenuKeyMapping.GetObjectProperty(importedObject, "AnnounceBusinessLocation"), CustomMenuKeyMapping.GetObjectProperty(importedObject, "AnnounceBusinessHours"))
		{
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0002FCA8 File Offset: 0x0002DEA8
		public CustomMenuKeyMapping(string key, string name, string extension, string autoAttendant, string promptFileName, string asrPhrases, string leaveVoicemailFor, string transferToMailbox) : this(key, name, extension, autoAttendant, promptFileName, asrPhrases, leaveVoicemailFor, transferToMailbox, null, null)
		{
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0002FCCC File Offset: 0x0002DECC
		public CustomMenuKeyMapping(string key, string name, string extension, string autoAttendant, string promptFileName, string asrPhrases, string leaveVoicemailFor, string transferToMailbox, string announceBusinessLocation, string announceBusinessHours)
		{
			this.key = key;
			this.description = CustomMenuKeyMapping.TrimAndMapEmptyToNull(name);
			this.extension = CustomMenuKeyMapping.TrimAndMapEmptyToNull(extension);
			this.autoAttendantName = CustomMenuKeyMapping.TrimAndMapEmptyToNull(autoAttendant);
			this.promptFileName = CustomMenuKeyMapping.TrimAndMapEmptyToNull(promptFileName);
			this.asrPhrases = CustomMenuKeyMapping.TrimAndMapEmptyToNull(asrPhrases);
			this.leaveVoicemailFor = CustomMenuKeyMapping.TrimAndMapEmptyToNull(leaveVoicemailFor);
			this.transferToMailbox = CustomMenuKeyMapping.TrimAndMapEmptyToNull(transferToMailbox);
			this.announceBusinessLocation = CustomMenuKeyMapping.TrimAndMapEmptyToNull(announceBusinessLocation);
			this.announceBusinessHours = CustomMenuKeyMapping.TrimAndMapEmptyToNull(announceBusinessHours);
			CustomMenuKeyMapping.validateDescription(this.description);
			this.mappedKey = CustomMenuKeyMapping.MapStringToKey(this.key);
			CustomMenuKeyMapping.ValidateFlag(this.announceBusinessLocation, "AnnounceBusinessLocation");
			CustomMenuKeyMapping.ValidateFlag(this.announceBusinessHours, "AnnounceBusinessHours");
			CustomMenuKeyMapping.validateExtension(this.extension);
			CustomMenuKeyMapping.ValidatePromptFileName("PromptFileName", this.promptFileName);
			this.Validate();
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0002FDB6 File Offset: 0x0002DFB6
		private static bool IsNumberofTokensValid(int number)
		{
			return number == 5 || number == 6 || number == 8 || number == 10;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0002FDCC File Offset: 0x0002DFCC
		private static string GetObjectProperty(PSObject importedObject, string propertyName)
		{
			if (importedObject.Properties.Match(propertyName).Count == 0)
			{
				return string.Empty;
			}
			return (string)importedObject.Properties[propertyName].Value;
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0002FE10 File Offset: 0x0002E010
		public static CustomMenuKeyMapping Parse(string customExtension)
		{
			if (customExtension == null)
			{
				throw new ArgumentNullException(customExtension);
			}
			string[] array = customExtension.Split(new char[]
			{
				','
			});
			if (array == null || !CustomMenuKeyMapping.IsNumberofTokensValid(array.Length))
			{
				throw new ArgumentException(DataStrings.KeyMappingInvalidArgument);
			}
			string text = (array.Length < 6) ? null : array[5];
			string text2 = (array.Length < 7) ? null : array[6];
			string text3 = (array.Length < 8) ? null : array[7];
			string text4 = (array.Length < 9) ? null : array[8];
			string text5 = (array.Length < 10) ? null : array[9];
			return new CustomMenuKeyMapping(array[0], array[1], array[2], array[3], array[4], text, text2, text3, text4, text5);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0002FEBC File Offset: 0x0002E0BC
		public void Validate()
		{
			switch (this.AreMultipleOptionsSet(new List<string>
			{
				this.autoAttendantName,
				this.extension,
				this.transferToMailbox,
				this.leaveVoicemailFor,
				this.announceBusinessLocation,
				this.announceBusinessHours
			}))
			{
			case CustomMenuKeyMapping.OptionSpecified.None:
				if (this.PromptFileName == null)
				{
					throw new FormatException(DataStrings.KeyMappingInvalidArgument);
				}
				break;
			case CustomMenuKeyMapping.OptionSpecified.Single:
				break;
			case CustomMenuKeyMapping.OptionSpecified.Multiple:
				throw new FormatException(DataStrings.InvalidCustomMenuKeyMappingA);
			default:
				return;
			}
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0002FF5C File Offset: 0x0002E15C
		private CustomMenuKeyMapping.OptionSpecified AreMultipleOptionsSet(List<string> options)
		{
			int num = 0;
			foreach (string text in options)
			{
				if (text != null)
				{
					num++;
					if (num == 2)
					{
						return CustomMenuKeyMapping.OptionSpecified.Multiple;
					}
				}
			}
			if (num != 0)
			{
				return CustomMenuKeyMapping.OptionSpecified.Single;
			}
			return CustomMenuKeyMapping.OptionSpecified.None;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0002FFBC File Offset: 0x0002E1BC
		private static void validateDescription(string description)
		{
			if (string.IsNullOrEmpty(description))
			{
				throw new ArgumentNullException("Description");
			}
			if (description.IndexOf(",") != -1)
			{
				throw new StrongTypeFormatException(DataStrings.InvalidCharInString("Description", ","), "Description");
			}
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00030009 File Offset: 0x0002E209
		private static void validateExtension(string extension)
		{
			if (!string.IsNullOrEmpty(extension) && !CustomMenuKeyMapping.IsNumeric(extension))
			{
				throw new StrongTypeFormatException(DataStrings.InvalidNumber(extension, "Extension"), "Extension");
			}
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00030038 File Offset: 0x0002E238
		public static void ValidatePromptFileName(string propertyName, string fileName)
		{
			if (!string.IsNullOrEmpty(fileName))
			{
				if (fileName.Length > 255)
				{
					throw new StrongTypeFormatException(DataStrings.ConstraintViolationStringLengthTooLong(255, fileName.Length), "PromptFileName");
				}
				Regex regex = new Regex("^$|\\.wav|\\.wma$", RegexOptions.IgnoreCase);
				if (!regex.IsMatch(fileName))
				{
					string pattern = DataStrings.CustomGreetingFilePatternDescription.ToString();
					throw new StrongTypeFormatException(DataStrings.ConstraintViolationStringDoesNotMatchRegularExpression(pattern, fileName).ToString(), "PromptFileName");
				}
			}
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x000300C3 File Offset: 0x0002E2C3
		public static string TrimAndMapEmptyToNull(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return null;
			}
			s = s.Trim();
			if (s.Length == 0)
			{
				return null;
			}
			return s;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x000300E4 File Offset: 0x0002E2E4
		private static bool IsNumeric(string digits)
		{
			for (int i = 0; i < digits.Length; i++)
			{
				if (!char.IsDigit(digits[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00030114 File Offset: 0x0002E314
		private static void ValidateCustomMenuKey(string keyString, out int key)
		{
			key = -1;
			int num = -1;
			if (!int.TryParse(keyString, out num))
			{
				throw new StrongTypeFormatException(DataStrings.InvalidKeySelectionA, "Key");
			}
			if (num == 0)
			{
				throw new StrongTypeFormatException(DataStrings.InvalidKeySelection_Zero, "Key");
			}
			if (num < 0 || num > 9)
			{
				throw new StrongTypeFormatException(DataStrings.InvalidKeySelectionA, "Key");
			}
			key = num;
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0003017E File Offset: 0x0002E37E
		private static void ValidateFlag(string flagString, string flagName)
		{
			if (!string.IsNullOrEmpty(flagString) && !string.Equals(flagString, "1", StringComparison.OrdinalIgnoreCase))
			{
				throw new StrongTypeFormatException(DataStrings.InvalidFlagValue, flagName);
			}
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x000301A8 File Offset: 0x0002E3A8
		public override string ToString()
		{
			return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", new object[]
			{
				CustomMenuKeyMapping.MapKeyToString(this.mappedKey),
				this.description,
				this.extension,
				this.autoAttendantName,
				this.promptFileName,
				this.asrPhrases,
				this.leaveVoicemailFor,
				this.transferToMailbox,
				this.announceBusinessLocation,
				this.announceBusinessHours
			});
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00030228 File Offset: 0x0002E428
		private static string MapKeyToString(CustomMenuKey key)
		{
			if (key == CustomMenuKey.Timeout)
			{
				return "-";
			}
			if (key != CustomMenuKey.InvalidKey)
			{
				return Convert.ToString((int)key);
			}
			return null;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00030250 File Offset: 0x0002E450
		private static CustomMenuKey MapStringToKey(string keyString)
		{
			string text = CustomMenuKeyMapping.TrimAndMapEmptyToNull(keyString);
			if (text == null)
			{
				throw new StrongTypeFormatException(DataStrings.InvalidKeySelectionA, "Key");
			}
			if (string.Compare(text, "-", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return CustomMenuKey.Timeout;
			}
			int result = -1;
			CustomMenuKeyMapping.ValidateCustomMenuKey(text, out result);
			return (CustomMenuKey)result;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00030298 File Offset: 0x0002E498
		public int CompareTo(object obj)
		{
			int result;
			if (obj == null)
			{
				result = 1;
			}
			else
			{
				CustomMenuKeyMapping customMenuKeyMapping = obj as CustomMenuKeyMapping;
				if (customMenuKeyMapping == null)
				{
					result = -1;
				}
				else if (customMenuKeyMapping.MappedKey == CustomMenuKey.Timeout)
				{
					result = -1;
				}
				else if (this.MappedKey == CustomMenuKey.Timeout)
				{
					result = 1;
				}
				else if (this.MappedKey == CustomMenuKey.NotSpecified && this.MappedKey == customMenuKeyMapping.MappedKey)
				{
					result = string.CompareOrdinal(this.Description, customMenuKeyMapping.Description);
				}
				else
				{
					result = this.MappedKey - customMenuKeyMapping.MappedKey;
				}
			}
			return result;
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00030314 File Offset: 0x0002E514
		public override bool Equals(object obj)
		{
			CustomMenuKeyMapping customMenuKeyMapping = obj as CustomMenuKeyMapping;
			return customMenuKeyMapping != null && this.Equals(customMenuKeyMapping);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00030334 File Offset: 0x0002E534
		public bool Equals(CustomMenuKeyMapping comparand)
		{
			return comparand != null && string.Equals(this.ToString(), comparand.ToString(), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0003034D File Offset: 0x0002E54D
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x04000951 RID: 2385
		internal const string CustomGreetingFileRegEx = "^$|\\.wav|\\.wma$";

		// Token: 0x04000952 RID: 2386
		internal const int FileNameMaxLength = 255;

		// Token: 0x04000953 RID: 2387
		private string key;

		// Token: 0x04000954 RID: 2388
		private CustomMenuKey mappedKey;

		// Token: 0x04000955 RID: 2389
		private string description;

		// Token: 0x04000956 RID: 2390
		private string extension;

		// Token: 0x04000957 RID: 2391
		private string autoAttendantName;

		// Token: 0x04000958 RID: 2392
		private string promptFileName;

		// Token: 0x04000959 RID: 2393
		private string asrPhrases;

		// Token: 0x0400095A RID: 2394
		private string leaveVoicemailFor;

		// Token: 0x0400095B RID: 2395
		private string transferToMailbox;

		// Token: 0x0400095C RID: 2396
		private string legacyDNToUseForLeaveVoicemailFor;

		// Token: 0x0400095D RID: 2397
		private string legacyDNToUseForTransferToMailbox;

		// Token: 0x0400095E RID: 2398
		private string announceBusinessLocation;

		// Token: 0x0400095F RID: 2399
		private string announceBusinessHours;

		// Token: 0x020001C0 RID: 448
		private enum OptionSpecified
		{
			// Token: 0x04000961 RID: 2401
			None,
			// Token: 0x04000962 RID: 2402
			Single,
			// Token: 0x04000963 RID: 2403
			Multiple
		}

		// Token: 0x020001C1 RID: 449
		private enum KeyMappingTokens
		{
			// Token: 0x04000965 RID: 2405
			Invalid = -1,
			// Token: 0x04000966 RID: 2406
			Key,
			// Token: 0x04000967 RID: 2407
			Name,
			// Token: 0x04000968 RID: 2408
			Extension,
			// Token: 0x04000969 RID: 2409
			AutoAttendant,
			// Token: 0x0400096A RID: 2410
			PromptFileName,
			// Token: 0x0400096B RID: 2411
			AsrPhrases,
			// Token: 0x0400096C RID: 2412
			LeaveVoicemailFor,
			// Token: 0x0400096D RID: 2413
			TransferToMailbox,
			// Token: 0x0400096E RID: 2414
			AnnounceBusinessLocation,
			// Token: 0x0400096F RID: 2415
			AnnounceBusinessHours,
			// Token: 0x04000970 RID: 2416
			Count
		}
	}
}
