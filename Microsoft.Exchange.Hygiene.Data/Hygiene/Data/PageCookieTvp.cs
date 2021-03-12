using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000097 RID: 151
	internal class PageCookieTvp : DataTable
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x00011028 File Offset: 0x0000F228
		public PageCookieTvp()
		{
			this.InitializeSchema();
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00011038 File Offset: 0x0000F238
		internal static PageCookieTvp Deserialize(string pageCookieXml)
		{
			PageCookieTvp pageCookieTvp = new PageCookieTvp();
			if (!string.IsNullOrWhiteSpace(pageCookieXml))
			{
				using (XmlReader xmlReader = XmlReader.Create(new StringReader(pageCookieXml), PageCookieTvp.xrs))
				{
					while (xmlReader.Read())
					{
						if (xmlReader.NodeType != XmlNodeType.Element || xmlReader.Name != "row")
						{
							throw new InvalidOperationException("XML data is invalid. Unable to deserialize page cookie.");
						}
						DataRow dataRow = pageCookieTvp.NewRow();
						string attribute = xmlReader.GetAttribute(PageCookieTvp.DatabaseNameCol);
						if (!string.IsNullOrWhiteSpace(attribute))
						{
							dataRow[PageCookieTvp.DatabaseNameCol] = attribute;
						}
						string attribute2 = xmlReader.GetAttribute(PageCookieTvp.LastChangedDatetimeCol);
						if (!string.IsNullOrWhiteSpace(attribute2))
						{
							dataRow[PageCookieTvp.LastChangedDatetimeCol] = attribute2;
						}
						string attribute3 = xmlReader.GetAttribute(PageCookieTvp.LastEntityIdCol);
						if (!string.IsNullOrWhiteSpace(attribute3))
						{
							dataRow[PageCookieTvp.LastEntityIdCol] = attribute3;
						}
						pageCookieTvp.Rows.Add(dataRow);
					}
				}
			}
			return pageCookieTvp;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00011324 File Offset: 0x0000F524
		internal static IEnumerable<DateTime> GetDateTimes(string pageCookieXml)
		{
			if (!string.IsNullOrWhiteSpace(pageCookieXml))
			{
				using (XmlReader reader = XmlReader.Create(new StringReader(pageCookieXml), PageCookieTvp.xrs))
				{
					while (reader.Read())
					{
						if (reader.NodeType != XmlNodeType.Element || reader.Name != "row")
						{
							throw new InvalidOperationException("XML data is invalid. Unable to deserialize page cookie.");
						}
						string lastChangedDatetime = reader.GetAttribute(PageCookieTvp.LastChangedDatetimeCol);
						if (!string.IsNullOrWhiteSpace(lastChangedDatetime))
						{
							yield return DateTime.Parse(lastChangedDatetime);
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00011344 File Offset: 0x0000F544
		private void InitializeSchema()
		{
			base.Columns.Add(new DataColumn(PageCookieTvp.DatabaseNameCol, typeof(string)));
			base.Columns.Add(new DataColumn(PageCookieTvp.LastChangedDatetimeCol, typeof(DateTime)));
			base.Columns.Add(new DataColumn(PageCookieTvp.LastEntityIdCol, typeof(Guid)));
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000113B0 File Offset: 0x0000F5B0
		internal static string CreatePageCookie(string[] dbNames, DateTime dateTime)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in dbNames)
			{
				XElement xelement = new XElement("row");
				xelement.SetAttributeValue(PageCookieTvp.LastChangedDatetimeCol, dateTime);
				xelement.SetAttributeValue(PageCookieTvp.DatabaseNameCol, value);
				xelement.SetAttributeValue(PageCookieTvp.LastEntityIdCol, Guid.Empty);
				stringBuilder.Append(xelement);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000343 RID: 835
		private static readonly string DatabaseNameCol = "nvc_DatabaseName";

		// Token: 0x04000344 RID: 836
		private static readonly string LastChangedDatetimeCol = "dt_LastChangedDatetime";

		// Token: 0x04000345 RID: 837
		private static readonly string LastEntityIdCol = "id_LastEntityId";

		// Token: 0x04000346 RID: 838
		private static readonly XmlReaderSettings xrs = new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Fragment
		};
	}
}
