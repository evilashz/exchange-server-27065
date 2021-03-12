using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001FC RID: 508
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class PhotoPerformanceMarkers
	{
		// Token: 0x040009E6 RID: 2534
		public const string ADHandlerTotal = "ADHandlerTotal";

		// Token: 0x040009E7 RID: 2535
		public const string ADHandlerReadPhoto = "ADHandlerReadPhoto";

		// Token: 0x040009E8 RID: 2536
		public const string FileSystemHandlerTotal = "FileSystemHandlerTotal";

		// Token: 0x040009E9 RID: 2537
		public const string FileSystemHandlerReadPhoto = "FileSystemHandlerReadPhoto";

		// Token: 0x040009EA RID: 2538
		public const string FileSystemHandlerReadThumbprint = "FileSystemHandlerReadThumbprint";

		// Token: 0x040009EB RID: 2539
		public const string MailboxHandlerTotal = "MailboxHandlerTotal";

		// Token: 0x040009EC RID: 2540
		public const string MailboxHandlerOpenMailbox = "MailboxHandlerOpenMailbox";

		// Token: 0x040009ED RID: 2541
		public const string MailboxHandlerReadPhoto = "MailboxHandlerReadPhoto";

		// Token: 0x040009EE RID: 2542
		public const string MailboxPhotoHandlerComputeTargetPrincipal = "MailboxPhotoHandlerComputeTargetPrincipal";

		// Token: 0x040009EF RID: 2543
		public const string MailboxPhotoReaderFindPhoto = "MailboxPhotoReaderFindPhoto";

		// Token: 0x040009F0 RID: 2544
		public const string MailboxPhotoReaderBindPhotoItem = "MailboxPhotoReaderBindPhotoItem";

		// Token: 0x040009F1 RID: 2545
		public const string MailboxPhotoReaderReadStream = "MailboxPhotoReaderReadStream";

		// Token: 0x040009F2 RID: 2546
		public const string CachingHandlerTotal = "CachingHandlerTotal";

		// Token: 0x040009F3 RID: 2547
		public const string CachingHandlerCachePhoto = "CachingHandlerCachePhoto";

		// Token: 0x040009F4 RID: 2548
		public const string CachingHandlerCacheNegativePhoto = "CachingHandlerCacheNegativePhoto";

		// Token: 0x040009F5 RID: 2549
		public const string GetPersonFromPersonId = "GetPersonFromPersonId";

		// Token: 0x040009F6 RID: 2550
		public const string FindEmailAddressFromADObjectId = "FindEmailAddressFromADObjectId";

		// Token: 0x040009F7 RID: 2551
		public const string FindPersonIdByEmailAddress = "FindPersonIdByEmailAddress";

		// Token: 0x040009F8 RID: 2552
		public const string GetPhotoStreamFromPerson = "GetPhotoStreamFromPerson";

		// Token: 0x040009F9 RID: 2553
		public const string GetPersonaPhotoTotal = "GetPersonaPhotoTotal";

		// Token: 0x040009FA RID: 2554
		public const string GetUserPhotoTotal = "GetUserPhotoTotal";

		// Token: 0x040009FB RID: 2555
		public const string QueryResolveTargetInDirectory = "QueryResolveTargetInDirectory";

		// Token: 0x040009FC RID: 2556
		public const string ProxyRequest = "ProxyRequest";

		// Token: 0x040009FD RID: 2557
		public const string LocalAuthorization = "LocalAuthorization";

		// Token: 0x040009FE RID: 2558
		public const string GetUserPhotoQueryCreation = "QueryCreation";

		// Token: 0x040009FF RID: 2559
		public const string CreateClientContext = "CreateClientContext";

		// Token: 0x04000A00 RID: 2560
		public const string ProxyOverRestService = "ProxyOverRestService";

		// Token: 0x04000A01 RID: 2561
		public const string ADHandlerPhotoServed = "ADHandlerPhotoServed";

		// Token: 0x04000A02 RID: 2562
		public const string FileSystemHandlerPhotoServed = "FileSystemHandlerPhotoServed";

		// Token: 0x04000A03 RID: 2563
		public const string MailboxHandlerPhotoServed = "MailboxHandlerPhotoServed";

		// Token: 0x04000A04 RID: 2564
		public const string ADHandlerError = "ADHandlerError";

		// Token: 0x04000A05 RID: 2565
		public const string FileSystemHandlerError = "FileSystemHandlerError";

		// Token: 0x04000A06 RID: 2566
		public const string MailboxHandlerError = "MailboxHandlerError";

		// Token: 0x04000A07 RID: 2567
		public const string ADHandlerPhotoAvailable = "ADHandlerPhotoAvailable";

		// Token: 0x04000A08 RID: 2568
		public const string FileSystemHandlerPhotoAvailable = "FileSystemHandlerPhotoAvailable";

		// Token: 0x04000A09 RID: 2569
		public const string MailboxHandlerPhotoAvailable = "MailboxHandlerPhotoAvailable";

		// Token: 0x04000A0A RID: 2570
		public const string ADFallbackPhotoServed = "ADFallbackPhotoServed";

		// Token: 0x04000A0B RID: 2571
		public const string GarbageCollection = "GarbageCollection";

		// Token: 0x04000A0C RID: 2572
		public const string HttpHandlerTotal = "HttpHandlerTotal";

		// Token: 0x04000A0D RID: 2573
		public const string HttpHandlerSendRequestAndGetResponse = "HttpHandlerSendRequestAndGetResponse";

		// Token: 0x04000A0E RID: 2574
		public const string HttpHandlerGetAndReadResponseStream = "HttpHandlerGetAndReadResponseStream";

		// Token: 0x04000A0F RID: 2575
		public const string HttpHandlerLocateService = "HttpHandlerLocateService";

		// Token: 0x04000A10 RID: 2576
		public const string HttpHandlerError = "HttpHandlerError";

		// Token: 0x04000A11 RID: 2577
		public const string RouterTotal = "RouterTotal";

		// Token: 0x04000A12 RID: 2578
		public const string RouterLookupTargetInDirectory = "RouterLookupTargetInDirectory";

		// Token: 0x04000A13 RID: 2579
		public const string RouterCheckTargetMailboxOnThisServer = "RouterCheckTargetMailboxOnThisServer";

		// Token: 0x04000A14 RID: 2580
		public const string PrivateHandlerTotal = "PrivateHandlerTotal";

		// Token: 0x04000A15 RID: 2581
		public const string ADHandlerProcessed = "ADHandlerProcessed";

		// Token: 0x04000A16 RID: 2582
		public const string FileSystemHandlerProcessed = "FileSystemHandlerProcessed";

		// Token: 0x04000A17 RID: 2583
		public const string MailboxHandlerProcessed = "MailboxHandlerProcessed";

		// Token: 0x04000A18 RID: 2584
		public const string CachingHandlerProcessed = "CachingHandlerProcessed";

		// Token: 0x04000A19 RID: 2585
		public const string HttpHandlerProcessed = "HttpHandlerProcessed";

		// Token: 0x04000A1A RID: 2586
		public const string PrivateHandlerProcessed = "PrivateHandlerProcessed";

		// Token: 0x04000A1B RID: 2587
		public const string PrivateHandlerServedContent = "PrivateHandlerServedContent";

		// Token: 0x04000A1C RID: 2588
		public const string PrivateHandlerServedRedirect = "PrivateHandlerServedRedirect";

		// Token: 0x04000A1D RID: 2589
		public const string PrivateHandlerReadPhotoStream = "PrivateHandlerReadPhotoStream";

		// Token: 0x04000A1E RID: 2590
		public const string LocalForestPhotoServiceLocatorLocateServer = "LocalForestPhotoServiceLocatorLocateServer";

		// Token: 0x04000A1F RID: 2591
		public const string LocalForestPhotoServiceLocatorGetPhotoServiceUri = "LocalForestPhotoServiceLocatorGetPhotoServiceUri";

		// Token: 0x04000A20 RID: 2592
		public const string WrongRoutingDetectedThenFallbackToOtherServer = "WrongRoutingDetectedThenFallbackToOtherServer";

		// Token: 0x04000A21 RID: 2593
		public const string PhotoRequestorWriterResolveIdentity = "PhotoRequestorWriterResolveIdentity";

		// Token: 0x04000A22 RID: 2594
		public const string RemoteForestHandlerProcessed = "RemoteForestHandlerProcessed";

		// Token: 0x04000A23 RID: 2595
		public const string RemoteForestHandlerTotal = "RemoteForestHandlerTotal";

		// Token: 0x04000A24 RID: 2596
		public const string RemoteForestHandlerServed = "RemoteForestHandlerServed";

		// Token: 0x04000A25 RID: 2597
		public const string RemoteForestHandlerError = "RemoteForestHandlerError";

		// Token: 0x04000A26 RID: 2598
		public const string MiscRoutingAndDiscovery = "MiscRoutingAndDiscovery";
	}
}
