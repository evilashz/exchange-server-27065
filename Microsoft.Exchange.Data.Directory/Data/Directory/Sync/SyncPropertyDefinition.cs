using System;
using System.Globalization;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000832 RID: 2098
	[Serializable]
	internal class SyncPropertyDefinition : ADPropertyDefinition
	{
		// Token: 0x170024D0 RID: 9424
		// (get) Token: 0x0600680B RID: 26635 RVA: 0x0016EAA3 File Offset: 0x0016CCA3
		public new SyncPropertyDefinitionFlags Flags
		{
			get
			{
				return (SyncPropertyDefinitionFlags)base.Flags;
			}
		}

		// Token: 0x170024D1 RID: 9425
		// (get) Token: 0x0600680C RID: 26636 RVA: 0x0016EAAB File Offset: 0x0016CCAB
		// (set) Token: 0x0600680D RID: 26637 RVA: 0x0016EAB3 File Offset: 0x0016CCB3
		public string MsoPropertyName { get; private set; }

		// Token: 0x170024D2 RID: 9426
		// (get) Token: 0x0600680E RID: 26638 RVA: 0x0016EABC File Offset: 0x0016CCBC
		// (set) Token: 0x0600680F RID: 26639 RVA: 0x0016EAC4 File Offset: 0x0016CCC4
		public ServerVersion SyncPropertyVersionAdded { get; private set; }

		// Token: 0x170024D3 RID: 9427
		// (get) Token: 0x06006810 RID: 26640 RVA: 0x0016EACD File Offset: 0x0016CCCD
		// (set) Token: 0x06006811 RID: 26641 RVA: 0x0016EAD5 File Offset: 0x0016CCD5
		public Func<bool> ShouldProcess { get; set; }

		// Token: 0x06006812 RID: 26642 RVA: 0x0016EADE File Offset: 0x0016CCDE
		internal SyncPropertyDefinition(ADPropertyDefinition aDPropertyDefinition, string msoPropertyName, Type externalType, SyncPropertyDefinitionFlags flags, ServerVersion versionAdded) : this(aDPropertyDefinition, msoPropertyName, aDPropertyDefinition.Type, externalType, flags, versionAdded)
		{
		}

		// Token: 0x06006813 RID: 26643 RVA: 0x0016EAF4 File Offset: 0x0016CCF4
		public SyncPropertyDefinition(ADPropertyDefinition aDPropertyDefinition, string msoPropertyName, Type type, Type externalType, SyncPropertyDefinitionFlags flags, ServerVersion versionAdded) : this(aDPropertyDefinition.Name, msoPropertyName, aDPropertyDefinition.VersionAdded, type, externalType, aDPropertyDefinition.LdapDisplayName, aDPropertyDefinition.Flags, flags, versionAdded, aDPropertyDefinition.DefaultValue, SyncPropertyDefinition.ConvertReadOnlyCollectionToArray<ProviderPropertyDefinition>(aDPropertyDefinition.SupportingProperties), aDPropertyDefinition.CustomFilterBuilderDelegate, aDPropertyDefinition.GetterDelegate, aDPropertyDefinition.SetterDelegate, aDPropertyDefinition.ShadowProperty)
		{
		}

		// Token: 0x06006814 RID: 26644 RVA: 0x0016EB50 File Offset: 0x0016CD50
		public SyncPropertyDefinition(string name, string msoPropertyName, Type type, Type externalType, SyncPropertyDefinitionFlags flags, ServerVersion versionAdded, object defaultValue) : this(name, msoPropertyName, type, externalType, flags, versionAdded, defaultValue, ProviderPropertyDefinition.None, null, null)
		{
		}

		// Token: 0x06006815 RID: 26645 RVA: 0x0016EB78 File Offset: 0x0016CD78
		public SyncPropertyDefinition(string name, string msoPropertyName, Type type, Type externalType, SyncPropertyDefinitionFlags flags, ServerVersion versionAdded, object defaultValue, ProviderPropertyDefinition[] supportingProperties, GetterDelegate getterDelegate, SetterDelegate setterDelegate) : this(name, msoPropertyName, ExchangeObjectVersion.Exchange2010, type, externalType, null, ADPropertyDefinitionFlags.None, flags, versionAdded, defaultValue, supportingProperties, new CustomFilterBuilderDelegate(ADObject.DummyCustomFilterBuilderDelegate), getterDelegate, setterDelegate, null)
		{
		}

		// Token: 0x06006816 RID: 26646 RVA: 0x0016EBB0 File Offset: 0x0016CDB0
		private SyncPropertyDefinition(string name, string msoPropertyName, ExchangeObjectVersion versionAdded, Type type, Type externalType, string ldapDisplayName, ADPropertyDefinitionFlags flags, SyncPropertyDefinitionFlags syncFlags, ServerVersion syncVersionAdded, object defaultValue, ProviderPropertyDefinition[] supportingProperties, CustomFilterBuilderDelegate customFilterBuilderDelegate, GetterDelegate getterDelegate, SetterDelegate setterDelegate, ADPropertyDefinition shadowProperty) : base(name, versionAdded, type, ldapDisplayName, SyncPropertyDefinition.CalculateFlags(ldapDisplayName, (ADPropertyDefinitionFlags)(syncFlags | (SyncPropertyDefinitionFlags)flags)), defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, supportingProperties, customFilterBuilderDelegate, getterDelegate, setterDelegate, null, null)
		{
			this.externalType = externalType;
			this.MsoPropertyName = msoPropertyName;
			this.SyncPropertyVersionAdded = syncVersionAdded;
			SyncPropertyDefinitionFlags syncPropertyDefinitionFlags = syncFlags & ~(SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.BackSync);
			syncPropertyDefinitionFlags |= SyncPropertyDefinitionFlags.Shadow;
			if (shadowProperty != null)
			{
				Type type2 = shadowProperty.Type;
				if (type2 == typeof(ADObjectId))
				{
					type2 = typeof(SyncLink);
				}
				this.shadowProperty = new SyncPropertyDefinition(shadowProperty, msoPropertyName, type2, externalType, syncPropertyDefinitionFlags, syncVersionAdded);
				return;
			}
			if (this.IsBackSync && base.SupportingProperties.Count == 1 && ((ADPropertyDefinition)base.SupportingProperties[0]).ShadowProperty != null)
			{
				this.shadowProperty = new SyncPropertyDefinition(string.Format(CultureInfo.InvariantCulture, "Shadow{0}", new object[]
				{
					base.Name
				}), msoPropertyName, base.Type, this.ExternalType, syncPropertyDefinitionFlags, syncVersionAdded, base.DefaultValue, new ProviderPropertyDefinition[]
				{
					((ADPropertyDefinition)base.SupportingProperties[0]).ShadowProperty
				}, base.GetterDelegate, base.SetterDelegate);
			}
		}

		// Token: 0x06006817 RID: 26647 RVA: 0x0016ECF4 File Offset: 0x0016CEF4
		private static T[] ConvertReadOnlyCollectionToArray<T>(ReadOnlyCollection<T> collection)
		{
			T[] array = new T[collection.Count];
			collection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06006818 RID: 26648 RVA: 0x0016ED16 File Offset: 0x0016CF16
		private static ADPropertyDefinitionFlags CalculateFlags(string ldapDisplayName, ADPropertyDefinitionFlags flags)
		{
			if ((flags & ADPropertyDefinitionFlags.Calculated) == ADPropertyDefinitionFlags.None && string.IsNullOrEmpty(ldapDisplayName))
			{
				return flags | ADPropertyDefinitionFlags.TaskPopulated;
			}
			return flags;
		}

		// Token: 0x170024D4 RID: 9428
		// (get) Token: 0x06006819 RID: 26649 RVA: 0x0016ED2E File Offset: 0x0016CF2E
		public bool IsForwardSync
		{
			get
			{
				return (this.Flags & SyncPropertyDefinitionFlags.ForwardSync) != (SyncPropertyDefinitionFlags)0;
			}
		}

		// Token: 0x170024D5 RID: 9429
		// (get) Token: 0x0600681A RID: 26650 RVA: 0x0016ED42 File Offset: 0x0016CF42
		public bool IsBackSync
		{
			get
			{
				return (this.Flags & SyncPropertyDefinitionFlags.BackSync) != (SyncPropertyDefinitionFlags)0;
			}
		}

		// Token: 0x170024D6 RID: 9430
		// (get) Token: 0x0600681B RID: 26651 RVA: 0x0016ED56 File Offset: 0x0016CF56
		public bool IsCloud
		{
			get
			{
				return (this.Flags & SyncPropertyDefinitionFlags.Cloud) != (SyncPropertyDefinitionFlags)0;
			}
		}

		// Token: 0x170024D7 RID: 9431
		// (get) Token: 0x0600681C RID: 26652 RVA: 0x0016ED6A File Offset: 0x0016CF6A
		public bool IsAlwaysReturned
		{
			get
			{
				return (this.Flags & SyncPropertyDefinitionFlags.AlwaysReturned) != (SyncPropertyDefinitionFlags)0;
			}
		}

		// Token: 0x170024D8 RID: 9432
		// (get) Token: 0x0600681D RID: 26653 RVA: 0x0016ED7E File Offset: 0x0016CF7E
		public bool IsNotInMsoDirectory
		{
			get
			{
				return (this.Flags & SyncPropertyDefinitionFlags.NotInMsoDirectory) != (SyncPropertyDefinitionFlags)0;
			}
		}

		// Token: 0x170024D9 RID: 9433
		// (get) Token: 0x0600681E RID: 26654 RVA: 0x0016ED92 File Offset: 0x0016CF92
		public bool IsShadow
		{
			get
			{
				return (this.Flags & SyncPropertyDefinitionFlags.Shadow) != (SyncPropertyDefinitionFlags)0;
			}
		}

		// Token: 0x170024DA RID: 9434
		// (get) Token: 0x0600681F RID: 26655 RVA: 0x0016EDA6 File Offset: 0x0016CFA6
		public bool IsFilteringOnly
		{
			get
			{
				return (this.Flags & SyncPropertyDefinitionFlags.FilteringOnly) != (SyncPropertyDefinitionFlags)0;
			}
		}

		// Token: 0x170024DB RID: 9435
		// (get) Token: 0x06006820 RID: 26656 RVA: 0x0016EDBA File Offset: 0x0016CFBA
		public bool IsSyncLink
		{
			get
			{
				return base.Type == typeof(SyncLink);
			}
		}

		// Token: 0x170024DC RID: 9436
		// (get) Token: 0x06006821 RID: 26657 RVA: 0x0016EDD1 File Offset: 0x0016CFD1
		public Type ExternalType
		{
			get
			{
				return this.externalType;
			}
		}

		// Token: 0x04004478 RID: 17528
		private Type externalType;

		// Token: 0x04004479 RID: 17529
		public static ServerVersion InitialSyncPropertySetVersion = new ServerVersion(14, 15, 0, 0);

		// Token: 0x0400447A RID: 17530
		public static ServerVersion SyncPropertySetVersion3 = new ServerVersion(15, 0, 377, 0);

		// Token: 0x0400447B RID: 17531
		public static ServerVersion SyncPropertySetVersion4 = new ServerVersion(15, 0, 414, 0);

		// Token: 0x0400447C RID: 17532
		public static ServerVersion SyncPropertySetVersion6 = new ServerVersion(15, 0, 510, 0);

		// Token: 0x0400447D RID: 17533
		public static ServerVersion SyncPropertySetVersion8 = new ServerVersion(15, 0, 548, 0);

		// Token: 0x0400447E RID: 17534
		public static ServerVersion SyncPropertySetVersion9 = new ServerVersion(15, 0, 562, 0);

		// Token: 0x0400447F RID: 17535
		public static ServerVersion SyncPropertySetVersion10 = new ServerVersion(15, 0, 564, 0);

		// Token: 0x04004480 RID: 17536
		public static ServerVersion SyncPropertySetVersion11 = new ServerVersion(15, 0, 565, 0);

		// Token: 0x04004481 RID: 17537
		public static ServerVersion SyncPropertySetVersion12 = new ServerVersion(15, 0, 810, 0);

		// Token: 0x04004482 RID: 17538
		public static ServerVersion SyncPropertySetVersion13 = new ServerVersion(15, 0, 827, 0);

		// Token: 0x04004483 RID: 17539
		public static ServerVersion SyncPropertySetVersion14 = new ServerVersion(15, 0, 842, 0);

		// Token: 0x04004484 RID: 17540
		public static ServerVersion SyncPropertySetVersion15 = new ServerVersion(15, 0, 885, 0);

		// Token: 0x04004485 RID: 17541
		public static ServerVersion SyncPropertySetVersion16 = new ServerVersion(15, 0, 907, 0);

		// Token: 0x04004486 RID: 17542
		public static ServerVersion SyncPropertySetVersion17 = new ServerVersion(15, 0, 946, 0);

		// Token: 0x04004487 RID: 17543
		public static ServerVersion SyncPropertySetVersion18 = new ServerVersion(15, 0, 976, 0);

		// Token: 0x04004488 RID: 17544
		public static ServerVersion SyncPropertySetVersion19 = new ServerVersion(15, 0, 1000, 0);

		// Token: 0x04004489 RID: 17545
		public static ServerVersion IgnoredSyncPropertySetVersion = new ServerVersion(1, 1, 1, 1);
	}
}
