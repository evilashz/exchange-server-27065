using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Policy;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008D6 RID: 2262
	[Guid("17156360-2f1a-384a-bc52-fde93c215c5b")]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[TypeLibImportClass(typeof(Assembly))]
	[CLSCompliant(false)]
	[ComVisible(true)]
	public interface _Assembly
	{
		// Token: 0x06005DB6 RID: 23990
		string ToString();

		// Token: 0x06005DB7 RID: 23991
		bool Equals(object other);

		// Token: 0x06005DB8 RID: 23992
		int GetHashCode();

		// Token: 0x06005DB9 RID: 23993
		Type GetType();

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x06005DBA RID: 23994
		string CodeBase { get; }

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x06005DBB RID: 23995
		string EscapedCodeBase { get; }

		// Token: 0x06005DBC RID: 23996
		AssemblyName GetName();

		// Token: 0x06005DBD RID: 23997
		AssemblyName GetName(bool copiedName);

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x06005DBE RID: 23998
		string FullName { get; }

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x06005DBF RID: 23999
		MethodInfo EntryPoint { get; }

		// Token: 0x06005DC0 RID: 24000
		Type GetType(string name);

		// Token: 0x06005DC1 RID: 24001
		Type GetType(string name, bool throwOnError);

		// Token: 0x06005DC2 RID: 24002
		Type[] GetExportedTypes();

		// Token: 0x06005DC3 RID: 24003
		Type[] GetTypes();

		// Token: 0x06005DC4 RID: 24004
		Stream GetManifestResourceStream(Type type, string name);

		// Token: 0x06005DC5 RID: 24005
		Stream GetManifestResourceStream(string name);

		// Token: 0x06005DC6 RID: 24006
		FileStream GetFile(string name);

		// Token: 0x06005DC7 RID: 24007
		FileStream[] GetFiles();

		// Token: 0x06005DC8 RID: 24008
		FileStream[] GetFiles(bool getResourceModules);

		// Token: 0x06005DC9 RID: 24009
		string[] GetManifestResourceNames();

		// Token: 0x06005DCA RID: 24010
		ManifestResourceInfo GetManifestResourceInfo(string resourceName);

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x06005DCB RID: 24011
		string Location { get; }

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x06005DCC RID: 24012
		Evidence Evidence { get; }

		// Token: 0x06005DCD RID: 24013
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06005DCE RID: 24014
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06005DCF RID: 24015
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x06005DD0 RID: 24016
		[SecurityCritical]
		void GetObjectData(SerializationInfo info, StreamingContext context);

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06005DD1 RID: 24017
		// (remove) Token: 0x06005DD2 RID: 24018
		event ModuleResolveEventHandler ModuleResolve;

		// Token: 0x06005DD3 RID: 24019
		Type GetType(string name, bool throwOnError, bool ignoreCase);

		// Token: 0x06005DD4 RID: 24020
		Assembly GetSatelliteAssembly(CultureInfo culture);

		// Token: 0x06005DD5 RID: 24021
		Assembly GetSatelliteAssembly(CultureInfo culture, Version version);

		// Token: 0x06005DD6 RID: 24022
		Module LoadModule(string moduleName, byte[] rawModule);

		// Token: 0x06005DD7 RID: 24023
		Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore);

		// Token: 0x06005DD8 RID: 24024
		object CreateInstance(string typeName);

		// Token: 0x06005DD9 RID: 24025
		object CreateInstance(string typeName, bool ignoreCase);

		// Token: 0x06005DDA RID: 24026
		object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes);

		// Token: 0x06005DDB RID: 24027
		Module[] GetLoadedModules();

		// Token: 0x06005DDC RID: 24028
		Module[] GetLoadedModules(bool getResourceModules);

		// Token: 0x06005DDD RID: 24029
		Module[] GetModules();

		// Token: 0x06005DDE RID: 24030
		Module[] GetModules(bool getResourceModules);

		// Token: 0x06005DDF RID: 24031
		Module GetModule(string name);

		// Token: 0x06005DE0 RID: 24032
		AssemblyName[] GetReferencedAssemblies();

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x06005DE1 RID: 24033
		bool GlobalAssemblyCache { get; }
	}
}
