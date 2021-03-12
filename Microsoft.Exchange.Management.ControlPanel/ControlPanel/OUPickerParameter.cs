using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000443 RID: 1091
	[DataContract]
	internal class OUPickerParameter : FormletParameter
	{
		// Token: 0x06003616 RID: 13846 RVA: 0x000A787D File Offset: 0x000A5A7D
		public OUPickerParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, string serviceUrl) : base(name, dialogTitle, dialogLabel)
		{
			this.ServiceUrl = serviceUrl;
		}

		// Token: 0x1700212A RID: 8490
		// (get) Token: 0x06003617 RID: 13847 RVA: 0x000A7890 File Offset: 0x000A5A90
		// (set) Token: 0x06003618 RID: 13848 RVA: 0x000A7898 File Offset: 0x000A5A98
		[DataMember]
		public string ServiceUrl { get; private set; }
	}
}
