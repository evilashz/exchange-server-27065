using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004A2 RID: 1186
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ContactExtendedPropertyData : IEquatable<ContactExtendedPropertyData>
	{
		// Token: 0x060034BB RID: 13499 RVA: 0x000D5588 File Offset: 0x000D3788
		public ContactExtendedPropertyData(PropertyDefinition definition, object rawValue)
		{
			this.definition = definition;
			this.rawValue = rawValue;
		}

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x060034BC RID: 13500 RVA: 0x000D559E File Offset: 0x000D379E
		public PropertyDefinition PropertyDefinition
		{
			get
			{
				return this.definition;
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x060034BD RID: 13501 RVA: 0x000D55A6 File Offset: 0x000D37A6
		public object RawValue
		{
			get
			{
				return this.rawValue;
			}
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x000D55AE File Offset: 0x000D37AE
		public bool Equals(ContactExtendedPropertyData other)
		{
			return other != null && other.definition == this.definition && other.rawValue.ToString().Equals(this.rawValue.ToString());
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x000D55E0 File Offset: 0x000D37E0
		public override bool Equals(object other)
		{
			return this.Equals(other as ContactExtendedPropertyData);
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x000D55EE File Offset: 0x000D37EE
		public override int GetHashCode()
		{
			return this.definition.GetHashCode() ^ this.rawValue.GetHashCode();
		}

		// Token: 0x04001C0F RID: 7183
		private readonly PropertyDefinition definition;

		// Token: 0x04001C10 RID: 7184
		private readonly object rawValue;
	}
}
