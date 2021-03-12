using System;
using System.Configuration;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.EventLog;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000059 RID: 89
	internal abstract class Config : IConfig
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x00003AD8 File Offset: 0x00001CD8
		internal Config() : this(AppConfigAdapter.Instance)
		{
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00003AE8 File Offset: 0x00001CE8
		protected Config(IConfigAdapter configAdapter)
		{
			Util.ThrowOnNullArgument(configAdapter, "configAdapter");
			this.configAdapter = configAdapter;
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("Config", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.CoreComponentTracer, (long)this.GetHashCode());
			this.Load();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00003B39 File Offset: 0x00001D39
		public virtual void Load()
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00003B3C File Offset: 0x00001D3C
		protected bool ReadBool(string key, bool defaultValue)
		{
			bool isDefault = false;
			bool? flag = null;
			try
			{
				string setting = this.configAdapter.GetSetting(key);
				if (!string.IsNullOrEmpty(setting))
				{
					flag = new bool?(bool.Parse(setting));
				}
			}
			catch (FormatException ex)
			{
				this.diagnosticsSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_InvalidConfiguration, new object[]
				{
					ex
				});
			}
			catch (ConfigurationErrorsException ex2)
			{
				this.diagnosticsSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_InvalidConfiguration, new object[]
				{
					ex2
				});
			}
			if (flag == null)
			{
				flag = new bool?(defaultValue);
				isDefault = true;
			}
			this.TraceConfigValue(key, flag, isDefault);
			return flag.Value;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00003C00 File Offset: 0x00001E00
		protected int ReadInt(string key, int defaultValue)
		{
			bool isDefault = false;
			int? num = null;
			try
			{
				string setting = this.configAdapter.GetSetting(key);
				if (!string.IsNullOrEmpty(setting))
				{
					num = new int?(int.Parse(setting));
				}
			}
			catch (FormatException ex)
			{
				this.diagnosticsSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_InvalidConfiguration, new object[]
				{
					ex
				});
			}
			catch (ConfigurationErrorsException ex2)
			{
				this.diagnosticsSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_InvalidConfiguration, new object[]
				{
					ex2
				});
			}
			if (num == null)
			{
				num = new int?(defaultValue);
				isDefault = true;
			}
			this.TraceConfigValue(key, num, isDefault);
			return num.Value;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00003CC4 File Offset: 0x00001EC4
		protected string ReadString(string key, string defaultValue)
		{
			bool isDefault = false;
			string text = null;
			try
			{
				text = this.configAdapter.GetSetting(key);
			}
			catch (ConfigurationErrorsException ex)
			{
				this.diagnosticsSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_InvalidConfiguration, new object[]
				{
					ex
				});
			}
			if (string.IsNullOrEmpty(text))
			{
				text = defaultValue;
				isDefault = true;
			}
			this.TraceConfigValue(key, text, isDefault);
			return text;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00003D2C File Offset: 0x00001F2C
		protected Guid ReadGuid(string key, Guid defaultValue)
		{
			bool isDefault = false;
			Guid? guid = null;
			try
			{
				string setting = this.configAdapter.GetSetting(key);
				if (!string.IsNullOrEmpty(setting))
				{
					guid = new Guid?(Guid.Parse(setting));
				}
			}
			catch (FormatException ex)
			{
				this.diagnosticsSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_InvalidConfiguration, new object[]
				{
					ex
				});
			}
			catch (ConfigurationErrorsException ex2)
			{
				this.diagnosticsSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_InvalidConfiguration, new object[]
				{
					ex2
				});
			}
			if (guid == null)
			{
				guid = new Guid?(defaultValue);
				isDefault = true;
			}
			this.TraceConfigValue(key, guid, isDefault);
			return guid.Value;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00003DF0 File Offset: 0x00001FF0
		protected TimeSpan ReadTimeSpan(string key, TimeSpan defaultValue)
		{
			bool isDefault = false;
			TimeSpan? timeSpan = null;
			try
			{
				string setting = this.configAdapter.GetSetting(key);
				if (!string.IsNullOrEmpty(setting))
				{
					timeSpan = new TimeSpan?(TimeSpan.Parse(setting));
				}
			}
			catch (FormatException ex)
			{
				this.diagnosticsSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_InvalidConfiguration, new object[]
				{
					ex
				});
			}
			catch (ConfigurationErrorsException ex2)
			{
				this.diagnosticsSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_InvalidConfiguration, new object[]
				{
					ex2
				});
			}
			if (timeSpan == null)
			{
				timeSpan = new TimeSpan?(defaultValue);
				isDefault = true;
			}
			this.TraceConfigValue(key, timeSpan, isDefault);
			return timeSpan.Value;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00003EB4 File Offset: 0x000020B4
		private void TraceConfigValue(string key, object value, bool isDefault)
		{
			this.diagnosticsSession.TraceDebug<string, object, string>("Reading config: {0}, value: '{1}'{2}.", key, value, isDefault ? " (default)" : string.Empty);
		}

		// Token: 0x040000AA RID: 170
		private readonly IConfigAdapter configAdapter;

		// Token: 0x040000AB RID: 171
		private readonly IDiagnosticsSession diagnosticsSession;
	}
}
