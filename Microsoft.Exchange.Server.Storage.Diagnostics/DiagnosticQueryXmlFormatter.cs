using System;
using System.Xml.Linq;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000031 RID: 49
	public class DiagnosticQueryXmlFormatter : DiagnosticQueryFormatter<XElement>
	{
		// Token: 0x06000187 RID: 391 RVA: 0x0000CAA9 File Offset: 0x0000ACA9
		private DiagnosticQueryXmlFormatter(DiagnosticQueryResults results) : base(results)
		{
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000CAB2 File Offset: 0x0000ACB2
		public static DiagnosticQueryXmlFormatter Create(DiagnosticQueryResults results)
		{
			return new DiagnosticQueryXmlFormatter(results);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000CABC File Offset: 0x0000ACBC
		public static XElement FormatException(DiagnosticQueryException e)
		{
			DiagnosticQueryResults results = DiagnosticQueryResults.Create(new string[]
			{
				e.GetType().Name
			}, new Type[]
			{
				e.Message.GetType()
			}, new uint[]
			{
				(uint)e.Message.Length
			}, new object[][]
			{
				new object[]
				{
					e.Message
				}
			}, false, false);
			return DiagnosticQueryXmlFormatter.WriteResults(results);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000CB3B File Offset: 0x0000AD3B
		public override XElement FormatResults()
		{
			return DiagnosticQueryXmlFormatter.WriteResults(base.Results);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000CB48 File Offset: 0x0000AD48
		private static XElement WriteResults(DiagnosticQueryResults results)
		{
			XElement xelement = DiagnosticQueryXmlFormatter.WriteColumns(results);
			XElement xelement2 = DiagnosticQueryXmlFormatter.WriteRows(results);
			XAttribute xattribute = new XAttribute("Truncated", results.IsTruncated);
			XAttribute xattribute2 = new XAttribute("Interrupted", results.IsInterrupted);
			return new XElement("Results", new object[]
			{
				xattribute,
				xattribute2,
				xelement,
				xelement2
			});
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000CBC8 File Offset: 0x0000ADC8
		private static XElement WriteColumns(DiagnosticQueryResults results)
		{
			XElement xelement = new XElement("Columns");
			for (int i = 0; i < results.Names.Count; i++)
			{
				XElement xelement2 = new XElement("Column");
				xelement2.Add(new XAttribute("Index", i));
				xelement2.Add(new XAttribute("Name", results.Names[i]));
				xelement2.Add(new XAttribute("Type", results.Types[i].FullName));
				xelement.Add(xelement2);
			}
			return xelement;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000CC78 File Offset: 0x0000AE78
		private static XElement WriteRows(DiagnosticQueryResults results)
		{
			XElement xelement = new XElement("Rows");
			foreach (object[] collection in results.Values)
			{
				XElement xelement2 = new XElement("Row");
				DiagnosticQueryFormatter<XElement>.WriteIndexedElements(xelement2, "Value", collection);
				xelement.Add(xelement2);
			}
			return xelement;
		}
	}
}
