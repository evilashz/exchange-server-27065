using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x0200001C RID: 28
	public class MockList : MockClientObject<List>, IList, IClientObject<List>
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public MockList(MockClientContext context) : this(context, "Documents")
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002DC2 File Offset: 0x00000FC2
		public MockList(MockClientContext context, string title)
		{
			if (title == null)
			{
				throw new ArgumentNullException("title");
			}
			this.context = context;
			this.title = title;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public IFolder RootFolder
		{
			get
			{
				MockFolder result;
				if ((result = this.rootFolder) == null)
				{
					result = (this.rootFolder = new MockFolder(this.title, this.context));
				}
				return result;
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002E19 File Offset: 0x00001019
		public override void LoadMockData()
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002E1C File Offset: 0x0000101C
		public IListItemCollection GetItems(CamlQuery query)
		{
			bool flag = false;
			bool flag2 = false;
			List<KeyValuePair<string, bool>> list = new List<KeyValuePair<string, bool>>();
			List<string> list2 = new List<string>();
			int rowLimit = -1;
			using (StringReader stringReader = new StringReader(query.ViewXml))
			{
				using (XmlReader xmlReader = XmlReader.Create(stringReader))
				{
					while (xmlReader.Read())
					{
						if (xmlReader.IsStartElement("OrderBy"))
						{
							flag = true;
							flag2 = false;
						}
						else if (xmlReader.IsStartElement("ViewFields"))
						{
							flag = false;
							flag2 = true;
						}
						else if (xmlReader.IsStartElement("FieldRef"))
						{
							if (flag)
							{
								string attribute = xmlReader.GetAttribute("Name");
								string attribute2 = xmlReader.GetAttribute("Ascending");
								bool value;
								if (!string.IsNullOrEmpty(attribute) && bool.TryParse(attribute2, out value))
								{
									list.Add(new KeyValuePair<string, bool>(attribute, value));
								}
							}
							else if (flag2)
							{
								string attribute3 = xmlReader.GetAttribute("Name");
								if (!string.IsNullOrEmpty(attribute3))
								{
									list2.Add(attribute3);
								}
							}
						}
						else if (xmlReader.IsStartElement("RowLimit"))
						{
							rowLimit = xmlReader.ReadElementContentAsInt();
						}
					}
				}
			}
			return new MockListItemCollection(this.context, this.title, query.FolderServerRelativeUrl, list, list2, rowLimit, query.ListItemCollectionPosition);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002F78 File Offset: 0x00001178
		public IListItem GetItemById(string id)
		{
			if (new DirectoryInfo(id).Exists)
			{
				return new MockListItem(new DirectoryInfo(id), Path.GetDirectoryName(id), this.context);
			}
			if (new FileInfo(id).Exists)
			{
				return new MockListItem(new FileInfo(id), Path.GetDirectoryName(id), this.context);
			}
			throw new MockServerException();
		}

		// Token: 0x0400002F RID: 47
		private const string RootDocumentLibrary = "Documents";

		// Token: 0x04000030 RID: 48
		private const string OrderByElementName = "OrderBy";

		// Token: 0x04000031 RID: 49
		private const string FieldRefElementName = "FieldRef";

		// Token: 0x04000032 RID: 50
		private const string ViewFieldsElementName = "ViewFields";

		// Token: 0x04000033 RID: 51
		private const string RowLimitElementName = "RowLimit";

		// Token: 0x04000034 RID: 52
		private const string NameAttributeName = "Name";

		// Token: 0x04000035 RID: 53
		private const string AscendingAttributeName = "Ascending";

		// Token: 0x04000036 RID: 54
		private readonly string title;

		// Token: 0x04000037 RID: 55
		private MockClientContext context;

		// Token: 0x04000038 RID: 56
		private MockFolder rootFolder;
	}
}
