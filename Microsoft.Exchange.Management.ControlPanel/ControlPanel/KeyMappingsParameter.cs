using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000441 RID: 1089
	[DataContract]
	public class KeyMappingsParameter : FormletParameter
	{
		// Token: 0x06003614 RID: 13844 RVA: 0x000A7844 File Offset: 0x000A5A44
		public KeyMappingsParameter(string name) : base(name, LocalizedString.Empty, LocalizedString.Empty)
		{
			this.noSelectionText = Strings.KeyMappingsParameterNoSelectionText;
			base.FormletType = typeof(KeyMappingsEditor);
		}
	}
}
