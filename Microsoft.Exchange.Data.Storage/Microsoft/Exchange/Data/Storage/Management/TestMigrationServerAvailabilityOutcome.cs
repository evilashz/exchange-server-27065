using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A49 RID: 2633
	[Serializable]
	public class TestMigrationServerAvailabilityOutcome : ConfigurableObject
	{
		// Token: 0x06006052 RID: 24658 RVA: 0x0019644F File Offset: 0x0019464F
		public TestMigrationServerAvailabilityOutcome() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x17001A83 RID: 6787
		// (get) Token: 0x06006053 RID: 24659 RVA: 0x0019645C File Offset: 0x0019465C
		// (set) Token: 0x06006054 RID: 24660 RVA: 0x0019646E File Offset: 0x0019466E
		public TestMigrationServerAvailabilityResult Result
		{
			get
			{
				return (TestMigrationServerAvailabilityResult)this[TestMigrationServerAvailabilityOutcome.TestMigrationServerAvailabilityOutcomeSchema.ResultProperty];
			}
			set
			{
				this[TestMigrationServerAvailabilityOutcome.TestMigrationServerAvailabilityOutcomeSchema.ResultProperty] = value;
			}
		}

		// Token: 0x17001A84 RID: 6788
		// (get) Token: 0x06006055 RID: 24661 RVA: 0x00196481 File Offset: 0x00194681
		// (set) Token: 0x06006056 RID: 24662 RVA: 0x00196493 File Offset: 0x00194693
		public LocalizedString Message
		{
			get
			{
				return (LocalizedString)this[TestMigrationServerAvailabilityOutcome.TestMigrationServerAvailabilityOutcomeSchema.MessageProperty];
			}
			set
			{
				this[TestMigrationServerAvailabilityOutcome.TestMigrationServerAvailabilityOutcomeSchema.MessageProperty] = value;
			}
		}

		// Token: 0x17001A85 RID: 6789
		// (get) Token: 0x06006057 RID: 24663 RVA: 0x001964A6 File Offset: 0x001946A6
		// (set) Token: 0x06006058 RID: 24664 RVA: 0x001964B8 File Offset: 0x001946B8
		public ExchangeConnectionSettings ConnectionSettings
		{
			get
			{
				return (ExchangeConnectionSettings)this[TestMigrationServerAvailabilityOutcome.TestMigrationServerAvailabilityOutcomeSchema.ConnectionSettings];
			}
			set
			{
				this[TestMigrationServerAvailabilityOutcome.TestMigrationServerAvailabilityOutcomeSchema.ConnectionSettings] = value;
			}
		}

		// Token: 0x17001A86 RID: 6790
		// (get) Token: 0x06006059 RID: 24665 RVA: 0x001964C6 File Offset: 0x001946C6
		// (set) Token: 0x0600605A RID: 24666 RVA: 0x001964D8 File Offset: 0x001946D8
		public bool SupportsCutover
		{
			get
			{
				return (bool)this[TestMigrationServerAvailabilityOutcome.TestMigrationServerAvailabilityOutcomeSchema.SupportsCutover];
			}
			set
			{
				this[TestMigrationServerAvailabilityOutcome.TestMigrationServerAvailabilityOutcomeSchema.SupportsCutover] = value;
			}
		}

		// Token: 0x17001A87 RID: 6791
		// (get) Token: 0x0600605B RID: 24667 RVA: 0x001964EB File Offset: 0x001946EB
		// (set) Token: 0x0600605C RID: 24668 RVA: 0x001964FD File Offset: 0x001946FD
		public string ErrorDetail
		{
			get
			{
				return (string)this[TestMigrationServerAvailabilityOutcome.TestMigrationServerAvailabilityOutcomeSchema.ErrorDetail];
			}
			set
			{
				this[TestMigrationServerAvailabilityOutcome.TestMigrationServerAvailabilityOutcomeSchema.ErrorDetail] = value;
			}
		}

		// Token: 0x17001A88 RID: 6792
		// (get) Token: 0x0600605D RID: 24669 RVA: 0x0019650B File Offset: 0x0019470B
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<TestMigrationServerAvailabilityOutcome.TestMigrationServerAvailabilityOutcomeSchema>();
			}
		}

		// Token: 0x17001A89 RID: 6793
		// (get) Token: 0x0600605E RID: 24670 RVA: 0x00196512 File Offset: 0x00194712
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17001A8A RID: 6794
		// (get) Token: 0x0600605F RID: 24671 RVA: 0x00196519 File Offset: 0x00194719
		private new ObjectId Identity
		{
			get
			{
				return base.Identity;
			}
		}

		// Token: 0x06006060 RID: 24672 RVA: 0x00196521 File Offset: 0x00194721
		public override string ToString()
		{
			return this.Result.ToString();
		}

		// Token: 0x06006061 RID: 24673 RVA: 0x00196534 File Offset: 0x00194734
		internal static TestMigrationServerAvailabilityOutcome Create(TestMigrationServerAvailabilityResult result, bool supportsCutover, LocalizedString message, string errorDetail)
		{
			return new TestMigrationServerAvailabilityOutcome
			{
				Result = result,
				SupportsCutover = supportsCutover,
				Message = message,
				ErrorDetail = errorDetail
			};
		}

		// Token: 0x06006062 RID: 24674 RVA: 0x00196564 File Offset: 0x00194764
		internal static TestMigrationServerAvailabilityOutcome Create(TestMigrationServerAvailabilityResult result, bool supportsCutover, ExchangeConnectionSettings connectionSettings)
		{
			TestMigrationServerAvailabilityOutcome testMigrationServerAvailabilityOutcome = new TestMigrationServerAvailabilityOutcome();
			testMigrationServerAvailabilityOutcome.Result = result;
			testMigrationServerAvailabilityOutcome.SupportsCutover = supportsCutover;
			if (connectionSettings != null)
			{
				testMigrationServerAvailabilityOutcome.ConnectionSettings = (ExchangeConnectionSettings)connectionSettings.CloneForPresentation();
			}
			return testMigrationServerAvailabilityOutcome;
		}

		// Token: 0x02000A4A RID: 2634
		private class TestMigrationServerAvailabilityOutcomeSchema : SimpleProviderObjectSchema
		{
			// Token: 0x040036D6 RID: 14038
			public static readonly ProviderPropertyDefinition MessageProperty = new SimpleProviderPropertyDefinition("MessageProperty", ExchangeObjectVersion.Exchange2010, typeof(LocalizedString), PropertyDefinitionFlags.TaskPopulated, LocalizedString.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040036D7 RID: 14039
			public static readonly ProviderPropertyDefinition ResultProperty = new SimpleProviderPropertyDefinition("ResultProperty", ExchangeObjectVersion.Exchange2010, typeof(TestMigrationServerAvailabilityResult), PropertyDefinitionFlags.TaskPopulated, TestMigrationServerAvailabilityResult.Failed, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040036D8 RID: 14040
			public static readonly ProviderPropertyDefinition ConnectionSettings = new SimpleProviderPropertyDefinition("ConnectionSettings", ExchangeObjectVersion.Exchange2010, typeof(ExchangeConnectionSettings), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040036D9 RID: 14041
			public static readonly ProviderPropertyDefinition SupportsCutover = new SimpleProviderPropertyDefinition("SupportsCutover", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040036DA RID: 14042
			public static readonly ProviderPropertyDefinition ErrorDetail = new SimpleProviderPropertyDefinition("ErrorDetail", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}
	}
}
