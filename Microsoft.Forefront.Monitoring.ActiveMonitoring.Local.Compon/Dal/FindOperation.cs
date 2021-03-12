using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Reflection;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Hygiene.Data;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x02000065 RID: 101
	public class FindOperation : ConfigDataProviderOperation
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600029F RID: 671 RVA: 0x000105C6 File Offset: 0x0000E7C6
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x000105CE File Offset: 0x0000E7CE
		[XmlAttribute]
		public string QueryString { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x000105D7 File Offset: 0x0000E7D7
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x000105DF File Offset: 0x0000E7DF
		[XmlAttribute]
		public string RootId { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x000105E8 File Offset: 0x0000E7E8
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x000105F0 File Offset: 0x0000E7F0
		[XmlArrayItem("Assert")]
		public string[] Asserts { get; set; }

		// Token: 0x060002A5 RID: 677 RVA: 0x000105FC File Offset: 0x0000E7FC
		protected override void ExecuteConfigDataProviderOperation(IConfigDataProvider configDataProvider, IDictionary<string, object> variables)
		{
			Type type = DalProbeOperation.ResolveDataType(base.DataType);
			IConfigurable iConfigurable = (IConfigurable)Activator.CreateInstance(type);
			QueryFilter queryFilter = null;
			if (!string.IsNullOrEmpty(this.QueryString))
			{
				string text = this.QueryString.Trim();
				if (!string.IsNullOrEmpty(text))
				{
					ObjectSchema objectSchema = this.GetObjectSchema(iConfigurable);
					queryFilter = this.CreateQueryFilter(text, objectSchema);
				}
			}
			MethodInfo methodInfo = FindOperation.findMethod.MakeGenericMethod(new Type[]
			{
				type
			});
			MethodBase methodBase = methodInfo;
			object[] array = new object[4];
			array[0] = queryFilter;
			array[2] = false;
			IConfigurable[] value = (IConfigurable[])methodBase.Invoke(configDataProvider, array);
			variables[base.Return] = value;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000106A8 File Offset: 0x0000E8A8
		private QueryFilter CreateQueryFilter(string queryString, ObjectSchema objectSchema)
		{
			QueryParser queryParser = new QueryParser(queryString, objectSchema, QueryParser.Capabilities.All, new QueryParser.EvaluateVariableDelegate(this.EvalDelegate), new QueryParser.ConvertValueFromStringDelegate(this.ConvertDelegate));
			return queryParser.ParseTree;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000106E4 File Offset: 0x0000E8E4
		private ObjectSchema GetObjectSchema(IConfigurable iConfigurable)
		{
			ConfigurablePropertyBag configurablePropertyBag = iConfigurable as ConfigurablePropertyBag;
			if (configurablePropertyBag == null)
			{
				ConfigurableObject configurableObject = iConfigurable as ConfigurableObject;
				if (configurableObject == null)
				{
					return null;
				}
				return configurableObject.ObjectSchema;
			}
			else
			{
				Type schemaType = configurablePropertyBag.GetSchemaType();
				if (schemaType == null)
				{
					return null;
				}
				return (ObjectSchema)Activator.CreateInstance(schemaType);
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0001072C File Offset: 0x0000E92C
		private object ConvertDelegate(object valueToConvert, Type resultType)
		{
			string text = valueToConvert as string;
			if (resultType == typeof(ADObjectId) && !string.IsNullOrEmpty(text) && !ADObjectId.IsValidDistinguishedName(text))
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
			return LanguagePrimitives.ConvertTo(text, resultType);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00010794 File Offset: 0x0000E994
		private object EvalDelegate(string varName)
		{
			return null;
		}

		// Token: 0x04000190 RID: 400
		private static MethodInfo findMethod = typeof(IConfigDataProvider).GetMethod("Find");
	}
}
