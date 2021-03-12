using System;
using Microsoft.Win32;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000029 RID: 41
	internal sealed class CrashData
	{
		// Token: 0x06000145 RID: 325 RVA: 0x000064FC File Offset: 0x000046FC
		public CrashData(int count, DateTime time)
		{
			this.count = count;
			this.time = time;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00006512 File Offset: 0x00004712
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000651A File Offset: 0x0000471A
		public DateTime Time
		{
			get
			{
				return this.time;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006524 File Offset: 0x00004724
		public static CrashData Read(RegistryKey parentKey, string subKeyName)
		{
			CrashData result;
			using (RegistryKey registryKey = parentKey.OpenSubKey(subKeyName))
			{
				try
				{
					DateTime dateTime = new DateTime(Util.ReadRegistryLong(registryKey, "CrashTime"));
					object value = registryKey.GetValue("CrashCount");
					if (!(value is int))
					{
						result = null;
					}
					else
					{
						result = new CrashData((int)value, dateTime);
					}
				}
				catch (FormatException)
				{
					result = null;
				}
				catch (OverflowException)
				{
					result = null;
				}
				catch (ArgumentOutOfRangeException)
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000065C0 File Offset: 0x000047C0
		public static void Write(RegistryKey parentKey, string subKeyName, int crashCount)
		{
			using (RegistryKey registryKey = parentKey.CreateSubKey(subKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree))
			{
				registryKey.SetValue("CrashCount", crashCount);
				Util.WriteRegistryLong(registryKey, "CrashTime", DateTime.UtcNow.Ticks);
			}
		}

		// Token: 0x0400012D RID: 301
		private const string RegistryNameCrashCount = "CrashCount";

		// Token: 0x0400012E RID: 302
		private const string RegistryNameCrashTime = "CrashTime";

		// Token: 0x0400012F RID: 303
		private int count;

		// Token: 0x04000130 RID: 304
		private DateTime time;
	}
}
