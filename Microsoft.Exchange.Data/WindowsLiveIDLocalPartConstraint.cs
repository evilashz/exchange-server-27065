using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001E6 RID: 486
	[Serializable]
	internal class WindowsLiveIDLocalPartConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x060010C3 RID: 4291 RVA: 0x00033293 File Offset: 0x00031493
		public WindowsLiveIDLocalPartConstraint(bool allowEmptyLocalPart)
		{
			this.allowEmptyLocalPart = allowEmptyLocalPart;
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x000332A4 File Offset: 0x000314A4
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (value != null)
			{
				string text = value.ToString();
				if (this.allowEmptyLocalPart && string.IsNullOrEmpty(text))
				{
					return null;
				}
				if (!WindowsLiveIDLocalPartConstraint.IsValidLocalPartOfWindowsLiveID(text))
				{
					return new PropertyConstraintViolationError(DataStrings.ConstraintViolationInvalidWindowsLiveIDLocalPart, propertyDefinition, value, this);
				}
			}
			return null;
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x000332E4 File Offset: 0x000314E4
		public static bool IsValidLocalPartOfWindowsLiveID(string liveID)
		{
			if (string.IsNullOrEmpty(liveID) || !char.IsLetter(liveID[0]) || liveID[liveID.Length - 1] == '.' || liveID.IndexOf("..") >= 0 || liveID.Length > 63)
			{
				return false;
			}
			foreach (char c in liveID)
			{
				if (!WindowsLiveIDLocalPartConstraint.IsValidCharForWindowsLiveID(c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00033360 File Offset: 0x00031560
		public static string RemoveInvalidPartOfWindowsLiveID(string liveID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(liveID))
			{
				foreach (char c in liveID)
				{
					if (stringBuilder.Length >= 63)
					{
						break;
					}
					if (WindowsLiveIDLocalPartConstraint.IsValidCharForWindowsLiveID(c) && (stringBuilder.Length != 0 || char.IsLetter(c)) && (stringBuilder.Length <= 0 || c != '.' || c != stringBuilder[stringBuilder.Length - 1]))
					{
						stringBuilder.Append(c);
					}
				}
				if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '.')
				{
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00033413 File Offset: 0x00031613
		private static bool IsValidCharForWindowsLiveID(char c)
		{
			return c <= 'ÿ' && (char.IsLetterOrDigit(c) || c == '.' || c == '-' || c == '_');
		}

		// Token: 0x04000A71 RID: 2673
		public const int MaxLengthOfLiveID = 63;

		// Token: 0x04000A72 RID: 2674
		private readonly bool allowEmptyLocalPart;
	}
}
