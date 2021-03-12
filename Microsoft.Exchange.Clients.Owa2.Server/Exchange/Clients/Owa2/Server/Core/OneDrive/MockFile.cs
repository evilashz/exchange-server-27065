using System;
using System.IO;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000010 RID: 16
	public class MockFile : MockClientObject<File>, IFile, IClientObject<File>
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000026EC File Offset: 0x000008EC
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000026F4 File Offset: 0x000008F4
		public string Name { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000026FD File Offset: 0x000008FD
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002705 File Offset: 0x00000905
		public string ServerRelativeUrl { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000270E File Offset: 0x0000090E
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002716 File Offset: 0x00000916
		public long Length { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000271F File Offset: 0x0000091F
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002727 File Offset: 0x00000927
		public bool Exists { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002730 File Offset: 0x00000930
		public string LinkingUrl
		{
			get
			{
				return new Uri(new Uri(this.context.Url), this.ServerRelativeUrl).ToString();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002752 File Offset: 0x00000952
		// (set) Token: 0x06000060 RID: 96 RVA: 0x0000275A File Offset: 0x0000095A
		public IListItem ListItemAllFields { get; private set; }

		// Token: 0x06000061 RID: 97 RVA: 0x00002763 File Offset: 0x00000963
		public MockFile(string relativeLocation, MockClientContext context)
		{
			this.ServerRelativeUrl = relativeLocation;
			this.context = context;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002779 File Offset: 0x00000979
		public MockFile(MockListItem mockListItem, MockClientContext context)
		{
			this.ListItemAllFields = mockListItem;
			this.context = context;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000278F File Offset: 0x0000098F
		public MockFile(FileCreationInformation parameters, string folderRelativeUrl, MockClientContext context)
		{
			this.fileCreationInformation = parameters;
			this.folderRelativeUrl = folderRelativeUrl;
			this.context = context;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000027AC File Offset: 0x000009AC
		public override void LoadMockData()
		{
			if (this.ServerRelativeUrl != null)
			{
				this.actualPath = Path.Combine(MockClientContext.MockAttachmentDataProviderFilePath, this.ServerRelativeUrl);
				FileInfo fileInfo = new FileInfo(this.actualPath);
				if (this.Exists = fileInfo.Exists)
				{
					this.ListItemAllFields = new MockListItem(fileInfo, Path.GetDirectoryName(this.ServerRelativeUrl), this.context);
					this.Name = fileInfo.Name;
					this.Length = fileInfo.Length;
				}
			}
			else if (this.ListItemAllFields != null)
			{
				this.ServerRelativeUrl = this.ListItemAllFields["FileRef"].ToString();
				this.Name = this.ListItemAllFields["FileLeafRef"].ToString();
				this.Length = (long)this.ListItemAllFields["File_x0020_Size"];
				this.actualPath = Path.Combine(MockClientContext.MockAttachmentDataProviderFilePath, this.ServerRelativeUrl);
				this.Exists = new FileInfo(this.actualPath).Exists;
			}
			else if (this.fileCreationInformation != null)
			{
				this.ServerRelativeUrl = Path.Combine(this.folderRelativeUrl, this.fileCreationInformation.Url);
				this.actualPath = Path.Combine(MockClientContext.MockAttachmentDataProviderFilePath, this.ServerRelativeUrl);
				FileInfo fileInfo2 = new FileInfo(this.actualPath);
				if (!fileInfo2.Exists || this.fileCreationInformation.Overwrite)
				{
					using (FileStream fileStream = fileInfo2.Create())
					{
						Stream contentStream = this.fileCreationInformation.ContentStream;
						contentStream.CopyTo(fileStream);
					}
				}
				fileInfo2 = new FileInfo(this.actualPath);
				this.ListItemAllFields = new MockListItem(fileInfo2, Path.GetDirectoryName(this.ServerRelativeUrl), this.context);
				this.Name = fileInfo2.Name;
				this.Length = fileInfo2.Length;
				this.Exists = fileInfo2.Exists;
			}
			if (this.openBinaryStreamResult != null)
			{
				Stream stream = new FileStream(this.actualPath, FileMode.Open);
				this.context.AddToDisposeList(stream);
				this.openBinaryStreamResult.Value = stream;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000029D4 File Offset: 0x00000BD4
		public IClientResult<Stream> OpenBinaryStream()
		{
			return this.openBinaryStreamResult = new MockClientResult<Stream>();
		}

		// Token: 0x04000014 RID: 20
		private readonly string folderRelativeUrl;

		// Token: 0x04000015 RID: 21
		private MockClientContext context;

		// Token: 0x04000016 RID: 22
		private string actualPath;

		// Token: 0x04000017 RID: 23
		private FileCreationInformation fileCreationInformation;

		// Token: 0x04000018 RID: 24
		private MockClientResult<Stream> openBinaryStreamResult;
	}
}
