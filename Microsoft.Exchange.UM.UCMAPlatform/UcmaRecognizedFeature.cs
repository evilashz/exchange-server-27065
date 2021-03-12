using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Speech.Recognition;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000050 RID: 80
	internal class UcmaRecognizedFeature : IUMRecognizedFeature
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x00011070 File Offset: 0x0000F270
		private UcmaRecognizedFeature(UcmaRecognizedFeature.SemanticValueBase semanticValue, int firstWordOffset, ICollection<UcmaReplacementText> replacementWordUnits)
		{
			this.name = semanticValue.Name;
			this.value = semanticValue.Value;
			this.firstWordIndex = semanticValue.FirstWordIndex;
			this.countOfWords = semanticValue.CountOfWords;
			List<UcmaReplacementText> list = new List<UcmaReplacementText>(1);
			List<UcmaReplacementText> list2 = new List<UcmaReplacementText>(10);
			foreach (UcmaReplacementText ucmaReplacementText in replacementWordUnits)
			{
				if (ucmaReplacementText.FirstWordIndex < this.firstWordIndex && this.firstWordIndex < ucmaReplacementText.FirstWordIndex + ucmaReplacementText.CountOfWords)
				{
					this.countOfWords += this.firstWordIndex - ucmaReplacementText.FirstWordIndex;
					this.firstWordIndex = ucmaReplacementText.FirstWordIndex;
				}
				if (ucmaReplacementText.FirstWordIndex < this.firstWordIndex + this.countOfWords && this.firstWordIndex + this.countOfWords < ucmaReplacementText.FirstWordIndex + ucmaReplacementText.CountOfWords)
				{
					this.countOfWords += ucmaReplacementText.FirstWordIndex + ucmaReplacementText.CountOfWords - (this.firstWordIndex + this.countOfWords);
				}
				if (this.firstWordIndex <= ucmaReplacementText.FirstWordIndex)
				{
					if (ucmaReplacementText.FirstWordIndex + ucmaReplacementText.CountOfWords > this.firstWordIndex + this.countOfWords)
					{
						break;
					}
					list.Add(ucmaReplacementText);
				}
				else
				{
					list2.Add(ucmaReplacementText);
				}
			}
			foreach (UcmaReplacementText ucmaReplacementText2 in list)
			{
				this.countOfWords -= ucmaReplacementText2.CountOfWords - 1;
			}
			foreach (UcmaReplacementText ucmaReplacementText3 in list2)
			{
				this.firstWordIndex -= ucmaReplacementText3.CountOfWords - 1;
			}
			this.firstWordIndex += firstWordOffset;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00011288 File Offset: 0x0000F488
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00011290 File Offset: 0x0000F490
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00011298 File Offset: 0x0000F498
		public int FirstWordIndex
		{
			get
			{
				return this.firstWordIndex;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003BA RID: 954 RVA: 0x000112A0 File Offset: 0x0000F4A0
		public int CountOfWords
		{
			get
			{
				return this.countOfWords;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000112A8 File Offset: 0x0000F4A8
		internal static bool TryCreate(KeyValuePair<string, SemanticValue> fragment, int firstWordOffset, ICollection<UcmaReplacementText> replacementWordUnits, out UcmaRecognizedFeature newFeature)
		{
			UcmaRecognizedFeature.SemanticValueBase semanticValue;
			if (UcmaRecognizedFeature.SemanticValueBase.TryCreate(fragment.Value, out semanticValue))
			{
				newFeature = new UcmaRecognizedFeature(semanticValue, firstWordOffset, replacementWordUnits);
			}
			else
			{
				newFeature = null;
			}
			return newFeature != null;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000112DC File Offset: 0x0000F4DC
		public static string ParseName(SemanticValue semanticValue)
		{
			return UcmaRecognizedFeature.GetAttributeString(semanticValue, "name");
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000112EC File Offset: 0x0000F4EC
		public static int ParseFirstWordIndex(SemanticValue semanticValue)
		{
			string attributeString = UcmaRecognizedFeature.GetAttributeString(semanticValue, "FirstWordIndex");
			return int.Parse(attributeString, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00011310 File Offset: 0x0000F510
		public static int ParseWordCount(SemanticValue semanticValue)
		{
			string attributeString = UcmaRecognizedFeature.GetAttributeString(semanticValue, "CountOfWords");
			return int.Parse(attributeString, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00011334 File Offset: 0x0000F534
		public static string ParsePhoneNumberSemanticValue(SemanticValue semanticValue)
		{
			return UcmaRecognizedFeature.PhoneNumberSemanticValue.ParseValue(semanticValue);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001133C File Offset: 0x0000F53C
		private static string GetAttributeString(SemanticValue semanticValue, string attributeName)
		{
			SemanticValue semanticValue2 = semanticValue["_attributes"];
			return (string)semanticValue2[attributeName].Value;
		}

		// Token: 0x04000122 RID: 290
		private readonly string name;

		// Token: 0x04000123 RID: 291
		private readonly int firstWordIndex;

		// Token: 0x04000124 RID: 292
		private readonly string value;

		// Token: 0x04000125 RID: 293
		private readonly int countOfWords;

		// Token: 0x02000051 RID: 81
		private class SemanticValueBase
		{
			// Token: 0x060003C1 RID: 961 RVA: 0x00011366 File Offset: 0x0000F566
			protected SemanticValueBase(SemanticValue semanticValue)
			{
				this.firstWordIndex = UcmaRecognizedFeature.ParseFirstWordIndex(semanticValue);
				this.countOfWords = UcmaRecognizedFeature.ParseWordCount(semanticValue);
			}

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x060003C2 RID: 962 RVA: 0x0001139C File Offset: 0x0000F59C
			internal int FirstWordIndex
			{
				get
				{
					return this.firstWordIndex;
				}
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x060003C3 RID: 963 RVA: 0x000113A4 File Offset: 0x0000F5A4
			internal int CountOfWords
			{
				get
				{
					return this.countOfWords;
				}
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x060003C4 RID: 964 RVA: 0x000113AC File Offset: 0x0000F5AC
			// (set) Token: 0x060003C5 RID: 965 RVA: 0x000113B4 File Offset: 0x0000F5B4
			protected internal string Name
			{
				get
				{
					return this.name;
				}
				protected set
				{
					this.name = value;
				}
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x060003C6 RID: 966 RVA: 0x000113BD File Offset: 0x0000F5BD
			// (set) Token: 0x060003C7 RID: 967 RVA: 0x000113C5 File Offset: 0x0000F5C5
			protected internal string Value
			{
				get
				{
					return this.value;
				}
				protected set
				{
					this.value = value;
				}
			}

			// Token: 0x060003C8 RID: 968 RVA: 0x000113D0 File Offset: 0x0000F5D0
			internal static bool TryCreate(SemanticValue semanticValue, out UcmaRecognizedFeature.SemanticValueBase newSemanticValue)
			{
				newSemanticValue = null;
				string a;
				if ((a = UcmaRecognizedFeature.ParseName(semanticValue)) != null)
				{
					if (!(a == "PhoneNumber"))
					{
						if (!(a == "PersonName"))
						{
							return false;
						}
						newSemanticValue = new UcmaRecognizedFeature.PersonNameSemanticValue(semanticValue);
					}
					else
					{
						newSemanticValue = new UcmaRecognizedFeature.PhoneNumberSemanticValue(semanticValue);
					}
					return null != newSemanticValue;
				}
				return false;
			}

			// Token: 0x04000126 RID: 294
			private readonly int firstWordIndex;

			// Token: 0x04000127 RID: 295
			private readonly int countOfWords;

			// Token: 0x04000128 RID: 296
			private string name = string.Empty;

			// Token: 0x04000129 RID: 297
			private string value = string.Empty;
		}

		// Token: 0x02000052 RID: 82
		private class PhoneNumberSemanticValue : UcmaRecognizedFeature.SemanticValueBase
		{
			// Token: 0x060003C9 RID: 969 RVA: 0x00011424 File Offset: 0x0000F624
			internal PhoneNumberSemanticValue(SemanticValue semanticValue) : base(semanticValue)
			{
				base.Name = "PhoneNumber";
				base.Value = UcmaRecognizedFeature.PhoneNumberSemanticValue.ParseValue(semanticValue);
			}

			// Token: 0x060003CA RID: 970 RVA: 0x00011444 File Offset: 0x0000F644
			internal static string ParseValue(SemanticValue semanticValue)
			{
				string result = string.Empty;
				if (semanticValue.ContainsKey("PhoneNumber"))
				{
					result = (string)semanticValue["PhoneNumber"].Value;
				}
				else
				{
					string str = string.Empty;
					string str2 = string.Empty;
					string str3 = string.Empty;
					if (semanticValue.ContainsKey("AreaCode"))
					{
						str = (string)semanticValue["AreaCode"].Value;
					}
					if (semanticValue.ContainsKey("LocalNumber"))
					{
						str2 = (string)semanticValue["LocalNumber"].Value;
					}
					if (semanticValue.ContainsKey("Extension"))
					{
						str3 = (string)semanticValue["Extension"].Value;
					}
					result = str + str2 + str3;
				}
				return result;
			}
		}

		// Token: 0x02000053 RID: 83
		private class DateSemanticValue : UcmaRecognizedFeature.SemanticValueBase
		{
			// Token: 0x060003CB RID: 971 RVA: 0x00011504 File Offset: 0x0000F704
			private DateSemanticValue(SemanticValue semanticValue, ExDateTime date) : base(semanticValue)
			{
				base.Name = "Date";
				base.Value = date.ToString("d", DateTimeFormatInfo.InvariantInfo);
			}

			// Token: 0x060003CC RID: 972 RVA: 0x00011530 File Offset: 0x0000F730
			internal new static bool TryCreate(SemanticValue semanticValue, out UcmaRecognizedFeature.SemanticValueBase dateSemantic)
			{
				dateSemantic = null;
				bool flag = bool.Parse((string)semanticValue["IsValidDate"].Value);
				if (flag)
				{
					int day = int.Parse((string)semanticValue["Day"].Value, NumberFormatInfo.InvariantInfo);
					int month = int.Parse((string)semanticValue["Month"].Value, NumberFormatInfo.InvariantInfo);
					int year = int.Parse((string)semanticValue["Year"].Value, NumberFormatInfo.InvariantInfo);
					ExDateTime date = new ExDateTime(ExTimeZone.UtcTimeZone, year, month, day);
					dateSemantic = new UcmaRecognizedFeature.DateSemanticValue(semanticValue, date);
				}
				return null != dateSemantic;
			}
		}

		// Token: 0x02000054 RID: 84
		private class TimeSemanticValue : UcmaRecognizedFeature.SemanticValueBase
		{
			// Token: 0x060003CD RID: 973 RVA: 0x000115E0 File Offset: 0x0000F7E0
			internal TimeSemanticValue(SemanticValue semanticValue) : base(semanticValue)
			{
				base.Name = "Time";
				int hour = int.Parse((string)semanticValue["Hour"].Value, NumberFormatInfo.InvariantInfo);
				int minute = int.Parse((string)semanticValue["Minute"].Value, NumberFormatInfo.InvariantInfo);
				ExDateTime exDateTime = new ExDateTime(ExTimeZone.UtcTimeZone, ExDateTime.UtcNow.Year, ExDateTime.UtcNow.Month, ExDateTime.UtcNow.Day, hour, minute, 0);
				base.Value = exDateTime.ToString("t", DateTimeFormatInfo.InvariantInfo);
			}
		}

		// Token: 0x02000055 RID: 85
		private class PersonNameSemanticValue : UcmaRecognizedFeature.SemanticValueBase
		{
			// Token: 0x060003CE RID: 974 RVA: 0x00011690 File Offset: 0x0000F890
			internal PersonNameSemanticValue(SemanticValue semanticValue) : base(semanticValue)
			{
				if (semanticValue.ContainsKey("Mailbox"))
				{
					base.Name = "Mailbox";
					base.Value = (string)semanticValue["Mailbox"].Value;
					return;
				}
				if (semanticValue.ContainsKey("Contact"))
				{
					base.Name = "Contact";
					base.Value = (string)semanticValue["Contact"].Value;
					return;
				}
				base.Name = "PersonName";
				SemanticValue semanticValue2 = semanticValue["_attributes"];
				base.Value = (string)semanticValue2["text"].Value;
			}
		}
	}
}
