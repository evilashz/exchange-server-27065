using System;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Diagnostics.Components.Tasks;

namespace Microsoft.Exchange.Configuration.PswsProxy
{
	// Token: 0x020000CF RID: 207
	internal class PSObjectSerializer
	{
		// Token: 0x060007AC RID: 1964 RVA: 0x0001C9A4 File Offset: 0x0001ABA4
		static PSObjectSerializer()
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				if (assembly.GetName().Name == "System.Management.Automation")
				{
					PSObjectSerializer.mgrAutomationAssembly = assembly;
				}
			}
			if (PSObjectSerializer.mgrAutomationAssembly == null)
			{
				throw new PswsProxySerializationException(Strings.PswsManagementAutomationAssemblyLoadError);
			}
			PSObjectSerializer.serializerType = PSObjectSerializer.mgrAutomationAssembly.GetType("System.Management.Automation.Serializer", true, false);
			PSObjectSerializer.serializerSerializeMethod = PSObjectSerializer.serializerType.GetMethod("Serialize", BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
			{
				typeof(object)
			}, null);
			PSObjectSerializer.serializerTypeTableProperty = PSObjectSerializer.serializerType.GetProperty("TypeTable", BindingFlags.Instance | BindingFlags.NonPublic);
			PSObjectSerializer.serializerDoneMethod = PSObjectSerializer.serializerType.GetMethod("Done", BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[0], null);
			PSObjectSerializer.deserializerType = PSObjectSerializer.mgrAutomationAssembly.GetType("System.Management.Automation.Deserializer", true, false);
			PSObjectSerializer.deserializerDeserializeMethod = PSObjectSerializer.deserializerType.GetMethod("Deserialize", BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[0], null);
			PSObjectSerializer.deserializerTypeTableProperty = PSObjectSerializer.deserializerType.GetProperty("TypeTable", BindingFlags.Instance | BindingFlags.NonPublic);
			PSObjectSerializer.deserializerDoneMethod = PSObjectSerializer.deserializerType.GetMethod("Done", BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[0], null);
			object obj = Enum.Parse(PSObjectSerializer.mgrAutomationAssembly.GetType("System.Management.Automation.SerializationOptions", true, false), "RemotingOptions");
			Assembly assembly2 = PSObjectSerializer.mgrAutomationAssembly;
			string typeName = "System.Management.Automation.SerializationContext";
			bool ignoreCase = true;
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance;
			Binder binder = null;
			object[] array2 = new object[3];
			array2[0] = 1;
			array2[1] = obj;
			PSObjectSerializer.serializationContext = assembly2.CreateInstance(typeName, ignoreCase, bindingAttr, binder, array2, null, null);
			object obj2 = Enum.Parse(PSObjectSerializer.mgrAutomationAssembly.GetType("System.Management.Automation.DeserializationOptions", true, false), "RemotingOptions");
			Assembly assembly3 = PSObjectSerializer.mgrAutomationAssembly;
			string typeName2 = "System.Management.Automation.DeserializationContext";
			bool ignoreCase2 = true;
			BindingFlags bindingAttr2 = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance;
			Binder binder2 = null;
			object[] array3 = new object[2];
			array3[0] = obj2;
			PSObjectSerializer.deserializationContext = assembly3.CreateInstance(typeName2, ignoreCase2, bindingAttr2, binder2, array3, null, null);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001CB89 File Offset: 0x0001AD89
		public PSObjectSerializer() : this(null)
		{
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001CB92 File Offset: 0x0001AD92
		public PSObjectSerializer(TypeTable typeTable)
		{
			this.TypeTable = typeTable;
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0001CBA1 File Offset: 0x0001ADA1
		// (set) Token: 0x060007B0 RID: 1968 RVA: 0x0001CBA9 File Offset: 0x0001ADA9
		internal TypeTable TypeTable { get; private set; }

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001CBB4 File Offset: 0x0001ADB4
		internal string Serialize(PSObject psObject)
		{
			if (psObject == null)
			{
				throw new ArgumentNullException("psObject");
			}
			StringBuilder stringBuilder = new StringBuilder();
			XmlWriterSettings settings = new XmlWriterSettings
			{
				CheckCharacters = false,
				Indent = false,
				CloseOutput = false,
				Encoding = Encoding.UTF8,
				NewLineHandling = NewLineHandling.None,
				OmitXmlDeclaration = true,
				ConformanceLevel = ConformanceLevel.Fragment
			};
			try
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, settings))
				{
					object obj = PSObjectSerializer.mgrAutomationAssembly.CreateInstance("System.Management.Automation.Serializer", true, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[]
					{
						xmlWriter,
						PSObjectSerializer.serializationContext
					}, null, null);
					PSObjectSerializer.serializerTypeTableProperty.SetValue(obj, this.TypeTable, null);
					PSObjectSerializer.serializerSerializeMethod.Invoke(obj, new object[]
					{
						psObject
					});
					PSObjectSerializer.serializerDoneMethod.Invoke(obj, null);
					xmlWriter.Flush();
				}
			}
			catch (Exception ex)
			{
				throw new PswsProxySerializationException(Strings.PswsSerializationError(ex.Message), ex);
			}
			ExTraceGlobals.LogTracer.Information<string, string>(0L, "ConvertToExchangeXml: Serialize object {0} successfully with following data:\r\n{1}", psObject.ToString(), stringBuilder.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001CCF8 File Offset: 0x0001AEF8
		internal PSObject Deserialize(string serializedData)
		{
			if (string.IsNullOrEmpty(serializedData))
			{
				throw new ArgumentNullException("serializedData");
			}
			XmlReaderSettings settings = new XmlReaderSettings
			{
				CheckCharacters = false,
				CloseInput = false,
				ConformanceLevel = ConformanceLevel.Document,
				IgnoreComments = true,
				IgnoreProcessingInstructions = true,
				IgnoreWhitespace = false,
				MaxCharactersFromEntities = 1024L,
				Schemas = null,
				ValidationFlags = XmlSchemaValidationFlags.None,
				ValidationType = ValidationType.None,
				XmlResolver = null
			};
			PSObject result;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					StreamWriter streamWriter = new StreamWriter(memoryStream);
					streamWriter.Write(serializedData);
					streamWriter.Flush();
					memoryStream.Seek(0L, SeekOrigin.Begin);
					using (XmlReader xmlReader = XmlReader.Create(memoryStream, settings))
					{
						object obj = PSObjectSerializer.mgrAutomationAssembly.CreateInstance("System.Management.Automation.Deserializer", true, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[]
						{
							xmlReader,
							PSObjectSerializer.deserializationContext
						}, null, null);
						PSObjectSerializer.deserializerTypeTableProperty.SetValue(obj, this.TypeTable, null);
						object obj2 = PSObjectSerializer.deserializerDeserializeMethod.Invoke(obj, null);
						PSObjectSerializer.deserializerDoneMethod.Invoke(obj, null);
						ExTraceGlobals.LogTracer.Information<string, string>(0L, "ObjectTransfer: Deserialize object {0} successfully by using following data:\r\n{1}", obj2.ToString(), serializedData);
						result = PSObject.AsPSObject(obj2);
					}
				}
			}
			catch (Exception ex)
			{
				throw new PswsProxySerializationException(Strings.PswsDeserializationError(ex.Message), ex);
			}
			return result;
		}

		// Token: 0x04000216 RID: 534
		private const string mgrAutomationAssemblyName = "System.Management.Automation";

		// Token: 0x04000217 RID: 535
		private const string serializerTypeName = "System.Management.Automation.Serializer";

		// Token: 0x04000218 RID: 536
		private const string serializerSerializeMethodName = "Serialize";

		// Token: 0x04000219 RID: 537
		private const string serializerTypeTablePropertyName = "TypeTable";

		// Token: 0x0400021A RID: 538
		private const string serializerDoneMethodName = "Done";

		// Token: 0x0400021B RID: 539
		private const string deserializerTypeName = "System.Management.Automation.Deserializer";

		// Token: 0x0400021C RID: 540
		private const string deserializerDeserializeMethodName = "Deserialize";

		// Token: 0x0400021D RID: 541
		private const string deserializerTypeTablePropertyName = "TypeTable";

		// Token: 0x0400021E RID: 542
		private const string deserializerDoneMethodName = "Done";

		// Token: 0x0400021F RID: 543
		private const string serializationContextTypeName = "System.Management.Automation.SerializationContext";

		// Token: 0x04000220 RID: 544
		private const int serializationDepth = 1;

		// Token: 0x04000221 RID: 545
		private const string serializationOptionsTypeName = "System.Management.Automation.SerializationOptions";

		// Token: 0x04000222 RID: 546
		private const string serializationRemotingOptions = "RemotingOptions";

		// Token: 0x04000223 RID: 547
		private const string deserializationContextTypeName = "System.Management.Automation.DeserializationContext";

		// Token: 0x04000224 RID: 548
		private const string deserializationOptionsTypeName = "System.Management.Automation.DeserializationOptions";

		// Token: 0x04000225 RID: 549
		private const string deserializationRemotingOptions = "RemotingOptions";

		// Token: 0x04000226 RID: 550
		private static Assembly mgrAutomationAssembly;

		// Token: 0x04000227 RID: 551
		private static Type serializerType;

		// Token: 0x04000228 RID: 552
		private static MethodInfo serializerSerializeMethod;

		// Token: 0x04000229 RID: 553
		private static PropertyInfo serializerTypeTableProperty;

		// Token: 0x0400022A RID: 554
		private static MethodInfo serializerDoneMethod;

		// Token: 0x0400022B RID: 555
		private static Type deserializerType;

		// Token: 0x0400022C RID: 556
		private static MethodInfo deserializerDeserializeMethod;

		// Token: 0x0400022D RID: 557
		private static PropertyInfo deserializerTypeTableProperty;

		// Token: 0x0400022E RID: 558
		private static MethodInfo deserializerDoneMethod;

		// Token: 0x0400022F RID: 559
		private static object serializationContext;

		// Token: 0x04000230 RID: 560
		private static object deserializationContext;
	}
}
