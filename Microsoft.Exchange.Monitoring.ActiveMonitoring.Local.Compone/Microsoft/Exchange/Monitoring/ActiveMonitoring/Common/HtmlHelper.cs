using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000128 RID: 296
	public static class HtmlHelper
	{
		// Token: 0x060008C0 RID: 2240 RVA: 0x00033347 File Offset: 0x00031547
		public static string CreateLink(string text, string url)
		{
			return string.Format(HtmlHelper.LinkTag, text, url);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0003335D File Offset: 0x0003155D
		public static string CreateTable(string[] headers, IEnumerable<IEnumerable<string>> rows)
		{
			return HtmlHelper.CreateTable<IEnumerable<string>>(headers, rows, (IEnumerable<string> row) => row.ToArray<string>());
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00033384 File Offset: 0x00031584
		public static string CreateTable<T>(string[] headers, IEnumerable<T> rowData, Func<T, string[]> getRowCellsDelegate)
		{
			if (headers == null || headers.Length == 0)
			{
				throw new ArgumentException("Headers cannot be null or 0 length", "headers");
			}
			if (getRowCellsDelegate == null)
			{
				throw new ArgumentNullException("getRowCellsDelegate");
			}
			if (rowData == null)
			{
				throw new ArgumentNullException("rowData");
			}
			StringBuilder stringBuilder = new StringBuilder(5120, 102400);
			StringBuilder stringBuilder2 = new StringBuilder(1024);
			try
			{
				foreach (string arg in headers)
				{
					stringBuilder2.AppendFormat(HtmlHelper.TableHeaderCell, arg);
				}
				stringBuilder.AppendFormat(HtmlHelper.TableStartHtml, stringBuilder2.ToString());
				string text = HtmlHelper.RowOneColour;
				foreach (T arg2 in rowData)
				{
					string[] array = getRowCellsDelegate(arg2);
					if (array.Length != headers.Length)
					{
						throw new ArgumentException(string.Format("Cell's array length ({0}) doesn't match length of header rows ({1})", array.Length, headers.Length));
					}
					StringBuilder stringBuilder3 = new StringBuilder(5120);
					foreach (string arg3 in array)
					{
						stringBuilder3.AppendFormat(HtmlHelper.TableRowCell, arg3);
					}
					stringBuilder.AppendFormat(HtmlHelper.TableRowHtml, text, stringBuilder3.ToString());
					text = (text.Equals(HtmlHelper.RowOneColour) ? HtmlHelper.RowTwoColour : HtmlHelper.RowOneColour);
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				stringBuilder.Remove(stringBuilder.Length - 1 - HtmlHelper.TableEndHtml.Length, HtmlHelper.TableEndHtml.Length);
			}
			finally
			{
				stringBuilder.Append(HtmlHelper.TableEndHtml);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000605 RID: 1541
		private const int StartingTableSize = 5120;

		// Token: 0x04000606 RID: 1542
		private const int MaxTableSize = 102400;

		// Token: 0x04000607 RID: 1543
		private const int MaxRowSize = 5120;

		// Token: 0x04000608 RID: 1544
		private static readonly string RowOneColour = "#fff";

		// Token: 0x04000609 RID: 1545
		private static readonly string RowTwoColour = "#f2f2f2";

		// Token: 0x0400060A RID: 1546
		private static readonly string TableStartHtml = "<table cellpadding=\"3\" cellspacing=\"0\" style=\"border: 1px solid #ccc;\"><tr style=\"background: #00223B; color: #fefefe; font-weight: bold\">{0}</tr>";

		// Token: 0x0400060B RID: 1547
		private static readonly string TableHeaderCell = "<th>{0}</th>";

		// Token: 0x0400060C RID: 1548
		private static readonly string TableRowCell = "<td>{0}</td>";

		// Token: 0x0400060D RID: 1549
		private static readonly string TableRowHtml = "<tr style=\"background: {0}\">{1}</tr>";

		// Token: 0x0400060E RID: 1550
		private static readonly string TableEndHtml = "</table>";

		// Token: 0x0400060F RID: 1551
		private static readonly string LinkTag = "<a href=\"{1}\">{0}</a>";
	}
}
