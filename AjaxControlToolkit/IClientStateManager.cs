using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000014 RID: 20
	public interface IClientStateManager
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008D RID: 141
		bool SupportsClientState { get; }

		// Token: 0x0600008E RID: 142
		void LoadClientState(string clientState);

		// Token: 0x0600008F RID: 143
		string SaveClientState();
	}
}
