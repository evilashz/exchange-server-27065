using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A42 RID: 2626
	[Serializable]
	public class MigrationUserSkippedItem : ConfigurableObject
	{
		// Token: 0x0600601E RID: 24606 RVA: 0x00195854 File Offset: 0x00193A54
		public MigrationUserSkippedItem() : base(new SimplePropertyBag(SimpleProviderObjectSchema.Identity, SimpleProviderObjectSchema.ObjectState, SimpleProviderObjectSchema.ExchangeVersion))
		{
			base.SetExchangeVersion(ExchangeObjectVersion.Exchange2010);
			base.ResetChangeTracking();
		}

		// Token: 0x17001A74 RID: 6772
		// (get) Token: 0x0600601F RID: 24607 RVA: 0x00195881 File Offset: 0x00193A81
		// (set) Token: 0x06006020 RID: 24608 RVA: 0x00195893 File Offset: 0x00193A93
		public string Kind
		{
			get
			{
				return (string)this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.Kind];
			}
			internal set
			{
				this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.Kind] = value;
			}
		}

		// Token: 0x17001A75 RID: 6773
		// (get) Token: 0x06006021 RID: 24609 RVA: 0x001958A1 File Offset: 0x00193AA1
		// (set) Token: 0x06006022 RID: 24610 RVA: 0x001958B3 File Offset: 0x00193AB3
		public string FolderName
		{
			get
			{
				return (string)this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.FolderName];
			}
			internal set
			{
				this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.FolderName] = value;
			}
		}

		// Token: 0x17001A76 RID: 6774
		// (get) Token: 0x06006023 RID: 24611 RVA: 0x001958C1 File Offset: 0x00193AC1
		// (set) Token: 0x06006024 RID: 24612 RVA: 0x001958D3 File Offset: 0x00193AD3
		public string Sender
		{
			get
			{
				return (string)this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.Sender];
			}
			internal set
			{
				this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.Sender] = value;
			}
		}

		// Token: 0x17001A77 RID: 6775
		// (get) Token: 0x06006025 RID: 24613 RVA: 0x001958E1 File Offset: 0x00193AE1
		// (set) Token: 0x06006026 RID: 24614 RVA: 0x001958F3 File Offset: 0x00193AF3
		public string Recipient
		{
			get
			{
				return (string)this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.Recipient];
			}
			internal set
			{
				this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.Recipient] = value;
			}
		}

		// Token: 0x17001A78 RID: 6776
		// (get) Token: 0x06006027 RID: 24615 RVA: 0x00195901 File Offset: 0x00193B01
		// (set) Token: 0x06006028 RID: 24616 RVA: 0x00195913 File Offset: 0x00193B13
		public string Subject
		{
			get
			{
				return (string)this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.Subject];
			}
			internal set
			{
				this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.Subject] = value;
			}
		}

		// Token: 0x17001A79 RID: 6777
		// (get) Token: 0x06006029 RID: 24617 RVA: 0x00195921 File Offset: 0x00193B21
		// (set) Token: 0x0600602A RID: 24618 RVA: 0x00195933 File Offset: 0x00193B33
		public string MessageClass
		{
			get
			{
				return (string)this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.MessageClass];
			}
			internal set
			{
				this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.MessageClass] = value;
			}
		}

		// Token: 0x17001A7A RID: 6778
		// (get) Token: 0x0600602B RID: 24619 RVA: 0x00195941 File Offset: 0x00193B41
		// (set) Token: 0x0600602C RID: 24620 RVA: 0x00195953 File Offset: 0x00193B53
		public int? MessageSize
		{
			get
			{
				return (int?)this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.MessageSize];
			}
			internal set
			{
				this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.MessageSize] = value;
			}
		}

		// Token: 0x17001A7B RID: 6779
		// (get) Token: 0x0600602D RID: 24621 RVA: 0x00195966 File Offset: 0x00193B66
		// (set) Token: 0x0600602E RID: 24622 RVA: 0x00195978 File Offset: 0x00193B78
		public DateTime? DateSent
		{
			get
			{
				return (DateTime?)this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.DateSent];
			}
			internal set
			{
				this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.DateSent] = value;
			}
		}

		// Token: 0x17001A7C RID: 6780
		// (get) Token: 0x0600602F RID: 24623 RVA: 0x0019598B File Offset: 0x00193B8B
		// (set) Token: 0x06006030 RID: 24624 RVA: 0x0019599D File Offset: 0x00193B9D
		public DateTime? DateReceived
		{
			get
			{
				return (DateTime?)this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.DateReceived];
			}
			internal set
			{
				this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.DateReceived] = value;
			}
		}

		// Token: 0x17001A7D RID: 6781
		// (get) Token: 0x06006031 RID: 24625 RVA: 0x001959B0 File Offset: 0x00193BB0
		// (set) Token: 0x06006032 RID: 24626 RVA: 0x001959C2 File Offset: 0x00193BC2
		public string Failure
		{
			get
			{
				return (string)this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.Failure];
			}
			internal set
			{
				this[MigrationUserSkippedItem.MigrationUserSkippedItemSchema.Failure] = value;
			}
		}

		// Token: 0x17001A7E RID: 6782
		// (get) Token: 0x06006033 RID: 24627 RVA: 0x001959D0 File Offset: 0x00193BD0
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MigrationUserSkippedItem.schema;
			}
		}

		// Token: 0x06006034 RID: 24628 RVA: 0x001959D7 File Offset: 0x00193BD7
		public override string ToString()
		{
			return ServerStrings.MigrationUserSkippedItemString(this.FolderName, this.Subject);
		}

		// Token: 0x0400369C RID: 13980
		private static MigrationUserSkippedItem.MigrationUserSkippedItemSchema schema = ObjectSchema.GetInstance<MigrationUserSkippedItem.MigrationUserSkippedItemSchema>();

		// Token: 0x02000A43 RID: 2627
		internal class MigrationUserSkippedItemSchema : SimpleProviderObjectSchema
		{
			// Token: 0x0400369D RID: 13981
			public static readonly ProviderPropertyDefinition Kind = new SimpleProviderPropertyDefinition("Kind", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400369E RID: 13982
			public static readonly ProviderPropertyDefinition FolderName = new SimpleProviderPropertyDefinition("FolderName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400369F RID: 13983
			public static readonly ProviderPropertyDefinition Sender = new SimpleProviderPropertyDefinition("Sender", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040036A0 RID: 13984
			public static readonly ProviderPropertyDefinition Recipient = new SimpleProviderPropertyDefinition("Recipient", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040036A1 RID: 13985
			public static readonly ProviderPropertyDefinition Subject = new SimpleProviderPropertyDefinition("Subject", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040036A2 RID: 13986
			public static readonly ProviderPropertyDefinition MessageClass = new SimpleProviderPropertyDefinition("MessageClass", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040036A3 RID: 13987
			public static readonly ProviderPropertyDefinition Failure = new SimpleProviderPropertyDefinition("Failure", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040036A4 RID: 13988
			public static readonly ProviderPropertyDefinition DateSent = new SimpleProviderPropertyDefinition("DateSent", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040036A5 RID: 13989
			public static readonly ProviderPropertyDefinition DateReceived = new SimpleProviderPropertyDefinition("DateReceived", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040036A6 RID: 13990
			public static readonly ProviderPropertyDefinition MessageSize = new SimpleProviderPropertyDefinition("MessageSize", ExchangeObjectVersion.Exchange2010, typeof(int?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}
	}
}
