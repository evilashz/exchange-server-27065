using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000452 RID: 1106
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RpcParameters
	{
		// Token: 0x06003127 RID: 12583 RVA: 0x000C97EC File Offset: 0x000C79EC
		public RpcParameters()
		{
			this.parameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x000C9804 File Offset: 0x000C7A04
		public RpcParameters(byte[] data)
		{
			if (data == null || data.Length <= 0)
			{
				throw new ArgumentNullException("data");
			}
			this.parameters = RpcCommon.ConvertByteArrayToRpcParameters(data);
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x000C982C File Offset: 0x000C7A2C
		public byte[] Serialize()
		{
			return RpcCommon.ConvertRpcParametersToByteArray(this.parameters);
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x000C9839 File Offset: 0x000C7A39
		protected void SetParameterValue(string name, object value)
		{
			this.parameters[name] = value;
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x000C9848 File Offset: 0x000C7A48
		protected object GetParameterValue(string name)
		{
			object result = null;
			if (!this.parameters.TryGetValue(name, out result))
			{
				throw new ArgumentNullException("RPC parameter is missing for " + name);
			}
			return result;
		}

		// Token: 0x04001AA3 RID: 6819
		private readonly Dictionary<string, object> parameters;
	}
}
