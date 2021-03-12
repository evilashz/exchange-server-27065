using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006DE RID: 1758
	[DataContract]
	public class NoTrailingSpecificCharacterValidatorInfo : ValidatorInfo
	{
		// Token: 0x06004A39 RID: 19001 RVA: 0x000E351E File Offset: 0x000E171E
		internal NoTrailingSpecificCharacterValidatorInfo(NoTrailingSpecificCharacterConstraint constraint) : base("NoTrailingSpecificCharacterValidator")
		{
			this.InvalidChar = constraint.InvalidChar;
		}

		// Token: 0x1700281A RID: 10266
		// (get) Token: 0x06004A3A RID: 19002 RVA: 0x000E3537 File Offset: 0x000E1737
		// (set) Token: 0x06004A3B RID: 19003 RVA: 0x000E353F File Offset: 0x000E173F
		[DataMember]
		public char InvalidChar { get; set; }
	}
}
