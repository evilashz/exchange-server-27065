using System;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000096 RID: 150
	[Serializable]
	public sealed class RegistryPushNotificationApp : RegistryObject, IPushNotificationRawSettings, IEquatable<IPushNotificationRawSettings>, IEquatable<RegistryPushNotificationApp>
	{
		// Token: 0x06000510 RID: 1296 RVA: 0x000115BF File Offset: 0x0000F7BF
		public RegistryPushNotificationApp()
		{
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000115C7 File Offset: 0x0000F7C7
		public RegistryPushNotificationApp(string name)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("name", name);
			this.propertyBag[SimpleProviderObjectSchema.Identity] = new RegistryObjectId("SYSTEM\\CurrentControlSet\\Services\\MSExchange PushNotifications\\Applications", name);
			base.ResetChangeTracking(true);
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x000115FC File Offset: 0x0000F7FC
		public string Name
		{
			get
			{
				return ((RegistryObjectId)this.Identity).Name;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0001160E File Offset: 0x0000F80E
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x00011620 File Offset: 0x0000F820
		public PushNotificationPlatform Platform
		{
			get
			{
				return (PushNotificationPlatform)this[RegistryPushNotificationApp.Schema.Platform];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.Platform] = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00011633 File Offset: 0x0000F833
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x00011645 File Offset: 0x0000F845
		public bool? Enabled
		{
			get
			{
				return (bool?)this[RegistryPushNotificationApp.Schema.Enabled];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.Enabled] = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00011658 File Offset: 0x0000F858
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x0001166A File Offset: 0x0000F86A
		public Version ExchangeMinimumVersion
		{
			get
			{
				return (Version)this[RegistryPushNotificationApp.Schema.ExchangeMinimumVersion];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.ExchangeMinimumVersion] = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00011678 File Offset: 0x0000F878
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x0001168A File Offset: 0x0000F88A
		public Version ExchangeMaximumVersion
		{
			get
			{
				return (Version)this[RegistryPushNotificationApp.Schema.ExchangeMaximumVersion];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.ExchangeMaximumVersion] = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00011698 File Offset: 0x0000F898
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x000116AA File Offset: 0x0000F8AA
		public int? QueueSize
		{
			get
			{
				return (int?)this[RegistryPushNotificationApp.Schema.QueueSize];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.QueueSize] = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x000116BD File Offset: 0x0000F8BD
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x000116CF File Offset: 0x0000F8CF
		public int? NumberOfChannels
		{
			get
			{
				return (int?)this[RegistryPushNotificationApp.Schema.NumberOfChannels];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.NumberOfChannels] = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000116E2 File Offset: 0x0000F8E2
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x000116F4 File Offset: 0x0000F8F4
		public int? BackOffTimeInSeconds
		{
			get
			{
				return (int?)this[RegistryPushNotificationApp.Schema.BackOffTimeInSeconds];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.BackOffTimeInSeconds] = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00011707 File Offset: 0x0000F907
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x00011719 File Offset: 0x0000F919
		public string AuthenticationId
		{
			get
			{
				return (string)this[RegistryPushNotificationApp.Schema.AuthenticationId];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.AuthenticationId] = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00011727 File Offset: 0x0000F927
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x00011739 File Offset: 0x0000F939
		public string AuthenticationKey
		{
			get
			{
				return (string)this[RegistryPushNotificationApp.Schema.AuthenticationKey];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.AuthenticationKey] = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00011747 File Offset: 0x0000F947
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00011759 File Offset: 0x0000F959
		public string AuthenticationKeyFallback
		{
			get
			{
				return (string)this[RegistryPushNotificationApp.Schema.AuthenticationKeyFallback];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.AuthenticationKeyFallback] = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00011767 File Offset: 0x0000F967
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x00011779 File Offset: 0x0000F979
		public bool? IsAuthenticationKeyEncrypted
		{
			get
			{
				return (bool?)this[RegistryPushNotificationApp.Schema.IsAuthenticationKeyEncrypted];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.IsAuthenticationKeyEncrypted] = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0001178C File Offset: 0x0000F98C
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x0001179E File Offset: 0x0000F99E
		public string Url
		{
			get
			{
				return (string)this[RegistryPushNotificationApp.Schema.Url];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.Url] = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x000117AC File Offset: 0x0000F9AC
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x000117BE File Offset: 0x0000F9BE
		public int? Port
		{
			get
			{
				return (int?)this[RegistryPushNotificationApp.Schema.Port];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.Port] = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x000117D1 File Offset: 0x0000F9D1
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x000117E3 File Offset: 0x0000F9E3
		public string SecondaryUrl
		{
			get
			{
				return (string)this[RegistryPushNotificationApp.Schema.SecondaryUrl];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.SecondaryUrl] = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x000117F1 File Offset: 0x0000F9F1
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x00011803 File Offset: 0x0000FA03
		public int? SecondaryPort
		{
			get
			{
				return (int?)this[RegistryPushNotificationApp.Schema.SecondaryPort];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.SecondaryPort] = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00011816 File Offset: 0x0000FA16
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x00011828 File Offset: 0x0000FA28
		public int? AddTimeout
		{
			get
			{
				return (int?)this[RegistryPushNotificationApp.Schema.AddTimeout];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.AddTimeout] = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0001183B File Offset: 0x0000FA3B
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0001184D File Offset: 0x0000FA4D
		public int? ConnectStepTimeout
		{
			get
			{
				return (int?)this[RegistryPushNotificationApp.Schema.ConnectStepTimeout];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.ConnectStepTimeout] = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00011860 File Offset: 0x0000FA60
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x00011872 File Offset: 0x0000FA72
		public int? ConnectTotalTimeout
		{
			get
			{
				return (int?)this[RegistryPushNotificationApp.Schema.ConnectTotalTimeout];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.ConnectTotalTimeout] = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00011885 File Offset: 0x0000FA85
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x00011897 File Offset: 0x0000FA97
		public int? ConnectRetryDelay
		{
			get
			{
				return (int?)this[RegistryPushNotificationApp.Schema.ConnectRetryDelay];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.ConnectRetryDelay] = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x000118AA File Offset: 0x0000FAAA
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x000118BC File Offset: 0x0000FABC
		public int? AuthenticateRetryMax
		{
			get
			{
				return (int?)this[RegistryPushNotificationApp.Schema.AuthenticateRetryMax];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.AuthenticateRetryMax] = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x000118CF File Offset: 0x0000FACF
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x000118E1 File Offset: 0x0000FAE1
		public int? ConnectRetryMax
		{
			get
			{
				return (int?)this[RegistryPushNotificationApp.Schema.ConnectRetryMax];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.ConnectRetryMax] = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x000118F4 File Offset: 0x0000FAF4
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x00011906 File Offset: 0x0000FB06
		public int? ReadTimeout
		{
			get
			{
				return (int?)this[RegistryPushNotificationApp.Schema.ReadTimeout];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.ReadTimeout] = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00011919 File Offset: 0x0000FB19
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x0001192B File Offset: 0x0000FB2B
		public int? WriteTimeout
		{
			get
			{
				return (int?)this[RegistryPushNotificationApp.Schema.WriteTimeout];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.WriteTimeout] = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0001193E File Offset: 0x0000FB3E
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x00011950 File Offset: 0x0000FB50
		public bool? IgnoreCertificateErrors
		{
			get
			{
				return (bool?)this[RegistryPushNotificationApp.Schema.IgnoreCertificateErrors];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.IgnoreCertificateErrors] = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00011963 File Offset: 0x0000FB63
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x00011975 File Offset: 0x0000FB75
		public string UriTemplate
		{
			get
			{
				return (string)this[RegistryPushNotificationApp.Schema.UriTemplate];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.UriTemplate] = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00011983 File Offset: 0x0000FB83
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x00011995 File Offset: 0x0000FB95
		public int? MaximumCacheSize
		{
			get
			{
				return (int?)this[RegistryPushNotificationApp.Schema.MaximumCacheSize];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.MaximumCacheSize] = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x000119A8 File Offset: 0x0000FBA8
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x000119BA File Offset: 0x0000FBBA
		public string RegistrationTemplate
		{
			get
			{
				return (string)this[RegistryPushNotificationApp.Schema.RegistrationTemplate];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.RegistrationTemplate] = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x000119C8 File Offset: 0x0000FBC8
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x000119DA File Offset: 0x0000FBDA
		public bool? RegistrationEnabled
		{
			get
			{
				return (bool?)this[RegistryPushNotificationApp.Schema.RegistrationEnabled];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.RegistrationEnabled] = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x000119ED File Offset: 0x0000FBED
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x000119FF File Offset: 0x0000FBFF
		public bool? MultifactorRegistrationEnabled
		{
			get
			{
				return (bool?)this[RegistryPushNotificationApp.Schema.MultifactorRegistrationEnabled];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.MultifactorRegistrationEnabled] = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00011A12 File Offset: 0x0000FC12
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x00011A24 File Offset: 0x0000FC24
		public string PartitionName
		{
			get
			{
				return (string)this[RegistryPushNotificationApp.Schema.PartitionName];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.PartitionName] = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00011A32 File Offset: 0x0000FC32
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x00011A44 File Offset: 0x0000FC44
		public bool? IsDefaultPartitionName
		{
			get
			{
				return (bool?)this[RegistryPushNotificationApp.Schema.IsDefaultPartitionName];
			}
			set
			{
				this[RegistryPushNotificationApp.Schema.IsDefaultPartitionName] = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00011A57 File Offset: 0x0000FC57
		internal override RegistryObjectSchema RegistrySchema
		{
			get
			{
				return RegistryPushNotificationApp.SchemaInstance;
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00011A60 File Offset: 0x0000FC60
		public string ToFullString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (PropertyDefinition propertyDefinition in this.RegistrySchema.AllProperties)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("; ");
				}
				stringBuilder.AppendFormat("{0}:{1}", propertyDefinition.Name, this[propertyDefinition].ToNullableString(null));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00011AF4 File Offset: 0x0000FCF4
		public override bool Equals(object obj)
		{
			return ConfigurableObject.AreEqual(this, obj as RegistryPushNotificationApp);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00011B02 File Offset: 0x0000FD02
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00011B0A File Offset: 0x0000FD0A
		public bool Equals(RegistryPushNotificationApp other)
		{
			return ConfigurableObject.AreEqual(this, other);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00011B13 File Offset: 0x0000FD13
		public bool Equals(IPushNotificationRawSettings other)
		{
			return ConfigurableObject.AreEqual(this, other as RegistryPushNotificationApp);
		}

		// Token: 0x04000274 RID: 628
		private static readonly RegistryPushNotificationApp.Schema SchemaInstance = ObjectSchema.GetInstance<RegistryPushNotificationApp.Schema>();

		// Token: 0x02000097 RID: 151
		internal class Schema : RegistryObjectSchema
		{
			// Token: 0x17000164 RID: 356
			// (get) Token: 0x06000559 RID: 1369 RVA: 0x00011B35 File Offset: 0x0000FD35
			public override string DefaultRegistryKeyPath
			{
				get
				{
					return "SYSTEM\\CurrentControlSet\\Services\\MSExchange PushNotifications\\Applications";
				}
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x0600055A RID: 1370 RVA: 0x00011B3C File Offset: 0x0000FD3C
			public override string DefaultName
			{
				get
				{
					return null;
				}
			}

			// Token: 0x04000275 RID: 629
			public const string RegistryRootName = "Applications";

			// Token: 0x04000276 RID: 630
			public const string RegistryRoot = "SYSTEM\\CurrentControlSet\\Services\\MSExchange PushNotifications\\Applications";

			// Token: 0x04000277 RID: 631
			public static readonly RegistryObjectId RootFolder = new RegistryObjectId("SYSTEM\\CurrentControlSet\\Services\\MSExchange PushNotifications\\Applications");

			// Token: 0x04000278 RID: 632
			public static readonly RegistryPropertyDefinition Platform = new RegistryPropertyDefinition("Platform", typeof(PushNotificationPlatform), PushNotificationPlatform.None);

			// Token: 0x04000279 RID: 633
			public static readonly RegistryPropertyDefinition Enabled = new RegistryPropertyDefinition("Enabled", typeof(bool?), null);

			// Token: 0x0400027A RID: 634
			public static readonly RegistryPropertyDefinition ExchangeMaximumVersion = new RegistryPropertyDefinition("ExchangeMaximumVersion", typeof(Version), null);

			// Token: 0x0400027B RID: 635
			public static readonly RegistryPropertyDefinition ExchangeMinimumVersion = new RegistryPropertyDefinition("ExchangeMinimumVersion", typeof(Version), null);

			// Token: 0x0400027C RID: 636
			public static readonly RegistryPropertyDefinition QueueSize = new RegistryPropertyDefinition("QueueSize", typeof(int?), null);

			// Token: 0x0400027D RID: 637
			public static readonly RegistryPropertyDefinition NumberOfChannels = new RegistryPropertyDefinition("NumberOfChannels", typeof(int?), null);

			// Token: 0x0400027E RID: 638
			public static readonly RegistryPropertyDefinition BackOffTimeInSeconds = new RegistryPropertyDefinition("BackOffTimeInSeconds", typeof(int?), null);

			// Token: 0x0400027F RID: 639
			public static readonly RegistryPropertyDefinition AuthenticationId = new RegistryPropertyDefinition("AuthenticationId", typeof(string), null);

			// Token: 0x04000280 RID: 640
			public static readonly RegistryPropertyDefinition AuthenticationKey = new RegistryPropertyDefinition("AuthenticationKey", typeof(string), null);

			// Token: 0x04000281 RID: 641
			public static readonly RegistryPropertyDefinition AuthenticationKeyFallback = new RegistryPropertyDefinition("AuthenticationKeyFallback", typeof(string), null);

			// Token: 0x04000282 RID: 642
			public static readonly RegistryPropertyDefinition IsAuthenticationKeyEncrypted = new RegistryPropertyDefinition("IsAuthenticationKeyEncrypted", typeof(bool?), null);

			// Token: 0x04000283 RID: 643
			public static readonly RegistryPropertyDefinition Url = new RegistryPropertyDefinition("Url", typeof(string), null);

			// Token: 0x04000284 RID: 644
			public static readonly RegistryPropertyDefinition Port = new RegistryPropertyDefinition("Port", typeof(int?), null);

			// Token: 0x04000285 RID: 645
			public static readonly RegistryPropertyDefinition SecondaryUrl = new RegistryPropertyDefinition("SecondaryUrl", typeof(string), null);

			// Token: 0x04000286 RID: 646
			public static readonly RegistryPropertyDefinition SecondaryPort = new RegistryPropertyDefinition("SecondaryPort", typeof(int?), null);

			// Token: 0x04000287 RID: 647
			public static readonly RegistryPropertyDefinition AddTimeout = new RegistryPropertyDefinition("AddTimeout", typeof(int?), null);

			// Token: 0x04000288 RID: 648
			public static readonly RegistryPropertyDefinition ConnectStepTimeout = new RegistryPropertyDefinition("ConnectStepTimeout", typeof(int?), null);

			// Token: 0x04000289 RID: 649
			public static readonly RegistryPropertyDefinition ConnectTotalTimeout = new RegistryPropertyDefinition("ConnectTotalTimeout", typeof(int?), null);

			// Token: 0x0400028A RID: 650
			public static readonly RegistryPropertyDefinition ConnectRetryDelay = new RegistryPropertyDefinition("ConnectRetryDelay", typeof(int?), null);

			// Token: 0x0400028B RID: 651
			public static readonly RegistryPropertyDefinition AuthenticateRetryMax = new RegistryPropertyDefinition("AuthenticateRetryMax", typeof(int?), null);

			// Token: 0x0400028C RID: 652
			public static readonly RegistryPropertyDefinition ConnectRetryMax = new RegistryPropertyDefinition("ConnectRetryMax", typeof(int?), null);

			// Token: 0x0400028D RID: 653
			public static readonly RegistryPropertyDefinition ReadTimeout = new RegistryPropertyDefinition("ReadTimeout", typeof(int?), null);

			// Token: 0x0400028E RID: 654
			public static readonly RegistryPropertyDefinition WriteTimeout = new RegistryPropertyDefinition("WriteTimeout", typeof(int?), null);

			// Token: 0x0400028F RID: 655
			public static readonly RegistryPropertyDefinition IgnoreCertificateErrors = new RegistryPropertyDefinition("IgnoreCertificateErrors", typeof(bool?), null);

			// Token: 0x04000290 RID: 656
			public static readonly RegistryPropertyDefinition UriTemplate = new RegistryPropertyDefinition("UriTemplate", typeof(string), null);

			// Token: 0x04000291 RID: 657
			public static readonly RegistryPropertyDefinition MaximumCacheSize = new RegistryPropertyDefinition("MaximumCacheSize", typeof(int?), null);

			// Token: 0x04000292 RID: 658
			public static readonly RegistryPropertyDefinition RegistrationTemplate = new RegistryPropertyDefinition("RegistrationTemplate", typeof(string), null);

			// Token: 0x04000293 RID: 659
			public static readonly RegistryPropertyDefinition RegistrationEnabled = new RegistryPropertyDefinition("RegistrationEnabled", typeof(bool?), null);

			// Token: 0x04000294 RID: 660
			public static readonly RegistryPropertyDefinition MultifactorRegistrationEnabled = new RegistryPropertyDefinition("MultifactorRegistrationEnabled", typeof(bool?), null);

			// Token: 0x04000295 RID: 661
			public static readonly RegistryPropertyDefinition PartitionName = new RegistryPropertyDefinition("PartitionName", typeof(string), null);

			// Token: 0x04000296 RID: 662
			public static readonly RegistryPropertyDefinition IsDefaultPartitionName = new RegistryPropertyDefinition("IsDefaultPartitionName", typeof(bool?), null);
		}
	}
}
