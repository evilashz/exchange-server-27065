using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200019C RID: 412
	internal class OVAPersonalContactsGrammarFile : PersonalContactsGrammarFile
	{
		// Token: 0x06000C24 RID: 3108 RVA: 0x00034A9E File Offset: 0x00032C9E
		internal OVAPersonalContactsGrammarFile(UMMailboxRecipient caller, CultureInfo culture) : base(caller, culture)
		{
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x00034AA8 File Offset: 0x00032CA8
		internal override string ContactsRulePrefix
		{
			get
			{
				return "\r\n\t<rule id=\"Names\" scope=\"public\">\r\n\t\t<tag>$.RecoEvent={{}};</tag>\r\n\t\t<tag>$.ResultType={{}};</tag>\r\n\t\t<tag>$.UmSubscriberObjectGuid={{}};</tag>\r\n\t\t<tag>$.UmSubscriberObjectGuid._value=\"{0}\";</tag>\r\n\t\t<tag>$.ContactId={{}};</tag>\r\n\t\t<tag>$.ContactName={{}};</tag>\r\n\t\t<tag>$.DisambiguationField={{}};</tag>\r\n";
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x00034AAF File Offset: 0x00032CAF
		internal override bool ShouldGenerateEmptyGrammar
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00034ABD File Offset: 0x00032CBD
		protected override List<ContactSearchItem> GetContactsList()
		{
			return base.GetContactsList(delegate(UMMailboxRecipient subscriber, IDictionary<PropertyDefinition, object> searchFilter, List<ContactSearchItem> list, int maxItemCount)
			{
				ContactSearchItem.AddSearchItems(subscriber, searchFilter, list, maxItemCount);
			});
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00034AE2 File Offset: 0x00032CE2
		protected override void GenerateContactsCompiledGrammar(List<ContactSearchItem> list)
		{
			if (list.Count > 0)
			{
				this.GenerateGrammar(list);
			}
			if (!this.HasEntries)
			{
				base.DisposeGrammarFile();
			}
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00034B04 File Offset: 0x00032D04
		protected override void AppendContactNode(XmlWriter namesGrammarWriter, ContactSearchItem contact, string entryName, string entryDisplayName)
		{
			namesGrammarWriter.WriteRaw(string.Format(CultureInfo.InvariantCulture, "\t\t\t<item>{0}\r\n\t\t\t\t<tag>\r\n\t\t\t\t\t$.RecoEvent._value=\"recoNameOrDepartment\";\r\n\t\t\t\t\t$.ResultType._value=\"PersonalContact\";\r\n\t\t\t\t\t$.ContactId._value=\"{1}\";\r\n\t\t\t\t\t$.ContactName._value=\"{0}\";\r\n\t\t\t\t\t$.DisambiguationField._value=\"{2}\";\r\n\t\t\t\t</tag>\r\n\t\t\t</item>\r\n", new object[]
			{
				entryName,
				contact.Id,
				entryDisplayName
			}));
		}
	}
}
