using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Text;

namespace System.Runtime.Remoting
{
	// Token: 0x020007A0 RID: 1952
	[SecurityCritical]
	[ComVisible(true)]
	public class SoapServices
	{
		// Token: 0x06005586 RID: 21894 RVA: 0x0012EB67 File Offset: 0x0012CD67
		private SoapServices()
		{
		}

		// Token: 0x06005587 RID: 21895 RVA: 0x0012EB6F File Offset: 0x0012CD6F
		private static string CreateKey(string elementName, string elementNamespace)
		{
			if (elementNamespace == null)
			{
				return elementName;
			}
			return elementName + " " + elementNamespace;
		}

		// Token: 0x06005588 RID: 21896 RVA: 0x0012EB82 File Offset: 0x0012CD82
		[SecurityCritical]
		public static void RegisterInteropXmlElement(string xmlElement, string xmlNamespace, Type type)
		{
			SoapServices._interopXmlElementToType[SoapServices.CreateKey(xmlElement, xmlNamespace)] = type;
			SoapServices._interopTypeToXmlElement[type] = new SoapServices.XmlEntry(xmlElement, xmlNamespace);
		}

		// Token: 0x06005589 RID: 21897 RVA: 0x0012EBA8 File Offset: 0x0012CDA8
		[SecurityCritical]
		public static void RegisterInteropXmlType(string xmlType, string xmlTypeNamespace, Type type)
		{
			SoapServices._interopXmlTypeToType[SoapServices.CreateKey(xmlType, xmlTypeNamespace)] = type;
			SoapServices._interopTypeToXmlType[type] = new SoapServices.XmlEntry(xmlType, xmlTypeNamespace);
		}

		// Token: 0x0600558A RID: 21898 RVA: 0x0012EBD0 File Offset: 0x0012CDD0
		[SecurityCritical]
		public static void PreLoad(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!(type is RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			MethodInfo[] methods = type.GetMethods();
			foreach (MethodInfo mb in methods)
			{
				SoapServices.RegisterSoapActionForMethodBase(mb);
			}
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (soapTypeAttribute.IsInteropXmlElement())
			{
				SoapServices.RegisterInteropXmlElement(soapTypeAttribute.XmlElementName, soapTypeAttribute.XmlNamespace, type);
			}
			if (soapTypeAttribute.IsInteropXmlType())
			{
				SoapServices.RegisterInteropXmlType(soapTypeAttribute.XmlTypeName, soapTypeAttribute.XmlTypeNamespace, type);
			}
			int num = 0;
			SoapServices.XmlToFieldTypeMap xmlToFieldTypeMap = new SoapServices.XmlToFieldTypeMap();
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				SoapFieldAttribute soapFieldAttribute = (SoapFieldAttribute)InternalRemotingServices.GetCachedSoapAttribute(fieldInfo);
				if (soapFieldAttribute.IsInteropXmlElement())
				{
					string xmlElementName = soapFieldAttribute.XmlElementName;
					string xmlNamespace = soapFieldAttribute.XmlNamespace;
					if (soapFieldAttribute.UseAttribute)
					{
						xmlToFieldTypeMap.AddXmlAttribute(fieldInfo.FieldType, fieldInfo.Name, xmlElementName, xmlNamespace);
					}
					else
					{
						xmlToFieldTypeMap.AddXmlElement(fieldInfo.FieldType, fieldInfo.Name, xmlElementName, xmlNamespace);
					}
					num++;
				}
			}
			if (num > 0)
			{
				SoapServices._xmlToFieldTypeMap[type] = xmlToFieldTypeMap;
			}
		}

