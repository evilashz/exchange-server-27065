using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000446 RID: 1094
	[DataContract]
	public class BooleanParameter : HiddenParameter
	{
		// Token: 0x06003624 RID: 13860 RVA: 0x000A7967 File Offset: 0x000A5B67
		public BooleanParameter(string name) : base(name, true)
		{
			base.EditorType = "HiddenParameterEditor";
		}
	}
}
