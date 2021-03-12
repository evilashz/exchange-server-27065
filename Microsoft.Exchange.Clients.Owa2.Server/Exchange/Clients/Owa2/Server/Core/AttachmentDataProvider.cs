using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003A1 RID: 929
	[KnownType(typeof(OneDriveProAttachmentDataProvider))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(MockAttachmentDataProvider))]
	[SimpleConfiguration("OWA.AttachmentDataProvider", "AttachmentDataProvider")]
	public class AttachmentDataProvider
	{
		// Token: 0x06001DA4 RID: 7588 RVA: 0x000761AD File Offset: 0x000743AD
		public AttachmentDataProvider()
		{
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x000761B5 File Offset: 0x000743B5
		public AttachmentDataProvider(string id, AttachmentDataProviderType type)
		{
			this.Id = id;
			this.Type = type;
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x000761CB File Offset: 0x000743CB
		// (set) Token: 0x06001DA7 RID: 7591 RVA: 0x000761D3 File Offset: 0x000743D3
		[SimpleConfigurationProperty("id")]
		[DataMember]
		public string Id { get; set; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x000761DC File Offset: 0x000743DC
		// (set) Token: 0x06001DA9 RID: 7593 RVA: 0x000761E4 File Offset: 0x000743E4
		[DataMember]
		[SimpleConfigurationProperty("type")]
		public AttachmentDataProviderType Type { get; set; }

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x000761ED File Offset: 0x000743ED
		// (set) Token: 0x06001DAB RID: 7595 RVA: 0x000761F5 File Offset: 0x000743F5
		[SimpleConfigurationProperty("EndPointUrl")]
		[DataMember]
		public string EndPointUrl { get; set; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x000761FE File Offset: 0x000743FE
		// (set) Token: 0x06001DAD RID: 7597 RVA: 0x00076206 File Offset: 0x00074406
		[SimpleConfigurationProperty("displayName")]
		[DataMember]
		public string DisplayName { get; set; }

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x0007620F File Offset: 0x0007440F
		// (set) Token: 0x06001DAF RID: 7599 RVA: 0x00076217 File Offset: 0x00074417
		[DataMember]
		public AttachmentDataProviderScope[] Scopes { get; set; }

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x00076220 File Offset: 0x00074420
		// (set) Token: 0x06001DB1 RID: 7601 RVA: 0x00076228 File Offset: 0x00074428
		[DataMember]
		public int DefaultScopeIndex { get; set; }

		// Token: 0x06001DB2 RID: 7602 RVA: 0x00076231 File Offset: 0x00074431
		public virtual bool FileExists(string fileUrl)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x00076238 File Offset: 0x00074438
		internal virtual string GetLinkingUrl(UserContext userContext, string fileUrl, string endpointUrl, string itemId = null, string parentItemId = null)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x0007623F File Offset: 0x0007443F
		internal virtual GetAttachmentDataProviderItemsResponse GetItems(AttachmentItemsPagingDetails paging, AttachmentDataProviderScope scope, MailboxSession mailboxSession)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x00076246 File Offset: 0x00074446
		public virtual Task<DownloadItemAsyncResult> DownloadItemAsync(string location, string itemId, string parentItemId, string providerEndpointUrl, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x0007624D File Offset: 0x0007444D
		internal virtual Task<UploadItemAsyncResult> UploadItemAsync(byte[] file, string fileName, CancellationToken cancellationToken, CallContext callContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x00076254 File Offset: 0x00074454
		public virtual UploadItemAsyncResult UploadItemSync(byte[] fileContent, string fileName, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0007625B File Offset: 0x0007445B
		public virtual Task<UpdatePermissionsAsyncResult> UpdateDocumentPermissionsAsync(string[] resources, AttachmentPermissionAssignment[] attachmentPermissionAssignments, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x00076262 File Offset: 0x00074462
		public virtual AttachmentDataProviderItem[] GetRecentItems()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x00076269 File Offset: 0x00074469
		public virtual string GetEndpointUrlFromItemLocation(string location)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x00076270 File Offset: 0x00074470
		internal virtual string GetItemAbsoulteUrl(UserContext userContext, string location, string providerEndpointUrl, string itemId = null, string parentItemId = null)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x00076277 File Offset: 0x00074477
		internal virtual void PostInitialize(GetAttachmentDataProvidersRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
