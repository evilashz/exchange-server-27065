using System;
using System.Globalization;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001DB RID: 475
	public class ExchangeColumnHeaderCollection : ChangeNotifyingCollection<ExchangeColumnHeader>
	{
		// Token: 0x06001569 RID: 5481 RVA: 0x0005841E File Offset: 0x0005661E
		public ExchangeColumnHeader Add(string name, string text)
		{
			return this.Add(name, text, -2);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0005842A File Offset: 0x0005662A
		public ExchangeColumnHeader Add(string name, string text, int width)
		{
			return this.Add(name, text, width, false);
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00058436 File Offset: 0x00056636
		public ExchangeColumnHeader Add(string name, string text, int width, bool isDefault)
		{
			return this.Add(name, text, width, isDefault, string.Empty);
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00058448 File Offset: 0x00056648
		public ExchangeColumnHeader Add(string name, string text, bool isDefault)
		{
			return this.Add(name, text, -2, isDefault, string.Empty);
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0005845C File Offset: 0x0005665C
		public ExchangeColumnHeader Add(string name, string text, int width, bool isDefault, string defaultEmptyText)
		{
			return this.Add(name, text, width, isDefault, defaultEmptyText, null, null, null);
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0005847C File Offset: 0x0005667C
		public ExchangeColumnHeader Add(string name, string text, bool isDefault, string defaultEmptyText)
		{
			return this.Add(name, text, -2, isDefault, defaultEmptyText, null, null, null);
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0005849C File Offset: 0x0005669C
		public ExchangeColumnHeader Add(string name, string text, bool isDefault, string defaultEmptyText, ICustomFormatter customFormatter, string formatString, IFormatProvider formatProvider)
		{
			ExchangeColumnHeader exchangeColumnHeader = new ExchangeColumnHeader(name, text, -2, isDefault, defaultEmptyText, customFormatter, formatString, formatProvider);
			base.Add(exchangeColumnHeader);
			return exchangeColumnHeader;
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x000584C4 File Offset: 0x000566C4
		public ExchangeColumnHeader Add(string name, string text, int width, bool isDefault, string defaultEmptyText, ICustomFormatter customFormatter, string formatString, IFormatProvider formatProvider)
		{
			ExchangeColumnHeader exchangeColumnHeader = new ExchangeColumnHeader(name, text, width, isDefault, defaultEmptyText, customFormatter, formatString, formatProvider);
			base.Add(exchangeColumnHeader);
			return exchangeColumnHeader;
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x000584EC File Offset: 0x000566EC
		public void AddRange(ExchangeColumnHeader[] columns)
		{
			foreach (ExchangeColumnHeader item in columns)
			{
				base.Add(item);
			}
		}

		// Token: 0x17000506 RID: 1286
		public ExchangeColumnHeader this[string name]
		{
			get
			{
				ExchangeColumnHeader result = null;
				if (!string.IsNullOrEmpty(name))
				{
					foreach (ExchangeColumnHeader exchangeColumnHeader in this)
					{
						if (string.Compare(name, exchangeColumnHeader.Name, false, CultureInfo.InvariantCulture) == 0)
						{
							result = exchangeColumnHeader;
							break;
						}
					}
				}
				return result;
			}
		}
	}
}
