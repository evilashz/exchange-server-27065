using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000195 RID: 405
	internal sealed class OwaEventXmlParser : OwaEventParserBase
	{
		// Token: 0x06000EBC RID: 3772 RVA: 0x0005D862 File Offset: 0x0005BA62
		internal OwaEventXmlParser(OwaEventHandlerBase eventHandler) : base(eventHandler, 4)
		{
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0005D86C File Offset: 0x0005BA6C
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

		// Token: 0x06000EBE RID: 3774 RVA: 0x0005D958 File Offset: 0x0005BB58
		protected override Hashtable ParseParameters()
		{
			Stream inputStream = base.EventHandler.HttpContext.Request.InputStream;
			if (inputStream.Length <= 0L)
			{
				return base.ParameterTable;
			}
			try
			{
				this.reader = SafeXmlFactory.CreateSafeXmlTextReader(inputStream);
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

		// Token: 0x06000EBF RID: 3775 RVA: 0x0005DA9C File Offset: 0x0005BC9C
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

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0005DAF4 File Offset: 0x0005BCF4
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

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0005DB8C File Offset: 0x0005BD8C
		private void ParseChild()
		{
			if (this.reader.NodeType == XmlNodeType.Text)
			{
				if (this.paramInfo.IsArray || this.paramInfo.IsStruct)
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
			if (this.reader.NodeType != XmlNodeType.Element)
			{
				base.ThrowParserException();
				return;
			}
			if (!this.paramInfo.IsArray && !this.paramInfo.IsStruct)
			{
				this.ThrowParserException("Array or struct expected");
			}
			if (!string.Equals(this.reader.Name, "item", StringComparison.OrdinalIgnoreCase))
			{
				if (!this.paramInfo.IsStruct)
				{
					this.ThrowParserException("Struct expected");
				}
				this.ParseAndAddStruct(null);
				this.state = OwaEventXmlParser.XmlParseState.ChildEnd;
				return;
			}
			if (!this.paramInfo.IsArray)
			{
				this.ThrowParserException("Array expected");
			}
			this.itemArray = new ArrayList();
			if (this.reader.IsEmptyElement)
			{
				base.AddEmptyItemToArray(this.paramInfo, this.itemArray);
				this.state = OwaEventXmlParser.XmlParseState.ItemEnd;
				return;
			}
			this.state = OwaEventXmlParser.XmlParseState.Item;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0005DD3C File Offset: 0x0005BF3C
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

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0005DD8C File Offset: 0x0005BF8C
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

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0005DE24 File Offset: 0x0005C024
		private void ParseItem()
		{
			if (this.reader.NodeType == XmlNodeType.Text)
			{
				if (this.paramInfo.IsStruct)
				{
					this.ThrowParserException("Expected struct, found simple type");
				}
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
			if (this.reader.NodeType == XmlNodeType.Element)
			{
				if (!this.paramInfo.IsStruct)
				{
					this.ThrowParserException("Expected simple value");
				}
				this.ParseAndAddStruct(this.itemArray);
				this.state = OwaEventXmlParser.XmlParseState.ItemEnd;
				return;
			}
			base.ThrowParserException();
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0005DF31 File Offset: 0x0005C131
		private void ParseItemText()
		{
			if (this.reader.NodeType == XmlNodeType.EndElement && string.Equals(this.reader.Name, "item", StringComparison.OrdinalIgnoreCase))
			{
				this.state = OwaEventXmlParser.XmlParseState.ItemEnd;
				return;
			}
			base.ThrowParserException();
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0005DF68 File Offset: 0x0005C168
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

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0005E028 File Offset: 0x0005C228
		private void ParseAndAddStruct(ArrayList itemArray)
		{
			OwaEventStructAttribute owaEventStructAttribute = OwaEventRegistry.FindStructInfo(this.reader.Name);
			if (owaEventStructAttribute != null)
			{
				bool isEmptyElement = this.reader.IsEmptyElement;
				object value = this.ParseStruct(owaEventStructAttribute);
				if (itemArray != null)
				{
					base.AddStructToArray(this.paramInfo, itemArray, value);
				}
				else
				{
					base.AddStructParameter(this.paramInfo, value);
				}
				if (!isEmptyElement)
				{
					if (!this.reader.Read())
					{
						this.ThrowParserException("Unexpected end of request");
					}
					else if (this.reader.NodeType != XmlNodeType.EndElement || !string.Equals(owaEventStructAttribute.Name, this.reader.Name, StringComparison.Ordinal))
					{
						this.ThrowParserException("Expected end of struct");
					}
				}
				if (!this.reader.Read())
				{
					this.ThrowParserException("Unexpected end of request");
					return;
				}
				if (itemArray != null)
				{
					if (this.reader.NodeType != XmlNodeType.EndElement || !string.Equals(this.reader.Name, "item", StringComparison.OrdinalIgnoreCase))
					{
						this.ThrowParserException("Expected end of item");
						return;
					}
				}
				else if (this.reader.NodeType != XmlNodeType.EndElement || !string.Equals(this.reader.Name, this.paramInfo.Name, StringComparison.Ordinal))
				{
					this.ThrowParserException("Expected end of param");
					return;
				}
			}
			else
			{
				this.ThrowParserException(string.Format(CultureInfo.InvariantCulture, "Type '{0}' is unknown", new object[]
				{
					this.reader.Name
				}));
			}
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0005E184 File Offset: 0x0005C384
		private object ParseStruct(OwaEventStructAttribute structInfo)
		{
			uint num = 0U;
			object obj = Activator.CreateInstance(structInfo.StructType);
			if (this.reader.MoveToFirstAttribute())
			{
				int num2 = 0;
				do
				{
					if (num2 >= 32)
					{
						this.ThrowParserException("Reached maximum number of fields per struct");
					}
					num2++;
					OwaEventFieldAttribute owaEventFieldAttribute = structInfo.FindFieldInfo(this.reader.Name);
					if (owaEventFieldAttribute == null)
					{
						this.ThrowParserException(string.Format(CultureInfo.InvariantCulture, "Field '{0}' doesn't exist", new object[]
						{
							this.reader.Name
						}));
					}
					FieldInfo fieldInfo = owaEventFieldAttribute.FieldInfo;
					Type fieldType = owaEventFieldAttribute.FieldType;
					object value;
					if (fieldType != typeof(string))
					{
						value = base.ConvertToStrongType(owaEventFieldAttribute.FieldType, this.reader.Value);
					}
					else
					{
						value = this.reader.Value;
					}
					fieldInfo.SetValue(obj, value);
					if ((num & owaEventFieldAttribute.FieldMask) != 0U)
					{
						this.ThrowParserException(string.Format(CultureInfo.InvariantCulture, "Field '{0}' is found twice", new object[]
						{
							this.reader.Name
						}));
					}
					num |= owaEventFieldAttribute.FieldMask;
				}
				while (this.reader.MoveToNextAttribute());
			}
			if ((structInfo.RequiredMask & num) != structInfo.RequiredMask)
			{
				this.ThrowParserException("A required field in the struct wasn't present");
			}
			if (num != structInfo.AllFieldsMask)
			{
				uint num3 = ~num & structInfo.AllFieldsMask;
				for (int i = 0; i < structInfo.FieldCount; i++)
				{
					if ((num3 & 1U) != 0U)
					{
						OwaEventFieldAttribute owaEventFieldAttribute2 = structInfo.FieldInfoIndexTable[i];
						owaEventFieldAttribute2.FieldInfo.SetValue(obj, owaEventFieldAttribute2.DefaultValue);
					}
					num3 >>= 1;
				}
			}
			return obj;
		}

		// Token: 0x04000A0A RID: 2570
		internal const string RequestBodyItemName = "item";

		// Token: 0x04000A0B RID: 2571
		internal const string RequestBodyParamsName = "params";

		// Token: 0x04000A0C RID: 2572
		private OwaEventParameterAttribute paramInfo;

		// Token: 0x04000A0D RID: 2573
		private ArrayList itemArray;

		// Token: 0x04000A0E RID: 2574
		private OwaEventXmlParser.XmlParseState state;

		// Token: 0x04000A0F RID: 2575
		private XmlTextReader reader;

		// Token: 0x02000196 RID: 406
		private enum XmlParseState
		{
			// Token: 0x04000A11 RID: 2577
			Start,
			// Token: 0x04000A12 RID: 2578
			Root,
			// Token: 0x04000A13 RID: 2579
			Child,
			// Token: 0x04000A14 RID: 2580
			ChildText,
			// Token: 0x04000A15 RID: 2581
			ChildEnd,
			// Token: 0x04000A16 RID: 2582
			Item,
			// Token: 0x04000A17 RID: 2583
			ItemText,
			// Token: 0x04000A18 RID: 2584
			ItemEnd,
			// Token: 0x04000A19 RID: 2585
			Finished
		}
	}
}
