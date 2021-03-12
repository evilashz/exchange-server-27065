using System;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200086B RID: 2155
	internal class ActivationAttributeStack
	{
		// Token: 0x06005C07 RID: 23559 RVA: 0x00141EC7 File Offset: 0x001400C7
		internal ActivationAttributeStack()
		{
			this.activationTypes = new object[4];
			this.activationAttributes = new object[4];
			this.freeIndex = 0;
		}

		// Token: 0x06005C08 RID: 23560 RVA: 0x00141EF0 File Offset: 0x001400F0
		internal void Push(Type typ, object[] attr)
		{
			if (this.freeIndex == this.activationTypes.Length)
			{
				object[] destinationArray = new object[this.activationTypes.Length * 2];
				object[] destinationArray2 = new object[this.activationAttributes.Length * 2];
				Array.Copy(this.activationTypes, destinationArray, this.activationTypes.Length);
				Array.Copy(this.activationAttributes, destinationArray2, this.activationAttributes.Length);
				this.activationTypes = destinationArray;
				this.activationAttributes = destinationArray2;
			}
			this.activationTypes[this.freeIndex] = typ;
			this.activationAttributes[this.freeIndex] = attr;
			this.freeIndex++;
		}

		// Token: 0x06005C09 RID: 23561 RVA: 0x00141F8D File Offset: 0x0014018D
		internal object[] Peek(Type typ)
		{
			if (this.freeIndex == 0 || this.activationTypes[this.freeIndex - 1] != typ)
			{
				return null;
			}
			return (object[])this.activationAttributes[this.freeIndex - 1];
		}

		// Token: 0x06005C0A RID: 23562 RVA: 0x00141FC0 File Offset: 0x001401C0
		internal void Pop(Type typ)
		{
			if (this.freeIndex != 0 && this.activationTypes[this.freeIndex - 1] == typ)
			{
				this.freeIndex--;
				this.activationTypes[this.freeIndex] = null;
				this.activationAttributes[this.freeIndex] = null;
			}
		}

		// Token: 0x04002937 RID: 10551
		private object[] activationTypes;

		// Token: 0x04002938 RID: 10552
		private object[] activationAttributes;

		// Token: 0x04002939 RID: 10553
		private int freeIndex;
	}
}
