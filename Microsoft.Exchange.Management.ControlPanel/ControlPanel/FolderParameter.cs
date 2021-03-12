using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000442 RID: 1090
	[DataContract]
	internal class FolderParameter : FormletParameter
	{
		// Token: 0x06003615 RID: 13845 RVA: 0x000A7872 File Offset: 0x000A5A72
		public FolderParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel) : base(name, dialogTitle, dialogLabel)
		{
		}
	}
}
