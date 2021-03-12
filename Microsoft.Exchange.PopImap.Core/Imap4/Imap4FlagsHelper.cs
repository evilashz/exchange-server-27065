using System;
using System.Text;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000019 RID: 25
	internal sealed class Imap4FlagsHelper
	{
		// Token: 0x06000154 RID: 340 RVA: 0x00005518 File Offset: 0x00003718
		public static bool TryParse(string flagString, out Imap4Flags flags, out bool containsKeywords)
		{
			flags = Imap4Flags.None;
			containsKeywords = false;
			if (!string.IsNullOrEmpty(flagString))
			{
				if (flagString[0] == '(' && flagString[flagString.Length - 1] == ')')
				{
					flagString = flagString.Substring(1, flagString.Length - 2);
					if (string.IsNullOrEmpty(flagString))
					{
						return true;
					}
				}
				string[] array = flagString.Split(Imap4FlagsHelper.wordDelimiter);
				for (int i = 0; i < array.Length; i++)
				{
					if (string.Compare(array[i], "\\Recent", StringComparison.OrdinalIgnoreCase) == 0)
					{
						flags |= Imap4Flags.Recent;
					}
					else if (string.Compare(array[i], "\\Seen", StringComparison.OrdinalIgnoreCase) == 0)
					{
						flags |= Imap4Flags.Seen;
					}
					else if (string.Compare(array[i], "\\Deleted", StringComparison.OrdinalIgnoreCase) == 0)
					{
						flags |= Imap4Flags.Deleted;
					}
					else if (string.Compare(array[i], "\\Answered", StringComparison.OrdinalIgnoreCase) == 0)
					{
						flags |= Imap4Flags.Answered;
					}
					else if (string.Compare(array[i], "\\Draft", StringComparison.OrdinalIgnoreCase) == 0)
					{
						flags |= Imap4Flags.Draft;
					}
					else if (string.Compare(array[i], "\\Flagged", StringComparison.OrdinalIgnoreCase) == 0)
					{
						flags |= Imap4Flags.Flagged;
					}
					else if (string.Compare(array[i], "$MDNSent", StringComparison.OrdinalIgnoreCase) == 0)
					{
						flags |= Imap4Flags.MdnSent;
					}
					else if (string.Compare(array[i], "\\Wildcard", StringComparison.OrdinalIgnoreCase) == 0)
					{
						flags |= Imap4Flags.Wildcard;
					}
					else
					{
						if (array[i].Length <= 0 || array[i].IndexOfAny(Imap4FlagsHelper.specialCharacters) != -1)
						{
							return false;
						}
						containsKeywords = true;
					}
				}
			}
			return true;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005684 File Offset: 0x00003884
		internal static Imap4Flags Parse(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item is null");
			}
			object delMarked = item.TryGetProperty(MessageItemSchema.MessageDelMarked);
			object answered = item.TryGetProperty(MessageItemSchema.MessageAnswered);
			object tagged = item.TryGetProperty(MessageItemSchema.MessageTagged);
			object notificationSent = item.TryGetProperty(MessageItemSchema.MessageDeliveryNotificationSent);
			object conversionFailed = item.TryGetProperty(MessageItemSchema.MimeConversionFailed);
			object isDraft = item.TryGetProperty(MessageItemSchema.IsDraft);
			object isRead = item.TryGetProperty(MessageItemSchema.IsRead);
			return Imap4FlagsHelper.Parse(delMarked, answered, tagged, notificationSent, conversionFailed, isDraft, isRead);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005708 File Offset: 0x00003908
		internal static Imap4Flags Parse(object delMarked, object answered, object tagged, object notificationSent, object conversionFailed, object isDraft, object isRead)
		{
			Imap4Flags result = Imap4Flags.None;
			Imap4FlagsHelper.SetFlagBit(ref result, delMarked, Imap4Flags.Deleted);
			Imap4FlagsHelper.SetFlagBit(ref result, answered, Imap4Flags.Answered);
			Imap4FlagsHelper.SetFlagBit(ref result, tagged, Imap4Flags.Flagged);
			Imap4FlagsHelper.SetFlagBit(ref result, notificationSent, Imap4Flags.MdnSent);
			Imap4FlagsHelper.SetFlagBit(ref result, conversionFailed, Imap4Flags.MimeFailed);
			Imap4FlagsHelper.SetFlagBit(ref result, isDraft, Imap4Flags.Draft);
			Imap4FlagsHelper.SetFlagBit(ref result, isRead, Imap4Flags.Seen);
			return result;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005764 File Offset: 0x00003964
		internal static void Apply(Item item, Imap4Flags flags)
		{
			item[MessageItemSchema.MessageDelMarked] = ((flags & Imap4Flags.Deleted) != Imap4Flags.None);
			item[MessageItemSchema.MessageAnswered] = ((flags & Imap4Flags.Answered) != Imap4Flags.None);
			item[MessageItemSchema.MessageTagged] = ((flags & Imap4Flags.Flagged) != Imap4Flags.None);
			item[MessageItemSchema.MessageDeliveryNotificationSent] = ((flags & Imap4Flags.MdnSent) != Imap4Flags.None);
			item[MessageItemSchema.MimeConversionFailed] = ((flags & Imap4Flags.MimeFailed) != Imap4Flags.None);
			item[MessageItemSchema.IsDraft] = ((flags & Imap4Flags.Draft) != Imap4Flags.None);
			item[MessageItemSchema.IsRead] = ((flags & Imap4Flags.Seen) != Imap4Flags.None);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005828 File Offset: 0x00003A28
		internal static string ToString(Imap4Flags flags)
		{
			int num = (int)(flags & (Imap4Flags.Recent | Imap4Flags.Seen | Imap4Flags.Deleted | Imap4Flags.Answered | Imap4Flags.Draft | Imap4Flags.Flagged | Imap4Flags.MdnSent | Imap4Flags.Wildcard));
			string text = Imap4FlagsHelper.flagStrings[num];
			if (text != null)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append('(');
			if ((flags & Imap4Flags.Seen) != Imap4Flags.None)
			{
				Imap4FlagsHelper.AddString(stringBuilder, "\\Seen");
			}
			if ((flags & Imap4Flags.Answered) != Imap4Flags.None)
			{
				Imap4FlagsHelper.AddString(stringBuilder, "\\Answered");
			}
			if ((flags & Imap4Flags.Flagged) != Imap4Flags.None)
			{
				Imap4FlagsHelper.AddString(stringBuilder, "\\Flagged");
			}
			if ((flags & Imap4Flags.Deleted) != Imap4Flags.None)
			{
				Imap4FlagsHelper.AddString(stringBuilder, "\\Deleted");
			}
			if ((flags & Imap4Flags.Draft) != Imap4Flags.None)
			{
				Imap4FlagsHelper.AddString(stringBuilder, "\\Draft");
			}
			if ((flags & Imap4Flags.MdnSent) != Imap4Flags.None)
			{
				Imap4FlagsHelper.AddString(stringBuilder, "$MDNSent");
			}
			if ((flags & Imap4Flags.Wildcard) != Imap4Flags.None)
			{
				Imap4FlagsHelper.AddString(stringBuilder, "\\Wildcard");
			}
			if ((flags & Imap4Flags.Recent) != Imap4Flags.None)
			{
				Imap4FlagsHelper.AddString(stringBuilder, "\\Recent");
			}
			stringBuilder.Append(')');
			text = stringBuilder.ToString();
			Imap4FlagsHelper.flagStrings[num] = text;
			return text;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000058FE File Offset: 0x00003AFE
		private static void AddString(StringBuilder stringBuilder, string str)
		{
			if (stringBuilder.Length > 1)
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append(str);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000591A File Offset: 0x00003B1A
		private static void SetFlagBit(ref Imap4Flags flags, object bitObject, Imap4Flags flagBit)
		{
			if (!(bitObject is PropertyError) && (bool)bitObject)
			{
				flags |= flagBit;
			}
		}

		// Token: 0x040000B2 RID: 178
		internal const string Recent = "\\Recent";

		// Token: 0x040000B3 RID: 179
		internal const string Seen = "\\Seen";

		// Token: 0x040000B4 RID: 180
		internal const string Deleted = "\\Deleted";

		// Token: 0x040000B5 RID: 181
		internal const string Answered = "\\Answered";

		// Token: 0x040000B6 RID: 182
		internal const string Draft = "\\Draft";

		// Token: 0x040000B7 RID: 183
		internal const string Flagged = "\\Flagged";

		// Token: 0x040000B8 RID: 184
		internal const string MdnSent = "$MDNSent";

		// Token: 0x040000B9 RID: 185
		internal const string Wildcard = "\\Wildcard";

		// Token: 0x040000BA RID: 186
		private static readonly char[] wordDelimiter = new char[]
		{
			' '
		};

		// Token: 0x040000BB RID: 187
		private static char[] specialCharacters = new char[]
		{
			'\\',
			'{',
			'(',
			')',
			'*',
			'%',
			']',
			'"'
		};

		// Token: 0x040000BC RID: 188
		private static string[] flagStrings = new string[256];
	}
}
