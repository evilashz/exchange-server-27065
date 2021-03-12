using System;
using System.Configuration;
using System.Security;
using Microsoft.Win32;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200000C RID: 12
	public class OverridableSetting<T>
	{
		// Token: 0x0600017D RID: 381 RVA: 0x00006080 File Offset: 0x00004280
		public OverridableSetting(string name, T defaultValue, Func<string, T> converter, bool allowOverride = false)
		{
			this.traceContext = new TracingContext(null)
			{
				LId = this.GetHashCode(),
				Id = base.GetType().GetHashCode()
			};
			this.Name = name;
			this.Value = defaultValue;
			this.Converter = converter;
			this.AllowOverride = allowOverride;
			string text = ConfigurationManager.AppSettings[name];
			if (text != null)
			{
				try
				{
					this.Value = converter(text);
				}
				catch (FormatException arg)
				{
					WTFDiagnostics.TraceError<string, string, FormatException>(WTFLog.Core, this.traceContext, "[OverridableSetting.OverridableSetting]: incorrect configuration value encountered, {0} : {1} with error:\n{2}", name, text, arg, null, ".ctor", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\OverridableSetting.cs", 62);
				}
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00006134 File Offset: 0x00004334
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000613C File Offset: 0x0000433C
		internal T Value { private get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00006145 File Offset: 0x00004345
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000614D File Offset: 0x0000434D
		private string Name { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00006156 File Offset: 0x00004356
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000615E File Offset: 0x0000435E
		private Func<string, T> Converter { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00006167 File Offset: 0x00004367
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0000616F File Offset: 0x0000436F
		private bool AllowOverride { get; set; }

		// Token: 0x06000186 RID: 390 RVA: 0x00006178 File Offset: 0x00004378
		public static implicit operator T(OverridableSetting<T> os)
		{
			if (os.AllowOverride)
			{
				try
				{
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(Settings.OverrideRegistryPath))
					{
						if (registryKey != null)
						{
							string text = registryKey.GetValue(os.Name) as string;
							if (!string.IsNullOrWhiteSpace(text))
							{
								try
								{
									os.Value = os.Converter(text);
								}
								catch (FormatException arg)
								{
									WTFDiagnostics.TraceError<string, string, FormatException>(WTFLog.Core, os.traceContext, "[OverridableSetting.Conversion]: incorrect configuration value encountered, {0} : {1}. Error:\n{2}", os.Name, text, arg, null, "op_Implicit", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\OverridableSetting.cs", 133);
								}
							}
						}
					}
				}
				catch (SecurityException arg2)
				{
					WTFDiagnostics.TraceError<string, SecurityException>(WTFLog.Core, os.traceContext, "[OverridableSetting.Conversion]: cannot access registry {0}. Error:\n{1}", Settings.OverrideRegistryPath, arg2, null, "op_Implicit", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\OverridableSetting.cs", 141);
				}
			}
			return os.Value;
		}

		// Token: 0x040000A6 RID: 166
		private TracingContext traceContext;
	}
}
