using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200076A RID: 1898
	[Serializable]
	internal sealed class SizedArray : ICloneable
	{
		// Token: 0x06005326 RID: 21286 RVA: 0x00124423 File Offset: 0x00122623
		internal SizedArray()
		{
			this.objects = new object[16];
			this.negObjects = new object[4];
		}

		// Token: 0x06005327 RID: 21287 RVA: 0x00124444 File Offset: 0x00122644
		internal SizedArray(int length)
		{
			this.objects = new object[length];
			this.negObjects = new object[length];
		}

		// Token: 0x06005328 RID: 21288 RVA: 0x00124464 File Offset: 0x00122664
		private SizedArray(SizedArray sizedArray)
		{
			this.objects = new object[sizedArray.objects.Length];
			sizedArray.objects.CopyTo(this.objects, 0);
			this.negObjects = new object[sizedArray.negObjects.Length];
			sizedArray.negObjects.CopyTo(this.negObjects, 0);
		}

		// Token: 0x06005329 RID: 21289 RVA: 0x001244C1 File Offset: 0x001226C1
		public object Clone()
		{
			return new SizedArray(this);
		}

		// Token: 0x17000DC1 RID: 3521
		internal object this[int index]
		{
			get
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						return null;
					}
					return this.negObjects[-index];
				}
				else
				{
					if (index > this.objects.Length - 1)
					{
						return null;
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
				object obj = this.objects[index];
				this.objects[index] = value;
			}
		}

		// Token: 0x0600532C RID: 21292 RVA: 0x00124558 File Offset: 0x00122758
		internal void IncreaseCapacity(int index)
		{
			try
			{
				if (index < 0)
				{
					int num = Math.Max(this.negObjects.Length * 2, -index + 1);
					object[] destinationArray = new object[num];
					Array.Copy(this.negObjects, 0, destinationArray, 0, this.negObjects.Length);
					this.negObjects = destinationArray;
				}
				else
				{
					int num2 = Math.Max(this.objects.Length * 2, index + 1);
					object[] destinationArray2 = new object[num2];
					Array.Copy(this.objects, 0, destinationArray2, 0, this.objects.Length);
					this.objects = destinationArray2;
				}
			}
			catch (Exception)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_CorruptedStream"));
			}
		}

		// Token: 0x04002597 RID: 9623
		internal object[] objects;

		// Token: 0x04002598 RID: 9624
		internal object[] negObjects;
	}
}
