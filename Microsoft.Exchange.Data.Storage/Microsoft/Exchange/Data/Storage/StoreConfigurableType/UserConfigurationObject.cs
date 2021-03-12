using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;

namespace Microsoft.Exchange.Data.Storage.StoreConfigurableType
{
	// Token: 0x020009F8 RID: 2552
	[Serializable]
	public abstract class UserConfigurationObject : ConfigurableObject, IMailboxStoreType, IConfigurable
	{
		// Token: 0x06005D3A RID: 23866 RVA: 0x0018B0CF File Offset: 0x001892CF
		public UserConfigurationObject() : base(new SimplePropertyBag(UserConfigurationObjectSchema.ExchangePrincipal, UserConfigurationObjectSchema.ObjectState, UserConfigurationObjectSchema.ExchangeVersion))
		{
		}

		// Token: 0x06005D3B RID: 23867 RVA: 0x0018B0EB File Offset: 0x001892EB
		internal UserConfigurationObject(IExchangePrincipal principal) : this()
		{
			this.Principal = principal;
		}

		// Token: 0x17001980 RID: 6528
		// (get) Token: 0x06005D3C RID: 23868 RVA: 0x0018B0FC File Offset: 0x001892FC
		public override ObjectId Identity
		{
			get
			{
				ObjectId objectId = this.identity;
				if (SuppressingPiiContext.NeedPiiSuppression && objectId is ADObjectId)
				{
					objectId = (ObjectId)SuppressingPiiProperty.TryRedact(ADObjectSchema.Id, objectId);
				}
				return objectId;
			}
		}

		// Token: 0x17001981 RID: 6529
		// (get) Token: 0x06005D3D RID: 23869 RVA: 0x0018B131 File Offset: 0x00189331
		internal virtual UserConfigurationObjectSchema Schema
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001982 RID: 6530
		// (get) Token: 0x06005D3E RID: 23870 RVA: 0x0018B134 File Offset: 0x00189334
		internal sealed override ObjectSchema ObjectSchema
		{
			get
			{
				return this.Schema;
			}
		}

		// Token: 0x17001983 RID: 6531
		// (get) Token: 0x06005D3F RID: 23871 RVA: 0x0018B13C File Offset: 0x0018933C
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17001984 RID: 6532
		// (get) Token: 0x06005D40 RID: 23872 RVA: 0x0018B143 File Offset: 0x00189343
		// (set) Token: 0x06005D41 RID: 23873 RVA: 0x0018B14B File Offset: 0x0018934B
		internal IExchangePrincipal Principal
		{
			get
			{
				return this.principal;
			}
			set
			{
				this.principal = value;
				if (value != null)
				{
					this.identity = value.ObjectId;
					return;
				}
				this.identity = null;
			}
		}

		// Token: 0x06005D42 RID: 23874 RVA: 0x0018B16C File Offset: 0x0018936C
		public new object Clone()
		{
			UserConfigurationObject userConfigurationObject = (UserConfigurationObject)base.Clone();
			userConfigurationObject.Principal = this.Principal;
			return userConfigurationObject;
		}

		// Token: 0x06005D43 RID: 23875
		public abstract void Save(MailboxStoreTypeProvider session);

		// Token: 0x06005D44 RID: 23876 RVA: 0x0018B192 File Offset: 0x00189392
		public virtual void Delete(MailboxStoreTypeProvider session)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D45 RID: 23877
		public abstract IConfigurable Read(MailboxStoreTypeProvider session, ObjectId identity);

		// Token: 0x06005D46 RID: 23878 RVA: 0x0018B199 File Offset: 0x00189399
		public IConfigurable[] Find(MailboxStoreTypeProvider session, QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D47 RID: 23879 RVA: 0x0018B1A0 File Offset: 0x001893A0
		public IEnumerable<T> FindPaged<T>(MailboxStoreTypeProvider session, QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			T t = (T)((object)this.Read(session, session.ADUser.Identity));
			List<T> list = new List<T>();
			if (t != null)
			{
				list.Add(t);
			}
			return list;
		}

		// Token: 0x06005D48 RID: 23880 RVA: 0x0018B1DC File Offset: 0x001893DC
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			foreach (PropertyDefinition propertyDefinition in this.Schema.AllProperties)
			{
				ProviderPropertyDefinition providerPropertyDefinition = propertyDefinition as ProviderPropertyDefinition;
				if (providerPropertyDefinition != null)
				{
					providerPropertyDefinition.ValidateValue(this[providerPropertyDefinition], false);
				}
			}
		}

		// Token: 0x04003421 RID: 13345
		[NonSerialized]
		private IExchangePrincipal principal;

		// Token: 0x04003422 RID: 13346
		private ObjectId identity;
	}
}
