using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002AA RID: 682
	[XmlRoot(ElementName = "PiiDataRedaction")]
	public class SuppressingPiiConfig
	{
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x0004DAAD File Offset: 0x0004BCAD
		// (set) Token: 0x0600188A RID: 6282 RVA: 0x0004DAB5 File Offset: 0x0004BCB5
		[XmlAttribute(AttributeName = "enable")]
		public bool Enable { get; set; }

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x0004DABE File Offset: 0x0004BCBE
		// (set) Token: 0x0600188C RID: 6284 RVA: 0x0004DAC6 File Offset: 0x0004BCC6
		[XmlAttribute(AttributeName = "redactorClassName")]
		public string RedactorClassName { get; set; }

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x0004DACF File Offset: 0x0004BCCF
		// (set) Token: 0x0600188E RID: 6286 RVA: 0x0004DAD7 File Offset: 0x0004BCD7
		[XmlArrayItem(ElementName = "Schema")]
		[XmlArray(ElementName = "PiiSchemas")]
		public SchemaPiiPropertyDefinition[] SchemaPiiDefinitions
		{
			get
			{
				return this.schemaPiiDefinitions;
			}
			set
			{
				this.schemaPiiDefinitions = value;
				this.redactorDict = this.CreateRedactorDictionary(this.schemaPiiDefinitions);
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x0004DAF2 File Offset: 0x0004BCF2
		// (set) Token: 0x06001890 RID: 6288 RVA: 0x0004DAFA File Offset: 0x0004BCFA
		[XmlArray(ElementName = "PiiStrings")]
		[XmlArrayItem(ElementName = "Resource")]
		public PiiResource[] PiiResourceDefinitions
		{
			get
			{
				return this.piiResourceDefinitions;
			}
			set
			{
				this.piiResourceDefinitions = value;
				this.piiStringDict = this.CreatePiiStringDictionary(this.piiResourceDefinitions);
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x0004DB15 File Offset: 0x0004BD15
		// (set) Token: 0x06001892 RID: 6290 RVA: 0x0004DB1D File Offset: 0x0004BD1D
		[XmlIgnore]
		public HashSet<Type> ExceptionSchemaTypes { get; private set; }

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0004DB26 File Offset: 0x0004BD26
		// (set) Token: 0x06001894 RID: 6292 RVA: 0x0004DB2E File Offset: 0x0004BD2E
		[XmlIgnore]
		public HashSet<PropertyDefinition> PropertiesNeedInPiiMap { get; private set; }

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x0004DB37 File Offset: 0x0004BD37
		// (set) Token: 0x06001896 RID: 6294 RVA: 0x0004DB48 File Offset: 0x0004BD48
		[XmlArray(ElementName = "ExceptionSchemas")]
		[XmlArrayItem(ElementName = "Schema")]
		public SchemaPiiPropertyDefinition[] ExceptionSchemas
		{
			get
			{
				return this.exceptionSchemas;
			}
			set
			{
				this.exceptionSchemas = value;
				this.ExceptionSchemaTypes = new HashSet<Type>(this.ResolveTypeNames(from x in this.exceptionSchemas
				select x.Name));
				this.ExceptionSchemaTypes.ExceptWith(this.definedSchemaHasPii);
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x0004DBA6 File Offset: 0x0004BDA6
		internal string DeserializationError
		{
			get
			{
				if (this.deserializationError.Length != 0)
				{
					return this.deserializationError.ToString();
				}
				return null;
			}
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0004DBC2 File Offset: 0x0004BDC2
		public bool TryGetRedactor(PropertyDefinition property, out MethodInfo redactor)
		{
			return this.redactorDict.TryGetValue(property, out redactor);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0004DBD4 File Offset: 0x0004BDD4
		public int[] GetPiiStringParams(string fullName)
		{
			int[] result;
			this.piiStringDict.TryGetValue(fullName, out result);
			return result;
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0004DBF1 File Offset: 0x0004BDF1
		public bool NeedAddIntoPiiMap(PropertyDefinition property, object original)
		{
			return original is ObjectId || original is ProxyAddress || original is SmtpAddress || this.PropertiesNeedInPiiMap.Contains(property) || (original is string && SmtpAddress.IsValidSmtpAddress((string)original));
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0004DC30 File Offset: 0x0004BE30
		private Dictionary<PropertyDefinition, MethodInfo> CreateRedactorDictionary(SchemaPiiPropertyDefinition[] piiDefinitions)
		{
			Dictionary<PropertyDefinition, MethodInfo> dictionary = new Dictionary<PropertyDefinition, MethodInfo>();
			this.PropertiesNeedInPiiMap = new HashSet<PropertyDefinition>();
			this.InitLoadedExchangeTypeDict();
			Type type = this.ResolveTypeName(this.RedactorClassName);
			if (type == null)
			{
				this.deserializationError.AppendLine(string.Format("Failed to resolve redactor class name: {0}. PII redaction feature will be disabled.", this.RedactorClassName));
				return dictionary;
			}
			List<Type> list = new List<Type>();
			foreach (SchemaPiiPropertyDefinition schemaPiiPropertyDefinition in piiDefinitions)
			{
				Type type2 = this.ResolveTypeName(schemaPiiPropertyDefinition.Name);
				if (type2 == null)
				{
					this.deserializationError.AppendLine(string.Format("Failed to resolve schema name: {0}. PII redaction on this schema will be skipped.", schemaPiiPropertyDefinition.Name));
				}
				else
				{
					list.Add(type2);
					foreach (PiiPropertyDefinition piiPropertyDefinition in schemaPiiPropertyDefinition.PiiProperties)
					{
						FieldInfo field = type2.GetTypeInfo().GetField(piiPropertyDefinition.Name);
						if (field == null)
						{
							this.deserializationError.AppendLine(string.Format("Cannot resolve property {0} in schema {1}, PII redaction on this property will be skipped.", piiPropertyDefinition.Name, schemaPiiPropertyDefinition.Name));
						}
						else
						{
							PropertyDefinition propertyDefinition = field.GetValue(null) as PropertyDefinition;
							if (propertyDefinition == null)
							{
								this.deserializationError.AppendLine(string.Format("Property {0} in schema {1} isn't a PropertyDefinition type.", piiPropertyDefinition.Name, schemaPiiPropertyDefinition.Name));
							}
							else
							{
								MethodInfo methodInfo = null;
								string text = piiPropertyDefinition.Redactor;
								if (piiPropertyDefinition.Enumerable)
								{
									text = "RedactWithoutHashing";
								}
								else if (string.IsNullOrEmpty(text))
								{
									text = "Redact";
								}
								try
								{
									if (propertyDefinition.Type.IsArray)
									{
										methodInfo = type.GetMethod(text, new Type[]
										{
											propertyDefinition.Type,
											typeof(string[]).MakeByRefType(),
											typeof(string[]).MakeByRefType()
										});
									}
									else
									{
										methodInfo = type.GetMethod(text, new Type[]
										{
											propertyDefinition.Type,
											typeof(string).MakeByRefType(),
											typeof(string).MakeByRefType()
										});
									}
								}
								catch (Exception ex)
								{
									this.deserializationError.AppendLine(string.Format("Specified redactor {0} could not be found. Reason: {1}", text, ex.ToString()));
								}
								if (methodInfo == null)
								{
									this.deserializationError.AppendLine(string.Format("Failed to find a redactor for PII property {0} in schema {1}. PII redaction on this property will be skipped.", piiPropertyDefinition.Name, schemaPiiPropertyDefinition.Name));
								}
								else
								{
									dictionary[propertyDefinition] = methodInfo;
								}
								if (piiPropertyDefinition.AddIntoMap)
								{
									this.PropertiesNeedInPiiMap.Add(propertyDefinition);
								}
							}
						}
					}
				}
			}
			this.definedSchemaHasPii = list.ToArray();
			return dictionary;
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0004DEF8 File Offset: 0x0004C0F8
		private Dictionary<string, int[]> CreatePiiStringDictionary(PiiResource[] piiResources)
		{
			Dictionary<string, int[]> dictionary = new Dictionary<string, int[]>();
			if (piiResources != null)
			{
				foreach (PiiResource piiResource in piiResources)
				{
					foreach (PiiLocString piiLocString in piiResource.LocStrings)
					{
						if (string.IsNullOrWhiteSpace(piiLocString.Id))
						{
							this.deserializationError.AppendLine("String id is required.");
						}
						else
						{
							string key = piiResource.Name + piiLocString.Id;
							dictionary[key] = piiLocString.Parameters;
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x0004E24C File Offset: 0x0004C44C
		private IEnumerable<Type> ResolveTypeNames(IEnumerable<string> names)
		{
			this.InitLoadedExchangeTypeDict();
			using (IEnumerator<string> enumerator = names.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string name = enumerator.Current;
					foreach (Type matchedType in from x in this.loadedExchangeTypeDict
					where Regex.IsMatch(x.Key, name)
					select x.Value)
					{
						yield return matchedType;
					}
				}
			}
			yield break;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0004E270 File Offset: 0x0004C470
		private Type ResolveTypeName(string name)
		{
			Type result;
			if (!this.loadedExchangeTypeDict.TryGetValue(name, out result))
			{
				this.deserializationError.AppendLine(string.Format("Cannot resolve type {0}, please check the spell of the type name.", name));
			}
			return result;
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0004E2C8 File Offset: 0x0004C4C8
		private void InitLoadedExchangeTypeDict()
		{
			if (this.loadedExchangeTypeDict == null)
			{
				this.loadedExchangeTypeDict = new Dictionary<string, Type>();
				foreach (Type[] array in from x in AppDomain.CurrentDomain.GetAssemblies()
				where x.FullName.StartsWith("Microsoft.Exchange.")
				select SerializationTypeBinder.GetLoadedTypes(x) into x
				where x != null
				select x)
				{
					foreach (Type type in array)
					{
						this.loadedExchangeTypeDict[type.FullName] = type;
					}
				}
			}
		}

		// Token: 0x04000E86 RID: 3718
		public const string RedactedDataMark = "REDACTED";

		// Token: 0x04000E87 RID: 3719
		private StringBuilder deserializationError = new StringBuilder();

		// Token: 0x04000E88 RID: 3720
		private Dictionary<PropertyDefinition, MethodInfo> redactorDict;

		// Token: 0x04000E89 RID: 3721
		private Dictionary<string, int[]> piiStringDict;

		// Token: 0x04000E8A RID: 3722
		private SchemaPiiPropertyDefinition[] schemaPiiDefinitions;

		// Token: 0x04000E8B RID: 3723
		private PiiResource[] piiResourceDefinitions;

		// Token: 0x04000E8C RID: 3724
		private SchemaPiiPropertyDefinition[] exceptionSchemas;

		// Token: 0x04000E8D RID: 3725
		private Dictionary<string, Type> loadedExchangeTypeDict;

		// Token: 0x04000E8E RID: 3726
		private Type[] definedSchemaHasPii;

		// Token: 0x04000E8F RID: 3727
		public static readonly string RedactedDataPrefix = "REDACTED" + '-';
	}
}
