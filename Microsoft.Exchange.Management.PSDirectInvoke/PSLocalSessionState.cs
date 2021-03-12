using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.PSDirectInvoke
{
	// Token: 0x02000004 RID: 4
	internal class PSLocalSessionState : ISessionState
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002F83 File Offset: 0x00001183
		public PSLocalSessionState()
		{
			this.Variables = new PSLocalVariableDictionary();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002F96 File Offset: 0x00001196
		public string CurrentPath
		{
			get
			{
				return Environment.CurrentDirectory;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002F9D File Offset: 0x0000119D
		public string CurrentPathProviderName
		{
			get
			{
				return "FileSystem";
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002FA4 File Offset: 0x000011A4
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002FAC File Offset: 0x000011AC
		public IVariableDictionary Variables { get; private set; }
	}
}
