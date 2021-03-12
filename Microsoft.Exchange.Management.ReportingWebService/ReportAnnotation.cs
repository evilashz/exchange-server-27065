using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x0200003E RID: 62
	internal class ReportAnnotation : IReportAnnotation
	{
		// Token: 0x06000165 RID: 357 RVA: 0x00007805 File Offset: 0x00005A05
		private ReportAnnotation()
		{
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000780D File Offset: 0x00005A0D
		public string ReportTitle
		{
			get
			{
				return this.reportTitle.GetLocalizedString();
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000079A8 File Offset: 0x00005BA8
		public IEnumerable<string> Xaxis
		{
			get
			{
				foreach (ReportAnnotation.StringInfo xaixs in this.xaxises)
				{
					yield return xaixs.GetLocalizedString();
				}
				yield break;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00007B54 File Offset: 0x00005D54
		public IEnumerable<string> Yaxis
		{
			get
			{
				foreach (ReportAnnotation.StringInfo yaixs in this.yaxises)
				{
					yield return yaixs.GetLocalizedString();
				}
				yield break;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007B74 File Offset: 0x00005D74
		public static IReportAnnotation Load(XmlNode annotationNode)
		{
			ReportAnnotation reportAnnotation = new ReportAnnotation();
			reportAnnotation.reportTitle = ReportAnnotation.GetStringInfo(ReportingSchema.SelectSingleNode(annotationNode, "ReportTitle"));
			ReportingSchema.CheckCondition(reportAnnotation.reportTitle != null && !string.IsNullOrEmpty(reportAnnotation.reportTitle.StringId), "Report title isn't present.");
			reportAnnotation.xaxises = ReportAnnotation.LoadSeries(annotationNode, "XAxis");
			reportAnnotation.yaxises = ReportAnnotation.LoadSeries(annotationNode, "YAxis");
			ReportingSchema.CheckCondition(reportAnnotation.yaxises != null && reportAnnotation.yaxises.Count > 0, "Report Y-Axis doesn't present.");
			return reportAnnotation;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007C0C File Offset: 0x00005E0C
		private static List<ReportAnnotation.StringInfo> LoadSeries(XmlNode parentNode, string xpath)
		{
			List<ReportAnnotation.StringInfo> list = new List<ReportAnnotation.StringInfo>();
			using (XmlNodeList xmlNodeList = parentNode.SelectNodes(xpath))
			{
				foreach (object obj in xmlNodeList)
				{
					XmlNode node = (XmlNode)obj;
					list.Add(ReportAnnotation.GetStringInfo(node));
				}
			}
			return list;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007C90 File Offset: 0x00005E90
		private static ReportAnnotation.StringInfo GetStringInfo(XmlNode node)
		{
			if (node == null)
			{
				return null;
			}
			string value = node.Attributes["Loc"].Value.Trim();
			bool localized = false;
			if (!string.IsNullOrEmpty(value))
			{
				bool.TryParse(value, out localized);
			}
			return new ReportAnnotation.StringInfo(node.InnerText, localized);
		}

		// Token: 0x040000BD RID: 189
		private const string LocAttribute = "Loc";

		// Token: 0x040000BE RID: 190
		private const string ReportTitleNode = "ReportTitle";

		// Token: 0x040000BF RID: 191
		private const string XAxisNode = "XAxis";

		// Token: 0x040000C0 RID: 192
		private const string YAxisNode = "YAxis";

		// Token: 0x040000C1 RID: 193
		private ReportAnnotation.StringInfo reportTitle;

		// Token: 0x040000C2 RID: 194
		private List<ReportAnnotation.StringInfo> xaxises;

		// Token: 0x040000C3 RID: 195
		private List<ReportAnnotation.StringInfo> yaxises;

		// Token: 0x0200003F RID: 63
		private class StringInfo
		{
			// Token: 0x0600016C RID: 364 RVA: 0x00007CE0 File Offset: 0x00005EE0
			public StringInfo(string stringId, bool localized)
			{
				this.StringId = stringId;
				this.localized = false;
				AnnotationStrings.IDs annotationStringId;
				if (localized && Enum.TryParse<AnnotationStrings.IDs>(this.StringId, true, out annotationStringId))
				{
					this.AnnotationStringId = annotationStringId;
					this.localized = true;
				}
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x0600016D RID: 365 RVA: 0x00007D22 File Offset: 0x00005F22
			// (set) Token: 0x0600016E RID: 366 RVA: 0x00007D2A File Offset: 0x00005F2A
			public string StringId { get; private set; }

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x0600016F RID: 367 RVA: 0x00007D33 File Offset: 0x00005F33
			// (set) Token: 0x06000170 RID: 368 RVA: 0x00007D3B File Offset: 0x00005F3B
			public AnnotationStrings.IDs AnnotationStringId { get; private set; }

			// Token: 0x06000171 RID: 369 RVA: 0x00007D44 File Offset: 0x00005F44
			public string GetLocalizedString()
			{
				if (this.localized)
				{
					return AnnotationStrings.GetLocalizedString(this.AnnotationStringId);
				}
				return this.StringId;
			}

			// Token: 0x040000C4 RID: 196
			private readonly bool localized;
		}
	}
}
