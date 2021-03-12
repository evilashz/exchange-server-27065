using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200010E RID: 270
	internal static class FolderFilterParser
	{
		// Token: 0x06000992 RID: 2450 RVA: 0x00013181 File Offset: 0x00011381
		public static string GetAlias(WellKnownFolderType wkfType)
		{
			return '#' + wkfType.ToString() + '#';
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x000131A4 File Offset: 0x000113A4
		public static void Parse(string folderPath, out WellKnownFolderType root, out List<string> folderNames, out FolderMappingFlags inheritanceFlags)
		{
			folderNames = new List<string>();
			root = WellKnownFolderType.IpmSubtree;
			inheritanceFlags = FolderMappingFlags.None;
			if (folderPath.EndsWith("/"))
			{
				folderPath = folderPath.Substring(0, folderPath.Length - "/".Length);
			}
			else if (folderPath.EndsWith("/*"))
			{
				folderPath = folderPath.Substring(0, folderPath.Length - "/*".Length);
				inheritanceFlags = FolderMappingFlags.Inherit;
			}
			bool flag = true;
			int i = 0;
			while (i < folderPath.Length)
			{
				string nextSubfolderName = FolderFilterParser.GetNextSubfolderName(folderPath, ref i);
				if (flag)
				{
					WellKnownFolderType wellKnownFolderType = WellKnownFolderType.None;
					if (folderPath[0] != '\\')
					{
						wellKnownFolderType = FolderFilterParser.CheckAlias(nextSubfolderName);
					}
					if (wellKnownFolderType != WellKnownFolderType.None)
					{
						root = wellKnownFolderType;
					}
					else
					{
						folderNames.Add(nextSubfolderName);
					}
					flag = false;
				}
				else
				{
					folderNames.Add(nextSubfolderName);
				}
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001325C File Offset: 0x0001145C
		public static bool IsDumpster(WellKnownFolderType folderType)
		{
			return FolderFilterParser.dumpsterIds.Contains(folderType);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001326C File Offset: 0x0001146C
		private static string GetNextSubfolderName(string folderPath, ref int curIndex)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			int num = curIndex;
			while (curIndex < folderPath.Length)
			{
				if (folderPath[curIndex] == '\\')
				{
					stringBuilder.Append(folderPath.Substring(num, curIndex - num));
					curIndex++;
					if (curIndex == folderPath.Length)
					{
						throw new FolderPathIsInvalidPermanentException(folderPath);
					}
					if (char.IsLetterOrDigit(folderPath[curIndex]))
					{
						throw new InvalidEscapedCharPermanentException(folderPath, curIndex);
					}
					num = curIndex;
				}
				else if (folderPath[curIndex] == '/')
				{
					break;
				}
				curIndex++;
			}
			stringBuilder.Append(folderPath.Substring(num, curIndex - num));
			if (curIndex < folderPath.Length)
			{
				curIndex++;
			}
			if (stringBuilder.Length == 0)
			{
				throw new FolderPathIsInvalidPermanentException(folderPath);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00013330 File Offset: 0x00011530
		private static WellKnownFolderType CheckAlias(string subfolderName)
		{
			if (subfolderName.Length < 2 || subfolderName[0] != '#' || subfolderName[subfolderName.Length - 1] != '#')
			{
				return WellKnownFolderType.None;
			}
			string text = subfolderName.Substring(1, subfolderName.Length - 2);
			WellKnownFolderType result;
			if (!Enum.TryParse<WellKnownFolderType>(text, true, out result))
			{
				throw new FolderAliasIsInvalidPermanentException(text);
			}
			return result;
		}

		// Token: 0x0400058A RID: 1418
		public const char FolderAliasMarker = '#';

		// Token: 0x0400058B RID: 1419
		public const char FolderSeparator = '/';

		// Token: 0x0400058C RID: 1420
		public const char FolderEscapeChar = '\\';

		// Token: 0x0400058D RID: 1421
		public const string ThisFolderOnlyInheritanceIndicator = "/";

		// Token: 0x0400058E RID: 1422
		public const string FolderAndSubfoldersInheritanceIndicator = "/*";

		// Token: 0x0400058F RID: 1423
		private static readonly HashSet<WellKnownFolderType> dumpsterIds = new HashSet<WellKnownFolderType>(new WellKnownFolderType[]
		{
			WellKnownFolderType.Dumpster,
			WellKnownFolderType.DumpsterDeletions,
			WellKnownFolderType.DumpsterVersions,
			WellKnownFolderType.DumpsterPurges,
			WellKnownFolderType.DumpsterAdminAuditLogs,
			WellKnownFolderType.DumpsterAudits,
			WellKnownFolderType.CalendarLogging
		});
	}
}
