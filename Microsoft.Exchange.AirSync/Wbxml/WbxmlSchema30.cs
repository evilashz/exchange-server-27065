using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Exchange.AirSync.Wbxml
{
	// Token: 0x020002A7 RID: 679
	internal class WbxmlSchema30 : WbxmlSchema
	{
		// Token: 0x060018B1 RID: 6321 RVA: 0x0009210C File Offset: 0x0009030C
		public WbxmlSchema30() : base(30)
		{
			WbxmlSchema30.CodePage30[] array = new WbxmlSchema30.CodePage30[]
			{
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.AirSync, "AirSync:", 0),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.Contacts, "Contacts:", 1),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.Email, "Email:", 2),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.AirNotify, "AirNotify:", 3),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.Cal, "Calendar:", 4),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.Move, "Move:", 5),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.ItemEstimate, "GetItemEstimate:", 6),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.FolderHierarchy, "FolderHierarchy:", 7),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.MeetingResponse, "MeetingResponse:", 8),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.Tasks, "Tasks:", 9),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.ResolveRecipients, "ResolveRecipients:", 10),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.ValidateCert, "ValidateCert:", 11),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.Contacts2, "Contacts2:", 12),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.Ping, "Ping:", 13),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.Provision, "Provision:", 14),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.Search, "Search:", 15),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.Gal, "Gal:", 16),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.AirsyncBase, "AirSyncBase:", 17),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.Settings, "Settings:", 18),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.DocumentLibrary, "DocumentLibrary:", 19),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.ItemOperations, "ItemOperations:", 20),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.ComposeMail, "ComposeMail:", 21),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.Email2, "Email2:", 22),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.Notes, "Notes:", 23),
				new WbxmlSchema30.CodePage30(AirSyncStringArrays.RightsManagement, "RightsManagement:", 24)
			};
			this.codeSpace30 = new Dictionary<int, WbxmlSchema30.CodePage30>();
			this.stringSpace30 = new Hashtable();
			for (int i = 0; i < array.Length; i++)
			{
				this.codeSpace30.Add(i, array[i]);
				this.stringSpace30.Add(array[i].NameSpace, array[i]);
			}
			WbxmlSchema30.CodePage30 value = new WbxmlSchema30.CodePage30(AirSyncStringArrays.WindowsLive, "WindowsLive:", 254);
			this.codeSpace30.Add(254, value);
			this.stringSpace30.Add("WindowsLive:", value);
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x000923A8 File Offset: 0x000905A8
		public override string GetName(int tag)
		{
			int num = (tag & 65280) >> 8;
			string text = this.codeSpace30[num].NameFromCode(tag);
			if (text == null || num != this.codeSpace30[num].PageNumber)
			{
				throw new WbxmlException("Invalid tag code: 0x" + tag.ToString("X", CultureInfo.InvariantCulture));
			}
			return text;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0009240C File Offset: 0x0009060C
		public override string GetNameSpace(int tag)
		{
			int key = (tag & 65280) >> 8;
			string nameSpace = this.codeSpace30[key].NameSpace;
			if (nameSpace == null)
			{
				throw new WbxmlException("Invalid tag pagecode: 0x" + tag.ToString("X", CultureInfo.InvariantCulture));
			}
			return nameSpace;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0009245C File Offset: 0x0009065C
		public override int GetTag(string nameSpace, string name)
		{
			if (nameSpace == null || name == null)
			{
				throw new WbxmlException("nameSpace and name must both be non-null");
			}
			WbxmlSchema30.CodePage30 codePage = (WbxmlSchema30.CodePage30)this.stringSpace30[nameSpace];
			if (codePage == null)
			{
				throw new WbxmlException("The namespace " + nameSpace + " is invalid in this schema");
			}
			int num = codePage.CodeFromName(name);
			if (num == 0)
			{
				throw new WbxmlException(string.Concat(new string[]
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

		// Token: 0x060018B5 RID: 6325 RVA: 0x000924E4 File Offset: 0x000906E4
		public override bool IsTagSecure(int tag)
		{
			return tag == 3871 || tag == 5141;
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x000924F8 File Offset: 0x000906F8
		public override bool IsTagAnOpaqueBlob(int tag)
		{
			return tag == 5392 || tag == 5144 || tag == 3872 || tag == 5641 || tag == 4383;
		}

		// Token: 0x0400118B RID: 4491
		private const int WindowsLiveCodePageNumber = 254;

		// Token: 0x0400118C RID: 4492
		private Dictionary<int, WbxmlSchema30.CodePage30> codeSpace30;

		// Token: 0x0400118D RID: 4493
		private Hashtable stringSpace30;

		// Token: 0x020002A8 RID: 680
		protected class CodePage30
		{
			// Token: 0x060018B7 RID: 6327 RVA: 0x00092524 File Offset: 0x00090724
			public CodePage30(string[] tags, string nameSpace, int page)
			{
				this.nameSpace = nameSpace;
				this.pageNumber = page;
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

			// Token: 0x17000842 RID: 2114
			// (get) Token: 0x060018B8 RID: 6328 RVA: 0x000925A0 File Offset: 0x000907A0
			public string NameSpace
			{
				get
				{
					return this.nameSpace;
				}
			}

			// Token: 0x17000843 RID: 2115
			// (get) Token: 0x060018B9 RID: 6329 RVA: 0x000925A8 File Offset: 0x000907A8
			public int PageNumber
			{
				get
				{
					return this.pageNumber;
				}
			}

			// Token: 0x060018BA RID: 6330 RVA: 0x000925B0 File Offset: 0x000907B0
			public int CodeFromName(string name)
			{
				object obj = this.stringTable[name];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}

			// Token: 0x060018BB RID: 6331 RVA: 0x000925D8 File Offset: 0x000907D8
			public string NameFromCode(int code)
			{
				int num = (code & 255) - 5;
				return this.codeTable[num];
			}

			// Token: 0x0400118E RID: 4494
			private string[] codeTable;

			// Token: 0x0400118F RID: 4495
			private string nameSpace;

			// Token: 0x04001190 RID: 4496
			private int pageNumber;

			// Token: 0x04001191 RID: 4497
			private Hashtable stringTable;
		}
	}
}
