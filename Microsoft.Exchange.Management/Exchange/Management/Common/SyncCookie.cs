using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000136 RID: 310
	[Serializable]
	internal sealed class SyncCookie
	{
		// Token: 0x06000B20 RID: 2848 RVA: 0x00033C69 File Offset: 0x00031E69
		public SyncCookie(Guid domainController, WatermarkMap lowWatermarks, WatermarkMap highWatermarks, byte[] pageCookie) : this(domainController, lowWatermarks, highWatermarks, pageCookie, null)
		{
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00033C78 File Offset: 0x00031E78
		public SyncCookie(Guid domainController, WatermarkMap lowWatermarks, WatermarkMap highWatermarks, byte[] pageCookie, byte[] pageCookie2)
		{
			this.CheckNullArgument("lowWatermarks", lowWatermarks);
			this.CheckNullArgument("highWatermarks", highWatermarks);
			this.DomainController = domainController;
			this.LowWatermarks = lowWatermarks;
			this.HighWatermarks = highWatermarks;
			this.PageCookie = pageCookie;
			this.PageCookie2 = pageCookie2;
			this.Version = SyncCookie.CurrentVersion;
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00033CD3 File Offset: 0x00031ED3
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x00033CDB File Offset: 0x00031EDB
		public int Version { get; private set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00033CE4 File Offset: 0x00031EE4
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x00033CEC File Offset: 0x00031EEC
		public Guid DomainController { get; private set; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00033CF5 File Offset: 0x00031EF5
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x00033CFD File Offset: 0x00031EFD
		public WatermarkMap LowWatermarks { get; private set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00033D06 File Offset: 0x00031F06
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x00033D0E File Offset: 0x00031F0E
		public WatermarkMap HighWatermarks { get; private set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x00033D17 File Offset: 0x00031F17
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x00033D1F File Offset: 0x00031F1F
		public byte[] PageCookie { get; private set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x00033D28 File Offset: 0x00031F28
		// (set) Token: 0x06000B2D RID: 2861 RVA: 0x00033D30 File Offset: 0x00031F30
		public byte[] PageCookie2 { get; private set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x00033D39 File Offset: 0x00031F39
		public long LowWatermark
		{
			get
			{
				if (!this.LowWatermarks.ContainsKey(this.DomainController))
				{
					return 0L;
				}
				return this.LowWatermarks[this.DomainController];
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00033D62 File Offset: 0x00031F62
		public long HighWatermark
		{
			get
			{
				if (!this.HighWatermarks.ContainsKey(this.DomainController))
				{
					return 0L;
				}
				return this.HighWatermarks[this.DomainController];
			}
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00033D8C File Offset: 0x00031F8C
		public byte[] ToBytes()
		{
			string text = string.Empty;
			if (this.PageCookie != null)
			{
				text = Convert.ToBase64String(this.PageCookie, Base64FormattingOptions.None);
			}
			string s = string.Join(SyncCookie.Delimiter, new string[]
			{
				this.Version.ToString(),
				this.DomainController.ToString(),
				this.LowWatermarks.SerializeToString(),
				this.HighWatermarks.SerializeToString(),
				text,
				text
			});
			return Encoding.UTF8.GetBytes(s);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00033E20 File Offset: 0x00032020
		public static bool TryFromBytes(byte[] cookieData, out SyncCookie cookie, out Exception ex)
		{
			ex = null;
			cookie = null;
			if (cookieData == null)
			{
				ex = new ArgumentNullException("cookieData");
				return false;
			}
			try
			{
				string @string = Encoding.UTF8.GetString(cookieData);
				string[] array = @string.Split(new string[]
				{
					SyncCookie.Delimiter
				}, StringSplitOptions.None);
				if (array.Length < 1)
				{
					ex = new InvalidCookieException();
					return false;
				}
				int num = int.Parse(array[0]);
				if (num != 1 && num != 2 && num != 3)
				{
					ex = new CookieVersionUnsupportedException(num);
					return false;
				}
				if (((num == 1 || num == 2) && array.Length != 5) || (num == 3 && array.Length != 6))
				{
					ex = new InvalidCookieException();
					return false;
				}
				Guid guid = new Guid(array[1]);
				WatermarkMap watermarkMap = WatermarkMap.Empty;
				if (num == 1)
				{
					long value = long.Parse(array[2]);
					watermarkMap.Add(guid, value);
				}
				else
				{
					watermarkMap = WatermarkMap.Parse(array[2]);
				}
				WatermarkMap watermarkMap2 = WatermarkMap.Empty;
				if (num == 1)
				{
					long value2 = long.Parse(array[3]);
					watermarkMap2.Add(guid, value2);
				}
				else
				{
					watermarkMap2 = WatermarkMap.Parse(array[3]);
				}
				byte[] pageCookie = null;
				if (!string.IsNullOrEmpty(array[4]))
				{
					pageCookie = Convert.FromBase64String(array[4]);
				}
				byte[] pageCookie2 = null;
				if (num == 3 && !string.IsNullOrEmpty(array[5]))
				{
					pageCookie2 = Convert.FromBase64String(array[5]);
				}
				cookie = new SyncCookie(guid, watermarkMap, watermarkMap2, pageCookie, pageCookie2);
			}
			catch (DecoderFallbackException innerException)
			{
				ex = new InvalidCookieException(innerException);
				return false;
			}
			catch (FormatException innerException2)
			{
				ex = new InvalidCookieException(innerException2);
				return false;
			}
			catch (OverflowException innerException3)
			{
				ex = new InvalidCookieException(innerException3);
				return false;
			}
			return true;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00033FF0 File Offset: 0x000321F0
		public static SyncCookie FromBytes(byte[] cookieData)
		{
			Exception ex = null;
			SyncCookie result = null;
			if (!SyncCookie.TryFromBytes(cookieData, out result, out ex))
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00034010 File Offset: 0x00032210
		private static string FormatWatermarks(WatermarkMap watermarks)
		{
			StringBuilder stringBuilder = new StringBuilder(watermarks.Count * 56);
			foreach (KeyValuePair<Guid, long> keyValuePair in watermarks)
			{
				stringBuilder.AppendFormat("{0}:{1};", keyValuePair.Key, keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00034090 File Offset: 0x00032290
		private static WatermarkMap ParseWatermarks(string rawstring)
		{
			WatermarkMap empty = WatermarkMap.Empty;
			string[] array = rawstring.Split(new string[]
			{
				";"
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				string[] array3 = text.Split(new string[]
				{
					":"
				}, StringSplitOptions.None);
				if (array3.Length != 2)
				{
					throw new FormatException();
				}
				Guid key = new Guid(array3[0]);
				long value = long.Parse(array3[1]);
				if (!empty.ContainsKey(key))
				{
					empty.Add(key, value);
				}
			}
			return empty;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0003412A File Offset: 0x0003232A
		private void CheckNullArgument(string name, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		// Token: 0x04000587 RID: 1415
		private static readonly int CurrentVersion = 3;

		// Token: 0x04000588 RID: 1416
		private static readonly string Delimiter = "\n";
	}
}
