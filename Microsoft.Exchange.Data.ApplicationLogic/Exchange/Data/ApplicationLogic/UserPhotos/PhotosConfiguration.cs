using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x0200020D RID: 525
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotosConfiguration
	{
		// Token: 0x0600130F RID: 4879 RVA: 0x0004EF84 File Offset: 0x0004D184
		public PhotosConfiguration(string exchangeInstallPath, TimeSpan photoFileTimeToLive)
		{
			if (string.IsNullOrEmpty(exchangeInstallPath))
			{
				throw new ArgumentNullException("exchangeInstallPath");
			}
			this.exchangeInstallPath = exchangeInstallPath;
			this.photoFileTimeToLive = photoFileTimeToLive;
			this.photosRootDirectoryPath = PhotosConfiguration.ComputePhotosRootDirectory(exchangeInstallPath);
			this.garbageCollectionLoggingPath = PhotosConfiguration.ComputeGarbageCollectionLoggingPath(exchangeInstallPath);
			this.photoRequestLoggingPath = PhotosConfiguration.ComputePhotoRequestLoggingPath(exchangeInstallPath);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0004EFDC File Offset: 0x0004D1DC
		public PhotosConfiguration(string exchangeInstallPath) : this(exchangeInstallPath, PhotosConfiguration.DefaultPhotoFileTimeToLive)
		{
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x0004EFEA File Offset: 0x0004D1EA
		public UserPhotoSize PhotoSizeToUploadToAD
		{
			get
			{
				return UserPhotoSize.HR64x64;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x0004EFED File Offset: 0x0004D1ED
		public string ExchangeInstallPath
		{
			get
			{
				return this.exchangeInstallPath;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x0004EFF5 File Offset: 0x0004D1F5
		public TimeSpan PhotoFileTimeToLive
		{
			get
			{
				return this.photoFileTimeToLive;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0004EFFD File Offset: 0x0004D1FD
		public ICollection<UserPhotoSize> SizesToCacheOnFileSystem
		{
			get
			{
				return PhotosConfiguration.DefaultSizesToCacheOnFileSystem;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x0004F004 File Offset: 0x0004D204
		public TimeSpan UserAgentCacheTimeToLive
		{
			get
			{
				return PhotosConfiguration.DefaultUserAgentCacheTimeToLive;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x0004F00B File Offset: 0x0004D20B
		public TimeSpan UserAgentCacheTimeToLiveNotFound
		{
			get
			{
				return PhotosConfiguration.DefaultUserAgentCacheTimeToLiveNotFound;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x0004F012 File Offset: 0x0004D212
		public string PhotoServiceEndpointRelativeToEwsWithLeadingSlash
		{
			get
			{
				return "/s/GetUserPhoto";
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x0004F019 File Offset: 0x0004D219
		public TimeSpan GarbageCollectionInterval
		{
			get
			{
				return PhotosConfiguration.DefaultGarbageCollectionInterval;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x0004F020 File Offset: 0x0004D220
		public TimeSpan LastAccessGarbageThreshold
		{
			get
			{
				return PhotosConfiguration.DefaultLastAccessGarbageThreshold;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x0004F027 File Offset: 0x0004D227
		public string GarbageCollectionLoggingPath
		{
			get
			{
				return this.garbageCollectionLoggingPath;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x0004F02F File Offset: 0x0004D22F
		public TimeSpan GarbageCollectionLogFileMaxAge
		{
			get
			{
				return PhotosConfiguration.DefaultGarbageCollectionLogFileMaxAge;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x0004F036 File Offset: 0x0004D236
		public long GarbageCollectionLogDirectoryMaxSize
		{
			get
			{
				return 104857600L;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x0004F03E File Offset: 0x0004D23E
		public long GarbageCollectionLogFileMaxSize
		{
			get
			{
				return 10485760L;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x0004F046 File Offset: 0x0004D246
		public string PhotoRequestLoggingPath
		{
			get
			{
				return this.photoRequestLoggingPath;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x0004F04E File Offset: 0x0004D24E
		public TimeSpan PhotoRequestLogFileMaxAge
		{
			get
			{
				return PhotosConfiguration.DefaultPhotoRequestLogFileMaxAge;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x0004F055 File Offset: 0x0004D255
		public long PhotoRequestLogDirectoryMaxSize
		{
			get
			{
				return 104857600L;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x0004F05D File Offset: 0x0004D25D
		public long PhotoRequestLogFileMaxSize
		{
			get
			{
				return 10485760L;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x0004F065 File Offset: 0x0004D265
		public string PhotosRootDirectoryFullPath
		{
			get
			{
				return this.photosRootDirectoryPath;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x0004F06D File Offset: 0x0004D26D
		public int OutgoingPhotoRequestTimeoutMilliseconds
		{
			get
			{
				return 5000;
			}
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0004F074 File Offset: 0x0004D274
		private static string ComputePhotosRootDirectory(string exchangeInstallPath)
		{
			return Path.Combine(exchangeInstallPath, "ClientAccess\\photos");
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0004F081 File Offset: 0x0004D281
		private static string ComputeGarbageCollectionLoggingPath(string exchangeInstallPath)
		{
			return Path.Combine(exchangeInstallPath, "Logging\\PhotoGarbageCollector");
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0004F08E File Offset: 0x0004D28E
		private static string ComputePhotoRequestLoggingPath(string exchangeInstallPath)
		{
			return Path.Combine(exchangeInstallPath, "Logging\\photos");
		}

		// Token: 0x04000A92 RID: 2706
		internal const string GarbageCollectionLoggingComponentName = "PhotoGarbageCollector";

		// Token: 0x04000A93 RID: 2707
		internal const string PhotoRequestLoggingComponentName = "photos";

		// Token: 0x04000A94 RID: 2708
		private const UserPhotoSize DefaultPhotoSizeToUploadToAD = UserPhotoSize.HR64x64;

		// Token: 0x04000A95 RID: 2709
		private const string DefaultPhotoServiceEndpointRelativeToEwsWithLeadingSlash = "/s/GetUserPhoto";

		// Token: 0x04000A96 RID: 2710
		private const string PhotosPathRelativeToInstallPath = "ClientAccess\\photos";

		// Token: 0x04000A97 RID: 2711
		private const long DefaultGarbageCollectionLogDirectoryMaxSize = 104857600L;

		// Token: 0x04000A98 RID: 2712
		private const long DefaultGarbageCollectionLogFileMaxSize = 10485760L;

		// Token: 0x04000A99 RID: 2713
		private const long DefaultPhotoRequestLogDirectoryMaxSize = 104857600L;

		// Token: 0x04000A9A RID: 2714
		private const long DefaultPhotoRequestLogFileMaxSize = 10485760L;

		// Token: 0x04000A9B RID: 2715
		private const int DefaultOutgoingPhotoRequestTimeoutMilliseconds = 5000;

		// Token: 0x04000A9C RID: 2716
		private static readonly UserPhotoSize[] DefaultSizesToCacheOnFileSystem = new UserPhotoSize[]
		{
			UserPhotoSize.HR96x96,
			UserPhotoSize.HR648x648
		};

		// Token: 0x04000A9D RID: 2717
		private static readonly TimeSpan DefaultPhotoFileTimeToLive = TimeSpan.FromDays(7.0);

		// Token: 0x04000A9E RID: 2718
		private static readonly TimeSpan DefaultUserAgentCacheTimeToLive = TimeSpan.FromDays(3.0);

		// Token: 0x04000A9F RID: 2719
		private static readonly TimeSpan DefaultUserAgentCacheTimeToLiveNotFound = TimeSpan.FromDays(1.0);

		// Token: 0x04000AA0 RID: 2720
		private static readonly TimeSpan DefaultGarbageCollectionInterval = TimeSpan.FromDays(7.0);

		// Token: 0x04000AA1 RID: 2721
		private static readonly TimeSpan DefaultLastAccessGarbageThreshold = TimeSpan.FromDays(7.0);

		// Token: 0x04000AA2 RID: 2722
		private static readonly TimeSpan DefaultGarbageCollectionLogFileMaxAge = TimeSpan.FromDays(90.0);

		// Token: 0x04000AA3 RID: 2723
		private static readonly TimeSpan DefaultPhotoRequestLogFileMaxAge = TimeSpan.FromDays(30.0);

		// Token: 0x04000AA4 RID: 2724
		private readonly string exchangeInstallPath;

		// Token: 0x04000AA5 RID: 2725
		private readonly TimeSpan photoFileTimeToLive;

		// Token: 0x04000AA6 RID: 2726
		private readonly string photosRootDirectoryPath;

		// Token: 0x04000AA7 RID: 2727
		private readonly string garbageCollectionLoggingPath;

		// Token: 0x04000AA8 RID: 2728
		private readonly string photoRequestLoggingPath;
	}
}
