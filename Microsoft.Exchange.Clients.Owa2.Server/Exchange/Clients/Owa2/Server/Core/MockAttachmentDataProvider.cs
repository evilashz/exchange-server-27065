using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000402 RID: 1026
	[SimpleConfiguration("OWA.AttachmentDataProvider", "MockAttachmentDataProvider")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MockAttachmentDataProvider : AttachmentDataProvider
	{
		// Token: 0x060021D2 RID: 8658 RVA: 0x0007A704 File Offset: 0x00078904
		public static MockAttachmentDataProvider CreateMockDataProvider()
		{
			return new MockAttachmentDataProvider
			{
				DisplayName = "MockDrive Pro",
				EndPointUrl = "http://MockEndPointUrl:14792",
				Id = Guid.NewGuid().ToString(),
				Type = AttachmentDataProviderType.OneDrivePro
			};
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x0007A750 File Offset: 0x00078950
		internal override GetAttachmentDataProviderItemsResponse GetItems(AttachmentItemsPagingDetails paging, AttachmentDataProviderScope scope, MailboxSession mailboxSession)
		{
			AttachmentDataProviderItem[] dummyItems = this.GetDummyItems();
			return new GetAttachmentDataProviderItemsResponse
			{
				ResultCode = AttachmentResultCode.Success,
				Items = dummyItems,
				PagingMetadata = null,
				TotalItemCount = dummyItems.Length
			};
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x0007A789 File Offset: 0x00078989
		public override AttachmentDataProviderItem[] GetRecentItems()
		{
			return this.GetDummyItems();
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x0007A794 File Offset: 0x00078994
		internal override string GetItemAbsoulteUrl(UserContext userContext, string location, string providerEndpointUrl, string itemId = null, string parentItemId = null)
		{
			string arg = string.IsNullOrEmpty(providerEndpointUrl) ? base.EndPointUrl : providerEndpointUrl;
			return string.Format("{0}{1}", arg, location);
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x0007A880 File Offset: 0x00078A80
		public override Task<DownloadItemAsyncResult> DownloadItemAsync(string location, string itemId, string parentItemId, string providerEndpointUrl, CancellationToken cancellationToken)
		{
			return Task.Run<DownloadItemAsyncResult>(() => new DownloadItemAsyncResult
			{
				ResultCode = AttachmentResultCode.Success,
				Item = new FileAttachmentDataProviderItem
				{
					Name = "file.txt",
					AttachmentProviderId = this.Id,
					Location = "/dummy/location/file.txt",
					ProviderType = this.Type,
					Size = 3L,
					ProviderEndpointUrl = (string.IsNullOrEmpty(providerEndpointUrl) ? this.EndPointUrl : providerEndpointUrl),
					Id = itemId
				},
				Bytes = new byte[]
				{
					1,
					2,
					3
				}
			}, cancellationToken);
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x0007A8E4 File Offset: 0x00078AE4
		internal override Task<UploadItemAsyncResult> UploadItemAsync(byte[] file, string fileName, CancellationToken cancellationToken, CallContext callContext)
		{
			return Task.Run<UploadItemAsyncResult>(() => this.UploadItemSync(file, fileName, cancellationToken), cancellationToken);
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x0007A92C File Offset: 0x00078B2C
		public override UploadItemAsyncResult UploadItemSync(byte[] file, string fileName, CancellationToken cancellationToken)
		{
			return new UploadItemAsyncResult
			{
				ResultCode = AttachmentResultCode.Success,
				Item = new FileAttachmentDataProviderItem
				{
					Name = fileName,
					AttachmentProviderId = base.Id,
					Location = this.GetMockLocation(fileName),
					ProviderType = base.Type,
					Size = file.LongLength,
					ProviderEndpointUrl = base.EndPointUrl,
					Id = "10"
				}
			};
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x0007A9A3 File Offset: 0x00078BA3
		public override bool FileExists(string fileUrl)
		{
			return true;
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x0007A9C3 File Offset: 0x00078BC3
		public override Task<UpdatePermissionsAsyncResult> UpdateDocumentPermissionsAsync(string[] resources, AttachmentPermissionAssignment[] attachmentPermissionAssignments, CancellationToken cancellationToken)
		{
			return Task.Run<UpdatePermissionsAsyncResult>(() => new UpdatePermissionsAsyncResult
			{
				ResultCode = AttachmentResultCode.Success
			}, cancellationToken);
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x0007A9E8 File Offset: 0x00078BE8
		internal override void PostInitialize(GetAttachmentDataProvidersRequest request)
		{
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x0007A9EA File Offset: 0x00078BEA
		public string GetMockLocation(string fileName)
		{
			return string.Format("/dummy/location/{0}", fileName);
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x0007A9F7 File Offset: 0x00078BF7
		public override string GetEndpointUrlFromItemLocation(string location)
		{
			return base.EndPointUrl;
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x0007AA00 File Offset: 0x00078C00
		private AttachmentDataProviderItem[] GetDummyItems()
		{
			return new AttachmentDataProviderItem[]
			{
				new FolderAttachmentDataProviderItem
				{
					Name = "folder",
					AttachmentProviderId = base.Id,
					Location = "/dummy/location/folder",
					ProviderType = base.Type,
					ProviderEndpointUrl = base.EndPointUrl,
					Id = "1"
				},
				new FolderAttachmentDataProviderItem
				{
					Name = "folder1",
					AttachmentProviderId = base.Id,
					Location = "/dummy/location/folder1",
					ProviderType = base.Type,
					ProviderEndpointUrl = base.EndPointUrl,
					Id = "2"
				},
				new FileAttachmentDataProviderItem
				{
					Name = "file.txt",
					AttachmentProviderId = base.Id,
					Location = "/dummy/location/file.txt",
					ProviderType = base.Type,
					Size = 299L,
					ProviderEndpointUrl = base.EndPointUrl,
					Id = "3"
				},
				new FileAttachmentDataProviderItem
				{
					Name = "file_image.jpg",
					AttachmentProviderId = base.Id,
					Location = "/dummy/location/file_image.jpg",
					ProviderType = base.Type,
					Size = 299L,
					ProviderEndpointUrl = base.EndPointUrl,
					Id = "4"
				},
				new FileAttachmentDataProviderItem
				{
					Name = "file_word.docx",
					AttachmentProviderId = base.Id,
					Location = "/dummy/location/file_word.docx",
					ProviderType = base.Type,
					Size = 299L,
					ProviderEndpointUrl = base.EndPointUrl,
					Id = "5"
				},
				new FileAttachmentDataProviderItem
				{
					Name = "file_excel.xlsx",
					AttachmentProviderId = base.Id,
					Location = "/dummy/location/file_excel.xlsx",
					ProviderType = base.Type,
					Size = 299L,
					ProviderEndpointUrl = base.EndPointUrl,
					Id = "6"
				},
				new FileAttachmentDataProviderItem
				{
					Name = "file_powerpoint.pptx",
					AttachmentProviderId = base.Id,
					Location = "/dummy/location/file_powerpoint.pptx",
					ProviderType = base.Type,
					Size = 299L,
					ProviderEndpointUrl = base.EndPointUrl,
					Id = "7"
				},
				new FileAttachmentDataProviderItem
				{
					Name = "file_pdf.pdf",
					AttachmentProviderId = base.Id,
					Location = "/dummy/location/file_pdf.pdf",
					ProviderType = base.Type,
					Size = 299L,
					ProviderEndpointUrl = base.EndPointUrl,
					Id = "8"
				}
			};
		}
	}
}
