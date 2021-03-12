using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000080 RID: 128
	public class PSSessionState : ISessionState
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x00011BDF File Offset: 0x0000FDDF
		public PSSessionState(SessionState sessionState)
		{
			this.sessionState = sessionState;
			this.variables = new PSVariables(this.sessionState.PSVariable);
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00011C04 File Offset: 0x0000FE04
		public string CurrentPath
		{
			get
			{
				return this.sessionState.Path.CurrentLocation.Path;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00011C1B File Offset: 0x0000FE1B
		public string CurrentPathProviderName
		{
			get
			{
				return this.sessionState.Path.CurrentLocation.Provider.Name;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00011C37 File Offset: 0x0000FE37
		public IVariableDictionary Variables
		{
			get
			{
				return this.variables;
			}
		}

		// Token: 0x0400011F RID: 287
		private readonly SessionState sessionState;

		// Token: 0x04000120 RID: 288
		private readonly IVariableDictionary variables;
	}
}
