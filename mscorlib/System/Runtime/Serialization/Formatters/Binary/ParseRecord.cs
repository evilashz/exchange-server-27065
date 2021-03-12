using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000768 RID: 1896
	internal sealed class ParseRecord
	{
		// Token: 0x06005319 RID: 21273 RVA: 0x00124174 File Offset: 0x00122374
		internal ParseRecord()
		{
		}

		// Token: 0x0600531A RID: 21274 RVA: 0x0012417C File Offset: 0x0012237C
		internal void Init()
		{
			this.PRparseTypeEnum = InternalParseTypeE.Empty;
			this.PRobjectTypeEnum = InternalObjectTypeE.Empty;
			this.PRarrayTypeEnum = InternalArrayTypeE.Empty;
			this.PRmemberTypeEnum = InternalMemberTypeE.Empty;
			this.PRmemberValueEnum = InternalMemberValueE.Empty;
			this.PRobjectPositionEnum = InternalObjectPositionE.Empty;
			this.PRname = null;
			this.PRvalue = null;
			this.PRkeyDt = null;
			this.PRdtType = null;
			this.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
			this.PRisEnum = false;
			this.PRobjectId = 0L;
			this.PRidRef = 0L;
			this.PRarrayElementTypeString = null;
			this.PRarrayElementType = null;
			this.PRisArrayVariant = false;
			this.PRarrayElementTypeCode = InternalPrimitiveTypeE.Invalid;
			this.PRrank = 0;
			this.PRlengthA = null;
			this.PRpositionA = null;
			this.PRlowerBoundA = null;
			this.PRupperBoundA = null;
			this.PRindexMap = null;
			this.PRmemberIndex = 0;
			this.PRlinearlength = 0;
			this.PRrectangularMap = null;
			this.PRisLowerBound = false;
			this.PRtopId = 0L;
			this.PRheaderId = 0L;
			this.PRisValueTypeFixup = false;
			this.PRnewObj = null;
			this.PRobjectA = null;
			this.PRprimitiveArray = null;
			this.PRobjectInfo = null;
			this.PRisRegistered = false;
			this.PRmemberData = null;
			this.PRsi = null;
			this.PRnullCount = 0;
		}

		// Token: 0x04002568 RID: 9576
		internal static int parseRecordIdCount = 1;

		// Token: 0x04002569 RID: 9577
		internal int PRparseRecordId;

		// Token: 0x0400256A RID: 9578
		internal InternalParseTypeE PRparseTypeEnum;

		// Token: 0x0400256B RID: 9579
		internal InternalObjectTypeE PRobjectTypeEnum;

		// Token: 0x0400256C RID: 9580
		internal InternalArrayTypeE PRarrayTypeEnum;

		// Token: 0x0400256D RID: 9581
		internal InternalMemberTypeE PRmemberTypeEnum;

		// Token: 0x0400256E RID: 9582
		internal InternalMemberValueE PRmemberValueEnum;

		// Token: 0x0400256F RID: 9583
		internal InternalObjectPositionE PRobjectPositionEnum;

		// Token: 0x04002570 RID: 9584
		internal string PRname;

		// Token: 0x04002571 RID: 9585
		internal string PRvalue;

		// Token: 0x04002572 RID: 9586
		internal object PRvarValue;

		// Token: 0x04002573 RID: 9587
		internal string PRkeyDt;

		// Token: 0x04002574 RID: 9588
		internal Type PRdtType;

		// Token: 0x04002575 RID: 9589
		internal InternalPrimitiveTypeE PRdtTypeCode;

		// Token: 0x04002576 RID: 9590
		internal bool PRisVariant;

		// Token: 0x04002577 RID: 9591
		internal bool PRisEnum;

		// Token: 0x04002578 RID: 9592
		internal long PRobjectId;

		// Token: 0x04002579 RID: 9593
		internal long PRidRef;

		// Token: 0x0400257A RID: 9594
		internal string PRarrayElementTypeString;

		// Token: 0x0400257B RID: 9595
		internal Type PRarrayElementType;

		// Token: 0x0400257C RID: 9596
		internal bool PRisArrayVariant;

		// Token: 0x0400257D RID: 9597
		internal InternalPrimitiveTypeE PRarrayElementTypeCode;

		// Token: 0x0400257E RID: 9598
		internal int PRrank;

		// Token: 0x0400257F RID: 9599
		internal int[] PRlengthA;

		// Token: 0x04002580 RID: 9600
		internal int[] PRpositionA;

		// Token: 0x04002581 RID: 9601
		internal int[] PRlowerBoundA;

		// Token: 0x04002582 RID: 9602
		internal int[] PRupperBoundA;

		// Token: 0x04002583 RID: 9603
		internal int[] PRindexMap;

		// Token: 0x04002584 RID: 9604
		internal int PRmemberIndex;

		// Token: 0x04002585 RID: 9605
		internal int PRlinearlength;

		// Token: 0x04002586 RID: 9606
		internal int[] PRrectangularMap;

		// Token: 0x04002587 RID: 9607
		internal bool PRisLowerBound;

		// Token: 0x04002588 RID: 9608
		internal long PRtopId;

		// Token: 0x04002589 RID: 9609
		internal long PRheaderId;

		// Token: 0x0400258A RID: 9610
		internal ReadObjectInfo PRobjectInfo;

		// Token: 0x0400258B RID: 9611
		internal bool PRisValueTypeFixup;

		// Token: 0x0400258C RID: 9612
		internal object PRnewObj;

		// Token: 0x0400258D RID: 9613
		internal object[] PRobjectA;

		// Token: 0x0400258E RID: 9614
		internal PrimitiveArray PRprimitiveArray;

		// Token: 0x0400258F RID: 9615
		internal bool PRisRegistered;

		// Token: 0x04002590 RID: 9616
		internal object[] PRmemberData;

		// Token: 0x04002591 RID: 9617
		internal SerializationInfo PRsi;

		// Token: 0x04002592 RID: 9618
		internal int PRnullCount;
	}
}
