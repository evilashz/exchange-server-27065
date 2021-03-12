using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.UM.Rpc
{
	// Token: 0x02000002 RID: 2
	[Serializable]
	public class UMDiagnosticObject : ConfigurableObject
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal UMDiagnosticObject() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020DD File Offset: 0x000002DD
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return UMDiagnosticObject.schema;
			}
		}

		// Token: 0x04000001 RID: 1
		private static UMDiagnosticObject.UMDiagnosticObjectSchema schema = new UMDiagnosticObject.UMDiagnosticObjectSchema();

		// Token: 0x02000003 RID: 3
		private class UMDiagnosticObjectSchema : SimpleProviderObjectSchema
		{
		}
	}
}
