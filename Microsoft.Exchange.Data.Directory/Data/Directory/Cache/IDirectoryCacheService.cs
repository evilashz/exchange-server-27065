using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x020000A5 RID: 165
	[ServiceContract(Namespace = "http://Microsoft.Exchange.Data.Directory.DirectoryCache", ConfigurationName = "Microsoft.Exchange.Data.Directory.Cache.IDirectoryCacheService")]
	internal interface IDirectoryCacheService
	{
		// Token: 0x0600091D RID: 2333
		[OperationContract(Name = "GetObject", AsyncPattern = true, Action = "http://Microsoft.Exchange.Data.Directory.DirectoryCache/IDirectoryCacheService/GetObject", ReplyAction = "http://Microsoft.Exchange.Data.Directory.DirectoryCache/IDirectoryCacheService/GetObjectResponse")]
		IAsyncResult BeginGetObject(DirectoryCacheRequest cacheRequest, AsyncCallback callback, object asyncState);

		// Token: 0x0600091E RID: 2334
		GetObjectContext EndGetObject(IAsyncResult result);

		// Token: 0x0600091F RID: 2335
		[OperationContract(Name = "PutObject", AsyncPattern = true, Action = "http://Microsoft.Exchange.Data.Directory.DirectoryCache/IDirectoryCacheService/PutObject", ReplyAction = "http://Microsoft.Exchange.Data.Directory.DirectoryCache/IDirectoryCacheService/PutObjectResponse")]
		IAsyncResult BeginPutObject(AddDirectoryCacheRequest cacheRequest, AsyncCallback callback, object asyncState);

		// Token: 0x06000920 RID: 2336
		CacheResponseContext EndPutObject(IAsyncResult result);

		// Token: 0x06000921 RID: 2337
		[OperationContract(Name = "RemoveObject", AsyncPattern = true, Action = "http://Microsoft.Exchange.Data.Directory.DirectoryCache/IDirectoryCacheService/RemoveObject", ReplyAction = "http://Microsoft.Exchange.Data.Directory.DirectoryCache/IDirectoryCacheService/RemoveObjectResponse")]
		IAsyncResult BeginRemoveObject(RemoveDirectoryCacheRequest cacheRequest, AsyncCallback callback, object asyncState);

		// Token: 0x06000922 RID: 2338
		CacheResponseContext EndRemoveObject(IAsyncResult result);

		// Token: 0x06000923 RID: 2339
		[OperationContract(Name = "Diagnostic", AsyncPattern = true, Action = "http://Microsoft.Exchange.Data.Directory.DirectoryCache/IDirectoryCacheService/Diagnostic", ReplyAction = "http://Microsoft.Exchange.Data.Directory.DirectoryCache/IDirectoryCacheService/DiagnosticResponse")]
		IAsyncResult BeginDiagnostic(DiagnosticsCacheRequest cacheRequest, AsyncCallback callback, object asyncState);

		// Token: 0x06000924 RID: 2340
		void EndDiagnostic(IAsyncResult result);
	}
}
