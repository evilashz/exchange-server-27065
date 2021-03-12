using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200083C RID: 2108
	[Serializable]
	internal class PropertyReference
	{
		// Token: 0x1700250F RID: 9487
		// (get) Token: 0x060068A5 RID: 26789 RVA: 0x001710B4 File Offset: 0x0016F2B4
		// (set) Token: 0x060068A6 RID: 26790 RVA: 0x001710BC File Offset: 0x0016F2BC
		public string TargetId { get; private set; }

		// Token: 0x17002510 RID: 9488
		// (get) Token: 0x060068A7 RID: 26791 RVA: 0x001710C5 File Offset: 0x0016F2C5
		// (set) Token: 0x060068A8 RID: 26792 RVA: 0x001710CD File Offset: 0x0016F2CD
		public DirectoryObjectClassAddressList TargetObjectClass { get; private set; }

		// Token: 0x17002511 RID: 9489
		// (get) Token: 0x060068A9 RID: 26793 RVA: 0x001710D6 File Offset: 0x0016F2D6
		// (set) Token: 0x060068AA RID: 26794 RVA: 0x001710DE File Offset: 0x0016F2DE
		public bool TargetDeleted { get; private set; }

		// Token: 0x17002512 RID: 9490
		// (get) Token: 0x060068AB RID: 26795 RVA: 0x001710E7 File Offset: 0x0016F2E7
		// (set) Token: 0x060068AC RID: 26796 RVA: 0x001710EF File Offset: 0x0016F2EF
		public ADObjectId TargetADObjectId { get; private set; }

		// Token: 0x060068AD RID: 26797 RVA: 0x001710F8 File Offset: 0x0016F2F8
		public PropertyReference(string targetId, DirectoryObjectClassAddressList targetObjectClass, bool targetDeleted)
		{
			this.TargetId = targetId;
			this.TargetObjectClass = targetObjectClass;
			this.TargetDeleted = targetDeleted;
		}

		// Token: 0x060068AE RID: 26798 RVA: 0x00171115 File Offset: 0x0016F315
		public PropertyReference(ADObjectId targetADObjectId)
		{
			this.TargetADObjectId = targetADObjectId;
		}

		// Token: 0x060068AF RID: 26799 RVA: 0x00171124 File Offset: 0x0016F324
		public static PropertyReference ParseFromADString(string adString)
		{
			ADObjectId targetADObjectId = ADObjectId.ParseExtendedDN(adString);
			return new PropertyReference(targetADObjectId);
		}

		// Token: 0x060068B0 RID: 26800 RVA: 0x0017113E File Offset: 0x0016F33E
		public void UpdateReferenceData(string targetId, DirectoryObjectClassAddressList targetObjectClass)
		{
			this.TargetId = targetId;
			this.TargetObjectClass = targetObjectClass;
		}

		// Token: 0x060068B1 RID: 26801 RVA: 0x0017114E File Offset: 0x0016F34E
		public override string ToString()
		{
			return this.TargetId ?? string.Empty;
		}

		// Token: 0x060068B2 RID: 26802 RVA: 0x00171160 File Offset: 0x0016F360
		public override bool Equals(object obj)
		{
			PropertyReference propertyReference = obj as PropertyReference;
			return propertyReference != null && (this.TargetId == propertyReference.TargetId && this.TargetDeleted == propertyReference.TargetDeleted && this.TargetObjectClass == propertyReference.TargetObjectClass) && this.TargetADObjectId == propertyReference.TargetADObjectId;
		}

		// Token: 0x060068B3 RID: 26803 RVA: 0x001711B8 File Offset: 0x0016F3B8
		public override int GetHashCode()
		{
			return (int)(((this.TargetId == null) ? 0 : this.TargetId.GetHashCode()) + ((this.TargetADObjectId == null) ? 0 : this.TargetADObjectId.GetHashCode()) + this.TargetObjectClass);
		}

		// Token: 0x060068B4 RID: 26804 RVA: 0x001711EE File Offset: 0x0016F3EE
		public string ToADString()
		{
			if (this.TargetADObjectId != null)
			{
				return this.TargetADObjectId.ToExtendedDN();
			}
			return string.Empty;
		}
	}
}
