using System;
using Microsoft.Exchange.Data.ContentTypes.Internal;

namespace Microsoft.Exchange.Data.ContentTypes.vCard
{
	// Token: 0x020000BB RID: 187
	internal class ContactValueTypeContainer : ValueTypeContainer
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x00029627 File Offset: 0x00027827
		public override bool IsTextType
		{
			get
			{
				this.CalculateValueType();
				return this.valueType == ContactValueType.Text || this.valueType == ContactValueType.PhoneNumber || this.valueType == ContactValueType.VCard;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0002964E File Offset: 0x0002784E
		public override bool CanBeMultivalued
		{
			get
			{
				this.CalculateValueType();
				return this.valueType != ContactValueType.Binary && this.valueType != ContactValueType.VCard;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0002966E File Offset: 0x0002786E
		public override bool CanBeCompound
		{
			get
			{
				this.CalculateValueType();
				return this.valueType != ContactValueType.Binary && this.valueType != ContactValueType.VCard;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0002968E File Offset: 0x0002788E
		public ContactValueType ValueType
		{
			get
			{
				this.CalculateValueType();
				return this.valueType;
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0002969C File Offset: 0x0002789C
		private void CalculateValueType()
		{
			if (this.isValueTypeInitialized)
			{
				return;
			}
			this.valueType = ContactValueType.Unknown;
			if (this.valueTypeParameter != null)
			{
				this.valueType = ContactCommon.GetValueTypeEnum(this.valueTypeParameter);
			}
			else
			{
				PropertyId propertyEnum = ContactCommon.GetPropertyEnum(this.propertyName);
				if (propertyEnum != PropertyId.Unknown)
				{
					this.valueType = ContactCommon.GetDefaultValueType(propertyEnum);
				}
			}
			if (this.valueType == ContactValueType.Unknown)
			{
				this.valueType = ContactValueType.Text;
			}
			this.isValueTypeInitialized = true;
		}

		// Token: 0x04000639 RID: 1593
		private ContactValueType valueType;
	}
}
