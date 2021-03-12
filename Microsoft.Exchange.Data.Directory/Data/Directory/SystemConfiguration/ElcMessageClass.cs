using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000422 RID: 1058
	internal class ElcMessageClass
	{
		// Token: 0x06002F9A RID: 12186 RVA: 0x000C069C File Offset: 0x000BE89C
		static ElcMessageClass()
		{
			ElcMessageClass.standardList = new Dictionary<string, string>(13, StringComparer.OrdinalIgnoreCase);
			ElcMessageClass.standardList.Add(ElcMessageClass.AllMailboxContent, DirectoryStrings.AllMailboxContentMC);
			ElcMessageClass.standardList.Add(ElcMessageClass.AllEmail, DirectoryStrings.AllEmailMC);
			ElcMessageClass.standardList.Add(ElcMessageClass.VoiceMail, DirectoryStrings.ExchangeVoicemailMC);
			ElcMessageClass.standardList.Add(ElcMessageClass.MissedCall, DirectoryStrings.ExchangeMissedcallMC);
			ElcMessageClass.standardList.Add(ElcMessageClass.Fax, DirectoryStrings.ExchangeFaxMC);
			ElcMessageClass.standardList.Add(ElcMessageClass.CalItems, DirectoryStrings.CalendarItemMC);
			ElcMessageClass.standardList.Add(ElcMessageClass.MeetingRequest, DirectoryStrings.MeetingRequestMC);
			ElcMessageClass.standardList.Add(ElcMessageClass.Contacts, DirectoryStrings.ContactItemsMC);
			ElcMessageClass.standardList.Add(ElcMessageClass.Documents, DirectoryStrings.DocumentMC);
			ElcMessageClass.standardList.Add(ElcMessageClass.Tasks, DirectoryStrings.TaskItemsMC);
			ElcMessageClass.standardList.Add(ElcMessageClass.Journal, DirectoryStrings.JournalItemsMC);
			ElcMessageClass.standardList.Add(ElcMessageClass.Notes, DirectoryStrings.NotesMC);
			ElcMessageClass.standardList.Add(ElcMessageClass.Posts, DirectoryStrings.PostMC);
			ElcMessageClass.standardList.Add(ElcMessageClass.RssSubscriptions, DirectoryStrings.RssSubscriptionMC);
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000C08CA File Offset: 0x000BEACA
		public ElcMessageClass(string messageClass) : this(messageClass, ElcMessageClass.GetDisplayName(messageClass))
		{
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000C08D9 File Offset: 0x000BEAD9
		public ElcMessageClass(string messageClass, string displayName)
		{
			if (messageClass != null)
			{
				this.messageClass = messageClass.Trim();
			}
			this.displayName = displayName;
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06002F9D RID: 12189 RVA: 0x000C08F7 File Offset: 0x000BEAF7
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06002F9E RID: 12190 RVA: 0x000C08FF File Offset: 0x000BEAFF
		public string MessageClass
		{
			get
			{
				return this.messageClass;
			}
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x000C0907 File Offset: 0x000BEB07
		public static ElcMessageClass Parse(string expression)
		{
			return ElcMessageClass.GetMessageClass(expression);
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000C090F File Offset: 0x000BEB0F
		public static bool operator ==(ElcMessageClass a, ElcMessageClass b)
		{
			return a == b || (a != null && b != null && a.MessageClass == b.MessageClass);
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000C0930 File Offset: 0x000BEB30
		public static bool operator !=(ElcMessageClass a, ElcMessageClass b)
		{
			return !(a == b);
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x000C093C File Offset: 0x000BEB3C
		public static ElcMessageClass GetMessageClass(string messageClass)
		{
			if (messageClass == null)
			{
				throw new ArgumentNullException("messageClass");
			}
			string text;
			ElcMessageClass result;
			if (!ElcMessageClass.standardList.TryGetValue(messageClass, out text))
			{
				result = new ElcMessageClass(messageClass, null);
			}
			else
			{
				result = new ElcMessageClass(messageClass, text);
			}
			return result;
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x000C097C File Offset: 0x000BEB7C
		public static ElcMessageClass[] GetStandardMessageClasses()
		{
			ElcMessageClass[] array = new ElcMessageClass[ElcMessageClass.standardList.Count];
			int num = 0;
			foreach (KeyValuePair<string, string> keyValuePair in ElcMessageClass.standardList)
			{
				array[num] = new ElcMessageClass(keyValuePair.Key, keyValuePair.Value);
				num++;
			}
			return array;
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000C09F4 File Offset: 0x000BEBF4
		internal static string GetDisplayName(string messageClass)
		{
			string text = null;
			if (messageClass != null)
			{
				ElcMessageClass.standardList.TryGetValue(messageClass, out text);
				if (text == null)
				{
					if (string.Compare(messageClass, ElcMessageClass.AllEmailList, StringComparison.OrdinalIgnoreCase) == 0)
					{
						text = ElcMessageClass.AllEmail;
					}
					else
					{
						text = messageClass;
					}
				}
			}
			return text;
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000C0A30 File Offset: 0x000BEC30
		internal static bool IsAllEmail(string messageClass)
		{
			return !string.IsNullOrEmpty(messageClass) && string.Compare(messageClass, ElcMessageClass.AllEmail, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000C0A4B File Offset: 0x000BEC4B
		internal static bool IsMultiMessageClass(string messageClass)
		{
			return !string.IsNullOrEmpty(messageClass) && messageClass.IndexOfAny(ElcMessageClass.MessageClassDelims) != -1;
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000C0A68 File Offset: 0x000BEC68
		internal static bool IsMultiMessageClassDeputy(string messageClass)
		{
			return !string.IsNullOrEmpty(messageClass) && string.Compare(messageClass, ElcMessageClass.MultiMessageClassDeputy, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000C0A83 File Offset: 0x000BEC83
		public sealed override string ToString()
		{
			return this.DisplayName;
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000C0A8B File Offset: 0x000BEC8B
		public sealed override int GetHashCode()
		{
			return this.MessageClass.GetHashCode();
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x000C0A98 File Offset: 0x000BEC98
		public sealed override bool Equals(object obj)
		{
			return this == obj as ElcMessageClass;
		}

		// Token: 0x0400201C RID: 8220
		private readonly string displayName;

		// Token: 0x0400201D RID: 8221
		private string messageClass;

		// Token: 0x0400201E RID: 8222
		private static readonly Dictionary<string, string> standardList;

		// Token: 0x0400201F RID: 8223
		public static readonly string VoiceMail = "IPM.Note.Microsoft.Voicemail*";

		// Token: 0x04002020 RID: 8224
		public static readonly string Fax = "IPM.Note.Microsoft.Fax*";

		// Token: 0x04002021 RID: 8225
		public static readonly string MissedCall = "IPM.Note.Microsoft.Missed.Voice*";

		// Token: 0x04002022 RID: 8226
		public static readonly string CalItems = "IPM.Appointment*";

		// Token: 0x04002023 RID: 8227
		public static readonly string Contacts = "IPM.Contact*";

		// Token: 0x04002024 RID: 8228
		public static readonly string Tasks = "IPM.Task*";

		// Token: 0x04002025 RID: 8229
		public static readonly string Journal = "IPM.Activity*";

		// Token: 0x04002026 RID: 8230
		public static readonly string Notes = "IPM.StickyNote*";

		// Token: 0x04002027 RID: 8231
		public static readonly string MeetingRequest = "IPM.Schedule*";

		// Token: 0x04002028 RID: 8232
		public static readonly string AllMailboxContent = "*";

		// Token: 0x04002029 RID: 8233
		public static readonly string Documents = "IPM.Document*";

		// Token: 0x0400202A RID: 8234
		public static readonly string Posts = "IPM.Post";

		// Token: 0x0400202B RID: 8235
		public static readonly string RssSubscriptions = "IPM.Post.RSS";

		// Token: 0x0400202C RID: 8236
		public static readonly string AllEmail = "E-mail";

		// Token: 0x0400202D RID: 8237
		internal static readonly string AllEmailList = "IPM.Note;IPM.Note.AS/400 Move Notification Form v1.0;IPM.Note.Delayed;IPM.Note.Exchange.ActiveSync.Report;IPM.Note.JournalReport.Msg;IPM.Note.JournalReport.Tnef;IPM.Note.Microsoft.Missed.Voice;IPM.Note.Rules.OofTemplate.Microsoft;IPM.Note.Rules.ReplyTemplate.Microsoft;IPM.Note.Secure.Sign;IPM.Note.SMIME;IPM.Note.SMIME.MultipartSigned;IPM.Note.StorageQuotaWarning;IPM.Note.StorageQuotaWarning.Warning;IPM.Notification.Meeting.Forward;IPM.Outlook.Recall;IPM.Recall.Report.Success;IPM.Schedule.Meeting.*;REPORT.IPM.Note.NDR";

		// Token: 0x0400202E RID: 8238
		internal static string MultiMessageClassDeputy = "{5C8E4D3F-96BD-4a97-BEB5-764F032A8ECD}(MultiMessageClassDeputy Exchange 2007 sp1 or later)";

		// Token: 0x0400202F RID: 8239
		internal static readonly char[] MessageClassDelims = new char[]
		{
			';'
		};
	}
}
