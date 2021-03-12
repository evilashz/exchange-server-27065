using System;
using System.Collections;
using System.Globalization;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.WBXml
{
	// Token: 0x02000081 RID: 129
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class WBXmlSchema30 : WBXmlSchema
	{
		// Token: 0x0600027A RID: 634 RVA: 0x00008C34 File Offset: 0x00006E34
		internal WBXmlSchema30() : base(30)
		{
			WBXmlSchema30.CodePage30[] array = new WBXmlSchema30.CodePage30[]
			{
				new WBXmlSchema30.CodePage30(EasStringArrays.AirSync, "AirSync", 0),
				new WBXmlSchema30.CodePage30(EasStringArrays.Contacts, "Contacts", 1),
				new WBXmlSchema30.CodePage30(EasStringArrays.Email, "Email", 2),
				new WBXmlSchema30.CodePage30(EasStringArrays.AirNotify, "AirNotify", 3),
				new WBXmlSchema30.CodePage30(EasStringArrays.Cal, "Calendar", 4),
				new WBXmlSchema30.CodePage30(EasStringArrays.Move, "Move", 5),
				new WBXmlSchema30.CodePage30(EasStringArrays.ItemEstimate, "GetItemEstimate", 6),
				new WBXmlSchema30.CodePage30(EasStringArrays.FolderHierarchy, "FolderHierarchy", 7),
				new WBXmlSchema30.CodePage30(EasStringArrays.MeetingResponse, "MeetingResponse", 8),
				new WBXmlSchema30.CodePage30(EasStringArrays.Tasks, "Tasks", 9),
				new WBXmlSchema30.CodePage30(EasStringArrays.ResolveRecipients, "ResolveRecipientsRequest", 10),
				new WBXmlSchema30.CodePage30(EasStringArrays.ValidateCert, "ValidateCertRequest", 11),
				new WBXmlSchema30.CodePage30(EasStringArrays.Contacts2, "Contacts2", 12),
				new WBXmlSchema30.CodePage30(EasStringArrays.Ping, "PingRequest", 13),
				new WBXmlSchema30.CodePage30(EasStringArrays.Provision, "ProvisionRequest", 14),
				new WBXmlSchema30.CodePage30(EasStringArrays.Search, "SearchRequest", 15),
				new WBXmlSchema30.CodePage30(EasStringArrays.Gal, "Gal", 16),
				new WBXmlSchema30.CodePage30(EasStringArrays.AirsyncBase, "AirSyncBase", 17),
				new WBXmlSchema30.CodePage30(EasStringArrays.Settings, "Settings", 18),
				new WBXmlSchema30.CodePage30(EasStringArrays.DocumentLibrary, "DocumentLibrary", 19),
				new WBXmlSchema30.CodePage30(EasStringArrays.ItemOperations, "ItemOperations", 20),
				new WBXmlSchema30.CodePage30(EasStringArrays.ComposeMail, "ComposeMail", 21),
				new WBXmlSchema30.CodePage30(EasStringArrays.Email2, "Email2", 22),
				new WBXmlSchema30.CodePage30(EasStringArrays.Notes, "Notes", 23),
				new WBXmlSchema30.CodePage30(EasStringArrays.RightsManagement, "RightsManagement", 24),
				new WBXmlSchema30.CodePage30(EasStringArrays.WindowsLive, "WindowsLive", 254)
			};
			this.codeSpace30 = new Hashtable();
			this.stringSpace30 = new Hashtable();
			foreach (WBXmlSchema30.CodePage30 codePage in array)
			{
				this.codeSpace30.Add(codePage.PageNumber, codePage);
				this.stringSpace30.Add(codePage.NameSpace, codePage);
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00008EC0 File Offset: 0x000070C0
		internal override string GetName(int tag)
		{
			int num = (tag & 65280) >> 8;
			string text = ((WBXmlSchema30.CodePage30)this.codeSpace30[num]).NameFromCode(tag);
			if (text == null || num != ((WBXmlSchema30.CodePage30)this.codeSpace30[num]).PageNumber)
			{
				throw new EasWBXmlTransientException("Invalid tag code: 0x" + tag.ToString("X", CultureInfo.InvariantCulture));
			}
			return text;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00008F38 File Offset: 0x00007138
		internal override string GetNameSpace(int tag)
		{
			int num = (tag & 65280) >> 8;
			string nameSpace = ((WBXmlSchema30.CodePage30)this.codeSpace30[num]).NameSpace;
			if (nameSpace == null)
			{
				throw new EasWBXmlTransientException("Invalid tag pagecode: 0x" + tag.ToString("X", CultureInfo.InvariantCulture));
			}
			return nameSpace;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00008F90 File Offset: 0x00007190
		internal override int GetTag(string nameSpace, string name)
		{
			if (nameSpace == null || name == null)
			{
				throw new EasWBXmlTransientException("nameSpace and name must both be non-null");
			}
			WBXmlSchema30.CodePage30 codePage = (WBXmlSchema30.CodePage30)this.stringSpace30[nameSpace];
			if (codePage == null)
			{
				throw new EasWBXmlTransientException("The namespace " + nameSpace + " is invalid in this schema");
			}
			int num = codePage.CodeFromName(name);
			if (num == 0)
			{
				throw new EasWBXmlTransientException(string.Concat(new string[]
				{
					"The name ",
					nameSpace,
					" ",
					name,
					" could not be found in schema"
				}));
			}
			return num;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00009018 File Offset: 0x00007218
		internal override bool IsTagSecure(int tag)
		{
			return tag == 3871 || tag == 5141;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000902C File Offset: 0x0000722C
		internal override bool IsTagAnOpaqueBlob(int tag)
		{
			return tag == 5392 || tag == 5144 || tag == 3872 || tag == 5641 || tag == 5642;
		}

		// Token: 0x04000416 RID: 1046
		private Hashtable codeSpace30;

		// Token: 0x04000417 RID: 1047
		private Hashtable stringSpace30;

		// Token: 0x02000082 RID: 130
		protected class CodePage30
		{
			// Token: 0x06000280 RID: 640 RVA: 0x00009058 File Offset: 0x00007258
			internal CodePage30(string[] tags, string nameSpace, int page)
			{
				this.NameSpace = nameSpace;
				this.PageNumber = page;
				this.stringTable = new Hashtable();
				this.codeTable = new string[tags.Length];
				int num = 5;
				foreach (string text in tags)
				{
					int num2 = page << 8;
					this.stringTable.Add(text, num | num2);
					this.codeTable[num - 5] = text;
					num++;
				}
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000281 RID: 641 RVA: 0x000090D4 File Offset: 0x000072D4
			// (set) Token: 0x06000282 RID: 642 RVA: 0x000090DC File Offset: 0x000072DC
			internal string NameSpace { get; private set; }

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06000283 RID: 643 RVA: 0x000090E5 File Offset: 0x000072E5
			// (set) Token: 0x06000284 RID: 644 RVA: 0x000090ED File Offset: 0x000072ED
			internal int PageNumber { get; private set; }

			// Token: 0x06000285 RID: 645 RVA: 0x000090F8 File Offset: 0x000072F8
			internal int CodeFromName(string name)
			{
				object obj = this.stringTable[name];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}

			// Token: 0x06000286 RID: 646 RVA: 0x00009120 File Offset: 0x00007320
			internal string NameFromCode(int code)
			{
				int num = (code & 255) - 5;
				return this.codeTable[num];
			}

			// Token: 0x04000418 RID: 1048
			private readonly string[] codeTable;

			// Token: 0x04000419 RID: 1049
			private readonly Hashtable stringTable;
		}
	}
}
