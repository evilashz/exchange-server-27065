using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200018B RID: 395
	internal class MowaPersonalContactsGrammarFile : PersonalContactsGrammarFile
	{
		// Token: 0x06000BB0 RID: 2992 RVA: 0x00032A7C File Offset: 0x00030C7C
		internal MowaPersonalContactsGrammarFile(UMMailboxRecipient caller, CultureInfo culture) : base(caller, culture)
		{
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x00032A86 File Offset: 0x00030C86
		internal override string ContactsRulePrefix
		{
			get
			{
				return "\r\n\t<rule id=\"Names\" scope=\"public\">\r\n\t\t<tag>$.RecoEvent={{}};</tag>\r\n\t\t<tag>$.ResultType={{}};</tag>\r\n\t\t<tag>$.UmSubscriberObjectGuid={{}};</tag>\r\n\t\t<tag>$.UmSubscriberObjectGuid._value=\"{0}\";</tag>\r\n\t\t<tag>$.ContactId={{}};</tag>\r\n\t\t<tag>$.ContactName={{}};</tag>\r\n\t\t<tag>$.DisambiguationField={{}};</tag>\r\n\t\t<tag>$.PersonId={{}};</tag>\r\n\t\t<tag>$.GALLinkID={{}};</tag>\r\n";
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x00032A8D File Offset: 0x00030C8D
		internal override bool ShouldGenerateEmptyGrammar
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00032A9B File Offset: 0x00030C9B
		protected override List<ContactSearchItem> GetContactsList()
		{
			return base.GetContactsList(delegate(UMMailboxRecipient subscriber, IDictionary<PropertyDefinition, object> searchFilter, List<ContactSearchItem> list, int maxItemCount)
			{
				ContactSearchItem.AddMOWASearchItems(subscriber, searchFilter, list, maxItemCount);
			});
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00032AC0 File Offset: 0x00030CC0
		protected override void GenerateContactsCompiledGrammar(List<ContactSearchItem> list)
		{
			this.GenerateGrammar(list);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00032ACC File Offset: 0x00030CCC
		protected override void AppendContactNode(XmlWriter namesGrammarWriter, ContactSearchItem contact, string entryName, string entryDisplayName)
		{
			string entryNames = this.GetEntryNames(contact, entryName);
			namesGrammarWriter.WriteRaw(string.Format(CultureInfo.InvariantCulture, "\t\t\t<item>{0}\r\n\t\t\t\t<tag>\r\n\t\t\t\t\t$.RecoEvent._value=\"recoNameOrDepartment\";\r\n\t\t\t\t\t$.ResultType._value=\"PersonalContact\";\r\n\t\t\t\t\t$.ContactId._value=\"{1}\";\r\n\t\t\t\t\t$.ContactName._value=\"{5}\";\r\n\t\t\t\t\t$.DisambiguationField._value=\"{2}\";\r\n\t\t\t\t\t$.PersonId._value=\"{3}\";\r\n\t\t\t\t\t$.GALLinkID._value=\"{4}\";\r\n\t\t\t\t</tag>\r\n\t\t\t</item>\r\n", new object[]
			{
				entryNames,
				contact.Id,
				entryDisplayName,
				contact.PersonId,
				contact.GALLinkId,
				entryName
			}));
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00032B28 File Offset: 0x00030D28
		private string GetEntryNames(ContactSearchItem contact, string entryName)
		{
			if (!contact.IsFromRecipientCache)
			{
				return entryName;
			}
			StringBuilder stringBuilder = new StringBuilder(300);
			stringBuilder.AppendLine("\t\t<one-of>\r\n");
			if (!string.IsNullOrEmpty(contact.FirstName))
			{
				this.AppendContactItem(contact.FirstName, stringBuilder);
			}
			if (!string.IsNullOrEmpty(contact.LastName))
			{
				this.AppendContactItem(contact.LastName, stringBuilder);
			}
			this.AppendContactItem(entryName, stringBuilder);
			stringBuilder.AppendLine("\t\t</one-of>\r\n");
			return stringBuilder.ToString();
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00032BA4 File Offset: 0x00030DA4
		private void AppendContactItem(string item, StringBuilder sb)
		{
			sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "<item>{0}</item>", new object[]
			{
				PersonalContactsGrammarFile.SrgsSanitizeString(item)
			}));
		}
	}
}
