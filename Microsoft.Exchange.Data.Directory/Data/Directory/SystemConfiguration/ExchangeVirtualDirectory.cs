using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000323 RID: 803
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public class ExchangeVirtualDirectory : ADVirtualDirectory
	{
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06002537 RID: 9527 RVA: 0x0009E643 File Offset: 0x0009C843
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x0009E64A File Offset: 0x0009C84A
		internal override ADObjectSchema Schema
		{
			get
			{
				return ExchangeVirtualDirectory.schema;
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x0009E651 File Offset: 0x0009C851
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x0600253A RID: 9530 RVA: 0x0009E658 File Offset: 0x0009C858
		// (set) Token: 0x0600253B RID: 9531 RVA: 0x0009E660 File Offset: 0x0009C860
		internal bool ADPropertiesOnly { get; set; }

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x0009E669 File Offset: 0x0009C869
		// (set) Token: 0x0600253D RID: 9533 RVA: 0x0009E67B File Offset: 0x0009C87B
		public string MetabasePath
		{
			get
			{
				return (string)this[ExchangeVirtualDirectorySchema.MetabasePath];
			}
			internal set
			{
				this[ExchangeVirtualDirectorySchema.MetabasePath] = value;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600253E RID: 9534 RVA: 0x0009E689 File Offset: 0x0009C889
		// (set) Token: 0x0600253F RID: 9535 RVA: 0x0009E691 File Offset: 0x0009C891
		public string Path
		{
			get
			{
				return this.path;
			}
			internal set
			{
				this.path = (value ?? string.Empty);
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06002540 RID: 9536 RVA: 0x0009E6A3 File Offset: 0x0009C8A3
		// (set) Token: 0x06002541 RID: 9537 RVA: 0x0009E6B5 File Offset: 0x0009C8B5
		public ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
		{
			get
			{
				return (ExtendedProtectionTokenCheckingMode)this[ExchangeVirtualDirectorySchema.ExtendedProtectionTokenChecking];
			}
			internal set
			{
				this[ExchangeVirtualDirectorySchema.ExtendedProtectionTokenChecking] = value;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x0009E6C8 File Offset: 0x0009C8C8
		// (set) Token: 0x06002543 RID: 9539 RVA: 0x0009E6DF File Offset: 0x0009C8DF
		public MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
		{
			get
			{
				return ExchangeVirtualDirectory.ExtendedProtectionFlagsToMultiValuedProperty((ExtendedProtectionFlag)this[ExchangeVirtualDirectorySchema.ExtendedProtectionFlags]);
			}
			internal set
			{
				this[ExchangeVirtualDirectorySchema.ExtendedProtectionFlags] = ExchangeVirtualDirectory.ExtendedProtectionMultiValuedPropertyToFlags(value);
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06002544 RID: 9540 RVA: 0x0009E6F7 File Offset: 0x0009C8F7
		// (set) Token: 0x06002545 RID: 9541 RVA: 0x0009E709 File Offset: 0x0009C909
		public MultiValuedProperty<string> ExtendedProtectionSPNList
		{
			get
			{
				return (MultiValuedProperty<string>)this[ExchangeVirtualDirectorySchema.ExtendedProtectionSPNList];
			}
			internal set
			{
				this[ExchangeVirtualDirectorySchema.ExtendedProtectionSPNList] = value;
			}
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x0009E718 File Offset: 0x0009C918
		internal static MultiValuedProperty<string> RemoveDNStringSyntax(MultiValuedProperty<string> objectDNString, ProviderPropertyDefinition propertyDefinition)
		{
			if (objectDNString == null)
			{
				return objectDNString;
			}
			string[] array = objectDNString.ToArray();
			char[] separator = new char[]
			{
				':'
			};
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				string[] array2 = text.Split(separator);
				int length = int.Parse(array2[1]);
				int startIndex = text.IndexOf(':', 2) + 1;
				array[i] = text.Substring(startIndex, length);
			}
			return new MultiValuedProperty<string>(false, propertyDefinition, array);
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x0009E790 File Offset: 0x0009C990
		internal static MultiValuedProperty<string> AddDNStringSyntax(MultiValuedProperty<string> objectDNString, ProviderPropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (objectDNString == null)
			{
				return objectDNString;
			}
			string[] array = objectDNString.ToArray();
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			string distinguishedName = adobjectId.DistinguishedName;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = string.Concat(new string[]
				{
					"S:",
					array[i].Length.ToString(),
					":",
					array[i],
					":",
					distinguishedName
				});
			}
			return new MultiValuedProperty<string>(false, propertyDefinition, array);
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x0009E82C File Offset: 0x0009CA2C
		internal static MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlagsToMultiValuedProperty(ExtendedProtectionFlag flags)
		{
			if (flags == ExtendedProtectionFlag.None)
			{
				return ExchangeVirtualDirectory.EmptyExtendedProtectionFlagsPropertyValue;
			}
			MultiValuedProperty<ExtendedProtectionFlag> multiValuedProperty = new MultiValuedProperty<ExtendedProtectionFlag>();
			foreach (ExtendedProtectionFlag extendedProtectionFlag in ExchangeVirtualDirectory.extendedProtectionFlagMasks)
			{
				if ((flags & extendedProtectionFlag) == extendedProtectionFlag)
				{
					multiValuedProperty.Add(extendedProtectionFlag);
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x0009E870 File Offset: 0x0009CA70
		internal static ExtendedProtectionFlag ExtendedProtectionMultiValuedPropertyToFlags(MultiValuedProperty<ExtendedProtectionFlag> flagsCollection)
		{
			ExtendedProtectionFlag extendedProtectionFlag = ExtendedProtectionFlag.None;
			if (flagsCollection != null)
			{
				foreach (ExtendedProtectionFlag extendedProtectionFlag2 in flagsCollection)
				{
					extendedProtectionFlag |= extendedProtectionFlag2;
				}
			}
			return extendedProtectionFlag;
		}

		// Token: 0x040016E3 RID: 5859
		private static readonly ExchangeVirtualDirectorySchema schema = ObjectSchema.GetInstance<ExchangeVirtualDirectorySchema>();

		// Token: 0x040016E4 RID: 5860
		private static readonly MultiValuedProperty<ExtendedProtectionFlag> EmptyExtendedProtectionFlagsPropertyValue = new MultiValuedProperty<ExtendedProtectionFlag>();

		// Token: 0x040016E5 RID: 5861
		private static ExtendedProtectionFlag[] extendedProtectionFlagMasks = new ExtendedProtectionFlag[]
		{
			ExtendedProtectionFlag.Proxy,
			ExtendedProtectionFlag.NoServiceNameCheck,
			ExtendedProtectionFlag.ProxyCohosting,
			ExtendedProtectionFlag.AllowDotlessSpn
		};

		// Token: 0x040016E6 RID: 5862
		private string path = string.Empty;
	}
}
