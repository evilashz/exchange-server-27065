using System;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004D5 RID: 1237
	internal sealed class Validation
	{
		// Token: 0x06002ADF RID: 10975 RVA: 0x000ABC58 File Offset: 0x000A9E58
		internal static void ValidateName(string name)
		{
			if (name.Length < 1 || name.Length > 260)
			{
				throw new IisTasksValidationStringLengthOutOfRangeException(Strings.IisTasksNameValidationProperty, 1, 260);
			}
			Validation.ValidateUnicode(name, Strings.IisTasksNameValidationProperty);
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000ABC91 File Offset: 0x000A9E91
		internal static void ValidateWebSite(string webSite)
		{
			if (webSite.Length < 1 || webSite.Length > 260)
			{
				throw new IisTasksValidationStringLengthOutOfRangeException(Strings.IisTasksWebSiteValidationProperty, 1, 260);
			}
			Validation.ValidateUnicode(webSite, Strings.IisTasksWebSiteValidationProperty);
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000ABCCC File Offset: 0x000A9ECC
		internal static void ValidateVirtualDirectory(string virtualDirectory)
		{
			if (virtualDirectory.Length < 1 || virtualDirectory.Length > 240)
			{
				throw new IisTasksValidationStringLengthOutOfRangeException(Strings.IisTasksVirtualDirectoryValidationProperty, 1, 240);
			}
			Validation.ValidateUnicode(virtualDirectory, Strings.IisTasksVirtualDirectoryValidationProperty);
			int num = virtualDirectory.IndexOfAny(Validation.invalidVirtualDirectoryChars);
			if (num != -1)
			{
				throw new IisTasksValidationInvalidVirtualDirectoryCharException(virtualDirectory, virtualDirectory[num], num, Validation.invalidVirtualDirectoryChars);
			}
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x000ABD34 File Offset: 0x000A9F34
		internal static void ValidateApplicationRoot(string applicationRoot)
		{
			if (applicationRoot.Length < 1 || applicationRoot.Length > 10000)
			{
				throw new IisTasksValidationStringLengthOutOfRangeException(Strings.IisTasksApplicationRootValidationProperty, 1, 10000);
			}
			Validation.ValidateUnicode(applicationRoot, Strings.IisTasksApplicationRootValidationProperty);
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x000ABD6D File Offset: 0x000A9F6D
		internal static void ValidateApplicationPool(string applicationPool)
		{
			if (applicationPool.Length < 1 || applicationPool.Length > 259)
			{
				throw new IisTasksValidationStringLengthOutOfRangeException(Strings.IisTasksApplicationPoolValidationProperty, 1, 259);
			}
			Validation.ValidateUnicode(applicationPool, Strings.IisTasksApplicationPoolValidationProperty);
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000ABDA8 File Offset: 0x000A9FA8
		private static void ValidateUnicode(string s, LocalizedString propertyName)
		{
			for (int i = 0; i < s.Length; i++)
			{
				for (int j = 0; j < Validation.invalidCategories.Length; j++)
				{
					if (char.GetUnicodeCategory(s, i) == Validation.invalidCategories[j])
					{
						throw new IisTasksValidationInvalidUnicodeException(propertyName, s, s[i], (int)s[i], i);
					}
				}
			}
		}

		// Token: 0x04001FF9 RID: 8185
		internal const int MinNameLength = 1;

		// Token: 0x04001FFA RID: 8186
		internal const int MaxNameLength = 260;

		// Token: 0x04001FFB RID: 8187
		internal const int MinWebSiteLength = 1;

		// Token: 0x04001FFC RID: 8188
		internal const int MaxWebSiteLength = 260;

		// Token: 0x04001FFD RID: 8189
		internal const int MinVirtualDirectoryLength = 1;

		// Token: 0x04001FFE RID: 8190
		internal const int MaxVirtualDirectoryLength = 240;

		// Token: 0x04001FFF RID: 8191
		internal const int MinApplicationPoolNameLength = 1;

		// Token: 0x04002000 RID: 8192
		internal const int MaxApplicationPoolNameLength = 259;

		// Token: 0x04002001 RID: 8193
		internal const int MinApplicationRootLength = 1;

		// Token: 0x04002002 RID: 8194
		internal const int MaxApplicationRootLength = 10000;

		// Token: 0x04002003 RID: 8195
		private static readonly char[] invalidVirtualDirectoryChars = "/?\\%*".ToCharArray();

		// Token: 0x04002004 RID: 8196
		private static readonly UnicodeCategory[] invalidCategories = new UnicodeCategory[]
		{
			UnicodeCategory.Control,
			UnicodeCategory.LineSeparator,
			UnicodeCategory.ParagraphSeparator
		};
	}
}
