using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x0200012D RID: 301
	internal class PropertyValue
	{
		// Token: 0x06000C8F RID: 3215 RVA: 0x00038794 File Offset: 0x00036994
		internal static PropertyValue Create(string rawStringValue, GlsProperty propertyDefinition)
		{
			string text = null;
			int num = -1;
			bool flag = false;
			ValidationError validationError = null;
			if (propertyDefinition.DataType == typeof(string))
			{
				if (string.IsNullOrWhiteSpace(rawStringValue))
				{
					text = (propertyDefinition.DefaultValue as string);
				}
				else
				{
					text = rawStringValue;
				}
			}
			else if (propertyDefinition.DataType == typeof(int))
			{
				if (string.IsNullOrWhiteSpace(rawStringValue))
				{
					num = (int)propertyDefinition.DefaultValue;
				}
				else if (!int.TryParse(rawStringValue, out num))
				{
					string text2 = string.Format("ArgumentException: Unsupported {0} property data format:{1}", "int", rawStringValue);
					ExTraceGlobals.GLSTracer.TraceError(0L, text2);
					validationError = new GlsPropertyValidationError(new LocalizedString(text2), propertyDefinition, rawStringValue);
					num = (int)propertyDefinition.DefaultValue;
				}
			}
			else
			{
				if (!(propertyDefinition.DataType == typeof(bool)))
				{
					throw new ArgumentException(string.Format("unsupported PropertyDataType:{0}", propertyDefinition.DataType), "propertyDefinition.DataType");
				}
				if (string.IsNullOrWhiteSpace(rawStringValue))
				{
					flag = (bool)propertyDefinition.DefaultValue;
				}
				else if (!bool.TryParse(rawStringValue, out flag))
				{
					int num2;
					if (int.TryParse(rawStringValue, out num2))
					{
						flag = (num2 != 0);
					}
					else
					{
						string text3 = string.Format("ArgumentException: Unsupported {0} property data format:{1}", "boolean", rawStringValue);
						ExTraceGlobals.GLSTracer.TraceError(0L, text3);
						validationError = new GlsPropertyValidationError(new LocalizedString(text3), propertyDefinition, rawStringValue);
						flag = (bool)propertyDefinition.DefaultValue;
					}
				}
			}
			return new PropertyValue(propertyDefinition.DataType, text, num, flag, validationError);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00038913 File Offset: 0x00036B13
		internal PropertyValue(string stringValue)
		{
			this.dataType = typeof(string);
			this.stringValue = stringValue;
			this.intValue = -1;
			this.boolValue = false;
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00038940 File Offset: 0x00036B40
		internal PropertyValue(int intValue)
		{
			this.dataType = typeof(int);
			this.stringValue = null;
			this.intValue = intValue;
			this.boolValue = false;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0003896D File Offset: 0x00036B6D
		internal PropertyValue(bool boolValue)
		{
			this.dataType = typeof(bool);
			this.stringValue = null;
			this.intValue = -1;
			this.boolValue = boolValue;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0003899A File Offset: 0x00036B9A
		private PropertyValue(Type dataType, string stringValue, int intValue, bool boolValue, ValidationError validationError)
		{
			this.dataType = dataType;
			this.stringValue = stringValue;
			this.intValue = intValue;
			this.boolValue = boolValue;
			this.validationError = validationError;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x000389C7 File Offset: 0x00036BC7
		internal Type DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x000389CF File Offset: 0x00036BCF
		internal string GetStringValue()
		{
			if (this.dataType != typeof(string))
			{
				throw new InvalidOperationException("property is not of dataType string");
			}
			return this.stringValue;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x000389F9 File Offset: 0x00036BF9
		internal int GetIntValue()
		{
			if (this.dataType != typeof(int))
			{
				throw new InvalidOperationException("property is not of dataType int");
			}
			return this.intValue;
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00038A23 File Offset: 0x00036C23
		internal bool GetBoolValue()
		{
			if (this.dataType != typeof(bool))
			{
				throw new InvalidOperationException("property is not of dataType bool");
			}
			return this.boolValue;
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x00038A4D File Offset: 0x00036C4D
		internal bool IsValid
		{
			get
			{
				return this.validationError == null;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x00038A58 File Offset: 0x00036C58
		internal ValidationError GetValidationError
		{
			get
			{
				return this.validationError;
			}
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00038A60 File Offset: 0x00036C60
		public override string ToString()
		{
			string result;
			if (this.dataType == typeof(string))
			{
				result = this.stringValue;
			}
			else if (this.dataType == typeof(int))
			{
				result = Convert.ToString(this.intValue);
			}
			else
			{
				if (!(this.dataType == typeof(bool)))
				{
					throw new InvalidOperationException(string.Format("unsupported PropertyDataType:{0}", this.dataType));
				}
				result = Convert.ToString(this.boolValue);
			}
			return result;
		}

		// Token: 0x0400067D RID: 1661
		private const string validationErrorFormat = "ArgumentException: Unsupported {0} property data format:{1}";

		// Token: 0x0400067E RID: 1662
		private readonly Type dataType;

		// Token: 0x0400067F RID: 1663
		private readonly string stringValue;

		// Token: 0x04000680 RID: 1664
		private readonly int intValue;

		// Token: 0x04000681 RID: 1665
		private readonly bool boolValue;

		// Token: 0x04000682 RID: 1666
		private readonly ValidationError validationError;
	}
}
