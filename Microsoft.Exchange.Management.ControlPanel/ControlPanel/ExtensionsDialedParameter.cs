using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200043E RID: 1086
	[DataContract]
	internal class ExtensionsDialedParameter : FormletParameter
	{
		// Token: 0x06003611 RID: 13841 RVA: 0x000A77BA File Offset: 0x000A59BA
		public ExtensionsDialedParameter(string name) : base(name, LocalizedString.Empty, LocalizedString.Empty)
		{
			this.noSelectionText = Strings.ExtensionsDialedParameterNoSelectionText;
			base.FormletType = typeof(ExtensionsDialedEditor);
		}
	}
}