		// Token: 0x0600558B RID: 21899 RVA: 0x0012ED18 File Offset: 0x0012CF18
		[SecurityCritical]
		public static void PreLoad(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (!(assembly is RuntimeAssembly))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), "assembly");
			}
			Type[] types = assembly.GetTypes();
			foreach (Type type in types)
			{
				SoapServices.PreLoad(type);
			}
		}

		// Token: 0x0600558C RID: 21900 RVA: 0x0012ED77 File Offset: 0x0012CF77
		[SecurityCritical]
		public static Type GetInteropTypeFromXmlElement(string xmlElement, string xmlNamespace)
		{
			return (Type)SoapServices._interopXmlElementToType[SoapServices.CreateKey(xmlElement, xmlNamespace)];
		}

		// Token: 0x0600558D RID: 21901 RVA: 0x0012ED8F File Offset: 0x0012CF8F
		[SecurityCritical]
		public static Type GetInteropTypeFromXmlType(string xmlType, string xmlTypeNamespace)
		{
			return (Type)SoapServices._interopXmlTypeToType[SoapServices.CreateKey(xmlType, xmlTypeNamespace)];
		}

		// Token: 0x0600558E RID: 21902 RVA: 0x0012EDA8 File Offset: 0x0012CFA8
		public static void GetInteropFieldTypeAndNameFromXmlElement(Type containingType, string xmlElement, string xmlNamespace, out Type type, out string name)
		{
			if (containingType == null)
			{
				type = null;
				name = null;
				return;
			}
			SoapServices.XmlToFieldTypeMap xmlToFieldTypeMap = (SoapServices.XmlToFieldTypeMap)SoapServices._xmlToFieldTypeMap[containingType];
			if (xmlToFieldTypeMap != null)
			{
				xmlToFieldTypeMap.GetFieldTypeAndNameFromXmlElement(xmlElement, xmlNamespace, out type, out name);
				return;
			}
			type = null;
			name = null;
		}

		// Token: 0x0600558F RID: 21903 RVA: 0x0012EDF0 File Offset: 0x0012CFF0
		public static void GetInteropFieldTypeAndNameFromXmlAttribute(Type containingType, string xmlAttribute, string xmlNamespace, out Type type, out string name)
		{
			if (containingType == null)
			{
				type = null;
				name = null;
				return;
			}
			SoapServices.XmlToFieldTypeMap xmlToFieldTypeMap = (SoapServices.XmlToFieldTypeMap)SoapServices._xmlToFieldTypeMap[containingType];
			if (xmlToFieldTypeMap != null)
			{
				xmlToFieldTypeMap.GetFieldTypeAndNameFromXmlAttribute(xmlAttribute, xmlNamespace, out type, out name);
				return;
			}
			type = null;
			name = null;
		}

		// Token: 0x06005590 RID: 21904 RVA: 0x0012EE38 File Offset: 0x0012D038
		[SecurityCritical]
		public static bool GetXmlElementForInteropType(Type type, out string xmlElement, out string xmlNamespace)
		{
			SoapServices.XmlEntry xmlEntry = (SoapServices.XmlEntry)SoapServices._interopTypeToXmlElement[type];
			if (xmlEntry != null)
			{
				xmlElement = xmlEntry.Name;
				xmlNamespace = xmlEntry.Namespace;
				return true;
			}
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (soapTypeAttribute.IsInteropXmlElement())
			{
				xmlElement = soapTypeAttribute.XmlElementName;
				xmlNamespace = soapTypeAttribute.XmlNamespace;
				return true;
			}
			xmlElement = null;
			xmlNamespace = null;
			return false;
		}

		// Token: 0x06005591 RID: 21905 RVA: 0x0012EE98 File Offset: 0x0012D098
		[SecurityCritical]
		public static bool GetXmlTypeForInteropType(Type type, out string xmlType, out string xmlTypeNamespace)
		{
			SoapServices.XmlEntry xmlEntry = (SoapServices.XmlEntry)SoapServices._interopTypeToXmlType[type];
			if (xmlEntry != null)
			{
				xmlType = xmlEntry.Name;
				xmlTypeNamespace = xmlEntry.Namespace;
				return true;
			}
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (soapTypeAttribute.IsInteropXmlType())
			{
				xmlType = soapTypeAttribute.XmlTypeName;
				xmlTypeNamespace = soapTypeAttribute.XmlTypeNamespace;
				return true;
			}
			xmlType = null;
			xmlTypeNamespace = null;
			return false;
		}

		// Token: 0x06005592 RID: 21906 RVA: 0x0012EEF8 File Offset: 0x0012D0F8
		[SecurityCritical]
		public static string GetXmlNamespaceForMethodCall(MethodBase mb)
		{
			SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb);
			return soapMethodAttribute.XmlNamespace;
		}

		// Token: 0x06005593 RID: 21907 RVA: 0x0012EF18 File Offset: 0x0012D118
		[SecurityCritical]
		public static string GetXmlNamespaceForMethodResponse(MethodBase mb)
		{
			SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb);
			return soapMethodAttribute.ResponseXmlNamespace;
		}

		// Token: 0x06005594 RID: 21908 RVA: 0x0012EF38 File Offset: 0x0012D138
		[SecurityCritical]
		public static void RegisterSoapActionForMethodBase(MethodBase mb)
		{
			SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb);
			if (soapMethodAttribute.SoapActionExplicitySet)
			{
				SoapServices.RegisterSoapActionForMethodBase(mb, soapMethodAttribute.SoapAction);
			}
		}

		// Token: 0x06005595 RID: 21909 RVA: 0x0012EF68 File Offset: 0x0012D168
		public static void RegisterSoapActionForMethodBase(MethodBase mb, string soapAction)
		{
			if (soapAction != null)
			{
				SoapServices._methodBaseToSoapAction[mb] = soapAction;
				ArrayList arrayList = (ArrayList)SoapServices._soapActionToMethodBase[soapAction];
				if (arrayList == null)
				{
					Hashtable soapActionToMethodBase = SoapServices._soapActionToMethodBase;
					lock (soapActionToMethodBase)
					{
						arrayList = ArrayList.Synchronized(new ArrayList());
						SoapServices._soapActionToMethodBase[soapAction] = arrayList;
					}
				}
				arrayList.Add(mb);
			}
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x0012EFE4 File Offset: 0x0012D1E4
		[SecurityCritical]
		public static string GetSoapActionFromMethodBase(MethodBase mb)
		{
			string text = (string)SoapServices._methodBaseToSoapAction[mb];
			if (text == null)
			{
				SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb);
				text = soapMethodAttribute.SoapAction;
			}
			return text;
		}

		// Token: 0x06005597 RID: 21911 RVA: 0x0012F01C File Offset: 0x0012D21C
		[SecurityCritical]
		public static bool IsSoapActionValidForMethodBase(string soapAction, MethodBase mb)
		{
			if (mb == null)
			{
				throw new ArgumentNullException("mb");
			}
			if (soapAction[0] == '"' && soapAction[soapAction.Length - 1] == '"')
			{
				soapAction = soapAction.Substring(1, soapAction.Length - 2);
			}
			SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb);
			if (string.CompareOrdinal(soapMethodAttribute.SoapAction, soapAction) == 0)
			{
				return true;
			}
			string text = (string)SoapServices._methodBaseToSoapAction[mb];
			if (text != null && string.CompareOrdinal(text, soapAction) == 0)
			{
				return true;
			}
			string[] array = soapAction.Split(new char[]
			{
				'#'
			});
			if (array.Length != 2)
			{
				return false;
			}
			bool flag;
			string typeNameForSoapActionNamespace = XmlNamespaceEncoder.GetTypeNameForSoapActionNamespace(array[0], out flag);
			if (typeNameForSoapActionNamespace == null)
			{
				return false;
			}
			string value = array[1];
			RuntimeMethodInfo runtimeMethodInfo = mb as RuntimeMethodInfo;
			RuntimeConstructorInfo runtimeConstructorInfo = mb as RuntimeConstructorInfo;
			RuntimeModule runtimeModule;
			if (runtimeMethodInfo != null)
			{
				runtimeModule = runtimeMethodInfo.GetRuntimeModule();
			}
			else
			{
				if (!(runtimeConstructorInfo != null))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
				}
				runtimeModule = runtimeConstructorInfo.GetRuntimeModule();
			}
			string text2 = mb.DeclaringType.FullName;
			if (flag)
			{
				text2 = text2 + ", " + runtimeModule.GetRuntimeAssembly().GetSimpleName();
			}
			return text2.Equals(typeNameForSoapActionNamespace) && mb.Name.Equals(value);
		}

		// Token: 0x06005598 RID: 21912 RVA: 0x0012F168 File Offset: 0x0012D368
		public static bool GetTypeAndMethodNameFromSoapAction(string soapAction, out string typeName, out string methodName)
		{
			if (soapAction[0] == '"' && soapAction[soapAction.Length - 1] == '"')
			{
				soapAction = soapAction.Substring(1, soapAction.Length - 2);
			}
			ArrayList arrayList = (ArrayList)SoapServices._soapActionToMethodBase[soapAction];
			if (arrayList != null)
			{
				if (arrayList.Count > 1)
				{
					typeName = null;
					methodName = null;
					return false;
				}
				MethodBase methodBase = (MethodBase)arrayList[0];
				if (methodBase != null)
				{
					RuntimeMethodInfo runtimeMethodInfo = methodBase as RuntimeMethodInfo;
					RuntimeConstructorInfo runtimeConstructorInfo = methodBase as RuntimeConstructorInfo;
					RuntimeModule runtimeModule;
					if (runtimeMethodInfo != null)
					{
						runtimeModule = runtimeMethodInfo.GetRuntimeModule();
					}
					else
					{
						if (!(runtimeConstructorInfo != null))
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
						}
						runtimeModule = runtimeConstructorInfo.GetRuntimeModule();
					}
					typeName = methodBase.DeclaringType.FullName + ", " + runtimeModule.GetRuntimeAssembly().GetSimpleName();
					methodName = methodBase.Name;
					return true;
				}
			}
			string[] array = soapAction.Split(new char[]
			{
				'#'
			});
			if (array.Length != 2)
			{
				typeName = null;
				methodName = null;
				return false;
			}
			bool flag;
			typeName = XmlNamespaceEncoder.GetTypeNameForSoapActionNamespace(array[0], out flag);
			if (typeName == null)
			{
				methodName = null;
				return false;
			}
			methodName = array[1];
			return true;
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06005599 RID: 21913 RVA: 0x0012F290 File Offset: 0x0012D490
		public static string XmlNsForClrType
		{
			get
			{
				return SoapServices.startNS;
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x0600559A RID: 21914 RVA: 0x0012F297 File Offset: 0x0012D497
		public static string XmlNsForClrTypeWithAssembly
		{
			get
			{
				return SoapServices.assemblyNS;
			}
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x0600559B RID: 21915 RVA: 0x0012F29E File Offset: 0x0012D49E
		public static string XmlNsForClrTypeWithNs
		{
			get
			{
				return SoapServices.namespaceNS;
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x0600559C RID: 21916 RVA: 0x0012F2A5 File Offset: 0x0012D4A5
		public static string XmlNsForClrTypeWithNsAndAssembly
		{
			get
			{
				return SoapServices.fullNS;
			}
		}

		// Token: 0x0600559D RID: 21917 RVA: 0x0012F2AC File Offset: 0x0012D4AC
		public static bool IsClrTypeNamespace(string namespaceString)
		{
			return namespaceString.StartsWith(SoapServices.startNS, StringComparison.Ordinal);
		}

		// Token: 0x0600559E RID: 21918 RVA: 0x0012F2C0 File Offset: 0x0012D4C0
		[SecurityCritical]
		public static string CodeXmlNamespaceForClrTypeNamespace(string typeNamespace, string assemblyName)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			if (SoapServices.IsNameNull(typeNamespace))
			{
				if (SoapServices.IsNameNull(assemblyName))
				{
					throw new ArgumentNullException("typeNamespace,assemblyName");
				}
				stringBuilder.Append(SoapServices.assemblyNS);
				SoapServices.UriEncode(assemblyName, stringBuilder);
			}
			else if (SoapServices.IsNameNull(assemblyName))
			{
				stringBuilder.Append(SoapServices.namespaceNS);
				stringBuilder.Append(typeNamespace);
			}
			else
			{
				stringBuilder.Append(SoapServices.fullNS);
				if (typeNamespace[0] == '.')
				{
					stringBuilder.Append(typeNamespace.Substring(1));
				}
				else
				{
					stringBuilder.Append(typeNamespace);
				}
				stringBuilder.Append('/');
				SoapServices.UriEncode(assemblyName, stringBuilder);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600559F RID: 21919 RVA: 0x0012F36C File Offset: 0x0012D56C
		[SecurityCritical]
		public static bool DecodeXmlNamespaceForClrTypeNamespace(string inNamespace, out string typeNamespace, out string assemblyName)
		{
			if (SoapServices.IsNameNull(inNamespace))
			{
				throw new ArgumentNullException("inNamespace");
			}
			assemblyName = null;
			typeNamespace = "";
			if (inNamespace.StartsWith(SoapServices.assemblyNS, StringComparison.Ordinal))
			{
				assemblyName = SoapServices.UriDecode(inNamespace.Substring(SoapServices.assemblyNS.Length));
			}
			else if (inNamespace.StartsWith(SoapServices.namespaceNS, StringComparison.Ordinal))
			{
				typeNamespace = inNamespace.Substring(SoapServices.namespaceNS.Length);
			}
			else
			{
				if (!inNamespace.StartsWith(SoapServices.fullNS, StringComparison.Ordinal))
				{
					return false;
				}
				int num = inNamespace.IndexOf("/", SoapServices.fullNS.Length);
				typeNamespace = inNamespace.Substring(SoapServices.fullNS.Length, num - SoapServices.fullNS.Length);
				assemblyName = SoapServices.UriDecode(inNamespace.Substring(num + 1));
			}
			return true;
		}

		// Token: 0x060055A0 RID: 21920 RVA: 0x0012F438 File Offset: 0x0012D638
		internal static void UriEncode(string value, StringBuilder sb)
		{
			if (value == null || value.Length == 0)
			{
				return;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == ' ')
				{
					sb.Append("%20");
				}
				else if (value[i] == '=')
				{
					sb.Append("%3D");
				}
				else if (value[i] == ',')
				{
					sb.Append("%2C");
				}
				else
				{
					sb.Append(value[i]);
				}
			}
		}

		// Token: 0x060055A1 RID: 21921 RVA: 0x0012F4BC File Offset: 0x0012D6BC
		internal static string UriDecode(string value)
		{
			if (value == null || value.Length == 0)
			{
				return value;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == '%' && value.Length - i >= 3)
				{
					if (value[i + 1] == '2' && value[i + 2] == '0')
					{
						stringBuilder.Append(' ');
						i += 2;
					}
					else if (value[i + 1] == '3' && value[i + 2] == 'D')
					{
						stringBuilder.Append('=');
						i += 2;
					}
					else if (value[i + 1] == '2' && value[i + 2] == 'C')
					{
						stringBuilder.Append(',');
						i += 2;
					}
					else
					{
						stringBuilder.Append(value[i]);
					}
				}
				else
				{
					stringBuilder.Append(value[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060055A2 RID: 21922 RVA: 0x0012F5AE File Offset: 0x0012D7AE
		private static bool IsNameNull(string name)
		{
			return name == null || name.Length == 0;
		}

		// Token: 0x040026EE RID: 9966
		private static Hashtable _interopXmlElementToType = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040026EF RID: 9967
		private static Hashtable _interopTypeToXmlElement = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040026F0 RID: 9968
		private static Hashtable _interopXmlTypeToType = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040026F1 RID: 9969
		private static Hashtable _interopTypeToXmlType = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040026F2 RID: 9970
		private static Hashtable _xmlToFieldTypeMap = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040026F3 RID: 9971
		private static Hashtable _methodBaseToSoapAction = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040026F4 RID: 9972
		private static Hashtable _soapActionToMethodBase = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040026F5 RID: 9973
		internal static string startNS = "http://schemas.microsoft.com/clr/";

		// Token: 0x040026F6 RID: 9974
		internal static string assemblyNS = "http://schemas.microsoft.com/clr/assem/";

		// Token: 0x040026F7 RID: 9975
		internal static string namespaceNS = "http://schemas.microsoft.com/clr/ns/";

		// Token: 0x040026F8 RID: 9976
		internal static string fullNS = "http://schemas.microsoft.com/clr/nsassem/";

		// Token: 0x02000C37 RID: 3127
		private class XmlEntry
		{
			// Token: 0x06006F7B RID: 28539 RVA: 0x0017FB66 File Offset: 0x0017DD66
			public XmlEntry(string name, string xmlNamespace)
			{
				this.Name = name;
				this.Namespace = xmlNamespace;
			}

			// Token: 0x040036FA RID: 14074
			public string Name;

			// Token: 0x040036FB RID: 14075
			public string Namespace;
		}

		// Token: 0x02000C38 RID: 3128
		private class XmlToFieldTypeMap
		{
			// Token: 0x06006F7D RID: 28541 RVA: 0x0017FB9A File Offset: 0x0017DD9A
			[SecurityCritical]
			public void AddXmlElement(Type fieldType, string fieldName, string xmlElement, string xmlNamespace)
			{
				this._elements[SoapServices.CreateKey(xmlElement, xmlNamespace)] = new SoapServices.XmlToFieldTypeMap.FieldEntry(fieldType, fieldName);
			}

			// Token: 0x06006F7E RID: 28542 RVA: 0x0017FBB6 File Offset: 0x0017DDB6
			[SecurityCritical]
			public void AddXmlAttribute(Type fieldType, string fieldName, string xmlAttribute, string xmlNamespace)
			{
				this._attributes[SoapServices.CreateKey(xmlAttribute, xmlNamespace)] = new SoapServices.XmlToFieldTypeMap.FieldEntry(fieldType, fieldName);
			}

			// Token: 0x06006F7F RID: 28543 RVA: 0x0017FBD4 File Offset: 0x0017DDD4
			[SecurityCritical]
			public void GetFieldTypeAndNameFromXmlElement(string xmlElement, string xmlNamespace, out Type type, out string name)
			{
				SoapServices.XmlToFieldTypeMap.FieldEntry fieldEntry = (SoapServices.XmlToFieldTypeMap.FieldEntry)this._elements[SoapServices.CreateKey(xmlElement, xmlNamespace)];
				if (fieldEntry != null)
				{
					type = fieldEntry.Type;
					name = fieldEntry.Name;
					return;
				}
				type = null;
				name = null;
			}

			// Token: 0x06006F80 RID: 28544 RVA: 0x0017FC18 File Offset: 0x0017DE18
			[SecurityCritical]
			public void GetFieldTypeAndNameFromXmlAttribute(string xmlAttribute, string xmlNamespace, out Type type, out string name)
			{
				SoapServices.XmlToFieldTypeMap.FieldEntry fieldEntry = (SoapServices.XmlToFieldTypeMap.FieldEntry)this._attributes[SoapServices.CreateKey(xmlAttribute, xmlNamespace)];
				if (fieldEntry != null)
				{
					type = fieldEntry.Type;
					name = fieldEntry.Name;
					return;
				}
				type = null;
				name = null;
			}

			// Token: 0x040036FC RID: 14076
			private Hashtable _attributes = new Hashtable();

			// Token: 0x040036FD RID: 14077
			private Hashtable _elements = new Hashtable();

			// Token: 0x02000CDA RID: 3290
			private class FieldEntry
			{
				// Token: 0x060070FB RID: 28923 RVA: 0x001849E0 File Offset: 0x00182BE0
				public FieldEntry(Type type, string name)
				{
					this.Type = type;
					this.Name = name;
				}

				// Token: 0x04003884 RID: 14468
				public Type Type;

				// Token: 0x04003885 RID: 14469
				public string Name;
			}
		}
	}
}
