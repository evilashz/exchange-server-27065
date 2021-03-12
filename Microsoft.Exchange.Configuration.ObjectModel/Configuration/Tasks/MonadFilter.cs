using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200006E RID: 110
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MonadFilter
	{
		// Token: 0x0600045F RID: 1119 RVA: 0x0000FA73 File Offset: 0x0000DC73
		public MonadFilter(string filterText, PSCmdlet cmdlet, ObjectSchema schema) : this(filterText, cmdlet, schema, QueryParser.Capabilities.All)
		{
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000FA84 File Offset: 0x0000DC84
		public MonadFilter(string filterText, PSCmdlet cmdlet, ObjectSchema schema, QueryParser.Capabilities capabilities)
		{
			QueryParser.EvaluateVariableDelegate evalDelegate = null;
			QueryParser.ConvertValueFromStringDelegate convertDelegate = new QueryParser.ConvertValueFromStringDelegate(MonadFilter.ConvertValueFromString);
			if (cmdlet != null)
			{
				evalDelegate = new QueryParser.EvaluateVariableDelegate(cmdlet.GetVariableValue);
			}
			QueryParser queryParser = new QueryParser(filterText, schema, capabilities, evalDelegate, convertDelegate);
			this.innerFilter = queryParser.ParseTree;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		public static object ConvertValueFromString(object valueToConvert, Type resultType)
		{
			string text = valueToConvert as string;
			if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				bool flag = text == null || "null".Equals(text, StringComparison.OrdinalIgnoreCase) || "$null".Equals(text, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					return null;
				}
			}
			if (resultType.Equals(typeof(ADObjectId)) && !string.IsNullOrEmpty(text) && !ADObjectId.IsValidDistinguishedName(text))
			{
				try
				{
					text = NativeHelpers.DistinguishedNameFromCanonicalName(text);
				}
				catch (NameConversionException)
				{
					throw new FormatException(DirectoryStrings.InvalidDNFormat(text));
				}
			}
			if (!resultType.Equals(typeof(bool)) && !resultType.Equals(typeof(bool?)))
			{
				return LanguagePrimitives.ConvertTo(text, resultType);
			}
			if (text == null)
			{
				return false;
			}
			return bool.Parse(text);
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000FBB8 File Offset: 0x0000DDB8
		public QueryFilter InnerFilter
		{
			get
			{
				return this.innerFilter;
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000FBC0 File Offset: 0x0000DDC0
		public override string ToString()
		{
			if (this.innerFilter == null)
			{
				return string.Empty;
			}
			return this.innerFilter.GenerateInfixString(FilterLanguage.Monad);
		}

		// Token: 0x04000110 RID: 272
		private QueryFilter innerFilter;
	}
}
