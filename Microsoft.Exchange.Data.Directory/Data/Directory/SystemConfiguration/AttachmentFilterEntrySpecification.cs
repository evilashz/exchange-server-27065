using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003AC RID: 940
	[Serializable]
	public class AttachmentFilterEntrySpecification : IConfigurable
	{
		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06002B18 RID: 11032 RVA: 0x000B33CA File Offset: 0x000B15CA
		public AttachmentType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x000B33D2 File Offset: 0x000B15D2
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06002B1A RID: 11034 RVA: 0x000B33DA File Offset: 0x000B15DA
		public string Identity
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000B33E2 File Offset: 0x000B15E2
		public AttachmentFilterEntrySpecification(AttachmentType type, string name)
		{
			this.type = type;
			this.name = name;
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000B33F8 File Offset: 0x000B15F8
		private AttachmentFilterEntrySpecification()
		{
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000B3400 File Offset: 0x000B1600
		public override string ToString()
		{
			return this.type.ToString() + ":" + this.name;
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000B3424 File Offset: 0x000B1624
		internal static AttachmentFilterEntrySpecification Parse(string storedAttribute)
		{
			AttachmentFilterEntrySpecification attachmentFilterEntrySpecification = new AttachmentFilterEntrySpecification();
			if (storedAttribute.StartsWith(AttachmentType.ContentType.ToString() + ":") && storedAttribute.Length >= AttachmentType.ContentType.ToString().Length + 2)
			{
				attachmentFilterEntrySpecification.type = AttachmentType.ContentType;
				attachmentFilterEntrySpecification.name = storedAttribute.Substring(AttachmentType.ContentType.ToString().Length + 1);
				return attachmentFilterEntrySpecification;
			}
			if (storedAttribute.StartsWith(AttachmentType.FileName.ToString() + ":") && storedAttribute.Length >= AttachmentType.FileName.ToString().Length + 2)
			{
				attachmentFilterEntrySpecification.type = AttachmentType.FileName;
				attachmentFilterEntrySpecification.name = storedAttribute.Substring(AttachmentType.FileName.ToString().Length + 1);
				return attachmentFilterEntrySpecification;
			}
			throw new InvalidDataException(DirectoryStrings.AttachmentFilterEntryInvalid.ToString());
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x000B350C File Offset: 0x000B170C
		internal static void ParseFileSpec(string fileSpec, out string blockedExtension, out Regex blockedExpression, out string blockedFileName)
		{
			blockedExtension = null;
			blockedExpression = null;
			blockedFileName = null;
			if (fileSpec.StartsWith("*.") && fileSpec.Length > 2)
			{
				string text = fileSpec.Substring(2);
				if (AttachmentFilterEntrySpecification.Utils.IsRegex(text))
				{
					throw new InvalidDataException(DirectoryStrings.InvalidAttachmentFilterExtension(fileSpec));
				}
				text = AttachmentFilterEntrySpecification.Utils.Unescape(text);
				blockedExtension = "." + text;
				return;
			}
			else
			{
				if (AttachmentFilterEntrySpecification.Utils.IsRegex(fileSpec))
				{
					string pattern = string.Format("^{0}$", fileSpec);
					Regex regex;
					try
					{
						regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
					}
					catch (ArgumentException innerException)
					{
						throw new InvalidDataException(DirectoryStrings.InvalidAttachmentFilterRegex(fileSpec), innerException);
					}
					blockedExpression = regex;
					return;
				}
				blockedFileName = AttachmentFilterEntrySpecification.Utils.Unescape(fileSpec);
				return;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06002B20 RID: 11040 RVA: 0x000B35C0 File Offset: 0x000B17C0
		ObjectId IConfigurable.Identity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x000B35C3 File Offset: 0x000B17C3
		ValidationError[] IConfigurable.Validate()
		{
			return ValidationError.None;
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06002B22 RID: 11042 RVA: 0x000B35CA File Offset: 0x000B17CA
		bool IConfigurable.IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06002B23 RID: 11043 RVA: 0x000B35CD File Offset: 0x000B17CD
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x000B35D0 File Offset: 0x000B17D0
		void IConfigurable.CopyChangesFrom(IConfigurable source)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x000B35D7 File Offset: 0x000B17D7
		void IConfigurable.ResetChangeTracking()
		{
			throw new NotSupportedException();
		}

		// Token: 0x040019E0 RID: 6624
		private AttachmentType type;

		// Token: 0x040019E1 RID: 6625
		private string name;

		// Token: 0x020003AD RID: 941
		internal sealed class Utils
		{
			// Token: 0x06002B26 RID: 11046 RVA: 0x000B35E0 File Offset: 0x000B17E0
			internal static bool IsRegex(string expression)
			{
				for (int i = 0; i < expression.Length; i++)
				{
					if (AttachmentFilterEntrySpecification.Utils.IsSpecial(expression[i]) && !AttachmentFilterEntrySpecification.Utils.IsEscapedCharacter(expression, i))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06002B27 RID: 11047 RVA: 0x000B3618 File Offset: 0x000B1818
			internal static bool IsEscapedCharacter(string expression, int position)
			{
				int num = 0;
				int num2 = position - 1;
				while (num2 >= 0 && expression[num2] == '\\')
				{
					num++;
					num2--;
				}
				return num % 2 != 0;
			}

			// Token: 0x06002B28 RID: 11048 RVA: 0x000B3650 File Offset: 0x000B1850
			internal static bool IsSpecial(char ch)
			{
				for (int i = 0; i < "*+?(|).[]".Length; i++)
				{
					if (ch == "*+?(|).[]"[i])
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06002B29 RID: 11049 RVA: 0x000B3684 File Offset: 0x000B1884
			internal static string Unescape(string expression)
			{
				StringBuilder stringBuilder = new StringBuilder();
				int i = 0;
				while (i < expression.Length)
				{
					if (expression[i] == '\\')
					{
						if (i >= expression.Length - 1)
						{
							string message = string.Format("Hit end of string '{0}' and there is no next character to unescape", expression);
							throw new InvalidDataException(message);
						}
						stringBuilder.Append(expression[i + 1]);
						i += 2;
					}
					else
					{
						stringBuilder.Append(expression[i]);
						i++;
					}
				}
				return stringBuilder.ToString();
			}

			// Token: 0x040019E2 RID: 6626
			private const string SpecialChars = "*+?(|).[]";
		}
	}
}
