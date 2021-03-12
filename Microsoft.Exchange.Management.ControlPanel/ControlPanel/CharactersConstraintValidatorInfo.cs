using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006D9 RID: 1753
	[DataContract]
	public class CharactersConstraintValidatorInfo : ValidatorInfo
	{
		// Token: 0x06004A2A RID: 18986 RVA: 0x000E3438 File Offset: 0x000E1638
		internal CharactersConstraintValidatorInfo(CharacterConstraint constraint) : base("CharactersConstraintValidator")
		{
			this.DisplayCharacters = ValidatorHelper.ToVisibleString(constraint.Characters);
			this.Characters = new string(constraint.Characters);
			this.ShowAsValid = constraint.ShowAsValid;
		}

		// Token: 0x17002815 RID: 10261
		// (get) Token: 0x06004A2B RID: 18987 RVA: 0x000E3473 File Offset: 0x000E1673
		// (set) Token: 0x06004A2C RID: 18988 RVA: 0x000E347B File Offset: 0x000E167B
		[DataMember]
		public string Characters { get; set; }

		// Token: 0x17002816 RID: 10262
		// (get) Token: 0x06004A2D RID: 18989 RVA: 0x000E3484 File Offset: 0x000E1684
		// (set) Token: 0x06004A2E RID: 18990 RVA: 0x000E348C File Offset: 0x000E168C
		[DataMember]
		public string DisplayCharacters { get; set; }

		// Token: 0x17002817 RID: 10263
		// (get) Token: 0x06004A2F RID: 18991 RVA: 0x000E3495 File Offset: 0x000E1695
		// (set) Token: 0x06004A30 RID: 18992 RVA: 0x000E349D File Offset: 0x000E169D
		[DataMember]
		public bool ShowAsValid { get; set; }
	}
}
