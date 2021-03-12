using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x0200002B RID: 43
	internal sealed class ResultsCache<T> : IEnumerable<Result<!0>>, IEnumerable
	{
		// Token: 0x06000140 RID: 320 RVA: 0x0000647A File Offset: 0x0000467A
		public ResultsCache()
		{
			this.cache = new ResultsCache<T>.Cache<Result<T>>.EmptyCache(1);
			this.isComplete = false;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006495 File Offset: 0x00004695
		private ResultsCache(ResultsCache<T>.ICache<Result<T>> cache, bool isComplete)
		{
			this.cache = cache;
			this.isComplete = isComplete;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000064AB File Offset: 0x000046AB
		public int Count
		{
			get
			{
				return this.cache.ResultCount;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000064B8 File Offset: 0x000046B8
		public bool IsComplete
		{
			get
			{
				return this.isComplete;
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000064C0 File Offset: 0x000046C0
		public ResultsCache<T> AsCompleted()
		{
			if (this.IsComplete)
			{
				return this;
			}
			return new ResultsCache<T>(this.cache, true);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000064D8 File Offset: 0x000046D8
		public ResultsCache<T> Add(Result<T> item)
		{
			if (this.isComplete)
			{
				throw new InvalidOperationException();
			}
			return new ResultsCache<T>(this.cache.Add(item), this.isComplete);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000064FF File Offset: 0x000046FF
		public IEnumerable<Result<T>> Skip(int count)
		{
			return this.cache.Skip(count);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000650D File Offset: 0x0000470D
		public IEnumerator<Result<T>> GetEnumerator()
		{
			return this.cache.GetEnumerator();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000651A File Offset: 0x0000471A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.cache.GetEnumerator();
		}

		// Token: 0x04000071 RID: 113
		private readonly ResultsCache<T>.ICache<Result<T>> cache;

		// Token: 0x04000072 RID: 114
		private readonly bool isComplete;

		// Token: 0x0200002C RID: 44
		private interface ICache<U> : IEnumerable<U>, IEnumerable
		{
			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000149 RID: 329
			int ResultCount { get; }

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x0600014A RID: 330
			int ItemsPerListlette { get; }

			// Token: 0x0600014B RID: 331
			ResultsCache<T>.ICache<U> Add(U item);

			// Token: 0x0600014C RID: 332
			IEnumerable<U> Skip(int count);
		}

		// Token: 0x0200002D RID: 45
		private sealed class Cache<U> : ResultsCache<T>.ICache<U>, IEnumerable<U>, IEnumerable
		{
			// Token: 0x0600014D RID: 333 RVA: 0x00006527 File Offset: 0x00004727
			public Cache(ResultsCache<T>.Cache<U>.Listlette head, ResultsCache<T>.ICache<ResultsCache<T>.Cache<U>.Listlette> tail, int itemsPerListlette)
			{
				this.head = head;
				this.tail = tail;
				this.itemsPerListlette = itemsPerListlette;
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x0600014E RID: 334 RVA: 0x00006544 File Offset: 0x00004744
			public int ResultCount
			{
				get
				{
					return this.tail.ResultCount + this.head.Count * this.ItemsPerListlette;
				}
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x0600014F RID: 335 RVA: 0x00006564 File Offset: 0x00004764
			public int ItemsPerListlette
			{
				get
				{
					return this.itemsPerListlette;
				}
			}

			// Token: 0x06000150 RID: 336 RVA: 0x0000656C File Offset: 0x0000476C
			public ResultsCache<T>.ICache<U> Add(U item)
			{
				if (this.head.Count < 4)
				{
					return new ResultsCache<T>.Cache<U>(this.head.Add(item), this.tail, this.itemsPerListlette);
				}
				return new ResultsCache<T>.Cache<U>(new ResultsCache<T>.Cache<U>.One(item), this.tail.Add(this.head), this.itemsPerListlette);
			}

			// Token: 0x06000151 RID: 337 RVA: 0x00007338 File Offset: 0x00005538
			public IEnumerable<U> Skip(int count)
			{
				if (count > this.ResultCount)
				{
					throw new ArgumentOutOfRangeException();
				}
				int tailCount = this.tail.ResultCount;
				if (count < tailCount)
				{
					bool isFirstListlette = true;
					foreach (ResultsCache<T>.Cache<U>.Listlette listlette in this.tail.Skip(count))
					{
						if (isFirstListlette)
						{
							isFirstListlette = false;
							int tailSkip = count % this.tail.ItemsPerListlette / this.itemsPerListlette;
							switch (listlette.Count)
							{
							case 1:
								switch (tailSkip)
								{
								case 0:
									yield return listlette.Item1;
									break;
								}
								break;
							case 2:
								switch (tailSkip)
								{
								case 0:
									yield return listlette.Item1;
									yield return listlette.Item2;
									break;
								case 1:
									yield return listlette.Item2;
									break;
								}
								break;
							case 3:
								switch (tailSkip)
								{
								case 0:
									yield return listlette.Item1;
									yield return listlette.Item2;
									yield return listlette.Item3;
									break;
								case 1:
									yield return listlette.Item2;
									yield return listlette.Item3;
									break;
								case 2:
									yield return listlette.Item3;
									break;
								}
								break;
							case 4:
								switch (tailSkip)
								{
								case 0:
									yield return listlette.Item1;
									yield return listlette.Item2;
									yield return listlette.Item3;
									yield return listlette.Item4;
									break;
								case 1:
									yield return listlette.Item2;
									yield return listlette.Item3;
									yield return listlette.Item4;
									break;
								case 2:
									yield return listlette.Item3;
									yield return listlette.Item4;
									break;
								case 3:
									yield return listlette.Item4;
									break;
								}
								break;
							}
						}
						else
						{
							switch (listlette.Count)
							{
							case 1:
								yield return listlette.Item1;
								break;
							case 2:
								yield return listlette.Item1;
								yield return listlette.Item2;
								break;
							case 3:
								yield return listlette.Item1;
								yield return listlette.Item2;
								yield return listlette.Item3;
								break;
							case 4:
								yield return listlette.Item1;
								yield return listlette.Item2;
								yield return listlette.Item3;
								yield return listlette.Item4;
								break;
							}
						}
					}
				}
				int headSkipCount = (count < tailCount) ? 0 : ((count - tailCount) / this.itemsPerListlette);
				switch (this.head.Count)
				{
				case 1:
					switch (headSkipCount)
					{
					case 0:
						yield return this.head.Item1;
						break;
					}
					break;
				case 2:
					switch (headSkipCount)
					{
					case 0:
						yield return this.head.Item1;
						yield return this.head.Item2;
						break;
					case 1:
						yield return this.head.Item2;
						break;
					}
					break;
				case 3:
					switch (headSkipCount)
					{
					case 0:
						yield return this.head.Item1;
						yield return this.head.Item2;
						yield return this.head.Item3;
						break;
					case 1:
						yield return this.head.Item2;
						yield return this.head.Item3;
						break;
					case 2:
						yield return this.head.Item3;
						break;
					}
					break;
				case 4:
					switch (headSkipCount)
					{
					case 0:
						yield return this.head.Item1;
						yield return this.head.Item2;
						yield return this.head.Item3;
						yield return this.head.Item4;
						break;
					case 1:
						yield return this.head.Item2;
						yield return this.head.Item3;
						yield return this.head.Item4;
						break;
					case 2:
						yield return this.head.Item3;
						yield return this.head.Item4;
						break;
					case 3:
						yield return this.head.Item4;
						break;
					}
					break;
				}
				yield break;
			}

			// Token: 0x06000152 RID: 338 RVA: 0x0000735C File Offset: 0x0000555C
			public IEnumerator<U> GetEnumerator()
			{
				return this.Skip(0).GetEnumerator();
			}

			// Token: 0x06000153 RID: 339 RVA: 0x0000736A File Offset: 0x0000556A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000154 RID: 340 RVA: 0x00007374 File Offset: 0x00005574
			public override string ToString()
			{
				return string.Format("CACHE(Count:{0} IPL:{1} Tail:{2} Head:{3})", new object[]
				{
					this.ResultCount,
					this.ItemsPerListlette,
					this.tail,
					this.head
				});
			}

			// Token: 0x04000073 RID: 115
			private readonly ResultsCache<T>.Cache<U>.Listlette head;

			// Token: 0x04000074 RID: 116
			private readonly ResultsCache<T>.ICache<ResultsCache<T>.Cache<U>.Listlette> tail;

			// Token: 0x04000075 RID: 117
			private readonly int itemsPerListlette;

			// Token: 0x0200002E RID: 46
			public sealed class EmptyCache : ResultsCache<T>.ICache<U>, IEnumerable<!1>, IEnumerable
			{
				// Token: 0x06000155 RID: 341 RVA: 0x000073C1 File Offset: 0x000055C1
				public EmptyCache(int itemsPerListlette)
				{
					this.itemsPerListlette = itemsPerListlette;
				}

				// Token: 0x17000059 RID: 89
				// (get) Token: 0x06000156 RID: 342 RVA: 0x000073D0 File Offset: 0x000055D0
				public int ResultCount
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x1700005A RID: 90
				// (get) Token: 0x06000157 RID: 343 RVA: 0x000073D3 File Offset: 0x000055D3
				public int ItemsPerListlette
				{
					get
					{
						return this.itemsPerListlette;
					}
				}

				// Token: 0x06000158 RID: 344 RVA: 0x000073DB File Offset: 0x000055DB
				public ResultsCache<T>.ICache<U> Add(U item)
				{
					return new ResultsCache<T>.Cache<U>(new ResultsCache<T>.Cache<U>.One(item), new ResultsCache<T>.Cache<ResultsCache<T>.Cache<U>.Listlette>.EmptyCache(this.itemsPerListlette * 4), this.itemsPerListlette);
				}

				// Token: 0x06000159 RID: 345 RVA: 0x000074BC File Offset: 0x000056BC
				public IEnumerable<U> Skip(int count)
				{
					if (count != 0)
					{
						throw new InvalidOperationException();
					}
					yield break;
				}

				// Token: 0x0600015A RID: 346 RVA: 0x00007530 File Offset: 0x00005730
				public IEnumerator<U> GetEnumerator()
				{
					yield break;
				}

				// Token: 0x0600015B RID: 347 RVA: 0x00007594 File Offset: 0x00005794
				IEnumerator IEnumerable.GetEnumerator()
				{
					yield break;
				}

				// Token: 0x0600015C RID: 348 RVA: 0x000075B0 File Offset: 0x000057B0
				public override string ToString()
				{
					return string.Format("EMPTY(IPL:{0})", this.ItemsPerListlette);
				}

				// Token: 0x04000076 RID: 118
				private readonly int itemsPerListlette;
			}

			// Token: 0x0200002F RID: 47
			public abstract class Listlette
			{
				// Token: 0x1700005B RID: 91
				// (get) Token: 0x0600015D RID: 349
				public abstract U Item1 { get; }

				// Token: 0x1700005C RID: 92
				// (get) Token: 0x0600015E RID: 350
				public abstract U Item2 { get; }

				// Token: 0x1700005D RID: 93
				// (get) Token: 0x0600015F RID: 351
				public abstract U Item3 { get; }

				// Token: 0x1700005E RID: 94
				// (get) Token: 0x06000160 RID: 352
				public abstract U Item4 { get; }

				// Token: 0x1700005F RID: 95
				// (get) Token: 0x06000161 RID: 353
				public abstract int Count { get; }

				// Token: 0x06000162 RID: 354
				public abstract ResultsCache<T>.Cache<U>.Listlette Add(U item);
			}

			// Token: 0x02000030 RID: 48
			public sealed class One : ResultsCache<T>.Cache<U>.Listlette
			{
				// Token: 0x06000164 RID: 356 RVA: 0x000075CF File Offset: 0x000057CF
				public One(U item1)
				{
					this.item1 = item1;
				}

				// Token: 0x17000060 RID: 96
				// (get) Token: 0x06000165 RID: 357 RVA: 0x000075DE File Offset: 0x000057DE
				public override U Item1
				{
					get
					{
						return this.item1;
					}
				}

				// Token: 0x17000061 RID: 97
				// (get) Token: 0x06000166 RID: 358 RVA: 0x000075E6 File Offset: 0x000057E6
				public override U Item2
				{
					get
					{
						throw new IndexOutOfRangeException();
					}
				}

				// Token: 0x17000062 RID: 98
				// (get) Token: 0x06000167 RID: 359 RVA: 0x000075ED File Offset: 0x000057ED
				public override U Item3
				{
					get
					{
						throw new IndexOutOfRangeException();
					}
				}

				// Token: 0x17000063 RID: 99
				// (get) Token: 0x06000168 RID: 360 RVA: 0x000075F4 File Offset: 0x000057F4
				public override U Item4
				{
					get
					{
						throw new IndexOutOfRangeException();
					}
				}

				// Token: 0x17000064 RID: 100
				// (get) Token: 0x06000169 RID: 361 RVA: 0x000075FB File Offset: 0x000057FB
				public override int Count
				{
					get
					{
						return 1;
					}
				}

				// Token: 0x0600016A RID: 362 RVA: 0x000075FE File Offset: 0x000057FE
				public override ResultsCache<T>.Cache<U>.Listlette Add(U item)
				{
					return new ResultsCache<T>.Cache<U>.Two(this.item1, item);
				}

				// Token: 0x0600016B RID: 363 RVA: 0x0000760C File Offset: 0x0000580C
				public override string ToString()
				{
					return string.Format("ONE({0})", this.Item1);
				}

				// Token: 0x04000077 RID: 119
				private readonly U item1;
			}

			// Token: 0x02000031 RID: 49
			public sealed class Two : ResultsCache<T>.Cache<U>.Listlette
			{
				// Token: 0x0600016C RID: 364 RVA: 0x00007623 File Offset: 0x00005823
				public Two(U item1, U item2)
				{
					this.item1 = item1;
					this.item2 = item2;
				}

				// Token: 0x17000065 RID: 101
				// (get) Token: 0x0600016D RID: 365 RVA: 0x00007639 File Offset: 0x00005839
				public override U Item1
				{
					get
					{
						return this.item1;
					}
				}

				// Token: 0x17000066 RID: 102
				// (get) Token: 0x0600016E RID: 366 RVA: 0x00007641 File Offset: 0x00005841
				public override U Item2
				{
					get
					{
						return this.item2;
					}
				}

				// Token: 0x17000067 RID: 103
				// (get) Token: 0x0600016F RID: 367 RVA: 0x00007649 File Offset: 0x00005849
				public override U Item3
				{
					get
					{
						throw new IndexOutOfRangeException();
					}
				}

				// Token: 0x17000068 RID: 104
				// (get) Token: 0x06000170 RID: 368 RVA: 0x00007650 File Offset: 0x00005850
				public override U Item4
				{
					get
					{
						throw new IndexOutOfRangeException();
					}
				}

				// Token: 0x17000069 RID: 105
				// (get) Token: 0x06000171 RID: 369 RVA: 0x00007657 File Offset: 0x00005857
				public override int Count
				{
					get
					{
						return 2;
					}
				}

				// Token: 0x06000172 RID: 370 RVA: 0x0000765A File Offset: 0x0000585A
				public override ResultsCache<T>.Cache<U>.Listlette Add(U item)
				{
					return new ResultsCache<T>.Cache<U>.Three(this.item1, this.item2, item);
				}

				// Token: 0x06000173 RID: 371 RVA: 0x0000766E File Offset: 0x0000586E
				public override string ToString()
				{
					return string.Format("TWO({0},{1})", this.Item1, this.Item2);
				}

				// Token: 0x04000078 RID: 120
				private readonly U item1;

				// Token: 0x04000079 RID: 121
				private readonly U item2;
			}

			// Token: 0x02000032 RID: 50
			public sealed class Three : ResultsCache<T>.Cache<U>.Listlette
			{
				// Token: 0x06000174 RID: 372 RVA: 0x00007690 File Offset: 0x00005890
				public Three(U item1, U item2, U item3)
				{
					this.item1 = item1;
					this.item2 = item2;
					this.item3 = item3;
				}

				// Token: 0x1700006A RID: 106
				// (get) Token: 0x06000175 RID: 373 RVA: 0x000076AD File Offset: 0x000058AD
				public override U Item1
				{
					get
					{
						return this.item1;
					}
				}

				// Token: 0x1700006B RID: 107
				// (get) Token: 0x06000176 RID: 374 RVA: 0x000076B5 File Offset: 0x000058B5
				public override U Item2
				{
					get
					{
						return this.item2;
					}
				}

				// Token: 0x1700006C RID: 108
				// (get) Token: 0x06000177 RID: 375 RVA: 0x000076BD File Offset: 0x000058BD
				public override U Item3
				{
					get
					{
						return this.item3;
					}
				}

				// Token: 0x1700006D RID: 109
				// (get) Token: 0x06000178 RID: 376 RVA: 0x000076C5 File Offset: 0x000058C5
				public override U Item4
				{
					get
					{
						throw new IndexOutOfRangeException();
					}
				}

				// Token: 0x1700006E RID: 110
				// (get) Token: 0x06000179 RID: 377 RVA: 0x000076CC File Offset: 0x000058CC
				public override int Count
				{
					get
					{
						return 3;
					}
				}

				// Token: 0x0600017A RID: 378 RVA: 0x000076CF File Offset: 0x000058CF
				public override ResultsCache<T>.Cache<U>.Listlette Add(U item)
				{
					return new ResultsCache<T>.Cache<U>.Four(this.item1, this.item2, this.item3, item);
				}

				// Token: 0x0600017B RID: 379 RVA: 0x000076E9 File Offset: 0x000058E9
				public override string ToString()
				{
					return string.Format("THREE({0},{1},{2})", this.Item1, this.Item2, this.Item3);
				}

				// Token: 0x0400007A RID: 122
				private readonly U item1;

				// Token: 0x0400007B RID: 123
				private readonly U item2;

				// Token: 0x0400007C RID: 124
				private readonly U item3;
			}

			// Token: 0x02000033 RID: 51
			public sealed class Four : ResultsCache<T>.Cache<U>.Listlette
			{
				// Token: 0x0600017C RID: 380 RVA: 0x00007716 File Offset: 0x00005916
				public Four(U item1, U item2, U item3, U item4)
				{
					this.item1 = item1;
					this.item2 = item2;
					this.item3 = item3;
					this.item4 = item4;
				}

				// Token: 0x1700006F RID: 111
				// (get) Token: 0x0600017D RID: 381 RVA: 0x0000773B File Offset: 0x0000593B
				public override U Item1
				{
					get
					{
						return this.item1;
					}
				}

				// Token: 0x17000070 RID: 112
				// (get) Token: 0x0600017E RID: 382 RVA: 0x00007743 File Offset: 0x00005943
				public override U Item2
				{
					get
					{
						return this.item2;
					}
				}

				// Token: 0x17000071 RID: 113
				// (get) Token: 0x0600017F RID: 383 RVA: 0x0000774B File Offset: 0x0000594B
				public override U Item3
				{
					get
					{
						return this.item3;
					}
				}

				// Token: 0x17000072 RID: 114
				// (get) Token: 0x06000180 RID: 384 RVA: 0x00007753 File Offset: 0x00005953
				public override U Item4
				{
					get
					{
						return this.item4;
					}
				}

				// Token: 0x17000073 RID: 115
				// (get) Token: 0x06000181 RID: 385 RVA: 0x0000775B File Offset: 0x0000595B
				public override int Count
				{
					get
					{
						return 4;
					}
				}

				// Token: 0x06000182 RID: 386 RVA: 0x0000775E File Offset: 0x0000595E
				public override ResultsCache<T>.Cache<U>.Listlette Add(U item)
				{
					throw new InvalidOperationException();
				}

				// Token: 0x06000183 RID: 387 RVA: 0x00007768 File Offset: 0x00005968
				public override string ToString()
				{
					return string.Format("FOUR({0},{1},{2},{3})", new object[]
					{
						this.Item1,
						this.Item2,
						this.Item3,
						this.Item4
					});
				}

				// Token: 0x0400007D RID: 125
				private readonly U item1;

				// Token: 0x0400007E RID: 126
				private readonly U item2;

				// Token: 0x0400007F RID: 127
				private readonly U item3;

				// Token: 0x04000080 RID: 128
				private readonly U item4;
			}
		}
	}
}
