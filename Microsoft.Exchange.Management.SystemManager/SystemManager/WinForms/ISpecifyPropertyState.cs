using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000142 RID: 322
	public interface ISpecifyPropertyState
	{
		// Token: 0x06000CED RID: 3309
		void SetPropertyState(string propertyName, PropertyState state, string message);
	}
}
