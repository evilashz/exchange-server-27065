using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis
{
	// Token: 0x0200003A RID: 58
	internal sealed class AgentDeserializationBinder : SerializationBinder
	{
		// Token: 0x06000151 RID: 337 RVA: 0x0000B9D0 File Offset: 0x00009BD0
		public override Type BindToType(string assemblyName, string typeName)
		{
			return Type.GetType(string.Format(CultureInfo.InvariantCulture, "{0}, {1}", new object[]
			{
				typeName,
				assemblyName
			}));
		}
	}
}
