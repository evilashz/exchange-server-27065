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
	// Token: 0x02000002 RID: 2
	public abstract class GetCASMailboxBase<TIdentity> : GetRecipientBase<TIdentity, ADUser> where TIdentity : RecipientIdParameter, new()
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public GetCASMailboxBase()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<CASMailboxSchema>();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020DF File Offset: 0x000002DF
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetCASMailboxBase<TIdentity>.SortPropertiesArray;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E8 File Offset: 0x000002E8
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			ADUser aduser = (ADUser)dataObject;
			base.WriteResult(aduser);
			if (!aduser.IsOWAEnabledStatusConsistent())
			{
				this.WriteWarning(Strings.ErrorOWAEnabledStatusNotConsistent(aduser.Identity.ToString()));
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ADObjectSchema.Name,
			CASMailboxSchema.DisplayName,
			CASMailboxSchema.ServerLegacyDN
		};
	}
}
