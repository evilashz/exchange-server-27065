using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000281 RID: 641
	internal sealed class UserOptionPropertyDefinition
	{
		// Token: 0x06001676 RID: 5750 RVA: 0x00083E3C File Offset: 0x0008203C
		internal UserOptionPropertyDefinition(string name, Type type, UserOptionPropertyDefinition.Validate validate, Guid guid)
		{
			this.name = name;
			this.type = type;
			this.guid = guid;
			this.validate = validate;
			this.hashCode = (this.guid.GetHashCode() ^ this.name.GetHashCode());
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x00083E8F File Offset: 0x0008208F
		internal UserOptionPropertyDefinition(string name, Type type, UserOptionPropertyDefinition.Validate validate) : this(name, type, validate, UserOptionPropertyDefinition.publicStringsGuid)
		{
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x00083E9F File Offset: 0x0008209F
		internal UserOptionPropertyDefinition(string name, Type type) : this(name, type, new UserOptionPropertyDefinition.Validate(UserOptionPropertyDefinition.DefaultValidateCallback), UserOptionPropertyDefinition.publicStringsGuid)
		{
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x00083EBA File Offset: 0x000820BA
		internal UserOptionPropertyDefinition.Validate GetValidatedProperty
		{
			get
			{
				return this.validate;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x00083EC2 File Offset: 0x000820C2
		internal string PropertyName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x00083ECA File Offset: 0x000820CA
		internal Type PropertyType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00083ED2 File Offset: 0x000820D2
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00083EDC File Offset: 0x000820DC
		public override bool Equals(object value)
		{
			UserOptionPropertyDefinition userOptionPropertyDefinition = value as UserOptionPropertyDefinition;
			return userOptionPropertyDefinition != null && (string.Equals(this.name, userOptionPropertyDefinition.name, StringComparison.OrdinalIgnoreCase) && this.guid.Equals(userOptionPropertyDefinition.guid)) && this.type.Equals(userOptionPropertyDefinition.type);
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00083F2F File Offset: 0x0008212F
		private static object DefaultValidateCallback(object value)
		{
			return value;
		}

		// Token: 0x0400116B RID: 4459
		private static readonly Guid publicStringsGuid = new Guid("00020329-0000-0000-C000-000000000046");

		// Token: 0x0400116C RID: 4460
		private UserOptionPropertyDefinition.Validate validate;

		// Token: 0x0400116D RID: 4461
		private int hashCode;

		// Token: 0x0400116E RID: 4462
		private string name;

		// Token: 0x0400116F RID: 4463
		private Type type;

		// Token: 0x04001170 RID: 4464
		private Guid guid;

		// Token: 0x02000282 RID: 642
		// (Invoke) Token: 0x06001681 RID: 5761
		internal delegate object Validate(object originalValue);
	}
}
