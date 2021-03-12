using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Mime.Encoders;

namespace Microsoft.Exchange.Data.ContentTypes.Internal
{
	// Token: 0x020000CD RID: 205
	internal class ContentLineReader : IDisposable
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x0002C68C File Offset: 0x0002A88C
		public ContentLineReader(Stream s, Encoding encoding, ComplianceTracker complianceTracker, ValueTypeContainer container)
		{
			this.valueType = container;
			this.parser = new ContentLineParser(s, encoding, complianceTracker);
			this.complianceTracker = complianceTracker;
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0002C6E2 File Offset: 0x0002A8E2
		public int Depth
		{
			get
			{
				this.CheckDisposed("Depth::get");
				if (ContentLineNodeType.BeforeComponentStart != this.nodeType)
				{
					return this.componentStack.Count;
				}
				return this.componentStack.Count - 1;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0002C712 File Offset: 0x0002A912
		public string ComponentName
		{
			get
			{
				this.CheckDisposed("ComponentName::get");
				return this.componentName;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0002C725 File Offset: 0x0002A925
		public string PropertyName
		{
			get
			{
				this.CheckDisposed("PropertyName::get");
				return this.propertyName;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x0002C738 File Offset: 0x0002A938
		public string ParameterName
		{
			get
			{
				this.CheckDisposed("ParameterName::get");
				return this.parameterName;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0002C74B File Offset: 0x0002A94B
		public ContentLineNodeType Type
		{
			get
			{
				this.CheckDisposed("Type::get");
				return this.nodeType;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x0002C75E File Offset: 0x0002A95E
		public ComplianceTracker ComplianceTracker
		{
			get
			{
				this.CheckDisposed("ComplianceTracker::get");
				return this.complianceTracker;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x0002C771 File Offset: 0x0002A971
		public ValueTypeContainer ValueType
		{
			get
			{
				this.CheckDisposed("ValueType::get");
				if (!this.valueType.IsInitialized)
				{
					this.valueType.SetPropertyName(this.PropertyName);
				}
				return this.valueType;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0002C7A2 File Offset: 0x0002A9A2
		public Encoding CurrentCharsetEncoding
		{
			get
			{
				this.CheckDisposed("CurrentEncoding::get");
				return this.parser.CurrentCharsetEncoding;
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0002C7BA File Offset: 0x0002A9BA
		protected virtual void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("ContentLineReader", methodName);
			}
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0002C7D0 File Offset: 0x0002A9D0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0002C7DF File Offset: 0x0002A9DF
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.isDisposed && this.parser != null)
			{
				this.parser.Dispose();
				this.parser = null;
			}
			this.isDisposed = true;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0002C80D File Offset: 0x0002AA0D
		public bool ReadNextComponent()
		{
			this.CheckDisposed("ReadNextComponent");
			this.DrainValueStream();
			while (this.Read())
			{
				if (this.nodeType == ContentLineNodeType.ComponentStart)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0002C838 File Offset: 0x0002AA38
		public bool ReadFirstChildComponent()
		{
			this.CheckDisposed("ReadFirstChildComponent");
			this.DrainValueStream();
			if (this.nodeType == ContentLineNodeType.ComponentEnd || this.nodeType == ContentLineNodeType.BeforeComponentEnd)
			{
				return false;
			}
			while (this.Read())
			{
				if (this.nodeType == ContentLineNodeType.ComponentStart)
				{
					return true;
				}
				if (this.nodeType == ContentLineNodeType.ComponentEnd)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0002C88C File Offset: 0x0002AA8C
		public bool ReadNextSiblingComponent()
		{
			this.CheckDisposed("ReadNextSiblingComponent");
			this.DrainValueStream();
			int depth = this.Depth;
			while ((this.nodeType != ContentLineNodeType.ComponentEnd || this.Depth > depth) && this.Read())
			{
			}
			if (this.nodeType == ContentLineNodeType.ComponentEnd)
			{
				this.Read();
			}
			while (this.nodeType == ContentLineNodeType.Property && this.Read())
			{
			}
			if (this.nodeType == ContentLineNodeType.BeforeComponentStart)
			{
				this.Read();
			}
			return this.nodeType == ContentLineNodeType.ComponentStart;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0002C908 File Offset: 0x0002AB08
		private bool Read()
		{
			ContentLineNodeType contentLineNodeType = this.nodeType;
			switch (contentLineNodeType)
			{
			case ContentLineNodeType.DocumentStart:
			case ContentLineNodeType.ComponentStart:
				goto IL_E3;
			case ContentLineNodeType.ComponentEnd:
				if (this.componentStack.Count > 0)
				{
					this.componentName = this.componentStack.Pop();
					goto IL_E3;
				}
				goto IL_E3;
			case ContentLineNodeType.ComponentStart | ContentLineNodeType.ComponentEnd:
			case ContentLineNodeType.ComponentStart | ContentLineNodeType.Parameter:
			case ContentLineNodeType.ComponentEnd | ContentLineNodeType.Parameter:
			case ContentLineNodeType.ComponentStart | ContentLineNodeType.ComponentEnd | ContentLineNodeType.Parameter:
				break;
			case ContentLineNodeType.Parameter:
			case ContentLineNodeType.Property:
				if (this.parser.State == ContentLineParser.States.ParamName || this.nodeType == ContentLineNodeType.Parameter)
				{
					while (this.ReadNextParameter())
					{
					}
				}
				if (this.parser.State == ContentLineParser.States.Value || this.parser.State == ContentLineParser.States.ValueStart)
				{
					this.ReadPropertyValue(false);
				}
				if (this.parser.State == ContentLineParser.States.ValueStartComma || this.parser.State == ContentLineParser.States.ValueStartSemiColon)
				{
					while (this.ReadNextPropertyValue())
					{
					}
					goto IL_E3;
				}
				goto IL_E3;
			default:
				if (contentLineNodeType == ContentLineNodeType.BeforeComponentStart)
				{
					this.nodeType = ContentLineNodeType.ComponentStart;
					return true;
				}
				if (contentLineNodeType == ContentLineNodeType.BeforeComponentEnd)
				{
					this.nodeType = ContentLineNodeType.ComponentEnd;
					return true;
				}
				break;
			}
			return false;
			IL_E3:
			if (this.parser.State == ContentLineParser.States.End)
			{
				if (this.componentStack.Count != 0)
				{
					this.complianceTracker.SetComplianceStatus(ComplianceStatus.StreamTruncated | ComplianceStatus.NotAllComponentsClosed, CalendarStrings.NotAllComponentsClosed);
					this.nodeType = ContentLineNodeType.BeforeComponentEnd;
				}
				else
				{
					this.nodeType = ContentLineNodeType.DocumentEnd;
				}
				return false;
			}
			this.nodeType = ContentLineNodeType.Property;
			this.propertyName = this.ReadName();
			this.parameterValueRead = false;
			this.propertyValueRead = false;
			if (this.parser.State == ContentLineParser.States.End)
			{
				if (0 < this.propertyName.Length)
				{
					this.complianceTracker.SetComplianceStatus(ComplianceStatus.StreamTruncated | ComplianceStatus.PropertyTruncated, CalendarStrings.PropertyTruncated);
				}
				if (0 < this.componentStack.Count)
				{
					this.complianceTracker.SetComplianceStatus(ComplianceStatus.StreamTruncated | ComplianceStatus.NotAllComponentsClosed, CalendarStrings.NotAllComponentsClosed);
					this.nodeType = ContentLineNodeType.BeforeComponentEnd;
				}
				else
				{
					this.nodeType = ContentLineNodeType.DocumentEnd;
				}
				return false;
			}
			if (this.propertyName.Equals("BEGIN", StringComparison.OrdinalIgnoreCase))
			{
				if (this.parser.State == ContentLineParser.States.ParamName)
				{
					this.complianceTracker.SetComplianceStatus(ComplianceStatus.ParametersOnComponentTag, CalendarStrings.ParametersNotPermittedOnComponentTag);
					while (this.ReadNextParameter())
					{
					}
				}
				if (this.parser.State != ContentLineParser.States.End)
				{
					this.componentStack.Push(this.componentName);
					this.componentName = this.ReadPropertyValue(true).Trim();
					if (this.componentName.Length == 0)
					{
						this.complianceTracker.SetComplianceStatus(ComplianceStatus.EmptyComponentName, CalendarStrings.EmptyComponentName);
					}
					this.nodeType = ContentLineNodeType.BeforeComponentStart;
				}
			}
			else if (this.propertyName.Equals("END", StringComparison.OrdinalIgnoreCase))
			{
				if (this.parser.State == ContentLineParser.States.ParamName)
				{
					this.complianceTracker.SetComplianceStatus(ComplianceStatus.ParametersOnComponentTag, CalendarStrings.ParametersNotPermittedOnComponentTag);
					while (this.ReadNextParameter())
					{
					}
				}
				if (this.parser.State != ContentLineParser.States.End)
				{
					string text = this.ReadPropertyValue(true).Trim();
					if (this.componentStack.Count == 0)
					{
						this.complianceTracker.SetComplianceStatus(ComplianceStatus.EndTagWithoutBegin, CalendarStrings.EndTagWithoutBegin);
					}
					if (!text.Equals(this.componentName, StringComparison.OrdinalIgnoreCase))
					{
						if (text.Length == 0)
						{
							this.complianceTracker.SetComplianceStatus(ComplianceStatus.EmptyComponentName, CalendarStrings.EmptyComponentName);
						}
						this.complianceTracker.SetComplianceStatus(ComplianceStatus.ComponentNameMismatch, CalendarStrings.ComponentNameMismatch);
					}
					this.nodeType = ContentLineNodeType.BeforeComponentEnd;
				}
			}
			else if (this.propertyName.Length == 0)
			{
				if (0 < this.componentStack.Count)
				{
					this.complianceTracker.SetComplianceStatus(ComplianceStatus.EmptyPropertyName, CalendarStrings.EmptyPropertyName);
				}
			}
			else if (this.componentStack.Count == 0)
			{
				this.complianceTracker.SetComplianceStatus(ComplianceStatus.PropertyOutsideOfComponent, CalendarStrings.PropertyOutsideOfComponent);
			}
			return true;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0002CC8C File Offset: 0x0002AE8C
		public bool ReadNextProperty()
		{
			this.CheckDisposed("ReadNextProperty");
			this.DrainValueStream();
			this.parameterValueRead = false;
			this.propertyValueRead = false;
			if (this.parser.State == ContentLineParser.States.End || this.nodeType == ContentLineNodeType.BeforeComponentEnd || this.nodeType == ContentLineNodeType.BeforeComponentStart)
			{
				return false;
			}
			if (!this.Read() || this.nodeType == ContentLineNodeType.BeforeComponentEnd || this.nodeType == ContentLineNodeType.ComponentEnd || this.nodeType != ContentLineNodeType.Property)
			{
				return false;
			}
			this.parameterName = null;
			this.valueType.Reset();
			this.propertyValueSeparator = ContentLineParser.Separators.Comma;
			return true;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0002CD20 File Offset: 0x0002AF20
		public bool ReadNextParameter()
		{
			this.CheckDisposed("ReadNextParameter");
			if (this.parser.State == ContentLineParser.States.ParamValueStart || this.parser.State == ContentLineParser.States.ParamValueQuoted || this.parser.State == ContentLineParser.States.ParamValueUnquoted)
			{
				while (this.ReadNextParameterValue())
				{
				}
			}
			if (this.parser.State == ContentLineParser.States.ParamName)
			{
				this.nodeType = ContentLineNodeType.Parameter;
				this.parameterName = this.ReadName();
				this.parameterValueSeparator = ContentLineParser.Separators.Comma;
				if (this.parser.State == ContentLineParser.States.UnnamedParamEnd)
				{
					this.unnamedParameterValue = this.parameterName;
					this.parameterName = null;
					int num;
					this.parser.ParseElement(this.charBuffer, 0, 256, out num, false, ContentLineParser.Separators.None);
				}
				else if (this.parameterName.Length == 0)
				{
					this.complianceTracker.SetComplianceStatus(ComplianceStatus.EmptyParameterName, CalendarStrings.EmptyParameterName);
				}
				this.parameterValueRead = false;
				return true;
			}
			return false;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0002CE00 File Offset: 0x0002B000
		public bool ReadNextParameterValue()
		{
			this.CheckDisposed("ReadNextParameterValue");
			if (this.parameterValueSeparator == ContentLineParser.Separators.None)
			{
				throw new InvalidOperationException(CalendarStrings.InvalidReaderState);
			}
			if (!this.parameterValueRead && this.parser.State != ContentLineParser.States.ParamValueStart && this.parser.State != ContentLineParser.States.UnnamedParamEnd)
			{
				this.ReadParameterValue(false);
			}
			bool flag = false;
			if (this.parser.State == ContentLineParser.States.UnnamedParamEnd)
			{
				flag = true;
				int num;
				this.parser.ParseElement(this.charBuffer, 0, 256, out num, false, ContentLineParser.Separators.None);
			}
			if (this.parser.State == ContentLineParser.States.ParamValueStart)
			{
				int num2;
				this.parser.ParseElement(this.charBuffer, 0, 256, out num2, false, ContentLineParser.Separators.None);
			}
			flag = (flag || this.parser.State == ContentLineParser.States.ParamValueUnquoted || this.parser.State == ContentLineParser.States.ParamValueQuoted);
			if (flag)
			{
				this.parameterValueRead = false;
				return true;
			}
			this.parameterValueRead = true;
			return false;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0002CEE8 File Offset: 0x0002B0E8
		public string ReadParameterValue(bool returnValue)
		{
			this.CheckDisposed("ReadParameterValue");
			if (this.parameterValueRead)
			{
				throw new InvalidOperationException(CalendarStrings.ValueAlreadyRead);
			}
			if (this.unnamedParameterValue != null && this.parameterName == null)
			{
				string result = null;
				if (returnValue)
				{
					result = this.unnamedParameterValue;
				}
				this.unnamedParameterValue = null;
				this.parameterValueRead = true;
				return result;
			}
			if (this.parser.State == ContentLineParser.States.ParamValueStart)
			{
				this.parameterValueSeparator = ContentLineParser.Separators.None;
				int charCount;
				this.parser.ParseElement(this.charBuffer, 0, 256, out charCount, false, ContentLineParser.Separators.None);
			}
			if (this.parser.State == ContentLineParser.States.Value || this.parser.State == ContentLineParser.States.End)
			{
				this.parameterValueRead = true;
				return string.Empty;
			}
			if (this.parser.State != ContentLineParser.States.ParamValueUnquoted && this.parser.State != ContentLineParser.States.ParamValueQuoted)
			{
				throw new InvalidOperationException(CalendarStrings.InvalidReaderState);
			}
			bool flag = string.Compare(this.parameterName, "VALUE", StringComparison.OrdinalIgnoreCase) == 0;
			string text = null;
			if (returnValue || flag)
			{
				this.stringBuilder.Length = 0;
				bool flag2;
				do
				{
					int charCount;
					flag2 = this.parser.ParseElement(this.charBuffer, 0, 256, out charCount, false, this.parameterValueSeparator);
					this.stringBuilder.Append(this.charBuffer, 0, charCount);
				}
				while (flag2);
				text = this.stringBuilder.ToString();
			}
			else
			{
				int charCount;
				while (this.parser.ParseElement(this.charBuffer, 0, 256, out charCount, false, this.parameterValueSeparator))
				{
				}
			}
			if (flag && text.Length > 0)
			{
				this.valueType.SetValueTypeParameter(text);
			}
			this.parameterValueRead = true;
			return text;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0002D078 File Offset: 0x0002B278
		public bool ReadNextPropertyValue()
		{
			this.CheckDisposed("ReadNextPropertyValue");
			if (this.propertyValueSeparator == ContentLineParser.Separators.None)
			{
				throw new InvalidOperationException(CalendarStrings.InvalidReaderState);
			}
			if (this.parser.State == ContentLineParser.States.ParamName || this.nodeType == ContentLineNodeType.Parameter)
			{
				while (this.ReadNextParameter())
				{
				}
			}
			if (!this.propertyValueRead && this.parser.State != ContentLineParser.States.ValueStart)
			{
				this.ReadPropertyValue(false);
			}
			if (this.parser.State == ContentLineParser.States.ValueStart || this.parser.State == ContentLineParser.States.ValueStartComma || this.parser.State == ContentLineParser.States.ValueStartSemiColon)
			{
				int num;
				this.parser.ParseElement(this.charBuffer, 0, 256, out num, false, ContentLineParser.Separators.None);
			}
			this.DrainValueStream();
			this.parameterName = null;
			bool flag = this.parser.State == ContentLineParser.States.Value;
			this.propertyValueRead = !flag;
			return flag;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0002D150 File Offset: 0x0002B350
		private string ReadPropertyValue(bool returnValue)
		{
			ContentLineParser.Separators separators;
			return this.ReadPropertyValue(returnValue, ContentLineParser.Separators.None, true, out separators);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0002D168 File Offset: 0x0002B368
		public string ReadPropertyValue(bool returnValue, ContentLineParser.Separators expectedSeparators, bool useDefaultSeparator, out ContentLineParser.Separators endSeparator)
		{
			this.CheckDisposed("ReadPropertyValue");
			if (this.propertyValueRead)
			{
				throw new InvalidOperationException(CalendarStrings.ValueAlreadyRead);
			}
			endSeparator = ContentLineParser.Separators.None;
			if (this.parser.State == ContentLineParser.States.ParamName || this.nodeType == ContentLineNodeType.Parameter)
			{
				while (this.ReadNextParameter())
				{
				}
			}
			if (!useDefaultSeparator)
			{
				this.propertyValueSeparator = expectedSeparators;
			}
			if (this.parser.State == ContentLineParser.States.ValueStart)
			{
				if (useDefaultSeparator)
				{
					this.propertyValueSeparator = ContentLineParser.Separators.None;
				}
				int charCount;
				this.parser.ParseElement(this.charBuffer, 0, 256, out charCount, false, ContentLineParser.Separators.None);
			}
			this.DrainValueStream();
			if (this.parser.State == ContentLineParser.States.End)
			{
				this.propertyValueRead = true;
				return string.Empty;
			}
			ContentLineParser.Separators separators = this.propertyValueSeparator;
			if (!this.ValueType.CanBeMultivalued)
			{
				separators &= ~ContentLineParser.Separators.Comma;
			}
			if (!this.ValueType.CanBeCompound)
			{
				separators &= ~ContentLineParser.Separators.SemiColon;
			}
			string result = null;
			if (returnValue)
			{
				this.stringBuilder.Length = 0;
				bool flag;
				do
				{
					int charCount;
					flag = this.parser.ParseElement(this.charBuffer, 0, 256, out charCount, this.ValueType.IsTextType, separators);
					this.stringBuilder.Append(this.charBuffer, 0, charCount);
				}
				while (flag);
				result = this.stringBuilder.ToString();
			}
			else
			{
				int charCount;
				while (this.parser.ParseElement(this.charBuffer, 0, 256, out charCount, this.ValueType.IsTextType, separators))
				{
				}
			}
			this.propertyValueRead = true;
			this.parameterName = null;
			if (this.parser.State == ContentLineParser.States.ValueStartComma)
			{
				endSeparator = ContentLineParser.Separators.Comma;
			}
			else if (this.parser.State == ContentLineParser.States.ValueStartSemiColon)
			{
				endSeparator = ContentLineParser.Separators.SemiColon;
			}
			return result;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0002D2FA File Offset: 0x0002B4FA
		public void ApplyValueOverrides(Encoding charset, ByteEncoder decoder)
		{
			this.parser.ApplyValueOverrides(charset, decoder);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0002D30C File Offset: 0x0002B50C
		public Stream GetValueReadStream()
		{
			this.CheckDisposed("GetValueReadStream");
			if (this.propertyValueRead)
			{
				throw new InvalidOperationException(CalendarStrings.ValueAlreadyRead);
			}
			if (this.parser.State == ContentLineParser.States.ParamName || this.nodeType == ContentLineNodeType.Parameter)
			{
				while (this.ReadNextParameter())
				{
				}
			}
			if (this.parser.State == ContentLineParser.States.ValueStart)
			{
				int num;
				this.parser.ParseElement(this.charBuffer, 0, 256, out num, false, ContentLineParser.Separators.None);
			}
			this.DrainValueStream();
			this.propertyValueRead = true;
			if (this.parser.State != ContentLineParser.States.Value)
			{
				return new MemoryStream(new byte[0], false);
			}
			this.valueStream = (this.ValueType.IsTextType ? new ContentLineReader.ValueStream(this.parser) : this.parser.GetValueReadStream());
			return this.valueStream;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0002D3DB File Offset: 0x0002B5DB
		public void AssertValidState(ContentLineNodeType nodeType)
		{
			this.CheckDisposed("AssertValidState");
			if ((this.nodeType & nodeType) == ContentLineNodeType.DocumentStart)
			{
				throw new InvalidOperationException(CalendarStrings.InvalidReaderState);
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0002D3FD File Offset: 0x0002B5FD
		private void DrainValueStream()
		{
			if (this.valueStream != null)
			{
				this.valueStream.Dispose();
				this.valueStream = null;
			}
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0002D41C File Offset: 0x0002B61C
		private string ReadName()
		{
			this.stringBuilder.Length = 0;
			bool flag;
			do
			{
				int val;
				flag = this.parser.ParseElement(this.charBuffer, 0, 256, out val, false, ContentLineParser.Separators.None);
				if (this.stringBuilder.Length < 255)
				{
					int charCount = Math.Min(val, 255 - this.stringBuilder.Length);
					this.stringBuilder.Append(this.charBuffer, 0, charCount);
				}
			}
			while (flag);
			return this.stringBuilder.ToString().Trim();
		}

		// Token: 0x040006DB RID: 1755
		private const int CharBufferSize = 256;

		// Token: 0x040006DC RID: 1756
		private const int MaxNameLength = 255;

		// Token: 0x040006DD RID: 1757
		private ContentLineParser parser;

		// Token: 0x040006DE RID: 1758
		private ComplianceTracker complianceTracker;

		// Token: 0x040006DF RID: 1759
		private ContentLineNodeType nodeType;

		// Token: 0x040006E0 RID: 1760
		private string parameterName;

		// Token: 0x040006E1 RID: 1761
		private string unnamedParameterValue;

		// Token: 0x040006E2 RID: 1762
		private string propertyName;

		// Token: 0x040006E3 RID: 1763
		private string componentName;

		// Token: 0x040006E4 RID: 1764
		private Stack<string> componentStack = new Stack<string>();

		// Token: 0x040006E5 RID: 1765
		private Stream valueStream;

		// Token: 0x040006E6 RID: 1766
		private char[] charBuffer = new char[256];

		// Token: 0x040006E7 RID: 1767
		private ValueTypeContainer valueType;

		// Token: 0x040006E8 RID: 1768
		private ContentLineParser.Separators propertyValueSeparator;

		// Token: 0x040006E9 RID: 1769
		private ContentLineParser.Separators parameterValueSeparator;

		// Token: 0x040006EA RID: 1770
		private StringBuilder stringBuilder = new StringBuilder();

		// Token: 0x040006EB RID: 1771
		private bool parameterValueRead;

		// Token: 0x040006EC RID: 1772
		private bool propertyValueRead;

		// Token: 0x040006ED RID: 1773
		private bool isDisposed;

		// Token: 0x020000CE RID: 206
		private class ValueStream : Stream
		{
			// Token: 0x0600081D RID: 2077 RVA: 0x0002D4A4 File Offset: 0x0002B6A4
			public ValueStream(ContentLineParser reader)
			{
				this.parser = reader;
				this.encoder = reader.CurrentCharsetEncoding.GetEncoder();
				this.buffer = new byte[reader.CurrentCharsetEncoding.GetMaxByteCount(this.charBuffer.Length)];
			}

			// Token: 0x17000266 RID: 614
			// (get) Token: 0x0600081E RID: 2078 RVA: 0x0002D4FD File Offset: 0x0002B6FD
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000267 RID: 615
			// (get) Token: 0x0600081F RID: 2079 RVA: 0x0002D500 File Offset: 0x0002B700
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000268 RID: 616
			// (get) Token: 0x06000820 RID: 2080 RVA: 0x0002D503 File Offset: 0x0002B703
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000269 RID: 617
			// (get) Token: 0x06000821 RID: 2081 RVA: 0x0002D506 File Offset: 0x0002B706
			public override long Length
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x1700026A RID: 618
			// (get) Token: 0x06000822 RID: 2082 RVA: 0x0002D50D File Offset: 0x0002B70D
			// (set) Token: 0x06000823 RID: 2083 RVA: 0x0002D521 File Offset: 0x0002B721
			public override long Position
			{
				get
				{
					this.CheckDisposed("Position::get");
					return (long)this.position;
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x06000824 RID: 2084 RVA: 0x0002D528 File Offset: 0x0002B728
			private void CheckDisposed(string methodName)
			{
				if (this.isClosed)
				{
					throw new ObjectDisposedException("ValueStream", methodName);
				}
			}

			// Token: 0x06000825 RID: 2085 RVA: 0x0002D540 File Offset: 0x0002B740
			protected override void Dispose(bool disposing)
			{
				if (disposing && !this.isClosed)
				{
					byte[] array = new byte[1024];
					while (this.Read(array, 0, array.Length) > 0)
					{
					}
				}
				this.isClosed = true;
			}

			// Token: 0x06000826 RID: 2086 RVA: 0x0002D578 File Offset: 0x0002B778
			public override void Flush()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000827 RID: 2087 RVA: 0x0002D580 File Offset: 0x0002B780
			public override int Read(byte[] buffer, int offset, int count)
			{
				this.CheckDisposed("Read");
				int num = 0;
				if (this.eof)
				{
					return 0;
				}
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (offset < 0 || offset > buffer.Length)
				{
					throw new ArgumentOutOfRangeException("offset", CalendarStrings.OffsetOutOfRange);
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count", CalendarStrings.CountLessThanZero);
				}
				if (this.bufferSize != 0)
				{
					int num2 = Math.Min(count, this.bufferSize);
					Buffer.BlockCopy(this.buffer, this.bufferOffset, buffer, offset, num2);
					count -= num2;
					offset += num2;
					this.bufferSize -= num2;
					this.bufferOffset += num2;
					num += num2;
				}
				while (count > 0)
				{
					if (this.bufferSize == 0 && !this.eof)
					{
						this.ReadBuffer();
					}
					if (this.bufferSize == 0)
					{
						break;
					}
					int num2 = Math.Min(count, this.bufferSize);
					Buffer.BlockCopy(this.buffer, this.bufferOffset, buffer, offset, num2);
					this.bufferOffset += num2;
					this.bufferSize -= num2;
					offset += num2;
					count -= num2;
					num += num2;
				}
				this.position += num;
				return num;
			}

			// Token: 0x06000828 RID: 2088 RVA: 0x0002D6B4 File Offset: 0x0002B8B4
			public override int ReadByte()
			{
				this.CheckDisposed("Read");
				if (this.eof)
				{
					return -1;
				}
				int result;
				if (this.bufferSize != 0)
				{
					result = (int)this.buffer[this.bufferOffset++];
					this.bufferSize--;
					this.position++;
				}
				else
				{
					if (this.bufferSize == 0 && !this.eof)
					{
						this.ReadBuffer();
					}
					if (this.bufferSize == 0)
					{
						return -1;
					}
					result = (int)this.buffer[this.bufferOffset++];
					this.bufferSize--;
					this.position++;
				}
				return result;
			}

			// Token: 0x06000829 RID: 2089 RVA: 0x0002D76B File Offset: 0x0002B96B
			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600082A RID: 2090 RVA: 0x0002D772 File Offset: 0x0002B972
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600082B RID: 2091 RVA: 0x0002D779 File Offset: 0x0002B979
			public override void WriteByte(byte value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600082C RID: 2092 RVA: 0x0002D780 File Offset: 0x0002B980
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600082D RID: 2093 RVA: 0x0002D788 File Offset: 0x0002B988
			private void ReadBuffer()
			{
				this.bufferOffset = 0;
				this.bufferSize = 0;
				while (!this.eof && this.bufferSize == 0)
				{
					int charCount;
					this.eof = !this.parser.ParseElement(this.charBuffer, 0, 256, out charCount, true, ContentLineParser.Separators.None);
					this.bufferSize = this.encoder.GetBytes(this.charBuffer, 0, charCount, this.buffer, 0, this.eof);
				}
			}

			// Token: 0x040006EE RID: 1774
			private ContentLineParser parser;

			// Token: 0x040006EF RID: 1775
			private bool eof;

			// Token: 0x040006F0 RID: 1776
			private bool isClosed;

			// Token: 0x040006F1 RID: 1777
			private int position;

			// Token: 0x040006F2 RID: 1778
			private byte[] buffer;

			// Token: 0x040006F3 RID: 1779
			private int bufferOffset;

			// Token: 0x040006F4 RID: 1780
			private int bufferSize;

			// Token: 0x040006F5 RID: 1781
			private Encoder encoder;

			// Token: 0x040006F6 RID: 1782
			private char[] charBuffer = new char[256];
		}
	}
}
