using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000059 RID: 89
	public class DDICodeBehind
	{
		// Token: 0x06001A1A RID: 6682 RVA: 0x000539A1 File Offset: 0x00051BA1
		public DDICodeBehind()
		{
			this.RbacMetaData = new Dictionary<string, List<string>>();
		}

		// Token: 0x1700182E RID: 6190
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x000539B4 File Offset: 0x00051BB4
		// (set) Token: 0x06001A1C RID: 6684 RVA: 0x000539BC File Offset: 0x00051BBC
		public Dictionary<string, List<string>> RbacMetaData { get; set; }

		// Token: 0x06001A1D RID: 6685 RVA: 0x000539C5 File Offset: 0x00051BC5
		public virtual void ApplyMetaData()
		{
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x000539C7 File Offset: 0x00051BC7
		public void RegisterRbacDependency(string variable, List<string> dependentVariable)
		{
			if (this.RbacMetaData.ContainsKey(variable))
			{
				throw new InvalidOperationException("The same variable cannot be registered twice!");
			}
			this.RbacMetaData[variable] = dependentVariable;
		}
	}
}
