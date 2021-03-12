using System;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000760 RID: 1888
	internal sealed class BinaryObjectWithMapTyped : IStreamable
	{
		// Token: 0x060052EA RID: 21226 RVA: 0x001234D7 File Offset: 0x001216D7
		internal BinaryObjectWithMapTyped()
		{
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x001234DF File Offset: 0x001216DF
		internal BinaryObjectWithMapTyped(BinaryHeaderEnum binaryHeaderEnum)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x001234F0 File Offset: 0x001216F0
		internal void Set(int objectId, string name, int numMembers, string[] memberNames, BinaryTypeEnum[] binaryTypeEnumA, object[] typeInformationA, int[] memberAssemIds, int assemId)
		{
			this.objectId = objectId;
			this.assemId = assemId;
			this.name = name;
			this.numMembers = numMembers;
			this.memberNames = memberNames;
			this.binaryTypeEnumA = binaryTypeEnumA;
			this.typeInformationA = typeInformationA;
			this.memberAssemIds = memberAssemIds;
			this.assemId = assemId;
			if (assemId > 0)
			{
				this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapTypedAssemId;
				return;
			}
			this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapTyped;
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x00123558 File Offset: 0x00121758
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte((byte)this.binaryHeaderEnum);
			sout.WriteInt32(this.objectId);
			sout.WriteString(this.name);
			sout.WriteInt32(this.numMembers);
			for (int i = 0; i < this.numMembers; i++)
			{
				sout.WriteString(this.memberNames[i]);
			}
			for (int j = 0; j < this.numMembers; j++)
			{
				sout.WriteByte((byte)this.binaryTypeEnumA[j]);
			}
			for (int k = 0; k < this.numMembers; k++)
			{
				BinaryConverter.WriteTypeInfo(this.binaryTypeEnumA[k], this.typeInformationA[k], this.memberAssemIds[k], sout);
			}
			if (this.assemId > 0)
			{
				sout.WriteInt32(this.assemId);
			}
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x0012361C File Offset: 0x0012181C
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.name = input.ReadString();
			this.numMembers = input.ReadInt32();
			this.memberNames = new string[this.numMembers];
			this.binaryTypeEnumA = new BinaryTypeEnum[this.numMembers];
			this.typeInformationA = new object[this.numMembers];
			this.memberAssemIds = new int[this.numMembers];
			for (int i = 0; i < this.numMembers; i++)
			{
				this.memberNames[i] = input.ReadString();
			}
			for (int j = 0; j < this.numMembers; j++)
			{
				this.binaryTypeEnumA[j] = (BinaryTypeEnum)input.ReadByte();
			}
			for (int k = 0; k < this.numMembers; k++)
			{
				if (this.binaryTypeEnumA[k] != BinaryTypeEnum.ObjectUrt && this.binaryTypeEnumA[k] != BinaryTypeEnum.ObjectUser)
				{
					this.typeInformationA[k] = BinaryConverter.ReadTypeInfo(this.binaryTypeEnumA[k], input, out this.memberAssemIds[k]);
				}
				else
				{
					BinaryConverter.ReadTypeInfo(this.binaryTypeEnumA[k], input, out this.memberAssemIds[k]);
				}
			}
			if (this.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMapTypedAssemId)
			{
				this.assemId = input.ReadInt32();
			}
		}

		// Token: 0x04002532 RID: 9522
		internal BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x04002533 RID: 9523
		internal int objectId;

		// Token: 0x04002534 RID: 9524
		internal string name;

		// Token: 0x04002535 RID: 9525
		internal int numMembers;

		// Token: 0x04002536 RID: 9526
		internal string[] memberNames;

		// Token: 0x04002537 RID: 9527
		internal BinaryTypeEnum[] binaryTypeEnumA;

		// Token: 0x04002538 RID: 9528
		internal object[] typeInformationA;

		// Token: 0x04002539 RID: 9529
		internal int[] memberAssemIds;

		// Token: 0x0400253A RID: 9530
		internal int assemId;
	}
}
