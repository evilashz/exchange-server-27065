using System;
using System.Collections.ObjectModel;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x020006A3 RID: 1699
	internal class MetaTypeWrapper : MetaType
	{
		// Token: 0x06003C28 RID: 15400 RVA: 0x00100C54 File Offset: 0x000FEE54
		public MetaTypeWrapper(MetaModelWrapper metaModelWrapper, MetaType metaType, MetaTableWrapper metaTableWrapper)
		{
			this.metaModelWrapper = metaModelWrapper;
			this.innerMetaType = metaType;
			this.metaTableWrapper = metaTableWrapper;
		}

		// Token: 0x06003C29 RID: 15401 RVA: 0x00100C71 File Offset: 0x000FEE71
		public override MetaType GetInheritanceType(Type type)
		{
			return this.innerMetaType.GetInheritanceType(type);
		}

		// Token: 0x06003C2A RID: 15402 RVA: 0x00100C7F File Offset: 0x000FEE7F
		public override MetaType GetTypeForInheritanceCode(object code)
		{
			return this.innerMetaType.GetTypeForInheritanceCode(code);
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x00100C8D File Offset: 0x000FEE8D
		public override MetaDataMember GetDataMember(MemberInfo member)
		{
			return this.innerMetaType.GetDataMember(member);
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x06003C2C RID: 15404 RVA: 0x00100C9B File Offset: 0x000FEE9B
		public override MetaModel Model
		{
			get
			{
				return this.metaModelWrapper;
			}
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06003C2D RID: 15405 RVA: 0x00100CA3 File Offset: 0x000FEEA3
		public override MetaTable Table
		{
			get
			{
				return this.metaTableWrapper;
			}
		}

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06003C2E RID: 15406 RVA: 0x00100CAB File Offset: 0x000FEEAB
		public override Type Type
		{
			get
			{
				return this.innerMetaType.Type;
			}
		}

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06003C2F RID: 15407 RVA: 0x00100CB8 File Offset: 0x000FEEB8
		public override string Name
		{
			get
			{
				return this.innerMetaType.Name;
			}
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x06003C30 RID: 15408 RVA: 0x00100CC5 File Offset: 0x000FEEC5
		public override bool IsEntity
		{
			get
			{
				return this.innerMetaType.IsEntity;
			}
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06003C31 RID: 15409 RVA: 0x00100CD2 File Offset: 0x000FEED2
		public override bool CanInstantiate
		{
			get
			{
				return this.innerMetaType.CanInstantiate;
			}
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06003C32 RID: 15410 RVA: 0x00100CDF File Offset: 0x000FEEDF
		public override MetaDataMember DBGeneratedIdentityMember
		{
			get
			{
				return this.innerMetaType.DBGeneratedIdentityMember;
			}
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06003C33 RID: 15411 RVA: 0x00100CEC File Offset: 0x000FEEEC
		public override MetaDataMember VersionMember
		{
			get
			{
				return this.innerMetaType.VersionMember;
			}
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06003C34 RID: 15412 RVA: 0x00100CF9 File Offset: 0x000FEEF9
		public override MetaDataMember Discriminator
		{
			get
			{
				return this.innerMetaType.Discriminator;
			}
		}

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06003C35 RID: 15413 RVA: 0x00100D06 File Offset: 0x000FEF06
		public override bool HasUpdateCheck
		{
			get
			{
				return this.innerMetaType.HasUpdateCheck;
			}
		}

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06003C36 RID: 15414 RVA: 0x00100D13 File Offset: 0x000FEF13
		public override bool HasInheritance
		{
			get
			{
				return this.innerMetaType.HasInheritance;
			}
		}

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06003C37 RID: 15415 RVA: 0x00100D20 File Offset: 0x000FEF20
		public override bool HasInheritanceCode
		{
			get
			{
				return this.innerMetaType.HasInheritanceCode;
			}
		}

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06003C38 RID: 15416 RVA: 0x00100D2D File Offset: 0x000FEF2D
		public override object InheritanceCode
		{
			get
			{
				return this.innerMetaType.InheritanceCode;
			}
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06003C39 RID: 15417 RVA: 0x00100D3A File Offset: 0x000FEF3A
		public override bool IsInheritanceDefault
		{
			get
			{
				return this.innerMetaType.IsInheritanceDefault;
			}
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06003C3A RID: 15418 RVA: 0x00100D47 File Offset: 0x000FEF47
		public override MetaType InheritanceRoot
		{
			get
			{
				return this.innerMetaType.InheritanceRoot;
			}
		}

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06003C3B RID: 15419 RVA: 0x00100D54 File Offset: 0x000FEF54
		public override MetaType InheritanceBase
		{
			get
			{
				return this.innerMetaType.InheritanceBase;
			}
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06003C3C RID: 15420 RVA: 0x00100D61 File Offset: 0x000FEF61
		public override MetaType InheritanceDefault
		{
			get
			{
				return this.innerMetaType.InheritanceDefault;
			}
		}

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06003C3D RID: 15421 RVA: 0x00100D6E File Offset: 0x000FEF6E
		public override ReadOnlyCollection<MetaType> InheritanceTypes
		{
			get
			{
				return this.innerMetaType.InheritanceTypes;
			}
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06003C3E RID: 15422 RVA: 0x00100D7B File Offset: 0x000FEF7B
		public override bool HasAnyLoadMethod
		{
			get
			{
				return this.innerMetaType.HasAnyLoadMethod;
			}
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06003C3F RID: 15423 RVA: 0x00100D88 File Offset: 0x000FEF88
		public override bool HasAnyValidateMethod
		{
			get
			{
				return this.innerMetaType.HasAnyValidateMethod;
			}
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06003C40 RID: 15424 RVA: 0x00100D95 File Offset: 0x000FEF95
		public override ReadOnlyCollection<MetaType> DerivedTypes
		{
			get
			{
				return this.innerMetaType.DerivedTypes;
			}
		}

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06003C41 RID: 15425 RVA: 0x00100DA2 File Offset: 0x000FEFA2
		public override ReadOnlyCollection<MetaDataMember> DataMembers
		{
			get
			{
				return this.innerMetaType.DataMembers;
			}
		}

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06003C42 RID: 15426 RVA: 0x00100DAF File Offset: 0x000FEFAF
		public override ReadOnlyCollection<MetaDataMember> PersistentDataMembers
		{
			get
			{
				return this.innerMetaType.PersistentDataMembers;
			}
		}

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06003C43 RID: 15427 RVA: 0x00100DBC File Offset: 0x000FEFBC
		public override ReadOnlyCollection<MetaDataMember> IdentityMembers
		{
			get
			{
				return this.innerMetaType.IdentityMembers;
			}
		}

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06003C44 RID: 15428 RVA: 0x00100DC9 File Offset: 0x000FEFC9
		public override ReadOnlyCollection<MetaAssociation> Associations
		{
			get
			{
				return this.innerMetaType.Associations;
			}
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06003C45 RID: 15429 RVA: 0x00100DD6 File Offset: 0x000FEFD6
		public override MethodInfo OnLoadedMethod
		{
			get
			{
				return this.innerMetaType.OnLoadedMethod;
			}
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06003C46 RID: 15430 RVA: 0x00100DE3 File Offset: 0x000FEFE3
		public override MethodInfo OnValidateMethod
		{
			get
			{
				return this.innerMetaType.OnValidateMethod;
			}
		}

		// Token: 0x04002728 RID: 10024
		private MetaType innerMetaType;

		// Token: 0x04002729 RID: 10025
		private MetaModelWrapper metaModelWrapper;

		// Token: 0x0400272A RID: 10026
		private MetaTableWrapper metaTableWrapper;
	}
}
