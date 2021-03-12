using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000050 RID: 80
	public class ExchangeDataHandler : DataHandler, IVersionable
	{
		// Token: 0x0600032F RID: 815 RVA: 0x0000B6AA File Offset: 0x000098AA
		public ExchangeDataHandler()
		{
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000B6B2 File Offset: 0x000098B2
		public ExchangeDataHandler(ICloneable dataSource) : this()
		{
			base.DataSource = dataSource.Clone();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000B6C6 File Offset: 0x000098C6
		public ExchangeDataHandler(bool breakOnError) : this()
		{
			base.BreakOnError = breakOnError;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000B6D8 File Offset: 0x000098D8
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

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000B778 File Offset: 0x00009978
		ExchangeObjectVersion IVersionable.MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return this.MaximumSupportedExchangeObjectVersion;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000B780 File Offset: 0x00009980
		bool IVersionable.ExchangeVersionUpgradeSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000B783 File Offset: 0x00009983
		bool IVersionable.IsPropertyAccessible(PropertyDefinition propertyDefinition)
		{
			return true;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000B788 File Offset: 0x00009988
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000B828 File Offset: 0x00009A28
		ExchangeObjectVersion IVersionable.ExchangeVersion
		{
			get
			{
				return this.ExchangeVersion;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000B830 File Offset: 0x00009A30
		bool IVersionable.IsReadOnly
		{
			get
			{
				return base.IsObjectReadOnly;
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000B838 File Offset: 0x00009A38
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

		// Token: 0x0600033A RID: 826 RVA: 0x0000B8D0 File Offset: 0x00009AD0
		protected void SetObjectReadOnlyReason()
		{
			if (base.IsObjectReadOnly)
			{
				base.ObjectReadOnlyReason = Strings.VersionMismatchWarning(this.ExchangeVersion.ExchangeBuild);
				return;
			}
			base.ObjectReadOnlyReason = string.Empty;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000B901 File Offset: 0x00009B01
		internal virtual ObjectSchema ObjectSchema
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000B904 File Offset: 0x00009B04
		ObjectSchema IVersionable.ObjectSchema
		{
			get
			{
				return this.ObjectSchema;
			}
		}
	}
}
