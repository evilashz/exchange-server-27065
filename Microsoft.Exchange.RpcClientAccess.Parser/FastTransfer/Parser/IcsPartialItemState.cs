using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000188 RID: 392
	internal class IcsPartialItemState
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x0001BCE0 File Offset: 0x00019EE0
		public PropertyGroupMapping GetCurrentMapping()
		{
			PropertyGroupMapping result;
			if (!this.propertyGroupMappings.TryGetValue(this.currentMappingId, out result))
			{
				throw new RopExecutionException(string.Format("Invalid property group mapping ID {0}", this.currentMappingId), (ErrorCode)2147942487U);
			}
			return result;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001BD23 File Offset: 0x00019F23
		public bool AddMapping(PropertyGroupMapping mapping)
		{
			if (!this.propertyGroupMappings.ContainsKey(mapping.MappingId))
			{
				this.propertyGroupMappings.Add(mapping.MappingId, mapping);
				return true;
			}
			return false;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0001BD4D File Offset: 0x00019F4D
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x0001BD55 File Offset: 0x00019F55
		public int CurrentMappingId
		{
			get
			{
				return this.currentMappingId;
			}
			set
			{
				this.currentMappingId = value;
			}
		}

		// Token: 0x040003D9 RID: 985
		private Dictionary<int, PropertyGroupMapping> propertyGroupMappings = new Dictionary<int, PropertyGroupMapping>(1);

		// Token: 0x040003DA RID: 986
		private int currentMappingId = -1;
	}
}
