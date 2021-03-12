using System;
using System.Collections.Generic;
using Microsoft.Office.Story.V1.ImageAnalysis.RegionScanner;

namespace Microsoft.Office.Story.V1.ImageAnalysis.SalientObjectDetection
{
	// Token: 0x02000057 RID: 87
	internal class SaliencyMap
	{
		// Token: 0x0600029B RID: 667 RVA: 0x00007A66 File Offset: 0x00005C66
		public SaliencyMap(ArgbImage image, int patchSize, float scale) : this(new LabTiledImage(image, patchSize, scale))
		{
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00007A78 File Offset: 0x00005C78
		public SaliencyMap(LabTiledImage tiledImage)
		{
			if (tiledImage == null)
			{
				throw new ArgumentNullException("tiledImage");
			}
			this.TiledImage = tiledImage;
			this.Initialize();
			this.BuildNeighboringGraph();
			this.BoundaryAnalysis();
			this.InitScores();
			this.UpdateScores();
			this.NormalizeScores();
			this.CreateSaliencyMap();
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00007ACA File Offset: 0x00005CCA
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00007AD2 File Offset: 0x00005CD2
		public LabTiledImage TiledImage { get; private set; }

		// Token: 0x0600029F RID: 671 RVA: 0x00007ADC File Offset: 0x00005CDC
		private void Initialize()
		{
			this.patchNumberX = this.TiledImage.WidthInTiles;
			this.patchNumberY = this.TiledImage.HeightInTiles;
			this.patchesCount = this.TiledImage.AreaInTiles;
			int num = (this.patchNumberX + this.patchNumberY) * 2 - 4;
			this.scores = new float[this.patchesCount];
			this.neighborPatchIds = new int[this.patchesCount, 4];
			this.neighborPatchDists = new float[this.patchesCount, 4];
			this.boundaryIds = new int[num];
			this.boundaryBgScores = new float[num];
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00007B7C File Offset: 0x00005D7C
		private void BuildNeighboringGraph()
		{
			for (int i = 0; i < this.patchesCount; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					this.neighborPatchIds[i, j] = -1;
					this.neighborPatchDists[i, j] = float.MaxValue;
				}
			}
			int num = 0;
			for (int k = 0; k < this.patchNumberY; k++)
			{
				for (int l = 0; l < this.patchNumberX; l++)
				{
					if (l + 1 < this.patchNumberX)
					{
						int num2 = num + 1;
						float num3 = this.TiledImage[l, k].ComputeLabDistance(this.TiledImage[l + 1, k]);
						this.neighborPatchIds[num, 2] = num2;
						this.neighborPatchIds[num2, 1] = num;
						this.neighborPatchDists[num, 2] = num3;
						this.neighborPatchDists[num2, 1] = num3;
					}
					if (k + 1 < this.patchNumberY)
					{
						int num4 = num + this.patchNumberX;
						float num5 = this.TiledImage[l, k].ComputeLabDistance(this.TiledImage[l, k + 1]);
						this.neighborPatchIds[num, 3] = num4;
						this.neighborPatchIds[num4, 0] = num;
						this.neighborPatchDists[num, 3] = num5;
						this.neighborPatchDists[num4, 0] = num5;
					}
					num++;
				}
			}
			float num6 = 0f;
			int num7 = 0;
			for (int m = 0; m < this.patchesCount; m++)
			{
				float num8 = float.MaxValue;
				for (int n = 0; n < 4; n++)
				{
					num8 = Math.Min(num8, this.neighborPatchDists[m, n]);
				}
				if (num8 < 3.4028235E+38f)
				{
					num6 += num8;
					num7++;
				}
			}
			this.neighborDistThre = num6 / (float)num7;
			for (int num9 = 0; num9 < this.patchesCount; num9++)
			{
				for (int num10 = 0; num10 < 4; num10++)
				{
					if (this.neighborPatchDists[num9, num10] < 3.4028235E+38f)
					{
						this.neighborPatchDists[num9, num10] = Math.Max(0f, this.neighborPatchDists[num9, num10] - this.neighborDistThre);
					}
				}
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007DC4 File Offset: 0x00005FC4
		private void BoundaryAnalysis()
		{
			int num = 0;
			for (int i = 0; i < this.patchNumberX; i++)
			{
				this.boundaryIds[num] = i;
				num++;
			}
			for (int j = 1; j < this.patchNumberY; j++)
			{
				this.boundaryIds[num] = j * this.patchNumberX - 1;
				num++;
			}
			for (int k = this.patchNumberX - 2; k >= 0; k--)
			{
				this.boundaryIds[num] = (this.patchNumberY - 1) * this.patchNumberX + k;
				num++;
			}
			for (int l = this.patchNumberY - 2; l >= 1; l--)
			{
				this.boundaryIds[num] = l * this.patchNumberX;
				num++;
			}
			float num2 = 1f / (float)Math.Min(this.patchNumberX, this.patchNumberY);
			this.boundaryPatchNumber = (this.patchNumberX + this.patchNumberY) * 2 - 4;
			float[] array = new float[this.boundaryPatchNumber];
			float num3 = float.MaxValue;
			float num4 = 0f;
			for (int m = 0; m < this.boundaryPatchNumber; m++)
			{
				int tile = this.boundaryIds[m];
				for (int n = 0; n < this.boundaryPatchNumber; n++)
				{
					if (n == m)
					{
						array[n] = float.MaxValue;
					}
					else
					{
						int tile2 = this.boundaryIds[n];
						float num5 = this.TiledImage[tile].ComputeLabDistance(this.TiledImage[tile2]);
						int num6 = Math.Abs(m - n);
						num6 = Math.Min(num6, this.boundaryPatchNumber - num6);
						array[n] = num5 / (1f + 3f * (float)num6 * num2);
					}
				}
				Array.Sort<float>(array, Comparer<float>.Default);
				float num7 = 0f;
				for (int num8 = 0; num8 < 10; num8++)
				{
					num7 += array[num8];
				}
				num7 /= 10f;
				num4 = Math.Max(num4, num7);
				num3 = Math.Min(num3, num7);
				this.boundaryBgScores[m] = num7;
			}
			if (num4 - num3 > 1f)
			{
				float num9 = 1f / (num4 - num3);
				for (int num10 = 0; num10 < this.boundaryPatchNumber; num10++)
				{
					float num11 = (this.boundaryBgScores[num10] - num3) * num9;
					if (num11 >= 0.8f)
					{
						this.boundaryBgScores[num10] = float.MaxValue;
					}
					else if ((double)num11 < 0.5)
					{
						this.boundaryBgScores[num10] = 0f;
					}
				}
				return;
			}
			for (int num12 = 0; num12 < this.boundaryBgScores.Length; num12++)
			{
				this.boundaryBgScores[num12] = 0f;
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00008068 File Offset: 0x00006268
		private void InitScores()
		{
			for (int i = 0; i < this.patchesCount; i++)
			{
				this.scores[i] = float.MaxValue;
			}
			for (int j = 0; j < this.boundaryPatchNumber; j++)
			{
				int num = this.boundaryIds[j];
				this.scores[num] = this.boundaryBgScores[j];
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000080C0 File Offset: 0x000062C0
		private void UpdateScores()
		{
			bool[] array = new bool[this.patchesCount];
			Queue<int> queue = new Queue<int>();
			for (int i = 0; i < this.patchesCount; i++)
			{
				array[i] = false;
				if (this.scores[i] < 3.4028235E+38f)
				{
					queue.Enqueue(i);
					array[i] = true;
				}
			}
			while (queue.Count > 0)
			{
				int num = queue.Dequeue();
				array[num] = false;
				for (int j = 0; j < 4; j++)
				{
					int num2 = this.neighborPatchIds[num, j];
					if (num2 >= 0)
					{
						float num3 = this.scores[num] + this.neighborPatchDists[num, j];
						if (this.scores[num2] > num3)
						{
							this.scores[num2] = num3;
							if (!array[num2])
							{
								queue.Enqueue(num2);
								array[num2] = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00008190 File Offset: 0x00006390
		private void NormalizeScores()
		{
			float[] array = new float[this.patchesCount];
			this.scores.CopyTo(array, 0);
			int num = (int)((float)this.patchesCount * 0.02f);
			Array.Sort<float>(array, new ReverseComparer<float>(null));
			float num2 = array[num - 1];
			for (int i = 0; i < this.patchesCount; i++)
			{
				this.scores[i] = Math.Min(num2, this.scores[i]) / num2;
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00008200 File Offset: 0x00006400
		private void CreateSaliencyMap()
		{
			for (int i = 0; i < this.scores.Length; i++)
			{
				this.TiledImage[i].Saliency = this.scores[i];
			}
		}

		// Token: 0x040001CC RID: 460
		private const float OutOfRange = 3.4028235E+38f;

		// Token: 0x040001CD RID: 461
		private const int Neighborsize = 4;

		// Token: 0x040001CE RID: 462
		private int patchNumberX;

		// Token: 0x040001CF RID: 463
		private int patchNumberY;

		// Token: 0x040001D0 RID: 464
		private int patchesCount;

		// Token: 0x040001D1 RID: 465
		private int boundaryPatchNumber;

		// Token: 0x040001D2 RID: 466
		private float[] scores;

		// Token: 0x040001D3 RID: 467
		private int[,] neighborPatchIds;

		// Token: 0x040001D4 RID: 468
		private float[,] neighborPatchDists;

		// Token: 0x040001D5 RID: 469
		private float neighborDistThre;

		// Token: 0x040001D6 RID: 470
		private int[] boundaryIds;

		// Token: 0x040001D7 RID: 471
		private float[] boundaryBgScores;
	}
}
