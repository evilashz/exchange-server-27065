using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security.AccessControl;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004B4 RID: 1204
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class MessageClassification : ADConfigurationObject
	{
		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x060036EC RID: 14060 RVA: 0x000D7363 File Offset: 0x000D5563
		internal override ADObjectSchema Schema
		{
			get
			{
				return MessageClassification.schema;
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x060036ED RID: 14061 RVA: 0x000D736C File Offset: 0x000D556C
		public RawSecurityDescriptor SharedSecurityDescriptor
		{
			get
			{
				if (this.sharedSecurityDescriptor == null)
				{
					RawSecurityDescriptor rawSecurityDescriptor = base.ReadSecurityDescriptor();
					string sddlForm = rawSecurityDescriptor.GetSddlForm(AccessControlSections.Access);
					lock (MessageClassification.securityDescriptorTable)
					{
						if (!MessageClassification.securityDescriptorTable.TryGetValue(sddlForm, out this.sharedSecurityDescriptor))
						{
							MessageClassification.securityDescriptorTable[sddlForm] = rawSecurityDescriptor;
							this.sharedSecurityDescriptor = rawSecurityDescriptor;
						}
					}
				}
				return this.sharedSecurityDescriptor;
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x060036EE RID: 14062 RVA: 0x000D73E8 File Offset: 0x000D55E8
		internal override string MostDerivedObjectClass
		{
			get
			{
				return MessageClassification.mostDerivedClass;
			}
		}

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x060036EF RID: 14063 RVA: 0x000D73EF File Offset: 0x000D55EF
		internal override ADObjectId ParentPath
		{
			get
			{
				return MessageClassification.parentPath;
			}
		}

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x060036F0 RID: 14064 RVA: 0x000D73F6 File Offset: 0x000D55F6
		internal override bool IsShareable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x060036F1 RID: 14065 RVA: 0x000D73F9 File Offset: 0x000D55F9
		// (set) Token: 0x060036F2 RID: 14066 RVA: 0x000D740B File Offset: 0x000D560B
		[Parameter(Mandatory = false)]
		public Guid ClassificationID
		{
			get
			{
				return (Guid)this[ClassificationSchema.ClassificationID];
			}
			internal set
			{
				this[ClassificationSchema.ClassificationID] = value;
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x060036F3 RID: 14067 RVA: 0x000D741E File Offset: 0x000D561E
		// (set) Token: 0x060036F4 RID: 14068 RVA: 0x000D7430 File Offset: 0x000D5630
		public int Version
		{
			get
			{
				return (int)this[ClassificationSchema.Version];
			}
			internal set
			{
				this[ClassificationSchema.Version] = value;
			}
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x060036F5 RID: 14069 RVA: 0x000D7443 File Offset: 0x000D5643
		// (set) Token: 0x060036F6 RID: 14070 RVA: 0x000D7455 File Offset: 0x000D5655
		[Parameter(Mandatory = false)]
		public ClassificationDisplayPrecedenceLevel DisplayPrecedence
		{
			get
			{
				return (ClassificationDisplayPrecedenceLevel)this[ClassificationSchema.DisplayPrecedence];
			}
			set
			{
				this[ClassificationSchema.DisplayPrecedence] = value;
			}
		}

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x060036F7 RID: 14071 RVA: 0x000D7468 File Offset: 0x000D5668
		public string Locale
		{
			get
			{
				return (string)this[ClassificationSchema.Locale];
			}
		}

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x060036F8 RID: 14072 RVA: 0x000D747A File Offset: 0x000D567A
		public bool IsDefault
		{
			get
			{
				return this[ClassificationSchema.Locale] == null;
			}
		}

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x060036F9 RID: 14073 RVA: 0x000D748A File Offset: 0x000D568A
		// (set) Token: 0x060036FA RID: 14074 RVA: 0x000D749C File Offset: 0x000D569C
		[Parameter(Mandatory = false)]
		public string DisplayName
		{
			get
			{
				return (string)this[ClassificationSchema.DisplayName];
			}
			set
			{
				this[ClassificationSchema.DisplayName] = value;
			}
		}

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x060036FB RID: 14075 RVA: 0x000D74AA File Offset: 0x000D56AA
		// (set) Token: 0x060036FC RID: 14076 RVA: 0x000D74BC File Offset: 0x000D56BC
		[Parameter(Mandatory = false)]
		public string SenderDescription
		{
			get
			{
				return (string)this[ClassificationSchema.SenderDescription];
			}
			set
			{
				this[ClassificationSchema.SenderDescription] = value;
			}
		}

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x060036FD RID: 14077 RVA: 0x000D74CA File Offset: 0x000D56CA
		// (set) Token: 0x060036FE RID: 14078 RVA: 0x000D74DC File Offset: 0x000D56DC
		[Parameter(Mandatory = false)]
		public string RecipientDescription
		{
			get
			{
				return (string)this[ClassificationSchema.RecipientDescription];
			}
			set
			{
				this[ClassificationSchema.RecipientDescription] = value;
			}
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x060036FF RID: 14079 RVA: 0x000D74EA File Offset: 0x000D56EA
		// (set) Token: 0x06003700 RID: 14080 RVA: 0x000D74FC File Offset: 0x000D56FC
		[Parameter(Mandatory = false)]
		public bool PermissionMenuVisible
		{
			get
			{
				return (bool)this[ClassificationSchema.PermissionMenuVisible];
			}
			set
			{
				this[ClassificationSchema.PermissionMenuVisible] = value;
			}
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x06003701 RID: 14081 RVA: 0x000D750F File Offset: 0x000D570F
		// (set) Token: 0x06003702 RID: 14082 RVA: 0x000D7521 File Offset: 0x000D5721
		[Parameter(Mandatory = false)]
		public bool RetainClassificationEnabled
		{
			get
			{
				return (bool)this[ClassificationSchema.RetainClassificationEnabled];
			}
			set
			{
				this[ClassificationSchema.RetainClassificationEnabled] = value;
			}
		}

		// Token: 0x0400252B RID: 9515
		private static readonly ADObjectId parentPath = new ADObjectId("CN=Message Classifications,CN=Transport Settings");

		// Token: 0x0400252C RID: 9516
		private static ClassificationSchema schema = ObjectSchema.GetInstance<ClassificationSchema>();

		// Token: 0x0400252D RID: 9517
		private static string mostDerivedClass = "msExchMessageClassification";

		// Token: 0x0400252E RID: 9518
		private static Dictionary<string, RawSecurityDescriptor> securityDescriptorTable = new Dictionary<string, RawSecurityDescriptor>();

		// Token: 0x0400252F RID: 9519
		[NonSerialized]
		private RawSecurityDescriptor sharedSecurityDescriptor;
	}
}
