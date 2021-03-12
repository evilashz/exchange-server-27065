using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000767 RID: 1895
	internal sealed class ObjectProgress
	{
		// Token: 0x06005313 RID: 21267 RVA: 0x00123F78 File Offset: 0x00122178
		internal ObjectProgress()
		{
		}

		// Token: 0x06005314 RID: 21268 RVA: 0x00123F94 File Offset: 0x00122194
		[Conditional("SER_LOGGING")]
		private void Counter()
		{
			lock (this)
			{
				this.opRecordId = ObjectProgress.opRecordIdCount++;
				if (ObjectProgress.opRecordIdCount > 1000)
				{
					ObjectProgress.opRecordIdCount = 1;
				}
			}
		}

		// Token: 0x06005315 RID: 21269 RVA: 0x00123FF0 File Offset: 0x001221F0
		internal void Init()
		{
			this.isInitial = false;
			this.count = 0;
			this.expectedType = BinaryTypeEnum.ObjectUrt;
			this.expectedTypeInformation = null;
			this.name = null;
			this.objectTypeEnum = InternalObjectTypeE.Empty;
			this.memberTypeEnum = InternalMemberTypeE.Empty;
			this.memberValueEnum = InternalMemberValueE.Empty;
			this.dtType = null;
			this.numItems = 0;
			this.nullCount = 0;
			this.typeInformation = null;
			this.memberLength = 0;
			this.binaryTypeEnumA = null;
			this.typeInformationA = null;
			this.memberNames = null;
			this.memberTypes = null;
			this.pr.Init();
		}

		// Token: 0x06005316 RID: 21270 RVA: 0x0012407F File Offset: 0x0012227F
		internal void ArrayCountIncrement(int value)
		{
			this.count += value;
		}

		// Token: 0x06005317 RID: 21271 RVA: 0x00124090 File Offset: 0x00122290
		internal bool GetNext(out BinaryTypeEnum outBinaryTypeEnum, out object outTypeInformation)
		{
			outBinaryTypeEnum = BinaryTypeEnum.Primitive;
			outTypeInformation = null;
			if (this.objectTypeEnum == InternalObjectTypeE.Array)
			{
				if (this.count == this.numItems)
				{
					return false;
				}
				outBinaryTypeEnum = this.binaryTypeEnum;
				outTypeInformation = this.typeInformation;
				if (this.count == 0)
				{
					this.isInitial = false;
				}
				this.count++;
				return true;
			}
			else
			{
				if (this.count == this.memberLength && !this.isInitial)
				{
					return false;
				}
				outBinaryTypeEnum = this.binaryTypeEnumA[this.count];
				outTypeInformation = this.typeInformationA[this.count];
				if (this.count == 0)
				{
					this.isInitial = false;
				}
				this.name = this.memberNames[this.count];
				Type[] array = this.memberTypes;
				this.dtType = this.memberTypes[this.count];
				this.count++;
				return true;
			}
		}

		// Token: 0x04002553 RID: 9555
		internal static int opRecordIdCount = 1;

		// Token: 0x04002554 RID: 9556
		internal int opRecordId;

		// Token: 0x04002555 RID: 9557
		internal bool isInitial;

		// Token: 0x04002556 RID: 9558
		internal int count;

		// Token: 0x04002557 RID: 9559
		internal BinaryTypeEnum expectedType = BinaryTypeEnum.ObjectUrt;

		// Token: 0x04002558 RID: 9560
		internal object expectedTypeInformation;

		// Token: 0x04002559 RID: 9561
		internal string name;

		// Token: 0x0400255A RID: 9562
		internal InternalObjectTypeE objectTypeEnum;

		// Token: 0x0400255B RID: 9563
		internal InternalMemberTypeE memberTypeEnum;

		// Token: 0x0400255C RID: 9564
		internal InternalMemberValueE memberValueEnum;

		// Token: 0x0400255D RID: 9565
		internal Type dtType;

		// Token: 0x0400255E RID: 9566
		internal int numItems;

		// Token: 0x0400255F RID: 9567
		internal BinaryTypeEnum binaryTypeEnum;

		// Token: 0x04002560 RID: 9568
		internal object typeInformation;

		// Token: 0x04002561 RID: 9569
		internal int nullCount;

		// Token: 0x04002562 RID: 9570
		internal int memberLength;

		// Token: 0x04002563 RID: 9571
		internal BinaryTypeEnum[] binaryTypeEnumA;

		// Token: 0x04002564 RID: 9572
		internal object[] typeInformationA;

		// Token: 0x04002565 RID: 9573
		internal string[] memberNames;

		// Token: 0x04002566 RID: 9574
		internal Type[] memberTypes;

		// Token: 0x04002567 RID: 9575
		internal ParseRecord pr = new ParseRecord();
	}
}
