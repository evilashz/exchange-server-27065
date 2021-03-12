using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200076B RID: 1899
	[Serializable]
	internal sealed class IntSizedArray : ICloneable
	{
		// Token: 0x0600532D RID: 21293 RVA: 0x00124600 File Offset: 0x00122800
		public IntSizedArray()
		{
		}

		// Token: 0x0600532E RID: 21294 RVA: 0x00124624 File Offset: 0x00122824
		private IntSizedArray(IntSizedArray sizedArray)
		{
			this.objects = new int[sizedArray.objects.Length];
			sizedArray.objects.CopyTo(this.objects, 0);
			this.negObjects = new int[sizedArray.negObjects.Length];
			sizedArray.negObjects.CopyTo(this.negObjects, 0);
		}

		// Token: 0x0600532F RID: 21295 RVA: 0x0012469A File Offset: 0x0012289A
		public object Clone()
		{
			return new IntSizedArray(this);
		}

		// Token: 0x17000DC2 RID: 3522
		internal int this[int index]
		{
			get
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						return 0;
					}
					return this.negObjects[-index];
				}
				else
				{
					if (index > this.objects.Length - 1)
					{
						return 0;
					}
					return this.objects[index];
				}
			}
			set
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						this.IncreaseCapacity(index);
					}
					this.negObjects[-index] = value;
					return;
				}
				if (index > this.objects.Length - 1)
				{
					this.IncreaseCapacity(index);
				}
				this.objects[index] = value;
			}
		}

		// Token: 0x06005332 RID: 21298 RVA: 0x0012472C File Offset: 0x0012292C
		internal void IncreaseCapacity(int index)
		{
			try
			{
				if (index < 0)
				{
					int num = Math.Max(this.negObjects.Length * 2, -index + 1);
					int[] destinationArray = new int[num];
					Array.Copy(this.negObjects, 0, destinationArray, 0, this.negObjects.Length);
					this.negObjects = destinationArray;
				}
				else
				{
					int num2 = Math.Max(this.objects.Length * 2, index + 1);
					int[] destinationArray2 = new int[num2];
					Array.Copy(this.objects, 0, destinationArray2, 0, this.objects.Length);
					this.objects = destinationArray2;
				}
			}
			catch (Exception)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_CorruptedStream"));
			}
		}

		// Token: 0x04002599 RID: 9625
		internal int[] objects = new int[16];

		// Token: 0x0400259A RID: 9626
		internal int[] negObjects = new int[4];
	}
}
