using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000047 RID: 71
	internal class SupportShellHelper : MarshalByRefObject
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x0000695E File Offset: 0x00004B5E
		public KeyValuePair<string, Type>[] GetAllPropertiesAndValueTypes()
		{
			return (from property in ContextPropertySchema.AllProperties
			select new KeyValuePair<string, Type>(property.Name, (property.Type.Assembly.GlobalAssemblyCache || (property.Type.IsEnum && property.Type.Assembly == typeof(SupportShellHelper).Assembly)) ? property.Type : typeof(string))).ToArray<KeyValuePair<string, Type>>();
		}
	}
}
