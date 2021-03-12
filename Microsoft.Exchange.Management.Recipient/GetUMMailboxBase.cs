using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000ED RID: 237
	public abstract class GetUMMailboxBase<TIdentity> : GetRecipientBase<TIdentity, ADUser> where TIdentity : RecipientIdParameter, new()
	{
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x00041EE4 File Offset: 0x000400E4
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter internalFilter = base.InternalFilter;
				QueryFilter queryFilter = new BitMaskAndFilter(ADUserSchema.UMEnabledFlags, 1UL);
				if (internalFilter != null)
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						internalFilter,
						queryFilter
					});
				}
				return queryFilter;
			}
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00041F1F File Offset: 0x0004011F
		public GetUMMailboxBase()
		{
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x00041F27 File Offset: 0x00040127
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetUMMailboxBase<MailboxIdParameter>.SortPropertiesArray;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x00041F2E File Offset: 0x0004012E
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<UMMailboxSchema>();
			}
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00041F38 File Offset: 0x00040138
		protected override bool ShouldSkipObject(IConfigurable dataObject)
		{
			if (this.Identity != null)
			{
				ADUser aduser = (ADUser)dataObject;
				if ((aduser.UMEnabledFlags & UMEnabledFlags.UMEnabled) != UMEnabledFlags.UMEnabled)
				{
					TIdentity identity = this.Identity;
					base.WriteVerbose(Strings.MailboxNotUmEnabled(identity.ToString()));
					return true;
				}
			}
			return base.ShouldSkipObject(dataObject);
		}

		// Token: 0x04000377 RID: 887
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ADObjectSchema.Name,
			UMMailboxSchema.DisplayName,
			UMMailboxSchema.ServerLegacyDN
		};
	}
}
