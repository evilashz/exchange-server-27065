using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ConfigurationSettings
{
	// Token: 0x020001F2 RID: 498
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ConfigDriverBase : DisposeTrackableBase, IConfigDriver, IDisposable
	{
		// Token: 0x0600112C RID: 4396 RVA: 0x0003400E File Offset: 0x0003220E
		public ConfigDriverBase(IConfigSchema schema) : this(schema, new TimeSpan?(ConfigDriverBase.DefaultErrorThresholdInterval))
		{
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00034021 File Offset: 0x00032221
		public ConfigDriverBase(IConfigSchema schema, TimeSpan? errorThresholdInterval)
		{
			this.Schema = schema;
			this.IsInitialized = false;
			this.ErrorThresholdInterval = errorThresholdInterval;
			this.LastKnownErrors = new List<ConfigDriverBase.DiagnosticsError>();
			this.LastUpdated = DateTime.MinValue;
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0003405F File Offset: 0x0003225F
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x00034067 File Offset: 0x00032267
		public bool IsInitialized { get; protected set; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x00034070 File Offset: 0x00032270
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x00034078 File Offset: 0x00032278
		public DateTime LastUpdated { get; protected set; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x00034081 File Offset: 0x00032281
		// (set) Token: 0x06001133 RID: 4403 RVA: 0x00034089 File Offset: 0x00032289
		private protected IConfigSchema Schema { protected get; private set; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x00034092 File Offset: 0x00032292
		// (set) Token: 0x06001135 RID: 4405 RVA: 0x0003409A File Offset: 0x0003229A
		private TimeSpan? ErrorThresholdInterval { get; set; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x000340A3 File Offset: 0x000322A3
		// (set) Token: 0x06001137 RID: 4407 RVA: 0x000340AB File Offset: 0x000322AB
		private List<ConfigDriverBase.DiagnosticsError> LastKnownErrors { get; set; }

		// Token: 0x06001138 RID: 4408 RVA: 0x000340B4 File Offset: 0x000322B4
		protected object ParseAndValidateConfigValue(string settingName, string serializedValue, Type settingType)
		{
			return this.Schema.ParseAndValidateConfigValue(settingName, serializedValue, settingType);
		}

		// Token: 0x06001139 RID: 4409
		public abstract void Initialize();

		// Token: 0x0600113A RID: 4410
		public abstract bool TryGetBoxedSetting(ISettingsContext context, string settingName, Type settingType, out object settingValue);

		// Token: 0x0600113B RID: 4411 RVA: 0x000340C4 File Offset: 0x000322C4
		public virtual XElement GetDiagnosticInfo(string argument)
		{
			XElement xelement = new XElement(base.GetType().Name);
			xelement.Add(new XAttribute("name", this.Schema.Name));
			xelement.Add(new XAttribute("LastUpdated", this.LastUpdated));
			XElement xelement2 = new XElement("LastKnownErrors");
			lock (this.errorLock)
			{
				foreach (ConfigDriverBase.DiagnosticsError diagnosticsError in this.LastKnownErrors)
				{
					xelement2.Add(diagnosticsError.GetDiagnosticInfo());
				}
			}
			xelement.Add(xelement2);
			return xelement;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x000341B8 File Offset: 0x000323B8
		protected virtual void HandleLoadError(Exception ex)
		{
			lock (this.errorLock)
			{
				if (this.LastKnownErrors.Count >= 50)
				{
					this.LastKnownErrors[this.LastKnownErrors.Count - 1] = new ConfigDriverBase.DiagnosticsError(ex);
				}
				else
				{
					this.LastKnownErrors.Add(new ConfigDriverBase.DiagnosticsError(ex));
				}
				if (this.ErrorThresholdInterval != null && this.ErrorThresholdInterval.Value < DateTime.UtcNow - this.LastKnownErrors[0].RaisedAt)
				{
					throw ex;
				}
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00034274 File Offset: 0x00032474
		protected void HandleLoadSuccess()
		{
			lock (this.errorLock)
			{
				this.LastKnownErrors.Clear();
			}
		}

		// Token: 0x04000A93 RID: 2707
		private const int MaximumLastKnownErrorSize = 50;

		// Token: 0x04000A94 RID: 2708
		public static readonly TimeSpan DefaultErrorThresholdInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000A95 RID: 2709
		private object errorLock = new object();

		// Token: 0x020001F3 RID: 499
		protected class DiagnosticsError
		{
			// Token: 0x0600113F RID: 4415 RVA: 0x000342D1 File Offset: 0x000324D1
			public DiagnosticsError(Exception ex)
			{
				if (ex == null)
				{
					throw new ArgumentNullException("ex");
				}
				this.Exception = ex;
				this.RaisedAt = DateTime.UtcNow;
			}

			// Token: 0x17000545 RID: 1349
			// (get) Token: 0x06001140 RID: 4416 RVA: 0x000342F9 File Offset: 0x000324F9
			// (set) Token: 0x06001141 RID: 4417 RVA: 0x00034301 File Offset: 0x00032501
			public Exception Exception { get; set; }

			// Token: 0x17000546 RID: 1350
			// (get) Token: 0x06001142 RID: 4418 RVA: 0x0003430A File Offset: 0x0003250A
			// (set) Token: 0x06001143 RID: 4419 RVA: 0x00034312 File Offset: 0x00032512
			public DateTime RaisedAt { get; private set; }

			// Token: 0x06001144 RID: 4420 RVA: 0x0003431C File Offset: 0x0003251C
			public XElement GetDiagnosticInfo()
			{
				if (this.Exception == null)
				{
					return null;
				}
				return new XElement("LastKnownError", new object[]
				{
					new XAttribute("RaisedAt", this.RaisedAt),
					new XElement("Exception", this.Exception)
				});
			}
		}
	}
}
