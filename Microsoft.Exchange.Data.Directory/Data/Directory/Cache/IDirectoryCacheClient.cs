using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x020000AA RID: 170
	[ServiceContract(Namespace = "http://Microsoft.Exchange.Data.Directory.DirectoryCache", ConfigurationName = "Microsoft.Exchange.Data.Directory.Cache.IDirectoryCacheService")]
	internal interface IDirectoryCacheClient : IDirectoryCacheService
	{
		// Token: 0x0600094E RID: 2382
		[OperationContract(Name = "GetObject", Action = "http://Microsoft.Exchange.Data.Directory.DirectoryCache/IDirectoryCacheService/GetObject", ReplyAction = "http://Microsoft.Exchange.Data.Directory.DirectoryCache/IDirectoryCacheService/GetObjectResponse")]
		GetObjectContext GetObject(DirectoryCacheRequest cacheRequest);

		// Token: 0x0600094F RID: 2383
		[OperationContract(Name = "PutObject", Action = "http://Microsoft.Exchange.Data.Directory.DirectoryCache/IDirectoryCacheService/PutObject", ReplyAction = "http://Microsoft.Exchange.Data.Directory.DirectoryCache/IDirectoryCacheService/PutObjectResponse")]
		CacheResponseContext PutObject(AddDirectoryCacheRequest cacheRequest);

		// Token: 0x06000950 RID: 2384
		[OperationContract(Name = "RemoveObject", Action = "http://Microsoft.Exchange.Data.Directory.DirectoryCache/IDirectoryCacheService/RemoveObject", ReplyAction = "http://Microsoft.Exchange.Data.Directory.DirectoryCache/IDirectoryCacheService/RemoveObjectResponse")]
		CacheResponseContext RemoveObject(RemoveDirectoryCacheRequest cacheRequest);

		// Token: 0x06000951 RID: 2385
		[OperationContract(Name = "Diagnostic", Action = "http://Microsoft.Exchange.Data.Directory.DirectoryCache/IDirectoryCacheService/Diagnostic", ReplyAction = "http://Microsoft.Exchange.Data.Directory.DirectoryCache/IDirectoryCacheService/DiagnosticResponse")]
		void Diagnostic(DiagnosticsCacheRequest cacheRequest);
	}
}
