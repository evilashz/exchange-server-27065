using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.Exchange.Services.OData.Web
{
	// Token: 0x02000E03 RID: 3587
	internal class ODataPathWrapper
	{
		// Token: 0x06005CE4 RID: 23780 RVA: 0x00122036 File Offset: 0x00120236
		public ODataPathWrapper(ODataPath odataPath)
		{
			ArgumentValidator.ThrowIfNull("odataPath", odataPath);
			this.ODataPath = odataPath;
			this.PathSegments = this.ODataPath.ToList<ODataPathSegment>();
			this.ResolveEntitySet();
		}

		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x06005CE5 RID: 23781 RVA: 0x00122067 File Offset: 0x00120267
		// (set) Token: 0x06005CE6 RID: 23782 RVA: 0x0012206F File Offset: 0x0012026F
		public ODataPath ODataPath { get; private set; }

		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x06005CE7 RID: 23783 RVA: 0x00122078 File Offset: 0x00120278
		// (set) Token: 0x06005CE8 RID: 23784 RVA: 0x00122080 File Offset: 0x00120280
		public IEdmEntityType EntityType { get; private set; }

		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x06005CE9 RID: 23785 RVA: 0x00122089 File Offset: 0x00120289
		// (set) Token: 0x06005CEA RID: 23786 RVA: 0x00122091 File Offset: 0x00120291
		public IEdmNavigationSource NavigationSource { get; private set; }

		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x06005CEB RID: 23787 RVA: 0x0012209A File Offset: 0x0012029A
		// (set) Token: 0x06005CEC RID: 23788 RVA: 0x001220A2 File Offset: 0x001202A2
		public List<ODataPathSegment> PathSegments { get; private set; }

		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x06005CED RID: 23789 RVA: 0x001220AB File Offset: 0x001202AB
		public ODataPathSegment FirstSegment
		{
			get
			{
				return this.ODataPath.FirstSegment;
			}
		}

		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x06005CEE RID: 23790 RVA: 0x001220B8 File Offset: 0x001202B8
		public ODataPathSegment LastSegment
		{
			get
			{
				return this.ODataPath.LastSegment;
			}
		}

		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x06005CEF RID: 23791 RVA: 0x001220C5 File Offset: 0x001202C5
		public ODataPathSegment EntitySegment
		{
			get
			{
				return this.PathSegments[this.entitySegmentIndex];
			}
		}

		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x06005CF0 RID: 23792 RVA: 0x001220D8 File Offset: 0x001202D8
		public ODataPathSegment ParentOfEntitySegment
		{
			get
			{
				return this.PathSegments[this.entitySegmentIndex - 1];
			}
		}

		// Token: 0x17001508 RID: 5384
		// (get) Token: 0x06005CF1 RID: 23793 RVA: 0x001220ED File Offset: 0x001202ED
		public ODataPathSegment GrandParentOfEntitySegment
		{
			get
			{
				return this.PathSegments[this.entitySegmentIndex - 2];
			}
		}

		// Token: 0x06005CF2 RID: 23794 RVA: 0x00122104 File Offset: 0x00120304
		public string ResolveEntityKey()
		{
			if (this.EntitySegment is KeySegment)
			{
				return this.EntitySegment.GetIdKey();
			}
			if (this.EntitySegment is SingletonSegment)
			{
				return this.EntitySegment.GetSingletonName();
			}
			if (this.EntitySegment is NavigationPropertySegment)
			{
				return this.EntitySegment.GetPropertyName();
			}
			throw new InvalidOperationException(string.Format("Unknown ID representation from last segment {0}", this.EntitySegment));
		}

		// Token: 0x06005CF3 RID: 23795 RVA: 0x00122174 File Offset: 0x00120374
		private void ResolveEntitySet()
		{
			this.entitySegmentIndex = this.PathSegments.Count - 1;
			if (this.EntitySegment is CountSegment)
			{
				this.entitySegmentIndex--;
			}
			this.EntityType = this.ResolveEntityType(this.EntitySegment);
			EntitySetSegment entitySetSegment = this.EntitySegment as EntitySetSegment;
			if (entitySetSegment != null)
			{
				this.NavigationSource = entitySetSegment.EntitySet;
				return;
			}
			SingletonSegment singletonSegment = this.EntitySegment as SingletonSegment;
			if (singletonSegment != null)
			{
				this.NavigationSource = singletonSegment.Singleton;
				return;
			}
			NavigationPropertySegment navigationPropertySegment = this.EntitySegment as NavigationPropertySegment;
			if (navigationPropertySegment != null)
			{
				this.NavigationSource = navigationPropertySegment.NavigationSource;
				return;
			}
			KeySegment keySegment = this.EntitySegment as KeySegment;
			if (keySegment != null)
			{
				this.NavigationSource = keySegment.NavigationSource;
				return;
			}
			OperationSegment operationSegment = this.EntitySegment as OperationSegment;
			if (operationSegment != null)
			{
				this.NavigationSource = operationSegment.EntitySet;
				return;
			}
			throw new NotSupportedException(string.Format("Unexpected ODataPathSegment type {0}", this.EntitySegment));
		}

		// Token: 0x06005CF4 RID: 23796 RVA: 0x00122268 File Offset: 0x00120468
		private IEdmEntityType ResolveEntityType(ODataPathSegment entitySegment)
		{
			IEdmType edmType = entitySegment.EdmType;
			OperationSegment operationSegment = entitySegment as OperationSegment;
			if (operationSegment != null && edmType == null)
			{
				IEdmOperation edmOperation = operationSegment.Operations.First<IEdmOperation>();
				IEdmOperationParameter edmOperationParameter = edmOperation.Parameters.First<IEdmOperationParameter>();
				edmType = edmOperationParameter.Type.Definition;
			}
			IEdmCollectionType edmCollectionType = edmType as IEdmCollectionType;
			if (edmCollectionType != null)
			{
				edmType = (edmCollectionType.ElementType.Definition as IEdmEntityType);
			}
			IEdmEntityType edmEntityType = edmType as IEdmEntityType;
			if (edmEntityType != null)
			{
				return edmEntityType;
			}
			throw new NotSupportedException(string.Format("Unexpected IEdmType type {0}", edmType));
		}

		// Token: 0x0400324F RID: 12879
		private int entitySegmentIndex;
	}
}
