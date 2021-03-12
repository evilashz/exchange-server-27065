using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Data.Providers
{
	// Token: 0x02000006 RID: 6
	internal sealed class MailMessageSchema : SimpleProviderObjectSchema
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002587 File Offset: 0x00000787
		private static SimpleProviderPropertyDefinition MakeProperty(string name, ExchangeObjectVersion versionAdded, Type type, PropertyDefinitionFlags flags, object defaultValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints)
		{
			return new SimpleProviderPropertyDefinition(name, versionAdded, type, flags, defaultValue, readConstraints, writeConstraints);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002598 File Offset: 0x00000798
		private static SimpleProviderPropertyDefinition MakeProperty(string name, Type type, object defaultValue)
		{
			return MailMessageSchema.MakeProperty(name, ExchangeObjectVersion.Exchange2010, type, PropertyDefinitionFlags.None, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025B2 File Offset: 0x000007B2
		private static SimpleProviderPropertyDefinition MakeProperty(string name, Type type)
		{
			return MailMessageSchema.MakeProperty(name, type, null);
		}

		// Token: 0x0400000F RID: 15
		public static readonly SimpleProviderPropertyDefinition Subject = MailMessageSchema.MakeProperty("Subject", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 255)
		});

		// Token: 0x04000010 RID: 16
		public static readonly SimpleProviderPropertyDefinition Body = MailMessageSchema.MakeProperty("Body", typeof(string), string.Empty);

		// Token: 0x04000011 RID: 17
		public static readonly SimpleProviderPropertyDefinition BodyFormat = MailMessageSchema.MakeProperty("BodyFormat", typeof(MailBodyFormat), MailBodyFormat.PlainText);

		// Token: 0x04000012 RID: 18
		public new static readonly SimpleProviderPropertyDefinition Identity = MailMessageSchema.MakeProperty("Identity", typeof(StoreObjectId));
	}
}
