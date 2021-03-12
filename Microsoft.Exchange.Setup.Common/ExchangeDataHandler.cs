using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000004 RID: 4
	public class ExchangeDataHandler : DataHandler, IVersionable
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002C0A File Offset: 0x00000E0A
		public ExchangeDataHandler()
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002C12 File Offset: 0x00000E12
		public ExchangeDataHandler(ICloneable dataSource) : this()
		{
			base.DataSource = dataSource.Clone();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C26 File Offset: 0x00000E26
		public ExchangeDataHandler(bool breakOnError) : this()
		{
			base.BreakOnError = breakOnError;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002C38 File Offset: 0x00000E38
		internal virtual ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				ExchangeObjectVersion exchangeObjectVersion = ExchangeObjectVersion.Exchange2003;
				if (base.HasDataHandlers)
				{
					using (IEnumerator<DataHandler> enumerator = base.DataHandlers.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							DataHandler dataHandler = enumerator.Current;
							ExchangeDataHandler exchangeDataHandler = (ExchangeDataHandler)dataHandler;
							if (exchangeObjectVersion.IsOlderThan(((IVersionable)exchangeDataHandler).MaximumSupportedExchangeObjectVersion))
							{
								exchangeObjectVersion = ((IVersionable)exchangeDataHandler).MaximumSupportedExchangeObjectVersion;
							}
						}
						return exchangeObjectVersion;
					}
				}
				if (base.DataSource != null && this != base.DataSource && base.DataSource is IVersionable)
				{
					exchangeObjectVersion = (base.DataSource as IVersionable).MaximumSupportedExchangeObjectVersion;
				}
				return exchangeObjectVersion;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002CD8 File Offset: 0x00000ED8
		ExchangeObjectVersion IVersionable.MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return this.MaximumSupportedExchangeObjectVersion;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002CE0 File Offset: 0x00000EE0
		internal virtual ExchangeObjectVersion ExchangeVersion
		{
			get
			{
				ExchangeObjectVersion exchangeObjectVersion = ExchangeObjectVersion.Exchange2003;
				if (base.HasDataHandlers)
				{
					using (IEnumerator<DataHandler> enumerator = base.DataHandlers.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							DataHandler dataHandler = enumerator.Current;
							ExchangeDataHandler exchangeDataHandler = (ExchangeDataHandler)dataHandler;
							if (exchangeObjectVersion.IsOlderThan(((IVersionable)exchangeDataHandler).ExchangeVersion))
							{
								exchangeObjectVersion = ((IVersionable)exchangeDataHandler).ExchangeVersion;
							}
						}
						return exchangeObjectVersion;
					}
				}
				if (base.DataSource != null && this != base.DataSource && base.DataSource is IVersionable)
				{
					exchangeObjectVersion = (base.DataSource as IVersionable).ExchangeVersion;
				}
				return exchangeObjectVersion;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002D80 File Offset: 0x00000F80
		ExchangeObjectVersion IVersionable.ExchangeVersion
		{
			get
			{
				return this.ExchangeVersion;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002D88 File Offset: 0x00000F88
		bool IVersionable.IsReadOnly
		{
			get
			{
				return base.IsObjectReadOnly;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002D90 File Offset: 0x00000F90
		bool IVersionable.ExchangeVersionUpgradeSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002D93 File Offset: 0x00000F93
		bool IVersionable.IsPropertyAccessible(PropertyDefinition propertyDefinition)
		{
			return true;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D98 File Offset: 0x00000F98
		protected override void CheckObjectReadOnly()
		{
			bool isObjectReadOnly = false;
			if (base.HasDataHandlers)
			{
				using (IEnumerator<DataHandler> enumerator = base.DataHandlers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DataHandler dataHandler = enumerator.Current;
						if (dataHandler.IsObjectReadOnly)
						{
							isObjectReadOnly = true;
						}
					}
					goto IL_6C;
				}
			}
			if (base.DataSource != null && this != base.DataSource && base.DataSource is IVersionable)
			{
				isObjectReadOnly = (base.DataSource as IVersionable).IsReadOnly;
			}
			IL_6C:
			base.IsObjectReadOnly = isObjectReadOnly;
			this.SetObjectReadOnlyReason();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E30 File Offset: 0x00001030
		protected void SetObjectReadOnlyReason()
		{
			if (base.IsObjectReadOnly)
			{
				base.ObjectReadOnlyReason = Strings.VersionMismatchWarning(this.ExchangeVersion.ExchangeBuild);
				return;
			}
			base.ObjectReadOnlyReason = string.Empty;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002E61 File Offset: 0x00001061
		internal virtual ObjectSchema ObjectSchema
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002E64 File Offset: 0x00001064
		ObjectSchema IVersionable.ObjectSchema
		{
			get
			{
				return this.ObjectSchema;
			}
		}
	}
}
