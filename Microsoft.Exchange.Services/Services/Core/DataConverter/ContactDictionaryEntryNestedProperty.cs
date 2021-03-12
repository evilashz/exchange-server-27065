using System;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000140 RID: 320
	internal class ContactDictionaryEntryNestedProperty : ContactDictionaryEntryProperty
	{
		// Token: 0x060008C0 RID: 2240 RVA: 0x0002AEA5 File Offset: 0x000290A5
		private ContactDictionaryEntryNestedProperty(CommandContext commandContext, string[] xmlNestedLocalNames) : base(commandContext, xmlNestedLocalNames)
		{
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0002AEAF File Offset: 0x000290AF
		private static ContactDictionaryEntryNestedProperty CreateCommand(CommandContext commandContext, string[] xmlNestedElements)
		{
			return new ContactDictionaryEntryNestedProperty(commandContext, xmlNestedElements);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0002AEB8 File Offset: 0x000290B8
		public static ContactDictionaryEntryNestedProperty CreateCommandForStreet(CommandContext commandContext)
		{
			return ContactDictionaryEntryNestedProperty.CreateCommand(commandContext, new string[]
			{
				"PhysicalAddresses",
				"Entry",
				"Street"
			});
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0002AEEC File Offset: 0x000290EC
		public static ContactDictionaryEntryNestedProperty CreateCommandForCity(CommandContext commandContext)
		{
			return ContactDictionaryEntryNestedProperty.CreateCommand(commandContext, new string[]
			{
				"PhysicalAddresses",
				"Entry",
				"City"
			});
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0002AF20 File Offset: 0x00029120
		public static ContactDictionaryEntryNestedProperty CreateCommandForState(CommandContext commandContext)
		{
			return ContactDictionaryEntryNestedProperty.CreateCommand(commandContext, new string[]
			{
				"PhysicalAddresses",
				"Entry",
				"State"
			});
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0002AF54 File Offset: 0x00029154
		public static ContactDictionaryEntryNestedProperty CreateCommandForCountryOrRegion(CommandContext commandContext)
		{
			return ContactDictionaryEntryNestedProperty.CreateCommand(commandContext, new string[]
			{
				"PhysicalAddresses",
				"Entry",
				"CountryOrRegion"
			});
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0002AF88 File Offset: 0x00029188
		public static ContactDictionaryEntryNestedProperty CreateCommandForPostalCode(CommandContext commandContext)
		{
			return ContactDictionaryEntryNestedProperty.CreateCommand(commandContext, new string[]
			{
				"PhysicalAddresses",
				"Entry",
				"PostalCode"
			});
		}
	}
}
