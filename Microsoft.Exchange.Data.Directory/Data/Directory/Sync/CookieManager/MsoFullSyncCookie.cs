using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007D9 RID: 2009
	[Serializable]
	internal sealed class MsoFullSyncCookie
	{
		// Token: 0x17002354 RID: 9044
		// (get) Token: 0x0600638E RID: 25486 RVA: 0x00159677 File Offset: 0x00157877
		// (set) Token: 0x0600638F RID: 25487 RVA: 0x0015967F File Offset: 0x0015787F
		public int Version { get; private set; }

		// Token: 0x17002355 RID: 9045
		// (get) Token: 0x06006390 RID: 25488 RVA: 0x00159688 File Offset: 0x00157888
		// (set) Token: 0x06006391 RID: 25489 RVA: 0x00159690 File Offset: 0x00157890
		public DateTime WhenSyncRequested { get; set; }

		// Token: 0x17002356 RID: 9046
		// (get) Token: 0x06006392 RID: 25490 RVA: 0x00159699 File Offset: 0x00157899
		// (set) Token: 0x06006393 RID: 25491 RVA: 0x001596A1 File Offset: 0x001578A1
		public DateTime WhenSyncStarted { get; set; }

		// Token: 0x17002357 RID: 9047
		// (get) Token: 0x06006394 RID: 25492 RVA: 0x001596AA File Offset: 0x001578AA
		// (set) Token: 0x06006395 RID: 25493 RVA: 0x001596B2 File Offset: 0x001578B2
		public string SyncRequestor
		{
			get
			{
				return this.syncRequestor;
			}
			set
			{
				this.syncRequestor = (value ?? string.Empty);
			}
		}

		// Token: 0x17002358 RID: 9048
		// (get) Token: 0x06006396 RID: 25494 RVA: 0x001596C4 File Offset: 0x001578C4
		// (set) Token: 0x06006397 RID: 25495 RVA: 0x001596CC File Offset: 0x001578CC
		public DateTime Timestamp { get; set; }

		// Token: 0x17002359 RID: 9049
		// (get) Token: 0x06006398 RID: 25496 RVA: 0x001596D5 File Offset: 0x001578D5
		// (set) Token: 0x06006399 RID: 25497 RVA: 0x001596DD File Offset: 0x001578DD
		public TenantSyncType SyncType { get; set; }

		// Token: 0x0600639A RID: 25498 RVA: 0x001596E8 File Offset: 0x001578E8
		public MsoFullSyncCookie(byte[] rawCookie, int cookieVersion)
		{
			this.RawCookie = rawCookie;
			this.WhenSyncRequested = DateTime.MinValue;
			this.WhenSyncStarted = DateTime.MinValue;
			this.SyncRequestor = string.Empty;
			this.Timestamp = DateTime.MinValue;
			this.Version = cookieVersion;
		}

		// Token: 0x1700235A RID: 9050
		// (get) Token: 0x0600639B RID: 25499 RVA: 0x00159735 File Offset: 0x00157935
		// (set) Token: 0x0600639C RID: 25500 RVA: 0x0015973D File Offset: 0x0015793D
		public byte[] RawCookie { get; private set; }

		// Token: 0x0600639D RID: 25501 RVA: 0x00159748 File Offset: 0x00157948
		public static MsoFullSyncCookie FromStorageCookie(byte[] storageCookie)
		{
			MsoFullSyncCookie result;
			Exception ex;
			if (!MsoFullSyncCookie.TryFromStorageCookie(storageCookie, out result, out ex))
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x0600639E RID: 25502 RVA: 0x00159764 File Offset: 0x00157964
		public byte[] ToStorageCookie()
		{
			List<string> list = new List<string>(3);
			if (this.Version >= 1)
			{
				list.Add(this.GetCookieVersion1Data());
			}
			if (this.Version >= 2 || this.Version < 1)
			{
				list.Add(this.GetCookieVersion2Data());
			}
			if (this.Version >= 3)
			{
				list.Add(this.GetCookieVersion3Data());
			}
			string s = string.Join("\n", list);
			return Encoding.UTF8.GetBytes(s);
		}

		// Token: 0x0600639F RID: 25503 RVA: 0x001597D8 File Offset: 0x001579D8
		private static bool TryFromStorageCookie(byte[] storageCookie, out MsoFullSyncCookie cookie, out Exception ex)
		{
			ex = null;
			cookie = null;
			if (storageCookie == null || storageCookie.Length == 0)
			{
				ex = new ArgumentNullException("storageCookie");
				return false;
			}
			try
			{
				string @string = Encoding.UTF8.GetString(storageCookie);
				string[] array = @string.Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				byte[] rawCookie = Convert.FromBase64String(array[0]);
				cookie = new MsoFullSyncCookie(rawCookie, 3);
				if (array.Length == 1)
				{
					MsoFullSyncCookie.FillCookieWithVersion1Data(cookie);
				}
				if (array.Length >= 2)
				{
					MsoFullSyncCookie.FillCookieWithVersion1And2Data(cookie, array[1]);
				}
				if (array.Length >= 3)
				{
					MsoFullSyncCookie.FillCookieWithVersion3Data(cookie, array[2]);
				}
			}
			catch (DecoderFallbackException innerException)
			{
				ex = new InvalidTenantFullSyncCookieException(innerException);
			}
			catch (ArgumentException innerException2)
			{
				ex = new InvalidTenantFullSyncCookieException(innerException2);
			}
			catch (FormatException innerException3)
			{
				ex = new InvalidTenantFullSyncCookieException(innerException3);
			}
			catch (OverflowException innerException4)
			{
				ex = new InvalidTenantFullSyncCookieException(innerException4);
			}
			return null == ex;
		}

		// Token: 0x060063A0 RID: 25504 RVA: 0x001598D4 File Offset: 0x00157AD4
		private static void FillCookieWithVersion1Data(MsoFullSyncCookie cookie)
		{
			cookie.SyncType = TenantSyncType.Full;
			cookie.Version = 1;
			cookie.WhenSyncRequested = DateTime.UtcNow;
		}

		// Token: 0x060063A1 RID: 25505 RVA: 0x001598F0 File Offset: 0x00157AF0
		private static void FillCookieWithVersion1And2Data(MsoFullSyncCookie cookie, string data)
		{
			byte[] buffer = Convert.FromBase64String(data);
			using (MemoryStream memoryStream = new MemoryStream(buffer))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					cookie.Version = binaryReader.ReadInt32();
					cookie.SyncType = (TenantSyncType)binaryReader.ReadByte();
					cookie.WhenSyncRequested = DateTime.FromBinary(binaryReader.ReadInt64());
					cookie.WhenSyncStarted = DateTime.FromBinary(binaryReader.ReadInt64());
					cookie.Timestamp = DateTime.FromBinary(binaryReader.ReadInt64());
				}
			}
		}

		// Token: 0x060063A2 RID: 25506 RVA: 0x00159994 File Offset: 0x00157B94
		private static void FillCookieWithVersion3Data(MsoFullSyncCookie cookie, string data)
		{
			byte[] buffer = Convert.FromBase64String(data);
			using (MemoryStream memoryStream = new MemoryStream(buffer))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					cookie.SyncRequestor = binaryReader.ReadString();
				}
			}
		}

		// Token: 0x060063A3 RID: 25507 RVA: 0x001599F8 File Offset: 0x00157BF8
		private string GetCookieVersion1Data()
		{
			if (this.RawCookie == null)
			{
				return string.Empty;
			}
			return Convert.ToBase64String(this.RawCookie, Base64FormattingOptions.None);
		}

		// Token: 0x060063A4 RID: 25508 RVA: 0x00159A14 File Offset: 0x00157C14
		private string GetCookieVersion2Data()
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(this.Version);
					binaryWriter.Write((byte)this.SyncType);
					binaryWriter.Write(this.WhenSyncRequested.ToBinary());
					binaryWriter.Write(this.WhenSyncStarted.ToBinary());
					binaryWriter.Write(this.Timestamp.ToBinary());
					binaryWriter.Flush();
					result = Convert.ToBase64String(memoryStream.ToArray());
				}
			}
			return result;
		}

		// Token: 0x060063A5 RID: 25509 RVA: 0x00159ACC File Offset: 0x00157CCC
		private string GetCookieVersion3Data()
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(this.SyncRequestor);
					binaryWriter.Flush();
					result = Convert.ToBase64String(memoryStream.ToArray());
				}
			}
			return result;
		}

		// Token: 0x1700235B RID: 9051
		// (get) Token: 0x060063A6 RID: 25510 RVA: 0x00159B38 File Offset: 0x00157D38
		public bool IsRawCookieNull
		{
			get
			{
				return this.RawCookie != null && this.RawCookie.Length > 0;
			}
		}

		// Token: 0x04004253 RID: 16979
		private const string Delimiter = "\n";

		// Token: 0x04004254 RID: 16980
		public const int CurrentCookieVersion = 3;

		// Token: 0x04004255 RID: 16981
		private string syncRequestor;
	}
}
