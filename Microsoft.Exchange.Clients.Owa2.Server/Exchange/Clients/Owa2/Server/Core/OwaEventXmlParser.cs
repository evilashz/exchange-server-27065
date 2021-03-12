using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001E3 RID: 483
	internal sealed class OwaEventXmlParser : OwaEventParserBase
	{
		// Token: 0x06001108 RID: 4360 RVA: 0x0004102A File Offset: 0x0003F22A
		internal OwaEventXmlParser(OwaEventHandlerBase eventHandler) : base(eventHandler, 4)
		{
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00041034 File Offset: 0x0003F234
		protected override void ThrowParserException(string description)
		{
			int num = 0;
			int num2 = 0;
			if (this.reader != null)
			{
				num = this.reader.LineNumber;
				num2 = this.reader.LinePosition;
			}
			Stream inputStream = base.EventHandler.HttpContext.Request.InputStream;
			string text;
			using (StreamReader streamReader = new StreamReader(inputStream))
			{
				text = streamReader.ReadToEnd();
			}
			throw new OwaInvalidRequestException(string.Format(CultureInfo.InvariantCulture, "Invalid request. Line number: {0} Position: {1}. Url: {2}. Request body: {3}. {4}", new object[]
			{
				num.ToString(CultureInfo.InvariantCulture),
				num2.ToString(CultureInfo.InvariantCulture),
				base.EventHandler.HttpContext.Request.RawUrl,
				text,
				(description != null) ? (" " + description) : string.Empty
			}), null, this);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00041120 File Offset: 0x0003F320
		protected override Hashtable ParseParameters()
		{
			Stream inputStream = base.EventHandler.HttpContext.Request.InputStream;
			if (inputStream.Length <= 0L)
			{
				return base.ParameterTable;
			}
			try
			{
				this.reader = new XmlTextReader(inputStream);
				this.reader.WhitespaceHandling = WhitespaceHandling.All;
				this.paramInfo = null;
				this.itemArray = null;
				this.state = OwaEventXmlParser.XmlParseState.Start;
				while (this.state != OwaEventXmlParser.XmlParseState.Finished && this.reader.Read())
				{
					switch (this.state)
					{
					case OwaEventXmlParser.XmlParseState.Start:
						this.ParseStart();
						break;
					case OwaEventXmlParser.XmlParseState.Root:
						this.ParseRoot();
						break;
					case OwaEventXmlParser.XmlParseState.Child:
						this.ParseChild();
						break;
					case OwaEventXmlParser.XmlParseState.ChildText:
						this.ParseChildText();
						break;
					case OwaEventXmlParser.XmlParseState.ChildEnd:
						this.ParseChildEnd();
						break;
					case OwaEventXmlParser.XmlParseState.Item:
						this.ParseItem();
						break;
					case OwaEventXmlParser.XmlParseState.ItemText:
						this.ParseItemText();
						break;
					case OwaEventXmlParser.XmlParseState.ItemEnd:
						this.ParseItemEnd();
						break;
					}
				}
			}
			catch (XmlException ex)
			{
				ExTraceGlobals.OehTracer.TraceDebug<string>(0L, "Parser threw an XML exception: {0}'", ex.Message);
				throw new OwaInvalidRequestException(ex.Message, ex, this);
			}
			finally
			{
				this.reader.Close();
			}
			return base.ParameterTable;
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00041264 File Offset: 0x0003F464
		private void ParseStart()
		{
			if (XmlNodeType.Element != this.reader.NodeType || !string.Equals("params", this.reader.Name, StringComparison.OrdinalIgnoreCase))
			{
				base.ThrowParserException();
				return;
			}
			if (this.reader.IsEmptyElement)
			{
				this.state = OwaEventXmlParser.XmlParseState.Finished;
				return;
			}
			this.state = OwaEventXmlParser.XmlParseState.Root;
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x000412BC File Offset: 0x0003F4BC
		private void ParseRoot()
		{
			if (this.reader.NodeType == XmlNodeType.Element)
			{
				this.paramInfo = base.GetParamInfo(this.reader.Name);
				if (this.reader.IsEmptyElement)
				{
					base.AddEmptyParameter(this.paramInfo);
					this.state = OwaEventXmlParser.XmlParseState.ChildEnd;
					this.paramInfo = null;
					return;
				}
				this.state = OwaEventXmlParser.XmlParseState.Child;
				return;
			}
			else
			{
				if (this.reader.NodeType == XmlNodeType.EndElement && string.Equals("params", this.reader.Name, StringComparison.OrdinalIgnoreCase))
				{
					this.state = OwaEventXmlParser.XmlParseState.Finished;
					return;
				}
				base.ThrowParserException();
				return;
			}
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00041354 File Offset: 0x0003F554
		private void ParseChild()
		{
			if (this.reader.NodeType == XmlNodeType.Text)
			{
				if (this.paramInfo.IsArray)
				{
					base.ThrowParserException();
				}
				base.AddSimpleTypeParameter(this.paramInfo, this.reader.Value);
				this.state = OwaEventXmlParser.XmlParseState.ChildText;
				return;
			}
			if (this.reader.NodeType == XmlNodeType.Whitespace)
			{
				if (this.reader.Value == null)
				{
					base.ThrowParserException();
				}
				base.AddSimpleTypeParameter(this.paramInfo, this.reader.Value);
				this.state = OwaEventXmlParser.XmlParseState.ChildText;
				return;
			}
			if (this.reader.NodeType == XmlNodeType.EndElement && string.Equals(this.reader.Name, this.paramInfo.Name, StringComparison.OrdinalIgnoreCase))
			{
				base.AddEmptyParameter(this.paramInfo);
				this.state = OwaEventXmlParser.XmlParseState.ChildEnd;
				this.paramInfo = null;
				return;
			}
			if (this.reader.NodeType == XmlNodeType.Element)
			{
				if (!this.paramInfo.IsArray)
				{
					this.ThrowParserException("Array expected");
				}
				if (string.Equals(this.reader.Name, "item", StringComparison.OrdinalIgnoreCase))
				{
					this.itemArray = new ArrayList();
					if (this.reader.IsEmptyElement)
					{
						base.AddEmptyItemToArray(this.paramInfo, this.itemArray);
						this.state = OwaEventXmlParser.XmlParseState.ItemEnd;
						return;
					}
					this.state = OwaEventXmlParser.XmlParseState.Item;
					return;
				}
			}
			else
			{
				base.ThrowParserException();
			}
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x000414A8 File Offset: 0x0003F6A8
		private void ParseChildText()
		{
			if (this.reader.NodeType == XmlNodeType.EndElement && string.Equals(this.reader.Name, this.paramInfo.Name, StringComparison.OrdinalIgnoreCase))
			{
				this.state = OwaEventXmlParser.XmlParseState.ChildEnd;
				this.paramInfo = null;
				return;
			}
			base.ThrowParserException();
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x000414F8 File Offset: 0x0003F6F8
		private void ParseChildEnd()
		{
			if (this.reader.NodeType == XmlNodeType.EndElement && string.Equals("params", this.reader.Name, StringComparison.OrdinalIgnoreCase))
			{
				this.state = OwaEventXmlParser.XmlParseState.Finished;
				return;
			}
			if (this.reader.NodeType != XmlNodeType.Element)
			{
				base.ThrowParserException();
				return;
			}
			this.paramInfo = base.GetParamInfo(this.reader.Name);
			if (this.reader.IsEmptyElement)
			{
				base.AddEmptyParameter(this.paramInfo);
				this.state = OwaEventXmlParser.XmlParseState.ChildEnd;
				this.paramInfo = null;
				return;
			}
			this.state = OwaEventXmlParser.XmlParseState.Child;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00041590 File Offset: 0x0003F790
		private void ParseItem()
		{
			if (this.reader.NodeType == XmlNodeType.Text)
			{
				base.AddSimpleTypeToArray(this.paramInfo, this.itemArray, this.reader.Value);
				this.state = OwaEventXmlParser.XmlParseState.ItemText;
				return;
			}
			if (this.reader.NodeType == XmlNodeType.Whitespace)
			{
				base.AddSimpleTypeToArray(this.paramInfo, this.itemArray, this.reader.Value);
				this.state = OwaEventXmlParser.XmlParseState.ItemText;
				return;
			}
			if (this.reader.NodeType == XmlNodeType.EndElement && string.Equals(this.reader.Name, "item", StringComparison.OrdinalIgnoreCase))
			{
				base.AddEmptyItemToArray(this.paramInfo, this.itemArray);
				this.state = OwaEventXmlParser.XmlParseState.ItemEnd;
				return;
			}
			base.ThrowParserException();
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0004164B File Offset: 0x0003F84B
		private void ParseItemText()
		{
			if (this.reader.NodeType == XmlNodeType.EndElement && string.Equals(this.reader.Name, "item", StringComparison.OrdinalIgnoreCase))
			{
				this.state = OwaEventXmlParser.XmlParseState.ItemEnd;
				return;
			}
			base.ThrowParserException();
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00041684 File Offset: 0x0003F884
		private void ParseItemEnd()
		{
			if (this.reader.NodeType == XmlNodeType.Element && string.Equals(this.reader.Name, "item", StringComparison.OrdinalIgnoreCase))
			{
				if (this.reader.IsEmptyElement)
				{
					base.AddEmptyItemToArray(this.paramInfo, this.itemArray);
					this.state = OwaEventXmlParser.XmlParseState.ItemEnd;
					return;
				}
				this.state = OwaEventXmlParser.XmlParseState.Item;
				return;
			}
			else
			{
				if (this.reader.NodeType == XmlNodeType.EndElement && string.Equals(this.reader.Name, this.paramInfo.Name, StringComparison.OrdinalIgnoreCase))
				{
					base.AddArrayParameter(this.paramInfo, this.itemArray);
					this.state = OwaEventXmlParser.XmlParseState.ChildEnd;
					this.itemArray = null;
					this.paramInfo = null;
					return;
				}
				base.ThrowParserException();
				return;
			}
		}

		// Token: 0x04000A0D RID: 2573
		internal const string RequestBodyItemName = "item";

		// Token: 0x04000A0E RID: 2574
		internal const string RequestBodyParamsName = "params";

		// Token: 0x04000A0F RID: 2575
		private OwaEventParameterAttribute paramInfo;

		// Token: 0x04000A10 RID: 2576
		private ArrayList itemArray;

		// Token: 0x04000A11 RID: 2577
		private OwaEventXmlParser.XmlParseState state;

		// Token: 0x04000A12 RID: 2578
		private XmlTextReader reader;

		// Token: 0x020001E4 RID: 484
		private enum XmlParseState
		{
			// Token: 0x04000A14 RID: 2580
			Start,
			// Token: 0x04000A15 RID: 2581
			Root,
			// Token: 0x04000A16 RID: 2582
			Child,
			// Token: 0x04000A17 RID: 2583
			ChildText,
			// Token: 0x04000A18 RID: 2584
			ChildEnd,
			// Token: 0x04000A19 RID: 2585
			Item,
			// Token: 0x04000A1A RID: 2586
			ItemText,
			// Token: 0x04000A1B RID: 2587
			ItemEnd,
			// Token: 0x04000A1C RID: 2588
			Finished
		}
	}
}
