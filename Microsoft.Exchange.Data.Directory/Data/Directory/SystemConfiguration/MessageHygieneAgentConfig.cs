using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004B9 RID: 1209
	[Serializable]
	public abstract class MessageHygieneAgentConfig : ADConfigurationObject
	{
		// Token: 0x06003712 RID: 14098 RVA: 0x000D782F File Offset: 0x000D5A2F
		public MessageHygieneAgentConfig()
		{
		}

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06003713 RID: 14099 RVA: 0x000D7837 File Offset: 0x000D5A37
		internal override ADObjectSchema Schema
		{
			get
			{
				return MessageHygieneAgentConfig.schema;
			}
		}

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x06003714 RID: 14100 RVA: 0x000D783E File Offset: 0x000D5A3E
		internal override ADObjectId ParentPath
		{
			get
			{
				return MessageHygieneAgentConfig.parentPath;
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x06003715 RID: 14101 RVA: 0x000D7845 File Offset: 0x000D5A45
		// (set) Token: 0x06003716 RID: 14102 RVA: 0x000D7857 File Offset: 0x000D5A57
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)this[MessageHygieneAgentConfigSchema.Enabled];
			}
			set
			{
				this[MessageHygieneAgentConfigSchema.Enabled] = value;
			}
		}

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x06003717 RID: 14103 RVA: 0x000D786A File Offset: 0x000D5A6A
		// (set) Token: 0x06003718 RID: 14104 RVA: 0x000D787C File Offset: 0x000D5A7C
		[Parameter(Mandatory = false)]
		public bool ExternalMailEnabled
		{
			get
			{
				return (bool)this[MessageHygieneAgentConfigSchema.ExternalMailEnabled];
			}
			set
			{
				this[MessageHygieneAgentConfigSchema.ExternalMailEnabled] = value;
			}
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x06003719 RID: 14105 RVA: 0x000D788F File Offset: 0x000D5A8F
		// (set) Token: 0x0600371A RID: 14106 RVA: 0x000D78A1 File Offset: 0x000D5AA1
		[Parameter(Mandatory = false)]
		public bool InternalMailEnabled
		{
			get
			{
				return (bool)this[MessageHygieneAgentConfigSchema.InternalMailEnabled];
			}
			set
			{
				this[MessageHygieneAgentConfigSchema.InternalMailEnabled] = value;
			}
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x000D78B4 File Offset: 0x000D5AB4
		protected void ValidateMaximumCollectionCount(List<ValidationError> errors, ICollection collection, int maximum, PropertyDefinition property)
		{
			if (collection != null && property != null)
			{
				int count = collection.Count;
				if (count > maximum)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ExceededMaximumCollectionCount(property.Name, maximum, count), property, count));
				}
			}
		}

		// Token: 0x04002545 RID: 9541
		private static ADObjectSchema schema = ObjectSchema.GetInstance<MessageHygieneAgentConfigSchema>();

		// Token: 0x04002546 RID: 9542
		private static ADObjectId parentPath = new ADObjectId("CN=Message Hygiene,CN=Transport Settings");
	}
}
