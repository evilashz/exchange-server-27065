using System;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007DA RID: 2010
	[Serializable]
	internal class MsoMainStreamCookie
	{
		// Token: 0x060063A7 RID: 25511 RVA: 0x00159B50 File Offset: 0x00157D50
		public MsoMainStreamCookie(string serviceInstanceName, byte[] rawCookie, DateTime timeStamp, ServerVersion syncPropertySetVersion, bool isSyncPropertySetUpgrading)
		{
			if (string.IsNullOrEmpty(serviceInstanceName))
			{
				throw new ArgumentNullException("serviceInstanceName");
			}
			if (rawCookie == null || rawCookie.Length == 0)
			{
				throw new ArgumentNullException("rawCookie");
			}
			this.ServiceInstanceName = serviceInstanceName;
			this.RawCookie = rawCookie;
			this.TimeStamp = timeStamp;
			this.SyncPropertySetVersion = syncPropertySetVersion;
			this.IsSyncPropertySetUpgrading = isSyncPropertySetUpgrading;
		}

		// Token: 0x1700235C RID: 9052
		// (get) Token: 0x060063A8 RID: 25512 RVA: 0x00159BAE File Offset: 0x00157DAE
		// (set) Token: 0x060063A9 RID: 25513 RVA: 0x00159BB6 File Offset: 0x00157DB6
		public string ServiceInstanceName { get; private set; }

		// Token: 0x1700235D RID: 9053
		// (get) Token: 0x060063AA RID: 25514 RVA: 0x00159BBF File Offset: 0x00157DBF
		// (set) Token: 0x060063AB RID: 25515 RVA: 0x00159BC7 File Offset: 0x00157DC7
		public byte[] RawCookie { get; private set; }

		// Token: 0x1700235E RID: 9054
		// (get) Token: 0x060063AC RID: 25516 RVA: 0x00159BD0 File Offset: 0x00157DD0
		// (set) Token: 0x060063AD RID: 25517 RVA: 0x00159BD8 File Offset: 0x00157DD8
		public DateTime TimeStamp { get; private set; }

		// Token: 0x1700235F RID: 9055
		// (get) Token: 0x060063AE RID: 25518 RVA: 0x00159BE1 File Offset: 0x00157DE1
		// (set) Token: 0x060063AF RID: 25519 RVA: 0x00159BE9 File Offset: 0x00157DE9
		public ServerVersion SyncPropertySetVersion { get; private set; }

		// Token: 0x17002360 RID: 9056
		// (get) Token: 0x060063B0 RID: 25520 RVA: 0x00159BF2 File Offset: 0x00157DF2
		// (set) Token: 0x060063B1 RID: 25521 RVA: 0x00159BFA File Offset: 0x00157DFA
		public bool IsSyncPropertySetUpgrading { get; private set; }

		// Token: 0x060063B2 RID: 25522 RVA: 0x00159C04 File Offset: 0x00157E04
		public static MsoMainStreamCookie FromStorageCookie(byte[] storageCookie)
		{
			Exception ex = null;
			MsoMainStreamCookie result = null;
			if (!MsoMainStreamCookie.TryFromStorageCookie(storageCookie, out result, out ex))
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x060063B3 RID: 25523 RVA: 0x00159C24 File Offset: 0x00157E24
		public static bool TryFromStorageCookie(byte[] storageCookie, out MsoMainStreamCookie cookie, out Exception ex)
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
					MsoMainStreamCookie.Delimiter
				}, StringSplitOptions.None);
				if (array.Length != 2 && array.Length != 4 && array.Length != 6)
				{
					ex = new InvalidMainStreamCookieException();
					return false;
				}
				string serviceInstanceName = string.Empty;
				DateTime timeStamp = DateTime.UtcNow;
				ServerVersion syncPropertySetVersion = SyncPropertyDefinition.InitialSyncPropertySetVersion;
				bool isSyncPropertySetUpgrading = false;
				int num;
				if (array.Length == 2)
				{
					num = 1;
					serviceInstanceName = array[0];
				}
				else
				{
					num = Convert.ToInt32(array[0]);
					serviceInstanceName = array[1];
					timeStamp = DateTime.FromFileTimeUtc(long.Parse(array[2]));
					if (num >= MsoMainStreamCookie.Version)
					{
						syncPropertySetVersion = new ServerVersion(Convert.ToInt32(array[3]));
						isSyncPropertySetUpgrading = bool.Parse(array[4]);
					}
				}
				if (num > MsoMainStreamCookie.Version)
				{
					return false;
				}
				byte[] rawCookie = Convert.FromBase64String(array[array.Length - 1]);
				cookie = new MsoMainStreamCookie(serviceInstanceName, rawCookie, timeStamp, syncPropertySetVersion, isSyncPropertySetUpgrading);
			}
			catch (DecoderFallbackException innerException)
			{
				ex = new InvalidMainStreamCookieException(innerException);
				return false;
			}
			catch (ArgumentException innerException2)
			{
				ex = new InvalidMainStreamCookieException(innerException2);
			}
			catch (FormatException innerException3)
			{
				ex = new InvalidMainStreamCookieException(innerException3);
				return false;
			}
			catch (OverflowException innerException4)
			{
				ex = new InvalidMainStreamCookieException(innerException4);
				return false;
			}
			return true;
		}

		// Token: 0x060063B4 RID: 25524 RVA: 0x00159D9C File Offset: 0x00157F9C
		public byte[] ToStorageCookie()
		{
			string text = Convert.ToBase64String(this.RawCookie, Base64FormattingOptions.None);
			string s = string.Empty;
			string[] value = new string[]
			{
				MsoMainStreamCookie.Version.ToString(),
				this.ServiceInstanceName,
				this.TimeStamp.ToFileTimeUtc().ToString(),
				this.SyncPropertySetVersion.ToInt().ToString(),
				this.IsSyncPropertySetUpgrading.ToString(),
				text
			};
			s = string.Join(MsoMainStreamCookie.Delimiter, value);
			return Encoding.UTF8.GetBytes(s);
		}

		// Token: 0x0400425C RID: 16988
		public static readonly int Version = 2;

		// Token: 0x0400425D RID: 16989
		private static readonly string Delimiter = "\n";
	}
}
