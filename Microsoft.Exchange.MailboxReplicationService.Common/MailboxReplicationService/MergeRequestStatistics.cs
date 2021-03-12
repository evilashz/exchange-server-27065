using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001FD RID: 509
	[Serializable]
	public sealed class MergeRequestStatistics : RequestStatisticsBase
	{
		// Token: 0x06001814 RID: 6164 RVA: 0x00032F7C File Offset: 0x0003117C
		public MergeRequestStatistics()
		{
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x00032F84 File Offset: 0x00031184
		internal MergeRequestStatistics(SimpleProviderPropertyBag propertyBag) : base(propertyBag)
		{
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x00032F8D File Offset: 0x0003118D
		internal MergeRequestStatistics(TransactionalRequestJob requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00032FA7 File Offset: 0x000311A7
		internal MergeRequestStatistics(RequestJobXML requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x00032FC1 File Offset: 0x000311C1
		// (set) Token: 0x06001819 RID: 6169 RVA: 0x00032FC9 File Offset: 0x000311C9
		public new string Name
		{
			get
			{
				return base.Name;
			}
			internal set
			{
				base.Name = value;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x00032FD2 File Offset: 0x000311D2
		// (set) Token: 0x0600181B RID: 6171 RVA: 0x00032FDA File Offset: 0x000311DA
		public new RequestStatus Status
		{
			get
			{
				return base.Status;
			}
			internal set
			{
				base.Status = value;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x00032FE3 File Offset: 0x000311E3
		public new RequestState StatusDetail
		{
			get
			{
				return base.StatusDetail;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x00032FEB File Offset: 0x000311EB
		// (set) Token: 0x0600181E RID: 6174 RVA: 0x00032FF3 File Offset: 0x000311F3
		public new SyncStage SyncStage
		{
			get
			{
				return base.SyncStage;
			}
			internal set
			{
				base.SyncStage = value;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x00032FFC File Offset: 0x000311FC
		// (set) Token: 0x06001820 RID: 6176 RVA: 0x00033004 File Offset: 0x00031204
		public new RequestFlags Flags
		{
			get
			{
				return base.Flags;
			}
			internal set
			{
				base.Flags = value;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x0003300D File Offset: 0x0003120D
		// (set) Token: 0x06001822 RID: 6178 RVA: 0x00033015 File Offset: 0x00031215
		public new RequestStyle RequestStyle
		{
			get
			{
				return base.RequestStyle;
			}
			internal set
			{
				base.RequestStyle = value;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x0003301E File Offset: 0x0003121E
		// (set) Token: 0x06001824 RID: 6180 RVA: 0x00033026 File Offset: 0x00031226
		public new RequestDirection Direction
		{
			get
			{
				return base.Direction;
			}
			internal set
			{
				base.Direction = value;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x0003302F File Offset: 0x0003122F
		// (set) Token: 0x06001826 RID: 6182 RVA: 0x00033037 File Offset: 0x00031237
		public new bool Protect
		{
			get
			{
				return base.Protect;
			}
			internal set
			{
				base.Protect = value;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x00033040 File Offset: 0x00031240
		// (set) Token: 0x06001828 RID: 6184 RVA: 0x00033048 File Offset: 0x00031248
		public new RequestPriority Priority
		{
			get
			{
				return base.Priority;
			}
			internal set
			{
				base.Priority = value;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x00033051 File Offset: 0x00031251
		// (set) Token: 0x0600182A RID: 6186 RVA: 0x00033059 File Offset: 0x00031259
		public new RequestWorkloadType WorkloadType
		{
			get
			{
				return base.WorkloadType;
			}
			internal set
			{
				base.WorkloadType = value;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x00033062 File Offset: 0x00031262
		// (set) Token: 0x0600182C RID: 6188 RVA: 0x0003306A File Offset: 0x0003126A
		public new bool Suspend
		{
			get
			{
				return base.Suspend;
			}
			internal set
			{
				base.Suspend = value;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x00033073 File Offset: 0x00031273
		// (set) Token: 0x0600182E RID: 6190 RVA: 0x0003307B File Offset: 0x0003127B
		public new string SourceAlias
		{
			get
			{
				return base.SourceAlias;
			}
			internal set
			{
				base.SourceAlias = value;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x00033084 File Offset: 0x00031284
		// (set) Token: 0x06001830 RID: 6192 RVA: 0x0003308C File Offset: 0x0003128C
		public new bool SourceIsArchive
		{
			get
			{
				return base.SourceIsArchive;
			}
			internal set
			{
				base.SourceIsArchive = value;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x00033095 File Offset: 0x00031295
		// (set) Token: 0x06001832 RID: 6194 RVA: 0x0003309D File Offset: 0x0003129D
		public new Guid SourceExchangeGuid
		{
			get
			{
				return base.SourceExchangeGuid;
			}
			internal set
			{
				base.SourceExchangeGuid = value;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x000330A6 File Offset: 0x000312A6
		// (set) Token: 0x06001834 RID: 6196 RVA: 0x000330AE File Offset: 0x000312AE
		public new string SourceRootFolder
		{
			get
			{
				return base.SourceRootFolder;
			}
			internal set
			{
				base.SourceRootFolder = value;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x000330B7 File Offset: 0x000312B7
		// (set) Token: 0x06001836 RID: 6198 RVA: 0x000330BF File Offset: 0x000312BF
		public new RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return base.RecipientTypeDetails;
			}
			internal set
			{
				base.RecipientTypeDetails = value;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x000330C8 File Offset: 0x000312C8
		// (set) Token: 0x06001838 RID: 6200 RVA: 0x000330D5 File Offset: 0x000312D5
		public new ServerVersion SourceVersion
		{
			get
			{
				return new ServerVersion(base.SourceVersion);
			}
			set
			{
				base.SourceVersion = value.ToInt();
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x000330E3 File Offset: 0x000312E3
		// (set) Token: 0x0600183A RID: 6202 RVA: 0x000330EB File Offset: 0x000312EB
		public new ADObjectId SourceDatabase
		{
			get
			{
				return base.SourceDatabase;
			}
			internal set
			{
				base.SourceDatabase = value;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x000330F4 File Offset: 0x000312F4
		// (set) Token: 0x0600183C RID: 6204 RVA: 0x000330FC File Offset: 0x000312FC
		public new string SourceServer
		{
			get
			{
				return base.SourceServer;
			}
			internal set
			{
				base.SourceServer = value;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x00033105 File Offset: 0x00031305
		// (set) Token: 0x0600183E RID: 6206 RVA: 0x0003310D File Offset: 0x0003130D
		public ADObjectId SourceMailboxIdentity
		{
			get
			{
				return base.SourceUserId;
			}
			internal set
			{
				base.SourceUserId = value;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x00033116 File Offset: 0x00031316
		// (set) Token: 0x06001840 RID: 6208 RVA: 0x0003311E File Offset: 0x0003131E
		public new string TargetAlias
		{
			get
			{
				return base.TargetAlias;
			}
			internal set
			{
				base.TargetAlias = value;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x00033127 File Offset: 0x00031327
		// (set) Token: 0x06001842 RID: 6210 RVA: 0x0003312F File Offset: 0x0003132F
		public new bool TargetIsArchive
		{
			get
			{
				return base.TargetIsArchive;
			}
			internal set
			{
				base.TargetIsArchive = value;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x00033138 File Offset: 0x00031338
		// (set) Token: 0x06001844 RID: 6212 RVA: 0x00033140 File Offset: 0x00031340
		public new Guid TargetExchangeGuid
		{
			get
			{
				return base.TargetExchangeGuid;
			}
			internal set
			{
				base.TargetExchangeGuid = value;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x00033149 File Offset: 0x00031349
		// (set) Token: 0x06001846 RID: 6214 RVA: 0x00033151 File Offset: 0x00031351
		public new string TargetRootFolder
		{
			get
			{
				return base.TargetRootFolder;
			}
			internal set
			{
				base.TargetRootFolder = value;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x0003315A File Offset: 0x0003135A
		// (set) Token: 0x06001848 RID: 6216 RVA: 0x00033167 File Offset: 0x00031367
		public new ServerVersion TargetVersion
		{
			get
			{
				return new ServerVersion(base.TargetVersion);
			}
			set
			{
				base.TargetVersion = value.ToInt();
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x00033175 File Offset: 0x00031375
		// (set) Token: 0x0600184A RID: 6218 RVA: 0x0003317D File Offset: 0x0003137D
		public new ADObjectId TargetDatabase
		{
			get
			{
				return base.TargetDatabase;
			}
			internal set
			{
				base.TargetDatabase = value;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x00033186 File Offset: 0x00031386
		// (set) Token: 0x0600184C RID: 6220 RVA: 0x0003318E File Offset: 0x0003138E
		public new string TargetServer
		{
			get
			{
				return base.TargetServer;
			}
			internal set
			{
				base.TargetServer = value;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x00033197 File Offset: 0x00031397
		// (set) Token: 0x0600184E RID: 6222 RVA: 0x0003319F File Offset: 0x0003139F
		public ADObjectId TargetMailboxIdentity
		{
			get
			{
				return base.TargetUserId;
			}
			internal set
			{
				base.TargetUserId = value;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x000331A8 File Offset: 0x000313A8
		// (set) Token: 0x06001850 RID: 6224 RVA: 0x000331B0 File Offset: 0x000313B0
		public new string[] IncludeFolders
		{
			get
			{
				return base.IncludeFolders;
			}
			internal set
			{
				base.IncludeFolders = value;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x000331B9 File Offset: 0x000313B9
		// (set) Token: 0x06001852 RID: 6226 RVA: 0x000331C1 File Offset: 0x000313C1
		public new string[] ExcludeFolders
		{
			get
			{
				return base.ExcludeFolders;
			}
			internal set
			{
				base.ExcludeFolders = value;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x000331CA File Offset: 0x000313CA
		// (set) Token: 0x06001854 RID: 6228 RVA: 0x000331D2 File Offset: 0x000313D2
		public new bool ExcludeDumpster
		{
			get
			{
				return base.ExcludeDumpster;
			}
			internal set
			{
				base.ExcludeDumpster = value;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x000331DB File Offset: 0x000313DB
		// (set) Token: 0x06001856 RID: 6230 RVA: 0x000331E3 File Offset: 0x000313E3
		public new ConflictResolutionOption? ConflictResolutionOption
		{
			get
			{
				return base.ConflictResolutionOption;
			}
			set
			{
				base.ConflictResolutionOption = value;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x000331EC File Offset: 0x000313EC
		// (set) Token: 0x06001858 RID: 6232 RVA: 0x000331F4 File Offset: 0x000313F4
		public new FAICopyOption? AssociatedMessagesCopyOption
		{
			get
			{
				return base.AssociatedMessagesCopyOption;
			}
			set
			{
				base.AssociatedMessagesCopyOption = value;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x000331FD File Offset: 0x000313FD
		// (set) Token: 0x0600185A RID: 6234 RVA: 0x00033205 File Offset: 0x00031405
		public new string RemoteHostName
		{
			get
			{
				return base.RemoteHostName;
			}
			internal set
			{
				base.RemoteHostName = value;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x0003320E File Offset: 0x0003140E
		// (set) Token: 0x0600185C RID: 6236 RVA: 0x00033216 File Offset: 0x00031416
		public new string OutlookAnywhereHostName
		{
			get
			{
				return base.OutlookAnywhereHostName;
			}
			internal set
			{
				base.OutlookAnywhereHostName = value;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x0003321F File Offset: 0x0003141F
		public new string RemoteGlobalCatalog
		{
			get
			{
				return base.RemoteGlobalCatalog;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x00033227 File Offset: 0x00031427
		// (set) Token: 0x0600185F RID: 6239 RVA: 0x0003322F File Offset: 0x0003142F
		public new string BatchName
		{
			get
			{
				return base.BatchName;
			}
			internal set
			{
				base.BatchName = value;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x00033238 File Offset: 0x00031438
		// (set) Token: 0x06001861 RID: 6241 RVA: 0x00033240 File Offset: 0x00031440
		public new string RemoteCredentialUsername
		{
			get
			{
				return base.RemoteCredentialUsername;
			}
			internal set
			{
				base.RemoteCredentialUsername = value;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x00033249 File Offset: 0x00031449
		// (set) Token: 0x06001863 RID: 6243 RVA: 0x00033251 File Offset: 0x00031451
		public new bool? IsAdministrativeCredential
		{
			get
			{
				return base.IsAdministrativeCredential;
			}
			internal set
			{
				base.IsAdministrativeCredential = value;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x0003325A File Offset: 0x0003145A
		// (set) Token: 0x06001865 RID: 6245 RVA: 0x00033262 File Offset: 0x00031462
		public new AuthenticationMethod? AuthenticationMethod
		{
			get
			{
				return base.AuthenticationMethod;
			}
			internal set
			{
				base.AuthenticationMethod = value;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x0003326B File Offset: 0x0003146B
		// (set) Token: 0x06001867 RID: 6247 RVA: 0x00033273 File Offset: 0x00031473
		public new string RemoteMailboxLegacyDN
		{
			get
			{
				return base.RemoteMailboxLegacyDN;
			}
			internal set
			{
				base.RemoteMailboxLegacyDN = value;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x0003327C File Offset: 0x0003147C
		// (set) Token: 0x06001869 RID: 6249 RVA: 0x00033284 File Offset: 0x00031484
		public new string RemoteUserLegacyDN
		{
			get
			{
				return base.RemoteUserLegacyDN;
			}
			internal set
			{
				base.RemoteUserLegacyDN = value;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x0003328D File Offset: 0x0003148D
		// (set) Token: 0x0600186B RID: 6251 RVA: 0x00033295 File Offset: 0x00031495
		public new string RemoteMailboxServerLegacyDN
		{
			get
			{
				return base.RemoteMailboxServerLegacyDN;
			}
			internal set
			{
				base.RemoteMailboxServerLegacyDN = value;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x0003329E File Offset: 0x0003149E
		// (set) Token: 0x0600186D RID: 6253 RVA: 0x000332A6 File Offset: 0x000314A6
		public new string RemoteDatabaseName
		{
			get
			{
				return base.RemoteDatabaseName;
			}
			internal set
			{
				base.RemoteDatabaseName = value;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x000332AF File Offset: 0x000314AF
		// (set) Token: 0x0600186F RID: 6255 RVA: 0x000332B7 File Offset: 0x000314B7
		public new Guid? RemoteDatabaseGuid
		{
			get
			{
				return base.RemoteDatabaseGuid;
			}
			internal set
			{
				base.RemoteDatabaseGuid = value;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x000332C0 File Offset: 0x000314C0
		// (set) Token: 0x06001871 RID: 6257 RVA: 0x000332C8 File Offset: 0x000314C8
		public new string ContentFilter
		{
			get
			{
				return base.ContentFilter;
			}
			internal set
			{
				base.ContentFilter = value;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x000332D1 File Offset: 0x000314D1
		// (set) Token: 0x06001873 RID: 6259 RVA: 0x000332DE File Offset: 0x000314DE
		public CultureInfo ContentFilterLanguage
		{
			get
			{
				return new CultureInfo(base.ContentFilterLCID);
			}
			internal set
			{
				base.ContentFilterLCID = value.LCID;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x000332EC File Offset: 0x000314EC
		// (set) Token: 0x06001875 RID: 6261 RVA: 0x000332F4 File Offset: 0x000314F4
		public new Unlimited<int> BadItemLimit
		{
			get
			{
				return base.BadItemLimit;
			}
			internal set
			{
				base.BadItemLimit = value;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x000332FD File Offset: 0x000314FD
		// (set) Token: 0x06001877 RID: 6263 RVA: 0x00033305 File Offset: 0x00031505
		public new int BadItemsEncountered
		{
			get
			{
				return base.BadItemsEncountered;
			}
			internal set
			{
				base.BadItemsEncountered = value;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x0003330E File Offset: 0x0003150E
		// (set) Token: 0x06001879 RID: 6265 RVA: 0x00033316 File Offset: 0x00031516
		public new Unlimited<int> LargeItemLimit
		{
			get
			{
				return base.LargeItemLimit;
			}
			internal set
			{
				base.LargeItemLimit = value;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x0003331F File Offset: 0x0003151F
		// (set) Token: 0x0600187B RID: 6267 RVA: 0x00033327 File Offset: 0x00031527
		public new int LargeItemsEncountered
		{
			get
			{
				return base.LargeItemsEncountered;
			}
			internal set
			{
				base.LargeItemsEncountered = value;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x00033330 File Offset: 0x00031530
		public DateTime? QueuedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Creation);
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x0003333E File Offset: 0x0003153E
		public DateTime? StartTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Start);
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x0003334C File Offset: 0x0003154C
		public DateTime? LastUpdateTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.LastUpdate);
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x0003335A File Offset: 0x0003155A
		public DateTime? InitialSeedingCompletedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.InitialSeedingCompleted);
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x00033368 File Offset: 0x00031568
		public DateTime? CompletionTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Completion);
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x00033376 File Offset: 0x00031576
		public DateTime? SuspendedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Suspended);
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x00033384 File Offset: 0x00031584
		public DateTime? StartAfter
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.StartAfter);
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x00033393 File Offset: 0x00031593
		public EnhancedTimeSpan? OverallDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.OverallMove).Duration);
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x000333B0 File Offset: 0x000315B0
		public EnhancedTimeSpan? TotalSuspendedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Suspended).Duration);
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x000333CE File Offset: 0x000315CE
		public EnhancedTimeSpan? TotalFailedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Failed).Duration);
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x000333EC File Offset: 0x000315EC
		public EnhancedTimeSpan? TotalQueuedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Queued).Duration);
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x00033409 File Offset: 0x00031609
		public EnhancedTimeSpan? TotalInProgressDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.InProgress).Duration);
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x00033426 File Offset: 0x00031626
		public EnhancedTimeSpan? TotalStalledDueToCIDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToCI).Duration);
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x00033444 File Offset: 0x00031644
		public EnhancedTimeSpan? TotalStalledDueToHADuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToHA).Duration);
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x00033462 File Offset: 0x00031662
		public EnhancedTimeSpan? TotalStalledDueToMailboxLockedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToMailboxLock).Duration);
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x00033480 File Offset: 0x00031680
		public EnhancedTimeSpan? TotalStalledDueToReadThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadThrottle).Duration);
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x0600188C RID: 6284 RVA: 0x0003349E File Offset: 0x0003169E
		public EnhancedTimeSpan? TotalStalledDueToWriteThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteThrottle).Duration);
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x000334BC File Offset: 0x000316BC
		public EnhancedTimeSpan? TotalStalledDueToReadCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadCpu).Duration);
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x000334DA File Offset: 0x000316DA
		public EnhancedTimeSpan? TotalStalledDueToWriteCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteCpu).Duration);
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x000334F8 File Offset: 0x000316F8
		public EnhancedTimeSpan? TotalStalledDueToReadUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadUnknown).Duration);
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x00033516 File Offset: 0x00031716
		public EnhancedTimeSpan? TotalStalledDueToWriteUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteUnknown).Duration);
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x00033534 File Offset: 0x00031734
		public EnhancedTimeSpan? TotalTransientFailureDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.TransientFailure).Duration);
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x00033552 File Offset: 0x00031752
		public EnhancedTimeSpan? TotalIdleDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Idle).Duration);
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x00033570 File Offset: 0x00031770
		// (set) Token: 0x06001894 RID: 6292 RVA: 0x00033578 File Offset: 0x00031778
		public new string MRSServerName
		{
			get
			{
				return base.MRSServerName;
			}
			internal set
			{
				base.MRSServerName = value;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x00033581 File Offset: 0x00031781
		// (set) Token: 0x06001896 RID: 6294 RVA: 0x0003358E File Offset: 0x0003178E
		public ByteQuantifiedSize EstimatedTransferSize
		{
			get
			{
				return new ByteQuantifiedSize(base.TotalMailboxSize);
			}
			internal set
			{
				base.TotalMailboxSize = value.ToBytes();
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x0003359D File Offset: 0x0003179D
		// (set) Token: 0x06001898 RID: 6296 RVA: 0x000335A5 File Offset: 0x000317A5
		public ulong EstimatedTransferItemCount
		{
			get
			{
				return base.TotalMailboxItemCount;
			}
			internal set
			{
				base.TotalMailboxItemCount = value;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x000335AE File Offset: 0x000317AE
		public override ByteQuantifiedSize? BytesTransferred
		{
			get
			{
				return base.BytesTransferred;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x000335B6 File Offset: 0x000317B6
		public override ByteQuantifiedSize? BytesTransferredPerMinute
		{
			get
			{
				return base.BytesTransferredPerMinute;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x000335BE File Offset: 0x000317BE
		public override ulong? ItemsTransferred
		{
			get
			{
				return base.ItemsTransferred;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x000335C6 File Offset: 0x000317C6
		// (set) Token: 0x0600189D RID: 6301 RVA: 0x000335CE File Offset: 0x000317CE
		public new int PercentComplete
		{
			get
			{
				return base.PercentComplete;
			}
			internal set
			{
				base.PercentComplete = value;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x000335D7 File Offset: 0x000317D7
		// (set) Token: 0x0600189F RID: 6303 RVA: 0x000335DF File Offset: 0x000317DF
		public new Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
		{
			get
			{
				return base.CompletedRequestAgeLimit;
			}
			internal set
			{
				base.CompletedRequestAgeLimit = value;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x000335E8 File Offset: 0x000317E8
		// (set) Token: 0x060018A1 RID: 6305 RVA: 0x000335F0 File Offset: 0x000317F0
		public override LocalizedString PositionInQueue
		{
			get
			{
				return base.PositionInQueue;
			}
			internal set
			{
				base.PositionInQueue = value;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060018A2 RID: 6306 RVA: 0x000335F9 File Offset: 0x000317F9
		// (set) Token: 0x060018A3 RID: 6307 RVA: 0x00033601 File Offset: 0x00031801
		public new bool SuspendWhenReadyToComplete
		{
			get
			{
				return base.SuspendWhenReadyToComplete;
			}
			internal set
			{
				base.SuspendWhenReadyToComplete = value;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x0003360A File Offset: 0x0003180A
		// (set) Token: 0x060018A5 RID: 6309 RVA: 0x00033612 File Offset: 0x00031812
		public RequestJobInternalFlags InternalFlags
		{
			get
			{
				return base.RequestJobInternalFlags;
			}
			internal set
			{
				base.RequestJobInternalFlags = value;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x0003361B File Offset: 0x0003181B
		// (set) Token: 0x060018A7 RID: 6311 RVA: 0x00033623 File Offset: 0x00031823
		public new int? FailureCode
		{
			get
			{
				return base.FailureCode;
			}
			internal set
			{
				base.FailureCode = value;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x0003362C File Offset: 0x0003182C
		// (set) Token: 0x060018A9 RID: 6313 RVA: 0x00033634 File Offset: 0x00031834
		public new string FailureType
		{
			get
			{
				return base.FailureType;
			}
			internal set
			{
				base.FailureType = value;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x0003363D File Offset: 0x0003183D
		// (set) Token: 0x060018AB RID: 6315 RVA: 0x00033645 File Offset: 0x00031845
		public new ExceptionSide? FailureSide
		{
			get
			{
				return base.FailureSide;
			}
			internal set
			{
				base.FailureSide = value;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0003364E File Offset: 0x0003184E
		// (set) Token: 0x060018AD RID: 6317 RVA: 0x00033656 File Offset: 0x00031856
		public new LocalizedString Message
		{
			get
			{
				return base.Message;
			}
			internal set
			{
				base.Message = value;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x0003365F File Offset: 0x0003185F
		public DateTime? FailureTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Failure);
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x0003366E File Offset: 0x0003186E
		public override bool IsValid
		{
			get
			{
				return base.IsValid;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x00033676 File Offset: 0x00031876
		// (set) Token: 0x060018B1 RID: 6321 RVA: 0x0003367E File Offset: 0x0003187E
		public new LocalizedString ValidationMessage
		{
			get
			{
				return base.ValidationMessage;
			}
			internal set
			{
				base.ValidationMessage = value;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x00033687 File Offset: 0x00031887
		// (set) Token: 0x060018B3 RID: 6323 RVA: 0x0003368F File Offset: 0x0003188F
		public new OrganizationId OrganizationId
		{
			get
			{
				return base.OrganizationId;
			}
			internal set
			{
				base.OrganizationId = value;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x00033698 File Offset: 0x00031898
		// (set) Token: 0x060018B5 RID: 6325 RVA: 0x000336A0 File Offset: 0x000318A0
		public new Guid RequestGuid
		{
			get
			{
				return base.RequestGuid;
			}
			internal set
			{
				base.RequestGuid = value;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x000336A9 File Offset: 0x000318A9
		// (set) Token: 0x060018B7 RID: 6327 RVA: 0x000336B1 File Offset: 0x000318B1
		public new ADObjectId RequestQueue
		{
			get
			{
				return base.RequestQueue;
			}
			internal set
			{
				base.RequestQueue = value;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x000336BA File Offset: 0x000318BA
		// (set) Token: 0x060018B9 RID: 6329 RVA: 0x000336C2 File Offset: 0x000318C2
		public new ObjectId Identity
		{
			get
			{
				return base.Identity;
			}
			internal set
			{
				base.Identity = (value as RequestJobObjectId);
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x000336D0 File Offset: 0x000318D0
		public new string DiagnosticInfo
		{
			get
			{
				return base.DiagnosticInfo;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x000336D8 File Offset: 0x000318D8
		// (set) Token: 0x060018BC RID: 6332 RVA: 0x000336E0 File Offset: 0x000318E0
		public override Report Report
		{
			get
			{
				return base.Report;
			}
			internal set
			{
				base.Report = value;
			}
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x000336EC File Offset: 0x000318EC
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.Name) || ((string.IsNullOrEmpty(this.TargetAlias) || this.Direction != RequestDirection.Pull) && (string.IsNullOrEmpty(this.SourceAlias) || this.Direction != RequestDirection.Push)))
			{
				return base.ToString();
			}
			if (this.Direction == RequestDirection.Pull)
			{
				return string.Format("{0}\\{1}", this.TargetAlias, this.Name);
			}
			return string.Format("{0}\\{1}", this.SourceAlias, this.Name);
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00033770 File Offset: 0x00031970
		internal static void ValidateRequestJob(RequestJobBase requestJob)
		{
			if (requestJob.IsFake || requestJob.WorkItemQueueMdb == null)
			{
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMissing);
				requestJob.ValidationMessage = MrsStrings.ValidationMoveRequestNotDeserialized;
				return;
			}
			if (requestJob.OriginatingMDBGuid != Guid.Empty && requestJob.OriginatingMDBGuid != requestJob.WorkItemQueueMdb.ObjectGuid)
			{
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Orphaned);
				requestJob.ValidationMessage = MrsStrings.ValidationMoveRequestInWrongMDB(requestJob.OriginatingMDBGuid, requestJob.WorkItemQueueMdb.ObjectGuid);
				return;
			}
			if (requestJob.CancelRequest)
			{
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
				requestJob.ValidationMessage = LocalizedString.Empty;
				return;
			}
			if (requestJob.Status == RequestStatus.Completed || requestJob.Status == RequestStatus.CompletedWithWarning)
			{
				MergeRequestStatistics.LoadAdditionalPropertiesFromUsers(requestJob);
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
				requestJob.ValidationMessage = LocalizedString.Empty;
				return;
			}
			if (requestJob.SourceIsLocal)
			{
				if (!requestJob.ValidateUser(requestJob.SourceUser, requestJob.SourceUserId))
				{
					return;
				}
				if (!requestJob.ValidateMailbox(requestJob.SourceUser, requestJob.SourceIsArchive))
				{
					return;
				}
			}
			else if (!requestJob.ValidateOutlookAnywhereParams())
			{
				return;
			}
			if (requestJob.TargetIsLocal)
			{
				if (!requestJob.ValidateUser(requestJob.TargetUser, requestJob.TargetUserId))
				{
					return;
				}
				if (!requestJob.ValidateMailbox(requestJob.TargetUser, requestJob.TargetIsArchive))
				{
					return;
				}
			}
			else if (!requestJob.ValidateOutlookAnywhereParams())
			{
				return;
			}
			MergeRequestStatistics.LoadAdditionalPropertiesFromUsers(requestJob);
			if (!requestJob.ValidateRequestIndexEntries())
			{
				return;
			}
			requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
			requestJob.ValidationMessage = LocalizedString.Empty;
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x000338E8 File Offset: 0x00031AE8
		private static void LoadAdditionalPropertiesFromUsers(RequestJobBase requestJob)
		{
			if (requestJob.SourceUser != null)
			{
				requestJob.SourceAlias = requestJob.SourceUser.Alias;
				requestJob.SourceExchangeGuid = (requestJob.SourceIsArchive ? requestJob.SourceUser.ArchiveGuid : requestJob.SourceUser.ExchangeGuid);
				requestJob.SourceDatabase = ADObjectIdResolutionHelper.ResolveDN(requestJob.SourceIsArchive ? requestJob.SourceUser.ArchiveDatabase : requestJob.SourceUser.Database);
				requestJob.SourceUserId = requestJob.SourceUser.Id;
			}
			if (requestJob.TargetUser != null)
			{
				requestJob.TargetAlias = requestJob.TargetUser.Alias;
				requestJob.TargetExchangeGuid = (requestJob.TargetIsArchive ? requestJob.TargetUser.ArchiveGuid : requestJob.TargetUser.ExchangeGuid);
				requestJob.TargetDatabase = ADObjectIdResolutionHelper.ResolveDN(requestJob.TargetIsArchive ? requestJob.TargetUser.ArchiveDatabase : requestJob.TargetUser.Database);
				requestJob.RecipientTypeDetails = requestJob.TargetUser.RecipientTypeDetails;
				requestJob.TargetUserId = requestJob.TargetUser.Id;
			}
		}
	}
}
