using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000F6 RID: 246
	public sealed class UserConfigurationPropertyDefinition
	{
		// Token: 0x060008BD RID: 2237 RVA: 0x0001CC7C File Offset: 0x0001AE7C
		internal UserConfigurationPropertyDefinition(string name, Type type, UserConfigurationPropertyDefinition.Validate validate, Guid guid)
		{
			this.name = name;
			this.type = type;
			this.guid = guid;
			this.validate = validate;
			this.hashCode = (this.guid.GetHashCode() ^ this.name.ToLowerInvariant().GetHashCode());
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0001CCD4 File Offset: 0x0001AED4
		internal UserConfigurationPropertyDefinition(string name, Type type, UserConfigurationPropertyDefinition.Validate validate) : this(name, type, validate, UserConfigurationPropertyDefinition.publicStringsGuid)
		{
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001CCE4 File Offset: 0x0001AEE4
		internal UserConfigurationPropertyDefinition(string name, Type type) : this(name, type, new UserConfigurationPropertyDefinition.Validate(UserConfigurationPropertyDefinition.DefaultValidateCallback), UserConfigurationPropertyDefinition.publicStringsGuid)
		{
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x0001CCFF File Offset: 0x0001AEFF
		internal UserConfigurationPropertyDefinition.Validate GetValidatedProperty
		{
			get
			{
				return this.validate;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0001CD07 File Offset: 0x0001AF07
		internal string PropertyName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0001CD0F File Offset: 0x0001AF0F
		internal Type PropertyType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001CD17 File Offset: 0x0001AF17
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001CD20 File Offset: 0x0001AF20
		public override bool Equals(object value)
		{
			UserConfigurationPropertyDefinition userConfigurationPropertyDefinition = value as UserConfigurationPropertyDefinition;
			return userConfigurationPropertyDefinition != null && (string.Equals(this.name, userConfigurationPropertyDefinition.name, StringComparison.OrdinalIgnoreCase) && this.guid.Equals(userConfigurationPropertyDefinition.guid)) && this.type.Equals(userConfigurationPropertyDefinition.type);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001CD76 File Offset: 0x0001AF76
		private static object DefaultValidateCallback(object value)
		{
			return value;
		}

		// Token: 0x04000565 RID: 1381
		private static readonly Guid publicStringsGuid = new Guid("00020329-0000-0000-C000-000000000046");

		// Token: 0x04000566 RID: 1382
		private readonly int hashCode;

		// Token: 0x04000567 RID: 1383
		private readonly string name;

		// Token: 0x04000568 RID: 1384
		private readonly Guid guid;

		// Token: 0x04000569 RID: 1385
		private UserConfigurationPropertyDefinition.Validate validate;

		// Token: 0x0400056A RID: 1386
		private Type type;

		// Token: 0x020000F7 RID: 247
		// (Invoke) Token: 0x060008C8 RID: 2248
		internal delegate object Validate(object originalValue);
	}
}
