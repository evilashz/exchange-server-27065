using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200043C RID: 1084
	[DataContract]
	public abstract class ObjectArrayParameter : FormletParameter
	{
		// Token: 0x06003609 RID: 13833 RVA: 0x000A7713 File Offset: 0x000A5913
		public ObjectArrayParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel) : base(name, dialogTitle, dialogLabel)
		{
		}

		// Token: 0x17002128 RID: 8488
		// (get) Token: 0x0600360A RID: 13834 RVA: 0x000A771E File Offset: 0x000A591E
		// (set) Token: 0x0600360B RID: 13835 RVA: 0x000A7726 File Offset: 0x000A5926
		[DataMember(EmitDefaultValue = false)]
		public bool UseAndDelimiter { get; set; }
	}
}
