using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000452 RID: 1106
	[DataContract]
	public class SupervisionTag : EnumValue
	{
		// Token: 0x06003648 RID: 13896 RVA: 0x000A7C9D File Offset: 0x000A5E9D
		public SupervisionTag(string name) : base(name, name)
		{
		}
	}
}
