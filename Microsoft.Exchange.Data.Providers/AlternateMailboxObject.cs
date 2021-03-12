using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Providers
{
	// Token: 0x02000002 RID: 2
	[Serializable]
	public class AlternateMailboxObject : ConfigurableObject
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		// (set) Token: 0x06000002 RID: 2 RVA: 0x000020E7 File Offset: 0x000002E7
		[Parameter]
		public string Name
		{
			get
			{
				return (string)this.propertyBag[AlternateMailboxSchema.Name];
			}
			set
			{
				this.propertyBag[AlternateMailboxSchema.Name] = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020FA File Offset: 0x000002FA
		internal static StringComparer NameComparer
		{
			get
			{
				return StringComparer.CurrentCultureIgnoreCase;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002101 File Offset: 0x00000301
		internal static StringComparison NameComparison
		{
			get
			{
				return StringComparison.CurrentCultureIgnoreCase;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002104 File Offset: 0x00000304
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000211B File Offset: 0x0000031B
		[Parameter]
		public string UserDisplayName
		{
			get
			{
				return (string)this.propertyBag[AlternateMailboxSchema.UserDisplayName];
			}
			set
			{
				this.propertyBag[AlternateMailboxSchema.UserDisplayName] = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000212E File Offset: 0x0000032E
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002145 File Offset: 0x00000345
		public AlternateMailbox.AlternateMailboxFlags Type
		{
			get
			{
				return (AlternateMailbox.AlternateMailboxFlags)this.propertyBag[AlternateMailboxSchema.Type];
			}
			internal set
			{
				this.propertyBag[AlternateMailboxSchema.Type] = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000215D File Offset: 0x0000035D
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002174 File Offset: 0x00000374
		[Parameter]
		public bool RetentionPolicyEnabled
		{
			get
			{
				return (bool)this.propertyBag[AlternateMailboxSchema.RetentionPolicyEnabled];
			}
			set
			{
				this.propertyBag[AlternateMailboxSchema.RetentionPolicyEnabled] = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000218C File Offset: 0x0000038C
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000021A3 File Offset: 0x000003A3
		public Guid DatabaseGuid
		{
			get
			{
				return (Guid)this.propertyBag[AlternateMailboxSchema.DatabaseGuid];
			}
			internal set
			{
				this.propertyBag[AlternateMailboxSchema.DatabaseGuid] = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021BB File Offset: 0x000003BB
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000021D2 File Offset: 0x000003D2
		public Guid MailboxGuid
		{
			get
			{
				return (Guid)this.propertyBag[AlternateMailboxSchema.MailboxGuid];
			}
			internal set
			{
				this.propertyBag[AlternateMailboxSchema.MailboxGuid] = value;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021EA File Offset: 0x000003EA
		internal void SetIdentity(AlternateMailboxObjectId id)
		{
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = id;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021FE File Offset: 0x000003FE
		public AlternateMailboxObject() : base(new AlternateMailboxPropertyBag())
		{
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000220B File Offset: 0x0000040B
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return AlternateMailboxObject.s_schema;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002212 File Offset: 0x00000412
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000001 RID: 1
		private static ObjectSchema s_schema = ObjectSchema.GetInstance<AlternateMailboxSchema>();
	}
}
