using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000022 RID: 34
	public class MockListItem : MockClientObject<ListItem>, IListItem, IClientObject<ListItem>
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00003100 File Offset: 0x00001300
		public static MockListItem GetById(string id)
		{
			if (MockListItem.idAndItemMap == null)
			{
				return null;
			}
			MockListItem result;
			if (MockListItem.idAndItemMap.TryGetValue(id, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x17000043 RID: 67
		public object this[string fieldName]
		{
			get
			{
				object result;
				if (!this.values.TryGetValue(fieldName, out result))
				{
					return null;
				}
				return result;
			}
			set
			{
				this.values[fieldName] = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003157 File Offset: 0x00001357
		public int Id
		{
			get
			{
				return (int)this["ID"];
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003169 File Offset: 0x00001369
		public string IdAsString
		{
			get
			{
				return (string)this["FileRef"];
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000317B File Offset: 0x0000137B
		public IFile File
		{
			get
			{
				if (this["FSObjType"].ToString() == "0")
				{
					return new MockFile(this, this.context);
				}
				return null;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000031A7 File Offset: 0x000013A7
		public IFolder Folder
		{
			get
			{
				if (this["FSObjType"].ToString() == "1")
				{
					return new MockFolder(this, this.context);
				}
				return null;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000031D3 File Offset: 0x000013D3
		public void BreakRoleInheritance(bool copyRoleAssignments, bool clearSubscopes)
		{
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000031D8 File Offset: 0x000013D8
		public MockListItem(DirectoryInfo dirInfo, string folderRelativePath, MockClientContext context) : this(context)
		{
			this.values["FSObjType"] = "1";
			this.values["FileLeafRef"] = dirInfo.Name;
			this.values["ID"] = this.GetId(dirInfo.FullName);
			this.values["SortBehavior"] = string.Empty;
			this.values["FileRef"] = Path.Combine(folderRelativePath, dirInfo.Name);
			this.values["File_x0020_Size"] = 0;
			if (dirInfo.Exists)
			{
				this.values["Modified"] = dirInfo.LastWriteTimeUtc;
			}
			this.values["Editor"] = FieldUserValue.FromUser("Administrator");
			this.values["ItemChildCount"] = "0";
			this.values["FolderChildCount"] = "0";
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000032E8 File Offset: 0x000014E8
		public MockListItem(FileInfo fileInfo, string folderRelativePath, MockClientContext context) : this(context)
		{
			this.values["FSObjType"] = "0";
			this.values["FileLeafRef"] = fileInfo.Name;
			this.values["ID"] = this.GetId(fileInfo.FullName);
			this.values["SortBehavior"] = string.Empty;
			this.values["FileRef"] = Path.Combine(folderRelativePath, fileInfo.Name);
			if (fileInfo.Exists)
			{
				this.values["File_x0020_Size"] = fileInfo.Length;
				this.values["Modified"] = fileInfo.LastWriteTimeUtc;
			}
			this.values["Editor"] = FieldUserValue.FromUser("Administrator");
			this.values["ItemChildCount"] = "0";
			this.values["FolderChildCount"] = "0";
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000033FB File Offset: 0x000015FB
		private MockListItem(MockClientContext context)
		{
			this.values = new Dictionary<string, object>();
			this.context = context;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003415 File Offset: 0x00001615
		public override void LoadMockData()
		{
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003418 File Offset: 0x00001618
		private int GetId(string fullpath)
		{
			if (MockListItem.pathAndIdMap == null)
			{
				MockListItem.pathAndIdMap = new Dictionary<string, int>();
			}
			int result;
			if (MockListItem.pathAndIdMap.TryGetValue(fullpath, out result))
			{
				return result;
			}
			result = (MockListItem.pathAndIdMap[fullpath] = ++MockListItem.idCounter);
			if (MockListItem.idAndItemMap == null)
			{
				MockListItem.idAndItemMap = new Dictionary<string, MockListItem>();
			}
			MockListItem.idAndItemMap[result.ToString()] = this;
			return result;
		}

		// Token: 0x0400003E RID: 62
		private const string DummyEditorName = "Administrator";

		// Token: 0x0400003F RID: 63
		private const string FileObjectType = "0";

		// Token: 0x04000040 RID: 64
		private const string FolderObjectType = "1";

		// Token: 0x04000041 RID: 65
		private static int idCounter = 0;

		// Token: 0x04000042 RID: 66
		private static Dictionary<string, int> pathAndIdMap;

		// Token: 0x04000043 RID: 67
		private static Dictionary<string, MockListItem> idAndItemMap;

		// Token: 0x04000044 RID: 68
		private Dictionary<string, object> values;

		// Token: 0x04000045 RID: 69
		private MockClientContext context;
	}
}
