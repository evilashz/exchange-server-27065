using System;

namespace Microsoft.Exchange.Data.Providers
{
	// Token: 0x02000005 RID: 5
	internal sealed class AlternateMailboxSchema : SimpleProviderObjectSchema
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000023DE File Offset: 0x000005DE
		private static SimpleProviderPropertyDefinition MakeProperty(string name, ExchangeObjectVersion versionAdded, Type type, PropertyDefinitionFlags flags, object defaultValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints)
		{
			return new SimpleProviderPropertyDefinition(name, versionAdded, type, flags, defaultValue, readConstraints, writeConstraints);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023EF File Offset: 0x000005EF
		private static SimpleProviderPropertyDefinition MakeProperty(string name, Type type, object defaultValue)
		{
			return AlternateMailboxSchema.MakeProperty(name, ExchangeObjectVersion.Exchange2010, type, PropertyDefinitionFlags.None, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002409 File Offset: 0x00000609
		private static SimpleProviderPropertyDefinition MakeProperty(string name, Type type)
		{
			return AlternateMailboxSchema.MakeProperty(name, type, null);
		}

		// Token: 0x04000008 RID: 8
		public static readonly SimpleProviderPropertyDefinition Name = AlternateMailboxSchema.MakeProperty("Name", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new MandatoryStringLengthConstraint(1, 127),
			new CharacterConstraint(new char[]
			{
				'\\',
				'/',
				'=',
				';',
				'\0',
				'\n'
			}, false)
		});

		// Token: 0x04000009 RID: 9
		public static readonly SimpleProviderPropertyDefinition UserDisplayName = AlternateMailboxSchema.MakeProperty("UserDisplayName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new CharacterConstraint(new char[]
			{
				'\\',
				'/',
				'=',
				';',
				'\0',
				'\n'
			}, false)
		});

		// Token: 0x0400000A RID: 10
		public static readonly SimpleProviderPropertyDefinition RetentionPolicyEnabled = AlternateMailboxSchema.MakeProperty("RetentionPolicyEnabled", typeof(bool), false);

		// Token: 0x0400000B RID: 11
		public static readonly SimpleProviderPropertyDefinition Type = AlternateMailboxSchema.MakeProperty("Type", typeof(AlternateMailbox.AlternateMailboxFlags), AlternateMailbox.AlternateMailboxFlags.Unknown);

		// Token: 0x0400000C RID: 12
		public new static readonly SimpleProviderPropertyDefinition Identity = AlternateMailboxSchema.MakeProperty("Identity", typeof(AlternateMailboxObjectId));

		// Token: 0x0400000D RID: 13
		public static readonly SimpleProviderPropertyDefinition DatabaseGuid = AlternateMailboxSchema.MakeProperty("DatabaseGuid", typeof(Guid), Guid.Empty);

		// Token: 0x0400000E RID: 14
		public static readonly SimpleProviderPropertyDefinition MailboxGuid = AlternateMailboxSchema.MakeProperty("Guid", typeof(Guid), Guid.Empty);
	}
}
