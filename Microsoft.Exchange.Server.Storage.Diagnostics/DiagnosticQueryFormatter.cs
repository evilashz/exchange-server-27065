using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000010 RID: 16
	public abstract class DiagnosticQueryFormatter<T>
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00004CE9 File Offset: 0x00002EE9
		protected DiagnosticQueryFormatter(DiagnosticQueryResults results)
		{
			this.results = results;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004CF8 File Offset: 0x00002EF8
		protected DiagnosticQueryResults Results
		{
			get
			{
				return this.results;
			}
		}

		// Token: 0x06000090 RID: 144
		public abstract T FormatResults();

		// Token: 0x06000091 RID: 145 RVA: 0x00004D00 File Offset: 0x00002F00
		protected static string FormatValue(object value)
		{
			if (value is byte[])
			{
				return string.Format("0x{0}", BitConverter.ToString((byte[])value).Replace("-", string.Empty));
			}
			if (value is DateTime)
			{
				return ((DateTime)value).ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss'.'fffffff");
			}
			if (value != null)
			{
				return value.ToString();
			}
			return "NULL";
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004D68 File Offset: 0x00002F68
		protected static void WriteValue(XElement element, object value)
		{
			Array array = value as Array;
			if (array == null || value is byte[])
			{
				element.Value = DiagnosticQueryFormatter<T>.FormatValue(value);
				return;
			}
			DiagnosticQueryFormatter<T>.WriteIndexedElements(element, "Item", array);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004DA0 File Offset: 0x00002FA0
		protected static void WriteIndexedElements(XElement parent, string name, Array collection)
		{
			for (int i = 0; i < collection.Length; i++)
			{
				XElement xelement = new XElement(name);
				DiagnosticQueryFormatter<T>.WriteValue(xelement, collection.GetValue(i));
				xelement.Add(new XAttribute("Index", i));
				parent.Add(xelement);
			}
		}

		// Token: 0x04000087 RID: 135
		private readonly DiagnosticQueryResults results;
	}
}
