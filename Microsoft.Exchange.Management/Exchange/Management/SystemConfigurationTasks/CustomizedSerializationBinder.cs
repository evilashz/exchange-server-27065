using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AC3 RID: 2755
	internal sealed class CustomizedSerializationBinder : SerializationBinder
	{
		// Token: 0x060061A3 RID: 24995 RVA: 0x00197104 File Offset: 0x00195304
		public override Type BindToType(string assemblyName, string typeName)
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (assemblyName.IndexOf(assembly.GetName().Name) >= 0)
				{
					return assembly.GetType(typeName);
				}
			}
			return null;
		}
	}
}
