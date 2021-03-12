using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP.Client
{
	// Token: 0x020001DB RID: 475
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class IMAPUtils
	{
		// Token: 0x06000EA8 RID: 3752 RVA: 0x000281C8 File Offset: 0x000263C8
		internal static string ToModifiedUTF7(string decodedString)
		{
			if (string.IsNullOrEmpty(decodedString))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(decodedString.Length * 3);
			char[] array = null;
			for (int i = 0; i < decodedString.Length; i++)
			{
				if (decodedString[i] <= '\u007f' && decodedString[i] != IMAPUtils.cShiftUTF7)
				{
					int num = i;
					while (i < decodedString.Length && decodedString[i] <= '\u007f' && decodedString[i] != IMAPUtils.cShiftUTF7)
					{
						i++;
					}
					if (num == 0 && i == decodedString.Length)
					{
						return decodedString;
					}
					stringBuilder.Append(decodedString.Substring(num, i - num));
					i--;
				}
				else if (decodedString[i] == IMAPUtils.cShiftUTF7)
				{
					stringBuilder.Append(IMAPUtils.cShiftUTF7);
					stringBuilder.Append(IMAPUtils.cShiftASCII);
				}
				else
				{
					if (array == null)
					{
						array = decodedString.ToCharArray();
					}
					int num = i;
					while (i < decodedString.Length && decodedString[i] > '\u007f')
					{
						i++;
					}
					byte[] bytes = Encoding.UTF7.GetBytes(array, num, i - num);
					bytes[0] = (byte)IMAPUtils.cShiftUTF7;
					for (int j = 0; j < bytes.Length; j++)
					{
						if (bytes[j] == IMAPUtils.bSlash)
						{
							stringBuilder.Append(IMAPUtils.cComma);
						}
						else
						{
							stringBuilder.Append((char)bytes[j]);
						}
					}
					i--;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00028324 File Offset: 0x00026524
		internal static bool FromModifiedUTF7(string encodedString, out string decodedString)
		{
			decodedString = string.Empty;
			if (string.IsNullOrEmpty(encodedString))
			{
				return true;
			}
			StringBuilder stringBuilder = new StringBuilder(encodedString.Length);
			byte[] array = null;
			int num = 0;
			int i = encodedString.IndexOf(IMAPUtils.cShiftUTF7);
			while (i > -1)
			{
				if (i > encodedString.Length - 1)
				{
					return false;
				}
				if (num < i)
				{
					stringBuilder.Append(encodedString.Substring(num, i - num));
				}
				num = i;
				i = encodedString.IndexOf(IMAPUtils.cShiftASCII, num);
				if (i == -1)
				{
					return false;
				}
				if (num + 1 == i)
				{
					stringBuilder.Append(IMAPUtils.cShiftUTF7);
					num = i + 1;
					i = encodedString.IndexOf(IMAPUtils.cShiftUTF7, num);
				}
				else
				{
					if (array == null)
					{
						array = Encoding.ASCII.GetBytes(encodedString);
					}
					array[num] = (byte)IMAPUtils.cOriginalShiftUTF7;
					for (int j = num; j < i; j++)
					{
						if (array[j] == IMAPUtils.bComma)
						{
							array[j] = IMAPUtils.bSlash;
						}
					}
					string @string = Encoding.UTF7.GetString(array, num, i - num);
					if (@string.Length == 0)
					{
						return false;
					}
					stringBuilder.Append(@string);
					num = i + 1;
					i = encodedString.IndexOf(IMAPUtils.cShiftUTF7, num);
				}
			}
			if (num == 0)
			{
				decodedString = encodedString;
				return true;
			}
			if (num < encodedString.Length)
			{
				stringBuilder.Append(encodedString.Substring(num));
			}
			decodedString = stringBuilder.ToString();
			return true;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00028460 File Offset: 0x00026660
		internal static string CreateEmailCloudId(IMAPFolder folder, string messageId)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				folder.Uniqueness,
				messageId
			});
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00028498 File Offset: 0x00026698
		internal static string CreateEmailCloudVersion(IMAPFolder folder, string uid, IMAPMailFlags flags)
		{
			flags = IMAPUtils.FilterFlagsAgainstSupported(flags, folder.Mailbox);
			return string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				uid,
				(uint)flags
			});
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x000284D8 File Offset: 0x000266D8
		internal static bool GetMessageIdFromCloudId(string cloudId, out string messageId)
		{
			int num = cloudId.IndexOf(' ');
			if (num >= 0)
			{
				messageId = cloudId.Substring(num + 1);
				return true;
			}
			messageId = cloudId;
			return false;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00028504 File Offset: 0x00026704
		internal static bool GetUidFromEmailCloudVersion(string cloudVersion, out string uid)
		{
			if (cloudVersion == null)
			{
				uid = null;
				return false;
			}
			int num = cloudVersion.IndexOf(' ');
			if (num < 0)
			{
				uid = null;
				return false;
			}
			string s = cloudVersion.Substring(0, num);
			uint num2 = 0U;
			if (!uint.TryParse(s, out num2))
			{
				uid = null;
				return false;
			}
			uid = num2.ToString(CultureInfo.InvariantCulture);
			return true;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00028558 File Offset: 0x00026758
		internal static bool GetFlagsFromCloudVersion(string cloudVersion, IMAPMailbox mailbox, out IMAPMailFlags flags)
		{
			int num = cloudVersion.IndexOf(' ');
			if (num < 0)
			{
				flags = IMAPMailFlags.None;
				return false;
			}
			string s = cloudVersion.Substring(num + 1);
			uint incomingFlags = 0U;
			if (!uint.TryParse(s, out incomingFlags))
			{
				flags = IMAPMailFlags.None;
				return false;
			}
			flags = IMAPUtils.FilterFlagsAgainstSupported((IMAPMailFlags)incomingFlags, mailbox);
			return true;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x000285A0 File Offset: 0x000267A0
		internal static void UpdateUidInCloudVersion(string uid, ref string cloudVersion)
		{
			int num = cloudVersion.IndexOf(' ');
			if (num < 0)
			{
				cloudVersion = uid + " 0";
				return;
			}
			string str = cloudVersion.Substring(num);
			cloudVersion = uid + str;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x000285DC File Offset: 0x000267DC
		internal static bool TryUpdateFlagsInCloudVersion(IMAPMailFlags flags, IMAPMailbox mailbox, ref string cloudVersion)
		{
			int num = cloudVersion.IndexOf(' ');
			if (num < 0)
			{
				return false;
			}
			IMAPMailFlags imapmailFlags = IMAPUtils.FilterFlagsAgainstSupported(flags, mailbox);
			string text = cloudVersion.Substring(0, num + 1);
			string str = text;
			int num2 = (int)imapmailFlags;
			cloudVersion = str + num2.ToString(CultureInfo.InvariantCulture);
			return true;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00028624 File Offset: 0x00026824
		internal static void AppendStringBuilderIMAPFlags(IMAPMailFlags flags, StringBuilder builderToUse)
		{
			builderToUse.Append('(');
			string value = string.Empty;
			if ((flags & IMAPMailFlags.Answered) == IMAPMailFlags.Answered)
			{
				builderToUse.Append(value);
				builderToUse.Append("\\Answered");
				value = " ";
			}
			if ((flags & IMAPMailFlags.Deleted) == IMAPMailFlags.Deleted)
			{
				builderToUse.Append(value);
				builderToUse.Append("\\Deleted");
				value = " ";
			}
			if ((flags & IMAPMailFlags.Draft) == IMAPMailFlags.Draft)
			{
				builderToUse.Append(value);
				builderToUse.Append("\\Draft");
				value = " ";
			}
			if ((flags & IMAPMailFlags.Flagged) == IMAPMailFlags.Flagged)
			{
				builderToUse.Append(value);
				builderToUse.Append("\\Flagged");
				value = " ";
			}
			if ((flags & IMAPMailFlags.Seen) == IMAPMailFlags.Seen)
			{
				builderToUse.Append(value);
				builderToUse.Append("\\Seen");
			}
			builderToUse.Append(')');
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x000286EC File Offset: 0x000268EC
		internal static IMAPMailFlags ConvertStringFormToFlags(string stringForm)
		{
			IMAPMailFlags imapmailFlags = IMAPMailFlags.None;
			if (!string.IsNullOrEmpty(stringForm))
			{
				string[] array = stringForm.ToUpperInvariant().Trim(new char[]
				{
					'(',
					')'
				}).Split(new char[]
				{
					' '
				});
				foreach (string text in array)
				{
					string a;
					if ((a = text) != null)
					{
						if (!(a == "\\ANSWERED"))
						{
							if (!(a == "\\DELETED"))
							{
								if (!(a == "\\DRAFT"))
								{
									if (!(a == "\\FLAGGED"))
									{
										if (a == "\\SEEN")
										{
											imapmailFlags |= IMAPMailFlags.Seen;
										}
									}
									else
									{
										imapmailFlags |= IMAPMailFlags.Flagged;
									}
								}
								else
								{
									imapmailFlags |= IMAPMailFlags.Draft;
								}
							}
							else
							{
								imapmailFlags |= IMAPMailFlags.Deleted;
							}
						}
						else
						{
							imapmailFlags |= IMAPMailFlags.Answered;
						}
					}
				}
			}
			return imapmailFlags;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x000287C4 File Offset: 0x000269C4
		internal static IMAPMailFlags FilterFlagsAgainstSupported(IMAPMailFlags incomingFlags, IMAPMailbox mailbox)
		{
			IMAPMailFlags imapmailFlags = IMAPMailFlags.All;
			if (mailbox != null)
			{
				imapmailFlags = mailbox.PermanentFlags;
			}
			return incomingFlags & imapmailFlags;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x000287E4 File Offset: 0x000269E4
		internal static void LogExceptionDetails(SyncLogSession log, Trace tracer, IMAPCommand failingCommand, Exception failure)
		{
			Exception ex = failure;
			while (ex.InnerException != null)
			{
				ex = ex.InnerException;
			}
			log.LogError((TSLID)884UL, tracer, "While executing [{0}]: {1}", new object[]
			{
				failingCommand.ToPiiCleanString(),
				ex.Message
			});
			string stackTrace = ex.StackTrace;
			if (stackTrace != null && stackTrace.Length > 0)
			{
				log.LogError((TSLID)885UL, tracer, "Stack [{0}]", new object[]
				{
					stackTrace
				});
			}
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0002886C File Offset: 0x00026A6C
		internal static void LogExceptionDetails(SyncLogSession log, Trace tracer, string errPrefix, Exception failure)
		{
			Exception ex = failure;
			while (ex.InnerException != null)
			{
				ex = ex.InnerException;
			}
			log.LogError((TSLID)886UL, tracer, "{0}: {1}", new object[]
			{
				errPrefix,
				ex.Message
			});
			string stackTrace = ex.StackTrace;
			if (stackTrace != null && stackTrace.Length > 0)
			{
				log.LogError((TSLID)887UL, tracer, "{0}: Stack [{1}]", new object[]
				{
					errPrefix,
					stackTrace
				});
			}
		}

		// Token: 0x0400083B RID: 2107
		private const string AnsweredFlag = "\\Answered";

		// Token: 0x0400083C RID: 2108
		private const string AnswerFlagUpper = "\\ANSWERED";

		// Token: 0x0400083D RID: 2109
		private const string DeletedFlag = "\\Deleted";

		// Token: 0x0400083E RID: 2110
		private const string DeletedFlagUpper = "\\DELETED";

		// Token: 0x0400083F RID: 2111
		private const string DraftFlag = "\\Draft";

		// Token: 0x04000840 RID: 2112
		private const string DraftFlagUpper = "\\DRAFT";

		// Token: 0x04000841 RID: 2113
		private const string FlaggedFlag = "\\Flagged";

		// Token: 0x04000842 RID: 2114
		private const string FlaggedFlagUpper = "\\FLAGGED";

		// Token: 0x04000843 RID: 2115
		private const string SeenFlag = "\\Seen";

		// Token: 0x04000844 RID: 2116
		private const string SeenFlagUpper = "\\SEEN";

		// Token: 0x04000845 RID: 2117
		private static char cShiftUTF7 = '&';

		// Token: 0x04000846 RID: 2118
		private static char cOriginalShiftUTF7 = '+';

		// Token: 0x04000847 RID: 2119
		private static char cShiftASCII = '-';

		// Token: 0x04000848 RID: 2120
		private static char cSlash = '/';

		// Token: 0x04000849 RID: 2121
		private static byte bSlash = (byte)IMAPUtils.cSlash;

		// Token: 0x0400084A RID: 2122
		private static char cComma = ',';

		// Token: 0x0400084B RID: 2123
		private static byte bComma = (byte)IMAPUtils.cComma;
	}
}
