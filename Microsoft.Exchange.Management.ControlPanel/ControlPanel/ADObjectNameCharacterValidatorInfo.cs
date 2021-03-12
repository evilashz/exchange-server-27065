using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006DA RID: 1754
	[DataContract]
	public class ADObjectNameCharacterValidatorInfo : ValidatorInfo
	{
		// Token: 0x06004A31 RID: 18993 RVA: 0x000E34A6 File Offset: 0x000E16A6
		internal ADObjectNameCharacterValidatorInfo(ADObjectNameCharacterConstraint constraint) : base("ADObjectNameCharacterValidator")
		{
			this.DisplayCharacters = ValidatorHelper.ToVisibleString(constraint.Characters);
			this.InvalidCharacters = new string(constraint.Characters);
		}

		// Token: 0x17002818 RID: 10264
		// (get) Token: 0x06004A32 RID: 18994 RVA: 0x000E34D5 File Offset: 0x000E16D5
		// (set) Token: 0x06004A33 RID: 18995 RVA: 0x000E34DD File Offset: 0x000E16DD
		[DataMember]
		public string DisplayCharacters { get; set; }

		// Token: 0x17002819 RID: 10265
		// (get) Token: 0x06004A34 RID: 18996 RVA: 0x000E34E6 File Offset: 0x000E16E6
		// (set) Token: 0x06004A35 RID: 18997 RVA: 0x000E34EE File Offset: 0x000E16EE
		[DataMember]
		public string InvalidCharacters { get; set; }
	}
}
