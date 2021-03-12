using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200043F RID: 1087
	[DataContract]
	internal class CallerIdsParameter : ObjectArrayParameter
	{
		// Token: 0x06003612 RID: 13842 RVA: 0x000A77E8 File Offset: 0x000A59E8
		public CallerIdsParameter(string name) : base(name, LocalizedString.Empty, LocalizedString.Empty)
		{
			this.noSelectionText = Strings.CallerIdParameterNoSelectionText;
			base.FormletType = typeof(CallerIdsEditor);
		}
	}
}
