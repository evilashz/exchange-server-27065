using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000073 RID: 115
	[Serializable]
	public class ADScope
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x0001E585 File Offset: 0x0001C785
		protected ADScope()
		{
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001E58D File Offset: 0x0001C78D
		public ADScope(ADObjectId root, QueryFilter filter)
		{
			this.root = root;
			this.filter = filter;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0001E5A3 File Offset: 0x0001C7A3
		public bool IsEmpty
		{
			get
			{
				return this.Root == null && null == this.Filter;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001E5B8 File Offset: 0x0001C7B8
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x0001E5C0 File Offset: 0x0001C7C0
		public virtual ADObjectId Root
		{
			get
			{
				return this.root;
			}
			protected internal set
			{
				if (this == ADScope.Empty || this == ADScope.NoAccess)
				{
					throw new InvalidOperationException();
				}
				this.root = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001E5DF File Offset: 0x0001C7DF
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x0001E5E7 File Offset: 0x0001C7E7
		public virtual QueryFilter Filter
		{
			get
			{
				return this.filter;
			}
			protected internal set
			{
				this.filter = value;
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001E5F0 File Offset: 0x0001C7F0
		public string GetFilterString()
		{
			if (this.Filter != null)
			{
				return this.Filter.GenerateInfixString(FilterLanguage.Monad);
			}
			return string.Empty;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001E60C File Offset: 0x0001C80C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ADScope);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001E61A File Offset: 0x0001C81A
		public bool Equals(ADScope obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (!ADObjectId.Equals(this.Root, obj.Root))
			{
				return false;
			}
			if (this.Filter != null)
			{
				return this.Filter.Equals(obj.Filter);
			}
			return obj.Filter == null;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001E65C File Offset: 0x0001C85C
		public override int GetHashCode()
		{
			int num = (this.Root == null) ? 0 : this.Root.GetHashCode();
			int num2 = (this.Filter == null) ? 0 : this.Filter.GetHashCode();
			return num ^ num2;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001E69A File Offset: 0x0001C89A
		public override string ToString()
		{
			return string.Format("{{{0}, {1}}}", this.Root, this.Filter);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001E6B4 File Offset: 0x0001C8B4
		private static QueryFilter CombineScopes(IList<ADScope> combinableScopes)
		{
			QueryFilter[] array = new QueryFilter[combinableScopes.Count];
			for (int i = 0; i < combinableScopes.Count; i++)
			{
				if (combinableScopes[i].Root != combinableScopes[0].Root)
				{
					throw new ArgumentException("combinableScopes");
				}
				array[i] = combinableScopes[i].Filter;
			}
			return (array.Length == 1) ? array[0] : new OrFilter(array);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001E728 File Offset: 0x0001C928
		internal static ADScope CombineScopeCollections(IList<ADScopeCollection> combinableScopeCollections)
		{
			QueryFilter[] array = new QueryFilter[combinableScopeCollections.Count];
			for (int i = 0; i < combinableScopeCollections.Count; i++)
			{
				ADScopeCollection combinableScopes = combinableScopeCollections[i];
				if (combinableScopeCollections[i][0].Root != combinableScopeCollections[0][0].Root)
				{
					throw new ArgumentException("combinableScopeCollections");
				}
				array[i] = ADScope.CombineScopes(combinableScopes);
			}
			QueryFilter queryFilter = (array.Length == 1) ? array[0] : new AndFilter(array);
			return new ADScope(combinableScopeCollections[0][0].Root, queryFilter);
		}

		// Token: 0x04000240 RID: 576
		internal static readonly QueryFilter NoObjectFilter = new NotFilter(new ExistsFilter(ADObjectSchema.ObjectClass));

		// Token: 0x04000241 RID: 577
		internal static readonly ADScope NoAccess = new ADScope(null, ADScope.NoObjectFilter);

		// Token: 0x04000242 RID: 578
		internal static readonly ADScope Empty = new ADScope();

		// Token: 0x04000243 RID: 579
		private static readonly ADScope[] EmptyArray = new ADScope[0];

		// Token: 0x04000244 RID: 580
		private ADObjectId root;

		// Token: 0x04000245 RID: 581
		private QueryFilter filter;
	}
}
