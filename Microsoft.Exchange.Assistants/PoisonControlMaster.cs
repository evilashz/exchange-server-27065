using System;
using Microsoft.Win32;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000073 RID: 115
	internal sealed class PoisonControlMaster
	{
		// Token: 0x06000364 RID: 868 RVA: 0x00010E5C File Offset: 0x0000F05C
		public PoisonControlMaster(string registryKeyBasePath)
		{
			if (string.IsNullOrEmpty(registryKeyBasePath))
			{
				return;
			}
			string text = registryKeyBasePath + "\\PoisonControl";
			this.registryKey = Registry.LocalMachine.OpenSubKey(text, true);
			if (this.registryKey == null)
			{
				this.registryKey = Registry.LocalMachine.CreateSubKey(text, RegistryKeyPermissionCheck.ReadWriteSubTree);
				this.registryKey.SetValue("Enabled", this.enabled ? 1 : 0);
				this.registryKey.SetValue("MaxCrashCount", this.poisonCrashCount);
				return;
			}
			object value = this.registryKey.GetValue("Enabled");
			if (value is int)
			{
				this.enabled = ((int)value != 0);
			}
			value = this.registryKey.GetValue("MaxCrashCount");
			if (value is int)
			{
				this.poisonCrashCount = (int)value;
				this.toxicCrashCount = this.poisonCrashCount + 1;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00010F60 File Offset: 0x0000F160
		public int PoisonCrashCount
		{
			get
			{
				return this.poisonCrashCount;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00010F68 File Offset: 0x0000F168
		public int ToxicCrashCount
		{
			get
			{
				return this.toxicCrashCount;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00010F70 File Offset: 0x0000F170
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00010F78 File Offset: 0x0000F178
		public RegistryKey RegistryKey
		{
			get
			{
				return this.registryKey;
			}
		}

		// Token: 0x040001EF RID: 495
		private const string RegistryNameEnabled = "Enabled";

		// Token: 0x040001F0 RID: 496
		private const string RegistryNameMaxCrashCount = "MaxCrashCount";

		// Token: 0x040001F1 RID: 497
		private RegistryKey registryKey;

		// Token: 0x040001F2 RID: 498
		private bool enabled = true;

		// Token: 0x040001F3 RID: 499
		private int poisonCrashCount = 2;

		// Token: 0x040001F4 RID: 500
		private int toxicCrashCount = 3;
	}
}
