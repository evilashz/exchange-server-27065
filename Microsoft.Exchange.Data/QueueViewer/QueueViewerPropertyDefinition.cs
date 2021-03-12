using System;
using Microsoft.Exchange.Rpc.QueueViewer;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x02000274 RID: 628
	[Serializable]
	internal sealed class QueueViewerPropertyDefinition<ObjectType> : ProviderPropertyDefinition where ObjectType : PagedDataObject
	{
		// Token: 0x060014EF RID: 5359 RVA: 0x00042E46 File Offset: 0x00041046
		internal QueueViewerPropertyDefinition(string name, Type type, object defaultValue, bool isBasic, CompareFieldWithValueDelegate<ObjectType> comparer1, CompareFieldWithFieldDelegate<ObjectType> comparer2) : this(name, type, defaultValue, isBasic, comparer1, comparer2, ExchangeObjectVersion.Exchange2003)
		{
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x00042E5C File Offset: 0x0004105C
		internal QueueViewerPropertyDefinition(string name, Type type, object defaultValue, bool isBasic, CompareFieldWithValueDelegate<ObjectType> comparer1, CompareFieldWithFieldDelegate<ObjectType> comparer2, ExchangeObjectVersion version) : this(name, type, defaultValue, isBasic, comparer1, comparer2, QueueViewerPropertyDefinition<ObjectType>.TextFilterNotSupportedMatcher, version)
		{
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00042E80 File Offset: 0x00041080
		internal QueueViewerPropertyDefinition(string name, Type type, object defaultValue, bool isBasic, CompareFieldWithValueDelegate<ObjectType> comparer1, CompareFieldWithFieldDelegate<ObjectType> comparer2, MatchFieldWithTextDelegate<ObjectType> matcher) : this(name, type, defaultValue, isBasic, comparer1, comparer2, matcher, ExchangeObjectVersion.Exchange2003)
		{
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x00042EA3 File Offset: 0x000410A3
		internal QueueViewerPropertyDefinition(string name, Type type, object defaultValue, bool isBasic, CompareFieldWithValueDelegate<ObjectType> comparer1, CompareFieldWithFieldDelegate<ObjectType> comparer2, MatchFieldWithTextDelegate<ObjectType> matcher, ExchangeObjectVersion version) : base(name, version, type, defaultValue)
		{
			this.isBasic = isBasic;
			this.comparer1 = comparer1;
			this.comparer2 = comparer2;
			this.matcher = matcher;
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x00042ED0 File Offset: 0x000410D0
		public override bool IsMultivalued
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x00042ED3 File Offset: 0x000410D3
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x00042ED6 File Offset: 0x000410D6
		public override bool IsCalculated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x00042ED9 File Offset: 0x000410D9
		public override bool IsFilterOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00042EDC File Offset: 0x000410DC
		public override bool IsMandatory
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x00042EDF File Offset: 0x000410DF
		public override bool PersistDefaultValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00042EE2 File Offset: 0x000410E2
		public override bool IsWriteOnce
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x00042EE5 File Offset: 0x000410E5
		public override bool IsBinary
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000C35 RID: 3125
		public bool isBasic;

		// Token: 0x04000C36 RID: 3126
		internal CompareFieldWithValueDelegate<ObjectType> comparer1;

		// Token: 0x04000C37 RID: 3127
		internal CompareFieldWithFieldDelegate<ObjectType> comparer2;

		// Token: 0x04000C38 RID: 3128
		internal MatchFieldWithTextDelegate<ObjectType> matcher;

		// Token: 0x04000C39 RID: 3129
		internal static MatchFieldWithTextDelegate<ObjectType> TextFilterNotSupportedMatcher = delegate(ObjectType dataObject, object matchPattern, MatchOptions matchOptions)
		{
			throw new QueueViewerException(QVErrorCode.QV_E_TEXT_MATCHING_NOT_SUPPORTED);
		};
	}
}
